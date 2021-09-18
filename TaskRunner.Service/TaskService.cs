using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskRunner.Data;
using TaskRunner.Dto.Models.TaskModel;
using TaskRunner.Dto.ViewModel;
using TaskRunner.Service.Map;
using TaskRunner.Service.Validation;

namespace TaskRunner.Service
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        public Exception _error { get; set; }

        public TaskService(ITaskRepository taskRepository)
        {
            this._taskRepository = taskRepository;
        }


        // Get all task
        public async Task<List<MyTaskViewModel>> GetAllTask()
        {
            List<MyTask> taskList = await _taskRepository.GetAllTask();
            if(taskList.Any()) return taskList.Select(c => new TaskMapper().mapMyTaskViewModel(c)).ToList();

            return new List<MyTaskViewModel>();
        }


        // Get by id task
        public async Task<MyTaskViewModel> GetMyTaskById(int id)
        {
            MyTask task = await _taskRepository.GetMyTaskById(id);
            if(task != null) return new TaskMapper().mapMyTaskViewModel(task);

            return new MyTaskViewModel();
        }


        // Save task
        public async Task<MyTaskViewModel> SaveMyTask(MyTaskViewModel task)
        {
            MyTask entity = await MapAndValidateMyTask(task);
            if (entity == null) throw new Exception("Some error occured. Please try again.");

            //save model
            MyTask savedTask = await SaveOrUpdateMyTask(entity);
            if(savedTask == null) throw _error;

            // mapping model to viewmodel
            MyTaskViewModel savedTaskVM = new TaskMapper().mapMyTaskViewModel(savedTask);

            return savedTaskVM;
        }


        // Update task
        public async Task<MyTaskViewModel> UpdateMyTask(MyTaskViewModel task)
        {
            MyTask entity = await MapAndValidateMyTask(task);
            if (entity == null) throw new Exception("Some error occured. Please try again.");

            //save model
            MyTask savedTask = await SaveOrUpdateMyTask(entity);
            if (savedTask == null) throw _error;

            // mapping model to viewmodel
            MyTaskViewModel savedTaskVM = new TaskMapper().mapMyTaskViewModel(savedTask);

            return savedTaskVM; ;
        }


        private async Task<MyTask> MapAndValidateMyTask(MyTaskViewModel task)
        {
            MyTask entity = new MyTask();

            if (task.Id == 0) // Save 
            {
                // mapping mytaskviewmodel to mytask
                entity = new TaskMapper().mapMyTaskViewModel(task);
                if (entity == null) throw new Exception("Some error occured. Please try again.");

                entity.CreatedOn = System.DateTime.Now;
            }
            else // Update
            {
                // get task from database by id
                entity = await _taskRepository.GetMyTaskById(task.Id);
                if (entity == null) throw new Exception("Some error occured. Please try again.");

                entity.Title = task.Title;
                entity.Description = task.Description;
                entity.Status = task.Status;
                entity.LastModifiedOn = System.DateTime.Now;
            }

            // Validating model
            ValidationResult result = new MyTaskValidation().Validate(entity);
            if (!result.IsValid) throw new Exception(result.ToString(" ~"));

            return entity;
        }


        private async Task<MyTask> SaveOrUpdateMyTask(MyTask task)
        {
            try
            {
                await _taskRepository.BEGIN_TRANSACTION();

                MyTask newTask = task.Id == 0 ? await _taskRepository.SaveMyTask(task) : await _taskRepository.UpdateMyTask(task);

                if (newTask == null)
                {
                    await _taskRepository.ROLL_BACK();
                    throw new Exception("Failed to save task");
                }

                await _taskRepository.COMMIT();
                return newTask;
            }
            catch(Exception ex)
            {
                _error = ex;
                await _taskRepository.ROLL_BACK();

                return null;
            }
        }


        // Delete task
        public async Task<Boolean> DeleteMyTask(int id)
        {
            return await _taskRepository.DeleteMyTask(id);
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using TaskRunner.Dto.Models.TaskModel;
using TaskRunner.Dto.ViewModel;

namespace TaskRunner.Service
{
    public interface ITaskService
    {
        Task<bool> DeleteMyTask(int id);
        Task<List<MyTaskViewModel>> GetAllTask();
        Task<MyTaskViewModel> GetMyTaskById(int id);
        Task<MyTaskViewModel> SaveMyTask(MyTaskViewModel entity);
        Task<MyTaskViewModel> UpdateMyTask(MyTaskViewModel entity);
    }
}
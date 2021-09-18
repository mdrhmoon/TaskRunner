using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskRunner.Dto.Models.TaskModel;

namespace TaskRunner.Data
{
    public class TaskRepository : ITaskRepository
    {
        public IConfiguration Config { get; }
        private readonly DbContext _context;
        public IDbContextTransaction transaction;

        public TaskRepository(IConfiguration config)
        {
            Config = config;
            this._context = new TaskContext(Config);
        }

        public async Task<Boolean> BEGIN_TRANSACTION()
        {
            transaction = await _context.Database.BeginTransactionAsync();
            return true;
        }

        public async Task<Boolean> COMMIT()
        {
            await transaction.CommitAsync();
            return true;
        }

        public async Task<Boolean> ROLL_BACK()
        {
            await transaction.RollbackAsync();
            return true;
        }

        // Get all task
        public async Task<List<MyTask>> GetAllTask()
        {
            return await new GenericRepository<MyTask>(_context).GetAll();
        }

        // Get by id task
        public async Task<MyTask> GetMyTaskById(int id)
        {
            return await new GenericRepository<MyTask>(_context).FindOne(i => i.Id == id);
        }

        // Save task
        public async Task<MyTask> SaveMyTask(MyTask entity)
        {
            return await new GenericRepository<MyTask>(_context).Insert(entity);
        }

        // Update task
        public async Task<MyTask> UpdateMyTask(MyTask entity)
        {
            return await new GenericRepository<MyTask>(_context).Update(entity, i => i.Id == entity.Id);
        }

        // Delete task
        public async Task<Boolean> DeleteMyTask(int id)
        {
            return await new GenericRepository<MyTask>(_context).Delete(i => i.Id == id);
        }
    }
}

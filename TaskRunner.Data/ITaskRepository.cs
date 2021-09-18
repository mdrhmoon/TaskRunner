using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskRunner.Dto.Models.TaskModel;

namespace TaskRunner.Data
{
    public interface ITaskRepository
    {
        IConfiguration Config { get; }

        Task<bool> BEGIN_TRANSACTION();
        Task<bool> COMMIT();
        Task<bool> DeleteMyTask(int id);
        Task<List<MyTask>> GetAllTask();
        Task<MyTask> GetMyTaskById(int id);
        Task<bool> ROLL_BACK();
        Task<MyTask> SaveMyTask(MyTask entity);
        Task<MyTask> UpdateMyTask(MyTask entity);
    }
}
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskRunner.Dto.Models.TaskModel;
using TaskRunner.Dto.ViewModel;

namespace TaskRunner.Service.Map
{
    public class TaskMapper
    {
        public MyTaskViewModel mapMyTaskViewModel(MyTask task)
        {
            var config = new MapperConfiguration(cfg =>{
               cfg.CreateMap<MyTask, MyTaskViewModel>();
           });

            return config.CreateMapper().Map<MyTask, MyTaskViewModel>(task);
        }

        public MyTask mapMyTaskViewModel(MyTaskViewModel task)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<MyTaskViewModel, MyTask>();
            });

            return config.CreateMapper().Map<MyTaskViewModel, MyTask>(task);
        }
    }
}

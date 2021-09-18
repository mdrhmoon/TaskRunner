using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskRunner.Dto.Models.TaskModel;

namespace TaskRunner.Service.Validation
{
    public class MyTaskValidation: AbstractValidator<MyTask>
    {
        public MyTaskValidation()
        {
            RuleFor(c => c.Title).NotNull().WithMessage("Title can not be null or empty.");
            RuleFor(c => c.Status).NotNull().WithMessage("Status can not be null or empty.");
            RuleFor(c => c.CreatedOn).NotNull().WithMessage("Create date can not be null or empty.");
        }
    }
}

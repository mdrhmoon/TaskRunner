using System;
using System.Collections.Generic;

#nullable disable

namespace TaskRunner.Dto.Models.TaskModel
{
    public partial class MyTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
}

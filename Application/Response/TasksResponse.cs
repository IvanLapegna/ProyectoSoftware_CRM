using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response
{
    public class TasksResponse
    {
        public Guid TaskID { get; set; }

        public string TaskName { get; set; }
        public DateTime TaskDueDate { get; set; }
        public Guid ProjectID { get; set; }

        public GenericResponse TaskStatus { get; set; }

        public UserResponse TaskUser { get; set; }

    }
}

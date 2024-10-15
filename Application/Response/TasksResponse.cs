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
        public Guid id { get; set; }

        public string name { get; set; }
        public DateTime dueDate { get; set; }
        public Guid projectId { get; set; }

        public GenericResponse status { get; set; }

        public UserResponse userAssigned { get; set; }

    }
}

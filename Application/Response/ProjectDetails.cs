using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response
{
    public class ProjectDetails
    {

        public ProjectsResponse data { get; set; }

        public ICollection<InteractionsResponse> Interactions { get; set; }

        public ICollection<TasksResponse> Tasks { get; set; }

    }
}

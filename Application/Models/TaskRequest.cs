using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Models
{
    public class TaskRequest
    {
        public string Name { get; set; }

        public DateTime DueDate { get; set; }

        public int user { get; set; }

        public int Status { get; set; }

        public void validacion()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new ArgumentNullException(nameof(Name));
            }
            else if (DueDate == default)
            {
                throw new ArgumentNullException(nameof(DueDate));
            }
            else if (user <= 0)
            {
                throw new ArgumentNullException(nameof(user));
            }
            else if (Status <= 0)
            {
                throw new ArgumentNullException(nameof(Status));
            }
        }
    }
}

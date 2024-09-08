using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Users
    {   
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public ICollection<Tasks> Tasks { get; set; }
    }
}

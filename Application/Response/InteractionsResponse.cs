using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response
{
    public class InteractionsResponse
    {
        public Guid InteractionID { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
        public Guid ProjectID { get; set; }
        public GenericResponse InteractionTypesObj { get; set; }

    }
}

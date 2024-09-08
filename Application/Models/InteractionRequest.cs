using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class InteractionRequest
    {
        public string Notes { get; set; }
        public DateTime Date { get; set; }
        public int InteractionType { get; set; }

        public void validacion()
        {
            if (string.IsNullOrWhiteSpace(Notes))
            {
                throw new ArgumentNullException(nameof(Notes));
            }
            else if (Date == default)
            {
                throw new ArgumentNullException(nameof(Date));
            }
            else if (InteractionType <= 0)
            {
                throw new ArgumentNullException(nameof(InteractionType));
            }
            
        }
    }
}

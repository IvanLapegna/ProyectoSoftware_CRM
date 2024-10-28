using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ProjectRequest
    {
        public string name { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public int? client { get; set; }

        public int? campaignType { get; set; }


        public void validacion()
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            else if (campaignType <= 0 || campaignType == null )
            {
                throw new ArgumentNullException(nameof(campaignType));
            }
            else if (client <= 0 || client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            else if (start == default)
            {
                throw new ArgumentException(nameof(start));
            }
            else if (end == default)
            {
                throw new ArgumentException(nameof(end));
            }
            else if (end < start)
            {
                throw new InvalidDataException("La fecha de finalización no puede ser anterior a la de inicio");
            }

        }
    }
}

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
        public string ProjectName { get; set; }

        public int? CampaignType { get; set; }

        public int? ClientID { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }


        public void validacion()
        {
            if (string.IsNullOrWhiteSpace(ProjectName))
            {
                throw new ArgumentNullException(nameof(ProjectName));
            }
            else if (CampaignType <= 0)
            {
                throw new ArgumentNullException(nameof(CampaignType));
            }
            else if (ClientID <= 0)
            {
                throw new ArgumentNullException(nameof(ClientID));
            }
            else if (StartDate == default)
            {
                throw new ArgumentException(nameof(StartDate));
            }
            else if (EndDate == default)
            {
                throw new ArgumentException(nameof(EndDate));
            }
            else if (EndDate < StartDate)
            {
                throw new InvalidDataException("La fecha de finalización no puede ser anterior a la de inicio");
            }

        }
    }
}

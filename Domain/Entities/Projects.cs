using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Projects
    {
        public Guid ProjectID { get; set; }
        public string ProjectName { get; set; }

        public int CampaignType { get; set; }
        public CampaignTypes CampaignTypeObj { get; set; }

        public int ClientID { get; set; }
        public Clients Client { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }


        public ICollection<Tasks> Tasks { get; set; }

        public ICollection<Interactions> Interactions { get; set; }

        
    }
}

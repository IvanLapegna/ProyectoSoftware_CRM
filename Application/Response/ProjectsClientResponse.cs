using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response
{
    public class ProjectsClientResponse
    {
        public Guid ProjectID { get; set; }
        public string ProjectName { get; set; }

        public int CampaignType { get; set; }

    }
}

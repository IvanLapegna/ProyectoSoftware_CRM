using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response
{
    public class ProjectsResponse
    {
        public Guid ID { get; set; }
        public string Name { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public ClientsResponse Client { get; set; }
        public GenericResponse CampaignType { get; set; }


        public static ProjectsResponse FromProject(Projects project)
        {
            return new ProjectsResponse
            {
                ID = project.ProjectID,
                Name = project.ProjectName,
                Start = project.StartDate,
                End = project.EndDate,
                Client = new ClientsResponse
                {
                    id = project.ClientID,
                    Name = project.Client.Name,
                    Email = project.Client.Email,
                    Company = project.Client.Company,
                    Phone = project.Client.Phone,
                    Address = project.Client.Address
                },
                CampaignType = new GenericResponse
                {
                    Id = project.CampaignType,
                    Name = project.CampaignTypeObj.Name,
                }
            };
        }

    }



}

using Domain.Entities;
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

        public static explicit operator ProjectDetails(Projects? project)
        {
            return new ProjectDetails
            {
                data = new ProjectsResponse
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
                },

                Interactions = project.Interactions.Select(i => new InteractionsResponse
                {
                    id = i.InteractionID,
                    Notes = i.Notes,
                    Date = i.Date,
                    ProjectId = project.ProjectID,
                    InteractionType = new GenericResponse
                    {
                        Id = i.InteractionType,
                        Name = i.InteractionTypesObj.Name,
                    }
                }).ToList(),

                Tasks = project.Tasks.Select(t => new TasksResponse
                {
                    id = t.TaskID,
                    name = t.Name,
                    dueDate = t.DueDate,
                    projectId = project.ProjectID,
                    status = new GenericResponse
                    {
                        Id = t.Status,
                        Name = t.TaskStatus.Name,
                    },
                    userAssigned = new UserResponse
                    {
                        id = t.AssignedTo,
                        name = t.User.Name,
                        email = t.User.Email,
                    }
                }).ToList(),
            };
        }
    }
}

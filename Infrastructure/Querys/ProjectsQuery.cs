using Application.Interfaces;
using Application.Response;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Querys
{
    public class ProjectsQuery : IProjectsQuery
    {

        private readonly AppDbContext _context;

        public ProjectsQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<ProjectsResponse>> GetAll(string? name, int? campaignType, int? client, int? offset, int? size)
        {
            var query = _context.Projects
                .Include(p => p.Client)
                .Include(p => p.CampaignTypeObj)
                .AsQueryable();

            if(!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.ProjectName.Contains(name));
            }

            if(!string.IsNullOrEmpty(campaignType.ToString())) 
            {
                query = query.Where(p => p.CampaignTypeObj.Id.Equals(campaignType));
            }

            if (!string.IsNullOrEmpty(client.ToString()))
            {
                query = query.Where(p => p.Client.ClientID.Equals(client));
            }

            if (offset != null) 
            {
                query = query.Skip(offset.Value);
            }

            if (size != null)
            {
                query = query.Skip(size.Value);
            }
            var project = await query.ToListAsync();

            var response = project.Select(project => new ProjectsResponse
            {
                ID = project.ProjectID,
                Name = project.ProjectName,
                Start = project.StartDate,
                End = project.EndDate,
                Client = new ClientsResponse
                {
                    ClientID = project.ClientID,
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
            }).ToList();

            return response;

        }


        public async Task<ProjectDetails> GetProject(Guid id)
        {
            var project = await _context.Projects
            .Include(p => p.Client)
            .Include(p => p.CampaignTypeObj)
            .Include(p => p.Tasks)
                    .ThenInclude(t => t.TaskStatus)
            .Include(p => p.Tasks)
                    .ThenInclude(t => t.User)
            .Include(p => p.Interactions)
                    .ThenInclude(i => i.InteractionTypesObj)
            .FirstOrDefaultAsync(p => p.ProjectID == id);

            if (project == null)
            {
                return null;
            }

            var details = new ProjectDetails
            {
                data = new ProjectsResponse
                {
                    ID = project.ProjectID,
                    Name = project.ProjectName,
                    Start = project.StartDate,
                    End = project.EndDate,
                    Client = new ClientsResponse
                    {
                        ClientID = project.ClientID,
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
                    InteractionID = i.InteractionID,
                    Notes = i.Notes,
                    Date = i.Date,
                    ProjectID = project.ProjectID,
                    InteractionTypesObj = new GenericResponse
                    {
                        Id = i.InteractionType,
                        Name = i.InteractionTypesObj.Name,
                    }
                }).ToList(),

                Tasks = project.Tasks.Select(t => new TasksResponse
                {
                    TaskID = t.TaskID,
                    TaskName = t.Name,
                    TaskDueDate = t.DueDate,
                    ProjectID = project.ProjectID,
                    TaskStatus = new GenericResponse
                    {
                        Id = t.Status,
                        Name = t.TaskStatus.Name,
                    },
                    TaskUser = new UserResponse
                    {
                        UserID = t.AssignedTo,
                        UserName = t.User.Name,
                        UserEmail = t.User.Email,
                    }
                }).ToList(),
            };


             return details;
            

        }

        public async Task<bool> ProjectNameExist(string name)
        {
            return await _context.Projects.AnyAsync(p => p.ProjectName == name);
        }


       
        public async Task<bool> ProjectExist(Guid id)
        {
            var exist = await _context.Projects.AnyAsync(p => p.ProjectID == id);
            if (exist)
            {
                return exist;
            }
            else 
            {
                throw new InvalidOperationException("No existe un proyecto con el id introducido");
            }
            
        }

    }
}

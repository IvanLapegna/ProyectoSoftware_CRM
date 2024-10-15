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

        public async Task<ICollection<Projects>> GetAll(string? name, int? campaign, int? client, int? offset, int? size)
        {
            var query = _context.Projects
                .Include(p => p.Client)
                .Include(p => p.CampaignTypeObj)
                .AsQueryable();

            if(!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.ProjectName.Contains(name));
            }

            if(!string.IsNullOrEmpty(campaign.ToString())) 
            {
                query = query.Where(p => p.CampaignTypeObj.Id.Equals(campaign));
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
                query = query.Take(size.Value);
            }
            var project = await query.ToListAsync();

            return project;

        }


        public async Task<Projects> GetProject(Guid id)
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


             return project;
            

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

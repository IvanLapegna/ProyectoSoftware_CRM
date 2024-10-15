using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Command
{
    public class ProjectsCommand: IProjectsCommand
    {
        private readonly AppDbContext _context;

        public ProjectsCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task insertProject(Projects project)
        {
            _context.Add(project);
            await _context.SaveChangesAsync();      
        }

        public async Task update(Projects project)
        {

            project.UpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }



    }
}

using Application.Interfaces;
using Application.Models;
using Application.Response;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Command
{
    public class TaskCommand: ITaskCommand
    {

        private readonly AppDbContext _context;

        public TaskCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Tasks> CreateTask(Projects project, Tasks task)
        {
           
            project.Tasks.Add(task);

            await _context.SaveChangesAsync();

            return task;

            
        }

        public async Task UpdateTask(Tasks task)
        {

            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();

        }
    }
}

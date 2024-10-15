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

        public async Task<Tasks> CreateTask(Projects project, TaskRequest request)
        {
            var task = new Tasks
            {
                Name = request.Name,
                DueDate = request.DueDate,
                AssignedTo = request.user,
                Status = request.Status,
                CreateDate = DateTime.Now,
            };

            project.Tasks.Add(task);

            await _context.SaveChangesAsync();

            return task;

            
        }

        public async Task UpdateTask(Tasks task, TaskRequest request)
        {

            task.Name = request.Name;
            task.DueDate = request.DueDate;
            task.AssignedTo = request.user;
            task.Status = request.Status;
            task.UpdateDate = DateTime.Now;

            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();

        }
    }
}

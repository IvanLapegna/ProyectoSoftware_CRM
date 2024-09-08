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

        public async Task<TasksResponse> CreateTask(Guid projectId, TaskRequest request)
        {
            var project = await _context.Projects
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.ProjectID == projectId);

            var taskStatus = await _context.TaskStatus
                .FirstOrDefaultAsync(t => t.Id == request.Status);

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserID == request.user);

            var task = new Tasks
            {
                Name = request.Name,
                DueDate = request.DueDate,
                AssignedTo = request.user,
                Status = request.Status,
            };

            project.Tasks.Add(task);
            await _context.SaveChangesAsync();

            var response = new TasksResponse
            {
                TaskID = task.TaskID,
                TaskName = task.Name,
                TaskDueDate = task.DueDate,
                ProjectID = projectId,
                TaskStatus = new GenericResponse
                {
                    Id = request.Status,
                    Name = task.TaskStatus.Name,
                },
                TaskUser = new UserResponse
                {
                    UserID = request.user,
                    UserName = user.Name,
                    UserEmail = user.Email,

                }

            };

            return response;
        }

        public async Task UpdateTask(Guid Id, TaskRequest request)
        {
            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.TaskID == Id);

            task.Name = request.Name;
            task.DueDate = request.DueDate;
            task.AssignedTo = request.user;
            task.Status = request.Status;

            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();

        }
    }
}

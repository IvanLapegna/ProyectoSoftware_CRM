﻿ using Application.Interfaces;
using Application.Response;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Querys
{
    public class TaskStatusQuery: ITaskStatusQuery
    {

        private readonly AppDbContext _context;

        public TaskStatusQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Domain.Entities.TaskStatus>> GetAll()
        {
            var taskStatus = await _context.TaskStatus.ToListAsync();
            

            return taskStatus;
        }

        public async Task<bool> existe(int id)
        {
            
            return await _context.TaskStatus.AnyAsync(t => t.Id == id);
        }

        public async Task<Domain.Entities.TaskStatus> GetStatus(int id)
        {
            var status = await _context.TaskStatus.FirstOrDefaultAsync(c => c.Id == id);
            return status;
        }


    }
}

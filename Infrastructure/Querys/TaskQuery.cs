using Application.Interfaces;
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
    public class TaskQuery: ITaskQuery
    {
        private readonly AppDbContext _context;

        public TaskQuery(AppDbContext context)
        {
            _context = context;
        }


        public async Task<Tasks> GetById(Guid id)
        {

            var task = await _context.Tasks
                .Include(t => t.TaskStatus)
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.TaskID == id);

            return task;



        }

        public async Task<bool> existe(Guid id)
        {

            return await _context.Tasks.AnyAsync(u => u.TaskID == id);
        }

    }
}

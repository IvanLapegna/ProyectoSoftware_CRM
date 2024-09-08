using Application.Interfaces;
using Application.Response;
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

        public async Task<ICollection<GenericResponse>> GetAll()
        {
            var taskStatus = await _context.TaskStatus.ToListAsync();
            ICollection<GenericResponse> list = new List<GenericResponse>();
            foreach (var x in taskStatus)
            {
                GenericResponse res = new GenericResponse()
                {
                    Id = x.Id,
                    Name = x.Name,

                };
                list.Add(res);


            };

            return list;
        }
    }
}

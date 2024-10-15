using Application.Interfaces;
using Application.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase
{
    public class TaskStatusService: ITaskStatusService
    {
        private readonly ITaskStatusQuery _taskStatusQuery;

        public TaskStatusService(ITaskStatusQuery taskStatusQuery)
        {
            _taskStatusQuery = taskStatusQuery;
        }

        public async Task<ICollection<GenericResponse>> GetAll()
        {
            var taskStatus = await _taskStatusQuery.GetAll();
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

        public async Task<bool> existe(int id)
        {
            return await _taskStatusQuery.existe(id);
        }

        
    }
}

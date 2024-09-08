using Application.Interfaces;
using Application.Response;
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
            return taskStatus;
        }
    }
}

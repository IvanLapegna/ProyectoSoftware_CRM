using Application.Models;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITaskCommand
    {
        Task<TasksResponse> CreateTask(Guid projectId, TaskRequest request);

        Task UpdateTask(Guid id, TaskRequest request);


    }
}

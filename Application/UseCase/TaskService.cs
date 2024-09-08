using Application.Interfaces;
using Application.Models;
using Application.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase
{
    public class TaskService: ITaskService
    {
        private readonly ITaskCommand _taskCommand;
        private readonly ITaskQuery _taskQuery;

        public TaskService(ITaskCommand taskCommand, ITaskQuery taskQuery)
        {
            _taskCommand = taskCommand;
            _taskQuery = taskQuery;
        }

        public async Task<TasksResponse> InsertTask(Guid projectId, TaskRequest request)
        {
            return await _taskCommand.CreateTask(projectId, request);

        }

        public async Task UpdateTask(Guid id, TaskRequest request)
        {
            await _taskCommand.UpdateTask(id , request);


        }

        public async Task<Tasks> GetById(Guid id)
        {
            return await _taskQuery.GetById(id);
        }


    }
}

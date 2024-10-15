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
        private readonly IUserService _userService;
        private readonly ITaskStatusService _taskStatusService;
        public TaskService(ITaskCommand taskCommand, ITaskQuery taskQuery, IUserService userService, ITaskStatusService taskStatusService)
        {
            _taskCommand = taskCommand;
            _taskQuery = taskQuery;
            _userService = userService;
            _taskStatusService = taskStatusService;
        }

        public async Task<TasksResponse> InsertTask(Projects project, TaskRequest request)
        {
            await _userService.existe(request.user);
            await _taskStatusService.existe(request.Status);
            var task = await _taskCommand.CreateTask(project, request);

            var response = new TasksResponse
            {
                id = task.TaskID,
                name = task.Name,
                dueDate = task.DueDate,
                projectId = project.ProjectID,
                status = new GenericResponse
                {
                    Id = request.Status,
                    Name = task.TaskStatus.Name,
                },
                userAssigned = new UserResponse
                {
                    id = request.user,
                    name = task.User.Name,
                    email = task.User.Email,

                }

            };
            return response;

        }

        public async Task UpdateTask(Guid id, TaskRequest request)
        {   
            var task = await _taskQuery.GetById(id);
            await _taskCommand.UpdateTask(task, request);


        }

        public async Task<Tasks> GetById(Guid id)
        {
            return await _taskQuery.GetById(id);
        }


    }
}

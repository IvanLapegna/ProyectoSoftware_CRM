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
    public class ProjectsService : IProjectsService
    {

        private readonly IProjectsCommand _command;
        private readonly IProjectsQuery _projectsQuery;
        private readonly IInteractionService _interactionService;
        private readonly ITaskService _taskService;
        private readonly ICampaignTypesService _campaignTypesService;
        private readonly IClientService _clientService;
        private readonly IInteractionTypesService _interactionTypesService;
        public ProjectsService(IProjectsCommand command, IProjectsQuery query, IInteractionService interactionService, ITaskService taskService, IClientService clientsService, ICampaignTypesService campaignTypesService, IInteractionTypesService interactionTypesService)
        {
            _command = command;
            _projectsQuery = query;
            _interactionService = interactionService;
            _taskService = taskService;
            _clientService = clientsService;
            _campaignTypesService = campaignTypesService;
            _interactionTypesService = interactionTypesService;
        }

        public async Task<ProjectDetails> CreateProject(ProjectRequest request)
        {
            //validaciones
            request.validacion();            
            await _campaignTypesService.existe(request.CampaignType.Value);
            await _clientService.existe(request.ClientID.Value);
            bool exist = await _projectsQuery.ProjectNameExist(request.ProjectName);
            if (exist) 
            {
                throw new InvalidOperationException("Ya existe un proyecto con el mismo nombre");
            
            }

            var project = new Projects
            {
                ProjectName = request.ProjectName,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                ClientID = request.ClientID.Value,
                CampaignType = request.CampaignType.Value,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,

            };
            await _command.insertProject(project);

            var Response = await _projectsQuery.GetProject(project.ProjectID);

            return Response;

            
            
        }

        public async Task<ICollection<ProjectsResponse>> GetAll(string? name, int? campaignType, int? client, int? offset, int? size)
        {
            var result = await _projectsQuery.GetAll(name, campaignType,client, offset, size);
            return result;

        }


        public async Task<ProjectDetails> GetById(Guid id)
        {
            await _projectsQuery.ProjectExist(id);
            var project = await _projectsQuery.GetProject(id);
            return project;
        }


        public async Task<InteractionsResponse> AddInteraction(Guid id, InteractionRequest request)
        {
            await _projectsQuery.ProjectExist(id);
            request.validacion();
            await _interactionTypesService.existe(request.InteractionType);
            return await _interactionService.InsertInteraction(id, request);
        }

        public async Task<TasksResponse> AddTask(Guid id, TaskRequest request)
        {
            await _projectsQuery.ProjectExist(id);
            request.validacion();
            return await _taskService.InsertTask(id, request);

        }


        public async Task<TasksResponse> UpdateTask(Guid id, TaskRequest request)
        {
            request.validacion();
            await _taskService.UpdateTask(id, request);
            var task = await _taskService.GetById(id);

            var response = new TasksResponse
            {
                TaskID = id,
                TaskName = task.Name,
                TaskDueDate = task.DueDate,
                ProjectID = task.ProjectID,
                TaskStatus = new GenericResponse
                {
                    Id = task.Status,
                    Name = task.TaskStatus.Name
                },
                TaskUser = new UserResponse
                {
                    UserID = task.AssignedTo,
                    UserName = task.User.Name,
                    UserEmail = task.User.Email,
                }
            };

            return response;

        }


    }
}

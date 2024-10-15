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
        private readonly IUserService _userService;
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
            };
            await _command.insertProject(project);

            var response = new ProjectDetails
            {
                data = new ProjectsResponse
                {
                    ID = project.ProjectID,
                    Name = project.ProjectName,
                    Start = project.StartDate,
                    End = project.EndDate,
                    Client = new ClientsResponse
                    {
                        id = project.ClientID,
                        Name = (await _clientService.GetById(project.ClientID)).Name,
                        Email = (await _clientService.GetById(project.ClientID)).Email,
                        Company = (await _clientService.GetById(project.ClientID)).Company,
                        Phone = (await _clientService.GetById(project.ClientID)).Phone,
                        Address = (await _clientService.GetById(project.ClientID)).Address
                    },
                    CampaignType = new GenericResponse
                    {
                        Id = project.CampaignType,
                        Name = (await _campaignTypesService.GetById(project.CampaignType)).Name
                    }
                },
                Interactions = new List<InteractionsResponse>(),
                Tasks = new List<TasksResponse>()
            };

            return response;


        }

        public async Task<ICollection<ProjectsResponse>> GetAll(string? name, int? campaign, int? client, int? offset, int? size)
        {
            var result = await _projectsQuery.GetAll(name, campaign,client, offset, size);
            var response = result.Select(ProjectsResponse.FromProject).ToList();
            return response;

        }


        public async Task<ProjectDetails> GetById(Guid id)
        {
            await _projectsQuery.ProjectExist(id);
            var project = await _projectsQuery.GetProject(id);
            var response = (ProjectDetails)project;
            return response;
        }


        public async Task<InteractionsResponse> AddInteraction(Guid id, InteractionRequest request)
        {
            await _projectsQuery.ProjectExist(id);
            request.validacion();
            bool typeExist = await _interactionTypesService.existe(request.InteractionType);
            if (!typeExist)
            {
                throw new InvalidOperationException("El tipo de interaccion ingresado no es valido");
            }


            var project = await _projectsQuery.GetProject(id);
            var response = await _interactionService.InsertInteraction(project, request);
            await _command.update(project);
            return response;
        }

        public async Task<TasksResponse> AddTask(Guid id, TaskRequest request)
        {
            await _projectsQuery.ProjectExist(id);
            request.validacion();
            var project = await _projectsQuery.GetProject(id);
            var result = await _taskService.InsertTask(project, request);
            await _command.update(project);
            return result;

        }


        public async Task<TasksResponse> UpdateTask(Guid id, TaskRequest request)
        {
            request.validacion();
            await _taskService.UpdateTask(id, request);
            var task = await _taskService.GetById(id);

            var response = new TasksResponse
            {
                id = id,
                name = task.Name,
                dueDate = task.DueDate,
                projectId = task.ProjectID,
                status = new GenericResponse
                {
                    Id = task.Status,
                    Name = task.TaskStatus.Name
                },
                userAssigned = new UserResponse
                {
                    id = task.AssignedTo,
                    name = task.User.Name,
                    email = task.User.Email,
                }
            };

            var project = await _projectsQuery.GetProject(task.ProjectID);
            await _command.update(project);
            return response;

        }


    }
}

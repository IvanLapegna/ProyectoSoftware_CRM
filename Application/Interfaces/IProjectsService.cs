using Application.Models;
using Application.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProjectsService
    {
        Task<ProjectDetails> CreateProject(ProjectRequest request);

        Task<ICollection<ProjectsResponse>> GetAll(string? name, int? campaignType, int? client, int? offset, int? size);

        Task<ProjectDetails> GetById(Guid id);

        Task<InteractionsResponse> AddInteraction(Guid projectId, InteractionRequest request);

        Task<TasksResponse> AddTask(Guid projectId, TaskRequest request);

        Task<TasksResponse> UpdateTask(Guid id, TaskRequest request);

    }
}

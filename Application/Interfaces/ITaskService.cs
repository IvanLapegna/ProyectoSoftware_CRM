using Application.Models;
using Application.Response;
using Application.UseCase;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITaskService
    {
        Task<TasksResponse> InsertTask(Projects project, TaskRequest requestt);

        Task UpdateTask(Guid id, TaskRequest request);
        Task<Tasks> GetById(Guid id);


    }
}

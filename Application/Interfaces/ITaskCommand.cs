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
    public interface ITaskCommand
    {
        Task<Tasks> CreateTask(Projects project, Tasks request);

        Task UpdateTask(Tasks task);


    }
}

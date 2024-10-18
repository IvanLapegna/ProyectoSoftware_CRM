using Application.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITaskStatusQuery
    {
        Task<ICollection<Domain.Entities.TaskStatus>> GetAll();

        Task<bool> existe(int id);
        Task<Domain.Entities.TaskStatus> GetStatus(int id);
    }
}

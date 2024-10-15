using Application.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProjectsQuery
    {
        Task<ICollection<Projects>> GetAll(string? name, int? campaignType, int? client, int? offset, int? size);

        Task<Projects> GetProject(Guid id);

        Task<bool> ProjectNameExist(string projectName);

        Task<bool> ProjectExist(Guid id);


    }
}

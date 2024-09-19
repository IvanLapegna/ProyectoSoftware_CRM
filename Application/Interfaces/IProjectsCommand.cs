using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProjectsCommand
    {
        Task insertProject(Projects project);
        Task update(Guid projectId);
    }
}

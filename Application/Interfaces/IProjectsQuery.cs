﻿using Application.Response;
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
        Task<ICollection<ProjectsResponse>> GetAll(string? name, int? campaignType, int? client, int? offset, int? size);

        Task<ProjectDetails> GetProject(Guid id);

        Task<bool> ProjectNameExist(string projectName);

        Task<bool> ProjectExist(Guid id);


    }
}
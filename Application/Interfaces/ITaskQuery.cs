﻿using Application.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITaskQuery
    {
        Task<Tasks> GetById(Guid id);
        Task<bool> existe(Guid id);
    }
}

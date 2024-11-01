﻿using Application.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<ICollection<UserResponse>> GetAll();

        Task<bool> existe(int id);
        Task<Users> GetById(int id);
    }
}

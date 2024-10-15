using Application.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserQuery
    {
        Task<ICollection<Users>> GetAll();

        Task<bool> existe(int id);

        Task<Users> GetUser(int id);


    }
}

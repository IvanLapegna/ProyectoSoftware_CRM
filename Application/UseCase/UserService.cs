using Application.Interfaces;
using Application.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.UseCase
{
    public class UserService : IUserService
    {
        private readonly IUserQuery _userQuey;

        public UserService(IUserQuery userQuey)
        {
            _userQuey = userQuey;
        }

        public async Task<ICollection<UserResponse>> GetAll()
        {
            var users = await _userQuey.GetAll();
            var response = users.Select(user => new UserResponse
            {
                id = user.UserID,
                name = user.Name,
                email = user.Email,
            }).ToList();
            return response;
        }

        public async Task<bool> existe(int id)
        {
            return await _userQuey.existe(id);
        }

        public async Task<Users> GetById(int id)
        {
            return await _userQuey.GetUser(id);
        }

        
    }
}

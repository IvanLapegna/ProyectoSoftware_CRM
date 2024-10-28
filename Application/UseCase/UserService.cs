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
        private readonly IUserQuery _userQuery;

        public UserService(IUserQuery userQuey)
        {
            _userQuery = userQuey;
        }

        public async Task<ICollection<UserResponse>> GetAll()
        {
            var users = await _userQuery.GetAll();
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
            var exist = await _userQuery.existe(id);

            if (!exist)
            {
                throw new InvalidOperationException("El id introducido para User no coincide con ningun registro");

            }
            return exist;
        }

        public async Task<Users> GetById(int id)
        {
            return await _userQuery.GetUser(id);
        }

        
    }
}

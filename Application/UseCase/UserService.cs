using Application.Interfaces;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return users;
        }

        public async Task<bool> existe(int id)
        {
            return await _userQuey.existe(id);
        }
    }
}

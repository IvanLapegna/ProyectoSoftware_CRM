using Application.Interfaces;
using Application.Response;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Querys
{
    public class UserQuery: IUserQuery
    {
        private readonly AppDbContext _context;

        public UserQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<UserResponse>> GetAll()
        {
            var users = await _context.Users
                .Select(users => new UserResponse
                {
                   UserID = users.UserID,
                   UserName = users.Name,
                   UserEmail = users.Email,
                }).ToListAsync();



            return users;
        }
    }
}

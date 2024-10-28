using Application.Interfaces;
using Application.Response;
using Domain.Entities;
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

        public async Task<ICollection<Users>> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }


        public async Task<bool> existe(int id)
        {
            
            return await _context.Users.AnyAsync(u => u.UserID == id);
        }

        public async Task<Users> GetUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(c => c.UserID == id);
            return user;
        }
    }
}

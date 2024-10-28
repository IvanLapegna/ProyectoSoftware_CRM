using Application.Interfaces;
using Application.Response;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Querys
{
    public class ClientQuery : IClientQuery
    {
        private readonly AppDbContext _context;

        public ClientQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Clients>> GetAll()
        {
            var clients = await _context.Clients.ToListAsync();
            return clients;
        }

        public async Task<bool> existe(int id)
        {
            return await _context.Clients.AnyAsync(c => c.ClientID == id);

        }
      
        public async Task<Clients> GetById(int id)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.ClientID == id);
            return client;


        }

    }
}

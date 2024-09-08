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

        public async Task<ICollection<ClientsResponse>> GetAll()
        {
            var clients = await _context.Clients
                .Select(client => new ClientsResponse 
                {
                    ClientID = client.ClientID,
                    Name = client.Name,
                    Email = client.Email,
                    Phone = client.Phone,
                    Company = client.Company,
                    Address = client.Address,

                }).ToListAsync();

                

            return clients;
        }

        public async Task<bool> existe(int id)
        {
            var exist = await _context.Clients.AnyAsync(c => c.ClientID == id);
            
            if (!exist)
            {
                throw new InvalidOperationException("El id introducido para Client no coincide con ningun registro");

            }
            return exist;
        }

    }
}

using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Command
{
    public class ClientsCommand : IClientsCommand
    {

        private readonly AppDbContext _context;

        public ClientsCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task insertClient(Clients clients)
        {          
            _context.Add(clients); 
            await _context.SaveChangesAsync();
        }

        
    }
}

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
    public class InteractionTypesQuery : IInteractionTypesQuery
    {
        private readonly AppDbContext _context;

        public InteractionTypesQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<InteractionTypes>> GetAll()
        {
            var interactionTypes = await _context.InteractionTypes.ToListAsync();
            

            return interactionTypes;
        }

        public async Task<bool> existe(int id)
        {
            
            return await _context.InteractionTypes.AnyAsync(c => c.Id == id);
        }

        public async Task<InteractionTypes> GetbyID(int id)
        {
            var type = await _context.InteractionTypes.FirstOrDefaultAsync(c => c.Id == id);
            return type;
        }


    }
}

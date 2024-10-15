using Application.Interfaces;
using Application.Models;
using Application.Response;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Command
{
    public class InteractionCommand: IInteractionCommand
    {

        private readonly AppDbContext _context;

        public InteractionCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Interactions> AddInteraction(Projects project, InteractionRequest request)
        {

            var interaction = new Interactions
            {
                Notes = request.Notes,
                Date = request.Date,
                InteractionType = request.InteractionType,
            };

            project.Interactions.Add(interaction);

            await _context.SaveChangesAsync();

            return interaction;
        }

    }
}

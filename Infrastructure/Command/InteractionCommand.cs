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

        public async Task<InteractionsResponse> AddInteraction(Guid projectId, InteractionRequest request)
        {
            var project = await _context.Projects
                .Include(p => p.Interactions)
                .FirstOrDefaultAsync(p => p.ProjectID == projectId);

            var interactionType = await _context.InteractionTypes
                .FirstOrDefaultAsync(i => i.Id == request.InteractionType);

            var interaction = new Interactions
            {
                Notes = request.Notes,
                Date = request.Date,
                ProjectID = projectId,
                InteractionType = request.InteractionType,
            };

            project.Interactions.Add(interaction);

            await _context.SaveChangesAsync();

            var response = new InteractionsResponse
            {
                InteractionID = interaction.InteractionID,
                Notes = interaction.Notes,
                Date = interaction.Date,
                ProjectID = projectId,
                InteractionTypesObj = new GenericResponse
                {
                    Id = request.InteractionType,
                    Name = interactionType.Name,
                }



            };

            return response;
        }

    }
}

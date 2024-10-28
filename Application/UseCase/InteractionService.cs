using Application.Interfaces;
using Application.Models;
using Application.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase
{
    public class InteractionService: IInteractionService
    {
        private readonly IInteractionCommand _interactionCommand;
        private readonly IInteractionTypesService _interactionTypesService;
        public InteractionService(IInteractionCommand interactionCommand, IInteractionTypesService interactionTypesService)
        {
            _interactionCommand = interactionCommand;
            _interactionTypesService = interactionTypesService;
        }

        public async Task<InteractionsResponse> InsertInteraction(Projects project, InteractionRequest request)
        {
         
            var interaction = await _interactionCommand.AddInteraction(project, request);
            var type = await _interactionTypesService.GetbyID(request.InteractionType);
            var response = new InteractionsResponse
            {
                id = interaction.InteractionID,
                Notes = interaction.Notes,
                Date = interaction.Date,
                ProjectId = project.ProjectID,
                InteractionType = new GenericResponse
                {
                    Id = request.InteractionType,
                    Name = type.Name,
                }
            };
            return response;

        }

    }
}

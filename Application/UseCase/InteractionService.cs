using Application.Interfaces;
using Application.Models;
using Application.Response;
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

        public InteractionService(IInteractionCommand interactionCommand)
        {
            _interactionCommand = interactionCommand;
        }

        public async Task<InteractionsResponse> InsertInteraction(Guid projectId, InteractionRequest request)
        {
            return await _interactionCommand.AddInteraction(projectId, request);

        }

    }
}

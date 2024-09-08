using Application.Interfaces;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase
{
    public class InteractionTypesService: IInteractionTypesService
    {
        private readonly IInteractionTypesQuery _interactionTypesQuery;

        public InteractionTypesService(IInteractionTypesQuery interactionTypesQuery)
        {
            _interactionTypesQuery = interactionTypesQuery;
        }

        public async Task<ICollection<GenericResponse>> GetAll()
        {
            var interactionTypes = await _interactionTypesQuery.GetAll();
            return interactionTypes;
        }

        public async Task<bool> existe(int id)
        {
            return await _interactionTypesQuery.existe(id);
        }
    }
}

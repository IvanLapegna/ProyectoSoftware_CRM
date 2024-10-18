using Application.Interfaces;
using Application.Response;
using Domain.Entities;
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
            ICollection<GenericResponse> list = new List<GenericResponse>();
            foreach (var x in interactionTypes)
            {
                GenericResponse res = new GenericResponse()
                {
                    Id = x.Id,
                    Name = x.Name,
                };
                list.Add(res);


            };
            return list;
        }

        public async Task<bool> existe(int id)
        {
            return await _interactionTypesQuery.existe(id);
        }

        public async Task<InteractionTypes> GetbyID(int id)
        {
            return await _interactionTypesQuery.GetbyID(id);
        }
    }
}

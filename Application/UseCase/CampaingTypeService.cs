using Application.Interfaces;
using Application.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase
{
    public class CampaingTypeService: ICampaignTypesService
    {
        private readonly ICampaignTypesQuery _campaignTypesQuery;

        public CampaingTypeService(ICampaignTypesQuery campaignTypesQuery)
        {
            _campaignTypesQuery = campaignTypesQuery;
        }

        public async Task<ICollection<GenericResponse>> GetAll()
        {
            var campaignTypes = await _campaignTypesQuery.GetAll();
            ICollection<GenericResponse> list = new List<GenericResponse>();
            foreach (var x in campaignTypes)
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
           return await _campaignTypesQuery.existe(id);
        }

        public async Task<GenericResponse> GetById(int id)
        {
            var campaignType = await _campaignTypesQuery.GetById(id);
            if (campaignType == null)
            {
                throw new InvalidOperationException("No existe un tipo de campaña con el id introducido");
            }
            return new GenericResponse
            {
                Id = campaignType.Id,
                Name = campaignType.Name
            };
        }



    }
}

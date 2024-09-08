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
            return campaignTypes;
        }

        public async Task<bool> existe(int id)
        {
           return await _campaignTypesQuery.existe(id);
        }

    }
}

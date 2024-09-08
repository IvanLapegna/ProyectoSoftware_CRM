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
using System.Xml.Linq;

namespace Infrastructure.Querys
{
    public class CampaingTypeQuery: ICampaignTypesQuery
    {
        private readonly AppDbContext _context;

        public CampaingTypeQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<GenericResponse>> GetAll()
        {
            var campaignTypes = await _context.CampaignTypes.ToListAsync();
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
            var exist = await _context.CampaignTypes.AnyAsync(c => c.Id == id);

            if (!exist)
            {
                throw new InvalidOperationException("El id introducido para campaignTypes no coincide con ningun registro");

            }
            return exist;
        }


    }
}

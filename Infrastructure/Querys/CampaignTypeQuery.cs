using Application.Interfaces;
using Application.Response;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Infrastructure.Querys
{
    public class CampaignTypeQuery: ICampaignTypesQuery
    {
        private readonly AppDbContext _context;

        public CampaignTypeQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<CampaignTypes>> GetAll()
        {
            var campaignTypes = await _context.CampaignTypes.ToListAsync();
            

            return campaignTypes;
        }

        public async Task<bool> existe(int id)
        {
            return await _context.CampaignTypes.AnyAsync(c => c.Id == id);

        }

        

        public async Task<CampaignTypes> GetById(int id)
        {
            var type = await _context.CampaignTypes.FirstOrDefaultAsync(c => c.Id == id);
            return type;


        }

    }
}

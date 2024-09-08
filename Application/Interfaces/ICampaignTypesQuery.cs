using Application.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICampaignTypesQuery
    {
        Task<ICollection<GenericResponse>> GetAll();

        Task<bool> existe(int id);

    }
}

using Application.Models;
using Application.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IInteractionService
    {
        Task<InteractionsResponse> InsertInteraction(Projects p,InteractionRequest request);

    }
}

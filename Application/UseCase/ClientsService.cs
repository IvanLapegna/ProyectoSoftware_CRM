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
    public class ClientsService : IClientService
    {
        private readonly IClientsCommand _clientCommand;

        private readonly IClientQuery _clientQuery;

        public ClientsService(IClientsCommand clientCommand, IClientQuery clientQuery)
        {
            _clientCommand = clientCommand;
            _clientQuery = clientQuery;
        }


        public async Task<ClientsResponse> CreateClient(ClientRequest request)
        {

            request.validaciones();

            var client = new Clients
            {
                Name = request.ClientName,
                Email = request.ClientEmail,
                Company = request.ClientCompany,
                Phone = request.ClientPhone,
                Address = request.ClientAddress,
                CreateDate = DateTime.Now
            };


            await _clientCommand.insertClient(client);

            return new ClientsResponse
            {
                ClientID = client.ClientID,
                Name = client.Name,
                Email = client.Email,
                Company = client.Company,
                Phone = client.Phone,
                Address = client.Address,

            };
        }

        public async Task<ICollection<ClientsResponse>> GetAll()
        {
            var clients = await _clientQuery.GetAll();
            return clients;
        }


        public async Task<bool> existe(int id)
        {
            return await _clientQuery.existe(id);
        }
    }
}

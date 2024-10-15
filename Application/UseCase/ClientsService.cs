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
                Name = request.name,
                Email = request.email,
                Company = request.company,
                Phone = request.phone,
                Address = request.address,
                CreateDate = DateTime.Now
            };


            await _clientCommand.insertClient(client);

            return new ClientsResponse
            {
                id = client.ClientID,
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
            var response = clients.Select(client => new ClientsResponse
            {
                id = client.ClientID,
                Name = client.Name,
                Email = client.Email,
                Phone = client.Phone,
                Company = client.Company,
                Address = client.Address,
            }).ToList();

            return response;
        }


        public async Task<bool> existe(int id)
        {
            return await _clientQuery.existe(id);
        }


        public async Task<ClientsResponse> GetById(int id)
        {
            var client = await _clientQuery.GetById(id);
            if (client == null)
            {
                throw new InvalidOperationException("No existe un cliente con el id introducido");
            }
            return new ClientsResponse
            {
                id = client.ClientID,
                Name = client.Name,
                Email = client.Email,
                Company = client.Company,
                Phone = client.Phone,
                Address = client.Address
            };
        }
    }
}

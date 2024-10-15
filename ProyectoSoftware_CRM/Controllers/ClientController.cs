using Application.Interfaces;
using Application.Models;
using Application.Response;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;

namespace ProyectoSoftware_CRM.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            var clientsResult = await _clientService.GetAll();
            return new JsonResult(clientsResult);
        }

        [HttpPost]
        public async Task<IActionResult> PostClient(ClientRequest request)
        {

            try
            {
                var clientsResult = await _clientService.CreateClient(request);
                return new JsonResult(clientsResult) { StatusCode = 201 };
            }

            catch (Exception ex)
            {
                return BadRequest(new ApiError(ex.Message));
            }

        }



    }
}

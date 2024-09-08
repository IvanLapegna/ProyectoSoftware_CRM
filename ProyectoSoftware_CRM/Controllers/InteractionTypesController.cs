using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoSoftware_CRM.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class InteractionTypesController : ControllerBase
    {
        private readonly IInteractionTypesService _interactionTypesService;

        public InteractionTypesController(IInteractionTypesService interactionTypesService)
        {
            _interactionTypesService = interactionTypesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInteractionTypes()
        {
            var result = await _interactionTypesService.GetAll();
            return new JsonResult(result);
        }



    }
}

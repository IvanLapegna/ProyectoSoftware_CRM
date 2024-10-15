using Application.Interfaces;
using Application.UseCase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoSoftware_CRM.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CampaingTypeController : ControllerBase
    {
        private readonly ICampaignTypesService _campaignTypesService;

        public CampaingTypeController(ICampaignTypesService campaignTypesService)
        {
            _campaignTypesService = campaignTypesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCampaignTypes()
        {
            var result = await _campaignTypesService.GetAll();
            return new JsonResult(result);
        }
    }
}

using Application.Interfaces;
using Application.Models;
using Application.Response;
using Application.UseCase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;

namespace ProyectoSoftware_CRM.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectsService _projectsService;

        public ProjectController(IProjectsService projectsService)
        {
            _projectsService = projectsService;
        }


        [HttpGet]

        public async Task<IActionResult> GetAll(string? name , int? campaign , int? client, int? offset, int? size = null)
        {
            var result = await _projectsService.GetAll(name ,campaign, client, offset, size);
            return new JsonResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostProject(ProjectRequest request)
        {
            try
            {
                var projectResult = await _projectsService.CreateProject(request);
                return new JsonResult(projectResult) { StatusCode = 201};
            }

            catch (Exception ex)
            {

                return BadRequest(new ApiError(ex.Message));
            }

        }


        

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(Guid id)
        {
            try
            {
                var result = await _projectsService.GetById(id);
                return new JsonResult(result);
            }

            catch (Exception ex)
            {

                return NotFound(new ApiError(ex.Message));
            }
            
        }

        [HttpPatch("{id}/interactions")]
        public async Task<IActionResult> AddInteraction(Guid id, InteractionRequest request)
        {
            try
            {
                var result = await _projectsService.AddInteraction(id, request);
                return new JsonResult(result) { StatusCode = 201 }; ;
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(ex.Message));
            }

        }

        [HttpPatch("{id}/tasks")]
        public async Task<IActionResult> AddTask(Guid id, TaskRequest request)
        {
            try
            {
                var result = await _projectsService.AddTask(id, request);
                return new JsonResult(result) { StatusCode = 201 }; ;
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(ex.Message));
            }
                   
        }


        [HttpPut("/api/v1/Tasks/{id}")]
        public async Task<IActionResult> UpdateTask(Guid id, TaskRequest request)
        {
            try
            {
                var result = await _projectsService.UpdateTask(id, request);
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(ex.Message));
            }
            
        }

    }
}

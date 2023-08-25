using Infrastructure.Utilities.ApiResponses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WS.Business.Implementations;
using WS.Business.Interfaces;
using WS.Model.Dtos.Employee;
using WS.Model.Dtos.Project;

namespace WS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : BaseController
    {
        private readonly IProjectBs _projectBs;
        public ProjectsController(IProjectBs projectBs)
        {
            _projectBs = projectBs;
        }
        [HttpGet]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<ProjectGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        public async Task<ActionResult> GetAllProjects()
        {
            var response = await _projectBs.GetProjectsAsync();

            return SendResponse(response);


        }

        [HttpGet("{id}")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<ProjectGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            var response = await _projectBs.GetByIdAsync(id);

            return SendResponse(response);

        }



        [HttpPost]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<ProjectPostDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        public async Task<ActionResult> SaveNewProject([FromBody] ProjectPostDto dto)
        {

            var response = await _projectBs.InsertAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = response.Data.ProjectID }, response.Data);
        }


        [HttpPut]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<ProjectPutDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        public async Task<ActionResult> UpdateProject([FromBody] ProjectPutDto dto)
        {
            var response = await _projectBs.UpdateAsync(dto);

            return SendResponse(response);
        }

        [HttpDelete("{id}")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<ProjectPutDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        public async Task<ActionResult> DeleteProject(int id)
        {
            var response = await _projectBs.DeleteAsync(id);

            return SendResponse(response);
        }
    }
}

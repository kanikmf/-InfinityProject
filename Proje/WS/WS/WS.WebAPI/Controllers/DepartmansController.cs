using AutoMapper;
using Infrastructure.Utilities.ApiResponses;
using Microsoft.AspNetCore.Mvc;
using WS.Business.Implementations;
using WS.Business.Interfaces;
using WS.Model.Dtos.Departman;

namespace WS.WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DepartmansController : BaseController
    {
        private readonly IDepartmanBs _departmanBs;
        public DepartmansController(IDepartmanBs departmanBs)
        {
            _departmanBs = departmanBs;
        }

        [HttpGet]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<DepartmanGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        public async Task<ActionResult> GetAllDepartman()
        {
            var response = await _departmanBs.GetDepartmansAsync();

            return SendResponse(response);


        }

        [HttpGet("{id}")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<DepartmanGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            var response = await _departmanBs.GetByIdAsync(id);

            return SendResponse(response);

        }



        [HttpPost]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<DepartmanPostDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        public async Task<ActionResult> SaveNewDepartmans([FromBody] DepartmanPostDto dto)
        {

            var response = await _departmanBs.InsertAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = response.Data.DepartmanId }, response.Data);
        }


        [HttpPut]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<DepartmanPutDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        public async Task<ActionResult> UpdateDepartman([FromBody] DepartmanPutDto dto)
        {
            var response = await _departmanBs.UpdateAsync(dto);

            return SendResponse(response);
        }

        [HttpDelete("{id}")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<DepartmanPutDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        public async Task<ActionResult> DeleteDepartman(int id)
        {
            var response = await _departmanBs.DeleteAsync(id);

            return SendResponse(response);
        }

    }
}

using AutoMapper;
using Infrastructure.Utilities.ApiResponses;
using Microsoft.AspNetCore.Mvc;
using WS.Business.Interfaces;
using WS.Model.Dtos.Employee;

namespace WS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController
    {

        private readonly IEmployeeBs _employeeBs;
        public EmployeesController(IEmployeeBs employeeBs)
        {
            _employeeBs = employeeBs;
        }

        [HttpGet]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<EmployeeGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        public async Task<ActionResult> GetAllEmployees()
        {
            var response = await _employeeBs.GetEmployeesAsync();

            return SendResponse(response);


        }

        [HttpGet("{id}")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<EmployeeGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            var response = await _employeeBs.GetByIdAsync(id);

            return SendResponse(response);

        }



        [HttpPost]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<EmployeePostDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        public async Task<ActionResult> SaveNewEmployee([FromBody] EmployeePostDto dto)
        {

            var response = await _employeeBs.InsertAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = response.Data.EmployeeID }, response.Data);
        }


        [HttpPut]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<EmployeePutDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        public async Task<ActionResult> UpdateEmployee([FromBody] EmployeePutDto dto)
        {
            var response = await _employeeBs.UpdateAsync(dto);

            return SendResponse(response);
        }

        [HttpDelete("{id}")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<EmployeePutDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            var response = await _employeeBs.DeleteAsync(id);

            return SendResponse(response);
        }
    }
}

using Infrastructure.Utilities.ApiResponses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WS.Business.Implementations;
using WS.Business.Interfaces;
using WS.Model.Dtos.Employee;
using WS.Model.Dtos.Supplier;

namespace WS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : BaseController
    {
        private readonly ISupplierBs _supplierBs;
        public SuppliersController(ISupplierBs supplierBs)
        {
            _supplierBs = supplierBs;
        }
        [HttpGet]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<SupplierGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        public async Task<ActionResult> GetAllSuppliers()
        {
            var response = await _supplierBs.GetSuppliersAsync();

            return SendResponse(response);


        }

        [HttpGet("{id}")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<SupplierGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            var response = await _supplierBs.GetByIdAsync(id);

            return SendResponse(response);

        }



        [HttpPost]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<SupplierPostDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        public async Task<ActionResult> SaveNewSupplier([FromBody] SupplierPostDto dto)
        {

            var response = await _supplierBs.InsertAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = response.Data.SupplierID }, response.Data);
        }


        [HttpPut]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<SupplierPutDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        public async Task<ActionResult> UpdateSupplier([FromBody] SupplierPutDto dto)
        {
            var response = await _supplierBs.UpdateAsync(dto);

            return SendResponse(response);
        }

        [HttpDelete("{id}")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<SupplierPutDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        public async Task<ActionResult> DeleteSupplier(int id)
        {
            var response = await _supplierBs.DeleteAsync(id);

            return SendResponse(response);
        }
    }
}

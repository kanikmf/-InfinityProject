using AutoMapper;
using Infrastructure.Utilities.ApiResponses;
using Microsoft.AspNetCore.Mvc;
using WS.Business.Implementations;
using WS.Business.Interfaces;
using WS.Model.Dtos.Client;

namespace WS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : BaseController
    {

        private readonly IClientBs _clientBs;
        public ClientsController(IClientBs clientBs)
        {
            _clientBs = clientBs;
        }

        [HttpGet]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<ClientGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        public async Task<ActionResult> GetAllClient()
        {
            var response = await _clientBs.GetClientsAsync();

            return SendResponse(response);


        }

        [HttpGet("{id}")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<ClientGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            var response = await _clientBs.GetByIdAsync(id);

            return SendResponse(response);

        }



        [HttpPost]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<ClientPostDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        public async Task<ActionResult> SaveNewClient([FromBody] ClientPostDto dto)
        {

            var response = await _clientBs.InsertAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = response.Data.ClientID }, response.Data);
        }


        [HttpPut]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<ClientPutDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        public async Task<ActionResult> UpdateClient([FromBody] ClientPutDto dto)
        {
            var response = await _clientBs.UpdateAsync(dto);

            return SendResponse(response);
        }

        [HttpDelete("{id}")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<ClientPutDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<NoData>))]
        public async Task<ActionResult> DeleteClient(int id)
        {
            var response = await _clientBs.DeleteAsync(id);

            return SendResponse(response);
        }









    }
}

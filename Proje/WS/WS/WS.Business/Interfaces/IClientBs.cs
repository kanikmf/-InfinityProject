using Infrastructure.Utilities.ApiResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Dtos.Client;
using WS.Model.Dtos.Departman;
using WS.Model.Entities;

namespace WS.Business.Interfaces
{
    public interface IClientBs
    {
        Task<ApiResponse<Client>> InsertAsync(ClientPostDto dto);
        Task<ApiResponse<NoData>> UpdateAsync(ClientPutDto dto);
        Task<ApiResponse<NoData>> DeleteAsync(int id);
        Task<ApiResponse<List<ClientGetDto>>> GetClientsAsync(params string[] includeList);
        Task<ApiResponse<ClientGetDto>> GetByIdAsync(int clientId, params string[] includeList);
    }
}

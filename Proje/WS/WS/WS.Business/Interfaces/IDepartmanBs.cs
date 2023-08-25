using Infrastructure.Utilities.ApiResponses;
using WS.Model.Dtos.Departman;
using WS.Model.Entities;

namespace WS.Business.Interfaces
{
    public interface IDepartmanBs
    {
       Task<ApiResponse<Departman>> InsertAsync(DepartmanPostDto dto);
       Task<ApiResponse<NoData>> UpdateAsync(DepartmanPutDto dto);
       Task<ApiResponse<NoData>> DeleteAsync(int id);
       Task<ApiResponse<List<DepartmanGetDto>>> GetDepartmansAsync(params string[] includeList);
       Task<ApiResponse<DepartmanGetDto>> GetByIdAsync(int departmanId, params string[] includeList);
    }
}

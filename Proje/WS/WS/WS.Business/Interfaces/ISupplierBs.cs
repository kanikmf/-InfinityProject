using Infrastructure.Utilities.ApiResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Dtos.Employee;
using WS.Model.Dtos.Supplier;
using WS.Model.Entities;

namespace WS.Business.Interfaces
{
    public interface ISupplierBs
    {
        Task<ApiResponse<Supplier>> InsertAsync(SupplierPostDto dto);
        Task<ApiResponse<NoData>> UpdateAsync(SupplierPutDto dto);
        Task<ApiResponse<NoData>> DeleteAsync(int id);
        Task<ApiResponse<SupplierGetDto>> GetByIdAsync(int supplierId, params string[] includeList);
        Task<ApiResponse<List<SupplierGetDto>>> GetSuppliersAsync(params string[] includeList);
    }
}

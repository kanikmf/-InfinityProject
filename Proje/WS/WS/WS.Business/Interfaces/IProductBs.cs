using Infrastructure.Utilities.ApiResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Dtos.Product;
using WS.Model.Entities;

namespace WS.Business.Interfaces
{
    public interface IProductBs
    {
        Task<ApiResponse<Product>> InsertAsync(ProductPostDto dto);
        Task<ApiResponse<NoData>> UpdateAsync(ProductPutDto dto);
        Task<ApiResponse<NoData>> DeleteAsync(int id);
        Task<ApiResponse<ProductGetDto>> GetByIdAsync(int productId, params string[] includeList);
        Task<ApiResponse<List<ProductGetDto>>> GetProductsAsync(params string[] includeList);
        Task<ApiResponse<List<ProductGetDto>>> GetProductsByUnitPriceAsync(decimal min, decimal max, params string[] includeList);
    }
}

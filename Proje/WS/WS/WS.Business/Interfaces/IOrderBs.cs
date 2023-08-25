using Infrastructure.Utilities.ApiResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Dtos.Order;
using WS.Model.Entities;

namespace WS.Business.Interfaces
{
    public interface IOrderBs
    {
        Task<ApiResponse<Order>> InsertAsync(OrderPostDto dto);
        Task<ApiResponse<NoData>> UpdateAsync(OrderPutDto dto);
        Task<ApiResponse<NoData>> DeleteAsync(int id);
        Task<ApiResponse<OrderGetDto>> GetByIdAsync(int orderId, params string[] includeList);
        Task<ApiResponse<List<OrderGetDto>>> GetOrdersAsync(params string[] includeList);
        Task<ApiResponse<List<OrderGetDto>>> GetOrdersByTotalAmountAsync(decimal min, decimal max, params string[] includeList);
    }
}

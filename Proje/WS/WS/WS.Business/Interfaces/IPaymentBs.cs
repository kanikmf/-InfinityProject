using Infrastructure.Utilities.ApiResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Dtos.Payment;
using WS.Model.Entities;

namespace WS.Business.Interfaces
{
    public interface IPaymentBs
    {
        Task<ApiResponse<Payment>> InsertAsync(PaymentPostDto dto);
        Task<ApiResponse<NoData>> UpdateAsync(PaymentPutDto dto);
        Task<ApiResponse<NoData>> DeleteAsync(int id);
        Task<ApiResponse<PaymentGetDto>> GetByIdAsync(int paymentId, params string[] includeList);
        Task<ApiResponse<List<PaymentGetDto>>> GetPaymentsAsync(params string[] includeList);
        Task<ApiResponse<List<PaymentGetDto>>> GetPaymentsByAmountAsync(decimal min, decimal max, params string[] includeList);
    }
}

using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Entities;

namespace WS.DataAccess.Interfaces
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        Task<Payment> GetByIdAsync(int paymentId, params string[] includeList);
        Task<Payment> GetByOrderIdAsync(int orderId, params string[] includeList);
        Task<List<Payment>> GetAmountAsync(Decimal min, Decimal max, params string[] includeList);
    }
}

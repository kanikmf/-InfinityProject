using Infrastructure.DataAccess;
using Infrastructure.DataAccess.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.DataAccess.Implementations.EF.Contex;
using WS.DataAccess.Interfaces;
using WS.Model.Entities;

namespace WS.DataAccess.Implementations.EF.Repositories
{
    public class PaymentRepository : BaseRepository<Payment, InfinityContext>, IPaymentRepository
    {
        public async Task<List<Payment>> GetAmountAsync(decimal min, decimal max, params string[] includeList)
        {
            return await GetAllAsync(prd => prd.Amount > min && prd.Amount < max, includeList);
        }

        public async Task<Payment> GetByIdAsync(int paymentId, params string[] includeList)
        {
            return await GetAsync(prd => prd.PaymentID == paymentId, includeList);
        }

        public async Task<Payment> GetByOrderIdAsync(int orderId, params string[] includeList)
        {
            return await GetAsync(prd => prd.OrderID == orderId, includeList);
        }
    }
}

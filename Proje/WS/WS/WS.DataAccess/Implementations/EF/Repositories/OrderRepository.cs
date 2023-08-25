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
    public class OrderRepository : BaseRepository<Order, InfinityContext>, IOrderRepository
    {
        public async Task<Order> GetByClientIdAsync(int clientId, params string[] includeList)
        {
            return await GetAsync(prd => prd.ClientID == clientId, includeList);
        }

        public async Task<Order> GetByIdAsync(int orderId, params string[] includeList)
        {
            return await GetAsync(prd => prd.OrderID == orderId, includeList);
        }

        public async Task<List<Order>> GetTotalAmountAsync(decimal min, decimal max, params string[] includeList)
        {
            return await GetAllAsync(prd => prd.TotalAmount > min && prd.TotalAmount < max, includeList);
        }
    }
}

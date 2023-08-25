using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Entities;

namespace WS.DataAccess.Interfaces
{
    public interface IOrderRepository  : IBaseRepository<Order>
    {
        Task<Order> GetByIdAsync(int orderId, params string[] includeList);
        Task<Order> GetByClientIdAsync(int clientId, params string[] includeList);
        Task<List<Order>> GetTotalAmountAsync(Decimal min, Decimal max, params string[] includeList);
    }
}

using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Entities;

namespace WS.DataAccess.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Product> GetByIdAsync(int productId, params string[] includeList);
        Task<List<Product>> GetByProductNameAsync(string productName, params string[] includeList);
        Task<List<Product>> GetByUnitPriceAsync(Decimal min, Decimal max, params string[] includeList);
    }
}

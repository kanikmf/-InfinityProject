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
    public class ProductRepository : BaseRepository<Product, InfinityContext>, IProductRepository
    {
        public  async Task<Product> GetByIdAsync(int productId, params string[] includeList)
        {
            return await GetAsync(prd => prd.ProductID == productId, includeList);
        }

        public async Task<List<Product>> GetByProductNameAsync(string productName, params string[] includeList)
        {
            return await GetAllAsync(emp => emp.ProductName == productName);
        }

        public async Task<List<Product>> GetByUnitPriceAsync(decimal min, decimal max, params string[] includeList)
        {
            return await GetAllAsync(prd => prd.UnitPrice > min && prd.UnitPrice < max, includeList);
        }
    }
}

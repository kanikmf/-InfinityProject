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
    public class SupplierRepository : BaseRepository<Supplier, InfinityContext>, ISupplierRepository
    {
        public async Task<List<Supplier>> GetByContactPersonAsync(string contactPerson, params string[] includeList)
        {
            return await GetAllAsync(emp => emp.ContactPerson == contactPerson);
        }

        public async Task<Supplier> GetByIdAsync(int supplierId, params string[] includeList)
        {
            return await GetAsync(prd => prd.SupplierID == supplierId, includeList);

        }

        public async Task<List<Supplier>> GetBySupplierNameAsync(string supplierName, params string[] includeList)
        {
            return await GetAllAsync(emp => emp.SupplierName == supplierName);
        }
    }
}

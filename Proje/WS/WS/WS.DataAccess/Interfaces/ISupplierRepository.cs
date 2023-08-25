using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Entities;

namespace WS.DataAccess.Interfaces
{
    public interface ISupplierRepository : IBaseRepository<Supplier>
    {
        Task<Supplier> GetByIdAsync(int supplierId, params string[] includeList);
        Task<List<Supplier>> GetBySupplierNameAsync(string supplierName, params string[] includeList);
        Task<List<Supplier>> GetByContactPersonAsync(string contactPerson, params string[] includeList);
    }
}

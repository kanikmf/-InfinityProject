using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Entities;

namespace WS.DataAccess.Interfaces
{
    public interface IClientRepository : IBaseRepository<Client>
    {
        Task<Client> GetByIdAsync(int clientId, params string[] includeList);
        Task<List<Client>> GetByClientNameAsync(string clientName, params string[] includeList);
    }
}

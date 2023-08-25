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
    public class ClientRepository : BaseRepository<Client, InfinityContext>, IClientRepository
    {
        public async Task<List<Client>> GetByClientNameAsync(string clientName, params string[] includeList)
        {
            return await GetAllAsync(emp => emp.ClientName == clientName);
        }

        public async Task<Client> GetByIdAsync(int clientId, params string[] includeList)
        {
            return await GetAsync(prd => prd.ClientID == clientId, includeList);
        }
    }
}

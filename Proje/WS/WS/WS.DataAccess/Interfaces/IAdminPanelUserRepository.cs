using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Entities;

namespace WS.DataAccess.Interfaces
{
    public interface IAdminPanelUserRepository : IBaseRepository<AdminPanelUser>
    {
        Task<AdminPanelUser> GetByUserNameAndPasswordAsync(string userName, string password, params string[] includeList);

    }
}

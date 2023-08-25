using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Entities;

namespace WS.DataAccess.Interfaces
{
    public interface IProjectRepository : IBaseRepository<Project>
    {
        Task<Project> GetByIdAsync(int projectId, params string[] includeList);
        Task<List<Project>> GetByProjectNameAsync(string projectName, params string[] includeList);
        Task<List<Project>> GetByStatusAsync(string status, params string[] includeList);
        Task<List<Project>> GetByBudgetAsync(Decimal min, Decimal max, params string[] includeList);
    }
}

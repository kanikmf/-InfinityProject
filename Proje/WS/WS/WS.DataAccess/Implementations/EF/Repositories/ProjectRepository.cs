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
    public class ProjectRepository : BaseRepository<Project, InfinityContext>, IProjectRepository
    {
        public async Task<List<Project>> GetByBudgetAsync(decimal min, decimal max, params string[] includeList)
        {
            return await GetAllAsync(prd => prd.Budget > min && prd.Budget < max, includeList);
        }

        public async Task<Project> GetByIdAsync(int projectId, params string[] includeList)
        {
            return await GetAsync(prd => prd.ProjectID == projectId, includeList);
        }

        public async Task<List<Project>> GetByProjectNameAsync(string projectName, params string[] includeList)
        {
            return await GetAllAsync(emp => emp.ProjectName == projectName);
        }

        public async Task<List<Project>> GetByStatusAsync(string status, params string[] includeList)
        {
            return await GetAllAsync(emp => emp.Status == status);
        }
    }
}

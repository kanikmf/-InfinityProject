using Infrastructure.DataAccess.EF;
using WS.DataAccess.Implementations.EF.Contex;
using WS.DataAccess.Interfaces;
using WS.Model.Entities;

namespace WS.DataAccess.Implementations.EF.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee , InfinityContext>, IEmployeeRepository
    {
        public async Task<List<Employee>> GetByRankAsync(string rank, params string[] includeList)
        {
            return await GetAllAsync(emp => emp.Rank == rank);
        }

        public async Task<Employee> GetByIdAsync(int employeeId, params string[] includeList)
        {
            return await GetAsync(prd => prd.EmployeeID == employeeId, includeList);
        }

        public async Task<List<Employee>> GetByFirstNameAsync(string firstname, params string[] includeList)
        {
            return await GetAllAsync(emp => emp.FirstName == firstname);
        }

        public async Task<List<Employee>> GetBySalaryAsync(decimal min, decimal max, params string[] includeList)
        {
            return await GetAllAsync(prd => prd.Salary > min && prd.Salary < max, includeList);
        }
    }
}

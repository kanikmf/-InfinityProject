using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Entities;

namespace WS.DataAccess.Interfaces
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        Task<Employee> GetByIdAsync(int employeeId, params string[] includeList);
        Task<List<Employee>> GetByFirstNameAsync(string firstname, params string[] includeList);
        Task<List<Employee>> GetByRankAsync(string rank, params string[] includeList);
        Task<List<Employee>> GetBySalaryAsync(Decimal min, Decimal max, params string[] includeList);
    }
}

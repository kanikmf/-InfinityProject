using Infrastructure.Utilities.ApiResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Dtos.Departman;
using WS.Model.Dtos.Employee;
using WS.Model.Entities;

namespace WS.Business.Interfaces
{
    public interface IEmployeeBs
    {
        Task<ApiResponse<Employee>> InsertAsync(EmployeePostDto dto);
        Task<ApiResponse<NoData>> UpdateAsync(EmployeePutDto dto);
        Task<ApiResponse<NoData>> DeleteAsync(int id);
        Task<ApiResponse<EmployeeGetDto>> GetByIdAsync(int employeeId, params string[] includeList);
        Task<ApiResponse<List<EmployeeGetDto>>> GetEmployeesAsync(params string[] includeList);
        Task<ApiResponse<List<EmployeeGetDto>>> GetEmployeesBySalaryAsync(decimal min, decimal max, params string[] includeList);
    }  
}

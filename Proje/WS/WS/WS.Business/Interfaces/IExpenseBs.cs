using Infrastructure.Utilities.ApiResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Dtos.Expense;
using WS.Model.Entities;

namespace WS.Business.Interfaces
{
    public interface IExpenseBs
    {
        Task<ApiResponse<Expense>> InsertAsync(ExpensePostDto dto);
        Task<ApiResponse<NoData>> UpdateAsync(ExpensePutDto dto);
        Task<ApiResponse<NoData>> DeleteAsync(int id);
        Task<ApiResponse<ExpenseGetDto>> GetByIdAsync(int expenseId, params string[] includeList);
        Task<ApiResponse<List<ExpenseGetDto>>> GetExpenseesAsync(params string[] includeList);
        Task<ApiResponse<List<ExpenseGetDto>>> GetExpensesByAmountAsync(decimal min, decimal max, params string[] includeList);
    }
}

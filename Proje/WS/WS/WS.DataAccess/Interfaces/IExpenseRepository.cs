using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Entities;

namespace WS.DataAccess.Interfaces
{
    public interface IExpenseRepository : IBaseRepository<Expense>
    {
        Task<Expense> GetByIdAsync(int expenseId, params string[] includeList);
        Task<List<Expense>> GetByCategoryAsync(string category, params string[] includeList);
        Task<List<Expense>> GetByAmountAsync(Decimal min, Decimal max, params string[] includeList);
    }
}

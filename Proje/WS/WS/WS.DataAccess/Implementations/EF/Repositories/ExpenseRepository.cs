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
    public class ExpenseRepository : BaseRepository<Expense, InfinityContext>, IExpenseRepository
    {
        public async Task<List<Expense>> GetByAmountAsync(decimal min, decimal max, params string[] includeList)
        {
            return await GetAllAsync(prd => prd.Amount > min && prd.Amount < max, includeList);

        }

        public async Task<List<Expense>> GetByCategoryAsync(string category, params string[] includeList)
        {
            return await GetAllAsync(emp => emp.Category == category);
        }

        public async Task<Expense> GetByIdAsync(int expenseId, params string[] includeList)
        {
            return await GetAsync(prd => prd.ExpenseID == expenseId, includeList);
        }
    }
}

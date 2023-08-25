using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.Model.Dtos.Expense
{
    public class ExpensePutDto : IDto
    {
        public int ExpenseID { get; set; }
        public DateTime? ExpenseDate { get; set; }
        public decimal? Amount { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }
}

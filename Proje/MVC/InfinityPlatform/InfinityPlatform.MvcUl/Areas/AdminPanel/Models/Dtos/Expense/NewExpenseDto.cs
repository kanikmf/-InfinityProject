namespace InfinityPlatform.MvcUl.Areas.AdminPanel.Models.Dtos.Expense
{
    public class NewExpenseDto
    {
        public DateTime? ExpenseDate { get; set; }
        public decimal? Amount { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }

    }
}

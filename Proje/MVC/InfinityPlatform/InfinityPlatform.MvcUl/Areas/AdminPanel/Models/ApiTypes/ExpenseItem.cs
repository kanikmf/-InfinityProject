namespace InfinityPlatform.MvcUl.Areas.AdminPanel.Models.ApiTypes
{
    public class ExpenseItem
    {
        public int ExpenseID { get; set; }
        public DateTime? ExpenseDate { get; set; }
        public decimal? Amount { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
    }
}

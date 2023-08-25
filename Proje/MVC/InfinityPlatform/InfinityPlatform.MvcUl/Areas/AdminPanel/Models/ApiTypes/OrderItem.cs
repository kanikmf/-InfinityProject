namespace InfinityPlatform.MvcUl.Areas.AdminPanel.Models.ApiTypes
{
    public class OrderItem
    {
        public int OrderID { get; set; }
        public int? ClientID { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public string Status { get; set; }
    }
}

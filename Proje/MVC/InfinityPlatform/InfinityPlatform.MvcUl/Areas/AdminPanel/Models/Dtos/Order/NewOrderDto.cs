namespace InfinityPlatform.MvcUl.Areas.AdminPanel.Models.Dtos.Order
{
    public class NewOrderDto
    {
        public int? ClientID { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public string Status { get; set; }
    }
}

namespace InfinityPlatform.MvcUl.Areas.AdminPanel.Models.ApiTypes
{
    public class PaymentItem
    {
        public int PaymentID { get; set; }
        public int? OrderID { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? Amount { get; set; }
        public string PaymentMethod { get; set; }
    }
}

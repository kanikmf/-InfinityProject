namespace InfinityPlatform.MvcUl.Areas.AdminPanel.Models.Dtos.Payment
{
    public class NewPaymentDto
    {
        public int? OrderID { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? Amount { get; set; }
        public string PaymentMethod { get; set; }
    }
}

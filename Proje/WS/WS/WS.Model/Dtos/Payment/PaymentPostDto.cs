using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.Model.Dtos.Payment
{
    public class PaymentPostDto : IDto
    {
        public int? OrderID { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? Amount { get; set; }
        public string PaymentMethod { get; set; }
    }
}

using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.Model.Dtos.Order
{
    public class OrderPutDto : IDto
    {
        public int OrderID { get; set; }
        public int? ClientID { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public string Status { get; set; }
        public bool? IsActive { get; set; }
    }
}

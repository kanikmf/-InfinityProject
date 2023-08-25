using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.Model.Dtos.Order
{
    public class OrderPostDto : IDto
    {
        public DateTime? OrderDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public string Status { get; set; }
    }
}

using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.Model.Dtos.Product
{
    public class ProductGetDto : IDto
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? StockQuantity { get; set; }
        public string Category { get; set; }
        public string? PhotoPath { get; set; }
        public bool? IsActive { get; set; }
    }
}

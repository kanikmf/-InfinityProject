using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.Model.Dtos.Product
{
    public class ProductPostDto : IDto
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string? PhotoPath { get; set; }

    }
}

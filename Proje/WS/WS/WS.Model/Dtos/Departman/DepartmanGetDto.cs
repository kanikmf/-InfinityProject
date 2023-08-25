using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.Model.Dtos.Departman
{
    public class DepartmanGetDto : IDto
    {
        public int DepartmanId { get; set; }
        public string? DepartmanName { get; set; }
        public string? Description { get; set; }
        public decimal? Budget { get; set; }
        public int? EmployeeCount { get; set; }
        public string? ContactInformation { get; set; }
    }
}

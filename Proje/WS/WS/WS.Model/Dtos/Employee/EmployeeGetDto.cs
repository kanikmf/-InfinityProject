using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.Model.Dtos.Employee
{
    public class EmployeeGetDto : IDto
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Beginning { get; set; }
        public decimal? Salary { get; set; }
        public string? Email { get; set; }
        public string? PhotoPath { get; set; }
        public string? Rank { get; set; }
    
    }
}

using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.Model.Dtos.Employee
{
    public class EmployeePutDto : IDto
    {
        public int DepartmanID { get; set; }
        public string FirstName { get; set; }
        public string? PhotoPath { get; set; }
        public decimal? Salary { get; set; }
    }
}

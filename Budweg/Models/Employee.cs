using System;
using System.Collections.Generic;
using System.Text;

namespace Budweg.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Initials { get; set; }
        public string Department { get; set; }

        public Employee()
        {
        }
    }
}

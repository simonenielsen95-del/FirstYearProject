using System;
using System.Collections.Generic;
using System.Text;

namespace NEFAB.Domains
{
    public class Employee
    {
      
            public string EmployeeID { get; set; }
            public string EmployeeName { get; set; }
        public string? Name { get; internal set; }

        public Employee(string employeeID)
            {
                EmployeeID = employeeID; 
        }
        
    }
}

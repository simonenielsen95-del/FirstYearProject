using System;
using System.Collections.Generic;
using System.Text;

namespace NEFAB.Services
{
    internal class EmployeeService
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeeService(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
    }

}

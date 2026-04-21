using System;
using System.Collections.Generic;
using System.Text;
using NEFAB.Repositories;

namespace NEFAB.Services
{
    internal class EmployeeService
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeeService(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public void AddEmployee(string employeeName)
        {
            _employeeRepository.Add(employeeName)
        }
    }

}

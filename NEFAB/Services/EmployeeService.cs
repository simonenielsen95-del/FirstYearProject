using NEFAB.Domains;
using NEFAB.Repositories;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using NEFAB.Repositories.Interfaces;

namespace NEFAB.Services
{
    public class EmployeeService
    {
        private readonly IRepoGetAddUpdate<Employee, string> _employeeRepository;

        public EmployeeService(IRepoGetAddUpdate<Employee, string> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public void Add(string employeeID, string employeeName)
        {
            if (string.IsNullOrEmpty(employeeID) || string.IsNullOrEmpty(employeeName))
                throw new ArgumentException("Udfyld venligst både EmployeeID og Name.");

            Employee newEmployee = new Employee(employeeID, employeeName)
            {
                EmployeeID = employeeID,
                EmployeeName = employeeName
            };

            _employeeRepository.Add(newEmployee);
        }

        public void Update(string employeeID, string newEmployeeID, string employeeName)
        {
            if (string.IsNullOrEmpty(employeeID) || string.IsNullOrEmpty(employeeName) || string.IsNullOrEmpty(newEmployeeID))
            {
                throw new ArgumentException("Udfyld venligst både EmployeeID, Nyt EmployeeID og Name.");
            }
           
            Employee? employee = _employeeRepository.GetByID(employeeID);
            if (employee == null)
            {
                throw new Exception($"Employee {employeeID} blev ikke fundet.");
            }

            employee.EmployeeID = newEmployeeID;
            employee.EmployeeName = employeeName;

            _employeeRepository.Update(employee);
        }
    }
}


   
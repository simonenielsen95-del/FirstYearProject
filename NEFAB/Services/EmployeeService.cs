using NEFAB.Domains;
using NEFAB.Repositories;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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

        public void Add(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee), "Du skal først vælge en medarbejder.");
            }

            if (string.IsNullOrWhiteSpace(employee.Name))
            {
                throw new ArgumentException("Medarbejderens navn må ikke være tomt.", nameof(employee));
            }

            if (string.IsNullOrWhiteSpace(employee.EmployeeID))
            {
                throw new ArgumentException("Medarbejderens ID må ikke være tomt.", nameof(employee));
            }

             _employeeRepository.Add(employee);
        }

        public void Update(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee), "Du skal først vælge en medarbejder.");
            }
            if (string.IsNullOrWhiteSpace(employee.Name))
            {
                throw new ArgumentException("Medarbejderens navn må ikke være tomt.", nameof(employee));
            }
            if (string.IsNullOrWhiteSpace(employee.EmployeeID))
            {
                throw new ArgumentException("Medarbejderens ID må ikke være tomt.", nameof(employee));
            }

            _employeeRepository.Update(employee);
        }

        public List<Employee> GetAll()
        {
            return _employeeRepository.GetAll();
        }

        public Employee? GetByID(string EmployeeID)
        {
            return _employeeRepository.GetByID(EmployeeID);
        }
    }
}




   
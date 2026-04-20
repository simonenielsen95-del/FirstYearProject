using System;
using System.Collections.Generic;
using System.Text;

namespace NEFAB.Repositories
{
    public class EmployeeRepository
    {
        private readonly string ConnectionString;
        private List<Employee> Employees;

        public EmployeeRepository()
        {
           IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Employees = new List<Employee>();
            ConnectionString = config.GetConnectionString("MyDBConnection");
        }
    }
}

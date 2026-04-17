using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Data.SqlClient;
using NEFAB.Domains;
using Microsoft.Extensions.Configuration;

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

        public List<Employee> GetAll()
        {
            //Her returner Employees der allerede ligger i databasen
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT EmployeeID, EmployeeName FROM Employees";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string employeeID = reader.GetString(0);
                    string employeeName = reader.GetString(1);
                    Employee employee = new Employee(employeeID)
                    {
                        EmployeeName = employeeName
                    };
                    Employees.Add(employee);
                }
            }
            return Employees;
        }

        public void Add(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO EMPLOYEES (EmployeeID, EmployeeName) VALUES (@EmployeeID, @EmployeeName);", con))
                {
                    cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
                    cmd.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                    cmd.ExecuteNonQuery();
                }
            }
            Employees.Add(employee);
        }

        public void RemoveAll(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Employees WHERE EmployeeID = @EmployeeID;", con))
                {
                    cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
                    cmd.ExecuteNonQuery();
                }
            }
            //Fjern employee fra den lokale liste, så den ikke længere vises i UI'et
            Employees.RemoveAll(e => e.EmployeeID == employee.EmployeeID);
        }

        public Employee GetByEmployeeID(string EmployeeID)
        {
            Employee employee = null;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT EmployeeID, EmployeeName FROM Employees WHERE EmployeeID = @EmployeeID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string employeeName = reader.GetString(1);
                    employee = new Employee(EmployeeID)
                    {
                        EmployeeName = employeeName
                    };
                }
            }
            return employee;
        }
    }

}




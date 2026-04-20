using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NEFAB.Domains;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Windows.Navigation;

namespace NEFAB.Repositories
{
    public class EmployeeRepository
    {
        private readonly string connectionString;
        private List<Employee> employees;

        public EmployeeRepository()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json")
                 .Build();

            employees = new List<Employee>();
            connectionString = config.GetConnectionString("MyDBConnection");
        }

        public List<Employee> GetAll()
        {
            List<Employee> employees = new List<Employee>();
            //Her returner Employees der allerede ligger i databasen
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT EmployeeID, EmployeeName FROM Employees", con);
                using (SqlDataReader dr = cmd.ExecuteReader())
                    while (dr.Read())
                    {
                        Employee employee = new Employee(dr.GetString(0))
                        {
                            EmployeeName = dr.GetString(1)
                        };

                        employees.Add(employee);
                    }
            }
            return employees;
        }

        public void Add(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO EMPLOYEES (EmployeeID, EmployeeName) VALUES (@EmployeeID, @EmployeeName);", con))
                {
                    cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
                    cmd.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                    cmd.ExecuteNonQuery();
                }
            }
            employees.Add(employee);
        }

        public void Update(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE EMPLOYEES SET EmployeeName = @EmployeeName WHERE EmployeeID = @EmployeeID;", con))
                {
                    cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
                    cmd.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                    cmd.ExecuteNonQuery();
                }
            }
        }




        public Employee GetByID(string EmployeeID)

        {
            Employee employee = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT EmployeeID, EmployeeName FROM Employees WHERE EmployeeID = @EmployeeID", con);
                cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        employee = new Employee(dr.GetString(0))
                        {
                            EmployeeName = dr.GetString(1)
                        };
                    }
                }
            }
            return employee;
        }
    }
}

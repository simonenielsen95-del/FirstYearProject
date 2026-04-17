using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NEFAB.Domains; 

namespace NEFAB.Repositories
{
    public class ContainerRepository : IContainerRepository
    {
        private readonly string ConnectionString;
        private List<Container> containers;

        public ContainerRepository()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            containers = new List<Container>();

            ConnectionString = config.GetConnectionString("MyDBConnection");
        }
         

        public List<Container> GetAll()
        {

            //returner containere der allerede ligger i databasen
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT ContainerNo, Week, Year FROM Containers";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string containerNo = reader.GetString(0);
                    int week = reader.GetInt32(1);
                    int year = reader.GetInt32(2);
                    Container container = new Container(containerNo)
                    {
                        Week = week,
                        Year = year
                    };
                    containers.Add(container);
                }
            }

            return containers;
        }

        public void Add(Container container)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Containers (ContainerNo, Week, Year) VALUES (@ContainerNo, @Week, @Year);",
                    con))
                {
                    cmd.Parameters.Add("@ContainerNo", SqlDbType.NVarChar, 50).Value = container.ContainerNo;
                    cmd.Parameters.Add("@Week", SqlDbType.Int).Value = container.Week;
                    cmd.Parameters.Add("@Year", SqlDbType.Int).Value = container.Year;

                    cmd.ExecuteNonQuery();
                }
            }
            containers.Add(container);
        }

        public void Remove(Container container) 

        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Containers WHERE ContainerNo = @ContainerNo", con))
                {
                    cmd.Parameters.Add("@ContainerNo", SqlDbType.NVarChar, 50).Value = container.ContainerNo;
                    cmd.ExecuteNonQuery();
                }
            }

            //Fjern også fra local liste
            containers.RemoveAll(c => c.ContainerNo == container.ContainerNo);
        
        }
        
        public Container GetByContainerNumber(string containerNo)
        {
            // check in-memory cache first
            var found = containers.FirstOrDefault(c => c.ContainerNo == containerNo);
            if (found != null) return found;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT ContainerNo, Week, Year FROM Containers WHERE ContainerNo = @ContainerNo";
                using var command = new SqlCommand(query, connection);
                command.Parameters.Add("@ContainerNo", SqlDbType.NVarChar, 50).Value = containerNo;
                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string dbContainerNo = reader.GetString(0);
                    int week = reader.GetInt32(1);
                    int year = reader.GetInt32(2);
                    var container = new Container(dbContainerNo) { Week = week, Year = year };
                    containers.Add(container);
                    return container;
                }
            }

            return null;
        }

        public List<Container> GetByWeek(int week, int year)
        {
            var results = containers.Where(c => c.Week == week && c.Year == year).ToList();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT ContainerNo, Week, Year FROM Containers WHERE Week = @Week AND Year = @Year";
                using var command = new SqlCommand(query, connection);
                command.Parameters.Add("@Week", SqlDbType.Int).Value = week;
                command.Parameters.Add("@Year", SqlDbType.Int).Value = year;
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string containerNo = reader.GetString(0);
                    int w = reader.GetInt32(1);
                    int y = reader.GetInt32(2);
                    if (!results.Any(c => c.ContainerNo == containerNo))
                    {
                        var container = new Container(containerNo) { Week = w, Year = y };
                        results.Add(container);
                        containers.Add(container);
                    }
                }
            }

            return results;
        }

        
        public void Update(Container container)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE Containers SET Week = @Week, Year = @Year WHERE ContainerNo = @ContainerNo",
                    con))
                {
                    cmd.Parameters.Add("@ContainerNo", SqlDbType.NVarChar, 50).Value = container.ContainerNo;
                    cmd.Parameters.Add("@Week", SqlDbType.Int).Value = container.Week;
                    cmd.Parameters.Add("@Year", SqlDbType.Int).Value = container.Year;

                    cmd.ExecuteNonQuery();
                }

            }


            //Opdaterer listen lokalt 
            Container containerToUpdate = containers.FirstOrDefault(c => c.ContainerNo == container.ContainerNo);
            if (containerToUpdate != null)
            {
                containerToUpdate.Week = container.Week;
                containerToUpdate.Year = container.Year;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using NEFAB.Domains;

namespace NEFAB.Repositories
{
    public class ContainerRepository
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


        public List<Container> GetAllContainers()
        {
            //using (SqlConnection connection = new SqlConnection(ConnectionString))
            //{
            //    connection.Open();
            //    string query = "SELECT ContainerNumber, Week, Year FROM Containers";
            //    SqlCommand command = new SqlCommand(query, connection);
            //    SqlDataReader reader = command.ExecuteReader();
            //    while (reader.Read())
            //    {
            //        string containerNumber = reader.GetString(0);
            //        int week = reader.GetInt32(1);
            //        int year = reader.GetInt32(2);
            //        Container container = new Container(containerNumber)
            //        {
            //            Week = week,
            //            Year = year
            //        };
            //        containers.Add(container);
            //    }
            //}

            return containers;
        }

        ////        + <<create>> ContainerRepository()
        ////+ Add(container: Container)
        ////+ Remove(container: Container)
        ////+ Update(container: Container)
        ////+ GetAllContainers() : List<Container> 
        ////+ GetByContainerNumber(containerNumber: String) : Container
        ////+ GetByWeek(week: Integer, year: Integer) : List<Container>
        ///

       public void CreateContainer(Container container)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO DeliveryNote (StartQuantity, ItemNo) " +
                     "VALUES(@StartQuantity,@ItemNo); " +
                      "SELECT @@IDENTITY", con))
                {
                    cmd.Parameters.Add("@StartQuantity", SqlDbType.Int).Value = deliveryNote.StartQuantity;
                    cmd.Parameters.Add("@ItemNo", SqlDbType.Int).Value = deliveryNote.ItemNo;
                    deliveryNote.OrderNo = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

    }
}

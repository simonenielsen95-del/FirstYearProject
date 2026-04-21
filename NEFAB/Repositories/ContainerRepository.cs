using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NEFAB.Domains; 

namespace NEFAB.Repositories
{
    public class ContainerRepository
    {
        private readonly string connectionString;

       
        private List<Container> containers;

        public ContainerRepository()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

          containers = new List<Container>();

            connectionString = config.GetConnectionString("MyDBConnection");
        }
         

        public List<Container> GetAll()//henter og gemmer dem i cachen
        {
            //kun inde i metoden. forsvinder når metoden er færdig, så vi ikke har forældet data i cachen
            //den gemmer ikke data til næste gang
            //men skal bruges fordi metoden skal returnere en list<container>
            List<Container> result = new List<Container>();
            //returner containere der allerede ligger i databasen
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT ContainerNo, Week, Year FROM CONTAINER", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Container container = new Container(dr.GetString(0))
                    {
                        Week = dr.GetInt32(1),
                        Year = dr.GetInt32(2)
                    };
                   result.Add(container);
                }
            }

          return result;
        }

        public void Add(Container container)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO CONTAINER (ContainerNo, Week, Year) VALUES (@ContainerNo, @Week, @Year);",
                    con))
                {
                    cmd.Parameters.Add("@ContainerNo", SqlDbType.NVarChar).Value = container.ContainerNo; 
                    cmd.Parameters.Add("@Week", SqlDbType.Int).Value = container.Week;
                    cmd.Parameters.Add("@Year", SqlDbType.Int).Value = container.Year;
             

                    cmd.ExecuteNonQuery();
                }
            }
           containers.Add(container);
        }

        public void Remove(Container container) 

        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM CONTAINER WHERE ContainerNo = @ContainerNo", con))
                {
                    cmd.Parameters.Add("@ContainerNo", SqlDbType.NVarChar).Value = container.ContainerNo; 
                    cmd.ExecuteNonQuery();
                }
            }

            
        }

        public Container? GetByContainerNumber(string containerNo)
        {

           
            Container? container = null; 
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using var cmd = new SqlCommand("SELECT ContainerNo, Week, Year FROM CONTAINER WHERE ContainerNo = @ContainerNo", con);
                cmd.Parameters.Add("@ContainerNo", SqlDbType.NVarChar).Value = containerNo;
                using var dr = cmd.ExecuteReader();
                {
                    if (dr.Read())
                    {
                        container = new Container(dr.GetString(0)) // skal være her
                        {
                        
                        Week = dr.GetInt32(1), 
                        Year = dr.GetInt32(2) 
                        };
                        
                       

                    }
                }

               
            }
            return container;
        }

        public List<Container> GetByWeek(int week, int year)
        {

            List<Container> containers = new List<Container>();

           

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using var cmd = new SqlCommand("SELECT ContainerNo, Week, Year FROM CONTAINER WHERE Week = @Week AND Year = @Year", con);
                using (SqlDataReader dr = cmd.ExecuteReader()) 
                while (dr.Read())
                {
                    Container container = new Container(dr.GetString(0)) // skal være her
                    {
                 
                    Week = dr.GetInt32(1), // Week = dr.GetInt32(1),
                    Year = dr.GetInt32(2) // Year =dr.GetInt32(2)
                    };
                    containers.Add(container);
                    
                    
                }
            }

            return containers;
        }

        
        public void Update(Container container)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE CONTAINER SET Week = @Week, Year = @Year WHERE ContainerNo = @ContainerNo",
                    con))
                {
                    cmd.Parameters.Add("@ContainerNo", SqlDbType.NVarChar).Value = container.ContainerNo; 
                    cmd.Parameters.Add("@Week", SqlDbType.Int).Value = container.Week;
                    cmd.Parameters.Add("@Year", SqlDbType.Int).Value = container.Year;

                    cmd.ExecuteNonQuery(); 
                }

            }

        }
    }
}

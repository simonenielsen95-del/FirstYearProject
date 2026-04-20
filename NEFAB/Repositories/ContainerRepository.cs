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

        //fjerner cachen helt for at sikre at vi altid henter data fra databasen,
        //og ikke risikerer at have forældet data i cachen
        // private List<Container> containers;

        public ContainerRepository()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

          //  containers = new List<Container>();

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

                SqlCommand cmd = new SqlCommand("SELECT ContainerNo, Week, Year FROM Container", con);
                SqlDataReader reader = cmd.ExecuteReader();
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
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Container (ContainerNo, Week, Year) VALUES (@ContainerNo, @Week, @Year);",
                    con))
                {
                    cmd.Parameters.Add("@ContainerNo", SqlDbType.NVarChar, 50).Value = container.ContainerNo;
                    cmd.Parameters.Add("@Week", SqlDbType.Int).Value = container.Week;
                    cmd.Parameters.Add("@Year", SqlDbType.Int).Value = container.Year;

                    cmd.ExecuteNonQuery();
                }
            }
           // containers.Add(container);
        }

        public void Remove(Container container) 

        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM CONTAINER WHERE ContainerNo = @ContainerNo", con))
                {
                    cmd.Parameters.Add("@ContainerNo", SqlDbType.NVarChar, 50).Value = container.ContainerNo;
                    cmd.ExecuteNonQuery();
                }
            }

            //Fjern også fra local liste
         //   containers.RemoveAll(c => c.ContainerNo == container.ContainerNo);
        
        }
        
        public Container GetByContainerNumber(string containerNo)
        {
          
          //  var found = containers.FirstOrDefault(c => c.ContainerNo == containerNo);
         //   if (found != null) return found;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
             
                using var cmd = new SqlCommand("SELECT ContainerNo, Week, Year FROM CONTAINER WHERE ContainerNo = @ContainerNo", con);
                cmd.Parameters.Add("@ContainerNo", SqlDbType.NVarChar, 50).Value = containerNo;
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string dbContainerNo = reader.GetString(0);
                    int week = reader.GetInt32(1);
                    int year = reader.GetInt32(2);
                    var container = new Container(dbContainerNo) { Week = week, Year = year };
                   // containers.Add(container);
                    return container;
                }
            }

            return null;
        }

        public List<Container> GetByWeek(int week, int year)
        {

            var results = new List<Container>();

            //var results = containers.Where(c => c.Week == week && c.Year == year).ToList();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using var cmd = new SqlCommand("SELECT ContainerNo, Week, Year FROM CONTAINER WHERE Week = @Week AND Year = @Year", con);
                cmd.Parameters.Add("@Week", SqlDbType.Int).Value = week;
                cmd.Parameters.Add("@Year", SqlDbType.Int).Value = year;
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string containerNo = reader.GetString(0);
                    int w = reader.GetInt32(1);
                    int y = reader.GetInt32(2);

                    //er den der allerede?
                    if (!results.Any(c => c.ContainerNo == containerNo))
                    {
                        var container = new Container(containerNo) { Week = w, Year = y };
                        results.Add(container);

                       //til cachen
                       //containers.Add(container);
                    }
                }
            }

            return results;
        }

        
        public void Update(Container container)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE CONTAINER SET Week = @Week, Year = @Year WHERE ContainerNo = @ContainerNo",
                    con))
                {
                    cmd.Parameters.Add("@ContainerNo", SqlDbType.NVarChar, 50).Value = container.ContainerNo;
                    cmd.Parameters.Add("@Week", SqlDbType.Int).Value = container.Week;
                    cmd.Parameters.Add("@Year", SqlDbType.Int).Value = container.Year;

                    cmd.ExecuteNonQuery();
                }

            }


            //Opdaterer listen lokalt 
            //Container containerToUpdate = containers.FirstOrDefault(c => c.ContainerNo == container.ContainerNo);
            //if (containerToUpdate != null)
            //{
            //    containerToUpdate.Week = container.Week;
            //    containerToUpdate.Year = container.Year;
            //}
        }
    }
}

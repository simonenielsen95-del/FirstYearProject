using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NEFAB.Domains;
using NEFAB.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace NEFAB.Repositories
{
    public class SupplierRepository : IRepoGetAdd<Supplier, string>
    {
        private readonly string ConnectionString;
        private List<Supplier> suppliers;

        public SupplierRepository()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            suppliers = new List<Supplier>();

            ConnectionString = config.GetConnectionString("MyDBConnection");

        }


        public void Add(Supplier supplier)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO SUPPLIER (SupplierName) VALUE (@SupplierName);",
                    con))
                {
                    cmd.Parameters.Add("@SupplierName", SqlDbType.NVarChar).Value = supplier.SupplierName;
                    suppliers.Add(supplier);
                }
            }
            return;
        }



        public List<Supplier> GetAll()
        {
            List<Supplier> suppliers = new List<Supplier>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT SupplierName FROM Supplier", con);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Supplier supplier = new Supplier(dr.GetString(0));
                        suppliers.Add(supplier);
                    }                    
                }
            }
            return suppliers;
        }

        public Supplier? GetByID(string Name)
        {
            Supplier? supplier = null;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT SupplierName FROM SUPPLIER WHERE SupplierName = @SupplierName", con);
                cmd.Parameters.Add("@SupplierName", SqlDbType.NVarChar).Value = Name;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        supplier = new Supplier(dr.GetString(0));
                    }
                }
            }
            return supplier;
        }

    }   

}

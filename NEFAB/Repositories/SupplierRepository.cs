using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NEFAB.Domains;

namespace NEFAB.Repositories
{
    public class SupplierRepository
    {
        private readonly string ConnectionString;

        private Supplier supplier;

        public SupplierRepository()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            ConnectionString = config.GetConnectionString("MyDBConnection");

        }



    }   

}

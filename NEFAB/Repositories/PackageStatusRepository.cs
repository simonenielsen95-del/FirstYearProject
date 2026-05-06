using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NEFAB.Domains;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace NEFAB.Repositories
{
    public class PackageStatusRepository
    {
        private readonly string connectionString;
        private List<PackageStatus> packageStatuses;

        public PackageStatusRepository()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            packageStatuses = new List<PackageStatus>();

            connectionString = config.GetConnectionString("MyDBConnection");
        }

        public void Add(PackageStatus packageStatus)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("spAddStatus", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                   
                    cmd.Parameters.Add("@StatusType", SqlDbType.NVarChar, 100).Value = packageStatus.Status.ToString();
                    cmd.Parameters.Add("@Comment", SqlDbType.NVarChar, 400).Value = packageStatus.Comment ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@EmployeeId", SqlDbType.NVarChar, 8).Value = packageStatus.EmployeeId;
                    cmd.Parameters.Add("@PackageId", SqlDbType.Int).Value = packageStatus.PackageId;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ChangeStatus(PackageStatus packageStatus)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("spChangeStatus", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PackageStatusId", SqlDbType.Int).Value = packageStatus.PackageStatusId;
                    cmd.Parameters.Add("@StatusType", SqlDbType.NVarChar, 100).Value = packageStatus.Status.ToString();
                    cmd.Parameters.Add("@Comment", SqlDbType.NVarChar, 400).Value = packageStatus.Comment ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@EmployeeId", SqlDbType.NVarChar, 8).Value = packageStatus.EmployeeId ?? "ADMI0101";
                    cmd.Parameters.Add("@PackageId", SqlDbType.Int).Value = packageStatus.PackageId ?? (object)DBNull.Value;
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

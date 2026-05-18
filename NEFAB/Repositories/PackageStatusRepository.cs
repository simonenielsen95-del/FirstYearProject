using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NEFAB.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO.Packaging;
using System.Text;
namespace NEFAB.Repositories
{
    public class PackageStatusRepository
    {
        private readonly string connectionString;
        public PackageStatusRepository()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            connectionString = config.GetConnectionString("MyDBConnection");
        }
        public void Add(PackageStatus packageStatus)
        {
            packageStatus.DateTime = DateTime.Now;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("spAddStatus", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@StatusType", SqlDbType.NVarChar, 100).Value = packageStatus.Status.ToString();
                    cmd.Parameters.Add("@Comment", SqlDbType.NVarChar, 400).Value = packageStatus.Comment ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@StatusTime", SqlDbType.DateTime).Value = packageStatus.DateTime;
                    cmd.Parameters.Add("@EmployeeId", SqlDbType.NVarChar, 8).Value = packageStatus.EmployeeId;
                    cmd.Parameters.Add("@PackageId", SqlDbType.Int).Value = packageStatus.PackageId;
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<PackageStatus> GetByPackageId(int? packageId)
        {
            List<PackageStatus> packagestatuses = new List<PackageStatus>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("spGetPackageStatusByPackageId", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PackageId", SqlDbType.Int).Value = packageId;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            PackageStatus packagestatus = new PackageStatus()
                            {
                                PackageStatusId = dr.GetInt32(0),
                                Status = Enum.TryParse<StatusType>(dr.GetString(1), out var parsedStatus) ? parsedStatus : default,
                                Comment = dr.IsDBNull(2) ? null : dr.GetString(2),
                                DateTime = dr.GetDateTime(3),
                                PackageId = dr.GetInt32(4),
                                EmployeeId = dr.GetString(5)
                            };
                            packagestatuses.Add(packagestatus);
                        }
                    }
                }
            }
            return packagestatuses;
        }
    }
}

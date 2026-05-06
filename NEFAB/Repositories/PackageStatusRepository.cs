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
                                PackageId = dr.GetInt32(3),
                                EmployeeId = dr.GetString(4)
                            };
                            packagestatuses.Add(packagestatus);
                        }
                    }
                }
            }
            return packagestatuses;
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

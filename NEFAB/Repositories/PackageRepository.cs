using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NEFAB.Domains;
using NEFAB.Repositories.Interfaces;
using System;
using System.Collections.Generic;
//using System.ComponentModel;
using System.Data;
using System.Xml.Linq;

namespace NEFAB.Repositories
{
    public class PackageRepository //: IRepoGetAdd<Package, string>
    {
        private readonly string ConnectionString;
        private List<Package> packages;

        public PackageRepository()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            packages = new List<Package>(); // rettet fra List<Packages> til List<Package>

            ConnectionString = config.GetConnectionString("MyDBConnection");
        }

        public void Add(Container container, Supplier supplier, Package package)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("spAddPackage", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ProjectNo", SqlDbType.Int).Value = package.ProjectNo;
                    cmd.Parameters.Add("@ProjectItemNo", SqlDbType.Int).Value = package.ProjectItemNo;
                    cmd.Parameters.Add("@PackageWeight", SqlDbType.Int).Value = package.PackageWeight;
                    cmd.Parameters.Add("@Amount", SqlDbType.Int).Value = package.Amount;
                    cmd.Parameters.Add("@PackageLength", SqlDbType.Float).Value = package.PackageLength;
                    cmd.Parameters.Add("@PackageWidth", SqlDbType.Float).Value = package.PackageWidth;
                    cmd.Parameters.Add("@PackageHeight", SqlDbType.Float).Value = package.PackageHeight;
                    cmd.Parameters.Add("@Comment", SqlDbType.NVarChar, 400).Value = package.Comment ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@ContainerNo", SqlDbType.NVarChar, 50).Value = container.ContainerNo;
                    cmd.Parameters.Add("@SupplierName", SqlDbType.NVarChar, 100).Value = supplier.SupplierName;
                    cmd.ExecuteNonQuery();
                    packages.Add(package);
                }
            }
        }

        public List<Package> GetAll()
        {
            List<Package> packages = new List<Package>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("spGetAllPackages", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Package package = new Package()
                            {
                                PackageId = dr.GetInt32(0),
                                ProjectNo = dr.GetInt32(1),
                                ProjectItemNo = dr.GetInt32(2),
                                PackageWeight = dr.GetInt32(3),
                                Amount = dr.GetInt32(4),
                                PackageLength = (float)dr.GetDouble(5),
                                PackageWidth = (float)dr.GetDouble(6),
                                PackageHeight = (float)dr.GetDouble(7),
                                Comment = dr.IsDBNull(8) ? null : dr.GetString(8),
                                //ContainerNo = dr.GetString(9),
                                //SupplierName = dr.GetString(10)
                            };
                            packages.Add(package);
                        }
                    }
                }
            }
            return packages;
        }

        public Package? GetByID(string containerNo)
        {
            Package? package = null;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("spGetPackagesByContainerNo", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ContainerNo", SqlDbType.NVarChar, 50).Value = containerNo;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            package = new Package()
                            {
                                PackageId = dr.GetInt32(0),
                                ProjectNo = dr.GetInt32(1),
                                ProjectItemNo = dr.GetInt32(2),
                                PackageWeight = dr.GetInt32(3),
                                Amount = dr.GetInt32(4),
                                PackageLength = (float)dr.GetDouble(5),
                                PackageWidth = (float)dr.GetDouble(6),
                                PackageHeight = (float)dr.GetDouble(7),
                                Comment = dr.IsDBNull(8) ? null : dr.GetString(8)
                                //ContainerNo = dr.GetString(9),
                                //SupplierName = dr.GetString(10)
                            };
                        }
                    }
                }
            }
            return package;
        }
        //fulde liste til combobox, da der kan være flere pakker i en container
        public List<Package> GetPackagesByContainerNo(string containerNo)
        {
            List<Package> result = new List<Package>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("spGetPackagesByContainerNo", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ContainerNo", SqlDbType.NVarChar, 50).Value = containerNo;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Package package = new Package()
                            {
                                PackageId = dr.GetInt32(0),
                                ProjectNo = dr.GetInt32(1),
                                ProjectItemNo = dr.GetInt32(2),
                                PackageWeight = dr.GetInt32(3),
                                Amount = dr.GetInt32(4),
                                PackageLength = (float)dr.GetDouble(5),
                                PackageWidth = (float)dr.GetDouble(6),
                                PackageHeight = (float)dr.GetDouble(7),
                                Comment = dr.IsDBNull(8) ? null : dr.GetString(8)
                                //ContainerNo = dr.GetString(9),
                                //SupplierName = dr.GetString(10)
                            };
                            result.Add(package);
                        }
                                
                    }
                }
            }
            return result;
        }
    }
}
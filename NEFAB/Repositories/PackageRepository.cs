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
        private readonly string connectionString;
        private List<Package> packages;

        public PackageRepository()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            packages = new List<Package>(); // rettet fra List<Packages> til List<Package>

            connectionString = config.GetConnectionString("MyDBConnection");
        }

        public void Add(Container container, Supplier supplier, Package package)// eller (package , string supplierName , string containerNo)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("spAddPackage", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ProjectNo", SqlDbType.BigInt).Value = package.ProjectNo;
                    cmd.Parameters.Add("@ProjectItemNo", SqlDbType.Int).Value = package.ProjectItemNo;
                    cmd.Parameters.Add("@PackageWeight", SqlDbType.Int).Value = package.PackageWeight;
                    cmd.Parameters.Add("@Amount", SqlDbType.Int).Value = package.Amount;
                    cmd.Parameters.Add("@InnerQuantity", SqlDbType.Int).Value = package.InnerQuantity;
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
            using (SqlConnection con = new SqlConnection(connectionString))
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
                                ProjectNo = dr.GetInt64(1),
                                ProjectItemNo = dr.GetInt32(2),
                                PackageWeight = dr.GetInt32(3),
                                Amount = dr.GetInt32(4),
                                InnerQuantity = dr.GetInt32(5),
                                PackageLength = (float)dr.GetDouble(6),
                                PackageWidth = (float)dr.GetDouble(7),
                                PackageHeight = (float)dr.GetDouble(8),
                                Comment = dr.IsDBNull(9) ? null : dr.GetString(9),
                                ContainerNo = dr.GetString(10),
                                SupplierName = dr.GetString(11)
                            };
                            packages.Add(package);
                        }
                    }
                }
            }
            return packages;
        }

        public Package? GetByID(string containerNo) // skal laves om. 
        {
            Package? package = null;
            using (SqlConnection con = new SqlConnection(connectionString))
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
                                ProjectNo = dr.GetInt64(1),
                                ProjectItemNo = dr.GetInt32(2),
                                PackageWeight = dr.GetInt32(3),
                                Amount = dr.GetInt32(4),
                                InnerQuantity = dr.GetInt32(5),
                                PackageLength = (float)dr.GetDouble(6),
                                PackageWidth = (float)dr.GetDouble(7),
                                PackageHeight = (float)dr.GetDouble(8),
                                Comment = dr.IsDBNull(9) ? null : dr.GetString(9),
                                ContainerNo = dr.GetString(10),
                                SupplierName = dr.GetString(11)
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
            using (SqlConnection con = new SqlConnection(connectionString))
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
                                ProjectNo = dr.GetInt64(1),
                                ProjectItemNo = dr.GetInt32(2),
                                PackageWeight = dr.GetInt32(3),
                                Amount = dr.GetInt32(4),
                                InnerQuantity = dr.GetInt32(5),
                                PackageLength = (float)dr.GetDouble(6),
                                PackageWidth = (float)dr.GetDouble(7),
                                PackageHeight = (float)dr.GetDouble(8),
                                Comment = dr.IsDBNull(9) ? null : dr.GetString(9),
                                ContainerNo = dr.GetString(10),
                                SupplierName = dr.GetString(11)
                            };
                            result.Add(package);
                        }
                                
                    }
                }
            }
            return result;
        }

        public void Remove(Package package)

        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM PACKAGE WHERE PackageId = @PackageId", con))
                {
                    cmd.Parameters.Add("@PackageId", SqlDbType.Int).Value = package.PackageId;
                    cmd.ExecuteNonQuery();
                }
            }


        }
    }
}
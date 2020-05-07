using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class DES
    {
        private static string EtsOrDir;
        private static string RelatedTable;
        private static int relatedDataId;

        public int ID { get; set; }
        public string Name { get; set; }

        public static List<DES> GetDataZero(string table)
        {
            List<DES> newArray = new List<DES>();

            if (table == "etablissement")
            {
                EtsOrDir = "ets";
                RelatedTable = "direction";
            }
            else if (table == "direction")
            {
                EtsOrDir = "dir";
                RelatedTable = "service";
            }

            string connectionString = ConfigurationManager.ConnectionStrings["DES"].ConnectionString;

            // Connect to the Database
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                // TODO : Rework this to be more readable (Linq?)
                string cmdString = "SELECT " + table + ".id, " + table + ".name"
                                    + " FROM " + table;

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = cmdString;

                    sqlConn.Open();

                    var dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        DES DES = new DES()
                        {
                            ID = Convert.ToInt32(dr["id"]),
                            Name = dr["name"].ToString()
                        };
                        newArray.Add(DES);
                    }
                }
            }
            return newArray;
        }
        public static List<DES> GetData(string table, string relatedData)
        {
            List<DES> newArray = new List<DES>();

            if (table == "etablissement")
            {
                EtsOrDir = "ets";
                RelatedTable = "direction";
            }
            else if (table == "direction")
            {
                EtsOrDir = "dir";
                RelatedTable = "service";
            }

            string connectionString = ConfigurationManager.ConnectionStrings["DES"].ConnectionString;


            // Connect to the Database
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                string cmdString = "SELECT id FROM " + table + " WHERE name = '" + relatedData + "'";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = cmdString;

                    sqlConn.Open();

                    var dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        relatedDataId = Convert.ToInt32(dr[0]);
                    }
                }
            }

            // Connect to the Database
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                // TODO : Rework this to be more readable (Linq?)
                string cmdString = "SELECT " + RelatedTable + ".id, " + RelatedTable + ".name"
                                    + " FROM " + table
                                    + " LEFT JOIN " + RelatedTable + " ON " + table + ".id = " + RelatedTable + ".linked_" + EtsOrDir + ""
                                    + " WHERE " + RelatedTable + ".linked_" + EtsOrDir + " = '" + relatedDataId + "'";

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = cmdString;

                    sqlConn.Open();

                    var dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        DES DES = new DES()
                        {
                            ID = Convert.ToInt32(dr["id"]),
                            Name = dr["name"].ToString()
                        };
                        newArray.Add(DES);
                    }
                }
            }
            return newArray;
        }
    }
}
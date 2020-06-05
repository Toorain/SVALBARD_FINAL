using Microsoft.Ajax.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WebApplication1
{
    public class Logs
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["LogsArchives"].ConnectionString;

        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string IssuerID { get; set; }
        public string IssuerDir { get; set; }
        public string IssuerEts { get; set; }
        public string IssuerService { get; set; }
        public string ArchiveID { get; set; }
        public string Localization { get; set; }
        public int Action { get; set; }
        public string Status { get; set; }

        /// <summary>
        /// When a request on Archive is generated (retreive/destroy), AssignArchiviste() method is called. 
        /// Use this method to find an Archiviste in the database and assign it to a request.
        /// </summary>
        /// <returns>
        /// Returns a string with Archiviste unique ID. 
        /// If two Archivists are found in the database, this most likely will generate a bug.
        /// </returns>

        // TODO : Find a way to assign evenly events on physical Archive to all Archivists known (if more than one exists).
        public static string AssignArchiviste()
        {
            string archivisteId = "";

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            // Connect to the Database
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                // RoleId : 4 is Archivist Role
                string cmdString = "SELECT UserId FROM AspNetUserRoles WHERE RoleId = 4";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = cmdString;

                    sqlConn.Open();

                    var dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        archivisteId = dr[0].ToString();
                    }
                }
                return archivisteId;
            }
        }

        public static string GetActionType(string currentArchiveRef)
        {
            string actionType = "";
            string connectionString = ConfigurationManager.ConnectionStrings["LogsArchives"].ConnectionString;

            // Connect to the Database
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                string cmdString = "SELECT action FROM logsArchive WHERE archiveID = '@val1'";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = cmdString;
                    cmd.Parameters.AddWithValue("@val1", currentArchiveRef);

                    sqlConn.Open();

                    var dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        actionType = dr[0].ToString();
                    }
                    return actionType;
                }
            }
        }

        public static List<List<string>> GetStatus()
        {
            List<string> arrayGlobal = new List<string>();
            List<string> arrayAjout = new List<string>();
            List<string> arrayRetrait = new List<string>();
            List<string> arrayDestruction = new List<string>();

            string connectionString = ConfigurationManager.ConnectionStrings["LogsArchives"].ConnectionString;

            // Connect to the Database
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                string cmdString = "SELECT status_name, group_code FROM status";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = cmdString;

                    sqlConn.Open();

                    var dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        /**
                         * For every group of status (global, ajouter, retrait, destruction) we fill a different array.
                         */
                        switch (Convert.ToInt32(dr["group_code"]))
                        {
                            case 1:
                                arrayGlobal.Add(dr["status_name"].ToString());
                                break;
                            case 2:
                                arrayAjout.Add(dr["status_name"].ToString());
                                break;
                            case 3:
                                arrayRetrait.Add(dr["status_name"].ToString());
                                break;
                            case 4:
                                arrayDestruction.Add(dr["status_name"].ToString());
                                break;
                            default:
                                break;
                        }
                    }
                    List<List<string>> arrayArray = new List<List<string>> { arrayGlobal, arrayAjout, arrayRetrait, arrayDestruction };
                    return arrayArray;
                }
            }
        }
        
        public static void AddArchive(
                                        DateTime date,
                                        string firstName,
                                        string lastName,
                                        string ets,
                                        string dir,
                                        string service,
                                        string receiverId,
                                        string cote,
                                        int action,
                                        int status
                                        )
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LogsArchives"].ConnectionString;
            string cmdString = "";
            int count = 0;
            
            
            // Connect to the Database
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                cmdString = "SELECT TOP (1) ID FROM [logsArchives].[dbo].[logsArchive] ORDER BY ID DESC";

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = cmdString;

                    sqlConn.Open();

                    var dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        count = Convert.ToInt32(dr["ID"]) + 1;
                    }
                    dr.Close();
                }

                cmdString = "INSERT INTO [dbo].[logsArchive] "
                            + "VALUES ("
                            + "@val1,"
                            + "@val2,"
                            + "NULL,"
                            + "@val3,"
                            + "@val4,"
                            + "@val5,"
                            + "@val6,"
                            + "@val7,"
                            + "@val8,"
                            + "@val9,"
                            + "NULL,"
                            + "@val10,"
                            + "@val11"
                            + ");";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = cmdString;
                    cmd.Parameters.AddWithValue("@val1", count);
                    cmd.Parameters.AddWithValue("@val2", date);
                    cmd.Parameters.AddWithValue("@val3", firstName);
                    cmd.Parameters.AddWithValue("@val4", lastName);
                    cmd.Parameters.AddWithValue("@val5", ets);
                    cmd.Parameters.AddWithValue("@val6", dir);
                    cmd.Parameters.AddWithValue("@val7", service);
                    cmd.Parameters.AddWithValue("@val8", receiverId);
                    cmd.Parameters.AddWithValue("@val9", cote);
                    cmd.Parameters.AddWithValue("@val10", action);
                    cmd.Parameters.AddWithValue("@val11", status);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
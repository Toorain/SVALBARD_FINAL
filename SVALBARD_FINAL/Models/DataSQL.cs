using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace WebApplication1
{
    public class DataSQL
    {
        // public int ID { get; set; }
        public string Cote { get; set; }
        public DateTime Versement { get; set; }
        public string Etablissement { get; set; }
        public string Direction { get; set; }
        public string Service { get; set; }
        public string Dossiers { get; set; }
        public string Extremes { get; set; }
        public string Elimination { get; set; }
        public string Communication { get; set; }
        public string Localisation { get; set; }

        public static void ModifyArchive(string currentRow)
        {
            var newArray = new List<DataSQL>();
            string connectionString = ConfigurationManager.ConnectionStrings["Archives"].ConnectionString;
            // Connect to the Database
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                string cmdString = "SELECT * FROM [dbo].[ArchivesV2] WHERE ID = @val1";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = cmdString;
                    cmd.Parameters.AddWithValue("@val1", currentRow);

                    sqlConn.Open();

                    var dr = cmd.ExecuteReader();
                    
                    while (dr.Read())
                    {
                        DataSQL dataSql = new DataSQL
                        {
                            // ID = Convert.ToInt32(dr["ID"].ToString()),
                            Cote = dr["cote"].ToString(),
                            Versement = string.IsNullOrEmpty(dr["versement"].ToString()) ? new DateTime(1900, 1, 1) : Convert.ToDateTime(dr["versement"].ToString()),
                            Etablissement = dr["etablissement"].ToString(),
                            Direction = dr["direction"].ToString(),
                            Service = dr["service"].ToString(),
                            Dossiers = dr["dossiers"].ToString(),
                            Extremes = dr["extremes"].ToString(),
                            Elimination = dr["elimination"].ToString(),
                            Communication = dr["communication"].ToString(),
                            Localisation = dr["localisation"].ToString()
                        };
                        newArray.Add(dataSql);
                    }
                }
            }
        }
        public static int GetLastItemArchive()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Archives"].ConnectionString;
            // Connect to the Database
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                string cmdString = "SELECT TOP (1) [ID] FROM [archives].[dbo].[ArchivesV2] ORDER BY ID DESC";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = cmdString;

                    sqlConn.Open();

                    var dr = cmd.ExecuteReader();

                    int lastItem = 0;

                    while (dr.Read())
                    {
                        lastItem =  Convert.ToInt32(dr["ID"]);
                    }
                    return lastItem;
                }
            }
        }
        
        public static string GetCote(string cote)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Archives"].ConnectionString;
            // Connect to the Database
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                string cmdString = "SELECT TOP 1 [cote] FROM [archives].[dbo].[ArchivesV2] WHERE cote LIKE @val1 ORDER BY cote DESC;";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = cmdString;
                    cmd.Parameters.AddWithValue("@val1", cote + '%');

                    sqlConn.Open();

                    var dr = cmd.ExecuteReader();
                    var drVal = "";

                    while (dr.Read())
                    {
                        drVal = dr.HasRows ? dr["cote"].ToString() : "no_entry";
                    }
                    return drVal;
                }
            }
        }
        
        public static string SuggestCote(string coteSpliced)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Archives"].ConnectionString;
            // Connect to the Database
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                string cmdString = "SELECT TOP (1) [cote] FROM [archives].[dbo].[ArchivesV2] WHERE cote LIKE @val1 + '%' ORDER BY cote DESC";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = cmdString;
                    cmd.Parameters.AddWithValue("@val1", coteSpliced);

                    sqlConn.Open();

                    var dr = cmd.ExecuteReader();
                    var drVal = "";

                    while (dr.Read())
                    {
                        if (dr.HasRows)
                        {
                            drVal = dr["cote"].ToString();
                        }
                        else
                        {
                            drVal = "no_entry";
                        }
                    }
                    return drVal;
                }
            }
        }
    }

    
}
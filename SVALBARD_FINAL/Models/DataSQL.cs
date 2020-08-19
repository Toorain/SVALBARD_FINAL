using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.ReportingServices.DataProcessing;
using MongoDB.Driver;
using CommandType = System.Data.CommandType;

namespace WebApplication1.Models
{
    public class DataSql
    {
        static string _connectionString = ConfigurationManager.ConnectionStrings["Archives"].ConnectionString;
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

        public static bool AddArchive(string identifier)
        {
            LogsPal individualRow = LogsPal.SelectIndividualRow(identifier);
            using (SqlConnection sqlConn = new SqlConnection(_connectionString))
            {
                string cmdString = "INSERT INTO [dbo].[ArchivesV2]"
                                 + " ([ID] ,[versement] ,[etablissement] ,[direction] ,[service] ,[dossiers] ,[extremes] ,[elimination] ,[communication] ,[cote] ,[localisation])"
                                 + " VALUES (@val1, @val2, @val3, @val4, @val5, @val6, @val7, @val8, @val9, @val10, @val11)";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = cmdString;
                    // Gets the last ID from ArchiveV2 database, and add ONE to increment gradually.
                    cmd.Parameters.AddWithValue("@val1", GetLastItemArchive() + 1);
                    cmd.Parameters.AddWithValue("@val2", individualRow.Date);
                    cmd.Parameters.AddWithValue("@val3", individualRow.IssuerEts);
                    cmd.Parameters.AddWithValue("@val4", individualRow.IssuerDir);
                    cmd.Parameters.AddWithValue("@val5", individualRow.IssuerService);
                    cmd.Parameters.AddWithValue("@val6", individualRow.Contenu);
                    cmd.Parameters.AddWithValue("@val7", individualRow.DateMin + "-" + individualRow.DateMax);
                    cmd.Parameters.AddWithValue("@val8", individualRow.Elimination);
                    cmd.Parameters.AddWithValue("@val9", individualRow.Communication);
                    cmd.Parameters.AddWithValue("@val10", individualRow.Cote);
                    cmd.Parameters.AddWithValue("@val11", individualRow.Localization);

                    sqlConn.Open();

                    cmd.ExecuteNonQuery();
                }
                
                
                
                cmdString = "UPDATE [logsArchives].[dbo].[logsArchivePAL]"
                          + " SET flg_treated = 1"
                          + " WHERE ID = @val1";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = cmdString;
                    cmd.Parameters.AddWithValue("@val1", identifier );
                    
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }
        
        public static void ModifyArchive(string currentRow)
        {
            var newArray = new List<DataSql>();
            // Connect to the Database
            using (SqlConnection sqlConn = new SqlConnection(_connectionString))
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
                        DataSql dataSql = new DataSql
                        {
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
            // Connect to the Database
            using (SqlConnection sqlConn = new SqlConnection(_connectionString))
            {
                string cmdString = "SELECT TOP (1) [ID] FROM [dbo].[ArchivesV2] ORDER BY ID DESC";
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
        
        public static LogsPal GetIndividualArchive(string identifier, string requestGroup = "holder")
        {
            // Connect to the Database
            using (SqlConnection sqlConn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConn;
                    /*cmd.CommandText = "SELECT * FROM [dbo].[ArchivesV2] WHERE cote = @val1";*/
                    cmd.CommandText = "GetIndividualArchive_ArchiveV2";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@val1", identifier);

                    sqlConn.Open();

                    var dr = cmd.ExecuteReader();

                    LogsPal logsPal = new LogsPal();
                    while (dr.Read())
                    {
                        logsPal = new LogsPal
                        {
                            Cote = dr["cote"].ToString(),
                            IssuerEts = dr["etablissement"].ToString(),
                            IssuerDir = dr["direction"].ToString(),
                            IssuerService = dr["service"].ToString(),
                            Contenu = dr["dossiers"].ToString(),
                            DateMin = Convert.ToInt32(dr["extremes"].ToString().Substring(0, dr["extremes"].ToString().IndexOf("-"))),
                            DateMax = Convert.ToInt32(dr["extremes"].ToString().Substring( dr["extremes"].ToString().IndexOf("-") + 1)),
                            Elimination = dr["elimination"].ToString(),
                            Localization = dr["localisation"].ToString(),
                            RequestGroup = requestGroup
                        };
                    }
                    return logsPal;
                }
            }
        }
        
        public static string CheckIfCoteHasAlreadyBeenRequested(string cote)
        {

            // Connect to the Database
            using (SqlConnection sqlConn = new SqlConnection(_connectionString))
            {
                string cmdString = " SELECT ID as cote_ID FROM [logsArchives].[dbo].[logsArchivePAL] WHERE ID = @val1 AND action = 2";
                
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = cmdString;
                    cmd.Parameters.AddWithValue("@val1", cote);

                    sqlConn.Open();

                    var dr = cmd.ExecuteReader();
                    var drVal = "";

                    while (dr.Read())
                    {
                        drVal = dr.HasRows ? dr["cote_ID"].ToString() : "no_entry";
                    }
                    return drVal;
                }
            }
        }

        public static string GetCote(string cote)
        {

            // Connect to the Database
            using (SqlConnection sqlConn = new SqlConnection(_connectionString))
            {
                // Check across 2 DB and 3 tables that suggested cote is last cote + 1 even if a cote is in the waiting list. Prevents insertion of random numbers of Cote in Database.
                /*string cmdString =
                    "SELECT  TOP (1) cote_ID" +
                    " FROM    ( " +
                    " SELECT cote as cote_ID FROM [archives].[dbo].[ArchivesV2] WHERE cote LIKE @val1" +
                    " UNION " +
                    " SELECT ID as cote_ID FROM [logsArchives].[dbo].[logsArchivePAL] WHERE ID LIKE @val1" +
                    " UNION " +
                    " SELECT archiveID as cote_ID FROM [logsArchives].[dbo].[logsArchive] WHERE ID LIKE @val1" +
                    " ) AS cote_ID" +
                    " ORDER BY cote_ID DESC";*/
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = "Get_Cote_ArchiveV2";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@val1", cote + '%');

                    sqlConn.Open();

                    var dr = cmd.ExecuteReader();
                    var drVal = "";

                    while (dr.Read())
                    {
                        drVal = dr.HasRows ? dr["cote_ID"].ToString() : "no_entry";
                    }
                    return drVal;
                }
            }
        }
    }
}
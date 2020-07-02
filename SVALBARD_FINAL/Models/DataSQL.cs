﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace WebApplication1.Models
{
    public class DataSql
    {
        static string _connectionString = ConfigurationManager.ConnectionStrings["Archives"].ConnectionString;
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
            // Connect to the Database
            using (SqlConnection sqlConn = new SqlConnection(_connectionString))
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

            // Connect to the Database
            using (SqlConnection sqlConn = new SqlConnection(_connectionString))
            {
                // Check across 2 DB and 3 tables that suggested cote is last cote + 1 even if a cote is in the waiting list. Prevents insertion of random numbers of Cote in Database.
                string cmdString =
                    "SELECT  TOP (1) cote_ID" +
                    " FROM    ( " +
                    " SELECT cote as cote_ID FROM [archives].[dbo].[ArchivesV2] WHERE cote LIKE @val1" +
                    " UNION " +
                    " SELECT ID as cote_ID FROM [logsArchives].[dbo].[logsArchivePAL] WHERE ID LIKE @val1" +
                    " UNION " +
                    " SELECT archiveID as cote_ID FROM [logsArchives].[dbo].[logsArchive] WHERE ID LIKE @val1" +
                    " ) AS cote_ID" +
                    " ORDER BY cote_ID DESC";
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
                        drVal = dr.HasRows ? dr["cote_ID"].ToString() : "no_entry";
                    }

                    return drVal;
                }
            }
        }
        
        public static string GetRequestGroup(string id)
        {
            // Connect to the Database
            using (SqlConnection sqlConn = new SqlConnection(_connectionString))
            {
                string cmdString = "SELECT request_group" +
                                   " FROM logsArchives.dbo.logsArchivePAL" +
                                   " WHERE ID = @val1";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = cmdString;
                    cmd.Parameters.AddWithValue("@val1", id);

                    sqlConn.Open();

                    var dr = cmd.ExecuteReader();
                    var drVal = "";

                    while (dr.Read())
                    {
                        drVal = dr.HasRows ? dr["request_group"].ToString() : "no_entry";
                    }
                    return drVal;
                }
            }
        }
    }
}
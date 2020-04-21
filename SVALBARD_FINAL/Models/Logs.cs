﻿using Microsoft.Ajax.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace WebApplication1
{
    public class Logs
    {
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
            // TODO : (CRITICAL) Change this connection string to the one where users are actually stored (Needs to be changed : Source)
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

        public static List<string> GetStatus()
        {
            List<string> arrayStatus = new List<string>();

            string connectionString = ConfigurationManager.ConnectionStrings["LogsArchives"].ConnectionString;

            // Connect to the Database
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                string cmdString = "SELECT status_name FROM status";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = cmdString;

                    sqlConn.Open();

                    var dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        arrayStatus.Add(dr[0].ToString());
                    }
                    return arrayStatus;
                }
            }
        }
    }
}
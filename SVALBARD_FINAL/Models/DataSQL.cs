using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace WebApplication1
{
    public class DataSQL
    {
        public int ID { get; set; }
        public DateTime Versement { get; set; }
        public string Etablissement { get; set; }
        public string Direction { get; set; }
        public string Service { get; set; }
        public string Dossiers { get; set; }
        public string Extremes { get; set; }
        public string Elimination { get; set; }
        public string Communication { get; set; }
        public string Cote { get; set; }
        public string Localisation { get; set; }

        public static void ModifyArchive(string CurrentRow)
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
                    cmd.Parameters.AddWithValue("@val1", CurrentRow);

                    sqlConn.Open();

                    var dr = cmd.ExecuteReader();
                    
                    while (dr.Read())
                    {
                        DataSQL dataSQL = new DataSQL
                        {
                            ID = Convert.ToInt32(dr["ID"].ToString()),
                            Versement = string.IsNullOrEmpty(dr["versement"].ToString()) ? new DateTime(1900, 1, 1) : Convert.ToDateTime(dr["versement"].ToString()),
                            Etablissement = dr["etablissement"].ToString(),
                            Direction = dr["direction"].ToString(),
                            Service = dr["service"].ToString(),
                            Dossiers = dr["dossiers"].ToString(),
                            Extremes = dr["extremes"].ToString(),
                            Elimination = dr["elimination"].ToString(),
                            Communication = dr["communication"].ToString(),
                            Cote = dr["cote"].ToString(),
                            Localisation = dr["localisation"].ToString()
                        };
                        newArray.Add(dataSQL);
                    }
                }
            }
        }
    }

    
}
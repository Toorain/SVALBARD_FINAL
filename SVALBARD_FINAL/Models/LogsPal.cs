using System;
using System.Configuration;
using System.Data.SqlClient;

namespace WebApplication1.Models
{
	public class LogsPal : Logs
	{
		public string Contenu { get; set; }
		public int DateMin { get; set; }
		public int DateMax { get; set; }
		public string Observation { get; set; }
		public int Elimination { get; set; }
		public string Communication { get; set; }
		public string Cote { get; set; }
		
		private static readonly string ConnectionStringArchives = ConfigurationManager.ConnectionStrings["LogsArchives"].ConnectionString;
		
		public static LogsPal SelectIndividualRow(string identifier)
		{
			using (SqlConnection sqlConn = new SqlConnection(ConnectionStringArchives))
			{
				string cmdString = "SELECT *" +
				                   " FROM [logsArchives].[dbo].[logsArchivePAL]" +
				                   " WHERE [ID] = @val1";
				using (SqlCommand cmd = new SqlCommand())
				{
					cmd.Connection = sqlConn;
					cmd.CommandText = cmdString;
					cmd.Parameters.AddWithValue("@val1", identifier);
                    
					sqlConn.Open();
                    
					var dr = cmd.ExecuteReader();

					LogsPal individualRow = null;
                    
					while (dr.Read())
					{
						individualRow = new LogsPal
						{
							  ID = ( DataSql.GetLastItemArchive() + 1 )
							, Date = Convert.ToDateTime(dr["date"])
							, IssuerID = dr["userID"].ToString()
							, Action = Convert.ToInt32(dr["action"])
							, ArchiveID = dr["ID"].ToString()
							, IssuerDir = dr["dir"].ToString()
							, IssuerEts = dr["ets"].ToString()
							, IssuerService = dr["service"].ToString()
							, Contenu = dr["contenu"].ToString()
							, DateMin = Convert.ToInt32(dr["date_min"])
							, DateMax = Convert.ToInt32(dr["date_max"])
							, Elimination = Convert.ToInt32(dr["prevision_elim"])
							, Communication = dr["autorisation_comm"].ToString()
							, Observation = dr["observations"].ToString()
							, Cote = dr["ID"].ToString()
							, Localization = dr["localization"].ToString()
							, Origin = ""
							, Status = dr["status"].ToString()
							, RequestGroup = dr["request_group"].ToString()
						};
					}
					return individualRow;
				}
			}
		}
	}
	
	
}
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Windows.Forms;
using Microsoft.AspNet.Identity;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace WebApplication1.Models
{
	public class LogsPal : Logs
	{
		public string Contenu { get; set; }
		public int DateMin { get; set; }
		public int DateMax { get; set; }
		public string Observation { get; set; }
		public string Elimination { get; set; }
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
							, Elimination = dr["prevision_elim"].ToString()
							, Communication = "NOT_USED_SINCE_2020"
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

		public static bool RequestArchive(LogsPal logsPal)
		{
			string cmdString = "INSERT INTO [dbo].[logsArchivePal] ([ID], [date], [user], [userID], [ets], [dir], [service], [contenu], [date_min], [date_max], [observations], [prevision_elim], [request_group], [localization], [action], [status], [flg_treated], [flg_new]) VALUES (@val1, @val2, @val3, @val4, @val5, @val6, @val7, @val8, @val9, @val10, @val11, @val12, @val13, @val14, @val15, @val16, @val17, @val18)";
      using (SqlConnection sqlConn = new SqlConnection(ConnectionStringArchives))
      {
	      using (SqlCommand cmd = new SqlCommand())
	      {
							int number;
              cmd.Connection = sqlConn;
              cmd.CommandText = cmdString;
              cmd.Parameters.AddWithValue("@val1", logsPal.Cote);
              cmd.Parameters.AddWithValue("@val2", DateTime.Now);
              cmd.Parameters.AddWithValue("@val3", DatabaseUser.GetUserFirstName() + DatabaseUser.GetUserLastName());
              cmd.Parameters.AddWithValue("@val4", HttpContext.Current.User.Identity.GetUserId());
              cmd.Parameters.AddWithValue("@val5", logsPal.IssuerEts);
              cmd.Parameters.AddWithValue("@val6", logsPal.IssuerDir);
              cmd.Parameters.AddWithValue("@val7", logsPal.IssuerService);
              cmd.Parameters.AddWithValue("@val8", logsPal.Contenu);
              cmd.Parameters.AddWithValue("@val9", logsPal.DateMin);
              cmd.Parameters.AddWithValue("@val10", logsPal.DateMax);
              cmd.Parameters.AddWithValue("@val11", "");
              cmd.Parameters.AddWithValue("@val12", Int32.TryParse(logsPal.Elimination, out number) ? Convert.ToInt32(logsPal.Elimination) : 0 );
              cmd.Parameters.AddWithValue("@val13", logsPal.Cote);
              cmd.Parameters.AddWithValue("@val14", logsPal.Localization);
              cmd.Parameters.AddWithValue("@val15", 2);
              cmd.Parameters.AddWithValue("@val16", 1);
              cmd.Parameters.AddWithValue("@val17", 0);
              cmd.Parameters.AddWithValue("@val18", 1);

              sqlConn.Open();
              cmd.ExecuteNonQuery();
	      }
      }
			return true;
		}
		
		public static int GetNewElementsCount()
		{
			using (SqlConnection sqlConn = new SqlConnection(ConnectionStringArchives))
			{
				string cmdString = "SELECT [flg_new] FROM [dbo].[logsArchivePAL] ";
				using (SqlCommand cmd = new SqlCommand())
				{
					cmd.Connection = sqlConn;
					cmd.CommandText = cmdString;

					sqlConn.Open();

					var dr = cmd.ExecuteReader();
					int newElementsCount = 0;

					while (dr.Read())
					{
						if (Convert.ToInt32(dr["flg_new"]) == 1)
						{
							newElementsCount += Convert.ToInt32(dr["flg_new"]);
						}
					}
					return newElementsCount;
				}
			}
		}
		
		public static int GetNewElementsCountIndividual(int action)
		{
			using (SqlConnection sqlConn = new SqlConnection(ConnectionStringArchives))
			{
				string cmdString = "SELECT [flg_new] FROM [dbo].[logsArchivePAL] WHERE [action] = @val1";
				using (SqlCommand cmd = new SqlCommand())
				{
					cmd.Connection = sqlConn;
					cmd.CommandText = cmdString;
					cmd.Parameters.AddWithValue("@val1", action);

					sqlConn.Open();

					var dr = cmd.ExecuteReader();
					int newElementsCount = 0;

					while (dr.Read())
					{
						if (Convert.ToInt32(dr["flg_new"]) == 1)
						{
							newElementsCount += Convert.ToInt32(dr["flg_new"]);
						}
					}
					return newElementsCount;
				}
			}
		}

		public static void WasSeen(string identifier)
		{
			using (SqlConnection sqlConn = new SqlConnection(ConnectionStringArchives))
			{
				string cmdString = "UPDATE [dbo].[logsArchivePAL] SET [flg_new] = 0 WHERE ID = @val1";
				using (SqlCommand cmd = new SqlCommand())
				{
					cmd.Connection = sqlConn;
					cmd.CommandText = cmdString;
					cmd.Parameters.AddWithValue("@val1", identifier);

					sqlConn.Open();

					cmd.ExecuteNonQuery();
				}
			}
		}
		
		/// <summary>
		/// Get the group identifier from a single identifier.
		/// Ex. 11W0222 was created at the same time as 11W0223 & 11W0224, if I put 11W0223 in GetRequestGroup it will return 11W0222 (which is the group identifier for this bundle of three).
		/// </summary>
		/// <param name="id">A single "côte"</param>
		/// <returns>Returns the group identifier for one element.</returns>
		public static string GetRequestGroup(string id)
		{
			// Connect to the Database
			using (SqlConnection sqlConn = new SqlConnection(ConnectionStringArchives))
			{
				string cmdString = "SELECT request_group" +
				                   " FROM [dbo].[logsArchivePAL]" +
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
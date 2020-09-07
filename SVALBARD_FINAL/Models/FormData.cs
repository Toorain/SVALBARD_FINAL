using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Windows.Forms;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebApplication1.Models
{
	[SuppressMessage("ReSharper", "CollectionNeverQueried.Local")]
	public class FormData
	{
		private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["LogsArchives"].ConnectionString;

		private string Id { get; set; }
		private string User { get; set; }
		private string UserId { get; set; }
		private string Contenu { get; set; }
		private int DateDebut { get; set; }
		private int DateFin { get; set;  }
		private string Observations { get; set;  }
		private int Elimination { get; set; }
		private string RequestGroup { get; set; }

		private static bool IsInDataBase(string entry)
		{
			using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
			{
				//string cmdString = $"SELECT ID FROM [dbo].[logsArchivePAL] WHERE ID = @val1";
				using (SqlCommand cmd = new SqlCommand())
				{
					cmd.Connection = sqlConn;
					cmd.CommandText = "CheckDataBaseEntry_LogsArchivePAL";
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@val1", entry);
					sqlConn.Open();

					var dr = cmd.ExecuteReader();

					return dr.Read();
				}
			}
		}
		public static bool DecypherData(string data, string user)
		{
			List<FormData> formDataList = new List<FormData>();
			// Recover JSON data, from a string to a JSON.
			JObject jsonAsJsonFormat = JObject.Parse(data);
			
			// Get JSON data Length
			int jsonLength = jsonAsJsonFormat.Count;

			for (int i = 1; i < jsonLength + 1; i++)
			{
				JToken jsonElm = jsonAsJsonFormat["article_" + i];
				FormData newFormData = new FormData {
					Id = jsonElm["id"].ToString(),
					User = jsonElm["user"].ToString(),
					UserId = jsonElm["user_id"].ToString(),
					Contenu = jsonElm["contenu"].ToString(),
					DateDebut = Convert.ToInt32(jsonElm["date_debut"]),
					DateFin = Convert.ToInt32(jsonElm["date_fin"]),
					Observations = jsonElm["observations"].ToString(),
					Elimination = Convert.ToInt32(jsonElm["elimination"]),
					RequestGroup = jsonElm["request_group"].ToString()
				};
				formDataList.Add(newFormData);
			}
			return PushData(formDataList, user);
		}

		private static bool PushData(List<FormData> data, string user)
		{
			AdUser adUser = AdUser.GetUserInfos(user);
			
			using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
			{
				// string cmdString = "INSERT INTO [dbo].[logsArchivePAL]"
				// 					+ " ([ID] , [date], [user], [userID], [ets], [dir], [service], [contenu] ,[date_min] ,[date_max] ,[observations], [prevision_elim], [request_group], [action], [status], [flg_treated], [flg_new])"
				// 					+ " VALUES"
				// 					+ " (@val1, @val2, @val3, @val4, @val5, @val6, @val7, @val8, @val9, @val10, @val11, @val12, @val13, @val14, @val15, @val16, @val17)";
				using (SqlCommand cmd = new SqlCommand())
				{
					cmd.Connection = sqlConn;
					cmd.CommandText = "PushData_LogsArchivePAL";
					cmd.CommandType = CommandType.StoredProcedure;
					
					sqlConn.Open();

					bool validData = false;
					
					foreach (FormData formData in data)
					{
						
						if (!IsInDataBase(formData.Id))
						{
							cmd.Parameters.AddWithValue("@val1", formData.Id);
							cmd.Parameters.AddWithValue("@val2", DateTime.Now);
							cmd.Parameters.AddWithValue("@val3", formData.User);
							cmd.Parameters.AddWithValue("@val4", formData.UserId);
							cmd.Parameters.AddWithValue("@val5", adUser.Site);
							cmd.Parameters.AddWithValue("@val6", adUser.Direction);
							cmd.Parameters.AddWithValue("@val7", adUser.Service);
							cmd.Parameters.AddWithValue("@val8", formData.Contenu);
							cmd.Parameters.AddWithValue("@val9", formData.DateDebut);
							cmd.Parameters.AddWithValue("@val10", formData.DateFin);
							cmd.Parameters.AddWithValue("@val11", formData.Observations);
							cmd.Parameters.AddWithValue("@val12", formData.Elimination);
							cmd.Parameters.AddWithValue("@val13", formData.RequestGroup);
							cmd.Parameters.AddWithValue("@val14", 1);
							cmd.Parameters.AddWithValue("@val15", 1);
							cmd.Parameters.AddWithValue("@val16", 0);
							cmd.Parameters.AddWithValue("@val17", 1);

							cmd.ExecuteNonQuery();
						
							cmd.Parameters.Clear();

							validData = true;
						}
						else
						{
							return false;
						}
					}
					return validData;
				}
			}
		}
	}
}
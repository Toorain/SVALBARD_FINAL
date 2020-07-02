using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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
		private string Contenu { get; set; }
		private int DateDebut { get; set; }
		private int DateFin { get; set;  }
		private string Observations { get; set;  }
		private int Elimination { get; set; }
		private int Communication { get; set; }
		private string RequestGroup { get; set; }

		private static bool CheckDataBaseEntry(string entry)
		{
			using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
			{
				string cmdString = $"SELECT ID FROM [dbo].[logsArchivePAL] WHERE ID = '{entry}'";
				using (SqlCommand cmd = new SqlCommand())
				{
					cmd.Connection = sqlConn;
					cmd.CommandText = cmdString;
					sqlConn.Open();

					var dr = cmd.ExecuteReader();

					return !dr.Read();
				}
			}
		}
		public static bool DecypherData(string data)
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
					Contenu = jsonElm["contenu"].ToString(),
					DateDebut = Convert.ToInt32(jsonElm["date_debut"]),
					DateFin = Convert.ToInt32(jsonElm["date_fin"]),
					Observations = jsonElm["observations"].ToString(),
					Elimination = Convert.ToInt32(jsonElm["elimination"]),
					Communication = Convert.ToInt32(jsonElm["communication"]),
					RequestGroup = jsonElm["request_group"].ToString()
				};
				formDataList.Add(newFormData);
			}
			return PushData(formDataList);
		}

		private static bool PushData(List<FormData> data)
		{
			using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
			{
				string cmdString = "INSERT INTO [dbo].[logsArchivePAL]"
									+ " ([id] , [date], [user], [ets], [dir], [service], [contenu] ,[date_min] ,[date_max] ,[observations], [prevision_elim], [autorisation_comm], [request_group], [action], [status])"
									+ " VALUES"
									+ " (@val1, @val2, @val3, @val4, @val5, @val6, @val7, @val8, @val9, @val10, @val11, @val12, @val13, @val14, @val15 )";
				using (SqlCommand cmd = new SqlCommand())
				{
					cmd.Connection = sqlConn;
					cmd.CommandText = cmdString;
					
					sqlConn.Open();

					bool validData = false;
					foreach (FormData formData in data) 
					{
						if (CheckDataBaseEntry(formData.Id))
						{
							cmd.Parameters.AddWithValue("@val1", formData.Id);
							cmd.Parameters.AddWithValue("@val2", DateTime.Now);
							cmd.Parameters.AddWithValue("@val3", formData.User);
							cmd.Parameters.AddWithValue("@val4", DatabaseUser.GetUserEts());
							cmd.Parameters.AddWithValue("@val5", DatabaseUser.GetUserDir());
							cmd.Parameters.AddWithValue("@val6", DatabaseUser.GetUserService());
							cmd.Parameters.AddWithValue("@val7", formData.Contenu);
							cmd.Parameters.AddWithValue("@val8", formData.DateDebut);
							cmd.Parameters.AddWithValue("@val9", formData.DateFin);
							cmd.Parameters.AddWithValue("@val10", formData.Observations);
							cmd.Parameters.AddWithValue("@val11", formData.Elimination);
							cmd.Parameters.AddWithValue("@val12", formData.Communication);
							cmd.Parameters.AddWithValue("@val13", formData.RequestGroup);
							cmd.Parameters.AddWithValue("@val14", 1);
							cmd.Parameters.AddWithValue("@val15", 1);

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
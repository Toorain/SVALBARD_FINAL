using System;
using System.Collections.Generic;
using System.Configuration;
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
		private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["TempFormDataHolder"].ConnectionString;

		private int Id { get; set; }
		private string Contenu { get; set; }
		private int DateDebut { get; set; }
		private int DateFin { get; set;  }
		private string Observations { get; set;  }
		private string LinkedCote { get; set; }

		public static void DecypherData(string data)
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
					Id = Convert.ToInt32(jsonElm["id"]),
					Contenu = jsonElm["contenu"].ToString(),
					DateDebut = Convert.ToInt32(jsonElm["date_debut"]),
					DateFin = Convert.ToInt32(jsonElm["date_fin"]),
					Observations = jsonElm["observations"].ToString(),
					LinkedCote = jsonElm["linked_cote"].ToString()
				};
				formDataList.Add(newFormData);
			}
			PushData(formDataList);
		}

		private static void PushData(List<FormData> data)
		{
			using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
			{
				string cmdString = "INSERT INTO [dbo].[tempData]"
									+ " ([id] ,[contenu] ,[date_debut] ,[date_fin] ,[observations], [linked_cote])"
									+ " VALUES"
									+ " (@val1, @val2, @val3, @val4, @val5, @val6)";
				using (SqlCommand cmd = new SqlCommand())
				{
					cmd.Connection = sqlConn;
					cmd.CommandText = cmdString;
					sqlConn.Open();

					foreach (FormData formData in data) 
					{
						cmd.Parameters.AddWithValue("@val1", formData.Id);
						cmd.Parameters.AddWithValue("@val2", formData.Contenu);
						cmd.Parameters.AddWithValue("@val3", Convert.ToInt32(formData.DateDebut));
						cmd.Parameters.AddWithValue("@val4", Convert.ToInt32(formData.DateFin));
						cmd.Parameters.AddWithValue("@val5", formData.Observations);
						cmd.Parameters.AddWithValue("@val6", formData.LinkedCote);

						cmd.ExecuteNonQuery();
						
						cmd.Parameters.Clear();
					}					
				}
			}
		}
		
		public static void DropTempTable()
		{
			using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
			{
				string cmdString = "DELETE FROM [dbo].[tempData]";
				using (SqlCommand cmd = new SqlCommand())
				{
					cmd.Connection = sqlConn;
					cmd.CommandText = cmdString;
					sqlConn.Open();

					cmd.ExecuteNonQuery();
				}
			}
		}
	}
}
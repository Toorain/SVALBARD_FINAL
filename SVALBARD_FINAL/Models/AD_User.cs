using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Principal;
using System.Web;
using System.Windows.Forms;
using Microsoft.AspNet.Identity;
using MongoDB.Driver.Core.Configuration;

namespace WebApplication1.Models
{
	public class AdUser
	{
		public string Id { get; set; }
		public string Nom { get; set; }
		public string Prenom { get; set; }
		public string NomAffiche { get; set; }
		public string Site { get; set; }
		public string Service { get; set; }
		public string Direction { get; set; }
		public string AdresseMail { get; set; }
		public string Telephone { get; set; }

		private static readonly string ConnectionStringPatrimoine = ConfigurationManager.ConnectionStrings["Patrimoine"].ConnectionString;
		private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

		public static string GetUserIdentity(string user)
		{
			string userIdentity = "";
			
			int found = user.IndexOf("\\", StringComparison.Ordinal);

			userIdentity = user.Substring(found + 1).ToUpper();
			
			return userIdentity;
		} 

		public static bool IsUserAdmin(string user)
		{
			// Connect to the Database
			using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
			{
				string cmdString = "SELECT role FROM [dbo].[ApplicationUser] WHERE last_name = @val1";
				using (SqlCommand cmd = new SqlCommand())
				{
					cmd.Connection = sqlConn;
					cmd.CommandText = cmdString;
					cmd.Parameters.AddWithValue("@val1", user );
        
					sqlConn.Open();
        
					var dr = cmd.ExecuteReader();
        
					while (dr.Read())
					{
						if (dr["role"].ToString() == "1")
						{
							return true;
						}
					}
					return false;
				}
			}
		}
		
		public static string GetCurrentUserAuthorization(string currentUser)
		{
			if (currentUser != null)
			{
				// Connect to the Database
				using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
				{
					string cmdString = "SELECT role FROM [dbo].[ApplicationUser] WHERE last_name = @val1";
					using (SqlCommand cmd = new SqlCommand())
					{
						cmd.Connection = sqlConn;
						cmd.CommandText = cmdString;
						cmd.Parameters.AddWithValue("@val1", currentUser);
        
						sqlConn.Open();
        
						var dr = cmd.ExecuteReader();
						string currentUserRoleId = "";
						while (dr.Read())
						{
							if (dr.HasRows)
							{
								currentUserRoleId = dr["role"].ToString();
							}
							else
							{
								// 3 = Consultation, everyone can consult archives.
								currentUserRoleId = "3";
							}
						}
						return currentUserRoleId;
					}
				}
			}
			return "NoUser";
		}
		
		public static AdUser GetUserInfos(string user)
		{
			using (SqlConnection sqlConn = new SqlConnection(ConnectionStringPatrimoine))
			{
				string cmdString = "SELECT [id],[Nom],[Prenom],[NomAffiche],[AdresseMail],[Site],[Direction],[Service],[telephone] FROM [dbo].[AD_CCIT] WHERE Nom = @val1 AND CompanyID = @val2";
				using (SqlCommand cmd = new SqlCommand())
				{
					cmd.Connection = sqlConn;
					cmd.CommandText = cmdString;
					cmd.Parameters.AddWithValue("@val1", user);
					cmd.Parameters.AddWithValue("@val2", "CCINCA");

					sqlConn.Open();

					var dr = cmd.ExecuteReader();

					while (dr.Read())
					{
						AdUser adUserObj = new AdUser
						{
							Id = dr["id"].ToString().ToUpper(),
							Nom = dr["Nom"].ToString(),
							Prenom = dr["Prenom"].ToString(),
							NomAffiche = dr["NomAffiche"].ToString(),
							AdresseMail = dr["AdresseMail"].ToString(),
							Site = dr["Site"].ToString(),
							Direction = dr["Direction"].ToString(),
							Service = dr["Service"].ToString(),
							Telephone = dr["telephone"].ToString()
						};
						return adUserObj;
					}

					return null;
				}
			}
		}
	}
}
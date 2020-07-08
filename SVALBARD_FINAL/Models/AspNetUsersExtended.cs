using System.Configuration;
using System.Data.SqlClient;

namespace WebApplication1.Models
{
	public class AspNetUsersExtended
	{
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Ets { get; set; }
		public string Dir { get; set; }
		public string Service { get; set; }

		/// <summary>
		/// Adds every elements in AspUserExtended taken from Inscription panel
		/// </summary>
		/// <param name="id">Identifier</param>
		/// <param name="firstName">First name </param>
		/// <param name="lastName">Last name</param>
		/// <param name="ets">Etablissement</param>
		/// <param name="dir">Direction</param>
		/// <param name="service">Service</param>
		public static void PushAdditionalUserData(string id, string firstName, string lastName, string ets, string dir, string service)
		{
			string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
			// Connect to the Database
			using (SqlConnection sqlConn = new SqlConnection(connectionString))
			{
				string cmdString = "INSERT INTO [dbo].[AspNetUsersExtended]" +
				                   " VALUES " +
				                   " (@val1, @val2, @val3, @val4, @val5, @val6);";
				using (SqlCommand cmd = new SqlCommand())
				{
					cmd.Connection = sqlConn;
					cmd.CommandText = cmdString;
					cmd.Parameters.AddWithValue("@val1", id);
					cmd.Parameters.AddWithValue("@val2",firstName);
					cmd.Parameters.AddWithValue("@val3",lastName);
					cmd.Parameters.AddWithValue("@val4",ets);
					cmd.Parameters.AddWithValue("@val5",dir);
					cmd.Parameters.AddWithValue("@val6",service);

					sqlConn.Open();

					cmd.ExecuteNonQuery();
				}
			}
		}
	}
}
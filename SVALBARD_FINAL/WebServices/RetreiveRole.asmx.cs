using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;

namespace WebApplication1.WebServices
{
	/// <summary>
	/// Description résumée de RetreiveRole
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
	// [System.Web.Script.Services.ScriptService]
	public class RetreiveRole : WebService
	{
		private string _roleId;

		[WebMethod]
		public void ClickedModal(string userId)
		{
			string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

			string cmdString = " SELECT Name FROM AspNetUserRoles" 
		                   + " LEFT JOIN AspNetRoles"
		                   + " ON AspNetUserRoles.RoleId = AspNetRoles.Id"
		                   + " WHERE UserId = @val1";
			using (SqlConnection sqlConn = new SqlConnection(connectionString))
			{
				using (SqlCommand cmd = new SqlCommand())
				{
					cmd.Connection = sqlConn;
					cmd.CommandText = cmdString;
					cmd.Parameters.AddWithValue("@val1", userId);
					sqlConn.Open();

					var dr = cmd.ExecuteReader();

					while (dr.Read())
					{
						_roleId = dr.GetString(0);
					}
					Context.Response.Write(_roleId);
					Context.Response.ContentType = "text/plain";
				}
			}
		}
	}
}
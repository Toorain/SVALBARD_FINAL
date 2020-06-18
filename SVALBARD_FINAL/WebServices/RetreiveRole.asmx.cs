using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Windows.Forms;

namespace WebApplication1
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
		public string RoleId;

		[WebMethod]
		public void ClickedModal(string UserId)
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
					cmd.Parameters.AddWithValue("@val1", UserId);
					sqlConn.Open();

					var dr = cmd.ExecuteReader();

					while (dr.Read())
					{
						RoleId = dr.GetString(0);
					}
					Context.Response.Write(RoleId);
					Context.Response.ContentType = "text/plain";
				}
			}
		}
	}
}
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class AdminPanel : Page
    {
        protected bool pageRender = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            var user = HttpContext.Current.User.Identity;
            
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=aspnet-WebApplication1-20200317091700;Integrated Security=True";

            // Connect to the Database
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Select * FROM AspNetUserRoles", sqlConn);
                sqlConn.Open();

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    if (user.GetUserId() == dr["UserId"].ToString() && dr["RoleId"].ToString() == "1")
                    {
                        pageRender = true;
                    }
                    else
                    {
                        pageRender = false;
                    }
                }
                sqlConn.Close();
            }
        }
    }
}
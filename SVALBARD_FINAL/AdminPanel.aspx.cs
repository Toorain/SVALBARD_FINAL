using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class AdminPanel : Page
    {
        protected bool PageRender = false;
        protected object JsonData;
        protected string UserId;
        protected string UserActualCategory;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Change bg-secondary to any color you prefer.
            RoleList.Items[4].Attributes.Add("class", "bg-secondary");

            var user = HttpContext.Current.User.Identity;

            bool isAdmin = DatabaseUser.IsUserAdmin(user);

            if (isAdmin)
            {
                RenderTable();
                PageRender = true;
            } else
            {
                PageRender = false;
            }

        }

        /// <summary>
        ///     RenderTable displays the list of users registered on the Application
        /// </summary>
        /// 
        private void RenderTable()
        {
            // CRITICAL : Change this connection string to the one where users are actually stored (Needs to be changed : Source)
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            // Connect to the Database.
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                string cmdString = "SELECT Id, Email, PhoneNumber, UserName FROM AspNetUsers";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = cmdString;

                    sqlConn.Open();

                    var dr = cmd.ExecuteReader();
                    List<DatabaseUser> datas = new List<DatabaseUser>();

                    while (dr.Read())
                    {
                        DatabaseUser userSql = new DatabaseUser
                        {
                            Id = dr["Id"].ToString(),
                            Email = dr["Email"].ToString(),
                            PhoneNumber = dr["PhoneNumber"].ToString(),
                            UserName = dr["UserName"].ToString()
                        };
                        datas.Add(userSql);
                    }
                    JsonData = JsonConvert.SerializeObject(datas);
                }
            }
        }
        protected void ChangeUserStatus(object sender, EventArgs e)
        {
            string userId = userIdAdmin.Value;
            string roleId = RoleList.SelectedItem.Value;            
            MessageBox.Show(userId);


            bool updateSuccess = DatabaseUser.ChangeUserStatus(userId, roleId);

            if (updateSuccess)
            {
                alertRequestSuccess.Visible = true;
                alertSuccessText.InnerText = "L'utilisateur à été mis à jour.";
            }
        }
    }
}
﻿using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace WebApplication1
{
    public partial class AdminPanel : Page
    {
        protected bool pageRender = false;
        protected object jsonData;
        protected string UserId;
        protected string UserActualCategory;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Change bg-secondary to any color you prefer.
            RoleList.Items[3].Attributes.Add("class", "bg-secondary");

            var user = HttpContext.Current.User.Identity;

            // CRITICAL : Change this connection string to the one where users are actually stored (Needs to be changed : Source)
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            // Connect to the Database
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                string cmdString = "SELECT * FROM AspNetUserRoles";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = cmdString;

                    sqlConn.Open();

                    var dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        if (user.GetUserId() == dr["UserId"].ToString() && dr["RoleId"].ToString() == "1")
                        {
                            RenderTable();
                            pageRender = true;
                            return;
                        }
                        else
                        {
                            pageRender = false;
                        }
                    }
                }
            }
            
        }

        /// <summary>
        ///     RenderTable displays the list of users registered on the Application
        /// </summary>
        /// 
        protected void RenderTable()
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
                        DatabaseUser userSQL = new DatabaseUser
                        {
                            ID = dr["Id"].ToString(),
                            Email = dr["Email"].ToString(),
                            PhoneNumber = dr["PhoneNumber"].ToString(),
                            UserName = dr["UserName"].ToString()
                        };
                        datas.Add(userSQL);
                    }
                    jsonData = JsonConvert.SerializeObject(datas);
                }
            }
        }
        protected void ChangeUserStatus(object sender, EventArgs e)
        {
            string UserId = userIdAdmin.Value;
            string RoleId = RoleList.SelectedItem.Value;

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string cmdString = 
            "IF EXISTS(SELECT 'True' FROM AspNetUserRoles WHERE UserId = @val1)"
            + " BEGIN"
                + " UPDATE AspNetUserRoles"
                + " SET UserId = @val1, RoleId = @val2"
                + " WHERE UserId = @val1;"
            + " END"
            + " ELSE"
            + " BEGIN"

                + " SELECT*"
                + " FROM AspNetUsers"
                + " LEFT JOIN AspNetUserRoles"

                + " ON AspNetUsers.Id = AspNetUserRoles.UserId"

                + " WHERE Id = @val1 "

                + " INSERT INTO dbo.AspNetUserRoles(UserId, RoleId)"

                + " VALUES(@val1, @val2);"
            + " END";

            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = cmdString;
                    cmd.Parameters.AddWithValue("@val1", UserId);
                    cmd.Parameters.AddWithValue("@val2", RoleId);

                    sqlConn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            alertRequestSuccess.Visible = true;
            alertSuccessText.InnerText = "L'utilisateur à été mis à jour.";
        }
    }
}
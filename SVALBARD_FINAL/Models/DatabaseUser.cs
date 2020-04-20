using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace WebApplication1
{
    public class DatabaseUser
    {
        public string ID { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }

        public static string GetCurrentUserAuthorization(string CurrentUser)
        {
            if (CurrentUser != null)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                // Connect to the Database
                using (SqlConnection sqlConn = new SqlConnection(connectionString))
                {
                    string cmdString = "SELECT RoleId FROM AspNetUserRoles WHERE UserId = @val1";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = sqlConn;
                        cmd.CommandText = cmdString;
                        cmd.Parameters.AddWithValue("@val1", CurrentUser);

                        sqlConn.Open();

                        var dr = cmd.ExecuteReader();
                        string CurrentUserRoleId = "";
                        while (dr.Read())
                        {
                           CurrentUserRoleId = dr["RoleId"].ToString();
                        }
                        return CurrentUserRoleId;
                    }
                }
            } else
            {
                return "NoUser";
            }
        }
    }

}
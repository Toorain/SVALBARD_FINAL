using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Web;
using Microsoft.AspNet.Identity;

namespace WebApplication1
{
    public class DatabaseUser
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public string ID { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }

        
        public static bool IsUserAdmin(IIdentity user)
        {
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
                            return true;
                        }
                    }
                    return false;
                }
            }
        }
        public static string GetCurrentUserAuthorization(string CurrentUser)
        {
            if (CurrentUser != null)
            {
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

        public static bool ChangeUserStatus(string UserId, string RoleId)
        {
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
            return true;            
        }
    }

}
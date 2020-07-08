using System.Configuration;
using System.Data.SqlClient;
using System.Security.Principal;
using System.Web;
using System.Windows.Forms;
using Microsoft.AspNet.Identity;

namespace WebApplication1.Models
{
    public class DatabaseUser
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private static string _id;
        public string Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }

        private static string GetUserIdentity()
        {
            return HttpContext.Current.User.Identity.Name;
        }
        public static string GetUserFirstName()
        {
            _id = GetUserIdentity();
            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            {
                string cmdString = "SELECT first_name FROM AspNetUsersExtended WHERE Id = @val1";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = cmdString;
                    cmd.Parameters.AddWithValue("@val1", _id);

                    sqlConn.Open();

                    var dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        return dr["first_name"].ToString();
                    }
                    return "";
                }
            }
        }
        
        public static string GetUserLastName()
        {
            _id = GetUserIdentity();
            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            {
                string cmdString = "SELECT last_name FROM AspNetUsersExtended WHERE Id = @val1";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = cmdString;
                    cmd.Parameters.AddWithValue("@val1", _id);

                    sqlConn.Open();

                    var dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        return dr["last_name"].ToString();
                    }
                    return "";
                }
            }
        }
        
        public static string GetUserEts()
        {
            _id = GetUserIdentity();
            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            {
                string cmdString = "SELECT ets FROM AspNetUsersExtended WHERE Id = @val1";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = cmdString;
                    cmd.Parameters.AddWithValue("@val1", _id);

                    sqlConn.Open();

                    var dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        return dr["ets"].ToString();
                    }
                    return "";
                }
            }
        }
        
        public static string GetUserDir()
        {
            _id = GetUserIdentity();
            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            {
                string cmdString = "SELECT dir FROM AspNetUsersExtended WHERE Id = @val1";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = cmdString;
                    cmd.Parameters.AddWithValue("@val1", _id);

                    sqlConn.Open();

                    var dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        return dr["dir"].ToString();
                    }
                    return "";
                }
            }
        }
        
        public static string GetUserService()
        {
            _id = GetUserIdentity();
            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            {
                string cmdString = "SELECT service FROM AspNetUsersExtended WHERE Id = @val1";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = cmdString;
                    cmd.Parameters.AddWithValue("@val1", _id);

                    sqlConn.Open();

                    var dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        return dr["service"].ToString();
                    }
                    return "";
                }
            }
        }

        public static bool IsUserAdmin(IIdentity user)
        {
            // Connect to the Database
            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
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
        public static string GetCurrentUserAuthorization(string currentUser)
        {
            if (currentUser != null)
            {
                // Connect to the Database
                using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
                {
                    string cmdString = "SELECT RoleId FROM AspNetUserRoles WHERE UserId = @val1";
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
                           currentUserRoleId = dr["RoleId"].ToString();
                        }
                        return currentUserRoleId;
                    }
                }
            } else
            {
                return "NoUser";
            }
        }

        public static bool ChangeUserStatus(string userId, string roleId)
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

            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = cmdString;
                    cmd.Parameters.AddWithValue("@val1", userId);
                    cmd.Parameters.AddWithValue("@val2", roleId);

                    sqlConn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            return true;            
        }
        
        /// <summary>
        /// GetArchiviste permet de récupérer l'identifiant de l'archiviste dans la DB, si il y a plus d' UN archiviste, la valeur retournée sera la dernière dans la base de données.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Modifier ce morceau de code pour inclure plusieurs archivistes à l'avenir.</remarks>
        public static string GetArchiviste()
        {
            string archivisteId = "";

            // Connect to the Database
            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            {
                // RoleId : 4 is Archivist Role
                string cmdString = "SELECT UserId FROM AspNetUserRoles WHERE RoleId = 4";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = cmdString;

                    sqlConn.Open();

                    var dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        archivisteId = dr[0].ToString();
                    }
                }
                return archivisteId;
            }
        }
    }

}
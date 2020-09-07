using System.Configuration;
using System.Data.SqlClient;
using System.Security.Principal;
using System.Web;
using System.Windows.Forms;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;

namespace WebApplication1.Models
{
    public class DatabaseUser
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private static readonly string ConnectionStringOther = ConfigurationManager.ConnectionStrings["Patrimoine"].ConnectionString;

        
        private static string _id;
        public string Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }

        private static string GetUserIdentity()
        {
            return HttpContext.Current.User.Identity.Name;
        }

        
        
        /// <summary>
        /// Check in CUBA/PATRIMOINE/AD_CCIT as CCINCA if user is in there, if not, user can't use the application.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool IsUserAllowed(string user)
        {
            using (SqlConnection sqlConn = new SqlConnection(ConnectionStringOther))
            {
                string cmdString = "SELECT Nom FROM [dbo].[AD_CCIT] WHERE Nom = @val1 AND CompanyID = @val2";
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
                        if (!dr["Nom"].ToString().IsNullOrWhiteSpace())
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }
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
        
        
       
        
        public static bool ChangeUserStatus(string userId, string roleId)
        {
            string cmdString =
            "IF EXISTS(SELECT 'True' FROM [dbo].[ApplicationUser] WHERE id = @val1)"
            + " BEGIN"
                + " UPDATE [dbo].[ApplicationUser]"
                + " SET role = @val2"
                + " WHERE Id = @val1;"
            + " END"
            + " ELSE"
            + " BEGIN"
                + " INSERT INTO Archives_Users.dbo.ApplicationUser (Id, first_name, last_name, ets, dir, service, role) "
                + " SELECT "
                + " PATRIMOINE.dbo.AD_CCIT.id, "
                + " PATRIMOINE.dbo.AD_CCIT.Prenom, "
                + " PATRIMOINE.dbo.AD_CCIT.Nom, "
                + " PATRIMOINE.dbo.AD_CCIT.Site, "
                + " PATRIMOINE.dbo.AD_CCIT.Direction,"
                + " PATRIMOINE.dbo.AD_CCIT.Service,"
                + " @val2 "
                + " FROM PATRIMOINE.dbo.AD_CCIT "
                + " WHERE id = @val1 "
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
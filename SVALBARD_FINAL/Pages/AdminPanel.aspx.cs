using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Windows.Forms;
using Newtonsoft.Json;
using WebApplication1.Models;


namespace WebApplication1.Pages
{
    public partial class AdminPanel : Page
    {
        protected bool PageRender = false;
        protected object JsonData;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Change bg-secondary to any color you prefer.
            RoleList.Items[4].Attributes.Add("class", "bg-secondary");

            if (Request.LogonUserIdentity != null)
            {
                string user = AdUser.GetUserIdentity(Request.LogonUserIdentity.Name);

                bool isAdmin = AdUser.IsUserAdmin(user);
            
                if (isAdmin)
                {
                    RenderTable();
                    PageRender = true;
                } else
                {
                    PageRender = false;
                }
            }
        }

        /// <summary>
        ///     RenderTable displays the list of users registered on the Application
        /// </summary>
        /// 
        private void RenderTable()
        {
            // CRITICAL : Change this connection string to the one where users are actually stored (Needs to be changed : Source)
            string connectionStringPatrimoine = ConfigurationManager.ConnectionStrings["Patrimoine"].ConnectionString;

            // Connect to the Database.
            using (SqlConnection sqlConn = new SqlConnection(connectionStringPatrimoine))
            {
                string cmdString = "SELECT DISTINCT id, AdresseMail, telephone, NomAffiche FROM [dbo].[AD_CCIT] WHERE CompanyID = @val1";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = cmdString;
                    cmd.Parameters.AddWithValue("@val1", "CCINCA");

                    sqlConn.Open();

                    var dr = cmd.ExecuteReader();
                    List<AdUser> datas = new List<AdUser>();

                    while (dr.Read())
                    {
                        AdUser adUser = new AdUser
                        {
                            Id = dr["id"].ToString(),
                            AdresseMail = dr["AdresseMail"].ToString(),
                            Telephone = dr["telephone"].ToString(),
                            NomAffiche = dr["NomAffiche"].ToString()
                        };
                        datas.Add(adUser);
                    }
                    JsonData = JsonConvert.SerializeObject(datas);
                }
            }
        }
        
        protected void ChangeUserStatus(object sender, EventArgs e)
        {
            string userId = userIdAdmin.Value;
            string roleId = RoleList.SelectedItem.Value;

            bool updateSuccess = DatabaseUser.ChangeUserStatus(userId, roleId);

            if (updateSuccess)
            {
                alertRequestSuccess.Visible = true;
                alertSuccessText.InnerText = "L'utilisateur à été mis à jour.";
            }
        }
    }
}
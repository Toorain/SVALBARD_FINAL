using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1.WebServices
{
    /// <summary>
    /// Description résumée de FetchAllUsersService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
    // [System.Web.Script.Services.ScriptService]
    public class FetchAllUsersService : WebService
    {

        [WebMethod]
        public void FetchAllUsers()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Archives"].ConnectionString;

            List<AdUser> datas = new List<AdUser>();
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                string cmdString = "SELECT id, Nom, Prenom, NomAffiche, telephone, AdresseMail, Site, Direction, Service FROM [dbo].[AD_CCIT] WHERE CompanyID = @val1";
                using (SqlCommand cmd = new SqlCommand()) 
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = cmdString;
                    cmd.Parameters.AddWithValue("@val1", "CCINCA");

                    sqlConn.Open();

                    var dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        AdUser adUser = new AdUser
                        {
                            Id = dr["id"].ToString(),
                            Nom = dr["Nom"].ToString(),
                            Prenom = dr["Prenom"].ToString(),
                            NomAffiche = dr["NomAffiche"].ToString(),
                            Telephone = dr["telephone"].ToString(),
                            AdresseMail = dr["AdresseMail"].ToString(),
                            Site = dr["Site"].ToString(),
                            Direction = dr["Direction"].ToString(),
                            Service = dr["Service"].ToString()
                        };
                        datas.Add(adUser);
                    }
                    Context.Response.Write(JsonConvert.SerializeObject(datas));
                } 
            }
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Services;

namespace WebApplication1
{
    /// <summary>
    /// Description résumée de UserRequestService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
    // [System.Web.Script.Services.ScriptService]
    public class UserRequestService : WebService
    {

        [WebMethod]
        public void GetDataIssuer(string userID)
        {
            string connectionString = @"Data Source=SHOGUN;Initial Catalog=logsArchives;Integrated Security=True";
            var datas = new List<Logs>();

            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Select * FROM logsArchive", sqlConn);
                sqlConn.Open();

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    if(dr["issuerID"].ToString() == userID)
                    {
                        Logs logs = new Logs
                        {
                            ID = Convert.ToInt32(dr["ID"]),
                            Date = Convert.ToDateTime(dr["date"].ToString()),
                            IssuerID = dr["issuerID"].ToString(),
                            IssuerEts = dr["issuerEts"].ToString(),
                            IssuerDir = dr["issuerDir"].ToString(),
                            IssuerService = dr["issuerService"].ToString(),
                            ArchiveID = dr["ArchiveID"].ToString(),
                            Action = Convert.ToInt32(dr["action"])
                        };
                        datas.Add(logs);
                    };
                }
                Context.Response.Write(JsonConvert.SerializeObject(datas));
            }
        }
    }
}

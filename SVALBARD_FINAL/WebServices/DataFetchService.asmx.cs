using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;
using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1.WebServices
{
    /// <summary>
    /// Description résumée de DataFetchService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
    // [System.Web.Script.Services.ScriptService]
    public class DataFetchService : WebService
    {
        /// <summary>
        /// GetDataArchives method recovers all datas and returns them as json
        /// </summary>
        /// <remarks>This method is used in DataTableArchives.js</remarks>
        [WebMethod]        
        public void GetDataArchives()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Archives"].ConnectionString;

            List<DataSql> datas = new List<DataSql>();
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                string cmdString = "SELECT versement, etablissement, direction, service, dossiers, extremes, elimination, communication, cote, localisation FROM ArchivesV2";
                using (SqlCommand cmd = new SqlCommand()) 
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = cmdString;

                    sqlConn.Open();

                    var dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        // We verify that Elimination doesn't start with 'éliminé', if so we don't fetch. 
                        if(!dr["elimination"].ToString().ToLower().Contains("éliminé") 
                           && !dr["communication"].ToString().ToLower().Contains("éliminé")
                           && !dr["communication"].ToString().ToLower().Contains("eliminé"))
                        {
                            DataSql dataSql = new DataSql
                            {
                                Cote = dr["cote"].ToString(),
                                Versement = string.IsNullOrEmpty(dr["versement"].ToString())
                                    ? new DateTime(1900, 1, 1)
                                    : Convert.ToDateTime(dr["versement"].ToString()),
                                Etablissement = dr["etablissement"].ToString(),
                                Direction = dr["direction"].ToString(),
                                Service = dr["service"].ToString(),
                                Dossiers = dr["dossiers"].ToString(),
                                Extremes = dr["extremes"].ToString(),
                                Elimination = dr["elimination"].ToString(),
                                Localisation = dr["localisation"].ToString()
                            };
                            datas.Add(dataSql);
                        }
                    }
                    Context.Response.Write(JsonConvert.SerializeObject(datas));
                } 
            }
        }

        
    }
}

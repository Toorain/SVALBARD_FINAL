using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Services;

namespace WebApplication1
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

        [WebMethod]
        public void GetData()
        {
            string connectionString = @"Data Source=SHOGUN;Initial Catalog=archives;Integrated Security=True";

            var datas = new List<DataSQL>();
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Select * from ArchivesV2", sqlConn);
                sqlConn.Open();

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var dataSQL = new DataSQL
                    {
                        /*ID = Convert.ToInt32(dr["ID"].ToString()),*/
                        Versement = string.IsNullOrEmpty(dr["versement"].ToString()) ? new DateTime(1900, 1, 1) : Convert.ToDateTime(dr["versement"].ToString()),
                        Etablissement = dr["etablissement"].ToString(),
                        Direction = dr["direction"].ToString(),
                        Service = dr["service"].ToString(),
                        Dossiers = dr["dossiers"].ToString(),
                        Extremes = dr["extremes"].ToString(),
                        Elimination = dr["elimination"].ToString(),
                        Communication = dr["communication"].ToString(),
                        Cote = dr["cote"].ToString(),
                        Localisation = dr["localisation"].ToString()
                        /* CL = string.IsNullOrEmpty(dr["CL"].ToString()) ? 0 : Convert.ToInt32(dr["CL"].ToString()),
                        Chrono = string.IsNullOrEmpty(dr["chrono"].ToString()) ? 0 : Convert.ToInt32(dr["chrono"].ToString()),
                        Calc = dr["calc"].ToString()*/
                    };
                    datas.Add(dataSQL);
                }
                Context.Response.Write(JsonConvert.SerializeObject(datas));
                sqlConn.Close();
            }
        }
    }
}

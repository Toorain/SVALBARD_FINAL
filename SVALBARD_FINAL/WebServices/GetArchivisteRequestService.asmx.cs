using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Windows.Forms;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1.WebServices
{
    /// <summary>
    /// Description résumée de GetArchivisteRequestService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
    // [System.Web.Script.Services.ScriptService]
    public class GetArchivisteRequestService : System.Web.Services.WebService
    {
        /// <summary>
        /// A WebService to retreive every users in DB
        /// </summary>
        /// <remarks>
        /// Parameter userID is given by an Ajax call in DataTablesArchiviste.js :
        /// <example>
        /// <code>
        /// $.ajax({
        /// serverSide: true, 
        ///         type: "POST",
        ///         dataType: "json",
        ///         async: true,
        ///         url: "UserRequestService.asmx/GetDataIssuer",
        ///         data: { userID: $("#archivisteID").val() },
        ///         contentType: "application/x-www-form-urlencoded; charset=UTF-8", 
        ///         crossDomain: true,
        /// </code>
        /// </example>
        /// Then GetDataIssuer connects to logsArchive Database and fetch this user's requests on the archive
        /// </remarks>
        /// <returns>
        /// Returns a JSON when a POST call is made with an unique identifier
        /// </returns>
        /// <param name="userId">An unique user identifier retreived from <see cref="Demandes.Page_Load(object, EventArgs)"/></param>
        /// 
        [WebMethod]
        public void GetDataArchiviste(string userId)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["LogsArchives"].ConnectionString;
            var datas = new List<Logs>();

            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                string cmdString = "SELECT [dbo].[logsArchive].*, [dbo].[status].status_name"
                                 + " FROM [dbo].[logsArchive]"
                                 + " LEFT JOIN [dbo].[status]"
                                 + " ON [dbo].[logsArchive].status = dbo.status.status_id";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = cmdString;

                    sqlConn.Open();

                    var dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        {
                            Logs logs = new Logs
                            {
                                ID = Convert.ToInt32(dr["ID"]),
                                Date = Convert.ToDateTime(dr["date"].ToString()),
                                IssuerID = !dr["issuerFirstName"].ToString().IsNullOrWhiteSpace() && !dr["issuerLastName"].ToString().IsNullOrWhiteSpace()
                                    ? dr["issuerLastName"] + " " + dr["issuerFirstName"]
                                    : dr["issuerID"].ToString(),
                                IssuerEts = dr["issuerEts"].ToString(),
                                IssuerDir = dr["issuerDir"].ToString(),
                                IssuerService = dr["issuerService"].ToString(),
                                ArchiveID = dr["ArchiveID"].ToString(),
                                Localization = dr["localization"].ToString(),
                                Action = Convert.ToInt32(dr["action"]),
                                Status = dr["status_name"].ToString(),
                                Origin = "NOT_PAL",
                                RequestGroup = null
                            };
                            datas.Add(logs);
                        };
                    }
                }
            }
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                string cmdString = "SELECT [dbo].[logsArchivePAL].*, [dbo].[status].[status_name]"
                                 + " FROM [dbo].[logsArchivePAL]"
                                 + " LEFT JOIN [dbo].[status]"
                                 + " ON [dbo].[logsArchivePAL].[status] = [dbo].[status].[status_id]";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = cmdString;

                    sqlConn.Open();

                    var dr = cmd.ExecuteReader();
                    
                    while (dr.Read())
                    {
                        Logs logs = new Logs
                        {
                            ID = 0,
                            Date = Convert.ToDateTime(dr["date"].ToString()),
                            IssuerID = dr["user"].ToString(),
                            IssuerEts = dr["ets"].ToString(),
                            IssuerDir = dr["dir"].ToString(),
                            IssuerService = dr["service"].ToString(),
                            ArchiveID = dr["id"].ToString(),
                            Localization = dr["localization"].ToString(),
                            Action = Convert.ToInt32(dr["action"]),
                            Status = dr["status_name"].ToString(),
                            Origin = "PAL",
                            RequestGroup = dr["request_group"].ToString()
                        };
                        datas.Add(logs);
                    }
                }
            }
            Context.Response.Write(JsonConvert.SerializeObject(datas));
        }
    }

}

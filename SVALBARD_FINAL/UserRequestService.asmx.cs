using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        /// <summary>
        /// A WebService to retreive every users in DB
        /// </summary>
        /// <remarks>
        /// Parameter userID is given by an Ajax call in DataTablesDemandes.js :
        /// <example>
        /// <code>
        /// $.ajax({
        /// serverSide: true, 
        ///         type: "POST",
        ///         dataType: "json",
        ///         async: true,
        ///         url: "UserRequestService.asmx/GetDataIssuer",
        ///         data: { userID: $("#userID").val() },
        ///         contentType: "application/x-www-form-urlencoded; charset=UTF-8", 
        ///         crossDomain: true,
        /// </code>
        /// </example>
        /// Then GetDataIssuer connects to logsArchive Database and fetch this user's requests on the archive
        /// </remarks>
        /// <returns>
        /// Returns a JSON when a POST call is made with an unique identifier
        /// </returns>
        /// <param name="userID">An unique user identifier retreived from <see cref="Demandes.Page_Load(object, EventArgs)"/></param>
        [WebMethod]
        public void GetDataIssuer(string userID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LogsArchive"].ConnectionString;
            var datas = new List<Logs>();

            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                string cmdString = "Select * FROM logsArchive";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = cmdString;

                    sqlConn.Open();

                    var dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        if (dr["issuerID"].ToString() == userID)
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
}

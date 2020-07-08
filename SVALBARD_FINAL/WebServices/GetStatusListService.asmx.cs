using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1.WebServices
{
    /// <summary>
    /// Description résumée de GetStatusListService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
    // [System.Web.Script.Services.ScriptService]
    public class GetStatusListService : WebService
    {
    /// <summary>
    /// Gets the status list according to linked action (Add / See / Delete)
    /// </summary>
    /// <param name="action"> Can be either 1 (Ajout), 2 (Retrait) or 3 (Destruction)</param>
        [WebMethod]
        public void GetStatusList(int action)
        {
            List<List<string>> arrayArray = Logs.GetStatus();
            Context.Response.Write(JsonConvert.SerializeObject(arrayArray));
        }
    }
}

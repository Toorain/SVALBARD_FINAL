using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebApplication1.Models;

namespace WebApplication1.WebServices
{
	/// <summary>
	/// Description résumée de DESFetchService
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
	// [System.Web.Script.Services.ScriptService]
	public class DESFetchService : WebService
	{
		[WebMethod]
		public void GetDes(string category, string relatedData)
		{
			List<DES> es = DES.GetData(category, relatedData);
			Context.Response.Write(JsonConvert.SerializeObject(es));
		}
	}
}
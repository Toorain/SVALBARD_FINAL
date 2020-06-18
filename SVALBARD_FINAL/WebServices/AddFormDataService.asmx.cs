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
	/// Description résumée de AddFormDataService
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
	// [System.Web.Script.Services.ScriptService]
	public class AddFormDataService : WebService
	{   

		[WebMethod]
		public void PushData(string data)
		{
			FormData.DecypherData(data);
		}
	}
}
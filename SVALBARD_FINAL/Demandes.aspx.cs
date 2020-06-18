using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using Microsoft.Reporting.WebForms;
using WebApplication1.Models;

namespace WebApplication1
{
	public partial class Demandes : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				userID.Value = User.Identity.GetUserId();
			}
		}
		protected void GeneratePdf(object sender, EventArgs eventArgs)
		{
			bool success = PdfMethods.GeneratePdf(Identifier.Value, Cote.Value, rptViewerDemandes);
			if (success)
			{
				collapseElm.Attributes["class"] = "collapse show";
			}

		}
	}
}
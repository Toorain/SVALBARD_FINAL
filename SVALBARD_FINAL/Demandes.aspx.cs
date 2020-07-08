using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using System.Windows.Forms;
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
			string requestGroup = DataSql.GetRequestGroup(Identifier.Value);
			
			bool success = PdfMethods.GeneratePdfPal(requestGroup, rptViewerDemandesPdf);

			//bool success = PdfMethods.GeneratePdfPalSolo(Identifier.Value, rptViewerDemandesPdf);
			if (success)
			{
				collapseElmPdf.Attributes["class"] = "collapse show";
			}
		}
		
		protected void GenerateEtiquette(object sender, EventArgs eventArgs)
		{
			bool success = EtiquetteMethods.GenerateEtiquetteSolo(Identifier.Value, rptViewerDemandesEtiquette);
			if (success)
			{
				collapseElmEtiquette.Attributes["class"] = "collapse show";
			}
		}
	}
}
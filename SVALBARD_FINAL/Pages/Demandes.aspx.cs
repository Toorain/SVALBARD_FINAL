using System;
using System.Web.UI;
using WebApplication1.Models;

namespace WebApplication1.Pages
{
	public partial class Demandes : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			AdUser user = new AdUser();
			
			if (Request.LogonUserIdentity != null)
			{
				user = AdUser.GetUserInfos(AdUser.GetUserIdentity(Request.LogonUserIdentity.Name));
			}
			
			// userID.Value = "33700";
			
			if(!IsPostBack)
			{
				// TODO : Uncomment all that and remove userID.Value above
				if (user != null)
				{
					userID.Value = user.Id;
				}
				else
				{
					Response.Redirect("/NotADUser.aspx");
				}
			}
		}
		protected void GeneratePdf(object sender, EventArgs eventArgs)
		{
			string requestGroup = LogsPal.GetRequestGroup(Identifier.Value);
			
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
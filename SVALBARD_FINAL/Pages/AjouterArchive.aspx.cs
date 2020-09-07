using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Windows.Forms;
using Microsoft.AspNet.Identity;
using WebApplication1.Models;

namespace WebApplication1.Pages
{
	public partial class AjouterArchive : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.LogonUserIdentity != null)
			{
				AdUser user = AdUser.GetUserInfos(AdUser.GetUserIdentity(Request.LogonUserIdentity.Name));
				// TODO : Change this for if else under.
				LoggedUser.Value = "ZEKRI";
				LoggedUserId.Value = "33700";
				if (user != null)
				{
					// LoggedUser.Value = user.Nom;
					// LoggedUserId.Value = user.Id;
				}
				else
				{
					//Response.Redirect("/NotADUser.aspx");
				}
				
			}
			

			// int lastItem = DataSql.GetLastItemArchive();
            
			//List<DES> desList = DES.GetDataZero("etablissement");
			
		}


		protected void GeneratePdfPal_Click(object sender, EventArgs e)
		{
			bool success = PdfMethods.GeneratePdfPal(coteGeneratePdf.Value, rptViewerPAL);
            

			alertRequestAdd.Visible = true;
			alertAdd.InnerHtml =
				"Demande d'ajout effectuée avec succès. Rendez-vous dans l'onglet <a href='/Demandes'>Demandes</a> pour voir l'avancement.";
            
			bool etiquetteSuccess = GenerateEtiquettePal();
            
			if (success && etiquetteSuccess)
			{
				collapseRow.Attributes["class"] = "row collapse show";
			}
		}

		private bool GenerateEtiquettePal()
		{
			return EtiquetteMethods.GenerateEtiquettePal(coteGeneratePdf.Value, rptViewerEtiquettePAL);
		}

		
	}
}
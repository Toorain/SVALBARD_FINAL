using System;
using System.Collections.Generic;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using WebApplication1.Models;

namespace WebApplication1
{
	public partial class AjouterArchive : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			LoggedUser.Value = User.Identity.GetUserName();
			LoggedUserId.Value = User.Identity.GetUserId();
			
			int lastItem = DataSql.GetLastItemArchive();
            
			List<DES> desList = DES.GetDataZero("etablissement");
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
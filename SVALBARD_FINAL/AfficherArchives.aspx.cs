using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using WebApplication1.Models;
using Button = System.Web.UI.WebControls.Button;

namespace WebApplication1
{
    public partial class AfficherArchives : Page
    {
        private string _requestStatusText;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }

            mainContainer.Visible = false;

            string dbAuthorization = DatabaseUser.GetCurrentUserAuthorization(User.Identity.GetUserId());
            switch (dbAuthorization)
            {
                case "NoUser":
                    // TODO : Add a personal response redirect if user is not connected
                    Response.Redirect("~/");
                    break;
                // 1 = Admin
                case "1":
                    mainContainer.Visible = true;
                    break;
                // 2 = Gestion
                case "2":
                    mainContainer.Visible = true;
                    break;
                // 3 = Consultation
                case "3":
                    mainContainer.Visible = true;
                    modalFooter.Visible = false;
                    consutlationMode.Attributes.Add("class", "col-md-8 offset-md-2 text-center");
                    break;
                // 4 = Archiviste
                case "4":
                    mainContainer.Visible = true;
                    break;
                // 5 = Juridique
                case "5":
                    mainContainer.Visible = true;
                    break;
                default:
                    mainContainer.Visible = false;
                    _requestStatusText = "Vous n'avez pas les droits requis pour consulter cet élément, contactez votre DSI pour plus d'informations";
                    alertRequestSuccess.Visible = false;
                    alertAlreadyRequested.Visible = true;
                    alertRequestedText.InnerText = _requestStatusText;
                    break;
            }
        }

        protected void LogConsulterArchive(object sender, EventArgs e)
        {
            bool connError = false;

            string identifier = archiveCoteID.Value;
            // Connect to the Database
            LogsPal individualRow = DataSql.GetIndividualArchive(identifier);
            bool requestSuccessful = LogsPal.RequestArchive(individualRow);
            
            if (requestSuccessful)
            {
                _requestStatusText = "La demande de retrait de l'archive s'est déroulée avec succès, l'archiviste vous tiendra au courant des prochaines étapes.";
                alertRequestSuccess.Visible = true;
                alertAlreadyRequested.Visible = false;
                alertSuccessText.InnerText = _requestStatusText;
            }
            // Legacy code, should be deleted when done.
            else if(connError)
            {
                _requestStatusText = "Merci de vous connecter";
                alertRequestSuccess.Visible = false;
                alertAlreadyRequested.Visible = true;
                alertRequestedText.InnerText = _requestStatusText;
            } 
            else
            {
                // Throw an error if a request for an Archive already exists
                _requestStatusText = "Le dossier que vous avez demandé n'existe plus dans l'archive ou une personne a déjà demandé son retrait de l'archive.";
                alertRequestSuccess.Visible = false;
                alertAlreadyRequested.Visible = true;
                alertRequestedText.InnerText = _requestStatusText;
            }
            // If request is allowed (not yet requested), we target Table and insert elements to it.
            
        }

        protected void LogRetirerArchive(object sender, EventArgs e)
        {
            
        }
        protected void ModifyArchive_Click(object sender, EventArgs e)
        {
            DataSql.ModifyArchive(archiveID.Value);
        }
    }
}
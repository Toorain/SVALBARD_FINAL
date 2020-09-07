using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Windows.Forms;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using WebApplication1.Models;

namespace WebApplication1.Pages
{
    public partial class AfficherArchives : Page
    {
        private bool _requestStatus = true;
        private string _requestStatusText;
        private string _idGroup;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }

            /* mainContainer.Visible = false;

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
            */
        }
        protected void LogConsulterArchive(object sender, EventArgs e)
        {
            bool connError = false;
            string identifier = arrayDropZoneHidden.Value;
            Array identifierArr = identifier.Split(',');
            List<LogsPal> arrLogsPal = new List<LogsPal>();
            List<string> alreadyRequestedCotes = new List<string>();
            bool firstCycle = true;
            
            foreach (string id in identifierArr)
            {
                if (firstCycle)
                {
                    _idGroup = id;
                    firstCycle = false;
                }
                if (DataSql.CheckIfCoteHasAlreadyBeenRequested(id).IsNullOrWhiteSpace())
                {
                    arrLogsPal.Add(DataSql.GetIndividualArchive(id, _idGroup));
                }
                else
                {
                    alreadyRequestedCotes.Add(id);
                    _requestStatus = false;
                }
            }

            foreach (LogsPal itemLogsPal in arrLogsPal)
            {
                LogsPal.RequestArchive(itemLogsPal);
            }
            
            // Connect to the Database
            /* LogsPal individualRow = DataSql.GetIndividualArchive(identifier);
            bool requestSuccessful = LogsPal.RequestArchive(individualRow);
            */
            if (_requestStatus)
            {
                _requestStatusText = "La demande de retrait de l'archive s'est déroulée avec succès, l'archiviste vous tiendra au courant des prochaines étapes.";
                alertRequestSuccess.Visible = true;
                alertAlreadyRequested.Visible = false;
                alertSuccessText.InnerHtml = _requestStatusText;
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
                _requestStatusText =
                    "Une ou plusieurs références demandées n'existent plus dans l'archive, <br />" +
                    "ou une personne a déjà demandé son retrait de l'archive. <br/>" +
                    "<p class=\"text-danger\" >Ces côtes n'ont pas été ajoutées : " + string.Join(" / ", alreadyRequestedCotes)  + "</p>";
                alertRequestSuccess.Visible = false;
                alertAlreadyRequested.Visible = true;
                alertRequestedText.InnerHtml = _requestStatusText;
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
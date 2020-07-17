using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using WebApplication1.Models;

namespace WebApplication1
{
    
    public partial class SiteMaster : MasterPage
    {
        protected const string AppName = "SVALBARD";
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;
        // Counter is initiated at 0, then proceeds to count then number of new elements in Demandes.
        protected int CountNewElements = 0;
        protected void Page_Init(object sender, EventArgs e)
        {
            // Le code ci-dessous vous aide à vous protéger des attaques XSRF
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out Guid requestCookieGuidValue))
            {
                // Utiliser le jeton Anti-XSRF à partir du cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Générer un nouveau jeton Anti-XSRF et l'enregistrer dans le cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += Master_Page_PreLoad;
        }

        protected void Master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Définir un jeton Anti-XSRF
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Valider le jeton Anti-XSRF
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Échec de la validation du jeton Anti-XSRF.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            archivistePanel.Visible = false;
            archivesPanel.Visible = false;
            ajouterPanel.Visible = false;
            demandesPanel.Visible = false;
            adminPanel.Visible = false;
            juridiquePanel.Visible = false;
            showNewElementsCount.Value = "no_show";

            var user = HttpContext.Current.User.Identity;

            if (user.IsAuthenticated)
            {
                string userId = DatabaseUser.GetCurrentUserAuthorization(user.GetUserId());
                switch (userId)
                {
                    // Administrateur
                    case "1":
                        archivesPanel.Visible = true;
                        ajouterPanel.Visible = true;
                        demandesPanel.Visible = true;
                        adminPanel.Visible = true;
                        break;
                    // Gestionnaire
                    case "2":
                        archivesPanel.Visible = true;
                        ajouterPanel.Visible = true;
                        demandesPanel.Visible = true;
                        break;
                    // Consultation
                    case "3":
                        archivesPanel.Visible = true;
                        break;
                    // Archiviste
                    case "4":
                        showNewElementsCount.Value = "show";
                        CountNewElements = LogsPal.GetNewElementsCount();
                        archivistePanel.Visible = true;
                        archivesPanel.Visible = true;
                        break;
                    case "5":
                        archivesPanel.Visible = true;
                        ajouterPanel.Visible = true;
                        demandesPanel.Visible = true;
                        juridiquePanel.Visible = true;
                        break;
                    default:
                        archivistePanel.Visible = false;
                        archivesPanel.Visible = false;
                        ajouterPanel.Visible = false;
                        demandesPanel.Visible = false;
                        adminPanel.Visible = false;
                        break;
                }
            }
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
    }

}
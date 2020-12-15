using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using WebApplication1.Models;

namespace WebApplication1.Account
{
    public partial class Register : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //List<DES> desList = DES.GetDataZero("etablissement");

            // if (!IsPostBack)
            // {
            //     EtsList.Items.Insert(0, new ListItem() { Value = "-- CHOOSE --", Text = "-- CHOOSE --" });
            //     DirList.Items.Insert(0, new ListItem() { Value = "-- CHOOSE --", Text = "-- CHOOSE --", Selected = true });
            //     ServiceList.Items.Insert(0, new ListItem() { Value = "-- CHOOSE --", Text = "-- CHOOSE --", Selected = true });
            //     foreach (var item in desList)
            //     {
            //         ListItem listItem = new ListItem()
            //         {
            //             Value = item.Name,
            //             Text = item.Name,
            //         };
            //         EtsList.Items.Insert(item.ID, listItem);
            //     } 
            // }
        }
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var user = new ApplicationUser() { UserName = Email.Text, Email = Email.Text };
            IdentityResult result = manager.Create(user, Password.Text);
            if (result.Succeeded)
            {
                // Pour plus d'informations sur l'activation de la confirmation de compte et de la réinitialisation de mot de passe, visitez https://go.microsoft.com/fwlink/?LinkID=320771
                //string code = manager.GenerateEmailConfirmationToken(user.Id);
                //string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                //manager.SendEmail(user.Id, "Confirmez votre compte", "Confirmez votre compte en cliquant <a href=\"" + callbackUrl + "\">ici</a>.");

                signInManager.SignIn( user, isPersistent: false, rememberBrowser: false);
                AspNetUsersExtended.PushAdditionalUserData(Email.Text, First_Name.Text, Last_Name.Text, EtsValue.Value, DirValue.Value, ServiceValue.Value);
                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            }
            else 
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }
    }
}
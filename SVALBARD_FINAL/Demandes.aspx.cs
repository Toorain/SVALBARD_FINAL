using Microsoft.AspNet.Identity;
using System;
using System.Web.UI;

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
    }
}
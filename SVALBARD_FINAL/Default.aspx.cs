﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
 using System.Windows.Forms;
 using WebApplication1.Models;

 namespace WebApplication1
{
    public partial class Default : Page
    {
	    private string _loggedUser;
	    protected void Page_Load(object sender, EventArgs e)
				{
					if (Request.LogonUserIdentity == null) return;
					_loggedUser = AdUser.GetUserIdentity(Request.LogonUserIdentity.Name);

					if (AdUser.GetUserInfos(_loggedUser) == null)
					{
						Response.Redirect("/NotADUser.aspx");
						return;
					}

					SessionUser = AdUser.GetUserInfos(_loggedUser);
					
					if (!DatabaseUser.IsUserAllowed(_loggedUser))
					{
						Response.Redirect("/NotAllowed.aspx");
					}
				}
	    
	    public static AdUser SessionUser
	    { 
		    get 
		    {
			    AdUser value = (AdUser) HttpContext.Current.Session["SessionUser"];
			    return value;
		    }
		    set => HttpContext.Current.Session["SessionUser"] = value;
	    }
    }    
}
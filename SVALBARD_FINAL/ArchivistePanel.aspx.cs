using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class ArchivistePanel : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                archivisteID.Value = User.Identity.GetUserId();

                List<string> arrayStatus = Logs.GetStatus();

                for (int i = 0; i < arrayStatus.Count; i++)
                {
                    ListItem listItem = new ListItem();
                    listItem.Value = arrayStatus[i];
                    listItem.Text = arrayStatus[i];
                    StatusList.Items.Add(listItem);
                }
            }
        }
    }
}
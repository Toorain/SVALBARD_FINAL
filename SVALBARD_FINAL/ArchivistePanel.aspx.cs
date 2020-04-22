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

                List<List<string>> arrayArray = Logs.GetStatus();

                foreach (var item in arrayArray)
                {
                    for (int i = 0; i < item.Count; i++)
                    {
                        ListItem listItem = new ListItem
                        {
                            Value = item[i],
                            Text = item[i]
                        };
                        StatusList.Items.Add(listItem);
                    }
                }
            }
        }
    }
}
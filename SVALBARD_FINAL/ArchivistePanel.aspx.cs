using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Microsoft.Reporting.WebForms;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class ArchivistePanel : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<string> DatabaseElements = DES.getDatabaseElements();

            int indexOfItem = 0;
            string leftSplitter = "";

            foreach (var item in DatabaseElements)
            {
                List<string> TableItems = DES.getTableElements(item);

                if (indexOfItem != 0) { leftSplitter = "vertical-line"; };

                // Generate a Div element with custom class
                System.Web.UI.HtmlControls.HtmlGenericControl div = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                div.Attributes["class"] = "col-md-4 text-center " + leftSplitter + "";

                // Generate h5 for column title
                System.Web.UI.HtmlControls.HtmlGenericControl h5 = new System.Web.UI.HtmlControls.HtmlGenericControl("h5")
                {
                    InnerText = Misc.UppercaseFirst(item)
                };
                div.Controls.Add(h5);

                // Generate two labels
                System.Web.UI.HtmlControls.HtmlGenericControl label1 = new System.Web.UI.HtmlControls.HtmlGenericControl("label")
                {
                    InnerHtml = "Code de référence : <input type='text' class='form-control'/>"
                };
                System.Web.UI.HtmlControls.HtmlGenericControl label2 = new System.Web.UI.HtmlControls.HtmlGenericControl("label")
                {
                    InnerHtml = "Nom : <input type='text' class='form-control'/>"
                };
                div.Controls.Add(label1);
                div.Controls.Add(label2);

                /*// Generate a dropdown menu IF Etablissement || Service
          UpdateStatusox dropdown1 = new ComboBox
                {

                };
                ComboBox dropdwon2 = new ComboBox
                {

                };*/

                // Generate an <asp:Button>
                System.Web.UI.WebControls.Button Button = new System.Web.UI.WebControls.Button
                {
                    Text = "Ajouter " + Misc.UppercaseFirst(item),
                    CssClass = "btn btn-outline-success",
                    ID = item
                };
                Button.ClientIDMode = ClientIDMode.Static;
                Button.Click += new EventHandler(this.AddSmth);
                div.Controls.Add(Button);

                ModalAjouter.Controls.Add(div);

                indexOfItem++;
            }

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

        void AddSmth(Object sender, EventArgs e)
        {
            System.Web.UI.WebControls.Button btn = (System.Web.UI.WebControls.Button)sender;
            DES.AddSmth(btn.ID);
        }
        
        protected void GeneratePdf(object sender, EventArgs eventArgs)
        {
            bool success = PdfMethods.GeneratePdf(Identifier.Value, Cote.Value, rptViewerArchiviste);

            if (success)
            {
                collapseElm.Attributes["class"] = "collapse show";
            }
        }

        protected void UpdateStatus(object sender, EventArgs e)
        {
            Logs.UpdateStatus(ArchiveCote.Value, StatusList.SelectedValue);
        }
    }
}
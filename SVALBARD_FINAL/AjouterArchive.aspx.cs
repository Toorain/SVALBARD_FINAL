using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class AjouterArchive : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            int lastItem = DataSQL.GetLastItemArchive();
            
            List<DES> DESList = DES.GetDataZero("etablissement");

            if (!IsPostBack)
            {
                EtsList.Items.Insert(0, new ListItem() { Value = "-- CHOOSE --", Text = "-- CHOOSE --" });
                DirList.Items.Insert(0, new ListItem() { Value = "-- CHOOSE --", Text = "-- CHOOSE --", Selected = true });
                ServiceList.Items.Insert(0, new ListItem() { Value = "-- CHOOSE --", Text = "-- CHOOSE --", Selected = true });
                foreach (var item in DESList)
                {
                    ListItem listItem = new ListItem()
                    {
                        Value = item.Name,
                        Text = item.Name,
                    };
                    EtsList.Items.Insert(item.ID, listItem);
                }
            }
        }

        protected void AddArchive(object sender, EventArgs eventArgs)
        {
            // coteValidationServer.Value.Length >= 7 <= ADD THIS TO CHECK COTE LENGTH BEFORE SENDING
            if (EtsValue.Value != "-- CHOOSE --"
                && DirValue.Value != "-- CHOOSE --"
                && ServiceValue.Value != "-- CHOOSE --" )
            {
                DateTime date = new DateTime();
                date = DateTime.Now;
                string issuerID = User.Identity.GetUserId();
                string firstName = validationFirstName.Value.ToUpper();
                string lastName = validationLastName.Value.ToUpper();
                string ets = EtsValue.Value.ToUpper();
                string dir = DirValue.Value.ToUpper();
                string service = ServiceValue.Value.ToUpper();
                string receiverId = DatabaseUser.GetArchiviste();
                string cote = coteValidationServer.Value;
                int action = 1;
                int status = 1;
            
                Logs.AddArchive(date,issuerID, firstName, lastName, ets, dir, service, receiverId, cote, action, status);
                alertRequestAdd.Visible = true;
                alertAdd.InnerHtml =
                    "Demande d'ajout effectuée avec succès. Rendez-vous dans l'onglet <a href='/Demandes'>Demandes</a> pour voir l'avancement.";
            }
            else
            {
                MessageBox.Show("Test");
            }
            
        }
    }
}
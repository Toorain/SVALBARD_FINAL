using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using MongoDB.Bson;
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
            DateTime date = new DateTime();
            date = DateTime.Now;
            string firstName = validationFirstName.Value.ToUpper();
            string lastName = validationLastName.Value.ToUpper();
            string ets = EtsValue.Value.ToUpper();
            string dir = DirValue.Value.ToUpper();
            string service = ServiceValue.Value.ToUpper();
            string receiverId = "test";
            string cote = coteValidationServer.Value;
            int action = 1;
            int status = 1;
            
            Logs.AddArchive(date, firstName, lastName, ets, dir, service, receiverId, cote, action, status);
        }
    }
}
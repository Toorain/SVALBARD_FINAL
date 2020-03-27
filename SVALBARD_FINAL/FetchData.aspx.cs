using Microsoft.AspNet.Identity;
using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class FetchData : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void LogRetirerArchive(object sender, EventArgs e)
        {
            // Recover the name of the clicked button
            Button btn = (Button)sender;
            // Capture it in a variable
            string buttonText = btn.Text;

            string requestStatusText;
            bool connError = false;
            bool canRequestArchive = true;
            string connectionString = @"Data Source=SHOGUN;Initial Catalog=logsArchives;Integrated Security=True";
            Logs log;

            // Connect to the Database
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                // Then request "ID" & "archiveID" columns
                SqlCommand cmd = new SqlCommand("Select ID, archiveID from logsAjoutArchive", sqlConn);
                sqlConn.Open();

                var dr = cmd.ExecuteReader();
                int count = 0;

                // Count how much entries we have in the Database
                while (dr.Read())
                {
                    if (dr["ID"].ToString() != "" || dr["ID"] != null)
                    {
                        count++;
                    }
                    // We check if a request has already been made in the "log" table, if so user won't be able to request another withdraw
                    if (dr["archiveID"].ToString() == archiveID.Value)
                    {
                        // Change canRequestArchive so the next part won't be called and will send a Bootstrap alert
                        canRequestArchive = false;
                    }
                }
                /* String value becomes an int code
                 * ajouter = 1
                 * retirer = 2
                 * detruire = 3
                 */
                int codeAction;
                switch(buttonText.ToLower())
                {
                    case "ajouter":
                        codeAction = 1;
                        break;
                    case "retirer":
                        codeAction = 2;
                        break;
                    case "detruire":
                        codeAction = 3;
                        break;
                    default:
                        codeAction = 0;
                        break;
                }
                // Generate a new Logs object
                log = new Logs
                {
                    ID = count + 1,
                    Date = DateTime.Now,
                    // Detects if user is logged-in, if False an alert is emmited and user is prompted to log-in, Insert does not complete.
                    IssuerID = User.Identity.IsAuthenticated ? User.Identity.GetUserId() : Convert.ToString(connError = true),
                    IssuerEts = validationEts.Text,
                    IssuerDir = validationDir.Text,
                    IssuerService = validationService.Text,
                    ArchiveID = canRequestArchive ? archiveID.Value : "ALREADY REQUESTED",
                    Action = codeAction
                };

                sqlConn.Close();
            }
            // If request is allowed (not yet requested), we target Table and insert elements to it.
            if (canRequestArchive && !connError)
            {

                string cmdString = "INSERT INTO [dbo].[logsAjoutArchive] (ID,date,issuerID,issuerEts,issuerDir,issuerService,receiverID,archiveID,action) VALUES (@val1, @val2, @val3, @val4, @val5, @val6, @val7, @val8, @val9)";
                using (SqlConnection sqlConn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = sqlConn;
                        cmd.CommandText = cmdString;
                        cmd.Parameters.AddWithValue("@val1", log.ID);
                        cmd.Parameters.AddWithValue("@val2", log.Date);
                        cmd.Parameters.AddWithValue("@val3", log.IssuerID);
                        cmd.Parameters.AddWithValue("@val4", log.IssuerEts);
                        cmd.Parameters.AddWithValue("@val5", log.IssuerDir);
                        cmd.Parameters.AddWithValue("@val6", log.IssuerService);
                        cmd.Parameters.AddWithValue("@val7", "");
                        cmd.Parameters.AddWithValue("@val8", log.ArchiveID);
                        cmd.Parameters.AddWithValue("@val9", log.Action);

                        sqlConn.Open();
                        cmd.ExecuteNonQuery();

                        requestStatusText = "La demande de retrait de l'archive s'est déroulée avec succès, l'archiviste vous tiendra au courant des prochaines étapes.";
                        alertRequestSuccess.Visible = true;
                        alertAlreadyRequested.Visible = false;
                        alertSuccessText.InnerText = requestStatusText;
                    }                    
                }
            } 
            else if(connError)
            {
                requestStatusText = "Merci de vous connecter";
                alertRequestSuccess.Visible = false;
                alertAlreadyRequested.Visible = true;
                alertRequestedText.InnerText = requestStatusText;
            } 
            else
            {
                // Throw an error if a request for an Archive already exists
                requestStatusText = "Le dossier que vous avez demandé n'existe plus dans l'archive ou une personne a déjà demandé son retrait de l'archive.";
                alertRequestSuccess.Visible = false;
                alertAlreadyRequested.Visible = true;
                alertRequestedText.InnerText = requestStatusText;
            }            
        }
    }
}
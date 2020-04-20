using Microsoft.AspNet.Identity;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class AfficherArchives : Page
    {
        protected string requestStatusText;
        protected void Page_Load(object sender, EventArgs e)
        {
            mainContainer.Visible = false;

            string DbAuthorization = DatabaseUser.GetUserAuthorization(User.Identity.GetUserId());
            switch (DbAuthorization)
            {
                case "NoUser":
                    // TODO : Add a personnal response redirect if user is not connected
                    Response.Redirect("~/");
                    break;
                // 1 = Admin
                case "1":
                    mainContainer.Visible = true;
                    break;
                // 2 = Gestion
                case "2":
                    mainContainer.Visible = true;
                    break;
                // 3 = Consultation
                case "3":
                    mainContainer.Visible = true;
                    modalFooter.Visible = false;
                    formRetrait.Visible = false;
                    consutlationMode.Attributes.Add("class", "col-md-8 offset-md-2 text-center");
                    break;
                case "4":
                    mainContainer.Visible = true;
                    break;
                default:
                    mainContainer.Visible = false;
                    requestStatusText = "Vous n'avez pas les droits requis pour consulter cet élément, contactez votre DSI pour plus d'informations";
                    alertRequestSuccess.Visible = false;
                    alertAlreadyRequested.Visible = true;
                    alertRequestedText.InnerText = requestStatusText;
                    break;
            }
        }

        protected void LogRetirerArchive(object sender, EventArgs e)
        {
            // Recover the name of the clicked button
            Button btn = (Button)sender;
            // Capture it in a variable
            string buttonText = btn.Text;

            
            bool connError = false;
            bool canRequestArchive = true;
            string connectionString = ConfigurationManager.ConnectionStrings["LogsArchive"].ConnectionString;
            Logs log;

            // Connect to the Database
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                string cmdString = "Select ID, archiveID from logsArchive";
                // Then request "ID" & "archiveID" columns
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandText = cmdString;

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
                        if (dr["archiveID"].ToString() == archiveCoteID.Value)
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
                    switch (buttonText.ToLower())
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
                        ArchiveID = canRequestArchive ? archiveCoteID.Value : "ALREADY REQUESTED",
                        Action = codeAction
                    };
                } 
            }
            // If request is allowed (not yet requested), we target Table and insert elements to it.
            if (canRequestArchive && !connError)
            {

                string cmdString = "INSERT INTO [dbo].[logsArchive] (ID,date,issuerID,issuerEts,issuerDir,issuerService,receiverID,archiveID,action,status) VALUES (@val1, @val2, @val3, @val4, @val5, @val6, @val7, @val8, @val9, @val10)";
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
                        cmd.Parameters.AddWithValue("@val10", 1);

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

        protected void ModifyArchive_Click(object sender, EventArgs e)
        {
            DataSQL.ModifyArchive(archiveID.Value);
        }
    }
}
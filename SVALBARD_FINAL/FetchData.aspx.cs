using Microsoft.AspNet.Identity;
using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Windows.Forms;

namespace WebApplication1
{
    public partial class FetchData : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void LogRetirerArchive(object sender, EventArgs e)
        {
            string requestStatusText;
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

                // Generate a new Logs object
                log = new Logs
                {
                    ID = count + 1,
                    Date = DateTime.Now,
                    // TODO : Check if user is connected, else prevent action from ending and send Error (Maybe use a Cookie ?)
                    IssuerID = User.Identity.GetUserId(),
                    IssuerEts = validationEts.Text,
                    IssuerDir = validationDir.Text,
                    IssuerService = validationService.Text,
                    ArchiveID = canRequestArchive? archiveID.Value : "ALREADY REQUESTED" 
                    
                };

                sqlConn.Close();
            }
            // If request is allowed (not yet requested), we target Table and insert elements to it.
            if (canRequestArchive)
            {
                string cmdString = "INSERT INTO [dbo].[logsAjoutArchive] (ID,date,issuerID,issuerEts,issuerDir,issuerService,receiverID,archiveID) VALUES (@val1, @val2, @val3, @val4, @val5, @val6, @val7, @val8)";
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

                        sqlConn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                requestStatusText = "La demande de retrait de l'archive s'est déroulée avec succès, l'archiviste vous tiendra au courant des prochaines étapes.";
                alertRequestSuccess.Visible = true;
                alertAlreadyRequested.Visible = false;
                alertSuccessText.InnerText = requestStatusText;
            } else
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
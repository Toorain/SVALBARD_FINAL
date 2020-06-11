using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Microsoft.Reporting.WebForms;

namespace WebApplication1
{
    public partial class TestReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GeneratePdf(object sender, EventArgs e)
        {
            try
            {
                string value = val.Text;
                string urlReportServer = ConfigurationManager.AppSettings["ReportServer"];
                rptViewer.Visible = true;
                rptViewer.ProcessingMode = ProcessingMode.Remote;
                // urlReportServer = http://glucide/reportserver
                rptViewer.ServerReport.ReportServerUrl = new Uri(urlReportServer);
                rptViewer.ServerReport.ReportPath = "/PDFGenerator/Retrait";
                rptViewer.ShowToolBar = true;
                
                List<ReportParameter> paramList = new List<ReportParameter>();
                paramList.Add(new ReportParameter("ID", value, false));
                rptViewer.ServerReport.SetParameters(paramList);
                rptViewer.ServerReport.Refresh();
                value = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
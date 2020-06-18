using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Reporting.WebForms;

namespace WebApplication1.Models
{
	public abstract class PdfMethods
	{
		public static bool GeneratePdf(string identifier, string cote, ReportViewer target)
		{
			try
			{
				target.ServerReport.Refresh();
				// string value = val.Text;
				string urlReportServer = ConfigurationManager.AppSettings["ReportServer"];
				target.Visible = true;
				target.ProcessingMode = ProcessingMode.Remote;
				// urlReportServer = http://glucide/reportserver
				target.ServerReport.ReportServerUrl = new Uri(urlReportServer);
				target.ServerReport.ReportPath = "/PDFGenerator/Versement";
				target.ShowToolBar = true;
                
				List<ReportParameter> paramList = new List<ReportParameter>();
				paramList.Add(new ReportParameter("ID", identifier, false));
				paramList.Add(new ReportParameter("archive_id", cote, false));
				target.ServerReport.SetParameters(paramList);
				target.ServerReport.Refresh();

				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
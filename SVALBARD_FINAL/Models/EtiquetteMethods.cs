using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Reporting.WebForms;

namespace WebApplication1.Models
{
	public static class EtiquetteMethods
	{
		public static bool GenerateEtiquettePal(string identifier, ReportViewer target)
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
				target.ServerReport.ReportPath = "/PDFGenerator/Etiquette";
				target.ShowToolBar = true;
                
				List<ReportParameter> paramList = new List<ReportParameter>();
				paramList.Add(new ReportParameter("ID", identifier, false));
				target.ServerReport.SetParameters(paramList);
				target.ServerReport.Refresh();

				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		
		public static bool GenerateEtiquetteSolo(string identifier, ReportViewer target)
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
				target.ServerReport.ReportPath = "/PDFGenerator/EtiquetteSolo";
				target.ShowToolBar = true;
                
				List<ReportParameter> paramList = new List<ReportParameter>();
				paramList.Add(new ReportParameter("ID", identifier, false));
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
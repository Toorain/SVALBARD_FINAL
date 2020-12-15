<%@ Page Title="Demandes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Demandes.aspx.cs" Inherits="WebApplication1.Pages.Demandes" %>
<%@ Register TagPrefix="rsweb" Namespace="Microsoft.Reporting.WebForms" Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table id="tableDemandes" class="table table-striped table-hover responsive">
        <thead>
            <tr>
                <!-- <th>ID</th> -->
                <th>Date</th>
                <!-- <th>IssuerID</th>
                <th>IssuerEts</th>
                <th>IssuerDir</th>
                <th>IssuerService</th>
                -->
                <th>ArchiveID</th>
                <th>Action</th>
                <th>Status</th>
                <th>???</th>
            </tr>
        </thead>
        <tfoot>
        </tfoot>
    </table>
    
    <div class="row">
        <div class="col-md-6">
            <div id="dropdownBordereauDemandes" class="dropdownPdf d-flex align-items-center" data-toggle="collapse" href="#collapseElmPdf" role="button" aria-expanded="false" aria-controls="collapseExample">
                <div class="btn btn-outline-secondary m-auto text-center">Afficher le bordereau</div>
            </div>
            <div runat="server" clientidmode="Static" class="collapse" ID="collapseElmPdf">
                <rsweb:ReportViewer ID="rptViewerDemandesPdf" runat="server" Font-Names="Verdana" Font-Size="8pt" ProcessingMode="Remote" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="1000px" Width="850px" 
                  BackColor="" ClientIDMode="AutoID" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" 
                  LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" 
                  SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" 
                  ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px"
                  ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226">
                  
                  <ServerReport></ServerReport>
                </rsweb:ReportViewer>
            </div>
            <asp:Button runat="server" ID="ButtonGeneratePdf" ClientIDMode="Static" OnClick="GeneratePdf" CssClass="d-none" />
        </div>
        <div class="col-md-6">
            <div id="dropdownEtiquetteDemandes" class="dropdownPdf d-flex align-items-center" data-toggle="collapse" href="#collapseElmEtiquette" role="button" aria-expanded="false" aria-controls="collapseExample">
                <div class="btn btn-outline-secondary m-auto text-center">Afficher l'étiquette</div>
            </div>
            <div runat="server" clientidmode="Static" class="collapse" ID="collapseElmEtiquette">
                <rsweb:ReportViewer ID="rptViewerDemandesEtiquette" runat="server" Font-Names="Verdana" Font-Size="8pt" ProcessingMode="Remote" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="1000px" Width="850px" 
                  BackColor="" ClientIDMode="AutoID" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" 
                  LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" 
                  SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" 
                  ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px"
                  ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226">
                  
                  <ServerReport></ServerReport>
                </rsweb:ReportViewer>
            </div>
            <asp:Button runat="server" ID="ButtonGenerateEtiquette" ClientIDMode="Static" OnClick="GenerateEtiquette" CssClass="d-none" />
        </div>
    </div>
    <asp:HiddenField runat="server" ID="Identifier" ClientIDMode="Static" Value="" />
    <asp:HiddenField runat="server" ID="Cote" ClientIDMode="Static" Value="" />
    <asp:HiddenField runat="server" ID="userID" ClientIDMode="Static" />
</asp:Content>

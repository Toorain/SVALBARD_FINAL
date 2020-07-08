<%@ Page Title="Archiviste Panel" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ArchivistePanel.aspx.cs" Inherits="WebApplication1.ArchivistePanel" %>
<%@ Register TagPrefix="rsweb" Namespace="Microsoft.Reporting.WebForms" Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="archivisteID" ClientIDMode="Static" />
    <!-- #region Alert -->
    <asp:Panel ID="alertRequestSuccess" runat="server" Visible="false">
        <div runat="server" ID="alertType" clientidmode="Static" class="alert alert-dismissible fade show text-center " role="alert">
            <h5 id="alertSuccessText" runat="server"></h5>
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
    </asp:Panel>
    <!-- #endregion Alert -->
    <div class="modal fade" id="modalArchiviste" tabindex="-1" role="dialog" aria-labelledby="modalArchiviste" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalLongTitle">Modifier l'archive<span class=""></span></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:HiddenField runat="server" ID="ArchiveCote" ClientIDMode="Static"/>
                    <asp:HiddenField runat="server" ID="ArchiveAction" ClientIDMode="Static"/>
                    <div class="row">
                        <div class="col-md-6">
                            <h5>Modifier le status</h5>
                            <asp:DropDownList ID="StatusList" runat="server" CssClass="form-control mb-1" />
                            <asp:Button runat="server" ID="ModifyStatus" Text="Modifier le status" CssClass="btn btn-success" OnClick="UpdateStatus" />
                        </div>
                        <div class="col-md-6">
                            <h5>Modifier l'emplacement</h5>
                            <input type="text" class="form-control mb-1" runat="server" ID="Emplacement" ClientIDMode="Static" />
                            <asp:Button runat="server" ID="ModifyEmplacement" Text="Modifier l'emplacement" CssClass="btn btn-success" OnClick="UpdateEmplacement" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalAjout" tabindex="-1" role="dialog" aria-labelledby="modalAjout" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Ajouter un élément<span class=""></span></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:HiddenField runat="server" ID="HiddenField1" ClientIDMode="Static"/>

                    <div runat="server" class="row" ID="ModalAjouter" ClientIDMode="Static"></div>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-success">Tout ajouter</button>
                </div>
            </div>
        </div>
    </div>
    <span class="btn btn-success float-right" data-toggle="modal" data-target="#modalAjout">Ajouter ?quelqueChose?</span>
    <table id="tableArchiviste" class="table table-striped table-hover">
        <thead>
            <tr>
                <!-- <th>ID</th> -->
                <th>Date</th>
                <th>Nom et prénom du demandeur</th>
                <th>Etablissement du demandeur</th>
                <th>Direction du demandeur</th>
                <th>Service du demandeur</th>
                <th>Cote</th>
                <th>Groupe d'archives</th>
                <th>Emplacement</th>
                <th>Action</th>
                <th>Status</th>
                <th>???</th>
                <th class="d-none">Origin</th>
            </tr>
        </thead>
        <tfoot>
        </tfoot>
    </table>
    <div id="dropdownPdfDemandes" class="dropdownPdf" data-toggle="collapse" href="#collapseElm" role="button" aria-expanded="false" aria-controls="collapseExample">
        <h3 class="m-auto text-center">v -- Afficher le PDF -- v</h3>
    </div>
    <div runat="server" clientidmode="Static" class="collapse" ID="collapseElm">
       <rsweb:ReportViewer ID="rptViewerArchiviste" runat="server" Font-Names="Verdana" Font-Size="8pt" ProcessingMode="Remote" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="1000px" Width="850px" 
           BackColor="" ClientIDMode="AutoID" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" 
           LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" 
           SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" 
           ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px"
           ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226">
           
           <ServerReport></ServerReport>
       </rsweb:ReportViewer>
    </div>
    <asp:Button runat="server" ID="ButtonGeneratePdf" ClientIDMode="Static" OnClick="GeneratePdf" CssClass="d-none" />
    <asp:HiddenField runat="server" ID="Origin" ClientIDMode="Static" Value="" />
    <asp:HiddenField runat="server" ID="Identifier" ClientIDMode="Static" Value="" />
    <asp:HiddenField runat="server" ID="Cote" ClientIDMode="Static" Value="" />
    <asp:HiddenField runat="server" ID="userID" ClientIDMode="Static" />
</asp:Content>

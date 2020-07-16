<%@ Page Title="Archiviste Panel" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ArchivistePanel.aspx.cs" Inherits="WebApplication1.ArchivistePanel" %>
<%@ Register TagPrefix="rsweb" Namespace="Microsoft.Reporting.WebForms" Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="archivisteID" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="ArchiveCote" ClientIDMode="Static"/>
    <asp:HiddenField runat="server" ID="ArchiveAction" ClientIDMode="Static"/>
    <asp:HiddenField runat="server" ID="Cote" ClientIDMode="Static" Value="" />
    <asp:HiddenField runat="server" ID="Identifier" ClientIDMode="Static" Value="" />
    <asp:HiddenField runat="server" ID="Origin" ClientIDMode="Static" Value="" />
    <asp:HiddenField runat="server" ID="userID" ClientIDMode="Static" />
    

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
                    
                    <div class="row">
                        <div class="col-md-6">
                            <h5>Modifier le status</h5>
                            <select id="StatusList" name="StatusList" class="form-control mb-1" onchange="checkIfAdded()" ></select>
                            <asp:Button runat="server" ID="ModifyStatus" Text="Modifier le status" CssClass="btn btn-success" OnClick="UpdateStatus" />
                        </div>
                        <div id="modifyLocalization" class="col-md-6">
                            <h5>Modifier l'emplacement</h5>
                            <input type="text" class="form-control mb-1" runat="server" ID="Emplacement" ClientIDMode="Static" />
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

                    <div runat="server" class="row" ID="ModalAjouter" ClientIDMode="Static"></div>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-success">Tout ajouter</button>
                </div>
            </div>
        </div>
    </div>
    <!-- <span class="btn btn-success float-right" data-toggle="modal" data-target="#modalAjout">Ajouter ?quelqueChose?</span> -->
    <nav>
      <div class="nav nav-tabs justify-content-around" id="nav-tab" role="tablist">
        <a class="nav-item archiviste-tabs nav-link active" id="nav-ajout-tab" data-toggle="tab" href="#nav-ajout" role="tab" aria-controls="nav-ajout" aria-selected="true">Ajout <span runat="server" class="badge badge-danger" id="NewNotifAjout" clientidmode="Static"></span></a>
        <a class="nav-item archiviste-tabs nav-link" id="nav-consultation-tab" data-toggle="tab" href="#nav-consultation" role="tab" aria-controls="nav-consultation" aria-selected="false">Consultation <span runat="server" class="badge badge-danger" ID="NewNotifConsult" clientidmode="Static"></span></a>
        <a class="nav-item archiviste-tabs nav-link" id="nav-destruction-tab" data-toggle="tab" href="#nav-destruction" role="tab" aria-controls="nav-destruction" aria-selected="false">Destruction ou AD <span runat="server" class="badge badge-danger" ID="NewNotifDestru" clientidmode="Static"></span></a>
      </div>
    </nav>
    <div class="tab-content " id="nav-tabContent">
        <div class="tab-pane fade show active" id="nav-ajout" role="tabpanel" aria-labelledby="nav-ajout-tab">
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
                        <th class="d-none">TEST</th>
                    </tr>
                </thead>
              <tfoot>
              </tfoot>
            </table>
        </div>
      <div class="tab-pane fade" id="nav-consultation" role="tabpanel" aria-labelledby="nav-consultation-tab">...</div>
      <div class="tab-pane fade" id="nav-destruction" role="tabpanel" aria-labelledby="nav-destruction-tab">...</div>
    </div>
    <div id="dropdownPdfDemandes" class="dropdownPdf text-center" data-toggle="collapse" href="#collapseElm" role="button" aria-expanded="false" aria-controls="collapseExample">
        <h3 class="m-auto text-center btn btn-secondary">Afficher le PDF</h3>
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
    
</asp:Content>

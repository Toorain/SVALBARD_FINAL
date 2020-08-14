<%@ Page Title="AfficherArchives" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AfficherArchives.aspx.cs" Inherits="WebApplication1.Pages.AfficherArchives" EnableEventValidation="false" %>
<%@ Register TagPrefix="rsweb" Namespace="Microsoft.Reporting.WebForms" Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="MainContent">
    <%--<div class="btn btn-danger" onclick="function getCheckedBoxes() {
                let checkedBoxes = $('input:checkbox:checked');
                for (let i = 0; i < checkedBoxes.length; i++) {
                    for (let j = 1; j < checkedBoxes[i].parentElement.parentElement.children.length; j++ ) {
                        //console.log(checkedBoxes[i].parentElement.parentElement.children[j].innerHTML);
                    }
                }
    }
    getCheckedBoxes()">ClickMe</div>--%>
    <div runat="server" id="mainContainer" class="container-fluid">
        <asp:HiddenField runat="server" ID="arrayDropZoneHidden" ClientIDMode="Static" />
        <!-- #region Alert -->
        <asp:Panel ID="alertAlreadyRequested" runat="server" Visible="false">
            <div class="alert alert-warning alert-dismissible fade show text-center " role="alert">
                <h5 id="alertRequestedText" runat="server"></h5>
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
        </asp:Panel>
        <asp:Panel ID="alertRequestSuccess" runat="server" Visible="false">
            <div class="alert alert-success alert-dismissible fade show text-center " role="alert">
                <h5 id="alertSuccessText" runat="server"></h5>
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
        </asp:Panel>
        <!-- #endregion Alert -->
        <div class="text-center" id="midget-spinner">
            <h1 class="text-center">Please stand-by</h1>
            <div class="spinner-border" role="status">
                <span class="sr-only">Loading...</span>
            </div>
        </div>
        <div class="hiddenLoad m-auto text-center">
            <input id="toggleOverlay" class="float-left" type="checkbox" data-toggle="toggle" href="#overlayDropZone" data-onstyle="secondary" />
            <span class="btn btn-outline-secondary" onclick="enableTourArchive()">Découvrir l'outil d'archivage</span>
            <span id="dropdownPdfConsultation" class="dropdownPdf text-center" data-toggle="collapse" href="#collapseElm" role="button" aria-expanded="false" aria-controls="collapseExample">
                <h3 class="m-auto text-center btn btn-secondary">Afficher le PDF</h3>
            </span>
            <div runat="server" clientidmode="Static" class="collapse" ID="collapseElm">
               <rsweb:ReportViewer ID="rptViewerConsultation" runat="server" Font-Names="Verdana" Font-Size="8pt" ProcessingMode="Remote" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="1000px" Width="850px" 
                   BackColor="" ClientIDMode="AutoID" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" 
                   LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" 
                   SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" 
                   ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px"
                   ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226">
                   
                   <ServerReport></ServerReport>
               </rsweb:ReportViewer>
            </div>
        </div>
        
        <!-- #region Modal -->
        <div class="modal fade" id="modalGetArchive" tabindex="-1" role="dialog" aria-labelledby="modalGetArchive" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title w-100 text-center pl-4" id="modalLongTitle">Informations</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div runat="server" id="consutlationMode" class="col-md-6 offset-3 text-center">
                                <p>Archive : <asp:Label ID="archiveCote" runat="server" ClientIDMode="Static" /></p>
                                <p>Ajoutée le : <span id="archiveVersement"></span></p>
                                <p>
                                    Par :
                                    <br />
                                    <span>Etablissement :
                                        <asp:Label ID="archiveEtablissement" runat="server" ClientIDMode="Static" /></span>
                                    <br />
                                    <span>Direction :
                                        <asp:Label ID="archiveDirection" runat="server" ClientIDMode="Static" /></span>
                                    <br />
                                    <span>Service :
                                        <asp:Label ID="archiveService" runat="server" ClientIDMode="Static" /></span>
                                </p>
                                <p>Élimination : <asp:Label ID="archiveElimination" runat="server" ClientIDMode="Static" /></p>
                                <p>
                                    Information complémentaires :
                                    <br />
                                    <asp:Label ID="archiveCommentaire" runat="server" ClientIDMode="Static" />
                                    
                                </p>
                                <!-- Used in Scripts/scripts_datatable/dataTableArchives.js  line:75-->
                                <asp:HiddenField ID="archiveCoteID" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="archiveID" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="localization" runat="server" ClientIDMode="Static" />
                            </div>
                            
                        </div>
                    </div>
                    <div runat="server" id="modalFooter" class="modal-footer">
                        <asp:Button runat="server" ID="btn_retirer" OnClick="LogConsulterArchive" Text="Consulter" CssClass="submitModal btn btn-warning" ClientIDMode="Static"  />
                        <asp:Button runat="server" ID="btn_detruire" OnClick="LogRetirerArchive" Text="Detruire" CssClass="submitModal btn btn-outline-danger" ClientIDMode="Static"  />
                    </div>
                </div>
            </div>
        </div>
        <!-- #endregion Modal -->
    <table id="tableArchive" class="table table-striped table-hover hiddenLoad responsive">
        <thead>
            <tr id="column-name">
                <th>Cote</th>
                <th>Versement</th>
                <th>Etablissement</th>
                <th>Direction</th>
                <th>Service</th>
                <th>Dossiers</th>
                <th>Extremes</th>
                <th>Elimination</th>
                <th>Localisation</th>
            </tr>
            <tr id="column-search">
                <th class="search-field"></th>
                <th class="search-field"></th>
                <th class="search-field"></th>
                <th class="search-field"></th>
                <th class="search-field"></th>
                <th class="search-field"></th>
                <th class="search-field"></th>
                <th class="search-field"></th>
                <th class="search-field"></th>
            </tr>
        </thead>
        <tfoot>
        </tfoot>
    </table>
    </div>
    <div id="overlayDropZone" class="text-center" style="display: none">
        <div class="header-overlay">
            <h2>Elements que je souhaite consulter</h2>
            <button id="validateChoice" class="btn btn-primary" disabled>Valider mes choix</button>
            <button runat="server" id="consultChoice" class="btn btn-success" ClientIDMode="Static">Effectuer la demande</button>
        </div>
        <div id="dropReceiver">
            
        </div>
    </div>
</asp:Content>

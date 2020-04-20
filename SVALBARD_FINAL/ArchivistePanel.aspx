<%@ Page Title="Archiviste Panel" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ArchivistePanel.aspx.cs" Inherits="WebApplication1.ArchivistePanel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="archivisteID" ClientIDMode="Static" />
    <!-- #region Alert -->
    <asp:Panel ID="alertRequestSuccess" runat="server" Visible="false">
        <div class="alert alert-success alert-dismissible fade show text-center " role="alert">
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
                    <h5 class="modal-title" id="modalLongTitle">Modifier le status<span class=""></span></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <asp:DropDownList id="StatusList" runat="server">
                            
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>
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
                <th>Emplacement</th>
                <th>Action</th>
                <th>Status</th>
            </tr>
        </thead>
        <tfoot>
        </tfoot>
    </table>
</asp:Content>

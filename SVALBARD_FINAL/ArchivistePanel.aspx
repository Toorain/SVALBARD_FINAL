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
                    <asp:HiddenField runat="server" ID="ArchiveCote" ClientIDMode="Static"/>
                    <div class="row">
                        <asp:DropDownList ID="StatusList" runat="server" CssClass="form-control m-3 w-25" />
                        <asp:Button runat="server" ID="ModifyStatus" Text="Modifier le status" CssClass="btn btn-success" />
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
                <th>Emplacement</th>
                <th>Action</th>
                <th>Status</th>
            </tr>
        </thead>
        <tfoot>
        </tfoot>
    </table>
</asp:Content>

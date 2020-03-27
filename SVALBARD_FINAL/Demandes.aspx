<%@ Page Title="Demandes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Demandes.aspx.cs" Inherits="WebApplication1.Demandes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="userID" ClientIDMode="Static" />
    <table id="tableDemandes" class="table table-striped table-hover">
        <thead>
            <tr>
                <th>ID</th>
                <th>Date</th>
                <th>IssuerID</th>
                <th>IssuerEts</th>
                <th>IssuerDir</th>
                <th>IssuerService</th>
                <th>ArchiveID</th>
                <th>Action</th>
            </tr>
        </thead>
        <tfoot>
        </tfoot>
    </table>
</asp:Content>

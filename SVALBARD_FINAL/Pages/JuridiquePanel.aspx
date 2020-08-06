<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="JuridiquePanel.aspx.cs" Inherits="WebApplication1.Pages.JuridiquePanel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table id="juridiquePanel" class="table table-striped table-hover responsive">
        <thead>
            <tr>
                <th class="col-title">UserName</th>
                <th class="col-title">ID</th>
                <th class="col-title">Email</th>
                <th class="col-title">PhoneNumber</th>
            </tr>
            <tr>
                <th class="search-field"></th>
                <th class="search-field"></th>
                <th class="search-field"></th>
                <th class="search-field"></th>
            </tr>
        </thead>
        <tfoot>
            
        </tfoot>
    </table>
</asp:Content>

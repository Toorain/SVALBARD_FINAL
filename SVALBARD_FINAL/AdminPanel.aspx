<%@ Page Title="Admin Panel" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminPanel.aspx.cs" Inherits="WebApplication1.AdminPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% if (pageRender) {%>
        <h1>TEST</h1>
    <%} else {%>
        <h1>Alors comme ça on tente d'accéder via l'URL</h1>
    <%} %>
</asp:Content>


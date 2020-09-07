<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Tour de l'application</h1>
        <p class="lead">Cliquez sur le bouton ci-dessous pour commencer la visite guidée</p>
        <p class="btn btn-primary" onclick="enableTourMain()">Démarrer la visite &raquo;</p>
    </div>

    

</asp:Content>

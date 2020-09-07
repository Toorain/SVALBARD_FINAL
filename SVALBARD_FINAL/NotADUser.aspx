<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NotADUser.aspx.cs" Inherits="WebApplication1.NotADUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<section class="container-fluid vh-100 d-flex justify-content-center align-items-center m-0 p-0">
        <div class="text-center">
            <h1>Votre profil n'exite pas dans l'Active Directory sur l'application</h1>
            <h3>Rapprochez-vous de votre DSI pour en savoir plus</h3>
            <h5>Infos à communiquer à votre DSI : </h5>
            <ul class="text-left mt-5">
                <li>SERVER : CUBA</li>
                <li>DB : PATRIMOINE</li>
                <li>Table : AD_CCIT</li>
                <li>Problème : L'utilisateur n'existe pas dans la table AD_CCIT</li>
            </ul>
        </div>
    </section>
</asp:Content>

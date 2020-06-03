<%@ Page Title="Ajouter archive" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AjouterArchive.aspx.cs" Inherits="WebApplication1.AjouterArchive" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <form class="needs-validation" novalidate>
            <!-- TODO : Ajouter des dropdown pour limiter le choix de l'etablissment / direction / service, afin de lisser les données de la DB --> 
            <div class="form-row flex-column">
                <div class="row">
                    <asp:DropDownList id="EtsList" CssClass="col-md-4 form-control" runat="server" ClientIDMode="Static" />
                    <asp:hiddenfield runat="server" ID="EtsValue" ClientIDMode="Static" />
                </div>
                <div class="row">
                    <asp:DropDownList id="DirList" CssClass="col-md-4 form-control" runat="server" ClientIDMode="Static" />
                    <asp:hiddenfield runat="server" ID="DirValue" ClientIDMode="Static" />                    
                </div>
                <div class="row">
                    <asp:DropDownList id="ServiceList" CssClass="col-md-4 form-control" runat="server" ClientIDMode="Static" />
                    <asp:hiddenfield runat="server" ID="ServiceValue" ClientIDMode="Static" />             
                </div>
            </div>
        </form>
</asp:Content>

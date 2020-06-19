<%@ Page Title="S'inscrire" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebApplication1.Account.Register" EnableEventValidation="false" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal">
        <h4>Créer un nouveau compte</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="form-group form-row">
            <div class="col-md-4">
                <label for="EtsList">Etablissement</label>
                <div class="mb-3">
                  <asp:DropDownList id="EtsList" CssClass="col-md-12 form-control" runat="server" ClientIDMode="Static" />
                  <asp:hiddenfield runat="server" ID="EtsValue" ClientIDMode="Static" />
                </div>
            </div>
            <div class="col-md-4">
                <label for="DirList">Direction</label>
                <div class="mb-3">
                  <asp:DropDownList id="DirList" CssClass="col-md-12 form-control" runat="server" ClientIDMode="Static" />
                  <asp:hiddenfield runat="server" ID="DirValue" ClientIDMode="Static" />
                </div>
            </div>
            <div class="col-md-4">
                <label for="ServiceList">Service</label>
                <div class="mb-3">
                  <asp:DropDownList id="ServiceList" CssClass="col-md-12 form-control" runat="server" ClientIDMode="Static" />
                  <asp:hiddenfield runat="server" ID="ServiceValue" ClientIDMode="Static" />
                </div>
            </div>
        </div>
        <div class="form-group form-row">
            <div class="col-md-6">
                <asp:Label runat="server" AssociatedControlID="First_Name" CssClass="col-md-2 control-label">Prénom</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="First_Name" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="First_Name"
                        CssClass="text-danger" ErrorMessage="Le champ Prénom est obligatoire." />
                </div>
            </div>
            <div class="col-md-6">
                <asp:Label runat="server" AssociatedControlID="Last_Name" CssClass="col-md-2 control-label">Nom de famille</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="Last_Name" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Last_Name"
                        CssClass="text-danger" ErrorMessage="Le champ Nom de famille est obligatoire." />
                </div>
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Messagerie</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                    CssClass="text-danger" ErrorMessage="Le champ d’adresse de messagerie est obligatoire." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">Mot de passe</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                    CssClass="text-danger" ErrorMessage="Le champ mot de passe est requis." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">Confirmer le mot de passe </asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="Le champ confirmer le mot de passe est requis." />
                <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="Le mot de passe et le mot de passe de confirmation ne correspondent pas." />
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="CreateUser_Click" Text="S'inscrire" CssClass="btn btn-default" />
            </div>
        </div>
    </div>
</asp:Content>

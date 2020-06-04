<%@ Page Title="Ajouter archive" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AjouterArchive.aspx.cs" Inherits="WebApplication1.AjouterArchive" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <form class="needs-validation border" novalidate>
      <div class="info text-center">
        <h3>Veuillez renseigner toutes les information pour effectuer une demande d'ajout à l'archive :</h3>
      </div>
      <div class="form-row justify-content-around">
        <div class="col-md-2 mb-3">
          <div class="mb-3">
            <label for="validationTooltip01">Prénom</label>
            <input type="text" class="form-control" id="validationTooltip01" placeholder="Prénom" value="" required>
            <div class="valid-tooltip">
              Parfait!
            </div>
          </div>
          <div class="mb-3">
            <label for="validationTooltip02">Nom de famille</label>
            <input type="text" class="form-control" id="validationTooltip02" placeholder="Nom de famille" value="" required>
            <div class="valid-tooltip">
              Parfait!
            </div>
          </div>
          <div class="mb-3">
            <label> Etablissement/ Direction / Service</label>
            <div class="mb-3">
                <asp:DropDownList id="EtsList" CssClass="col-md-12 form-control" runat="server" ClientIDMode="Static" />
                <asp:hiddenfield runat="server" ID="EtsValue" ClientIDMode="Static" />
            </div>
            <div class="mb-3">
                <asp:DropDownList id="DirList" CssClass="col-md-12 form-control" runat="server" ClientIDMode="Static" />
                <asp:hiddenfield runat="server" ID="DirValue" ClientIDMode="Static" />                    
            </div>
            <div class="mb-3">
                <asp:DropDownList id="ServiceList" CssClass="col-md-12 form-control" runat="server" ClientIDMode="Static" />
                <asp:hiddenfield runat="server" ID="ServiceValue" ClientIDMode="Static" />             
            </div>
          </div>
          <div class="form-row">
            <!-- TODO : Ajouter des dropdown pour limiter le choix de l'etablissment / direction / service, afin de lisser les données de la DB --> 
            <div class="col-md-12">
              <div class="mb-3">
                <label for="validationTooltip03">City</label>
                <input type="text" class="form-control" id="validationTooltip03" placeholder="City" required>
                <div class="invalid-tooltip">
                  Please provide a valid city.
                </div>
              </div>
              <div class="mb-3">
                <label for="validationTooltip04">State</label>
                <input type="text" class="form-control" id="validationTooltip04" placeholder="State" required>
                <div class="invalid-tooltip">
                  Please provide a valid state.
                </div>
              </div>
              <div class="col-md-6 m-auto">
                <button class="btn btn-primary align-self-auto" type="submit">Submit form</button>
              </div>
            </div>
          </div>
        </div>     
      </div>
    </form>
</asp:Content>

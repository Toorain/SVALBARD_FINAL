<%@ Page Title="Ajouter archive" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AjouterArchive.aspx.cs" Inherits="WebApplication1.AjouterArchive" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <form class="needs-validation border" novalidate>
      <div class="info text-center">
        <h3>Veuillez renseigner toutes les information pour effectuer une demande d'ajout à l'archive :</h3>
      </div>
      <div class="form-row justify-content-around">
        <div class="col-md-2 mb-3">
          <hr>
          <div class="mb-3">
            <label for="validationFirstName">Prénom</label>
            <!-- TODO: Add required --> 
            <input type="text" class="form-control" id="validationFirstName" placeholder="Prénom" value="">
            <div class="valid-tooltip">
              Parfait!
            </div>
          </div>
          <hr>
          <div class="mb-3">
            <label for="validationLastName">Nom</label>
            <!-- TODO: Add required --> 
            <input type="text" class="form-control" id="validationLastName" placeholder="Nom" value="">
            <div class="valid-tooltip">
              Parfait!
            </div>
          </div>
          <hr>
          <div class="mb-3">
            <label for="EtsList">Administration</label>
            <div class="mb-3">
              <asp:DropDownList id="EtsList" CssClass="col-md-12 form-control" runat="server" ClientIDMode="Static" />
              <asp:hiddenfield runat="server" ID="EtsValue" ClientIDMode="Static" />
            </div>
            <hr>
            <label for="DirList">Direction</label>
            <div class="mb-3">
              <asp:DropDownList id="DirList" CssClass="col-md-12 form-control" runat="server" ClientIDMode="Static" />
              <asp:hiddenfield runat="server" ID="DirValue" ClientIDMode="Static" />
            </div>
            <hr>
            <label for="ServiceList">Service</label>
            <div class="mb-3">
              <asp:DropDownList id="ServiceList" CssClass="col-md-12 form-control" runat="server" ClientIDMode="Static" />
              <asp:hiddenfield runat="server" ID="ServiceValue" ClientIDMode="Static" />
            </div>
            <hr>
            <div class="mb-3">
              <label for="coteValidation">Côte de l'archive (XXWXXXX)</label>
              <p>Exemple CFW0014</p>
              <p id="coteExists" class="bg-dim-danger p-2" hidden></p>
              <div class='form-inline p-0'>
                <input type="text" class="form-control" min="7" max="7" id="coteValidation" onkeyup="checkCote(this.value)" />
                <div id="resetInput" class="btn btn-secondary" onclick="resetInput()">Reset</div>
              </div>
            </div>
            <hr>
            <div class="col-md-8 m-auto">
              <button class="btn btn-success align-self-auto" type="submit">Demander un ajout</button>
            </div>
          </div>
          <!--
          <div class="form-row">
            
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
            </div> 
          </div> -->
        </div>     
      </div>
    </form>
</asp:Content>

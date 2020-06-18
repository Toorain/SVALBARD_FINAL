<%@ Page Title="Ajouter archive" EnableEventValidation="false" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AjouterArchive.aspx.cs" Inherits="WebApplication1.AjouterArchive" %>
<%@ Register TagPrefix="rsweb" Namespace="Microsoft.Reporting.WebForms" Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="alertRequestAdd" runat="server" Visible="false">
      <div class="alert alert-success alert-dismissible fade show text-center " role="alert">
          <h5 id="alertAdd" runat="server"></h5>
          <button type="button" class="close" data-dismiss="alert" aria-label="Close">
              <span aria-hidden="true">&times;</span>
          </button>
      </div>
    </asp:Panel>
    <form id="regForm" class="needs-validation border" novalidate>
      <div id="dropdownPdf" class="dropdownPdf" data-toggle="collapse" href="#collapseElmAdd" role="button" aria-expanded="false" aria-controls="collapseExample">
        <h3 class="m-auto text-center">v -- Afficher le PDF -- v</h3>
      </div>
      <div runat="server" ClientIDMode="Static" class="collapse" ID="collapseElmAdd">
              <rsweb:ReportViewer ID="rptViewer" runat="server" Font-Names="Verdana" Font-Size="8pt" ProcessingMode="Remote" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="1000px" Width="850px" 
                  BackColor="" ClientIDMode="AutoID" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" 
                  LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" 
                  SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" 
                  ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px"
                  ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226">
                  
                  <ServerReport></ServerReport>
              </rsweb:ReportViewer>
        </div>
      <h3>Veuillez renseigner toutes les information pour effectuer une demande d'ajout à l'archive :</h3>

      <!-- One "tab" for each step in the form: -->
      <div class="tab">
        <div class="form-row justify-content-center">
          <div class="col-md-3 mb-3">
            <hr>
            <div class="mb-3">
              <label for="validationFirstName">Prénom</label>
              <input runat="server" type="text" class="form-control" id="validationFirstName" placeholder="Prénom" value="" required />
            </div>
            <hr>
            <div class="mb-3">
              <label for="validationLastName">Nom</label>
              <input runat="server" type="text" class="form-control" id="validationLastName" placeholder="Nom" value="" required />
            </div>
            <hr>
            <div class="mb-3">
              <label for="validationNombreArticle">Nombre de boîtes à ajouter</label> 
              <input type="number" class="form-control" id="validationNombreArticle" placeholder="Nombre de boîtes" value="" required />
            </div>
          </div>
          <div class="col-md-1 mb-3"></div>
          <div class="col-md-3 mb-3">
            <hr/>
            <label for="EtsList">Etablissement</label>
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
            <div class="mb-3">
              <label for="coteValidation">Côte de l'archive (XXWXXXX)</label>
              <p>Exemple 04W0014</p>
              <p id="coteExists" class="bg-dim-danger p-2" hidden></p>
              <div class='form-inline p-0'>
                <input type="text" class="form-control" min="3" max="7" id="coteValidation" value="" onkeyup="checkCote(this.value)" autocomplete="off" required/>
                <asp:hiddenfield runat="server" ID="coteValidationServer" ClientIDMode="Static" />
                <div id="resetInput" class="btn btn-secondary" onclick="resetInput()">Reset</div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="tab">
        <table class="table">
          <thead id="articleFillThead">
            <tr>
              <th scope="col">#</th>
              <th scope="col">Contenu</th>
              <th scope="col">Date début</th>
              <th scope="col">Date fin</th>
              <th scope="col">Observations : échantillonage, circuit, documentaire...</th>
              <th scope="col">Modifier</th>
            </tr>
          </thead>
          <tbody id="articleFill"></tbody>
          <tfoot id="articleFillTfoot"></tfoot>
        </table>
        <div class="row mt-4">
          <div class="col-md-2 align-self-center text-center m-auto">
            <div class="btn btn-secondary mb-3" onclick="validateData()">Valider le formulaire</div>
            <asp:HiddenField runat="server" ID="JSONFormData" ClientIDMode="Static" />
            <asp:Button runat="server" ID="addArchiveButton" ClientIDMode="Static" CssClass="btn btn-primary" OnClick="AddArchive" Text="Demander un ajout" Enabled="False" />
          </div>
        </div>
      </div>
      

      <div style="overflow:auto;">
      <div style="float:right;">
        <button class="btn btn-success" type="button" id="prevBtn" onclick="nextPrev(-1)">Previous</button>
        <button class="btn btn-success" type="button" id="nextBtn" onclick="nextPrev(1)">Next</button>
      </div>
      </div>

      <!-- Circles which indicates the steps of the form: -->
      <div style="text-align:center;margin-top:40px;">
        <span class="step"></span>
        <span class="step"></span>
      </div>
    </form>
</asp:Content>

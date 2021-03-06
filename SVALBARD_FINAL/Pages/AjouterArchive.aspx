﻿<%@ Page Title="Ajouter archive" EnableEventValidation="false" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AjouterArchive.aspx.cs" Inherits="WebApplication1.Pages.AjouterArchive" %>
<%@ Register TagPrefix="rsweb" Namespace="Microsoft.Reporting.WebForms" Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <asp:HiddenField ID="LoggedUser" ClientIDMode="Static" runat="server" Value=""/>
  <asp:HiddenField ID="LoggedUserId" ClientIDMode="Static" runat="server" Value="" />
  <asp:Panel ID="alertRequestAdd" runat="server">
      <div id="alertRequestAddContainer" class="alert alert-dismissible fade show text-center d-none" role="alert">
          <h5 id="alertAdd" runat="server"></h5>
      </div>
    </asp:Panel>
    <form id="regForm" class="needs-validation border" novalidate>
      <div id="dropdownPdf" class="dropdownPdf d-flex align-items-center" data-toggle="collapse" href="#collapseRow" role="button" aria-expanded="false" aria-controls="collapseExample">
        <div class="m-auto text-center btn btn-outline-secondary" id="pdfShow">Afficher le bordereau / étiquette</div>
      </div>
      <div runat="server" class="row collapse" ID="collapseRow" ClientIDMode="Static">
        <div class="col-md-6">
          <div runat="server" ClientIDMode="Static" ID="collapseElmAdd">
            <rsweb:ReportViewer ID="rptViewerPAL" runat="server" Font-Names="Verdana" Font-Size="8pt" ProcessingMode="Remote" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="1000px" Width="850px" 
                BackColor="" ClientIDMode="AutoID" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" 
                LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" 
                SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" 
                ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px"
                ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226">
                
                <ServerReport></ServerReport>
            </rsweb:ReportViewer>
          </div>
        </div>
        <div class="col-md-6">
          <div runat="server" ClientIDMode="Static" ID="collapseElmEtqPal">
            <rsweb:ReportViewer ID="rptViewerEtiquettePAL" runat="server" Font-Names="Verdana" Font-Size="8pt" ProcessingMode="Remote" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="1000px" Width="850px" 
                BackColor="" ClientIDMode="AutoID" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" 
                LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" 
                SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" 
                ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px"
                ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226">
              <ServerReport></ServerReport>
            </rsweb:ReportViewer>
          </div>
        </div>
      </div>
      <p class="h3 text-center m-4">Veuillez renseigner toutes les information pour effectuer une demande d'ajout à l'archive :</p>
      <!-- TODO : REMOVE AFTER TESTS ARE COMPLETE ON FORM -->
      <!-- <div class="btn btn-warning text-center" onclick="populateForm()">POPULATE FORM</div> -->

      <!-- One "tab" for each step in the form: -->
      <div class="tab">
        <div class="form-row justify-content-center">
          <div class="col-md-6 mb-3 p-3 border border-dark rounded">
            <div class="mb-3">
              <label for="validationNombreArticle">Nombre de boîtes à ajouter</label> 
              <input type="number" class="form-control" id="validationNombreArticle" placeholder="Nombre de boîtes" value="" required />
            </div>
            <div class="mb-3">
              <hr>
              <label for="coteValidation">Côte de l'archive (XXWXXXX)</label>
              <p>Exemple 04W0014</p>
              <p id="coteExists" class="bg-dim-danger p-2" hidden></p>
              <div class='form-inline p-0'>
                <input type="text" 
                       class="form-control col-md" 
                       id="coteValidation" 
                       value="" 
                       onkeyup="checkCote(this.value)" 
                       autocomplete="off" 
                       minlength="6" 
                       maxlength="7" 
                       required/>
                <asp:hiddenfield runat="server" ID="coteValidationServer" ClientIDMode="Static" />
                <asp:hiddenfield runat="server" ID="coteGeneratePdf" ClientIDMode="Static" Value="" />
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
              <th scope="col">Elimination prévue</th>
              <th scope="col">Modifier</th>
            </tr>
          </thead>
          <%--tbody is filled with FormSlider.js => AddArticle function--%>
          <tbody id="articleFill"></tbody>
          <tfoot id="articleFillTfoot"></tfoot>
        </table>
        <div class="row mt-4">
          <div class="col-md-2 align-self-center text-center m-auto">
            <div id="validateForm" class="btn btn-success mb-3" onclick="validateData()">Valider le formulaire</div>
            <asp:HiddenField runat="server" ID="JSONFormData" ClientIDMode="Static" />
            <!-- OnClick="AddArchive" -->
            <asp:Button runat="server" ID="GeneratePdfPal" OnClick="GeneratePdfPal_Click" ClientIDMode="Static" CssClass="btn btn-outline-success d-none" Text="Générer le bordereau" />
            
          </div>
        </div>
      </div>
      

      <div style="overflow:auto;">
      <div style="float:right;">
        <button class="btn btn-secondary" type="button" id="prevBtn" onclick="nextPrev(-1)">Previous</button>
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

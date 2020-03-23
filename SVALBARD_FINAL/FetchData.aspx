<%@ Page Title="FetchData" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FetchData.aspx.cs" Inherits="WebApplication1.FetchData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="text-center" id="midget-spinner">
            <h1 class="text-center">Please stand-by</h1>
            <div class="spinner-border" role="status">
                <span class="sr-only">Loading...</span>
            </div>
        </div>
        <div class="hiddenLoad">
            <span>Effectuer une demande d'ajout à l'archive :</span>
            <a href="#" class="btn btn-danger">Ajouter</a>
        </div>
        <table id="testTable" class="table table-striped table-hover hiddenLoad">
            <thead>
                <tr>
                    <!-- WARNING : Don't comment <th>ID</th> or <th>Versement</th> will break  -->
                    <th>ID</th>
                    <th>Versement</th>
                    <th>Etablissement</th>
                    <th>Direction</th>
                    <th>Service</th>
                    <th>Dossiers</th>
                    <th>Extremes</th>
                    <th>Elimination</th>
                    <th>Communication</th>
                    <th>Cote</th>
                    <th>Localisation</th>
                    <!-- <th>CL</th>
                    <th>Chrono</th>
                    <th>Calc</th> -->
                </tr>
            </thead>
            <tfoot>
            </tfoot>
        </table>
    </div>
</asp:Content>

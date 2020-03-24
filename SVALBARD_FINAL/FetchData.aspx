﻿<%@ Page Title="FetchData" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FetchData.aspx.cs" Inherits="WebApplication1.FetchData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <asp:Panel ID="alertAlreadyRequested" runat="server" Visible="false">
            <div class="alert alert-warning alert-dismissible fade show text-center " role="alert">
                <p id="alertRequestedText" runat="server"></p>
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
        </asp:Panel>
        <asp:Panel ID="alertRequestSuccess" runat="server" Visible="false">
            <div class="alert alert-success alert-dismissible fade show text-center " role="alert">
                <p id="alertSuccessText" runat="server"></p>
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
        </asp:Panel>
        <div class="text-center" id="midget-spinner">
            <h1 class="text-center">Please stand-by</h1>
            <div class="spinner-border" role="status">
                <span class="sr-only">Loading...</span>
            </div>
        </div>
        <div class="hiddenLoad">
            <h1>Cliquer sur un élément pour en afficher les détails</h1>
        </div>
        <!-- Modal -->
        <div class="modal fade" id="modalGetArchive" tabindex="-1" role="dialog" aria-labelledby="modalGetArchive" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="modalLongTitle">Effectuer une demande de retrait de l'archive</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-5">
                                <p>Archive : <span id="archiveCote"></span></p>
                                <p>Ajoutée le : <span id="archiveVersement"></span></p>
                                <p>
                                    Par :
                                    <br />
                                    <span>Etablissement :
                                        <asp:Label ID="archiveEtablissement" runat="server" ClientIDMode="Static"></asp:Label></span>
                                    <br />
                                    <span>Direction :
                                        <asp:Label ID="archiveDirection" runat="server" ClientIDMode="Static"></asp:Label></span>
                                    <br />
                                    <span>Service :
                                        <asp:Label ID="archiveService" runat="server" ClientIDMode="Static"></asp:Label></span>
                                </p>
                                <p>Élimination : <span id="archiveElimination"></span></p>
                                <asp:HiddenField ID="archiveID" runat="server" ClientIDMode="Static" />
                            </div>
                            <div class="col-md-7 vertical-line">
                                <p>
                                    Information complémentaires :
                                    <br />
                                    <span id="archiveCommentaire"></span>
                                </p>
                                <hr />
                                <div class="needs-validation">
                                    <div class="form-row">
                                        <h5>Merci de renseigner votre établissement, la direction à laquelle vous appartenez ainsi que le service auquel vous appartenez</h5>
                                        <small>Par exemple : SIEGE / Port de cannes / Compta</small>
                                    </div>
                                    <!-- TODO : Ajouter des dropdown pour limiter le choix de l'etablissment / direction / service, afin de lisser les données de la DB --> 
                                    <div class="form-row">
                                        <div class="col-md-4 mb-3">
                                            <label for="validationEts">Etablissement</label>
                                            <asp:TextBox runat="server" ID="validationEts" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                            <!-- <input type="text" class="form-control" id="validationEts" placeholder="Etablissement" required> -->
                                            <div class="valid-feedback">
                                                Looks good!
                                            </div>
                                        </div>
                                        <div class="col-md-4 mb-3">
                                            <label for="validationDir">Direction</label>
                                            <asp:TextBox runat="server" ID="validationDir" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                            <!-- <input type="text" class="form-control" id="validationDir" placeholder="Direction" required> -->
                                            <div class="valid-feedback">
                                                Looks good!
                                            </div>
                                        </div>
                                        <div class="col-md-4 mb-3">
                                            <label for="validationService">Service</label>
                                            <asp:TextBox runat="server" ID="validationService" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                            <!-- <input type="text" class="form-control" id="validationService" placeholder="Service" required> -->
                                            <div class="valid-feedback">
                                                Looks good!
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" OnClick="LogRetirerArchive" Text="Retirer l'archive" CssClass="btn btn-warning" />
                    </div>
                </div>
            </div>
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

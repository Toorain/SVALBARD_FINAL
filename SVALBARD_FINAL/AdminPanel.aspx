<%@ Page Title="Admin Panel" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminPanel.aspx.cs" Inherits="WebApplication1.AdminPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label runat="server" ID="jsonData" ClientIDMode="Static" CssClass="d-none"><%: jsonData %></asp:Label>
    <% if (pageRender) {%>
    <!-- #region Alert -->
        <asp:Panel ID="alertRequestSuccess" runat="server" Visible="false">
            <div class="alert alert-success alert-dismissible fade show text-center " role="alert">
                <h5 id="alertSuccessText" runat="server"></h5>
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
        </asp:Panel>
        <!-- #endregion Alert -->
        <div class="modal fade" id="modalGetAdmin" tabindex="-1" role="dialog" aria-labelledby="modalGetAdmin" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="modalLongTitle">Modifier l'utilisateur <span class="userNameAdmin"></span></h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-lg-5">
                                <asp:HiddenField ID="userIdAdmin" runat="server" ClientIDMode="Static" />
                                <p>Changer le status de l'utilisateur :</p>
                                <p>Ancien status : <asp:Label runat="server" ID="UserRoleId" ClientIDMode="Static"></asp:Label></p>
                                <asp:DropDownList id="RoleList" runat="server">
                                    <asp:ListItem Value="3"> Consultation </asp:ListItem>
                                    <asp:ListItem Value="2"> Gestion </asp:ListItem>
                                    <asp:ListItem Value="4"> Archiviste </asp:ListItem>
                                    <asp:ListItem Value="1"> Administration </asp:ListItem>
                                </asp:DropDownList>
                                <asp:Button runat="server" ID="changeUserStatus" OnClick="ChangeUserStatus" Text="Changer de status" CssClass="submitModal btn mt-4" ClientIDMode="Static"  />
                                <p class="confirmAdmin" hidden>Attention, vous allez donner les droits d'administration à cette personne.<br /> Êtes-vous certain(e) de vouloir continuer ?</p>
                                <button id="confirmAdminButton" class="btn btn-success confirmAdmin" hidden>Confirmer</button>
                            </div>
                            <div class="col-lg-3"></div>
                            <div class="col-lg-4"></div>
                        </div>
                    </div>
                    <div class="modal-footer">
                    </div>
                </div>
            </div>
        </div>
        <table id="adminTable" class="table table-striped table-hover">
            <thead>
                <tr>
                    <th class="col-title">UserName</th>
                    <th class="col-title">ID</th>
                    <th class="col-title">Email</th>
                    <th class="col-title">PhoneNumber</th>
                </tr>
                <tr>
                    <th class="search-field"></th>
                    <th class="search-field"></th>
                    <th class="search-field"></th>
                    <th class="search-field"></th>
                </tr>
            </thead>
            <tfoot>
                
            </tfoot>
        </table>
    <%} else {
            Response.Redirect("~/");
        } %>
</asp:Content>


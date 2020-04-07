<%@ Page Title="Admin Panel" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminPanel.aspx.cs" Inherits="WebApplication1.AdminPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label runat="server" ID="jsonData" ClientIDMode="Static" CssClass="d-none"><%: jsonData %></asp:Label>
    <% if (pageRender) {%>
        <!--hiddenLoad -->
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
                            <div class="col-lg-4">
                                <asp:Label runat="server" ID="clickedAdmin" ClientIDMode="Static"></asp:Label>
                                <p>Changer le status de l'utilisateur :</p>
                                <asp:DropDownList id="RoleList" runat="server">
                                    <asp:ListItem Value="3"> Consultation </asp:ListItem>
                                    <asp:ListItem Value="2"> Gestion </asp:ListItem>
                                    <asp:ListItem Value="1"> Administration </asp:ListItem>
                                </asp:DropDownList>
                                <asp:Button runat="server" OnClick="ChangeUserStatus" Text="Changer de status" CssClass="submitModal btn btn-primary mt-4" ClientIDMode="Static"  />
                            </div>
                            <div class="col-lg-4"></div>
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
                    <th>UserName</th>
                    <th>ID</th>
                    <th>Email</th>
                    <th>PhoneNumber</th>
                </tr>
            </thead>
            <tfoot>
            </tfoot>
        </table>
    <%} else {
            Response.Redirect("/Default.aspx");
        } %>
</asp:Content>


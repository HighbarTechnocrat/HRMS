<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="loginad.aspx.cs" Inherits="loginad" %>--%>

<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="commpagesdiv2">
    <div class="commonpages3">
        <div class="loginloginregisterheading">
            Login</div>
        <div class="loginregisterboxdiv">
        <div class="loginregisterbox">
            <div class="loginbox">
                <div class="loginformbox">
      <asp:TextBox ID="txtDomain" CssClass="logininput" Runat="Server" placeholder="Domain"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvdomain" runat="server" ErrorMessage="Domain is mandatory" ControlToValidate="txtDomain" Display="Static" ForeColor="Red" Font-Size="12px" ValidationGroup="submit"></asp:RequiredFieldValidator>
                </div>
                <div class="loginformbox">
      <asp:TextBox ID="txtUsername" Runat="Server" CssClass="logininput" placeholder="Username"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvusername" runat="server" ErrorMessage="Username is mandatory" ControlToValidate="txtUsername" Display="Static" ForeColor="Red" Font-Size="12px" ValidationGroup="submit"></asp:RequiredFieldValidator>
                    </div>
                <div class="loginformbox">
      <asp:TextBox ID="txtPassword" Runat="Server" CssClass="logininput" TextMode="Password" placeholder="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvpass" runat="server" ErrorMessage="Password is mandatory" ControlToValidate="txtPassword" Display="Static" ForeColor="Red" Font-Size="12px" ValidationGroup="submit"></asp:RequiredFieldValidator>
                </div>

                <div class="loginbtndiv">
                    <asp:CheckBox ID="chkPersist" Runat="Server" Text="Persist Cookie" Visible="false"/>
      <asp:Button ID="btnLogin" Runat="Server" CssClass="loginbtn" Text="Login" OnClick="Login_Click" ValidationGroup="submit"></asp:Button>
                    </div>
                      <div class="">
                          <asp:Label ID="errorLabel" Runat="Server" Font-Size="12px" ForeColor="#ff3300"></asp:Label>
                    </div>
                </div>
            </div>
            </div>
        </div>
        </div>
    </asp:Content>


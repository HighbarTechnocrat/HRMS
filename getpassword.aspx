<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="getpassword.aspx.cs" Inherits="getpassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="sc_form" style="min-height: 100%;">
        <div class="sc_surveyform">
            <div class="sc_formdiv">
          
                <div style="padding-top:150px;">
    <ul class="tblbasic tbllogin" id="pnllogin" runat="server" >
        
     <li class="lilogin">Enter Email Id :<br /> <asp:TextBox ID="txtuname" runat="server"></asp:TextBox> </li>
    <li class="lilogin" > <asp:Label ID="Label1" runat="server" Text=""></asp:Label></li>
    <li class="lilogin">
    <asp:Button ID="Button1" runat="server" Text="Get Password" OnClick="Button1_Click" />
        </li>
        </ul></div>      
                </div>
        </div></div>
</asp:Content>


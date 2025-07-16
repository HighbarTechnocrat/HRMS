<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="SessionEnd.aspx.cs" Inherits="SessionEnd" EnableViewState="true" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <div class="divSessionExpire">
            <a href="http://localhost/hrms/login.aspx" class="sessionExpire"> <u>Your session has expired. Please click here to log in again </u> </a>
     </div>
</asp:Content>


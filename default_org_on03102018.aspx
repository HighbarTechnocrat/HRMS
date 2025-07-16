<%@ Page Title="" Language="C#" MasterPageFile="~/home.master" AutoEventWireup="true" CodeFile="default_org_on03102018.aspx.cs" Inherits="_default" EnableViewState="true" %>
<%@ Register Src="~/themes/creative1.0/LayoutControls/homecontent.ascx" TagName="homecontent" TagPrefix="uc" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
<style>
#content-container .box {
    /*Comment */
    background: rgb(223, 222, 200) none repeat scroll 0 0 !important;
    border-color: rgb(223, 222, 200) !important; 
    /* background: rgb(255, 255, 255) none repeat scroll 0 0 !important; */
    /* box-shadow: 0 1px 10px rgba(0, 0, 0, 0.03); */
    max-width: 100%;
    border-color: #fff !important;
    
}
</style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
        <asp:Literal ID="ltrpopup" runat="server" ></asp:Literal>
           <uc:homecontent ID="uxhomecontent" runat="server" />
</asp:Content>


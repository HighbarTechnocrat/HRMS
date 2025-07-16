<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="Default4.aspx.cs" Inherits="Default4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="../Chosen/chosen.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <script src="../js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
    <script src="../js/HtmlControl/jquery-1.3.2.js"></script>
    <script src="../js/HtmlControl/jHtmlArea-0.8.min.js"></script>
    <link href="../js/HtmlControl/jHtmlArea.css" rel="stylesheet" />
    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="New Recruitment Requisition"></asp:Label>
                    </span>
                </div>
                
                 <div class="edit-contact">

                    <ul id="editform" runat="server" style="padding-bottom:300px">
    <li></li>
<li>
          <select  class="chosen-select" tabindex="2" style="width:30%">
            <option value=""></option>
            <option value="United States">United States</option>
            <option value="United Kingdom">United Kingdom</option>
            <option value="Afghanistan">Afghanistan</option>
            <option value="Aland Islands">Aland Islands</option>
            <option value="Zimbabwe">Zimbabwe</option>
          </select>
                         </li>
                         <li></li>
                        </ul>
 </div> </div> </div>     
    <script src="../Chosen/jquery-3.2.1.min.js"></script>
    <script src="../Chosen/chosen.jquery.js"></script>
    <script src="../Chosen/init.js"></script>
    <script src="../Chosen/prism.js"></script>
</asp:Content>


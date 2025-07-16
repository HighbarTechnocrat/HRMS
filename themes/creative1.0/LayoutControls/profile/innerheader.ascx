<%@ Control Language="C#" AutoEventWireup="true" CodeFile="innerheader.ascx.cs" Inherits="themes_creative1" %>
<%--PRAJYOT COMMENTED BELOW LINE TO HIDE COVER PHOTO ON innerheader.aspx 04nov2017--%>
<%--<div id="item-header" role="complementary">--%>
   <%--PRAJYOT COMMENTED ABOVE LINE TO HIDE COVER PHOTO ON innerheader.aspx 04nov2017--%>
    <div id="item-header-avatar">
        <a id="lnkhead" runat="server">
            <img id="imghead" runat="server" class="avatar user-3-avatar avatar-150 photo" width="150" height="150"/>
        </a>
    </div>
    <div id="item-header-content">
<%--        JAYESH COMMENTED BELOW LINE TO HIDE USER NAME ON editprofile.aspx 14oct20178--%>
<%--        <h2 class="user-nicename">
           <a id="lnkhead2" runat="server"> <asp:Label ID="lblheadname" runat="server"></asp:Label></a></h2>--%>
        <%--JAYESH COMMENTED ABOVE LINE TO HIDE USER NAME ON editprofile.aspx 14oct20178--%>
        <span class="activity"></span>
        <div id="item-meta">
            <div id="latest-update">
            </div>
            <div id="item-buttons">
            </div>
        </div>
    </div>
    <%--        JAYESH COMMENTED BELOW LINE TO HIDE COVER IMAGE ON editprofile.aspx 14oct20178--%>
 <%--   <div class="user-cover-layer">
        <img id="imgcover" runat="server" visible="true" />
    </div>--%>
    <%--        JAYESH COMMENTED ABOVE LINE TO HIDE COVER IMAGE  ON editprofile.aspx 14oct20178--%>
    <button id="woffice_cover_delete" class="btn-cover-upload" style="display: none;"><i class="fa fa-times"></i></button>
<%--</div>--%>

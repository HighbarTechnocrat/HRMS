<%@ Control Language="C#" AutoEventWireup="true" CodeFile="zommer.ascx.cs" Inherits="themes_creative1_LayoutControls_profile_zommer" %>
<%-- SAGAR COMMENTED THIS FOR REMOVIMG PROFILE PHOTO FROM AND COVER PHOTO FROM VIEWPROFILE 14OCT2017 STARTS HERE--%>
<%--<div id="coverphoto" runat="server" class="coverphotoimage backgroundimage" data-img-width="1750" data-img-height="1064">--%><%--<div class="expand-coverphoto"><i class="fa fa-expand"></i></div>--%><%--<div class="compress-coverphoto"><i class="fa fa-compress"></i></div>--%>

<%--<div class="profile-photo-div">
  <div class="profile-photo"> <a id="lnkuserimage" runat="server"><img id="imgprofile" runat="server" width="110" height="110" /></a> </div>
  <div class="profile-name"> <a id="lnkusername" runat="server" style="color:#fff;text-decoration:none;">
    <asp:Label ID="lblusername" runat="server" Text="" Font-Size="20px"></asp:Label>
    </a> </div>
</div>--%>

 <%--</div>--%>
<%-- SAGAR COMMENTED THIS FOR REMOVIMG PROFILE PHOTO FROM AND COVER PHOTO FROM VIEWPROFILE 14OCT2017 ENDS HERE--%>
<div class="profilediv" style="padding-bottom:0;">
  <div class="profile-tabs-desktop">
    <div class="profiletabs">
      <ul>
          <li id="profile" runat="server">
          <asp:HyperLink ID="lnkprofile" runat="server">View Profile</asp:HyperLink>
        </li>
        <li id="review" runat="server">
          <asp:HyperLink ID="lnkreview" runat="server">Ratings & Reviews</asp:HyperLink>
        </li>
        <li id="following" runat="server">
          <asp:HyperLink ID="lnkfollowing" runat="server">Following</asp:HyperLink>
        </li>
        <li id="follower" runat="server">
          <asp:HyperLink ID="lnkfollowers" runat="server">Followers</asp:HyperLink>
        </li>
        <li id="favorite" runat="server">
          <asp:HyperLink ID="lnkfavorite" runat="server">Favorites</asp:HyperLink>
        </li>
        <li id="Li1" runat="server">
          <asp:HyperLink ID="lnkgroup" runat="server">Groups</asp:HyperLink>
        </li>
      </ul>
    </div>
  </div>
</div>



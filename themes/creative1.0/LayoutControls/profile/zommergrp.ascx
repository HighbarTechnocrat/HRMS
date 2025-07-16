<%@ Control Language="C#" AutoEventWireup="true" CodeFile="zommergrp.ascx.cs" Inherits="themes_creative1_LayoutControls_profile_zommer" %>



<div id="coverphoto" runat="server" class="coverphotoimage backgroundimage" data-img-width="1750" data-img-height="1064"><div class="expand-coverphoto"><i class="fa fa-expand"></i></div><div class="compress-coverphoto"><i class="fa fa-compress"></i></div>

<div class="profile-photo-div">
  <div class="profile-photo"> <a id="lnkuserimage" runat="server"><img id="imgprofile" runat="server" width="110" height="110" /></a> </div>
  <div class="profile-name"> <a id="lnkusername" runat="server" style="color:#fff;text-decoration:none;">
    <asp:Label ID="lblusername" runat="server" Text="" Font-Size="20px"></asp:Label>
    </a> </div>
</div>

 </div>

<div class="profilediv" style="padding-bottom:0;">
  <div class="profile-tabs-desktop">
    <div class="profiletabs">
      <ul>
          <li id="Post" runat="server">
          <asp:HyperLink ID="lnkpost" runat="server">Post</asp:HyperLink>
        </li>
        <li id="Members" runat="server">
          <asp:HyperLink ID="lnkmem" runat="server">Members</asp:HyperLink>
        </li>
        
        
      </ul>
    </div>
  </div>
</div>



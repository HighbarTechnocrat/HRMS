<%@ Control Language="C#" AutoEventWireup="true" CodeFile="messageboard.ascx.cs" Inherits="themes_creative1_LayoutControls_profile_viewprofile" %>
 <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
<link rel="stylesheet" href="<%=ReturnUrl("css") %>messageboard/messagewall.css" type="text/css" media="all" />
<link rel="stylesheet" href="<%=ReturnUrl("css") %>commonpages/commonpages.css" type="text/css" media="all" />
    <style>
        .myaccountpagesheading {
  text-align: center !important;text-transform:uppercase !important;
}.aspNetDisabled {
  background: #faf7f7 none repeat scroll 0 0;
}.editprofile {
  margin: 0 !important;
  width: auto !important;
  float:none !important;
}
    </style>
  <div class="mainpostwallcat">
  <div class="commonpages">
      <div class="myaccountpagesheading">Message Board</div>
      <div class="userposts">
             </div>
               <div class="contact-container" >
                   <ul id="msgboard" runat="server">
                       <li class="msglable"><span class="msgheading">WRITE ON WALL</span></li>
                       <li class="msglable"><span>Tilte </span>&nbsp;
                           <span> 
                               <asp:TextBox ID="txttitle" runat="server" MaxLength="50" CssClass="msgtitle"> </asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txttitle" SetFocusOnError="True" ErrorMessage="Please enter first name" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
                           </span>
                       </li>
                       <li class="msglable"> <span>Message </span>&nbsp;
                           <span>
                                <asp:TextBox ID="txtmsg" runat="server" MaxLength="100" TextMode="MultiLine" ValidationGroup="validate" CssClass="msgdesc" ></asp:TextBox><br />
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="formerror" ControlToValidate="txtmsg" SetFocusOnError="True" ErrorMessage="Please enter Message" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
                           </span>
                       </li>
                     
                      <li class="proviewbtn">
                             <div class="submitbtndiv">
                                            <asp:LinkButton ID="btnedit" runat="server" Text="Edit Profile" ToolTip="Update" ValidationGroup="validate" CssClass="submitbtn" OnClick="btnedit_Click"></asp:LinkButton>
                                        </div>
                                        <div class="submitbtndiv msgbtn">
                                            <asp:LinkButton ID="btnhome" runat="server" Text="Cancel" ToolTip="Back To Home" CssClass="submitbtn" OnClick="btnhome_Click" ></asp:LinkButton>
                                        </div>
                        </li>
                  </ul>
                   </div>
      </div>
      </div>
<div class="profilemsg" id="divmsg" runat="server" visible="false">
</div>

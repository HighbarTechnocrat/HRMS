<%@ Control Language="C#" AutoEventWireup="true" CodeFile="viewprofile.ascx.cs" Inherits="themes_creative1_LayoutControls_profile_followers" %>
<%@ Register Src="~/themes/creative1.0/LayoutControls/profile/innerheader.ascx" TagName="innerheader" TagPrefix="uc" %>
 <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />

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
<uc:innerheader ID="uxinnerheader" runat="server" />
  <div class="mainpostwallcat">
  <div class="comments-summery2">
      <div class="userposts">
           <span> <asp:Label ID="lblheading" runat="server" Text="Profile"></asp:Label>
                        </span>
             </div>
               <div class="contact-container" >
                   <ul id="editform" runat="server">
				  <!-- <li class="viewlable"><img src="<%=ReturnUrl("sitepath") %>images/icons/user-shape.png" style="width:20px;"><span class="profileheading">PERSONAL DETAILS</span></li>-->
                       <li class="viewlable"><span class="personal">Full Name  </span> &nbsp;<span class="secdivnew"><i class="fa fa-user" aria-hidden="true"></i> <asp:Label ID="lblname" runat="server" CssClass="profiledetails"></asp:Label>
                       </span></li>
                      <%--<li class="viewlable"> <span class="personal">Date Of Birth</span> &nbsp;<span><i class="fa fa-birthday-cake" aria-hidden="true"></i> <asp:Label ID="lbldob" runat="server" CssClass="profiledetails" ></asp:Label></span> </li>--%>
                       <%-- wasim comment below code--%>
                      <%--<li class="viewlable"><span class="personal">Permanent Address</span> &nbsp;<span><i class="fa fa-map-marker" aria-hidden="true"></i> <asp:Label ID="lbladdress" runat="server" CssClass="profiledetails" ></asp:Label> </span>
                      </li>
                       <li class="viewlable"><span class="personal">Temporary Address</span> &nbsp;<span class="temryaddress"><i class="fa fa-map-marker" aria-hidden="true"></i> <asp:Label ID="lbltempaddress" runat="server" CssClass="profiledetails" ></asp:Label> </span>
                      </li>--%>
                        <%--wasim comment above code 23 oct 17--%>
                       <li class="viewlable"><span class="personal">Location</span> &nbsp;<span class="secdivnew"><i class="fa fa-map-marker" aria-hidden="true"></i> <asp:Label ID="lblloc" runat="server" CssClass="profiledetails" ></asp:Label> </span>
                      </li>
                       <li class="viewlable"><span class="personal">Department</span> &nbsp;<span class="secdivnew"><i class="fa fa-info-circle circlenew" aria-hidden="true"></i> <asp:Label ID="lbldept" runat="server" CssClass="profiledetails" ></asp:Label> </span>
                      </li>
                        <%--<li class="viewlable"><span class="personal">Sub-Department</span> &nbsp;<span><i class="fa fa-info-circle" aria-hidden="true"></i> <asp:Label ID="lblsubdept" runat="server" CssClass="profiledetails" ></asp:Label> </span>
                      </li>--%>
                        <li class="viewlable"><span class="personal">Designation</span> &nbsp;<span class="secdivnew"><i class="fa fa-info-circle circlenew" aria-hidden="true"></i> <asp:Label ID="lbldesg" runat="server" CssClass="profiledetails" ></asp:Label> </span>
                      </li>
                     <!-- <li class="viewlable"><img src="<%=ReturnUrl("sitepath") %>images/icons/log.png"><span class="profileheading1">Profile</span></li>-->
                      <li class="viewlable"> <span class="personal">Email-ID</span> &nbsp;<span class="secdivnew"><i class="fa fa-envelope" aria-hidden="true"></i>  <asp:Label ID="lblemail" runat="server" CssClass="profiledetails" ></asp:Label> </span> </li>
                      <li class="viewlable"> <span class="personal telnoview">Office Telephone No.</span> &nbsp;<span class="secdivnew"><i class="fa fa-mobile" aria-hidden="true"></i> <asp:Label ID="lbloffmob" runat="server" CssClass="profiledetails"></asp:Label> </span> </li>
                          <li class="viewlable"> <span class="personal"><asp:Label ID="mob" runat="server" Text="Mobile No."></asp:Label></span> &nbsp;<span class="secdivnew">
                              <asp:Label ID="ls" runat="server" Text='<i class="fa fa-mobile" aria-hidden="true"></i>'></asp:Label>  <asp:Label ID="lblmobno" runat="server" CssClass="profiledetails" ></asp:Label> </span> </li>
                      <%-- wasim comment below code--%>
                     <%-- <li class="viewlable"> <span class="personal">Alternate Mobile No</span> &nbsp;<span><i class="fa fa-mobile" aria-hidden="true"></i>  <asp:Label ID="lblaltmob" runat="server" CssClass="profiledetails"></asp:Label></span> </li>
                      <li class="viewlable"> <span class="personal">Telephone No</span><span> &nbsp;<i class="fa fa-phone" aria-hidden="true"></i> <asp:Label ID="lbltelno" runat="server" CssClass="profiledetails"></asp:Label>  </span> </li>
                      <li class="viewlable"> <span class="personal">Office Telephone No</span> &nbsp;<span><i class="fa fa-phone" aria-hidden="true"></i> <asp:Label ID="lblofftelno" runat="server" CssClass="profiledetails" ></asp:Label></span> </li>
                      <li class="viewlable"> <span class="personal">Office Extension No</span> &nbsp;<span><i class="fa fa-phone" aria-hidden="true"></i>  <asp:Label ID="lblextno" runat="server" CssClass="profiledetails" ></asp:Label></span> </li>--%>
                       <%--wasim comment above code 23 oct 17--%>                      
                       <%--<li class="viewlable"> <span class="personal">Office Fax No</span> &nbsp;<span><i class="fa fa-fax" aria-hidden="true"></i> <asp:Label ID="lblfaxno" runat="server" CssClass="profiledetails" ></asp:Label></span> </li>
                        <li class="viewlable"> <span class="personal">Alternate Email-ID</span> &nbsp;<span> <i class="fa fa-envelope" aria-hidden="true"></i>  <asp:Label ID="lblaltemail" runat="server" CssClass="profiledetails" ></asp:Label></span> </li>--%>                
                      <li class="proviewbtn" id="viewbtn" runat="server">
                             <div class="cancelbtndiv">
                                  
                                         <asp:LinkButton ID="btnedit" runat="server" Text="Edit Profile" ToolTip="Update" ValidationGroup="validate" CssClass="message-box-searchbtn1" OnClick="btnedit_Click"><i class="fa fa-pencil" aria-hidden="true"></i>Edit Profile</asp:LinkButton>
                                        </div> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                                      
                      </li>
                  </ul>
                   </div>
      </div>
      </div>
<div class="profilemsg" id="divmsg" runat="server" visible="false">
</div>

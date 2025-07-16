<%@ Control Language="C#" AutoEventWireup="true" CodeFile="enquiry.ascx.cs" Inherits="control_enquiry" %>


  <ul id="enqform" class="tblcompose" runat="server">
           <li>
                        <label><span class="star">*&nbsp;</span>Email ID</label>
                        <asp:TextBox ID="txtusername" runat="server" CssClass="txtcomposetitle" MaxLength="250"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Email ID is required" ValidationGroup="valgrp" ControlToValidate="txtusername" Display="Dynamic" CssClass="errorfield" SetFocusOnError="true" EnableViewState="true"></asp:RequiredFieldValidator>
               <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Special characters are not allowed" CssClass="formerror" ControlToValidate="txtusername" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^[a-zA-Z0-9 @.]+$" ValidationGroup="valgrp"></asp:RegularExpressionValidator>

                    </li>
      <li id ="liprod" runat="server">
                        <label>Product Name</label>
          <asp:TextBox ID="txtproductname" runat="server" CssClass="txtcomposetitle" MaxLength="250"></asp:TextBox>
          <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Special characters are not allowed" CssClass="formerror" ControlToValidate="txtproductname" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^[a-zA-Z0-9 ]+$" ValidationGroup="valgrp"></asp:RegularExpressionValidator>

          </li>
      <li>
           <li>
                        <label>Contact No</label>
          <asp:TextBox ID="txtcontact" runat="server" CssClass="txtcomposetitle" MaxLength="12"></asp:TextBox><br />
               <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Only numeric values are allowed" CssClass="formerror" ControlToValidate="txtcontact" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^[0-9 ]+$" ValidationGroup="valgrp"></asp:RegularExpressionValidator>

          </li>
      <li>
                        <label><span class="star">*&nbsp;</span>Subject</label>
          <asp:TextBox ID="txtsubject" runat="server" CssClass="txtcomposetitle" MaxLength="250"></asp:TextBox>
          <br />
           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Subject is required" ValidationGroup="valgrp" ControlToValidate="txtsubject" Display="Dynamic" CssClass="errorfield" SetFocusOnError="true" EnableViewState="true"></asp:RequiredFieldValidator>
           <asp:RegularExpressionValidator ID="regsearch" runat="server" ErrorMessage="Special characters are not allowed" CssClass="formerror" ControlToValidate="txtsubject" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^[a-zA-Z0-9 ]+$" ValidationGroup="valgrp"></asp:RegularExpressionValidator>
          </li>
      <li>
           <label><span class="star">*&nbsp;</span>Comment</label>
          <asp:TextBox ID="txtcomment" runat="server" CssClass="txtcomposetitle" Height="200px" TextMode="MultiLine" MaxLength="550"></asp:TextBox><br />
           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Comment is required" ValidationGroup="valgrp" ControlToValidate="txtcomment" Display="Dynamic" CssClass="errorfield" SetFocusOnError="true" EnableViewState="true"></asp:RequiredFieldValidator>
           <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Special characters are not allowed" CssClass="formerror" ControlToValidate="txtcomment" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^[a-zA-Z0-9 ]+$" ValidationGroup="valgrp"></asp:RegularExpressionValidator>
          </li>
      <li class="submit">
                        <asp:LinkButton ID="btnsend" runat="server" ValidationGroup="valgrp" CssClass="message-box-searchbtn1 align-right" OnClick="btnsend_Click"><i class="fa fa-send m-r-xs"></i>&nbsp;&nbsp;Send Enquiry</asp:LinkButton>
                    </li>
  </ul>
<asp:Label ID="lblError" runat="server" Width="219px"></asp:Label>
<div id ="senddiv" runat="server" visible="false" class="proviewbtn btndiv">
    <div class="proviewbtn btndiv" style="float:left !important;width:500px;padding:20px;">
<div class="cancelbtndiv" style="width:140px;float:left !important;">
 <asp:LinkButton ID="lnkhome" runat="server" CssClass="message-box-searchbtn align-right" OnClick="lnkhome_Click">
     Go To Home</asp:LinkButton>
</div>
<div class="cancelbtndiv"  style="width:150px;float:left !important;">
 <asp:LinkButton ID="lnkcont" runat="server" CssClass="message-box-searchbtn align-right" OnClick="lnkcont_Click">
     Continue</asp:LinkButton>
</div>
        </div>
</div>
<%--<%@ Control Language="C#" AutoEventWireup="true" CodeFile="logins.ascx.cs" Inherits="logins" %>--%>



<%@ Control Language="C#" AutoEventWireup="true" CodeFile="login.ascx.cs" Inherits="Themes_SecondTheme_LayoutControls_login" %>


<asp:Panel ID="pnlloginew" runat="server" DefaultButton="LoginButtonew">
<div class="loginboxnew">

    <div>
      <p class="login-box-msg"><i class="fa fa-user" aria-hidden="true"></i>SIGN IN</p>
        </div>
  <div class="login-usernamenew">
    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" class="loginlabel"></asp:Label>
    <asp:TextBox ID="UserName" placeholder="Email" runat="server" class="logininputnew" TabIndex="1"
 onkeydown="javascript:if((event.which&amp;&amp;event.which==13)||(event.keyCode&amp;&amp;event.keyCode==13)){document.forms[0].elements['MainContent_log_in_m_uxlogin_LoginButton'].click();return false;}else return true;"
></asp:TextBox> <i class="fa fa-envelope-o" aria-hidden="true"></i>
    <asp:Label ID="lbluser" Style="margin-left:0px" runat="server" CssClass="loginerror" Visible="false"></asp:Label>
    <asp:RequiredFieldValidator Style="margin-left:0px" ID="UserNameRequired" runat="server" ControlToValidate="UserName" ToolTip="Email id is required." ErrorMessage="Email id is required." ValidationGroup="Login1" CssClass="loginerror"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="EmailIdRequired" runat="server" ValidationGroup="Login1" ControlToValidate="UserName" ToolTip="Email id is not vaild." ErrorMessage="Email id is not vaild." ValidationExpression="^[a-zA-Z0-9_\.\-]+@[a-zA-Z0-9\-]+\.[a-zA-Z0-9\-\.]+$" CssClass="loginerror"></asp:RegularExpressionValidator>
  </div>
  <div class="login-password">
    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" class="loginlabel"></asp:Label>
    <asp:TextBox ID="Password" placeholder="Password" runat="server" TextMode="Password" class="logininputnew" TabIndex="2"
 onkeydown="javascript:if((event.which&amp;&amp;event.which==13)||(event.keyCode&amp;&amp;event.keyCode==13)){document.forms[0].elements['MainContent_log_in_m_uxlogin_LoginButton'].click();return false;}else return true;"
></asp:TextBox>   <i class="fa fa-lock" aria-hidden="true"></i>
    <asp:Label ID="lblpwd" runat="server" CssClass="loginerror" Visible="false" Style="margin-left: 90px"></asp:Label>
    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ValidationGroup="Login1" ControlToValidate="Password" ToolTip="Password is required." ErrorMessage="Password is required." CssClass="loginerror"></asp:RequiredFieldValidator>
  </div>

  <div class="loginremember" style="display:none;">
    <asp:CheckBox ID="RememberMe" runat="server" />
    <span>Remember me on this computer</span> </div>
  <div class="loginremember" style="display:none"> <span class="toggler"><a href="javascript:void(0)" >Forgot your password?</a></span> <span class="toggler" style="display:none"><a href="javascript:void(0)" >Forgot your password?</a></span> </div>
  <div class="forgotpassemail" style="display:none" id="divf" runat="server">
    <asp:Label ID="lblfusername" runat="server" Text="Enter your email id already registered" Visible="false"></asp:Label>
    <asp:TextBox ID="txt_forgetpwd" runat="server" class="forgotpassemailinput" placeholder="Enter Valid Email Id" ></asp:TextBox>
    <asp:Button ID="submitButton" runat="server" CommandName="submit" Text="Submit" ToolTip="Submit" CssClass="forgotpassemailbtn" OnClick="btnsubmit_onclick" ValidationGroup="valforgetpwdgrp" />
    <br />
      <asp:RequiredFieldValidator ID="valforgetpwd" runat="server" ControlToValidate="txt_forgetpwd" SetFocusOnError="true" Display="Dynamic" CssClass="forgotpasserror" ErrorMessage="Please enter email id."  ValidationGroup="valforgetpwdgrp"></asp:RequiredFieldValidator>
   <asp:RegularExpressionValidator ID="regularforEmailId" runat="server" ControlToValidate="txt_forgetpwd" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Please enter valid email id" ValidationGroup="valforgetpwdgrp" CssClass="forgotpasserror" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

   
  </div>

  <div class="failuretext">
    <asp:Label ID="lblformsg" runat="server"></asp:Label>
  </div>
  <div class="loginbtndivnew">
    <asp:Button ID="LoginButtonew" runat="server" OnClick="Login1_Authenticate" class="loginbtnew" ValidationGroup="Login1" Text="Log In" ToolTip="Log In" TabIndex="3"></asp:Button>
  </div>
  <div class="failuretext">
    <asp:Label runat="server"  ID="lblSuccess" Visible="false" Font-Bold="false" />
    <asp:Label ID="lblerror" runat="server"></asp:Label>
    <asp:Label ID="FailureText" runat="server"></asp:Label>
  </div>
  <div class="sociallogin" id="divsin" runat="server" visible="false">
    <div class="sociallogintitle">Or</div>
    <asp:HyperLink onclick="dologin();" class="login-box-fbbtn" runat="server" href="#" id="fblink"> <img src='ReturnUrl("sitepath")images/fb-login.png' alt='image1' width="210" height="45" title="Facebook Login" /></asp:HyperLink>
    <asp:LinkButton  OnClick="Login" runat="server" 
            ID="btnLogin"> <img src='ReturnUrl("sitepath")images/gplus-login.png' alt='image1' width="210" height="45" title="Google Plus Login" /> </asp:LinkButton>
  </div>
</div>
    </asp:Panel>

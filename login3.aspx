<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login3.aspx.cs" Inherits="login3" %>

<%@ Register Src="~/themes/creative1.0/LayoutControls/login.ascx" TagName="login" TagPrefix="uc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HRMS</title>
    <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=0;" />
    <link href="<%=ReturnUrl("sitepathmain") %>font/css/font-awesome.min.css" rel="stylesheet" />
    <asp:PlaceHolder runat="server" ID="metakeyword" />
    <asp:PlaceHolder runat="server" ID="metadescription" />
    <asp:PlaceHolder runat="server" ID="id1"></asp:PlaceHolder>
    <link rel="SHORTCUT ICON" href="~/images/Highbar.jpeg" type="image/x-icon" />
    <link rel="stylesheet" href="~/CSS/creative1.0/highbar/hrms.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="~/CSS/creative1.0/highbar/login.css" type="text/css" media="screen" />
    <style>
        html, body, form {
            height: 100% !important;
            overflow: hidden;
        }

        #content-container {
            margin: 0;
            display: flex !important;
        }
    </style>
</head>
<body class="login-full-page">
                <form id="user_login" runat="server" accept-charset="UTF-8" method="POST">
    <div class="login-page">
        <div class="login-box">
            <h4 style="color: #18578c;" class="text-center">HRMS</h4>
            <div class="login-logo">
<%--                <img src="http://localhost/hrms/images/homepage_imgs/Technocrat_Logo.png" class="yellow-img">--%>
                <a href="#">
                    <img src="http://localhost/hrms/images/homepage_imgs/Technocrat_Logo.png" class="hbt-logo"></a>

            </div>
            <!-- /.login-logo -->
            <div class="login-box-body">
                <div style="display: none;" class="overlay" id="spin">
                    <i class="fa fa-spin fa-lg fa-spinner"></i>
                </div>
                <p class="login-box-msg"><i class="fa fa-user" aria-hidden="true"></i>SIGN IN</p>

                    <input type="hidden" value="4MsabJXlp4eby1JuHlN8g8aHuBu44GPhIMsrqkhz" name="_token">
                    <div class="form-group has-feedback">
                        <input runat="server" type="email" placeholder="Email" value="" name="email" id="email" style="color: #fff" class="form-control">
                        <%--<asp:TextBox runat="server" id="email" style="color: #fff" class="form-control"></asp:TextBox>--%>
                        <i class="fa fa-envelope-o" aria-hidden="true"></i>
                        <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
                        <span class="help-inline text-danger" id="email_error"></span>
                    </div>
                    <div class="form-group has-feedback">
                        <%--<asp:TextBox runat="server" id="password" style="color: #fff" class="form-control"></asp:TextBox>--%>
                        <input runat="server" id="password" type="password" value="" placeholder="Password" name="password" style="color: #fff" class="form-control">
                        <i class="fa fa-lock" aria-hidden="true"></i>
                        <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                        <span class="help-inline text-danger" id="password_error"></span>
                    </div>
                    <div class="form-group has-feedback">
                        <button runat="server" id="submit" onsubmit="SubmitButton_Click" visible="true"></button>   
                        </div>
                    <div class="checkbox icheck">
                        <div class="checkbox">
                            <label class="chackboxtext">
                                <input type="checkbox" class="rememberchackbox">
                                Remember me</label>
                        </div>
                    </div>

                    <%--<a href="#" class="btn btn-primary" id="login_submit" runat="server" onclick="SubmitButton_Click">Sign In</a>--%>
                                
                    <a href="#" class="fpswrd">I forgot my password</a><br>
            </div>

        </div>
    </div>
    </form>                    
</body>
</html>

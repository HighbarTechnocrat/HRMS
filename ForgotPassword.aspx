<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>oneHR</title>
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
<body>
    <form id="form1" runat="server">
    <div class="login-page">
        <div class="login-box">
            <%--<h4 style="color: #18578c;" class="text-center">oneHR</h4>--%>
            <div class="login-logo">
<%--                <img src="http://localhost/hrms/images/homepage_imgs/Technocrat_Logo.png" class="yellow-img">--%>
                <a href="#">
<%--                    <img src="http://localhost/hrms/images/homepage_imgs/Technocrat_Logo.png" class="hbt-logo"></a>--%>

            </div>
            <!-- /.login-logo -->
            <div class="login-box-body">
                <div style="display: none;" class="overlay" id="spin">
                    <i class="fa fa-spin fa-lg fa-spinner"></i>
                </div>
                <p class="login-box-msg"><i class="fa fa-user" aria-hidden="true"></i>Reset Password</p>

                    <input type="hidden" value="4MsabJXlp4eby1JuHlN8g8aHuBu44GPhIMsrqkhz" name="_token">
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Yellow"></asp:Label>
                    <div class="form-group has-feedback">
                        <asp:TextBox runat="server" id="email" style="color: #fff" class="form-control" TextMode="Email" placeholder="Email" ></asp:TextBox>
                        <i class="fa fa-envelope-o" aria-hidden="true"></i>
                        <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
                        <span class="help-inline text-danger" id="email_error"></span>
                    </div>
<%--                    <div class="form-group has-feedback">
                        <asp:TextBox runat="server" id="password" style="color: #fff" placeholder="Password" TextMode="Password" class="form-control"></asp:TextBox>
                        <i class="fa fa-lock" aria-hidden="true"></i>
                        <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                        <span class="help-inline text-danger" id="password_error"></span>
                    </div>--%>
<%--                    <div class="checkbox icheck">
                        <div class="checkbox">
                            <label class="chackboxtext">
                                <input type="checkbox" class="rememberchackbox">
                                Remember me</label>
                        </div>
                    </div>--%>
                <asp:Button runat="server" id="btnsubmit" Text="Reset Password" OnClick="SubmitButton_Click" />
                    <%--<a href="#" class="btn btn-primary" id="login_submit" runat="server" onclick="SubmitButton_Click">Sign In</a>--%>
                                
                    <br>
            </div>

        </div>
    </div>
    </form>
</body>
</html>

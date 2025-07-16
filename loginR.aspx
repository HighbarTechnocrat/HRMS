<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="logins.aspx.cs" Inherits="logins" %>--%>




<%@ Page Language="C#" AutoEventWireup="true" CodeFile="loginR.aspx.cs" Inherits="loginR" %>

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
<body class="page page-id-4 page-template page-template-page-templates page-template-login page-template-page-templateslogin-php layout-1 js sidebar-hidden pace-done" style="width: auto;">
    <form id="formnew" runat="server">
        <div class="fullpage">
        <div id="page-wrapper">
            <div id="content-container">
                <section id="woffice-loginew" class="revslider-disabled">
                 
                      <%-- prajyot comment below code 26 oct 2017 For IE login page--%>

                    <%--<div id="woffice-login-left">
                    </div>--%>
                    <div id="woffice-login-rightnew">
                        <header class="loginheadernew">
                            <a href="#" id="login-logo">
                              <%--  SAGAR CHANGED LOGO 19SEPT2017--%>
                                <img src="images/yellow.png" class="yellow-img">
                               <img src="images/logo.jpg" class="hbt-logo" width="125px" height="25px" ></a> <%--JAYESH CHANGES FINAL LOGO 14oct2017--%>

                            <div class="social-login-btns one-btn">

                            </div>
                        </header>

                        <uc:login ID="uxlogin" runat="server" />
                    </div>
                </section>
            </div>
        </div>
      </div>
    </form>
</body>
</html>

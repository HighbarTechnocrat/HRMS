<%@ Page Language="C#" AutoEventWireup="true" CodeFile="chatgptlogin.aspx.cs" Inherits="chatgptlogin" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login - Highbar Technocrat</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f2f2f2;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
        }
        .login-container {
            background-color: #333;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0px 0px 15px rgba(0, 0, 0, 0.5);
            color: white;
            width: 300px;
            text-align: center;
        }
        .login-container input[type="text"],
        .login-container input[type="password"] {
            width: 100%;
            padding: 10px;
            margin: 10px 0;
            border: none;
            border-radius: 5px;
            box-sizing: border-box;
        }
        .login-container input[type="submit"] {
            background-color: #4CAF50;
            color: white;
            padding: 10px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            width: 100%;
            margin-top: 10px;
        }
        .login-container input[type="submit"]:hover {
            background-color: #45a049;
        }
        .login-container a {
            color: #66b3ff;
            text-decoration: none;
            font-size: 12px;
        }
        .login-container .note {
            color: red;
            font-size: 12px;
            margin-top: 15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server" placeholder="Email" CssClass="form-control" TextMode="SingleLine"></asp:TextBox>
            <asp:TextBox ID="txtPassword" runat="server" placeholder="Password" CssClass="form-control" TextMode="Password"></asp:TextBox>
            <asp:Button ID="btnLogin" runat="server" Text="Submit" OnClick="btnLogin_Click" CssClass="btn btn-primary" />
            <asp:HyperLink ID="hlForgotPassword" runat="server" NavigateUrl="ForgotPassword.aspx">I forgot my password</asp:HyperLink>
            <p class="note">Note: You would be Logged out of the portal on browser closure.</p>
        </div>
    </form>
</body>
</html>

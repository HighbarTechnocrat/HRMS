<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sslogin.aspx.cs" Inherits="sslogin" %>
<%@ Register Src="~/control/ssologin.ascx" TagName="log_in" TagPrefix="uclogin" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SSO Login</title>
    <style>
table {
  border-collapse: collapse;
  margin: 50px;
  width: 400px;
}
td {
  border: 1px solid #ccc;
  border-collapse: collapse;
  padding: 5px;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
<uclogin:log_in ID="log_in" runat="server" />
    </form>
</body>
</html>

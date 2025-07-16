<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="myaccount_Index" %>

<!DOCTYPE html>
<asp:linkbutton runat="server">LinkButton</asp:linkbutton>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <span><asp:LinkButton ID="LinkButton1" runat="server">Leave Request</asp:LinkButton> </span>
         <span><asp:LinkButton ID="LinkButton2" runat="server">Attendance Regularization</asp:LinkButton> </span>
        <span><asp:LinkButton ID="LinkButton3" runat="server">Manage Leave Requests</asp:LinkButton> </span>
        <span><asp:LinkButton ID="LinkButton4" runat="server">Manage Attendance regularization</asp:LinkButton> </span>
    </div>
        
         <div>
         <span><asp:LinkButton ID="LinkButton5" runat="server" Text="Inbox list of leave requests">Inbox list of leave requests</asp:LinkButton> </span>
         <span><asp:LinkButton ID="LinkButton6" runat="server" Text ="Inbox list of attendance regularization requests">Inbox list of attendance regularization requests</asp:LinkButton> </span>
        <span><asp:LinkButton ID="LinkButton7" runat="server" Text ="Leave Report (Employee wise ">Leave Report (Employee wise </asp:LinkButton> </span>
        <span><asp:LinkButton ID="LinkButton8" runat="server" Text ="Leave Parameters Master">Leave Parameters Master</asp:LinkButton> </span>
    </div>
       
    </form>
</body>
</html>

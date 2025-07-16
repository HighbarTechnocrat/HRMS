<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ssologin.ascx.cs" Inherits="themes_ssologin" %>
<table id="tbldata" runat="server" visible="false">
    <tr>
        <td>IsValid</td>
        <td><asp:Label ID="lblvalid" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td>Employee No</td>
        <td><asp:Label ID="lblid" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td>Login ID</td>
        <td><asp:Label ID="lbllogin" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td>Name</td>
        <td><asp:Label ID="lblname" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td>Email ID</td>
        <td><asp:Label ID="lblemail" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td>Mobile No</td>
        <td><asp:Label ID="lblmobile" runat="server"></asp:Label></td>
    </tr>
     <tr>
        <td>Date Of Birth</td>
        <td><asp:Label ID="lblbod" runat="server"></asp:Label></td>
    </tr>
         <tr>
        <td>Gender</td>
        <td><asp:Label ID="lblgender" runat="server"></asp:Label></td>
    </tr>
     <tr>
        <td>Department</td>
        <td><asp:Label ID="lbldept" runat="server"></asp:Label></td>
    </tr>
     <tr>
        <td>Designation</td>
        <td><asp:Label ID="lbldesg" runat="server"></asp:Label></td>
    </tr>
     <tr>
        <td>Permanent Address</td>
        <td><asp:Label ID="lbladdress" runat="server"></asp:Label></td>
    </tr>
     <tr>
        <td>Temporary Address</td>
        <td><asp:Label ID="lbltempadd" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td>ExtensionNo</td>
        <td><asp:Label ID="lblextno" runat="server"></asp:Label></td>
    </tr>
</table>
<asp:Label ID="lblerror" runat="server" Text="Invalid Login" ForeColor="Red" Font-Size="22px" Visible="false"></asp:Label>

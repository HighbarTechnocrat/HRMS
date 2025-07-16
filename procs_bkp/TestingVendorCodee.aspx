<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestingVendorCodee.aspx.cs" EnableEventValidation = "false" Inherits="procs_TestingVendorCodee" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style = "font-family:Arial; height:100px">This is a test page</div>
    <div>
       <div style="height: 700px;">  
        <table cellpadding="10" cellspacing="10" width="85%" align="center" style="background: SkyBlue;">  
            <tr>  
                <td>  
                    
                   
                    <asp:Button ID="btnExport" runat="server" Text="Download" OnClick="btnExport_Click" />  
                </td>  
            </tr>  
            <tr>  
                <td>  
                   
                </td>  
            </tr>  
        </table>  
    </div>  

      <br /><br />
    </div>
    <div>
    <%--   <asp:Button ID="btnExport" runat="server" Text="Export"  OnClick="btnExport_Click" />--%>
        <asp:Button ID="Button1" runat="server" Text="Button"   OnClick="Button1_Click"/>
    </div>
        </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>
<!DOCTYPE html>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <span>Main SkillSet</span>&nbsp;&nbsp;<span style="color:red">*</span><br />
                                    <asp:DropDownList runat="server" ID="lstMainSkillset">
                                    </asp:DropDownList>
                                   <asp:TextBox AutoComplete="off" ID="TextBox1"  runat="server"></asp:TextBox>
                                    <ajaxToolkit:DropDownExtender runat="server" ID="DDE"
                            TargetControlID="TextBox1" DropDownControlID="lstMainSkillset" />
        </div>
    </form>
</body>
</html>

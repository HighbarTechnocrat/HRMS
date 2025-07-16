<%@ Page Title="" Language="C#" MasterPageFile="~/cms.master" AutoEventWireup="true" CodeFile="errorpg2.aspx.cs" Inherits="errorpage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table align="center">
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" width="100%" align="center">

                    <tr height="15"></tr>
                    <tr>
                        <td align="center">
                            <img src="<%# ReturnUrl("sitepath")%>themes/creative1.0/images/404.png" width="180"/>
                            <br />
                            <font color="black" size="2px"><br /> Kindly 
                 <a href="javascript:history.go(-1);" class="footer-link">Click here </a> to continue OR return to last page you were browsing.
                 </font>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td height="20px"></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

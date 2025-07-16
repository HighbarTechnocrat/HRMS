<%@ Control Language="C#" AutoEventWireup="true" CodeFile="reviewcomments.ascx.cs" Inherits="themes_creative1_LayoutControls_reviewcomments" %>
<asp:HiddenField ID="hdvalue" runat="server" Value="1" ViewStateMode="Inherit" />
<div  style="width:auto;float:left;padding:10px;min-width:900px;">
        <div style="width:230px;float:left;">
            <div style="width:220px;float:left;">
            <asp:HyperLink ID="lnkusername" runat="server" target="_blank"></asp:HyperLink>
            </div>
            <div style="width:220px;float:left;">
            <asp:HyperLink ID="lnkimage" runat="server"><img id="imgprofile" runat="server" width="200" height="200" alt=''/></asp:HyperLink>
            </div>
            <div style="width:220px;float:left; height: 76px;">
            <asp:Label ID="lblreviewid" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lbluserid" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lblproductid" runat="server" Visible="false"></asp:Label>
            <div id='jqxRating'>
            </div>
            Review : <asp:Label ID="review" runat="server"></asp:Label><br />
            <asp:LinkButton ID="lnklike" runat="server" Text="" onclick="lnklike_Click"></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblcomment" runat="server" ForeColor="white"></asp:Label>
            </div>
            <div style="width:220px;float:left;">
                <asp:LinkButton ID="lnkfollow" runat="server" Text="" onclick="lnkfollow_Click"></asp:LinkButton>
            </div>
        </div>
    </div>


<asp:HiddenField ID="hdcomment" runat="server" Value="0" ViewStateMode="Inherit" />

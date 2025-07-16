<%@ Control Language="C#" AutoEventWireup="true" CodeFile="profileheader.ascx.cs" Inherits="themes_creative1_LayoutControls_profileheader" %>
<style type="text/css">
a{text-decoration:none;}
</style>
 <script type="text/javascript">
     function ChangeUrl(title, url) {
         if (typeof (history.pushState) != "undefined") {
             var obj = { Title: title, Url: url };
             history.pushState(obj, obj.Title, obj.Url);
         } else {
             alert("Browser does not support HTML5.");
         }
     }
</script>
<div  style="width:auto;float:left;padding:10px;min-width:900px;margin-left:175px;">
        <img id="imgcover" runat="server" />
</div>
<div  style="width:auto;float:left;padding:10px;min-width:900px;">
        <img id="imgprofile" runat="server" width=200 height=200 />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblusername" runat="server" Text="" Font-Size="20px"></asp:Label>
</div>
<div style="width:auto;float:left;padding:10px;min-width:900px;">
<%--<input type="button" value="Reviews & Ratings" onclick="ChangeUrl('Page1', 'reviewratings');" />
<input type="button" value="Followers" onclick="ChangeUrl('Page2', 'followers');" />
<input type="button" value="Following" onclick="ChangeUrl('Page3', 'following');" />--%>
    <asp:HyperLink ID="lnkreview" runat="server" ForeColor="White">Reviews & Ratings</asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:HyperLink ID="lnkfollowers" runat="server" ForeColor="White">Followers</asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:HyperLink ID="lnkfollowing" runat="server" ForeColor="White">Following</asp:HyperLink>
</div>

<%@ Control Language="C#" AutoEventWireup="true" CodeFile="photo.ascx.cs" Inherits="themes_creative_LayoutControls_photo" %>
<asp:Repeater ID="rptcategname" runat="server">
    <ItemTemplate>
        <asp:Panel ID="pnlcat" runat="server">
            <div class="birth-main">
                <div class="bd-title">Photo Gallary</div>
                <div class="gallary-main">
                    <div class="gallary-image">
                        <a href="#">
                            <img src="https://graph.facebook.com/797954163634488/picture?type=large" style=""></a>
                    </div>
                    <div class="gallary-image">
                        <a href="#">
                            <img src="https://graph.facebook.com/797954163634488/picture?type=large" style=""></a>
                    </div>
                    <div class="gallary-image">
                        <a href="#">
                            <img src="https://graph.facebook.com/797954163634488/picture?type=large" style=""></a>
                    </div>
                    <div class="gallary-image">
                        <a href="#">
                            <img src="https://graph.facebook.com/797954163634488/picture?type=large" style=""></a>
                    </div>
                    <div class="gallary-image">
                        <a href="#">
                            <img src="https://graph.facebook.com/797954163634488/picture?type=large" style=""></a>
                    </div>
                    <div class="gallary-image">
                        <a href="#">
                            <img src="https://graph.facebook.com/797954163634488/picture?type=large" style=""></a>
                    </div>

                </div>
            </div>
        </asp:Panel>
    </ItemTemplate>
</asp:Repeater>


<%--SAGAR COMMRNTED THIS FOR REMOVING THE WEATHER WIDGET 22SEPT2017--%>
<%--<%@ Control Language="C#" AutoEventWireup="true" CodeFile="whether.ascx.cs" Inherits="themes_creative" %>
<asp:Panel ID="pnlweather" runat="server">
<div class="widget box WiseChatWidget">
    <div class="intern-padding">
                    <a class="lnkclose" data="weather"><i class="fa fa-close"></i></a>
        <div class="intern-box box-title"><h3>Weather</h3></div>
        <div class="textwidget">
            <iframe class="sourceView" frameborder="0" noresize="noresize" src="<%=ReturnUrl("sitepathmain") %>weather.aspx" style="height: 270px; width: 94%;margin:0 3%;"></iframe>
        </div>
    </div>
</div>
</asp:Panel>--%>

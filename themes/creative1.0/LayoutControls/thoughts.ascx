
<%--SAGAR COMMENTED THOUGHT OF THE FOR REMOVING THE WIDGET 22SEPT2017--%>
<%--<%@ Control Language="C#" AutoEventWireup="true" CodeFile="thoughts.ascx.cs" Inherits="themes_creative" %>
<asp:Panel ID="thoughts" runat="server" CssClass="widget box WiseChatWidget">
    <div class="intern-padding">
            <a class="lnkclose" data="thoughts"><i class="fa fa-close"></i></a>
        <div class="intern-box box-title">
            <h3>Thought Of The Day</h3>
        </div>
        <asp:Repeater ID="rptthought" runat="server">
            <ItemTemplate>
                <div class="textwidget text-writer">
                    "<%#(DataBinder.Eval(Container, "DataItem.descsmall")) %>"<br /><span class="lbl-writer"><%#(DataBinder.Eval(Container, "DataItem.title")) %></span>
                    </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Panel>--%>

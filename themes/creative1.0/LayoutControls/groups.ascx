<%@ Control Language="C#" AutoEventWireup="true" CodeFile="groups.ascx.cs" Inherits="themes_creative" %>
    <asp:Panel ID="pnlgrp" runat="server" CssClass="widget box widget_fw_woffice_poll">
        <div class="intern-padding">
            <div class="widgGroupset_poll intern-box box-title"><h3>GROUPS</h3>
                <asp:LinkButton ID="lnkview" runat="server" Visible="false" OnClick="lnkview_Click">View All</asp:LinkButton>
            </div>
            <div class="text-birth mCustomScrollbar">
            <asp:Repeater ID="rptgrp" runat="server">
                <ItemTemplate>
                   <div class="textwidget">
                       <asp:Label ID="lblgid" runat="server" Text='<%#Eval("grpid")%>' Visible="false"></asp:Label>
                                <a  href='<%#getgroup( Eval("grpid"))%>'>
                                    <i class="fa fa-users fa-2x" aria-hidden="true"></i><%#(DataBinder.Eval(Container, "DataItem.grpname")) %>&nbsp;
                                    <asp:Label ID="lblcount" runat="server"></asp:Label>
                                </a>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            </div>
        </div>
    </asp:Panel>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="birthdays.ascx.cs" Inherits="themes_creative1_LayoutControls_bithdays" %>
<div class="list_carousel" id="divbd" runat="server" visible="false">
    <asp:Literal ID="ltrjs" runat="server"></asp:Literal>
    <asp:Literal ID="ltrcat" runat="server"></asp:Literal>
    <asp:Repeater ID="rptrnews" runat="server">
        <ItemTemplate>
            <div class="carouselproitem">
                <div class="view-first">
                    <div class="view view-content">
                        <div class="mask">
                            <a href='newsdetail.aspx?newsid=<%# Eval("id") %>'>
                                <h2>
                                    <%# Eval("name") %></h2>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>

</div>

<div class="birth-main">

<!-- Birth DAy -->
    <asp:Panel ID="bday" runat="server">
    <div class="bd-title">BIRTHDAYS<%--<a href="#">View All</a>--%></div>
    <asp:Repeater ID="rptbday" runat="server">
        <ItemTemplate>
            <div class="following">
                    <div class="following-image">
                        <a href='<%#getuser(Eval("indexid")) %>'>
                        <img style="height: 55px; width:55px;" src='<%=ReturnUrl("mediapath")%>profilephoto/<%#(DataBinder.Eval(Container, "DataItem.profilephoto")) %>'></a>
                    </div>
                    <div class="following-name1">
                        <a href='<%#getuser(Eval("indexid")) %>'><%#(DataBinder.Eval(Container, "DataItem.fullname")) %></a>
                        <label style="font-size: 14px; color: #4b1f09;"><i class="fa fa-birthday-cake"></i>&nbsp;&nbsp;
                            <%# Convert.ToDateTime(Eval("event_date")).ToString("M") %>
                        </label></a>
                    </div>
       
            </div>
        </ItemTemplate>
    </asp:Repeater>
    </asp:Panel>



    <!-- Groups -->
    <asp:Panel ID="pnlgrp" runat="server" Visible="false">
    <div class="bd-title">GROUPS
        <asp:LinkButton ID="lnkview" runat="server" Visible="false" OnClick="lnkview_Click">View All</asp:LinkButton>
    </div>
    <asp:Repeater ID="rptgrp" runat="server">
        <ItemTemplate>
            <div class="following">
                    <div class="following-image">
                        <a href='<%#getgroup( Eval("grpid"))%>'>
                        <img id="profimg" style="height: 55px; width:55px;" src='<%=ConfigurationManager.AppSettings["adminsitepath"]%>images/grpimages/<%#(DataBinder.Eval(Container, "DataItem.grpimg")) %>'></a>
                    </div>
                    <div class="following-name">
                        <a href='<%#getgroup( Eval("grpid"))%>'><%#(DataBinder.Eval(Container, "DataItem.grpname")) %></a>
                        <label style="font-size: 14px; color: #646464"><i class="fa fa-users"></i>&nbsp;&nbsp;<%#(DataBinder.Eval(Container, "DataItem.daydt")) %>&nbsp;&nbsp;<%#(DataBinder.Eval(Container, "DataItem.mndt")) %></label></a>
                    </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
        </asp:Panel>

</div>



<%@ Control Language="C#" AutoEventWireup="true" CodeFile="followers.ascx.cs" Inherits="themes_creative1_LayoutControls_profile_followers" %>
<div class="profilediv" id="profilediv" runat="server">
    <div class="followerrowdiv">
        <div class="followerrow">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="hdfollowers" runat="server" Value="0" ViewStateMode="Inherit" />
                    <script type="text/javascript">
                        Sys.Application.add_load(jScript);
                    </script>
                    <ul id="groups-list" class="item-list" role="main">
                        <asp:Repeater ID="rptfollowers" runat="server" OnItemCommand="rptfollowers_ItemCommand">
                            <ItemTemplate>
                                <li class="bp-single-group public is-admin is-member group-has-avatar">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>

                                            <div class="item-avatar">
                                                <asp:LinkButton ID="lnkimage" runat="server" CommandName="cmdimage" OnClientClick="window.document.forms[0].target='_blank';" CommandArgument='<%# Eval("followerid") %>'>
                                                    <asp:Image ID="imgprofile" CssClass="avatar group-1-avatar avatar-100 photo" runat="Server" Width="100" Height="100" />
                                                </asp:LinkButton>
                                            </div>
                                            <div class="item">
                                                <div class="item-title">
                                                    <asp:LinkButton ID="lnkusername" runat="server" OnClientClick="window.document.forms[0].target='_blank';" CommandName="username" CommandArgument='<%# Eval("followerid") %>' CssClass="heading">
                                        <h3><%# Eval("fullname")%></h3>
                                                    </asp:LinkButton>
                                                </div>
                                             
                                            </div>
                                            <div class="action">
                                                <div class="group-button public generic-button" id="groupbutton-1">
                                                    <asp:LinkButton ID="lnkfollow" CommandName="follow" runat="server" CssClass="group-button leave-group" CommandArgument='<%# Eval("followerid") %>'></asp:LinkButton>
                                                </div>
                                              
                                            </div>
                                            <div class="clear"></div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="lnkfollow" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </li>

                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                     <div class="grid-pager">
          <asp:LinkButton ID="lnkprev" runat="server" CssClass="searchpostbtn" OnClick="lnkprev_Click"><<</asp:LinkButton> 
          <asp:Repeater ID="rptPager" runat="server">
                <ItemTemplate>
                   <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                   CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>' OnClick="Page_Changed"
                        OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                </ItemTemplate>
          </asp:Repeater>
            <asp:LinkButton ID="lnknxt" runat="server" CssClass="searchpostbtn" OnClick="lnknxt_Click">>></asp:LinkButton>
    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</div>
<div class="profilemsg" id="divmsg" runat="server" visible="false">
</div>

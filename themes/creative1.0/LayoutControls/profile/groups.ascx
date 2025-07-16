<%@ Control Language="C#" AutoEventWireup="true" CodeFile="groups.ascx.cs" Inherits="themes_creative1_LayoutControls_profile_followers" %>
<div class="profilediv" id="profilediv" runat="server">
    <div class="followerrowdiv">
        <div class="followerrow">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="hdfollowers" runat="server" Value="0" ViewStateMode="Inherit" />
                    <ul id="groups-list" class="item-list" role="main">
                        <asp:Repeater ID="rptfollowers" runat="server" OnItemCommand="rptfollowers_ItemCommand">
                            <ItemTemplate>
                                <li class="bp-single-group public is-admin is-member group-has-avatar">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <script type="text/javascript">
                                                Sys.Application.add_load(jScript);
                                            </script>
                                            <div class="item-avatar">
                                                <asp:Label ID="grpid" runat="server" Text='<%# Eval("grpid")%>' Visible="false"></asp:Label>
                                                
                                                    <a id="lnkimage" runat="server" href='<%#groupUrlrewriting( Eval("grpid")) %>'>
                            <img src='<%=ConfigurationManager.AppSettings["sitepathadmin"]%>images/grpimages/thumbnail/<%# Eval("grpimg") %>' class="avatar group-1-avatar avatar-100 photo" width="100" height="100"/>
                                               </a>
                                            </div>
                                            <div class="item">
                                                <div class="item-title">
                                                         <a id="lnkusername" runat="server" href='<%#groupUrlrewriting( Eval("grpid")) %>' class="heading">
                                        <h3><%# Eval("grpname")%></h3>
                                                    </a>
                                                </div>
                                                <div class="item-meta">
                                    <span class="activity">Created On &nbsp;<%# Convert.ToDateTime(Eval("createddate")).ToString("M") %></span></div>
                                                <div class="item-desc">
                                    <p>Members &nbsp;(<asp:Label ID="membercount" runat="server"></asp:Label>)</p>
                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</div>
<div class="profilemsg" id="divmsg" runat="server" visible="false">
</div>

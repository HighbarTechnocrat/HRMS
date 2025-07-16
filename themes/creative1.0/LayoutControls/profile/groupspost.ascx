<%@ Control Language="C#" AutoEventWireup="true" CodeFile="groupspost.ascx.cs" Inherits="themes_creative1_LayoutControls_profile_groups" %>

<!-- Post Module -->
<div class="userposts">
           <span> <span id="MainContent_userfav_lblcatname">Group Wall</span>
                        </span>
             </div>
<div class="groupleftpanel" id="divleft" runat="server" visible="false">

    <asp:Repeater ID="rptcategname" runat="server">
        <ItemTemplate>
            <asp:Panel ID="pnlcat" runat="server" CssClass="widget box WiseChatWidget">
                <asp:Label ID="lbcatlid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.categoryid") %>'
                    Visible="false"></asp:Label>
                <div class="intern-padding">
                    <div class="intern-box box-title">
                        <h3><%# DataBinder.Eval(Container, "DataItem.categoryname") %></h3>
                        <asp:LinkButton ID="lnkview" CssClass="lnkview" Visible="false" runat="server" PostBackUrl='<%#onclick_hlnkcategory(Eval("CategoryId"),Eval("categoryname")) %>'>View All</asp:LinkButton>
                    </div>
                    <asp:Repeater ID="rptcatproduct" runat="server" OnItemDataBound="rptcategname_ItemDataBound"
                        OnItemCommand="rptcatproduct_ItemCommand">
                        <ItemTemplate>
                            <div class="textwidget">
                                <a href='<%#productUrlrewriting( Eval("productname"), Eval("productid")) %>'>
                                    <asp:Label ID="lblicon" runat="server"><i class="fa fa-square"></i></asp:Label><asp:Label ID="lbleventdate" runat="server" CssClass="eventdate" Text='<%# Eval("startdate")%>' Font-Bold="true" Visible="false"></asp:Label><%# Eval("productname")%>
                                    <asp:Label ID="lblpid" runat="server" Text='<%# Eval("productid")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblpname" runat="server" Text=' <%# Eval("productname") %>' Visible="false"></asp:Label>
                                </a>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </asp:Panel>
        </ItemTemplate>
    </asp:Repeater>

    <!-- Documents -->
    <asp:Repeater ID="rptcatdoc" runat="server">
    <ItemTemplate>
        <asp:Panel ID="pandoc" runat="server" CssClass="widget box WiseChatWidget">
            <div class="intern-padding">
                <asp:Label ID="lbldocid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.categoryid") %>'
                    Visible="false"></asp:Label>
                <div class="intern-box box-title">
                    <h3>
                        <%--Documents--%><%# DataBinder.Eval(Container, "DataItem.categoryname") %></h3>
                    <asp:LinkButton ID="lnkview" runat="server" CssClass="lnkview" Visible="false" PostBackUrl='<%#onclick_hlnkcategory(Eval("categoryid"),Eval("categoryname")) %>'>View All</asp:LinkButton>
                </div>
                <asp:Repeater ID="rptdoc" runat="server">
                    <ItemTemplate>
                        <div class="textwidget">
                            <a href='<%#productUrlrewriting( Eval("productname"), Eval("productid")) %>'>
                                <img src="images/doc.png" style="display: none;" />
                                <%#getdocext(Eval("filename")) %><%#(DataBinder.Eval(Container, "DataItem.productname")) %></a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </asp:Panel>
    </ItemTemplate>
</asp:Repeater>

    <!-- Photo Gallery -->

    <asp:Repeater ID="rptcatimg" runat="server">
        <ItemTemplate>
            <asp:Panel ID="pnlphoto" runat="server" CssClass="widget box widget_woffice_tasks_assigned">
                <div class="intern-padding">
                    <div class="project-assigned-container">
                                            <asp:Label ID="lblimgid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.categoryid") %>'
                        Visible="false"></asp:Label>
                        <asp:Panel ID="panimg" runat="server" CssClass="project-assigned-head">
                        <div class="intern-box box-title">
                            <h3><%# DataBinder.Eval(Container, "DataItem.categoryname") %></h3>
                            <asp:LinkButton ID="lnkview" CssClass="lnkview" runat="server" Visible="false" PostBackUrl='<%#onclick_hlnkcategory(Eval("CategoryId"),Eval("categoryname")) %>'>View All</asp:LinkButton>
                        </div>
                        <div class="gallary-main3">
                            <asp:Repeater ID="rptimg" runat="server">
                                <ItemTemplate>
                                    <div id="divimg" runat="server" class="gallary-image">
                                        <a href='<%#galleryUrlrewriting(Eval("productid")) %>'>
                                            <asp:Image ID="imghome" runat="server" />
                                        </a>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                            </asp:Panel>
                    </div>
                    </div>
            </asp:Panel>
        </ItemTemplate>
    </asp:Repeater>


    <!-- Market Place -->
        <asp:Repeater ID="rptcatads" runat="server">
    <ItemTemplate>
        <asp:Panel ID="panads" runat="server" CssClass="widget box WiseChatWidget wtads">
            <div class="intern-padding">
                <asp:Label ID="lbladsid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.categoryid") %>'
                    Visible="false"></asp:Label>
                <div class="intern-box box-title">
                    <h3>
                        <%--Documents--%><%# DataBinder.Eval(Container, "DataItem.categoryname") %></h3>
                    <asp:LinkButton ID="lnkview" runat="server" CssClass="lnkview" Visible="false" PostBackUrl='<%#onclick_hlnkcategory(Eval("categoryid"),Eval("categoryname")) %>'>View All</asp:LinkButton>
                </div>
                <asp:Repeater ID="rptads" runat="server">
                    <ItemTemplate>
                        <div class="textwidget">                                          
                                   <a href='<%#productUrlrewriting( Eval("productname"), Eval("productid")) %>'>
                                       <asp:Image ID="imgads" runat="server" class="fullwidth" hspace="20" align="left" />
                                       <span><%# Eval("productname").ToString().Length > 30 ? Eval("productname").ToString().Substring(0, 30)+"..." : Eval("productname") %></span><br /><%#getsubstr(Eval("shortdescription")) %></a> 
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </asp:Panel>
    </ItemTemplate>
</asp:Repeater>

    <!-- Video gallery -->

    <asp:Repeater ID="rptcatvideo" runat="server">
    <ItemTemplate>
        <asp:Panel ID="pnlvideo" runat="server" CssClass="widget box widget_woffice_tasks_assigned">
            <div class="intern-padding">
                <div class="project-assigned-container">
                    <asp:Label ID="lblvideoid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.categoryid") %>'
                        Visible="false"></asp:Label>
                    <asp:Panel ID="panvideo" runat="server" CssClass="project-assigned-head">
                        <div class="intern-box box-title">
                            <h3><%# DataBinder.Eval(Container, "DataItem.categoryname") %></h3>
                            <asp:LinkButton ID="lnkview" runat="server" CssClass="lnkview" Visible="false" PostBackUrl='<%#onclick_hlnkcategory(Eval("CategoryId"),Eval("categoryname")) %>'>View All</asp:LinkButton>
                        </div>
                        <div class="gallary-main">
                            <asp:Repeater ID="rptvideo" runat="server">
                                <ItemTemplate>
                                    <div class="gallary-image">
                                        <a href='<%#productUrlrewriting( Eval("productname"), Eval("productid")) %>'>
                                            <asp:Image ID="videoimg" runat="server" />
                                        </a>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </asp:Panel>
    </ItemTemplate>
</asp:Repeater>



    <!--  Fun zone -->
    <asp:Panel ID="panfun" runat="server" CssClass="widget box widget_woffice_tasks_assigned">
    <div class="intern-padding">
        <div class="intern-box box-title">
            <h3>Fun zone</h3>
            <asp:LinkButton ID="lnkview" runat="server" CssClass="lnkview" Visible="false" PostBackUrl='<%#onclick_hlnkcategory("27","Fun zone") %>'>View All</asp:LinkButton>
        </div>
        <asp:Repeater ID="rptfun" runat="server">
            <ItemTemplate>
                <div class="textwidget">
                    <a href='<%#productUrlrewriting( Eval("productname"), Eval("productid")) %>'><i class="fa fa-square"></i><%#getsubstr(Eval("shortdescription")) %></a>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Panel>
</div>

<!-- End here -->
<!-- Mywall -->
<div class="grouprightpanel">
    <asp:Panel ID="pnlpst" runat="server" Visible="true">
    <div class="comments-summerytitle">
        <div class="comments-summerytitle">
            <div class="mywallgrp">
                <div class="commentsdiv">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
<asp:Repeater ID="rptwall" runat="server" OnItemCommand="rptwall_ItemCommand">
                                                        <ItemTemplate>
                                                            <asp:UpdatePanel ID="updreview" runat="server" UpdateMode="Always">
                                                                <ContentTemplate>
                                                                    <script type="text/javascript">
                                                                        Sys.Application.add_load(jScript);
                                                                    </script>
                                                                    <div class="textwidget bd_wall">
                                                                        <a id="lnkimage" runat="server" class="imgwallpic" target="_blank" href='<%#getuser(Eval("indexid")) %>'>
                                                                            <img id="imgprofile" runat="server" alt=""
                                                                                src='<%#getuserimage(Eval("profilephoto")) %>' />
                                                                        </a>
                                                                        <div class="bd_wallright">
                                                                            <a id="lnkusername" runat="server" class="imgwallpic" href='<%#productUrlrewriting2( Eval("cattype"),Eval("productname"), Eval("productid")) %>'>
                                                                                <%# DataBinder.Eval(Container, "DataItem.productname") %></a> 
                                                                            <%-- SAGAR COMMENTED BELOW CODE FOR REMOVING POSTED BY AND NAME OF THE USER WHO POSTED THE POST 9OCT2017 STARTS HERE--%>
                                                                            <%--posted by <a id="LinkButton1" runat="server" class="imgwallpic" href='<%#getuser(Eval("indexid")) %>'><%# DataBinder.Eval(Container, "DataItem.fullname") %></a>--%>
                                                                             <%-- SAGAR COMMENTED BELOW CODE FOR REMOVING POSTED BY AND NAME OF THE USER WHO POSTED THE POST 9OCT2017 ENDS HERE--%><br />
                                                                            <asp:Label ID="lbldate" runat="server" Text='<%#getdate(Eval("createdon")) %>'></asp:Label>
                                                                            <asp:Panel ID="img1" runat="server" Visible="false">
                                                                                <div class="user-comment">
                                                                                    <a href='<%#productUrlrewriting2( Eval("cattype"),Eval("productname"), Eval("productid")) %>'>
                                                                                        <span class="wallimg"><%#getproductimage(Eval("bigimage")) %> </span></a>
                                                                                </div>
                                                                            </asp:Panel>
                                                                            <asp:Panel ID="pnlvideo" runat="server" Visible="false">
                                                                                <div class="user-comment">
                                                                                    <span class="wallvideo"><%#getpostvideo(Eval("videoembed"), Eval("movietrailorcode")) %> </span>
                                                                                </div>
                                                                            </asp:Panel>
                                                                            <asp:Panel ID="pstdoc" runat="server" Visible="false">
                                                                                <div class="user-comment">
                                                                                    <span class="doclink"><a href='<%#productUrlrewriting2( Eval("cattype"),Eval("productname"), Eval("productid")) %>'>
                                                                                      
                                                                                        <%#getdocext(Eval("filename")) %> &nbsp;<span class="docfilename"><%#getpostdoc(Eval("filename")) %></span>
                                                                                    </a></span>
                                                                                </div>
                                                                            </asp:Panel>
                                                                            <div class="user-comment">
                                                                             
                                                                                <%#getshortdesc(Eval("shortdescription")) %>
                                                                            </div>
                                                                            <br />
                                                                            <div class="writereview" id="MainContent_reviewcomments_writereview">
                                                                                <asp:Label ID="lblpid" runat="server" Text='<%# Eval("productid") %>' Visible="false"></asp:Label>
                                                                                <asp:LinkButton ID="lnklike" runat="server" CssClass="btn-like" CommandName="cmdlike" CommandArgument='<%# Eval("productid") %>'>
                                                                                    <asp:Label ID="lbllike" runat="server"></asp:Label>
                                                                                    <asp:Label ID="lbllikecount" runat="server"></asp:Label>
                                                                                    <asp:Label ID="lblcnt" runat="server" Visible="false"></asp:Label>
                                                                                </asp:LinkButton>&nbsp;&nbsp;&nbsp;
                                            <asp:LinkButton ID="lnkdislike" CssClass="btn-like" runat="server" CommandName="cmddislike" CommandArgument='<%# Eval("productid") %>'>
                                                <asp:Label ID="lbldislike" runat="server"></asp:Label>
                                                <asp:Label ID="lbldislikecount" runat="server"></asp:Label>
                                                <asp:Label ID="lbldcnt" runat="server" Visible="false"></asp:Label>
                                            </asp:LinkButton>&nbsp;&nbsp;&nbsp;
                      <asp:LinkButton ID="lnkcomm" runat="server" CssClass="btn-like" CommandName="cmdcomment" CommandArgument='<%# Eval("productid")%>' PostBackUrl='<%#productUrlrewriting2( Eval("cattype"),Eval("productname"), Eval("productid")) %>'> </asp:LinkButton>
                                                                            </div>
                                                                            <asp:LinkButton ID="lnknext" runat="server" OnClick="Page_Changed" Style="display: none;">Next</asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="lnklike" EventName="Click" />
                                                                    <asp:AsyncPostBackTrigger ControlID="lnkdislike" EventName="Click" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div id="desc" runat="server" visible="false"></div>
            <div class="grid-pager">
                <asp:LinkButton ID="lnkprev" runat="server" CssClass="searchpostbtn" OnClick="lnkprev_Click" Visible="false"><<</asp:LinkButton>
                <asp:Repeater ID="rptPager" runat="server">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                            CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'
                            OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:LinkButton ID="lnknxt" runat="server" CssClass="searchpostbtn" OnClick="lnknxt_Click" Visible="false">>></asp:LinkButton>
            </div>
        </div>
    </div>
        </asp:Panel>
     <div class="userpostscats">
           <asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>
       </div>
</div>

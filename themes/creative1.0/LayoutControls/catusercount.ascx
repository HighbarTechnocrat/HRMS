<%@ Control Language="C#" AutoEventWireup="true" CodeFile="catusercount.ascx.cs" Inherits="Themes_SecondTheme_LayoutControls_catusercount" ViewStateMode="Enabled" EnableViewState="true" %>

<asp:Panel ID="pnlcat" runat="server">
    <asp:Repeater ID="rptcatuser" runat="server">
        <ItemTemplate>
            <div class="categoryheading-home">

                <asp:Label ID="lblname" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.categoryname") %>'></asp:Label>

            </div>
            <asp:Label ID="lbcatlid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.categoryid") %>'
                Visible="false"></asp:Label>
            <div class="list_carousel">
                <asp:Literal ID="ltrjs" runat="server"></asp:Literal>
                <asp:Literal ID="ltrcat" runat="server"></asp:Literal>
                <asp:Repeater ID="rptcatuserproduct" runat="server" OnItemDataBound="rptcatuserproduct_ItemDataBound" OnItemCommand="rptcatuserproduct_ItemCommand">
                    <ItemTemplate>
                        <div class="carouselproitem">
                            <div class="view-first">
                                <div class="view view-content">
                                    <div class="image">
                                        <a href='<%#productUrlrewriting( Eval("productname"), Eval("productid")) %>'>
                                            <img src='<%=ConfigurationManager.AppSettings["sitepathadmin"]%>images/smallproduct/<%# Eval("smallimage") %>'
                                                alt="image" height="325" width="225" /></a>
                                    </div>
                                    <div class="mask">
                                        <a href='<%#productUrlrewriting( Eval("productname"), Eval("productid")) %>'>
                                            <h2>
                                                <%# Eval("productname") %></h2>
                                            <asp:Label ID="lblpname" runat="server" Text='<%# Eval("productname") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblpid" runat="server" Text='<%# Eval("productid")%>' Visible="false"></asp:Label>

                                            <asp:Label ID="lblimgsmall" runat="server" Visible="false" Text=' <%# Eval("smallimage") %>'></asp:Label>

                                        </a>
                                        <div class="ratingstar">
                                            <ajaxToolkit:Rating ID="ratingcatuser" runat="server" MaxRating="5"
                                                StarCssClass="ratingStar" WaitingStarCssClass="savedRatingStar" FilledStarCssClass="filledRatingStar"
                                                EmptyStarCssClass="emptyRatingStar" ReadOnly="true">
                                            </ajaxToolkit:Rating>
                                            <asp:Label ID="lblpkg" runat="server" Text='<%# Eval("parentflag") %>' Visible="true"></asp:Label>

                                            
                                        </div>
                                        <a href='<%#productUrlrewriting( Eval("productname"), Eval("productid")) %>'>
                                            <p>
                                                <%# Eval("shortdescription")%>
                                            </p>
                                            <asp:Label ID="lblshortdesr" runat="server" Visible="false" Text=' <%# Eval("shortdescription")%>'></asp:Label>
                                        </a>
                                        <div class="info">
                                            <div class="pinit">
                                                <asp:Literal ID="ltrimage" runat="server"></asp:Literal>
                                            </div>
                                            <div class="favourite" id="divfav" runat="server">
                                                <a href="ReturnUrl("sitepathmain")procs/wishlist/<%# Eval("productid") %>" class='<%#productfav(Eval("productid")) %>' id="lnkfav"></a>
                                                <span id="spanpkg" runat="server"><span>
                                                    <div class="pakgsubscript" id="divsub" runat="server">
                                                        <asp:LinkButton ID="lblmessage" runat="server" CssClass="pakgsubscriptbtn"></asp:LinkButton>
                                                    </div>
                                                </span></span>
                                            </div>
                                            <div class="playbtn">
                                                <a title='Play' href='<%#productUrlrewriting( Eval("productname"), Eval("productid")) %>'>
                                                    <i class="fa fa-play"></i></a>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="name">
                                    <a title='<%# Eval("productname") %>' href="#">
                                        <%# Eval("productname") %></a>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>

</asp:Panel>

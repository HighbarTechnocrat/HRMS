<%@ Control Language="C#" AutoEventWireup="true" CodeFile="productusercount.ascx.cs" Inherits="themes_creative1_LayoutControls_productusercount"  ViewStateMode="Enabled" EnableViewState="true"%>
<asp:Panel ID="pnlrecent" runat="server">
  <div class="categoryheading-home"><span>Mostly Viewed</span></div>
  <div class="list_carousel">
    <div id="carousel" class="IM-carousel">
      <asp:Repeater ID="rptproductcount" runat="server"  OnItemDataBound="rptproductcount_ItemDataBound" OnItemCommand="rptproductcount_ItemCommand">
        <ItemTemplate>
          <div class="carouselproitem">
            <div class="view-first">
              <div class="view view-content">
                <div class="image"> <a href='<%#productUrlrewriting( Eval("productname"), Eval("productid")) %>'> <img src='<%=ConfigurationManager.AppSettings["sitepathadmin"]%>images/smallproduct/<%# Eval("smallimage") %>'
                                        alt="image"  /></a></div>
                <div class="mask"> <a href='<%#productUrlrewriting( Eval("productname"), Eval("productid")) %>'>
                  <h2> <%# Eval("productname") %></h2>
                  <asp:Label ID="lblpname" runat="server" Text=
                                            ' <%# Eval("productname") %>' Visible="false"></asp:Label>

                                          
                                             <asp:Label ID="lblimgsmall" runat="server" Visible="false" Text = ' <%# Eval("smallimage") %>' ></asp:Label>
                  </a>
                  <div class="ratingstar">
                    <ajaxToolkit:Rating ID="Rating1" runat="server" MaxRating="5"
                                            StarCssClass="ratingStar" WaitingStarCssClass="savedRatingStar" FilledStarCssClass="filledRatingStar"
                                            EmptyStarCssClass="emptyRatingStar" ReadOnly="true"> </ajaxToolkit:Rating>
                    <asp:Label ID="lblpkg" runat="server" Text='<%# Eval("parentflag") %>' Visible="true"></asp:Label>

                     
                  </div>
                  <a href='<%#productUrlrewriting( Eval("productname"), Eval("productid")) %>'>
                  <p>
                    <asp:Label ID="lblpid" runat="server" Text='<%# Eval("productid")%>' Visible="false"></asp:Label>
                    <%# Eval("shortdescription")%>
                     <asp:Label ID="lblshortdesr" runat="server" Visible="false" Text=' <%# Eval("shortdescription")%>'></asp:Label>
                    </p>
                  </a>
                  <div class="info">
                      <div class="pinit">
                       <asp:Literal ID="ltrimage" runat="server"></asp:Literal>
                          </div>
                    <div class="favourite" id="divfav" runat="server">
                     
                          <a href="ReturnUrl("sitepathmain")procs/wishlist/<%# Eval("productid") %>" class='<%#productfav(Eval("productid")) %>' id="lnkfav"></a>
                          <span id="spanpkg" runat="server"><span>
                          <div class="pakgsubscript" id="divsub" runat="server">
                            <asp:LinkButton  ID="lblmessage" runat="server"  CssClass="pakgsubscriptbtn"></asp:LinkButton>
                          </div>
                          </span></span>
                    </div>
                    <div class="playbtn"> <a title='Play' href='<%#productUrlrewriting( Eval("productname"), Eval("productid")) %>'> <i class="fa fa-play"></i></a> </div>
                  </div>
                </div>
              </div>
              <div class="name"> <a title='<%# Eval("productname") %>' href="#"> <%# Eval("productname") %></a> </div>
            </div>
          </div>
        </ItemTemplate>
      </asp:Repeater>
    </div>
  </div>
  </asp:Panel>

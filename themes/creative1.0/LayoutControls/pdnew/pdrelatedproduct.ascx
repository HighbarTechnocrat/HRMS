<%@ Control Language="C#" AutoEventWireup="true" CodeFile="pdrelatedproduct.ascx.cs"
    Inherits="themes_secondtheme_LayoutControls_pdnew_pdrelatedproduct" %>
<div id="filmsyoumaylikediv" runat="server" class="filmsyoumaylikediv">
  <div id="filmsyoumaylike" class="filmsyoumaylike">
    <div class="filmsyoumaylike-heading wow fadeInDown">Other Photos In This Album</div>
    <div class="IM-carousel wow slideInRight">
            <asp:Repeater ID="rptarrival" runat="server"  OnItemDataBound="rptarrival_ItemDataBound" OnItemCommand="rptarrival_ItemCommand">
                <ItemTemplate>
                    <div class="carouselproitem">
                        <div class="view-first">
                            <div class="view view-content">
                                <div class="image">
                                    <a href='<%#productUrlrewriting( Eval("productname"), Eval("productid")) %>' title="<%# Eval("productname") %>">
                                    <img src='<%=ConfigurationManager.AppSettings["sitepathadmin"]%>images/smallproduct/<%# Eval("smallimage") %>'
                                        alt="image"/>
                                     </a>
                                </div>
                                <div class="mask">
                                    <a href='<%#productUrlrewriting( Eval("productname"), Eval("productid")) %>' title="<%# Eval("productname") %>">
                                        <h2>
                                            <%# Eval("productname") %>
                                        </h2>
                                             <asp:Label ID="lblpname" runat="server" Text=
                                            ' <%# Eval("productname") %>' Visible="false"></asp:Label>
                                              <asp:Label ID="lblimgsmall" runat="server" Visible="false" Text = ' <%# Eval("smallimage") %>' ></asp:Label>
                                    </a>
                                    <div class="ratingstar">
                                        <ajaxToolkit:Rating ID="Rating1" runat="server" BehaviorID="Ratingproduct" MaxRating="5"
                                            StarCssClass="ratingStar" WaitingStarCssClass="savedRatingStar" FilledStarCssClass="filledRatingStar"
                                            EmptyStarCssClass="emptyRatingStar" ReadOnly="true">
                                        </ajaxToolkit:Rating>
                                        <asp:Label ID="lblpkg" runat="server" Text='<%# Eval("parentflag") %>' Visible="true"></asp:Label>
                                   
                                    </div>
                                    <a href='<%#productUrlrewriting( Eval("productname"), Eval("productid")) %>' title="<%# Eval("productname") %>">
                                        <p>
                                            <%# Eval("shortdescription")%></p>
                                             <asp:Label ID="lblshortdesr" runat="server" Visible="false" Text=' <%# Eval("shortdescription")%>'></asp:Label>
                                    </a>
                                    <div class="info">
                                        <div class="pinit">
                                        <asp:Literal ID="ltrimage" runat="server"></asp:Literal>
                                    </div>
                                        <div class="favourite" id="divfav" runat="server">
                                          <a href="ReturnUrl("sitepathmain")procs/wishlist/<%# Eval("productid") %>" class='<%#productfav(Eval("productid")) %>' id="lnkfav"></a>

      <span id="spanpkg" runat="server">
          <span>
            <div id="divsub" runat="server">
             <asp:LinkButton  ID="lblmessage" runat="server" Enabled="false"></asp:LinkButton>
            </div>
        </span>
      </span>
                        <asp:Panel ID="pnlpkgs" runat="server" Visible="false">
                       <span id="spanmessage" runat="server"><span>
                          <div  id="div1" runat="server">
                            <asp:LinkButton  ID="lnksavemessage" runat="server" Enabled="false"></asp:LinkButton>
                          </div>
                          </span></span></asp:Panel>
                                        </div>
                                        <div class="playbtn">
                                            <a title='Play' href='<%#productUrlrewriting( Eval("productname"), Eval("productid")) %>'>
                                                <i class="fa fa-play"></i></a>
                                        </div>
                                    </div>
                                </div>
<%--                                <div class="name">
                                    <a title='<%# Eval("productname") %>' href="#">
                                        <%# Eval("productname") %></a>
                                </div>--%>                             
                                <asp:Label ID="lblpid" runat="server" Visible="false" Text='<%# Eval("productid") %>'></asp:Label>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
</div>
  </div>
</div>



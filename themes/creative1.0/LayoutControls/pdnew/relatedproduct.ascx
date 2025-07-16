<%@ Control Language="C#" AutoEventWireup="true" CodeFile="relatedproduct.ascx.cs" Inherits="themes_creative1_LayoutControls_pdnew_relatedproduct" %>
    <link href="ReturnUrl("sitepathmain")ver1/css/carousel/carousel.css" rel="stylesheet" type="text/css" />
    <link href="<%=ReturnUrl("css") %>boilerplate.css" rel="stylesheet" type="text/css"  />
    <link href="<%=ReturnUrl("css") %>fontawesome/font-awesome.css" rel="stylesheet" type="text/css"  />
    <script type="text/javascript" src="ReturnUrl("sitepathmain")ver1/js/carousel/carousel.js"></script>
    <div id="filmsyoumaylikediv" runat="server" class="filmsyoumaylikediv">
  <div class="filmsyoumaylike">
    <div class="filmsyoumaylike-heading wow fadeInDown">Films you may like</div>
      <div class="IM-carousel wow slideInRight">
       <asp:Repeater ID="rptarrival" runat="server">
       <ItemTemplate>
        <div class="carouselproitem">
         <div class="view-first">
          <div class="view view-content">
            <div class="image">
                <a href='<%#productUrlrewriting( Eval("productname"), Eval("productid")) %>' title="<%# Eval("productname") %>">
                    <img src='<%=ConfigurationManager.AppSettings["sitepathadmin"]%>images/smallproduct/<%# Eval("smallimage") %>' alt="image"/>
                </a>    
            </div>
            <div class="mask"> 
               <a href='<%#productUrlrewriting( Eval("productname"), Eval("productid")) %>' title="<%# Eval("productname") %>">
                  <h2>
                  <%# Eval("productname") %>
                  </h2>
                  <asp:Label ID="lblpid" runat="server" Text=' <%# Eval("productid") %>' Visible="false"></asp:Label>
              </a>
              <div class="ratingstar">
                  <ajaxToolkit:Rating ID="Rating1" runat="server" BehaviorID="Ratingproduct" MaxRating="5"
                                            StarCssClass="ratingStar" WaitingStarCssClass="savedRatingStar" FilledStarCssClass="filledRatingStar"
                                            EmptyStarCssClass="emptyRatingStar" ReadOnly="true">
                  </ajaxToolkit:Rating>
                  <asp:Label ID="lblpkg" runat="server" Text='<%# Eval("parentflag") %>' Visible="true"></asp:Label>
                  <div class="pinit">
                  <asp:Literal ID="ltrimage" runat="server"></asp:Literal>
                  </div>
              </div>
              <a href='<%#productUrlrewriting( Eval("productname"), Eval("productid")) %>' title="<%# Eval("productname") %>">
                  <p><%# Eval("shortdescription")%></p>
                  <asp:Label ID="lblshortdesr" runat="server" Visible="false" Text=' <%# Eval("shortdescription")%>'></asp:Label>
              </a>
              <div class="info">
                <div class="favourite">
                   <asp:LinkButton ID="lnkfav" runat="server" PostBackUrl='ReturnUrl("sitepathmain")procs/wishlist/<%# Eval("productid") %>'>
                       <i class="fa fa-heart-o"></i>
                    </asp:LinkButton>
                </div>
                <div class="playbtn">
                    <a title='Play' href='<%#productUrlrewriting( Eval("productname"), Eval("productid")) %>'>
                       <i class="fa fa-play"></i>
                    </a>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </ItemTemplate>
    </asp:Repeater>
      </div>
  </div>
</div>
    <script>
    $('.IM-carousel').IMCarousel({
        loop: true,
        margin: 50,
        responsiveClass: true,
        responsive: {
            0: {
                items: 2,
                nav: false,
                loop: false
            },
            320: {
                items: 3,
                nav: false,
                loop: false
            },
            480: {
                items: 3,
                nav: false,
                loop: false
            },
            640: {
                items: 4,
                nav: false,
                loop: false
            },
            720: {
                items: 4,
                nav: false,
                loop: false
            },
            1000: {
                items: 4,
                nav: true,
                loop: false
            },
            1200: {
                items: 5,
                nav: true,
                loop: false
            },
            1400: {
                items: 5,
                nav: true,
                loop: false
            },
            1500: {
                items: 6,
                nav: true,
                loop: false
            },
            1600: {
                items: 6,
                nav: true,
                loop: false
            },
            1800: {
                items: 7,
                nav: true,
                loop: false
            },
            2200: {
                items: 9,
                nav: true,
                loop: false
            },
            2400: {
                items: 10,
                nav: true,
                loop: false
            },
            2800: {
                items: 14,
                nav: true,
                loop: false
            },
        }
    })
</script> 
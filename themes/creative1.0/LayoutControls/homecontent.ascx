<%@ Control Language="C#" AutoEventWireup="true" CodeFile="homecontent.ascx.cs" Inherits="themes_creative1" %>
<%@ Register Src="~/Components/Common/homeglobal.ascx" TagName="home" TagPrefix="uc" %>

<%--<script src="<%=ReturnUrl("sitepath") %>js/highbar/jquery.js"></script>--%>
<script type="text/javascript" src="Images_effects/a.js"></script>
<link rel="stylesheet" type="text/css" href="Images_effects/style.css" />
<script type="text/javascript" src="/Images_effects/jquery.js"></script>
<script type="text/javascript" src="Images_effects/a.js"></script>


<div id="left-content">
    <!-- START FEATURED IMAGE AND TITLE -->
    <header id="featuredbox" class="centered ">
        <div style="display: none">
            <div class="slider1">
                <asp:Repeater ID="rptbanner" runat="server">
                    <ItemTemplate>
                        <div class="slide">
                            <div class="pagetitle animate-me fadeIn" style="display: none">
                                <h1><%# Eval("altname") %></h1>
                            </div>
                            <div class="featured-background">
                                <!-- style="background-image: url('images/1.jpg');" -->

                                <%-- <a href='<%=ReturnUrl("ongoingProjectonHome") %>'>--%>

                                <img id="bannerimg" src='<%=ReturnUrl("sitepathadmin") %>images/banner/<%# Eval("imagename") %>' class="fullscreen" /></a>

                                 <%--  <div class="featured-background1" ><!-- style="background-image: url('images/1.jpg');" -->
                                   <a href="<%# Eval("url") %>" target="_blank">
                     
                                <img id="bannerimg1"  src='<%=ReturnUrl("sitepathadmin") %>images/banner/bannermobile/<%# Eval("imagename") %>' class ="fullscreen"/></a>--%>
                                                <%-- <div class="featured-background">
                                    <div class="featured-layer"></div>  
                                </div>--%>
                                                <%-- <script type="text/javascript">
                                       jQuery(document).ready(function () {
                                           var width;
                                           width = jQuery(window).width();
                                           if (width < 340) {
                                               jQuery('#bannerimg').attr('src', jQuery('#bannerimg').attr('src').replace("/banner/", "/bannermobile/"));
                                           }
                                       });
					                </script>--%>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>


        <div class="ruled1">

            <div class="ws_images">
                <div id="wowslider-container1">
                    <div class="ws_images">
                        <ul>
                            <asp:Repeater ID="rptSlider" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <%# Eval("Image_URL") != DBNull.Value && !string.IsNullOrEmpty(Eval("Image_URL").ToString()) ? 
                                        "<a href='" + Eval("Image_URL") + "' target='_blank'>" : "" %>
                                        <img src='<%# ResolveUrl("~/Images_a/") + Eval("Image_Name") %>' alt="Highbar">
                                        <%# Eval("Image_URL") != DBNull.Value && !string.IsNullOrEmpty(Eval("Image_URL").ToString()) ? "</a>" : "" %>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
            </div>
        </div>



        <script type="text/javascript" src="Images_effects/scriptB.js"></script>
        <script src="Images_effects/wowslider.js"></script>
        <script>
            document.addEventListener("DOMContentLoaded", function () {
                // Initialize WowSlider
                new WOW().init();
            });
        </script>
    </header>




    <uc:home ID="uxhome" runat="server" />
    <div id="scroll-top-container">
        <a href="#main-header" id="scroll-top">
            <i class="fa fa-arrow-circle-o-up"></i>
        </a>
    </div>
</div>


<%@ Control Language="C#" AutoEventWireup="true" CodeFile="pdfproduct.ascx.cs" Inherits="Themes_SecondTheme_LayoutControls_pdnew_pdfproduct" %>

<%--Sony commented entire page code to HIDE display of Like, Dislikes, Views, display from front end--%>
<!-- 
 <div class="productsummerytitle">
<div class="movieinfodiv" style="display:none;">
      <div class="movieinfo wow zoomIn" id="divlength" runat="server" visible="false">Length: <asp:Label ID="lbltime" runat="server"></asp:Label></div>
      <div class="movieinfo wow zoomIn" id="divrating" runat="server" visible="false">Star Rateing: <asp:Label ID="lblrating" runat="server"></asp:Label></div>
      <div class="movieinfo wow zoomIn" id="diview" runat="server">Views: </div>
     <div class="movieinfo wow zoomIn" id="divlike" runat="server">Likes: </div>
    <div class="movieinfo wow zoomIn" id="divreviewcount" runat="server">Comments:</div>
</div>
<div class="Default">

  <div class="productsummerytitle">
<div class="wpb-container vc_row wpb_row vc_row-fluid vc_custom_1459742395186"><div class="container"><div class="row">
    <div class="col-sm-3 col-has-fill">
        <div class="vc_column-inner vc_custom_1459742341368">
            <div class="wpb_wrapper"> <div class="counters default ">
   <div class="counter-wrap">
				 	<i class="fa fa-thumbs-up" aria-hidden="true"></i>
				<span class="clearfix"></span>
	   <span class="counter counterUp"><asp:Label ID="lbllike" runat="server" CssClass="numcounter"></asp:Label></span>
   </div> 
    <h5>Like</h5>
</div>
</div></div>
    </div>

    <div class="col-sm-3 col-has-fill">
        <div class="vc_column-inner vc_custom_1459742341368">
            <div class="wpb_wrapper"> <div class="counters default ">
   <div class="counter-wrap">
				 	<i class="fa fa-thumbs-down" aria-hidden="true"></i>
				<span class="clearfix"></span>
	   <span class="counter counterUp"><asp:Label ID="lbldislike" runat="server" CssClass="numcounter"></asp:Label></span>
   </div> 
    <h5>Dislike</h5>
</div>
</div></div>
    </div>

    <div class="col-sm-3 col-has-fill">
        <div class="vc_column-inner vc_custom_1459742350486"><div class="wpb_wrapper"> <div class="counters default ">
   <div class="counter-wrap">
				 	<i style="" class="fa fa-comment"></i>
				<span class="clearfix"></span>
	   <span class="counter counterUp"><asp:Label ID="reviewcount" runat="server" CssClass="numcounter"></asp:Label></span>
   </div> 
    <h5>Comments</h5>
</div>
</div></div>
    </div>
    <div class="col-sm-3 col-has-fill">
        <div class="vc_column-inner vc_custom_1459742358622"><div class="wpb_wrapper"> <div class="counters default ">
   <div class="counter-wrap">
				 	<i class="fa fa-heart"></i>
				<span class="clearfix"></span>
	   <span class="counter counterUp"><asp:Label ID="lblfav" runat="server" CssClass="numcounter"></asp:Label> </span>
   </div> 
    <h5>Favourite</h5>
</div>
</div></div>

    </div>
	 <div class="col-sm-3 col-has-fill">
        <div class="vc_column-inner vc_custom_1459742358622"><div class="wpb_wrapper"> <div class="counters default ">
   <div class="counter-wrap">
				 	<i class="fa fa-street-view"></i>
				<span class="clearfix"></span>
	   <span class="counter counterUp"><asp:Label ID="lblview" runat="server" CssClass="numcounter"></asp:Label></span>
   </div> 
    <h5>Views</h5>
</div>
</div></div>

    </div>
</div></div>
    <div class="down-arrow bounce"  id="gotocritics" runat="server">
<a href="#MainContent_pdnewglobal_m_uxPdLayout_ctl04_criticcornerdiv"><i class="fa fa-angle-down"></i></a>
</div>  

</div></div>

    </div></div>
<asp:HiddenField ID="hdproductid" runat="server" />
<script type='text/javascript' src='<%=ReturnUrl("sitepath") %>js/num-count.js'></script>
<script type='text/javascript' src='<%=ReturnUrl("sitepath") %>js/number-counter.js'></script>
<script type="text/javascript">
    jQuery(document).ready(function ($) {
        $('.numcounter').counterUp({
            delay: 10,
            time: 1000
        });
    });
            </script>

-->  
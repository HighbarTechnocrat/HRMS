<%@ Page Language="C#" MasterPageFile="~/cms.master" AutoEventWireup="true" CodeFile="projectdetail.aspx.cs" Inherits="projectdetail" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <link href="<%=ReturnUrl("css") %>movie-detail/movie-detail.css" rel="stylesheet" type="text/css"  />
	 <link href="<%=ReturnUrl("css") %>includes/mywall.css" rel="stylesheet" type="text/css"  />
    <style>.sitelogo{left:0;top:0;}.productsummerytitlenew{border:none;float:none;}.productname{float:none;width:auto;}</style>
  
   
    <div id="productsummerynew" class="productsummeryinew">
        <div class="Default prodetail-new">
        <div class="productsummerytitlenew">
		   <asp:Repeater ID="rptrprojectdetail2" runat="server">
                <ItemTemplate>
                     <div class="productname wow fadeInDown animated" style="visibility: visible; animation-name: fadeInDown;">
						<asp:Label ID="lblprojectttl" runat="server" Text='<%# Bind("projecttitle") %>' ></asp:Label> 
						<%-- <asp:Label ID="lbldate" visible="false" runat="server" Text='<%#((DateTime)DataBinder.Eval(Container, "DataItem.projectcompletedate")).ToString("dd-MMM-yyyy") %>'></asp:Label>--%>
                         <asp:Label ID="lbldate" visible="false" runat="server" Text=' <%# DataBinder.Eval(Container, "DataItem.projectcompletedate")%>'></asp:Label>
                     </div>
                     <div class="synopsis wow fadeInDown animated main-div" style="visibility: visible; animation-name: fadeInDown;">
						 <div class="projectstatusnew" id="Cyear" runat="server">
						 <asp:Panel ID="pnlyr" runat="server">
						<asp:Label ID="spnyr" runat="server" class="scopeworkCY" Text="Completion Year : "></asp:Label>
						<div class="newdivpCY"><p><%# ((DateTime)DataBinder.Eval(Container, "DataItem.projectcompletedate")).ToString("yyyy")%></p> 
						</div>
						<asp:Label ID="lblyr" runat="server" Visible="false" Text='<%# ((DateTime)DataBinder.Eval(Container, "DataItem.projectcompletedate")).ToString("yyyy")%>' ></asp:Label> 
						</asp:Panel>
						</div>
					</div>
                </ItemTemplate>
           </asp:Repeater>
		           <asp:Panel ID="pnlimg1" runat="server">
						<div class="slidercouraselnew">
						<!--<span class="scopework12">Progress Photograph:</span>-->
						<div class="bx-wrappernew" style="max-width: 1240px; margin: 0px auto;">
							<div class="bx-viewport" style="width: 100%; overflow: hidden; position: relative; height: 186px;">
								<div id="carousel" class="slider2" style="width: 715%; position: relative; transition-duration: 0s; transform: translate3d(-1077px, 0px, 0px);">
								<asp:Repeater ID="rptprojectimages1" runat="server">           
                                <ItemTemplate>
								
									<%--<a class="example-image-link" href='<%=ConfigurationManager.AppSettings["sitepathadmin"]%>images/moviebanner/<%# Eval("imagename") %>' data-lightbox="example-set" data-title='<%# DataBinder.Eval(Container, "DataItem.title") %>'>
									<div class="slidenew">
									<div class="view-first">
										<div class="view-content">                                 
										<div class="imagenew">
									<img id="thumb_37" class="example-image"   width="200" height="150" src='<%=ConfigurationManager.AppSettings["sitepathadmin"]%>images/moviebanner/<%# Eval("imagename") %>' alt='<%# DataBinder.Eval(Container, "DataItem.title") %>'/>
									
									</div>                                   
										</div>
									</div>
									</div>
									</a>--%>
								
								
								
									<%--<a rel="lightbox" title='<%# DataBinder.Eval(Container, "DataItem.title") %>' id="MainContent_homebanner_msviewprod_rptfamilyproduct_Hlinkimg_9" class="recentlyaddproduct bx-clone" href='<%=ConfigurationManager.AppSettings["sitepathadmin"]%>images/moviebanner/<%# Eval("imagename") %>' style="float: left; list-style: outside none none; position: relative; width: 205.4px; margin-right: 10px;">--%>
									<a class="example-image-link" href='<%=ConfigurationManager.AppSettings["sitepathadmin"]%>images/moviebanner/<%# Eval("imagename") %>' data-lightbox="example-set" data-title='<%# DataBinder.Eval(Container, "DataItem.title") %>'>
									<div class="slidenew">
									<div class="view-first">
										<div class="view-content">                                 
										<div class="imagenew">
											<img id="thumb_37" class="example-image"  src='<%=ConfigurationManager.AppSettings["sitepathadmin"]%>images/moviebanner/<%# Eval("imagename") %>' width="200" height="150" >                                              
											<asp:Label ID="lblad"  runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.title") %>' Visible="false"></asp:Label>           
										</div>                                   
										</div>
									</div>
									</div>
									</a>
								</ItemTemplate>
								</asp:Repeater> 
								</div>
							</div>
						</div>
						</div>
					</asp:Panel>
            <asp:Repeater ID="rptrprojectdetail" runat="server">
                <ItemTemplate>
                     <%--<div class="productname wow fadeInDown animated" style="visibility: visible; animation-name: fadeInDown;">
                    <asp:Label ID="lblprojectttl" runat="server" Text='<%# Bind("projecttitle") %>' ></asp:Label> 
                       <asp:Label ID="lbldate" visible="false" runat="server" Text='<%#((DateTime)DataBinder.Eval(Container, "DataItem.projectcompletedate")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                          <asp:Label ID="lbldate" visible="false" runat="server" Text=' <%# DataBinder.Eval(Container, "DataItem.projectcompletedate")%>'></asp:Label>
                     </div>
                                <div class="synopsis wow fadeInDown animated main-div" style="visibility: visible; animation-name: fadeInDown;">
<div class="projectstatusnew">
 <span class="scopeworkCY">Completion Year : </span><div class="newdivpCY"><p><%# ((DateTime)DataBinder.Eval(Container, "DataItem.projectcompletedate")).ToString("yyyy")%></p> </div>
 </div>--%>
                                      <div class="projdesc" >
                                       <div class="newdivp1"><%# DataBinder.Eval(Container, "DataItem.projectdescsmall")%></div>  
        </div>

                                  <div class="clientnew2" ><span class="scopework">Client:</span><p> <%# DataBinder.Eval(Container, "DataItem.client")%></p>
        </div>
                                    <div class="clientvaluenew"><span class="scopework">Value:</span><p><%# DataBinder.Eval(Container, "DataItem.Value")%> </p>
        </div>
        <div class="readmorenew2"><span class="scopework">Scope Of Work:</span><div class="newdivp"><%# DataBinder.Eval(Container, "DataItem.projectdesclong")%></div>
        </div>

                                    <div class="" style="display:none"> <span class="scopework">Major Achievement:</span><%# DataBinder.Eval(Container, "DataItem.majorachievements")%>
        </div>
        
                                    <div class="projectstatusnew"><span class="scopework">Project Status:</span><div class="newdivpnew"><%# DataBinder.Eval(Container, "DataItem.projectstatus")%></div>
        </div>

                                 <%--<asp:Button ID="btnviewall" runat="server" Text="View All" OnClick="Viewall_Click"></asp:Button>--%>
                                
                                        <%-- <div class="new" >
                                        Image:<p> <%# DataBinder.Eval(Container, "DataItem.projectimage")%></p>--%>
                                          <%--<asp:Image ID="imgbanner1" runat="server" AlternateText='<%# DataBinder.Eval(Container, "DataItem.projectimage") %>' CssClass="universalmultiimg" ToolTip="Click to view image in full size." />--%>
                                <%--        <asp:Image ID="Image1" src='<%# Eval("ImageURL") %>' runat="server" ClientIDMode="Inherit"  />  
                                      </div>  --%>       

                                <%--Jayesh_Sagar added below working code to display project manager profile picture ,,project manager name and telephone on projectdetail.aspx page 21nov2017--%>
                                    <%--  <div class="projectdetails_image">
                                         <asp:Label ID="pms" runat="server" Text="<h3>Project Manager</h3>"></asp:Label>
                                          
                              <img id="imgprofile1" runat="server" src='<%#getuserimage(Eval("projectimage")) %>' style="width:150px;height:150px" visible="true" />
										 <div class="managername">--%>
                               <%--        <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.projectmanager")%>'></asp:Label><br/>
                                              <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.telephoneno")%>' ></asp:Label>--%>
										  <%--<h4 class="promanagername"><%# DataBinder.Eval(Container, "DataItem.projectmanager")%></h4>
                                          <h4 class="promanagertelephone"><span></span><%# DataBinder.Eval(Container, "DataItem.telephoneno")%></h4>

										  </div>
                                       </div>
								--%>
                                   
                                  
                                    <%--Jayesh_Sagar added above working code to display project manager profile picture,project manager name and telephone on projectdetail.aspx page 21nov2017--%>
  </div>
                </ItemTemplate>
                </asp:Repeater>
            <asp:Repeater ID="Repeater1" runat="server" Visible="false">
                <ItemTemplate>
                    <div class="projectdetails_image">
                <!--<asp:Label ID="pms1" runat="server" Text="<h3>Project Manager</h3>"></asp:Label>-->
				 <h4 class="promanagertelephone1"><p style="text-align:center"><%# DataBinder.Eval(Container, "DataItem.designation")%></p></h4>
                                          
                              <img id="imgprofile2" runat="server" src='<%#getuserimage(Eval("projectimage")) %>' style="width:150px;height:150px" visible="true" />
										 <div class="managername" style="text-align:center">
                               <%--        <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.projectmanager")%>'></asp:Label><br/>
                                              <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.telephoneno")%>' ></asp:Label>--%>
										  <h4 class="promanagername1" style="text-align:center"><%# DataBinder.Eval(Container, "DataItem.projectmanager")%></h4>
                                          <h4 class="promanagertelephone1" style="text-align:center"><span></span><%# DataBinder.Eval(Container, "DataItem.telephoneno")%></h4>
										  
                                             </div>
                                       </div>

</ItemTemplate>
            </asp:Repeater>
             <img id="imgprofiles1" runat="server" src="http://localhost/hrmsadmin/images/project/noimage.png" style="width:150px;height:150px" visible="false"/>
            <%--Jayesh_Sagar  below code to display project images on projectdetail.aspx page 21nov2017--%>          
            <%--<asp:Panel ID="pnlimg" runat="server">
			
                  
				   
				   <div class="slidercouraselnew">
				   <span class="scopework12">Progress Photograph:</span>
				    <div class="bx-wrappernew" style="max-width: 1240px; margin: 0px auto;">
	<div class="bx-viewport" style="width: 100%; overflow: hidden; position: relative; height: 186px;">
	<div id="carousel" class="slider2" style="width: 715%; position: relative; transition-duration: 0s; transform: translate3d(-1077px, 0px, 0px);">
             <asp:Repeater ID="rptprojectimages" runat="server">           
                                     <ItemTemplate>
									
		
		<a rel="lightbox" title='<%# DataBinder.Eval(Container, "DataItem.title") %>' id="MainContent_homebanner_msviewprod_rptfamilyproduct_Hlinkimg_9" class="recentlyaddproduct bx-clone" href='<%=ConfigurationManager.AppSettings["sitepathadmin"]%>images/moviebanner/<%# Eval("imagename") %>' style="float: left; list-style: outside none none; position: relative; width: 205.4px; margin-right: 10px;">
                        <div class="slidenew">
                            <div class="view-first">
                                <div class="view-content">
                                 
                                    <div class="imagenew">
<img id="thumb_37" src='<%=ConfigurationManager.AppSettings["sitepathadmin"]%>images/moviebanner/<%# Eval("imagename") %>' width="200" height="150">      
                                        
                                         <asp:Label ID="lblad" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.title") %>' Visible="false"></asp:Label>           
                                    </div>
                                    
                                </div>
                            </div>
                        </div>
                            </a>
 </ItemTemplate>
 </asp:Repeater>
 
</div>
 </div>
 </div>
 </div>
</asp:Panel>--%>
 </div>
			<div class="btngback">
                <a href='<%=ReturnUrl("hccurlmain")%>/completed.aspx' >View All</a>
				</div>
                              
       <%--Jayesh_Sagar  added Above  new code  for multiple images tobe displayed on projectdetail page 24nov2017--%>
            </div>
            </div>
<div  class="content-top-cfc">
      
      <div class="faq-box-container-cfc">
      <div style="text-align:right;display:none;">
       <asp:LinkButton  ID="lbtnback" runat="server" OnClick="lbtnback_Click"  CausesValidation="False" >Back</asp:LinkButton>
       </div>
<fieldset>             
 <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
          <tr>           
            <td valign="top">
              <table cellpadding="0" cellspacing="0" width="100%">
              <tr><td class="productbg" valign="top">
               <asp:Repeater ID="rptrprojectdetail1" runat="server">
         <ItemTemplate>
     <table width="98%" cellpadding="0" cellspacing="0" runat="server" id="tbl">    
        <tr>
        <td width="8"></td>
        <td align="left">
         <font class="ineercontent"><strong>
         </strong> 
          </font>                                                   
        </td>
         </tr>
        <tr>
        <td width="8"></td>
        <td align="left"><br />                      
        </td>        
        </tr>
         <tr><td width="8"></td><td height="10px">&nbsp;</td></tr>
        <tr align="right"><td width="8"></td>
        <td align ="right">
       
        </td></tr>
     </table>
     <br />
     </ItemTemplate>
        </asp:Repeater>
    </td></tr>
              </table>
		</td>
          </tr>
        </table>
      </fieldset></div></div>
	  <%--<link rel="stylesheet" href="http://localhost/hrms/lightbox/lightbox.css" type="text/css" media="screen" />
	<script type="text/javascript" src="http://localhost/hrms/lightbox/lightbox.js"></script>--%>
	  <script src="http://localhost/hrms/CSS/creative1.0/js/bxslider/jquery.bxslider.js"></script> 
	<script src="http://localhost/hrms/lightbox/lightbox-plus-jquery.min.js"></script>
   <link rel="stylesheet" href="http://localhost/hrms/lightbox/lightbox.min.css">
   
   
   
<script type="text/javascript">
    jQuery(document).ready(function () {
        var screenwidth = jQuery(window).width();
        if (screenwidth < 481)
        {
            jQuery('.slider2').bxSlider({
                pager: false,
                slideWidth: 250,
                minSlides: 1,
                maxSlides: 1,
                slideMargin: 20,
                adaptiveHeight: true,
                captions: true
            });
        }
        if (screenwidth > 481 && screenwidth < 781) {
            jQuery('.slider2').bxSlider({
                pager: false,
                slideWidth: 250,
                minSlides: 3,
                maxSlides: 3,
                slideMargin: 20,
                adaptiveHeight: true,
                captions: true
            });
        }
        if (screenwidth > 1000) {
            jQuery('.slider2').bxSlider({
                pager: false,
                slideWidth: 240,
                minSlides: 4,
                maxSlides: 4,
                slideMargin: 10,
                adaptiveHeight: true,
                captions: true
            });
        }
        var name = document.getElementById("lblid").innerText;
        alert(name);
       
    });




</script>



      
  


</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/cms.master" CodeFile="ps.aspx.cs" Inherits="ps" EnableViewState="true" %>
<%@ Register Src="~/Components/Common/psglobal.ascx" TagName="psglobal" TagPrefix="ucps" %>
<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="<%=ReturnUrl("css") %>includes/mywall.css" rel="stylesheet" type="text/css"  />
    <link href="<%=ReturnUrl("css") %>calendar/calendar.css" rel="stylesheet" type="text/css"  />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

  <style>
  body{ background: #ddd; margin: 0; padding: 0; }
  #container{ width: 950px; margin: 0 auto; background: #fff; padding: 200px 25px; }
  table{ border-collapse: collapse; width: 100% }
  td{ border: 0px solid #eee; padding: 10px;width: 200px;  }        
  @media only screen and (max-width:600px) and (min-width:480px){
   #container{ width: auto; margin: 0; padding: 25px 0; }
   table { display: block; }
   td{ display: block; padding: 5px 0; border: none; }
   td img{ display: block; margin: 0; width: 100%; max-width: none; }
  }
  @media screen and (max-width: 480px){
   #container{ width: auto; margin: 0; padding: 25px 0; }
   table { display: block; }
   td{ display: block; padding: 5px 0; border: none; }
   td img{ display: block; margin: 0; width: 100%; max-width: none; }
  }
  img{ max-width: 100%; height: auto; width: auto\\9; /* ie8 */ }
 </style>


  <div class="mainpostwallcat">
  <div class="comments-summery1">
      <asp:Panel ID="pnlsearch" runat="server" Visible="true" DefaultButton="lnksearch">
      <div class="userposts">
           <span> <asp:Label ID="lblcatname"  runat="server"></asp:Label>
                        </span>
          <span class="searchposts">
   <asp:TextBox ID="txtsearch" placeholder="Search"  Visible="false" runat="server" CssClass="txtbox" ToolTip="Enter post title to search post."></asp:TextBox>
                <asp:TextBox ID="txtsdate" runat="server" ReadOnly="true" CssClass="txtbox1" placeholder="Start-Date"  Visible="false"></asp:TextBox>  
              <asp:TextBox ID="txtedate" runat="server" ReadOnly="true"  CssClass="txtbox1" placeholder="End-Date" Visible="false"></asp:TextBox>
<asp:LinkButton ID="lnksearch" Visible="false" runat="server" OnClick="lnksearch_Click" CssClass="searchpostbtn" ToolTip="Search Post" ValidationGroup="valgrp"><i class="fa fa-search"></i></asp:LinkButton>
              <asp:LinkButton ID="lnkreset" visible="false" runat="server" OnClick="lnkreset_Click" CssClass="searchpostbtn" ToolTip="Reset Search"><i class="fa fa-undo" ></i></asp:LinkButton>
              <asp:RegularExpressionValidator ID="regsearch" runat="server" ErrorMessage="Special characters are not allowed" CssClass="formerror" ControlToValidate="txtsearch" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^[a-zA-Z0-9 ]+$" ValidationGroup="valgrp"></asp:RegularExpressionValidator>
               <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Special characters are not allowed. Except slash" CssClass="formerror" ControlToValidate="txtsdate" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^[a-zA-Z0-9 /]+$" ValidationGroup="valgrp"></asp:RegularExpressionValidator>
               <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Special characters are not allowed. Except slash" CssClass="formerror" ControlToValidate="txtedate" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^[a-zA-Z0-9 /]+$" ValidationGroup="valgrp"></asp:RegularExpressionValidator>
          </span>
             </div>
             <span>
                    <a id="gobackbtn"  runat="server" class="aaa"  ></a>
                </span>  

      </asp:Panel>
      <asp:Panel ID="pnlpst" runat="server" Visible="true">
    <div class="comments-summerytitle">
        <div class="commentsdiv mywall">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                      <asp:DataList ID="rptwallTemp" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal" RepeatColumns="3" OnItemDataBound="rptwallTemp_ItemDataBound">
                        <ItemTemplate>       
                                   <div id="img1" class="user-comment" runat="server">
                                      <%--  <a href='<%#productUrlrewriting2(Eval ("cattype"),Eval("productname"), Eval("productid")) %>'>
                                            <span class="wallimg" style="min-width: 150px !important;max-width: 150px !important" ><%#getproductimage(Eval("bigimage")) %> </span></a>
                           --%>    


                                        <div id="pnlimg1" runat="server">
						                            <div class="slidercouraselnew">						
						                            <div class="bx-wrappernew" style="max-width: 1240px; margin: 0px auto;">
						                        	    <div class="bx-viewport" style="width: 100%; overflow: hidden; position: relative; height: 186px;">
								                            <div id="carousel" class="slider2" style="width: 715%; position: relative; transition-duration: 0s; transform: translate3d(-1077px, 0px, 0px);">
                                                                
                                                                <asp:Repeater ID="rptprojectimages1"  runat="server"><%--OnItemDataBound="Repeater1_ItemDataBound"--%>
                                                                    <ItemTemplate>                                                                       
                                                                         <a  class="example-image-link" href='<%=ConfigurationManager.AppSettings["sitepathadmin"]%>images/bigproduct/<%# Eval("bigimage") %>' data-lightbox='example-set[<%# DataBinder.Eval(Container, "DataItem.productname")%>]' data-title='<%# DataBinder.Eval(Container, "DataItem.bigimage") %>'>
                                                                              <div class="slidenew">
                                                                                <div class="view-first">
                                                                                    <div class="view-content">
                                                                                        <div class="imagenew">                                                                                            
                                                                                            <span class="wallimg" style="min-width: 150px !important;max-width: 150px !important" ><%#getproductimage(Eval("bigimage")) %> </span>
                                                                                            <asp:Label ID="lblad" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.bigimage") %>' Visible="false"></asp:Label>
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
					                           </div>
                                       <div id="Panel4" runat="server">
                                               <a href='<%#productUrlrewriting2(Eval ("cattype"),Eval("productname"), Eval("productid")) %>'>
                                            <span class="wallimg" style="min-width: 150px !important;max-width: 150px !important" ><%#getproductimage(Eval("bigimage")) %> </span></a>
                                         </div>
                                
                                   </div>
                                   <div id="pnlvideo" class="user-comment" runat="server">
                                            <span class="wallvideo"> <%#getpostvideo(Eval("videoembed"), Eval("movietrailorcode")) %> </span>
                                    </div>
                            
                                   <div class="userinfo" >
                                 <div class="user-name" runat="server" id="uInfo">
                                        <a id="A2" runat="server" href='<%#productUrlrewriting2( Eval("cattype"),Eval("productname"), Eval("productid")) %>'>
                                         <%# DataBinder.Eval(Container, "DataItem.productname") %></a>                                                                  </div>
                                 <div class="user-name-doc" runat="server" id="DocName">
                                    <a id="A1" runat="server"  href='<%#getFileUrl(Eval("filename")) %>'>               
                                            <%# DataBinder.Eval(Container, "DataItem.productname") %></a>
                                 </div>
                                </div>
                              <div >
                                   <div class="user-comment" runat="server" id="ucomm">
                        <span class="doclink">
                                 <a href='<%#productUrlrewriting( Eval("productname"), Eval("productid")) %>'>
                                  <%#getdocext(Eval("filename")) %> &nbsp;<span class="docfilename"><%#getpostdoc(Eval("filename")) %>
                            </span>                                                                                                                                         
                     </a>
</span>
         </div>
                                   <div class="user-comment-doc" runat="server" id="DocDesc">
                    <%#getshortdesc(Eval("shortdescription")) %> <br />
         </div>
                             </div>

                            </ItemTemplate>
                     </asp:DataList>

                    <asp:Repeater ID="rptwall" runat="server" OnItemCommand="rptwall_ItemCommand" Visible="true" >
                        <ItemTemplate>
                            <asp:UpdatePanel ID="updreview" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                     <div class="commentsbyuser slideInLeft animated">
                                      <!--
                                          <div class="comments-user-image">                                           
                                            <a id="lnkimage" runat="server" target="_blank" href='<%#getuser(Eval("indexid")) %>'>
                                                <img id="imgprofile" runat="server" width="70" height="70" alt="" 
                                                    src='<%#getuserimage(Eval("profilephoto")) %>' />
                                            </a>                                            
                                        </div> -->
                                        <%-- sagar below panel for diffrent disaplay images and,video and events starts here 22dec2017--%>
                                            <asp:Panel ID="Panel1" runat="server" Visible="true">

                                        <div class="userinfo">
                                            <div class="user-name">
                                                <a id="lnkusername" runat="server" href='<%#productUrlrewriting2( Eval("cattype"),Eval("productname"), Eval("productid")) %>'><br />
                                                    <%# DataBinder.Eval(Container, "DataItem.productname") %></a>
													
                                                <%--Sony commented this lines to HIDE posted by--%>
                                                <%-- posted by <a id="LinkButton1" runat="server" href='<%#getuser(Eval("indexid")) %>'><%# DataBinder.Eval(Container, "DataItem.fullname") %></a><br />
                                                <asp:Label ID="lbldate" runat="server" Text='<%#getdate(Eval("createdon")) %>'></asp:Label>--%>
                                            </div>
                                        </div>
                                        <asp:Panel id="img1" runat="server" Visible="false">
                                        <div class="user-comment">
                                           <a href='<%#productUrlrewriting2(Eval ("cattype"),Eval("productname"), Eval("productid")) %>'>
                                               <span class="wallimg"><%#getproductimage(Eval("bigimage")) %> </span></a>
                                        </div>
                                        </asp:Panel>

                                        <asp:Panel ID="pnlvideo" runat="server" Visible="false">
                                        <div class="user-comment">
                                               <span class="wallvideo"> <%#getpostvideo(Eval("videoembed"), Eval("movietrailorcode")) %> </span>
                                        </div>
                                        </asp:Panel>
                                        <asp:Panel id="pstdoc" runat="server" Visible="false">
                                           
                                             

                                        <div class="user-comment" >
                                            <span class="doclink"><a href='<%#productUrlrewriting( Eval("productname"), Eval("productid")) %>'>
                                                <%#getdocext(Eval("filename")) %> &nbsp;<span class="docfilename"><%#getpostdoc(Eval("filename")) %></span>
                                                  
                                                                                        
                                            </a></span>
                                        </div>

                                          
                                        </asp:Panel>
                                        <div class="user-comment">
                                            <%#getshortdesc(Eval("shortdescription")) %>
                                        </div>
                                          </asp:Panel>
                                        <%-- sagar below panel for diffrent disaplay images and,video and events ends here 22dec2017--%>
                                        <%-- sagar below panel for diffrent disaplay document type starts here 22dec2017--%>
                                            <asp:Panel ID="Panel2" runat="server" Visible="false">
                                            <div class="user-name-doc">
<%--sagar commented below line this line link working for pd page 26dec2017 starts here--%>
                                               <%-- <a id="A1" runat="server" href='<%#productUrlrewriting2( Eval("cattype"),Eval("productname"), Eval("productid")) %>'>--%>
     <%--sagar commented above line this line link for pd page 26dec2017 ends here--%>                                                  
       <%--sagar added below line this line link working for file like download link for 26dec2017 starts here--%>                                                
                     <a id="A1" runat="server"  href='<%#getFileUrl(Eval("filename")) %>'>
       <%--sagar added above line this line link working for file like download link for 26dec2017 ends here--%>                                                    

                                                       <br />
                                                    <%# DataBinder.Eval(Container, "DataItem.productname") %></a>
                                                <%--Sony commented this lines to HIDE posted by--%>
                                                <%-- posted by <a id="LinkButton1" runat="server" href='<%#getuser(Eval("indexid")) %>'><%# DataBinder.Eval(Container, "DataItem.fullname") %></a><br />
                                                <asp:Label ID="lbldate" runat="server" Text='<%#getdate(Eval("createdon")) %>'></asp:Label>--%>
                                            </div>
                                            <asp:Panel id="Panel3" runat="server" Visible="true">     
                                       <%--sagar commented below line for hide pdf file icon and his title 26dec2017 starts here--%>  
                                       <%-- <div class="user-comment-doc" >
                                            <span class="doclink"><a href='<%#producturlrewriting( eval("productname"), eval("productid")) %>'>
                                                <%#getdocext(eval("filename")) %> &nbsp;<span class="docfilename"><%#getpostdoc(eval("filename")) %></span>
                                           </a></span>
                                       </div>--%>

                                       <%--sagar commented below line this line link working for file like download link for 26dec2017 starts here--%>                                   
                                        </asp:Panel>
                                                <div class="user-comment-doc">
                                            <%#getshortdesc(Eval("shortdescription")) %>
                                        </div>
                                          </asp:Panel>

                                         <%-- sagar above panel for diffrent disaplay document ends here 22dec2017--%>
                                         <%--Sony ONLY commented the DISPLAY output of the entire Like, Dislike and Comments Module. Note: Actual ASP.Net processing is still happening only div is commented--%>                                        
                                        <!--<div class="writereview" id="MainContent_reviewcomments_writereview">
                                            <asp:Label ID="lblpid" runat="server" Text='<%# Eval("productid") %>' Visible="false"></asp:Label>

                                            <asp:LinkButton ID="lnklike" runat="server" CommandName="cmdlike" CommandArgument='<%# Eval("productid") %>'>
                                                <asp:Label ID="lbllike" runat="server"></asp:Label>
                                                <asp:Label ID="lbllikecount" runat="server"></asp:Label>
                                                <asp:Label ID="lblcnt" runat="server" Visible="false"></asp:Label>
                                            </asp:LinkButton>&nbsp;&nbsp;&nbsp;
                                            <asp:LinkButton ID="lnkdislike" runat="server" CommandName="cmddislike" CommandArgument='<%# Eval("productid") %>'>
                                                <asp:Label ID="lbldislike" runat="server"></asp:Label>
                                                <asp:Label ID="lbldislikecount" runat="server"></asp:Label>
                                                <asp:Label ID="lbldcnt" runat="server" Visible="false"></asp:Label>
                                            </asp:LinkButton>&nbsp;&nbsp;&nbsp;
                      <asp:LinkButton ID="lnkcomm" runat="server" CommandArgument='<%# Eval("productid")%>' PostBackUrl='<%#productUrlrewriting2( Eval("cattype"),Eval("productname"), Eval("productid")) %>'> </asp:LinkButton>
                                        </div>-->

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
        <div id="lastPostsLoader" style="text-align: center; margin: 20px 0px 0px;"></div>
        <div id="desc" runat="server" visible="false"></div>
        <div class="grid-pager">
            <asp:LinkButton ID="lnkprev" runat="server" CssClass="searchpostbtn" OnClick="lnkprev_Click"><<</asp:LinkButton> 
            <asp:Repeater ID="rptPager" runat="server">
                <ItemTemplate>
                       <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                        CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'
                                        OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                </ItemTemplate>
          </asp:Repeater>
          <asp:LinkButton ID="lnknxt" runat="server" CssClass="searchpostbtn" OnClick="lnknxt_Click">>></asp:LinkButton>
    </div>

    </div>
          </asp:Panel>
       <div class="userpostscats">
           <asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>
                        
             </div>
 </div>
 </div>
    
  <script src="http://localhost/hrms/CSS/creative1.0/js/bxslider/jquery.bxslider.js"></script> 
	<script src="http://localhost/hrms/lightbox/lightbox-plus-jquery.min.js"></script>
    <link rel="stylesheet" href="http://localhost/hrms/lightbox/lightbox.min.css"/>

    <script type="text/javascript">

      
        jQuery(document).ready(function () {
            var screenwidth = jQuery(window).width();
            if (screenwidth < 481) {
                jQuery('.slider2').bxSlider({
                    pager: true,
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
                    pager: true,
                    slideWidth: 250,
                    minSlides: 1,
                    maxSlides: 1,
                    slideMargin: 20,
                    adaptiveHeight: true,
                    captions: true
                });
            }
            if (screenwidth > 1000) {
                jQuery('.slider2').bxSlider({
                    pager: true,
                    slideWidth: 240,
                    minSlides: 1,
                    maxSlides: 1,
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

<%@ Control Language="C#" AutoEventWireup="true" CodeFile="reviewmore.ascx.cs" Inherits="themes_creative1_LayoutControls_pdnew_reviewmore" %>
<script type="text/javascript">
    window.onload = function () {
        pageLoad();
    };
</script>
<div class="comments-summery">
      <div class="comments-summerytitle">
          <div class="moviename fadeInDown">
              <a id="lnkproduct" runat="server"><asp:Label ID="lblproductname" runat="server" Text=""></asp:Label></a>
          </div>
          <div class="dateanddirector fadeInDown"> 
             <asp:Label ID="lblyear" runat="server" Text="Label" CssClass="productyear"></asp:Label>
             <span id="director" runat="server">, Directed by </span>
              <a id="lnkdirector" runat="server">
                  <asp:Label ID="lbldirectorname" runat="server" Text=""></asp:Label>
              </a>
          </div>
     </div>
</div>
<div id="criticcornerdiv" runat="server" class="criticcornerdiv">
	<div class="criticcorner">
        <div class="criticcorner-container">
        	<ul class="criticcorner-grid effect slideInLeft" id="comment-grid">

            <asp:Repeater ID="rptmoviereview" runat="server" OnItemCommand="rptmoviereview_ItemCommand">
               <ItemTemplate>
			    <li>
                      <asp:UpdatePanel ID="UpdatePanel1" runat="server"> 
                        <ContentTemplate>                     
                    <div class="user-image">
                        <asp:Image ID="imgprofile" runat="Server" class="reviewimgs" /></div>
                   	<div class="userinfo">
                    	<div class="user-name">
                        <asp:LinkButton ID="lnkusername" runat="server" CommandName="username" ForeColor="Black" OnClientClick="window.document.forms[0].target='_blank';" CommandArgument='<%# Eval("username") %>'>
                        <%# Eval("fullname")%>
                        </asp:LinkButton>
                        </div>
                        <div class="user-rating">
                        <%--<asp:Literal ID="ltrjs" runat="server"></asp:Literal>--%>
                            <asp:HiddenField ID="hdvalue" runat="server" Value='<%# Eval("ratingvalue") %>' ViewStateMode="Inherit" />
                        <asp:Label ID="lblratval" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ratingvalue") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lblrevid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.reviewid") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lbluseremail" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.username") %>' Visible="false"></asp:Label>
                        <div class="ratingstar" style="padding:0px;margin:0px;">
                                        <ajaxToolkit:Rating ID="Rating1" runat="server" BehaviorID="Ratingproduct" MaxRating="5"
                                            StarCssClass="ratingStar" WaitingStarCssClass="savedRatingStar" FilledStarCssClass="filledRatingStar"
                                            EmptyStarCssClass="emptyRatingStar" ReadOnly="true">
                                        </ajaxToolkit:Rating>
                                      
                        </div>
                        </div>
                        <div class="user-follow">
                        	<asp:LinkButton ID="lnkfollow" CommandName="follow" runat="server" CommandArgument='<%# Eval("username") %>'></asp:LinkButton>
                        </div>

                   	</div>
                    <div class="userinfo1">
                    	<div class="post-date">
                        <asp:Label ID="lbldate" runat="server" Text='<%#Eval("reviewdate","{0:dd-MMM-yyyy}") %>'></asp:Label>
                        </div>
                        <div class="post-comments">
                        <asp:LinkButton ID="lnkcomm" runat="server" CommandName="cmdcomment" CommandArgument='<%# Eval("reviewid") %>' ForeColor="Black">
                        </asp:LinkButton>
                        <asp:Label ID="lblcommentcount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.commentcount") %>' Visible="false"></asp:Label>
                        </div>
                        <div class="post-likes">

                        <asp:LinkButton ID="lnklike" runat="server" CommandName="cmdlike" CommandArgument='<%# Eval("reviewid") %>' ForeColor="Black">
                        </asp:LinkButton>

                       
                        <asp:Label ID="lblcnt" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.likecount") %>' Visible="false"></asp:Label>
                       
                        </div>
                    </div>
                    <div class="user-comment">
                    	<asp:Label ID="lblreviews" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.reviewtext") %>'></asp:Label>
                        <asp:LinkButton ID="read" runat="server" ForeColor="#88B7E6" Text="Read More" Visible="false">
                        </asp:LinkButton>
                    </div>
                     </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="lnklike" EventName="Click" />
			    <asp:AsyncPostBackTrigger ControlID="lnkfollow" EventName="Click" />
                        </Triggers>
                         </asp:UpdatePanel>                           
              </li>
                             
    	</ItemTemplate>
      </asp:Repeater>
		  	</ul>
        </div>
	</div>
</div>

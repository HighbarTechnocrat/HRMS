<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="completed.aspx.cs" Inherits="completed" %>--%>

<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ongoing.aspx.cs" Inherits="ongoing" %>--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/cms.master" AutoEventWireup="true" CodeFile="newslettersnew.aspx.cs" Inherits="newslettersnew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <link href="<%=ReturnUrl("css") %>profile/mprofile.css" rel="stylesheet" type="text/css" />
    <div class="mainpostwallcat">
        <div class="comments-summery1">
            <div class="userposts"><span> Rajasthan Atomic Power Plant  </span> </div>
            <span> <a id="gobackbtn" href='<%=ReturnUrl("compeletedprojectHome")%>'  class="aaa" >Projects</a> </span>
                    <br /><br /> 
   
		</div>
            <asp:Panel ID="pnlproject" runat="server">
              
             
                        <div class="commentsbyuser slideInLeft animated innerbd_wallnew">
                            <div class="userinfo">
                                <div class="user-name">
                                    <a id="lnkusernameew" href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=V_vtC1fFvsOUBOoa-xh2yA=='>									
                                      Rajasthan Atomic Power Plant - Units 3 & 4
                                    </a>
                                    <br />
                                
                                </div>
                                <div class="user-rating">
                                </div>
								<div class="user-name">
                                    <a id="lnkusernameew" href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=Jendib1mrSjpTjXz_Uw1eQ=='>									
                                      Rajasthan Atomic Power Plant - Units 5 & 6
                                    </a>
                                    <br />
                                
                                </div>
                                <div class="user-rating">
                                </div>
								<div class="user-name">
                                    <a id="lnkusernameew" href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=MtZwD4FoM9AVtYHhva8iKQ=='>									
                                      Rajasthan Atomic Power Plant - Units 7 & 8
                                    </a>
                                    <br />
                                
                                </div>
                                <div class="user-rating">
                                </div>
                            
                            </div>

                        </div>
                  
              
               
            </asp:Panel>
           
        </div>
		
    </div>
</asp:Content>






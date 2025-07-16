<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="completed.aspx.cs" Inherits="completed" %>--%>

<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ongoing.aspx.cs" Inherits="ongoing" %>--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/cms.master" AutoEventWireup="true" CodeFile="newslettersnew.aspx.cs" Inherits="newslettersnew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <link href="<%=ReturnUrl("css") %>profile/mprofile.css" rel="stylesheet" type="text/css" />
    <div class="mainpostwallcat">
        <div class="comments-summery1">
            <div class="userposts"><span>  Pune Metro Rail Project  </span> </div>
            <span> <a id="gobackbtn" href='<%=ReturnUrl("compeletedprojectHome")%>'  class="aaa" >Projects</a> </span>
                    <br /><br /> 
   
		</div>
            <asp:Panel ID="pnlproject" runat="server">
              
             
                        <div class="commentsbyuser slideInLeft animated innerbd_wallnew">
                            <div class="userinfo">
                                <div class="user-name">
                                    <a id="lnkusernameew" href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=hh0nQybvWzWITccFG_k7HQ=='>									
                                      Pune Metro Rail Project
                                    </a>
                                    <br />
                                
                                </div>
                                <div class="user-rating">
                                </div>
								<div class="user-name">
                                    <%--<a id="lnkusernameew" href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=kY1KG9MQ44HxNCBznoIM0w=='>--%>		
                                    <a id="lnkusernameew" href="#">
                                      Pune Metro Rail Project (2nd Order)
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






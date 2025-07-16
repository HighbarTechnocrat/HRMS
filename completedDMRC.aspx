<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="completed.aspx.cs" Inherits="completed" %>--%>

<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ongoing.aspx.cs" Inherits="ongoing" %>--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/cms.master" AutoEventWireup="true" CodeFile="newslettersnew.aspx.cs" Inherits="newslettersnew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <link href="<%=ReturnUrl("css") %>profile/mprofile.css" rel="stylesheet" type="text/css" />
    <div class="mainpostwallcat">
        <div class="comments-summery1">
            <div class="userposts"><span> DMRC  </span> 
                <span> <a id="gobackbtn" href='<%=ReturnUrl("compeletedprojectHome")%>'  class="aaa" >Projects</a> </span>
                    <br /><br /> 
            </div>

   
		</div>
            <asp:Panel ID="pnlproject" runat="server">
              
             
                        <div class="commentsbyuser slideInLeft animated innerbd_wallnew">
                            <div class="userinfo">
                                <div class="user-name">
                                    <a id="lnkusernameew" href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=oVX4PJhFx3id7M5KlnUj0g=='>									
                                       DMRC - MC1A
                                    </a>
                                    <br />
                                
                                </div>
                                <div class="user-rating">
                                </div>
								<div class="user-name">
                                    <a id="lnkusernameew" href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=WXEBCjYpdB241hqFpgxCyA=='>									
                                       DMRC AMEL C1
                                    </a>
                                    <br />
                                </div>
                                <div class="user-rating">
                                </div>
                            
								<div class="user-name">
                                    <a id="lnkusernameew" href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=K6MuB70S58escH1vBceYzg=='>									
                                       DMRC AMEL C6
                                    </a>
                                    <br />
                                </div>
                                <div class="user-rating">
                                </div>
								
								<div class="user-name">
                                    <a id="lnkusernameew" href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=-uO2eAwAI_nkTt6J-n5TLg=='>									
                                       DMRC CC30 PACKAGE
                                    </a>
                                    <br />
                                </div>
                                <div class="user-rating">
                                </div>
								
								<div class="user-name">
                                    <a id="lnkusernameew" href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=_ZkX5_FwOEAp4XC3EqGYkQ=='>									
                                       DMRC CC34 PACKAGE
                                    </a>
                                    <br />
                                </div>
                                <div class="user-rating">
                                </div>
								
								<div class="user-name">
                                    <a id="lnkusernameew" href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=J_yVyNXZRqiZSDuFDu3yvQ=='>									
                                       DMRC CC66 PACKAGE
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






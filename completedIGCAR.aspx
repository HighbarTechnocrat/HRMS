<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="completed.aspx.cs" Inherits="completed" %>--%>

<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ongoing.aspx.cs" Inherits="ongoing" %>--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/cms.master" AutoEventWireup="true" CodeFile="newslettersnew.aspx.cs" Inherits="newslettersnew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <link href="<%=ReturnUrl("css") %>profile/mprofile.css" rel="stylesheet" type="text/css" />
    <div class="mainpostwallcat">
        <div class="comments-summery1">
            <div class="userposts"><span> IGCAR  </span> </div>

   
		</div>
            <asp:Panel ID="pnlproject" runat="server">
              
             
                        <div class="commentsbyuser slideInLeft animated innerbd_wallnew">
                            <div class="userinfo">
                                <div class="user-name">
                                    <a id="lnkusernameew" href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=sGzfIij6odtqK8h-BLTT2Q=='>									
                                      IGCAR Buildings Project
                                    </a>
                                    <br />
                                
                                </div>
                                <div class="user-rating">
                                </div>
								<div class="user-name">
                                    <a id="lnkusernameew" href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=4aJzbWjiPEXwTONAaqYKVQ=='>									
                                      IGCAR Fast Reactor Fuel Cycle Facility
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






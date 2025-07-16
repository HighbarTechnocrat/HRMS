<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IdeaCentral.aspx.cs" Inherits="IdeaCentral" %>--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/cms.master" AutoEventWireup="true" CodeFile="IdeaCentral.aspx.cs" Inherits="IdeaCentral" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <link href="<%=ReturnUrl("css") %>profile/mprofile.css" rel="stylesheet" type="text/css" />
    <div class="mainpostwallcat">
        <div class="comments-summery1">
            <div class="userposts"><span>Idea Central</span> <br><br>
			   
		</div>
        <asp:Panel ID="pnlIdea" runat="server">
			<div class="commentsbyuserHeader" >
                        <div class="userinfo" >
                            <div class="user-name" >
                            <a id="lnkusernameewHeader" >Title
							</a>
                            <%--<p style="text-aline>By :
                                    <asp:Label ID="lbldate" visible="true" runat="server" Text='<%# Eval("createdby")%>'></asp:Label></p><br>--%>
                            </div>
							
							<div class="user-rating" id="lnkusernameew1Header">
							 Description 
                            <%--# Eval("shortdescription").ToString().Length > 900 ? Eval("shortdescription").ToString().Substring(0, 900)+"..." : Eval("shortdescription")   --%> 
							</div>
							<div class="user-rating" id="lnkusernameew2Header">
							 Posted by 
                            <%--# Eval("shortdescription").ToString().Length > 900 ? Eval("shortdescription").ToString().Substring(0, 900)+"..." : Eval("shortdescription")   --%> 
							</div>
                         </div>
			</div>
            <asp:Repeater ID="rptridea" runat="server">
                <ItemTemplate>
					<div class="commentsbyuserD" >
                        <div class="userinfo" >
                            <div class="user-name" >
                            <a id="lnkusernametw" href='<%#getprojectURL(Eval("productid")) %>'>                                       							
							<%# Eval("productname") %>
							</a>
                            <%--<p style="text-aline>By :
                                    <asp:Label ID="lbldate" visible="true" runat="server" Text='<%# Eval("createdby")%>'></asp:Label></p><br>--%>
                            </div>
							
							<div class="user-rating" id="lnkusernameew1">
							 <%#Eval("shortdescription")   %> 
                            <%--# Eval("shortdescription").ToString().Length > 900 ? Eval("shortdescription").ToString().Substring(0, 900)+"..." : Eval("shortdescription")   --%> 
							</div>
							<div class="user-rating" id="lnkusernameew2">
							 <%#Eval("createdby")   %> 
                            <%--# Eval("shortdescription").ToString().Length > 900 ? Eval("createdby").ToString().Substring(0, 900)+"..." : Eval("shortdescription")   --%> 
							</div>
                         </div>
					</div>
                </ItemTemplate>
            </asp:Repeater>
              
            <div class="grid-pager">                    
                    <asp:Repeater ID="rptPager" runat="server">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'
                                OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:Repeater>            
            </div>
        </asp:Panel>
            <div class="projectlblmsg">
				<asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>               
            </div>
        
    </div>
</asp:Content>





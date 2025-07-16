<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="completed.aspx.cs" Inherits="completed" %>--%>

<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ongoing.aspx.cs" Inherits="ongoing" %>--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/cms.master" AutoEventWireup="true" CodeFile="completed.aspx.cs" Inherits="completed" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <link href="<%=ReturnUrl("css") %>profile/mprofile.css" rel="stylesheet" type="text/css" />
    <div class="mainpostwallcat">
        <div class="comments-summery1">
            <div class="userposts"><span>Projects</span> </div>

             
			<div class="projectlist1">
             
                    <div class="projectlistddl">
                    <asp:DropDownList ID="ddlprojectcat" runat="server" Width="200px" TabIndex="0"> </asp:DropDownList>
                        
                       <%-- <asp:DropDownList ID="projectddl" runat="server" CssClass="msgselect1new" TabIndex="1">
				             <asp:ListItem Text="All"></asp:ListItem>
                             <asp:ListItem Text="Transportation"></asp:ListItem>
                            <asp:ListItem Text="Hydro Power"></asp:ListItem>
                            <asp:ListItem Text="Nuclear & Special Projects"></asp:ListItem>
                            <asp:ListItem Text="Water Soluction"></asp:ListItem>
                        </asp:DropDownList>--%>
                </div>
		        <div class="dropdown2" style="display:inline">
			        <asp:DropDownList ID="ddlprojectstatus" runat="server" Width="200px" TabIndex="0"> </asp:DropDownList>
		        </div>

                <div class="projectlistddl" style="display:inline">
			        <asp:DropDownList ID="ddlprojectStates" runat="server" Width="200px" TabIndex="0"> 
			        </asp:DropDownList>
		        </div>
                <%--<div class="projectstatusddl">
                    <asp:DropDownList ID="ddlprojectstatus" runat="server" Width="100px" TabIndex="0"> </asp:DropDownList>
                    </div>--%>
          
              <div class="project-text-box">              
                  <asp:TextBox ID="txtprjyear" runat="server"  placeholder="Enter year for search" Width="100" MaxLength="4"></asp:TextBox>           
              </div>
                  
            <div class="project-text-box">              
                    <%--<asp:TextBox ID="txtprojectsearch" visible="true"  placeholder="Enter text for search" runat="server"></asp:TextBox>--%>
                    <%--<asp:TextBox ID="txtstarsearch" visible="true"  placeholder="Enter text for search" runat="server" CssClass="txtbox" TabIndex="2"></asp:TextBox>--%>
                    <%--<asp:TextBox ID="txtstarsearch" runat="server" visible="true" placeholder="Enter text for search" CssClass="txtbox" TabIndex="2"></asp:TextBox>--%>
             <asp:TextBox ID="txttitle" runat="server"  placeholder="Enter text for search" Width="100"></asp:TextBox>           
          </div>
          <div class="projectbtn">
            <span class="projectsearch">
                 <asp:LinkButton ID="projectsearchbtn" runat="server" ToolTip="Search Project" OnClick="lnksearch_Click"
                    TabIndex="3" ValidationGroup="valgrp" OnClientClick="projectsearchbtn_Click"><i class="fa fa-search" ></i></asp:LinkButton></span>
            <span class="projectsearch">
                <asp:LinkButton ID="projectresetbtn" OnClick="lnkreset_Click" ToolTip="Reset Search" TabIndex="4" runat="server"><i class="fa fa-undo" aria-hidden="true"></i>
                </asp:LinkButton></span>
<%--            <span class="searchalldirectory">
                <asp:TextBox ID="txtallsearch" placeholder="Search By Name or Email-ID" runat="server" CssClass="txtbox" ToolTip="Type Name or Email-id to search contact." TabIndex="3"></asp:TextBox><asp:LinkButton ID="lnkadllcontact" runat="server" CssClass="searchpostbtn" OnClick="lnkadllcontact_Click" TabIndex="4"><i class="fa fa-search"></i></asp:LinkButton></span>--%>

        </div>
		</div>
            <asp:Panel ID="pnlproject" runat="server">
              
                <asp:Repeater ID="rptrproject" runat="server">
                      <ItemTemplate>
                        <div class="commentsbyuser slideInLeft animated innerbd_wallnew">
                            <div class="userinfo">
                                <div class="user-name">
                                    <a id="lnkusernameew" href='<%#getprojectURL(Eval("projectid")) %>'>
									
                                        <%# Eval("projecttitle") %>
                                    </a>
                                    <br />
                                 <%--  Project Date :
                                    <asp:Label ID="lbldate" visible="false" runat="server" Text='<%# Eval("projectcompletedate")%>'></asp:Label>--%>
                                </div>
                                <div class="user-rating">
                                </div>

                               <%-- <div class="user-comment">
                                    <%# Eval("projectdescsmall").ToString().Length > 150 ? Eval("projectdescsmall").ToString().Substring(0, 147)+"..." : Eval("projectdescsmall")   %>
                                </div>--%>
                            </div>

                        </div>
                    </ItemTemplate>
                </asp:Repeater>
               <%-- <asp:Repeater ID="project" runat="server">
                    <asp:Label runat="server" Text="<%#Eval('projecttitle')%>"></asp:Label>
                </asp:Repeater>--%>
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
		<div><asp:Label ID="notemsg" runat="server" style="color:red;" Text="Note: This data is only for limited project." Visible="false"></asp:Label></div>
    </div>
<script type="text/javascript">
    function onCharOnlyNumber(e) {
        var keynum;
        var keychar;
        var numcheck = /[0123456789]/;

        if (window.event) {
            keynum = e.keyCode;
        }
        else if (e.which) {
            keynum = e.which;
        }
        keychar = String.fromCharCode(keynum);
        return numcheck.test(keychar);
    }
</script>
</asp:Content>






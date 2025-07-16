<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="contacts.aspx.cs" Inherits="contacts" %>--%>

<%@ Page Language="C#" MasterPageFile="~/cms.master" AutoEventWireup="true" CodeFile="contacts.aspx.cs" Inherits="contacts" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script>
            (function () { 
                $(document).on("contextmenu", function (e) {
                    return false;
                });
            });


    </script>


    <link href="<%=ReturnUrl("css") %>movie-detail/movie-detail.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>profile/mprofile.css" type="text/css" media="all" />
    <link href="<%=ReturnUrl("css") %>directory/directory.css" rel="stylesheet" type="text/css" media="all" />

    <style>
        .sitelogo {
            left: 0;
            top: 0;
        }

        .productsummerytitle {
            border: none;
            float: none;
        }

        .productname {
            float: none;
            width: auto;
        }
    </style>

    <div id="content-container">
        <asp:Panel ID="Panel1" runat="server" DefaultButton="lnkstarcontact">
            <div class="contactlbl">
<%--                <div class="starred-contacts" style="color: #0366ba;
	font-size: 22px;
	font-weight: 300;
	text-align: left;" >Contacts</div>--%>
                <%--Jayesh commented below working of dropdown and textbox="txtstarsearch" 1dec2017--%>
                <%--<asp:DropDownList ID="ddlserch" visible="true" runat="server"  CssClass="msgselect1" TabIndex="1">
                    <asp:ListItem Text="Name"></asp:ListItem>
                    <asp:ListItem Text="Email-ID"></asp:ListItem>
                    <asp:ListItem Text="Department"></asp:ListItem>
                    <asp:ListItem Text="Designation"></asp:ListItem>
                </asp:DropDownList>
                <span class="searchstardirectory1">
                    <asp:TextBox ID="txtstarsearch" visible="true"  placeholder="Enter text for search" runat="server" CssClass="txtbox" TabIndex="2"></asp:TextBox>
                     <asp:RegularExpressionValidator ID="regsearch" runat="server" ErrorMessage="Special characters are not allowed" CssClass="formerror" ControlToValidate="txtstarsearch" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^[a-zA-Z0-9@. ]+$" ValidationGroup="valgrp"></asp:RegularExpressionValidator>
                </span> --%>
                <%--Jayesh commented above working of dropdown and textbox="txtstarsearch" 1dec2017--%>

                <%--Jayesh added below div class="textboxes" for 4 textboxes 1dec2017--%>
                <div class="textboxes" id="SearchDiv" runat="server" >
                  
                    <asp:TextBox ID="txtnamesearch" visible="true"  placeholder="search by name" runat="server" CssClass="txtbox" ></asp:TextBox>
                
                    <asp:TextBox ID="txtlocation" placeholder="search by location" runat="server" CssClass="txtbox" ></asp:TextBox>
                    <asp:TextBox ID="txtemailidsearch" Visible="false"  placeholder="search by email id" runat="server" CssClass="txtbox" ></asp:TextBox>
                    <asp:TextBox ID="txtdepartmentsearch" visible="true"  placeholder="search by department" runat="server" CssClass="txtbox" ></asp:TextBox>
                    <asp:TextBox ID="txtdesignationsearch" visible="true"  placeholder="search by designation" runat="server" CssClass="txtbox" ></asp:TextBox>
                    <asp:TextBox ID="txtcompanysearch" visible="false"  placeholder="search by company" runat="server" CssClass="txtbox" ></asp:TextBox>
					 <div class="btnsearchreset" >
                 <span class="directorysearch">
                    <asp:LinkButton ID="lnkstarcontact" runat="server" OnClick="lnkstarcontact_Click" ToolTip="Search Contact" CssClass="userdirectory-searchbtn" TabIndex="3" ValidationGroup="valgrp"><i class="fa fa-search" ></i></asp:LinkButton></span>
                <span class="directoryreset">
                    <asp:LinkButton ID="lnkreset" runat="server" OnClick="lnkreset_Click" CssClass="userdirectory-searchbtn" ToolTip="Reset Search" TabIndex="4"><i class="fa fa-undo" aria-hidden="true"></i>
                    </asp:LinkButton></span>
              </div>
                </div>
                <%--Jayesh added above div class="textboxes" for 4 textboxes 1dec2017--%>
             
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlstar" runat="server" DefaultButton="lnkstarcontact" Visible="false" >

            <div id="content">
                <div class="starred-contacts" ><%--<i class="fa fa-star" aria-hidden="true"></i>--%>Starred Contacts</div>
                <div id="buddypress">
                    <div id="item-body" role="main">
                        <div class="intern-padding">
                            <div class="contact-members-starred">
                                <asp:Panel ID="pnlstar1" runat="server" >
                                    <asp:Repeater ID="rptstar" runat="server" OnItemCommand="rptstar_ItemCommand">
                                        <ItemTemplate>
                                            
                                            <div class="contactimage">
                                                <asp:LinkButton ID="lnkusername" runat="server" CommandArgument='<%# Eval("indexid") %>'>
                                                    <div class="contactimgprofile">
                                                        <img runat="server" visible="true" id="imgprofile" class="contactimg" src='<%#getuserimage(Eval("profilephoto")) %>' /></div>
                                                    <div class="contactfieldsname"><span class="contactfileds1"><span class="contactfileds2"><%# Eval("fullname") %></span></span></div>
                                                </asp:LinkButton>
                                                <div class="contactfieldsname5"><span class="contactfileds1"><span class="contactfileds3"><%# Eval("department").ToString().Length >0 ? Eval("department") : "-"%></span></span></div>
                                                <div class="mail">
                                                    <span class="mail1"><span class="mail2">
                                                         <%--prajyot added below code 25Dec2017--%>
                                                       <%-- <asp:LinkButton ID="lnkemail" runat="server" CommandName="sentmsg" CommandArgument='<%# Eval("username")%>' ToolTip="Click here to send an message">--%>
                                                      <span class="emailbtn1"><i class="fa fa-envelope" aria-hidden="true"></i><%#Eval("username").ToString().Length >25 ? Eval("username").ToString().Substring(0, 24)+"..." : Eval("username") %></span>
                                                        <%--</asp:LinkButton>--%>
                                                         <%--prajyot added above code 21nov2017--%>
                                                    </span></span>
                                                </div>
                                                
                                                <div class="contactfieldsphone" style="display:none"><span class="contactphone1"><span class="contactphone3"><i class="fa fa-phone" aria-hidden="true"></i><%# Eval("mobileno") %></span></span></div>
                                                 
                                            <div class="follow" style="display:none">
                                                    <asp:UpdatePanel ID="updfollowstar" runat="server" UpdateMode="Always">
                                                        <ContentTemplate>
                                                            <asp:LinkButton ID="lnkfollow1" runat="server" CommandName="follow1" CommandArgument='<%# Eval("username")%>' Visible="False">
                                                                <asp:Label ID="followbtn1" runat="server" CssClass="follow" Visible="False"></asp:Label>
                                                            </asp:LinkButton>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="lnkfollow1" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div> 
                                                   
                                                <div class="starred">
                                                    <asp:LinkButton ID="lnkdelete" runat="server" CommandName="deletestar"
                                                        CommandArgument='<%# Eval("username")%>' ToolTip="Click here to remove this contact form Starred List" Visible="True">
                                      <span class="starbtn1"><i class="fa fa-star" aria-hidden="true"></i></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                </asp:Panel>
                                <asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>
                                <div class="grid-pager">
                                    <asp:LinkButton ID="lnkprev" runat="server" CssClass="searchpostbtn" OnClick="lnkprev_Click" ToolTip="Previous" Visible="false"><<</asp:LinkButton>
                                    <asp:Repeater ID="rptPager1" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPage1" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'
                                                OnClick="lnkPage1_Click" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:LinkButton ID="lnknxt" runat="server" CssClass="searchpostbtn" OnClick="lnknxt_Click" ToolTip="Next" Visible="false">>></asp:LinkButton>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </asp:Panel>
                                                  
        <div class="contactlbl" id="divallcntc" runat="server" visible="false">
            <span>All Contact</span>
            <span class="searchalldirectory">
                <asp:TextBox ID="txtallsearch" placeholder="Search By Name or Email-ID" runat="server" CssClass="txtbox" ToolTip="Type Name or Email-id to search contact." TabIndex="3"></asp:TextBox><asp:LinkButton ID="lnkadllcontact" runat="server" CssClass="searchpostbtn" OnClick="lnkadllcontact_Click" TabIndex="4"><i class="fa fa-search"></i></asp:LinkButton></span>
        </div>
        <asp:Panel ID="pnlallcntc" runat="server" DefaultButton="lnkstarcontact">
            <div id="content2">
                <div class="starred-contacts"><%--<i class="fa fa-user" aria-hidden="true"></i>--%>Contacts</div>
                <div id="buddypress2">
                    <div id="item-body2" role="main">
                        <div class="intern-padding">
                            <div class="contact-members-starred">
                                <asp:Panel ID="pnlallcntc1" runat="server">
                                    <asp:Repeater ID="rptall" runat="server" OnItemCommand="rptall_ItemCommand">
                                        <ItemTemplate>
                                            <div class="contactimage">
                                                <asp:LinkButton ID="lnkusername" runat="server" CommandArgument='<%# Eval("indexid") %>'>
                                                    <div class="contactimgprofile" style="visibility:hidden">
                                                        <img runat="server" visible="true" id="imgprofile1" class="contactimg" src='<%#getuserimage(Eval("profilephoto")) %>' /></div>
                                                    <div class="contactfieldsname"><span class="contactfileds1"><span class="contactfileds2"><%# Eval("fullname") %></span></span></div>
                                                </asp:LinkButton>
                                                 <div class="contactfieldsname5"><span class="contactfileds1"><span class="contactfileds3"><%# Eval("department").ToString().Length >0 ? Eval("department") : "-"%></span></span></div>
                                                <div class="contactfieldsdesg"><span class="contactfileds1"><span class="contactfileds3"><%# Eval("designation").ToString().Length >0 ? Eval("designation") : "-"%></span></span></div>
                                                <div class="mail">
                                                    <span class="mail1"><span class="mail2">
                                                     <%--   <asp:LinkButton ID="lnkemail1" runat="server" CommandName="sentmsg" CommandArgument='<%# Eval("username") %>' ToolTip="Click here to send Message">--%>
                                                        <%--<span class="emailbtn1"><i class="fa fa-envelope" aria-hidden="true"></i><%#Eval("username").ToString().Length >25 ? Eval("username").ToString().Substring(0, 24)+"..." : Eval("username") %></span>   --%>
                                                        
                                                         <span class="emailbtn1"><i class="fa fa-envelope" aria-hidden="true"></i>
                                                             <a href="mailto:<%#Eval("username")%>" style="visibility:visible">
                                                                    <%#Eval("username").ToString().Length >50 ? Eval("username").ToString().Substring(0, 100)+"..." : Eval("username") %>
                                                                 </a> 
                                                         </span>
                                                    
                                                        
                                                        <%--</asp:LinkButton>--%>
                                                    </span></span>
                                                </div>
                                               
                                                <div class="contactfieldsphone" style="display:none"><span class="contactphone1"><span class="contactphone3"><i class="fa fa-phone" aria-hidden="true"></i><%# Eval("mobileno") %></span></span></div>
                                               
                                               <div class="follow" style="display:none">
                                                    <asp:UpdatePanel ID="updfollowall" runat="server" UpdateMode="Always">
                                                        <ContentTemplate>
                                                            <asp:LinkButton ID="lnkfollow" runat="server" CommandName="follow" CommandArgument='<%# Eval("username")%>' Visible="false">
                                                                <asp:Label ID="followbtn" runat="server" CssClass="follow" Visible="false"></asp:Label>
                                                            </asp:LinkButton>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="lnkfollow" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div> 
                                              
                                                <div class="starred" style="display:none">
                                                    <asp:LinkButton ID="lnkadd" runat="server" CommandName="addstar" CommandArgument='<%# Eval("username")%>' ToolTip="Click here to Starred this contact">
                                                    <span class="starbtn1"><i class="fa fa-star-o" aria-hidden="true"></i></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </asp:Panel>
                                <asp:Label ID="lblmsg1" runat="server" Visible="false"></asp:Label>
                                <div class="grid-pager">
                                    <asp:LinkButton ID="lnkprev1" runat="server" CssClass="searchpostbtn" OnClick="lnkprev1_Click" ToolTip="Previous" Visible="false"><<</asp:LinkButton>
                                    <asp:Repeater ID="rptPager" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'
                                                OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:LinkButton ID="lnknxt1" runat="server" CssClass="searchpostbtn" OnClick="lnknxt1_Click" ToolTip="Next" Visible="false">>></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>


        <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchDepartments" MinimumPrefixLength="1"
        CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtdepartmentsearch"
        ID="AutoCompleteExtender1" runat="server" FirstRowSelected="true">
        </ajaxToolkit:AutoCompleteExtender>

            <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchLocations" MinimumPrefixLength="1"
        CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtlocation"
        ID="AutoCompleteExtender2" runat="server" FirstRowSelected="true">
        </ajaxToolkit:AutoCompleteExtender>

            <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchCompaniess" MinimumPrefixLength="1"
        CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtcompanysearch"
        ID="AutoCompleteExtender3" runat="server" FirstRowSelected="true">
        </ajaxToolkit:AutoCompleteExtender>

    </div>
</asp:Content>


<%@ Page Language="C#" MasterPageFile="~/cms.master" AutoEventWireup="true" CodeFile="userdirectory.aspx.cs" Inherits="newsdetail" %>


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
                <div class="starred-contacts" style="color: #0366ba;
	font-size: 22px;
	font-weight: 300;
	text-align: left;"><%--<i class="fa fa-user" aria-hidden="true"></i>--%>User Directory</div>
                <asp:DropDownList ID="ddlserch" visible="true" runat="server"  CssClass="msgselect1" TabIndex="1">
                    <asp:ListItem Text="Name"></asp:ListItem>
                    <asp:ListItem Text="Email-ID"></asp:ListItem>
                    <asp:ListItem Text="Department"></asp:ListItem>
                    <asp:ListItem Text="Designation"></asp:ListItem>
                </asp:DropDownList>
                <span class="searchstardirectory1">
                    <asp:TextBox ID="txtstarsearch" visible="true"  placeholder="Enter text for search" runat="server" CssClass="txtbox" TabIndex="2"></asp:TextBox>
                     <asp:RegularExpressionValidator ID="regsearch" runat="server" ErrorMessage="Special characters are not allowed" CssClass="formerror" ControlToValidate="txtstarsearch" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^[a-zA-Z0-9@. ]+$" ValidationGroup="valgrp"></asp:RegularExpressionValidator>
                </span>
              <div class="btnsearchreset"
                 <span class="directorysearch">
                    <asp:LinkButton ID="lnkstarcontact" runat="server" OnClick="lnkstarcontact_Click" ToolTip="Search Contact" CssClass="userdirectory-searchbtn" TabIndex="3" ValidationGroup="valgrp"><i class="fa fa-search" ></i></asp:LinkButton></span>
                <span class="directoryreset">
                    <asp:LinkButton ID="lnkreset" runat="server" OnClick="lnkreset_Click" CssClass="userdirectory-searchbtn" ToolTip="Reset Search" TabIndex="4"><i class="fa fa-undo" aria-hidden="true"></i>
                    </asp:LinkButton></span>
              </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlstar" runat="server" Visible="false" DefaultButton="lnkstarcontact">

            <div id="content">
                <div class="starred-contacts"><i class="fa fa-star" aria-hidden="true"></i>Starred Contacts</div>
                <div id="buddypress">
                    <div id="item-body" role="main">
                        <div class="intern-padding">
                            <div class="contact-members-starred">
                                <asp:Panel ID="pnlstar1" runat="server" Visible="false">
                                    <asp:Repeater ID="rptstar" runat="server" OnItemCommand="rptstar_ItemCommand">
                                        <ItemTemplate>
                                            
                                            <div class="contactimage">
                                                <asp:LinkButton ID="lnkusername" runat="server" CommandArgument='<%# Eval("indexid") %>'>
                                                    <div class="contactimgprofile">
                                                        <img runat="server" id="imgprofile" class="contactimg" src='<%#getuserimage(Eval("profilephoto")) %>' /></div>
                                                    <div class="contactfieldsname"><span class="contactfileds1"><span class="contactfileds2"><%# Eval("fullname") %></span></span></div>
                                                </asp:LinkButton>
                                                <div class="mail">
                                                    <span class="mail1"><span class="mail2">
                                                        <asp:LinkButton ID="lnkemail" runat="server" CommandName="sentmsg" CommandArgument='<%# Eval("username")%>' ToolTip="Click here to send an message">
                                    <span class="emailbtn1"><i class="fa fa-envelope" aria-hidden="true"></i><%#Eval("username").ToString().Length >25 ? Eval("username").ToString().Substring(0, 24)+"..." : Eval("username") %></span>
                                                        </asp:LinkButton>
                                                    </span></span>
                                                </div>
                                                <div class="contactfieldsname5"><span class="contactfileds1"><span class="contactfileds3"><%# Eval("department").ToString().Length >0 ? Eval("department") : "-"%></span></span></div>
                                                <div class="contactfieldsphone"><span class="contactphone1"><span class="contactphone3"><i class="fa fa-phone" aria-hidden="true"></i><%# Eval("mobileno") %></span></span></div>

                                                <div class="follow">
                                                    <asp:UpdatePanel ID="updfollowstar" runat="server" UpdateMode="Always">
                                                        <ContentTemplate>
                                                            <asp:LinkButton ID="lnkfollow1" runat="server" CommandName="follow1" CommandArgument='<%# Eval("username")%>' Visible="false">
                                                                <asp:Label ID="followbtn1" runat="server" CssClass="follow" Visible="false"></asp:Label>
                                                            </asp:LinkButton>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="lnkfollow1" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>

                                                <div class="starred">
                                                    <asp:LinkButton ID="lnkdelete" runat="server" CommandName="deletestar"
                                                        CommandArgument='<%# Eval("username")%>' ToolTip="Click here to remove this contact form Starred List" Visible="false">
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
                <div class="starred-contacts"><i class="fa fa-user" aria-hidden="true"></i>Contacts</div>
                <div id="buddypress2">
                    <div id="item-body2" role="main">
                        <div class="intern-padding">
                            <div class="contact-members-starred">
                                <asp:Panel ID="pnlallcntc1" runat="server">
                                    <asp:Repeater ID="rptall" runat="server" OnItemCommand="rptall_ItemCommand">
                                        <ItemTemplate>
                                            <div class="contactimage">
                                                <asp:LinkButton ID="lnkusername" runat="server" CommandArgument='<%# Eval("indexid") %>'>
                                                    <div class="contactimgprofile">
                                                        <img runat="server" id="imgprofile1" class="contactimg" src='<%#getuserimage(Eval("profilephoto")) %>' /></div>
                                                    <div class="contactfieldsname"><span class="contactfileds1"><span class="contactfileds2"><%# Eval("fullname") %></span></span></div>
                                                </asp:LinkButton>
                                                <div class="mail">
                                                    <span class="mail1"><span class="mail2">
                                                        <asp:LinkButton ID="lnkemail1" runat="server" CommandName="sentmsg" CommandArgument='<%# Eval("username") %>' ToolTip="Click here to send Message">
                                    <span class="emailbtn1"><i class="fa fa-envelope" aria-hidden="true"></i><%#Eval("username").ToString().Length >25 ? Eval("username").ToString().Substring(0, 24)+"..." : Eval("username") %></span>
                                                        </asp:LinkButton>
                                                    </span></span>
                                                </div>
                                                <div class="contactfieldsname5"><span class="contactfileds1"><span class="contactfileds3"><%# Eval("department").ToString().Length >0 ? Eval("department") : "-"%></span></span></div>
                                                <div class="contactfieldsphone"><span class="contactphone1"><span class="contactphone3"><i class="fa fa-phone" aria-hidden="true"></i><%# Eval("mobileno") %></span></span></div>

                                                <div class="follow">
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

                                               <%-- <div class="starred">
                                                    <asp:LinkButton ID="lnkadd" runat="server" CommandName="addstar" CommandArgument='<%# Eval("username")%>' ToolTip="Click here to Starred this contact">
                                      <span class="starbtn1"><i class="fa fa-star-o" aria-hidden="true"></i></span>
                                                    </asp:LinkButton>
                                                </div>--%>
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
    </div>
</asp:Content>


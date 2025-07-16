<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="composemail.aspx.cs" Inherits="sathisendmail" %>

<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="item-list-tabs no-ajax" id="subnav" role="navigation">
        <ul>
            <li id="compose-personal-li" class="current selected"><a id="compose" href="<%= ReturnUrl("sitepathmain")%>composemail.aspx">Compose</a></li>
            <li id="inbox-personal-li"><a id="inbox" href="<%= ReturnUrl("sitepathmain")%>inbox.aspx">Inbox</a></li>
            <li id="sentbox-personal-li"><a id="sentbox" href="<%= ReturnUrl("sitepathmain")%>outbox.aspx">Sent</a></li>
        </ul>
    </div>
    <div class="accountpage">
        <div class="messagebox5" style="display: none;">
            <div class="messagebox1">
                <div class="compose"><a class="message-unselected" href="<%= ReturnUrl("sitepathmain")%>inbox.aspx" title="Compose Message">Back To Inbox</a></div>
                <div class="messagebox-head">
                    <a class="message-selected" href="~/inbox.aspx" title="My Inbox"><i class="fa fa-inbox"></i>Inbox</a>
                    <a class="message-selected" href="~/outbox.aspx" title="My Outbox"><i class="fa fa-sign-out"></i>Sent</a>

                </div>
            </div>
        </div>
        <div class="messagebox">
            <table id="Table2" runat="server" align="center" width="100%" style="display: none;">
                <tr>
                    <td align="center" runat="server">
                        <ul class="msgfilter align-left">
                            <li>
                                <div class="commonpagesheading">COMPOSE MESSAGE</div>
                            </li>
                        </ul>
                        <ul class="msgfilter align-right">

                            <li>
                                <a title="Inbox" href='~/inbox.aspx' class="message-box-searchbtn"><i class="fa fa-inbox"></i></a>
                            </li>
                            <li>
                                <a title="Sent" href='~/outbox.aspx' class="message-box-searchbtn"><i class="fa fa-paper-plane"></i></a></li>
                            <asp:DropDownList ID="ddllabels" runat="server" CssClass="msgselect" AutoPostBack="true" OnSelectedIndexChanged="ddllabels_SelectedIndexChanged" Visible="false">
                                <asp:ListItem Text="Compose"></asp:ListItem>
                                <asp:ListItem Text="Inbox"></asp:ListItem>
                                <asp:ListItem Text="Outbox"></asp:ListItem>
                            </asp:DropDownList>
                        </ul>
                    </td>
                </tr>
            </table>
            <table id="divsearch" runat="server" visible="false">
                <tr>
                    <td align="center">
                        <asp:Label ID="lblmessage" runat="server" Style="display: inline-block; padding: 10px 0 0;"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <div class="submitbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="submitbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" OnClick="lnkcont_Click"></asp:LinkButton>
                        </div>

                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnltbl1" runat="server" Visible="false">
                <ul id="TABLE1" class="tblcompose" runat="server">
                    <li>
                        <div class="example example_tagclass">
                            <div class="bs-example">
                                <asp:HiddenField ID="hdvalue" runat="server" />
                                <asp:HiddenField ID="hdarray" runat="server" />
                            </div>
                        </div>
                        <label>Send To</label>
                        <asp:TextBox ID="txtTo" CssClass="composetxt" runat="server" TextMode="MultiLine" Visible="false" ReadOnly="true"></asp:TextBox>
                        <asp:TextBox ID="txtTo1" CssClass="txtcomposetitle txtsearch" runat="server" TextMode="MultiLine" Text=""></asp:TextBox>
                        <asp:HiddenField ID="hfCustomerId" runat="server" />
                        <asp:Label ID="lblto" runat="server" Text="" Font-Size="15px" Visible="false"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Username is required"
                            ValidationGroup="valgrp" ControlToValidate="txtTo1" Display="Dynamic" CssClass="errorfield" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="regsearch" runat="server" ErrorMessage="Special characters are not allowed" CssClass="formerror" ControlToValidate="txtTo1" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^[a-zA-Z0-9 @.]+$" ValidationGroup="valgrp"></asp:RegularExpressionValidator>
                    </li>
                    <li class="submit" style="display: none;">
                        <a id="MainContent_btnbrowse" title="Browse Users" class="message-box-searchbtn align-right" href="javascript:__doPostBack('ctl00$MainContent$btnbrowse','')"><i aria-hidden="true" class="fa fa-search"></i></a>
                    </li>
                    <li>
                        <label>Subject</label>
                        <asp:TextBox ID="txttitle" runat="server" CssClass="txtcomposetitle" MaxLength="250"></asp:TextBox>
                        <asp:Label ID="lbltitle" runat="server" Text="" Font-Size="15px" Visible="false"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Subject is required"
                            ValidationGroup="valgrp" ControlToValidate="txtTitle" Display="Dynamic" CssClass="errorfield" SetFocusOnError="true"
                            EnableViewState="true"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Special characters are not allowed" CssClass="formerror" ControlToValidate="txtTitle" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^[a-zA-Z0-9 .]+$" ValidationGroup="valgrp"></asp:RegularExpressionValidator>
                    </li>
                    <li>
                        <label>Message</label>
                        <asp:TextBox ID="txtlongdesc" runat="server" CssClass="txtcomposedesc" Height="200px" TextMode="MultiLine" MaxLength="500"></asp:TextBox>
                        <asp:Label ID="lbldesc" runat="server" Text="" Font-Size="15px" Visible="false"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Message is required"
                            ValidationGroup="valgrp" ControlToValidate="txtlongdesc" Display="Dynamic" CssClass="errorfield" SetFocusOnError="true" EnableViewState="true"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Special characters are not allowed" CssClass="formerror" ControlToValidate="txtlongdesc" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^[a-zA-Z0-9 .]+$" ValidationGroup="valgrp"></asp:RegularExpressionValidator>
                    </li>
                    <li class="submit">
                        <asp:LinkButton ID="btnsendmail" runat="server" ValidationGroup="valgrp" CssClass="message-box-searchbtn1 align-right" OnClick="btnsendmail_Click"><i class="fa fa-send m-r-xs"></i>&nbsp;&nbsp;Send Message</asp:LinkButton>
                    </li>
                </ul>
                <ul class="tblcompose" id="TABLE12" runat="server" visible="false">
                    <li></li>
                    <li>
                        <asp:LinkButton ID="btnbrowse" runat="server" Visible="false" CausesValidation="False" ToolTip="Browse Users" OnClick="btnbrowse_Click" CssClass="message-box-searchbtn1 align-right"><i aria-hidden="true" class="fa fa-search"></i></asp:LinkButton>
                    </li>
                    <li></li>
                    <li></li>
                    <li></li>
                </ul>
            </asp:Panel>
            <asp:Panel ID="pnltbl2" Visible="false" runat="server">
                <asp:Repeater ID="rptsender" runat="server">
                    <ItemTemplate>
                        <table style="margin: 20px 0;">

                            <tr>
                                <td colspan="1">
                                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle" height="20px">Send From</td>
                                <td align="left" valign="middle" height="20px">
                                    <asp:Label ID="lblfrom" runat="server" Text='<%#Eval("username") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle" height="25px">Subject</td>
                                <td align="left" height="25px" valign="middle">
                                    <asp:Label ID="lblsub" runat="server" Text='<%#Eval("title") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle" height="25px">Date</td>
                                <td align="left" height="25px" valign="middle">
                                    <%# DataBinder.Eval(Container, "DataItem.Expirydate") %>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px; height: 15px">Message</td>
                                <td style="width: 650px; height: 15px">
                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("description") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <br />
                                    <asp:LinkButton ID="btnreply" runat="server" Text="Reply" CssClass="message-box-searchbtn" OnClick="btnreply_Click" /></td>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:Repeater>
            </asp:Panel>
            <asp:Panel ID="pnltbl3" Visible="false" runat="server">
                <asp:Repeater ID="rptreceiver" runat="server">
                    <ItemTemplate>
                        <table>

                            <tr>
                                <td colspan="2" style="height: 34px">
                                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle" height="20px">Send To</td>
                                <td align="left" valign="middle" height="20px">
                                    <asp:Label ID="lblfrom" runat="server" Text='<%#Eval("usernameto") %>'></asp:Label>

                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle" height="25px">Subject</td>
                                <td align="left" height="25px" valign="middle">
                                    <asp:Label ID="lblsub" runat="server" Text='<%#Eval("title") %>'></asp:Label>
                                </td>

                            </tr>

                            <tr>
                                <td align="left" valign="middle" height="25px">Date</td>
                                <td align="left" height="25px" valign="middle">
                                    <%#DataBinder.Eval(Container,"DataItem.Expirydate","{0:dd-MMM-yyyy hh:mm:ss}")%>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px; height: 15px">Message</td>
                                <td style="width: 650px; height: 15px">
                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("description") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:LinkButton ID="btnback" runat="server" Text="Back" CssClass="message-box-searchbtn" OnClick="btnback_Click" /></td>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:Repeater>
            </asp:Panel>
        </div>
    </div>
    <link rel="stylesheet" href="<%=ReturnUrl("css")%>autocomplete/app.css" />
    <script src="<%=ReturnUrl("sitepath")%>js/autocomplete/jquery.min.js"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/autocomplete/typeahead.bundle.min.js"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/autocomplete/bootstrap-tagsinput.min.js"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/autocomplete/app.js"></script>
    <asp:Literal ID="ltmsg" runat="server" Visible="true"></asp:Literal>
    <script>
        jQuery(document).ready(function () {
            jQuery("#MainContent_RequiredFieldValidator3").css("visibility", "hidden");
            jQuery("#MainContent_btnsendmail").click(function () { jQuery("#MainContent_RequiredFieldValidator3").css("visibility", "visible"); });
        });
    </script>
</asp:Content>

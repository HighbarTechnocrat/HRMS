<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="inbox.aspx.cs" Inherits="inbox" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style>
        .commonpagesheading {
            font-size: 24px !important;
            margin: 0 20px 0 0 !important;
            text-align: left;
            padding: 0 !important;
            width: auto;
        }
    </style>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>message.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="<%=ReturnUrl("css")%>datepicker/jquery-ui.css" type="text/css" media="all" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript">
        function SelectAll(id) {
            var grid = document.getElementById("<%# gvsendmail.ClientID %>");
            var st = "true"
            var cell;

            if (grid.rows.length > 0) {
                for (i = 1; i < grid.rows.length; i++) {
                    cell = grid.rows[i].cells[0];
                    for (j = 0; j < cell.childNodes.length; j++) {      
                        if (cell.childNodes[j].type == "checkbox") {
                            cell.childNodes[j].checked = document.getElementById(id).checked;

                        }
                    }
                }
            }
        }

    </script>
    <div class="item-list-tabs no-ajax" id="subnav" role="navigation">
        <ul>
            <li id="compose-personal-li"><a id="compose" href="<%# ReturnUrl("sitepathmain")%>composemail.aspx">Compose</a></li>
            <li id="inbox-personal-li" class="current selected"><a id="inbox" href="<%# ReturnUrl("sitepathmain")%>inbox.aspx">Inbox</a></li>
            <li id="sentbox-personal-li"><a id="sentbox" href="<%# ReturnUrl("sitepathmain")%>outbox.aspx">Sent</a></li>
        </ul>
    </div>
    <div class="accountpage">
        <div class="messagebox5" style="display: none;">
            <div class="messagebox1">
                <div class="compose"><a title="Compose Message" href='<%#ReturnUrl("sitepathmain")%>composemail.aspx' class="message-unselected">Compose</a></div>
                <div class="messagebox-head">
                    <a title="My Inbox" href="#" class="message-selected"><i class="fa fa-inbox"></i>Inbox</a>
                    <a title="My Outbox" href='<%#ReturnUrl("sitepathmain")%>outbox.aspx' class="message-selected"><i class="fa fa-sign-out"></i>Sent</a>
                </div>
            </div>
        </div>
        <div class="messagebox">
            <table id="Table1" class="" runat="server" align="center" width="100%">
                <tr id="trsearch" runat="server">
                    <td align="center" runat="server">
                        <asp:Panel ID="pnlsearch" runat="server" Visible="true" DefaultButton="btnsearch">
                            <ul class="msgfilter align-left" id="tblsearch" runat="server">
                                <li class="msglisearchmsg">
                                    <asp:TextBox ID="txtsearch" placeholder="Search By Subject" runat="server" CssClass="msgtitlebox" ToolTip="Type any words from subject line" onkeydown="javascript:if ((event.which &amp;&amp; event.which == 13) || (event.keyCode &amp;&amp; event.keyCode == 13)) {document.forms[0].elements['ctl00_ContentPlaceHolder5_btnsearch'].click();return false;} else return true;"></asp:TextBox>
                                     <asp:RegularExpressionValidator ID="regsearch" runat="server" ErrorMessage="Special characters are not allowed" CssClass="formerror" ControlToValidate="txtsearch" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^[a-zA-Z0-9@. ]+$" ValidationGroup="valgrp"></asp:RegularExpressionValidator>
                                </li>
                                <li>
                                    <asp:TextBox ID="txtfromdate" placeholder="From" runat="server" CssClass="txtbox" Width="80px"></asp:TextBox>
                                    <img id="btnfromdate" class="calimgs" src='<%#ReturnUrl("sitepathmain")%>themes/creative1.0/images/icons/calendar.png' />
                                   <%-- <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfromdate"
                                        PopupButtonID="btnfromdate" Format="MM/d/yyyy">
                                    </ajaxToolkit:CalendarExtender>--%>
                                </li>
                                <li>
                                    <asp:TextBox ID="txttodate" placeholder="To" runat="server" CssClass="txtbox" Width="80px"></asp:TextBox>
                                    <img id="btnttodate" class="calimgs" src='<%#ReturnUrl("sitepathmain")%>themes/creative1.0/images/icons/calendar.png' />
                                 <%--   <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txttodate"
                                        PopupButtonID="btnttodate" Format="MM/d/yyyy">
                                    </ajaxToolkit:CalendarExtender>--%>
                                </li>
                                <li>
                                    <asp:LinkButton ID="btnsearch" runat="server" OnClick="btnsearch_Click" ToolTip="Search" CssClass="message-box-searchbtn"><i class="fa fa-search"></i></asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton ID="lnkreset" runat="server" OnClick="lnkreset_Click" ToolTip="Reset Search" CssClass="message-box-searchbtn"><i class="fa fa-undo"></i></asp:LinkButton>
                                </li>
                            </ul>
                        </asp:Panel>
                        <ul class="msgfilter align-right">
                            <li>
                                <a title="Compose New Message" href='<%#ReturnUrl("sitepathmain")%>composemail.aspx' class="message-box-searchbtn"><i class="fa fa-pencil"></i></a>
                            </li>
                            <li>
                                <a title="Inbox" href='<%#ReturnUrl("sitepathmain")%>inbox.aspx' class="message-box-searchbtn"><i class="fa fa-inbox"></i></a>
                            </li>
                            <li>
                                <a title="Sent" href='<%#ReturnUrl("sitepathmain")%>outbox.aspx' class="message-box-searchbtn"><i class="fa fa-paper-plane"></i></a>
                            </li>
                            <asp:DropDownList ID="ddllabels" runat="server" CssClass="msgselect" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddllabels_SelectedIndexChanged">
                                <asp:ListItem Text="Compose"></asp:ListItem>
                                <asp:ListItem Text="Inbox"></asp:ListItem>
                                <asp:ListItem Text="Sent"></asp:ListItem>
                            </asp:DropDownList>
                            <li>
                                <asp:LinkButton ID="btnDelte" Text="Delete" runat="server" OnClick="btnDelte_Click" ToolTip="Delete" CssClass="message-box-searchbtn"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                            </li>
                            <li>
                                <asp:DropDownList ID="ddlstmore" CssClass="msgselect" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlstmore_OnSelectedIndexChanged">
                                    <asp:ListItem Text="More Actions" Value="S"></asp:ListItem>
                                    <asp:ListItem Text="Mark As Read" Value="N"></asp:ListItem>
                                    <asp:ListItem Text="Mark As Unread" Value="Y"></asp:ListItem>
                                </asp:DropDownList>
                            </li>
                        </ul>


                    </td>
                </tr>
                <tr align="center" runat="server" id="divfilter" visible="false">
                    <td align="center" runat="server">
                        <table align="center" id="mytable" runat="server" visible="false">
                            <tr>
                                <td align="center" runat="server" id="mytd" visible="false">
                                    <table border="0" align="center" cellspacing="0" cellpadding="0" style="vertical-align: middle">
                                        <tr>
                                            <td width="7" style="height: 17px"></td>
                                            <td bgcolor="#FFCC66" style="height: 17px" valign="middle">
                                                <asp:Label ID="lblmessagenew" runat="server" Font-Bold="true" Font-Size="10"></asp:Label>
                                            </td>
                                            <td style="width: 7px; height: 17px"></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <asp:Label ID="lblmsg" runat="server" Visible="false" BackColor="#FFCC66"></asp:Label>
                        <asp:Label ID="lblmsg1" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="Label2" Text="You searched for" runat="server" Visible="false"></asp:Label>
                        &nbsp;
                
                                <asp:Label ID="lblmessage11" ForeColor="red" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblnomsg" ForeColor="red" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr id="divsearch" runat="server" visible="false">
                    <td align="center">
                        <asp:Label ID="lblmessage" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:GridView ID="gvsendmail" runat="server" AllowSorting="True" BorderStyle="None"
                            CellPadding="3" BorderColor="white" BorderWidth="0" PageSize="10" OnRowDataBound="gvsendmail_RowDataBound"
                            AutoGenerateColumns="False" DataKeyNames="mailid" OnRowCommand="gvsendmail_RowCommand"
                            AllowPaging="True" Width="100%" OnPageIndexChanging="gvsendmail_PageIndexChanging" CssClass="msgtable">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="cbSelectAll" runat="server" />
                                    </HeaderTemplate>
                                    <ItemStyle HorizontalAlign="center" CssClass="border1 itemdescriptionfont border2" />
                                    <HeaderStyle HorizontalAlign="center" CssClass="border1" Width="3%" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="From">
                                    <ItemTemplate>
                                        <asp:Label ID="lnbtnedit1" runat="server" Text='<%#Eval("fname") %>' ForeColor="Black"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" CssClass=" border1 border3" />
                                    <HeaderStyle HorizontalAlign="center" CssClass="msgusername border1"
                                        ForeColor="White" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Click the Link to Access Message">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbltitle" runat="server" Text='<%#Eval("title") %>'
                                            CommandName="ReplyItem" CssClass="msgtitle" Font-Underline="false" CommandArgument='<%#Eval("mailid") %>' ToolTip="Click here to read the mail"></asp:LinkButton>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" CssClass="border1 itemdescriptionfont border4"
                                        Font-Underline="false" />
                                    <HeaderStyle HorizontalAlign="center" CssClass="border1 msgbody"
                                        ForeColor="White" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate" runat="server" Text='<%#getdate(Eval("Expirydate")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" CssClass="border1 itemdescriptionfont border5" />
                                    <HeaderStyle HorizontalAlign="center" CssClass="border1 msgdates" ForeColor="White" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Message" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblmsg" runat="server" Text='<%# Eval("description") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" CssClass="border1 itemdescriptionfont border6" />
                                    <HeaderStyle HorizontalAlign="center" Width="17%" CssClass="border1" BackColor="#e9f5f5"
                                        ForeColor="Black" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reply">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnbtnedit" runat="server" CommandName="EditItem"
                                            CommandArgument='<%# Eval("mailid") %>'><i class="fa fa-reply" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="center" CssClass="border1 itemdescriptionfont border6" />
                                    <HeaderStyle HorizontalAlign="center" CssClass="border1 msgreply" ForeColor="White" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UnreadFlag" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFlag" runat="server" Text='<%# Eval("unread") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <PagerSettings NextPageText="&amp;lt;" Mode="NumericFirstLast" />
                            <PagerStyle HorizontalAlign="Right" VerticalAlign="Middle" CssClass="msgpager" />
                        </asp:GridView>

                    </td>
                </tr>

            </table>
        </div>
    </div>
</asp:Content>



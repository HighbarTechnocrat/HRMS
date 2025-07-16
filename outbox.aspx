<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="outbox.aspx.cs" Inherits="outbox" %>

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
            //get reference of GridView control
            var grid = document.getElementById("<%= gvsendmail.ClientID %>");
            var st = "true"
            //variable to contain the cell of the grid
            var cell;

            if (grid.rows.length > 0) {
                //loop starts from 1. rows[0] points to the header.
                for (i = 1; i < grid.rows.length; i++) {
                    //get the reference of first column
                    cell = grid.rows[i].cells[0];

                    //loop according to the number of childNodes in the cell
                    for (j = 0; j < cell.childNodes.length; j++) {
                        //if childNode type is CheckBox                 
                        if (cell.childNodes[j].type == "checkbox") {
                            //assign the status of the Select All checkbox to the cell checkbox within the grid
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
            <li id="inbox-personal-li"><a id="inbox" href="<%# ReturnUrl("sitepathmain")%>inbox.aspx">Inbox</a></li>
            <li id="sentbox-personal-li" class="current selected"><a id="sentbox" href="<%# ReturnUrl("sitepathmain")%>outbox.aspx">Sent</a></li>

        </ul>
    </div>
    <div class="accountpage">
        <div class="messagebox5" style="display: none;">
            <div class="messagebox1">
                <div class="compose"><a title="Compose Message" href='<%#ReturnUrl("sitepathmain")%>composemail.aspx' class="message-unselected">Compose</a></div>
                <div class="messagebox-head">
                    <a title="My Inbox" href='<%#ReturnUrl("sitepathmain")%>inbox.aspx' class="message-selected"><i class="fa fa-inbox"></i>Inbox</a>
                    <a title="My Outbox" href="#" class="message-selected"><i class="fa fa-sign-out"></i>Sent</a>

                </div>
            </div>
        </div>
        <div class="messagebox">
            <table id="Table1" runat="server" align="center" width="100%">
                <tr>
                    <td align="center" runat="server">
                        <asp:Panel ID="pnlsearch" runat="server" Visible="true" DefaultButton="btnsearch">
                            <ul class="msgfilter align-left" id="tblsearch" runat="server">
                                <li class="msglisearchmsg">
                                    <asp:TextBox ID="txtsearch" placeholder="Search By Subject" runat="server" CssClass="msgtitlebox" ToolTip="Type any words from subject line" onkeydown="javascript:if ((event.which &amp;&amp; event.which == 13) || (event.keyCode &amp;&amp; event.keyCode == 13)) {document.forms[0].elements['ctl00_ContentPlaceHolder5_btnsearch'].click();return false;} else return true;"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="regsearch" runat="server" ErrorMessage="Special characters are not allowed" CssClass="formerror" ControlToValidate="txtsearch" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^[a-zA-Z0-9@. ]+$" ValidationGroup="valgrp"></asp:RegularExpressionValidator>
                                </li>
                                <li>
                                    <asp:TextBox ID="txtfromdate" placeholder="From" runat="server" CssClass="txtbox" Width="100px"></asp:TextBox>
                                    <img id="btnfromdate" class="calimgs" src='<%#ReturnUrl("sitepathmain")%>themes/creative1.0/images/icons/calendar.png' />
                                 <%--   <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtfromdate"
                                        PopupButtonID="btnfromdate" Format="MM/d/yyyy">
                                    </ajaxToolkit:CalendarExtender>--%>

                                </li>
                                <li>
                                    <asp:TextBox ID="txttodate" placeholder="To" runat="server" CssClass="txtbox" Width="100px"></asp:TextBox>
                                    <img id="btnttodate" class="calimgs" src='<%#ReturnUrl("sitepathmain")%>themes/creative1.0/images/icons/calendar.png' />
<%--                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txttodate"
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
                            <ul class="msgfilter align-right">
                                <li>
                                    <a title="Compose New Message" href='<%#ReturnUrl("sitepathmain")%>composemail.aspx' class="message-box-searchbtn"><i class="fa fa-pencil"></i></a></li>
                                <li>
                                    <a title="Inbox" href='<%#ReturnUrl("sitepathmain")%>inbox.aspx' class="message-box-searchbtn"><i class="fa fa-inbox"></i></a>
                                    <asp:DropDownList ID="ddllabels" runat="server" CssClass="msgselect" AutoPostBack="true" OnSelectedIndexChanged="ddllabels_SelectedIndexChanged" Visible="false">
                                        <asp:ListItem Text="Compose"></asp:ListItem>
                                        <asp:ListItem Text="Inbox"></asp:ListItem>
                                        <asp:ListItem Text="Sent"></asp:ListItem>
                                    </asp:DropDownList>

                                </li>
                                <li>
                                    <a title="Sent" href='<%#ReturnUrl("sitepathmain")%>outbox.aspx' class="message-box-searchbtn"><i class="fa fa-paper-plane"></i></a></li>

                                <li>
                                    <asp:LinkButton ID="btnDelte" Text="Delete" runat="server" OnClick="btnDelte_Click" ToolTip="Delete" CssClass="message-box-searchbtn"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                </li>
                            </ul>
                        </asp:Panel>
                    </td>
                </tr>
                <tr id="divsearch" runat="server" visible="false">
                    <td align="center">
                        <asp:Label ID="lblmessage" ForeColor="red" runat="server" Style="display: inline-block; padding: 10px 0 0;"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:GridView ID="gvsendmail" runat="server" AllowSorting="True" BorderStyle="None" BorderColor="white" BorderWidth="0" OnRowDataBound="gvsendmail_RowDataBound" CssClass="msgtable" AutoGenerateColumns="False" DataKeyNames="mailid" PageSize="10" CellPadding="3"
                            AllowPaging="True" Width="100%" OnRowCommand="gvvendor_RowCommand" OnPageIndexChanging="gvsendmail_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="cbSelectAll" runat="server" />
                                    </HeaderTemplate>
                                    <ItemStyle HorizontalAlign="center" CssClass=" border1 itemdescriptionfont border2" />
                                    <HeaderStyle HorizontalAlign="center" CssClass="border1 border7" ForeColor="White" Width="3%" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="To">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnbtnedit1" runat="server" ForeColor="Black" Font-Underline="true" Text='<%# Bind("coach_fname") %>'
                                            CommandName="ReplyItem" CommandArgument='<%#Eval("mailid") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" CssClass="border1 itemdescriptionfont border3" />
                                    <HeaderStyle HorizontalAlign="center" CssClass="msgusername border1 border7"
                                        ForeColor="White" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Subject">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltitle" runat="server" Text='<%# Bind("title") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" CssClass=" border1 itemdescriptionfont border4" />
                                    <HeaderStyle HorizontalAlign="center" CssClass="msgbody border1 border7"
                                        ForeColor="White" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate" runat="server" Text='<%#getdate(Eval("Expirydate")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" CssClass=" border1 itemdescriptionfont border5" />
                                    <HeaderStyle HorizontalAlign="center" CssClass="border1 msgdates border7" ForeColor="White" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Message" Visible="false">
                                    <ItemTemplate>
                                    <asp:Label ID="lblmsg" runat="server" Text='<%# Bind("description") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" Width="172px" />
                                    <HeaderStyle HorizontalAlign="Center" BackColor="#e9f5f5" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btndel" runat="server" CssClass="msgdelete" CommandArgument='<% #Eval("mailid")%>' OnClientClick="return confirm('Are you sure you want to delete this record?');" CommandName="DeleteItem"><i aria-hidden="true" class="fa fa-trash-o"></i></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" CssClass=" border1 itemdescriptionfont border6" />
                                    <HeaderStyle HorizontalAlign="center" CssClass="border1 msgreply border7" ForeColor="White" />
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


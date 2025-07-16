<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="settings.aspx.cs" Inherits="myaccount_editprofile" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link href="<%=ReturnUrl("css") %>upload/style.css" rel="stylesheet" type="text/css" />
    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
            background: #faf7f7 none repeat scroll 0 0;
        }

        .editprofile {
            margin: 0 !important;
            width: auto !important;
            float: none !important;
        }

        .myaccountpagesheading {
            border-bottom: medium none;
            color: #843719;
            font-size: 24px;
            font-weight: 300;
            margin: 0 auto;
            padding: 20px 0 0;
            text-align: left;
            text-transform: capitalize;
            width: 870px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepathmain") %>js/jsframework.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/togglebuttonjs/jquery-1.11.2.min.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/togglebuttonjs/on-off-switch-onload.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/togglebuttonjs/on-off-switch.js" type="text/javascript"></script>
    <div class="accountpage">
        <div class="messagebox">
            <div class="commonpages">
                <div class="contact-container">
                    <div class="editprofile" style="text-align: center; border: none;" id="divmsg" runat="server" visible="false">
                        <asp:Label runat="server" ID="lblmessage" Visible="false" Style="color: green; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                    </div>
                    <div class="editprofile" id="divbtn" runat="server" visible="false">
                        <div class="submitbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                    </div>
                    <ul id="editform" runat="server">

                        <asp:Repeater ID="rptwidget" runat="server">
                            <ItemTemplate>
                                <li>
                                    <label><%# Eval("widget_name") %></label>
                                    <div class="checkbox-container">
                                        <asp:CheckBox ID="chkwidgetonoff" runat="server" Checked='<%#getonoff(Eval("flag"))%>' />
                                        <asp:Label ID="lblid" runat="server" Text='<%# Eval("widget_id") %>' Style="display: none;"></asp:Label>
                                        <asp:Label ID="lbltitle" runat="server" Text='<%# Eval("widget_title") %>' Style="display: none;"></asp:Label>
                                        <asp:HiddenField ID="hfwid" Value='<%#getonoff(Eval("flag"))%>' runat="server" />
                                    </div>
                                    <asp:Literal ID="ltscript" runat="server"></asp:Literal>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                        <li class="submit">
                            <asp:LinkButton ID="btnsubmit" runat="server" ToolTip="Update" ValidationGroup="validate" CssClass="message-box-searchbtn1" OnClick="btnsubmit_Click"><i class="fa fa-undo"></i>&nbsp;&nbsp;Reset Settings</asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <script>
        function UpdateSettings(id, flag) {
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                <%-- url: '<%=ReturnUrl("sitepathmain") %>procs/settings.aspx/UpdateSettings',--%>
                url: '<%=ReturnUrl("sitepathmain") %>procs/settings.aspx/UpdateSettings',
                data: "{'id':'" + id + "','flag':'" + flag + "'}",
                dataType: 'text',
                async: false,
                success: function (response) {
                    if (response.indexOf("success") > -1) {
                        return false;
                    }
                    else if (response.indexOf("error") > -1) {
                        return false;
                    }
                },
                error: function (xhr, status, error) {
                    return false;
                }
            });
        }
    </script>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="HolidayList.aspx.cs" Inherits="procs_HolidayList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <style>
        .linkcss {
            text-decoration-color: #EA215A;
            text-decoration-thickness: .125em;
            text-underline-offset: 1.5px;
        }

        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
            /*background: #dae1ed;*/
            background: #ebebe4;
        }

        .Calender {
            float: left;
            padding: 5% 5% 5% 5% !important;
        }

        .leavegridMain {
            margin: 0 0 0 2% !important;
        }

        #MainContent_lnk_leaverequest:link, #MainContent_lnk_leaverequest:visited,
        #MainContent_lnk_LeaveRequest_FrmHR:link, #MainContent_lnk_LeaveRequest_FrmHR:visited,
        #MainContent_lnk_mng_leaverequest:link, #MainContent_lnk_mng_leaverequest:visited,
        #MainContent_lnk_HRLeaveInbox:link, #MainContent_lnk_HRLeaveInbox:visited,
        #MainContent_lnkempLeaveRpt:link, #MainContent_lnkempLeaveRpt:visited,
        #MainContent_lnk_leavereport_Hr:link, #MainContent_lnk_leavereport_Hr:visited,
        #MainContent_lnk_leavereport:link, #MainContent_lnk_leavereport:visited,
        #MainContent_lnk_leaveinbox:link, #MainContent_lnk_leaveinbox:visited,
        #MainContent_lnk_TeamCalendar:link, #MainContent_lnk_TeamCalendar:visited,
        #MainContent_lnk_leaveencashment:link, #MainContent_lnk_leaveencashment:visited {
            background-color: #C7D3D4;
            color: #603F83 !important;
            border-radius: 10px;
            /*color: white;*/
            padding: 25px 5px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            width: 90% !important;
        }

        #MainContent_lnk_leaverequest:hover, #MainContent_lnk_leaverequest:active,
        #MainContent_lnk_LeaveRequest_FrmHR:hover, #MainContent_lnk_LeaveRequest_FrmHR:active,
        #MainContent_lnk_mng_leaverequest:hover, #MainContent_lnk_mng_leaverequest:active,
        #MainContent_lnk_HRLeaveInbox:hover, #MainContent_lnk_HRLeaveInbox:active,
        #MainContent_lnkempLeaveRpt:hover, #MainContent_lnkempLeaveRpt:active,
        #MainContent_lnk_leavereport_Hr:hover, #MainContent_lnk_leavereport_Hr:active,
        #MainContent_lnk_leavereport:hover, #MainContent_lnk_leavereport:active,
        #MainContent_lnk_leaveinbox:hover, #MainContent_lnk_leaveinbox:active,
        #MainContent_lnk_TeamCalendar:hover, #MainContent_lnk_TeamCalendar:active,
        #MainContent_lnk_leaveencashment:hover, #MainContent_lnk_leaveencashment:active {
            /*background-color: #603F83;*/
            background-color: #3D1956;
            color: #C7D3D4 !important;
            border-color: #3D1956;
            border-width: 2pt;
            border-style: inset;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="wishlistpage">
                    <div class="userposts">
                        <span>
                            <asp:Label ID="lblheading" runat="server" Text="Holiday List"></asp:Label>
                        </span>
                    </div>
                                        <div>
                        <span>
                            <a href="Leaves.aspx" class="aaaa">Back</a>
                        </span>
                    </div>
                    <br />
                    <br />
                    <br />
                    <div class="leavegridMain">

                        <asp:GridView ID="dghbtHoliday" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                            AutoGenerateColumns="False" Width="50%">
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                            <PagerStyle ForeColor="#000066" HorizontalAlign="left" BackColor="White" />
                            <RowStyle ForeColor="#000066" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                            <Columns>
                                <asp:BoundField DataField="Holiday_Name" HeaderText="Holiday Name">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Width="30%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Holiday_Date" HeaderText="Holiday Date" DataFormatString="{0:dd/MM/yyyy}">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Weekday" HeaderText="Weekday">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


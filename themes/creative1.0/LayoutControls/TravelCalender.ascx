<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TravelCalender.ascx.cs" Inherits="themes_creative_LayoutControls_TravelCalender" %>

<html>
<head>
   <%-- <link rel="Stylesheet" media="all" type="text/css" href="../Includes/styles.css" />--%>
    <style type="text/css">
        td.anlign {
            font-size: 13px;
            text-align: left;
        }
    </style>
    <title></title>
</head>
<body>
    <table width="200px">
		<tr>
            <td style="height: 20px"></td>
        </tr>
        <tr>
            <td valign="top" align="left"  >
                <%--<hr style="width: 200px; color: #05568b; height: 1px" />--%>
                <div class="userposts">
                  <span>Individual Calendar </span>
                    </div>
                <%--<asp:TextBox ID="txtcalMonths" runat="server"  AutoPostBack="true"  OnTextChanged="txtcalMonths_TextChanged" BackColor="#53D5FF" BorderStyle="None" Width="226px"></asp:TextBox>--%>
                <asp:Calendar ID="Calendar1" runat="server" OnDayRender="Calendar1_DayRender" OnVisibleMonthChanged="Calendar1_VisibleMonthChanged"
                    Font-Bold="False"  Width="250px"   DayNameFormat="FirstLetter"
                    FirstDayOfWeek="Sunday" TitleFormat="MonthYear" BorderColor="White" ForeColor="Black"
                    Font-Overline="False" Font-Size="14px">
                    <TitleStyle BackColor="#05568b" Font-Bold="True" Font-Size="14px"
                        ForeColor="White" HorizontalAlign="Center"  />
                </asp:Calendar>

           <%-- <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="MMM-yyyy"  TargetControlID="txtcalMonths" runat="server">
            </ajaxToolkit:CalendarExtender> --%>

            </td>
        </tr>
        <tr>
            <td style="height: 10px"></td>
        </tr>
        <tr>
            <td class="anlign">
             <%--   Please
            <asp:HyperLink ID="HyperLeave" runat="server" Target="_blank"
                NavigateUrl="~/procs/Leave_Req.aspx"><u> Click </u></asp:HyperLink>
                here to apply for Leave--%>
                Legends
            </td>
        </tr>
        <tr>
            <td class="anlign">
           <%-- Please<asp:HyperLink ID="HyperLink1" runat="server" Target="_blank"
                NavigateUrl="~/InOutCaledar.aspx"><u> Click </u> </asp:HyperLink>
                here for In & Out Time.
            </td>--%>
        </tr>
        <tr>
            <td class="anlign">
                <asp:Image ID="imgLeavePending" runat="server"
                    ImageUrl="~/images/Calendar/leaveRqstPending.png" Height="13px" Width="25px" Style="margin-bottom: 0px" />
                 &nbsp;Pending
                Travel Request</td>
        </tr>
        <tr>
            <td class="anlign">
                <asp:Image ID="imgLeaveApproved" runat="server"
                    ImageUrl="~/images/Calendar/approveTravels.png" Height="13px" Width="25px" Style="margin-bottom: 0px" />
                 &nbsp;Approved
                Travel Request</td>
        </tr>
         <tr style="display:none">
            <td class="anlign">
                <asp:Image ID="imgLeaveReject" runat="server"
                    ImageUrl="~/images/Calendar/Correction.png" Height="13px" Width="25px" Style="margin-bottom: 0px" />
                 &nbsp;Correction
                Travel Request</td>
        </tr>         
        <tr>
            <td class="anlign">
                <asp:Image ID="HolidayImg" runat="server"
                    ImageUrl="~/images/Calendar/Holiday.png" Height="13px" Width="25px" />
                 &nbsp;Holiday                     
                           
            </td>
        </tr>
        <tr>       
            <td class="anlign">
                <asp:Image ID="Image2" runat="server"
                    ImageUrl="~/images/Calendar/Pending_leaves_team.png" Height="13px" Width="25px" Style="margin-bottom: 0px" />
                 &nbsp;Pending Leave Request
            </td>
        </tr>
          <tr>
            <td class="anlign">
                <asp:Image ID="Image1" runat="server"
                    ImageUrl="~/images/Calendar/leaveRqstApproved.png" Height="13px" Width="25px" Style="margin-bottom: 0px" />
                 &nbsp;Approved Leaves
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblError" runat="server" Text="Label" Font-Bold="True"
                    ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>

      
    </table>



    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    <asp:HiddenField ID="hdnmonthvalue" runat="server" />
    <asp:HiddenField ID="hdnmonthvalue_New" runat="server" />

</body>
</html>
 <script type="text/javascript">
     function noanyCharecters(e) {

         var keynum;
         var keychar;
         var numcheck = /[]/;

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
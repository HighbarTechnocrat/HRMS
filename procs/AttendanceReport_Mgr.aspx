<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="AttendanceReport_Mgr.aspx.cs" Inherits="AttendanceReport_Mgr" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <%--<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leaveteamrpt.css" type="text/css" media="all" />
    <%--<link rel="stylesheet" href="http://172.18.37.5/hrms/MAP_IMG/css/bootstrap.css" type="text/css" media="all" />--%>


    <style>
        /*.txtcls {
            width: 140px;
            height: 25px !important;
        }*/

        /*.edit-contact > ul > li {
            display: inline-block;
            margin-right: 9%;
            width: 20%;
        }*/

        /*.edit-contact input {
            padding-left: 30px !important;
            width: 100%;
        }*/

            .edit-contact input:focus {
                border-bottom: 2px solid rgb(51, 142, 201) !important;
            }

        .edit-contact input {
            padding-left: 30px !important;
            width: 83%;
        }

        .edit-contact > ul {
            padding: 0;
        }

            
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">


    <div class="userposts">
        <span>
            <asp:Label ID="lblheading" runat="server" Text="Regularization Report - Team"></asp:Label>
        </span>
    </div>


    <div class="leavegrid">
        <a href="Attendance.aspx" class="aaa">Attendance Menu</a>
    </div>
    <div style="width: 100%; overflow: auto; align-content: flex-start">
        <div class="editprofileform">
            <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="false" Style="margin-left: 135px"></asp:Label>
        </div>
    </div>

    <div class="edit-contact">
        <ul>
             <li>
                <span style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">Select Regularization Type:</span><br />
                <asp:DropDownList ID="ddlleaveTypeHR" runat="server" TabIndex="0" Style="float: none;"></asp:DropDownList>
            </li>

        </ul>
        <ul>
            <li>

                 <span style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">Enter Employee Name or Code:</span><br />
                <asp:TextBox ID="txtempname" runat="server"  ToolTip="Enter employee name or code" Visible="false" CssClass="txtcls"></asp:TextBox>
                <asp:TextBox ID="txtempnameNonHr" runat="server"  ToolTip="Enter employee name or code " Visible="false" CssClass="txtcls" ></asp:TextBox>
                </li>
            <li style="display:none;">

                <span style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">Enter no. Of Regularization Days:</span><br />
                <asp:TextBox ID="txtnoofDays" runat="server" MaxLength="2" CssClass="txtcls" ToolTip="Enter no.of Regularization days for search" autocomplete="off"></asp:TextBox>
            </li>

        </ul>
       

        <ul>
            <li>
                 <span style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">From Month:</span><br />
                <asp:TextBox ID="txtFrmDate" runat="server" MaxLength="10"  autocomplete="off" CssClass="txtcls" contentEditable="false"></asp:TextBox>


                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" OnClientHidden="onCalendarHidden" OnClientShown="onCalendarShown" TargetControlID="txtFrmDate"
                    Format="MMM/yyyy" runat="server" BehaviorID="calendar1">
                </ajaxToolkit:CalendarExtender>
            </li>

            <li>
                 <span style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">To Month:</span><br />
                <asp:TextBox ID="txtToDate" runat="server" MaxLength="10"  autocomplete="off" CssClass="txtcls" ></asp:TextBox>



                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" OnClientHidden="onCalendarHidden1" OnClientShown="onCalendarShown1" TargetControlID="txtToDate"
                    Format="MMM/yyyy" runat="server" BehaviorID="calendar2">
                </ajaxToolkit:CalendarExtender>

            </li>


        </ul>

         <ul>

             <li>
                   <asp:LinkButton ID="btnSave" runat="server" Text="View Leave Report" ToolTip="Leave Report" CssClass="leaverpt" OnClick="btnSave_Click">Generate Report</asp:LinkButton>
             </li>
        </ul>
    </div>
     <asp:DropDownList ID="ddlYear" runat="server" TabIndex="0" Visible="false"></asp:DropDownList>

   <%-- <table style="width: 100%; margin-bottom: 25px;" class="table table-dark">
        <tr>
            <td style="text-align: left"></td>
        </tr>
        <tr>


            <td>
              
                <br />
                

            </td>


            <td>
                
            </td>


            <td>

                
            </td>
            <td></td>

        </tr>
        <tr>
            <td></td>
        </tr>
        <tr>
            <td></td>
        </tr>
        <tr>



            <td>

               



            </td>



            <td>
               


            </td>
            <td>
              
            </td>
            <td style="display: none;">
               
            </td>




        </tr>
    </table>--%>



    <div style="width: 100%; overflow: auto">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="300px"
            Width="80%" ShowBackButton="False" SizeToReportContent="True"
            ShowCredentialPrompts="False" ShowDocumentMapButton="False"
            ShowPageNavigationControls="False" ShowRefreshButton="False">
        </rsweb:ReportViewer>
    </div>
    <asp:HiddenField ID="hdnloginempcode" runat="server" />
    <asp:HiddenField ID="hdnSearchempcode" runat="server" />
    <asp:HiddenField ID="hdnGrade" runat="server" />
    <asp:HiddenField ID="hdnIsHr" runat="server" />


    <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchEmployees" MinimumPrefixLength="1"
        CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtempname"
        ID="AutoCompleteExtender2" runat="server" FirstRowSelected="true">
    </ajaxToolkit:AutoCompleteExtender>

    <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchEmployeesNonHR" MinimumPrefixLength="1"
        CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtempnameNonHr"
        ID="AutoCompleteExtender1" runat="server" FirstRowSelected="true">
    </ajaxToolkit:AutoCompleteExtender>

    <script type="text/javascript">

        function onCharOnlyNumber(e) {
            var keynum;
            var keychar;
            var numcheck = /[0123456789/]/;

            if (window.event) {
                keynum = e.keyCode;
            }
            else if (e.which) {
                keynum = e.which;
            }
            keychar = String.fromCharCode(keynum);
            return numcheck.test(keychar);
        }



        function onCalendarShown() {

            var cal = $find("calendar1");
            //Setting the default mode to month
            cal._switchMode("months", true);

            //Iterate every month Item and attach click event to it
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }

        function onCalendarHidden() {
            var cal = $find("calendar1");
            //Iterate every month Item and remove click event from it
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }

        }


        function onCalendarShown1() {

            var cal = $find("calendar2");
            //Setting the default mode to month
            cal._switchMode("months", true);

            //Iterate every month Item and attach click event to it
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call1);
                    }
                }
            }
        }

        function onCalendarHidden1() {
            var cal = $find("calendar2");
            //Iterate every month Item and remove click event from it
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call1);
                    }
                }
            }

        }

        function call(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("calendar1");
                    cal._visibleDate = target.date;
                    cal.set_selectedDate(target.date);
                    cal._switchMonth(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged();
                    break;
            }
        }

        function call1(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("calendar2");
                    cal._visibleDate = target.date;
                    cal.set_selectedDate(target.date);
                    cal._switchMonth(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged();
                    break;
            }
        }


    </script>
</asp:Content>


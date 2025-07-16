<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="Fuel_Report_Monthly.aspx.cs" Inherits="myaccount_FuelReimbMonthlyReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leaveteamrpt.css" type="text/css" media="all" />
    <style type="text/css">
        .completionList {
            border: solid 1px Gray;
            margin: 0px;
            padding: 3px;
            height: 120px;
            overflow: auto;
            background-color: #FFFFFF;
        }

        .listItem {
            color: #191919;
        }

        .itemHighlighted {
            background-color: #ADD6FF;
        }
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
            <asp:Label ID="lblheading" runat="server" Text="Fuel Claims Report"></asp:Label>
        </span>
    </div>

    <div class="leavegrid">
        <a href="Fuel.aspx" class="aaa">Fuel Claim Home</a>
    </div>

    <div class="edit-contact">
        <ul>
            <li>
                 <span style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">Enter Employee Name:</span><br />
                    <asp:TextBox ID="txtemp" runat="server" Width="200px" CssClass="txtcls" TabIndex="0" ClientIDMode="Static" > </asp:TextBox>  
                    <ajaxToolkit:AutoCompleteExtender ID="auto1" runat="server" TargetControlID="txtemp"
                        MinimumPrefixLength="1" CompletionSetCount="10" ServiceMethod="GetEmployee" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                    </ajaxToolkit:AutoCompleteExtender>

            </li>
            <li>
                <span style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">Select Month:</span><br />
                     <asp:TextBox ID="txtMonth" runat="server" Width="200px" AutoComplete="off" AutoPostBack="False" CssClass="txtcls" TabIndex="1"   > </asp:TextBox>
                    
                       <ajaxToolkit:CalendarExtender ID="cal1" runat="server" OnClientHidden="onCalendarHidden" OnClientShown="onCalendarShown" TargetControlID="txtMonth" 
                           Format="MMM-yyyy" Enabled="true" BehaviorID="calendar1">
                    </ajaxToolkit:CalendarExtender>

            </li>
        </ul>
         <ul>
             <li>
                    <asp:LinkButton ID="btnSave" runat="server" Text="View Report" ToolTip="Generate Monthly Report" CssClass="leaverpt" OnClick="btnReport_Click">Generate Report</asp:LinkButton>
             </li>
        </ul>
    </div>

    <div style="width: 100%; overflow: auto">

        <rsweb:ReportViewer ID="ReportViewer2" runat="server" Height="300px"
            Width="80%" ShowBackButton="False" SizeToReportContent="True" ShowCredentialPrompts="False"
            ShowDocumentMapButton="False"
            ShowPageNavigationControls="False" ShowRefreshButton="False" ShowFindControls="False">
        </rsweb:ReportViewer>
    </div>


    <script type="text/javascript">

        function DisableCnt(id, disId) {
           
            var fld = document.getElementById(id).value;
            var DisFldId = document.getElementById(disId);


            if (fld.length > 1) {
                //alert('hi');
                DisFldId.disabled = true;

            }
            else {
               // alert('hi1');
                DisFldId.disabled = false;
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

        function ClearFields()
        {

            var emp = document.getElementById('txtemp');
            var mon = document.getElementById('txtMonth');

            emp.value = '';
            mon.value = '';
        }
    </script>
    <asp:HiddenField ID="hdnloginempcode" runat="server" />
</asp:Content>


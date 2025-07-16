<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="Mobile_Report.aspx.cs" Inherits="myaccount_MobilereimbursmentReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leaveteamrpt.css" type="text/css" media="all" />
    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    
       <div class="userposts">
            <span>
                <asp:Label ID="lblheading" runat="server" Text="Mobile Claims Report"></asp:Label>
            </span>
        </div>

        <div class="leavegrid">                    
             <a href="Mobile.aspx" class="aaa">Mobile Claim Home</a>
        </div>  
       <div style="width:100%;overflow:auto;align-content:flex-start" >      
            <div class="editprofileform">                            
                <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="false" Style="margin-left: 135px"></asp:Label>
            </div>
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
                        <asp:LinkButton ID="btnSave" runat="server" Text="View Report" ToolTip="Reimbursement Report" CssClass="leaverpt"  OnClick="btnSave_Click">Generate Report</asp:LinkButton>                        
             </li>
        </ul>
    </div>
    

        <div style="width:100%;overflow:auto">
        
        <rsweb:ReportViewer ID="ReportViewer2" runat="server" Height="300px"  
        Width="80%" ShowBackButton="False" SizeToReportContent="True" ShowCredentialPrompts="False"
        ShowDocumentMapButton="False" 
        ShowPageNavigationControls="False" ShowRefreshButton="False" ShowFindControls="False">

        </rsweb:ReportViewer>
        </div>

<asp:HiddenField ID="hdnloginempcode" runat="server" />

    <script type="text/javascript">
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
    </script>

</asp:Content>


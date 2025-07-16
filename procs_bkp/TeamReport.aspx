<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="TeamReport.aspx.cs" Inherits="TeamReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
      <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/ModifyLeaveRequest_css.css" type="text/css" media="all" />
   
    <%-- <link href="<%=ReturnUrl("css") %>calendar/calendar.css" rel="stylesheet" type="text/css" />
    <link href="sampleform_css.css" rel="stylesheet" type="text/css" />--%>
    <%--<ajaxToolkit:ToolkitScriptManager ID="srcp" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leaveteamrpt.css" type="text/css" media="all" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <script src="<%=ReturnUrl("hccurlmain")%>/js/gridviewscroll.js"></script>
    <link rel="stylesheet" href="<%=ReturnUrl("css")%>highbar/web.css" type="text/css" media="all" />

   
  <style type="text/css">
        .FixedHeader {
            position: absolute;
            font-weight: bold;
        }     

       
    </style>   
 <script type="text/javascript">
     var gridViewScroll = null;
     var height1 = ((62 * screen.height) / 800) ;
     var width1 = ((252 * screen.width) / 1280);

     if (window.screen.width >= 1800) {
         height1 = height1 * 6.3;
         width1 = width1 * 3;
         //width1 = width1 * 5.1;
     }
     if (window.screen.width >= 1400 && window.screen.width < 1800) {
         height1 = height1 * 6.3;
         width1 = width1 * 3.4;
         //width1 = width1 * 5.1;
     }
     if (window.screen.width >= 800 && window.screen.width < 1400) {
         height1 = height1 * 6.3;
         width1 = width1 * 4.4;
         //width1 = width1 * 5.1;
     }

     if (window.screen.width >= 600 && window.screen.width < 800)
     {
         height1 = height1 * 6.3;
         //width1 = width1 * 5;
         ////width1 = width1 * 4.2;
     }
     if (window.screen.width < 600 && window.screen.width > 480) {
         height1 = height1 * 6;
         //width1 = width1 * 4.2;
         ////width1 = width1 * 4;
     }
     if (window.screen.width <= 480) {
         height1 = height1 * 5;
         ////width1 = width1 * 4.3;
     }
    
     window.onload = function () {
         gridViewScroll = new GridViewScroll({
             elementID: "gvMain",
             width: width1,
             height: height1,
             freezeColumn: true,
             freezeFooter: false,
             freezeColumnCssClass: "GridViewScrollItemFreeze",
             freezeFooterCssClass: "GridViewScrollFooterFreeze",
             freezeHeaderRowCount: 1,
             freezeColumnCount: 1,
             onscroll: function (scrollTop, scrollLeft) {
                 console.log(scrollTop + " - " + scrollLeft);
             }
         });
         gridViewScroll.enhance();
     }
     function getScrollPosition() {
         var position = gridViewScroll.scrollPosition;
         alert("scrollTop: " + position.scrollTop + ", scrollLeft: " + position.scrollLeft);
     }
     function setScrollPosition() {
         var scrollPosition = { scrollTop: 50, scrollLeft: 50 };

         gridViewScroll.scrollPosition = scrollPosition;
     }
    </script>
     
        <div class="userposts">
            <span>
                <asp:Label ID="lblheading" runat="server" Text="Team Calendar"></asp:Label>
            </span>
        </div>
    <div class="leavegrid">                    
                    <a href="Leaves.aspx" class="aaa">Leave Menu</a>
     </div>    
    <table style="width:50%;" >
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
                <asp:Image ID="imgLeavePending" runat="server"
                    ImageUrl="~/images/Calendar/PendLeave.png" Height="13px" Width="25px" Style="margin-bottom: 0px" />
                 &nbsp;Pending Leave
            </td>
      </tr>
     <tr>
            <td class="anlign">
                <asp:Image ID="imgLeaveApproved" runat="server"
                    ImageUrl="~/images/Calendar/ApprLeave.png" Height="13px" Width="25px" Style="margin-bottom: 0px" />
                 &nbsp;Approved Leave
            </td>
      </tr>
        <tr> 
            <td class="anlign">
                <asp:Image ID="HolidayImg" runat="server"
                    ImageUrl="~/images/Calendar/Holiday.png" Height="13px" Width="25px" />
                 &nbsp;Holiday                   
                           
            </td>
        </tr>
<%--       <tr>   
            <td class="anlign">
                <asp:Image ID="imgLeaveReject" runat="server"
                    ImageUrl="~/images/Calendar/Correction.png" Height="13px" Width="25px" Style="margin-bottom: 0px" />
                 &nbsp;Correction Leaves
            </td>
       </tr>
            <tr>       
            <td class="anlign">
                <asp:Image ID="Image1" runat="server"
                    ImageUrl="~/images/Calendar/Pending_leaves_team.png" Height="13px" Width="25px" Style="margin-bottom: 0px" />
                 &nbsp;Pending Travels
            </td>
        </tr>

        <tr>
            <td class="anlign">
                <asp:Image ID="imgApproveTravels" runat="server"
                    ImageUrl="~/images/Calendar/approveTravels.png" Height="13px" Width="25px" Style="margin-bottom: 0px" />
                 &nbsp;Approved Travels
            </td>
        </tr>--%>
  
    </table>
     <br />
    <div style="width:100%;overflow:auto;align-content:flex-start" >
        <span>Select Month</span>
        <asp:TextBox ID="txtRequest_Date" AutoComplete="off" runat="server" Width="150px" MaxLength="10"></asp:TextBox>
        <asp:RadioButton runat="server" class="form-check-input" ID="RdDirect" Text="Direct Reportees" Checked="true" GroupName="optradio" />
        <asp:RadioButton runat="server" class="form-check-input" ID="RdAll" Text="All Reportees" GroupName="optradio" />


       <%-- <div class="Savebtndiv">--%>
              <asp:LinkButton ID="btnSave" runat="server" Text="Download Report" ToolTip="Save" CssClass="Savebtnsve"  OnClick="btnSave_Click">Download Report</asp:LinkButton>
              <asp:Button ID="btnExport" runat="server" Text="Export To Excel"  style="background-color:#febf39;font-size: 12px;" OnClick = "ExportToExcel" />
        <%--</div>--%>
           
       <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  OnClientHidden="onCalendarHidden"  OnClientShown="onCalendarShown" TargetControlID="txtRequest_Date"
                             Format="MMM/yyyy"    runat="server" BehaviorID="calendar1">
                            </ajaxToolkit:CalendarExtender>
         <%--<a href="Leaves.aspx" class="aaa">Leave Menu</a>--%>
         
    </div>
     <asp:PlaceHolder ID = "PlaceHolder1" runat="server" />
  <%--  <div style="width:100%;overflow:auto">--%>
        <div id="div_gridholder" style="height:180px; width:100%; overflow:auto;">
        <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" 
        GridLines="Both" style="display:none;"  Width="100%"
        AutoGenerateColumns="true" OnRowDataBound="GV_RowDataBound" ></asp:GridView>
   
        </div>


   

<asp:HiddenField ID="hdnloginempcode" runat="server" />

    
    <script type="text/javascript">
        function textboxMultilineMaxNumber(txt, maxLen) {
            try {
                if (txt.value.length > (maxLen - 1)) return false;
            } catch (e) {
            }
        }


        function maxLengthPaste(field, maxChars) {
            event.returnValue = false;
            if ((field.value.length + window.clipboardData.getData("Text").length) > maxChars) {
                return false;
            }
            event.returnValue = true;
        }

        function Count(text) {
            var maxlength = 250;
            var object = document.getElementById(text.id)
            if (object.value.length > maxlength) {
                object.focus();
                object.value = text.value.substring(0, maxlength);
                object.scrollTop = object.scrollHeight;
                return false;
            }
            return true;
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

        var d = new Date();
        var monthArray = new Array();
        monthArray[0] = "January";
        monthArray[1] = "February";
        monthArray[2] = "March";
        monthArray[3] = "April";
        monthArray[4] = "May";
        monthArray[5] = "June";
        monthArray[6] = "July";
        monthArray[7] = "August";
        monthArray[8] = "September";
        monthArray[9] = "October";
        monthArray[10] = "November";
        monthArray[11] = "December";
        for (m = 0; m <= 11; m++) {
            var optn = document.createElement("OPTION");
            optn.text = monthArray[m];
            // server side month start from one
            optn.value = (m + 1);

            // if june selected
            if (m == 5) {
                optn.selected = true;
            }

            document.getElementById('txtFromdate1').options.add(optn);
        }


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

        function onCharOnlyNumber(e) {
            var keynum;
            var keychar;
            var numcheck = /[0123456789.]/;

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

</asp:Content>

 

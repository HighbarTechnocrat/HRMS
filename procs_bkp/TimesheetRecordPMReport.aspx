<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="TimesheetRecordPMReport.aspx.cs" Inherits="TimesheetRecordPMReport" EnableEventValidation="false" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>
<%@ Register TagName="calender" TagPrefix="ucical" Src="~/themes/creative1.0/LayoutControls/leaveCalender.ascx" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
     <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <%--<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/ModifyLeaveRequest_css.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Report.css" type="text/css" media="all" />
    
	

	<style>
        #content-container #gvMain {
            width: 215% !important;
        }
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }


        .Dropdown {
            border-bottom: 2px solid #cccccc;
            /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
            background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;
            cursor: default;
        }

         .grayDropdown {
            border-bottom: 2px solid #cccccc;
            /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
            background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;            
            background-color:#ebebe4;
        }

         .grayDropdownTxt
         {            
            background-color:#ebebe4;
            }

        .taskparentclass2 {
            width: 29.5%;
            height: 112px;
        }

        .taskparentclasskkk {
            width: 29.5%;
            height: 72px;
            /*overflow:initial;*/
        }

        #MainContent_ListBox1, #MainContent_ListBox2 {
            padding: 0 0 0% 0 !important;
            /*overflow: unset;*/
        }

        #MainContent_lstApprover, #MainContent_lstIntermediate {
            padding: 0 0 33px 0 !important;
            /*overflow: unset;*/
        }

         .noresize {
            resize: none;
        }     

    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("hccurlmain")%>/js/gridviewscroll.js"></script>
    <link rel="stylesheet" href="<%=ReturnUrl("css")%>highbar/web.css" type="text/css" media="all" />
      <style type="text/css">
        .FixedHeader {
            position: absolute;
            font-weight: bold;
        }     
        #content-container #gvMain {
    width: 281% !important;
}
       
    </style>   
 <script type="text/javascript">
     var gridViewScroll = null;
     var height1 = ((62 * screen.height) / 800);
     var width1 = ((230 * screen.width) / 1280);

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

     if (window.screen.width >= 600 && window.screen.width < 800) {
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
             freezeHeaderRowCount: 0,
             freezeColumnCount: 2,
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
    <script type="text/javascript">
        var deprt;
        $(document).ready(function () {
            $("#MainContent_txtcity").autocomplete("<%=ReturnUrl("sitepathmain") %>Search_City.ashx", {
                inputClass: "ac_input",
                resultsClass: "ac_results",
                loadingClass: "ac_loading",
                minChars: 1,
                delay: 1,
                matchCase: false,
                matchSubset: false,
                matchContains: false,
                cacheLength: 0,
                max: 12,
                mustMatch: false,
                extraParams: {},
                selectFirst: false,
                formatItem: function (row) { return row[0] },
                formatMatch: null,
                autoFill: false,
                width: 0,
                multiple: false,
                scroll: false,
                scrollHeight: 180,
                success: function (data) {
                    response($.map(data, function (item) {
                        return {

                        }
                    }));
                },
                context: this
            });

            $("#MainContent_txtloc").autocomplete("<%=ReturnUrl("sitepathmain") %>Search_location.ashx", {
                inputClass: "ac_input",
                resultsClass: "ac_results",
                loadingClass: "ac_loading",
                minChars: 1,
                delay: 1,
                matchCase: false,
                matchSubset: false,
                matchContains: false,
                cacheLength: 0,
                max: 12,
                mustMatch: false,
                extraParams: {},
                selectFirst: false,
                formatItem: function (row) { return row[0] },
                formatMatch: null,
                autoFill: false,
                width: 0,
                multiple: false,
                scroll: false,
                scrollHeight: 180,
                success: function (data) {
                    response($.map(data, function (item) {
                        return {

                        }
                    }));
                },
                context: this
            });


            $("#MainContent_txtsubdept").autocomplete("<%=ReturnUrl("sitepathmain") %>Search_subdepartment.ashx?d=" + deprt, {
                inputClass: "ac_input",
                resultsClass: "ac_results",
                loadingClass: "ac_loading",
                minChars: 1,
                delay: 1,
                matchCase: false,
                matchSubset: false,
                matchContains: false,
                cacheLength: 0,
                max: 12,
                mustMatch: false,
                extraParams: { d: deprt },
                selectFirst: false,
                formatItem: function (row) { return row[0] },
                formatMatch: null,
                autoFill: false,
                width: 0,
                multiple: false,
                scroll: false,
                scrollHeight: 180,
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                        }
                    }));
                },

                context: this
            });
        });

    </script>
    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Team Deployment Report"></asp:Label>
                    </span>
                  
                    <%--PostBackUrl="~/procs/Index.aspx"--%>
                </div>
                <div>
                      <span>
                     <a href="Timesheet.aspx" class="aaaa" >Timesheet Menu</a>
                </span>
                </div>
             
                <div class="edit-contact">
                   
                    <ul id="editform" runat="server" visible="true">

                        <li>
                            <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                            <%--<asp:TextBox runat="server" ID="lblmessage1" Visible="False" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;" OnTextChanged="lblmessage1_TextChanged"></asp:TextBox>--%>
                        </li>
                        <li></li>
                        <li><span>Select Month Year</span>&nbsp;&nbsp;<span style="color:red">*</span> <br />
                            <asp:DropDownList runat="server"  ID="ddlMonthYear" AutoPostBack="true" OnSelectedIndexChanged="ddlMonthYear_SelectedIndexChanged"></asp:DropDownList></li>
                        <li></li>
                        <li class="date" style="padding-bottom:15px">
                            <br />
                            <span>From Date</span> &nbsp;&nbsp;      <span style="color:red">*</span>        
                                 <asp:TextBox ID="txtFromdate" Enabled="false"  CssClass="txtcls"  AutoComplete="off" runat="server" AutoPostBack="true" OnTextChanged="txtFromdate_TextChanged" MaxLength="10" AutoCompleteType="Disabled"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy"  TargetControlID="txtFromdate"   runat="server">
                                </ajaxToolkit:CalendarExtender>
							
                        </li>
                        <li class="date" style="padding-bottom:15px">
                            <br />
                            <span>To Date</span> &nbsp;&nbsp;    <span style="color:red">*</span>    
                                 <asp:TextBox ID="txtToDate"  AutoComplete="off" CssClass="txtcls" runat="server" AutoPostBack="true" OnTextChanged="txtFromdate_TextChanged" MaxLength="10" Enabled="false" AutoCompleteType="Disabled"></asp:TextBox>
                                
                        </li>
                         <li class="leavedays" runat="server" visible="true">

                            <span>Select Project</span>&nbsp;&nbsp;<br />
                            <asp:DropDownList runat="server" Enabled="false"  ID="ddlProject" AutoPostBack="false"></asp:DropDownList>
                            <asp:TextBox runat="server" ID="txtProject" Visible="false"></asp:TextBox>
                        </li>
                        <li></li>
                        <li>
                            <br />
                            <span>Reportees</span>&nbsp;&nbsp;<br />
                            <asp:DropDownList runat="server" ID="Reportees" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="Reportees_SelectedIndexChanged">
                                <asp:ListItem Value="All" Selected="True">All</asp:ListItem>
                                <asp:ListItem Value="MR">My Reportee</asp:ListItem>
                                <asp:ListItem Value="ROFF">Reportees Off</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li>
                            <br />
                            <span>Select Employee for Reportees Of</span>&nbsp;&nbsp;<br />
                            <asp:DropDownList CssClass="dropdownRequestList" Enabled="false" runat="server"  ID="Reporteesoff" AutoPostBack="true" OnSelectedIndexChanged="Reporteesoff_SelectedIndexChanged" >
                                <asp:ListItem Value="0"  Selected="True">Employee for Reportees Of </asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li>
                            <br />
                            <span>Direct/ All / An Employee</span>&nbsp;&nbsp;<br />
                            <asp:DropDownList  runat="server" ID="ddlDirectALLR" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlDirectALLR_SelectedIndexChanged">
                                <%--<asp:ListItem Value="All" Selected="True">All</asp:ListItem>--%>
                                <asp:ListItem Value="ALLR">All Reportees</asp:ListItem>
                                <asp:ListItem Value="DR">Direct Reportees</asp:ListItem>                                
                                <asp:ListItem Value="ANEMP">An Employee</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox runat="server" Visible="false" ID="TextBox2"></asp:TextBox>
                        </li>
                        <li class="leavedays" runat="server" visible="true">
                            <br />
                           <span>Select Employee</span>&nbsp;&nbsp;<br />
                            <asp:DropDownList  runat="server" Enabled="false" AutoPostBack="false" ID="ddlEmployee">
                                <asp:ListItem Value="0" Selected="True">Select Employee</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox runat="server" Visible="false" ID="txtTask"></asp:TextBox>
                        </li>
                        <li class="leavedays" runat="server" visible="false" id="li1" >
                          
                        </li>
                        <li>
                           <br /><br />
                            <asp:LinkButton ID="btnIn" runat="server" Visible="true" CssClass="Savebtnsve" Text="Generate Report" OnClick="btnIn_Click" >Generate Report</asp:LinkButton>
                        </li>
                        <li></li>
                        
                        
                    </ul>
                </div>
                <div style="width: 100%; overflow: auto">

                   <rsweb:reportviewer id="ReportViewer2" runat="server"  height="700px"
                        Width="100%" ShowBackButton="False" SizeToReportContent="false" 
        ShowCredentialPrompts="False" ShowDocumentMapButton="False" 
        ShowPageNavigationControls="true" ShowRefreshButton="False" ShowExportControls="true"
	 PageCountMode="Actual">

        </rsweb:reportviewer>
                </div>
            </div>
        </div>
    </div>
    <div class="Savebtndiv">
      
    </div>
    <div>
        
    </div>
    <asp:TextBox ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>

    <asp:TextBox ID="TextBox1" runat="server" MaxLength="50" Visible="false" AutoPostBack="true" > </asp:TextBox>

    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />

    <asp:HiddenField ID="hflEmpName" runat="server" />

    <asp:HiddenField ID="hflEmpDesignation" runat="server" />

    <asp:HiddenField ID="hflEmpDepartment" runat="server" />

    <asp:HiddenField ID="hflEmailAddress" runat="server" />

    <asp:HiddenField ID="hflGrade" runat="server" />

    <asp:HiddenField ID="hflapprcode" runat="server" />

    <asp:HiddenField ID="hdnFromFor" runat="server" />


    <asp:HiddenField ID="hdlDate" runat="server" />
    
    <asp:HiddenField ID="hdnleaveconditiontypeid" runat="server" />
    <asp:HiddenField ID="htnleavetypeid" runat="server" />
    <asp:HiddenField ID="hdnleavedays" runat="server" />

    <asp:HiddenField ID="hdnlstfromfor" runat="server" />
    <asp:HiddenField ID="hdnlsttofor" runat="server" />
    <asp:HiddenField ID="hdnToDate" runat="server" />
    <asp:HiddenField ID="hdnReqid" runat="server" />
    <asp:HiddenField ID="hdnOldLeaveCount" runat="server" />
    <asp:HiddenField ID="hdnLeaveStatus" runat="server" />
    <asp:HiddenField ID="hflLeavestatus" runat="server" />
    <asp:HiddenField ID="hflstatusid" runat="server" />
    <asp:HiddenField ID="hdnAppr_status" runat="server" />

    <asp:HiddenField ID="hdnmsg" runat="server" />
    <asp:HiddenField ID="hdnfrmdate_emial" runat="server" />
    <asp:HiddenField ID="hdntodate_emial" runat="server" />
    <asp:HiddenField ID="hdnHRMailId_MLLWP" runat="server" />
    <asp:HiddenField ID="hdnPLwithSL_succession" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="hdnInTime" runat="server" />
    <asp:HiddenField ID="hdnOutTime" runat="server" />
    <asp:HiddenField ID="hdnisTimeInShow" runat="server" />
    <asp:HiddenField ID="hdnisTimeoutShow" runat="server" />
    <asp:HiddenField ID="hdnId" runat="server" />
    <asp:HiddenField ID="hdnStartDate" runat="server" />
    <asp:HiddenField ID="hdnEndDate" runat="server" />
    <asp:HiddenField ID="hdnDay" runat="server" />
   <%-- <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchProject" MinimumPrefixLength="1"
        CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtProject"
        ID="AutoCompleteExtender1" runat="server" FirstRowSelected="true">
    </ajaxToolkit:AutoCompleteExtender>

    <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchTask" MinimumPrefixLength="1"
        CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtTask"
        ID="AutoCompleteExtender2" runat="server" FirstRowSelected="true">
    </ajaxToolkit:AutoCompleteExtender>--%>
        <script src="../js/dist/jquery-3.2.1.min.js"></script>       
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" /> 
    
    <script type="text/javascript">      
        $(document).ready(function () {
            $("#MainContent_ddlProject").select2();
            $("#MainContent_Reportees").select2();
            $("#MainContent_Reporteesoff").select2();
            $("#MainContent_ddlDirectALLR").select2();
            $("#MainContent_ddlEmployee").select2();
            $("#MainContent_ddlMonthYear").select2();

        });
    </script>
    <script type="text/javascript">
        //$(document).ready(function () {
        //    ShowTime();});
        var xmlHttp;
        function srvTime() {
            try {
                //FF, Opera, Safari, Chrome
                xmlHttp = new XMLHttpRequest();
            }
            catch (err1) {
                //IE
                try {
                    xmlHttp = new ActiveXObject('Msxml2.XMLHTTP');
                }
                catch (err2) {
                    try {
                        xmlHttp = new ActiveXObject('Microsoft.XMLHTTP');
                    }
                    catch (eerr3) {
                        //AJAX not supported, use CPU time.
                        alert("AJAX not supported");
                    }
                }
            }
            xmlHttp.open('HEAD', window.location.href.toString(), false);
            xmlHttp.setRequestHeader("Content-Type", "text/html");
            xmlHttp.send('');
            return xmlHttp.getResponseHeader("Date");
        }
        function addZero(i) {
            if (i < 10) {
                i = "0" + i;
            }
            return i;
        }
    
        //window.setTimeout("ShowTime()", 10);
        function validateFromFor(leavetypeid, leavetypeFSH, tt, todate, fromdate, msg) {

            document.getElementById("<%=lblmessage.ClientID%>").innerHTML = msg;
            return;
        }

        function validateToFor(tt, msg) {

            document.getElementById("<%=lblmessage.ClientID%>").innerHTML = msg;
            return;
        }

        function textboxMultilineMaxNumber(txt, maxLen) {
            try {
                if (txt.value.length > (maxLen - 1)) return false;
            } catch (e) {
            }
        }

        function validateLeaveType(leavetypeid)
        {
            document.getElementById("<%=txtFromdate.ClientID%>").value = "";
            document.getElementById("<%=lblmessage.ClientID%>").innerHTML = "";
            document.getElementById("<%=hdnlstfromfor.ClientID%>").value = "";
            document.getElementById("<%=hdnlsttofor.ClientID%>").value = "";
            document.getElementById("<%=hdnToDate.ClientID%>").value = "";
            document.getElementById("<%=hdnleavedays.ClientID%>").value = "";
            document.getElementById("<%=txtFromdate.ClientID%>").disabled = false;
            return;
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
        function onCharOnlyNumber_Time(e) {
            var keynum;
            var keychar;
            var numcheck = /[0123456789:]/;

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
            var numcheck = /[0123456789./]/;

            if (window.event) {
                keynum = e.keyCode;
            }
            else if (e.which) {
                keynum = e.which;
            }
            keychar = String.fromCharCode(keynum);
            return numcheck.test(keychar);
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
      
        function SaveInClick() {
            try {

                var retunboolean = true;
                var ele = document.getElementById('<%=btnIn.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        ConfirmIn();
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        function ConfirmIn() {
            //Testing();
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Submit ?")) {
                confirm_value.value = "Yes";               
            } else {
                confirm_value.value = "No";
                document.getElementById("<%= hdnInTime.ClientID %>").value = '';
                document.getElementById("<%= hdnOutTime.ClientID %>").value = '';
            }
            //alert(confirm_value.value);
            //document.forms[0].appendChild(confirm_value);
            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return;

        }

    </script>
    
</asp:Content>

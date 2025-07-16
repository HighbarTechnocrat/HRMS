<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="CreateEmployeeTransferReq.aspx.cs" Inherits="CreateEmployeeTransferReq" EnableEventValidation="false" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>
<%@ Register TagName="calender" TagPrefix="ucical" Src="~/themes/creative1.0/LayoutControls/leaveCalender.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/ModifyLeaveRequest_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />

    <style>
        #MainContent_btnIn {
            background-attachment: scroll;
            background-clip: border-box;
            /*by Highbartech on 24-06-2020
        background: #febf39;*/
            background-color: #3D1956;
            color: #febf39 !important;
            background-image: none;
            background-origin: padding-box;
            background-position-x: 0;
            background-position-y: 0;
            background-repeat: repeat;
            background-size: auto auto;
            padding-bottom: 8px;
            padding-left: 23px;
            padding-right: 23px;
            padding-top: 8px;
        }

        #content-container #gvMain {
            width: 231% !important;
        }

        #MainContent_icalender_Calendar1 {
            color: Black;
            border-color: #2A2A2A;
            border-width: 1px;
            border-style: solid;
            font-size: 14px;
            font-weight: normal;
            text-decoration: none;
            width: 194px;
            border-collapse: collapse;
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
            background-color: #ebebe4;
        }

        .grayDropdownTxt {
            background-color: #ebebe4;
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
    <script type="text/javascript">
        var deprt;
        $(document).ready(function () {

        });

    </script>
    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Create Employee Transfer Request"></asp:Label>
                    </span>

                    <%--PostBackUrl="~/procs/Index.aspx"--%>
                </div>
                <div>
                    <span>
                        <a href="EmployeeTransfer.aspx" class="aaaa">Employee Transfer</a>
                    </span>
                </div>
                <div class="leavegrid" style="display: none;">
                </div>

                <div class="edit-contact">

                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                    </div>

                    <div class="editprofile" id="editform1" runat="server" visible="true" style="margin: 7px -12% !important;">
                        <div class="editprofileform">
                        </div>
                    </div>

                    <ul id="editform" runat="server" visible="false">

                        <li>
                            <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                            <%--<asp:TextBox runat="server" ID="lblmessage1" Visible="False" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;" OnTextChanged="lblmessage1_TextChanged"></asp:TextBox>--%>
                        </li>
                        <li></li>


                        <li style="width: 78%">
                            <br />
                            <span id="IMPMsg" runat="server" style="color: red">Note: Employees will get transferred to the new project after 2 days</span>
                        </li>



                        <li class="leavedays"> 
                            <span>Select New Project/Location</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                            <asp:DropDownList runat="server" ID="ddlProject" AutoPostBack="true" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged"></asp:DropDownList>
                            <asp:TextBox runat="server" Visible="false" ID="txtProject"></asp:TextBox>
                        </li>
                         <li class="leavedays"> 
                            <span>Actual Transfer Date</span><br />
                            <asp:TextBox ID="txtFromdate" Enabled="false" AutoComplete="off" runat="server" AutoPostBack="true" MaxLength="10" AutoCompleteType="Disabled"></asp:TextBox>
                        </li>

                         <li class="leavedays" id="liSelectedShift_1" runat="server" visible="false">                              
                            <span>Selected New Shift</span>&nbsp;&nbsp;<br />
                            <asp:DropDownList runat="server" Visible="false" ID="ddlTask"></asp:DropDownList>
                            <asp:TextBox runat="server" Visible="false" Enabled="false" ID="txtTask"></asp:TextBox>
                            <span runat="server" style="color: blue !important;" id="lblShift"></span>                             
                        </li>
                        <li class="leavedays" id="liSelectedShift_2" runat="server" visible="false"></li>


                        <li class="date" id="hide1" runat="server">
                            <br />
                            <span>Select & Add Employees For Transfer </span> &nbsp;&nbsp;<span style="color: red">*</span>
                            <asp:DropDownList runat="server" ID="ddlEmployee"></asp:DropDownList>
                        </li>
                        <li class="date" id="hide3" runat="server">
                            <br />
                            <span>Select & Add Deployment Location </span> &nbsp;&nbsp;<span style="color: red">*</span>
                            <asp:DropDownList runat="server" ID="DDLDeploymentType"></asp:DropDownList>
                        </li>


                        <li class="date" id="libench_Reason_1" runat="server" visible="false">
                            <br />
                            <span>Select Bench Reason For Transfer </span> &nbsp;&nbsp;<span style="color: red">*</span>
                            <asp:DropDownList runat="server" ID="ddlbenchReasonsList"></asp:DropDownList>
                        </li>
                        <li class="date" id="libench_Reason_2" runat="server" visible="false">
                           
                            <br />
                              <div id="idReportingManager" runat="server" visible="false">
                            <span>Select Reporting Manager for Bench Employee </span> &nbsp;&nbsp;<span style="color: red">*</span>
                            <asp:DropDownList runat="server" ID="ddlbenchReportingManager" CssClass="ddlSearch"></asp:DropDownList>
                            </div>
                        </li>

                         <li class="date" id="libench_Reason_Remarks_1" visible="false" runat="server">
                            <br />
                            <span>Remarks for Bench Transfer *</span> &nbsp;&nbsp;<span style="color: red">*</span>
                            <asp:TextBox ID="txtBenchRemsrkas" AutoComplete="off" runat="server"   MaxLength="10" TextMode="MultiLine" AutoCompleteType="Disabled"></asp:TextBox>
                        </li>
                        <li class="date" id="libench_Reason_Remarks_2" visible="false" runat="server">
                           
                        </li>





                        <li class="leavedays" id="hide2" runat="server">
                            <br />
                            <asp:LinkButton ID="btnIn" runat="server" CssClass="Savebtnsve" Text="Add" OnClick="btnEdit_Click" OnClientClick="return SaveInClick();">Add</asp:LinkButton>
                        </li>
                        <li></li>
                        <li>
                            <br />
                            <br />
                            <span>Employees to be Transferred:</span></li>
                        <li></li>
                        <li class="Approver">
                            <%--<span>Approver </span>--%>
                            <br />
                            <asp:ListBox Visible="false" ID="lstApprover" runat="server"></asp:ListBox>
                            <asp:GridView ID="DgvApprover"  runat="server" BackColor="White" DataKeyNames="Id" BorderColor="Navy"
                                PageSize="100" AllowPaging="false"
                                BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="140%">
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
                                    <asp:BoundField HeaderText="Employee Code"
                                        DataField="Emp_Code"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="EmployeeName"
                                        DataField="Emp_Name"
                                        ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="15%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Module"
                                       DataField="MODULE"
                                        ItemStyle-HorizontalAlign="Center"
                                       HeaderStyle-HorizontalAlign="Center"
                                       ItemStyle-Width="8%" 
                                       ItemStyle-BorderColor="Navy"
                                       />

                                    <asp:BoundField HeaderText="Current Project"
                                        DataField="ProjectName"
                                        ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="12%"
                                        ItemStyle-BorderColor="Navy" />
                                    <asp:BoundField HeaderText="Current Shift"
                                        DataField="Shift_Name"
                                        ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%"
                                        ItemStyle-BorderColor="Navy" />
                                    <asp:BoundField HeaderText="Deployment Location"
                                        DataField="TypeName"
                                        ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />


                                    <asp:BoundField HeaderText="Bench Transfer Reason"
                                        DataField="Bench_Transfer_reason"
                                        ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%"
                                        ItemStyle-BorderColor="Navy"/>

                                    <asp:BoundField HeaderText="Bench Transfer Remarks"
                                        DataField="bench_remarks"
                                        ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="15%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Bench Employee RM"
                                        DataField="Bench_employee_RM"
                                        ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="15%"
                                        ItemStyle-BorderColor="Navy"/>


                                    <asp:TemplateField HeaderText="Reporting Manager" ItemStyle-Width="25%"
                                        HeaderStyle-Width="25%" ItemStyle-HorizontalAlign="center" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="DDlReportingManager" AutoPostBack="false" runat="server" CssClass="ddlSearch">
                                                <asp:ListItem Value="0">Select Reporting manager</asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:HiddenField runat="server" ID="Hddiploymenttype" Value='<%# Eval("DeploymentLocationTypeId") %>' />
                                            <asp:HiddenField runat="server" ID="hdnBenchReasonId" Value='<%# Eval("bench_reason") %>' />
                                            <asp:HiddenField runat="server" ID="hdnBenchRM" Value='<%# Eval("Bench_RM") %>' />
                                            

                                            <asp:ImageButton ID="lnkEdit" runat="server" Width="15px" Height="15px" ImageUrl="~/images/delete.png" OnClick="lnkEdit_Click" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </li>
                        <%--<li></li>--%>

                        <li>
                           
                        </li>
                        <li class="leavedays">
                            <br />
                            <br />
                            
                        </li>



                        <li class="leavedays">
                            
                        </li>
                        <li></li>
                        <li>
                            <br />
                            <asp:LinkButton ID="btnBack" runat="server" CssClass="Savebtnsve" Text="Create Transfer request" OnClick="btnBack_Click1" OnClientClick="return SaveOutClick();">Create Transfer Request</asp:LinkButton>
                            <asp:LinkButton ID="btnback_mng" runat="server" OnClick="btnback_mng_Click" Visible="false" Text="Cancel Transfer Request" ToolTip="Cancel Transfer Request" CssClass="Savebtnsve" OnClientClick="return CancelClick();">Cancel Transfer Request</asp:LinkButton>
                        </li>


                    </ul>
                </div>
            </div>
        </div>
    </div>
    
     
    <asp:TextBox ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>

    <asp:TextBox ID="TextBox1" runat="server" MaxLength="50" Visible="false" AutoPostBack="true"> </asp:TextBox>

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
    <asp:HiddenField ID="hdnFullDay" runat="server" />
    <asp:HiddenField ID="hdnHalfDay" runat="server" />
    <asp:HiddenField ID="HDCase" runat="server" />
    <asp:HiddenField ID="hdnIsDHLogin" runat="server" />
    <asp:HiddenField ID="HDCaseApproved" runat="server" />
    <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchProject" MinimumPrefixLength="3"
        CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtProject"
        ID="AutoCompleteExtender1" runat="server" FirstRowSelected="true">
    </ajaxToolkit:AutoCompleteExtender>

    <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchTask" MinimumPrefixLength="3"
        CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtTask"
        ID="AutoCompleteExtender2" runat="server" FirstRowSelected="true">
    </ajaxToolkit:AutoCompleteExtender>
    <script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <script type="text/javascript">      
        $(document).ready(function () {
            $("#MainContent_ddlProject").select2();
            $("#MainContent_ddlTask").select2();
            $("#MainContent_ddlEmployee").select2();
            $("#MainContent_DDLDeploymentType").select2();
            $("#MainContent_ddlbenchReasonsList").select2();
            
            $(".ddlSearch").select2();
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

        function validateLeaveType(leavetypeid) {

            document.getElementById("<%=lblmessage.ClientID%>").innerHTML = "";
            document.getElementById("<%=hdnlstfromfor.ClientID%>").value = "";
            document.getElementById("<%=hdnlsttofor.ClientID%>").value = "";
            document.getElementById("<%=hdnToDate.ClientID%>").value = "";
            document.getElementById("<%=hdnleavedays.ClientID%>").value = "";

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

        function SaveMultiClick() {
            try {

                var retunboolean = true;
                var ele = document.getElementById('<%=btnBack.ClientID%>');

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

        function SaveOutClick() {
            try {
                var msg = "Do you want to Create Transfer Request ?";
                var retunboolean = true;
                var ele = document.getElementById('<%=btnBack.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        ConfirmIn(msg);
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        function SaveInClick() {
            try {
                var msg = "Do you want to add employee & Deployment Location ?";
                var retunboolean = true;
                var ele = document.getElementById('<%=btnIn.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        ConfirmIn(msg);
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        function ConfirmIn(msg) {
            //Testing();
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm(msg)) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            //alert(confirm_value.value);
            //document.forms[0].appendChild(confirm_value);
            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return;

        }
        function CancelClick() {
            try {
                var msg = "Do you want to Cancel Transfer Request ?";
                var retunboolean = true;
                var ele = document.getElementById('<%=btnback_mng.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        ConfirmIn(msg);
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
    </script>

</asp:Content>

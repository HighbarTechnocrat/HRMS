<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="ODApplication_App.aspx.cs" Inherits="ODApplication_App" %>


<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>
<%@ Register TagName="calender" TagPrefix="ucical" Src="~/themes/creative1.0/LayoutControls/leaveCalender.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/ModifyLeaveRequest_css.css" type="text/css" media="all" />
    <%-- <link href="<%=ReturnUrl("css") %>calendar/calendar.css" rel="stylesheet" type="text/css" />
    <link href="sampleform_css.css" rel="stylesheet" type="text/css" />--%>
    <%--<ajaxToolkit:ToolkitScriptManager ID="srcp" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />

    <style>
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
    &nbsp;
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
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
                        <asp:Label ID="lblheading" runat="server" Text="OD Application"></asp:Label>
                    </span>

                    <%--PostBackUrl="~/procs/Index.aspx"--%>
                </div>
                <div class="leavegrid">
                    <%--<h3 id="hheadyear" runat="server">Leave Card - 2020</h3>--%>
                    <a href="Attendance.aspx" class="aaa">Attendance</a>
                </div>
                <div class="edit-contact">
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                    </div>
                    <%--    <div class="editprofile" id="editform1" runat="server" visible="true">
                        <div class="editprofileform">
                            <ucical:calender ID="icalender" runat="server"></ucical:calender>
                        </div>
                    </div>--%>
                    <ul id="editform" runat="server" visible="false">
                        <li>
                            <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                            <%--<asp:TextBox runat="server" ID="lblmessage1" Visible="False" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;" OnTextChanged="lblmessage1_TextChanged"></asp:TextBox>--%>
                        </li>
                        <li></li>


                        <li class="date">
                            <span>Emp Code</span>
                            <asp:TextBox ID="txtEmpCode" runat="server" Enabled="false"> </asp:TextBox>
                        </li>
                        <li class="leavedays">
                            <span>Emp Name</span>
                            <asp:TextBox ID="txtEmp_Name" Enabled="false" AutoComplete="off" runat="server"></asp:TextBox>
                        </li>

                        <li><span>Designation</span>
                            <asp:TextBox ID="txtEmp_Desigantion" Enabled="false" AutoComplete="off" runat="server"></asp:TextBox>
                        </li>

                        <li class="leavedays">

                            <span>Department</span>
                            <asp:TextBox ID="txtEmp_Department" Enabled="false" AutoComplete="off" runat="server"></asp:TextBox>
                        </li>
                        <li class="leavedays">
                            <span>Select Type</span>
                            <br />
                            <asp:DropDownList runat="server" ID="lstLeaveType" OnSelectedIndexChanged="lstLeaveType_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                            <br />
                        </li>
                        <li>
                            <span>Location / Project</span>
                            <br />
                            <asp:TextBox ID="txt_Project" Enabled="false" AutoComplete="off" runat="server"></asp:TextBox>
                            <asp:TextBox Visible="false" ID="txtLeaveType" AutoComplete="off" ReadOnly="true" runat="server" CssClass="Dropdown" OnTextChanged="txtLeaveType_TextChanged" Text="Outdoor Duty" AutoPostBack="true"></asp:TextBox>

                        </li>



                        <li class="date">

                            <span>From Date</span>
                            <asp:TextBox ID="txtFromdate" AutoComplete="off" runat="server" AutoPostBack="true" OnTextChanged="txtFromdate_TextChanged" MaxLength="10" AutoCompleteType="Disabled"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtFromdate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>
                        <li class="taskparentclass">
                            <span>For</span><br />

                            <div class="form-check-inline" runat="server" style="display: none">
                                <asp:RadioButton runat="server" class="form-check-input" ID="Fullday" Text="Full Day" AutoPostBack="true" OnCheckedChanged="Fullday_CheckedChanged" GroupName="optradio" />
                                <asp:RadioButton runat="server" class="form-check-input" ID="FirstHalf" Text="First Half" AutoPostBack="true" OnCheckedChanged="Fullday_CheckedChanged" GroupName="optradio" />
                                <asp:RadioButton runat="server" class="form-check-input" ID="SecondHalf" Text="Second Half" AutoPostBack="true" OnCheckedChanged="Fullday_CheckedChanged" GroupName="optradio" />
                            </div>
                            <asp:TextBox Visible="true" ID="txtFromfor" runat="server" AutoPostBack="true" OnTextChanged="txtFromfor_TextChanged"></asp:TextBox>
                        </li>

                        <li class="date">
                            <span>To Date</span><br />
                            <asp:TextBox ID="txtToDate" AutoComplete="off" runat="server" OnTextChanged="txtToDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>

                        <li class="fordate">
                            <span>For</span><br />
                            <div class="form-check-inline" runat="server" style="display: none">
                                <asp:RadioButton runat="server" class="form-check-input" ID="Radio1" Text="Full Day" AutoPostBack="true" OnCheckedChanged="Fullday_CheckedChanged" GroupName="Toradio" />
                                <asp:RadioButton runat="server" class="form-check-input" ID="Radio2" Text="First Half" AutoPostBack="true" OnCheckedChanged="Fullday_CheckedChanged" GroupName="Toradio" />
                            </div>
                            <asp:TextBox Visible="true" ID="txtToFor" runat="server" AutoComplete="off" AutoPostBack="true" OnTextChanged="txtToFor_TextChanged"></asp:TextBox>
                        </li>

                        <li class="leavedays" style="display: none;">
                            <span>Leave Days</span><br />
                            <asp:TextBox ID="txtLeaveDays" Enabled="false" runat="server"></asp:TextBox>
                        </li>

                        <li class="Reason">
                            <span>City</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_Citys" MaxLength="500" CssClass="noresize" TextMode="MultiLine" Rows="4" runat="server" Visible="True"></asp:TextBox>
                             <%--<asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>                        

                        </li>
                        <li class="Reason">
                            <span>Client</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_Client" MaxLength="500" CssClass="noresize" TextMode="MultiLine" Rows="4" runat="server" Visible="True"></asp:TextBox>
                            <%--<asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>                        

                        </li>
                        <li class="Reason">
                            <span>Employee Remarks</span>
                            <br />
                            <asp:TextBox ID="txtReason" AutoComplete="off" runat="server" MaxLength="30"></asp:TextBox>
                        </li>
                        <li class="Reason" id="HideRemark" runat="server">
                            <span>Remarks &nbsp;&nbsp <span style="color: red">*</span></span>
                            <br />
                            <asp:TextBox ID="txtRemark" AutoComplete="off" runat="server" MaxLength="30"></asp:TextBox>
                            <asp:TextBox ID="txtLeaveCancelReason" AutoComplete="off" runat="server" MaxLength="30" Visible="false"></asp:TextBox>
                        </li>
                        <li class="Approver">
                            <span>Approver History </span>
                            <br />
                            <br />
                            <asp:ListBox Visible="false" ID="lstApprover" runat="server"></asp:ListBox>
                            <asp:GridView ID="DgvApprover" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%">
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
                                    <asp:BoundField HeaderText="Approver Name"
                                        DataField="tName"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="left"
                                        ItemStyle-Width="25%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Status"
                                        DataField="Status"
                                        ItemStyle-HorizontalAlign="left"
                                        ItemStyle-Width="12%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Approved on"
                                        DataField="tdate"
                                        ItemStyle-HorizontalAlign="left"
                                        ItemStyle-Width="12%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Approver Remarks"
                                        DataField="Comment"
                                        ItemStyle-HorizontalAlign="left"
                                        ItemStyle-Width="46%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="APPR_ID"
                                        DataField="APPR_ID"
                                        ItemStyle-HorizontalAlign="center"
                                        ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy"
                                        Visible="false" />

                                    <asp:BoundField HeaderText="Emp_Name"
                                        DataField="Emp_Name"
                                        ItemStyle-HorizontalAlign="center"
                                        ItemStyle-Width="1%"
                                        ItemStyle-BorderColor="Navy"
                                        Visible="false" />

                                    <asp:BoundField HeaderText="Emp_Emailaddress"
                                        DataField="Emp_Emailaddress"
                                        ItemStyle-HorizontalAlign="center"
                                        ItemStyle-Width="1%"
                                        ItemStyle-BorderColor="Navy"
                                        Visible="false" />

                                    <asp:BoundField HeaderText="A_EMP_CODE"
                                        DataField="A_EMP_CODE"
                                        ItemStyle-HorizontalAlign="center"
                                        ItemStyle-Width="1%"
                                        ItemStyle-BorderColor="Navy"
                                        Visible="false" />
                                </Columns>
                            </asp:GridView>
                        </li>
                        
                        </ul>
                </div>
            </div>
        </div>
    </div>
    
    <div>
        <br />
        <asp:LinkButton ID="btnMod" runat="server" Text="Approve" ToolTip="Approve" CssClass="Savebtnsve" OnClick="btnMod_Click" OnClientClick="return SaveMultiClick_Modify();">Approve</asp:LinkButton>
        <asp:LinkButton ID="btnCancel" runat="server" Text="Reject" ToolTip="Reject" CssClass="Savebtnsve" OnClick="btnCancel_Click" OnClientClick="return CancelMultiClick();">Reject</asp:LinkButton>
        <asp:LinkButton ID="btnSave" Visible="false" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="Savebtnsve" OnClick="btnCancelAPP_Click" OnClientClick="return CancelAPPMultiClick();">Cancel</asp:LinkButton>
        <asp:LinkButton ID="btnback_mng" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="btnback_mng_Click">Back</asp:LinkButton>
    <asp:TextBox ID="TextBox1" runat="server" MaxLength="50" Visible="false" AutoPostBack="true"> </asp:TextBox>
</div>
    
    
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
    <asp:HiddenField ID="hdnA_Emp_Code" runat="server" />
    <asp:HiddenField ID="hdnAppr_id" runat="server" />
    <asp:HiddenField ID="Emp_Name" runat="server" />
    <asp:HiddenField ID="Emp_EmailAddress" runat="server" />
    <asp:HiddenField ID="hdnEmpEmail" runat="server" />
    <script type="text/javascript">

        function validateFromFor(leavetypeid, leavetypeFSH, tt, todate, fromdate, msg) {

            if (leavetypeFSH == "First Half") {

                document.getElementById("<%=txtToDate.ClientID%>").disabled = true;
                document.getElementById("<%=txtToFor.ClientID%>").disabled = true;
                document.getElementById("<%=txtToFor.ClientID%>").value = "";
            }
            else {

                document.getElementById("<%=txtToDate.ClientID%>").disabled = false;
                document.getElementById("<%=txtToFor.ClientID%>").disabled = false;
            }
            if (todate == fromdate) {

                document.getElementById("<%=txtToFor.ClientID%>").disabled = true;
                document.getElementById("<%=txtToFor.ClientID%>").value = "";
            }
            else {

                document.getElementById("<%=txtToFor.ClientID%>").disabled = false;

            }
            //}  
            if (leavetypeid == "5") {
                document.getElementById("<%=txtToFor.ClientID%>").disabled = true;
                document.getElementById("<%=txtToDate.ClientID%>").disabled = true;
            }

            if (todate != "") {
                document.getElementById("<%=txtToDate.ClientID%>").value = todate;
            }
            if (tt == "0") {
                document.getElementById("<%=txtLeaveDays.ClientID%>").value = "";
            }
            else {
                document.getElementById("<%=txtLeaveDays.ClientID%>").value = tt;
            }
            document.getElementById("<%=lblmessage.ClientID%>").innerHTML = msg;
            return;
        }

        function validateToFor(tt, msg) {

            document.getElementById("<%=txtLeaveDays.ClientID%>").value = tt;
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

            document.getElementById("<%=txtToDate.ClientID%>").value = "";
            document.getElementById("<%=txtFromdate.ClientID%>").value = "";
            document.getElementById("<%=txtLeaveDays.ClientID%>").value = "";
            document.getElementById("<%=lblmessage.ClientID%>").innerHTML = "";
            document.getElementById("<%=txtToFor.ClientID%>").value = "Full Day";
            document.getElementById("<%=txtFromfor.ClientID%>").value = "Full Day";
            document.getElementById("<%=txtReason.ClientID%>").value = "";
            document.getElementById("<%=hdnlstfromfor.ClientID%>").value = "";
            document.getElementById("<%=hdnlsttofor.ClientID%>").value = "";
            document.getElementById("<%=hdnToDate.ClientID%>").value = "";
            document.getElementById("<%=hdnleavedays.ClientID%>").value = "";

            document.getElementById("<%=txtFromfor.ClientID%>").disabled = false;
            document.getElementById("<%=txtFromdate.ClientID%>").disabled = false;
            document.getElementById("<%=txtToFor.ClientID%>").disabled = false;
            document.getElementById("<%=txtToDate.ClientID%>").disabled = false;

            if (leavetypeid == "5") {
                document.getElementById("<%=txtToFor.ClientID%>").disabled = true;
                document.getElementById("<%=txtToDate.ClientID%>").disabled = true;
            }
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

        function UploadFile(fileUpload) {
           <%-- if (fileUpload.value != '') {
                document.getElementById("<%=btnUpload.ClientID %>").click();
            }--%>
        }

        function SaveMultiClick_Modify() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=btnMod.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        Confirm();
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        function CancelMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=btnCancel.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        Confirm();
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        function CancelAPPMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=btnSave.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        Confirm();
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }

        function Confirm() {
            //Testing();
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Submit ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            //alert(confirm_value.value);
            //document.forms[0].appendChild(confirm_value);
            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return;

        }

    </script>

</asp:Content>

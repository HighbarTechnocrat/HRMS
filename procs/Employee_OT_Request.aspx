<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="Employee_OT_Request.aspx.cs" Inherits="Employee_OT_Request" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/ModifyLeaveRequest_css.css" type="text/css" media="all" />
    <%-- <link href="<%=ReturnUrl("css") %>calendar/calendar.css" rel="stylesheet" type="text/css" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leaveteamrpt.css" type="text/css" media="all" />
    <link href="sampleform_css.css" rel="stylesheet" type="text/css" />--%>
    <%--<ajaxToolkit:ToolkitScriptManager ID="srcp" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }
        /*.select2-search__field{
            width:0.0em !important;
        }*/
        .aspNetDisabled {
            /*background: #dae1ed;*/
            background: #ebebe4;
        }

        #MainContent_lstApprover {
            overflow: hidden !important;
        }

        /*.edit-contact input {
            padding-left: 0px !important;
        }*/

        #MainContent_lnk_ed_Search,
        #MainContent_lnk_ed_Clear,
        #MainContent_lnk_LinkButton1,
        #MainContent_lnk_LinkButton2,
        #MainContent_lnk_LinkButton3,
        #MainContent_lnk_ed_Download {
            background: #3D1956;
            color: #febf39 !important;
            padding: 0.5% 1.4%;
            margin: 0% 0% 0 0;
        }

        .noresize {
            resize: none;
        }
        .textBoxST{
            margin-bottom: 2px !important;
        }
        /*.select2-container .select2-selection--multiple {
            box-sizing: border-box;
            cursor: pointer;
            display: block;
            min-height: 32px;
            user-select: none;
            -webkit-user-select: none;
            min-height: 10px;
        }*/
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("hccurlmain")%>/js/gridviewscroll.js"></script>
    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Employee OT Request"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>               
                 <span runat="server" id="backList" visible="false">
                    <a href="InboxOTRequest.aspx" class="aaaa">Back</a>
                </span>
                <span>
                    <a href="Attendance.aspx" style="margin-right: 18px;" class="aaaa">Attendance</a>&nbsp;&nbsp; 
                </span>
                <div class="edit-contact">
                    <div class="editprofile" style="text-align: center; border: none;" id="divmsg" runat="server" visible="false">
                    </div>
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server"></asp:LinkButton>
                        </div>
                    </div>
                    <ul id="editform" runat="server" visible="false">

                        <li class="mobile_inboxEmpCode">
                            <span><b>OT Request</b></span><br />
                            <br />
                        </li>
                        <li>
                            <asp:Label runat="server" ID="Label2" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                        </li>
                         <li><span>Select Month Year</span>&nbsp;&nbsp;<span style="color:red">*</span> <br />
                            <asp:DropDownList runat="server" ID="ddlMonthYear" AutoPostBack="true" OnSelectedIndexChanged="ddlMonthYear_SelectedIndexChanged"></asp:DropDownList></li>
                        <li runat="server" id="show5">
                            <br />
                            <span>Employee Name</span>
                            <br />
                            <asp:DropDownList runat="server" ID="ddl_Employee" OnSelectedIndexChanged="ddl_Employee_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </li>
                       
                        <li class="date">
                            <br />
                            <span>From Date</span> &nbsp;&nbsp;              
                                 <asp:TextBox ID="txtFromdate" Enabled="false" AutoComplete="off" runat="server" AutoPostBack="true" OnTextChanged="txtFromdate_TextChanged" MaxLength="10" AutoCompleteType="Disabled"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtFromdate" runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>
                        <li class="date">
                            <br />
                            <span>To Date</span> &nbsp;&nbsp;        
                                 <asp:TextBox ID="txtToDate" AutoComplete="off" runat="server" AutoPostBack="true" OnTextChanged="txtToDate_TextChanged" MaxLength="10" Enabled="false" AutoCompleteType="Disabled"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtToDate" runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>
                        <li class="mobile_InboxEmpName">
                            <span>Location</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_Location" runat="server" Visible="True" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="mobile_InboxEmpName">
                            <span>Designation </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_Designation" Enabled="false" runat="server"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode" style="width:83%">
                            <span>Shift Timings</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_ShiftTime" runat="server" MaxLength="50" Visible="True" Enabled="false"> </asp:TextBox>

                        </li>
                        <li>
                            <br />
                            <asp:LinkButton ID="lnk_ed_Search" Visible="true" runat="server" Text="View" ToolTip="View" CssClass="Savebtnsve" OnClick="lnk_ed_Search_Click"></asp:LinkButton>
                        </li>
                        <li>
                            <br />
                            <%--<asp:LinkButton ID="lnk_ed_Clear" Visible="true" runat="server" Text="Clear Search" ToolTip="Clear Search" CssClass="Savebtnsve" OnClick="lnk_ed_Clear_Click"></asp:LinkButton>--%>
                        </li>
                        <hr />
                        <li style="width: 100% !important">
                            <span style="color: red;"><b id="spanNoRe" runat="server" visible="false">No Of Records</b></span>
                            <br />
                           
                            <span><b>Attendance And Leave Details</b></span>
                            <br /> <br />
                           
                            <div>
                                <asp:GridView ID="gv_EmployeeDetails" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" AllowPaging="true" PageSize="60" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                    DataKeyNames="Emp_Code" OnPageIndexChanging="gv_EmployeeDetails_PageIndexChanging" ShowFooter="true">
                                    <FooterStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                    <PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Date"
                                            DataField="att_date"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-BorderColor="Navy" ItemStyle-Width="9%" />

                                        <asp:BoundField HeaderText="In Time - Out Time"
                                            DataField="IN_OUT_TIME"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-BorderColor="Navy" ItemStyle-Width="13%" />
                                        <asp:BoundField HeaderText="Status"
                                            DataField="IN_OUT_STATUS"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-BorderColor="Navy" ItemStyle-Width="14%" />
                                        <asp:BoundField HeaderText="Leave Type"
                                            DataField="Leave_Type_Description"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-BorderColor="Navy" ItemStyle-Width="11%" />
                                        <asp:BoundField HeaderText="Leave Status"
                                            DataField="Request_status1"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-BorderColor="Navy" ItemStyle-Width="11%" />

                                        <asp:TemplateField ItemStyle-Width="14%" HeaderText="Reason"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%#Eval("Remark") %>
                                                <asp:HiddenField runat="server" ID="hdnReason" Value='<%#Eval("Remark") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="Total" Text="Total"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Working Hours" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%#Eval("WorkingHours") %>
                                                <asp:HiddenField runat="server" ID="hdnWorkingHours" Value='<%#Eval("WorkingHours") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <FooterTemplate>        
                                                <asp:Label runat="server" ID="TotalWorkingHours" Text='<%#Eval("TotalWorkingHours")%>'></asp:Label>
                                           </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Over Time" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%#Eval("OverTime") %>
                                                <asp:HiddenField runat="server" ID="hdnOverTime" Value='<%#Eval("OverTime") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>                                           
                                            <FooterTemplate >
                                                 <asp:Label runat="server" ID="TotalOverTime" Text='<%#Eval("TotalOverTime") %>'></asp:Label>                                              
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remark" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="18%" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox  ID="txt_Remark_Enter" Width="72%" Text='<%#Eval("EnterRemark") %>' CssClass="textBoxST" MaxLength="100" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>

                                </asp:GridView>
                            </div>
                            <br />
                        </li>
                        <li></li>
                        <li></li>
                       <hr />
                        <li runat="server" id="Li1">
                            <br />
                            <span>Eligible OT Hours i.e beyond 25 Hrs</span>
                            <br />
                            <asp:TextBox runat="server" ID="txt_EligibleOTHours" MaxLength="10" Enabled="false">
                            </asp:TextBox>
                        </li>
                        <li runat="server" id="Li2">
                            <br />
                            <span>Eligible Payable OT Amount</span>
                            <br />
                            <asp:TextBox runat="server" ID="txt_EligiblePayableOT" MaxLength="10" Enabled="false">
                            </asp:TextBox>
                        </li>
                        <li runat="server" id="Li3">
                            <br />
                            <span>OT Amount to be paid</span>
                            <br />
                            <asp:TextBox runat="server" ID="txt_OTAmounttobepaid" MaxLength="10">
                            </asp:TextBox>
                        </li>
                        <li runat="server" id="Li4">
                            <br />
                            <span>Reporting Manager Name: </span>
                            <br />
                            <asp:TextBox runat="server" ID="txt_RM" Enabled="false">
                            </asp:TextBox>
                        </li>
                        <li runat="server" id="Li5">
                            <br />
                            <span>Overall Remarks: </span>
                            <br />
                            <asp:TextBox runat="server" ID="txtOverAllRemark" MaxLength="250" CssClass="noresize" TextMode="MultiLine" Columns="6" Rows="5">
                            </asp:TextBox>
                        </li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li style="text-align:center !important;">                          
                            <asp:LinkButton ID="lnk_LinkButton1" Visible="false" runat="server" Text="Send To HR" ToolTip="Send To HR" CssClass="Savebtnsve" OnClick="lnk_LinkButton1_Click"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="lnk_LinkButton2" Visible="false" runat="server" Text="Download" ToolTip="Download" CssClass="Savebtnsve" OnClick="lnk_LinkButton2_Click"></asp:LinkButton>
                        </li>
                        <li></li>
                        <li></li>
                        <li></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="mobile_Savebtndiv">
    </div>

    <br />
    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />--%>

    <asp:HiddenField ID="hdnvouno" runat="server" />
    <asp:HiddenField ID="hdnIsMarried" runat="server" />
    <asp:HiddenField ID="hdnFamilyDetailID" runat="server" />
    <asp:HiddenField ID="hdnEduactonDetailID" runat="server" />
    <asp:HiddenField ID="hdnCertificationDetailID" runat="server" />
    <asp:HiddenField ID="hdnProjectDetailID" runat="server" />
    <asp:HiddenField ID="hdnDomainDetailID" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="hdnempcode" runat="server" />
    <asp:HiddenField ID="hdnTotalHR" runat="server" />
    <asp:HiddenField ID="hdnTotalOverTimeHR" runat="server" />
    <asp:HiddenField ID="hdnApp_Id" runat="server" />


    <asp:HiddenField ID="FilePath" runat="server" />

    <script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <script type="text/javascript">      
        $(document).ready(function () {           
            $("#MainContent_ddl_Employee").select2();
            $("#MainContent_ddlMonthYear").select2();          
        });
    </script>
    <script type="text/javascript">

        function onCalendarShown() {
            var cal = $find("calendar1");
            cal._switchMode("years", true);
            if (cal._yearsBody) {
                for (var i = 0; i < cal._yearsBody.rows.length; i++) {
                    var row = cal._yearsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }

        function onCalendarHidden() {
            var cal = $find("calendar1");
            if (cal._yearsBody) {
                for (var i = 0; i < cal._yearsBody.rows.length; i++) {
                    var row = cal._yearsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }
        function call(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "year":
                    var cal = $find("calendar1");
                    cal.set_selectedDate(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged(); break;
            }
        }
        function checkProjectEndDate(sender, args) {
            if (sender._selectedDate >= new Date()) {
                alert("You can not select a future date than today!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }

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



        function onCharOnlyNumber(e) {
            var keynum;
            var keychar;
            var numcheck = /[0123456789]/;

            if (window.event) {
                keynum = e.keyCode;
            }
            else if (e.which) {
                keynum = e.which;
            }
            keychar = String.fromCharCode(keynum);
            return numcheck.test(keychar);
        }
        function Confirm(msg) {
            //Testing();
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm(msg)) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }

            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return;
        }
        //
        function onCharOnlyNumber_EXP(e) {
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
        function onCharOnlyNumber_Mobile(e) {
            var keynum;
            var keychar;
            var numcheck = /[0123456789]/;

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

    </script>
</asp:Content>

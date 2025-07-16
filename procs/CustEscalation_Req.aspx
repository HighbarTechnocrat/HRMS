<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="CustEscalation_Req.aspx.cs" Inherits="CustEscalation_Req" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
            /*background: #dae1ed;*/
            background: #ebebe4;
        }

        #MainContent_lstApprover {
            overflow: hidden !important;
        }

        .noresize {
            resize: none;
        }
    	.CustEscala {
			padding-bottom:15px;
    	}
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
	 <script src="../js/dist/jquery-3.2.1.min.js"></script>  
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
                        <asp:Label ID="lblheading" runat="server" Text="Create Customer Incident"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>
                <div>
                    <span>
                        <a href="CustEscalation.aspx" class="aaaa">Cust Incident Home</a>
                    </span>
                </div>


                <div class="edit-contact">

                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server"></asp:LinkButton>
                        </div>
                    </div>


                    <ul id="editform" runat="server" visible="false">

                        <li class="mobile_inboxEmpCode" style="display: none;">
                            <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="False"> </asp:TextBox>
                        </li>

                        <li class="mobile_InboxEmpName" style="display: none;">
                            <asp:TextBox AutoComplete="off" ID="txtEmpName" runat="server" Visible="false"></asp:TextBox>
                        </li>

                        <li class="mobile_date" style="display: none;">

                          
                        </li>
                        <li class="mobile_inboxEmpCode" style="display: none;">
                            <asp:TextBox AutoComplete="off" ID="TextBox4" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
                        </li>
                       
                        <li class="mobile_inboxEmpCode" style="display: none;">
                            <span>Alternate Contact no. (if any) </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_Alt_Contact" runat="server" MaxLength="10"> </asp:TextBox>
                        </li>

                        <li class="mobile_InboxEmpName" style="display: none;">
                            <span style="visibility: hidden;"></span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
                        </li>
						<li class="claimmob_fromdate">
							<span>Incident Number </span>&nbsp;&nbsp;
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtIncidentNo" ReadOnly="true" Enabled="false" runat="server" AutoPostBack="true"></asp:TextBox>
                            
                            </li>
						<li>
							 <span>Date </span>&nbsp;&nbsp;
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_DateCreate" ReadOnly="true" Enabled="false" runat="server" AutoPostBack="true"></asp:TextBox>
                             
						</li>
						<li class="claimmob_fromdate">
                            <span>Escalation Raised By  </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtEscalationBy"  runat="server" ></asp:TextBox>
                             </li>
						<li class="claimmob_fromdate">
                            <span>Email ID </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtEmailID" CssClass="email"  runat="server"></asp:TextBox>
                             </li>
                        <li class="mobile_inboxEmpCode CustEscala">
                            <span>Project Name </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="DropdownSelect">                         
                            </asp:DropDownList>
                        </li>
                        <li class="claimmob_Amount CustEscala">
                            <span>	Mode </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList ID="ddlCustMode" runat="server" CssClass="DropdownSelect">
                            </asp:DropDownList>
                        </li>
                        
                         <li class="mobile_inboxEmpCode CustEscala">
                            <span>Role of the person </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList ID="ddlRolePerson" runat="server" CssClass="DropdownSelect">                         
                            </asp:DropDownList>
                        </li>

						 <li class="claimmob_Amount CustEscala">
                            <span>Customer Satisfaction Index  </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList ID="ddlCustSatisfaction" runat="server" CssClass="DropdownSelect">
                            </asp:DropDownList>
                        </li>
                        <li class="claimmob_Amount CustEscala">
                            <span>Severity </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList ID="ddlSeverity" runat="server" CssClass="DropdownSelect">
                            </asp:DropDownList>
                        </li>
						<li class="mobile_inboxEmpCode CustEscala">
                            <span>Impact on Project </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList ID="ddlImpactProject" runat="server" CssClass="DropdownSelect">                         
                            </asp:DropDownList>
                        </li>
                        
                        <li class="claimmob_fromdate">
                            <span>Incident Description</span>&nbsp;&nbsp;<span style="color: red">*</span><br />

                            <asp:TextBox AutoComplete="off" CssClass="noresize" ID="Txt_Service_Desription" runat="server" MaxLength="500"  TextMode="MultiLine" Rows="6" Width="188%" onKeyUp="javascript:Count(this);"></asp:TextBox>
                        </li>
						<li></li>
                        <li class="claimmob_upload">
                            <span>Upload File</span>&nbsp;&nbsp;<%--<span style="color:red">*</span>--%><br />
                            <asp:FileUpload ID="uploadfile" runat="server" />
                            <asp:LinkButton ID="lnkuplodedfile" OnClick="lnkuplodedfile_Click" runat="server"></asp:LinkButton>

                        </li>
                        <li class="mobile_inboxEmpCode">
                            <br />
                            <asp:TextBox AutoComplete="off" ID="TextBox11" runat="server" MaxLength="100" Visible="false"></asp:TextBox>
                        </li>
                        

                        <li class="mobile_grid" style="display: none">
                            <div>
                                <asp:GridView ID="dgMobileClaim" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%"
                                    DataKeyNames="Claims_id,Rem_id" OnRowCreated="dgMobileClaim_RowCreated">
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                    <PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />

                                    <Columns>
                                        <asp:BoundField HeaderText="Month"
                                            DataField="Rem_Month"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="20%" />

                                        <asp:BoundField HeaderText="Bill Amount"
                                            DataField="Amount"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="20%" />

                                        <asp:BoundField HeaderText="Eligibility"
                                            DataField="Eligible_Amt"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="20%" />

                                        <asp:BoundField HeaderText="Deviation"
                                            DataField="Remarks"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="20%" />

                                        <asp:TemplateField HeaderText="Details">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" Text='View' OnClick="lnkEdit_Click">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <asp:TextBox AutoComplete="off" ID="TextBox6" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
                        </li>

                        

                        <li class="mobile_Approver" style="display: none">
                            <span>Approver </span>
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
                                        ItemStyle-Width="26%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Status"
                                        DataField="Status"
                                        ItemStyle-HorizontalAlign="left"
                                        ItemStyle-Width="20%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Approved on"
                                        DataField="tdate"
                                        ItemStyle-HorizontalAlign="left"
                                        ItemStyle-Width="20%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Approver Remarks"
                                        DataField="Comment"
                                        ItemStyle-HorizontalAlign="left"
                                        ItemStyle-Width="34%"
                                        ItemStyle-BorderColor="Navy" />
                                </Columns>
                            </asp:GridView>
                        </li>

                        <li class="mobile_inboxEmpCode">
                            <asp:TextBox AutoComplete="off" ID="TextBox99" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>

                        </li>

                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="mobile_Savebtndiv">
        <asp:LinkButton ID="mobile_btnSave" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click" OnClientClick="return SaveMultiClick();">Submit</asp:LinkButton>
        <asp:LinkButton ID="mobile_cancel" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="Savebtnsve"  OnClientClick="return CancelMultiClick();">Cancel</asp:LinkButton>
        <asp:LinkButton ID="mobile_btnBack" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/CustEscalation.aspx">Back</asp:LinkButton>
        <%--<asp:LinkButton ID="mobile_btnPrintPV" runat="server" Text="Print Payment Voucher" ToolTip="Print Payment Voucher" CssClass="Savebtnsve" OnClick="mobile_btnPrintPV_Click">Print Payment Voucher</asp:LinkButton>--%>

       


        
    </div>
    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />
    <%--<asp:RegularExpressionValidator runat="server" ID="valInput"   ControlToValidate="Txt_Service_Desription"    ValidationExpression="^[\s\S]{0,500}$"    ErrorMessage="Please enter a maximum of 500 characters"    Display="Dynamic">*</asp:RegularExpressionValidator>--%>
    <asp:HiddenField ID="hdnBilltype" runat="server" />

    <asp:HiddenField ID="hdnvouno" runat="server" />

    <asp:HiddenField ID="hflEmpDesignation" runat="server" />

    <asp:HiddenField ID="hflEmpDepartment" runat="server" />

    <asp:HiddenField ID="hflEmailAddress" runat="server" />

    <asp:HiddenField ID="hflGrade" runat="server" />

    <asp:HiddenField ID="hflapprcode" runat="server" />

    <asp:HiddenField ID="hdnDesk" runat="server" />

    <asp:HiddenField ID="hdnDestnation" runat="server" />

    <asp:HiddenField ID="hdnRemid" runat="server" />

    <asp:HiddenField ID="hdnClaimid" runat="server" />

    <asp:HiddenField ID="hdnLcalTripid" runat="server" />

    <asp:HiddenField ID="hdnTraveltypeid" runat="server" />

    <asp:HiddenField ID="hdnDeptPlace" runat="server" />

    <asp:HiddenField ID="hdnTravelmode" runat="server" />

    <asp:HiddenField ID="hdnDeviation" runat="server" />

    <asp:HiddenField ID="hdnTrDetRequirements" runat="server" />

    <asp:HiddenField ID="hdnAccReq" runat="server" />

    <asp:HiddenField ID="hdnAccCOS" runat="server" />

    <asp:HiddenField ID="hdnlocaltrReq" runat="server" />

    <asp:HiddenField ID="hdnlocalTrCOS" runat="server" />

    <asp:HiddenField ID="hdnTravelConditionid" runat="server" />

    <asp:HiddenField ID="hdnApprId" runat="server" />

    <asp:HiddenField ID="hdnApprEmailaddress" runat="server" />

    <asp:HiddenField ID="hdnEligible" runat="server" />
    <asp:HiddenField ID="hdnTrdays" runat="server" />
    <asp:HiddenField ID="hdnTravelDtlsId" runat="server" />
    <asp:HiddenField ID="hdnAccId" runat="server" />
    <asp:HiddenField ID="hdnLocalId" runat="server" />
    <asp:HiddenField ID="hdnTravelstatus" runat="server" />
    <asp:HiddenField ID="hdnLeavestatusValue" runat="server" />
    <asp:HiddenField ID="hdnLeavestatusId" runat="server" />
    <asp:HiddenField ID="hdnIsApprover" runat="server" />

    <asp:HiddenField ID="hdnApprovalCOS_mail" runat="server" />
    <asp:HiddenField ID="hdnApprovalCOS_Code" runat="server" />
    <asp:HiddenField ID="hdnApprovalCOS_ID" runat="server" />
    <asp:HiddenField ID="hdnApprovalCOS_Name" runat="server" />
    <asp:HiddenField ID="hdnMobRemStatusM" runat="server" />
    <asp:HiddenField ID="hdnMobRemStatus_dtls" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />

    <asp:HiddenField ID="hdnsptype" runat="server" />
    <asp:HiddenField ID="hdnGrade" runat="server" />

    

         
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" /> 
    
    <script type="text/javascript">      
        $(document).ready(function () {
            $(".DropdownSelect").select2();
            
        });
    </script>

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
            var maxlength = 500;
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

            //document.getElementById('txtFromdate1').options.add(optn);
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

        function SaveMultiClick() {
            //alert();
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=mobile_btnSave.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        retunboolean = Confirm();

                    if (retunboolean == false)
                        ele.disabled = false;
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
                var ele = document.getElementById('<%=mobile_cancel.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        var retunboolean = Confirm();

                    if (retunboolean == false)
                        ele.disabled = false;
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }

        function Confirm() {
            //Testing();
            var confirmval = false;
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Are you sure you want to submit?")) {
                confirm_value.value = "Yes";
                confirmval = true;
            } else {
                confirm_value.value = "No";
                confirmval = false;
            }
            //alert(confirm_value.value);
            //document.forms[0].appendChild(confirm_value);
            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return confirmval;

		}
			$(".email").change(function () {    
			var inputvalues = $(this).val();    
			var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;    
			if(!regex.test(inputvalues)){    
				alert("Please enter valid email id");
				//$(".email").val("");
			return regex.test(inputvalues);    
			}    
			});    

    </script>
</asp:Content>



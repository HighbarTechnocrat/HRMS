<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="Service_Req.aspx.cs" Inherits="Service_Req" %>

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
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
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
                        <asp:Label ID="lblheading" runat="server" Text="Create Service Request"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>
                <div>
                    <span>
                        <a href="Service.aspx" class="aaaa">EmployeeFIRST Home</a>
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

                            <!--Commented by R1 on 01-10-2018 <span>Claim Date </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtFromdateold" runat="server" AutoPostBack="True"></asp:TextBox>
-->
                        </li>
                        <li class="mobile_inboxEmpCode" style="display: none;">
                            <asp:TextBox AutoComplete="off" ID="TextBox4" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
                        </li>
                        <!-- by Highbartech on 10-06-2020 -->
                        <!--<li class="mobile_detail">                           
                            <asp:LinkButton ID="btnTra_Details" Visible="false" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" OnClick="btnTra_Details_Click"></asp:LinkButton>
                             <span>Claim Details</span>
                        </li>
                        <li class="mobile_inboxEmpCode">                         
                            <asp:TextBox AutoComplete="off" ID="TextBox5" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
                        </li>-->
                        <!-- by Highbartech on 10-06-2020 -->
                        <!-- by Highbartech on 10-06-2020 -->
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

                        <li class="mobile_inboxEmpCode">
                            <span>Service Request To Department </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                <%--                                <asp:ListItem Selected="True" Value="0">Select Department</asp:ListItem>
                                --%>
                            </asp:DropDownList>
                        </li>
                        <li class="claimmob_Amount">
                            <span>Select Category </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList ID="ddlCategory" runat="server">
                            </asp:DropDownList>
                        </li>
                        <li class="claimmob_fromdate">
                            <span>Date </span>&nbsp;&nbsp;
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_DateCreate" ReadOnly="true" Enabled="false" runat="server" AutoPostBack="true"></asp:TextBox>

                            <%--<asp:DropDownList ID="ddlDepartment" runat="server"></asp:DropDownList>--%>
                            <%--<asp:TextBox AutoComplete="off" ID="Txt_DeptName" runat="server" Visible="true"></asp:TextBox>--%>
                        </li>
                        <li class="claimmob_fromdate" style="visibility: hidden;">
                            <br />
                            <span>From Date </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtFromdate_N" runat="server" AutoPostBack="True" OnTextChanged="txtFromdate_N_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtFromdate_N"
                                Format="dd/MM/yyyy" runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>
                        <li class="claimmob_fromdate">
                            <span>Service Request Description</span>&nbsp;&nbsp;<span style="color: red">*</span><br />

                            <asp:TextBox AutoComplete="off" CssClass="noresize" ID="Txt_Service_Desription" runat="server" MaxLength="500"  TextMode="MultiLine" Rows="6" Width="180%"></asp:TextBox>

                        </li>

                        <li class="claimmob_fromdate" style="visibility: hidden;">
                            <br />
                            <span>To Date </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtTodate_N" runat="server" Enabled="false" AutoPostBack="True"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" TargetControlID="txtTodate_N"
                                Format="dd/MM/yyyy" runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>
                        <li class="claimmob_fromdate" style="display: none;">
                            <span>Bill Month </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtFromdate" runat="server" AutoPostBack="True" OnTextChanged="txtFromdate_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" OnClientHidden="onCalendarHidden" OnClientShown="onCalendarShown" TargetControlID="txtFromdate"
                                Format="MMM/yyyy" runat="server" BehaviorID="calendar1">
                            </ajaxToolkit:CalendarExtender>
                        </li>

                        <li class="claimmob_Amount" style="display: none;">
                            <span>Bill / Receipt no. </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_BillNo" runat="server" MaxLength="12"></asp:TextBox>

                        </li>

                        <%-- <li class="mobile_inboxEmpCode" style="visibility:hidden;">                          
                            <span>Reason: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtReason" runat="server" MaxLength="100"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode" style="visibility:hidden;">  
                            <span runat="server" id="Spn_HOD_Recm_Amt" visible="false">Approved Amount </span>                         
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_Approved_Amount" runat="server" MaxLength="100" Enabled="false" Visible="false"></asp:TextBox>
                        </li>--%>
                        <!--<li class="claimmob_ElgAmount">
                            <span>Elig Amount </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ReadOnly="true" ID="txtElgAmnt" CssClass="graytextbox" runat="server" MaxLength="100" AutoPostBack="true" Visible="false"></asp:TextBox>
                        </li>

                        <li class="claimmob_Remark">
                            <span>Deviation</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtRemark" ReadOnly="true" CssClass="graytextbox" runat="server" MaxLength="100" Visible="false" ></asp:TextBox>
                        </li>-->

                        <li class="claimmob_upload">
                            <span>Upload File</span>&nbsp;&nbsp;<%--<span style="color:red">*</span>--%><br />
                            <asp:FileUpload ID="uploadfile" runat="server" />
                            <asp:LinkButton ID="lnkuplodedfile" OnClick="lnkuplodedfile_Click" runat="server"></asp:LinkButton>

                        </li>
                        <li class="mobile_inboxEmpCode">
                            <br />
                            <asp:TextBox AutoComplete="off" ID="TextBox11" runat="server" MaxLength="100" Visible="false"></asp:TextBox>
                        </li>
                        <%--                        <li class="claimmob_upload">
                            <span>Upload Receipt</span><br />
                            <asp:FileUpload ID="uploadRcpt" runat="server" />
                            <asp:LinkButton ID="lnkuploadRcpt" OnClick="lnkuploadRcpt_Click" runat="server"></asp:LinkButton>
                        </li>
                          <li class="mobile_inboxEmpCode">    
                            <br />
                            <asp:TextBox AutoComplete="off" ID="TextBox12" runat="server" MaxLength="100" Visible="false" ></asp:TextBox>
                        </li>--%>
                        <!-- by Highbartech on 10-06-2020 -->

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

                        <!--<li class="mobile_Amount">
                            <span>Total Amount Claimed: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtAmountTot" ReadOnly="true" Enabled="false" runat="server" MaxLength="100" Visible="false"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <asp:TextBox AutoComplete="off" ID="TextBox7" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
                         </li>

                        <li class="mobile_deviation">
                            <%--<span>Reason for Deviation </span>--%>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtDeviation" Visible="false" runat="server" MaxLength="100"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                           <asp:TextBox AutoComplete="off" ID="TextBox8" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
                        </li>-->

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
        <asp:LinkButton ID="mobile_cancel" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="Savebtnsve" OnClick="mobile_cancel_Click" OnClientClick="return CancelMultiClick();">Cancel</asp:LinkButton>
        <asp:LinkButton ID="mobile_btnBack" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/Service.aspx">Back</asp:LinkButton>
        <%--<asp:LinkButton ID="mobile_btnPrintPV" runat="server" Text="Print Payment Voucher" ToolTip="Print Payment Voucher" CssClass="Savebtnsve" OnClick="mobile_btnPrintPV_Click">Print Payment Voucher</asp:LinkButton>--%>

        <%-- Following Popup for Submit Mobile Rem Request  
        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" DisplayModalPopupID="mpe" TargetControlID="mobile_btnSave">
        </ajaxToolkit:ConfirmButtonExtender>
        <ajaxToolkit:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="mobile_btnSave" OkControlID = "btnYes"
            CancelControlID="btnNo" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Confirmation
            </div>
            <div class="body">
           
                Do you want to Submit ?
            </div>
            <div class="footer" align="right">
                <asp:Button ID="btnNo" runat="server" Text="No" />
                <asp:Button ID="btnYes" runat="server" Text="Yes" />
            </div>
        </asp:Panel>
        End Here --%>


        <%-- Following Popup for Cancel Mobile Rem Requestt  
        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" DisplayModalPopupID="mpe_Cancel" TargetControlID="mobile_cancel">
        </ajaxToolkit:ConfirmButtonExtender>
        <ajaxToolkit:ModalPopupExtender ID="mpe_Cancel" runat="server" PopupControlID="pnlPopup_Cancel" TargetControlID="mobile_cancel" OkControlID = "btnYes_CLR"
            CancelControlID="btnNo_CLR" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup_Cancel" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Confirmation
            </div>
            <div class="body">
               
                Do you want to Cancel ?
            </div>
            <div class="footer" align="right">                                
                <asp:Button ID="btnNo_CLR" runat="server" Text="No" />
                <asp:Button ID="btnYes_CLR" runat="server" Text="Yes" />                
            </div>
        </asp:Panel>
        End Here --%>
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

    <%--<ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchProject" MinimumPrefixLength="1"
        CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="Txt_ProjectName"
        ID="AutoCompleteExtender1" runat="server" FirstRowSelected="true">
    </ajaxToolkit:AutoCompleteExtender>--%>

    <%--<ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchDepartment" MinimumPrefixLength="1"
        CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="Txt_DeptName"
        ID="AutoCompleteExtender2" runat="server" FirstRowSelected="true">
    </ajaxToolkit:AutoCompleteExtender>--%>

     <script src="../js/dist/jquery-3.2.1.min.js"></script>       
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" /> 
    
    <script type="text/javascript">      
        $(document).ready(function () {
            $("#MainContent_ddlDepartment").select2();
            $("#MainContent_ddlCategory").select2();
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

    </script>
</asp:Content>



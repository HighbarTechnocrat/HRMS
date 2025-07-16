<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="VSCB_InboxPaymentRequest_View.aspx.cs" Inherits="procs_VSCB_InboxPaymentRequest_View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
            background: #dae1ed;
        }

        .paging a {
            background-color: #C7D3D4;
            padding: 5px 7px;
            text-decoration: none;
            border: 1px solid #C7D3D4;
        }

            .paging a:hover {
                background-color: #E1FFEF;
                color: #00C157;
                border: 1px solid #C7D3D4;
            }

        .paging span {
            background-color: #E1FFEF;
            padding: 5px 7px;
            color: #00C157;
            border: 1px solid #C7D3D4;
        }

        tr.paging {
            background: none !important;
        }

            tr.paging tr {
                background: none !important;
            }

            tr.paging td {
                border: none;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
    <script src="../js/freeze/jquery-1.11.0.min.js"></script>
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />
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
                        <asp:Label ID="lblheading" runat="server" Text="Payment Requests View"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>

                </div>

                <span>
                    <a href="vscb_index.aspx" class="aaaa"><%=System.Configuration.ConfigurationManager.AppSettings["VSCBPageTitle"]%></a>

                </span>
                <div class="edit-contact">
                    <ul id="editform" runat="server">
                        <li style="padding-top: 30px">
                            <span>PO/ WO No.</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" ID="lstPOWONo" CssClass="DropdownListSearch">
                            </asp:DropDownList>
                        </li>
                        <li style="padding-top: 30px">
                            <span>PO/ WO Date</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstPOWODate">
                            </asp:DropDownList>
                        </li>
                        <li style="padding-top: 30px">
                            <span>Vendor Name</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" ID="lstVendorName" CssClass="DropdownListSearch" Width="98%">
                            </asp:DropDownList>
                        </li>
                        <li style="padding-top: 15px">
                            <span>Invoice No</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstInvoiceNo">
                            </asp:DropDownList>
                        </li>
                        <li style="padding-top: 15px">
                            <span>Payment Request No.</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstPaymentRequestNo">
                            </asp:DropDownList>
                        </li>
                        <li style="padding-top: 15px">
                            <span>Payment Request Date</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstpaymentRequestDate">
                            </asp:DropDownList>
                        </li>
                        <%--						<li style="padding-top: 15px">
							<span>Project/Department</span>&nbsp;&nbsp;
                             <br />
							<asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstDepartment">
							</asp:DropDownList>
						</li>--%>
                        <li style="padding-top: 15px">
                            <span>Cost Center</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstCostCentre">
                            </asp:DropDownList>
                        </li>

                        <li style="padding-top: 15px">
                            <span>Status</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstStatus">
                            </asp:DropDownList>
                        </li>

                    </ul>
                </div>
                <%--<div class="mobile_Savebtndiv" style="margin-top: 10px !important">
                    <asp:LinkButton ID="mobile_btnSave" runat="server" Text="Submit" ToolTip="Search" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click">Search </asp:LinkButton>
                    <asp:LinkButton ID="mobile_btnBack" runat="server" Text="Clear Search" OnClick="mobile_btnBack_Click" ToolTip="Clear Search" class="trvl_Savebtndiv">Clear Search</asp:LinkButton>
                    <asp:LinkButton ID="btnCancel" runat="server" Text="Export To Excel" ToolTip="Export To Excel" CssClass="Savebtnsve" OnClick="ExportToExcel" Visible="true">Export To Excel</asp:LinkButton>
                </div>--%>
                <div class="trvl_Savebtndiv">
                    <asp:LinkButton ID="trvl_btnSave" runat="server" Text="Submit" ToolTip="Search" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click">Search</asp:LinkButton>

                    <asp:LinkButton ID="btnCorrection" runat="server" Text="Clear Search" ToolTip="Clear Search" CssClass="Savebtnsve" OnClick="mobile_btnBack_Click">Clear Search</asp:LinkButton>

                    <asp:LinkButton ID="btnCancel" runat="server" Text="Export To Excel" ToolTip="Export To Excel" CssClass="Savebtnsve" OnClick="ExportToExcel" Visible="false">Export To Excel</asp:LinkButton>
                </div>
                <div class="manage_grid">
                    <asp:Label runat="server" ID="RecordCount" Style="color: red; font-size: 14px;"></asp:Label>
                    <center>
                        <asp:GridView ID="gvMngTravelRqstList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                            DataKeyNames="Payment_ID,POID,IsInvoiceWithPO" CellPadding="3" AutoGenerateColumns="False" Width="120%" EditRowStyle-Wrap="false" PageSize="20" AllowPaging="True" OnPageIndexChanging="gvMngTravelRqstList_PageIndexChanging">
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                            <PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" />
                            <RowStyle ForeColor="#000066" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                            <PagerStyle HorizontalAlign="Right" CssClass="paging" />

                            <Columns>
                                <asp:BoundField HeaderText="Payment Status"
                                    DataField="PyamentStatus"
                                    ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="5%" />

                                <asp:BoundField HeaderText="Vendor Name"
                                    DataField="Name"
                                    ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="13%" />
                                <asp:BoundField HeaderText="Payment Request No."
                                    DataField="PaymentReqNo"
                                    ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="15%" />
                                <asp:BoundField HeaderText="Payment Request Date"
                                    DataField="PaymentReqDate"
                                    ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="6%" />

                                <asp:BoundField HeaderText="Payment Request Amt"
                                    DataField="TobePaidAmtWithtax"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-Width="6%" />

                                <asp:BoundField HeaderText="Account Paid Amount"
                                    DataField="Amt_paid_Account"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-Width="6%" />
                                <asp:BoundField HeaderText="Payment Balance  Amount"
                                    DataField="BalanceAmt"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-Width="6%" />
                                <asp:BoundField HeaderText="Invoice No"
                                    DataField="InvoiceNo"
                                    ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="9%" />
                                <asp:BoundField HeaderText="Invoice Amount (Without GST)"
                                    DataField="Invoice_Amount_Without_GST"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-Width="6%" />

                                <asp:BoundField HeaderText="Invoice Amount (With GST) (A)"
                                    DataField="Invoice_Amount_With_GST_A"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-Width="6%" />

                                <asp:BoundField HeaderText="Direct Tax (B)"
                                    DataField="Direct_Tax_B"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-Width="5%" />

                                <asp:BoundField HeaderText="Invoice Payable Amt (A-B)"
                                    DataField="Invoice_Payable_Amt_A_B"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-Width="6%" />

                                <asp:BoundField HeaderText="Invoice Paid Amount"
                                    DataField="Invoice_Paid_Amount"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-Width="5%" />

                                <asp:BoundField HeaderText="Invoice Balance Amt."
                                    DataField="Invoice_Balance_Amt"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-Width="5%" />
                                <%--<asp:BoundField HeaderText="Milestone No"
                                DataField="MstoneID"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="5%" />--%>

                                <asp:BoundField HeaderText="Milestone Particulars"
                                    DataField="MilestoneName"
                                    ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="10%" />

                                <asp:BoundField HeaderText="PO Number"
                                    DataField="PONumber"
                                    ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="6%" />
                                <%--<asp:BoundField HeaderText="Milestone Amt"
                                DataField="AmtWithTax"
                                ItemStyle-HorizontalAlign="Right"
                                HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-Width="5%" />

							<asp:BoundField HeaderText="PO/ WO No."
                                DataField="PONumber"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="7%" />

							<asp:BoundField HeaderText="PO/ WO Date"
                                DataField="PODate"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="5%" />
							<asp:BoundField HeaderText="Department"
                                DataField="Department"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="8%" />

							<asp:BoundField HeaderText="Cost Center"
                                DataField="CostCentre"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="12%" />
                            <asp:BoundField HeaderText=" Approval Status"
                                DataField="Request_status"  
								 ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="5%" />--%>
                            </Columns>
                        </asp:GridView>
                    </center>

                </div>
                <br />
                <br />
                <br />
                <asp:HiddenField ID="hdnInboxType" runat="server" />
                <asp:HiddenField ID="hdnEmpCode" runat="server" />
                <asp:HiddenField ID="hdnRecruitment_ReqID" runat="server" />
                <asp:HiddenField ID="FilePath" runat="server" />
                <asp:HiddenField ID="hdnPOID" runat="server" />

            </div>
        </div>
    </div>


    <script src="../js/freeze/jquery-ui.min.js"></script>
    <script src="../js/freeze/gridviewScroll.min.js"></script>
    <script type="text/javascript">      
        $(document).ready(function () {
            $(".DropdownListSearch").select2();
            $('#MainContent_gvMngTravelRqstList').gridviewScroll({
                width: 1060,
                height: 1000,
                freezesize: 4, // Freeze Number of Columns.
                headerrowcount: 1, //Freeze Number of Rows with Header.
            });

        });

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
        $('.number').keypress(function (event) {
            if ((event.which != 46 || $(this).val().indexOf('.') != -1) &&
                ((event.which < 48 || event.which > 57) &&
                    (event.which != 0 && event.which != 8))) {
                event.preventDefault();
            }
            var text = $(this).val();
            if ((text.indexOf('.') != -1) &&
                (text.substring(text.indexOf('.')).length > 2) &&
                (event.which != 0 && event.which != 8) &&
                ($(this)[0].selectionStart >= text.length - 2)) {
                event.preventDefault();
            }
        });

    </script>
</asp:Content>



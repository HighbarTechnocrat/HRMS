<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="VSCB_ViewAllDetails.aspx.cs" Inherits="procs_VSCB_ViewAllDetails" EnableEventValidation="false" %>

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
            /*background: #dae1ed;*/
            background: #ebebe4;
        }

        .taskparentclasskkk {
            width: 29.5%;
            height: 72px;
            /*overflow:initial;*/
        }

        #MainContent_lstApprover, #MainContent_lstIntermediate {
            padding: 0 0 33px 0 !important;
            /*overflow: unset;*/
        }

        .Dropdown {
            border-bottom: 2px solid #cccccc;
            /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
            background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;
            cursor: default;
        }

        .textboxBackColor {
            background-color: cadetblue;
            color: aliceblue;
        }


        .BtnShow {
            color: blue !important;
            background-color: transparent;
            text-decoration: none;
            font-size: 13px !important;
        }

            .BtnShow:visited {
                color: blue !important;
                background-color: transparent;
                text-decoration: none;
            }

            .BtnShow:hover {
                color: red !important;
                background-color: transparent;
                text-decoration: none !important;
            }

        .POWOContentTextArea {
            width: 750px !important;
            height: 400px !important;
            overflow: auto;
        }

        .LableName {
            color: #F28820;
            font-size: 16px;
            font-weight: normal;
            text-align: left;
        }

        .boxTest {
            BORDER-RIGHT: black 1px solid;
            BORDER-TOP: black 1px solid;
            BORDER-LEFT: black 1px solid;
            BORDER-BOTTOM: black 1px solid;
            BACKGROUND-COLOR: White;
        }

        .AmtTextAlign {
            text-align: right
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
                        <asp:Label ID="lblheading" runat="server" Text="Approved Batch Details"></asp:Label>
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
                            <span>PO/ WO Number.</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" ID="lstPOWONo" CssClass="DropdownListSearch">
                            </asp:DropDownList>
                        </li>

                        <li style="padding-top: 30px">
                            <span>Vendor Name</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" ID="lstVendorName" CssClass="DropdownListSearch" Width="98%">
                            </asp:DropDownList>
                        </li>
                        <li style="padding-top: 15px">
                            <span>Cost Center</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstCostCentre">
                            </asp:DropDownList>

                        </li>
                        <li style="padding-top: 15px">
                            <span>Invoice No.</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstInvoiceNo">
                            </asp:DropDownList>
                        </li>
                        <li style="padding-top: 15px">
                            <span>Batch No.</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstBatchNo">
                            </asp:DropDownList>
                        </li>
                        <li style="padding-top: 15px"></li>
                        <li style="padding-top: 15px">
                            <span>Batch Approved From Date</span>&nbsp;&nbsp;
                             <br />
                            <asp:TextBox ID="txtFromdate" CssClass="txtcls" AutoComplete="off" runat="server" AutoPostBack="false" MaxLength="10" AutoCompleteType="Disabled"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd-MM-yyyy" TargetControlID="txtFromdate" runat="server">
                            </ajaxToolkit:CalendarExtender>

                        </li>

                        <li style="padding-top: 15px">
                            <span>Batch Approved To Date</span>&nbsp;&nbsp;
                             <br />
                            <asp:TextBox ID="txtToDate" CssClass="txtcls" AutoComplete="off" runat="server" AutoPostBack="false"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd-MM-yyyy" TargetControlID="txtToDate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>
                    </ul>
                </div>
                <div class="trvl_Savebtndiv">
                    <asp:LinkButton ID="trvl_btnSave" runat="server" Text="Search" ToolTip="Search" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click">Search</asp:LinkButton>

                    <asp:LinkButton ID="btnCorrection" runat="server" Text="Clear Search" ToolTip="Clear Search" CssClass="Savebtnsve" OnClick="mobile_btnBack_Click">Clear Search</asp:LinkButton>

                    <asp:LinkButton ID="btnCancel" runat="server" Text="Export To Excel" ToolTip="Export To Excel" CssClass="Savebtnsve" OnClick="ExportToExcel" Visible="false">Export To Excel</asp:LinkButton>
                </div>

                <div>
                    <asp:Label runat="server" ID="RecordCount" Style="color: red; font-size: 14px;"></asp:Label>

                    <center>
                        <asp:GridView ID="gvMngTravelRqstList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                            DataKeyNames="Batch_ID,POID,InvoiceID,Payment_ID" CellPadding="3" AutoGenerateColumns="False" Width="145%" EditRowStyle-Wrap="false" PageSize="10" AllowPaging="True" OnPageIndexChanging="gvMngTravelRqstList_PageIndexChanging">
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
                                <asp:TemplateField HeaderText="View" ItemStyle-Width="3%">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="lnkView" runat="server" ToolTip="View" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkView_Click" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />

                                </asp:TemplateField>

                                <asp:BoundField HeaderText="Batch No."
                                    DataField="Batch_No"
                                    ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-BorderColor="Navy" ItemStyle-Width="7%" />

                                <asp:BoundField HeaderText="Actual Payment Paid On."
                                    DataField="Actual_Payment_On"
                                    ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-BorderColor="Navy" ItemStyle-Width="7%" />


                                <asp:BoundField HeaderText="UTR No"
                                    DataField="PaymentRefNo"
                                    ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-BorderColor="Navy" ItemStyle-Width="10%" />


                                <asp:BoundField HeaderText="Vendor Name"
                                    DataField="Vendorname"
                                    ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="15%" />

                                <asp:BoundField HeaderText="Payment Req Amt."
                                    DataField="PaymentReqAmt"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-Width="6%" />
                                <asp:BoundField HeaderText="Amount Paid By Acc."
                                    DataField="Amt_paid_Account"
                                     ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-Width="6%" />
                                <asp:BoundField HeaderText="Payment Req. Balance Amt."
                                    DataField="BalanceAmt"
                                  ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-Width="5%" />
                                <asp:BoundField HeaderText="Invoice No."
                                    DataField="InvoiceNo"
                                    ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="8%" />


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

                                <asp:BoundField HeaderText="PO Number"
                                    DataField="PONumber"
                                    ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="10%" />

                                <asp:BoundField HeaderText="Cost Center"
                                    DataField="Tallycode"
                                    ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="10%" />



                                <asp:BoundField HeaderText="PO/WO Amt."
                                    DataField="POWO_Amt"
                                     ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-Width="7%" />
                                <asp:BoundField HeaderText="PO/WO Paid Amt."
                                    DataField="POWOPaidAmt"
                                     ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-Width="7%" />
                                <asp:BoundField HeaderText="PO/WO Balance Amt."
                                    DataField="POWOBalAmount"
                                     ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-Width="7%" />

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
                <asp:HiddenField ID="hdnInvoiceFromDate" runat="server" />
                <asp:HiddenField ID="hdnInvoiceToDate" runat="server" />
            </div>
        </div>
    </div>


    <script src="../js/freeze/jquery-ui.min.js"></script>
    <script src="../js/freeze/gridviewScroll.min.js"></script>
    <script type="text/javascript">      
        $(document).ready(function () {
            $(".DropdownListSearch").select2();
            $('#MainContent_gvMngTravelRqstList').gridviewScroll({
                width: 1070,
                height: 1000,
                freezesize: 5, // Freeze Number of Columns.
                headerrowcount: 1, //Freeze Number of Rows with Header.
            });

        });  

    </script>
</asp:Content>


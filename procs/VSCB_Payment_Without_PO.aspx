<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="VSCB_Payment_Without_PO.aspx.cs" Inherits="VSCB_Payment_Without_PO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />

    <style>
        .noresize {
            resize: none;
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

        .hiddencol1 {
            display: none !important;
        }


        /*InvoiceDetails*/
        .LableName {
            color: #F28820;
            font-size: 16px;
            font-weight: normal;
            text-align: left;
        }

        .PayNo {
            font-weight: bold;
            font-family: 14px !important;
        }

        .BalAmt {
            /*background-color: yellow;	
			color: red*/
            font-weight: bold;
            background-color: cadetblue;
            color: aliceblue;
            text-align: right;
        }

        .txtAligntextBox {
            text-align: right;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>


    <script src="../js/freeze/jquery-1.11.0.min.js"></script>

    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Create Payment Request With Out PO"></asp:Label>
                    </span>
                </div>

                <div>
                    <span>
                        <a href="vscb_index.aspx" class="aaaa"><%=System.Configuration.ConfigurationManager.AppSettings["VSCBPageTitle"]%></a>
                    </span>
                    <span>
                        <a href="VSCB_InboxMyPaymentRequest.aspx" title="Back" runat="server" visible="false" id="btnRecBack" style="margin-right: 10px;" class="aaaa">Back</a>
                    </span>
                    <span>
                        <a href="VSCB_InboxPaymentRequest_WithOutPO.aspx" title="Back" runat="server" visible="false" id="ACreate" style="margin-right: 10px;" class="aaaa">Back</a>
                    </span>
                </div>

                <div class="edit-contact">
                    <ul id="editform" runat="server">

                        <li class="trvl_date">
                            <span>Invoice No.  </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <%--<asp:DropDownList runat="server" ID="lstInvoiceNo" CssClass="DropdownListSearch" AutoPostBack="true" OnSelectedIndexChanged="lstInvoiceNo_SelectedIndexChanged">
							</asp:DropDownList>--%>
                            <asp:TextBox AutoComplete="off" ID="lstInvoiceNo" runat="server" Enabled="false"></asp:TextBox>

                            <br />
                        </li>

                        <li class="trvl_date">
                            <span>Vendor Name </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtVendorName" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li>
                            <span>Invoice Type </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtInvoiceType" runat="server" Enabled="false"></asp:TextBox>
                            <asp:HiddenField ID="hdnInvoiceType" runat="server" />
                            <asp:HiddenField ID="hdnInvoiceTypeId" runat="server" />
                        </li>
                        <li class="trvl_date">
                            <span>Cost Center </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtTallyCode_display" runat="server" Enabled="false"></asp:TextBox>

                            <span style="display: none">Department Name </span>
                            <asp:TextBox AutoComplete="off" ID="txtDepartment" runat="server" Enabled="false" Visible="false"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span style="display: none">Cost Center </span>
                            <asp:TextBox AutoComplete="off" ID="txtCostCentor" runat="server" Enabled="false" Visible="false"></asp:TextBox>
                        </li>
                        <%--<div style="overflow: scroll; width: 100%; padding-top: 0px; margin-bottom: 10px;">--%>
                        <span class="LableName" runat="server" visible="false" id="spInvoice">Invoice Details
                        </span>

                        <asp:GridView ID="GrdInvoiceDetails" CssClass="Milestones" runat="server" DataKeyNames="InvoiceID" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="130%" OnRowDataBound="GrdInvoiceDetails_RowDataBound">
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
                                <asp:TemplateField HeaderText="Payment Request">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkView" title="Create" CssClass="BtnShow" runat="server" Visible='<%# Eval("PaymentStatusID").ToString() == "2" ? false : true %>' Text='Create' OnClick="lnkView_Click"></asp:LinkButton>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="SrNo."
                                    DataField="Srno"
                                    ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-Width="3%"
                                    ItemStyle-BorderColor="Navy" />
                                <asp:BoundField HeaderText="Cost Centre / Department" Visible="false"
                                    DataField="Project_Dept_Name"
                                    ItemStyle-HorizontalAlign="left"
                                    HeaderStyle-HorizontalAlign="left"
                                    ItemStyle-Width="15%"
                                    ItemStyle-BorderColor="Navy" />

                                <asp:BoundField HeaderText="Invoice No."
                                    DataField="InvoiceNo"
                                    ItemStyle-HorizontalAlign="left"
                                    HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="12%"
                                    ItemStyle-BorderColor="Navy" />

                                <asp:BoundField HeaderText="Invoice Date"
                                    DataField="InvoiceDate"
                                    ItemStyle-HorizontalAlign="left"
                                    HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="6%"
                                    ItemStyle-BorderColor="Navy" />

                                <asp:BoundField HeaderText="Amount (Without GST)"
                                    DataField="AmtWithoutTax"
                                    ItemStyle-HorizontalAlign="right"
                                    HeaderStyle-HorizontalAlign="right"
                                    ItemStyle-Width="7%"
                                    ItemStyle-BorderColor="Navy" />

                                <asp:BoundField HeaderText="CGST Amount"
                                    DataField="CGST_Amt"
                                    ItemStyle-HorizontalAlign="right"
                                    HeaderStyle-HorizontalAlign="right"
                                    ItemStyle-Width="5%"
                                    ItemStyle-BorderColor="Navy" />
                                <%--	49--%>
                                <asp:BoundField HeaderText="SGST Amount"
                                    DataField="SGST_Amt"
                                    ItemStyle-HorizontalAlign="right"
                                    HeaderStyle-HorizontalAlign="right"
                                    ItemStyle-Width="5%"
                                    ItemStyle-BorderColor="Navy" />

                                <asp:BoundField HeaderText="IGST Amount"
                                    DataField="IGST_Amt"
                                    ItemStyle-HorizontalAlign="right"
                                    HeaderStyle-HorizontalAlign="right"
                                    ItemStyle-Width="5%"
                                    ItemStyle-BorderColor="Navy" />
                                <%--Payable_Amt_With_Tax / AmtWithTax--%>
                                <asp:BoundField HeaderText="Amount (With GST) (A)"
                                    DataField="AmtWithTax"
                                    ItemStyle-HorizontalAlign="right"
                                    HeaderStyle-HorizontalAlign="right"
                                    ItemStyle-Width="8%"
                                    ItemStyle-BorderColor="Navy" />
                                <asp:BoundField HeaderText="Direct Tax Type "
                                    DataField="DirectTax_Type"
                                    ItemStyle-HorizontalAlign="right"
                                    HeaderStyle-HorizontalAlign="right"
                                    ItemStyle-Width="6%"
                                    ItemStyle-BorderColor="Navy" />
                                <asp:BoundField HeaderText="Direct Tax(%)"
                                    DataField="DirectTax_Percentage"
                                    ItemStyle-HorizontalAlign="right"
                                    HeaderStyle-HorizontalAlign="right"
                                    ItemStyle-Width="5%"
                                    ItemStyle-BorderColor="Navy" />
                                <asp:BoundField HeaderText="Direct Tax Amount (B) "
                                    DataField="DirectTax_Amount"
                                    ItemStyle-HorizontalAlign="right"
                                    HeaderStyle-HorizontalAlign="right"
                                    ItemStyle-Width="7%"
                                    ItemStyle-BorderColor="Navy" />
                                <asp:BoundField HeaderText="Payable Amount (C=A-B) "
                                    DataField="Payable_Amt_With_Tax"
                                    ItemStyle-HorizontalAlign="right"
                                    HeaderStyle-HorizontalAlign="right"
                                    ItemStyle-Width="7%"
                                    ItemStyle-BorderColor="Navy" />

                                <asp:BoundField HeaderText="Paid Amount (D)"
                                    DataField="AccountPaidAmt"
                                    ItemStyle-HorizontalAlign="right"
                                    HeaderStyle-HorizontalAlign="right"
                                    ItemStyle-Width="6%"
                                    ItemStyle-BorderColor="Navy" NullDisplayText="0.00" />

                                <asp:BoundField HeaderText="Invoice Balance Amount (C-D)"
                                    DataField="BalanceAmt"
                                    ItemStyle-HorizontalAlign="right"
                                    HeaderStyle-HorizontalAlign="right"
                                    ItemStyle-Width="7%"
                                    ItemStyle-BorderColor="Navy" />
                                <asp:BoundField HeaderText="Payment Status"
                                    DataField="PyamentStatus"
                                    ItemStyle-HorizontalAlign="left"
                                    HeaderStyle-HorizontalAlign="left"
                                    ItemStyle-Width="5%"
                                    ItemStyle-BorderColor="Navy" />
                                <%--ImageUrl="~/Images/Create.png"--%>
                            </Columns>
                        </asp:GridView>
                        <%--</div>--%>
                    </ul>
                    <div runat="server" id="DivCreateInvoice" visible="false">
                        <div class="edit-contact">

                            <ul id="Ul2" runat="server">
                                <li class="trvl_date" style="padding-bottom: 20px">
                                    <span style="text-decoration: underline; color: #F28820; font-size: 16px" id="SPCreate" runat="server">Create Payment Request : </span>&nbsp;&nbsp;
                                    <br />
                                </li>
                                <li>
                                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 500; text-align: center;"></asp:Label>
                                </li>
                                <li></li>

                                <li class="trvl_date">
                                    <span>Invoice No</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtInvoiceNo" runat="server" Enabled="false"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span>Invoice Amount including GST (A)</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtInvoiceAmount" CssClass="txtAligntextBox" runat="server" Enabled="false"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span>Direct Tax Amount (if Any) (B)</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtInvoiceTDSAmt" CssClass="txtAligntextBox" runat="server" Enabled="false"></asp:TextBox>
                                </li>

                                <li class="trvl_date">
                                    <span>Invoice Paid Amount (C)</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtInvoicePaidAmount" CssClass="txtAligntextBox" runat="server" Enabled="false"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span>Payment Requested for (Amount)</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtPaymentRequestAmount" CssClass="txtAligntextBox" runat="server" Enabled="false"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span>Invoice Balance Amount (A-B-C)</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtInvoiceBalAmt" CssClass="txtAligntextBox" runat="server" Enabled="false"></asp:TextBox>
                                </li>

                                <li class="trvl_date">
                                    <span>Payment Request No </span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <br />
                                    <%--<asp:TextBox AutoComplete="off" ID="txtPaymentRequestNo" runat="server" Enabled="false"></asp:TextBox>--%>
                                    <asp:Label AutoComplete="off" ID="txtPaymentRequestNo" runat="server" Enabled="false" CssClass="PayNo"></asp:Label>

                                </li>
                                <li class="trvl_date">
                                    <span>Request Date </span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtRequestDate" runat="server" Enabled="false"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span>Amount to be paid </span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtAmountPaidWithTax" CssClass="number" runat="server"></asp:TextBox>
                                </li>
                                <li class="trvl_date" runat="server" id="Account1" visible="false">

                                    <span>Amount paid by Accounts </span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtAccountPaidAmt" Enabled="false" runat="server" CssClass="txtAligntextBox"></asp:TextBox>
                                </li>
                                <li class="trvl_date" runat="server" id="Account2" visible="false">
                                    <span>Balance Amount </span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtAccountAmtBal" CssClass="txtAligntextBox" runat="server" Enabled="false"></asp:TextBox>
                                </li>


                                <li class="trvl_date" runat="server" id="Account3" visible="false"></li>
                                <li class="trvl_date">
                                    <span>Upload Supporting Files</span>&nbsp;&nbsp;<%--<span style="color:red">*</span>--%><br />
                                    <asp:FileUpload ID="uploadfile" runat="server" AllowMultiple="true" />
                                    <%--<asp:LinkButton ID="lnkuplodedfile" OnClick="lnkuplodedfile_Click" runat="server"></asp:LinkButton>--%>
                                </li>
                                <li></li>
                                <li></li>

                                <asp:GridView ID="GrdFileUpload" DataKeyNames="Payment_ID" runat="server" CssClass="Milestones" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="50%">
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

                                        <asp:TemplateField HeaderText="File View" HeaderStyle-Width="8%">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnkFileView" runat="server" ToolTip=" File View" Width="15px" Height="15px" ImageUrl="~/Images/Download.png" OnClientClick=<%# "DownloadFile('" + Eval("FileName") + "')" %> />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="3%" Visible="false">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnkdelete" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/delete.png" OnClick="lnkdelete_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="File Name"
                                            DataField="FileName"
                                            ItemStyle-HorizontalAlign="left"
                                            ItemStyle-Width="35%"
                                            ItemStyle-BorderColor="Navy" />




                                    </Columns>
                                </asp:GridView>
                                <div>
                                    <span class="LableName" runat="server" id="Span1">Approvals:
                                    </span>
                                    <asp:GridView ID="DgvApprover" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="75%">
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
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <br />
                                <br />
                                <li class="trvl_Approver">
                                    <span class="LableName" runat="server" id="Span2">Invoice Approvers:</span>
                                    <asp:GridView ID="GrdInvoiceApr" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="75%">
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
                                                DataField="ApproverName"
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
                                                DataField="approved_on"
                                                ItemStyle-HorizontalAlign="left"
                                                ItemStyle-Width="12%"
                                                ItemStyle-BorderColor="Navy" />

                                            <asp:BoundField HeaderText="Approver Remarks"
                                                DataField="Remarks"
                                                ItemStyle-HorizontalAlign="left"
                                                ItemStyle-Width="46%"
                                                ItemStyle-BorderColor="Navy" />

                                            <asp:BoundField HeaderText="APPR_ID"
                                                DataField="Appr_ID"
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
                                                DataField="approver_emp_code"
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

                    <div class="Req_Savebtndiv" style="text-align: center">
                        <asp:LinkButton ID="localtrvl_btnSave" runat="server" Text="Save As Draft" ToolTip="Create Payment Request" CssClass="Savebtnsve" OnClick="localtrvl_btnSave_Click" OnClientClick="return SaveMultiClick();"> Create Payment Request </asp:LinkButton>
                        <asp:LinkButton ID="trvldeatils_btnSave" runat="server" Visible="false" Text="Submit" ToolTip="Cancel Request" CssClass="Savebtnsve" OnClick="trvldeatils_btnSave_Click" OnClientClick="return SaveMultiClick();">Cancel Request</asp:LinkButton>
                        <asp:LinkButton ID="trvldeatils_cancel_btn" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/VSCB_Index.aspx">    Back</asp:LinkButton>
                        <asp:LinkButton ID="accmo_delete_btn" runat="server" Text="Clear" Visible="false" ToolTip="Clear" CssClass="Savebtnsve" OnClick="accmo_delete_btn_Click">  Clear Data  </asp:LinkButton>

                    </div>

                    <div class="edit-contact">

                        <ul id="Ul1" runat="server">

                            <span class="LableName" runat="server" visible="false" id="SPPayHist">Payments History
                            </span>
                            <asp:GridView ID="GrdInvoiceHistDetails" DataKeyNames="Payment_ID" runat="server" CssClass="Milestones" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%">
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

                                    <asp:TemplateField HeaderText="View" HeaderStyle-Width="1%">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkViewhist" runat="server" ToolTip="View" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkViewhist_Click" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:BoundField HeaderText="Sr.No"
                                        DataField="Srno"
                                        ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="3%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Invoice No."
                                        DataField="InvoiceNo"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="15%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Payment Request No."
                                        DataField="PaymentReqNo"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="15%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Request Date"
                                        DataField="PaymentReqDate"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="10%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Amount to be Paid"
                                        DataField="TobePaidAmtWithtax"
                                        ItemStyle-HorizontalAlign="right"
                                        HeaderStyle-HorizontalAlign="right"
                                        ItemStyle-Width="10%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Amount Paid by Accounts"
                                        DataField="Amt_paid_Account"
                                        ItemStyle-HorizontalAlign="right"
                                        HeaderStyle-HorizontalAlign="right"
                                        ItemStyle-Width="10%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Balance Amount"
                                        DataField="BalanceAmt"
                                        ItemStyle-HorizontalAlign="right"
                                        HeaderStyle-HorizontalAlign="right"
                                        ItemStyle-Width="10%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Payment Status"
                                        DataField="PyamentStatus"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="left"
                                        ItemStyle-Width="10%"
                                        ItemStyle-BorderColor="Navy" />


                                </Columns>
                            </asp:GridView>

                        </ul>
                        <br />
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hflEmpDesignation" runat="server" />
    <asp:HiddenField ID="hflEmpDepartment" runat="server" />
    <asp:HiddenField ID="hflEmailAddress" runat="server" />
    <asp:HiddenField ID="hdnEmpCpde" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="FilePath" runat="server" />
    <asp:HiddenField ID="FileName" runat="server" />
    <asp:HiddenField ID="hdnType" runat="server" />
    <asp:HiddenField ID="hdnInvoiceID" runat="server" />
    <asp:HiddenField ID="hdnPayment_ID" runat="server" />
    <asp:HiddenField ID="hdnPOID" runat="server" />
    <asp:HiddenField ID="hdnapprcode" runat="server" />
    <asp:HiddenField ID="hdnCostCentre" runat="server" />
    <asp:HiddenField ID="hdnTallyCode" runat="server" />
    <asp:HiddenField ID="hdnDept_Name" runat="server" />
    <asp:HiddenField ID="hdnTobePaidAmt" runat="server" />
    <asp:HiddenField ID="hdnInvoicePayableAmt" runat="server" />
    <asp:HiddenField ID="hdnDirectTax_Type" runat="server" />
    <asp:HiddenField ID="hdnDirectTax_Percentage" runat="server" />
    <asp:HiddenField ID="hdnDirectTax_Amount" runat="server" />
    <asp:HiddenField ID="hdnPayable_Amt_Invoice" runat="server" />
    <asp:HiddenField ID="hdnInvoiceTaxAmount" runat="server" />
    <asp:HiddenField ID="hdnInvoiceBalAmt" runat="server" />
    <asp:HiddenField ID="hflStatusID" runat="server" />



    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />


    <script src="../js/freeze/jquery-ui.min.js"></script>
    <script src="../js/freeze/gridviewScroll.min.js"></script>

    <script type="text/javascript">      
        $(document).ready(function () {
            $(".DropdownListSearch").select2();

            $('#MainContent_GrdInvoiceDetails').gridviewScroll({
                width: 1070,
                height: 600,
                freezesize: 6, // Freeze Number of Columns.
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
        function onOnlyNumber(e) {
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
        function SaveMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=localtrvl_btnSave.ClientID%>');

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

        function DownloadFile(FileName) {
            var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;
            //window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
            window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);

        }

    </script>
</asp:Content>




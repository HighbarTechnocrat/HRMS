<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="VSCB_ViewAllDetailsApprovedBatch.aspx.cs" Inherits="procs_VSCB_ViewAllDetailsApprovedBatch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%--<link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <%--<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />

    <style>
        .noresize {
            resize: none;
        }

        #MainContent_lnkuplodedfile:link {
            color: red;
            background-color: transparent;
            text-decoration: none;
            font-size: 14px;
        }

        #MainContent_lnkuplodedfile:visited {
            color: red;
            background-color: transparent;
            text-decoration: none;
        }

        #MainContent_lnkuplodedfile:hover {
            color: green;
            background-color: transparent;
            text-decoration: none !important;
        }



        .LableName {
            color: #F28820;
            font-size: 16px;
            font-weight: normal;
            text-align: left;
        }

        .PayNo {
            font-family: 13px !important;
            font-weight: bold;
        }

        .BalAmt {
            background-color: yellow;
            font-weight: bold;
            color: red
        }

        .textboxBackColor {
            background-color: cadetblue;
            color: aliceblue;
            text-align: right;
        }

        .textboxalignAmount {
            text-align: right;
        }
        /*InvoiceDetails*/

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

        .LableFiles {
            color: darkorchid;
            font-size: 14px;
            font-weight: normal;
            text-align: left;
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
                        <asp:Label ID="lblheading" runat="server" Text=" View Approved Batch Details"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>
                <div>
                    <span>
                        <a href="vscb_index.aspx" class="aaaa"><%=System.Configuration.ConfigurationManager.AppSettings["VSCBPageTitle"]%></a>
                    </span>
                    <span>
                        <a href="VSCB_ViewAllDetails.aspx" title="Back" runat="server" visible="true" id="btnRecBack" style="margin-right: 10px;" class="aaaa">Back</a>
                    </span>
                </div>

                <div class="edit-contact">
                    <ul id="editform" runat="server">
                        <li class="trvl_date">
                            <span>PO/ WO Number  </span>
                            <br />
                            <%--<asp:DropDownList runat="server" Enabled="false" ID="lstPOWONumber" CssClass="DropdownListSearch">
							</asp:DropDownList>--%>
                            <asp:TextBox AutoComplete="off" ID="txtPOWONumber" runat="server" Enabled="false"></asp:TextBox>

                            <br />
                        </li>

                        <li class="trvl_date">
                            <span>PO/ WO Date </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtPoWoDate" runat="server" Enabled="false"></asp:TextBox>
                        </li>

                        <li class="trvl_date">
                            <span>PO/ WO Type </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtPoWoType" runat="server" Enabled="false"></asp:TextBox>
                            <span style="display: none">PO/ WO Title </span>
                            <asp:TextBox AutoComplete="off" ID="txtPoWoTitle" runat="server" Enabled="false" Visible="false"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>Vendor Name </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtVendorName" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>GSTIN No. </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtGSTINNO" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>Cost Center </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtProjectName" runat="server" Enabled="false"></asp:TextBox>

                            <span style="display: none">Project/Department</span>
                            <asp:TextBox AutoComplete="off" ID="txtDepartment" runat="server" Enabled="false" Visible="false"></asp:TextBox>
                        </li>

                        <li class="trvl_date">
                            <span>PO/ WO Status </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtPoWoStatus" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>Currency </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtCurrency" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li>
                            <span>PO/ WO Amount (Without GST) </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtPoWOAmtWIthoutTax" runat="server" Enabled="false" CssClass="textboxBackColor AmtTextAlign"></asp:TextBox>
                        </li>

                        <li class="trvl_date">
                            <span>PO/ WO Amount (With GST) (A)</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtPoWoAmtWithTaxes" runat="server" Enabled="false" CssClass="textboxBackColor AmtTextAlign"></asp:TextBox>
                        </li>

                        <li class="trvl_date">
                            <span>Direct Tax Amount (B)</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtPODirectTaxAmt" runat="server" Enabled="false" CssClass="textboxBackColor AmtTextAlign"></asp:TextBox>

                        </li>
                        <li class="trvl_date">
                            <span>Paid Amount (C)</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtPoWOPaidAmt" runat="server" Enabled="false" Visible="false" CssClass="textboxBackColor AmtTextAlign"></asp:TextBox>
                            <asp:TextBox AutoComplete="off" ID="txtPoPaidAmt_WithOutDT" runat="server" ReadOnly="true" Enabled="False" CssClass="textboxBackColor AmtTextAlign"></asp:TextBox>

                        </li>

                        <li class="trvl_date">
                            <span id="spnPOWOSettelmentAmt" runat="server">Settlement Amount (D)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPOWOSettelmentAmt" runat="server" ReadOnly="true" Enabled="False" CssClass="textboxBackColor AmtTextAlign"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>Balance Amount (A-B-C-D)</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtPoWOPaidBalAmt" runat="server" Enabled="false" CssClass="textboxBackColor AmtTextAlign"></asp:TextBox>
                        </li>

                        <li class="trvl_date">
                            <span id="spnPOWOSignCopy" runat="server" visible="false">PO/ WO Sign Copy</span><br />
                            <asp:LinkButton ID="lnkfile_PO" runat="server" OnClientClick="Download_POSignCopyFile()" CssClass="BtnShow" Visible="false"></asp:LinkButton>
                        </li>

                        <li class="trvl_date">
                            <div class="manage_grid" style="overflow: hidden; width: 130%; margin-bottom: 4px;">
                                <span id="spnSettlemnt_N" style="font-size: 13px !important">Settlement = Discount/Correction/Settlement/Deduction </span>
                            </div>
                        </li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>

                        <div>
                            <br />
                            <span class="LableName" runat="server" visible="true" id="spMilestones">PO/ WO Milestones
                            </span>
                            <asp:GridView ID="DgvMilestones" runat="server" CssClass="Milestones" DataKeyNames="MstoneID,POID" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="120%">
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
                                    <asp:BoundField HeaderText="Milestone No."
                                        DataField="Srno"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="left"
                                        ItemStyle-Width="4%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Milestone Particulars"
                                        DataField="MilestoneName"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="left"
                                        ItemStyle-Width="15%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Milestone Due Date"
                                        DataField="Milestone_due_date"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="7%" ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="UOM"
                                        DataField="UOM"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />


                                    <asp:BoundField HeaderText="Quantity"
                                        DataField="Quantity"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Rate"
                                        DataField="Rate"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Amount (Without GST)"
                                        DataField="Amount"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="CGST Amount"
                                        DataField="CGST_Amt"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="SGST Amount"
                                        DataField="SGST_Amt"
                                          ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="IGST Amount"
                                        DataField="IGST_Amt"
                                          ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Amount (With  GST) (A)"
                                        DataField="AmtWithTax"
                                         ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Direct Tax Amount (B)"
                                        DataField="Collect_TDS_Amt"
                                         ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Paid Amount (C)"
                                        DataField="milestonepaidAmt"
                                          ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Settlement Amount (D)"
                                        DataField="Milestone_settlementAmt"
                                        ItemStyle-HorizontalAlign="right"
                                        HeaderStyle-HorizontalAlign="right"
                                        ItemStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />


                                    <asp:BoundField HeaderText="Balance Amount (A-B-C-D)"
                                        DataField="Milesstone_Balance_Amt"
                                          ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Payment Status"
                                        DataField="PyamentStatus"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="6%"
                                        ItemStyle-BorderColor="Navy" />
                                </Columns>
                            </asp:GridView>
                        </div>



                        <br />
                        <hr />
                        <span class="LableName" runat="server" visible="true" id="spInvoice">Invoice Details
                        </span>

                        <asp:GridView ID="GrdInvoiceDetails" CssClass="Milestones" runat="server" DataKeyNames="MstoneID,InvoiceID" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="130%">
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
                                <asp:BoundField HeaderText="Milestone No."
                                    DataField="Srno"
                                    ItemStyle-HorizontalAlign="left"
                                    HeaderStyle-HorizontalAlign="left"
                                    ItemStyle-Width="4%"
                                    ItemStyle-BorderColor="Navy" />
                                <asp:BoundField HeaderText="Milestone Particulars"
                                    DataField="MilestoneName"
                                    ItemStyle-HorizontalAlign="left"
                                    HeaderStyle-HorizontalAlign="left"
                                    ItemStyle-Width="22%"
                                    ItemStyle-BorderColor="Navy" />


                                <asp:BoundField HeaderText="Milestone Amount for Invoice (Without GST) (A)"
                                    DataField="Milesstone_Amt_ForInvoice"
                                    ItemStyle-HorizontalAlign="right"
                                    HeaderStyle-HorizontalAlign="right"
                                    ItemStyle-Width="10%"
                                    ItemStyle-BorderColor="Navy" />

                                <asp:BoundField HeaderText="CGST Amount"
                                    DataField="CGST_Amt"
                                    ItemStyle-HorizontalAlign="right"
                                    HeaderStyle-HorizontalAlign="right"
                                    ItemStyle-Width="8%"
                                    ItemStyle-BorderColor="Navy" />

                                <asp:BoundField HeaderText="SGST Amount"
                                    DataField="SGST_Amt"
                                    ItemStyle-HorizontalAlign="right"
                                    HeaderStyle-HorizontalAlign="right"
                                    ItemStyle-Width="8%"
                                    ItemStyle-BorderColor="Navy" />

                                <asp:BoundField HeaderText="IGST Amount"
                                    DataField="IGST_Amt"
                                    ItemStyle-HorizontalAlign="right"
                                    HeaderStyle-HorizontalAlign="right"
                                    ItemStyle-Width="8%"
                                    ItemStyle-BorderColor="Navy" />

                                <asp:BoundField HeaderText="Milestone Amount for Invoice (With GST) (B)"
                                    DataField="MilestoneAmt_WithTax_ForInvoice"
                                    ItemStyle-HorizontalAlign="right"
                                    HeaderStyle-HorizontalAlign="right"
                                    ItemStyle-Width="10%"
                                    ItemStyle-BorderColor="Navy" />

                                <asp:BoundField HeaderText="Direct Tax Amount (C)"
                                    DataField="Collect_TDS_Amt"
                                    ItemStyle-HorizontalAlign="right"
                                    HeaderStyle-HorizontalAlign="right"
                                    ItemStyle-Width="9%"
                                    ItemStyle-BorderColor="Navy" />


                                <asp:BoundField HeaderText="Milestone Payable Amount for Invoice (D=B-C)"
                                    DataField="Milesstone_PaybleInvoice_Amt"
                                    ItemStyle-HorizontalAlign="right"
                                    HeaderStyle-HorizontalAlign="right"
                                    ItemStyle-Width="10%"
                                    ItemStyle-BorderColor="Navy" />

                                <asp:BoundField HeaderText="Milestone Settelement Amount"
                                    DataField="Milestone_settlementAmt"
                                    ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-Width="10%"
                                    ItemStyle-BorderColor="Navy" />

                                <asp:BoundField HeaderText="Payment Status"
                                    DataField="PyamentStatus"
                                    ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="left"
                                    ItemStyle-Width="9%"
                                    ItemStyle-BorderColor="Navy" />
                            </Columns>
                        </asp:GridView>

                        <asp:GridView ID="GrdInvoiceDetails_1" Visible="false" CssClass="Milestones" runat="server" DataKeyNames="MstoneID,InvoiceID" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="130%">
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

                                <asp:BoundField HeaderText="Sr No."
                                    DataField="Srno"
                                    ItemStyle-HorizontalAlign="left"
                                    HeaderStyle-HorizontalAlign="left"
                                    ItemStyle-Width="3%"
                                    ItemStyle-BorderColor="Navy" />

                                <asp:BoundField HeaderText="Milestone Particulars"
                                    DataField="MilestoneName"
                                    ItemStyle-HorizontalAlign="left"
                                    HeaderStyle-HorizontalAlign="left"
                                    ItemStyle-Width="15%"
                                    ItemStyle-BorderColor="Navy" />

                                <asp:BoundField HeaderText="Invoice No."
                                    DataField="InvoiceNo"
                                    ItemStyle-HorizontalAlign="left"
                                    HeaderStyle-HorizontalAlign="left"
                                    ItemStyle-Width="8%"
                                    ItemStyle-BorderColor="Navy" />

                                <asp:BoundField HeaderText="Invoice Date"
                                    DataField="InvoiceDate"
                                    ItemStyle-HorizontalAlign="left"
                                    HeaderStyle-HorizontalAlign="left"
                                    ItemStyle-Width="5%"
                                    ItemStyle-BorderColor="Navy" />

                                <asp:BoundField HeaderText="Amount (Without GST)"
                                    HeaderStyle-HorizontalAlign="Center"
                                    DataField="AmtWithoutTax"
                                    ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Width="6%"
                                    ItemStyle-BorderColor="Navy" />

                                <asp:BoundField HeaderText="CGST Amount"
                                    DataField="CGST_Amt"
                                    ItemStyle-HorizontalAlign="center"
                                    ItemStyle-Width="5%"
                                    ItemStyle-BorderColor="Navy" />
                                <%--	49--%>
                                <asp:BoundField HeaderText="SGST Amount"
                                    DataField="SGST_Amt"
                                    ItemStyle-HorizontalAlign="center"
                                    ItemStyle-Width="5%"
                                    ItemStyle-BorderColor="Navy" />

                                <asp:BoundField HeaderText="IGST Amount"
                                    DataField="IGST_Amt"
                                    ItemStyle-HorizontalAlign="center"
                                    ItemStyle-Width="5%"
                                    ItemStyle-BorderColor="Navy" />
                                <asp:BoundField HeaderText="Amount (With GST) (A)"
                                    DataField="AmtWithTax"
                                    HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="center"
                                    ItemStyle-Width="6%"
                                    ItemStyle-BorderColor="Navy" />
                                <asp:BoundField HeaderText="Direct Tax Type "
                                    DataField="DirectTax_Type"
                                    ItemStyle-HorizontalAlign="center"
                                    ItemStyle-Width="5%"
                                    ItemStyle-BorderColor="Navy" />
                                <asp:BoundField HeaderText="Direct Tax (%)"
                                    DataField="DirectTax_Percentage"
                                    ItemStyle-HorizontalAlign="center"
                                    ItemStyle-Width="5%"
                                    ItemStyle-BorderColor="Navy" />
                                <asp:BoundField HeaderText="Direct Tax Amount (B) "
                                    DataField="DirectTax_Amount"
                                    ItemStyle-HorizontalAlign="center"
                                    ItemStyle-Width="7%"
                                    ItemStyle-BorderColor="Navy" />
                                <asp:BoundField HeaderText="Payable Amount (C=A-B) "
                                    DataField="Payable_Amt_With_Tax"
                                    ItemStyle-HorizontalAlign="center"
                                    ItemStyle-Width="7%"
                                    ItemStyle-BorderColor="Navy" />
                                <asp:BoundField HeaderText="Paid Amount (D) "
                                    DataField="AccountPaidAmt"
                                    ItemStyle-HorizontalAlign="center"
                                    ItemStyle-Width="6%"
                                    ItemStyle-BorderColor="Navy" />

                                <asp:BoundField HeaderText="Invoice Balance Amount (C-D)"
                                    DataField="BalanceAmt"
                                    ItemStyle-HorizontalAlign="center"
                                    ItemStyle-Width="7%"
                                    ItemStyle-BorderColor="Navy" />
                                <asp:BoundField HeaderText="Payment Status"
                                    DataField="PyamentStatus"
                                    ItemStyle-HorizontalAlign="center"
                                    ItemStyle-Width="5%"
                                    ItemStyle-BorderColor="Navy" />
                                <%--ImageUrl="~/Images/Create.png"--%>
                            </Columns>
                        </asp:GridView>

                    </ul>
                    <div runat="server" id="DivCreateInvoice" visible="true">
                        <div class="edit-contact">

                            <ul id="Ul2" runat="server">

                                <li class="trvl_date">
                                    <span>Invoice No</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtInvoiceNo" runat="server" Enabled="false"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span>Invoice Amount including GST (A)</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtInvoiceAmount" runat="server" Enabled="false"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span>Direct Tax Amount (if Any) (B)</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtInvoiceTDSAmt" runat="server" Enabled="false"></asp:TextBox>
                                </li>

                                <li class="trvl_date">
                                    <span>Invoice Paid Amount (C)</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtInvoicePaidAmount" runat="server" Enabled="false"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span>Invoice Balance Amount (A-B-C)</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtInvoiceBalAmt" runat="server" Enabled="false"></asp:TextBox>
                                    <span style="display: none">Payment Requested for (Amount)</span>
                                    <asp:TextBox AutoComplete="off" ID="txtPaymentRequestAmount" runat="server" Enabled="false" Visible="false"></asp:TextBox>
                                </li>
                                <li class="trvl_date"></li>

                                <%--InvoiceFile--%>

                                <li>
                                    <span>Invoice File</span>
                                    <br />
                                    <asp:LinkButton ID="lnkfile_Invoice" runat="server" OnClientClick="DownloadFile_S()" CssClass="BtnShow"></asp:LinkButton>
                                    <br />
                                </li>
                                <li></li>
                                <li></li>

                                <li id="spnSupportinFiles" runat="server" visible="false">
                                    <span>Invoice Supporting Files</span>

                                </li>

                                <li id="spnSupportinFiles1" runat="server" visible="false"></li>
                                <li id="spnSupportinFiles2" runat="server" visible="false"></li>

                                <li id="liInvoiceUploadFile" runat="server" visible="false">
                                    <asp:GridView ID="gvuploadedFiles" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="50%" EditRowStyle-Wrap="false"
                                        DataKeyNames="fileid">
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
                                                    <asp:ImageButton ID="lnkDeleteexpFile" runat="server" ToolTip=" File View" Width="15px"
                                                        Height="15px" ImageUrl="~/Images/Download.png"
                                                        OnClientClick=<%# "DownloadFile1('" + Eval("FileName") + "')" %> />

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField HeaderText="File Name"
                                                DataField="filename"
                                                ItemStyle-HorizontalAlign="left"
                                                ItemStyle-Width="35%"
                                                ItemStyle-BorderColor="Navy" />

                                        </Columns>
                                    </asp:GridView>

                                </li>
                                <li id="liInvoiceUploadFile2" runat="server" visible="false"></li>
                                <li id="liInvoiceUploadFile3" runat="server" visible="false"></li>

                                <li>
                                    <br />
                                    <span class="LableName" runat="server" id="Span1">Invoice Approvals:
                                    </span>
                                </li>
                                <li></li>
                                <li></li>

                                <li class="trvl_grid">

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
                                        </Columns>
                                    </asp:GridView>

                                </li>
                                <li></li>
                                <li></li>

                                <hr />
                                <li class="trvl_date">
                                    <span class="LableName" runat="server" visible="true" id="Span2">Payment Details
                                    </span>
                                    <br />
                                    <br />
                                </li>
                                <li class="trvl_date"></li>
                                <li class="trvl_date"></li>

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
                                    <span>Amount to be paid</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtAmountPaidWithTax" CssClass="number" Enabled="false" runat="server"></asp:TextBox>
                                </li>

                                <li class="trvl_date" runat="server" id="Account1">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <span>Amount paid by Accounts </span>&nbsp;&nbsp;<span style="color: red">*</span>
                                            <br />
                                            <asp:TextBox AutoComplete="off" ID="txtAccountPaidAmt" runat="server" CssClass="number"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </li>

                                <li class="trvl_date" runat="server" id="Account2">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <span>Balance Amount </span>&nbsp;&nbsp;<span style="color: red">*</span>
                                            <br />
                                            <asp:TextBox AutoComplete="off" ID="txtAccountAmtBal" runat="server" Enabled="false"></asp:TextBox>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </li>

                                <li class="trvl_date" runat="server" id="Account3"></li>


                                <li class="trvl_date">
                                    <span id="spnPaymentSupportingFile" runat="server" visible="false">Payment Supporting Files</span>&nbsp;&nbsp;<br />
                                    <asp:FileUpload ID="uploadfile" runat="server" AllowMultiple="true" Visible="false" />
                                </li>
                                <li></li>
                                <li></li>

                                <li class="trvl_grid">
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

                                            <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="5%" Visible="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="lnkdelete" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/delete.png" />
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
                                </li>


                                <li>
                                    <br />
                                    <span class="LableName" runat="server" id="Span3">Payment Approvals:
                                    </span>
                                </li>
                                <li></li>
                                <li></li>

                                <li class="trvl_grid">
                                    <asp:GridView ID="DgvApproverPayments" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="75%">
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

                                </li>
                                <li></li>
                                <li></li>


                                <hr />

                                <li style="padding-top: 15px">
                                    <span class="LableName">Batch Details</span>
                                </li>
                                <li style="padding-top: 15px"></li>
                                <li style="padding-top: 15px"></li>

                                <li class="trvl_date" style="padding-top: 15px">
                                    <span>Batch Created Date:</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtbatchCreateDate" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span>Batch Created by:</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtbatchCreatedBy" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span>Batch No.:</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtbatchNo" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                                </li>


                                <li class="trvl_date">
                                    <span>Batch no. of Requests:</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtbatchNoOfRequest" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span>Batch Total Payament:</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtbatchTotalPayment" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span>Bank Name:</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtBank_name" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                                </li>


                                <li class="trvl_date">
                                    <span>Bank Ref.No:</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtBankRefNo" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span>Bank Ref.Date:</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtBankRefDate" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                                </li>
                                <li class="trvl_date" style="display: none">
                                    <span style="display: none">Bank Ref.Link:</span>
                                    <a id="lnkBank" runat="server" class="BtnShow" target="_blank"></a>
                                    <asp:TextBox AutoComplete="off" ID="txtBankRef_Link" runat="server" ReadOnly="true" Enabled="False" Visible="false"></asp:TextBox>
                                </li>

                                <li class="trvl_date">
                                    <span id="spnInvoicefile" runat="server" class="LableFiles" visible="false">Batch Transaction File</span>&nbsp;&nbsp;<br />
                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lnkfile_Invoice_Click" CssClass="BtnShow" Visible="false"></asp:LinkButton><br />
                                    <br />
                                </li>


                                <li style="padding-top: 15px">
                                    <span class="LableName" style="float: left;">Batch Approvals:</span>
                                </li>


                                <li class="trvl_Approver">

                                    <asp:GridView ID="DgvApproverBatch" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="75%">
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
                                                ItemStyle-Width="33%"
                                                ItemStyle-BorderColor="Navy" />

                                            <asp:BoundField HeaderText="Status"
                                                DataField="Status"
                                                ItemStyle-HorizontalAlign="left"
                                                ItemStyle-Width="12%"
                                                ItemStyle-BorderColor="Navy" />

                                            <asp:BoundField HeaderText="Approved on"
                                                DataField="approved_on"
                                                ItemStyle-HorizontalAlign="left"
                                                ItemStyle-Width="15%"
                                                ItemStyle-BorderColor="Navy" />

                                            <asp:BoundField HeaderText="Approver Remarks"
                                                DataField="Remarks"
                                                ItemStyle-HorizontalAlign="left"
                                                ItemStyle-Width="37%"
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
                                <li class="trvl_date"></li>
                                <li class="trvl_date"></li>
                            </ul>



                        </div>
                    </div>


                </div>

            </div>
        </div>

    </div>

    <div class="trvl_Savebtndiv">
        <asp:LinkButton ID="btnback_mng" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/VSCB_ViewAllDetails.aspx">Back</asp:LinkButton>
        <asp:LinkButton ID="btnCorrection" runat="server" Text="Download PO/ WO" ToolTip="Download PO/ WO" CssClass="Savebtnsve" OnClientClick="Download_ApprovedPO()">Download PO/ WO</asp:LinkButton>
    </div>


    <asp:HiddenField ID="hdnEmpCode" runat="server" />
    <asp:HiddenField ID="FilePath" runat="server" />
    <asp:HiddenField ID="FilePathInvoice" runat="server" />
    <asp:HiddenField ID="hdnInvoiceID" runat="server" />
    <asp:HiddenField ID="hdnPayment_ID" runat="server" />
    <asp:HiddenField ID="hdnPOID" runat="server" />
    <asp:HiddenField ID="hdnPOTypeID" runat="server" />
    <asp:HiddenField ID="hdnExtraAPP" runat="server" />
    <asp:HiddenField ID="hdnCostCentre" runat="server" />
    <asp:HiddenField ID="hdnTallyCode" runat="server" />
    <asp:HiddenField ID="hdnDept_Name" runat="server" />
    <asp:HiddenField ID="hdnApprovertype" runat="server" />
    <asp:HiddenField ID="hdnEmpCodePrve" runat="server" />
    <asp:HiddenField ID="hdnEmpCodePrveName" runat="server" />
    <asp:HiddenField ID="hdnEmpCodePrveEmailID" runat="server" />
    <asp:HiddenField ID="hdnLoginUserName" runat="server" />
    <asp:HiddenField ID="hdnLoginEmpEmail" runat="server" />

    <asp:HiddenField ID="hdnApprovedPO_FileName" runat="server" />
    <asp:HiddenField ID="hdnApprovedPO_FilePath" runat="server" />
    <asp:HiddenField ID="hdnSingPOCopyFilePath" runat="server" />
    <asp:HiddenField ID="hdnBatchId" runat="server" />

    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <script src="../js/freeze/jquery-ui.min.js"></script>
    <script src="../js/freeze/gridviewScroll.min.js"></script>


    <script type="text/javascript">      
        $(document).ready(function () {
            $(".DropdownListSearch").select2();

            $('#MainContent_DgvMilestones').gridviewScroll({
                width: 1070,
                height: 600,
                freezesize: 6, // Freeze Number of Columns.
                headerrowcount: 1, //Freeze Number of Rows with Header.
            });

            $('#MainContent_GrdInvoiceDetails').gridviewScroll({
                width: 1070,
                height: 600,
                freezesize: 3, // Freeze Number of Columns.
                headerrowcount: 1, //Freeze Number of Rows with Header.
            });

        });

        function DownloadFile(FileName) {
            var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;

            window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
            //window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
            //window.open("https://192.168.21.193/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
        }

        function DownloadFile1(FileName) {
            var localFilePath = document.getElementById("<%=FilePathInvoice.ClientID%>").value;

            window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
            //window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);

        }


        function DownloadFile_S() {

            var localFilePath = document.getElementById("<%=FilePathInvoice.ClientID%>").value;
            var localFileName = document.getElementById("<%=lnkfile_Invoice.ClientID%>").innerText;

            //alert(localFilePath);
            //  alert(localFileName);

            //window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
            //window.open("https://192.168.21.193/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
            window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + localFileName);
        }

        function Download_POSignCopyFile() {
            // alert(file);
            var localFilePath = document.getElementById("<%=hdnSingPOCopyFilePath.ClientID%>").value;
            var localFileName = document.getElementById("<%=lnkfile_PO.ClientID%>").innerText;

            //alert(localFilePath);
            window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + localFileName);

            // window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + localFileName);

        }


        function Download_ApprovedPO() {
            // alert(file);
            var localFilePath = document.getElementById("<%=hdnApprovedPO_FilePath.ClientID%>").value;
            var localFileName = document.getElementById("<%=hdnApprovedPO_FileName.ClientID%>").value;


            window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + localFileName);
            //  window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + localFileName);

        }

    </script>
</asp:Content>


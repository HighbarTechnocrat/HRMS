<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" EnableViewState="true"
    MaintainScrollPositionOnPostback="true" CodeFile="VSCB_CreateInvoiceACC.aspx.cs" Inherits="VSCB_CreateInvoiceACC" EnableSessionState="True" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <style>
        .Dropdown {
            border-bottom: 2px solid #cccccc;
            background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;
            cursor: default;
        }

        .LableName {
            color: #F28820;
            font-size: 16px;
            font-weight: normal;
            text-align: left;
        }

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

        /*InvoiceDetails*/
        .LableName {
            color: #F28820;
            font-size: 16px;
            font-weight: normal;
            text-align: left;
        }

        .textboxBackColor {
            background-color: cadetblue;
            color: aliceblue;
        }

        .POWOContentTextArea {
            width: 750px !important;
            height: 400px !important;
            overflow: auto;
        }

        .InvoiceRemarksText {
              width: 755px;
              height: 140px;
              overflow: auto;
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
                        <asp:Label ID="lblheading" runat="server" Text="Create Invoice"></asp:Label>
                    </span>
                </div>
                <div>
                    <span>
                          <a href="vscb_index.aspx" class="aaaa"><%=System.Configuration.ConfigurationManager.AppSettings["VSCBPageTitle"]%></a>
                    </span>
                </div>
                <div class="edit-contact">

                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" Visible="false"></asp:LinkButton>
                        </div>
                    </div>

                    <ul>
                        <li class="trvl_date">
                            <asp:CheckBox ID="chkIsInvoiceWithoutPO" Visible="false" runat="server" Text="Is Invoice Without PO" OnCheckedChanged="chkIsInvoiceWithoutPO_CheckedChanged" AutoPostBack="true" />
                        </li>

                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>
                    </ul>
                    <ul id="editform" runat="server" visible="true">
                        <li class="trvl_type">
                            <span>PO/ WO Number</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                            <asp:DropDownList runat="server" ID="lstTripType" AutoPostBack="true" CssClass="DropdownListSearch" OnSelectedIndexChanged="lstTripType_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:TextBox AutoComplete="off" ID="txtTriptype" runat="server" CssClass="Dropdown" Visible="false"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>PO/ WO Type</span>
                            <br />
                            <asp:DropDownList runat="server" ID="lstPOType" AutoPostBack="false" CssClass="DropdownListSearch">
                            </asp:DropDownList>
                            <asp:TextBox AutoComplete="off" ID="txtPOtype" runat="server" CssClass="Dropdown" Visible="false"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>PO/ WO Date </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtFromdate" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>


                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>

                        <li class="trvl_date">
                            <span>PO/ WO Title</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPOTitle" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>Vendor Name</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtVendor" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>GSTIN No.</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtGSTIN_No" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>


                        <li class="trvl_date">
                            <span>Cost Center</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtCostCenter" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>Project/Department</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtProject" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>

                        </li>
                        <li class="trvl_date">
                            <span>PO/ WO Status</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPOStatus" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>


                        <li class="trvl_date">
                            <span>PO/ WO Amount (Without GST)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtBasePOWOWAmt" runat="server" ReadOnly="true" Enabled="False" CssClass="textboxBackColor"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>PO/ WO Amount (With GST) (A)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPOWOAmt" runat="server" ReadOnly="true" Enabled="False" CssClass="textboxBackColor"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>Direct Tax Amount (B)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtDiretTaxAmt" runat="server" ReadOnly="true" Enabled="False" CssClass="textboxBackColor"></asp:TextBox>
                        </li>

                        <li class="trvl_date">
                            <span>Paid Amount (C)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPaidAmt" runat="server" ReadOnly="true" Enabled="False" Visible="false" CssClass="textboxBackColor"></asp:TextBox>
                            <asp:TextBox AutoComplete="off" ID="txtPoPaidAmt_WithOutDT" runat="server" ReadOnly="true" Enabled="False" CssClass="textboxBackColor"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>Balance Amount (A-B-C)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtBalanceAmt" runat="server" ReadOnly="true" Enabled="False" CssClass="textboxBackColor"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span id="spnPOWODShortClosedAmt" runat="server" visible="false">PO/ WO Short Closed Amount</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPOWOShortClosedAmt" runat="server" ReadOnly="true" Enabled="False" Visible="false" CssClass="textboxBackColor"></asp:TextBox>

                            <span id="spnPOWOSignCopy" runat="server" visible="false">PO/ WO Sign Copy</span><br />
                            <asp:LinkButton ID="lnkfile_PO" runat="server" OnClick="lnkfile_PO_Click" CssClass="BtnShow" Visible="false"></asp:LinkButton>
                        </li>
                    </ul>

                    <ul id="editform1" runat="server">
                        <li class="trvl_grid" id="litrvlgrid" runat="server">
                            <div>
                                <span class="LableName" runat="server" visible="false" id="spMilestones">Milestone Details</span>
                                <asp:GridView ID="dgTravelRequest" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="130%" EditRowStyle-Wrap="false"
                                    DataKeyNames="srno,PaymentStatusID,MstoneID">
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
                                        <asp:TemplateField HeaderText="Invoice" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkTravelDetailsEdit" CssClass="BtnShow" runat="server" Text='Create' OnClick="lnkTravelDetailsEdit_Click" Visible='<%# Eval("createbtn").ToString() == "1" ? false : true %>'></asp:LinkButton>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="5%" />
                                        </asp:TemplateField>

									<asp:BoundField HeaderText="Milestone No."
										DataField="Srno"
										ItemStyle-HorizontalAlign="left"
										HeaderStyle-HorizontalAlign="Left"
										ItemStyle-Width="4%"
										ItemStyle-BorderColor="Navy" />

									<asp:BoundField HeaderText="Milestone Particulars"
										DataField="MilestoneName"
										ItemStyle-HorizontalAlign="left"
										 HeaderStyle-HorizontalAlign="Left"
										ItemStyle-Width="20%"
										ItemStyle-BorderColor="Navy" />

                                     <asp:BoundField HeaderText="Milestone Due Date"
                                            DataField="Milestone_due_date"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="6%" ItemStyle-BorderColor="Navy" />

									<asp:BoundField HeaderText="Quantity"
										DataField="Quantity"
										ItemStyle-HorizontalAlign="Right"
										 HeaderStyle-HorizontalAlign="Right"
										ItemStyle-Width="4%"
										ItemStyle-BorderColor="Navy" />

									<asp:BoundField HeaderText="Rate"
										DataField="Rate"
										ItemStyle-HorizontalAlign="Right"
										 HeaderStyle-HorizontalAlign="Right"
										ItemStyle-Width="5%"
										ItemStyle-BorderColor="Navy" />

									<asp:BoundField HeaderText="Amount (Without GST)"
										DataField="Amount"
										ItemStyle-HorizontalAlign="Right"
										 HeaderStyle-HorizontalAlign="Right"
										ItemStyle-Width="6%"
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
										ItemStyle-Width="6%"
										ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Direct Tax Amount (B)"  
										DataField="Collect_TDS_Amt"
										ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
										ItemStyle-Width="6%"
										ItemStyle-BorderColor="Navy" NullDisplayText="0.00"/>  

                                    <asp:BoundField HeaderText="Paid Amount (C)"
										DataField="milestonepaidAmt"
										ItemStyle-HorizontalAlign="Right"
                                         HeaderStyle-HorizontalAlign="Right"
										ItemStyle-Width="6%"
										ItemStyle-BorderColor="Navy" />

                                       <asp:BoundField HeaderText="Balance Amount (A-B-C)"
										DataField="Milesstone_Balance_Amt"
										ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
										ItemStyle-Width="7%"
										ItemStyle-BorderColor="Navy" />  

									<asp:BoundField HeaderText="Payment Status"
										DataField="PyamentStatus"
										ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
										ItemStyle-Width="5%"
										ItemStyle-BorderColor="Navy" />
								</Columns>

                                </asp:GridView>

                                <asp:GridView ID="gvShortCloseMilestone" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                    DataKeyNames="srno,PaymentStatusID,MstoneID">
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


                                        <asp:BoundField HeaderText="Milestone No"
                                            DataField="srno"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Milestone Particular"
                                            DataField="MilestoneName"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="20%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Quantity"
                                            DataField="Quantity"
                                            ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Rate"
                                            DataField="Rate"
                                            ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Milestone Amount"
                                            DataField="Amount"
                                            ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="CGST Amount"
                                            DataField="CGST_Amt"
                                            ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="SGST Amount"
                                            DataField="SGST_Amt"
                                            ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="IGST Amount"
                                            DataField="IGST_Amt"
                                            ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Total Amount"
                                            DataField="AmtWithTax"
                                            ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Payment Status"
                                            DataField="PyamentStatus"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Short Close Amount"
                                            DataField="shortClosedAmt"
                                            ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />


                                    </Columns>
                                </asp:GridView>

                            </div>
                        </li>

                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>


                        <div>
                            <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                        </div>
                        <br />

                        <li>
                            <span id="Span1" runat="server" class="LableName">Invoice Details</span>
                        </li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>


                        <li class="trvl_date" id="idWithoutInv_ProjectDept" runat="server">
                            <span>Project/Department</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList runat="server" ID="lstProjectDept" AutoPostBack="true" CssClass="DropdownListSearch" OnSelectedIndexChanged="lstProjectDept_SelectedIndexChanged">
                            </asp:DropDownList>

                        </li>
                        <li class="trvl_date" id="idWithoutInv_Vendor" runat="server">
                            <span>Cost Center</span>
                            <br />
                            <asp:DropDownList runat="server" ID="lstCostCenter" CssClass="DropdownListSearch" Enabled="false">
                            </asp:DropDownList>
                        </li>
                        <li class="trvl_date" id="idWithoutInv_Blank" runat="server">
                            <span>Vendor</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                            <asp:DropDownList runat="server" ID="lstVendors" AutoPostBack="false" CssClass="DropdownListSearch" OnSelectedIndexChanged="lstVendors_SelectedIndexChanged">
                            </asp:DropDownList>
                        </li>

                        <li class="trvl_date" id="idWithoutInv_bank_1" runat="server"></li>
                        <li class="trvl_date" id="idWithoutInv_bank_2" runat="server"></li>
                        <li class="trvl_date" id="idWithoutInv_bank_3" runat="server"></li> 

                        <li class="trvl_date" id="idWithoutInv_InvoiceType_1" runat="server">
                            <span>Invoice Type</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                            <asp:DropDownList runat="server" ID="lstDisplayPOTypes" AutoPostBack="true" CssClass="DropdownListSearch" OnSelectedIndexChanged="lstDisplayPOTypes_SelectedIndexChanged">
                            </asp:DropDownList>
                        </li>
                        <li class="trvl_date" id="idWithoutInv_InvoiceType_2" runat="server"></li>
                        <li class="trvl_date" id="idWithoutInv_InvoiceType_3" runat="server"></li>


                        <li class="trvl_date" id="idWithoutInv_InvoiceRemarks_1" runat="server">
                            <br />
                            <span>Invoice Description </span> &nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtInvocieRemarks" runat="server" TextMode="MultiLine" CssClass="InvoiceRemarksText"></asp:TextBox>
                        </li>
                        <li class="trvl_date"  id="idWithoutInv_InvoiceRemarks_2" runat="server"></li>
                        <li class="trvl_date"  id="idWithoutInv_InvoiceRemarks_3" runat="server"></li>

                        <li class="trvl_date" id="idMilestone" runat="server">
                            <span>Milestone Particulars </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtMilestoneName_Invoice" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date" id="idMilestoneAmt" runat="server">
                            <span>Milestone Amount (A)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtMilestoneAmt_Invoice" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date" id="idDirectTaxAmt" runat="server">
                             <span>Milestone Direct Tax Amount (if Any) (B)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtMilestoneDirectTaxAmt" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>

                         <li class="trvl_date" id="idMilestonePaidAmt" runat="server">
                            <span>Milestone Paid Amount (C)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtMilestonePaidAmt" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                         <li class="trvl_date" id="idBalanceAmt" runat="server">
                             <span>Balance Amount (A-B-C)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtMilestoneBalanceAmt_Invoice" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                         <li class="trvl_date" id="idMilestoneBlnk_1" runat="server">                            
                        </li>


                         <li class="trvl_date" id="livoucher_1" runat="server" visible="false">
                            <span>Voucher No</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtVoucherNo" runat="server" MaxLength="50" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="trvl_date" id="livoucher_2" runat="server" visible="false">
                             
                            <asp:TextBox AutoComplete="off" ID="txtSupplierInvoiceDt" runat="server" Visible="false"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd-MM-yyyy" TargetControlID="txtSupplierInvoiceDt"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>

                        </li>
                        <li class="trvl_date" id="livoucher_3" runat="server" visible="false">
                             
                        </li>


                        <li class="trvl_date">
                            <span>Invoice No</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtInvoiceNo" runat="server" MaxLength="50"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>Supplier Invoice Date</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtInvoiceDate" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd-MM-yyyy" TargetControlID="txtInvoiceDate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>

                        </li>
                        <li class="trvl_date">
                            <span>Amount (Without GST)</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtAmtWithOutTax" runat="server" MaxLength="10" OnTextChanged="txtAmtWithOutTax_TextChanged" AutoPostBack="true"></asp:TextBox>

                        </li>


                        <li class="trvl_date">
                            <span>CGST(%)</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtCGST_Per" runat="server" AutoPostBack="true" OnTextChanged="txtCGST_Per_TextChanged"> </asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>SGST(%)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtSGST_Per" runat="server" AutoPostBack="true" OnTextChanged="txtSGST_Per_TextChanged"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>IGST(%)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtIGST_Per" runat="server" AutoPostBack="true" OnTextChanged="txtIGST_Per_TextChanged"></asp:TextBox>
                        </li>

                        <li class="trvl_date">
                            <span>CGST Amount</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtCGST_Amt" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>SGST Amount</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtSGST_Amt" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>IGST Amount</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtIGST_Amt" runat="server" Enabled="false"></asp:TextBox>
                        </li>


                        <li class="trvl_date">
                            <span>Amount (With GST)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtAmtWithTax_Invoice" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>

                        <li class="trvl_local" id="iduploaded_Invoice_1" runat="server" visible="false">
                            <span>Upload Invoice File</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <asp:FileUpload ID="InvoiceUploadfile" runat="server"  accept="pdf/*" AllowMultiple="false"></asp:FileUpload> 
                            
                        </li>
                        <li class="trvl_date" id="iduploaded_Invoice_2" runat="server" visible="false">
                              &nbsp;&nbsp;&nbsp; <asp:LinkButton ID="trvldeatils_btnSave" runat="server" Text="Upload Invoice Copy" ToolTip="Upload Invoice Copy" CssClass="Savebtnsve"  OnClick="trvldeatils_btnSave_Click"></asp:LinkButton>
                        </li>
                        <li class="trvl_date" id="iduploaded_Invoice_3" runat="server" visible="false"></li>


                        <li class="trvl_date">  
                             <span id="idspnUploadedInvoice" runat="server" visible="false">Uploaded Invoice File</span>
                            <br />
                             <asp:LinkButton ID="lnkfile_Invoice" runat="server" OnClick="lnkfile_Invoice_Click" Visible="false" CssClass="BtnShow"></asp:LinkButton>
                        </li>
                        <li class="trvl_date" > </li>
                        <li class="trvl_date" >  </li>


                        <li class="trvl_local">
                            <span>Upload Supporting Files</span>
                            <asp:FileUpload ID="ploadexpfile" runat="server" AllowMultiple="true"></asp:FileUpload>
                             <asp:LinkButton ID="btnCancel" runat="server" Visible="true" Text="Upload Supportting Files" ToolTip="Upload Supportting Files" CssClass="Savebtnsve" OnClientClick="return CancelMultiClick();" OnClick="btnCancel_Click"  >Upload Supporting Files</asp:LinkButton>
                           
                            <asp:GridView ID="gvuploadedFiles" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                DataKeyNames="Srno" OnRowDataBound="gvuploadedFiles_RowDataBound">
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
                                    <asp:TemplateField HeaderText="File Name">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkviewfile" CssClass="BtnShow" runat="server" OnClientClick=<%#"DownloadFile('" + Eval("filename") + "')" %> Text='<%# Eval("filename") %>'>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDeleteexpFile" runat="server" Text="Delete" OnClick="lnkDeleteexpFile_Click">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>

                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>

                        <li class="trvl_Approver">
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
    <div class="trvl_Savebtndiv">
        <asp:LinkButton ID="trvl_btnSave" runat="server" Text="Create Invoice" ToolTip="Create Invoice" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();" OnClick="trvl_btnSave_Click">Create Invoice</asp:LinkButton>
        
        <asp:LinkButton ID="btnback_mng" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/VSCB_MyInvoiceACC.aspx">Back</asp:LinkButton>
    </div>


    <div>
        <span class="LableName" runat="server" visible="false" id="spInvocies">Invoice History  </span>
        <br />
        <br />
        <asp:GridView ID="gvMngPaymentList_Batch" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
            DataKeyNames="InvoiceID,MstoneID,PaymentStatusID" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false" PageSize="20" AllowPaging="True">
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
                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%--<asp:ImageButton ID="lnkTravelDetailsEdit" runat="server" Width="15px" ToolTip="Create Invoice" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkTravelDetailsEdit_Click1" />--%>
                        <asp:LinkButton ID="lnkTravelDetailsEdit" CssClass="BtnShow" runat="server" Text='Edit' OnClick="lnkTravelDetailsEdit_Click1"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="3%" />
                </asp:TemplateField>

                <asp:BoundField HeaderText="Milestone No"
                    DataField="Srno"
                    ItemStyle-HorizontalAlign="Left"
                    HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />

                <asp:BoundField HeaderText="Milestone Particular"
                    DataField="MilestoneName"
                    ItemStyle-HorizontalAlign="Left"
                    HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-Width="20%" ItemStyle-BorderColor="Navy" />

                <asp:BoundField HeaderText="Invoice No"
                    DataField="InvoiceNo"
                    ItemStyle-HorizontalAlign="Left"
                    HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                <asp:BoundField HeaderText="Invoice Date"
                    DataField="InvoiceDate"
                    ItemStyle-HorizontalAlign="Left"
                    HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
                <asp:BoundField HeaderText="Amount (Without GST)"
                    DataField="AmtWithoutTax"
                    ItemStyle-HorizontalAlign="Right"
                    HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                <asp:BoundField HeaderText="CGST Amount"
                    DataField="CGST_Amt"
                    ItemStyle-HorizontalAlign="Right"
                    HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                <asp:BoundField HeaderText="SGST Amount"
                    DataField="SGST_Amt"
                    ItemStyle-HorizontalAlign="Right"
                    HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                <asp:BoundField HeaderText="IGST Amount"
                    DataField="IGST_Amt"
                    ItemStyle-HorizontalAlign="Right"
                    HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                <asp:BoundField HeaderText="Amount (With GST)"
                    DataField="AmtWithTax"
                    ItemStyle-HorizontalAlign="Right"
                    HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                <asp:BoundField HeaderText="Payment Status"
                    DataField="PyamentStatus"
                    ItemStyle-HorizontalAlign="Right"
                    HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

            </Columns>
        </asp:GridView>
    </div>

    <div style="display:none">
               <li class="trvl_date" id="idWithoutInv_Expenses_1" runat="server">
                             <span>Select Expenses</span>
                            <br />
                            <asp:DropDownList runat="server" ID="lstExpenses" CssClass="DropdownListSearch" OnSelectedIndexChanged="lstExpenses_SelectedIndexChanged">
                            </asp:DropDownList>
                        </li>
                        <li class="trvl_date"  id="idWithoutInv_Expenses_2" runat="server">
                             <span>Select Expenses Details</span>
                            <br />
                            <asp:DropDownList runat="server" ID="lstExpense_details" CssClass="DropdownListSearch" >
                            </asp:DropDownList>
                        </li>
                        <li class="trvl_date"  id="idWithoutInv_Expenses_3" runat="server">

                        </li>
        </div>

    <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
    <asp:TextBox AutoComplete="off" ID="txtEmpName" runat="server" Visible="false"></asp:TextBox>
    <asp:Label ID="testsanjay" runat="server" Visible="false"></asp:Label>
    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />

    <asp:HiddenField ID="hflEmpName" runat="server" />
    <asp:HiddenField ID="hflEmpDesignation" runat="server" />
    <asp:HiddenField ID="hflEmpDepartment" runat="server" />
    <asp:HiddenField ID="hflEmailAddress" runat="server" />
    <asp:HiddenField ID="hflGrade" runat="server" />

    <asp:HiddenField ID="FilePath" runat="server" />
    <asp:HiddenField ID="hdnSrno" runat="server" />
    <asp:HiddenField ID="hdnpamentStatusid" runat="server" />
    <asp:HiddenField ID="hdnIGSTAmt" runat="server" />
    <asp:HiddenField ID="hdnCompCode" runat="server" />
    <asp:HiddenField ID="hdnProject_Dept_Id" runat="server" />
    <asp:HiddenField ID="hdnVendorId" runat="server" />
    <asp:HiddenField ID="hdnPOWOID" runat="server" />
    <asp:HiddenField ID="hdnInvoiceId" runat="server" />
    <asp:HiddenField ID="hdnInvoiceApprStatusId" runat="server" />

    <asp:HiddenField ID="hdnstype_Main" runat="server" />
    <asp:HiddenField ID="hdnMilestoneID" runat="server" />

    <asp:HiddenField ID="hdnProject_Dept_Name" runat="server" />

    <asp:HiddenField ID="hdnPOTypeId" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="hdnfileid" runat="server" />

    <asp:HiddenField ID="hdnCGSTPer_O" runat="server" />
    <asp:HiddenField ID="hdnSGSTPer_O" runat="server" />
    <asp:HiddenField ID="hdnIGSTPer_O" runat="server" />
    <asp:HiddenField ID="hdnTotalInvoiceAmt" runat="server" />

    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />


      <script src="../js/freeze/jquery-ui.min.js"></script>
    <script src="../js/freeze/gridviewScroll.min.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {
        $('#' + "<%=InvoiceUploadfile.ClientID%>").attr('accept', 'application/pdf');
    })

        $(document).ready(function () {
            $(".DropdownListSearch").select2();

              $('#MainContent_dgTravelRequest').gridviewScroll({
                width: 1070,
                height: 600,
                freezesize:6, // Freeze Number of Columns.
                headerrowcount: 1, //Freeze Number of Rows with Header.
            });

    
        });

     
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
        function onCharOnlyNumber_dot(e) {
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
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=trvl_btnSave.ClientID%>');

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

        function DownloadFile(file) {
            // alert(file);
            var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;
           // alert( localFilePath );
           // window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
            //window.open("https://192.168.21.193/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
           window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
        }
    </script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="VSCB_MyPaymentRequestView.aspx.cs" Inherits="procs_VSCB_MyPaymentRequestView" %>

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
            font-weight: bold;
            background-color: cadetblue;
            color: aliceblue;
            text-align:right;
        }
         .txtAligntextBox 
        {
             text-align:right;
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
                        <asp:Label ID="lblheading" runat="server" Text="Payment Request View"></asp:Label>
                    </span>
                </div>
                <div>
                </div>
                <div>
                    <span>
                          <a href="vscb_index.aspx" class="aaaa"><%=System.Configuration.ConfigurationManager.AppSettings["VSCBPageTitle"]%></a>
                    </span>
                    <span>
                        <a href="VSCB_InboxMyPaymentRequest.aspx" title="Back" runat="server" visible="false" id="btnRecBack" style="margin-right: 10px;" class="aaaa">Back</a>
                    </span>
                </div>

                <div class="edit-contact">
                    <ul id="editform" runat="server">
                        <li class="trvl_date">
                            <span>PO/ WO Number  </span>
                            <br />
                            <asp:DropDownList runat="server" Enabled="false" ID="lstPOWONumber" CssClass="DropdownListSearch">
                            </asp:DropDownList>
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

                            <span style="display:none">PO/ WO Title </span> 
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
                               <asp:TextBox AutoComplete="off" ID="txtTallyCode_display" runat="server" Enabled="false"></asp:TextBox>
                            <asp:TextBox AutoComplete="off" ID="txtProjectName" runat="server" Enabled="false" Visible="false"></asp:TextBox>
                            <span style="display:none">Department</span> 
                            <asp:TextBox AutoComplete="off" ID="txtDepartment" runat="server" Enabled="false" Visible="false"></asp:TextBox>
                        </li>

                        <li class="trvl_date">
                        <span>PO/ WO Status </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtPoWoStatus" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                               <span>Currency </span><br />
                            <asp:TextBox AutoComplete="off" ID="txtCurrency" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox> 

                        </li>
                        <li>
                            <span>PO/ WO Amount (Without GST) </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtPoWOAmtWIthoutTax" CssClass="BalAmt" runat="server" Enabled="false"></asp:TextBox>
                        </li>

                        <li class="trvl_date">
                            <span>PO/ WO Amount (With GST) (A) </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtPoWoAmtWithTaxes" CssClass="BalAmt" runat="server" Enabled="false"></asp:TextBox>

                        </li>
                        <li class="trvl_date">
                            <span>Direct Tax Amount (B)</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtPODirectTaxAmt" runat="server" CssClass="BalAmt" Enabled="false"></asp:TextBox>

                        </li>
                        <li class="trvl_date">
                            
                            <span>Paid Amount (C)</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtPoWOPaidAmt" runat="server" CssClass="BalAmt" Enabled="false" Visible="false"></asp:TextBox>
                            <asp:TextBox AutoComplete="off" ID="txtPoPaidAmt_WithOutDT" runat="server" ReadOnly="true" Enabled="False" CssClass="BalAmt"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                    	    <span id="spnPOWOSettelmentAmt" runat="server"> Settlement Amount (D)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPOWOSettelmentAmt" runat="server" ReadOnly="true" Enabled="False" CssClass="BalAmt AmtTextAlign"></asp:TextBox>

                        </li>
                        <li class="trvl_date">
                            <span>Balance Amount (A-B-C-D)</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtPoWOPaidBalAmt" CssClass="BalAmt" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span id="spnPOWOSignCopy" runat="server" visible="false">PO/ WO Sign Copy</span><br />
                            <asp:LinkButton ID="lnkfile_PO" runat="server" OnClick="lnkfile_PO_Click" CssClass="BtnShow" Visible="false"></asp:LinkButton>
                        </li>
                        <div>
                            <br />
                            <span class="LableName" runat="server" id="Span3">PO/ WO Milestones
                            </span>
                            <asp:GridView ID="DgvMilestones" runat="server" CssClass="Milestones" DataKeyNames="MstoneID,POID" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="140%">
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
                                    <asp:BoundField HeaderText=" Milestone No."
                                        DataField="Srno"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="left"
                                        ItemStyle-Width="4%"
                                        ItemStyle-BorderColor="Navy" />
                                    <asp:BoundField HeaderText="Milestone Particulars"
                                        DataField="MilestoneName"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="left"
                                        ItemStyle-Width="17%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Milestone Due Date"
                                        DataField="Milestone_due_date"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Quantity"
                                        DataField="Quantity"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="left"
                                        ItemStyle-Width="4%"
                                        ItemStyle-BorderColor="Navy" />

                                     <asp:BoundField HeaderText="UOM"
                                        DataField="UOM"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="left"
                                        ItemStyle-Width="6%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Rate"
                                        DataField="Rate"
                                        ItemStyle-HorizontalAlign="right"
                                        HeaderStyle-HorizontalAlign="right"
                                        ItemStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Amount (Without GST)"
                                        DataField="Amount"
                                        ItemStyle-HorizontalAlign="right"
                                        HeaderStyle-HorizontalAlign="right"
                                        ItemStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="CGST Amount"
                                        DataField="CGST_Amt"
                                        ItemStyle-HorizontalAlign="right"
                                        HeaderStyle-HorizontalAlign="right"
                                        ItemStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="SGST Amount"
                                        DataField="SGST_Amt"
                                         ItemStyle-HorizontalAlign="right"
                                        HeaderStyle-HorizontalAlign="right"
                                        ItemStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="IGST Amount"
                                        DataField="IGST_Amt"
                                        ItemStyle-HorizontalAlign="right"
                                        HeaderStyle-HorizontalAlign="right"
                                        ItemStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Amount (With GST) (A)"
                                        DataField="AmtWithTax"
                                       ItemStyle-HorizontalAlign="right"
                                        HeaderStyle-HorizontalAlign="right"
                                        ItemStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Direct Tax Amount (B)"
                                        DataField="Collect_TDS_Amt"
                                        ItemStyle-HorizontalAlign="right"
                                        HeaderStyle-HorizontalAlign="right"
                                        ItemStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Paid Amount (C)"
                                        DataField="milestonepaidAmt"
                                       ItemStyle-HorizontalAlign="right"
                                        HeaderStyle-HorizontalAlign="right"
                                        ItemStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Settlement Amount (D)"
										DataField="Milestone_settlementAmt"
										ItemStyle-HorizontalAlign="right"
                                        HeaderStyle-HorizontalAlign="right"
										ItemStyle-Width="7%"
										ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Balance Amount (A-B-C)"
                                        DataField="Milesstone_Balance_Amt"
                                        ItemStyle-HorizontalAlign="right"
                                        HeaderStyle-HorizontalAlign="right"
                                        ItemStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Payment Status"
                                        DataField="PyamentStatus"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="4%"
                                        ItemStyle-BorderColor="Navy" />
                                </Columns>
                            </asp:GridView>
                        </div>
                       <%-- <div style="overflow: scroll; width: 100%; padding-top: 0px; margin-bottom: 5px;">--%>
                            <br />
                            <span class="LableName" runat="server" id="Span2">Invoice Details
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
                                        ItemStyle-Width="22%"
                                        ItemStyle-BorderColor="Navy" />

                                  
                                    <asp:BoundField HeaderText="Milestone Amount for Invoice (Without GST)"                                        
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

                                       <asp:BoundField HeaderText="Direct Tax Amount"
                                        DataField="Collect_TDS_Amt"
                                        ItemStyle-HorizontalAlign="right"
                                        HeaderStyle-HorizontalAlign="right"
                                        ItemStyle-Width="9%"
                                        ItemStyle-BorderColor="Navy" />


                                    <asp:BoundField HeaderText="Milestone Amount for Invoice (With GST)"
                                        DataField="MilestoneAmt_WithTax_ForInvoice"
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
                       <%-- </div>--%>
                    </ul>
                    <div runat="server" id="DivCreateInvoice" visible="true">
                        <div class="edit-contact">

                            <ul id="Ul2" runat="server">
                                <li class="trvl_date" style="padding-bottom: 20px">
                                    <span style="text-decoration: underline; color: #F28820; font-size: 16px">Payment Request View : </span>&nbsp;&nbsp;
                                    <br />
                                </li>
                                <li>
                                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 500; text-align: center;"></asp:Label>
                                </li>
                                <li></li>
                                <li class="trvl_date">
                                    <span>Invoice No</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtInvoiceNo" runat="server" Enabled="false"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span>Invoice Amount including GST (A)</span> 
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtInvoiceAmount" runat="server" CssClass="txtAligntextBox" Enabled="false"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span>Direct Tax Amount (if Any) (B)</span>
									<br />
									<asp:TextBox AutoComplete="off" ID="txtInvoiceTDSAmt" runat="server" CssClass="txtAligntextBox" Enabled="false"></asp:TextBox>
                                    
                                </li>

                                 <li class="trvl_date">
									 <span>Invoice Paid Amount (C)</span>
									<br />
									<asp:TextBox AutoComplete="off" ID="txtInvoicePaidAmount" runat="server" CssClass="txtAligntextBox" Enabled="false"></asp:TextBox>
								</li>
								<li class="trvl_date">
									 <span>Payment Requested for (Amount)</span>
									<br />
									<asp:TextBox AutoComplete="off" ID="txtPaymentRequestAmount" runat="server" CssClass="txtAligntextBox" Enabled="false"></asp:TextBox>
								</li>
								<li class="trvl_date">
								 	 <span>Invoice Balance Amount (A-B-C)</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtInvoiceBalAmt" runat="server" CssClass="txtAligntextBox" Enabled="false"></asp:TextBox>
								</li>
                                <br />
                                <span class="LableName" runat="server" visible="true" id="Span4">Payment Invoice Detail
							   </span> 
                                <asp:GridView ID="GVEditpaymentReqamtpaid" CssClass="Milestones" Visible="false" runat="server" DataKeyNames="MstoneID,InvoiceID" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="130%">
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

                                   <%-- <asp:BoundField HeaderText="Amount To be Requested"
                                        DataField="Amounttobepaidd"
                                       ItemStyle-HorizontalAlign="right"
                                        HeaderStyle-HorizontalAlign="right"
                                        ItemStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />--%>

                                     <asp:TemplateField HeaderText="Amount To be Requested" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                 <asp:TextBox ID="txtMilestoneInvoiceAmt"  Text='<%# Eval("Amounttobepaidd") %>' Width="70px" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*)\./g, '$1');" AutoComplete="off" runat="server"  CssClass="txtWidth" AutoPostBack="true"  MaxLength="18" OnTextChanged="txtMilestoneInvoiceAmt_TextChanged"></asp:TextBox>
                                                 <asp:Label runat="server" ID="lblamountcheck" Text='<%# Eval("Milesstone_PaybleInvoice_Amtt") %>' Visible="false"></asp:Label>
                                                 </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="12%" />
                                        </asp:TemplateField>


                                    <asp:BoundField HeaderText="Milestone Particulars"
                                        DataField="MilestoneName"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="left"
                                        ItemStyle-Width="22%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Milestone Amount for Invoice (Without GST)"                                        
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

                                       <asp:BoundField HeaderText="Direct Tax Amount"
                                        DataField="Collect_TDS_Amt"
                                        ItemStyle-HorizontalAlign="right"
                                        HeaderStyle-HorizontalAlign="right"
                                        ItemStyle-Width="9%"
                                        ItemStyle-BorderColor="Navy" />


                                    <asp:BoundField HeaderText="Milestone Amount for Invoice (With GST)"
                                        DataField="MilestoneAmt_WithTax_ForInvoice"
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

                                    <asp:BoundField HeaderText="Milesstone Payble Invoice Amount"
										DataField="Milesstone_PaybleInvoice_Amtt"
										ItemStyle-HorizontalAlign="Right" 
                                        HeaderStyle-HorizontalAlign="Right" ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />
                                     
                                    <asp:BoundField HeaderText="Payment Status"
                                        DataField="PyamentStatus"
                                        ItemStyle-HorizontalAlign="Left"
                                         HeaderStyle-HorizontalAlign="left"
                                        ItemStyle-Width="9%"
                                        ItemStyle-BorderColor="Navy" />
                                </Columns>
                            </asp:GridView>

                                <br />

                                <li class="trvl_date">
                                    <span>Payment Request No </span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <br />
                                    <%--<asp:TextBox AutoComplete="off" ID="txtPaymentRequestNo" runat="server" Enabled="false"></asp:TextBox>--%>
                                    <asp:Label AutoComplete="off" ID="txtPaymentRequestNo" runat="server" Enabled="false" CssClass="PayNo"></asp:Label>

                                </li>
                                <li class="trvl_date">
                                    <span>Request Date </span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtRequestDate" runat="server" Enabled="false"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span>Amount to be paid </span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtAmountPaidWithTax" CssClass="number" Enabled="false" runat="server"></asp:TextBox>
                                </li>

                                  


                                <li class="trvl_date" runat="server" id="Account1">

                                    <span>Amount paid by Accounts </span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtAccountPaidAmt" runat="server" CssClass="number"></asp:TextBox>
                                </li>
                                <li class="trvl_date" runat="server" id="Account2">
                                    <span>Balance Amount </span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtAccountAmtBal" runat="server" CssClass="txtAligntextBox" Enabled="false"></asp:TextBox>
                                </li>

                                <li class="trvl_date" runat="server" id="Account3"></li>
                                <li class="trvl_date"> 
                                    <span id="spnPaymentSuppotingFiles" runat="server" visible="false">Payment Supporting Files</span>&nbsp;&nbsp;<%--<span style="color:red">*</span>--%><br />
                                    <asp:FileUpload ID="uploadfile" runat="server" AllowMultiple="true" />
                                    <%--<asp:LinkButton ID="lnkuplodedfile" OnClick="lnkuplodedfile_Click" runat="server"></asp:LinkButton>--%>
                                </li>

                                <asp:GridView ID="GrdFileUpload" DataKeyNames="Payment_ID,FileName" runat="server" CssClass="Milestones" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="50%">
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
                                        <asp:TemplateField HeaderText="File View" HeaderStyle-Width="3%">
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

                                <li>
                                    <span >Invoice File</span> <br />
                                    <asp:LinkButton ID="lnkfile_Invoice" runat="server"  OnClientClick="DownloadFile_S()" CssClass="BtnShow"></asp:LinkButton>
                                </li>

                                <div style=" padding-top:20px; padding-bottom:20px">
                                    <span id="spnSupportinFiles" runat="server" visible="false">Invoice Supporting Files</span>
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

                                </div>

                                <div>
                                    <br />
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
                                    <span class="LableName" runat="server" id="Span5">Invoice Approvers:</span>
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

                </div>

            </div>
        </div>

    </div>

    <div class="Req_Savebtndiv" style="text-align: center">
        <br />
        <asp:LinkButton ID="localtrvl_btnSave" runat="server" Visible="false" Text="Save As Draft" ToolTip="Create Payment Request" CssClass="Savebtnsve" OnClick="localtrvl_btnSave_Click" OnClientClick="return SaveMultiClick();"> Create Payment Request </asp:LinkButton>
        <asp:LinkButton ID="trvldeatils_btnSave" runat="server" Visible="true" Text="Cancel Request" ToolTip="Cancel Request" CssClass="Savebtnsve" OnClick="trvldeatils_btnSave_Click" OnClientClick="return SaveMultiClick1();">Cancel Request</asp:LinkButton>

        <asp:LinkButton ID="trvldeatils_cancel_btn" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/VSCB_InboxMyPaymentRequest.aspx">    Back</asp:LinkButton>

        <asp:LinkButton ID="accmo_delete_btn" runat="server" Text="Clear" Visible="false" ToolTip="Clear" CssClass="Savebtnsve" OnClick="accmo_delete_btn_Click" OnClientClick="return ClearMultiClick();">  Clear  </asp:LinkButton>

        <%--Position Criticality--%>
        <asp:LinkButton ID="trvldeatils_delete_btn" runat="server" Text="Position Criticality Change" ToolTip="Cancel" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();" Visible="false">Position Criticality Change</asp:LinkButton>
    </div>

    <div>
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
                        <asp:ImageButton ID="lnkViewhist" runat="server" ToolTip="View" Width="15px" Height="15px" Visible='<%# Eval("Payment_ID").ToString() == hdnPayment_ID.Value ? false : true %>' ImageUrl="~/Images/edit.png" OnClick="lnkViewhist_Click" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:BoundField HeaderText="Sr.No"
                    DataField="Srno"
                    ItemStyle-HorizontalAlign="left"
                    ItemStyle-Width="3%"
                    ItemStyle-BorderColor="Navy" />

                <asp:BoundField HeaderText="Invoice No."
                    DataField="InvoiceNo"
                    ItemStyle-HorizontalAlign="left"
                    HeaderStyle-HorizontalAlign="left"
                    ItemStyle-Width="15%"
                    ItemStyle-BorderColor="Navy" />

                <asp:BoundField HeaderText="Payment Request No."
                    DataField="PaymentReqNo"
                    ItemStyle-HorizontalAlign="left"
                    HeaderStyle-HorizontalAlign="left"
                    ItemStyle-Width="15%"
                    ItemStyle-BorderColor="Navy" />

                <asp:BoundField HeaderText="Request Date"
                    DataField="PaymentReqDate"
                    ItemStyle-HorizontalAlign="left"
                    HeaderStyle-HorizontalAlign="left"
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
        <br />
        <br />
    </div>

    <asp:HiddenField ID="hflEmpDesignation" runat="server" />
    <asp:HiddenField ID="hflEmpDepartment" runat="server" />
    <asp:HiddenField ID="hflEmailAddress" runat="server" />
    <asp:HiddenField ID="hdnEmpCpde" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="FilePath" runat="server" />
    <asp:HiddenField ID="FilePathInvoice" runat="server" />
    <asp:HiddenField ID="FileName" runat="server" />
    <asp:HiddenField ID="hdnInvoiceID" runat="server" />
    <asp:HiddenField ID="hdnMstoneID" runat="server" />
    <asp:HiddenField ID="hdnapprcode" runat="server" />
    <asp:HiddenField ID="hdnPayment_ID" runat="server" />
    <asp:HiddenField ID="hdnPOID" runat="server" />
    <asp:HiddenField ID="hdnCostCentre" runat="server" />
    <asp:HiddenField ID="hdnTallyCode" runat="server" />
    <asp:HiddenField ID="hdnDept_Name" runat="server" />
    <asp:HiddenField ID="hflStatusID" runat="server" />
    <asp:HiddenField ID="hdnTobePaidAmt" runat="server" />
    <asp:HiddenField ID="hdnInvoicePayableAmt" runat="server" />
    <asp:HiddenField ID="hdnpaymentAmt" runat="server" />
    <asp:HiddenField ID="hdnDirectTax_Type" runat="server" />
    <asp:HiddenField ID="hdnDirectTax_Percentage" runat="server" />
    <asp:HiddenField ID="hdnDirectTax_Amount" runat="server" />
    <asp:HiddenField ID="hdnPayable_Amt_Invoice" runat="server" />
    <asp:HiddenField ID="hdnMilestoneName" runat="server" />
    <asp:HiddenField ID="hdnMilestAmount" runat="server" />
    <asp:HiddenField ID="hdnMilestCGST_Amt" runat="server" />
    <asp:HiddenField ID="hdnMilestSGST_Amt" runat="server" />
    <asp:HiddenField ID="hdnMilestIGST_Amt" runat="server" />
    <asp:HiddenField ID="hdnMilestAmtWithTax" runat="server" />
    <asp:HiddenField ID="hdnMilestPyamentStatus" runat="server" />
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

            $('#MainContent_GVEditpaymentReqamtpaid').gridviewScroll({
                width: 1070,
                height: 600,
                freezesize: 6, // Freeze Number of Columns.
                headerrowcount: 1, //Freeze Number of Rows with Header. 
            });

            $('#MainContent_GrdInvoiceDetails').gridviewScroll({
                width: 1070,
                height: 600,
                freezesize: 5, // Freeze Number of Columns.
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

        function SaveMultiClick1() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=trvldeatils_btnSave.ClientID%>'); 

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


        function ClearMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=accmo_delete_btn.ClientID%>');

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
                var ele = document.getElementById('<%=trvldeatils_delete_btn.ClientID%>');

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
            window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);

        }

         function DownloadFile1(FileName) {
            var localFilePath = document.getElementById("<%=FilePathInvoice.ClientID%>").value;

            //window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
            window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);

        }

          function DownloadFile_S() {
            
             var localFilePath = document.getElementById("<%=FilePathInvoice.ClientID%>").value;
             var localFileName = document.getElementById("<%=lnkfile_Invoice.ClientID%>").innerText;

             //alert(localFilePath);
           //  alert(localFileName);

            window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
            //window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + localFileName);
        }

    </script>
</asp:Content>




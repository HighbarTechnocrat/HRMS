<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" EnableViewState="true"
    MaintainScrollPositionOnPostback="true" CodeFile="VSCB_ApprovedInvoice_View.aspx.cs" Inherits="VSCB_ApprovedInvoice_View" EnableSessionState="True" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <%--<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <%-- <link href="<%=ReturnUrl("css") %>calendar/calendar.css" rel="stylesheet" type="text/css" />
    <link href="sampleform_css.css" rel="stylesheet" type="text/css" />--%>
    <%--<ajaxToolkit:ToolkitScriptManager ID="srcp" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
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
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>

    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Approved Invoice View"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>
                <div>
                    <span>
                          <a href="vscb_index.aspx" class="aaaa"><%=System.Configuration.ConfigurationManager.AppSettings["VSCBPageTitle"]%></a>
                    </span>
                </div>

                
                   <ul>
                       <li class="trvl_date">
                                <asp:CheckBox ID="chkIsInvoiceWithoutPO" runat="server" Text="Is Invoice Without PO"  Visible="false" />
                            <asp:Label ID="lblIsInvoiceWithPO" runat="server" Text="Invoice Without PO/WO"  Visible="false" ></asp:Label>
                       </li>

                       <li class="trvl_date">
                        </li>
                        <li class="trvl_date">
                       </li>
                   </ul>

                <div class="edit-contact">

                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" Visible="false"></asp:LinkButton>
                        </div>
                    </div>

                    <ul id="editform" runat="server" visible="true">
                        <li class="trvl_type">
                            <span>PO/ WO Number</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                            <asp:DropDownList runat="server" ID="lstTripType" AutoPostBack="true" OnSelectedIndexChanged="lstTripType_SelectedIndexChanged"  CssClass="DropdownListSearch">
                            </asp:DropDownList>
                            <asp:TextBox AutoComplete="off" ID="txtTriptype" runat="server" CssClass="Dropdown" Visible="false"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>PO/ WO Type</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                            <asp:DropDownList runat="server" ID="lstPOType" AutoPostBack="false"  CssClass="DropdownListSearch">
                            </asp:DropDownList>
                            <asp:TextBox AutoComplete="off" ID="txtPOtype" runat="server" CssClass="Dropdown" Visible="false"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                             <span>PO/ WO Date </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtFromdate" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>


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
                             <span>Department</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtProject" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>PO/ WO Status</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPOStatus" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>


                        <li class="trvl_date">
                             <span>PO/ WO Amount (With GST)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPOWOAmt" runat="server" ReadOnly="true" Enabled="False" CssClass="textboxBackColor"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>Paid Amount</span><br /> 
                            <asp:TextBox AutoComplete="off" ID="txtPaidAmr" runat="server" ReadOnly="true" Enabled="False" CssClass="textboxBackColor"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                              <span>Direct Tax Amount</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtDiretTaxAmtPOWO" runat="server" ReadOnly="true" Enabled="False" CssClass="textboxBackColor"></asp:TextBox>
                        </li>

                         <li class="trvl_date">                            
                             <span>Balance Amount</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtBalanceAmt" runat="server" ReadOnly="true" Enabled="False" CssClass="textboxBackColor"></asp:TextBox>
                        </li>
                        <li class="trvl_date">                        
                        </li>
                        <li class="trvl_date">
                         
                        </li>
                    </ul>
                     <ul id="editform1" runat="server">
                        <li class="trvl_grid" id="litrvlgrid" runat="server">
                            <div>
                                  <span class="LableName" runat="server" visible="false" id="spMilestones">PO/ WO Milestones</span>
                                <asp:GridView ID="dgTravelRequest" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                    DataKeyNames="srno,PaymentStatusID,MstoneID" OnRowCreated="dgTravelRequest_RowCreated">
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
                                        <asp:BoundField HeaderText="Srno"
                                            DataField="srno"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Particulars"
                                            DataField="MilestoneName"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="20%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Quantity"
                                            DataField="Quantity"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Rate"
                                            DataField="Rate"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Amount"
                                            DataField="Amount"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="CGST Amount"
                                            DataField="CGST_Amt"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="SGST Amount"
                                            DataField="SGST_Amt"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="IGST Amount"
                                            DataField="IGST_Amt"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Total Amount"
                                            DataField="AmtWithTax"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Payment Status"
                                            DataField="PyamentStatus"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />

                                        <asp:TemplateField HeaderText="Create" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnkTravelDetailsEdit" runat="server" Width="15px" ToolTip="Create Invoice" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkTravelDetailsEdit_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="8%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                            </div>
                        </li>

                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>
                   

                        <li class="trvL_detail" id="li1" runat="server">
                            <span id="Span1" runat="server" class="LableName">Invoice Details</span>
                        </li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>


                         <li class="trvl_date" id="idWithoutInv_ProjectDept" runat="server">
                            <span>Department</span>
                            <br />
                            <asp:DropDownList runat="server" ID="lstProjectDept"  CssClass="DropdownListSearch">
                             </asp:DropDownList> 
                        </li>
                        <li class="trvl_date" id="idWithoutInv_Vendor" runat="server">
                             <span>Cost Center</span><br />
                            <asp:DropDownList runat="server" ID="lstCostCenter" AutoPostBack="true"  CssClass="DropdownListSearch" Enabled="false">
                             </asp:DropDownList>
                        </li>
                        <li class="trvl_date" id="idWithoutInv_Blank" runat="server">  
                             <span>Vendor</span><br />
                            <asp:DropDownList runat="server" ID="lstVendors" AutoPostBack="true"  CssClass="DropdownListSearch">
                             </asp:DropDownList>
                        </li>
                        
                         <li class="trvl_date"></li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>


                        <li class="trvl_date" id="idMilestone" runat="server">
                            <span>Milestone Particulars </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtMilestoneName_Invoice" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date"  id="idMilestoneAmt" runat="server">
                            <span>Milestone Amount</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtMilestoneAmt_Invoice" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date"  id="idBalanceAmt" runat="server">
                            <span>Balance Amount</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtMilestoneBalanceAmt_Invoice" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>


                        <li class="trvl_date">
                            <span>Invoice No</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtInvoiceNo" runat="server" MaxLength="50" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>Invoice Date</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtInvoiceDate" runat="server" Enabled="False"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtInvoiceDate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>

                        </li>
                        <li class="trvl_date">
                            <span>Amount (Without Tax)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtAmtWithOutTax" runat="server" MaxLength="10" OnTextChanged="txtAmtWithOutTax_TextChanged" AutoPostBack="true" Enabled="False"></asp:TextBox>
                        </li>


                        <li class="trvl_date">
                            <span>CGST(%)</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtCGST_Per" runat="server" ReadOnly="true" Enabled="False"> </asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>SGST(%)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtSGST_Per" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>IGST(%)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtIGST_Per" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>

                         <li class="trvl_date" id="idDirectTaxApplicable" runat="server" visible="false">
                            <span>Direct Tax Applicable</span><br />
                             <asp:DropDownList runat="server" ID="lstDirectTax" AutoPostBack="true" CssClass="DropdownListSearch" OnSelectedIndexChanged="lstDirectTax_SelectedIndexChanged">
                              </asp:DropDownList>
                        </li>
                        <li class="trvl_date" id="idPercentage" runat="server" visible="false">
                            <span>Percentage(%)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtDirectTaxPercentage" runat="server" MaxLength="5" AutoPostBack="true" OnTextChanged="txtDirectTaxPercentage_TextChanged" ></asp:TextBox>
                        </li>
                        <li class="trvl_date" id="idDirectTaxAmount" runat="server" visible="false">
                            <span>Direct tax Amount</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtDirecttaxAmt" runat="server" Enabled="false"></asp:TextBox>
                        </li>


                        <li class="trvl_date">
                            <span>Amount (With Tax)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtAmtWithTax_Invoice" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>

                        <li class="trvl_date">
                            <span style="display:none">Remarks </span> 
                            <asp:TextBox AutoComplete="off" ID="txtRemarks" runat="server" MaxLength="100" TextMode="MultiLine" Visible="false"></asp:TextBox>
                        </li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>

                       <li class="trvl_local">
                         <span>Invoice File</span>                            
                            <br />
                            <asp:LinkButton ID="lnkfile_Invoice"   runat="server"  OnClick="lnkfile_Invoice_Click" Visible="false"></asp:LinkButton>                           
                       </li>
                       <li class="trvl_date"></li>
                       <li class="trvl_date"></li>

                           <li class="trvl_date"></li>
                       <li class="trvl_date"></li>
                           <li class="trvl_date"></li> 

                         <li class="trvl_local">                     
                        <span>Supporting Files</span>    
                          <asp:GridView ID="gvuploadedFiles" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                   DataKeyNames="fileid"  OnRowDataBound="gvuploadedFiles_RowDataBound">
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
                                                <asp:LinkButton ID="lnkviewfile" runat="server" OnClientClick=<%# "DownloadFile('" + Eval("filename") + "')" %> Text='<%# Eval("filename") %>'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDeleteexpFile" runat="server" Text="View" OnClick="lnkDeleteexpFile_Click">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                        </asp:GridView>
                       </li>
                         <li class="trvl_date"> </li>
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

                <div class="edit-contact"> 
							<span class="LableName" runat="server" visible="false" id="lblApprovedPaymentHistory">Earlier Payments History
							</span>
							<asp:GridView ID="GrdPaymentHistory" DataKeyNames="Payment_ID" runat="server" CssClass="Milestones" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%">
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

									<asp:BoundField HeaderText="Sr.No"
										DataField="Payment_ID"
										ItemStyle-HorizontalAlign="left"
										ItemStyle-Width="3%"
										ItemStyle-BorderColor="Navy" />

									<asp:BoundField HeaderText="Invoice No."
										DataField="InvoiceNo"
										ItemStyle-HorizontalAlign="left"
										ItemStyle-Width="15%"
										ItemStyle-BorderColor="Navy" />

									<asp:BoundField HeaderText="Payment Request No."
										DataField="PaymentReqNo"
										ItemStyle-HorizontalAlign="left"
										ItemStyle-Width="15%"
										ItemStyle-BorderColor="Navy" />

									<asp:BoundField HeaderText="Request Date"
										DataField="PaymentReqDate"
										ItemStyle-HorizontalAlign="left"
										ItemStyle-Width="10%"
										ItemStyle-BorderColor="Navy" />

									<asp:BoundField HeaderText="Amount to be Paid"
										DataField="TobePaidAmtWithtax"
										ItemStyle-HorizontalAlign="center"
										ItemStyle-Width="10%"
										ItemStyle-BorderColor="Navy" />

									<asp:BoundField HeaderText="Amount Paid by Accounts"
										DataField="Amt_paid_Account"
										ItemStyle-HorizontalAlign="center"
										ItemStyle-Width="10%"
										ItemStyle-BorderColor="Navy" />

									<asp:BoundField HeaderText="Balance "
										DataField="BalanceAmt"
										ItemStyle-HorizontalAlign="center"
										ItemStyle-Width="10%"
										ItemStyle-BorderColor="Navy" />

									<asp:BoundField HeaderText="Payment Status"
										DataField="PyamentStatus"
										ItemStyle-HorizontalAlign="center"
										ItemStyle-Width="10%"
										ItemStyle-BorderColor="Navy" /> 
								</Columns>
							</asp:GridView> 
						 
						<br />
						<br />
					</div>

            </div>
        </div>
    </div>
    <div class="trvl_Savebtndiv">
        <span>
            <asp:LinkButton ID="trvl_btnSave" Visible="false" runat="server" Text="Approve" ToolTip="Approve" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();" OnClick="trvl_btnSave_Click">Approve</asp:LinkButton>
        </span>
        <span>
            <asp:LinkButton ID="btnCancel" Visible="false" runat="server" Text="Reject" ToolTip="Reject" CssClass="Savebtnsve" OnClientClick="return CancelMultiClick();" OnClick="btnCancel_Click">Reject</asp:LinkButton>
        </span>
        <span>
            <asp:LinkButton ID="btnCorrection" Visible="false" runat="server" Text="Correction" ToolTip="" CssClass="Savebtnsve" OnClientClick="return SendforCorrectionMultiClick();" OnClick="btnCorrection_Click">Send for Correction</asp:LinkButton>
        </span>

        <asp:LinkButton ID="btnback_mng" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="btnback_mng_Click">Back</asp:LinkButton> 
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
    <asp:HiddenField ID="hdnVendorId" runat="server" />
    <asp:HiddenField ID="hdnPOWOID" runat="server" />
    <asp:HiddenField ID="hdnInvoiceId" runat="server" />

    <asp:HiddenField ID="hdnstype_Main" runat="server" />
    <asp:HiddenField ID="hdnMilestoneID" runat="server" />

    <asp:HiddenField ID="hdnPOTypeId" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    
    <asp:HiddenField ID="hdn_next_Appr_ID" runat="server" />
    <asp:HiddenField ID="hdn_next_Appr_Empcode" runat="server" />
    <asp:HiddenField ID="hdn_next_Appr_EmpEmail_ID" runat="server" />
    <asp:HiddenField ID="hdn_next_Appr_Emp_Name" runat="server" />

    <asp:HiddenField ID="hdn_curnt_Appr_ID" runat="server" />
    <asp:HiddenField ID="hdn_curnt_Appr_Empcode" runat="server" />
    <asp:HiddenField ID="hdn_curnt_Appr_EmpEmail_ID" runat="server" />
    <asp:HiddenField ID="hdn_curnt_Appr_Emp_Name" runat="server" /> 

    <asp:HiddenField ID="hdnProject_Dept_Name" runat="server" />
    <asp:HiddenField ID="hdnProject_Dept_Id" runat="server" />

    <asp:HiddenField ID="hdnInvoiceCreator_Name" runat="server" />
    <asp:HiddenField ID="hdnInvoiceCreator_Email" runat="server" />
    <asp:HiddenField ID="hdnIsFinalApprover" runat="server" />

    <asp:HiddenField ID="hdnbatchId" runat="server" />

 <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <script type="text/javascript">

        $(document).ready(function () {
            $(".DropdownListSearch").select2();
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

         function SendforCorrectionMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=btnCorrection.ClientID%>');

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

            //alert(localFilePath);
            window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
        }
    </script>
</asp:Content>

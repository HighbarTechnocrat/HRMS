<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="VSCB_ApproveInvoiceTDSAmtChange.aspx.cs" 
    Inherits="procs_VSCB_ApproveInvoiceTDSAmtChange" EnableSessionState="True" MaintainScrollPositionOnPostback="true" %>


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
         .InvoiceRemarksText {
              width: 755px;
              height: 100px;
              overflow: auto;
        }
         .InvoiceMilestonegrid{
                 margin: -24px 0 0 0 !important;
         }
        .txtWidth
        {
            padding: 2px !important;
            width: 100px !important;
            height: 21px !important;
            text-align:right !important;
        }
        a#MainContent_trvldeatils_btnSave {
    margin: 3px 0 6px 0px !important;
}

           .AmtTextAlign
        {
            text-align:right
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
                        <asp:Label ID="lblheading" runat="server" Text="Approve Invoice"></asp:Label>
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
                        <asp:CheckBox ID="chkIsInvoiceWithoutPO" runat="server" Text="Is Invoice Without PO/WO" Visible="false" />
                        <asp:Label ID="lblIsInvoiceWithPO" runat="server" Text="Invoice Without PO/WO" Visible="false"></asp:Label>
                    </li>

                    <li class="trvl_date"></li>
                    <li class="trvl_date"></li>
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
                            <span>PO/ WO Number</span><br />
                            <asp:DropDownList runat="server" ID="lstTripType" AutoPostBack="true"  CssClass="DropdownListSearch">
                            </asp:DropDownList>
                            <asp:TextBox AutoComplete="off" ID="txtTriptype" runat="server" CssClass="Dropdown" Visible="false"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>PO/ WO Type</span><br />
                            <asp:DropDownList runat="server" ID="lstPOType" AutoPostBack="false" CssClass="DropdownListSearch">
                            </asp:DropDownList>
                            <asp:TextBox AutoComplete="off" ID="txtPOtype" runat="server" CssClass="Dropdown" Visible="false"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>PO/ WO Date </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtFromdate" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>


                        <li class="trvl_date">
                             <span>Vendor Name</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtVendor" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>

                            <span style="display:none">PO/ WO Title</span> 
                            <asp:TextBox AutoComplete="off" ID="txtPOTitle" runat="server" ReadOnly="true" Enabled="False" Visible="false"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                              <span>GSTIN No.</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtGSTIN_No" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                              <span>Cost Center</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtCostCenter" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>


                         <li class="trvl_date">
                           <span>PO/ WO Status</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPOStatus" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date">                           
                              <span>Currency </span><br />
                            <asp:TextBox AutoComplete="off" ID="txtCurrency" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox> 
                             <span style="display:none">Project/Department</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtProject" runat="server" ReadOnly="true" Enabled="False" Visible="false"></asp:TextBox>
                        </li>                       
                        <li class="trvl_date">
                             <span>PO/ WO Amount (Without GST)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtBasePOWOWAmt" runat="server" ReadOnly="true" Enabled="False" CssClass="textboxBackColor AmtTextAlign"></asp:TextBox>

                        </li>


                        <li class="trvl_date">
                             <span>PO/ WO Amount (With GST) (A) </span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPOWOAmt" runat="server" ReadOnly="true" Enabled="False" CssClass="textboxBackColor AmtTextAlign"></asp:TextBox>
                           
                        </li>
                        <li class="trvl_date">
                              <span>Direct Tax Amount (B)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtDiretTaxAmtPOWO" runat="server" ReadOnly="true" Enabled="False" CssClass="textboxBackColor AmtTextAlign"></asp:TextBox>
                            
                        </li>
                        <li class="trvl_date">                              
                            <span>Paid Amount (With GST) (C)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPaidAmr" runat="server" ReadOnly="true" Enabled="False" CssClass="textboxBackColor AmtTextAlign" Visible="false"></asp:TextBox>
                             <asp:TextBox AutoComplete="off" ID="txtPoPaidAmt_WithOutDT" runat="server" ReadOnly="true" Enabled="False" CssClass="textboxBackColor AmtTextAlign"></asp:TextBox>
                        </li>

                        <li class="trvl_date">
                              <span id="spnPOWOSettelmentAmt" runat="server"> Settlement Amount (D)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPOWOSettelmentAmt" runat="server" ReadOnly="true" Enabled="False" CssClass="textboxBackColor AmtTextAlign"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                              <span>Balance Amount (A-B-C-D)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtBalanceAmt" runat="server" ReadOnly="true" Enabled="False" CssClass="textboxBackColor AmtTextAlign"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                             <span id="spnPOWOSignCopy" runat="server" visible="false">PO/ WO Sign Copy</span><br />
                               <asp:LinkButton ID="lnkfile_PO" runat="server"  OnClientClick="Download_POSignCopyFile()"  CssClass="BtnShow" Visible="false"></asp:LinkButton> 
                        </li>


                        <li class="trvl_date">
                            <div class="manage_grid" style="overflow: hidden; width: 130%; margin-bottom: 4px;">
                                <span id="spnSettlemnt_N" style="font-size: 13px !important">Settlement = Discount/Correction/Settlement/Deduction </span>
                            </div>
                        </li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>
                       
                    </ul>
                    <ul id="editform1" runat="server">
                        <li class="trvl_grid" id="litrvlgrid" runat="server">
                            <div>
                                <span class="LableName" runat="server" visible="false" id="spMilestones">PO/ WO Milestones</span>
                                <asp:GridView ID="dgTravelRequest" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="140%" EditRowStyle-Wrap="false"
                                    DataKeyNames="srno,PaymentStatusID,MstoneID">
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
                                            ItemStyle-Width="7%" ItemStyle-BorderColor="Navy" />

                                      <asp:BoundField HeaderText="UOM"
                                        DataField="UOM"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="6%"
                                        ItemStyle-BorderColor="Navy" />

									<asp:BoundField HeaderText="Quantity"
										DataField="Quantity"
										ItemStyle-HorizontalAlign="Right"
										 HeaderStyle-HorizontalAlign="Right"
										ItemStyle-Width="5%"
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

                                      <asp:BoundField HeaderText="CGST Per (%)"
                                        DataField="CGST_Per"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="3%" ItemStyle-BorderColor="Navy" />

                                     <asp:BoundField HeaderText="SGST Per (%)"
                                        DataField="SGST_Per"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="3%" ItemStyle-BorderColor="Navy" />

                                     <asp:BoundField HeaderText="IGST Per (%)"
                                        DataField="IGST_Per"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="3%" ItemStyle-BorderColor="Navy" />

									<asp:BoundField HeaderText="CGST Amount"
										DataField="CGST_Amt"
										ItemStyle-HorizontalAlign="Right"
                                         HeaderStyle-HorizontalAlign="Right"
										ItemStyle-Width="6%"
										ItemStyle-BorderColor="Navy" />

									<asp:BoundField HeaderText="SGST Amount"
										DataField="SGST_Amt"
										ItemStyle-HorizontalAlign="Right"
                                         HeaderStyle-HorizontalAlign="Right"
										ItemStyle-Width="6%"
										ItemStyle-BorderColor="Navy" />

									<asp:BoundField HeaderText="IGST Amount"
										DataField="IGST_Amt"
										ItemStyle-HorizontalAlign="Right"
                                         HeaderStyle-HorizontalAlign="Right"
										ItemStyle-Width="6%"
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

                                    <asp:BoundField HeaderText="Paid Amount (With GST) (C)"
										DataField="milestonepaidAmt"
										ItemStyle-HorizontalAlign="Right"
                                         HeaderStyle-HorizontalAlign="Right"
										ItemStyle-Width="6%"
										ItemStyle-BorderColor="Navy" />

                                        
                                    <asp:BoundField HeaderText="Settlement Amount (D)"
										DataField="MilestoneSettelmentAmt"
										ItemStyle-HorizontalAlign="Right"
                                         HeaderStyle-HorizontalAlign="Right"
										ItemStyle-Width="6%"
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
										ItemStyle-Width="5%"
										ItemStyle-BorderColor="Navy" />
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
                            <span>Cost Center</span>
                            <br />
                            <asp:DropDownList runat="server" ID="lstProjectDept" CssClass="DropdownListSearch">
                            </asp:DropDownList>
                        </li>
                        <li class="trvl_date" id="idWithoutInv_Vendor" runat="server">
                              <span>Invoice Type</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                            <asp:DropDownList runat="server" ID="lstDisplayPOTypes" CssClass="DropdownListSearch">
                            </asp:DropDownList>

                            <span style="display:none">Cost Center</span> 
                            <asp:DropDownList runat="server" ID="lstCostCenter" CssClass="DropdownListSearch" Enabled="false" Visible="false">
                            </asp:DropDownList>
                        </li>
                        <li class="trvl_date" id="idWithoutInv_Blank" runat="server">
                            <span>Vendor</span><br />
                            <asp:DropDownList runat="server" ID="lstVendors" AutoPostBack="true" CssClass="DropdownListSearch">
                            </asp:DropDownList>
                        </li>

                        <li class="trvl_date" id="idWithoutInv_bank_1" runat="server"></li>
                        <li class="trvl_date" id="idWithoutInv_bank_2" runat="server"></li>
                        <li class="trvl_date" id="idWithoutInv_bank_3" runat="server"></li>  
                      
                       <li class="trvl_date" id="idWithoutInv_InvoiceType_1" runat="server">
                           <span id="idspnSelectExpenses" runat="server">Select Expenses</span>&nbsp;&nbsp;<span style="color: red" id="idspnexpstar" runat="server">*</span><br />                            
                            <asp:DropDownList runat="server" ID="lstExpenses" CssClass="DropdownListSearch" AutoPostBack="true">
                            </asp:DropDownList>
                        </li>
                        <li class="trvl_date" id="idWithoutInv_InvoiceType_2" runat="server">
                            <span id="idspnSelectExpenses_details" runat="server">Select Expenses Details</span>
                            <br />
                            <asp:DropDownList runat="server" ID="lstExpense_details" CssClass="DropdownListSearch" Enabled="false">
                            </asp:DropDownList>
                        </li>
                        <li class="trvl_date" id="idWithoutInv_InvoiceType_3" runat="server">                           
                        </li>
                         
                        <li class="trvl_date" id="Li2" runat="server"></li>
                        <li class="trvl_date" id="Li3" runat="server"></li>
                        <li class="trvl_date" id="Li4" runat="server"></li>  

                         <li class="trvl_date" id="idWithoutInv_InvoiceRemarks_1" runat="server">                            
                            <span>Invoice Description </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtInvocieRemarks" runat="server" TextMode="MultiLine" CssClass="InvoiceRemarksText"  Enabled="false"></asp:TextBox>
                        </li>
                        <li class="trvl_date"  id="idWithoutInv_InvoiceRemarks_2" runat="server"></li>
                        <li class="trvl_date"  id="idWithoutInv_InvoiceRemarks_3" runat="server"></li>  


                        <li class="trvl_grid">  
                            <div class="InvoiceMilestonegrid"> 
                                <div style="width:102%;overflow:auto"> 
                                <asp:GridView ID="gvInvoiceMilestone_Acc" Visible="false" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="105%" EditRowStyle-Wrap="false"
                                    DataKeyNames="srno,PaymentStatusID,MstoneID"  OnRowDataBound="gvInvoiceMilestone_Acc_RowDataBound" >
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

                                        <asp:TemplateField HeaderText="Direct Tax Section" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="lstDirectTaxSections_ACC" runat="server" CssClass="DropdownListSearch" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="lstDirectTaxSections_ACC_SelectedIndexChanged"></asp:DropDownList>
                                         </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="20%" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Percentage(%)" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                             <asp:TextBox AutoComplete="off" ID="txtDirectTaxPercentage_ACC" Width="60px" runat="server" MaxLength="5" AutoPostBack="true"  ReadOnly="true"  OnTextChanged="txtDirectTaxPercentage_ACC_TextChanged" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*)\./g, '$1');"></asp:TextBox>
                                            <asp:TextBox AutoComplete="off" ID="txtInvoiceAmtWithoutGST_ACC" Visible="false"  runat="server"  Text='<%# Eval("Milesstone_Amt_ForInvoice") %>'></asp:TextBox>
                                         </ItemTemplate>
                                      <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="20%" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Is LDC Available" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkLDC_Applicable_ACC"   runat="server" AutoPostBack="true" OnCheckedChanged="chkLDC_Applicable_ACC_CheckedChanged"/>
                                         </ItemTemplate>
                                      <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="10%" />
                                    </asp:TemplateField>
                                       <asp:BoundField HeaderText="Milestone No."
										DataField="Srno"
										ItemStyle-HorizontalAlign="left"
										HeaderStyle-HorizontalAlign="Left"
										ItemStyle-Width="5%"
										ItemStyle-BorderColor="Navy" />

									<asp:BoundField HeaderText="Milestone Particulars"
										DataField="MilestoneName"
										ItemStyle-HorizontalAlign="left"
										 HeaderStyle-HorizontalAlign="Left"
										ItemStyle-Width="30%"
										ItemStyle-BorderColor="Navy" />

									<asp:BoundField HeaderText="Invoice Amount (Without GST)"
										DataField="Milesstone_Amt_ForInvoice"
										ItemStyle-HorizontalAlign="Right"
										 HeaderStyle-HorizontalAlign="Right"
										ItemStyle-Width="10%"
										ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="CGST Per (%)"
                                        DataField="CGST_Per"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />

                                     <asp:BoundField HeaderText="SGST Per (%)"
                                        DataField="SGST_Per"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />

                                     <asp:BoundField HeaderText="IGST Per (%)"
                                        DataField="IGST_Per"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />

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
 
                                      <asp:BoundField HeaderText="Invoice Amount (With GST)"
										DataField="MilestoneAmt_WithTax_ForInvoice"
										ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
										ItemStyle-Width="8%"
										ItemStyle-BorderColor="Navy" />

                                     <asp:BoundField HeaderText="Settlement Amount"
										DataField="Milestone_settlementAmt"
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
                              </div>
                            </div>
                        </li>

                       
                        <li class="trvl_date" id="liGst_per_blank_1" runat="server" visible="false"></li>
                        <li class="trvl_date" id="liGst_per_blank_2" runat="server" visible="false"></li>
                        <li class="trvl_date" id="liGst_per_blank_3" runat="server" visible="false"></li>


                        <li class="trvl_date" id="idMilestone" runat="server" style="display:none">
                            <span>Milestone Particulars </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtMilestoneName_Invoice" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date" id="idMilestoneAmt" runat="server" style="display:none">>
                            <span>Milestone Amount</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtMilestoneAmt_Invoice" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date" id="idBalanceAmt" runat="server" style="display:none">>
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
                            <span>Amount (Without GST)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtAmtWithOutTax" runat="server" MaxLength="10"  Enabled="False"></asp:TextBox>
                        </li>


                        <li class="trvl_date" id="liGST_Per_1" runat="server" visible="false">
                            <span>CGST(%)</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtCGST_Per" runat="server" ReadOnly="true" Enabled="False"> </asp:TextBox>
                        </li>
                        <li class="trvl_date" id="liGST_Per_2" runat="server" visible="false">
                            <span>SGST(%)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtSGST_Per" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date"  id="liGST_Per_3" runat="server" visible="false">
                            <span>IGST(%)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtIGST_Per" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>

                        <li class="trvl_date">
                            <span>CGST Amount</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtCGST_Amt" runat="server" ReadOnly="true" Enabled="False"> </asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>SGST Amount</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtSGST_Amt" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>IGST Amount</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtIGST_Amt" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>


                         <li class="trvl_date" id="idDirectTaxApplicable_TDS_1" runat="server" visible="false">
                            <span>Direct Tax Section</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                            <asp:DropDownList runat="server" ID="lstDirectTaxSections" AutoPostBack="true" CssClass="DropdownListSearch">
                            </asp:DropDownList>
                        </li>
                        <li class="trvl_date" id="idDirectTaxApplicable_TDS_2" runat="server" visible="false">
                            <span>TDS/ TCS Description</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtTDSTCS_Description" runat="server" ReadOnly="true" Enabled="False" ></asp:TextBox>
                        </li>
                        <li class="trvl_date" id="idDirectTaxApplicable_TDS_3" runat="server" visible="false"> 
                              <span id="spnLDCApplicable" runat="server" visible="false"> Is LDC Available</span><br />
                             <asp:CheckBox ID="chkLDC_Applicable" runat="server" Text="Is LDC Available" OnCheckedChanged="chkLDC_Applicable_CheckedChanged" AutoPostBack="true" Visible="false"/>
                        </li>



                        <li class="trvl_date" id="idDirectTaxApplicable" runat="server" visible="false"> 
                            <span>Direct Tax Applicable</span>
                            <asp:DropDownList runat="server" ID="lstDirectTax"  AutoPostBack="false" CssClass="DropdownListSearch"  ReadOnly="true" Enabled="False">
                            </asp:DropDownList>
                        </li>
                        <li class="trvl_date" id="idPercentage" runat="server" visible="false">
                            <span>Percentage(%)</span> 
                            <asp:TextBox AutoComplete="off"   ID="txtDirectTaxPercentage" runat="server" MaxLength="5" AutoPostBack="true"  ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date" id="idDirectTaxAmount" runat="server" visible="false">
                              <span>Direct tax Amount</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtDirecttaxAmt" runat="server" Enabled="false"></asp:TextBox>
                        </li> 

                         <li class="trvl_date" id="idDirectTaxAmt_ACC_1" runat="server" visible="false"> 
                              <span>Direct tax Amount</span><br />
                              <asp:TextBox AutoComplete="off" ID="txtDirecttaxAmt_ACC_Display" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="trvl_date" id="idDirectTaxAmt_ACC_2" runat="server" visible="false">                          
                        </li>
                        <li class="trvl_date" id="idDirectTaxAmt_ACC_3" runat="server" visible="false">                           
                        </li> 


                        <li class="trvl_date">
                            <span>Amount (With GST)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtAmtWithTax_Invoice" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>


                         <li class="trvl_grid" runat="server" id="idliCostCenterList_ACC">                         
                                <asp:Label ID="lblMilestoneCostCenter_Err" runat="server" Visible="True" Style="color: red; font-size: 13px; font-weight: 500; text-align: center;"></asp:Label>
                             <br />  
                             <asp:GridView ID="dgConstcneterList_Approved" runat="server" Visible="false" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="50%" >
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
                                     
                                     <asp:BoundField HeaderText="Cost Centre"
                                        DataField="CostCentre"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="left"
                                        ItemStyle-Width="20%"
                                        ItemStyle-BorderColor="Navy"/> 

                                    <asp:BoundField HeaderText="Cost Centre Amount (Without GST)"
                                        DataField="CostCenter_Amt_format"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="10%"
                                        ItemStyle-BorderColor="Navy"/> 

                                </Columns>
                            </asp:GridView>
                           <br />
                            
                        </li>

                         

                        <li class="trvl_date">
                            <span>Remarks </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtRemarks" runat="server" MaxLength="100" TextMode="MultiLine"></asp:TextBox>
                        </li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>

                        <li class="trvl_local">
                            <span>Invoice File</span>
                            <br />
                            <asp:LinkButton ID="lnkfile_Invoice" runat="server" OnClientClick="DownloadFile_S()" Visible="false" CssClass="BtnShow"></asp:LinkButton>
                        </li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>


                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>

                        <li class="trvl_local">
                            <span id="spnSupportinFiles" runat="server" visible="false">Supporting Files</span>
                            <asp:GridView ID="gvuploadedFiles" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
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
                                    <asp:TemplateField HeaderText="File Name">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkviewfile" CssClass="BtnShow"  runat="server" OnClientClick=<%# "DownloadFile('" + Eval("filename") + "')" %> Text='<%# Eval("filename") %>'>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDeleteexpFile" runat="server" CssClass="BtnShow" Text="View"  OnClientClick=<%# "DownloadFile('" + Eval("FileName") + "')" %>>
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
        <span>
            <asp:LinkButton ID="trvl_btnSave" runat="server" Text="Submit" ToolTip="Submit" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();" OnClick="trvl_btnSave_Click">Submit</asp:LinkButton>
        </span>
        <asp:LinkButton ID="btnback_mng" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="btnback_mng_Click">Back</asp:LinkButton>
         <asp:LinkButton ID="btnTra_Details" runat="server" Visible="true" Text="Download PO/ WO" ToolTip="Download PO/ WO" CssClass="Savebtnsve" OnClientClick="Download_ApprovedPO()">Download PO/ WO</asp:LinkButton>
     </div>

    <div>
        <span class="LableName" runat="server" visible="false" id="spInvocies">Invoice History  </span>
        <br />
        <br />
        <asp:GridView ID="gvMngPaymentList_Batch" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
            DataKeyNames="InvoiceID" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false">
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
                    HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
                 

                <asp:BoundField HeaderText="CGST Amount"
                    DataField="CGST_Amt"
                    ItemStyle-HorizontalAlign="Right"
                    HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                <asp:BoundField HeaderText="SGST Amount"
                    DataField="SGST_Amt"
                    ItemStyle-HorizontalAlign="Right"
                    HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                <asp:BoundField HeaderText="IGST Amount"
                    DataField="IGST_Amt"
                    ItemStyle-HorizontalAlign="Right"
                    HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                <asp:BoundField HeaderText="Direct Tax Amount"
                    DataField="DirectTax_Amount"
                    ItemStyle-HorizontalAlign="Right"
                    HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                <asp:BoundField HeaderText="Amount (With GST)"
                    DataField="AmtWithTax"
                    ItemStyle-HorizontalAlign="Right"
                    HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                <asp:BoundField HeaderText="Payment Status"
                    DataField="PyamentStatus"
                    ItemStyle-HorizontalAlign="Left"
                    HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
            </Columns>
        </asp:GridView>
    </div>

    
    <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
    <asp:TextBox AutoComplete="off" ID="txtEmpName" runat="server" Visible="false"></asp:TextBox>
    <asp:Label ID="testsanjay" runat="server" Visible="false"></asp:Label>
    <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
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
    <asp:HiddenField ID="hdnInvocieAmtWithTax" runat="server" />
    
    <asp:HiddenField ID="hdnMilestoneRowCnt" runat="server" /> 
    <asp:HiddenField ID="hdnApprovedPO_FileName" runat="server" />
    <asp:HiddenField ID="hdnApprovedPO_FilePath" runat="server" />

     <asp:HiddenField ID="hdnSingPOCopyFilePath" runat="server" /> 
     <asp:HiddenField ID="hdnSingPOCopyFileName" runat="server" />
    <asp:HiddenField ID="hdnInvoiceApprovalStatus" runat="server" />


    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

      <script src="../js/freeze/jquery-ui.min.js"></script>
    <script src="../js/freeze/gridviewScroll.min.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {
            $(".DropdownListSearch").select2();

             $(".DropdownListSearch").select2();

              $('#MainContent_dgTravelRequest').gridviewScroll({
                width: 1070,
                height: 600,
                freezesize:6, // Freeze Number of Columns.
                headerrowcount: 1, //Freeze Number of Rows with Header.
              });

             $('#MainContent_gvInvoiceMilestone').gridviewScroll({
                width: 1070,
                height: 600,
                freezesize:3, // Freeze Number of Columns.
                headerrowcount: 1, //Freeze Number of Rows with Header.
             });

             //$('#MainContent_gvInvoiceMilestone_Acc').gridviewScroll({
             //   width: 1070,
             //   height: 600,
             //   freezesize:3, // Freeze Number of Columns.
             //   headerrowcount: 1, //Freeze Number of Rows with Header.
             // });

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
            //window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
            //window.open("https://192.168.21.193/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
            window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
        }

         function DownloadFile_S() {
            
             var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;
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
             var localFileName = document.getElementById("<%=hdnSingPOCopyFileName.ClientID%>").value;

            //alert(localFilePath);
            window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + localFileName);

             //window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + localFileName);

        }

        function Download_ApprovedPO() {
            // alert(file);
            var localFilePath = document.getElementById("<%=hdnApprovedPO_FilePath.ClientID%>").value;
             var localFileName = document.getElementById("<%=hdnApprovedPO_FileName.ClientID%>").value;

            //alert(localFilePath);
            window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + localFileName);

            //window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + localFileName);

        }
         
    </script>
</asp:Content>


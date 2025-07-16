<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="VSCB_CreateBatch.aspx.cs" Inherits="VSCB_CreateBatch" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/manageleave_css.css" type="text/css" media="all" />

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

        .LableName {
            color: #F28820;
            font-size: 16px;
            font-weight: normal;
            text-align: left;
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

        .txtAligntextBox {
            text-align: right;
        }


        .paging a
        {
            background-color: #C7D3D4;
            padding: 5px 7px;
            text-decoration: none;
            border: 1px solid #C7D3D4;
        }
         
        .paging a:hover
        {
            background-color: #E1FFEF;
            color: #00C157;
            border: 1px solid #C7D3D4;
        }
         
        .paging span
        {
            background-color: #E1FFEF;
            padding: 5px 7px;
            color: #00C157;
            border: 1px solid #C7D3D4;
        }
         
        tr.paging
        {
            background: none !important;
        }
         
        tr.paging tr
        {
            background: none !important;
        }
        tr.paging td
        {
            border: none;
        }


    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>


    <script src="../js/freeze/jquery-1.11.0.min.js"></script>
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />


    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">

                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Create Payment Batch"></asp:Label>
                    </span>
                </div>
                <div>
                    <span>
                        <a href="vscb_index.aspx" class="aaaa"><%=System.Configuration.ConfigurationManager.AppSettings["VSCBPageTitle"]%></a>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                </div>


                <div class="edit-contact">
                    <ul id="ulSearch" runat="server">
                        <li style="padding-top: 30px">
                            <span>PO/ WO Number.</span>&nbsp;&nbsp;
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
                        <li style="padding-top: 15px">
                            <span>Vendor Name</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" ID="lstVendorName" CssClass="DropdownListSearch">
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

                        <li style="padding-top: 15px">
                            <span>Cost Center</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstDepartment">
                            </asp:DropDownList>
                        </li>
                        <li style="padding-top: 15px">
                            <span>Payment Request Amt</span>&nbsp;&nbsp;
                             <br />
                            <asp:TextBox runat="server" ID="txtPaymentRequestamt" AutoComplete="off" CssClass="number"></asp:TextBox>

                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstStatus" Visible="false">
                            </asp:DropDownList>

                        </li>
                        <li style="padding-top: 15px">
                            <span>Payment Request Type</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstPaymentRequestType">
                            </asp:DropDownList>
                        </li>



                        <li>
                            <span>
                                <asp:LinkButton ID="btnCorrection" runat="server" Text="Search" ToolTip="Search" CssClass="Savebtnsve" OnClick="btnCorrection_Click">Search</asp:LinkButton>
                            </span>

                        </li>

                        <li class="trvl_grid" id="litrvlgrid" runat="server">
                            <%--  <div class="manage_grid" style="overflow: scroll; width: 100%; padding-top: 20px; margin-bottom: 30px;">--%>

                            <asp:GridView ID="gvMngTravelRqstList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                                DataKeyNames="POID,paymentType_id,Payment_ID,InvoiceID,VendorID,CurID" CellPadding="3" AutoGenerateColumns="False" Width="140%" EditRowStyle-Wrap="false" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvMngTravelRqstList_PageIndexChanging">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                <PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" />
                                <RowStyle ForeColor="#000066" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                                <PagerStyle HorizontalAlign = "Right" CssClass = "paging" />

                                <Columns>
                                    <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkLeaveDetails" runat="server" ToolTip="View Approved Payment" Width="13px" Height="13px"   ImageUrl="~/Images/edit.png" OnClick="lnkLeaveDetails_Click" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"/>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Add" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkAddPaymentRequest" AutoPostBack="false" runat="server" OnCheckedChanged="chkAddPaymentRequest_CheckedChanged" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:BoundField HeaderText="Payment Request Type"
                                        DataField="paymentType"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="left"
                                        ItemStyle-BorderColor="Navy" ItemStyle-Width="5%" />

                                    <asp:BoundField HeaderText="Vendor Name"
                                        DataField="Name"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="left"
                                        ItemStyle-BorderColor="Navy" ItemStyle-Width="15%" />

                                    <asp:BoundField HeaderText="Payment request Created By"
                                        DataField="PayementRequestCreatedBy"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-BorderColor="Navy" ItemStyle-Width="13%" /> 

                                    <asp:BoundField HeaderText="Payment request No"
                                        DataField="PaymentReqNo"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-BorderColor="Navy" ItemStyle-Width="15%" />

                                    <asp:BoundField HeaderText="Payment request Date"
                                        DataField="PaymentReqDate"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-BorderColor="Navy" ItemStyle-Width="5%" />

                                    <asp:BoundField HeaderText="Amount to be paid"
                                        DataField="Amt_paid_Account"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-BorderColor="Navy" ItemStyle-Width="5%" />

                                    <asp:BoundField HeaderText="Payment request approved by"
                                        DataField="PaymentApproverName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-BorderColor="Navy" ItemStyle-Width="13%" />

                                    <asp:BoundField HeaderText="Invoice No"
                                        DataField="InvoiceNo"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="left"
                                        ItemStyle-BorderColor="Navy" ItemStyle-Width="8%" />

                                    <asp:BoundField HeaderText="Invoice Amount"
                                        DataField="InvoiceAmt"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-BorderColor="Navy" ItemStyle-Width="5%" />

                                    <asp:BoundField HeaderText="Invoice Balance Amount"
                                        DataField="Invoice_Balance_Amt"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-BorderColor="Navy" ItemStyle-Width="5%" />

                                    <asp:BoundField HeaderText="Invoice approved by"
                                        DataField="InvoiceApproverName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-BorderColor="Navy" ItemStyle-Width="13%" />


                                      <asp:BoundField HeaderText="Milestone Particular"
                                        DataField="MilestoneParticular"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-BorderColor="Navy" ItemStyle-Width="25%" /> 

                                    <asp:BoundField HeaderText="PO/ WO Number"
                                        DataField="PONumber"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="left"
                                        ItemStyle-BorderColor="Navy" ItemStyle-Width="10%" />

                                    <asp:BoundField HeaderText="PO/ WO Date"
                                        DataField="PODate"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="left"
                                        ItemStyle-BorderColor="Navy" ItemStyle-Width="5%" />

                                    <asp:BoundField HeaderText="Acc.Remarks"
                                        DataField="AccRemarks"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="left"
                                        ItemStyle-BorderColor="Navy" ItemStyle-Width="10%" /> 

                                         <asp:TemplateField HeaderText="View Bank Details" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>                                               
                                                <asp:LinkButton ID="lnkBankdetails"  OnClientClick=<%#"downloadBankdetails('" + Eval("bankfilename") + "')" %> CssClass="BtnShow" runat="server" Text='View' Visible='<%#Eval("showbankdtls").ToString() == "1" ? true : false %>'>

                                                </asp:LinkButton>
                                               </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="10%" />
                                        </asp:TemplateField>

                                </Columns>
                            </asp:GridView>

                            <%--  </div>--%>

                 

                        </li>

                        <li>
                            <asp:LinkButton ID="trvl_btnSave" Visible="false" runat="server" Text="Add Payment Request" ToolTip="Add Payment Request for Create batch" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click">Add Payment Request </asp:LinkButton>
                        </li>
                        <li></li>
                        <li></li>

                    </ul>
                    <ul id="Ul1" runat="server">



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
                            <span id="spnBatchNo" runat="server" visible="false">Batch No.:</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtbatchNo" runat="server" ReadOnly="true" Enabled="False" Visible="false"></asp:TextBox>
                        </li>


                        <li class="trvl_date">
                            <span>No. of Requests in Batch:</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtbatchNoOfRequest" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>Batch Total Payament:</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtbatchTotalPayment" runat="server" ReadOnly="true" Enabled="False" CssClass="txtAligntextBox"></asp:TextBox>
                        </li>
                        <li class="trvl_date"></li>

                        <li class="trvl_date" id="idBankRef_1" runat="server" visible="false">
                            <span>Bank Ref.No:</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtBankRefNo" runat="server" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date" id="idBankRef_2" runat="server" visible="false">
                            <span>Bank Ref.Date:</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtBankRefDate" runat="server" Enabled="False"></asp:TextBox>

                        </li>
                        <li class="trvl_date" id="idBankRef_3" runat="server" visible="false">
                            <span>Bank Ref.Link:</span><br />
                            <a id="lnkBank" runat="server" class="BtnShow" target="_blank"></a>
                            <asp:TextBox AutoComplete="off" ID="txtBankRef_Link" runat="server" Visible="false" Enabled="false"></asp:TextBox>
                        </li>


                        <li class="trvl_date">
                            <span>Select Bank Name</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList runat="server" ID="lstBanksList" CssClass="DropdownListSearch">
                            </asp:DropDownList>

                        </li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>

                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>

                        <li class="trvl_grid">
                            <%-- <div class="manage_grid" style="overflow: scroll; width: 100%; padding-top: 20px; margin-bottom: 30px;">--%>

                            <asp:GridView ID="gvMngPaymentList_Batch" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                                DataKeyNames="POID,MstoneID,Payment_ID,InvoiceID,paymentType_id" CellPadding="3" AutoGenerateColumns="False" Width="150%" EditRowStyle-Wrap="false" PageSize="100" AllowPaging="true" OnRowDataBound="gvMngPaymentList_Batch_RowDataBound" OnPageIndexChanging="gvMngPaymentList_Batch_PageIndexChanging">
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

                                    <asp:TemplateField HeaderText="Batch" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkTravelDetailsEdit" CssClass="BtnShow" runat="server" Text='Delete' OnClick="lnkTravelDetailsEdit_Click">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="4%" />
                                    </asp:TemplateField>

                                      <asp:BoundField HeaderText="Payment Request Type"
                                        DataField="paymentType"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="left"
                                        ItemStyle-BorderColor="Navy" ItemStyle-Width="5%" />

                                    <asp:BoundField HeaderText="Vendor Name"
                                        DataField="VendorName"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="left"
                                        ItemStyle-BorderColor="Navy" ItemStyle-Width="15%" />


                                    <asp:BoundField HeaderText="Payment request Created By"
                                        DataField="PayementRequestCreatedBy"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-BorderColor="Navy" ItemStyle-Width="13%" />

                                    <asp:BoundField HeaderText="Payment request No"
                                        DataField="PaymentReqNo"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="left"
                                        ItemStyle-BorderColor="Navy" ItemStyle-Width="15%" />

                                    <asp:BoundField HeaderText="Payment request Date"
                                        DataField="PaymentReqDate"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="left"
                                        ItemStyle-BorderColor="Navy" ItemStyle-Width="5%" />

                                    <asp:BoundField HeaderText="Amount to be paid"
                                        DataField="Amt_paid_Account"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-BorderColor="Navy" ItemStyle-Width="8%" />

                                    <asp:BoundField HeaderText="Payment request approved by"
                                        DataField="PaymentApproverName"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="left"
                                        ItemStyle-BorderColor="Navy" ItemStyle-Width="10%" />


                                    <asp:BoundField HeaderText="Invoice No"
                                        DataField="InvoiceNo"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="left"
                                        ItemStyle-BorderColor="Navy" ItemStyle-Width="8%" />

                                    <asp:BoundField HeaderText="Invoice Amount"
                                        DataField="InvoiceAmt"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-BorderColor="Navy" ItemStyle-Width="8%" />

                                    <asp:BoundField HeaderText="Invoice Balance Amount"
                                        DataField="Invoice_Balance_Amt"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-BorderColor="Navy" ItemStyle-Width="8%" />

                                    <asp:BoundField HeaderText="Invoice approved by"
                                        DataField="InvoiceApproverName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-BorderColor="Navy" ItemStyle-Width="13%" />


                                     <asp:BoundField HeaderText="Milestone Particular"
                                        DataField="MilestoneParticular"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-BorderColor="Navy" ItemStyle-Width="25%" /> 

                                    <asp:BoundField HeaderText="PO/ WO Number"
                                        DataField="PONumber"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="left"
                                        ItemStyle-BorderColor="Navy" ItemStyle-Width="10%" />

                                    <asp:BoundField HeaderText="PO/ WO Date"
                                        DataField="PODate"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="left"
                                        ItemStyle-BorderColor="Navy" ItemStyle-Width="8%" />



                                </Columns>
                            </asp:GridView>

                            <%-- </div>--%>
                        </li>


                        <li style="padding-top: 15px" id="lichecker" runat="server">
                            <span>Checker</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList runat="server" ID="lstChecker" CssClass="DropdownListSearch">
                            </asp:DropDownList>
                        </li>
                        <li style="padding-top: 15px" id="liApprover1" runat="server">
                            <span>Approver 1</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList runat="server" ID="lstApprover1" CssClass="DropdownListSearch">
                            </asp:DropDownList>
                        </li>
                        <li style="padding-top: 15px" id="liApprover2" runat="server">
                            <span>Approver 2</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList runat="server" ID="lstApprover2" CssClass="DropdownListSearch">
                            </asp:DropDownList>
                        </li>

                        <li class="trvl_Approver">
                            <br />
                            <br />
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
                                        DataField="remarks"
                                        ItemStyle-HorizontalAlign="left"
                                        ItemStyle-Width="37%"
                                        ItemStyle-BorderColor="Navy" />
                                </Columns>
                            </asp:GridView>
                        </li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>

                    </ul>
                </div>

                <div class="trvl_Savebtndiv">

                    <span>
                        <asp:LinkButton ID="btnCancel" runat="server" Text="Create Batch" ToolTip="Create Batch Request" CssClass="Savebtnsve" OnClientClick="return CancelMultiClick();" OnClick="btnCancel_Click">Create Batch</asp:LinkButton>
                    </span>

                    <asp:LinkButton ID="btnback_mng" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/vscb_index.aspx">Back</asp:LinkButton>
                </div>

                <ul>
                    <li>
                        <asp:Label runat="server" ID="RecordCount" Style="color: red; font-size: 14px;"></asp:Label>
                    </li>
                    <li></li>
                    <li></li>
                </ul>



                <%--'<%#"~/Details.aspx?ID="+Eval("ID") %>'>--%>



                <div class="edit-contact">
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" OnClick="lnkcont_Click"></asp:LinkButton>
                        </div>
                    </div>
                    <ul id="editform" runat="server" visible="false">
                        <li></li>
                    </ul>
                </div>
                <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
                <asp:TextBox AutoComplete="off" ID="txtEmpName" runat="server" Visible="false"></asp:TextBox>
                <asp:HiddenField ID="hdnPOWOID" runat="server" />
                <asp:HiddenField ID="hdnMilestoneId" runat="server" />
                <asp:HiddenField ID="hdnInvoiceId" runat="server" />

                <asp:HiddenField ID="hflEmpName" runat="server" />
                <asp:HiddenField ID="hflEmpDesignation" runat="server" />
                <asp:HiddenField ID="hflEmpDepartment" runat="server" />
                <asp:HiddenField ID="hflEmailAddress" runat="server" />
                <asp:HiddenField ID="hflGrade" runat="server" />
                <asp:HiddenField ID="hdnVendorBankDetails" runat="server" />

                <asp:HiddenField ID="hdnYesNo" runat="server" />
                <asp:HiddenField ID="hdncurId" runat="server" />
                <asp:HiddenField ID="hdStatusIDcheck" runat="server" />


            </div>
        </div>
    </div>

    <%--  <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />--%>

    <script src="../js/freeze/jquery-ui.min.js"></script>
    <script src="../js/freeze/gridviewScroll.min.js"></script>

    <script type="text/javascript">      
        $(document).ready(function () {
            $(".DropdownListSearch").select2();

            $('#MainContent_gvMngTravelRqstList').gridviewScroll({
                width: 1060,
                height: 600,
                freezesize: 4, // Freeze Number of Columns.
                headerrowcount: 1, //Freeze Number of Rows with Header.
            });

            $('#MainContent_gvMngPaymentList_Batch').gridviewScroll({
                width: 1060,
                height: 600,                
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

        function downloadBankdetails(filename) {
          // alert(filename);
             var localFilePath = document.getElementById("<%=hdnVendorBankDetails.ClientID%>").value;
            var localFileName = filename;

            //alert(localFilePath);
            window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + localFileName);

             //window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + localFileName);

        }

    </script>
</asp:Content>


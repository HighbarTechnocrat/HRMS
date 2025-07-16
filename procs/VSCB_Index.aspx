<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" 
	CodeFile="VSCB_Index.aspx.cs" Inherits="procs_VSCB_Index" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

	<link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />

	<style>
		.myaccountpagesheading {
			text-align: center !important;
			text-transform: uppercase !important;
		}

		.aspNetDisabled {
			/*background: #dae1ed;*/
			background: #ebebe4;
		}

		.Calender {
			float: left;
			padding: 5% 5% 5% 5% !important;
		}


		#MainContent_lnk_leaverequest:link, #MainContent_lnk_leaverequest:visited,
		#MainContent_lnk_mng_leaverequest:link, #MainContent_lnk_mng_leaverequest:visited,
		#MainContent_lnk_reimbursmentReport:link, #MainContent_lnk_reimbursmentReport:visited,
		#MainContent_lnk_leaveinbox:link, #MainContent_lnk_leaveinbox:visited,
		#MainContent_lnk_MobACC:link, #MainContent_lnk_MobACC:visited,
		#MainContent_lnk_MobCFO:link, #MainContent_lnk_MobPastApproved_ACC:link, #MainContent_lnk_MobCFO:visited, #MainContent_lnk_MobPastApproved_ACC:visited,
		#MainContent_lnk_reimbursmentReport_1:link, #MainContent_lnk_reimbursmentReport_1:visited,
		#MainContent_lnk_summary_report:link, #MainContent_lnk_summary_report:visited,
		#MainContent_lnk_CustomerFirstReport:link, #MainContent_lnk_CustomerFirstReport:visited,
		#MainContent_lnk_CustomerFirstView:link, #MainContent_lnk_CustomerFirstView:visited,
		#MainContent_lnk_Inbox_Payment_Request:link, #MainContent_lnk_Inbox_Payment_Request:visited,
		#MainContent_lnk_Index_Acc_Invoices:link, #MainContent_lnk_Index_Acc_Invoices:visited,
		#MainContent_lnk_Index_Acc_Payment_Requests:link, #MainContent_lnk_Index_Acc_Payment_Requests:visited,
		#MainContent_lnk_Index_Acc_Batch_Approval:link, #MainContent_lnk_Index_Acc_Batch_Approval:visited,
		#MainContent_lnk_Index_Acc_Batch_Requests:link, #MainContent_lnk_Index_My_Batch_Requests:link, #MainContent_lnk_Index_Acc_Batch_Requests:visited,
		#MainContent_lnk_Index_My_Batch_Requests:visited,
		#MainContent_lnk_Inbox_Payment_View:link, #MainContent_lnk_Inbox_Payment_View:visited,
		#MainContent_lnk_Approved_Invoice:link, #MainContent_lnk_lnk_Approved_Invoice:visited,
		#MainContent_lnk_Payment_Request_withOutPO:link, #MainContent_lnk_Payment_Request_withOutPO:visited,
		#MainContent_lnk_Index_myapprovedBatchList:link, #MainContent_lnk_Index_myapprovedBatchList:visited,
		#MainContent_lnk_audit_trail_Report:link, #MainContent_lnk_audit_trail_Report:visited,
		#MainContent_lnk_Payment_Request_view:link, #MainContent_lnk_Payment_Request_view:visited,
		#MainContent_lnk_VendorwiseDuesReport:link, #MainContent_lnk_VendorwiseDuesReport:visited,
		#MainContent_lnk_VendorWisePaymentHistory:link, #MainContent_lnk_VendorWisePaymentHistory:visited,
        #MainContent_lnk_POWOInbox:link, #MainContent_lnk_POWOInbox:visited ,
        #MainContent_lnk_ApprovedPOWO_Inbox:link, #MainContent_lnk_ApprovedPOWO_Inbox:visited,
        #MainContent_lnk_InvoiceApprovalMatrix:link, #MainContent_lnk_InvoiceApprovalMatrix:visited,
        #MainContent_lnkAttachBatch_BankPaymentRef:link, #MainContent_lnkAttachBatch_BankPaymentRef:visited ,        
        #MainContent_lnkAttachBatch_BankPaymentRef_View:link, #MainContent_lnkAttachBatch_BankPaymentRef_View:visited,
        #MainContent_lnkPOWOACC_View:link, #MainContent_lnkPOWOACC_View:visited,
        #MainContent_lnkInvoiceACC_View:link, #MainContent_lnkInvoiceACC_View:visited,
        #MainContent_Lnk_PaymentRequestAll:link, #MainContent_Lnk_PaymentRequestAll:visited,
        #MainContent_lnk_ApprovalStatusreport:link, #MainContent_lnk_ApprovalStatusreport:visited,
        #MainContent_Lnk_Createvendor:link, #MainContent_Lnk_Createvendor:visited,
        #MainContent_lnk_ApprovedPOWOList:link, #MainContent_lnk_ApprovedPOWOList:visited,
        #MainContent_lnk_PaymentApprovalStatusreport:link, #MainContent_lnk_PaymentApprovalStatusreport:visited,
        #MainContent_lnkuploadcutoffdata:link, #MainContent_lnkuploadcutoffdata,
        #MainContent_Lnk_Invoice_Payment_Maker:link, #MainContent_Lnk_Invoice_Payment_Maker,
        #MainContent_lnk_PaymentApprovalMatrix:link,#MainContent_lnk_PaymentApprovalMatrix:visited,
        #MainContent_lnkAttachBatch_BankPaymentApproved:link, #MainContent_lnkAttachBatch_BankPaymentApproved:visited,
        #MainContent_lnk_ViewAllDetails:link, #MainContent_lnk_ViewAllDetails:visited ,
        #MainContent_lnkbtn_ACCPIADAMTCHANGE:link, #MainContent_lnkbtn_ACCPIADAMTCHANGE:visited,
        #MainContent_Lnk_POWO_Other_Approval:link, #MainContent_Lnk_POWO_Other_Approval:visited,
        #MainContent_lnk_Payment_Advice_list_Report:link, #MainContent_lnk_Payment_Advice_list_Report:visited,
        #MainContent_lnk_POWO_Approval_Status_Report:link, #MainContent_lnk_POWO_Approval_Status_Report:visited,
        #MainContent_lnkbtn_ChangeInvoiceTDSAmount:link, #MainContent_lnkbtn_ChangeInvoiceTDSAmount:visited,
        #MainContent_Lnk_Create_Advance_Pay_Requests:link, #MainContent_Lnk_Create_Advance_Pay_Requests:visited,
		#MainContent_Lnk_My_Advance_Pay_Requests:link, #MainContent_Lnk_My_Advance_Pay_Requests:visited,
		#MainContent_lnk_Inbox_ADV_Payment_Request:link, #MainContent_lnk_Inbox_ADV_Payment_Request:visited,
		#MainContent_lnk_Inbox_ADV_Payment_View:link, #MainContent_lnk_Inbox_ADV_Payment_View:visited,
         #MainContent_lnkBtnCreateproduct:link, #MainContent_lnkBtnCreateproduct:visited,
        #MainContent_Lnk_InboxInvoiceReverse:link, #MainContent_Lnk_InboxInvoiceReverse:visited,
        #MainContent_Lnk_ApprovedInvoiceReverse:link, #MainContent_Lnk_ApprovedInvoiceReverse:visited,
        #MainContent_lnk_CreateInvoice_SecurityDeposit:link, #MainContent_lnk_CreateInvoice_SecurityDeposit:visited  
		{
			background-color: #C7D3D4;
			color: #603F83 !important;
			border-radius: 10px;
			/*color: white;  */
			padding: 25px 5px;
			text-align: center;
			text-decoration: none;
			display: inline-block;
			width: 90% !important;
		}

	        #MainContent_lnk_leaverequest:hover, #MainContent_lnk_leaverequest:active,
            #MainContent_Lnk_InboxInvoiceReverse:hover, #MainContent_Lnk_InboxInvoiceReverse:active,
            #MainContent_Lnk_ApprovedInvoiceReverse:hover, #MainContent_Lnk_ApprovedInvoiceReverse:active,

	        #MainContent_Lnk_Invoice_Payment_Maker:hover, #MainContent_Lnk_Invoice_Payment_Maker:active,
	        #MainContent_lnk_mng_leaverequest:hover, #MainContent_lnk_mng_leaverequest:active,
	        #MainContent_lnk_ApprovalStatusreport:hover, #MainContent_lnk_ApprovalStatusreport:active,
	        #MainContent_Lnk_Createvendor:hover, #MainContent_Lnk_Createvendor:active,
	        #MainContent_lnk_reimbursmentReport:hover, #MainContent_lnk_reimbursmentReport:active,
	        #MainContent_lnk_reimbursmentReport:hover, #MainContent_Lnk_PaymentRequestAll:active,
	        #MainContent_lnk_leaveinbox:hover, #MainContent_lnk_leaveinbox:active,
	        #MainContent_lnk_MobACC:hover, #MainContent_lnk_MobACC:active,
	        #MainContent_lnk_MobCFO:hover, #MainContent_lnk_MobPastApproved_ACC:hover, #MainContent_lnk_MobCFO:active, #MainContent_lnk_MobPastApproved_ACC:active,
	        #MainContent_lnk_reimbursmentReport_1:hover, #MainContent_lnk_reimbursmentReport_1:active,
	        #MainContent_lnk_summary_report:hover, #MainContent_lnk_summary_report:active,
	        #MainContent_lnk_CustomerFirstReport:hover, #MainContent_lnk_CustomerFirstReport:active,
	        #MainContent_lnk_CustomerFirstView:hover, #MainContent_lnk_CustomerFirstView:active,
	        #MainContent_lnk_Inbox_Payment_Request:hover, #MainContent_lnk_Inbox_Payment_Request:active,
	        #MainContent_lnk_Index_Acc_Invoices:hover, #MainContent_lnk_Index_Acc_Invoices:active,
	        #MainContent_lnk_Index_Acc_Payment_Requests:hover, #MainContent_lnk_Index_Acc_Payment_Requests:active,
	        #MainContent_lnk_Index_Acc_Batch_Approval:hover, #MainContent_lnk_Index_Acc_Batch_Approval:active,
	        #MainContent_lnk_Index_Acc_Batch_Requests:hover, #MainContent_lnk_Index_Acc_Batch_Requests:active,
	        #MainContent_lnk_Index_My_Batch_Requests:hover, #MainContent_lnk_Index_My_Batch_Requests:active,
	        #MainContent_lnk_Inbox_Payment_View:hover, #MainContent_lnk_Inbox_Payment_View:active,
	        #MainContent_lnk_Approved_Invoice:hover, #MainContent_lnk_lnk_Approved_Invoice:active,
	        #MainContent_lnk_Payment_Request_withOutPO:hover, #MainContent_lnk_Payment_Request_withOutPO:active,
	        #MainContent_lnk_Index_myapprovedBatchList:hover, #MainContent_lnk_Index_myapprovedBatchList:active,
	        #MainContent_lnk_audit_trail_Report:hover, #MainContent_lnk_audit_trail_Report:active,
	        #MainContent_lnk_Payment_Request_view:hover, #MainContent_lnk_Payment_Request_view:active,
	        #MainContent_lnk_VendorwiseDuesReport:hover, #MainContent_lnk_VendorwiseDuesReport:active,
	        #MainContent_lnk_VendorWisePaymentHistory:hover, #MainContent_lnk_VendorWisePaymentHistory:active,
	        #MainContent_lnk_POWOInbox:hover, #MainContent_lnk_POWOInbox:active,
	        #MainContent_lnk_ApprovedPOWO_Inbox:hover, #MainContent_lnk_ApprovedPOWO_Inbox:active,
	        #MainContent_lnk_InvoiceApprovalMatrix:hover, #MainContent_lnk_InvoiceApprovalMatrix:active,
	        #MainContent_lnkAttachBatch_BankPaymentRef:hover, #MainContent_lnkAttachBatch_BankPaymentRef:active,
	        #MainContent_lnkAttachBatch_BankPaymentRef_View:hover, #MainContent_lnkAttachBatch_BankPaymentRef_View:active,
	        #MainContent_lnkPOWOACC_View:hover, #MainContent_lnkPOWOACC_View:active,
	        #MainContent_lnkInvoiceACC_View:hover, #MainContent_lnkInvoiceACC_View:active,
	        #MainContent_lnkuploadcutoffdata:hover, #MainContent_lnkuploadcutoffdata:active,
	        #MainContent_lnk_PaymentApprovalStatusreport:hover, #MainContent_lnk_PaymentApprovalStatusreport:active,
	        #MainContent_lnk_ApprovedPOWOList:hover, #MainContent_lnk_ApprovedPOWOList:active,
	        #MainContent_lnk_PaymentApprovalMatrix:hover, #MainContent_lnk_PaymentApprovalMatrix:active,
	        #MainContent_lnkAttachBatch_BankPaymentApproved:hover, #MainContent_lnkAttachBatch_BankPaymentApproved:active,
	        #MainContent_lnk_ViewAllDetails:hover, #MainContent_lnk_ViewAllDetails:active ,
            #MainContent_lnkbtn_ACCPIADAMTCHANGE:hover, #MainContent_lnkbtn_ACCPIADAMTCHANGE:active,
            #MainContent_Lnk_POWO_Other_Approval:hover, #MainContent_Lnk_POWO_Other_Approval:active,
            #MainContent_lnk_Payment_Advice_list_Report:hover, #MainContent_lnk_Payment_Advice_list_Report:active ,
            #MainContent_lnk_POWO_Approval_Status_Report:hover, #MainContent_lnk_POWO_Approval_Status_Report:active,
            #MainContent_lnkbtn_ChangeInvoiceTDSAmount:hover, #MainContent_lnkbtn_ChangeInvoiceTDSAmount:active,
            #MainContent_Lnk_Create_Advance_Pay_Requests:hover, #MainContent_Lnk_Create_Advance_Pay_Requests:active,
		    #MainContent_Lnk_My_Advance_Pay_Requests:hover, #MainContent_Lnk_My_Advance_Pay_Requests:active,
			#MainContent_lnk_Inbox_ADV_Payment_Request:hover, #MainContent_lnk_Inbox_ADV_Payment_Request:active,
		    #MainContent_lnk_Inbox_ADV_Payment_View:hover, #MainContent_lnk_Inbox_ADV_Payment_View:active,
             #MainContent_lnkBtnCreateproduct:hover, #MainContent_lnkBtnCreateproduct:active,
            #MainContent_lnk_CreateInvoice_SecurityDeposit:hover, #MainContent_lnk_CreateInvoice_SecurityDeposit:active
            {
	            /*background-color: #603F83; lnk_Approved_Invoice */
	            background-color: #3D1956;
	            color: #C7D3D4 !important;
	            border-color: #3D1956;
	            border-width: 2pt;
	            border-style: inset;
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
				<div class="wishlistpage">
					<div class="userposts">
						<span>
							<asp:Label ID="lblheading" runat="server" Text="Procurement System"></asp:Label>
						</span>
					</div>
					<%--  <div class="leavegrid">
                        <a href="https://ess.highbartech.com/hrms/Service.aspx" class="aaa" >Service Request Menu</a>
                     </div>--%>
					<div class="editprofile" id="editform1" runat="server" visible="true">
						<div class="editprofileform">
							<asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="true" Style="margin-left: 135px"></asp:Label>
							<table>
								<%--Create PO/ WO  User Acc--%>
								<tr style="padding-top: 1px; padding-bottom: 2px;" id="idTRPOWO_Create" runat="server" visible="false">
									<td class="formtitle">
										<asp:LinkButton ID="lnk_leaverequest" runat="server" Visible="true" ToolTip="Send PO/ WO For Approval" PostBackUrl="~/procs/VSCB_CreateMilestone.aspx">Send PO/ WO For Approval</asp:LinkButton>
									</td>
									<td class="formtitle">
										<asp:LinkButton ID="lnk_mng_leaverequest" runat="server" Visible="true" ToolTip="My PO/ WO" PostBackUrl="~/procs/VSCB_MyPOWOMilestone.aspx">My PO/ WO</asp:LinkButton>
									</td>
								</tr>

                                 

								<tr style="padding-top: 1px; padding-bottom: 2px;">
									<td class="formtitle">
										<asp:LinkButton ID="lnk_leaveinbox" runat="server" Text="" Visible="true" ToolTip="Create Invoices" PostBackUrl="~/procs/VSCB_CreateInvoice.aspx">Create Invoices</asp:LinkButton>
									</td>

									<td class="formtitle">
										<asp:LinkButton ID="lnk_reimbursmentReport" runat="server" Visible="true" Text="" ToolTip="My Invoices" PostBackUrl="~/procs/VSCB_MyInvoice.aspx">My Invoices</asp:LinkButton>
									</td>
								</tr>
 								<tr style="padding-top: 1px; padding-bottom: 2px;" id="tr_CreateInvoice_Security_Deposit" runat="server" visible="false">
									<td class="formtitle">
										<asp:LinkButton ID="lnk_CreateInvoice_SecurityDeposit" runat="server" Text="" Visible="false" ToolTip="Create Invoice Against Security Deposit" PostBackUrl="~/procs/VSCB_CreateInvoice_SecurityDeposit.aspx">Create Invoicve Against Security Deposit</asp:LinkButton>
									</td>

									<td class="formtitle">
										 
									</td>
								</tr>

								<tr style="padding-top: 1px; padding-bottom: 2px;">
									<td class="formtitle" runat="server" id="showPSR">
                                        <asp:LinkButton ID="lnk_Payment_Request_withOutPO" runat="server" Text="" Visible="false" ToolTip="Create Payment Requests Without PO/ WO" PostBackUrl="~/procs/VSCB_InboxPaymentRequest_WithOutPO.aspx">Create Payment Requests Without PO/ WO</asp:LinkButton>
										
                                        <asp:LinkButton ID="Lnk_PaymentRequestAll" runat="server" Text="" Visible="false"  PostBackUrl="~/procs/VSCB_PaymentRequestAll.aspx">Create Payment Requests (0)</asp:LinkButton>
                                    
									</td>
									<td class="formtitle" runat="server" id="Td1">
										<asp:LinkButton ID="lnk_reimbursmentReport_1" runat="server" Visible="false" Text="" ToolTip="My Payment Requests " PostBackUrl="~/procs/VSCB_InboxMyPaymentRequest.aspx">My Payment Requests </asp:LinkButton>
									</td>
								</tr>

								<tr style="padding-top: 1px; padding-bottom: 2px;" id="idTRPaymentReqWithPOWO" runat="server" visible="false">
									<td class="formtitle" runat="server" id="Td9">
										 <asp:LinkButton ID="lnk_MobACC" runat="server" Text="" Visible="true" ToolTip="Create Payment Requests With PO/ WO" PostBackUrl="~/procs/VSCB_PaymentRequest.aspx">Create Payment Requests With PO/ WO</asp:LinkButton>
									</td>

                                    <td class="formtitle" runat="server" id="idInvoiceApprMatrix">
										 <%--<asp:LinkButton ID="lnk_InvoiceApprovalMatrix" runat="server" Text="" Visible="true" ToolTip="Invoice/ Payment Approval Matrix" PostBackUrl="~/procs/VSCB_InvoiceAppMatrixList.aspx">View Invoice/ Payment Approval Matrix</asp:LinkButton>--%>
									
                                    </td>

								</tr>

                                <tr style="padding-top: 1px; padding-bottom: 2px;" runat="server" id="tdAdvance_Create" visible="false">
									<td class="formtitle" >
                                        <asp:LinkButton ID="Lnk_Create_Advance_Pay_Requests" runat="server" Text="" Visible="true" ToolTip="Create Advance Payment Requests" PostBackUrl="~/procs/VSCB_Advance_Payment_Create.aspx">Create Advance Payment Requests</asp:LinkButton>	
                                        
									</td>
									<td class="formtitle" runat="server" id="Td33">
									 
                                        	<asp:LinkButton ID="Lnk_My_Advance_Pay_Requests" runat="server" Visible="true" Text="" ToolTip="MyAdvance Payment Requests " PostBackUrl="~/procs/VSCB_InboxMy_Adv_PayRequest.aspx">My Advance Payment Requests </asp:LinkButton>

									</td>
								</tr>

                               <%-- <tr style="padding-top: 1px; padding-bottom: 2px;" id="IdTRCreInvoicePRequest" runat="server" visible="false">
									<td class="formtitle" runat="server" id="Td25">
                                         <asp:LinkButton ID="Lnk_PaymentRequestAll" runat="server" Text="" Visible="true"  PostBackUrl="~/procs/VSCB_PaymentRequestAll.aspx">Create Payment Requests (0)</asp:LinkButton>
                                    
									</td>

                                    <td class="formtitle" runat="server" id="Td26">
									
                                    </td>

								</tr>--%>

                                <tr style="padding-top: 1px; padding-bottom: 2px;" id="idTRInvoiceApprMatrix" runat="server" visible="false">
									<td class="formtitle" runat="server" id="Td19">
										 <asp:LinkButton ID="lnk_InvoiceApprovalMatrix" runat="server" Text="" Visible="true" ToolTip="Invoice Approval Matrix" PostBackUrl="~/procs/VSCB_InvoiceAppMatrixList.aspx">View Invoice Approval Matrix</asp:LinkButton>
									</td>

                                    <td class="formtitle" runat="server" id="Td20">	
                                         <asp:LinkButton ID="lnk_PaymentApprovalMatrix" runat="server" Text="" Visible="true" ToolTip="Payment Approval Matrix" PostBackUrl="~/procs/VSCB_PaymentAppMatrixList.aspx">View Payment Approval Matrix</asp:LinkButton>
									</td>

								</tr>

                                 <tr style="padding-top: 1px; padding-bottom: 2px;" id="idTRUploadCutoffdata" runat="server" visible="false">
									<td class="formtitle" runat="server" id="Td23">
										 <asp:LinkButton ID="lnkuploadcutoffdata" runat="server" Text="" Visible="true" ToolTip="Upload PO Cutoff data" PostBackUrl="~/procs/VSCB_Uploadcutoffdata.aspx">Upload PO/ WO cutoff Data</asp:LinkButton>
									</td>

                                    <td class="formtitle" runat="server" id="Td24">		
                                          
                                  </td>
								</tr>
                                                                
                                <tr style="padding-top: 1px; padding-bottom: 2px;" id="idTRACCPOWO" runat="server" visible="false">
									<td class="formtitle" runat="server" id="Td21">
										 <asp:LinkButton ID="lnkPOWOACC_View" runat="server" Text="" Visible="true" ToolTip="View Uploaded PO" PostBackUrl="~/procs/VSCB_MyPOWOMilestoneAcc.aspx">View Uploaded PO</asp:LinkButton>
									</td>

                                    <td class="formtitle" runat="server" id="Td22">		
                                         <asp:LinkButton ID="lnkInvoiceACC_View" runat="server" Text="" Visible="true" ToolTip="View Uploaded Invocie" PostBackUrl="~/procs/VSCB_MyInvoiceACC.aspx">View Uploaded Invoice</asp:LinkButton>
									</td>

								</tr>

                                 <tr style="padding-top: 1px; padding-bottom: 2px;">
									<td class="formtitle">
									<asp:LinkButton ID="Lnk_Createvendor" runat="server" Visible="false" ToolTip="Create Vendor" 
                                        PostBackUrl="~/procs/VSCB_VendorList.aspx">Create Vendor</asp:LinkButton>
								
									</td>
									<td class="formtitle">
										<asp:LinkButton ID="lnk_ApprovedPOWOList" runat="server" Visible="false" ToolTip="Delete PO/ WO List" PostBackUrl="~/procs/VSCB_ApprovedPOWOList.aspx">Delete PO/ WO List</asp:LinkButton>
									</td>
								</tr>

                                 <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle" runat="server"  id="TrACCPIADAMTCHANGE" visible="false">
                                        <asp:LinkButton ID="lnkbtn_ACCPIADAMTCHANGE"   runat="server" Visible="true" Text="" PostBackUrl="~/procs/VSCB_ApprovedPaymentRequestViewForChange.aspx">Amount Paid Account Change</asp:LinkButton>
                                    </td>
                                    <td class="formtitle" runat="server" id="TDid_ChangeInvoiceAmt" visible="false">
                                         <asp:LinkButton ID="lnkbtn_ChangeInvoiceTDSAmount" runat="server" Visible="true" Text="" PostBackUrl="~/procs/VSCB_approvedinvoiceChangeReq.aspx">Change Invoice TDS Amount</asp:LinkButton>

                                    </td>
                                </tr>

                                <tr style="padding-top: 1px; padding-bottom: 2px;">
									<td class="formtitle">
									<asp:LinkButton ID="Lnk_Invoice_Payment_Maker" runat="server" Visible="false" ToolTip="Create Invoice/Payment Request Creator" 
                                        PostBackUrl="~/procs/VSCB_InvoicePaymentMakerList.aspx">Create Invoice/Payment Request Creator</asp:LinkButton> 
									</td>
                                    <td class="formtitle">
                                            <asp:LinkButton ID="Lnk_POWO_Other_Approval" runat="server"  Visible="false" ToolTip="View PO/WO Approval Matrix" 
                                            PostBackUrl="~/procs/VSCB_POWO_OtherApprovalList.aspx">View PO/WO Approval Matrix </asp:LinkButton>
                                    </td>
                              </tr>

                                <tr style="padding-top: 1px;padding-bottom: 2px;" id="TrProduct" runat="server" visible="false">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnkBtnCreateproduct" runat="server" Visible="false" ToolTip="Create Product"
                                            PostBackUrl="~/procs/VSCB_ProductList.aspx">Create Product</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                    </td>
                                </tr>
                                 


								<%--Approval  PO/ WO  User Acc--%>
								<tr style="padding-top: 1px; padding-bottom: 2px;" id="idTRApproverHead" runat="server" visible="false">
									<td class="formtitle">
										<br />
										<span id="span_App_head" runat="server" style="font-size: 12pt; font-weight: bold; color: #3D1956;">Approvers : </span>
									</td>
								</tr>
								<tr id="idTRApproverHead_Line" runat="server" visible="false">
									<td colspan="2">
										<hr runat="server" id="hr_App_head" visible="true" />
									</td>
								</tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;" id="idTRPOWOApprover_inbox" runat="server" visible="false">
									<td class="formtitle" runat="server" id="Td16">
										<asp:LinkButton ID="lnk_POWOInbox" runat="server" Text="" Visible="true" PostBackUrl="~/procs/VSCB_InboxPOWO.aspx">Inbox PO/ WO</asp:LinkButton>
									</td>
									<td class="formtitle">
										<asp:LinkButton ID="lnk_ApprovedPOWO_Inbox" runat="server" Text="" Visible="true" PostBackUrl="~/procs/VSCB_MyapprovedPOWO.aspx">Approved PO/ WO List</asp:LinkButton>
									</td>
								</tr>

								<tr style="padding-top: 1px; padding-bottom: 2px;" id="idTRInvoiceApprover_inbox" runat="server" visible="false">
									<td class="formtitle" runat="server" id="Td2">
										<asp:LinkButton ID="lnk_summary_report" runat="server" Text="" Visible="true" PostBackUrl="~/procs/VSCB_Inboxinvoice.aspx">Inbox Invoices</asp:LinkButton>
									</td>
									<td class="formtitle">
										<asp:LinkButton ID="lnk_Approved_Invoice" runat="server" Text="" Visible="true" PostBackUrl="~/procs/VSCB_Myapprovedinvoice.aspx">Approved Invoice List</asp:LinkButton>
									</td>
								</tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;" id="Tr1" runat="server">
									<td class="formtitle" runat="server" id="Td27">
										<asp:LinkButton ID="Lnk_InboxInvoiceReverse" runat="server" Text="" Visible="false" PostBackUrl="~/procs/VSCB_MyApprove_InvoiceInbox_Reversal.aspx">Inbox Reversel Invoices</asp:LinkButton>
									</td>
									<td class="formtitle">
										<asp:LinkButton ID="Lnk_ApprovedInvoiceReverse" runat="server" Text="" Visible="false" PostBackUrl="~/procs/VSCB_Myapprovedinvoice_Reversal.aspx">Approved Invoice List - Reversal</asp:LinkButton>
									</td>
								</tr>

								<tr style="padding-top: 5px; padding-bottom: 5px;" id="idTRPaymentReqApprover_inbox" runat="server" visible="false">
									<td class="formtitle" runat="server" id="Td3">
										<asp:LinkButton ID="lnk_Inbox_Payment_Request" runat="server" Text="" Visible="true" PostBackUrl="~/procs/VSCB_InboxPaymentRequest.aspx">Inbox Payment Requests</asp:LinkButton>
									</td>
									<td class="formtitle" runat="server" id="Td8">
										<asp:LinkButton ID="lnk_Inbox_Payment_View" runat="server" Text="" Visible="true" PostBackUrl="~/procs/VSCB_InboxPaymentRequestView.aspx">Payment Approval List</asp:LinkButton>
									</td>
								</tr>

                                <tr style="padding-top: 5px; padding-bottom: 5px;" id="TrADPayment" runat="server" visible="false">
									<td class="formtitle" runat="server" id="Td34">
										<asp:LinkButton ID="lnk_Inbox_ADV_Payment_Request" runat="server" Text="" Visible="true" PostBackUrl="~/procs/VSCB_Inbox_ADV_Payment.aspx?Type=Pending">Inbox Advance Payment Requests</asp:LinkButton>
									</td>
									<td class="formtitle" runat="server" id="Td35">
										<asp:LinkButton ID="lnk_Inbox_ADV_Payment_View" runat="server" Text="" Visible="true" PostBackUrl="~/procs/VSCB_Inbox_ADV_Payment.aspx?Type=View">Payment Advance Approval List</asp:LinkButton>
									</td>
								</tr>

                                
								<%--Account  PO/ WO  User Acc--%>
								<tr style="padding-top: 1px; padding-bottom: 2px;" id="idTRAccountHead" runat="server" visible="false">
									<td class="formtitle">
										<br />
										<span id="span_Account_App" runat="server" style="font-size: 12pt; font-weight: bold; color: #3D1956;">Accounts : </span>
									</td>
								</tr>
								<tr id="idTRAccountHead_Line" runat="server" visible="false">
									<td colspan="2">
										<hr runat="server" id="hr_Acc_head" visible="true" />
									</td>
								</tr>

								<tr style="padding-top: 1px; padding-bottom: 2px;"  id="idTRPartialPay_Inbox" runat="server" visible="false">
									<td class="formtitle" runat="server" id="Report">
										<asp:LinkButton ID="lnk_CustomerFirstReport" runat="server" Text="" Visible="true" PostBackUrl="~/procs/VSCB_InboxPartialPaymentRequest.aspx">Partial Payment Requests</asp:LinkButton>
									</td>

									<td class="formtitle" runat="server" id="View">
										<asp:LinkButton ID="lnk_CustomerFirstView" runat="server" Visible="true" Text="" PostBackUrl="~/procs/VSCB_MyPartialPaymentRequest.aspx">My Partial Payment Requests</asp:LinkButton>
									</td>
								</tr>
								<tr style="padding-top: 1px; padding-bottom: 2px;" id="idTRAcc_Invoice_Inbox" runat="server" visible="false">
									<td class="formtitle" runat="server" id="Td4">
										<asp:LinkButton ID="lnk_Index_Acc_Invoices" runat="server" Text="" Visible="true" PostBackUrl="~/procs/VSCB_Inboxinvoice.aspx">Inbox Invoices</asp:LinkButton>
									</td>

									<td class="formtitle" runat="server" id="Td5">
										<asp:LinkButton ID="lnk_Index_Acc_Payment_Requests" runat="server" Visible="true" Text="" PostBackUrl="~/procs/VSCB_InboxPaymentRequest.aspx">Inbox Payment Requests</asp:LinkButton>
									</td>
								</tr>

								<tr style="padding-top: 1px; padding-bottom: 2px;" id="idTRBatch_Create" runat="server" visible="false">
									<td class="formtitle" runat="server" id="Td6">
										<asp:LinkButton ID="lnk_Index_Acc_Batch_Approval" runat="server" Text="" Visible="true" OnClick="lnk_Index_Acc_Batch_Approval_Click">Create Batch Request</asp:LinkButton>
									</td>
									<td class="formtitle" runat="server" id="Td10">
										<asp:LinkButton ID="lnk_Index_My_Batch_Requests" runat="server" Visible="true" Text="" PostBackUrl="~/procs/VSCB_MyBatch.aspx">My Batch Requests</asp:LinkButton>
									</td>
								</tr>

                                <tr style="padding-top: 1px; padding-bottom: 2px;" id="idTRAtachBatch_BankRef" runat="server" visible="false"><%--VSCB_AssignbankRefApproveBatch--%>
	                                <td class="formtitle" runat="server" id="Td17">
	                                  <asp:LinkButton ID="lnkAttachBatch_BankPaymentRef" runat="server" Text="" Visible="true" PostBackUrl="~/procs/VSCB_PendingBatchReqForAtchBankRef.aspx">Attach Bank Ref to Batch for Approval</asp:LinkButton>
	                                </td>
	                                <td class="formtitle" runat="server" id="Td18">
                                        <asp:LinkButton ID="lnkAttachBatch_BankPaymentApproved" runat="server" Text="" Visible="true" 
                                                          PostBackUrl="~/procs/VSCB_AssignBankRefApprovedbatch.aspx">Assign Payment Ref no to  Approved batch</asp:LinkButton>
                                        </td>
                                 </tr>


								<tr style="padding-top: 1px; padding-bottom: 2px;" id="idTRBatch_Req_Approver" runat="server" visible="false">
									<td class="formtitle" runat="server" id="Td7">
										<asp:LinkButton ID="lnk_Index_Acc_Batch_Requests" runat="server" Visible="true" Text="" PostBackUrl="~/procs/VSCB_InboxBatchReq.aspx">Inbox Batch Requests</asp:LinkButton>
									</td>

									<td class="formtitle" runat="server" id="Td11">
										<asp:LinkButton ID="lnk_Index_myapprovedBatchList" runat="server" Visible="true" Text="" PostBackUrl="~/procs/VSCB_Myapprovedbatch.aspx">Approved Batch Requests</asp:LinkButton>
									</td>

								</tr>

								<%--Report  Section--%>
								<tr style="padding-top: 1px; padding-bottom: 2px;"  id="idTR_Reports_1" runat="server" visible="false">
									<td class="formtitle">
										<br />
										<span id="span1" runat="server" style="font-size: 12pt; font-weight: bold; color: #3D1956;">Report : </span>
									</td>
								</tr>
								<tr id="idTR_Reports_4" runat="server" visible="false">
									<td colspan="2">
										<hr runat="server" id="hr1" visible="true" />
									</td>
								</tr>
								<tr style="padding-top: 1px; padding-bottom: 2px;"  id="idTR_Reports_2" runat="server" visible="false">
									<td class="formtitle" runat="server" id="Td12">
										<asp:LinkButton ID="lnk_audit_trail_Report" runat="server" Text="" ToolTip="Audit Trail Report" Visible="true" PostBackUrl="~/procs/VSCB_AuditTrailReport.aspx">Audit Trail Report</asp:LinkButton>
									</td>
									<td class="formtitle" runat="server" id="Td14">
                                        	<asp:LinkButton ID="lnk_Payment_Request_view" runat="server" ToolTip="Payment Requests View" Visible="true" Text="" PostBackUrl="~/procs/VSCB_InboxPaymentRequest_View.aspx">Payment Requests View</asp:LinkButton>
										    <asp:LinkButton ID="lnk_VendorwiseDuesReport" runat="server" ToolTip="Vendor PO/ WO Wise Dues Report" Visible="false" Text="" PostBackUrl="~/procs/VSCB_VendorWiseDuesReport.aspx">Vendor PO/ WO Wise Dues Report</asp:LinkButton>
									</td>

								</tr>
								<tr style="padding-top: 1px; padding-bottom: 2px;"  id="idTR_Reports_3" runat="server" visible="false">
									<td class="formtitle" runat="server" id="Td15">
										<asp:LinkButton ID="lnk_VendorWisePaymentHistory" runat="server" ToolTip="Vendor Wise Payment History Report" Visible="true" Text="" PostBackUrl="~/procs/VSCB_Rpt_VendorPaymenthistory.aspx">Vendor Wise Payment History Report</asp:LinkButton>								
									</td>
									<td class="formtitle" runat="server" id="Td13">
									
									</td>
								</tr>

                                <tr style="padding-top: 1px; padding-bottom: 2px;"  id="IdTR_Reports_5" runat="server" visible="false">
									<td class="formtitle" runat="server" id="Td25">
										<asp:LinkButton ID="lnk_ApprovalStatusreport" runat="server" ToolTip="Invoice Approval Status Report" Visible="true" Text="" PostBackUrl="~/procs/VSCB_Rpt_ApprovalStatusReport.aspx">Invoice Approval Status Report</asp:LinkButton>								
									</td>
									<td class="formtitle" runat="server" id="Td26">
										<asp:LinkButton ID="lnk_PaymentApprovalStatusreport" runat="server" ToolTip="Payment Approval Status Report" Visible="false" Text="" PostBackUrl="~/procs/VSCB_Rpt_paymentApprovalStatusReport.aspx">Payment Approval Status Report</asp:LinkButton>								
									
									</td>
								</tr>
                                 <tr style="padding-top: 1px; padding-bottom: 2px;"  id="Tr_POWO_Report" runat="server" visible="false">
									<td class="formtitle" runat="server" id="Td30">
										<asp:LinkButton ID="lnk_POWO_Approval_Status_Report" runat="server" ToolTip="PO/WO Approval Status Report" Visible="true" Text="" PostBackUrl="~/procs/VSCB_POApproval_Status.aspx">PO/WO Approval Status Report</asp:LinkButton>								
									</td>
									<td class="formtitle" runat="server" id="Td31">
												
									</td>
								</tr>

                                  <tr style="padding-top: 1px; padding-bottom: 2px;" >
                                    <td class="formtitle" id="trViewApprovedBatch" runat="server" visible="false">
                                        <asp:LinkButton ID="lnk_ViewAllDetails" runat="server" Text="View Approved Batch Details" Visible="true" ToolTip="View All Details" PostBackUrl="~/procs/VSCB_ViewAllDetails.aspx">View Approved Batch Details</asp:LinkButton>
                                    </td>

                                   <td class="formtitle" runat="server" id="Tr_Payment_Report" visible="false">
										<asp:LinkButton ID="lnk_Payment_Advice_list_Report" runat="server" ToolTip="Payment Advice List Report" Visible="true" Text="" PostBackUrl="~/procs/VSCB_PaymentAdvicelist.aspx">Payment Advice List Report</asp:LinkButton>																	
									</td>
                                </tr>
                                 
                              


							</table>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>

	<asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>

	<br />
	<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
	<asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />

	<asp:HiddenField ID="hflEmpCode" runat="server" />

	<asp:HiddenField ID="hflEmpDesignation" runat="server" />

	<asp:HiddenField ID="hflEmpDepartment" runat="server" />

	<asp:HiddenField ID="hflEmailAddress" runat="server" />

	<asp:HiddenField ID="hflGrade" runat="server" />


	<asp:HiddenField ID="hflapprcode" runat="server" />
	<asp:HiddenField ID="hdnClaimDate" runat="server" />


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

	</script>
</asp:Content>


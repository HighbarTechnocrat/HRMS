<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" MaintainScrollPositionOnPostback="true"
	AutoEventWireup="true" CodeFile="VSCB_VendorWiseDuesReport.aspx.cs" Inherits="procs_VendorWiseDuesReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
      <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
   <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <%--<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leaveteamrpt.css" type="text/css" media="all" />--%>
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
             /*background: #dae1ed;*/
           background:#ebebe4;
        }

        #MainContent_lstApprover {
            overflow: hidden !important;
        }
		        input.select2-search__field {
            padding-left: 0px !important;
            height: 0px !important;
            border:0px !important;
        }

        li.select2-search--inline {
            float: left !important;
            width: 0px !important;
            height: 0px !important;
            border: 0px !important;
        }

        select2-search--dropdown {
            float: left !important;
            width: 0px !important;
            height: 0px !important;
            border: 0px !important;
        }

        select2-search--dropdown select2-search__field {
            padding-left: 0px !important;
            height: 20px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
	 <script src="../js/dist/jquery-3.2.1.min.js"></script>
	<script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
	<script type="text/javascript">
		var deprt;
		$(document).ready(function () {

		});
	</script>
	<div class="commpagesdiv">
		<div class="commonpages">
			<div class="wishlistpagediv">
				<div class="userposts">
					<span>
						<asp:Label ID="lblheading" runat="server" Text="Vendor PO/ WO Wise Dues Report"></asp:Label>
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
						
						<li style="padding-top: 0px" class="trvl_date">
							<span>PO/ WO No.</span>&nbsp;&nbsp;                       
							<asp:ListBox runat="server"  ID="lstPOWONo" CssClass="DropdownListSearch" SelectionMode="multiple" Width="250px"></asp:ListBox>
                               
						</li>
						<li style="padding-top:0px" class="trvl_date">
							<span>PO/ WO Date</span>&nbsp;&nbsp;
							<asp:ListBox runat="server"  ID="lstPOWODate" CssClass="DropdownListSearch" SelectionMode="multiple" Width="250px"></asp:ListBox>
                               
						</li>
						<li style="padding-top: 5px" class="trvl_date">
							<span>Vendor Name</span>&nbsp;&nbsp;                          		
							<asp:ListBox runat="server"  ID="lstVendorName" CssClass="DropdownListSearch" SelectionMode="multiple" Width="250px"></asp:ListBox>
						</li>
						<li style="padding-top: 5px" class="trvl_date">
							<span>Invoice No</span>&nbsp;&nbsp;
							<asp:ListBox runat="server"  ID="lstInvoiceNo" CssClass="DropdownListSearch" SelectionMode="multiple" Width="250px"></asp:ListBox>
						</li>
						
<%--						<li style="padding-top: 5px">
							<span>Department</span>&nbsp;&nbsp;
							<asp:ListBox runat="server"  ID="lstDepartment" CssClass="DropdownListSearch" SelectionMode="multiple"></asp:ListBox>     
						</li>--%>
						
						<li style="padding-top: 5px" class="trvl_date">
							<span>Cost Center</span>&nbsp;&nbsp;
							<asp:ListBox runat="server"  ID="lstCostCentre" CssClass="DropdownListSearch" SelectionMode="multiple" Width="250px"></asp:ListBox>     
						</li>
						<li style="padding-top: 5px" class="trvl_date">
							<span>Status</span>&nbsp;&nbsp;
							<asp:ListBox runat="server"  ID="lstStatus" CssClass="DropdownListSearch" SelectionMode="multiple" Width="250px"></asp:ListBox>
                               
						</li>
						
						<li style="padding-top: 10px" class="trvl_date">
                            <span>Invoice From Date</span>&nbsp;&nbsp; <br />                             
                            <asp:TextBox ID="txtPaymntReqFrmDt" runat="server" CssClass="txtcls OfferDates" AutoPostBack="true" AutoComplete="off" style="height:25px;" OnTextChanged="txtPaymntReqFrmDt_TextChanged" ></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" Format="dd/MM/yyyy" TargetControlID="txtPaymntReqFrmDt" OnClientDateSelectionChanged="checkOffeDate"
                                        runat="server">
                                    </ajaxToolkit:CalendarExtender>
                        </li>
						
                        <li style="padding-top: 10px" class="trvl_date">
                              <span>Invoice To Date</span>&nbsp;&nbsp;<br />                              
                             <asp:TextBox ID="txtPaymntReqToDt" runat="server"  CssClass="txtcls OfferDates"  AutoPostBack="true" AutoComplete="off" style="height:25px;" OnTextChanged="txtPaymntReqToDt_TextChanged"></asp:TextBox>
                              <ajaxToolkit:CalendarExtender ID="CalendarExtender4" Format="dd/MM/yyyy" TargetControlID="txtPaymntReqToDt" runat ="server"  OnClientDateSelectionChanged="checkOffeDate">
                            </ajaxToolkit:CalendarExtender>
                        </li>
						<li style="padding-top: 5px" runat="server" id="PAYMENTNO" visible="false">
							<span>Payment Request No.</span>&nbsp;&nbsp;
							<asp:ListBox runat="server"  ID="lstPaymentRequestNo" CssClass="DropdownListSearch" SelectionMode="multiple"></asp:ListBox>                     
						</li>
						<li style="padding-top: 5px" runat="server" id="PAYMENTDATE"  visible="false">
							<span>Payment Request Date</span>&nbsp;&nbsp;	
							<asp:ListBox runat="server"  ID="lstpaymentRequestDate" CssClass="DropdownListSearch" SelectionMode="multiple"></asp:ListBox>                          
						</li>
						
					</ul>
				</div>
				<div class="trvl_Savebtndiv" style="margin-bottom: 10px !important; text-align: center">
					<asp:LinkButton ID="trvl_btnSave" runat="server" Text="Search" ToolTip="Search" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click">Search </asp:LinkButton>
					<asp:LinkButton ID="btnback_mng" runat="server" Text="Clear Search" OnClick="mobile_btnBack_Click" ToolTip="Clear Search" class="trvl_Savebtndiv">Clear Search</asp:LinkButton>
				</div>	
				<br />
    <div style="width:100%;overflow:auto">
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="700px"
            Width="100%" ShowBackButton="False" SizeToReportContent="false"
            ShowCredentialPrompts="False" ShowDocumentMapButton="False"
            ShowPageNavigationControls="true" ShowFindControls="false" ShowExportControls="true" ShowRefreshButton="False" PageCountMode="Actual" >
    </rsweb:ReportViewer>
        </div>
         
   
    <br />
				<asp:HiddenField ID="hdnInboxType" runat="server" />
				<asp:HiddenField ID="hdnEmpCode" runat="server" />
				<asp:HiddenField ID="hdnRecruitment_ReqID" runat="server" />
				<asp:HiddenField ID="FilePath" runat="server" />
				<asp:HiddenField ID="hdnPOID" runat="server" />
				<br />
				<br />
			</div>
		</div>
	</div>


	<script src="../js/dist/select2.min.js"></script>
	<link href="../js/dist/select2.min.css" rel="stylesheet" />

	<script type="text/javascript">      
		$(document).ready(function () {
			$(".DropdownListSearch").select2();
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
		function checkOffeDate(sender, args)
		{
			if (sender._selectedDate >= new Date()) {
				alert("You can not select a future date than today!");
				sender._selectedDate = new Date();
				sender._textbox.set_Value(sender._selectedDate.format(sender._format))
			}
		}
        $('.OfferDates').keydown(function (e) {  
			var k;			
			document.all ? k = e.keyCode : k = e.which;
			if (k == 8 ||k == 46)
				return false;
			else
				return true;
			
});

	</script>
</asp:Content>


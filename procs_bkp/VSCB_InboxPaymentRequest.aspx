<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
	CodeFile="VSCB_InboxPaymentRequest.aspx.cs" Inherits="procs_VSCB_InboxPaymentRequest" %>

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
			background: #dae1ed;
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
						<asp:Label ID="lblheading" runat="server" Text="Inbox Payment Requests"></asp:Label>
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
							<span>PO/ WO Date</span>&nbsp;&nbsp;
                             <br />
							<asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstPOWODate">
							</asp:DropDownList>
						</li>
						<li style="padding-top: 30px">
							<span>Vendor Name</span>&nbsp;&nbsp;
                             <br />
							<asp:DropDownList runat="server" ID="lstVendorName" CssClass="DropdownListSearch" Width="98%">						
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
							<asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstCostCentre">
							</asp:DropDownList>

							<span style="display:none">Project/Department</span> 
							<asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstDepartment" Visible="false">
							</asp:DropDownList>
						</li>
						<li style="padding-top: 15px">
						 <span>Status</span>&nbsp;&nbsp;
                             <br />
							<asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstStatus">
							</asp:DropDownList>
						</li>
						
						<li style="padding-top: 15px">
							 <span>Payment Request Amt</span>&nbsp;&nbsp;
                             <br />
							<asp:TextBox runat="server" id="txtPaymentRequestamt" AutoComplete="off" CssClass="number"	></asp:TextBox>
						</li>
						 
					</ul>
				</div>
				<div class="mobile_Savebtndiv" style="margin-top:10px !important">
					<asp:LinkButton ID="mobile_btnSave" runat="server" Text="Submit" ToolTip="Search" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click">Search </asp:LinkButton>
					<asp:LinkButton ID="mobile_btnBack" runat="server" Text="Clear Search" OnClick="mobile_btnBack_Click" ToolTip="Clear Search" class="trvl_Savebtndiv">Clear Search</asp:LinkButton>
				</div>
				<div class="manage_grid" >
					<asp:Label runat="server" ID="RecordCount" Style="color:red;font-size:14px;"></asp:Label>
					<center>
                          <asp:GridView ID="gvMngTravelRqstList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                            DataKeyNames="Payment_ID,POID,IsInvoiceWithPO"   CellPadding="3" AutoGenerateColumns="False" Width="130%"   EditRowStyle-Wrap="false" PageSize="12" AllowPaging="True" OnPageIndexChanging="gvMngTravelRqstList_PageIndexChanging"   >
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
							<asp:TemplateField HeaderText="View" >
                                <ItemTemplate>
                                 <asp:ImageButton id="lnkView" runat="server" ToolTip="View" Width="15px" Height="15px"  ImageUrl="~/Images/edit.png" OnClick="lnkView_Click"/>                               
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>


                              <asp:BoundField HeaderText="Payment request Created By"
                                        DataField="PayementRequestCreatedBy"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-BorderColor="Navy" ItemStyle-Width="13%" /> 

							<asp:BoundField HeaderText="Payment Status"
                                DataField="PyamentStatus"   
								ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="5%" />

							 <asp:BoundField HeaderText="Vendor Name"
                                DataField="Name"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="18%" />
							<asp:BoundField HeaderText="Payment Request No."
                                DataField="PaymentReqNo"   
								ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="18%" />						

							<asp:BoundField HeaderText="Payment Request Amt"
                                DataField="TobePaidAmtWithtax"   
								ItemStyle-HorizontalAlign="Right"
                                HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-Width="6%" />
							<asp:BoundField HeaderText="Payment Request Date"
                                DataField="PaymentReqDate"   
								ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="5%" />

							<asp:BoundField HeaderText="Invoice No"
                                DataField="InvoiceNo"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="9%" />

							 
							<asp:BoundField HeaderText="PO/ WO Number"
                                DataField="PONumber"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="15%" />

							<asp:BoundField HeaderText="PO/ WO Date"
                                DataField="PODate"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="8%" />
							<asp:BoundField HeaderText="Project/Department"
                                DataField="Department"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="8%" Visible="false"/>

							<asp:BoundField HeaderText="Cost Center"
                                DataField="CostCentre"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="18%" />

                            <asp:BoundField HeaderText="Approved on"
                                DataField="approved_on"   
								ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="5%" />

                            <asp:BoundField HeaderText=" Approval Status"
                                DataField="Request_status"  
								 ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="5%" />  
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

	</script>
</asp:Content>




<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
	CodeFile="CustEscalationView.aspx.cs" Inherits="procs_CustEscalationView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />
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

		#MainContent_lstApprover {
			overflow: hidden !important;
		}

		#MainContent_service_btnAssgine,
		#MainContent_service_btnClose,
		#MainContent_service_btnEscelateToHOD,
		#MainContent_service_btnEscelateToCEO,
		#MainContent_service_btnClearText,
		#MainContent_service_btnSendSPOC,
		#MainContent_mobile_btnCorrection,
		#MainContent_trvldeatils_delete_btn,
		#MainContent_btnbackA ,
		#MainContent_service_btnCreateAction,
		#MainContent_Customer_Reject,
		#MainContent_Service_Approval{
			background: #3D1956;
			color: #febf39 !important;
			padding: 0.5% 1.4%;
			margin: 0% 0% 0 0;
		}

		.noresize {
			resize: none;
		}

		.ViewFiles {
			color: red;
			background-color: transparent;
			text-decoration: none !important;
		}

			.ViewFiles:hover {
				color: dodgerblue;
				background-color: transparent;
				text-decoration: none !important;
			}

		.TLQuestio {
			padding: 10px 30px 0px 30px;
		}

		.Actiontd {
			float: left;
			padding-left: 10px;
		}

		#MainContent_ModalPopupExtenderIRSheet_foregroundElement, #MainContent_ModalPopupExtenderIRSheet_foregroundElement 
		,#MainContent_modalChangeOwner_foregroundElement
			
		{
			position: fixed !important;
			z-index: 10001 !important;
			top: 10% !important;
			width: 60% !important;
		}

		.IRmodalPopup {
			background-color: #FFFFFF;
			border: 1px solid #000 !important;
			padding-top: 10px;
			padding-left: 10px;
			padding-bottom: 20px;
			height: 250% !important;
			
		}
		

		.IRSheetBtn {
			text-align: right;
			margin-right: 30px;
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
						<asp:Label ID="lblheading" runat="server" Text="Customer Incident View "></asp:Label>
					</span>
				</div>
				<div>
					<asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
				</div>
				<span runat="server" id="backToSPOC" visible="false">
					<a href="InboxCustEscalation.aspx" class="aaaa">Back</a>
				</span>
				<span runat="server" id="backToEmployee" visible="false">
					<a href="MyCustEscalation_Req.aspx" class="aaaa">Back</a>
				</span>
				<span runat="server" id="backToArr" visible="true">
					<a href="CustEscalationView_Index.aspx" class="aaaa">Back</a>
				</span>
				<span>
					<a href="CustEscalation.aspx" style="margin-right: 18px;" class="aaaa">Cust Incident Home</a>&nbsp;&nbsp; 
				</span>
				<div class="edit-contact">
					<div class="editprofile" style="text-align: center; border: none;" id="divmsg" runat="server" visible="false">
					</div>
					<div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
						<div class="cancelbtndiv">
							<asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
						</div>
						<div class="cancelbtndiv">
							<asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server"></asp:LinkButton>
						</div>
					</div>


					<ul id="editform" runat="server" visible="false">

						<li class="mobile_inboxEmpCode">
							<span><b>Customer Incident Details</b></span><br />

							<asp:TextBox AutoComplete="off" ID="Txt_" runat="server" MaxLength="50" Visible="false" Enabled="false"> </asp:TextBox>

						</li>
						<li class="mobile_InboxEmpName" style="visibility: hidden">
							<span>Employee Name</span><br />

							<asp:TextBox AutoComplete="off" ID="TextBox3" runat="server" Visible="True" Enabled="false"></asp:TextBox>

						</li>
						<li class="mobile_InboxEmpName">
							<span>Created By</span><br />

							<asp:TextBox AutoComplete="off" ID="txtEmpName" runat="server" Visible="True" Enabled="false"></asp:TextBox>

						</li>
						<li class="mobile_InboxEmpName">
							<span>Department Name </span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_DeptName" Enabled="false" runat="server"></asp:TextBox>
						</li>
						<li class="mobile_inboxEmpCode">
							<span>Employee Code</span><br />

							<asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="True" Enabled="false"> </asp:TextBox>

						</li>
						<li class="mobile_inboxEmpCode">
							<span>Designation  </span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_Designation" Enabled="false" runat="server" AutoPostBack="true"></asp:TextBox>
						</li>
						<li class="mobile_inboxEmpCode">
							<span>Employee Email</span><br />

							<asp:TextBox AutoComplete="off" ID="Txt_EmpEmail" runat="server" MaxLength="50" Visible="True" Enabled="false"> </asp:TextBox>

						</li>
						<li class="mobile_inboxEmpCode">
							<span>Employee Mobile</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_EmpMobile" Enabled="false" runat="server" AutoPostBack="true"></asp:TextBox>
						</li>
						<li>
							<span>Customer Incident For Project Name</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtSelectedDepartment" runat="server" MaxLength="100" Enabled="false"></asp:TextBox>
						</li>
						<li class="mobile_inboxEmpCode">
							<span>Created Date</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtFirstCreatedDate" runat="server" MaxLength="100" Enabled="false"></asp:TextBox>
						</li>
						<li class="mobile_inboxEmpCode">
							<span>Escalation Raised By </span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtEscalationBy" runat="server" Enabled="false"></asp:TextBox>
						</li>
						<li class="mobile_inboxEmpCode">
							<span>Email ID </span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtEmailID" runat="server" Enabled="false"></asp:TextBox>
						</li>
						<li class="mobile_inboxEmpCode">
							<span>Mode </span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txt_category" runat="server" Enabled="false"></asp:TextBox>
						</li>
						<li class="mobile_inboxEmpCode">
							<span>Role of the person </span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtRolePerson" runat="server" Enabled="false"></asp:TextBox>
						</li>
						<li class="mobile_inboxEmpCode">
							<span>Customer Satisfaction Index </span>
							<br />
							<asp:TextBox AutoComplete="off" ID="TXTCustomerSatisfaction" runat="server" Enabled="false"></asp:TextBox>
						</li>
						<li class="mobile_inboxEmpCode">
							<span>Severity </span>
							<br />
							<asp:TextBox AutoComplete="off" ID="TXTSeverity" runat="server" Enabled="false"></asp:TextBox>
						</li>
						<li class="mobile_inboxEmpCode">
							<span>Impact on Project </span>
							<br />
							<asp:TextBox AutoComplete="off" ID="TXTImpactProject" runat="server" Enabled="false"></asp:TextBox>
						</li>
						<li class="mobile_inboxEmpCode">
							<span>Incident Number </span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtIncidentNumber" runat="server" Enabled="false"></asp:TextBox>
						</li>
						<li class="mobile_inboxEmpCode">
							<span>Incident Description</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtFirstDeacription" runat="server" TextMode="MultiLine" Rows="6" Width="188%" CssClass="noresize" Enabled="false"></asp:TextBox>
						</li>
						<li style="visibility: hidden"></li>
						<li class="mobile_grid">
							<span>Uploaded File</span>
							<br />
							<a href="#" onclick="return DownloadFileEmployee()"><span runat="server" id="lblCreateFile" class="ViewFiles"></span></a>
						</li>
						<li style="visibility: hidden"></li>
						<hr runat="server" id="hr4" />
						<li class="mobile_date" runat="server" id="empShow1">
							<span><b runat="server" id="lblAssgineTitle"> Edit Delivery Date </b></span>
							<br />
							
							<asp:TextBox AutoComplete="off" ID="txtFromdate" runat="server" AutoPostBack="True" Visible="false" Enabled="false"></asp:TextBox>
							<%--<ajaxToolkit:CalendarExtender ID="CalendarExtender2"  OnClientHidden="onCalendarHidden"  OnClientShown="onCalendarShown" TargetControlID="txtFromdate"
                             Format="MMM/yyyy"    runat="server" BehaviorID="calendar1">
                            </ajaxToolkit:CalendarExtender>--%>
						</li>

						<li class="mobile_date" style="visibility: hidden" runat="server" id="EmpShow13" ></li>


						<li class="claimmob_fromdate" runat="server" id="empShow">
							<br />
							<span>Assigned To Project </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<%--<asp:TextBox AutoComplete="off" ID="txtFromdate_N" runat="server" Enabled="false" AutoPostBack="True"></asp:TextBox>--%>
							<asp:DropDownList ID="ddlDepartment" runat="server" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
							</asp:DropDownList>
							<%--   <ajaxToolkit:CalendarExtender ID="CalendarExtender2"  TargetControlID="txtFromdate_N"
                             Format="dd/MM/yyyy" runat="server">
                            </ajaxToolkit:CalendarExtender>--%>
						</li>
						<li class="claimmob_fromdate" runat="server" style="visibility: hidden" id="empShow12">
							<span>Assigned To Category </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />

							<asp:DropDownList ID="ddlCategory" Enabled="false" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
							</asp:DropDownList>
							<%--<ajaxToolkit:CalendarExtender ID="CalendarExtender3"  TargetControlID="txtTodate_N"
                             Format="dd/MM/yyyy" runat="server">
                            </ajaxToolkit:CalendarExtender>--%>
						</li>

						<li class="mobile_Amount" runat="server" id="empShow2">
							<span>Assigned To Employee </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<%--<asp:TextBox AutoComplete="off" ID="TXT_AssginmentEMP" runat="server" Enabled="false" AutoPostBack="True"></asp:TextBox>--%>
							<asp:DropDownList ID="ddl_AssginmentEMP" runat="server" AutoPostBack="true" />


						</li>
						<li class="claimmob_fromdate" runat="server" id="empShow3">
							<br />
							<span runat="server" id="lblActionAssginDate">Date</span>&nbsp;&nbsp;<br />

							<asp:TextBox AutoComplete="off" ID="TXT_AssginmentDate" Enabled="false" runat="server"></asp:TextBox>
						</li>

						<li class="claimmob_fromdate" runat="server" id="ResponseTime">
							<span runat="server" id="Responselabel">Response  Date </span> &nbsp;&nbsp;<span style="color: red"></span> 
							<br />
							<asp:TextBox AutoComplete="off" ID="txtResponceDate" runat="server" class="OfferDates"></asp:TextBox>
							<ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtResponceDate"
								runat="server">
							</ajaxToolkit:CalendarExtender>
						</li>
						<li class="claimmob_fromdate" runat="server" id="Delivery">
							<span runat="server" id="Deleverydate1">Delivery Date &nbsp;&nbsp;<span style="color: red">*</span> </span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtDeliveryDate" runat="server" AutoPostBack="true" class="OfferDates" OnTextChanged="txtDeliveryDate_TextChanged" ></asp:TextBox>
							<ajaxToolkit:CalendarExtender ID="CalendarExtender3" Format="dd/MM/yyyy" TargetControlID="txtDeliveryDate"
								runat="server" OnClientDateSelectionChanged="checkDeliveryDate">
							</ajaxToolkit:CalendarExtender>
						</li>
						<li runat="server" id="divsh"></li>
						<li class="mobile_date" runat="server" id="divsh2" style="visibility: hidden"></li>
						<li class="claimmob_Amount" runat="server" id="empShow4">
							<span runat="server" id="lblActionAssginComment">Closure Confirmation Description </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txt_SPOCComment" Enabled="true" runat="server" TextMode="MultiLine" MaxLength="500" Rows="6" Width="188%" CssClass="noresize" onKeyUp="javascript:Count1(this);"></asp:TextBox>

						</li>
						<li class="mobile_Amount" style="visibility: hidden" runat="server" id="empShow14"></li>
						<li class="mobile_inboxEmpCode" runat="server" id="empShow5">
							<%-- <span>Reason: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtReason" runat="server" MaxLength="100" ></asp:TextBox>--%>
							<span>Upload File</span>&nbsp;&nbsp;<%--<span style="color:red">*</span>--%><br />
							<asp:FileUpload AutoComplete="off" ID="uploadfile" runat="server" />
							<asp:LinkButton ID="lnkuplodedfile" OnClick="lnkuplodedfile_Click" runat="server" CssClass="ViewFiles"></asp:LinkButton>
						</li>


						<li class="mobile_inboxEmpCode" runat="server" style="visibility: hidden" id="empShow8"></li>
						
						<li class="claimmob_fromdate"></li>


						<li class="mobile_inboxEmpCode" style="width: 100%" runat="server" id="empShow6">
							<asp:LinkButton ID="service_btnAssgine" Visible="false" runat="server" Text="Assigne Incident" ToolTip="Assigne Incident" CssClass="Savebtnsve" OnClientClick="return SendForAssigne();" OnClick="service_btnAssgine_Click">Assigne Incident</asp:LinkButton>
							<asp:LinkButton ID="service_btnCreateAction"  runat="server" Text="Create Action" ToolTip="Create Action" CssClass="Savebtnsve CAction" Style="display:none"   >Create Action</asp:LinkButton>
							<asp:LinkButton ID="service_btnClose" Visible="false" runat="server" Text="Close Incident" ToolTip="Close Incident" CssClass="Savebtnsve" OnClick="service_btnClose_Click" OnClientClick="return SendForClose();">Close Incident</asp:LinkButton>
							<asp:LinkButton ID="service_btnSendSPOC" Visible="false" runat="server" Text="Send Back Coordinator" ToolTip="Send Back To Coordinator" CssClass="Savebtnsve" OnClick="service_btnSendSPOC_Click" OnClientClick="return SendForSPOC();">Send Back To Coordinator</asp:LinkButton>
							<asp:LinkButton ID="mobile_btnPrintPV" runat="server" Text="Update " Visible="false" ToolTip="Update " CssClass="Savebtnsve" OnClick="mobile_btnPrintPV_Click" OnClientClick="SendForResponse()">Update </asp:LinkButton>
							<asp:LinkButton ID="Service_Approval" runat="server" Text="Change Incident Owner" ToolTip="Change Incident Owner" Visible="false" CssClass="Savebtnsve" >Change Incident Owner</asp:LinkButton>
							<asp:LinkButton ID="Customer_Reject" runat="server" Text="Reject" Visible="false" ToolTip="Reject" CssClass="Savebtnsve" OnClick="Customer_Reject_Click" OnClientClick="SendForReject()">Reject</asp:LinkButton>
							<asp:LinkButton ID="service_btnClearText1" runat="server" Text="Back" Visible="false" ToolTip="Back" CssClass="Savebtnsve" OnClick="service_btnClearText_Click">Back</asp:LinkButton>
							

							<%--<asp:LinkButton ID="mobile_btnPrintPV" runat="server" Text="Print Payment Voucher" ToolTip="Print Payment Voucher" CssClass="Savebtnsve" OnClick="mobile_btnPrintPV_Click">Print Payment Voucher</asp:LinkButton>--%>

						</li>
						<hr runat="server" id="empShow7" />
						<li runat="server" id="liAction">
							<span><b>Action Details</b> </span>
						</li>
						<li style="visibility: hidden" runat="server" id="liAction3" ></li>
						<li style="width: 100%;" runat="server" id="liAction2">

							<asp:GridView ID="GridActionDeatils" runat="server" BackColor="White" DataKeyNames="Cust_ActionID" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%">
								<FooterStyle BackColor="White" ForeColor="#000066" />
								<HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" HorizontalAlign="Center" />
								<PagerStyle ForeColor="#000066" HorizontalAlign="left" BackColor="White" />
								<RowStyle ForeColor="#000066" />
								<SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
								<SortedAscendingCellStyle BackColor="#F1F1F1" />
								<SortedAscendingHeaderStyle BackColor="#007DBB" />
								<SortedDescendingCellStyle BackColor="#CAC9C9" />
								<SortedDescendingHeaderStyle BackColor="#00547E" />

								<Columns>
									<asp:BoundField HeaderText="Created By"
										DataField="ActionBy"
										ItemStyle-HorizontalAlign="left"
										ItemStyle-Width="15%"
										ItemStyle-BorderColor="Navy" />


									<asp:BoundField HeaderText="Action Date"
										DataField="ActionDate"
										ItemStyle-HorizontalAlign="left"
										ItemStyle-Width="8%"
										ItemStyle-BorderColor="Navy" />

									<asp:BoundField HeaderText="Action Title"
										DataField="ActionTitle"
										ItemStyle-HorizontalAlign="left"
										ItemStyle-Width="25%"
										ItemStyle-BorderColor="Navy" />


									<asp:BoundField HeaderText="Remark"
										DataField="Remarks"
										ItemStyle-HorizontalAlign="left"
										ItemStyle-Width="25%"
										ItemStyle-BorderColor="Navy" />

									<asp:BoundField HeaderText="Task Completed Date"
										DataField="TaskCompletedDate"
										ItemStyle-HorizontalAlign="Center"
										ItemStyle-Width="10%"
										ItemStyle-BorderColor="Navy" />

									<asp:BoundField HeaderText="Task Completed"
										DataField="TaskCompleted"
										ItemStyle-HorizontalAlign="Center"
										ItemStyle-Width="7%"
										ItemStyle-BorderColor="Navy" />
									<asp:TemplateField HeaderText="View" HeaderStyle-Width="5%">
										<ItemTemplate>
											<asp:ImageButton ID="lnkActionEdit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkActionEdit_Click" />
										</ItemTemplate>
										<ItemStyle HorizontalAlign="Center" />
									</asp:TemplateField>
								</Columns>
							</asp:GridView>
							<br />
						</li>

						<hr runat="server" id="empShow11" />
						<li>
							<span><b>Customer Incident History</b> </span>
						</li>
						<li style="visibility: hidden"></li>

						<li style="width: 100%;">
							<!--<span>Upload File</span>-->
							<span></span>
							<asp:ListBox Visible="false" ID="lstApprover" runat="server"></asp:ListBox>
							<asp:GridView ID="gvServiceHistory" runat="server" BackColor="White" DataKeyNames="Id" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%">
								<FooterStyle BackColor="White" ForeColor="#000066" />
								<HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" HorizontalAlign="Center" />
								<PagerStyle ForeColor="#000066" HorizontalAlign="left" BackColor="White" />
								<RowStyle ForeColor="#000066" />
								<SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
								<SortedAscendingCellStyle BackColor="#F1F1F1" />
								<SortedAscendingHeaderStyle BackColor="#007DBB" />
								<SortedDescendingCellStyle BackColor="#CAC9C9" />
								<SortedDescendingHeaderStyle BackColor="#00547E" />

								<Columns>
									<asp:BoundField HeaderText="Action By"
										DataField="ActionBy"
										ItemStyle-HorizontalAlign="left"
										ItemStyle-Width="20%"
										ItemStyle-BorderColor="Navy" />


									<asp:BoundField HeaderText="Received Date"
										DataField="ReceivedDate"
										ItemStyle-HorizontalAlign="left"
										ItemStyle-Width="20%"
										ItemStyle-BorderColor="Navy" />

									<asp:BoundField HeaderText="Action"
										DataField="StatusName"
										ItemStyle-HorizontalAlign="left"
										ItemStyle-Width="26%"
										ItemStyle-BorderColor="Navy" />


									<asp:BoundField HeaderText="Action Date"
										DataField="ActionDate"
										ItemStyle-HorizontalAlign="left"
										ItemStyle-Width="34%"
										ItemStyle-BorderColor="Navy" />
									<asp:TemplateField HeaderText="View" HeaderStyle-Width="5%">
										<ItemTemplate>
											<asp:ImageButton ID="lnkEdit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkEdit_Click" />
										</ItemTemplate>
										<ItemStyle HorizontalAlign="Center" />
									</asp:TemplateField>
								</Columns>
							</asp:GridView>
						</li>
						<li class="mobile_inboxEmpCode" style="visibility: hidden">
							<asp:TextBox AutoComplete="off" ID="TextBox2" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
						</li>
						<hr />
						<li class="mobile_Approver">
							<span><b>Customer Incident History Details</b> </span>
							<br />

						</li>
						<li style="visibility: hidden" class="mobile_inboxEmpCode"></li>
						<li style="visibility: hidden" class="mobile_inboxEmpCode"></li>
						<li class="mobile_inboxEmpCode">
							<span>Action Date</span><br />
							<asp:TextBox AutoComplete="off" ID="txt_AssgimentShowDate" runat="server" ReadOnly="true" Enabled="false"> </asp:TextBox>
						</li>
						<li class="mobile_inboxEmpCode">
							<span>Action By</span><br />
							<asp:TextBox AutoComplete="off" ID="txtActionBy" runat="server" ReadOnly="true" Enabled="false"> </asp:TextBox>
						</li>
						<li class="mobile_inboxEmpCode">
							<span>Action</span><br />
							<asp:TextBox AutoComplete="off" ID="txt_ASDate" runat="server" ReadOnly="true" Enabled="false"> </asp:TextBox>
						</li>
						<li class="mobile_inboxEmpCode">
							<span>Assgin To</span><br />
							<asp:TextBox AutoComplete="off" ID="txt_Assigne_By" runat="server" ReadOnly="true" Enabled="false"> </asp:TextBox>
						</li>
						<li class="mobile_inboxEmpCode" runat="server" id="Response1" visible="false">
							<span>Response Date</span><br />
							<asp:TextBox AutoComplete="off" ID="txtResponseDates" runat="server" Enabled="false"> </asp:TextBox>
						</li>
						<li class="mobile_inboxEmpCode" runat="server" id="Response2" visible="false">
							<span>Delivery Date </span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtDeliveryDates" runat="server" Enabled="false"> </asp:TextBox>
						</li>
						<li class="mobile_inboxEmpCode">
							<span>Incident Description </span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txt_Service_Description" runat="server" TextMode="MultiLine" Rows="6" Width="188%" CssClass="noresize" Enabled="false"></asp:TextBox>
						</li>
						<li style="visibility: hidden" class="mobile_inboxEmpCode"></li>
						<li class="mobile_grid">
							<span>Uploaded File</span>
							<br />
							<a href="#" onclick="return DownloadFile()"><span runat="server" id="bindFilePath" class="ViewFiles"></span></a>
						</li>
					</ul>
				</div>
			</div>
		</div>
	</div>
	<div class="mobile_Savebtndiv">
		<asp:Panel ID="PnlIrSheet" runat="server" CssClass="IRmodalPopup" Style="display: none" Height="400px">
			<div id="Div2" runat="server" style="max-height: 500px;">
				<div class="userposts">
					<span>
						<asp:Label ID="lblActions" runat="server" Text="Create Action Deatils"></asp:Label>
					</span>

				</div>
				<div style="text-align: left; padding-left: 10px;">
					<asp:Label runat="server" ID="lblActionMSG" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
				</div>
				<div style="text-align: right; padding-right: 20px;">
					<span><%--<a href="#" id="btBack" title="Back" class="aaaa" style="margin-right: 30px">Back</a>--%>
						<asp:LinkButton ID="btnbackA" runat="server" ToolTip="Back" CssClass="Savebtnsve" OnClientClick="ClearAction()" Style="display:none">Back</asp:LinkButton>

					</span>
				</div>
				<hr />
				<table class="TLQuestio">
					<tr>
						<td>
							<span runat="server" id="Span1" class="Actiontd">Action Date &nbsp;&nbsp;<span style="color: red">*</span></span>
							<asp:TextBox AutoComplete="off" ID="TxtActionDate" runat="server"  AutoPostBack="true" OnTextChanged="TxtActionDate_TextChanged"></asp:TextBox>
							<ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="TxtActionDate"
								runat="server">
							</ajaxToolkit:CalendarExtender>
						</td>
						<td></td>
					</tr>
					<tr>
						<td>
							<span runat="server" id="Span2" class="Actiontd">Action Title&nbsp;&nbsp;<span style="color: red">*</span></span>
							<asp:TextBox AutoComplete="off" ID="txtActionTitle" runat="server" Rows="4" TextMode="MultiLine" Width="80%" CssClass="noresize"  onKeyUp="javascript:Count(this);"></asp:TextBox>
						</td>
						<td>
							<span runat="server" id="Span3" class="Actiontd">Remarks&nbsp;&nbsp;<span style="color: red"></span></span>
							<asp:TextBox AutoComplete="off" ID="txtRemarks" runat="server" Rows="4" TextMode="MultiLine" Width="80%" CssClass="noresize"  onKeyUp="javascript:Count(this);"></asp:TextBox>
						</td>
					</tr>
					<tr>
						<td>
							<span runat="server" id="Span4" class="Actiontd">Task Completed (Yes/No)</span>&nbsp;&nbsp;<span style="color: red"></span>
							<asp:CheckBox ID="ckckTask" runat="server" CssClass="Actiontd" AutoPostBack="true" OnCheckedChanged="ckckTask_CheckedChanged" />

						</td>
						
							<td>
							<span runat="server" id="TaskDate1" class="Actiontd" visible="false">Task Completed Date &nbsp;&nbsp;<span style="color: red">*</span></span>
							<asp:TextBox AutoComplete="off" ID="txtTaskCompletedDate" AutoPostBack="true" runat="server" Visible="false" CssClass="OfferDates"  OnTextChanged="txtTaskCompletedDate_TextChanged"></asp:TextBox>
							<ajaxToolkit:CalendarExtender ID="CalendarExtender4" Format="dd/MM/yyyy" TargetControlID="txtTaskCompletedDate" OnClientDateSelectionChanged="checkTaskDate"
								runat="server">
							</ajaxToolkit:CalendarExtender>
						
						</td>	
					</tr>
				</table>
				<hr />
				<div>
					<asp:LinkButton ID="mobile_btnSave" runat="server" Text="Submit" ToolTip="Submit" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click" OnClientClick="return ActionMultiClick();" Style="font-size:14px !important">Submit</asp:LinkButton>
				</div>
				<br />
				<div class="IRSheetBtn">
					<asp:LinkButton ID="trvldeatils_delete_btn" runat="server" ToolTip="Back" CssClass="Savebtnsve" OnClick="trvldeatils_delete_btn_Click" Style="font-size:14px !important">Back</asp:LinkButton>
				</div>
			</div>
		</asp:Panel>
		<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderIRSheet" runat="server"
			TargetControlID="service_btnCreateAction" PopupControlID="PnlIrSheet" RepositionMode="RepositionOnWindowResizeAndScroll"
			BackgroundCssClass="modalBackground" DropShadow="true" OkControlID="Oth_btnDelete1"
			OnOkScript="ok()" CancelControlID="btnbackA" />

		<asp:Panel ID="pnlIncidentOwner" runat="server" CssClass="IRmodalPopup" Style="display: none" Height="400px">
			<div id="Div1" runat="server" style="max-height: 500px;">
				<div class="userposts">
					<span>
						<asp:Label ID="Label1" runat="server" Text="Change Incident Owner"></asp:Label>
					</span>

				</div>
				<div style="text-align: left; padding-left: 10px;">
					<asp:Label runat="server" ID="lblChangeOwner" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
				</div>
				<div style="text-align: right; padding-right: 20px;">
					<span><%--<a href="#" id="btBack" title="Back" class="aaaa" style="margin-right: 30px">Back</a>--%>
						<asp:LinkButton ID="LinkButton1" runat="server" ToolTip="Back" CssClass="Savebtnsve" OnClientClick="ClearAction()" Style="display:none">Back</asp:LinkButton>

					</span>
				</div>
				<hr />
				<table class="TLQuestio">
					
					<tr>
						<td>
							<span runat="server" id="Span6" class="Actiontd">Current Incident Owner&nbsp;&nbsp;<span style="color: red">*</span></span>
							<asp:DropDownList ID="ddlCurrentOwner" class="select-dropdown1" runat="server" Enabled="false">
							</asp:DropDownList></td>
						<td>
							<span runat="server" id="Span7" class="Actiontd">New Incident Owner &nbsp;&nbsp;<span style="color: red">*</span></span>
							<asp:DropDownList ID="ddlNewOwner" class="select-dropdown1" runat="server">
							</asp:DropDownList>
							
						</td>
						
					</tr>
								
				</table>
				<br />
							
				<hr />
				<div>
					<asp:LinkButton ID="service_btnEscelateToHOD" Visible="true" runat="server" Text="Submit" ToolTip="Submit" CssClass="Savebtnsve" OnClick="service_btnEscelateToHOD_Click" OnClientClick="return SendForHOD();" Style="font-size:14px !important">Submit</asp:LinkButton>			
				</div>
				<br />
				<div class="IRSheetBtn">
					<asp:LinkButton ID="service_btnClearText" runat="server" ToolTip="Back" CssClass="Savebtnsve" OnClick="service_btnClearText_Click1" Style="font-size:14px !important">Back</asp:LinkButton>
				</div>
			</div>
		</asp:Panel>
		<ajaxToolkit:ModalPopupExtender ID="modalChangeOwner" runat="server"
			TargetControlID="Service_Approval" PopupControlID="pnlIncidentOwner" RepositionMode="RepositionOnWindowResizeAndScroll"
			BackgroundCssClass="modalBackground" DropShadow="true" OkControlID="Oth_btnDelete1"
			OnOkScript="ok()" CancelControlID="btnbackA" />

		<%-- Following Popup for Approve Mobile Rem Request 
        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" DisplayModalPopupID="mpe" TargetControlID="mobile_btnSave">
        </ajaxToolkit:ConfirmButtonExtender>
        <ajaxToolkit:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="mobile_btnSave" OkControlID = "btnYes"
            CancelControlID="btnNo" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Confirmation
            </div>
            <div class="body">
                Do you want to Approve ?
            </div>
            <div class="footer" align="right">
                <asp:Button ID="btnNo" runat="server" Text="No" />
                <asp:Button ID="btnYes" runat="server" Text="Yes" />
            </div>
        </asp:Panel>
          End Here --%>


		<%-- Following Popup for Approve Mobile Rem Request  
        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" DisplayModalPopupID="mpe_COSACC" TargetControlID="mobile_btnSave_COSACC">
        </ajaxToolkit:ConfirmButtonExtender>
        <ajaxToolkit:ModalPopupExtender ID="mpe_COSACC" runat="server" PopupControlID="pnlPopup_COSACC" TargetControlID="mobile_btnSave_COSACC" OkControlID = "btnYes_COSACC"
            CancelControlID="btnNo_COSACC" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup_COSACC" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Confirmation
            </div>
            <div class="body">
                Do you want to Submit ?
            </div>
            <div class="footer" align="right">
                <asp:Button ID="btnNo_COSACC" runat="server" Text="No" />
                <asp:Button ID="btnYes_COSACC" runat="server" Text="Yes" />
            </div>
        </asp:Panel>
          End Here --%>



		<%-- Following Popup for Reject Mobile Rem Requestt  
        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" DisplayModalPopupID="mpe_Cancel" TargetControlID="mobile_btnReject">
        </ajaxToolkit:ConfirmButtonExtender>
        <ajaxToolkit:ModalPopupExtender ID="mpe_Cancel" runat="server" PopupControlID="pnlPopup_Cancel" TargetControlID="mobile_btnReject" OkControlID = "btnYes_CLR"
            CancelControlID="btnNo_CLR" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup_Cancel" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Confirmation
            </div>
            <div class="body">
                Do you want to Reject Mobile Reimbursement Request ?
            </div>
            <div class="footer" align="right">                                
                <asp:Button ID="btnNo_CLR" runat="server" Text="No" />
                <asp:Button ID="btnYes_CLR" runat="server" Text="Yes" />                
            </div>
        </asp:Panel>
         End Here --%>
	</div>

	<br />
	<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
	<asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />

	<asp:HiddenField ID="hdnvouno" runat="server" />

	<asp:HiddenField ID="hflEmpDesignation" runat="server" />

	<asp:HiddenField ID="hflEmpDepartment" runat="server" />
	<asp:HiddenField ID="hdnDelivaryDate" runat="server" />
	<asp:HiddenField ID="hdnResponseDate" runat="server" />
	<asp:HiddenField ID="hflEmailAddress" runat="server" />

	<asp:HiddenField ID="hflGrade" runat="server" />

	<asp:HiddenField ID="hflapprcode" runat="server" />

	<asp:HiddenField ID="hdnDesk" runat="server" />

	<asp:HiddenField ID="hdnDestnation" runat="server" />

	<asp:HiddenField ID="hdnRemid" runat="server" />
	<asp:HiddenField ID="hdnRemid_Type" runat="server" />

	<asp:HiddenField ID="hdnClaimid" runat="server" />

	<asp:HiddenField ID="hdnLcalTripid" runat="server" />

	<asp:HiddenField ID="hdnTraveltypeid" runat="server" />

	<asp:HiddenField ID="hdnDeptPlace" runat="server" />

	<asp:HiddenField ID="hdnTravelmode" runat="server" />

	<asp:HiddenField ID="hdnDeviation" runat="server" />

	<asp:HiddenField ID="hdnTrDetRequirements" runat="server" />

	<asp:HiddenField ID="hdnAccReq" runat="server" />

	<asp:HiddenField ID="hdnAccCOS" runat="server" />

	<asp:HiddenField ID="hdnlocaltrReq" runat="server" />

	<asp:HiddenField ID="hdnlocalTrCOS" runat="server" />

	<asp:HiddenField ID="hdnTravelConditionid" runat="server" />

	<asp:HiddenField ID="hdnApprId" runat="server" />

	<asp:HiddenField ID="hdnApprEmailaddress" runat="server" />

	<asp:HiddenField ID="hdnEligible" runat="server" />
	<asp:HiddenField ID="hdnTrdays" runat="server" />
	<asp:HiddenField ID="hdnTravelDtlsId" runat="server" />
	<asp:HiddenField ID="hdnAccId" runat="server" />
	<asp:HiddenField ID="hdnLocalId" runat="server" />
	<asp:HiddenField ID="hdnTravelstatus" runat="server" />
	<asp:HiddenField ID="hdnLeavestatusValue" runat="server" />
	<asp:HiddenField ID="hdnLeavestatusId" runat="server" />
	<asp:HiddenField ID="hdnIsApprover" runat="server" />

	<asp:HiddenField ID="hflempName" runat="server" />
	<asp:HiddenField ID="hdnempcode" runat="server" />

	<asp:HiddenField ID="hdnCurrentApprID" runat="server" />
	<asp:HiddenField ID="hdnReqEmailaddress" runat="server" />
	<asp:HiddenField ID="hdnFuelReimbursementType" runat="server" />
	<asp:HiddenField ID="hdnApprovalTD_Code" runat="server" />

	<asp:HiddenField ID="hdnApproverTDCOS_status" runat="server" />

	<asp:HiddenField ID="hdnisBookthrugh_TD" runat="server" />
	<asp:HiddenField ID="hdnisBookthrugh_COS" runat="server" />
	<asp:HiddenField ID="hdnisApprover_TDCOS" runat="server" />
	<asp:HiddenField ID="hdnNextApprId" runat="server" />

	<asp:HiddenField ID="HiddenField29" runat="server" />
	<asp:HiddenField ID="hdnIntermediateEmail" runat="server" />
	<asp:HiddenField ID="hdnstaus" runat="server" />


	<asp:HiddenField ID="hdnNextApprCode" runat="server" />

	<asp:HiddenField ID="hdnNextApprName" runat="server" />

	<asp:HiddenField ID="hdnNextApprEmail" runat="server" />


	<asp:HiddenField ID="hdnApprovalCOS_mail" runat="server" />
	<asp:HiddenField ID="hdnApprovalCOS_Code" runat="server" />
	<asp:HiddenField ID="hdnApprovalCOS_ID" runat="server" />
	<asp:HiddenField ID="hdnApprovalCOS_Name" runat="server" />

	<asp:HiddenField ID="hdnApprovalACCHOD_mail" runat="server" />
	<asp:HiddenField ID="hdnApprovalACCHOD_Code" runat="server" />
	<asp:HiddenField ID="hdnApprovalACCHOD_ID" runat="server" />
	<asp:HiddenField ID="hdnApprovalACCHOD_Name" runat="server" />
	<asp:HiddenField ID="hdnInboxType" runat="server" />
	<asp:HiddenField ID="hdnloginemployee_name" runat="server" />
	<asp:HiddenField ID="hdnYesNo" runat="server" />
	<asp:HiddenField ID="hdnYearlymobileAmount" runat="server" />
	<asp:HiddenField ID="hdnAssgineEMP" runat="server" />
	<asp:HiddenField ID="hdnDepartmentID" runat="server" />
	<asp:HiddenField ID="hdnCategoryId" runat="server" />
	<asp:HiddenField ID="hdnDepartmentName" runat="server" />
	<asp:HiddenField ID="hdnCurrentAssgineEMP" runat="server" />
	<asp:HiddenField ID="hdnServiceRequestNo" runat="server" />
	<asp:HiddenField ID="hdnIsExceletd" runat="server" />
	<asp:HiddenField ID="FilePath" runat="server" />
	<asp:HiddenField ID="hdnCust_ActionID" runat="server" />
	<asp:HiddenField ID="hdnCreatedDate" runat="server" />
	<asp:HiddenField ID="hdnActionBTN" runat="server" />
	<asp:HiddenField ID="hdnStatusID" runat="server" />

	<script src="../js/dist/select2.min.js"></script>
	<link href="../js/dist/select2.min.css" rel="stylesheet" />

	<script type="text/javascript">      
		$(document).ready(function () {
			$("#MainContent_ddlDepartment").select2();
			$("#MainContent_ddlCategory").select2();
			$("#MainContent_ddl_AssginmentEMP").select2();
			$(".select-dropdown").select2();
		});
	</script>
	<script type="text/javascript">
		function textboxMultilineMaxNumber(txt, maxLen) {
			try {
				if (txt.value.length > (maxLen - 1)) return false;
			} catch (e) {
			}
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
			var unicode = e.keyCode ? e.keyCode : e.charCode
			if (unicode == 8 || unicode == 46) {
				keychar = unicode;
			}
			return numcheck.test(keychar);
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

		function Count1(text) {
			var maxlength = 500;
			var object = document.getElementById(text.id)
			if (object.value.length > maxlength) {
				object.focus();
				object.value = text.value.substring(0, maxlength);
				object.scrollTop = object.scrollHeight;
				return false;
			}
			return true;
		}
		
		function SendForReject() {
			try {
				debugger;
				var msg = "Are you sure you want to Reject?";
				var retunboolean = true;
				var ele = document.getElementById('<%=Customer_Reject.ClientID%>');

				if (ele != null && !ele.disabled)
					retunboolean = true;
				else
					retunboolean = false;
				if (ele != null) {
					ele.disabled = true;
					if (retunboolean == true)
						retunboolean = ConfirmToSend(msg);

					if (retunboolean == false)
						ele.disabled = false;
				}
			}
			catch (err) {
				alert(err.description);
			}
			return retunboolean;
		}


		function ActionMultiClick() {
			try {
				debugger;
				var msg = "Are you sure you want to Action Details?";
				var retunboolean = true;
				var ele = document.getElementById('<%=mobile_btnSave.ClientID%>');

				if (ele != null && !ele.disabled)
					retunboolean = true;
				else
					retunboolean = false;
				if (ele != null) {
					ele.disabled = true;
					if (retunboolean == true)
						retunboolean = ConfirmToSend(msg);

					if (retunboolean == false)
						ele.disabled = false;
				}
			}
			catch (err) {
				alert(err.description);
			}
			return retunboolean;
		}

		function SaveMultiClick() {
			try {
				var retunboolean = true;
				var ele = document.getElementById('<%=service_btnClose.ClientID%>');

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

		function SaveMultiClick_COSSACC() {
           <%-- try {
                var retunboolean = true;
                var ele = document.getElementById('<%=mobile_btnSave_COSACC.ClientID%>');

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
            return retunboolean;--%>
		}

		function SendforCorrectionMultiClick() {
			try {
				var retunboolean = true;
				var ele = document.getElementById('<%=service_btnAssgine.ClientID%>');

				if (ele != null && !ele.disabled)
					retunboolean = true;
				else
					retunboolean = false;
				if (ele != null) {
					ele.disabled = true;
					if (retunboolean == true)
						SendforCorrection_Confirm();
				}
			}
			catch (err) {
				alert(err.description);
			}
			return retunboolean;
		}

		function SendforCorrection_Confirm() {
			//Testing();
			var confirm_value = document.createElement("INPUT");
			confirm_value.type = "hidden";
			confirm_value.name = "confirm_value";
			if (confirm("Do you want to Assgine Service Request ?")) {
				confirm_value.value = "Yes";
			} else {
				confirm_value.value = "No";
			}
			document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
			return;

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
		//



		///
		function SendForAssigne() {
			try {
				var msg = "Are you sure you want to assign request?";
				var retunboolean = true;
				var ele = document.getElementById('<%=service_btnAssgine.ClientID%>');

				if (ele != null && !ele.disabled)
					retunboolean = true;
				else
					retunboolean = false;
				if (ele != null) {
					ele.disabled = true;
					if (retunboolean == true)
						retunboolean = ConfirmToSend(msg);
					if (retunboolean == false)
						ele.disabled = false;
				}
			}
			catch (err) {
				alert(err.description);
			}
			return retunboolean;
		}
		//

		function SendForClose() {
			try {
				var msg = "Are you sure you want to close request?";
				var retunboolean = true;
				var ele = document.getElementById('<%=service_btnClose.ClientID%>');

				if (ele != null && !ele.disabled)
					retunboolean = true;
				else
					retunboolean = false;
				if (ele != null) {
					ele.disabled = true;

					if (retunboolean == true)
						retunboolean = ConfirmToSend(msg);

					if (retunboolean == false)
						ele.disabled = false;
				}
			}
			catch (err) {
				alert(err.description);
			}
			return retunboolean;
		}

		function SendForCEO() {
			try {
				var msg = "Are you sure you want to to submit?";//To Be Change
				var retunboolean = true;
				var ele = 10; 

				if (ele != null && !ele.disabled)
					retunboolean = true;
				else
					retunboolean = false;
				if (ele != null) {
					ele.disabled = true;
					if (retunboolean == true)
						retunboolean = ConfirmToSend(msg);
					if (retunboolean == false)
						ele.disabled = false;
				}
			}
			catch (err) {
				alert(err.description);
			}
			return retunboolean;
		}

		function SendForHOD() {
			try {
				var msg = "Are you sure you want to change incident owner?";//To Be Change
				var retunboolean = true;
				var ele = document.getElementById('<%=service_btnEscelateToHOD.ClientID%>');

				if (ele != null && !ele.disabled)
					retunboolean = true;
				else
					retunboolean = false;
				if (ele != null) {
					ele.disabled = true;
					if (retunboolean == true)
						retunboolean = ConfirmToSend(msg);
					if (retunboolean == false)
						ele.disabled = false;
				}
			}
			catch (err) {
				alert(err.description);
			}
			return retunboolean;
		}

		function SendForSPOC() {
			try {
				var msg = "Are you sure you want to sent back to SPOC?";//To Be Change
				var retunboolean = true;
				var ele = document.getElementById('<%=service_btnSendSPOC.ClientID%>');

				if (ele != null && !ele.disabled)
					retunboolean = true;
				else
					retunboolean = false;
				if (ele != null) {
					ele.disabled = true;
					if (retunboolean == true)
						retunboolean = ConfirmToSend(msg);
					if (retunboolean == false)
						ele.disabled = false;

				}
			}
			catch (err) {
				alert(err.description);
			}
			return retunboolean;
		}

		function SendForClear() {
			try {
				var msg = "Are you sure you want to clear this text?";//To Be Change
				var retunboolean = true;
				var ele = document.getElementById('<%=service_btnClearText.ClientID%>');

				if (ele != null && !ele.disabled)
					retunboolean = true;
				else
					retunboolean = false;
				if (ele != null) {
					ele.disabled = true;
					if (retunboolean == true)
						retunboolean = ConfirmToSend(msg);

					if (retunboolean == false)
						ele.disabled = false;
				}

			}
			catch (err) {
				alert(err.description);
			}
			return retunboolean;
		}
		// Confirm To All Button
		function ConfirmToSend(msg) {
			//Testing();
			var isConfirm = false;
			var confirm_value = document.createElement("INPUT");
			confirm_value.type = "hidden";
			confirm_value.name = "confirm_value";
			if (confirm(msg)) {
				confirm_value.value = "Yes";
				isConfirm = true;
			} else {
				confirm_value.value = "No";
				isConfirm = false;
			}
			document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
			return isConfirm;
		}
		function SendForResponse() {
			try {
				var msg = "Are you sure you want to Update this Delivery Date ?";//To Be Change
				var retunboolean = true;
				var ele = document.getElementById('<%=mobile_btnPrintPV.ClientID%>');

				if (ele != null && !ele.disabled)
					retunboolean = true;
				else
					retunboolean = false;
				if (ele != null) {
					ele.disabled = true;
					if (retunboolean == true)
						retunboolean = ConfirmToSend(msg);

					if (retunboolean == false)
						ele.disabled = false;
				}

			}
			catch (err) {
				alert(err.description);
			}
			return retunboolean;
		}

		function checkDeliveryDate(sender, args) {
			var fromdate = sender._selectedDate;
			var ToDate = new Date();
			ToDate.setMinutes(0);
			ToDate.setSeconds(0);
			ToDate.setHours(0);
			ToDate.setMilliseconds(0);
			if (fromdate < ToDate) {
				alert("You can not select a previous date than today!");
				sender._selectedDate = new Date();
				sender._textbox.set_Value(sender._selectedDate.format(sender._format))
			}
		}

		$('.OfferDates').keydown(function (e) {
			var k;
			document.all ? k = e.keyCode : k = e.which;
			if (k == 8 || k == 46)
				return false;
			else
				return true;

		});

		function DownloadFile() {
			// alert(file);
			var fileName = document.getElementById("<%=bindFilePath.ClientID%>").innerText;
			//alert(fileName);
			var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;

			//alert(localFilePath);
			 window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + fileName);

			//window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + fileName);
		}

		function DownloadFileEmployee() {
			// alert(file);
			var fileName = document.getElementById("<%=lblCreateFile.ClientID%>").innerText;
			//alert(fileName);
			var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;

			//alert(localFilePath);
			window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + fileName);

			//window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + fileName);
		}
		function ClearAction()
		{
			document.getElementById("<%=lblActions.ClientID%>").value = "Create Action Details";
			document.getElementById("<%=TxtActionDate.ClientID%>").value = "";
			document.getElementById("<%=txtActionTitle.ClientID%>").value = "";
			document.getElementById("<%=txtRemarks.ClientID%>").value = "";					
			document.getElementById("<%=ckckTask.ClientID%>").checked = false;		
			//document.getElementById('<%=mobile_btnSave.ClientID%>').style.display = 'inline-block';			
			document.getElementById('<%=hdnCust_ActionID.ClientID%>').value = "0";
			$('#MainContent_hdnCust_ActionID').value("0");	
			$('#MainContent_mobile_btnSave').text("Submit");	
			//$('#MainContent_ckckTask')
		}
		function checkTaskDate(sender, args) {
			if (sender._selectedDate >= new Date()) {
				alert("You can not select a future date than today!");
				sender._selectedDate = new Date();
				sender._textbox.set_Value(sender._selectedDate.format(sender._format))
			}
		}
	</script>
</asp:Content>


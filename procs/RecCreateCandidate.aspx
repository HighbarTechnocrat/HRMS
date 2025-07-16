<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="RecCreateCandidate.aspx.cs"
	MaintainScrollPositionOnPostback="true" Inherits="procs_RecCreateCandidate" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
	Namespace="eWorld.UI" TagPrefix="ew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

	<link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />

	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
	<%--<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />--%>
	<%--<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/fuel_RemRequest_css.css" type="text/css" media="all" />--%>
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />

	<style>
		.myaccountpagesheading {
			text-align: center !important;
			text-transform: uppercase !important;
		}

		.noresize {
			resize: none;
		}

		.aspNetDisabled {
			/*background: #dae1ed;*/
			background: #ebebe4;
		}

		#MainContent_lstApprover {
			overflow: hidden !important;
		}

		.Req_Position {
			padding-bottom: 10px !important;
		}

		#MainContent_Txt_CandidatePAN {
			text-transform: uppercase;
		}

		.DivDropdownlist {
			margin: 0px;
			margin-top: 0px !important;
			padding: 0px !important;
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
						<asp:Label ID="lblheading" runat="server" Text="Candidate Information"></asp:Label>
					</span>
				</div>
				<div>
					<asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
				</div>
				<div>
					<span>
						<a href="Requisition_Index.aspx" class="aaaa">Recruitment  Home</a>
					</span>
					<span>
						<a href="SearchCandidate.aspx" title="Back" runat="server" id="btnRecBack" style="margin-right: 10px;" class="aaaa">Back</a>
					</span>

				</div>
				<div class="edit-contact">
					<ul id="editform" runat="server">
						<li class="trvl_date" style="padding-bottom: 20px">
							<span style="font-size: 15px; text-decoration: underline">Personal Details: </span>&nbsp;&nbsp;
                            <br />
						</li>
						<li></li>
						<li></li>
						<li class="mobile_inboxEmpCode">
							<span>Name </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_CandidateName" runat="server" MaxLength="50"></asp:TextBox>
						</li>
						<li class="mobile_inboxEmpCode">
							<span>Email </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_CandidateEmail" MaxLength="50" runat="server" AutoPostBack="true" OnTextChanged="Txt_CandidateEmail_TextChanged"></asp:TextBox>
							<%--<asp:TextBox AutoComplete="off" ID="Txt_CandidateEmail" MaxLength="50" runat="server"></asp:TextBox>--%>

						</li>
						<li class="mobile_InboxEmpName">
							<span>Mobile </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_CandidateMobile" MaxLength="20" runat="server" AutoPostBack="true" OnTextChanged="Txt_CandidateMobile_TextChanged"></asp:TextBox>
							<%--<asp:TextBox AutoComplete="off" ID="Txt_CandidateMobile" MaxLength="20" runat="server"></asp:TextBox>--%>
						</li>


						<li class="mobile_InboxEmpName">
							<span>Birthday </span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<%--<asp:TextBox AutoComplete="off" ID="Txt_CandidateBirthday" runat="server"  
                               MaxLength="10" AutoCompleteType="Disabled" ></asp:TextBox>--%>
							<asp:TextBox AutoComplete="off" ID="Txt_CandidateBirthday" runat="server" AutoPostBack="true" OnTextChanged="Txt_CandidateBirthday_TextChanged"
								MaxLength="10" AutoCompleteType="Disabled"></asp:TextBox>
							<ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="Txt_CandidateBirthday"
								runat="server">
							</ajaxToolkit:CalendarExtender>

						</li>
						<li class="mobile_InboxEmpName">
							<span>Age </span>&nbsp;&nbsp;
                            <br />
							<asp:TextBox AutoComplete="off" ID="Txt_CandidateAge" MaxLength="2" ReadOnly="true" runat="server"></asp:TextBox>
						</li>


						<li class="mobile_InboxEmpName" style="margin-bottom: 10px">
							<span>Gender </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />

							<asp:DropDownList runat="server" ID="lstCandidategender" CssClass="DropDownSerach">
								<asp:ListItem Value="0" Text="Select Gender"></asp:ListItem>
								<asp:ListItem Value="1" Text="Male"></asp:ListItem>
								<asp:ListItem Value="2" Text="Female"></asp:ListItem>
							</asp:DropDownList>

						</li>

						<li class="mobile_InboxEmpName" style="margin-bottom: 10px">
							<span>Marital status </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList runat="server" ID="lstMaritalStatus" CssClass="DropDownSerach">
								<asp:ListItem Value="0" Text="Select Status"></asp:ListItem>
								<asp:ListItem Value="1" Text="Married"></asp:ListItem>
								<asp:ListItem Value="2" Text="UnMarried"></asp:ListItem>
							</asp:DropDownList>
						</li>
						<li class="mobile_InboxEmpName" style="margin-bottom: 10px">
							<span>Main Skillset </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList runat="server" ID="lstMainSkillset" CssClass="DropDownSerach">
							</asp:DropDownList>
						</li>
						<li>
							<span>Total Experience In(Year) </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_CandidateExperence" MaxLength="4" runat="server"></asp:TextBox>
						</li>

						<li class="mobile_inboxEmpCode">
							<span>Adhar No </span>&nbsp;&nbsp;
                            <br />
							<asp:TextBox AutoComplete="off" ID="txtAdharNo" data-type="adhaar-number" MaxLength="19" runat="server" onchage="AdharNo()"></asp:TextBox>
						</li>

						<li class="mobile_inboxEmpCode">
							<span>PAN </span>&nbsp;&nbsp;
                            <br />
							<asp:TextBox AutoComplete="off" ID="Txt_CandidatePAN" MaxLength="10" runat="server"></asp:TextBox>
						</li>


						<li>
							<span>Relevant Experience In(Year) </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="TxtRelevantExpYrs" Class="number" MaxLength="5" runat="server"></asp:TextBox>
						</li>
						<li class="mobile_InboxEmpName">
							<span>Additional Skillset </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_AdditionalSkillset" MaxLength="200" runat="server" TextMode="MultiLine" Rows="5" CssClass="noresize"></asp:TextBox>
						</li>
						<li class="mobile_inboxEmpCode">
							<span>Comments </span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_Comments" runat="server" MaxLength="200" TextMode="MultiLine" Rows="5" CssClass="noresize"></asp:TextBox>
						</li>
						<li></li>
						<li class="mobile_inboxEmpCode" style="margin-bottom: 10px">
							<span>CV Source </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList runat="server" ID="lstCVSource" AutoPostBack="true" CssClass="DropDownSerach" OnSelectedIndexChanged="lstCVSource_SelectedIndexChanged">
							</asp:DropDownList>
						</li>
						<li class="mobile_InboxEmpName">
							<span>
								<asp:Label ID="lbltext" runat="server" Text="Referred By" Visible="false"></asp:Label>
							</span>&nbsp;&nbsp;<span style="color: red" runat="server" id="spanIDreferredby" visible="false">*</span>
							<br />
							<asp:DropDownList runat="server" ID="lstInterviewerOne" CssClass="DropDownSerach" Visible="false"></asp:DropDownList>
							<asp:DropDownList runat="server" ID="lstVendorName" CssClass="DropDownSerach" Visible="false"></asp:DropDownList>
							<asp:DropDownList runat="server" ID="lstJobSites" CssClass="DropDownSerach" Visible="false"></asp:DropDownList>
							<asp:DropDownList runat="server" ID="DDlOther" CssClass="DropDownSerach" Visible="false">
							</asp:DropDownList>
						</li>


						<li class="mobile_InboxEmpName">
							<%--<span>Others </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_ReferredBy" MaxLength="50" runat="server"></asp:TextBox>--%>
						</li>

						<li class="trvl_date" style="padding-bottom: 20px">
							<span style="font-size: 15px; text-decoration: underline">Salary Details: </span>&nbsp;&nbsp;
                            <br />
						</li>
						<li></li>
						<li></li>
						<li>
							<span>Current CTC_Fixed In(lakh) </span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<%--							<asp:TextBox AutoComplete="off" ID="Txt_CurrentCTC_Fixed" AutoPostBack="true" Class="number" MaxLength="5" OnTextChanged="Txt_CurrentCTC_Fixed_TextChanged" runat="server"></asp:TextBox>--%>
							<asp:TextBox AutoComplete="off" ID="Txt_CurrentCTC_Fixed" Class="number" MaxLength="5" runat="server"></asp:TextBox>
						</li>
						<li>
							<span>Current CTC_Variable In(lakh)</span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<%--							<asp:TextBox AutoComplete="off" ID="TxtCurrentCTC_Variable" AutoPostBack="true" Class="number" MaxLength="5" OnTextChanged="Txt_CurrentCTC_Fixed_TextChanged" runat="server"></asp:TextBox>--%>
							<asp:TextBox AutoComplete="off" ID="TxtCurrentCTC_Variable" Class="number" MaxLength="5" runat="server"></asp:TextBox>
						</li>

						<li class="mobile_InboxEmpName">
							<span>Current CTC_Total In(lakh)</span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<asp:TextBox AutoComplete="off" ID="TxtCurrentCTC_Total" runat="server" ReadOnly="true"></asp:TextBox>
						</li>

						<li>
							<span>Exp. CTC_Fixed In(lakh)</span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<%--<asp:TextBox AutoComplete="off" ID="TxtExpCTC_Fixed" Class="number" AutoPostBack="true" MaxLength="5" runat="server" OnTextChanged="TxtExpCTC_Fixed_TextChanged"></asp:TextBox>--%>
							<asp:TextBox AutoComplete="off" ID="TxtExpCTC_Fixed" Class="number" MaxLength="5" runat="server"></asp:TextBox>
						</li>
						<li>
							<span>Exp. CTC_Variable In(lakh)</span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<%--<asp:TextBox AutoComplete="off" ID="TxtExpCTC_Variable" AutoPostBack="true" Class="number" MaxLength="5" runat="server" OnTextChanged="TxtExpCTC_Fixed_TextChanged"></asp:TextBox>--%>
							<asp:TextBox AutoComplete="off" ID="TxtExpCTC_Variable" Class="number" MaxLength="5" runat="server"></asp:TextBox>

						</li>
						<li class="mobile_InboxEmpName">
							<span>Exp. CTC_Total In(lakh)</span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<asp:TextBox AutoComplete="off" ID="TxtExpCTC_Total" runat="server" ReadOnly="true"></asp:TextBox>
						</li>
						<li class="trvl_date" style="padding-bottom: 20px">
							<span style="font-size: 15px; text-decoration: underline">Joining Details: </span>&nbsp;&nbsp;
                            <br />
						</li>
						<li></li>
						<li></li>
						<li>
							<span>Currently On Notice </span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<asp:DropDownList ID="DDlCurrentlyonnotice" CssClass="DropDownSerach" runat="server">
								<asp:ListItem Text="Select" Value="0"></asp:ListItem>
								<asp:ListItem Text="Yes" Value="1"></asp:ListItem>
								<asp:ListItem Text="No" Value="2"></asp:ListItem>
							</asp:DropDownList>
						</li>
						<li>
							<span>Notice Period( In Days) </span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_NoticePeriodInday" Class="number" onkeypress="return onlyNumbers(this);" MaxLength="4" runat="server"></asp:TextBox>
						</li>
						<li class="mobile_InboxEmpName"></li>


						<li class="trvl_date" style="padding-bottom: 20px">
							<span style="font-size: 15px; text-decoration: underline">Education Details: </span>&nbsp;&nbsp;
                            <br />
						</li>
						<li></li>
						<li></li>
						<li class="mobile_InboxEmpName " style="margin-bottom: 15px">
							<span>Education Type </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList runat="server" AutoPostBack="true" ID="lstEducationType" CssClass="DropDownSerach" OnSelectedIndexChanged="lstEducationType_SelectedIndexChanged"></asp:DropDownList>
						</li>
						<li>
							<span>Full Time / Part Time  </span>&nbsp;&nbsp;<span style="color: red" runat="server" id="spfull" visible="false">*</span>
							<br />
							<asp:DropDownList ID="DDLFullTime" CssClass="DropDownSerach" runat="server" Enabled="false">
								<asp:ListItem Text="Select" Value="Select"></asp:ListItem>
								<asp:ListItem Text="Full Time" Value="Full Time"></asp:ListItem>
								<asp:ListItem Text="Part Time" Value="Part Time"></asp:ListItem>
							</asp:DropDownList>
						</li>
						<li>
							<asp:Label runat="server" ID="lblEducation" Visible="True" Style="color: red; font-size: 14px; font-weight: 500; text-align: center;"></asp:Label>
						</li>

						<li class="mobile_InboxEmpName">
							<span>College / Institute/Professional body </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtCollegeName" runat="server" CssClass="noresize" TextMode="MultiLine" onKeyUp="javascript:Count(this);"></asp:TextBox>
						</li>
						<li class="mobile_InboxEmpName">
							<span>University Name / Board </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtUniversity" runat="server" CssClass="noresize" TextMode="MultiLine" onKeyUp="javascript:Count2(this);"></asp:TextBox>
						</li>


						<li>
							<span>Discipline  </span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtDiscipline" runat="server" Enabled="false" CssClass="noresize" TextMode="MultiLine" onKeyUp="javascript:Count(this);"></asp:TextBox>

						</li>
						<li class="mobile_InboxEmpName">
							<span>Year of Passing </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtYearofPassing" runat="server" AutoPostBack="true" MaxLength="10" AutoCompleteType="Disabled"></asp:TextBox>

							<ajaxToolkit:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" OnClientHidden="onCalendarHidden" OnClientDateSelectionChanged="checkProjectEndDate"
								OnClientShown="onCalendarShown" Format="yyyy" BehaviorID="calendar1" TargetControlID="txtYearofPassing">
							</ajaxToolkit:CalendarExtender>
						</li>
						<li class="mobile_InboxEmpName">
							<span>Final Score in % </span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtFinalScore" runat="server" MaxLength="5" CssClass="number"></asp:TextBox>
						</li>

						<li class="mobile_InboxEmpName">
							<asp:LinkButton ID="trvl_localbtn" runat="server" Text="ADD" ToolTip="ADD" CssClass="Savebtnsve" OnClick="trvldeatils_delete_btn_Click"></asp:LinkButton>

						</li>

						<br />

						<br />
						<asp:GridView ID="DgvEducationDetails" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
							DataKeyNames="CandEducationID" Width="100%">
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
								<asp:BoundField HeaderText="Education Type"
									DataField="EducationType"
									ItemStyle-HorizontalAlign="left"
									HeaderStyle-HorizontalAlign="left"
									ItemStyle-Width="10%"
									ItemStyle-BorderColor="Navy" />

								<asp:BoundField HeaderText="University Name / Board"
									DataField="PGUniversityName"
									ItemStyle-HorizontalAlign="left"
									ItemStyle-Width="20%"
									ItemStyle-BorderColor="Navy" />
								<asp:BoundField HeaderText="School / College Name"
									DataField="CollegeName"
									ItemStyle-HorizontalAlign="left"
									ItemStyle-Width="20%"
									ItemStyle-BorderColor="Navy" />

								<asp:BoundField HeaderText="Year of Passing"
									DataField="YearofPassing"
									ItemStyle-HorizontalAlign="left"
									ItemStyle-Width="8%"
									ItemStyle-BorderColor="Navy" />

								<asp:BoundField HeaderText="Final Score"
									DataField="FinalScore"
									ItemStyle-HorizontalAlign="left"
									ItemStyle-Width="8%"
									ItemStyle-BorderColor="Navy" />

								<asp:BoundField HeaderText="Discipline"
									DataField="PGDiscipline"
									ItemStyle-HorizontalAlign="left"
									ItemStyle-Width="10%"
									ItemStyle-BorderColor="Navy" />

								<asp:BoundField HeaderText="Type"
									DataField="PGType"
									ItemStyle-HorizontalAlign="left"
									ItemStyle-Width="8%"
									ItemStyle-BorderColor="Navy" />

								<asp:TemplateField HeaderText="Action" HeaderStyle-Width="15%">
									<ItemTemplate>
										<asp:ImageButton ID="btn_Edit" runat="server" ToolTip="Modify" ImageUrl="~/Images/edit.png" Width="15px" Height="15px" Style="float: left" OnClick="btn_Edit_Click" />
										<asp:ImageButton ID="btn_del" runat="server" ToolTip="Delete" Width="15px" Height="15px" ImageUrl="~/Images/delete.png" OnClick="btn_del_Click" />
									</ItemTemplate>
									<ItemStyle HorizontalAlign="Center" />
								</asp:TemplateField>

							</Columns>
						</asp:GridView>

						<br />
						<li class="trvl_date" style="padding-bottom: 20px">
							<span style="font-size: 15px; text-decoration: underline">Work Experience Details: </span>&nbsp;&nbsp;
                            <br />
						</li>
						<li></li>
						<li>
							<asp:Label runat="server" ID="lblCompany" Visible="True" Style="color: red; font-size: 14px; font-weight: 500; text-align: center;"></asp:Label>
						</li>




						<li class="mobile_InboxEmpNam ">
							<span>Name of Company </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtCompanyName" runat="server" CssClass="noresize" TextMode="MultiLine" Rows="2" onKeyUp="javascript:Count(this);"></asp:TextBox>

						</li>

						<li class="mobile_InboxEmpName">
							<span>Designation </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtComDesignation" runat="server" CssClass="noresize" TextMode="MultiLine" Rows="2" onKeyUp="javascript:Count2(this);"></asp:TextBox>
						</li>
						<li></li>
						<li class="mobile_InboxEmpName">
							<span>Start Date </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtComStartDate" runat="server" AutoPostBack="true" MaxLength="10" AutoCompleteType="Disabled" OnTextChanged="txtComStartDate_TextChanged"></asp:TextBox>
							<ajaxToolkit:CalendarExtender ID="CalendarExtender3" Format="dd/MM/yyyy" TargetControlID="txtComStartDate" OnClientDateSelectionChanged="checkProjectEndDate"
								runat="server">
							</ajaxToolkit:CalendarExtender>
						</li>
						<li class="mobile_InboxEmpName">
							<span>End Date </span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtComEndDate" runat="server" AutoPostBack="true" MaxLength="10" AutoCompleteType="Disabled" OnTextChanged="txtComStartDate_TextChanged"></asp:TextBox>
							<ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtComEndDate" OnClientDateSelectionChanged="checkProjectEndDate"
								runat="server">
							</ajaxToolkit:CalendarExtender>
						</li>
						<li></li>
						<li class="trvldetails_bookthrough">
							<asp:LinkButton ID="lnkbtn_expdtls" runat="server" Text="ADD" ToolTip="ADD" CssClass="Savebtnsve" OnClick="trvldeatils_cancel_btn_Click"></asp:LinkButton>
						</li>

						<br />
						<br />
						<asp:GridView ID="GrdCompanyInfo" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid"
							BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" DataKeyNames="CandCompanyID" Width="85%">
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
								<asp:BoundField HeaderText="Name of Company"
									DataField="NameofCompany"
									ItemStyle-HorizontalAlign="left"
									HeaderStyle-HorizontalAlign="left"
									ItemStyle-Width="20%"
									ItemStyle-BorderColor="Navy" />
								<asp:BoundField HeaderText="Designation"
									DataField="CandDesignation"
									ItemStyle-HorizontalAlign="left"
									ItemStyle-Width="15%"
									ItemStyle-BorderColor="Navy" />
								<asp:BoundField HeaderText="Start Date"
									DataField="StartDate"
									ItemStyle-HorizontalAlign="left"
									ItemStyle-Width="10%"
									ItemStyle-BorderColor="Navy" />
								<asp:BoundField HeaderText="End date"
									DataField="EndDate"
									ItemStyle-HorizontalAlign="left"
									ItemStyle-Width="10%"
									ItemStyle-BorderColor="Navy" />

								<asp:TemplateField HeaderText="Action" HeaderStyle-Width="10%">
									<ItemTemplate>
										<asp:ImageButton ID="btn_CompEdit" runat="server" ToolTip="Modify" ImageUrl="~/Images/edit.png" Width="15px" Height="15px" Style="float: left" OnClick="btn_CompEdit_Click" />
										<asp:ImageButton ID="btn_Comp" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/delete.png" OnClick="btn_Comp_Click" />
									</ItemTemplate>
									<ItemStyle HorizontalAlign="Center" />
								</asp:TemplateField>
							</Columns>
						</asp:GridView>


						<br />
						<br />
						<li class="trvl_date" style="padding-bottom: 20px">
							<span style="font-size: 15px; text-decoration: underline">Location Details: </span>&nbsp;&nbsp;
                            <br />
						</li>
						<li></li>
						<li></li>
						<li>
							<span>Current Location </span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_CandidateCurrentLocation" MaxLength="30" runat="server"></asp:TextBox></li>
						<li>
							<span>Native /Home Location </span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_NativeHomeLocation" MaxLength="30" runat="server"></asp:TextBox>
						</li>
						<li class="mobile_InboxEmpName">
							<span>Base Location in current company  </span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_BaseLocationcurrentcompany" MaxLength="50" runat="server"></asp:TextBox>
						</li>

						<li>
							<span>Base Location Preference </span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<asp:DropDownList runat="server" ID="DDLBaseLocationPreference" CssClass="DropDownSerach">
							</asp:DropDownList>

						</li>
						<li>
							<span>Is he ready to relocate and travel to any locations in India & Abroad for project implementations </span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<asp:DropDownList runat="server" ID="DDLRelocateTravelAnyLocation" CssClass="DropDownSerach">
								<asp:ListItem Text="-- Select --" Value="0"></asp:ListItem>
								<asp:ListItem Text="Yes" Value="1"></asp:ListItem>
								<asp:ListItem Text="No" Value="2"></asp:ListItem>
							</asp:DropDownList>
						</li>
						<li class="mobile_InboxEmpName">
							<span>Travel Contraint in Pandemic Situation  </span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_TravelContraintPandemicSituation" MaxLength="50" runat="server"></asp:TextBox>
						</li>


						<li class="trvl_date" style="padding-bottom: 20px">
							<br />
							<span style="font-size: 15px; text-decoration: underline">Employment Details: </span>&nbsp;&nbsp;
                            <br />
						</li>
						<li class="mobile_inboxEmpCode">
							<span></span>&nbsp;&nbsp;
                            <br />
						</li>
						<li class="mobile_InboxEmpName">
							<span></span>&nbsp;&nbsp;
                            <br />
						</li>

						<li style="padding-bottom: 10px">
							<span>Open to Travel </span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<asp:DropDownList runat="server" ID="DDLOpenToTravel" CssClass="DropDownSerach">
								<asp:ListItem Text="-- Select --" Value="0"></asp:ListItem>
								<asp:ListItem Text="Yes" Value="1"></asp:ListItem>
								<asp:ListItem Text="No" Value="2"></asp:ListItem>
							</asp:DropDownList>
						</li>
						<li style="padding-bottom: 10px">
							<span>Candidate is on whose payrolls today—IT company or third party </span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<asp:DropDownList runat="server" ID="DDlpayrollsCompany" CssClass="DropDownSerach">
								<asp:ListItem Text="-- Select --" Value="0"></asp:ListItem>
								<asp:ListItem Text="IT Company" Value="1"></asp:ListItem>
								<asp:ListItem Text="Third Party" Value="2"></asp:ListItem>
							</asp:DropDownList>
						</li>


						<li style="padding-bottom: 10px">
							<span>Whether there is any break in service. If Yes - reason </span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<asp:DropDownList runat="server" ID="DDLBreakInService" CssClass="DropDownSerach">
								<asp:ListItem Text="-- Select --" Value="0"></asp:ListItem>
								<asp:ListItem Text="Yes" Value="1"></asp:ListItem>
								<asp:ListItem Text="No" Value="2"></asp:ListItem>
							</asp:DropDownList>
						</li>


						<li style="padding-bottom: 15px">
							<span>How many full life E2E implementation projects have you worked on? </span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<asp:DropDownList runat="server" ID="DDLImplementationprojectWorkedOn" CssClass="DropDownSerach">
							</asp:DropDownList>
						</li>
						<li style="padding-bottom: 15px">
							<span>What is your TOTAL Domain experience  </span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<asp:DropDownList runat="server" ID="DDLDomainExperence" CssClass="DropDownSerach">
							</asp:DropDownList>
						</li>
						<li class="mobile_InboxEmpName" style="padding-bottom: 15px">
							<span>What is your TOTAL SAP experience  </span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<asp:DropDownList runat="server" ID="DDLSAPExperence" CssClass="DropDownSerach">
							</asp:DropDownList>
						</li>

						<li style="padding-bottom: 15px">
							<span>How many Support Project  </span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<asp:DropDownList runat="server" ID="DDLSupportproject" CssClass="DropDownSerach">
							</asp:DropDownList>
						</li>
						<li style="padding-bottom: 15px">
							<span>Which of the phases in implementation you have worked  </span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<asp:DropDownList runat="server" ID="DDLPhaseWorkimplementation" CssClass="DropDownSerach">
							</asp:DropDownList>
						</li>
						<li class="mobile_InboxEmpName" style="padding-bottom: 15px">
							<span>What roles have you played in implementation projects so far?  </span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<asp:DropDownList runat="server" ID="DDLRolesPlaydImplementation" CssClass="DropDownSerach">
							</asp:DropDownList>
						</li>

						<li class="mobile_InboxEmpName" style="padding-bottom: 10px">
							<span>What type of projects have you handled?  </span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<asp:DropDownList runat="server" ID="DDLprojecthandled" CssClass="DropDownSerach">
							</asp:DropDownList>
						</li>

						<li class="mobile_InboxEmpName" style="padding-bottom: 15px">
							<span>Nature of Industry of the clients  </span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<asp:DropDownList runat="server" ID="DDLnatureOfIndustryClient" CssClass="DropDownSerach">
							</asp:DropDownList>
						</li>

						<li style="padding-bottom: 15px">
							<span>Check   communication skill--Virtual  </span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<asp:DropDownList runat="server" ID="DDLCommunicationSkill" CssClass="DropDownSerach">
							</asp:DropDownList>
						</li>

						<li class="mobile_InboxEmpName" style="padding-bottom: 10px">
							<span>His current Role in the organization- Consultant, Team lead, Solution architect, Project Manager.  </span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_CurrentRoleorganization" MaxLength="50" runat="server"></asp:TextBox>
						</li>
						<li style="padding-bottom: 15px">
							<span>Role in Domain company  </span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_RoleDomaincompany" MaxLength="50" runat="server"></asp:TextBox>
						</li>
						<li></li>

						<li style="padding-bottom: 10px">
							<span>Reason for Break </span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<asp:TextBox AutoComplete="off" ID="TxtReasonforBreak" Height="50" CssClass="noresize" TextMode="MultiLine" MaxLength="200" runat="server"></asp:TextBox>
						</li>
						<li style="padding-bottom: 15px">
							<span>Why is he looking for a change  </span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_lookingforChange" Height="50" CssClass="noresize" TextMode="MultiLine" MaxLength="200" runat="server"></asp:TextBox>
						</li>

						<li></li>
						<br />
						<br />
						<li class="upload">
							<span>Upload Resume</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
							<asp:FileUpload ID="uploadfile" runat="server" />
							<asp:LinkButton ID="lnkuplodedfile" runat="server" ForeColor="#ff0000" OnClientClick="return DownloadFile();"></asp:LinkButton>
						</li>
						<li></li>
						<li class="mobile_grid">
							<span>Upload Other File</span><br />
							<asp:FileUpload ID="uploadotherfile" runat="server" AllowMultiple="true" />
							<asp:TextBox AutoComplete="off" ID="txtprofilename" runat="server" CssClass="mobilenotextbox popup" Visible="false"> </asp:TextBox>
							<asp:LinkButton ID="lnkuplodedfilemultiple" runat="server" Visible="false"></asp:LinkButton>
						</li>
					</ul>

				</div>
			</div>
		</div>

	</div>
	<br />
	<div class="mobile_Savebtndiv">
		<asp:LinkButton ID="mobile_btnCorrection" runat="server" Text="Save As Draft" ToolTip="Save As Draft" CssClass="Savebtnsve" OnClick="localtrvl_btnSave_Click" OnClientClick="return SaveMultiClick();">Save As Draft</asp:LinkButton>
		<asp:LinkButton ID="mobile_btnSave" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();" OnClick="mobile_btnSave_Click">Save</asp:LinkButton>
		<asp:LinkButton ID="mobile_cancel" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="mobile_cancel_Click">Back</asp:LinkButton>
	</div>
	<br />
	<%-- <ajaxToolkit:AutoCompleteExtender ServiceMethod="Searchempcode" MinimumPrefixLength="3"
        CompletionInterval="3" EnableCaching="false" CompletionSetCount="3" TargetControlID="Txt_ReferredbyEmpcode"
        ID="AutoCompleteExtender1" runat="server" FirstRowSelected="true">
    </ajaxToolkit:AutoCompleteExtender>--%>

	<asp:HiddenField ID="hdCandidate_ID" runat="server" />
	<asp:HiddenField ID="hdnRemid" runat="server" />
	<asp:HiddenField ID="hdnsptype" runat="server" />
	<asp:HiddenField ID="hdfilefath" runat="server" />
	<asp:HiddenField ID="hdfilename" runat="server" />
	<asp:HiddenField ID="hdnCandEducationID" runat="server" />
	<asp:HiddenField ID="hdnCandCompanyID" runat="server" />
	<asp:HiddenField ID="hdnYesNo" runat="server" />

	<script src="../js/dist/select2.min.js"></script>
	<link href="../js/dist/select2.min.css" rel="stylesheet" />

	<script type="text/javascript">

		$(document).ready(function () {
			$(".DropDownSerach").select2();

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

		function Count2(text) {
			var maxlength = 100;
			var object = document.getElementById(text.id)
			if (object.value.length > maxlength) {
				object.focus();
				object.value = text.value.substring(0, maxlength);
				object.scrollTop = object.scrollHeight;
				return false;
			}
			return true;
		}

		function DownloadFile() {
			var localFilePath = document.getElementById("<%=hdfilefath.ClientID%>").value;
			var file = document.getElementById("<%=hdfilename.ClientID%>").value;
			// alert(localFilePath); 
			// alert(file); 
			window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
			// window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
		}

		function DownloadFilemultiple(file) {
			var localFilePath = document.getElementById("<%=hdfilefath.ClientID%>").value;
          //   var file = document.getElementById("<%=hdfilename.ClientID%>").value;
			// alert(localFilePath); 
			// alert(file); 
			window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
			//window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
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

		function SaveMultiClick() {
			try {
				var retunboolean = true;
				var ele = document.getElementById('<%=mobile_btnSave.ClientID%>');

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

		$('[data-type="adhaar-number"]').keyup(function () {
			var value = $(this).val();
			value = value.replace(/\D/g, "").split(/(?:([\d]{4}))/g).filter(s => s.length > 0).join("-");
			$(this).val(value);
			AdharNo();
		});

		function AdharNo() {

			var value = $('#MainContent_txtAdharNo').val();
			var maxLength = $('#MainContent_txtAdharNo').attr("maxLength");
			if (value.length != maxLength) {
				$('#MainContent_txtAdharNo').css("background-color", "red");
			}
			else {
				$('#MainContent_txtAdharNo').css("background-color", "white");
			}
		}

		$('#MainContent_txtFinalScore').keyup(function () {
			var value = $(this).val();
			if (value >= 100) {
				$(this).val('');
			}
		});
		function onCalendarShown() {
			var cal = $find("calendar1");
			cal._switchMode("years", true);
			if (cal._yearsBody) {
				for (var i = 0; i < cal._yearsBody.rows.length; i++) {
					var row = cal._yearsBody.rows[i];
					for (var j = 0; j < row.cells.length; j++) {
						Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
					}
				}
			}
		}

		function onCalendarHidden() {
			var cal = $find("calendar1");
			if (cal._yearsBody) {
				for (var i = 0; i < cal._yearsBody.rows.length; i++) {
					var row = cal._yearsBody.rows[i];
					for (var j = 0; j < row.cells.length; j++) {
						Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
					}
				}
			}
		}

		function call(eventElement) {
			var target = eventElement.target;
			switch (target.mode) {
				case "year":
					var cal = $find("calendar1");
					cal.set_selectedDate(target.date);
					cal._blur.post(true);
					cal.raiseDateSelectionChanged(); break;
			}
		}
		function checkProjectEndDate(sender, args) {
			if (sender._selectedDate >= new Date()) {
				alert("You can not select a future date than today!");
				sender._selectedDate = new Date();
				sender._textbox.set_Value(sender._selectedDate.format(sender._format))
			}
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


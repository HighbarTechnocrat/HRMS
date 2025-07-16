<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="RecruiterViewEdit.aspx.cs"
	MaintainScrollPositionOnPostback="true" Inherits="procs_RecruiterViewEdit" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
	Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

	<link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Req_Requisition.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />
	<%-- Rec_AddjoinDetailbtn--%>
	<style>
		.OfferFileUplad {
			min-width: 0;
			min-height: 0;
			word-wrap: break-word;
			word-break: break-all;
			max-width: 100%;
		}

		.lnkbtn_HideUnhide {
			/*by Highbartech on 24-06-2020
        background: #febf39;*/
			background: #3D1956;
			color: #febf39 !important;
			padding: 6px 16px;
		}

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

		iframe1 {
			pointer-events: none !important;
			/*//opacity: 0.8 !important;*/
		}

		.noresize {
			resize: none;
		}
		.OfferHeading {
			color: #F28820;
			font-size: 22px;
			font-weight: normal;
			text-align: left;
			padding: 0px !important;
			margin: 0px !important;
		}

		.Total {
			background-color: #F28820;
			font-weight: 700;
			font-size: 14px;
		}

			.Total > td > input[type="text"] {
				background-color: #F28820;
				font-weight: 700;
				font-size: 14px;
				border: none !important;
				color: black;
			}
		.HideOffer 
		{
			display:none;
		}
	</style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

	<script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
	<script src="../js/HtmlControl/jquery-1.3.2.js"></script>
	<script src="../js/dist/jquery-3.2.1.min.js"></script>
	<script src="../js/HtmlControl/jHtmlArea-0.8.min.js"></script>
	<link href="../js/HtmlControl/jHtmlArea.css" rel="stylesheet" />
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
						<asp:Label ID="lblheading" runat="server" Text="Recruitment Information"></asp:Label>
					</span>
				</div>
				<div>
					<asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
				</div>
				<div>
					<span style="margin-bottom: 20px">
						<a href="Requisition_Index.aspx" class="aaaa">Recruitment Home </a>
					</span>
					<span>
						<a href="Rec_RecruiterInbox.aspx?type=VRR" title="Back" runat="server" id="btnRecBack" style="margin-right: 10px;" class="aaaa">Back</a>
					</span>

				</div>
				<div class="edit-contact">

					<ul id="editform" runat="server">
						<li></li>
						<li></li>
						<li></li>
						<li class="trvl_date">
							<span>Requisition Number  </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtReqNumber" runat="server" Enabled="false"></asp:TextBox>
						</li>

						<li class="trvl_date">
							<span>Requisition Date </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtFromdate" runat="server" Enabled="false"></asp:TextBox>
						</li>

						<li class="trvl_date">
							<span>Name </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtReqName" runat="server" Enabled="false"></asp:TextBox>
						</li>
						<li class="trvl_date">
							<span>Department </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtReqDept" runat="server" Enabled="false"></asp:TextBox>
						</li>
						<li class="trvl_date">
							<span>Designation </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtReqDesig" runat="server" Enabled="false"></asp:TextBox>
						</li>
						<li class="trvl_date">
							<span>Email </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtReqEmail" runat="server" Enabled="false"></asp:TextBox>
						</li>

						<li class="trvl_date Req_Position">

							<span>Position Title</span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList runat="server" Enabled="false" ID="lstPositionName" AutoPostBack="true">
							</asp:DropDownList>
							<br />

						</li>


						<li class="trvl_date Req_Position">
							<span>Position Criticality</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
							<asp:DropDownList runat="server" Enabled="false" ID="lstPositionCriti">
							</asp:DropDownList>
							<br />
						</li>
						<li class="trvl_date Req_Position">

							<span>Main Skillset </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList runat="server" ID="lstSkillset" Enabled="false" AutoPostBack="true">
							</asp:DropDownList>
							<br />

						</li>

						<li class="trvl_date Req_Position">
							<span>Department Name</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
							<asp:DropDownList runat="server" ID="lstPositionDept" Enabled="false">
							</asp:DropDownList>
							<br />
						</li>

						<li class="trvl_date Req_Position" style="display: none">
							<span>Position Designation</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
							<asp:DropDownList runat="server" ID="lstPositionDesign" Enabled="false">
							</asp:DropDownList>
							<br />
						</li>
						<li class="trvl_date Req_Position">
							<span>Position Location</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
							<asp:DropDownList runat="server" ID="lstPositionLoca" Enabled="false">
							</asp:DropDownList>
							<br />
						</li>
						<li></li>

						<li class="trvl_date" style="display: none">
							<span>Other Department</span>&nbsp;&nbsp;<span style="color: red"></span><br />
							<asp:TextBox AutoComplete="off" ID="txtOtherDept" runat="server" Enabled="false"></asp:TextBox>
							<br />
						</li>
						<li class="trvl_date" style="display: none">
							<span>Position Designation Other</span>&nbsp;&nbsp;<span style="color: red"></span><br />
							<asp:TextBox AutoComplete="off" ID="txtPositionDesig" runat="server" Enabled="false"></asp:TextBox>
						</li>
						<li class="trvl_date">
							<span>No Of Position</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
							<asp:TextBox AutoComplete="off" ID="txtNoofPosition" runat="server" MaxLength="2" Enabled="false"></asp:TextBox>
						</li>

						<li class="trvl_date">
							<span>Additional Skillset</span>&nbsp;&nbsp;<span style="color: red"> </span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtAdditionSkill" runat="server" Enabled="false"></asp:TextBox>
						</li>
						<li class="trvl_date">
							<span>To Be Filled In(Days)</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
							<asp:TextBox AutoComplete="off" ID="txttofilledIn" runat="server" MaxLength="3" Enabled="false"></asp:TextBox>
						</li>


						<li class="trvl_date Req_Salary" style="display: none">
							<span style="display: none">
								<span>Salary Range(Lakh/Year)</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
								<asp:TextBox AutoComplete="off" ID="txtSalaryRangeFrom" runat="server" MaxLength="4" Enabled="false"></asp:TextBox>
								&nbsp;  To  &nbsp;
                            <asp:TextBox AutoComplete="off" ID="txtSalaryRangeTo" runat="server" MaxLength="5" Enabled="false"></asp:TextBox>
							</span>
						</li>

						<li class="trvl_date Req_Position">
							<span>Reason For Requisition</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
							<asp:DropDownList runat="server" ID="lstReasonForRequi" Enabled="false">
							</asp:DropDownList>
						</li>
						<li class="trvl_date Req_Position">

							<span>Preferred Employment Type</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
							<asp:DropDownList runat="server" ID="lstPreferredEmpType" Enabled="false">
							</asp:DropDownList>
						</li>
						<li class="trvl_date Req_Position">
							<span>Band</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
							<asp:DropDownList runat="server" ID="lstPositionBand" Enabled="false">
							</asp:DropDownList>
						</li>
						<li class="trvl_date Req_Salary">
							<span>Required Experience(Year)</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
							<asp:TextBox AutoComplete="off" ID="txtRequiredExperiencefrom" Enabled="false" runat="server" MaxLength="4"></asp:TextBox>
							&nbsp; To &nbsp;
                            <asp:TextBox AutoComplete="off" ID="txtRequiredExperienceto" Enabled="false" runat="server" MaxLength="4"></asp:TextBox>

						</li>

						<li class="trvl_date">
							<span style="display: none">
								<span>Recommended Person</span>&nbsp;&nbsp;<span style="color: red"></span><br />
								<asp:DropDownList runat="server" ID="LstRecommPerson" Enabled="false">
								</asp:DropDownList>
							</span>
						</li>
						<li></li>

						<li class="trvL_detail" id="litrvldetail" runat="server">
							<asp:LinkButton ID="btnTra_Details" runat="server" Text="+" OnClick="btnTra_Details_Click" ToolTip="Browse" CssClass="Savebtnsve"></asp:LinkButton>
							<span id="spntrvldtls" runat="server">Recruitment Other Details</span>
						</li>
						<li></li>
						<li></li>
						<div id="DivRecruitment" class="edit-contact" runat="server" visible="false">

							<ul id="Ul3" runat="server">

								<li class="Req_Requi_Esse">
									<span>Essential Qualification</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
									<asp:TextBox AutoComplete="off" ID="txtEssentialQualifi" runat="server" Enabled="false" Rows="7" CssClass="noresize" TextMode="MultiLine"></asp:TextBox>
								</li>
								<li class="Req_Requi_Esse">
									<span>Desired Qualification</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
									<asp:TextBox AutoComplete="off" ID="txtDesiredQualifi" runat="server" Enabled="false" Rows="7" CssClass="noresize" TextMode="MultiLine"></asp:TextBox>
								</li>

								<li class="Req_Job_Desc">
									<span>Job Description</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
									<asp:TextBox AutoComplete="off" ID="txtJobDescription" runat="server" CssClass="noresize" Rows="7" TextMode="MultiLine" Enabled="false"></asp:TextBox>
								</li>
								<li></li>
								<li></li>
								<li></li>

								<li>
									<asp:LinkButton ID="localtrvl_btnSave" Visible="false" runat="server" Text="Questionnaire" ToolTip="Get JD From Bank" CssClass="Savebtnsve" PostBackUrl="~/procs/Req_JD_Bank_Search.aspx"> Get JD From Bank  </asp:LinkButton>
								</li>
								<li></li>
								<li class="trvl_date" runat="server" visible="true">
									<span>Assign Questionnaire</span>&nbsp;&nbsp;<span style="color: red"></span>
								</li>
								<li>

									<asp:LinkButton ID="Lnk_Questionnaire" runat="server" OnClientClick="DownloadFile1()"></asp:LinkButton>

								</li>
								<li class="trvl_date" style="display: none">
									<asp:LinkButton ID="accmo_cancel_btn" runat="server" Visible="false" Text="Questionnaire" ToolTip="Select Questionnaire" CssClass="Savebtnsve"> Select Questionnaire  </asp:LinkButton>

								</li>

								<li class="Req_Requi_Cmt" id="lsttrvlapprover" runat="server">
									<span>Comments</span>&nbsp;&nbsp;<span style="color: red"></span><br />
									<asp:TextBox AutoComplete="off" ID="txtComments" runat="server" Enabled="false" CssClass="noresize" TextMode="MultiLine" Rows="5"></asp:TextBox>
								</li>
								<li class="trvl_date Req_Position">
									<span>Screening By</span>&nbsp;&nbsp;<br />
									<asp:DropDownList runat="server" ID="lstInterviewerOneView" Enabled="false">
									</asp:DropDownList>
								</li>
								<li class="trvl_date Req_Position">
									<%-- <span>Interviewer (2st Level)</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                           <asp:TextBox AutoComplete="off" ID="lstInterviewerTwo" runat="server" Enabled="false"></asp:TextBox>--%>

								</li>
								<li></li>
								<li class="trvl_date">
									<%--    <span>Interviewer (1st Level)</span>&nbsp;&nbsp;<span style="color: red"></span><br />
                            <asp:TextBox AutoComplete="off" ID="txtInterviewerOptOne" runat="server" Enabled="false"></asp:TextBox>--%>
								</li>

								<li class="trvl_date">
									<%--  <span>Interviewer (2st Level)</span>&nbsp;&nbsp;<span style="color: red"></span><br />
                            <asp:TextBox AutoComplete="off" ID="txtInterviewerOptTwo" runat="server" Enabled="false"></asp:TextBox>--%>
								</li>
							</ul>
						</div>
						<li class="trvl_local">
							<asp:LinkButton ID="trvl_localbtn" runat="server" Text="-" ToolTip="Browse" CssClass="Savebtnsve"></asp:LinkButton>
							<span id="spnlocalTrvl" runat="server">Candidate Information</span>
						</li>
						<li></li>
						<li></li>
						<li></li>
						<li></li>
						<li></li>
						<%-- <li></li>--%>
						<li class="trvl_local" style="margin-left: 60px">
							<div class="mobile_Savebtndiv" id="DivInterviewerShortListButton" runat="server">
								<asp:LinkButton ID="Linkbtn_CandidateShortlisting" runat="server" Text=" Click Here Candidate Send for Shortlisting" ToolTip="Click Here Candidate Send for Shortlisting" OnClick="Linkbtn_CandidateShortlisting_Click"
									CssClass="Savebtnsve"></asp:LinkButton>
							</div>
						</li>
						<li>
							<span id="SpanCloseReqChk" runat="server" visible="false">&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="CheckClosedRequisition" runat="server" AutoPostBack="true" OnCheckedChanged="CheckClosedRequisition_CheckedChanged" />&nbsp;<span>Close Requisition</span>
							</span>
						</li>
						<li class="trvl_local" style="margin-left: -85px;">
							<span id="SpanJoinemployee" runat="server" visible="false">
								<span>Joining Employee Name</span>&nbsp;&nbsp; <span style="color: red">*</span><br />
								<asp:DropDownList runat="server" ID="DDLJoinedemployee" AutoPostBack="true" OnSelectedIndexChanged="DDLJoinedemployee_SelectedIndexChanged">
								</asp:DropDownList>
							</span>

						</li>
						<li class="trvl_local">
							<span id="SpanJoiningDate" runat="server" visible="false">
								<span>Joining Date</span>&nbsp;&nbsp;<span style="color: red"></span><br />
								<asp:TextBox AutoComplete="off" ID="txtemployeeJoiningDate" runat="server" Enabled="false" Width="50%"></asp:TextBox>
							</span>
						</li>

					</ul>
				</div>

			</div>


			<br />
			<br />
			<div class="manage_grid" style="width: 95%; height: auto; padding-left: 40px" runat="server" id="Div1">
				<asp:GridView ID="GVShortListInterviewer" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="solid" BorderWidth="1px"
					DataKeyNames="Candidate_ID,InterviewShortListMain_ID" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false">
					<FooterStyle BackColor="White" ForeColor="#000066" />
					<HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
					<PagerStyle ForeColor="#000066" HorizontalAlign="right" BackColor="White" />
					<RowStyle ForeColor="#000066" />
					<SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
					<SortedAscendingCellStyle BackColor="#F1F1F1" />
					<SortedAscendingHeaderStyle BackColor="#007DBB" />
					<SortedDescendingCellStyle BackColor="#CAC9C9" />
					<SortedDescendingHeaderStyle BackColor="#00547E" />
					<Columns>
						<asp:BoundField HeaderText="CandidateID" DataField="CandidateID" ItemStyle-HorizontalAlign="left"
							HeaderStyle-HorizontalAlign="left" ItemStyle-Width="25%" ItemStyle-BorderColor="Navy" Visible="false" />

						<asp:BoundField HeaderText="Name" DataField="CandidateName" ItemStyle-HorizontalAlign="left"
							HeaderStyle-HorizontalAlign="left" ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />
						<asp:BoundField HeaderText="Main Skillset" DataField="ModuleDesc" ItemStyle-HorizontalAlign="center"
							ItemStyle-Width="6%" ItemStyle-BorderColor="Navy" />
						<asp:BoundField HeaderText="Currently On Notice" DataField="CurrentlyOnNotice" ItemStyle-HorizontalAlign="center"
							ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />
						<asp:BoundField HeaderText="Notice Period" DataField="NoticePeriod" ItemStyle-HorizontalAlign="center"
							ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />
						<asp:BoundField HeaderText=" Tot. Exper. Year" DataField="ExperienceYear" ItemStyle-HorizontalAlign="center"
							ItemStyle-Width="7%" ItemStyle-BorderColor="Navy" />
						<asp:BoundField HeaderText=" Rel. Exper. Year" DataField="RelevantExpYrs" ItemStyle-HorizontalAlign="center"
							ItemStyle-Width="7%" ItemStyle-BorderColor="Navy" />
						<asp:BoundField HeaderText="Current CTC(Lakh)" DataField="CurrentCTC" ItemStyle-HorizontalAlign="center"
							ItemStyle-Width="6%" ItemStyle-BorderColor="Navy" />
						<asp:BoundField HeaderText="Expected CTC(Lakh)" DataField="ExpectedCTC" ItemStyle-HorizontalAlign="center"
							ItemStyle-Width="6%" ItemStyle-BorderColor="Navy" />
						<asp:BoundField HeaderText="Shortlisting Status" DataField="ShortlistingStatus" ItemStyle-HorizontalAlign="center"
							ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
						<asp:BoundField HeaderText="Inter. Status" DataField="InterStatus" ItemStyle-HorizontalAlign="center"
							ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
						<asp:BoundField HeaderText="Inter. Feedback" DataField="InterFeedback" ItemStyle-HorizontalAlign="center"
							ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
						<asp:BoundField HeaderText="Hiring status" DataField="StatusUpdate" ItemStyle-HorizontalAlign="center"
							ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />
						<asp:BoundField HeaderText="Candidate status" DataField="RecCandidateStatus" ItemStyle-HorizontalAlign="center"
							ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

						<asp:TemplateField HeaderText="View" HeaderStyle-Width="15%">
							<ItemTemplate>
								<asp:HiddenField ID="HFInterFeedBackvalue" runat="server" Value='<%# Bind("InterFeedback") %>' />
								<asp:HiddenField ID="HFCurrentlyOnNotice" runat="server" Value='<%# Bind("CurrentlyOnNotice") %>' />
								<asp:HiddenField ID="HFNoticePeriod" runat="server" Value='<%# Bind("NoticePeriod") %>' />
								<asp:HiddenField ID="HFCurrentCTC" runat="server" Value='<%# Bind("CurrentCTC") %>' />
								<asp:HiddenField ID="HFExpectedCTC" runat="server" Value='<%# Bind("ExpectedCTC") %>' />
								<asp:HiddenField ID="HFExperienceYear" runat="server" Value='<%# Bind("ExperienceYear") %>' />
								<asp:HiddenField ID="HFScreenerRemarks" runat="server" Value='<%# Bind("Remarks") %>' />

								<asp:ImageButton ID="lnkEditView" runat="server" Width="20px" Height="15px" OnClick="lnkEditView_Click" ImageUrl="~/Images/edit.png" />
							</ItemTemplate>
							<ItemStyle HorizontalAlign="Center" />
						</asp:TemplateField>
					</Columns>
				</asp:GridView>
			</div>

			<br />
			<br />
			<div class="manage_grid" style="width: 100%; height: auto; padding-left: 40px" runat="server" id="DivCandidateRoundHistory" visible="false">
				<asp:GridView ID="GVCandidateRoundHistory" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="solid" BorderWidth="1px"
					DataKeyNames="Candidate_ID" CellPadding="3" AutoGenerateColumns="False" Width="95%" EditRowStyle-Wrap="false">
					<FooterStyle BackColor="White" ForeColor="#000066" />
					<HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
					<PagerStyle ForeColor="#000066" HorizontalAlign="right" BackColor="White" />
					<RowStyle ForeColor="#000066" />
					<SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
					<SortedAscendingCellStyle BackColor="#F1F1F1" />
					<SortedAscendingHeaderStyle BackColor="#007DBB" />
					<SortedDescendingCellStyle BackColor="#CAC9C9" />
					<SortedDescendingHeaderStyle BackColor="#00547E" />
					<Columns>
						<asp:BoundField HeaderText="CandidateID" DataField="CandidateID" ItemStyle-HorizontalAlign="left"
							HeaderStyle-HorizontalAlign="left" ItemStyle-Width="25%" ItemStyle-BorderColor="Navy" Visible="false" />
						<asp:BoundField HeaderText="Name" DataField="CandidateName" ItemStyle-HorizontalAlign="left"
							HeaderStyle-HorizontalAlign="left" ItemStyle-Width="20%" ItemStyle-BorderColor="Navy" Visible="false" />
						<%--  <asp:BoundField HeaderText="PAN"  DataField="CandidatePAN" ItemStyle-HorizontalAlign="center"                                 
                                ItemStyle-Width="15%"  ItemStyle-BorderColor="Navy"  />--%>
						<asp:BoundField HeaderText="Interview Date" DataField="EnterviewDate" ItemStyle-HorizontalAlign="center"
							ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />
						<asp:BoundField HeaderText="Interview Time" DataField="InterviewTime" ItemStyle-HorizontalAlign="center"
							ItemStyle-Width="6%" ItemStyle-BorderColor="Navy" />
						<asp:BoundField HeaderText="Interviewer / Interview Schedular" DataField="Interviewer" ItemStyle-HorizontalAlign="center"
							ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />
						<asp:BoundField HeaderText="Inter. Status" DataField="InterviewStatus" ItemStyle-HorizontalAlign="center"
							ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />
						<asp:BoundField HeaderText="Inter. Feedback" DataField="InterviewFeedback" ItemStyle-HorizontalAlign="center"
							ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />
						<asp:BoundField HeaderText="Inter. Round" DataField="InterviewRound" ItemStyle-HorizontalAlign="center"
							ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />
						<asp:BoundField HeaderText="Inter. Type" DataField="InterviewType" ItemStyle-HorizontalAlign="center"
							ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />
						<asp:BoundField HeaderText="Interviewer / Interview Schedular Comments" DataField="InterviewerComments" ItemStyle-HorizontalAlign="Left"
							ItemStyle-Width="22%" ItemStyle-BorderColor="Navy" />
						<asp:TemplateField HeaderText="IR Sheet" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="2%">
							<ItemTemplate>
								<%--<asp:LinkButton ID="lnkViewFilesIRSheet" runat="server" Text='<%# Eval("IRSheet") %>' OnClientClick=<%# "DownloadFilemultipleIRSheet('" + Eval("IRSheet") + "')" %>></asp:LinkButton>--%>
								<asp:ImageButton ID="lnkIRsheetView" runat="server" Width="15px" ToolTip="View IR Sheet " Height="15px" OnClick="lnkIRsheetView_Click" ImageUrl="~/Images/edit.png" />

							</ItemTemplate>
							<ItemStyle HorizontalAlign="Center" />
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Download IR Sheet" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="2%">
							<ItemTemplate>
								<asp:ImageButton ID="lnkIRsheetExport" runat="server" Width="15px" ToolTip="Download IR Sheet" Height="15px" Visible='<%# String.IsNullOrEmpty(Convert.ToString(Eval("InterviewerComments"))) ? false : true %>' OnClick="lnkIRsheetExport_Click" ImageUrl="~/Images/Download.png" />
							</ItemTemplate>
							<ItemStyle HorizontalAlign="Center" />
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Photo" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="2%">
							<ItemTemplate>
								<asp:ImageButton ID="lnkinterviewPhoto" runat="server" Width="15px" ToolTip="View Photo" Height="15px" ImageUrl="~/Images/Download.png"
									OnClientClick=<%# "DownloadInterViewerPhoto('" + Eval("Photo") + "')" %> Visible='<%# String.IsNullOrEmpty(Convert.ToString(Eval("Photo"))) ? false : true %>' />
							</ItemTemplate>
							<ItemStyle HorizontalAlign="Center" />
						</asp:TemplateField>
					</Columns>
				</asp:GridView>
			</div>


			<div runat="server" id="DivViewrowWiseCandidateInformation" visible="false">
				<div class="edit-contact">

					<ul id="Ul2" runat="server">

						<li class="trvl_date" style="padding-bottom: 20px">
							<span style="font-size: larger">Candidate Information </span>&nbsp;&nbsp;
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


						<li class="trvl_local" style="padding-bottom: 20px" runat="server" id="DIV_TransferCanInfo1" visible="false">
							<asp:LinkButton ID="Link_TransferHideUnhide" runat="server" Text="+" ToolTip="Browse" CssClass="lnkbtn_HideUnhide" OnClick="Link_TransferHideUnhide_Click"></asp:LinkButton>
							<span style="font-size: larger; text-decoration: underline">Transfer Candidate : </span>&nbsp;&nbsp;
                            <br />
						</li>
						<li class="mobile_inboxEmpCode" runat="server" id="DIV_TransferCanInfo2" visible="false">
							<span></span>&nbsp;&nbsp;
                            <br />
						</li>
						<li class="mobile_InboxEmpName" runat="server" id="DIV_TransferCanInfo3" visible="false">
							<span></span>&nbsp;&nbsp;
                            <br />
						</li>

						<li style="padding-top: 10px" runat="server" id="DIV_TransferCanInfo7" visible="false">
							<span>Position Title</span>&nbsp;&nbsp;
                             <br />
							<asp:DropDownList runat="server" ID="lstPositionTitleSearch" CssClass="DropdownListSearch">
							</asp:DropDownList>
						</li>
						<li style="padding-top: 10px" runat="server" id="DIV_TransferCanInfo8" visible="false">
							<span>Skill Set</span>&nbsp;&nbsp;
                             <br />
							<asp:DropDownList runat="server" ID="LstSkillSetSearch" CssClass="DropdownListSearch">
							</asp:DropDownList>
						</li>
						<li style="padding-top: 10px" runat="server" id="DIV_TransferCanInfo9" visible="false">
							<span>Location</span>&nbsp;&nbsp;
                             <br />
							<asp:DropDownList runat="server" ID="LstLocationSearch" CssClass="DropdownListSearch">
							</asp:DropDownList>
						</li>

						<li style="padding-top: 10px" runat="server" id="DIV_TransferCanInfo10" visible="false">
							<span>Band</span>&nbsp;&nbsp;
                             <br />
							<asp:DropDownList runat="server" ID="LstbandSearch" CssClass="DropdownListSearch">
							</asp:DropDownList>
						</li>
						<li runat="server" id="DIV_TransferCanInfo11" visible="false"></li>
						<li runat="server" id="DIV_TransferCanInfo12" visible="false"></li>

						<li style="padding-top: 15px" runat="server" id="DIV_TransferCanInfo13" visible="false">
							<asp:LinkButton ID="LinkBtnSearchTransferfilter" runat="server" Text="Submit" ToolTip="Search" OnClick="LinkBtnSearchTransferfilter_Click" CssClass="link_buttonClass">Search </asp:LinkButton>
							<asp:LinkButton ID="LinkBtnSearchTransferfilterClear" runat="server" Text="Clear Search" OnClick="LinkBtnSearchTransferfilterClear_Click" ToolTip="Clear Search" class="link_buttonClass">Clear Search</asp:LinkButton>

						</li>
						<li style="padding-top: 15px" runat="server" id="DIV_TransferCanInfo14" visible="false"></li>
						<li style="padding-top: 15px" runat="server" id="DIV_TransferCanInfo15" visible="false"></li>


						<li class="mobile_InboxEmpName" style="padding-bottom: 30px; padding-top: 30px" runat="server" id="DIV_TransferCanInfo4" visible="false">
							<span>Requisition Number </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList runat="server" ID="DDLsearchRequisitionnumber" CssClass="DropdownListSearch">
							</asp:DropDownList>
						</li>
						<li runat="server" id="DIV_TransferCanInfo5" visible="false" style="padding-top: 30px; padding-bottom: 30px">
							<asp:LinkButton ID="Link_BtnTransferCandidate" runat="server" Text="Transfer Candidate" ToolTip="Transfer Candidate"
								CssClass="link_buttonClass" OnClientClick="return SaveMultiLinkTransferCandidateClick();" OnClick="Link_BtnTransferCandidate_Click"></asp:LinkButton>

						</li>
						<li runat="server" id="DIV_TransferCanInfo6" visible="false" style="padding-top: 30px; padding-bottom: 30px"></li>





						<li class="trvl_date" style="padding-bottom: 20px">
							<span style="font-size: larger; text-decoration: underline">Personal Details: </span>&nbsp;&nbsp;
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

						<li class="mobile_inboxEmpCode">
							<span>Name </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtName" runat="server" MaxLength="50" Enabled="false"></asp:TextBox>
						</li>
						<li class="mobile_inboxEmpCode">
							<span>Email </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtEmail" MaxLength="50" runat="server" Enabled="false"></asp:TextBox>
						</li>
						<li class="mobile_InboxEmpName">
							<span>Mobile </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_CandidateMobile" MaxLength="10" runat="server" Enabled="false"></asp:TextBox>
						</li>
						<li class="mobile_InboxEmpName">
							<span>Birthday </span>&nbsp;&nbsp;
                            <br />
							<asp:TextBox AutoComplete="off" ID="Txt_CandidateBirthday" runat="server" Enabled="false"
								MaxLength="10" AutoCompleteType="Disabled"></asp:TextBox>
							<ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="Txt_CandidateBirthday"
								runat="server">
							</ajaxToolkit:CalendarExtender>
						</li>
						<li class="mobile_InboxEmpName">
							<span>Age </span>&nbsp;&nbsp;
                            <br />
							<asp:TextBox AutoComplete="off" ID="Txt_CandidateAge" MaxLength="2" Enabled="false" ReadOnly="true" runat="server"></asp:TextBox>
						</li>
						<li class="mobile_InboxEmpName">
							<%--<span >Current Location </span>&nbsp;&nbsp;<span style="color:red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_CandidateCurrentLocation" Enabled="false" MaxLength="20" runat="server"></asp:TextBox>
							--%>
						</li>

						<li class="mobile_InboxEmpName" style="margin-bottom: 10px">
							<span>Gender </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList runat="server" ID="lstCandidategender" Enabled="false">
								<asp:ListItem Value="0" Text="Select Gender"></asp:ListItem>
								<asp:ListItem Value="1" Text="Male"></asp:ListItem>
								<asp:ListItem Value="2" Text="Female"></asp:ListItem>
							</asp:DropDownList>
						</li>
						<li class="mobile_InboxEmpName" style="margin-bottom: 10px">
							<span>Marital status </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList runat="server" ID="lstMaritalStatus" Enabled="false">
								<asp:ListItem Value="0" Text="Select Status"></asp:ListItem>
								<asp:ListItem Value="1" Text="Married"></asp:ListItem>
								<asp:ListItem Value="2" Text="UnMarried"></asp:ListItem>
							</asp:DropDownList>
						</li>
						<li class="mobile_InboxEmpName" style="margin-bottom: 10px">
							<span>Main Skillset </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList runat="server" ID="DDLmainSkillSet" Enabled="false">
							</asp:DropDownList>
						</li>
						<li class="mobile_inboxEmpCode">
							<span>PAN </span>&nbsp;&nbsp;
                            <br />
							<asp:TextBox AutoComplete="off" ID="Txt_CandidatePAN" Enabled="false" MaxLength="10" runat="server"></asp:TextBox>
						</li>
						<li class="mobile_inboxEmpCode">
							<span>Aadhar No. </span>&nbsp;&nbsp;
                            <br />
							<asp:TextBox AutoComplete="off" ID="TxtAadharNo" Enabled="false" MaxLength="10" runat="server"></asp:TextBox>
						</li>
						<li class="mobile_inboxEmpCode">
							<span>CV Source </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList runat="server" ID="lstCVSource" Visible="false" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="lstCVSource_SelectedIndexChanged">
							</asp:DropDownList>
							<asp:TextBox AutoComplete="off" ID="Txt_lstCVSource" Enabled="false" MaxLength="9" runat="server"></asp:TextBox>
						</li>

						<li class="mobile_InboxEmpName" style="margin-bottom: 10px">
							<span>
								<asp:Label ID="lbltext" runat="server" Text="Referred By"></asp:Label>
							</span>&nbsp;&nbsp;<span style="color: red" runat="server" id="spanIDreferredby" visible="false">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_ReferredBy" MaxLength="50" runat="server" Enabled="false"></asp:TextBox>
							<asp:TextBox AutoComplete="off" ID="Txt_ReferredbyEmpcode" MaxLength="50" runat="server" Visible="false" Enabled="false"></asp:TextBox>
						</li>
						<li>
							<span>Total Experience In(Year) </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="TxtTotalExperienceYrs" Class="number" Enabled="false" MaxLength="5" runat="server"></asp:TextBox>
						</li>
						<li>
							<span>Relevant Experience In(Year) </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="TxtRelevantExpYrs" Class="number" Enabled="false" MaxLength="5" runat="server"></asp:TextBox>
						</li>

						<li class="mobile_InboxEmpName">
							<span>Additional Skillset </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_AdditionalSkillset" Enabled="false" MaxLength="200" runat="server" CssClass="noresize" TextMode="MultiLine" Rows="5"></asp:TextBox>
						</li>

						<li class="mobile_inboxEmpCode">
							<span>Comments </span>&nbsp;&nbsp;
                            <br />
							<asp:TextBox AutoComplete="off" ID="Txt_Comments" Enabled="false" runat="server" MaxLength="200" TextMode="MultiLine" Rows="5" CssClass="noresize"></asp:TextBox>
						</li>
						<li class="mobile_inboxEmpCode">
							<span>Screener Comments </span>&nbsp;&nbsp;
                            <br />
							<asp:TextBox AutoComplete="off" ID="Txt_ScreenerComments" Enabled="false" runat="server" TextMode="MultiLine" Rows="5" CssClass="noresize"></asp:TextBox>
						</li>

						<li></li>
						<li></li>
						<li></li>

						<li class="trvl_date" style="padding-bottom: 20px">
							<span style="font-size: larger; text-decoration: underline">Salary Details: </span>&nbsp;&nbsp;
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

						<li>
							<span>Current CTC_Fixed In(lakh) </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_CurrentCTC_Fixed" Class="number" Enabled="false" MaxLength="5" runat="server"></asp:TextBox>
						</li>
						<li>
							<span>Current CTC_Variable In(lakh)</span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="TxtCurrentCTC_Variable" Enabled="false" Class="number" MaxLength="5" runat="server"></asp:TextBox>
						</li>

						<li class="mobile_InboxEmpName">
							<span>Current CTC_Total In(lakh)</span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="TxtCurrentCTC_Total" Enabled="false" runat="server"></asp:TextBox>
						</li>

						<li>
							<span>Exp. CTC_Fixed In(lakh)</span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="TxtExpCTC_Fixed" Class="number" Enabled="false" MaxLength="5" runat="server"></asp:TextBox>
						</li>
						<li>
							<span>Exp. CTC_Variable In(lakh)</span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="TxtExpCTC_Variable" Class="number" Enabled="false" MaxLength="5" runat="server"></asp:TextBox>
						</li>
						<li class="mobile_InboxEmpName">
							<span>Exp. CTC_Total In(lakh)</span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="TxtExpCTC_Total" Enabled="false" runat="server"></asp:TextBox>
						</li>
						<li class="trvl_date" style="padding-bottom: 10px">
							<span style="font-size: larger; text-decoration: underline" runat="server" id="CTC1">CTC Exception History: </span>&nbsp;&nbsp;
                            <br />
						</li>
						<li></li>
						<li></li>
						<li>
							<asp:CheckBox ID="Chk_Exception_CTC" runat="server" Enabled="false" Text="CTC Exception" />
						</li>
						<li></li>
						<li></li>
						<li>
							<span runat="server" id="ExceptionR" visible="false">
								<span>Recruiter Remark </span>&nbsp;&nbsp;<span style="color: red">*</span>
								<br />
								<asp:TextBox AutoComplete="off" ID="txtRecruiterRemark" Enabled="false" CssClass="noresize" runat="server" onKeyUp="javascript:CountRemark(this);" Width="190%" TextMode="MultiLine" Rows="4"></asp:TextBox>
							</span>
						</li>
						<li></li>
						<li></li>
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
						<br />
						<br />
						<li class="trvl_date" style="padding-bottom: 20px">
							<span style="font-size: larger; text-decoration: underline">Joining Details: </span>&nbsp;&nbsp;
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
						<li>
							<span>Currently On Notice </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList ID="DDlCurrentlyonnotice" runat="server" Enabled="false">
								<asp:ListItem Text="Select" Value="0"></asp:ListItem>
								<asp:ListItem Text="Yes" Value="1"></asp:ListItem>
								<asp:ListItem Text="No" Value="2"></asp:ListItem>
							</asp:DropDownList>
						</li>
						<li>
							<span>Notice Period( In Days) </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_NoticePeriodInday" Class="number" Enabled="false" onkeypress="return onlyNumbers(this);" MaxLength="4" runat="server"></asp:TextBox>
						</li>
						<li class="mobile_InboxEmpName"></li>

						<li class="trvl_date" style="padding-bottom: 20px">
							<span style="font-size: larger; text-decoration: underline">Educational Details: </span>&nbsp;&nbsp;
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



						<li class="mobile_grid" style="margin-bottom: 20px" runat="server" id="SpanEducationDetails1">
							<span runat="server" id="SpanEducationDetails"></span>
							<asp:GridView ID="GVEducationDetails" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%"
								DataKeyNames="CandEducationID">
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
									<asp:BoundField HeaderText="Qualification" DataField="EducationType" ItemStyle-HorizontalAlign="left"
										HeaderStyle-HorizontalAlign="left" ItemStyle-Width="20%" />
									<asp:BoundField HeaderText="University Name / Board" DataField="PGUniversityName" ItemStyle-HorizontalAlign="left"
										HeaderStyle-HorizontalAlign="left" ItemStyle-Width="50%" />
									<asp:BoundField HeaderText="School / College Name" DataField="CollegeName" ItemStyle-HorizontalAlign="left"
										HeaderStyle-HorizontalAlign="left" ItemStyle-Width="50%" />
									<asp:BoundField HeaderText="Year of Passing" DataField="YearofPassing" ItemStyle-HorizontalAlign="left"
										HeaderStyle-HorizontalAlign="left" ItemStyle-Width="15%" />
									<asp:BoundField HeaderText="Percentage" DataField="FinalScore" ItemStyle-HorizontalAlign="left"
										HeaderStyle-HorizontalAlign="left" ItemStyle-Width="15%" />
									<asp:BoundField HeaderText="Discipline" DataField="PGDiscipline" ItemStyle-HorizontalAlign="left"
										HeaderStyle-HorizontalAlign="left" ItemStyle-Width="15%" />
									<asp:BoundField HeaderText="Type" DataField="PGType" ItemStyle-HorizontalAlign="left"
										HeaderStyle-HorizontalAlign="left" ItemStyle-Width="15%" />
								</Columns>
							</asp:GridView>
						</li>
						<li runat="server" id="SpanEducationDetails2"></li>
						<li class="trvl_date" style="padding-bottom: 20px">
							<span style="font-size: larger; text-decoration: underline">Work experience details: </span>&nbsp;&nbsp;
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

						<li class="mobile_grid" style="margin-bottom: 20px" runat="server" id="SpanWorkExperiencedetail1">
							<span runat="server" id="SpanWorkExperiencedetail"></span>
							<asp:GridView ID="GVWorkExperiencedetail" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%"
								DataKeyNames="CandCompanyID">
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
									<asp:BoundField HeaderText="Name of Company" DataField="NameofCompany" ItemStyle-HorizontalAlign="left"
										HeaderStyle-HorizontalAlign="left" ItemStyle-Width="40%" />
									<asp:BoundField HeaderText="Cand. Designation" DataField="CandDesignation" ItemStyle-HorizontalAlign="left"
										HeaderStyle-HorizontalAlign="left" ItemStyle-Width="30%" />
									<asp:BoundField HeaderText="Start Date" DataField="StartDate" ItemStyle-HorizontalAlign="left"
										HeaderStyle-HorizontalAlign="left" ItemStyle-Width="15%" />
									<asp:BoundField HeaderText="End Date" DataField="EndDate" ItemStyle-HorizontalAlign="left"
										HeaderStyle-HorizontalAlign="left" ItemStyle-Width="15%" />
								</Columns>
							</asp:GridView>
						</li>
						<li runat="server" id="SpanWorkExperiencedetail2"></li>

						<li class="trvl_date" style="padding-bottom: 20px">
							<span style="font-size: larger; text-decoration: underline">Location Details: </span>&nbsp;&nbsp;
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


						<li>
							<span>Current Location </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_CurrentLocation" Enabled="false" MaxLength="30" runat="server"></asp:TextBox>
						</li>
						<li>
							<span>Native /Home Location </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_NativeHomeLocation" Enabled="false" MaxLength="30" runat="server"></asp:TextBox>
						</li>
						<li class="mobile_InboxEmpName">
							<span>Base Location in current company  </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_BaseLocationcurrentcompany" Enabled="false" MaxLength="50" runat="server"></asp:TextBox>
						</li>

						<li>
							<span>Base Location Preference </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList runat="server" ID="DDLBaseLocationPreference" Enabled="false">
							</asp:DropDownList>

						</li>
						<li>
							<span>Is he ready to relocate and travel to any locations in India & Abroad for project implementations </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList runat="server" ID="DDLRelocateTravelAnyLocation" Enabled="false">
								<asp:ListItem Text="-- Select --" Value="0"></asp:ListItem>
								<asp:ListItem Text="Yes" Value="1"></asp:ListItem>
								<asp:ListItem Text="No" Value="2"></asp:ListItem>
							</asp:DropDownList>
						</li>
						<li class="mobile_InboxEmpName">
							<span>Travel Contraint in Pandemic Situation  </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_TravelContraintPandemicSituation" Enabled="false" MaxLength="50" runat="server"></asp:TextBox>
						</li>


						<li class="trvl_date" style="padding-bottom: 20px">
							<span style="font-size: larger; text-decoration: underline">Employment Details: </span>&nbsp;&nbsp;
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
							<span>Open to Travel </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList runat="server" ID="DDLOpenToTravel" Enabled="false">
								<asp:ListItem Text="-- Select --" Value="0"></asp:ListItem>
								<asp:ListItem Text="Yes" Value="1"></asp:ListItem>
								<asp:ListItem Text="No" Value="2"></asp:ListItem>
							</asp:DropDownList>
						</li>
						<li style="padding-bottom: 10px">
							<span>Candidate is on whose payrolls today—IT company or third party </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList runat="server" ID="DDlpayrollsCompany" Enabled="false">
								<asp:ListItem Text="-- Select --" Value="0"></asp:ListItem>
								<asp:ListItem Text="IT Company" Value="1"></asp:ListItem>
								<asp:ListItem Text="Third Party" Value="2"></asp:ListItem>
							</asp:DropDownList>
						</li>

						<li style="padding-bottom: 15px">
							<span>How many full life E2E implementation projects have you worked on? </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList runat="server" ID="DDLImplementationprojectWorkedOn" Enabled="false">
							</asp:DropDownList>
						</li>
						<li style="padding-bottom: 15px">
							<span>What is your TOTAL Domain experience  </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList runat="server" ID="DDLDomainExperence" Enabled="false">
							</asp:DropDownList>
						</li>
						<li class="mobile_InboxEmpName" style="padding-bottom: 15px">
							<span>What is your TOTAL SAP experience  </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList runat="server" ID="DDLSAPExperence" Enabled="false">
							</asp:DropDownList>
						</li>

						<li style="padding-bottom: 15px">
							<span>How many Support Project  </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList runat="server" ID="DDLSupportproject" Enabled="false">
							</asp:DropDownList>
						</li>
						<li style="padding-bottom: 15px">
							<span>Which of the phases in implementation you have worked  </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList runat="server" ID="DDLPhaseWorkimplementation" Enabled="false">
							</asp:DropDownList>
						</li>
						<li class="mobile_InboxEmpName" style="padding-bottom: 15px">
							<span>What roles have you played in implementation projects so far?  </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList runat="server" ID="DDLRolesPlaydImplementation" Enabled="false">
							</asp:DropDownList>
						</li>

						<li class="mobile_InboxEmpName" style="padding-bottom: 10px">
							<span>What type of projects have you handled?  </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList runat="server" ID="DDLprojecthandled" Enabled="false">
							</asp:DropDownList>
						</li>

						<li style="padding-bottom: 10px">
							<span>Whether there is any break in service. If Yes - reason </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList runat="server" ID="DDLBreakInService" Enabled="false">
								<asp:ListItem Text="-- Select --" Value="0"></asp:ListItem>
								<asp:ListItem Text="Yes" Value="1"></asp:ListItem>
								<asp:ListItem Text="No" Value="2"></asp:ListItem>
							</asp:DropDownList>
						</li>

						<li class="mobile_InboxEmpName" style="padding-bottom: 15px">
							<span>Nature of Industry of the clients  </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList runat="server" ID="DDLnatureOfIndustryClient" Enabled="false">
							</asp:DropDownList>
						</li>

						<li style="padding-bottom: 15px">
							<span>Check   communication skill--Virtual  </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList runat="server" ID="DDLCommunicationSkill" Enabled="false">
							</asp:DropDownList>
						</li>

						<li style="padding-bottom: 10px">
							<span runat="server" id="SpanTxtReasonforBreak1" visible="false">Reason for Break </span>&nbsp;&nbsp;<span style="color: red" id="SpanTxtReasonforBreak" runat="server"></span>
							<br />
							<asp:TextBox AutoComplete="off" ID="TxtReasonforBreak" Visible="false" Height="50" CssClass="noresize" TextMode="MultiLine" MaxLength="200" runat="server"></asp:TextBox>
						</li>

						<li style="padding-bottom: 10px">
							<span runat="server" id="SpanTxtOtherNatureOfIndustryClient1" visible="false">Other </span>&nbsp;&nbsp;<span style="color: red" id="SpanTxtOtherNatureOfIndustryClient" runat="server"></span>
							<br />
							<asp:TextBox AutoComplete="off" Visible="false" ID="Txt_OtherNatureOfIndustryClient" Height="50" CssClass="noresize" TextMode="MultiLine" MaxLength="200" runat="server"></asp:TextBox>
						</li>

						<li style="padding-bottom: 15px">
							<span>Why is he looking for a change  </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_lookingforChange" Height="50" CssClass="noresize" TextMode="MultiLine" MaxLength="200" runat="server"></asp:TextBox>
						</li>

						<li class="mobile_InboxEmpName" style="padding-bottom: 10px">
							<span>His current Role in the organization- Consultant, Team lead, Solution architect, Project Manager.  </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_CurrentRoleorganization" Enabled="false" MaxLength="50" runat="server"></asp:TextBox>
						</li>
						<li style="padding-bottom: 15px">
							<span>Role in Domain company  </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_RoleDomaincompany" Enabled="false" MaxLength="50" runat="server"></asp:TextBox>
						</li>
						<li></li>
						<li style="padding-bottom: 15px">
							<span>Agreed for BG </span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtAgreedBD" Enabled="false" runat="server"></asp:TextBox>
						</li>
						<li style="padding-bottom: 15px">
							<span>Agreed for PDC  </span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtAgreedPDC" Enabled="false" runat="server"></asp:TextBox>
						</li>
						<li style="padding-bottom: 15px"></li>

						<%--<li style="padding-bottom: 10px">
							<span>Reason for Break </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="TxtReasonforBreak" Height="50" CssClass="noresize" Enabled="false" TextMode="MultiLine" MaxLength="200" runat="server"></asp:TextBox>
						</li>
						<li style="padding-bottom: 15px">
							<span>Why is he looking for a change  </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_lookingforChange" Height="50" Enabled="false" CssClass="noresize" TextMode="MultiLine" MaxLength="200" runat="server"></asp:TextBox>
						</li>

						<li></li>--%>


						<li class="trvl_date" style="padding-bottom: 20px">
							<span style="font-size: larger; text-decoration: underline">Attachment Details: </span>&nbsp;&nbsp;
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

						<li class="upload">
							<span>View Resume</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
							<asp:FileUpload ID="uploadfile" runat="server" Enabled="false" Visible="false" />
							<asp:LinkButton ID="lnkuplodedfileResume" runat="server" ForeColor="#ff0000" OnClientClick="return DownloadFile();"></asp:LinkButton>
							<br />
							<br />
						</li>
						<li runat="server" id="LIBlank1"></li>
						<li runat="server" id="LIBlank2"></li>

						<li class="mobile_grid" style="margin-bottom: 10px" runat="server" id="DivViewotherFiles1">
							<asp:FileUpload ID="uploadotherfile" runat="server" AllowMultiple="true" Visible="false" />
							<asp:TextBox AutoComplete="off" ID="txtprofilename" runat="server" CssClass="mobilenotextbox popup" Visible="false"> </asp:TextBox>
							<asp:LinkButton ID="lnkuplodedfilemultiple" runat="server" Visible="false"></asp:LinkButton>
							<span>Other Files</span>&nbsp;&nbsp;<br />
							<asp:GridView ID="gvotherfile" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%"
								DataKeyNames="MultipleFileID">
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
									<asp:BoundField HeaderText="File List"
										DataField="FileName"
										ItemStyle-HorizontalAlign="left"
										HeaderStyle-HorizontalAlign="left"
										ItemStyle-Width="40%" />
									<asp:TemplateField HeaderText="Files" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
										<ItemTemplate>
											<asp:LinkButton ID="lnkViewFiles" runat="server" Text='View' OnClientClick=<%# "DownloadFilemultiple('" + Eval("FileName") + "')" %>></asp:LinkButton>
										</ItemTemplate>
										<ItemStyle HorizontalAlign="Center" />
									</asp:TemplateField>
								</Columns>
							</asp:GridView>
						</li>

						<li runat="server" id="DivViewotherFiles2"></li>

						<li class="mobile_grid" runat="server" id="DivViewotherIRSheetFile1">
							<span>View Other IR-Sheet File</span><br />
							<asp:GridView ID="GVUploadOtherFilesIRSheet" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%"
								DataKeyNames="OtherfileAttach_ID">
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
									<asp:BoundField HeaderText="File List"
										DataField="OtherFiles"
										ItemStyle-HorizontalAlign="left"
										HeaderStyle-HorizontalAlign="left"
										ItemStyle-Width="40%" />
									<asp:TemplateField HeaderText="Files" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
										<ItemTemplate>
											<asp:LinkButton ID="lnkViewFilesIRSheet" runat="server" Text='View' OnClientClick=<%# "DownloadFilemultipleIRSheet('" + Eval("OtherFiles") + "')" %>></asp:LinkButton>
										</ItemTemplate>
										<ItemStyle HorizontalAlign="Center" />
									</asp:TemplateField>
								</Columns>
							</asp:GridView>
						</li>
						<li runat="server" id="DivViewotherIRSheetFile2"></li>
						<li runat="server" id="DivViewotherIRSheetFile3"></li>

					</ul>

				</div>
			</div>

			<div class="edit-contact" runat="server" id="DivJoiningDetails1" visible="false">

				<ul id="Ul4" runat="server">
					<li class="trvl_local">
						<asp:LinkButton ID="Rec_AddjoinDetailbtn" runat="server" Text="-" ToolTip="Add Joining Detail" OnClick="Rec_AddjoinDetailbtn_Click" CssClass="Savebtnsve"></asp:LinkButton>
						<span id="Span1" runat="server">Add Joining Detail</span>
					</li>
					<li></li>
					<li></li>
				</ul>
			</div>

			<div runat="server" id="DivJoiningDetails2" visible="false">
				<div class="edit-contact">
					<ul id="Ul1" runat="server">
						<li class="mobile_InboxEmpName" style="margin-bottom: 10px" runat="server" id="LIStatusUpdate">
							<span>Status Update </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList runat="server" ID="DDLStatusUpdate" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="DDLStatusUpdate_SelectedIndexChanged">
							</asp:DropDownList>
						</li>

						<li>
							<div id="joiningEmpType" runat="server" visible="false">
								<span>Employment Type </span>&nbsp;&nbsp;<span style="color: red">*</span>
								<br />
								<asp:DropDownList runat="server" ID="lstJoinEmploymentType" Width="250px">
								</asp:DropDownList>
							</div>
						</li>
						<li>
							<div id="joinBand" runat="server" visible="false">
								<span>Band</span>&nbsp;&nbsp;<span style="color: red">*</span>
								<br />
								<asp:DropDownList runat="server" ID="lstjoinband" Width="250px">
								</asp:DropDownList>
							</div>
						</li>

						<li class="mobile_InboxEmpName" runat="server" id="LIJoiningDate">
							<span>Date</span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_JoiningDate" MaxLength="50" runat="server"></asp:TextBox>
							<ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="Txt_JoiningDate"
								OnClientDateSelectionChanged="checkDate" runat="server">
							</ajaxToolkit:CalendarExtender>
						</li>
						<li>
							<div id="JoiningDate" runat="server" visible="false">
								<span>Joining Date</span>&nbsp;&nbsp;<span style="color: red">*</span>
								<br />
								<asp:TextBox AutoComplete="off" ID="txtcandjoindate" MaxLength="10" runat="server"></asp:TextBox>
								<ajaxToolkit:CalendarExtender ID="CalendarExtender4" Format="dd/MM/yyyy" TargetControlID="txtcandjoindate"
									runat="server">
								</ajaxToolkit:CalendarExtender>
							</div>
						</li>
						<li></li>
						<li class="mobile_InboxEmpName">
							<span runat="server" id="LIRecruiterComment">Recruiter comment</span>
							&nbsp;&nbsp;<span style="color: red" id="SpanRecruitercomment" runat="server" visible="false">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtRecruitercomment" runat="server" MaxLength="180" TextMode="MultiLine" Rows="5" CssClass="noresize"></asp:TextBox>
						</li>
						<li></li>
						<li></li>

						<li class="upload" runat="server" id="LI1JoiningdetailCandidate" visible="false">
							<span style="margin-bottom: 10px">Upload File</span>&nbsp;&nbsp;<span style="color: red" id="FileValidation" runat="server">*</span><br />
							<asp:FileUpload ID="FileUpload1" runat="server" Width="200px" />
						</li>
						<li runat="server" id="LI2JoiningdetailCandidate" visible="false"></li>
						<li runat="server" id="LI3JoiningdetailCandidate" visible="false"></li>


						<li></li>
						<li></li>
						<li></li>
					</ul>
				</div>

				<div style="padding-left: 20%; padding-bottom: 30px" runat="server" id="DivMessageJoining" visible="false">
					<asp:Label runat="server" ID="lblmessageJoining" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
				</div>

				<div style="padding-left: 30%">

					<asp:LinkButton ID="trvl_accmo_btn" runat="server" Text="Offer Create" ToolTip="Offer Create" Visible="false" CssClass="Savebtnsve" OnClick="trvl_accmo_btn_Click"></asp:LinkButton>


					<asp:LinkButton ID="JobDetail_btnSave" runat="server" Text="Joining Detail Save" ToolTip="Joining Detail Save"
						CssClass="Savebtnsve" OnClick="JobDetail_btnSave_Click" OnClientClick="return SaveMultiJobJoiningClick();"></asp:LinkButton>
				</div>
			</div>
			<br />

			<div class="manage_grid" style="width: 80%; height: auto; padding-left: 40px" runat="server" id="DivJoiningDetailInformation" visible="false">
				<asp:GridView ID="GVJoiningDetailInformation" runat="server" OnRowDataBound="GVJoiningDetailInformation_RowDataBound" BackColor="White" BorderColor="Navy" BorderStyle="solid" BorderWidth="1px"
					DataKeyNames="RecJobJoiningID" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false">
					<FooterStyle BackColor="White" ForeColor="#000066" />
					<HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
					<PagerStyle ForeColor="#000066" HorizontalAlign="right" BackColor="White" />
					<RowStyle ForeColor="#000066" />
					<SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
					<SortedAscendingCellStyle BackColor="#F1F1F1" />
					<SortedAscendingHeaderStyle BackColor="#007DBB" />
					<SortedDescendingCellStyle BackColor="#CAC9C9" />
					<SortedDescendingHeaderStyle BackColor="#00547E" />
					<Columns>
						<asp:BoundField HeaderText="CandidateID" DataField="CandidateID" ItemStyle-HorizontalAlign="left"
							HeaderStyle-HorizontalAlign="left" ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" Visible="false" />
						<asp:BoundField HeaderText="Date" DataField="JoiningDate" ItemStyle-HorizontalAlign="center"
							HeaderStyle-HorizontalAlign="center" ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
						<asp:BoundField HeaderText="Status Update" DataField="StatusUpdate" ItemStyle-HorizontalAlign="center"
							ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />
						<asp:BoundField HeaderText="Comment" DataField="RecruiterComments" ItemStyle-HorizontalAlign="center"
							ItemStyle-Width="20%" ItemStyle-BorderColor="Navy" />
						<asp:BoundField HeaderText="Employment Type" DataField="Particulars" ItemStyle-HorizontalAlign="center"
							ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
						<asp:BoundField HeaderText="Band" DataField="BAND" ItemStyle-HorizontalAlign="center"
							ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />
						<asp:BoundField HeaderText="Joining Date" DataField="CandJoiningDate" ItemStyle-HorizontalAlign="center"
							ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />


						<asp:TemplateField HeaderText="Files" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="5%">
							<ItemTemplate>
								<asp:ImageButton ID="lnkAcceptanceFile" runat="server" Width="15px" ToolTip="View File" Height="15px"
									ImageUrl="~/Images/Download.png" Visible='<%# String.IsNullOrEmpty(Convert.ToString(Eval("AcceptanceFile"))) ? false : true %>'
									OnClientClick=<%# "DownloadFilemultipleAcceptanceFile('" + Eval("AcceptanceFile") + "')"%> />

								<%--<asp:LinkButton ID="lnkAcceptanceFile" runat="server" Text='<%# Eval("AcceptanceFile") %>' OnClientClick=<%# "DownloadFilemultipleAcceptanceFile('" + Eval("AcceptanceFile") + "')" %>  ></asp:LinkButton>
								
								--%>
							</ItemTemplate>
							<ItemStyle HorizontalAlign="Center" />
						</asp:TemplateField>
					</Columns>
				</asp:GridView>
			</div>
			<div class="edit-contact">
				<ul id="Ul5" runat="server">
					<%--Offer Approval edit data--%>
					<div style="padding-left: 0px;">
						<span runat="server" id="Span2" visible="false">Offer Created List  </span>
						<asp:GridView ID="GRDOfferCreatelist" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%"
							DataKeyNames="Offer_App_ID">
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
								<asp:BoundField HeaderText="Offer Date"
									DataField="Offer_Date"
									ItemStyle-HorizontalAlign="left"
									HeaderStyle-HorizontalAlign="Center"
									ItemStyle-Width="8%" />

								<asp:BoundField HeaderText="Position Title"
									DataField="PositionTitle"
									ItemStyle-HorizontalAlign="left"
									HeaderStyle-HorizontalAlign="Center"
									ItemStyle-Width="15%" />

								<asp:BoundField HeaderText="Offered Band"
									DataField="OfferBAND"
									ItemStyle-HorizontalAlign="left"
									HeaderStyle-HorizontalAlign="Center"
									ItemStyle-Width="5%" />

								<asp:BoundField HeaderText="Total CTC Offered (LPA)"
									DataField="OldCTC"
									ItemStyle-HorizontalAlign="left"
									HeaderStyle-HorizontalAlign="Center"
									ItemStyle-Width="9%" />

								<asp:BoundField HeaderText="CTC as per Band Eligibility & Other Allowances (Max Amount) LP"
									DataField="NewCTC"
									ItemStyle-HorizontalAlign="left"
									HeaderStyle-HorizontalAlign="Center"
									ItemStyle-Width="10%" />

								<asp:BoundField HeaderText="Exception Amount in LPA "
									DataField="ExceptionAmount"
									ItemStyle-HorizontalAlign="left"
									HeaderStyle-HorizontalAlign="left"
									ItemStyle-Width="8%" />

								<asp:BoundField HeaderText="Comments"
									DataField="Comment"
									ItemStyle-HorizontalAlign="left"
									HeaderStyle-HorizontalAlign="Center"
									ItemStyle-Width="22%" />

								<asp:BoundField HeaderText="Probable Joining Date"
									DataField="ProbableJoiningDate"
									ItemStyle-HorizontalAlign="left"
									HeaderStyle-HorizontalAlign="Center"
									ItemStyle-Width="10%" />

								<%--<asp:BoundField HeaderText="Recruitment Charges"
									DataField="RecruitmentCharges"
									ItemStyle-HorizontalAlign="left"
									HeaderStyle-HorizontalAlign="Center"
									ItemStyle-Width="3%" />

								<asp:BoundField HeaderText="Is Exception"
									DataField="IsException"
									ItemStyle-HorizontalAlign="left"
									HeaderStyle-HorizontalAlign="Center"
									ItemStyle-Width="3%" />--%>
								<asp:BoundField HeaderText=" Offer Status"
									DataField="Request_status"
									ItemStyle-HorizontalAlign="left"
									HeaderStyle-HorizontalAlign="Center"
									ItemStyle-Width="6%" />

								<asp:BoundField HeaderText="Candidate Status"
									DataField="Candidate_Status"
									ItemStyle-HorizontalAlign="left"
									HeaderStyle-HorizontalAlign="Center"
									ItemStyle-Width="6%" />

								<asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="1%">
									<ItemTemplate>
										<asp:ImageButton ID="lnkOfferedit" runat="server" ToolTip=" File View" Width="15px" Height="15px" OnClick="lnnOfferedit_Click" ImageUrl="~/Images/edit.png" />
									</ItemTemplate>
									<ItemStyle HorizontalAlign="Center" />
								</asp:TemplateField>
							</Columns>
						</asp:GridView>
					</div>
					<li>
						<br />
						<span runat="server" id="OfferhistoryS" visible="false">Offer Approval History  </span>
					</li>
					<li></li>
					<li></li>
				</ul>
			</div>
			<div style="padding-left: 42px;">
				<asp:GridView ID="GRDOfferHistory" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="95%"
					DataKeyNames="OtherFileAttach_ID">
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
						

						<asp:BoundField HeaderText="Approver Name"
							DataField="Emp_Name"
							ItemStyle-HorizontalAlign="left"
							HeaderStyle-HorizontalAlign="left"
							ItemStyle-Width="15%" />

						<asp:BoundField HeaderText="Approved On"
							DataField="Offer_Date"
							ItemStyle-HorizontalAlign="left"
							HeaderStyle-HorizontalAlign="left"
							ItemStyle-Width="8%" />

						<asp:BoundField HeaderText=" Offer Status"
							DataField="Request_status"
							ItemStyle-HorizontalAlign="left"
							HeaderStyle-HorizontalAlign="left"
							ItemStyle-Width="8%" />

						<asp:BoundField HeaderText=" Offer Approval Comment"
							DataField="appr_comments"
							ItemStyle-HorizontalAlign="left"
							HeaderStyle-HorizontalAlign="left"
							ItemStyle-Width="30%" />

						<asp:BoundField HeaderText=" Offer File List" ItemStyle-CssClass="OfferFileUplad"
							DataField="Offer_Approval_File"
							ItemStyle-HorizontalAlign="left"
							HeaderStyle-HorizontalAlign="left"
							ItemStyle-Width="20%" />

						<asp:TemplateField HeaderText="Files View" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="5%">
							<ItemTemplate>
								<asp:ImageButton ID="lnkViewFiles" runat="server" ToolTip=" File View" Width="15px" Height="15px" ImageUrl="~/Images/Download.png" Visible='<%# String.IsNullOrEmpty(Convert.ToString(Eval("Offer_Approval_File"))) ? false : true %>' OnClientClick=<%# "DownloadmultipleOfferH('" + Eval("Offer_Approval_File") + "')" %> />
							</ItemTemplate>
							<ItemStyle HorizontalAlign="Center" />
						</asp:TemplateField>
					</Columns>
				</asp:GridView>
			</div>
			<div id="OfferCreate" runat="server" visible="false">
				<div class="edit-contact">
					<ul>
						<li class="trvl_date" style="padding-bottom: 20px">

							<asp:Label ID="lblOfferCreate" Text="Offer Create" runat="server" Style="font-size: larger; text-decoration: underline" />
							<br />
						</li>
						<li>
							<asp:Label runat="server" ID="lblOffer" Visible="True" Style="color: red; font-size: 14px; font-weight: 500; text-align: center;"></asp:Label>
						</li>
						<li></li>
							<div style="padding-left: 0px;">

							<asp:GridView ID="GRDGenerate_Offer" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="65%"
								DataKeyNames="TempLatterID,Offer_App_ID">
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

									<asp:BoundField HeaderText="CTC Per Annum Including PLP" ItemStyle-CssClass="Comma1"
										DataField="CTC_PER_ANNUM_INCLUDING_PLP"
										ItemStyle-HorizontalAlign="left"
										HeaderStyle-HorizontalAlign="left"
										ItemStyle-Width="10%" />

									<asp:BoundField HeaderText="PLP/Varible Pay" ItemStyle-CssClass="Comma1"
										DataField="PLP_VARIABLE_PAY"
										ItemStyle-HorizontalAlign="left"
										HeaderStyle-HorizontalAlign="left"
										ItemStyle-Width="10%" />

									<asp:BoundField HeaderText="CTC Per Annum"
										DataField="CTC_PER_ANNUM"
										ItemStyle-HorizontalAlign="left"
										HeaderStyle-HorizontalAlign="left"
										ItemStyle-Width="10%" />

									<asp:BoundField HeaderText="CTC Per Month"
										DataField="CTC_PER_MONTH"
										ItemStyle-HorizontalAlign="left"
										HeaderStyle-HorizontalAlign="left"
										ItemStyle-Width="10%" />
									
									<asp:BoundField HeaderText="Gross Payslip"
										DataField="TOTAL1"
										ItemStyle-HorizontalAlign="left"
										HeaderStyle-HorizontalAlign="left"
										ItemStyle-Width="10%" />

									<asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="5%">
										<ItemTemplate>
											<asp:ImageButton ID="lnkGenerateFiles" runat="server" ToolTip=" File View" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkGenerateFiles_Click" />
										</ItemTemplate>
										<ItemStyle HorizontalAlign="Center" />
									</asp:TemplateField>

									<asp:TemplateField HeaderText="Download Offer" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="5%">
										<ItemTemplate>
											<asp:ImageButton ID="lnkOffersFiles" runat="server" ToolTip="Download Offer" Width="15px" Height="15px" ImageUrl="~/Images/Download.png" OnClick="lnkOffersFiles_Click" Visible='<%# String.IsNullOrEmpty(Convert.ToString(Eval("Candidate_Status"))) ? false : true %>' />
										</ItemTemplate>
										<ItemStyle HorizontalAlign="Center" />
									</asp:TemplateField>
								</Columns>
							</asp:GridView>
							<br />
						</div>
						<li class="mobile_inboxEmpCode">
							<span>Offer Date  </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtOfferDate" runat="server" CssClass="OfferDates"></asp:TextBox>
							<ajaxToolkit:CalendarExtender ID="CalendarExtender3" Format="dd/MM/yyyy" TargetControlID="txtOfferDate" OnClientDateSelectionChanged="checkOffeDate"
								runat="server">
							</ajaxToolkit:CalendarExtender>
						</li>
						<li>
							<span>Offer Position Title </span>&nbsp;&nbsp;<span style="color: red"> *</span>
							<br />
							<%--<asp:TextBox AutoComplete="off" ID="txtpositionOffer" runat="server" Enabled="false"></asp:TextBox>--%>
						   <asp:DropDownList runat="server" ID="lstOfferPositionName" CssClass="DropdownListSearch" Width="98%">
							</asp:DropDownList>
						</li>
						<li><span>Offer Position Location </span>&nbsp;&nbsp;<span style="color: red"> *</span>
							<br />							
						   <asp:DropDownList runat="server" ID="lstOfferLocation" CssClass="DropdownListSearch" Width="98%">
							</asp:DropDownList></li>

                        <li><span>Offer Office Location </span>&nbsp;&nbsp;<span style="color: red"> *</span>
							<br />							
						   <asp:DropDownList runat="server" ID="lstOfferOfficeLocation" CssClass="DropdownListSearch" Width="98%">
							</asp:DropDownList></li>
						<li></li>
                        <li></li>

                        <li></li>
                        <li></li>
                        <li></li> 
						<li>
							<span>Offered Band </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList runat="server" ID="lstOfferBand" CssClass="DropdownListSearch" Width="98%" AutoPostBack="true" OnSelectedIndexChanged="lstOfferBand_SelectedIndexChanged">
							</asp:DropDownList>
						</li>
						<li style="padding-bottom:15px;"><span>Employment Type </span>&nbsp;&nbsp;<span style="color: red">*</span>
						<br />		
						<asp:DropDownList runat="server"  ID="ddl_Offer_EmploymentType" CssClass="DropdownListSearch" Width="98%" AutoPostBack="true" OnSelectedIndexChanged="ddl_Offer_EmploymentType_SelectedIndexChanged" >
								</asp:DropDownList></li>
						<li>
							<asp:LinkButton ID="btn_Generate_Offer" runat="server"  Text="Generate Offer" ToolTip="Generate Offer" CssClass="Savebtnsve" Style="padding: 7px 15px 7px 15px; margin: 0px 0px 15px 0px; display:none"></asp:LinkButton>
                           
						</li>
						<li>
							<span>Total CTC Offered (LPA) </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtOfferno1" runat="server" AutoPostBack="true" CssClass="number" OnTextChanged="txtOfferno1_TextChanged"></asp:TextBox>
						</li>
						<li>
							<span>CTC as per Band Eligibility & Other Allowances (Max Amount) LP </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtOfferno2" runat="server" AutoPostBack="true" CssClass="number" OnTextChanged="txtOfferno1_TextChanged"></asp:TextBox>
						</li>
						<li>
							<span>Exception Amount in LPA </span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtExceptionamt" runat="server" Enabled="false"></asp:TextBox>
						</li>
						<li class="mobile_inboxEmpCode">
							<span>Comments  </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtOfferAppcmt" Rows="3" runat="server" TextMode="MultiLine" CssClass="noresize" Width="288%" onKeyUp="javascript:Count(this);"></asp:TextBox>
						</li>
						<li></li>
						<li></li>
						<li class="mobile_inboxEmpCode">
							<span>Probable Joining Date </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtProbableJoiningDate" runat="server" CssClass="OfferDates" OnTextChanged="txtProbableJoiningDate_TextChanged" AutoPostBack="true"></asp:TextBox>
							<ajaxToolkit:CalendarExtender ID="CalendarExtender5" Format="dd/MM/yyyy" TargetControlID="txtProbableJoiningDate" runat="server">
							</ajaxToolkit:CalendarExtender>
						</li>
						<li>
							<span>Recruitment Charges </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<asp:DropDownList runat="server" ID="lstRecruitmentCharges" CssClass="DropdownListSearch" Width="97%">
								<asp:ListItem Text="Select Recruitment Charges" Value="0"></asp:ListItem>
								<asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
								<asp:ListItem Text="No" Value="No"></asp:ListItem>
							</asp:DropDownList>

						</li>
						<li class="mobile_InboxEmpName" style="padding-left: 20px">
							<asp:CheckBox ID="chk_exception" runat="server" Text="Is Exception" />
						</li>
                        
                        <li>
                            <span>Offer Acceptance By Date </span>&nbsp;&nbsp;
							<br />
							<asp:TextBox AutoComplete="off" ID="TxtOfferAcceptanceByDate" runat="server" CssClass="OfferDates" ></asp:TextBox>
							<ajaxToolkit:CalendarExtender ID="CalendarExtender6" Format="dd/MM/yyyy" TargetControlID="TxtOfferAcceptanceByDate" runat="server">
							</ajaxToolkit:CalendarExtender>

                        </li>
                        <li>
                             <asp:LinkButton ID="btn_ViewDraft_Offer" runat="server"  Text="View Draft Offer" ToolTip="View Draft Offer" CssClass="Savebtnsve"  Style="padding: 7px 15px 7px 15px; color:blue; margin: 0px 0px 15px 0px; display:none"  OnClick="btn_ViewDraft_Offer_Click"></asp:LinkButton>

                        </li>
                        <li></li>
						<li class="mobile_InboxEmpName">
							<span runat="server" id ="FileSupport1">Other files </span>&nbsp;&nbsp;<span style="color: red" runat="server" id="FileSupport">*</span>
							<asp:FileUpload ID="FileOfferUpload" runat="server" AllowMultiple="true"  Width="210px"/>
							<br />
						</li>
						<br />
						<br />
						<asp:GridView ID="DgvOfferApprover" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="75%">
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

					</ul>
				</div>
				<div class="mobile_Savebtndiv">
					<asp:LinkButton ID="mobile_btnSave" runat="server" Text="Save" ToolTip="Send for Approval" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click" OnClientClick="return SaveMultiClickOffer();">Send for Approval</asp:LinkButton>

				</div>



			</div>
			<div class="mobile_Savebtndiv" id="DivButton" runat="server" visible="false">
				<asp:LinkButton ID="trvldeatils_btnSave" runat="server" Text="Save" ToolTip="Save" Visible="false"
					CssClass="Savebtnsve" OnClick="trvldeatils_btnSave_Click" OnClientClick="return SaveMultiClick();"></asp:LinkButton>
				<asp:LinkButton ID="mobile_btnBack" runat="server" Text="Close" ToolTip="Close" OnClick="mobile_btnBack_Click"
					CssClass="Savebtnsve">Close</asp:LinkButton>
			</div>
			<br />
		</div>
	</div>
	<%--panel IR sheet--%>
	<asp:Panel ID="PnlIrSheet" runat="server" CssClass="IRmodalPopup" Style="display: none" Height="400px">
		<div id="Div2" runat="server" style="max-height: 500px; overflow: auto;">
			<div class="userposts">
				<span>
					<asp:Label ID="Label1" runat="server" Text="IR Sheet Summary"></asp:Label>
				</span>

			</div>
			<div>
				<span><a href="#" id="btBack" title="Back" class="aaaa" style="margin-right: 30px">Back</a>
				</span>
			</div>


			<table class="TLQuestio">
				<tr>
					<td>
						<span>Requisition Number  </span>
						<br />
						<asp:TextBox AutoComplete="off" ID="txtRec_No" runat="server" Enabled="false"></asp:TextBox>
					</td>

					<td><span>Position Title </span>
						<br />
						<asp:TextBox AutoComplete="off" ID="txtpostionTitle" runat="server" Enabled="false"></asp:TextBox>
					</td>
					<td><span>Position Interviewed for </span>
						<br />
						<asp:TextBox AutoComplete="off" ID="txtPositionInterviwed" runat="server" Enabled="false"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td><span>Candidate's Name </span>
						<br />
						<asp:TextBox AutoComplete="off" ID="txtCandidateName" runat="server" Enabled="false"></asp:TextBox>
					</td>
					<td><span>Total Experience (In Year)</span>
						<br />
						<asp:TextBox AutoComplete="off" ID="txttotalExperince" runat="server" Enabled="false"></asp:TextBox>
					</td>
					<td><span>Relevant Experience (In Year) </span>
						<br />
						<asp:TextBox AutoComplete="off" ID="txtRelevantExp" runat="server" Enabled="false"></asp:TextBox>
					</td>
				</tr>
			</table>
			<asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<asp:GridView ID="DgvIrSheetSummary" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
						DataKeyNames="Main_Type_ID,Ishedeing,SubType_Rating,SubType_ID" Width="98%" OnRowDataBound="DgvIrSheetSummary_RowDataBound">
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
							<asp:BoundField HeaderText="Name" DataField="SubType_ID" ItemStyle-HorizontalAlign="left"
								HeaderStyle-HorizontalAlign="left" ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" Visible="false" />

							<asp:BoundField HeaderText="Competency"
								DataField="heading"
								ItemStyle-HorizontalAlign="left"
								HeaderStyle-HorizontalAlign="left"
								ItemStyle-Width="30%"
								ItemStyle-BorderColor="Navy" />
						</Columns>
					</asp:GridView>
				</ContentTemplate>
			</asp:UpdatePanel>
			<br />
			<br />
			<asp:GridView ID="GrdIRIntSummary" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
				DataKeyNames="Rec_Main_Irsheet_ID" Width="98%" ShowFooter="false">
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

					<asp:BoundField HeaderText="Interviewer level"
						DataField="InterviewRound"
						ItemStyle-HorizontalAlign="left"
						HeaderStyle-HorizontalAlign="left"
						ItemStyle-Width="10%"
						ItemStyle-BorderColor="Navy" />


					<asp:BoundField HeaderText="Overall Rating"
						DataField="RatingName"
						ItemStyle-HorizontalAlign="left"
						HeaderStyle-HorizontalAlign="left"
						ItemStyle-Width="10%"
						ItemStyle-BorderColor="Navy" />

					<asp:BoundField HeaderText="Selection Recommendation"
						DataField="Selection_Recommendation"
						ItemStyle-HorizontalAlign="left"
						HeaderStyle-HorizontalAlign="left"
						ItemStyle-Width="10%"
						ItemStyle-BorderColor="Navy" />

					<asp:BoundField HeaderText="Notes if any"
						DataField="Notes"
						ItemStyle-HorizontalAlign="left"
						HeaderStyle-HorizontalAlign="left"
						ItemStyle-Width="30%"
						ItemStyle-BorderColor="Navy" />

					<asp:BoundField HeaderText="Interviewr Remarks"
						DataField="InterviewrRemarks"
						ItemStyle-HorizontalAlign="left"
						HeaderStyle-HorizontalAlign="left"
						ItemStyle-Width="25%"
						ItemStyle-BorderColor="Navy" />

					<asp:BoundField HeaderText="Name of the Interviewer"
						DataField="Emp_Name"
						ItemStyle-HorizontalAlign="left"
						HeaderStyle-HorizontalAlign="left"
						ItemStyle-Width="15%"
						ItemStyle-BorderColor="Navy" />
				</Columns>
			</asp:GridView>

			<br />
			<br />
			<br />


			<div class="IRSheetBtn">
				<asp:LinkButton ID="mobile_cancel" runat="server" ToolTip="Back" CssClass="Savebtnsve">Back</asp:LinkButton>

			</div>
		</div>
	</asp:Panel>
	<%--panel Generate Offer latter--%>
	<asp:Panel ID="pnlGenerateOffer" runat="server" CssClass="IRmodalPopup" Style="display: none" Height="400px">
		<div id="Div3" runat="server" style="max-height: 500px; overflow: auto;">
			<div class="OfferHeading" style="">
				<span>
					<asp:Label ID="Label2" runat="server" Text="Compensation Structure"></asp:Label>
				</span>
				<div>
					<asp:Label runat="server" ID="lblGenerateMsg" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
				</div>
			</div>
			<span>
				<a href="#" id="btn_Offer_Back1" title="Back" class="aaaa" style="margin-right: 30px; margin-bottom: 10px">Back</a>
			</span>
			<div>
			</div>
			<table class="TLOffer1">
				<tr>
					<td>
						<span>Candidate Name As Aadhar.</span> &nbsp&nbsp <span style="color: red">*</span>
						<br />
						<asp:TextBox AutoComplete="off" ID="txtOffer_Can_Name" style="width:40%" CssClass="Offermargin" runat="server" ></asp:TextBox>
					</td>
					<td>

					</td> 
					</tr>
				<tr>
					<td> 
						<span>Candidate Address As Aadhar.</span> &nbsp&nbsp <span style="color: red">*</span>
						<br />
						<asp:TextBox AutoComplete="off" ID="txtCandAddress" style="width:40%" CssClass="Offermargin noresize" runat="server" Rows="5"  onKeyUp="javascript:Count(this);" TextMode="MultiLine"></asp:TextBox>
					
					</td>
					<td> </td> 
				</tr>
				<tr style="display: none">
					<td><span>Band</span>&nbsp&nbsp <span style="color: red">*</span>
						<br />
						<asp:TextBox AutoComplete="off" ID="TextBox1" CssClass="Offermargin" runat="server"></asp:TextBox>
					</td>
					<td><span>Designation </span>&nbsp&nbsp <span style="color: red">*</span>
						<br />
						<asp:TextBox AutoComplete="off" ID="txt_Offer_Designation" CssClass="Offermargin" runat="server" onpaste="return false"></asp:TextBox>
					</td>


				</tr>
			</table>
			<br />
			<asp:UpdatePanel ID="updatePanel1" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<asp:HiddenField ID="hdnOffer_Generate" runat="server" />
					<asp:HiddenField ID="hdnTempLatterID" runat="server" />
					<table class="TLOffer">
						<tr>
							<td rowspan="3">Monthly Payments - taxability as per applicable Income Tax rule</td>
							<td><span>Basic </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Offer_Basic" CssClass="Offermargin number" MaxLength="10" onpaste="return false" runat="server" AutoPostBack="true" OnTextChanged="txt_Offer_Basic_TextChanged"></asp:TextBox>
							</td>

						</tr>
						<tr>
							<td><span>House Rent Allowance (HRA) </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td><span id="SP_HRA" runat="server"></span>
								<asp:HiddenField ID="hdn_HRA_Per" runat="server" />
								<asp:HiddenField ID="hdn_HRA_StructureID" runat="server" />
							</td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Offer_HRA" CssClass="Offermargin number" MaxLength="10" onpaste="return false" runat="server" Enabled="false"></asp:TextBox>
							</td>

						</tr>
						<tr >
							<td><span>Special Allowance </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Offer_Special_Allowance" MaxLength="10" CssClass="Offermargin number" onpaste="return false" runat="server" AutoPostBack="true" onkeypress="return validateFloatKeyPress(this,event);" OnTextChanged="txt_Offer_Special_Allowance_TextChanged"></asp:TextBox>
							</td>

						</tr>
						<tr class="HideOffer">
							<td><span>Conveyance </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Offer_Conveyance" MaxLength="10" CssClass="Offermargin number" onpaste="return false" runat="server" AutoPostBack="true" onkeypress="return validateFloatKeyPress(this,event);" OnTextChanged="txt_Offer_ADHOC_Allowance_TextChanged"></asp:TextBox>
							</td>

						</tr>
						<tr class="HideOffer">
							<td><span>ADHOC Allowance a </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Offer_ADHOC_Allowance" MaxLength="10" CssClass="Offermargin number" onpaste="return false" runat="server" AutoPostBack="true" onkeypress="return validateFloatKeyPress(this,event);" OnTextChanged="txt_Offer_ADHOC_Allowance_TextChanged"></asp:TextBox>
							</td>

						</tr>
						<tr class="HideOffer">
							<td><span>Skill Allowance </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td></td>
							<td>
								<asp:HiddenField ID="hdn_band" runat="server" />
								<asp:HiddenField ID="hdn_Skill_ALL_StructureID" runat="server" />
								<asp:TextBox AutoComplete="off" ID="txt_offer_Skill_Allowance" MaxLength="10" CssClass="Offermargin number" onpaste="return false" runat="server" onkeypress="return validateFloatKeyPress(this,event);"  AutoPostBack="true" OnTextChanged="txt_Offer_ADHOC_Allowance_TextChanged"></asp:TextBox>
							</td>

						</tr>
						<tr class="HideOffer">
							<td><span>Superannucation Allowance b </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td><span id="SP_Superannucation_All" runat="server"></span>
								<asp:HiddenField ID="hdn_Superan_All" runat="server" />
								<asp:HiddenField ID="hdn_Superan_All_StructureID" runat="server" />
							</td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Offer_Superannucation" MaxLength="10" CssClass="Offermargin number" onpaste="return false" runat="server" AutoPostBack="true" onkeypress="return validateFloatKeyPress(this,event);" OnTextChanged="txt_Offer_ADHOC_Allowance_TextChanged"></asp:TextBox>
							</td>

						</tr>
						<tr class="HideOffer">
							<td><span>Certificate Allowance </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Offer_Certificate_All" MaxLength="10" CssClass="Offermargin number" onpaste="return false" runat="server" AutoPostBack="true" onkeypress="return validateFloatKeyPress(this,event);" OnTextChanged="txt_Offer_ADHOC_Allowance_TextChanged"></asp:TextBox>
							</td>

						</tr>
						<tr class="HideOffer">
							<td><span>Multi Skill Allowance </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Offer_Multi_Skill_All" MaxLength="10" CssClass="Offermargin number" onkeypress="return validateFloatKeyPress(this,event);" onpaste="return false" runat="server" AutoPostBack="true" OnTextChanged="txt_Offer_ADHOC_Allowance_TextChanged"></asp:TextBox>
							</td>

						</tr>
						<tr class="HideOffer">
							<td><span>Additional Skill Allowance </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Offer_Additional_Skill" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" AutoPostBack="true" onkeypress="return validateFloatKeyPress(this,event);" OnTextChanged="txt_Offer_ADHOC_Allowance_TextChanged"></asp:TextBox>
							</td>

						</tr>
						<tr class="HideOffer">
							<td><span>Car Allowance </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txtCar_All" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" AutoPostBack="true" onkeypress="return validateFloatKeyPress(this,event);" OnTextChanged="txt_Offer_ADHOC_Allowance_TextChanged"></asp:TextBox>
							</td>

						</tr>
						<tr class="HideOffer">
							<td><span>Food Allowance </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt__Offer_Food_All" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" AutoPostBack="true" onkeypress="return validateFloatKeyPress(this,event);"  OnTextChanged="txt_Offer_ADHOC_Allowance_TextChanged"></asp:TextBox>
							</td>

						</tr>
						<tr class="Total">
							<td></td>
							<td><span>Total 1 </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td>Gross Payslip</td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Offer_Total1" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>

						</tr>
						<tr>
							<td rowspan="4">Retirals - taxability as per applicable Income tax rule</td>
							<td><span>LTA </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td><span id="SP_LTA" runat="server"></span>
								<asp:HiddenField ID="hdn_LTA" runat="server" />
								<asp:HiddenField ID="hdn_LTA_StructureID" runat="server" />
							</td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Offer_LTA" MaxLength="10" CssClass="Offermargin number" runat="server"  onpaste="return false" Enabled="false"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td><span>Medical </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_offer_Medical" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" AutoPostBack="true" onkeypress="return validateFloatKeyPress(this,event);" OnTextChanged="txt_Offer_LTA_TextChanged"></asp:TextBox>
							</td>

						</tr>
						<tr>
							<td><span>Driver Salary </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Offer_Driver_Salary" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" AutoPostBack="true" onkeypress="return validateFloatKeyPress(this,event);" OnTextChanged="txt_Offer_LTA_TextChanged"></asp:TextBox>
							</td>

						</tr>
						<tr>
							<td><span>Car lease  </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Offer_Car_lease" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" AutoPostBack="true" onkeypress="return validateFloatKeyPress(this,event);" OnTextChanged="txt_Offer_LTA_TextChanged"></asp:TextBox>
							</td>

						</tr>
						<tr class="Total">
							<td></td>
							<td><span>Total 2 </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Offer_Total2" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>

						</tr>
						<tr>
							<td rowspan="2">Facilities not convertible into cash - notional figures</td>
							<td><span>PF </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td><span id="SP_PF" runat="server"></span>
								<asp:HiddenField ID="hdn_PF" runat="server" />
								<asp:HiddenField ID="hdn_PF_StructureID" runat="server" />
							</td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Offer_PF" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>

						</tr>
						<tr>
							<td><span>Gratuity b </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td><span id="SP_Gratuity" runat="server"></span>
								<asp:HiddenField ID="hdn_Gratuity" runat="server" />
								<asp:HiddenField ID="hdn_Gratuity_StructureID" runat="server" />
							</td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Offer_Gratuity" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>
						</tr>
						<tr class="Total">
							<td></td>
							<td><span>Total 3 </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Offer_Total3" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td rowspan="3">Facilities not convertible into cash - notional figures</td>
							<td><span>Mediclaim c </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Offer_Mediclaim" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" AutoPostBack="true" onkeypress="return validateFloatKeyPress(this,event);" OnTextChanged="txt_Offer_Mediclaim_TextChanged"></asp:TextBox>
							</td>

						</tr>

                        <tr>
							<td><span runat="server" id="Span_Helthcheckup">HEALTH CHECK UP</span>&nbsp&nbsp <span style="color: red" runat="server" id="Span_Helthcheckup1">*</span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Offer_Health_Checkup" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" AutoPostBack="true" onkeypress="return validateFloatKeyPress(this,event);" OnTextChanged="txt_Offer_Health_Checkup_TextChanged"></asp:TextBox>
							</td>
						</tr>

						<tr>
							<td><span>Group Acc Policy </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Offer_Group_Policy" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" AutoPostBack="true" onkeypress="return validateFloatKeyPress(this,event);" OnTextChanged="txt_Offer_Group_Policy_TextChanged"></asp:TextBox>
							</td>
						</tr>

						<tr class="Total">
							<td></td>
							<td><span>Total 4 </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txtTotal4" CssClass="Offermargin" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>

						</tr>
                        <tr runat="server" id="TRHide1">
							<td></td>
							<td><span >CAR HIRE COST </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td>
							</td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_offer_Car_Hire_Cost" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="true" AutoPostBack="true" onkeypress="return validateFloatKeyPress(this,event);" OnTextChanged="txt_offer_Car_Hire_Cost_TextChanged"></asp:TextBox>
							</td>
						</tr>

                       <tr runat="server" id="TRHide2">

							<td></td>
							<td><span>CAR EXPENSES REIMBURSEMENT </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td>
							</td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Offer_Car_Expenses_Reimbursment" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="true" AutoPostBack="true" onkeypress="return validateFloatKeyPress(this,event);" OnTextChanged="txt_Offer_Car_Expenses_Reimbursment_TextChanged"></asp:TextBox>
							</td>
						</tr>

                        <tr runat="server" id="TRHide3">
							<td></td>
							<td><span>CAR FUEL EXPENSES REIMBURSEMENT </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td>
							</td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Car_Fuel_Expenses_Reimbursment" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="true" AutoPostBack="true" onkeypress="return validateFloatKeyPress(this,event);" OnTextChanged="txt_Car_Fuel_Expenses_Reimbursment_TextChanged"></asp:TextBox>
							</td>
						</tr>
                        
                        <tr class="Total" runat="server" id="TRHide4">
							<td></td>
							<td><span>Total 5-CAR Perquisit</span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txtTotal5" CssClass="Offermargin" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>

						</tr>

						<tr>
							<td></td>
							<td><span>CTC Per Month </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Offer_CTC_Month" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false" ></asp:TextBox>
							</td>

						</tr>
						<tr>
							<td></td>
							<td><span>CTC Per Annum </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Offer_CTC_Annum" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>

						</tr>
						<tr>
							<td></td>
							<td><span>PLP/Variable Percentage(%) </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Offer_PLP_Per" MaxLength="5" CssClass="Offermargin number" runat="server" onpaste="return false" AutoPostBack="true" onkeypress="return validateFloatKeyPress(this,event);" OnTextChanged="txt_Offer_PLP_variable_TextChanged"></asp:TextBox>
							</td>

						</tr>
						<tr>
							<td></td>
							<td><span>PLP/Variable Pay </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Offer_PLP_variable" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>

						</tr>
						<tr>
							<td></td>
							<td><span>CTC Per Annum Including PLP </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Offer_CTC_Annum_Incl_PLP" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>

						</tr>
						<tr>
							<td></td>
							<td><span>Joining Bonus </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td><asp:TextBox AutoComplete="off" ID="txt_Retention_Comment" MaxLength="1000"  CssClass="Offermargin noresize" runat="server" TextMode="MultiLine" onpaste="return false" Style="width:86%" onKeyUp="javascript:Count_Offer(this);" Rows="5" ></asp:TextBox>
		</td>
							<td >
								<asp:TextBox AutoComplete="off" ID="txt_Retention_Bonus" MaxLength="10" CssClass="Offermargin number" runat="server" onkeypress="return validateFloatKeyPress(this,event);" onpaste="return false"></asp:TextBox>
							</td>
						</tr>
						
						<tr>
							<td></td>
							<td><span>Annual Bonus </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Annual_Bonus" MaxLength="10" CssClass="Offermargin number" runat="server" onkeypress="return validateFloatKeyPress(this,event);" onpaste="return false"></asp:TextBox>
							</td>

						</tr>
						<tr>
							<td></td>
							<td><span>ALP Amount </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td><asp:TextBox AutoComplete="off" ID="txt_ALP_Comment" MaxLength="1000"  CssClass="Offermargin noresize" runat="server" TextMode="MultiLine" onpaste="return false" Style="width:86%" onKeyUp="javascript:Count_Offer(this);" Rows="5" ></asp:TextBox>
					</td>
							<td >
								<asp:TextBox AutoComplete="off" ID="txt_ALP_Amount" MaxLength="10" CssClass="Offermargin number" runat="server" onkeypress="return validateFloatKeyPress(this,event);" onpaste="return false"></asp:TextBox>
							</td>
						</tr>
						
						<tr>
							<td></td>
							<td><span>Other </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Offer_Other" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" onkeypress="return validateFloatKeyPress(this,event);" ></asp:TextBox>
							</td>

						</tr>
					</table>
				</ContentTemplate>
			</asp:UpdatePanel>
			<div style="text-align: center">
				<br />
				<asp:LinkButton ID="lnk_Offer_Submit" runat="server" ToolTip="Submit" CssClass="Savebtnsve" OnClientClick="SaveMultiClickGenerate()" OnClick="lnk_Offer_Submit_Click">Submit</asp:LinkButton>
				<asp:LinkButton ID="lnk_Offer_Cancle" runat="server" ToolTip="Clear" CssClass="Savebtnsve" OnClick="lnk_Offer_Cancle_Click">Clear</asp:LinkButton>

			</div>


			<br />
			<br />
			<div class="IRSheetBtn">
				<asp:LinkButton ID="btn_Offer_Back" runat="server" ToolTip="Back" CssClass="Savebtnsve">Back</asp:LinkButton>

			</div>
		</div>
	</asp:Panel>
	<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderGenerateOffer" runat="server"
		TargetControlID="btn_Generate_Offer" PopupControlID="pnlGenerateOffer" RepositionMode="RepositionOnWindowResizeAndScroll"
		BackgroundCssClass="modalBackground" DropShadow="true" OkControlID="btn_Offer_Back"
		OnOkScript="ok()" CancelControlID="btn_Offer_Back1" />

    


	<%--panel Display Offer latter--%>
	<asp:Panel ID="pnlDisplayOffer" runat="server" CssClass="IRmodalPopup" Style="display: none" Height="400px">
		<div id="Div4" runat="server" style="max-height: 500px; overflow: auto;">
			<div class="OfferHeading" style="">
				<span>
					<asp:Label ID="Label3" runat="server" Text="Compensation Structure"></asp:Label>
				</span>
				<div>
					<asp:Label runat="server" ID="Label4" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
				</div>
			</div>
			<span><a href="#" id="btn_Offer_Back3" title="Back" class="aaaa" style="margin-right: 30px; margin-bottom: 10px">Back</a>
			</span>
			<div>
			</div>
			<table class="TLOffer1">
				<tr>
					<td>
						<span>Candidate Name As Aadhar.</span> &nbsp&nbsp <span style="color: red">*</span>
						<br />
						<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_Name" CssClass="Offermargin" runat="server" Enabled="false"></asp:TextBox>
					</td>
					<td></td>
					</tr><tr>
					<td><span>Candidate Address As Aadhar. </span>
						<br />
						<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_Address" CssClass="Offermargin noresize" runat="server" TextMode="MultiLine" Rows="5" onpaste="return false" Enabled="false"></asp:TextBox>
					</td>
						<td></td>
				</tr>
				<tr style="display: none">
					<td><span>Band</span>&nbsp&nbsp <span style="color: red">*</span>
						<br />
						<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_Band" CssClass="Offermargin" runat="server"></asp:TextBox>
					</td>
					<td><span>Designation </span>&nbsp&nbsp <span style="color: red">*</span>
						<br />
						<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_Designation" CssClass="Offermargin" runat="server" onpaste="return false"></asp:TextBox>
					</td>


				</tr>
			</table>
			<br />
			<asp:UpdatePanel ID="updatePanel2" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<table class="TLOffer">
						<tr>
							<td runat="server" id="tb1">Monthly Payments - taxability as per applicable Income Tax rule</td>
							<td><span>Basic </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_Basic" CssClass="Offermargin number" MaxLength="10" onpaste="return false" runat="server" Enabled="false"></asp:TextBox>
							</td>

						</tr>
						<tr >
							<td><span>House Rent Allowance (HRA) </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td><span id="SP_Dis_HRA" runat="server"></span>
								<asp:HiddenField ID="hdn_Dis_HRA" runat="server" />

							</td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_HRA" CssClass="Offermargin number" MaxLength="10" onpaste="return false" runat="server" Enabled="false"></asp:TextBox>
							</td>

						</tr>
						<tr runat="server" id="TSpecial">
							<td><span>Special Allowance </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_Special_All" MaxLength="10" CssClass="Offermargin number" onpaste="return false" runat="server" Enabled="false"></asp:TextBox>
							</td>

						</tr>
						<tr runat="server" id="TConveyance">
							<td><span>Conveyance </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_Conveyance" MaxLength="10" CssClass="Offermargin number" onpaste="return false" runat="server" Enabled="false"></asp:TextBox>
							</td>

						</tr>
						<tr runat="server" id="TADHOC">
							<td><span>ADHOC Allowance a </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_ADHOC" MaxLength="10" CssClass="Offermargin number" onpaste="return false" runat="server" Enabled="false"></asp:TextBox>
							</td>

						</tr>
						<tr runat="server" id="TSkill">
							<td><span>Skill Allowance </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_Skill_All" MaxLength="10" CssClass="Offermargin number" onpaste="return false" runat="server" Enabled="false"></asp:TextBox>
							</td>

						</tr>
						<tr runat="server" id="TSuperannucation">
							<td><span>Superannucation Allowance b </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td><span id="SP_Dis_Supperannum" runat="server"></span>

							</td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_Superann_All" MaxLength="10" CssClass="Offermargin number" onpaste="return false" runat="server" Enabled="false"></asp:TextBox>
							</td>

						</tr>
						<tr runat="server" id="TCertificate">
							<td><span>Certificate Allowance </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_Certificate_All" MaxLength="10" CssClass="Offermargin number" onpaste="return false" runat="server" Enabled="false"></asp:TextBox>
							</td>

						</tr>
						<tr runat="server" id="Tr1">
							<td><span>Multi Skill Allowance </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_Multi" MaxLength="10" CssClass="Offermargin number" onkeypress="return validateFloatKeyPress(this,event);" onpaste="return false" runat="server" Enabled="false"></asp:TextBox>
							</td>

						</tr>
						<tr runat="server" id="Tr2">
							<td><span>Additional Skill Allowance </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_Additional_Skill" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>

						</tr>
						<tr runat="server" id="Tr3">
							<td><span>Car Allowance </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_Car_All" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>

						</tr>
						<tr runat="server" id="Tr4">
							<td><span>Food Allowance </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_Food_All" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>

						</tr>
						<tr class="Total">
							<td></td>
							<td><span>Total 1 </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td>Gross Payslip</td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_Total" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>

						</tr>
						<tr>
							<td runat="server" id="tb2">Retirals - taxability as per applicable Income tax rule</td>
							<td><span>LTA </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td><span id="SP_dis_LTA" runat="server"></span>

							</td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_LTA" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>
						</tr>
						<tr runat="server" id="Tr5">
							<td><span>Medical </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_Medical" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>

						</tr>
						<tr runat="server" id="Tr6">
							<td><span>Driver Salary </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_Driver_Salary" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>

						</tr>
						<tr runat="server" id="Tr7">
							<td><span>Car lease  </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_Car_lease" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>

						</tr>
						<tr class="Total">
							<td></td>
							<td><span>Total 2 </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_Total2" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>

						</tr>
						<tr>
							<td rowspan="2">Facilities not convertible into cash - notional figures</td>
							<td><span>PF </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td><span id="SP_Dis_PF" runat="server"></span>

							</td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_PF" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>

						</tr>
						<tr>
							<td><span>Gratuity b </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td><span id="SP_Dis_Gratuity" runat="server"></span>

							</td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_Gratuity" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>
						</tr>
						<tr class="Total">
							<td></td>
							<td><span>Total 3 </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_Total3" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td rowspan="3">Facilities not convertible into cash - notional figures</td>
							<td><span>Mediclaim c </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_Mediclaim" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>

						</tr>
                        <tr >
							<td runat="server" id="TD_HEALTHCHECKUP1"><span>HEALTH CHECK UP </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td runat="server" id="TD_HEALTHCHECKUP2"></td>
							<td runat="server" id="TD_HEALTHCHECKUP3">
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_HEALTHCHECKUP" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>
						</tr>

						<tr>
							<td><span>Group Acc Policy </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_Group_Acc_Po" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>
						</tr>

						<tr class="Total">
							<td></td>
							<td><span>Total 4 </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_Total4" CssClass="Offermargin" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>

						</tr>

                        <tr runat="server" id="TR_CARHIRECOST">
							<td></td>
							<td><span>CAR HIRE COST </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td>
                                

							</td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_CARHIRECOST" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>

						</tr>
                         
                        <tr runat="server" id="TR_CAREXPENSESREIMBURSEMENT">
							<td></td>
							<td><span>CAR EXPENSES REIMBURSEMENT</span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td>
                                
							</td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_CAREXPENSESREIMBURSEMENT" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>

						</tr>

                        <tr runat="server" id="TR_CARFUELEXPENSESREIMBURSEMENT">
							<td></td>
							<td><span>CAR FUEL EXPENSES REIMBURSEMENT </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td>
                               
							</td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_CARFUELEXPENSESREIMBURSEMENT" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>

						</tr>

                        <tr class="Total" runat="server" id="Tr_Total5">
							<td></td>
							<td><span>Total 5-CAR Perquisit </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_Total5" CssClass="Offermargin" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>

						</tr>

						<tr>
							<td></td>
							<td><span>CTC Per Month </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_CTC_Per_Month" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>

						</tr>
						<tr>
							<td></td>
							<td><span>CTC Per Annum </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_CTC_Per_Annum" MaxLength="10" CssClass="Offermargin number Comma" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>

						</tr>
						<tr>
							<td></td>
							<td><span>PLP/Variable Percentage (%)</span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_Percentage" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td></td>
							<td><span>PLP/Variable Pay </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_PLP_Variable_Pay" MaxLength="10" CssClass="Offermargin  Comma" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td
						</tr>
						<tr>
							<td></td>
							<td><span>CTC Per Annum Including PLP </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_CTC_Per_Annum_PLP" MaxLength="10" CssClass="Offermargin  Comma" runat="server" Enabled="false"></asp:TextBox>
							</td>

						</tr>
						<tr runat="server" id="Tr8">
							<td></td>
							<td><span>Joining Bonus </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td><asp:TextBox AutoComplete="off" ID="txt_Dis_Joining_Remark"   CssClass="Offermargin noresize" runat="server" TextMode="MultiLine" onpaste="return false" Style="width:86%" Enabled="false" Rows="5" ></asp:TextBox>
						</td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_Joining_Bonus" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>
						</tr>
						
						<tr runat="server" id="Tr9">
							<td></td>
							<td><span>Annual Bonus </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_Annual_Bonus" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>

						</tr>
						<tr runat="server" id="Tr12">
							<td></td>
							<td><span>ALP Amount </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td><asp:TextBox AutoComplete="off" ID="txt_Dis_ALP_Remark"   CssClass="Offermargin noresize" runat="server" TextMode="MultiLine" onpaste="return false" Style="width:86%" Enabled="false" Rows="5" ></asp:TextBox>
					</td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Dis_ALP_Amount" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false"  Enabled="false" ></asp:TextBox>
							</td>

						</tr>
						
						
						<tr runat="server" id="Tr10">
							<td></td>
							<td><span>Other </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Off_Dis_Other" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>

						</tr>
					</table>
				</ContentTemplate>
			</asp:UpdatePanel>
			<div style="text-align: center">
				<br />

			</div>


			<br />
			<br />
			<div class="IRSheetBtn">
				<asp:LinkButton ID="btn_Offer_Back4" runat="server" ToolTip="Back" CssClass="Savebtnsve">Back</asp:LinkButton>

			</div>
		</div>
	</asp:Panel>
	<asp:LinkButton ID="lnk_Display_Offer" runat="server" Text="Display Offer" ToolTip="Display Offer" CssClass="Savebtnsve" Style="display: none"></asp:LinkButton>
	
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderDisplayOffer" runat="server"
		TargetControlID="lnk_Display_Offer" PopupControlID="pnlDisplayOffer" RepositionMode="RepositionOnWindowResizeAndScroll"
		BackgroundCssClass="modalBackground" DropShadow="true" OkControlID="btn_Offer_Back4"
		OnOkScript="ok()" CancelControlID="btn_Offer_Back3" />
   

	
	<asp:LinkButton ID="localtrvl_delete_btn" runat="server" Text="IR sheet Summary" ToolTip="IR sheet Summary" CssClass="Savebtnsve" Style="display: none" OnClick="trvl_accmo_btn_Click"></asp:LinkButton>

	<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderIRSheet" runat="server"
		TargetControlID="localtrvl_delete_btn" PopupControlID="PnlIrSheet" RepositionMode="RepositionOnWindowResizeAndScroll"
		BackgroundCssClass="modalBackground" DropShadow="true" OkControlID="Oth_btnDelete1"
		OnOkScript="ok()" CancelControlID="btBack" />

     <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderGenerateOfferDraft" runat="server"
		TargetControlID="btn_ViewDraft_Offer" PopupControlID="pnlGenerateOfferDraft" RepositionMode="RepositionOnWindowResizeAndScroll"
		BackgroundCssClass="modalBackground" DropShadow="true" OkControlID="btn_Offer_Back1Draft"
		OnOkScript="ok()" CancelControlID="btn_Offer_Back1Draft" />

    <%--panel Draft Generate Offer latter--%>
		<asp:Panel ID="pnlGenerateOfferDraft" runat="server" CssClass="IRmodalPopup" Style="display: none" Height="400px">
			<div id="Div5" runat="server" style="max-height: 500px; overflow: auto;">
				<div class="OfferHeading" style="">
					<span>
						<asp:Label ID="Label5" runat="server" Text="Compensation Structure "></asp:Label>
                      <span style="padding-left:300px"> <asp:Label ID="Label6" runat="server" Text="View Draft Copy "></asp:Label> </span>
					</span>
					<div>
						<asp:Label runat="server" ID="lblGenerateMsgDraft" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
					</div>
				</div>
				<span><a href="#" id="btn_Offer_Back1Draft" title="Back" class="aaaa" style="margin-right: 30px; margin-bottom: 10px">Back</a>
				</span>
				<div>
				</div>
				<table class="TLOffer1" style="font-family: 'Trebuchet MS'; font-size: 14px;">
					<div runat="server" id="Offer_Show" visible="false">
					<tr>

						<td>
							<br />
							<br />
							<span style="font-weight: bold; font-size: 14px"><span runat="server" id="SPOffer_latterNo"></span></span>
						</td>
						<td>
							
							<img src="../images/HB%20Logo.png" style="width:180px;float:right" />
						</td>

					</tr>
					<tr>
						<td style="font-weight: bold; font-size: 13px"><span runat="server" id="SPOffer_Date"></span></td>

					</tr>
					<tr>
						<td style="font-weight: bold; font-size: 13px"><span runat="server" id="SPOffer_CandidateName"></span></td>
						<td></td>
					</tr>
					<tr>
						<td><div style="width:45%"><span runat="server" id="SPOffer_Can_Address"></span></div>
							<br />
							<br />
							<br />
						</td>
						<td></td>



					</tr>
					<tr>
						<td colspan="2"><span style="font-weight: bold; font-size: 14px">Greetings  <span runat="server" id="SP_Greetings"></span>,</span></td>

					</tr>
					<tr>
						<td colspan="2">
							<span>Please refer to your application and the discussions we had regarding your appointment in <b>HIGHBAR
                            TECHNOCRAT LIMITED.</b> We are pleased to offer you a position in the company as <b><span runat="server" id="SP_Design"></span></b> in <b> Band  <span runat="server" id="SP_Band"></span>.</b>
								Your functional designation may change from time to time as per the assignment given to you.</span></td>
					</tr>
					<tr>
						<td colspan="2">
							<span>Presently, your posting will be based at <b><span runat="server" id="SP_Location"></span>.</b> However, based on the needs of the
								company, you can be transferred to any branch, site (or project), group (or associate) companies or a
								joint venture entered into by the Company, located anywhere in India or abroad.</span></td>
					</tr>
					<tr>
						<td colspan="2">
							<span>Please indicate the date of your joining which should not be later than <b><span runat="server" id="SP_JoiningDate"></span></b>. We would
                            like you to join <b>HIGHBAR TECHNOCRAT LIMITED.</b> at the earliest.</span></td>
					</tr>

					<tr>
						<td colspan="2">
							<span>You are requested to submit the documents as per the Annexure at the time of joining.</span></td>
					</tr>

					<tr>
						<td colspan="2">
							<span>A formal letter of appointment will be issued on the terms and conditions as discussed between us on your
                              joining <b>HIGHBAR TECHNOCRAT LIMITED.</b> subject to the following:</span></td>
					</tr>
					<tr>
						<td colspan="2">
							<span>1. Receipt of your joining report from branch / department</span><br />

						</td>
					</tr>
					<tr>
						<td colspan="2">

							<span>2. On verification of the documents submitted by you as per the annexure.</span>
						</td>
					</tr>

					<tr>
						<td colspan="2">
							<span>Please sign the duplicate copy of this letter in token of having received its original and acceptance of the
									same and also indicate the date of your joining. The duplicate copy of this letter duly accepted & signed
									by you should reach us before <b><span runat="server" id="SP_SP_JoiningDate1"></span></b> otherwise this offer letter will stand cancelled.
								
								
							</span></td>
					</tr>
					<tr>
						<td colspan="2">

							<span>Looking forward to a mutually beneficial association.</span>
							<br />
							<br />
						</td>
					</tr>

					<tr>
						<td colspan="2">
							<span>Thanks & Regards,
								<br />
								for <b>HIGHBAR TECHNOCRAT LIMITED.</b></span>
							<br />
							<br />
						</td>
					</tr>

					<tr>
						<td colspan="2">
							<b><span runat="server" id="SP_Recruitment_Head"></span> <br />  <span runat="server" id="SP_ApprovalDate1"></span></b>
							<br />
							</td>
					</tr>
					<tr>
						<td colspan="2">						
							<%--<span><b>Recruitment Head.</b></span></td>--%>
                        <span><b>For Head - HR.</b></span></td>
					</tr>
					<tr style="padding: 5px">
						<td colspan="2"></td>
					</tr>
					<tr>
						<td colspan="2">
							<span>I accept the above offer and will join your Organization on or before <span runat="server" id="SP_JoiningDate2"></span>
							</span></td>
					</tr>
					<tr>
						<td></td>
						<td style="float: right; text-align: center"><span>________________________</span><br />
							<b><span runat="server" id="SP_candidate_Name1"></span>
								<br />
							<span runat="server" id="SP_Candidate_Accpted_Date" ></span>
							</b></td>
					</tr>
					<tr style="padding: 10px">
						<td colspan="2">
							<br />
							<br />
						</td>
					</tr>
					<tr>
						<td colspan="2">
							<hr />
							<br />
							<br />
						</td>

					</tr>
					<tr style="text-align: center">
						<td colspan="2"><span style="font-size: 15px; text-decoration: underline; font-weight: bold">Attached to the Offer letter dated <span runat="server" id="SP_Candidate_Jo"></span></span></td>
					</tr>
					<tr style="padding: 10px">
						<td colspan="2">
							<br />
							<br />
							<br />
						</td>
					</tr>
					<tr>
						<td colspan="2">You are requested to submit the following documents on the day you of your joining. This will enable us to
						issue you your appointment letter immediately and also process the payroll for your salary. Any noncompliance to submit the same, will delay the issuance of appointment letter and payment of your salary
						there of.<br />
							<br />
						</td>
					</tr>

					<tr>
						<td colspan="2"><span style="padding-left: 30px;">1. A copy of your resignation letter addressed to the present employer.
						</span></td>
					</tr>
					<tr>
						<td colspan="2"><span style="padding-left: 30px;">2. A copy of your resignation letter duly accepted by your present employer</span></td>
					</tr>
					<tr>
						<td colspan="2"><span style="padding-left: 30px;">3. A copy of the Relieving letter issued to you by your present employer.</span></td>
					</tr>

					<tr>
						<td colspan="2"><span style="padding-left: 30px;">4. Certificate of Birth (school leaving certificate or SSC passing certificate where the date of
birth is mentioned).</span></td>
					</tr>
					<tr>
						<td colspan="2"><span style="padding-left: 30px;">5. A copy of your Educational qualification certificates duly attested by a Gazetted Officer.</span></td>
					</tr>
					<tr>
						<td colspan="2"><span style="padding-left: 30px;">6. Tax certificate issued by your previous employer for the tax deducted at source up to the
last date of your employment.</span></td>
					</tr>
					<tr>
						<td colspan="2"><span style="padding-left: 30px;">7. Medical fitness certificate with blood group (certified by a registered Medical Practitioner
holding MBBS Degree).</span></td>
					</tr>
					<tr>
						<td colspan="2"><span style="padding-left: 30px;">8. Last salary slip of your present employer.</span></td>
					</tr>
					<tr>
						<td colspan="2"><span style="padding-left: 30px;">9. Passport size colored photograph in White background 3 nos.</span></td>
					</tr>
					<tr>
						<td colspan="2"><span style="padding-left: 30px;">10. A copy of your Permanent Account Number (PAN).</span></td>
					</tr>
					<tr>
						<td colspan="2"><span style="padding-left: 30px;">11. A copy of your Aadhar Card.</span></td>
					</tr>
					<tr>
						<td colspan="2"><span style="padding-left: 30px;">12. A copy of your Passport (If available).</span></td>
					</tr>
					<tr>
						<td colspan="2"><span>
							<br />
							I agree to submit the aforesaid documents at the time of joining the organization..<br />
							<br />
						</span></td>
					</tr>
					<tr>
						<%--<td><span>NAME : _____________________ </span>
							<br />
							<br />
						</td>--%>
						<td>SIGNATURE :&nbsp <span runat="server" id="SP_Candidate_Name5"></span><br />
							<span runat="server" id="SP_Candidate_Accpted_Date2"></span>
							<br />
						</td>
						<td></td>
					</tr>
					<tr style="display:none">
						<td><span>PLACE : _____________________</span><br />
							<br />
						</td>
						<td></td>
					</tr>
					<tr style="display:none">
						<td><span>DATE : _____________________</span></td>
						<td></td>
					</tr>
					<tr>
						<td colspan="2">
							<br />
							<br />
							<br />
						</td>

					</tr>
					<tr>
						<td colspan="2">
							<hr />
							<br />
							<br />
							<br />
							<br />
						</td>

					</tr>
					<tr>
						<td colspan="2"><span style="font-weight: bold; font-size: 14px"><span runat="server" id="SP_OfferNo2"></span></span></td>

					</tr>
					<tr>
						<td colspan="2" style="font-weight: bold; font-size: 13px"><span runat="server" id="SP_Offer_Date2"></span></td>

					</tr>
					<tr>
						<td colspan="2" style="font-weight: bold; font-size: 13px"><span runat="server" id="SP_Candidate_Name2"></span></td>

					</tr>
					<tr>
						<td><div style="width:45%"><span runat="server" id="SP_Offer_Address2"></span></div>
							<br />
							<br />
							<br />
						</td>
						<td></td>
						<td></td>
						<td></td>

					</tr>
					<tr>
						<td colspan="2"><span style="font-weight: bold; font-size: 14px">Greetings <span runat="server" id="SP_Greetings2"></span>,</span><br />
							<br />
						</td>

					</tr>
					<tr>
						<td colspan="2">This is further to our offer letter dated <span runat="server" id="SP_Offer_Date3"></span>.<br />
							<br />
						</td>

					</tr>
					<tr>
						<td colspan="2">We are pleased to provide the following relevant details, which will help you on the day of your joining
                  the company.<br />
							<br />
						</td>
					</tr>
					<tr>
						<td colspan="2"><span style="padding-left: 40px">1. The office hours commence at 9:00 am on all working days.</span></td>
					</tr>
					<tr>
						<td colspan="2"><span style="padding-left: 40px">Address:<b> HIGHBAR TECHNOCRAT LIMITED</b> </span>
						</td>
					</tr>
					<tr>
						<td colspan="2"><span style="padding-left: 96px">D-Wing, 14th Floor, Empire Tower,</span></td>
					</tr>
					<tr>
						<td colspan="2"><span style="padding-left: 96px">Reliable Cloud City,</span></td>
					</tr>

					<tr>
						<td colspan="2"><span style="padding-left: 96px">Off. Thane-Belapur Road,</span></td>
					</tr>
					<tr>
						<td colspan="2"><span style="padding-left: 96px">Airoli, Navi Mumbai 400 703 </span>
							<br />
							<br />
						</td>
					</tr>
					<tr>
						<td colspan="2"><span style="padding-left: 40px">2. On joining, you are requested to contact <b><span runat="server" id="SP_Recruiter_Name"></span></b>,who will guide you
regarding the further process.</span></td>

					</tr>
					<tr>
						<td>
							<br />
							<br />
							<br />
						</td>
						<td></td>
					</tr>
					<tr>
						<td colspan="2">Looking forward to a mutually beneficial association.</td>
					</tr>
					<tr>
						<td>
							<br />
							<br />
							<br />
						</td>
						<td></td>
					</tr>
					<tr>
						<td>Thanks & egards.</td>
						<td></td>
					</tr>
					<tr>
						<td>
							<br />

						</td>
						<td></td>
					</tr>
					<tr>
						<td>For <span style="font-size: 16px"><b>HIGHBAR TECHNOCRAT LIMITED.</b></span></td>
						<td></td>
					</tr>
					<tr>
						<td>
							<br />
							<br />

						</td>
						<td></td>
					</tr>
					<tr>
						<td><b><span runat="server" id="SP_Recruitment2"></span> <br />
							<span runat="server" id="SP_ApprovalDate"></span></b>
							</td>
						<td></td>
					</tr>
						<tr>
						<td>
							<%--<span><b>Recruitment Head</b></span></td>--%>
                            <span><b>For Head - HR.</b></span></td>
						<td></td>
					</tr>
					<tr>
						<td>
							<br />

						</td>
						<td></td>
					</tr>
					<tr>
						<td colspan="2">
							<hr />
							<br />
							<br />
						</td>

					</tr>
					<tr>
						<td><span style="font-size: 15px"><b>Enclosures:
							<br />
							Annexure A - Compensation.</b></span></td>
						<td><span style="font-size: 15px"><b>Annexure A</b></span></td>
					</tr>
					<tr>
						<td>
							<br />
						</td>
						<td></td>
					</tr>
						</div>
					<tr>
						<td><span>Candidate Name</span> &nbsp;&nbsp; <span style="color: red">*</span>
							<br />
							<asp:TextBox ID="txtOffer_Can_Namedraft" runat="server" AutoComplete="off" CssClass="Offermargin" Enabled="false"></asp:TextBox>
						</td>
							
						<td><span>Designation </span>&nbsp;&nbsp; <span style="color: red">*</span>
							<br />
							<asp:TextBox ID="txt_Offer_Designationdraft" runat="server" AutoComplete="off" CssClass="Offermargin" Enabled="false" onpaste="return false"></asp:TextBox>
						</td>
					</tr>
					<tr>
						<td><span>Offer Location </span>&nbsp;&nbsp; <span style="color: red">*</span>
							<br />
							<asp:TextBox ID="txtOffer_Position_Location" runat="server" AutoComplete="off" CssClass="Offermargin" Enabled="false" onpaste="return false"></asp:TextBox>
						</td>
						
						<td><span>Band</span>&nbsp;&nbsp; <span style="color: red">*</span>
							<br />
							<asp:TextBox ID="txt_Offer_band" runat="server" AutoComplete="off" CssClass="Offermargin" Enabled="false"></asp:TextBox>
						</td>
					</tr>
					<tr>
						<td><span>Candidate Address  </span>
							<br />
							<asp:TextBox ID="txt_Offer_Location" runat="server" AutoComplete="off" CssClass="Offermargin noresize" Enabled="false" TextMode="MultiLine" Rows="5" onpaste="return false"></asp:TextBox>
					
						<td></td>
					</tr>
					
				</table>
				<br />
				<asp:UpdatePanel ID="updatePanel3" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<table class="TLOffer">
							<tr>
								<td runat="server" id="Td1">Monthly Payments - taxability as per applicable Income Tax rule</td>
								<td><span>Basic </span>&nbsp&nbsp <span style="color: red">*</span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_BasicDraft" CssClass="Offermargin number" MaxLength="10" onpaste="return false" runat="server" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr>
								<td><span>House Rent Allowance (HRA) </span>&nbsp&nbsp <span style="color: red">*</span>
								</td>
								<td><span id="SP_HRAdraft" runat="server"></span>
									<asp:HiddenField ID="hdn_HRA_Perdraft" runat="server" />
									<asp:HiddenField ID="hdn_HRA_StructureIDdraft" runat="server" />
								</td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_HRAdraft" CssClass="Offermargin number" MaxLength="10" onpaste="return false" runat="server" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr runat="server" id="TSpecialdraft">
								<td><span>Special Allowance  </span>&nbsp&nbsp <span style="color: red">*</span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Off_Special_All" MaxLength="10" CssClass="Offermargin number" onpaste="return false" runat="server" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr runat="server" id="TConveyancedraft">
								<td><span>Conveyance </span>&nbsp&nbsp <span style="color: red"></span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_Conveyancedraft" MaxLength="10" CssClass="Offermargin number" onpaste="return false" runat="server" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr id="TADHOCdraft" runat="server">
								<td><span>ADHOC Allowance a </span>&nbsp&nbsp <span style="color: red">*</span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_ADHOC_Allowancedraft" MaxLength="10" CssClass="Offermargin number" onpaste="return false" runat="server" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr runat="server" id="TSkilldraft">
								<td><span>Skill Allowance </span>&nbsp&nbsp <span style="color: red"></span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_offer_Skill_Allowancedraft" MaxLength="10" CssClass="Offermargin number" onpaste="return false" runat="server" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr runat="server" id="TSuperannucationdraft">
								<td><span>Superannucation Allowance b </span>&nbsp&nbsp <span style="color: red"></span>
								</td>
								<td><span id="SP_Superannucation_Alldraft" runat="server"></span>
									<asp:HiddenField ID="hdn_Superan_Alldraft" runat="server" />
									<asp:HiddenField ID="hdn_Superan_All_StructureIDdraft" runat="server" />
								</td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_Superannucationdraft" MaxLength="10" CssClass="Offermargin number" onpaste="return false" runat="server" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr runat="server" id="TCertificatedraft">
								<td><span>Certificate Allowance </span>&nbsp&nbsp <span style="color: red"></span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_Certificate_Alldraft" MaxLength="10" CssClass="Offermargin number" onpaste="return false" runat="server" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr runat="server" id="Tr1draft">
								<td><span>Multi Skill Allowance </span>&nbsp&nbsp <span style="color: red"></span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_Multi_Skill_Alldraft" MaxLength="10" CssClass="Offermargin number" onkeypress="return validateFloatKeyPress(this,event);" onpaste="return false" runat="server" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr runat="server" id="Tr2draft">
								<td><span>Additional Skill Allowance </span>&nbsp&nbsp <span style="color: red"></span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_Additional_Skilldraft" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr runat="server" id="Tr3draft">
								<td><span>Car Allowance </span>&nbsp&nbsp <span style="color: red"></span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txtCar_Alldraft" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr runat="server" id="Tr4draft">
								<td><span>Food Allowance </span>&nbsp&nbsp <span style="color: red"></span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt__Offer_Food_Alldraft" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr class="Total">
								<td></td>
								<td><span>Total 1 </span>&nbsp&nbsp <span style="color: red">*</span>
								</td>
								<td>Gross Payslip</td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_Total1draft" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr>
								<td runat="server" id="Td2">Retirals - taxability as per applicable Income tax rule</td>
								<td><span>LTA </span>&nbsp&nbsp <span style="color: red">*</span>
								</td>
								<td><span id="SP_LTAdraft" runat="server"></span>
									<asp:HiddenField ID="hdn_LTAdraft" runat="server" />
									<asp:HiddenField ID="hdn_LTA_StructureIDdraft" runat="server" />
								</td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_LTAdraft" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
								</td>
							</tr>
							<tr runat="server" id="TMedical">
								<td><span>Medical </span>&nbsp&nbsp <span style="color: red"></span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_offer_Medicaldraft" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr runat="server" id="TDriver">
								<td><span>Driver Salary </span>&nbsp&nbsp <span style="color: red"></span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_Driver_Salarydraft" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr runat="server" id="TCar">
								<td><span>Car lease  </span>&nbsp&nbsp <span style="color: red"></span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_Car_leasedraft" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr class="Total">
								<td></td>
								<td><span>Total 2 </span>&nbsp&nbsp <span style="color: red">*</span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_Total2draft" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr>
								<td rowspan="2">Facilities not convertible into cash - notional figures</td>
								<td><span>PF </span>&nbsp&nbsp <span style="color: red">*</span>
								</td>
								<td><span id="SP_PFdraft" runat="server"></span>
									<asp:HiddenField ID="hdn_PFdraft" runat="server" />
									<asp:HiddenField ID="hdn_PF_StructureIDdraft" runat="server" />
								</td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_PFdraft" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr>
								<td><span>Gratuity b </span>&nbsp&nbsp <span style="color: red">*</span>
								</td>
								<td><span id="SP_Gratuitydraft" runat="server"></span>
									<asp:HiddenField ID="hdn_Gratuitydraft" runat="server" />
									<asp:HiddenField ID="hdn_Gratuity_StructureIDdraft" runat="server" />
								</td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_Gratuitydraft" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
								</td>
							</tr>
							<tr class="Total">
								<td></td>
								<td><span>Total 3 </span>&nbsp&nbsp <span style="color: red">*</span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_Total3draft" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td rowspan="3">Facilities not convertible into cash - notional figures</td>
								<td><span>Mediclaim c </span>&nbsp&nbsp <span style="color: red">*</span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_Mediclaimdraft" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
								</td>

							</tr>

                            <tr >
	                          <td runat="server" id="TD_HEALTHCHECKUPDraft1"><span>HEALTH CHECK UP</span>&nbsp&nbsp <span style="color: red">*</span>
	                            </td>
	                            <td runat="server" id="TD_HEALTHCHECKUPDraft2"></td>
	                            <td runat="server" id="TD_HEALTHCHECKUPDraft3">
	                          <asp:TextBox AutoComplete="off" ID="txt_Offer_Health_CheckupDraft" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
	                         </td>
	                          </tr>

							<tr>
								<td><span>Group Acc Policy </span>&nbsp&nbsp <span style="color: red">*</span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_Group_Policydraft" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
								</td>
							</tr>

							<tr class="Total">
								<td></td>
								<td><span>Total 4 </span>&nbsp&nbsp <span style="color: red">*</span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txtTotal4draft" CssClass="Offermargin" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
								</td>

							</tr>

                            <tr runat="server" id="Tr_CarHireCostDraft">
							<td></td>
							<td><span>CAR HIRE COST </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td>
							</td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_offer_Car_Hire_CostDraft" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false" ></asp:TextBox>
							</td>
						</tr>

                        <tr runat="server" id="Tr_CarExpensesReimbursementDraft">
							<td></td>
							<td><span>CAR EXPENSES REIMBURSEMENT </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td>
							</td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Offer_Car_Expenses_ReimbursmentDraft" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false" ></asp:TextBox>
							</td>
						</tr>
                      
                            <tr runat="server" id="Tr_CarFuelExpencesReimbursementDraft">
							<td></td>
							<td><span>CAR FUEL EXPENSES REIMBURSEMENT </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td>
							</td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Car_Fuel_Expenses_ReimbursmentDraft" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false" ></asp:TextBox>
							</td>
						</tr>
                        
                        <tr class="Total" runat="server" id="Tr_Total5Draft">
							<td></td>
							<td><span>Total 5-CAR Perquisit</span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txtTotal5Draft" CssClass="Offermargin" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>

						</tr>

							<tr>
								<td></td>
								<td><span>CTC Per Month </span>&nbsp&nbsp <span style="color: red">*</span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_CTC_Monthdraft" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr>
								<td></td>
								<td><span>CTC Per Annum </span>&nbsp&nbsp <span style="color: red">*</span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_CTC_Annumdraft" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr runat="server" id="Tr9draft">
								<td></td>
							<td><span>PLP/Variable Percentage(%) </span>&nbsp&nbsp <span style="color: red"></span>
							</td>
							<td></td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Offer_PLP_Perdraft" MaxLength="5" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false" ></asp:TextBox>
							</td>

							</tr>
							<tr runat="server" id="TPLP">
								<td></td>
								<td><span>PLP/Variable Pay </span>&nbsp&nbsp <span style="color: red"></span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_PLP_variabledraft" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr runat="server" id="Tr5draft">
								<td></td>
								<td><span>CTC Per Annum Including PLP </span>&nbsp&nbsp <span style="color: red"></span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_CTC_Annum_Incl_PLPdraft" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr runat="server" id="Tr6draft">
								<td></td>
								<td><span>Joining Bonus </span>&nbsp&nbsp <span style="color: red"></span>
								</td>
								<td><asp:TextBox AutoComplete="off" ID="txt_Retention_Remark" Visible="false" Style="display:none"  CssClass="Offermargin noresize" runat="server" onpaste="return false" Width="86%" TextMode="MultiLine"  Enabled="false" Rows="5"></asp:TextBox>
					</td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Retention_Bonusdraft" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
								</td>
							</tr>
							<tr runat="server" id="Tr7draft">
								<td></td>
								<td><span>Annual Bonus </span>&nbsp&nbsp <span style="color: red"></span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Annual_Bonusdraft" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr runat="server" id="Tr10draft">
								<td></td>
								<td><span>ALP Amount   </span>&nbsp&nbsp <span style="color: red"></span>
								</td>
								<td><asp:TextBox AutoComplete="off" ID="txt_ALP_Remark" Visible="false" Style="display:none"  CssClass="Offermargin noresize" runat="server" onpaste="return false" Width="86%" TextMode="MultiLine"  Enabled="false" Rows="5"></asp:TextBox>
					          </td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_ALP_Amountdraft" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
								</td>
							</tr>
							<tr runat="server" id="Tr8draft">
								<td></td>
								<td><span>Other </span>&nbsp&nbsp <span style="color: red"></span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_Otherdraft" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
								</td>

							</tr>
						</table>
						<div runat="server" id="Offer_Show1" visible="false">
						<table class="TLOffer">
							<tr runat="server" id="adhoc">
								<td colspan="3"><span style="font-size: 12px; font-family: Arial; font-style: italic;">a - The adhoc allowance given to you may be adjusted in other allowance / Basic, in future without any
reduction in your total gross salary.
</span></td>
								<td></td>
							</tr>
							<tr>
								<td colspan="3"><span style="font-size: 12px; font-family: Arial; font-style: italic;">b - Applicable as per Payment of Gratuity Act.
</span></td>
								<td></td>
							</tr>
							<tr>
								<td colspan="3"><span style="font-size: 12px; font-family: Arial; font-style: italic;">c- As per applicable policy of the company covering upto maximum 4 family members i.e. Self,
Spouse, 2 Children (subject to age limits)
</span></td>
								<td></td>
							</tr>
							<tr>
								<td colspan="3"><span style="font-size: 12px; font-family: Arial; font-weight: bold; font-style: italic;">The value of House Rent Allowance (HRA) is computed as per company policy. In case of
transfer, the same will change as per policy applicable to the place of transfer. 
</span></td>
								<td></td>
							</tr>
							<tr  visible="false" id="TRJoining" runat="server" >
									<td colspan="3"><span style="font-size: 12px; font-family: Arial; font-style: italic;" runat="server" id="TDJoiningRemark"> 

									</span></td>
									<td></td>
								</tr>
								<tr visible="false" id="TRALP" runat="server">
									<td colspan="3"><span style="font-size: 12px; font-family: Arial; font-style: italic;" runat="server" id="TDALPRemark">

									</span></td>
									<td></td>
								</tr>
							<tr>
								<td style="text-align: center"><b><span style="font-size: 14px; font-family: Arial;" runat="server" id="SP_Recruitment3"></span></b></td>
								<td></td>
								<td></td>
								<td style="text-align: center"><b><span style="font-size: 14px; font-family: Arial;" runat="server" id="SP_Candidate_Name4"></span></b>
				</td>
							</tr>
							<tr>
								<td style="text-align: center"><b><span style="font-size: 14px; font-family: Arial;" runat="server" id="SP_ApprovalDate3"></span></b></td>
								<td></td>
								<td></td>
								<td style="text-align: center"><b><span style="font-size: 14px; font-family: Arial;" runat="server" id="SP_Candidate_Accpted_Date3"></span></b></td>
							</tr>
							<tr>
								<td style="text-align: center"><b><span style="font-size: 14px; font-family: Arial;">
                                   For Head - HR.</b></span></td>
                                   <%-- Recruitment Head </b></span></td>--%>

								<td></td>
								<td></td>
								<td>
								</td>
							</tr>
						</table>
							</div>
					</ContentTemplate>
				</asp:UpdatePanel>
				<div style="text-align: center">
					<br />
					<asp:LinkButton ID="lnk_Offer_SubmitDraft" runat="server" Visible="false" ToolTip="Submit" CssClass="Savebtnsve" OnClientClick="SaveMultiClickGenerate()">Submit</asp:LinkButton>
					<asp:LinkButton ID="LinkButton2" runat="server" Visible="false" ToolTip="Cancle" CssClass="Savebtnsve">Cancle</asp:LinkButton>

				</div>
				</div>
		</asp:Panel>

	<asp:HiddenField ID="hdCandidate_ID" runat="server" />
	<asp:HiddenField ID="hdnSaveStatusFlag" runat="server" />
	<asp:HiddenField ID="hdRecruitment_ReqID" runat="server" />
	<asp:HiddenField ID="FilePath" runat="server" />
	<asp:HiddenField ID="hdfilename" runat="server" />
	<asp:HiddenField ID="hdfilefath" runat="server" />
	<asp:HiddenField ID="hdnBankDetailID" runat="server" />

	<asp:HiddenField ID="hdfilefathIRSheet" runat="server" />
	<asp:HiddenField ID="hdfilenameIRSheet" runat="server" />
	<asp:HiddenField ID="HFQuestionnaire" runat="server" />
	<asp:HiddenField ID="HDCandidateResignationSubAcceptanceDoc" runat="server" />
	<asp:HiddenField ID="HDInterviewerSchedularEmpCode" runat="server" />

	<asp:HiddenField ID="OfferApprovalOther" runat="server" />
	<%--<asp:HiddenField ID="hdnReqID" runat="server" Value="37" />--%>
	<asp:HiddenField ID="hdnOfferAppID" runat="server" />
    <asp:HiddenField ID="HDnOfferDrftCopy" runat="server" />
	<asp:HiddenField ID="hdnofferappcode" runat="server" />
	<asp:HiddenField ID="hdnapprid" runat="server" />
	<asp:HiddenField ID="hdnLoginUserName" runat="server" />
	<asp:HiddenField ID="hdnLoginEmpEmail" runat="server" />
	<asp:HiddenField ID="hdnYesNo" runat="server" />
	<asp:HiddenField ID="hdnFinalizedDate" runat="server" />
	<asp:HiddenField ID="hdnOfferStatus" runat="server" />
	<asp:HiddenField ID="hdncandidateOffer" runat="server" />
	<asp:HiddenField ID="HFempcoderec" runat="server" />
	<asp:HiddenField ID="hdnOfferConditionid" runat="server" Value="0" />
	<asp:HiddenField ID="HFRecruitmentStatus" runat="server" />
	<asp:HiddenField ID="hdnInterviewphtoPath" runat="server" />
	<asp:HiddenField ID="hdnExtraApproverEmail" runat="server" />
	<asp:HiddenField ID="hdncomp_code" runat="server" />
	<asp:HiddenField ID="hdndept_Id" runat="server" />
	<asp:HiddenField ID="HFRecruitmentCancel" runat="server" />


	<script src="../js/dist/select2.min.js"></script>
	<link href="../js/dist/select2.min.css" rel="stylesheet" />

	<script type="text/javascript">      
		$(document).ready(function () {
			$("#MainContent_txtJobDescription").htmlarea();
			$("#MainContent_lstInterviewerOneView").select2();
			$("#MainContent_LstRecommPerson").select2();
			$("#MainContent_DDLJoinedemployee").select2();
			
			$("#MainContent_lstMainSkillset").select2();
			$("#MainContent_DDLEducationQualification").select2();

			$("#MainContent_DDLBaseLocationPreference").select2();
			$("#MainContent_DDLRelocateTravelAnyLocation").select2();
			$("#MainContent_DDLOpenToTravel").select2();
			$("#MainContent_DDlpayrollsCompany").select2();
			$("#MainContent_DDLBreakInService").select2();
			$("#MainContent_DDLprojecthandled").select2();
			$("#MainContent_DDLDomainExperence").select2();
			$("#MainContent_DDLSAPExperence").select2();
			$("#MainContent_DDLImplementationprojectWorkedOn").select2();
			$("#MainContent_DDLSupportproject").select2();
			$("#MainContent_DDLPhaseWorkimplementation").select2();
			$("#MainContent_DDLRolesPlaydImplementation").select2();
			$("#MainContent_DDLCommunicationSkill").select2();
			$("#MainContent_DDLnatureOfIndustryClient").select2();
			$("#MainContent_DDlCurrentlyonnotice").select2();
			$(".DropdownListSearch").select2();

		});
	</script>

	<script type="text/javascript">

		function checkDate(sender, args) {
			var toDate = new Date();
			toDate.setMinutes(0);
			toDate.setSeconds(0);
			toDate.setHours(0);
			toDate.setMilliseconds(0);
			if (sender._selectedDate < toDate) {
				alert("You can't select day earlier than today!");
				sender._selectedDate = toDate;
				//set the date back to the current date
				sender._textbox.set_Value(sender._selectedDate.format(sender._format))
			}
		}

		function SaveMultiClick() {
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

		function SaveMultiJobJoiningClick() {
			try {
				var retunboolean = true;
				var ele = document.getElementById('<%=JobDetail_btnSave.ClientID%>');

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

		function SaveMultiClickOffer() {
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
		function Count_Offer(text) {
			var maxlength = 1000;
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
			var unicode = e.keyCode ? e.keyCode : e.charCode
			if (unicode == 8 || unicode == 46) {
				keychar = unicode;
			}
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

		function SaveMultiLinkTransferCandidateClick() {
			try {
				var retunboolean = true;
				var ele = document.getElementById('<%=Link_BtnTransferCandidate.ClientID%>');

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

		function DownloadFileBlankIRSheet() {
			var localFilePath = document.getElementById("<%=hdfilefathIRSheet.ClientID%>").value;
			var file = document.getElementById("<%=hdfilenameIRSheet.ClientID%>").value;
			// alert(localFilePath); 
			// alert(file); 
			window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
		}

		function DownloadFilemultipleIRSheet(file) {
			var localFilePath = document.getElementById("<%=hdfilefathIRSheet.ClientID%>").value;
          //   var file = document.getElementById("<%=hdfilename.ClientID%>").value;
			// alert(localFilePath); 
			// alert(file); 
			window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
		}

		function DownloadFilemultipleAcceptanceFile(file) {
			var localFilePath = document.getElementById("<%=HDCandidateResignationSubAcceptanceDoc.ClientID%>").value;
          //   var file = document.getElementById("<%=hdfilename.ClientID%>").value;
			// alert(localFilePath); 
			// alert(file); 
			window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
			//  window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);

		}


		function DownloadFile() {
			var localFilePath = document.getElementById("<%=hdfilefath.ClientID%>").value;
			var file = document.getElementById("<%=hdfilename.ClientID%>").value;
			// alert(localFilePath); 
			// alert(file); 
			window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
		}

		function DownloadFilemultiple(file) {
			var localFilePath = document.getElementById("<%=hdfilefath.ClientID%>").value;
          //   var file = document.getElementById("<%=hdfilename.ClientID%>").value;
			// alert(localFilePath); 
			// alert(file); 
			window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
		}
		function DownloadmultipleOfferH(file) {
			var localFilePath = document.getElementById("<%=OfferApprovalOther.ClientID%>").value;
			// alert(localFilePath);           
			window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
			//window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
		}
		function DownloadInterViewerPhoto(file) {
			var localFilePath = document.getElementById("<%=hdnInterviewphtoPath.ClientID%>").value;
			window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
			//window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
		}

		function checkOffeDate(sender, args) {
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

		$('.OfferDates').keydown(function (e) {
			var k;
			document.all ? k = e.keyCode : k = e.which;
			if (k == 8 || k == 46)
				return false;
			else
				return true;

		});
		function validateFloatKeyPress(el, evt) {
			var charCode = (evt.which) ? evt.which : event.keyCode;

			if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
				return false;
			}

			if (charCode == 46 && el.value.indexOf(".") !== -1) {
				return false;
			}

			if (el.value.indexOf(".") !== -1) {
				var range = 90119;
				if (range.text == 90119) {
				}
				else {
					var number = el.value.split('.');
					if (number.length == 2 && number[1].length > 1)
						return false;
				}
			}

			return true;
		}
		function SaveMultiClickGenerate() {
			try {
				var retunboolean = true;
				var ele = document.getElementById('<%=lnk_Offer_Submit.ClientID%>');

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

		$(".Comma3").each(function () {
			
			let culture = parseFloat($(this).val());  
			let formatted = culture.toLocaleString('en-IN'); 
			//let formatted = culture.toLocaleString('en-IN', {maximumFractionDigits: 2})
			//$(this).text(formatted);	
			$(this).val(formatted);
		});	
		
		$('#MainContent_GRDGenerate_Offer tr').find('td').each(function (index, element) {
			
			//var ss = $(element).text();
			var trimStr = $.trim($(element).text());
			if(!trimStr==""){
			var culture = parseFloat($(element).text());
				let formatted = culture.toLocaleString('en-IN');
				$(element).text(formatted);
			}
		});
	</script>
</asp:Content>


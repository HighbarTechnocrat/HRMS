<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" 
	CodeFile="RecruiterCTCView.aspx.cs" Inherits="procs_RecruiterCTCView" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
	Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

	<link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Req_Requisition.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />
	<style>
		.myaccountpagesheading {
			text-align: center !important;
			text-transform: uppercase !important;
		}

		.aspNetDisabled {
			/*background: #dae1ed;*/
			background: #ebebe4;
		}

		.SalaryRange {
			background-color: red !important;
			font-weight: 700;
			color: white;
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
	</style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

	<script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
	<%-- <script src="../js/HtmlControl/jquery-1.3.2.js"></script>--%>
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
						<asp:Label ID="lblheading" runat="server" Text="Recruitment Candidate CTC Exception Approval View"></asp:Label>
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
						<a href="Req_CTCIndex.aspx?itype=APP" title="Back" runat="server" id="btnRecBack" style="margin-right: 10px;" class="aaaa">Back</a>
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


						<li class="trvl_date Req_Position">
							<span>Position Location</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
							<asp:DropDownList runat="server" ID="lstPositionLoca" Enabled="false">
							</asp:DropDownList>
							<br />
						</li>
						<li class="trvl_date Req_Position">
							<span style="display: none">
								<span>Position Designation</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
								<asp:DropDownList runat="server" ID="lstPositionDesign" Enabled="false">
								</asp:DropDownList>
								<br />
							</span>
						</li>
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
							
								<span>Salary Range(Lakh/Year)</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
								<asp:TextBox AutoComplete="off" ID="txtSalaryRangeFrom" CssClass="SalaryRange" runat="server" MaxLength="4" Enabled="false"></asp:TextBox>
								&nbsp;  To  &nbsp;
                            <asp:TextBox AutoComplete="off" ID="txtSalaryRangeTo" CssClass="SalaryRange" runat="server" MaxLength="5" Enabled="false"></asp:TextBox>
							
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
						

						<li class="trvL_detail" id="litrvldetail" runat="server">
							<asp:LinkButton ID="btnTra_Details" runat="server" Text="+" OnClick="btnTra_Details_Click" ToolTip="Browse" CssClass="Savebtnsve"></asp:LinkButton>
							<span id="spntrvldtls" runat="server">Recruitment Details</span>
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
									<asp:TextBox AutoComplete="off" ID="txtJobDescription" runat="server" Rows="7" CssClass="noresize" TextMode="MultiLine" Enabled="false"></asp:TextBox>
								</li>
								<li></li>
								<li></li>
								<li></li>

								<li>
									<asp:LinkButton ID="localtrvl_btnSave" Visible="false" runat="server" Text="Questionnaire" ToolTip="Get JD From Bank" CssClass="Savebtnsve" PostBackUrl="~/procs/Req_JD_Bank_Search.aspx"> Get JD From Bank  </asp:LinkButton>
								</li>
								<li></li>

								<li class="trvl_date" runat="server">
									<span>Assign Questionnaire</span>&nbsp;&nbsp;<span style="color: red"></span>
								</li>
								<li>
									<asp:LinkButton ID="Lnk_Questionnaire" ForeColor="#ff0000" runat="server" OnClientClick="DownloadFileQuestionnaire()"></asp:LinkButton>
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
									<%--   <span>Interviewer (2st Level)</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                          <asp:TextBox AutoComplete="off" ID="lstInterviewerTwo" runat="server" Enabled="false"></asp:TextBox>--%>

								</li>
								<li></li>
								<li class="trvl_date">
									<%-- <span>Interviewer (1st Level)</span>&nbsp;&nbsp;<span style="color: red"></span><br />
                            <asp:TextBox AutoComplete="off" ID="txtInterviewerOptOne" runat="server" Enabled="false"></asp:TextBox>--%>
								</li>

								<li class="trvl_date">
									<%--  <span>Interviewer (2st Level)</span>&nbsp;&nbsp;<span style="color: red"></span><br />
                            <asp:TextBox AutoComplete="off" ID="txtInterviewerOptTwo" runat="server" Enabled="false"></asp:TextBox>--%>
								</li>
							</ul>
						</div>
						<%--<li class="trvl_local">
							<asp:LinkButton ID="trvl_localbtn" runat="server" Text="-" ToolTip="Browse" CssClass="Savebtnsve" OnClick="trvl_localbtn_Click"></asp:LinkButton>
							
						</li>
						<li></li>
						<li></li>--%>
						
					</ul>
					<div runat="server" id="DivViewrowWiseCandidateInformation" visible="true">
							<div class="edit-contact">
								<ul id="Ul2" runat="server">
									<span style="font-size: larger">Candidate Information </span>&nbsp;&nbsp;
									<br /><br />

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

									<li class="trvl_date" style="padding-bottom: 20px" runat="server" id="LiSalarySection1">
										<span style="font-size: larger; text-decoration: underline">Salary Details: </span>&nbsp;&nbsp;
                            <br />
									</li>
									<li class="mobile_inboxEmpCode" runat="server" id="LiSalarySection2">
										<div>
					<asp:Label runat="server" ID="lblmsg2" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
				</div>
									</li>
									<li class="mobile_InboxEmpName" runat="server" id="LiSalarySection3">
										<span></span>&nbsp;&nbsp;
                            <br />
									</li>
									<li class="trvl_date Req_Salary">
							
								<span>Salary Range(Lakh/Year)</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
								<asp:TextBox AutoComplete="off" ID="txtsalaryfrom" CssClass="SalaryRange" runat="server" MaxLength="4" Enabled="false"></asp:TextBox>
								&nbsp;  To  &nbsp;
                            <asp:TextBox AutoComplete="off" ID="txtsalaryto" CssClass="SalaryRange" runat="server" MaxLength="5" Enabled="false"></asp:TextBox>
							
						</li><li></li>
									<li></li>
									<li runat="server" id="LiSalarySection4">
										<span>Current CTC_Fixed In(lakh) </span>&nbsp;&nbsp;<span style="color: red">*</span>
										<br />
										<asp:TextBox AutoComplete="off" ID="Txt_CurrentCTC_Fixed" Class="number" Enabled="false" MaxLength="5" runat="server"></asp:TextBox>
									</li>
									<li runat="server" id="LiSalarySection5">
										<span>Current CTC_Variable In(lakh)</span>&nbsp;&nbsp;<span style="color: red">*</span>
										<br />
										<asp:TextBox AutoComplete="off" ID="TxtCurrentCTC_Variable" Enabled="false" Class="number" MaxLength="5" runat="server"></asp:TextBox>
									</li>

									<li class="mobile_InboxEmpName" runat="server" id="LiSalarySection6">
										<span>Current CTC_Total In(lakh)</span>&nbsp;&nbsp;<span style="color: red">*</span>
										<br />
										<asp:TextBox AutoComplete="off" ID="TxtCurrentCTC_Total" Enabled="false" runat="server"></asp:TextBox>
									</li>

									<li runat="server" id="LiSalarySection7">
										<span>Exp. CTC_Fixed In(lakh)</span>&nbsp;&nbsp;<span style="color: red">*</span>
										<br />
										<asp:TextBox AutoComplete="off" ID="TxtExpCTC_Fixed" Class="number" Enabled="false" MaxLength="5" runat="server"></asp:TextBox>
									</li>
									<li runat="server" id="LiSalarySection8">
										<span>Exp. CTC_Variable In(lakh)</span>&nbsp;&nbsp;<span style="color: red">*</span>
										<br />
										<asp:TextBox AutoComplete="off" ID="TxtExpCTC_Variable" Class="number" Enabled="false" MaxLength="5" runat="server"></asp:TextBox>
									</li>
									<li class="mobile_InboxEmpName" runat="server" id="LiSalarySection9">
										<span>Exp. CTC_Total In(lakh)</span>&nbsp;&nbsp;<span style="color: red">*</span>
										<br />
										<asp:TextBox AutoComplete="off" ID="TxtExpCTC_Total" Enabled="false" runat="server"></asp:TextBox>
									</li>
									<li  runat="server" id="Li7">
										<asp:CheckBox ID="chk_exception" Enabled="false"  runat="server" Text="CTC Exception" />
										</li>
									<li runat="server" id="Li8">
										</li>
									<li runat="server" id="Li9">
										</li>
									<li class="mobile_InboxEmpName" runat="server" id="Li1">
										<span>Recruiter Remark</span>&nbsp;&nbsp;<span style="color: red">*</span>
										<br />
										<asp:TextBox AutoComplete="off" ID="txtRecruterRemark" Width="188%" Enabled="false" Rows="3"  TextMode="MultiLine" CssClass="noresize" runat="server"></asp:TextBox>
					
										</li>
									<li  runat="server" id="Li2">
										</li>
									<li runat="server" id="Li3">
										</li>
									<li class="mobile_InboxEmpName" runat="server" id="Li4" visible="false">
										<span>Approval Comment</span>&nbsp;&nbsp;<span style="color: red">*</span>
										<br />
										<asp:TextBox AutoComplete="off" ID="txtAPPRemark" Width="188%" Enabled="false" onKeyUp="javascript:Count(this);" Rows="3" TextMode="MultiLine" CssClass="noresize" runat="server"></asp:TextBox>
				
										</li>
									<li  runat="server" id="Li5">
										</li>
									<li runat="server" id="Li6">
										</li>
									<div>
							

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
						</div>
									<div  style="text-align:left"	>
									<asp:LinkButton ID="trvldeatils_cancel_btn" runat="server" Text="Save & Approve" Visible="false" ToolTip="Approve" CssClass="Savebtnsve" OnClick="trvldeatils_cancel_btn_Click" OnClientClick="return SaveMultiClick();">Approve </asp:LinkButton>
		                           <asp:LinkButton ID="trvldeatils_btnSave" runat="server" Text="Reject" ToolTip="Reject"  Visible="false" CssClass="Savebtnsve" OnClick="trvldeatils_btnSave_Click1" OnClientClick="return SaveMultiClick();">Reject</asp:LinkButton>
		                           <asp:LinkButton ID="accmo_delete_btn" runat="server" Text="Back" ToolTip="Back"  Visible="false" CssClass="Savebtnsve" PostBackUrl="~/procs/Req_RequisitionIndex.aspx?itype=Pending">  Back  </asp:LinkButton>
	</div>
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
										<asp:TextBox AutoComplete="off" Visible="false" ID="Txt_OtherNatureOfIndustryClient" Height="50" CssClass="noresize" Enabled="false" TextMode="MultiLine" MaxLength="200" runat="server"></asp:TextBox>
									</li>

									<li style="padding-bottom: 15px">
										<span>Why is he looking for a change  </span>&nbsp;&nbsp;<span style="color: red">*</span>
										<br />
										<asp:TextBox AutoComplete="off" ID="Txt_lookingforChange" Height="50" CssClass="noresize" TextMode="MultiLine" Enabled="false" MaxLength="200" runat="server"></asp:TextBox>
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
									
								</ul>
							</div>
						</div>
				</div>
			</div>
		</div>
	</div>
	<br />
	<br />



	<asp:HiddenField ID="hdnEmpCode" runat="server" />
	<asp:HiddenField ID="hdnLoginUserName" runat="server" />
	<asp:HiddenField ID="hdnLoginEmpEmail" runat="server" />
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
	<asp:HiddenField ID="HFQuestionnairename" runat="server" />
	<asp:HiddenField ID="hdnInterviewphtoPath" runat="server" />
	<asp:HiddenField ID="hdnCTCID" runat="server" />
	<asp:HiddenField  ID="hdnYesNo" runat="server"/>
	<asp:HiddenField  ID="hdnnextappcode" runat="server"/>
	<asp:HiddenField  ID="hdnapprid" runat="server"/>
	<asp:HiddenField  ID="hflApproverEmail" runat="server"/>
	<asp:HiddenField  ID="hdnstaus" runat="server"/>
	<asp:HiddenField  ID="HDNIntMain_ID" runat="server"/>
	<asp:HiddenField  ID="hdnRecruterName" runat="server"/>
	<asp:HiddenField  ID="hdnRecruterEmail" runat="server"/>
	<asp:HiddenField  ID="HiddenField1" runat="server"/>

	<script src="../js/dist/select2.min.js"></script>
	<link href="../js/dist/select2.min.css" rel="stylesheet" />

	<script type="text/javascript">      
		$(document).ready(function () {
			$("#MainContent_txtJobDescription").htmlarea();
			$("#MainContent_lstInterviewerOneView").select2();
			$("#MainContent_LstRecommPerson").select2();

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
		});
	</script>

	<script type="text/javascript">

		function SaveMultiClick() {
			try {
				var retunboolean = true;
				var ele = document.getElementById('<%=trvldeatils_cancel_btn.ClientID%>');

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

		function DownloadFileQuestionnaire() {
			var localFilePath = document.getElementById("<%=HFQuestionnaire.ClientID%>").value;
			var file = document.getElementById("<%=HFQuestionnairename.ClientID%>").value;
			//alert(localFilePath); 
			//alert(file); 
			window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
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


	</script>

</asp:Content>






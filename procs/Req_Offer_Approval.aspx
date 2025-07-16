<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" ValidateRequest="false" MaintainScrollPositionOnPostback="true"
	CodeFile="Req_Offer_Approval.aspx.cs" Inherits="procs_Rec_Offer_Approval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Req_Requisition.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
	<style>
		.lnkbtn_HideUnhide {
			/*by Highbartech on 24-06-2020
        background: #febf39;*/
			background: #3D1956;
			color: #febf39 !important;
			padding: 6px 16px;
		}

		.SalaryRange {
			background-color: red !important;
			font-weight: 700;
			color: white;
		}

		.OfferFileUplad {
			min-width: 0;
			min-height: 0;
			word-wrap: break-word;
			word-break: break-all;
			max-width: 100%;
		}


		.noresize {
			resize: none;
		}

		.btntransferlink {
			background: #3D1956;
			color: #febf39 !important;
			padding: 0.5% 1.4%;
			padding-top: 10px;
			margin: 0% 0% 0 0;
			height: 25px;
		}

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

		#MainContent_lnkuplodedfile:link {
			color: red;
			background-color: transparent;
			text-decoration: none;
			font-size: 14px;
		}

		#MainContent_lnkuplodedfile:visited {
			color: red;
			background-color: transparent;
			text-decoration: none;
		}

		#MainContent_lnkuplodedfile:hover {
			color: green;
			background-color: transparent;
			text-decoration: none !important;
		}

		.disabledbutton {
			pointer-events: none !important;
		}

		iframe1 {
			pointer-events: none !important;
			/*//opacity: 0.8 !important;*/
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
						<asp:Label ID="lblheading" runat="server" Text="Offer Approval Request"></asp:Label>
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
						<a href="Req_Offer_Index.aspx?itype=Pending" title="Back" runat="server" id="btnRecBack" style="margin-right: 10px;" class="aaaa">Back</a>
					</span>
				</div>

				<div class="edit-contact">

					<ul id="editform" runat="server">
						<div id="DivTrvl" class="edit-contact" runat="server" visible="true">
							<ul>
								<li class="trvl_date">
									<span>Requisition Number  </span>&nbsp;&nbsp;<span style="color: red">*</span>
									<br />
									<asp:TextBox AutoComplete="off" ID="txtReqNumber" runat="server" AutoPostBack="True" Enabled="false"></asp:TextBox>
								</li>

								<li class="trvl_date">
									<span>Requisition Date </span>&nbsp;&nbsp;<span style="color: red">*</span>
									<br />
									<asp:TextBox AutoComplete="off" ID="txtFromdate" runat="server" AutoPostBack="True" Enabled="false"></asp:TextBox>
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
									<asp:DropDownList runat="server" ID="lstPositionName" AutoPostBack="true">
									</asp:DropDownList>
									<br />


								</li>


								<li class="trvl_date Req_Position">
									<span>Position Criticality</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
									<asp:DropDownList runat="server" ID="lstPositionCriti">
									</asp:DropDownList>
									<br />
								</li>
								<li class="trvl_date Req_Position">

									<span>Main Skillset </span>&nbsp;&nbsp;<span style="color: red">*</span>
									<br />
									<asp:DropDownList runat="server" ID="lstSkillset" AutoPostBack="true">
									</asp:DropDownList>
									<br />

								</li>


								<li class="trvl_date Req_Position">
									<span>Department Name</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
									<asp:DropDownList runat="server" ID="lstPositionDept">
									</asp:DropDownList>
									<br />
								</li>




								<li class="trvl_date Req_Position">
									<span>Position Location</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
									<asp:DropDownList runat="server" ID="lstPositionLoca">
									</asp:DropDownList>
									<br />
								</li>
								<li></li>
								<li class="trvl_date Req_Position" style="display: none">
									<span>Position Designation</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
									<asp:DropDownList runat="server" ID="lstPositionDesign">
									</asp:DropDownList>
									<br />
								</li>
								<li class="trvl_date Req_Position" style="display: none">
									<span>Other Department</span>&nbsp;&nbsp;<span style="color: red"></span><br />
									<asp:TextBox AutoComplete="off" ID="txtOtherDept" runat="server"></asp:TextBox>
									<br />
								</li>
								<li class="trvl_date Req_Position" style="display: none">
									<span>Position Designation Other</span>&nbsp;&nbsp;<span style="color: red"></span><br />
									<asp:TextBox AutoComplete="off" ID="txtPositionDesig" runat="server"></asp:TextBox>
								</li>

								<li class="trvl_date">
									<span>No Of Position</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
									<asp:TextBox AutoComplete="off" ID="txtNoofPosition" runat="server" MaxLength="2"></asp:TextBox>
								</li>



								<li class="trvl_date">
									<span>Additional Skillset</span>&nbsp;&nbsp;<span style="color: red"> </span>
									<br />
									<asp:TextBox AutoComplete="off" ID="txtAdditionSkill" runat="server"></asp:TextBox>
								</li>
								<li class="trvl_date">
									<span>To Be Filled In(Days)</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
									<asp:TextBox AutoComplete="off" ID="txttofilledIn" runat="server" MaxLength="3"></asp:TextBox>
								</li>


								<li class="trvl_date Req_Position">
									<span>Reason For Requisition</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
									<asp:DropDownList runat="server" ID="lstReasonForRequi">
									</asp:DropDownList>
								</li>
								<li class="trvl_date Req_Position">

									<span>Preferred Employment Type</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
									<asp:DropDownList runat="server" ID="lstPreferredEmpType">
									</asp:DropDownList>
								</li>
								<li class="trvl_date Req_Position">
									<span>Band</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
									<asp:DropDownList runat="server" ID="lstPositionBand">
									</asp:DropDownList>
								</li>
								<li class="trvl_date Req_Salary">
									<div>
										<span>Salary Range(Lakh/Year)</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
										<asp:TextBox AutoComplete="off" ID="txtSalaryRangeFrom" CssClass="SalaryRange" runat="server" MaxLength="4"></asp:TextBox>
										&nbsp;  To  &nbsp;
                                      <asp:TextBox AutoComplete="off" ID="txtSalaryRangeTo" runat="server" CssClass="SalaryRange" MaxLength="5"></asp:TextBox>
									</div>
								</li>
								<li class="trvl_date Req_Salary">
									<span>Required Experience(Year)</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
									<asp:TextBox AutoComplete="off" ID="txtRequiredExperiencefrom" runat="server" MaxLength="4"></asp:TextBox>
									&nbsp; To &nbsp;
                            <asp:TextBox AutoComplete="off" ID="txtRequiredExperienceto" runat="server" MaxLength="4"></asp:TextBox>

								</li>

							</ul>
						</div>

						<li class="trvl_local">
							<br />
							<asp:LinkButton ID="trvl_localbtn" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" OnClick="trvl_localbtn_Click"></asp:LinkButton>
							<span id="spnlocalTrvl" runat="server">Other Details </span>
						</li>
						<li></li>
						<li></li>
						<div id="Div_Locl" class="edit-contact" runat="server" visible="false">
							<ul>


								<li class="Req_Requi_Esse">
									<span>Essential Qualification</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
									<asp:TextBox AutoComplete="off" ID="txtEssentialQualifi" runat="server" Rows="7" TextMode="MultiLine"></asp:TextBox>
								</li>
								<li class="Req_Requi_Esse">
									<span>Desired Qualification</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
									<asp:TextBox AutoComplete="off" ID="txtDesiredQualifi" runat="server" Rows="7" TextMode="MultiLine"></asp:TextBox>
								</li>

								<li class="trvl_date">
									<div style="display: none">
										<span>Recommended Person</span>&nbsp;&nbsp;<span style="color: red"></span><br />
										<asp:DropDownList runat="server" ID="lstRecommPerson"></asp:DropDownList>
										<br />
										<br />
									</div>
								</li>
								<li></li>
								<li></li>
								<li class="Req_Job_Desc">
									<span>Job Description</span>&nbsp;&nbsp;<span style="color: red">*</span><br />

									<asp:TextBox AutoComplete="off" ID="txtJobDescription" runat="server" Rows="7" TextMode="MultiLine"></asp:TextBox>
								</li>
								<li></li>
								<li></li>
								<li></li>

								<li></li>
								<li></li>
								<li class="trvl_date">
									<span>Assign Questionnaire</span>&nbsp;&nbsp;<span style="color: red"></span>
									<br />
									<br />
								</li>

								<li>

									<asp:LinkButton ID="lnkuplodedfile" runat="server" OnClientClick="DownloadFile1()"></asp:LinkButton>
									<br />
									<br />
								</li>

								<li class="Req_Requi_Cmt" id="lsttrvlapprover" runat="server">
									<span>Comments</span>&nbsp;&nbsp;<span style="color: red"></span><br />
									<asp:TextBox AutoComplete="off" ID="txtComments" runat="server" TextMode="MultiLine" Rows="5"></asp:TextBox>
								</li>
								<li class="trvl_date Req_Position">
									<span>Screening By</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
									<asp:DropDownList runat="server" ID="lstInterviewerOne">
									</asp:DropDownList></li>
								<%--<li class="trvl_date Req_Position" id="Recruiter" runat="server" visible="true">
									<span>Recruiter</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
									<asp:DropDownList runat="server" ID="listRecruiter">
									</asp:DropDownList></li>--%>
								<div style="display: none">
									<li class="trvl_date Req_Position">
										<span>Interviewer (2st Level)</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
										<asp:DropDownList runat="server" ID="lstInterviewerTwo">
										</asp:DropDownList>

									</li>
									<li></li>
									<li class="trvl_date">
										<span>Interviewer (1st Level)</span>&nbsp;&nbsp;<span style="color: red"></span><br />
										<asp:TextBox AutoComplete="off" ID="txtInterviewerOptOne" runat="server"></asp:TextBox>
									</li>

									<li class="trvl_date">
										<span>Interviewer (2st Level)</span>&nbsp;&nbsp;<span style="color: red"></span><br />
										<asp:TextBox AutoComplete="off" ID="txtInterviewerOptTwo" runat="server"></asp:TextBox>
									</li>
								</div>
							</ul>
						</div>

						<li class="trvL_detail" id="litrvldetail" runat="server">
							<asp:LinkButton ID="btnTra_Details" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" OnClick="btnTra_Details_Click"></asp:LinkButton>
							<span id="spntrvldtls" runat="server">Candidate Information</span>
						</li>
						<li></li>
						<li></li>



						<div id="Div_CanDetails" class="edit-contact" runat="server" visible="false">

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
											HeaderStyle-HorizontalAlign="left" ItemStyle-Width="18%" ItemStyle-BorderColor="Navy" />
										<asp:BoundField HeaderText="Main Skillset" DataField="ModuleDesc" ItemStyle-HorizontalAlign="center"
											ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />
										<asp:BoundField HeaderText="Currently On Notice" DataField="CurrentlyOnNotice" ItemStyle-HorizontalAlign="center"
											ItemStyle-Width="6%" ItemStyle-BorderColor="Navy" />
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
											ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />
										<asp:BoundField HeaderText="Inter. Status" DataField="InterStatus" ItemStyle-HorizontalAlign="center"
											ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />
										<asp:BoundField HeaderText="Inter. Feedback" DataField="InterFeedback" ItemStyle-HorizontalAlign="center"
											ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />


										<asp:TemplateField HeaderText="View" HeaderStyle-Width="15%" Visible="false">
											<ItemTemplate>
												<asp:ImageButton ID="lnkEditView" runat="server" Width="20px" Height="15px" OnClick="lnkEditView_Click" ImageUrl="~/Images/edit.png" />
											</ItemTemplate>
											<ItemStyle HorizontalAlign="Center" />
										</asp:TemplateField>
									</Columns>
								</asp:GridView>
							</div>

							<br />
							<br />
							<div class="manage_grid" style="width: 95%; height: auto; padding-left: 40px" runat="server" id="DivCandidateRoundHistory" visible="false">
								<asp:GridView ID="GVCandidateRoundHistory" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="solid" BorderWidth="1px"
									DataKeyNames="Candidate_ID" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false">
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
											HeaderStyle-HorizontalAlign="left" ItemStyle-Width="2%" ItemStyle-BorderColor="Navy" Visible="false" />
										<asp:BoundField HeaderText="Name" DataField="CandidateName" ItemStyle-HorizontalAlign="left"
											HeaderStyle-HorizontalAlign="left" ItemStyle-Width="2%" ItemStyle-BorderColor="Navy" Visible="false" />
										<%--  <asp:BoundField HeaderText="PAN"  DataField="CandidatePAN" ItemStyle-HorizontalAlign="center"                                 
                                ItemStyle-Width="15%"  ItemStyle-BorderColor="Navy"  />--%>
										<asp:BoundField HeaderText="Interview Date" DataField="EnterviewDate" ItemStyle-HorizontalAlign="center"
											ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
										<asp:BoundField HeaderText="Interview Time" DataField="InterviewTime" ItemStyle-HorizontalAlign="center"
											ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
										<asp:BoundField HeaderText="Interviewer / Interview Schedular" DataField="Interviewer" ItemStyle-HorizontalAlign="center"
											ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />
										<asp:BoundField HeaderText="Inter. Status" DataField="InterviewStatus" ItemStyle-HorizontalAlign="center"
											ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
										<asp:BoundField HeaderText="Inter. Feedback" DataField="InterviewFeedback" ItemStyle-HorizontalAlign="center"
											ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
										<asp:BoundField HeaderText="Inter. Round" DataField="InterviewRound" ItemStyle-HorizontalAlign="center"
											ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
										<asp:BoundField HeaderText="Interviewer / Interview Schedular Comments" DataField="InterviewerComments" ItemStyle-HorizontalAlign="Left"
											ItemStyle-Width="25%" ItemStyle-BorderColor="Navy" />
										<asp:TemplateField HeaderText="IR Sheet" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
											<ItemTemplate>
												<%--<asp:LinkButton ID="lnkViewFilesIRSheet" runat="server" Text='<%# Eval("IRSheet") %>' OnClientClick=<%# "DownloadFilemultipleIRSheet('" + Eval("IRSheet") + "')" %>></asp:LinkButton>--%>
												<asp:ImageButton ID="lnkIRsheetView" runat="server" Width="20px" ToolTip="View IR Sheet " Height="15px" OnClick="lnkIRsheetView_Click" ImageUrl="~/Images/edit.png" />
											</ItemTemplate>
											<ItemStyle HorizontalAlign="Center" />
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Download IR Sheet" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
											<ItemTemplate>
												<asp:ImageButton ID="lnkIRsheetExport" runat="server" Width="20px" ToolTip="Download IR Sheet" Height="15px" Visible='<%# String.IsNullOrEmpty(Convert.ToString(Eval("InterviewerComments"))) ? false : true %>' OnClick="lnkIRsheetExport_Click" ImageUrl="~/Images/Download.png" />
											</ItemTemplate>
											<ItemStyle HorizontalAlign="Center" />
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Photo" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
											<ItemTemplate>
												<asp:ImageButton ID="lnkinterviewPhoto" runat="server" Width="20px" ToolTip="View Photo" Height="15px" ImageUrl="~/Images/Download.png"
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
											<%--<span  style="font-size:larger">Candidate Information </span>&nbsp;&nbsp;--%>
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

										<li class="mobile_inboxEmpCode">
											<span>PAN </span>&nbsp;&nbsp;
                            <br />
											<asp:TextBox AutoComplete="off" ID="Txt_CandidatePAN" Enabled="false" MaxLength="10" runat="server"></asp:TextBox>
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
										<li></li>
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

										<li class="trvl_date Req_Salary"><span>Salary Range(Lakh/Year)</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
											<asp:TextBox AutoComplete="off" ID="txtsalaryfrom" CssClass="SalaryRange" runat="server" MaxLength="4" Enabled="false"></asp:TextBox>
											&nbsp;  To  &nbsp;
                            <asp:TextBox AutoComplete="off" ID="txtsalaryto" CssClass="SalaryRange" runat="server" MaxLength="5" Enabled="false"></asp:TextBox>
										</li>
										<li></li>
										<li></li>
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
											<asp:DropDownList ID="DDlCurrentlyonnotice" runat="server" CssClass="DropdownListSearch" Enabled="false">
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
											<br />
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



										<span runat="server" id="SpanEducationDetails"></span>
										<asp:GridView ID="GVEducationDetails" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="90%"
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
												<asp:BoundField HeaderText="Education Type" DataField="EducationType" ItemStyle-HorizontalAlign="left"
													HeaderStyle-HorizontalAlign="left" ItemStyle-Width="10%" />
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
												<asp:BoundField HeaderText="Year of Passing" DataField="YearofPassing" ItemStyle-HorizontalAlign="left"
													HeaderStyle-HorizontalAlign="left" ItemStyle-Width="10%" />
												<asp:BoundField HeaderText="Final Score" DataField="FinalScore" ItemStyle-HorizontalAlign="left"
													HeaderStyle-HorizontalAlign="left" ItemStyle-Width="10%" />
												<asp:BoundField HeaderText="Discipline"
													DataField="PGDiscipline"
													ItemStyle-HorizontalAlign="left"
													ItemStyle-Width="15%"
													ItemStyle-BorderColor="Navy" />

												<asp:BoundField HeaderText="Type"
													DataField="PGType"
													ItemStyle-HorizontalAlign="left"
													ItemStyle-Width="8%"
													ItemStyle-BorderColor="Navy" />
											</Columns>
										</asp:GridView>

										<br />
										<br />
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
											<asp:DropDownList runat="server" ID="DDLBaseLocationPreference" CssClass="DropdownListSearch" Enabled="false">
											</asp:DropDownList>

										</li>
										<li>
											<span>Is he ready to relocate and travel to any locations in India & Abroad for project implementations </span>&nbsp;&nbsp;<span style="color: red">*</span>
											<br />
											<asp:DropDownList runat="server" ID="DDLRelocateTravelAnyLocation" CssClass="DropdownListSearch" Enabled="false">
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
											<br />
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

											<asp:LinkButton ID="lnkResume" runat="server" ForeColor="#ff0000" OnClientClick="return DownloadResume();"></asp:LinkButton>
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
															<%--<asp:LinkButton ID="lnkViewFiles" runat="server" Text='View' OnClientClick=<%# "DownloadFilemultiple('" + Eval("FileName") + "')" %>></asp:LinkButton>--%>
															<asp:ImageButton ID="lnkViewFiles" runat="server" ToolTip=" File View" Width="15px" Height="15px" ImageUrl="~/Images/Download.png" OnClientClick=<%# "DownloadFilemultiple('" + Eval("FileName") + "')" %> />

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
															<%--<asp:LinkButton ID="lnkViewFilesIRSheet" runat="server" Text='View' OnClientClick=<%# "DownloadFilemultipleIRSheet('" + Eval("OtherFiles") + "')" %>></asp:LinkButton>--%>
															<asp:ImageButton ID="lnkViewFilesIRSheet" runat="server" ToolTip=" File View" Width="15px" Height="15px" ImageUrl="~/Images/Download.png" OnClientClick=<%# "DownloadFilemultipleIRSheet('" + Eval("OtherFiles") + "')" %> />

														</ItemTemplate>
														<ItemStyle HorizontalAlign="Center" />
													</asp:TemplateField>
												</Columns>
											</asp:GridView>
										</li>
										<li runat="server" id="DivViewotherIRSheetFile2"></li>
										<li runat="server" id="DivViewotherIRSheetFile3"></li>
										<li></li>
										<li></li>


									</ul>
								</div>
							</div>
						</div>





						<li class="trvl_local">
							<br />
							<asp:LinkButton ID="lnkbtn_expdtls" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" OnClick="lnkbtn_expdtls_Click"></asp:LinkButton>
							<span id="spnexpdtls" runat="server">Offer Approver Details&nbsp;&nbsp; </span>
						</li>
						<li></li>
						<li></li>
						<div id="Div_Oth" class="edit-contact" runat="server" visible="true">
							<ul>
								<br />
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
										<asp:BoundField HeaderText="Offer Release Date"
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

										<asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="2%">
											<ItemTemplate>
												<asp:ImageButton ID="lnkOfferedit" runat="server" ToolTip=" File View" Width="15px" Height="15px" OnClick="lnnOfferedit_Click" ImageUrl="~/Images/edit.png" />
											</ItemTemplate>
											<ItemStyle HorizontalAlign="Center" />
										</asp:TemplateField>
									</Columns>
								</asp:GridView>
								<br />
								<li>
									<span>Offer Release Date </span>&nbsp;&nbsp;<span style="color: red">*</span>
									<br />
									<asp:TextBox AutoComplete="off" ID="txtofferDate" Enabled="false" runat="server"></asp:TextBox>
								</li>
								<li>
									<span>Offer Position Title </span>&nbsp;&nbsp;<span style="color: red">*</span>
									<br />
									<asp:TextBox AutoComplete="off" ID="txtpositionOffer" runat="server" Enabled="false"></asp:TextBox>
								</li>
								<li>
									<span>Offered Position Location </span>&nbsp;&nbsp;<span style="color: red">*</span>
									<br />
									<asp:TextBox AutoComplete="off" ID="txtOfferedLocation" runat="server" Enabled="false"></asp:TextBox>
								</li>

								<li>
									<span>Total CTC Offered (LPA) </span>&nbsp;&nbsp;<span style="color: red">*</span>
									<br />
									<asp:TextBox AutoComplete="off" ID="txtOfferno1" runat="server" CssClass="number" Enabled="false"></asp:TextBox>
								</li>
								<li>
									<span>CTC as per Band Eligibility & Other Allowances (Max Amount) LP </span>&nbsp;&nbsp;<span style="color: red">*</span>
									<br />
									<asp:TextBox AutoComplete="off" ID="txtOfferno2" runat="server" CssClass="number" Enabled="false"></asp:TextBox>
								</li>
								<li>
									<span>Exception Amount in LPA </span>&nbsp;&nbsp;<span style="color: red"></span>
									<br />
									<asp:TextBox AutoComplete="off" ID="txtExceptionamt" runat="server" Enabled="false"></asp:TextBox>
								</li>

								<li>
									<span>Offered Band </span>&nbsp;&nbsp;<span style="color: red">*</span>
									<br />
									<asp:DropDownList runat="server" ID="lstOfferBand" CssClass="DropdownListSearch" Enabled="false" Width="98%">
									</asp:DropDownList>
								</li>
								<li><span>Employment Type</span>&nbsp;&nbsp;<span style="color: red">*</span>
									<br />
									<asp:TextBox AutoComplete="off" ID="txt_EmploymentType" runat="server" CssClass="OfferDates" Enabled="false"></asp:TextBox>
								</li>
								<li>
									<span>Probable Joining Date </span>&nbsp;&nbsp;<span style="color: red">*</span>
									<br />
									<asp:TextBox AutoComplete="off" ID="txtProbableJoiningDate" runat="server" CssClass="OfferDates" Enabled="false"></asp:TextBox>
								</li>
								<li>

									<span>Recruitment Charges </span>&nbsp;&nbsp;<span style="color: red">*</span>
									<asp:DropDownList runat="server" ID="lstRecruitmentCharges" CssClass="DropdownListSearch" Enabled="false">
										<asp:ListItem Text="Select Recruitment Charges" Value="0"></asp:ListItem>
										<asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
										<asp:ListItem Text="No" Value="No"></asp:ListItem>
									</asp:DropDownList>
								</li>
								<li></li>
								<li></li>
								<li class="trvl_date">
									<br />
									<span>Recruiter Comments  </span>&nbsp;&nbsp;<span style="color: red">*</span>
									<br />
									<asp:TextBox AutoComplete="off" ID="txtOfferAppcmt" Rows="3" runat="server" TextMode="MultiLine" CssClass="noresize" Width="260%" onKeyUp="javascript:Count(this);"></asp:TextBox>
								</li>
								<li></li>
								<li></li>
								<li>
									<span runat="server" id="OfferhistoryS" visible="false">Offer Approval History  </span>
								</li>
								<li></li>
								<li></li>
								<br />
								<br />
								<asp:GridView ID="GRDOfferHistory" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="98%"
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
											ItemStyle-Width="10%" />

										<asp:BoundField HeaderText=" Offer Status"
											DataField="Request_status"
											ItemStyle-HorizontalAlign="left"
											HeaderStyle-HorizontalAlign="left"
											ItemStyle-Width="10%" />

										<asp:BoundField HeaderText=" Offer Approval Comment"
											DataField="appr_comments"
											ItemStyle-HorizontalAlign="left"
											HeaderStyle-HorizontalAlign="left"
											ItemStyle-Width="20%" />

										<asp:BoundField HeaderText=" Offer File List" ItemStyle-CssClass="OfferFileUplad"
											DataField="Offer_Approval_File"
											ItemStyle-HorizontalAlign="left"
											HeaderStyle-HorizontalAlign="left"
											ItemStyle-Wrap="true"
											ItemStyle-Width="40%" />

										<asp:TemplateField HeaderText="Files View" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="5%">
											<ItemTemplate>
												<asp:ImageButton ID="lnkViewFiles" runat="server" ToolTip=" File View" Width="15px" Height="15px" ImageUrl="~/Images/Download.png" Visible='<%# String.IsNullOrEmpty(Convert.ToString(Eval("Offer_Approval_File"))) ? false : true %>' OnClientClick=<%# "DownloadFilemultipleOffer('" + Eval("Offer_Approval_File") + "')" %> />
											</ItemTemplate>
											<ItemStyle HorizontalAlign="Center" />
										</asp:TemplateField>
									</Columns>
								</asp:GridView>
								<br />

								<div style="display: none">
									<asp:GridView ID="gvOfferOtherfile" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="68%"
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
											<asp:BoundField HeaderText=" Offer File List"
												DataField="Offer_Approval_File"
												ItemStyle-HorizontalAlign="left"
												HeaderStyle-HorizontalAlign="left"
												ItemStyle-Width="40%" />
											<asp:TemplateField HeaderText="Files View" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
												<ItemTemplate>
													<asp:ImageButton ID="lnkViewFiles" runat="server" ToolTip=" File View" Width="15px" Height="15px" ImageUrl="~/Images/Download.png" OnClientClick=<%# "DownloadFilemultipleOffer('" + Eval("Offer_Approval_File") + "')" %> />
												</ItemTemplate>
												<ItemStyle HorizontalAlign="Center" />
											</asp:TemplateField>
										</Columns>
									</asp:GridView>
								</div>
								<div style="padding-left: 0px;">
									<br />
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

											<asp:BoundField HeaderText="CTC Per Annum Including PLP"
												DataField="CTC_PER_ANNUM_INCLUDING_PLP"
												ItemStyle-HorizontalAlign="left"
												HeaderStyle-HorizontalAlign="left"
												ItemStyle-Width="10%" />

											<asp:BoundField HeaderText="PLP/Varible Pay"
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

											<%--<asp:BoundField HeaderText="Status" Visible="false"
							DataField="Offer_Approval_File"
							ItemStyle-HorizontalAlign="left"
							HeaderStyle-HorizontalAlign="left"
							ItemStyle-Width="20%" />--%>

											<asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="5%">
												<ItemTemplate>
													<asp:ImageButton ID="lnkGenerateFiles" runat="server" ToolTip=" File View" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkGenerateFiles_Click" />
												</ItemTemplate>
												<ItemStyle HorizontalAlign="Center" />
											</asp:TemplateField>
										</Columns>
									</asp:GridView>
								</div>
								<li class="trvl_date">
									<br />
									<span>Approval Comments  </span>&nbsp;&nbsp;<span style="color: red">*</span>
									<br />
									<asp:TextBox AutoComplete="off" ID="txtApprovalcmt" Rows="3" runat="server" MaxLength="10" TextMode="MultiLine" CssClass="noresize"
										Width="260%" onKeyUp="javascript:CountApproval(this);"></asp:TextBox>
								</li>
								<li class="trvl_date"></li>
								<li class="trvl_date"></li>
								<li class="trvl_date">
									<asp:CheckBox ID="chk_exception" runat="server" AutoPostBack="true" Text="Is Exception" OnCheckedChanged="chk_exception_CheckedChanged" />
								</li>
								<asp:GridView ID="DgvOfferApprover" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="68%">
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


					</ul>
				</div>
			</div>

		</div>
		<div class="mobile_Savebtndiv">
			<asp:LinkButton ID="mobile_btnSave" runat="server" Text="Approval" ToolTip="Approval" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click" OnClientClick="return SaveMultiClick();">Approval</asp:LinkButton>

			<asp:LinkButton ID="mobile_cancel" runat="server" Text="Reject" ToolTip="Reject" CssClass="Savebtnsve" OnClick="mobile_cancel_Click" OnClientClick="return RejectMultiClick();">Reject</asp:LinkButton>
			<asp:LinkButton ID="mobile_btnBack" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/Requisition_Index.aspx">Back</asp:LinkButton>

		</div>
		<br />

		<div>
			<ul>
				<li class="trvl_local" style="padding-bottom: 10px" runat="server" id="DIV_TransferCanInfo1" visible="true">
					<asp:LinkButton ID="Link_TransferHideUnhide" runat="server" Text="+" ToolTip="Browse" CssClass="lnkbtn_HideUnhide" OnClick="Link_TransferHideUnhide_Click"></asp:LinkButton>
					<span style="font-size: medium">Transfer Candidate </span>&nbsp;&nbsp;
                            <br />
				</li>
				<li class="mobile_inboxEmpCode" runat="server" id="DIV_TransferCanInfo2" visible="true">
					<span></span>&nbsp;&nbsp;
                            <br />
				</li>
				<li class="mobile_InboxEmpName" runat="server" id="DIV_TransferCanInfo3" visible="true">
					<span></span>&nbsp;&nbsp;
                            <br />
				</li>
			</ul>

			<div id="DIV_TransferCanInfo16" class="edit-contact" runat="server" visible="false">
				<ul>

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
						<asp:LinkButton ID="LinkBtnSearchTransferfilter" runat="server" Text="Submit" ToolTip="Search" CssClass="link_buttonClass" OnClick="LinkBtnSearchTransferfilter_Click">Search </asp:LinkButton>
						<asp:LinkButton ID="LinkBtnSearchTransferfilterClear" runat="server" Text="Clear Search" ToolTip="Clear Search" class="link_buttonClass" OnClick="LinkBtnSearchTransferfilterClear_Click">Clear Search</asp:LinkButton>
					</li>
					<li style="padding-top: 15px" runat="server" id="DIV_TransferCanInfo14" visible="false"></li>
					<li style="padding-top: 15px" runat="server" id="DIV_TransferCanInfo15" visible="false"></li>
					<li class="mobile_InboxEmpName" style="padding-bottom: 30px; padding-top: 30px" runat="server" id="DIV_TransferCanInfo4" visible="false">
						<span>Requisition Number </span>&nbsp;&nbsp;<span style="color: red">*</span>
						<br />
						<asp:DropDownList runat="server" ID="DDLsearchRequisitionnumber" CssClass="DropdownListSearch">
						</asp:DropDownList>
					</li>
					<li runat="server" id="DIV_TransferCanInfo5" style="padding-top: 30px; padding-bottom: 30px" visible="false">
						<asp:LinkButton ID="Link_BtnTransferCandidate" OnClick="Link_BtnTransferCandidate_Click" runat="server" Text="Approval & Transfer Candidate " ToolTip="Transfer Candidate & Approval"
							CssClass="btntransferlink" OnClientClick="return SaveMultiLinkTransferCandidateClick();"></asp:LinkButton>
					</li>
					<li runat="server" id="DIV_TransferCanInfo6" style="padding-top: 30px; padding-bottom: 30px" visible="false"></li>

				</ul>
			</div>

		</div>

		<br />
		<br />
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
					<asp:LinkButton ID="trvldeatils_delete_btn" runat="server" ToolTip="Back" CssClass="Savebtnsve">Back</asp:LinkButton>

				</div>
			</div>
		</asp:Panel>


		<asp:LinkButton ID="localtrvl_delete_btn" runat="server" Text="IR sheet Summary" ToolTip="IR sheet Summary" CssClass="Savebtnsve" Style="display: none"></asp:LinkButton>

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
				<span><a href="#" id="btn_Offer_Back1" title="Back" class="aaaa" style="margin-right: 30px; margin-bottom: 10px">Back</a>
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

								<img src="../images/HB%20Logo.png" style="width: 180px; float: right" />
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
							<td>
								<div style="width: 45%">
									<span runat="server" id="SPOffer_Can_Address"></span>
								</div>
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
                            TECHNOCRAT LIMITED.</b> We are pleased to offer you a position in the company as <b><span runat="server" id="SP_Design"></span></b> in <b>Band  <span runat="server" id="SP_Band"></span>.</b>
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
									by you should reach us before <b><span runat="server" id="SP_SP_JoiningDate1"></span></b>otherwise this offer letter will stand cancelled.
								
								
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
								<b><span runat="server" id="SP_Recruitment_Head"></span>
									<br />
									<span runat="server" id="SP_ApprovalDate1"></span></b>

							</td>
						</tr>
						<tr>
							<td colspan="2">
								<%--<span><b>Recruitment Head.</b></span>--%>

							</td>
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
									<span runat="server" id="SP_Candidate_Accpted_Date"></span>
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
							<td>SIGNATURE:  _____________________
								<br />
								<span runat="server" id="SP_Candidate_Name5"></span>
								<br />
								<span runat="server" id="SP_Candidate_Accpted_Date2"></span>
								<br />
							</td>
							<td></td>
						</tr>
						<tr style="display: none">
							<td><span>PLACE : _____________________</span><br />
								<br />
							</td>
							<td></td>
						</tr>
						<tr style="display: none">
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
							<td>
								<div style="width: 45%"><span runat="server" id="SP_Offer_Address2"></span></div>
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
							<td><b><span runat="server" id="SP_Recruitment2"></span>
								<br />
								<span runat="server" id="SP_ApprovalDate"></span></b>
							</td>
							<td></td>
						</tr>
						<tr>
							<td>
								<%--<span><b>Recruitment Head</b></span>--%>

							</td>
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
							<asp:TextBox ID="txtOffer_Can_Name" runat="server" AutoComplete="off" CssClass="Offermargin" Enabled="false"></asp:TextBox>
						</td>
						<td><span>Offer Designation </span>&nbsp;&nbsp; <span style="color: red">*</span>
							<br />
							<asp:TextBox ID="txt_Offer_Designation" runat="server" AutoComplete="off" CssClass="Offermargin" Enabled="false" onpaste="return false"></asp:TextBox>
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
						<td><span>Candidate Address </span>
							<br />
							<asp:TextBox ID="txt_Offer_Location" runat="server" AutoComplete="off" CssClass="Offermargin noresize" Enabled="false" onpaste="return false" Rows="5" TextMode="MultiLine"></asp:TextBox>
						</td>
						<td></td>

					</tr>

				</table>
				<br />
				<asp:UpdatePanel ID="updatePanel1" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<table class="TLOffer">
							<tr>
								<td runat="server" id="tb1">Monthly Payments - taxability as per applicable Income Tax rule</td>
								<td><span>Basic </span>&nbsp&nbsp <span style="color: red">*</span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_Basic" CssClass="Offermargin number" MaxLength="10" onpaste="return false" runat="server" Enabled="false"></asp:TextBox>
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
							<tr runat="server" id="TSpecial">
								<td><span>Special Allowance  </span>&nbsp&nbsp <span style="color: red">*</span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Off_Special_All" MaxLength="10" CssClass="Offermargin number" onpaste="return false" runat="server" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr runat="server" id="TConveyance">
								<td><span>Conveyance </span>&nbsp&nbsp <span style="color: red"></span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_Conveyance" MaxLength="10" CssClass="Offermargin number" onpaste="return false" runat="server" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr runat="server" id="TADHOC">
								<td><span>ADHOC Allowance a </span>&nbsp&nbsp <span style="color: red">*</span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_ADHOC_Allowance" MaxLength="10" CssClass="Offermargin number" onpaste="return false" runat="server" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr runat="server" id="TSkill">
								<td><span>Skill Allowance </span>&nbsp&nbsp <span style="color: red"></span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_offer_Skill_Allowance" MaxLength="10" CssClass="Offermargin number" onpaste="return false" runat="server" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr runat="server" id="TSuperannucation">
								<td><span>Superannucation Allowance b </span>&nbsp&nbsp <span style="color: red"></span>
								</td>
								<td><span id="SP_Superannucation_All" runat="server"></span>
									<asp:HiddenField ID="hdn_Superan_All" runat="server" />
									<asp:HiddenField ID="hdn_Superan_All_StructureID" runat="server" />
								</td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_Superannucation" MaxLength="10" CssClass="Offermargin number" onpaste="return false" runat="server" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr runat="server" id="TCertificate">
								<td><span>Certificate Allowance </span>&nbsp&nbsp <span style="color: red"></span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_Certificate_All" MaxLength="10" CssClass="Offermargin number" onpaste="return false" runat="server" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr runat="server" id="Tr1">
								<td><span>Multi Skill Allowance </span>&nbsp&nbsp <span style="color: red"></span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_Multi_Skill_All" MaxLength="10" CssClass="Offermargin number" onkeypress="return validateFloatKeyPress(this,event);" onpaste="return false" runat="server" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr runat="server" id="Tr2">
								<td><span>Additional Skill Allowance </span>&nbsp&nbsp <span style="color: red"></span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_Additional_Skill" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr runat="server" id="Tr3">
								<td><span>Car Allowance </span>&nbsp&nbsp <span style="color: red"></span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txtCar_All" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr runat="server" id="Tr4">
								<td><span>Food Allowance </span>&nbsp&nbsp <span style="color: red"></span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt__Offer_Food_All" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
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
								<td runat="server" id="tb2">Retirals - taxability as per applicable Income tax rule</td>
								<td><span>LTA </span>&nbsp&nbsp <span style="color: red">*</span>
								</td>
								<td><span id="SP_LTA" runat="server"></span>
									<asp:HiddenField ID="hdn_LTA" runat="server" />
									<asp:HiddenField ID="hdn_LTA_StructureID" runat="server" />
								</td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_LTA" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
								</td>
							</tr>
							<tr runat="server" id="TMedical">
								<td><span>Medical </span>&nbsp&nbsp <span style="color: red"></span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_offer_Medical" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr runat="server" id="TDriver">
								<td><span>Driver Salary </span>&nbsp&nbsp <span style="color: red"></span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_Driver_Salary" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr runat="server" id="TCar">
								<td><span>Car lease  </span>&nbsp&nbsp <span style="color: red"></span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_Car_lease" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
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
									<asp:TextBox AutoComplete="off" ID="txt_Offer_Mediclaim" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
								</td>

							</tr>

                        <tr>
							<td runat="server" id="TD_HealthCheckup1"><span>HEALTH CHECK UP</span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td runat="server" id="TD_HealthCheckup2"></td>
							<td runat="server" id="TD_HealthCheckup3">
								<asp:TextBox AutoComplete="off" ID="txt_Offer_Health_Checkup" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>
						</tr>

							<tr>
								<td><span>Group Acc Policy </span>&nbsp&nbsp <span style="color: red">*</span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_Group_Policy" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
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

                             <tr runat="server" id="TR_CarHireCost">
							<td></td>
							<td><span>CAR HIRE COST </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td>
							</td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_offer_Car_Hire_Cost" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>
						</tr>

                        <tr runat="server" id="TR_CarExpensesReimbursment">
							<td></td>
							<td><span>CAR EXPENSES REIMBURSEMENT </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td>
							</td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Offer_Car_Expenses_Reimbursment" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false" ></asp:TextBox>
							</td>
						</tr>
                        <tr runat="server" id="TR_CarFuelExpensesReimbursment">
							<td></td>
							<td><span>CAR FUEL EXPENSES REIMBURSEMENT </span>&nbsp&nbsp <span style="color: red">*</span>
							</td>
							<td>
							</td>
							<td>
								<asp:TextBox AutoComplete="off" ID="txt_Car_Fuel_Expenses_Reimbursment" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
							</td>
						</tr>
                        
                        <tr class="Total" runat="server" id="Tr_Total5">
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
									<asp:TextBox AutoComplete="off" ID="txt_Offer_CTC_Month" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
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
							<tr runat="server" id="Tr9">
								<td></td>
								<td><span>PLP/Variable Percentage(%) </span>&nbsp&nbsp <span style="color: red"></span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_PLP_Per" MaxLength="5" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr runat="server" id="TPLP">
								<td></td>
								<td><span>PLP/Variable Pay </span>&nbsp&nbsp <span style="color: red"></span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_PLP_variable" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr runat="server" id="Tr5">
								<td></td>
								<td><span>CTC Per Annum Including PLP </span>&nbsp&nbsp <span style="color: red"></span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_CTC_Annum_Incl_PLP" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
								</td>

							</tr>
								<tr runat="server" id="Tr6">
								<td></td>
								<td><span>Joining Bonus </span>&nbsp&nbsp <span style="color: red"></span>
								</td>
								<td><asp:TextBox AutoComplete="off" ID="txt_Retention_Remark"  CssClass="Offermargin noresize" Visible="false" Style="display:none" runat="server" onpaste="return false" Width="86%" TextMode="MultiLine"  Enabled="false" Rows="5"></asp:TextBox>
					</td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Retention_Bonus"  MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
								</td>
							</tr>
							<tr runat="server" id="Tr7">
								<td></td>
								<td><span>Annual Bonus </span>&nbsp&nbsp <span style="color: red"></span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Annual_Bonus" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
								</td>

							</tr>
							<tr runat="server" id="Tr10">
								<td></td>
								<td><span>ALP Amount   </span>&nbsp&nbsp <span style="color: red"></span>
								</td>
								<td><asp:TextBox AutoComplete="off" ID="txt_ALP_Remark"  CssClass="Offermargin noresize" runat="server" Visible="false" Style="display:none" onpaste="return false" Width="86%" TextMode="MultiLine"  Enabled="false" Rows="5"></asp:TextBox>
					          </td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_ALP_Amount" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
								</td>
							</tr>
							<tr runat="server" id="Tr8">
								<td></td>
								<td><span>Other </span>&nbsp&nbsp <span style="color: red"></span>
								</td>
								<td></td>
								<td>
									<asp:TextBox AutoComplete="off" ID="txt_Offer_Other" MaxLength="10" CssClass="Offermargin number" runat="server" onpaste="return false" Enabled="false"></asp:TextBox>
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
								<tr runat="server">
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
									<td colspan="3"><span style="font-size: 12px; font-family: Arial; font-weight: bold; font-style: italic;">The value of House Rent Allowance (HRA) is computed as per company policy. In case of
transfer, the same will change as per policy applicable to the place of transfer. 
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
                                        <%--Recruitment Head --%>
                                        </b></span></td>
									<td></td>
									<td></td>
									<td></td>
								</tr>
							</table>
						</div>
					</ContentTemplate>
				</asp:UpdatePanel>
				<div style="text-align: center">
					<br />
					<asp:LinkButton ID="lnk_Offer_Submit" runat="server" Visible="false" ToolTip="Submit" CssClass="Savebtnsve" OnClientClick="SaveMultiClickGenerate()">Submit</asp:LinkButton>
					<asp:LinkButton ID="lnk_Offer_Cancle" runat="server" Visible="false" ToolTip="Cancle" CssClass="Savebtnsve">Cancle</asp:LinkButton>

				</div>


				<br />
				<br />
				<div class="IRSheetBtn">
					<asp:LinkButton ID="btn_Offer_Back" runat="server" ToolTip="Back" CssClass="Savebtnsve">Back</asp:LinkButton>

				</div>
			</div>
		</asp:Panel>


		<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderIRSheet" runat="server"
			TargetControlID="localtrvl_delete_btn" PopupControlID="PnlIrSheet" RepositionMode="RepositionOnWindowResizeAndScroll"
			BackgroundCssClass="modalBackground" DropShadow="true" OkControlID="Oth_btnDelete1"
			OnOkScript="ok()" CancelControlID="btBack" />

		<asp:LinkButton ID="btn_Generate_Offer" runat="server" Text="Generate Offer Latter" ToolTip="Generate Offer Latter" CssClass="Savebtnsve" Style="padding: 7px 15px 7px 15px; margin: 0px 0px 15px 0px; display: none"></asp:LinkButton>
		<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderGenerateOffer" runat="server"
			TargetControlID="btn_Generate_Offer" PopupControlID="pnlGenerateOffer" RepositionMode="RepositionOnWindowResizeAndScroll"
			BackgroundCssClass="modalBackground" DropShadow="true" OkControlID="btn_Offer_Back"
			OnOkScript="ok()" CancelControlID="btn_Offer_Back1" />

		<asp:HiddenField ID="hflEmpDesignation" runat="server" />
		<asp:HiddenField ID="hflEmpDepartment" runat="server" />
		<asp:HiddenField ID="hflEmailAddress" runat="server" />
		<asp:HiddenField ID="hdCandidate_ID" runat="server" Value="2" />
		<asp:HiddenField ID="hdnGrade" runat="server" />
		<asp:HiddenField ID="hdnYesNo" runat="server" />
		<asp:HiddenField ID="FileName" runat="server" />
		<asp:HiddenField ID="FilePath" runat="server" />
		<asp:HiddenField ID="OfferApprovalOther" runat="server" />
		<asp:HiddenField ID="hdnOffer_App_ID" runat="server" />
		<asp:HiddenField ID="hdnEmpCode" runat="server" />
		<asp:HiddenField ID="hdnStatusName" runat="server" />
		<asp:HiddenField ID="hdnLoginUserName" runat="server" />
		<asp:HiddenField ID="hdnLoginEmpEmail" runat="server" />
		<asp:HiddenField ID="hdnCurrentID" runat="server" />
		<asp:HiddenField ID="hdnReqID" runat="server" />
		<asp:HiddenField ID="hdnRecruiteName" runat="server" />
		<asp:HiddenField ID="hdnRecruiteEmailID" runat="server" />
		<asp:HiddenField ID="hdnRecruiteCode" runat="server" />
		<asp:HiddenField ID="hdfilefath" runat="server" />
		<asp:HiddenField ID="hdCanResume" runat="server" />
		<asp:HiddenField ID="hdfilefathIRSheet" runat="server" />
		<asp:HiddenField ID="hdnFinalizedDate" runat="server" />
		<asp:HiddenField ID="hdnApproverid_LWPPLEmail" runat="server" />
		<asp:HiddenField ID="hdnOfferConditionid" runat="server" />
		<asp:HiddenField ID="hdnNextofferApprovalName" runat="server" />
		<asp:HiddenField ID="hdnNextofferApprovalEmail" runat="server" />
		<asp:HiddenField ID="hdnNextofferApprovalCode" runat="server" />
		<asp:HiddenField ID="hdnNextofferApprovalID" runat="server" />
		<asp:HiddenField ID="HiddenField2" runat="server" />
		<asp:HiddenField ID="hdnInterviewphtoPath" runat="server" />
		<asp:HiddenField ID="hdncomp_code" runat="server" />
		<asp:HiddenField ID="hdndept_Id" runat="server" />
		<asp:HiddenField ID="HdnFinalStatus" runat="server" />
		<asp:HiddenField ID="hdnEmployeeType" runat="server" />

		<script src="../js/dist/select2.min.js"></script>
		<link href="../js/dist/select2.min.css" rel="stylesheet" />

		<script type="text/javascript">      
			$(document).ready(function () {
				$("#MainContent_txtJobDescription").htmlarea();
				$(".DropdownListSearch").select2();
				//$("iframe").addClass("disabledbutton");

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

			function CountApproval(text) {
				var maxlength = 695;
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

			function RejectMultiClick() {
				try {
					var retunboolean = true;
					var ele = document.getElementById('<%=mobile_cancel.ClientID%>');

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
			function DownloadFilemultipleOffer(file) {
				var localFilePath = document.getElementById("<%=OfferApprovalOther.ClientID%>").value;
				//alert(localFilePath);
				window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
				//window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
			}
			function DownloadFile() {
				var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;
				var FileName = document.getElementById("<%=FileName.ClientID%>").value;

				window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
				//window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
			}
			function DownloadResume() {
				var localFilePath = document.getElementById("<%=hdfilefath.ClientID%>").value;
				var FileName = document.getElementById("<%=hdCanResume.ClientID%>").value;

				window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
				//window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
			}

			function DownloadFilemultipleIRSheet(file) {
				var localFilePath = document.getElementById("<%=hdfilefathIRSheet.ClientID%>").value;

				window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
				//window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
			}

			function DownloadFilemultiple(file) {
				var localFilePath = document.getElementById("<%=hdfilefath.ClientID%>").value;

				window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
				//window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);

			}
			function DownloadInterViewerPhoto(file) {
				var localFilePath = document.getElementById("<%=hdnInterviewphtoPath.ClientID%>").value;
				window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
				//window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
			}
			$('#MainContent_GRDGenerate_Offer tr').find('td').each(function (index, element) {

				//var ss = $(element).text();
				var trimStr = $.trim($(element).text());
				if (!trimStr == "") {
					var culture = parseFloat($(element).text());
					let formatted = culture.toLocaleString('en-IN');
					$(element).text(formatted);
				}
			});

		</script>
</asp:Content>



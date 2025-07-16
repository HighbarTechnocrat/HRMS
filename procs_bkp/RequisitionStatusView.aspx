<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="RequisitionStatusView.aspx.cs" 
    MaintainScrollPositionOnPostback="true" Inherits="procs_RequisitionStatusView" %>

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
		.OfferFileUplad 
		{
			min-width: 0;
			min-height: 0;
			word-wrap: break-word;
			word-break: break-all;
			max-width: 100%;
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
						<asp:Label ID="lblheading" runat="server" Text="Requisition Information"></asp:Label>
					</span>
				</div>
				<div>
					<asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
				</div>
				<div>
					<span style="margin-bottom: 20px">
						<a href="Requisition_Index.aspx" class="aaaa">Recruitment Home</a>
					</span>
                    <span>
                       <a href="RequisitionStatusList.aspx" title="Back" runat="server"  id="btnRecBack" style="margin-right: 10px;" class="aaaa">Back</a>
                    </span>
					<br />
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
						<%--<li class="trvl_local" style="margin-left: 100px">
							<div class="mobile_Savebtndiv" id="DivInterviewerShortListButton" runat="server">
								<asp:LinkButton ID="Linkbtn_CandidateShortlisting" runat="server" Text=" Click Here Candidate Send for Shortlisting" ToolTip="Save" OnClick="Linkbtn_CandidateShortlisting_Click"
									CssClass="Savebtnsve"></asp:LinkButton>
							</div>
						</li>
						<li>
							<span id="SpanCloseReqChk" runat="server" visible="false">&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="CheckClosedRequisition" runat="server" AutoPostBack="true" OnCheckedChanged="CheckClosedRequisition_CheckedChanged" />&nbsp;<span>Close Requisition</span>
							</span>
						</li>
						<li class="trvl_local">
							<span id="SpanJoinemployee" runat="server" visible="false">
								<span>Joined employee</span>&nbsp;&nbsp; <span style="color: red">*</span><br />
								<asp:DropDownList runat="server" ID="DDLJoinedemployee">
								</asp:DropDownList>
							</span>
						</li>--%>

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
						<%--<asp:BoundField HeaderText="Current CTC(Lakh)" DataField="CurrentCTC" ItemStyle-HorizontalAlign="center"
							ItemStyle-Width="6%" ItemStyle-BorderColor="Navy" />
						<asp:BoundField HeaderText="Expected CTC(Lakh)" DataField="ExpectedCTC" ItemStyle-HorizontalAlign="center"
							ItemStyle-Width="6%" ItemStyle-BorderColor="Navy" />--%>
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
							HeaderStyle-HorizontalAlign="left" ItemStyle-Width="25%" ItemStyle-BorderColor="Navy" Visible="false" />
						<asp:BoundField HeaderText="Name" DataField="CandidateName" ItemStyle-HorizontalAlign="left"
							HeaderStyle-HorizontalAlign="left" ItemStyle-Width="20%" ItemStyle-BorderColor="Navy" Visible="false" />
						<%--  <asp:BoundField HeaderText="PAN"  DataField="CandidatePAN" ItemStyle-HorizontalAlign="center"                                 
                                ItemStyle-Width="15%"  ItemStyle-BorderColor="Navy"  />--%>
						<asp:BoundField HeaderText="Interview Date" DataField="EnterviewDate" ItemStyle-HorizontalAlign="center"
							ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
						<asp:BoundField HeaderText="Interview Time" DataField="InterviewTime" ItemStyle-HorizontalAlign="center"
							ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
						<asp:BoundField HeaderText="Interviewer / Interview Schedular" DataField="Interviewer" ItemStyle-HorizontalAlign="center"
							ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />
						<asp:BoundField HeaderText="Inter. Status" DataField="InterviewStatus" ItemStyle-HorizontalAlign="center"
							ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />
						<asp:BoundField HeaderText="Inter. Feedback" DataField="InterviewFeedback" ItemStyle-HorizontalAlign="center"
							ItemStyle-Width="9%" ItemStyle-BorderColor="Navy" />
						<asp:BoundField HeaderText="Inter. Round" DataField="InterviewRound" ItemStyle-HorizontalAlign="center"
							ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
						<asp:BoundField HeaderText="Interviewer / Interview Schedular Comments" DataField="InterviewerComments" ItemStyle-HorizontalAlign="Left"
							ItemStyle-Width="25%" ItemStyle-BorderColor="Navy" />
						<asp:TemplateField HeaderText="IR Sheet" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="8%">
							<ItemTemplate>
								<%--<asp:LinkButton ID="lnkViewFilesIRSheet" runat="server" Text='<%# Eval("IRSheet") %>' OnClientClick=<%# "DownloadFilemultipleIRSheet('" + Eval("IRSheet") + "')" %>></asp:LinkButton>--%>
							<asp:ImageButton ID="lnkIRsheetView" runat="server" Width="20px" ToolTip="View IR Sheet " Height="15px" OnClick="lnkIRsheetView_Click"  ImageUrl="~/Images/edit.png" />
						
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
							<asp:ImageButton ID="lnkinterviewPhoto" runat="server" Width="20px" ToolTip="View Photo" Height="15px"  ImageUrl="~/Images/Download.png" 
								OnClientClick=<%# "DownloadInterViewerPhoto('" + Eval("Photo") + "')" %> Visible='<%# String.IsNullOrEmpty(Convert.ToString(Eval("Photo"))) ? false : true %>'
									  />
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

                        <li style="padding-bottom:10px">
                            <span  runat="server" id="SpanTxtReasonforBreak1" Visible="false"> Reason for Break </span>&nbsp;&nbsp;<span style="color:red" id="SpanTxtReasonforBreak" runat="server"></span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="TxtReasonforBreak" Visible="false" Height="50" CssClass="noresize" TextMode="MultiLine"  MaxLength="200" runat="server"></asp:TextBox>
                        </li>

                         <li style="padding-bottom:10px">
                            <span runat="server" id="SpanTxtOtherNatureOfIndustryClient1" Visible="false"> Other </span>&nbsp;&nbsp;<span style="color:red" id="SpanTxtOtherNatureOfIndustryClient" runat="server"></span>
                            <br />
                            <asp:TextBox AutoComplete="off" Visible="false" ID="Txt_OtherNatureOfIndustryClient" Height="50" CssClass="noresize" TextMode="MultiLine"  MaxLength="200" runat="server"></asp:TextBox>
                        </li>

                        <li style="padding-bottom:15px">
                            <span > Why is he looking for a change  </span>&nbsp;&nbsp;<span style="color:red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_lookingforChange" Height="50" CssClass="noresize" TextMode="MultiLine"  MaxLength="200" runat="server"></asp:TextBox>
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
						<%--<asp:LinkButton ID="Rec_AddjoinDetailbtn" runat="server" Text="-" ToolTip="Add Joining Detail" OnClick="Rec_AddjoinDetailbtn_Click" CssClass="Savebtnsve"></asp:LinkButton>
						--%>
                       	<span style="font-size: larger; text-decoration: underline">Joining Details: </span>&nbsp;&nbsp;
					</li>
					<li></li>
					<li></li>
				</ul>
			</div>

			<%--<div runat="server" id="DivJoiningDetails2" visible="false">
				<div class="edit-contact">
					<ul id="Ul1" runat="server">
						<li class="mobile_InboxEmpName" style="margin-bottom: 10px" runat="server" id="LIStatusUpdate">
							<span>Status Update </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList runat="server" ID="DDLStatusUpdate" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="DDLStatusUpdate_SelectedIndexChanged">
							</asp:DropDownList>
						</li>

						<li></li>
						<li></li>

						<li class="mobile_InboxEmpName" runat="server" id="LIJoiningDate">
							<span>Date</span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_JoiningDate" MaxLength="50" runat="server"></asp:TextBox>
							<ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="Txt_JoiningDate"
								OnClientDateSelectionChanged="checkDate" runat="server">
							</ajaxToolkit:CalendarExtender>
						</li>
						<li></li>
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
							<span style="margin-bottom: 10px">Upload File</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
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
			</div>--%>
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
									OnClientClick=<%# "DownloadFilemultipleAcceptanceFile('" + Eval("AcceptanceFile") + "')"%>
									 />

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
					<br />
					
					<%--Offer Approval edit data--%>
					<div style="padding-left: 0px;">
						<span runat="server" id="spoffer" visible="false" >Offer Release List  </span>
						<br /><br />
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
									ItemStyle-Width="7%" />

								<asp:BoundField HeaderText="Position Title"
									DataField="PositionTitle"
									ItemStyle-HorizontalAlign="left"
									HeaderStyle-HorizontalAlign="Center"
									ItemStyle-Width="10%" />

								<asp:BoundField HeaderText="Offered Band"
									DataField="OfferBAND"
									ItemStyle-HorizontalAlign="left"
									HeaderStyle-HorizontalAlign="Center"
									ItemStyle-Width="5%" />

								<asp:BoundField HeaderText="Total CTC Offered (LPA)"
									DataField="OldCTC"
									ItemStyle-HorizontalAlign="left"
									HeaderStyle-HorizontalAlign="Center"
									ItemStyle-Width="5%" />

								<asp:BoundField HeaderText="CTC as per Band Eligibility & Other Allowances (Max Amount) LP"
									DataField="NewCTC"
									ItemStyle-HorizontalAlign="left"
									HeaderStyle-HorizontalAlign="Center"
									ItemStyle-Width="8%" />

								<asp:BoundField HeaderText="Exception Amount in LPA "
									DataField="ExceptionAmount"
									ItemStyle-HorizontalAlign="left"
									HeaderStyle-HorizontalAlign="left"
									ItemStyle-Width="4%" />

								<asp:BoundField HeaderText="Comments"
									DataField="Comment"
									ItemStyle-HorizontalAlign="left"
									HeaderStyle-HorizontalAlign="Center"
									ItemStyle-Width="18%" />

								<asp:BoundField HeaderText="Probable Joining Date"
									DataField="ProbableJoiningDate"
									ItemStyle-HorizontalAlign="left"
									HeaderStyle-HorizontalAlign="Center"
									ItemStyle-Width="6%" />

								<asp:BoundField HeaderText="Recruitment Charges"
									DataField="RecruitmentCharges"
									ItemStyle-HorizontalAlign="left"
									HeaderStyle-HorizontalAlign="Center"
									ItemStyle-Width="3%" />

								<asp:BoundField HeaderText="Is Exception"
									DataField="IsException"
									ItemStyle-HorizontalAlign="left"
									HeaderStyle-HorizontalAlign="Center"
									ItemStyle-Width="3%" />

								<asp:BoundField HeaderText="Status"
									DataField="Request_status"
									ItemStyle-HorizontalAlign="left"
									HeaderStyle-HorizontalAlign="Center"
									ItemStyle-Width="5%" />

								<asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="2%">
									<ItemTemplate>
										<asp:ImageButton ID="lnkOfferedit" runat="server" ToolTip=" File View" Width="15px" Height="15px" OnClick="lnnOfferedit_Click" ImageUrl="~/Images/edit.png" />
									</ItemTemplate>
									<ItemStyle HorizontalAlign="Center" />
								</asp:TemplateField>
							</Columns>
						</asp:GridView>
					</div>
					
				</ul>
			</div>	
        
			<div id="OfferCreate" runat="server" visible="false">
				<div class="edit-contact">
					<ul>
						<li class="trvl_date" style="padding-bottom: 20px">				
							<asp:Label ID="lblOfferCreate"  Text="Offer Edit " runat="server" style="font-size: larger; text-decoration: underline"  />
							<br />
							
						</li>
						<li>
							<asp:Label runat="server" ID="lblOffer" Visible="True" Style="color: red; font-size: 14px; font-weight: 500; text-align: center;"></asp:Label>
						</li>
						<li></li>
						<li class="mobile_inboxEmpCode">
							<span>Offer Release Date  </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtOfferDate" runat="server" CssClass="OfferDates" Enabled="false"></asp:TextBox>
							<ajaxToolkit:CalendarExtender ID="CalendarExtender3" Format="dd/MM/yyyy" TargetControlID="txtOfferDate" OnClientDateSelectionChanged="checkOffeDate"
								runat="server">
							</ajaxToolkit:CalendarExtender>
						</li>
						<li>
							<span>Position Title </span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtpositionOffer" runat="server" Enabled="false"></asp:TextBox>
						</li>
						<li>
							<span>Offered Band </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							
							<asp:TextBox AutoComplete="off" ID="lstOfferBand" runat="server" Enabled="false"></asp:TextBox>
					
						</li>
						<li>
							<span>Total CTC Offered (LPA) </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtOfferno1" runat="server"  CssClass="number" Enabled="false" ></asp:TextBox>
						</li>
						<li>
							<span>CTC as per Band Eligibility & Other Allowances (Max Amount) LP </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtOfferno2" runat="server" Enabled="false" CssClass="number" ></asp:TextBox>
						</li>
						<li>
							<span>Exception Amount in LPA </span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtExceptionamt" runat="server" Enabled="false"></asp:TextBox>
						</li>
						<li class="mobile_inboxEmpCode">
							<span>Comments  </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtOfferAppcmt" Rows="3" runat="server" Enabled="false" TextMode="MultiLine" CssClass="noresize" Width="288%" onKeyUp="javascript:Count(this);"></asp:TextBox>
						</li>
						<li></li>
						<li></li>
						<li class="mobile_inboxEmpCode">
							<span>Probable Joining Date </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtProbableJoiningDate" runat="server" CssClass="OfferDates" Enabled="false"></asp:TextBox>
							<ajaxToolkit:CalendarExtender ID="CalendarExtender5" Format="dd/MM/yyyy" TargetControlID="txtProbableJoiningDate" runat="server" Enabled="false">
							</ajaxToolkit:CalendarExtender>
						</li>
						<li>
							<span>Recruitment Charges </span>&nbsp;&nbsp;<span style="color: red">*</span>							
							<asp:TextBox AutoComplete="off" ID="lstRecruitmentCharges" runat="server" CssClass="OfferDates" Enabled="false"></asp:TextBox>
							
						</li>
						<li class="mobile_InboxEmpName" style="padding-left: 20px">
							<asp:CheckBox ID="chk_exception" runat="server" Text="Is Exception" Enabled="false" />
						</li>
						<li class="mobile_InboxEmpName">
							<span>Upload files </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<asp:FileUpload ID="FileOfferUpload" runat="server" AllowMultiple="true" Enabled="false" />
							<br />
						</li>
						<br />
						<br />
						<li>
						<br />
						<span runat="server" id="OfferhistoryS" visible="false">Offer Approval History  </span>
					</li>
					<li></li>
					<li></li>
					</ul>
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
						<asp:BoundField HeaderText=" Offer File List"  ItemStyle-CssClass="OfferFileUplad"
							DataField="Offer_Approval_File"
							ItemStyle-HorizontalAlign="left"
							HeaderStyle-HorizontalAlign="left"
							ItemStyle-Width="20%" />

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

						<asp:TemplateField HeaderText="Files View" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="5%">
							<ItemTemplate>
								<asp:ImageButton ID="lnkViewFiles" runat="server" ToolTip=" File View" Width="15px" Height="15px" ImageUrl="~/Images/Download.png" OnClientClick=<%# "DownloadmultipleOfferH('" + Eval("Offer_Approval_File") + "')" %> />
							</ItemTemplate>
							<ItemStyle HorizontalAlign="Center" />
						</asp:TemplateField>
					</Columns>
				</asp:GridView>
			</div>
				</div>
			
			</div>
		
										
			
			<div class="mobile_Savebtndiv" id="DivButton" runat="server" visible="false">
				<%--<asp:LinkButton ID="trvldeatils_btnSave" runat="server" Text="Save" ToolTip="Save"
					CssClass="Savebtnsve" OnClick="trvldeatils_btnSave_Click" OnClientClick="return SaveMultiClick();"></asp:LinkButton>--%>
				<asp:LinkButton ID="mobile_btnBack" runat="server" Text="Close" ToolTip="Close" OnClick="mobile_btnBack_Click"
					CssClass="Savebtnsve">Close</asp:LinkButton>
			</div>
			<br />
		</div>
	</div>
	 <%--panel IR sheet--%>
					<asp:Panel ID="PnlIrSheet" runat="server" CssClass="IRmodalPopup"  Style="display: none"   Height="400px">
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
							<br /> <br />
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
						<br /><br />
							
						
						<div class="IRSheetBtn">
							<asp:LinkButton ID="mobile_cancel" runat="server" ToolTip="Back" CssClass="Savebtnsve" >Back</asp:LinkButton>

						</div>
							 </div>
					</asp:Panel>
	
					
					<asp:LinkButton ID="localtrvl_delete_btn" runat="server" Text="IR sheet Summary" ToolTip="IR sheet Summary" CssClass="Savebtnsve" Style="display:none" OnClick="trvl_accmo_btn_Click"></asp:LinkButton>

				
	<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderIRSheet" runat="server"
						TargetControlID="localtrvl_delete_btn" PopupControlID="PnlIrSheet" RepositionMode="RepositionOnWindowResizeAndScroll"
						BackgroundCssClass="modalBackground" DropShadow="true" OkControlID="Oth_btnDelete1"
						OnOkScript="ok()" CancelControlID="btBack" />
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
	<asp:HiddenField ID="hdnofferappcode" runat="server" />
	<asp:HiddenField ID="hdnapprid" runat="server" />
	<asp:HiddenField ID="hdnLoginUserName" runat="server" />
	<asp:HiddenField ID="hdnLoginEmpEmail" runat="server" />
	<asp:HiddenField ID="hdnYesNo" runat="server" />
	<asp:HiddenField ID="hdnFinalizedDate" runat="server" />
	<asp:HiddenField ID="hdnOfferStatus" runat="server" />
	<asp:HiddenField ID="hdncandidateOffer" runat="server" />
	<asp:HiddenField ID="hdnInterviewphtoPath" runat="server" />
	 <asp:HiddenField ID="hdncomp_code" runat="server" />
        <asp:HiddenField ID="hdndept_Id" runat="server" />
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

		<%--function SaveMultiClick() {
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
		}--%>
		
		<%--function SaveMultiJobJoiningClick() {
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
		}--%>

		<%--function SaveMultiClickOffer() {
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
		}--%>


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
	</script>
</asp:Content>


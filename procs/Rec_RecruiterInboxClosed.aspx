<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" 
    CodeFile="Rec_RecruiterInboxClosed.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="procs_Rec_RecruiterInboxClosed" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
	Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

	<link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
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

		.gridpager, .gridpager td {
			padding-left: 5px;
			text-align: right;
		}
	</style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
	<script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>


	<script src="../js/HtmlControl/jquery-1.3.2.js"></script>
	<script src="../js/dist/jquery-3.2.1.min.js"></script>
	<script src="../js/HtmlControl/jHtmlArea-0.8.min.js"></script>
	<link href="../js/HtmlControl/jHtmlArea.css" rel="stylesheet" />


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
						<asp:Label ID="lblheading" runat="server" Text=" View Recruitment List"></asp:Label>
					</span>
				</div>
				<%-- <div>
                        <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                    </div>--%>
				<div>
					<asp:Label runat="server" ID="lblmessagesearch" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
				</div>
				<div>
					<span style="margin-bottom: 20px">
						<a href="Requisition_Index.aspx" class="aaaa">Recruitment Home</a>
					</span>

				</div>

				<div class="edit-contact">
					<ul id="Ul1" runat="server">
						<li style="padding-top: 30px">
							<span>Skill Set</span>&nbsp;&nbsp;
                             <br />
							<asp:DropDownList runat="server" ID="lstSkillSet" CssClass="DropdownListSearch">
							</asp:DropDownList>
						</li>
						<li style="padding-top: 30px">
							<span>Location</span>&nbsp;&nbsp;
                             <br />
							<asp:DropDownList runat="server" ID="LstLocation" CssClass="DropdownListSearch">
							</asp:DropDownList>
						</li>
						<li style="padding-top: 30px">
							<span>Requisition Number</span>&nbsp;&nbsp;
                             <br />
							<asp:DropDownList runat="server" ID="lstRequisitionNo" CssClass="DropdownListSearch">
							</asp:DropDownList>
						</li>
						<li style="padding-top: 15px">
							<span>Band</span>&nbsp;&nbsp;
                             <br />
							<asp:DropDownList runat="server" ID="Lstband" CssClass="DropdownListSearch">
							</asp:DropDownList>
						</li>
						<li style="padding-top: 15px">
							<span>Department</span>&nbsp;&nbsp;
                             <br />
							<asp:DropDownList runat="server" ID="LstDepartment" CssClass="DropdownListSearch">
							</asp:DropDownList>
						</li>
						<li style="padding-top: 15px">
							<div runat="server" id="Status" visible="false">
								<span>Requisition Status</span>&nbsp;&nbsp;
                             <br />
								<asp:DropDownList runat="server" ID="LstRequistionStatus" CssClass="DropdownListSearch">
									<asp:ListItem Text="Select Status" Value="0"></asp:ListItem>
									<asp:ListItem Text="Open" Value="Open"></asp:ListItem>
									<asp:ListItem Text="Closed" Value="Closed"></asp:ListItem>
									<asp:ListItem Text="Cancelled" Value="Cancelled"></asp:ListItem>
								</asp:DropDownList>
							</div>

							<div runat="server" id="Div_Candidate" visible="false">
								<span>Candidate Name</span>&nbsp;&nbsp;
                             <br />
								<asp:DropDownList runat="server" ID="lstCandidateName" CssClass="DropdownListSearch">
								</asp:DropDownList>
							</div>
						</li>
					</ul>
					<asp:HiddenField ID="hdCandidate_ID" runat="server" />
				</div>

				<div class="edit-contact">
					<ul id="editform" runat="server">
						<li>
							<%--<span style="padding-top:20px">Additional Skillset </span>&nbsp;&nbsp;
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_AdditionalSkillset" runat="server"  MaxLength="50"></asp:TextBox>--%>
						</li>
						<li></li>
						<li></li>
					</ul>
					<%--<asp:HiddenField ID="hdCandidate_ID" runat="server" /> --%>
				</div>
			</div>
		</div>
	</div>

	<div class="mobile_Savebtndiv">
		<asp:LinkButton ID="mobile_btnSave" runat="server" Text="Submit" ToolTip="Search" OnClick="mobile_btnSave_Click" CssClass="Savebtnsve">Search </asp:LinkButton>
		<asp:LinkButton ID="mobile_btnBack" runat="server" Text="Clear Search" ToolTip="Clear Search" OnClick="mobile_btnBack_Click" class="trvl_Savebtndiv">Clear Search</asp:LinkButton>
	</div>

	<br />
	<div class="manage_grid" style="width: 100%; height: auto;">
		<asp:GridView ID="gvRecruterInoxList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="solid" BorderWidth="1px"
			DataKeyNames="Recruitment_ReqID" CellPadding="3" AutoGenerateColumns="False" Width="100%" OnPageIndexChanging="gvRecruterInoxList_PageIndexChanging" EditRowStyle-Wrap="false" PageSize="20" AllowPaging="True">
			<FooterStyle BackColor="White" ForeColor="#000066" />
			<HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
			<PagerStyle CssClass="gridpager" HorizontalAlign="Right" />
			<RowStyle ForeColor="#000066" />
			<SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
			<SortedAscendingCellStyle BackColor="#F1F1F1" />
			<SortedAscendingHeaderStyle BackColor="#007DBB" />
			<SortedDescendingCellStyle BackColor="#CAC9C9" />
			<SortedDescendingHeaderStyle BackColor="#00547E" />
			<Columns>
				<asp:TemplateField HeaderText="View" HeaderStyle-Width="8%">
				<ItemTemplate>
					<asp:ImageButton ID="lnkEdit" runat="server" Width="20px" Height="15px" OnClick="lnkEdit_Click" ImageUrl="~/Images/edit.png" />
				</ItemTemplate>
				<ItemStyle HorizontalAlign="Center" />
</asp:TemplateField>
				<asp:BoundField HeaderText="Requisition No" DataField="RequisitionNumber"
					ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="14%" />
				<asp:BoundField HeaderText="Department" DataField="Department_Name"
					HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="10%" />
				<asp:BoundField HeaderText="Skill Set" DataField="SkillSet"
					HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="8%" />
				<asp:BoundField HeaderText="Position Title" DataField="PositionTitle"
					HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="15%" />
				<asp:BoundField HeaderText="Band" DataField="Band"
					ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="4%" />
				<asp:BoundField HeaderText="Location" DataField="Location_name"
					HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="18%" />
				<asp:BoundField HeaderText="Requisition Date" DataField="RequisitionDate"
					ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="7%" />
				<asp:BoundField HeaderText="Required by date" DataField="RequiredbyDate"
					ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="7%" />
				<asp:BoundField HeaderText="Recruitment Status" DataField="RecruitmentStatus"
					ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="6%" />
				<asp:BoundField HeaderText="Requisition Status" DataField="Request_status"
					ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="6%" />

				<%--<asp:BoundField HeaderText="No Of Position"
                                DataField="NoOfPosition"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="3%" />--%>

				<%--<asp:BoundField HeaderText="Requestor Name"
                                DataField="fullNmae"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="15%" />--%>

				<%--<asp:BoundField HeaderText="Reason For Requisition"
                                DataField="ReasonRequisition"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="7%" />--%>
				 
			</Columns>
		</asp:GridView>

	</div>

	<br />

	<asp:HiddenField ID="hdLinkType" runat="server" />
	<asp:HiddenField ID="HFRecruitment_ReqID" runat="server" />
	<asp:HiddenField ID="HFCandidateID" runat="server" />
	<asp:HiddenField ID="HFISLMID" runat="server" />
	<asp:HiddenField ID="HFCandidateScheduleRound_ID" runat="server" />

	<script src="../js/dist/select2.min.js"></script>
	<link href="../js/dist/select2.min.css" rel="stylesheet" />

	<script type="text/javascript">      
		$(document).ready(function () {
			$(".DropdownListSearch").select2();
		});
	</script>

</asp:Content>


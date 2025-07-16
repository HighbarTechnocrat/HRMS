<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"  MaintainScrollPositionOnPostback="true"
	CodeFile="Ref_Moderator_View.aspx.cs" Inherits="procs_Ref_Moderator_View" %>

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
						<asp:Label ID="lblheading" runat="server" Text="Inbox Employee Referral"></asp:Label>
					</span>
				</div>
				<div>
					<asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>

				</div>

				<span>
				 <a href="Ref_Employee_Index.aspx" class="aaaa">Emp Referral Home</a>

				</span>
				<div class="edit-contact">
					<ul id="editform" runat="server">
						
						<li style="padding-top: 30px">
							<span>Referred By</span>&nbsp;&nbsp;
                             <br />
							<asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstEmployeeName">
							</asp:DropDownList>
						</li>
						<li style="padding-top: 30px">
							<span>Skill Set</span>&nbsp;&nbsp;
                             <br />
							<asp:DropDownList runat="server" ID="lstMainSkillset" CssClass="DropdownListSearch">
							</asp:DropDownList>
						</li>
						<li style="padding-top: 30px">
							<span>Gender</span>&nbsp;&nbsp;
                             <br />
							<asp:DropDownList runat="server" ID="lstCandidategender" CssClass="DropdownListSearch" Width="98%">
								<asp:ListItem Value="0" Text="Select Gender"></asp:ListItem>
								<asp:ListItem Value="1" Text="Male"></asp:ListItem>
								<asp:ListItem Value="2" Text="Female"></asp:ListItem>
							</asp:DropDownList>
						</li>
						<li style="padding-top: 15px">
							<span>Candidate Name</span>&nbsp;&nbsp;
                             <br />
							<asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstCandidateName">
							</asp:DropDownList>
						</li>
						<li style="padding-top: 15px">
							<span>Status Name</span>&nbsp;&nbsp;
                             <br />
							<asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstStatusName">
							</asp:DropDownList>
						</li>
						
						<li style="padding-top: 15px"></li>
					</ul>
				</div>
				<div class="mobile_Savebtndiv" style="margin-top:20px !important">
					<asp:LinkButton ID="mobile_btnSave" runat="server" Text="Submit" ToolTip="Search" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click">Search </asp:LinkButton>
					<asp:LinkButton ID="mobile_btnBack" runat="server" Text="Clear Search" OnClick="mobile_btnBack_Click" ToolTip="Clear Search" class="trvl_Savebtndiv">Clear Search</asp:LinkButton>
				</div>
				<div class="manage_grid" style="width: 100%; height: auto; padding-top: 20px;">
				<asp:Label runat="server" ID="RecordCount" Style="color:red;font-size:14px;"></asp:Label>
					<center>
                          <asp:GridView ID="gvMngTravelRqstList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                            DataKeyNames="Ref_Candidate_ID"   CellPadding="3" AutoGenerateColumns="False" Width="100%"   EditRowStyle-Wrap="false" PageSize="20" AllowPaging="True" OnPageIndexChanging="gvMngTravelRqstList_PageIndexChanging"   >
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
                            <asp:BoundField HeaderText="Referred By"
                                DataField="Emp_Name"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="12%" />

							<asp:BoundField HeaderText="Referred On"
                                DataField="CREATEDDate"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="8%" />

							 <asp:BoundField HeaderText="Candidate Name"
                                DataField="Ref_CandidateName"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="12%" />

							<asp:BoundField HeaderText="Candidate Email"
                                DataField="Ref_CandidateEmail"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="12%" />

							   <asp:BoundField HeaderText="Mobile No"
                                DataField="Ref_CandidateMobile"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="10%" />

							<asp:BoundField HeaderText="Gender"
                                DataField="Gender"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="5%" />

							<asp:BoundField HeaderText="Main Skillset"
                                DataField="ModuleDesc"   
								ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="15%" />

							<asp:BoundField HeaderText="Total Exp in (Years)"
                                DataField="Ref_CandidateTotalExperience"   
								ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="5%" />

							<asp:BoundField HeaderText="Relevant Exp in (Years)"
                                DataField="Ref_CandidateRelevantExperience"   
								ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="5%" />

                            <asp:BoundField HeaderText="Status Name"
                                DataField="StatusName"  
								 ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="13%" />

                            <asp:TemplateField HeaderText="View" HeaderStyle-Width="3%">
                                <ItemTemplate>
                                 <asp:ImageButton id="lnkView" runat="server" ToolTip="View" Width="15px" Height="15px"  ImageUrl="~/Images/edit.png" OnClick="lnkView_Click"/>
                                
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

    

                        </Columns>
                    </asp:GridView>
                    </center>
					<br />
					<br />
	<br />
				</div>

				
				<asp:HiddenField ID="hdnInboxType" runat="server" />
				<asp:HiddenField ID="hdnRecruitment_ReqID" runat="server" />
				<asp:HiddenField ID="FilePath" runat="server" />

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

	</script>
</asp:Content>




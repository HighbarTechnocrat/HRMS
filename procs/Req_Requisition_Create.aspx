<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" ValidateRequest="false"
	CodeFile="Req_Requisition_Create.aspx.cs" Inherits="Req_Requisition_Create" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
	Namespace="eWorld.UI" TagPrefix="ew" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />

	<link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Req_Requisition.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />

	<style>
		.noresize {
			resize: none;
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
		/*.disabledbutton*/
		iframe1 {
			pointer-events: none !important;
		}
		 #MainContent_Lnk_CTCWith_BAND {
            background: #3D1956;
            color: #febf39 !important;
            padding: 1.9% 5.4%;
            margin: 0% 0% 0 0;
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
						<asp:Label ID="lblheading" runat="server" Text="New Recruitment Requisition"></asp:Label>
					</span>
				</div>
				<div>
					<asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
				</div>
				<div>
					<span>
						<a href="Requisition_Index.aspx" class="aaaa">Recruitment Home</a>
					</span>
					 <span>
                       <a href="Req_Requisition_Details.aspx" title="Back" runat="server" visible="false" id="btnRecBack" style="margin-right: 10px;" class="aaaa">Back</a>
                    </span>
				</div>

				<div class="edit-contact">

					<ul id="editform" runat="server">

						<%--<li class="trvL_detail" id="litrvldetail" runat="server">
                            <asp:LinkButton ID="btnTra_Details" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" OnClick="btnTra_Details_Click"></asp:LinkButton>
                            <span id="spntrvldtls" runat="server">Requisition Details</span>
                        </li>--%>
						<%--<li></li>
                        <li></li>--%>
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

							</ul>
						</div>

						<li class="trvl_Accomodation" id="litrvlaccomodation" runat="server">
							<asp:LinkButton ID="trvl_accmo_btn" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" OnClick="LinkButton1_Click"></asp:LinkButton>
							<span id="spnaccomodation" runat="server">Position Details </span>
						</li>
						<li></li>
						<li></li>
						<div id="DivAccm" class="edit-contact" runat="server" visible="false">

							<ul>

								<li class="trvl_date Req_Position">

									<span>Position Title</span>&nbsp;&nbsp;<span style="color: red">*</span>
									<br />
									<asp:DropDownList runat="server" ID="lstPositionName" CssClass="DropdownListSearch" AutoPostBack="true" OnSelectedIndexChanged="lstPositionName_SelectedIndexChanged">
									</asp:DropDownList>
									<br />


								</li>


								<li class="trvl_date Req_Position">
									<span>Position Criticality</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
									<asp:DropDownList runat="server" ID="lstPositionCriti" CssClass="DropdownListSearch">
									</asp:DropDownList>
									<br />
								</li>
								<li class="trvl_date Req_Position">

									<span>Main Skillset </span>&nbsp;&nbsp;<span style="color: red">*</span>
									<br />
									<asp:DropDownList runat="server" ID="lstSkillset" AutoPostBack="true" CssClass="DropdownListSearch" OnSelectedIndexChanged="lstSkillset_SelectedIndexChanged">
									</asp:DropDownList>
									<br />

								</li>

									<li class="trvl_date Req_Position">

									<span>Position Location</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
									<asp:DropDownList runat="server" ID="lstPositionLoca" AutoPostBack="true" OnSelectedIndexChanged="lstPositionLoca_SelectedIndexChanged" CssClass="DropdownListSearch">
									</asp:DropDownList>
									<br />

								</li>
								<li class="trvl_date Req_Position">
									<span>Department Name</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
									<asp:DropDownList runat="server" ID="lstPositionDept" CssClass="DropdownListSearch" AutoPostBack="true" OnSelectedIndexChanged="lstPositionDept_SelectedIndexChanged">
									</asp:DropDownList>
									<br />
								</li>
								<li></li>
								<li class="trvl_date Req_Position" style="display: none">
									<span>Position Designation</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
									<asp:DropDownList runat="server" ID="lstPositionDesign" CssClass="DropdownListSearch">
									</asp:DropDownList>
									<br />
								</li>
								<li class="trvl_date" style="display: none">
									<span>Other Department</span>&nbsp;&nbsp;<span style="color: red"></span><br />
									<asp:TextBox AutoComplete="off" ID="txtOtherDept" runat="server"></asp:TextBox>
									<br />
								</li>
								<li class="trvl_date" style="display: none">
									<span>Position Designation Other</span>&nbsp;&nbsp;<span style="color: red"></span><br />
									<asp:TextBox AutoComplete="off" ID="txtPositionDesig" runat="server"></asp:TextBox>
								</li>

								<li class="trvl_date">
									<span>No Of Position</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
									<asp:TextBox AutoComplete="off" ID="txtNoofPosition" runat="server" Text="1" Enabled="false" MaxLength="2"></asp:TextBox>
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
									<asp:DropDownList runat="server" ID="lstReasonForRequi" CssClass="DropdownListSearch">
									</asp:DropDownList>
								</li>
								<li class="trvl_date Req_Position">

									<span>Preferred Employment Type</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
									<asp:DropDownList runat="server" ID="lstPreferredEmpType" CssClass="DropdownListSearch">
									</asp:DropDownList>
								</li>
								<li class="trvl_date Req_Position">
									<span>Band</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
									<asp:DropDownList runat="server" ID="lstPositionBand" CssClass="DropdownListSearch" AutoPostBack="true" OnSelectedIndexChanged="lstPositionBand_SelectedIndexChanged">
									</asp:DropDownList>
								</li>
								<li class="trvl_date Req_Salary">
									<div>
										<span>Salary Range(Lakh/Year)</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
										<asp:TextBox AutoComplete="off" ID="txtSalaryRangeFrom" Enabled="false" class="number" runat="server" MaxLength="4"></asp:TextBox>
										&nbsp;  To  &nbsp;
                                      <asp:TextBox AutoComplete="off" ID="txtSalaryRangeTo" Enabled="false" class="number" runat="server" MaxLength="5"></asp:TextBox>
									</div>
								</li>
								<li	><asp:LinkButton ID="Lnk_CTCWith_BAND" runat="server" Text="Update CTC Range" ToolTip="Update CTC Range" CssClass="Savebtnsve" OnClientClick="return CTCClick();" OnClick="Lnk_CTCWith_BAND_Click" Visible="false">Update CTC Range</asp:LinkButton>
                               </li>
							</ul>
						</div>

						<li class="trvl_local">
							<br />
							<asp:LinkButton ID="trvl_localbtn" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" OnClick="trvl_localbtn_Click"></asp:LinkButton>
							<span id="spnlocalTrvl" runat="server">Job Details </span>
						</li>
						<li></li>
						<li></li>
						<div id="Div_Locl" class="edit-contact" runat="server" visible="false">
							<ul>


								<li class="Req_Requi_Esse">
									<span>Essential Qualification</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
									<asp:TextBox AutoComplete="off" ID="txtEssentialQualifi" runat="server" Rows="7" CssClass="noresize" TextMode="MultiLine"></asp:TextBox>
								</li>
								<li class="Req_Requi_Esse">
									<span>Desired Qualification</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
									<asp:TextBox AutoComplete="off" ID="txtDesiredQualifi" runat="server" Rows="7" CssClass="noresize" TextMode="MultiLine"></asp:TextBox>
								</li>
								<li class="trvl_date Req_Salary">
									<span>Required Experience(Year)</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
									<asp:TextBox AutoComplete="off" ID="txtRequiredExperiencefrom" class="number" runat="server" MaxLength="4"></asp:TextBox>
									&nbsp; To &nbsp;
                            <asp:TextBox AutoComplete="off" ID="txtRequiredExperienceto" runat="server" class="number" MaxLength="4"></asp:TextBox>

								</li>



								<li class="trvl_date">
									<div style="display: none">
										<span>Recommended Person</span>&nbsp;&nbsp;<span style="color: red"></span><br />
										<asp:DropDownList runat="server" ID="lstRecommPerson" CssClass="DropdownListSearch">
										</asp:DropDownList>
									</div>
								</li>

								<li></li>
								<li class="Req_Job_Desc">
									<span>Job Description</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
									<asp:TextBox AutoComplete="off" ID="txtJobDescription" runat="server" Rows="7" TextMode="MultiLine"></asp:TextBox>
								</li>
								<li></li>
								<li></li>
								<li></li>

								<li>
									<asp:LinkButton ID="localtrvl_delete_btn" runat="server" Text="Questionnaire" ToolTip="Get JD From Bank" CssClass="Savebtnsve" Visible="false"> Get JD From Bank  </asp:LinkButton>

								</li>
								<li></li>
								<li class="trvl_date">


									<span>Assign Questionnaire</span>&nbsp;&nbsp;<span style="color: red"></span>

								</li>
								<li>

									<asp:LinkButton ID="lnkuplodedfile" runat="server" OnClientClick="DownloadFile1()"></asp:LinkButton>

								</li>
								<li class="trvl_date">
									<asp:LinkButton ID="accmo_cancel_btn" runat="server" Text="Questionnaire" ToolTip="Select Questionnaire" CssClass="Savebtnsve" Visible="false"> Select Questionnaire  </asp:LinkButton>

								</li>
								<br />
								<br />
								<li class="Req_Requi_Cmt" id="lsttrvlapprover" runat="server">
									<span>Comments</span>&nbsp;&nbsp;<span style="color: red"></span><br />
									<asp:TextBox AutoComplete="off" ID="txtComments" runat="server" CssClass="noresize" TextMode="MultiLine" Rows="5"></asp:TextBox>
								</li>
								<li class="trvl_date Req_Position">
									<span>Screening By</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
									<asp:DropDownList runat="server" ID="lstInterviewerOne" CssClass="DropdownListSearch">
									</asp:DropDownList></li>
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

						<div>
							<br />


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

									<asp:BoundField HeaderText="APPR_ID"
										DataField="APPR_ID"
										ItemStyle-HorizontalAlign="center"
										ItemStyle-Width="2%"
										ItemStyle-BorderColor="Navy"
										Visible="false" />

									<asp:BoundField HeaderText="Emp_Name"
										DataField="Emp_Name"
										ItemStyle-HorizontalAlign="center"
										ItemStyle-Width="1%"
										ItemStyle-BorderColor="Navy"
										Visible="false" />

									<asp:BoundField HeaderText="Emp_Emailaddress"
										DataField="Emp_Emailaddress"
										ItemStyle-HorizontalAlign="center"
										ItemStyle-Width="1%"
										ItemStyle-BorderColor="Navy"
										Visible="false" />

									<asp:BoundField HeaderText="A_EMP_CODE"
										DataField="A_EMP_CODE"
										ItemStyle-HorizontalAlign="center"
										ItemStyle-Width="1%"
										ItemStyle-BorderColor="Navy"
										Visible="false" />
								</Columns>
							</asp:GridView>
						</div>
					</ul>
				</div>

			</div>
		</div>

	</div>
	<asp:Panel ID="pnlJD" runat="server" CssClass="modalPopup" align="center" Style="display: none">

		<div class="userposts">
			<span>
				<asp:Label ID="Label3" runat="server" Text="Search JD From JD Bank"></asp:Label>
			</span>
		</div>
		<asp:UpdatePanel ID="UpdatePanel2" runat="server">
			<ContentTemplate>

				<div>
					<asp:Label runat="server" ID="lblmsg3" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; float: left; padding-left: 88px"></asp:Label>
				</div>
				<table class="TLQuestio">
					<tr>
						<td><span>For Skillset  </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList runat="server" ID="lstDGSkillset" AutoPostBack="true">
							</asp:DropDownList>
						</td>
						<td><span>For Position </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList runat="server" ID="lstDGPosition" AutoPostBack="true">
							</asp:DropDownList>
						</td>
					</tr>

				</table>
			</ContentTemplate>
		</asp:UpdatePanel>

		<div class="JD_Savebtndiv">
			<asp:LinkButton ID="localtrvl_cancel_btn" runat="server" Text="Filter JD Bank" ToolTip="Filter JD Bank" CssClass="Savebtnsve" OnClick="localtrvl_cancel_btn_Click">Filter JD Bank</asp:LinkButton>
			<asp:LinkButton ID="Oth_btnSave" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve">Back</asp:LinkButton>
		</div>
		<asp:UpdatePanel ID="UpdatePanel3" runat="server">
			<ContentTemplate>
				<div class="manage_grid" style="width: 92%; height: auto;">

					<center>
                          <asp:GridView ID="grdGetGD" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                            DataKeyNames="JD_BankDetail_ID"   CellPadding="3" AutoGenerateColumns="False" Width="100%"   EditRowStyle-Wrap="false" PageSize="20" AllowPaging="True"  >
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
                            <asp:BoundField HeaderText="Skill Set"
                                DataField="ModuleDesc"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="15%" />
                            <asp:BoundField HeaderText="Position Title"
                                DataField="PositionTitle"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="15%" />
                            <%--<asp:TemplateField  HeaderText="View JD" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center" >  
                             <ItemTemplate>  
                                   <asp:ImageButton id="lnkJDEdit" runat="server" Width="15px" visible="false" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkJDEdit_Click"/>
                                 </ItemTemplate>  
                        </asp:TemplateField>  --%>

                            <asp:TemplateField HeaderText="Select JD" HeaderStyle-Width="5%">
                                <ItemTemplate>
                                     <asp:CheckBox ID="CheckBox2" runat="server" />  
                                <%--<asp:ImageButton id="lnkEdit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkEdit_Click"/>--%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                    </center>
				</div>

				<div runat="server" id="JDShow" visible="false">
					<asp:TextBox AutoComplete="off" ID="txtJDShow" runat="server" Rows="7" TextMode="MultiLine"></asp:TextBox>
				</div>

			</ContentTemplate>
		</asp:UpdatePanel>
		<div class="JD_Savebtndiv">
			<asp:LinkButton ID="Oth_btnDelete" runat="server" Text="Cancel" ToolTip="Select JD Bank Details" CssClass="Savebtnsve" OnClick="Oth_btnDelete_Click" Visible="false">Select JD Bank Details</asp:LinkButton>
		</div>


		<%--<asp:Button ID="btnCancel" runat="server" Text="Cancel" />--%>
	</asp:Panel>

	<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderDG" runat="server"
		TargetControlID="localtrvl_delete_btn" PopupControlID="pnlJD"
		BackgroundCssClass="modalBackground" DropShadow="true" OkControlID="Oth_btnDelete1"
		OnOkScript="ok()" CancelControlID="Oth_btnSave" />


	<%--panel Question--%>
	<asp:Panel ID="PnlQuestio" runat="server" CssClass="modalPopup" align="center" Style="display: none">

		<div class="userposts">
			<span>
				<asp:Label ID="Label1" runat="server" Text="Search Questionnaire Recruitment"></asp:Label>
			</span>
		</div>
		<asp:UpdatePanel ID="updateQuest" runat="server">
			<ContentTemplate>

				<div>
					<asp:Label runat="server" ID="Label2" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; float: left; padding-left: 88px"></asp:Label>
				</div>
				<table class="TLQuestio">
					<tr>
						<td><span>For Skillset  </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList runat="server" ID="lstSkillsetQue" AutoPostBack="true">
							</asp:DropDownList>
						</td>
						<td><span>For Position </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList runat="server" ID="lstPositionQue" AutoPostBack="true">
							</asp:DropDownList>
						</td>
					</tr>

				</table>
			</ContentTemplate>
		</asp:UpdatePanel>
		<div class="Quest_Savebtndiv">
			<asp:LinkButton ID="mobile_btnSave" runat="server" Text="Filter Questionnaire" ToolTip="Filter Questionnaire" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click">Filter Questionnaire</asp:LinkButton>
			<asp:LinkButton ID="mobile_btnBack" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve">Back</asp:LinkButton>
		</div>
		<asp:UpdatePanel ID="UpdatePanel1" runat="server">
			<ContentTemplate>
				<div class="manage_grid" style="width: 92%; height: auto; padding-top: 20px;">

					<center>
                          <asp:GridView ID="gvMngTravelRqstList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                            DataKeyNames="AssignQuestionnaire_ID"   CellPadding="3" AutoGenerateColumns="False" Width="100%"   EditRowStyle-Wrap="false" PageSize="20" AllowPaging="True"  >
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
                            <asp:BoundField HeaderText="Skill Set"
                                DataField="ModuleDesc"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="15%" />
                            <asp:BoundField HeaderText="Position Title"
                                DataField="PositionTitle"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="15%" />
              
                            <asp:TemplateField  HeaderText="File View" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center">  
                               
                             <ItemTemplate >  
                                 <asp:HiddenField ID="AssignQuestiID" runat="server" Value='<%#Eval("AssignQuestionnaire_ID") %>'/>
                                 <asp:HiddenField ID="hfId" runat="server" Value='<%#Eval("UploadData") %>'/>
                                     <asp:LinkButton ID="lnkViewFiles" runat="server" Text='View' OnClientClick=<%# "DownloadFile('" + Eval("UploadData") + "')" %>  ></asp:LinkButton>                           
                            </ItemTemplate>  
                        </asp:TemplateField>  

                            <asp:TemplateField HeaderText="Select File" HeaderStyle-Width="5%">
                                <ItemTemplate>
                                     <asp:CheckBox ID="CheckBox1" runat="server" />  
                                <%--<asp:ImageButton id="lnkEdit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkEdit_Click"/>--%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                    </center>
				</div>
			</ContentTemplate>
		</asp:UpdatePanel>
		<div class="Quest_Savebtndiv">
			<asp:LinkButton ID="mobile_cancel" runat="server" Text="Cancel" ToolTip="Select Questionnaire File" CssClass="Savebtnsve" OnClick="mobile_cancel_Click" Visible="false">Select Questionnaire File</asp:LinkButton>

		</div>


		<%--<asp:Button ID="btnCancel" runat="server" Text="Cancel" />--%>
	</asp:Panel>

	<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderLogin" runat="server"
		TargetControlID="accmo_cancel_btn" PopupControlID="PnlQuestio"
		BackgroundCssClass="modalBackground" DropShadow="true" OkControlID="mobile_cancel1"
		OnOkScript="ok()" CancelControlID="mobile_btnBack" />
	<%--<Triggers>
<asp:AsyncPostBackTrigger ControlID = "mobile_btnSave" />
<asp:AsyncPostBackTrigger ControlID = "mobile_cancel" />
</Triggers>--%>


	<div class="Req_Savebtndiv">
		<br />
		<asp:LinkButton ID="localtrvl_btnSave" runat="server" Text="Save As Draft" ToolTip="Save As Draft" CssClass="Savebtnsve" OnClick="trvl_accmo_btn_Click" OnClientClick="return SaveMultiClick();"> Save As Draft </asp:LinkButton>
		<asp:LinkButton ID="trvldeatils_btnSave" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClick="trvldeatils_btnSave_Click" OnClientClick="return SaveMultiClick();">Submit</asp:LinkButton>
		
		<asp:LinkButton ID="trvldeatils_cancel_btn" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/Req_Requisition_Details.aspx">    Back</asp:LinkButton>

		<asp:LinkButton ID="accmo_delete_btn" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="accmo_delete_btn_Click" OnClientClick="return SaveMultiClick();" Visible="false">  Cancel Requisition  </asp:LinkButton>

	<%--Position Criticality--%>
		<asp:LinkButton ID="trvldeatils_delete_btn" runat="server" Text="Position Criticality Change" ToolTip="Position Criticality Change" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();" OnClick="trvldeatils_delete_btn_Click" Visible="false">Position Criticality Change</asp:LinkButton>
	
	</div>
	<br />
	<br />


	<asp:HiddenField ID="hflEmpDesignation" runat="server" />
	<asp:HiddenField ID="hflEmpDepartment" runat="server" />
	<asp:HiddenField ID="hflEmailAddress" runat="server" />
	<asp:HiddenField ID="hdnRecruitment_ReqID" runat="server" />
	<asp:HiddenField ID="hdnEmpCpde" runat="server" />
	<asp:HiddenField ID="hdnYesNo" runat="server" />
	<asp:HiddenField ID="hdnBankDetailID" runat="server" />
	<asp:HiddenField ID="hdnAssignQuestiID" runat="server" />
	<asp:HiddenField ID="hdnFilter" runat="server" />
	<asp:HiddenField ID="FilePath" runat="server" />
	<asp:HiddenField ID="FileName" runat="server" />
	<asp:HiddenField ID="hflapprcode" runat="server" />
	<asp:HiddenField ID="hflStatusID" runat="server" />
	<asp:HiddenField ID="hdnHOD" runat="server" />
	<asp:HiddenField ID="hdnHR" runat="server" />
	<asp:HiddenField ID="hdnnextappcode" runat="server" />
	<asp:HiddenField ID="hdnapprid" runat="server" />
	<asp:HiddenField ID="hdnApproverid_LWPPLEmail" runat="server" />
	<asp:HiddenField ID="hdnisApprover_TDCOS" runat="server" />
	<asp:HiddenField ID="HDNISDraft" runat="server" />
	<asp:HiddenField ID="hflStatusName" runat="server" />
	<asp:HiddenField ID="hdnRecrtStatus" runat="server" />

	<%--<ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchInterviewerOne" MinimumPrefixLength="3"
        CompletionInterval="3" EnableCaching="false" CompletionSetCount="3" TargetControlID="txtRecommPerson"
        ID="AutoCompleteExtender5" runat="server" FirstRowSelected="true">
    </ajaxToolkit:AutoCompleteExtender>

    <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchInterviewerOne" MinimumPrefixLength="3"
        CompletionInterval="3" EnableCaching="false" CompletionSetCount="3" TargetControlID="txtInterviewerOptOne"
        ID="AutoCompleteExtender6" runat="server" FirstRowSelected="true">
    </ajaxToolkit:AutoCompleteExtender>

    <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchInterviewerOne" MinimumPrefixLength="3"
        CompletionInterval="3" EnableCaching="false" CompletionSetCount="3" TargetControlID="txtInterviewerOptTwo"
        ID="AutoCompleteExtender3" runat="server" FirstRowSelected="true">
    </ajaxToolkit:AutoCompleteExtender>--%>



	<%--<ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchInterviewerOne" MinimumPrefixLength="3"
        CompletionInterval="3" EnableCaching="false" CompletionSetCount="3" TargetControlID="txtInterviewerTwo"
        ID="AutoCompleteExtender1" runat="server" FirstRowSelected="true">
    </ajaxToolkit:AutoCompleteExtender>


    <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchInterviewerOne" MinimumPrefixLength="3"
        CompletionInterval="3" EnableCaching="false" CompletionSetCount="3" TargetControlID="txtInterviewerOne"
        ID="AutoCompleteExtender2" runat="server" FirstRowSelected="true">
    </ajaxToolkit:AutoCompleteExtender>--%>


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
		function onOnlyNumber(e) {
			var keynum;
			var keychar;

			var numcheck = /[0123456789]/;

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
				var ele = document.getElementById('<%=trvl_accmo_btn.ClientID%>');

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
		function CTCClick() {
			try {
				var retunboolean = true;
				var ele = document.getElementById('<%=Lnk_CTCWith_BAND.ClientID%>');

				if (ele != null && !ele.disabled)
					retunboolean = true;
				else
					retunboolean = false;
				if (ele != null) {
					ele.disabled = true;
					if (retunboolean == true)
						CTCConfirm();
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
		function CTCConfirm() {
			//Testing();
			var confirm_value = document.createElement("INPUT");
			confirm_value.type = "hidden";
			confirm_value.name = "confirm_value";
			if (confirm("Do you want to Update CTC ?")) {
				confirm_value.value = "Yes";
			} else {
				confirm_value.value = "No";
			}
			//alert(confirm_value.value);
			//document.forms[0].appendChild(confirm_value);
			document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
			return;

		}
		function CancelMultiClick() {
			try {
				var retunboolean = true;
				var ele = document.getElementById('<%=accmo_delete_btn.ClientID%>');

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
		function CancelMultiClick() {
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

		function DownloadFile(FileName) {
			var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;

			window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
			// window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
		}
		function DownloadFile1() {
			var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;
			var FileName = document.getElementById("<%=FileName.ClientID%>").value;

			window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
			//window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
		}





	</script>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="Ref_CandidateReport.aspx.cs" 
    MaintainScrollPositionOnPostback="true"
    Inherits="procs_Ref_CandidateReport" %>

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

         .paging a {
            background-color: #C7D3D4;
            padding: 5px 7px;
            text-decoration: none;
            border: 1px solid #C7D3D4;
        }

            .paging a:hover {
                background-color: #E1FFEF;
                color: #00C157;
                border: 1px solid #C7D3D4;
            }

        .paging span {
            background-color: #E1FFEF;
            padding: 5px 7px;
            color: #00C157;
            border: 1px solid #C7D3D4;
        }

        tr.paging {
            background: none !important;
        }

            tr.paging tr {
                background: none !important;
            }

            tr.paging td {
                border: none;
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
						<asp:Label ID="lblheading" runat="server" Text=" Refer. Candidate History"></asp:Label>
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
							<asp:DropDownList runat="server" ID="lstSkillSet" CssClass="DropdownListSearch" Width="250px">
							</asp:DropDownList>
						</li>
						<li style="padding-top: 30px">
							<span>Refer By</span>&nbsp;&nbsp;
                             <br />
							<asp:DropDownList runat="server" ID="lstReferenceBy" CssClass="DropdownListSearch"  Width="250px">
							</asp:DropDownList>
						</li>
						<li style="padding-top: 30px">
							<span>Recruiter Name</span>&nbsp;&nbsp;
                             <br />
							<asp:DropDownList runat="server" ID="lstRecuitername" CssClass="DropdownListSearch"  Width="250px">
							</asp:DropDownList>
						</li>
                        <li style="padding-top: 7px">
                            <span>Refer Candidate Name</span>&nbsp;&nbsp;
                             <br />
                            <asp:TextBox ID="txtRefCandidateSearch" runat="server" MaxLength="100"></asp:TextBox>
							<asp:DropDownList runat="server" ID="DDlRefCandidateName" CssClass="DropdownListSearch"  Width="250px" Visible="false">
							</asp:DropDownList>

						</li>
						<li style="padding-top: 7px">
							<span>Refer Exp Year From</span>&nbsp;&nbsp;
                             <br />
                            <asp:TextBox ID="txtExpfrom" runat="server" MaxLength="4"></asp:TextBox>
						</li>
						<li style="padding-top: 7px">
							<span>Refer Exp Year To</span>&nbsp;&nbsp;
                             <br />
							<asp:TextBox ID="TxtExpTo" runat="server" MaxLength="4"></asp:TextBox>
						</li>
						
                        <li>
                             <span>Refer Candidate Status</span>&nbsp;&nbsp;
                             <br />
                            
							<asp:DropDownList runat="server" ID="DDLRefCandidateStatus" CssClass="DropdownListSearch"  Width="250px">
							</asp:DropDownList>
						</li>
                        <li>
							<span>Refer From Date</span>&nbsp;&nbsp;
                             <br />
							<asp:TextBox ID="txtfromdate" runat="server" MaxLength="10" AutoComplete="off" AutoPostBack="true" OnTextChanged="txtfromdate_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" Format="dd/MM/yyyy" TargetControlID="txtfromdate" runat="server">
                             </ajaxToolkit:CalendarExtender>
						</li>
						<li>
							<span>Refer To Date</span>&nbsp;&nbsp;
                             <br />
							<asp:TextBox ID="txttodate" runat="server" MaxLength="10" AutoComplete="off" AutoPostBack="true" OnTextChanged="txtfromdate_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txttodate" runat="server">
                             </ajaxToolkit:CalendarExtender>  
						</li>
						


					</ul>
					<asp:HiddenField ID="hdCandidate_ID" runat="server" />
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
		<asp:GridView ID="GVRefCandidate" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="solid" BorderWidth="1px"
			DataKeyNames="Ref_Candidate_ID" CellPadding="3" AutoGenerateColumns="False" Width="100%" OnPageIndexChanging="GVRefCandidate_PageIndexChanging" EditRowStyle-Wrap="false" PageSize="20" AllowPaging="True">
			<FooterStyle BackColor="White" ForeColor="#000066" />
			<HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
			<PagerStyle CssClass="gridpager" HorizontalAlign="Right" />
			<RowStyle ForeColor="#000066" />
			<SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
			<SortedAscendingCellStyle BackColor="#F1F1F1" />
			<SortedAscendingHeaderStyle BackColor="#007DBB" />
			<SortedDescendingCellStyle BackColor="#CAC9C9" />
			<SortedDescendingHeaderStyle BackColor="#00547E" />
             <PagerStyle HorizontalAlign = "Right" CssClass = "paging" />
			<Columns>
				<asp:BoundField HeaderText="Refer. By" DataField="ReferenceBy"
					ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" ItemStyle-Width="14%" />

				<asp:BoundField HeaderText="Refer. Date" DataField="ReferenceDate"
					HeaderStyle-HorizontalAlign="left" ItemStyle-Width="7%" />

				<asp:BoundField HeaderText="Refer. Candidate Name" DataField="Ref_CandidateName"
					HeaderStyle-HorizontalAlign="left" ItemStyle-Width="15%" />

				<asp:BoundField HeaderText="Refer. Main Skill" DataField="MainSkill"
					HeaderStyle-HorizontalAlign="left" ItemStyle-Width="12%" />

				<asp:BoundField HeaderText="Total Exp." DataField="TotalExp"
					ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" ItemStyle-Width="4%" />

				<asp:BoundField HeaderText="Rel Exp." DataField="RelExp"
					HeaderStyle-HorizontalAlign="left" ItemStyle-Width="4%" />

				<asp:BoundField HeaderText="Notice Period" DataField="NoticePeriod"
					ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" ItemStyle-Width="5%" />

				<asp:BoundField HeaderText="Req. No" DataField="RequisitionNumber"
					ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" ItemStyle-Width="12%" />

                <asp:BoundField HeaderText="Recruiter Name" DataField="RecruiterName"
					ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" ItemStyle-Width="10%" />

				<asp:BoundField HeaderText="Candidate Status" DataField="Ref_StatusID"
					ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" ItemStyle-Width="6%" />

                <asp:TemplateField HeaderText="Download Resume" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="2%">
							<ItemTemplate>
								<asp:ImageButton ID="lnkinterviewPhoto" runat="server" Width="15px" ToolTip="View Photo" Height="15px" ImageUrl="~/Images/Download.png"
									OnClientClick=<%# "DownloadResume('" + Eval("Ref_UploadResume") + "')" %> Visible='<%# String.IsNullOrEmpty(Convert.ToString(Eval("Ref_UploadResume"))) ? false : true %>' />
							</ItemTemplate>
							<ItemStyle HorizontalAlign="Center" />
						</asp:TemplateField>
				<%--<asp:TemplateField HeaderText="Download Resume" HeaderStyle-Width="8%">
					<ItemTemplate>
						<asp:ImageButton ID="lnkEdit" runat="server" Width="20px" Height="15px" OnClick="lnkEdit_Click" ImageUrl="~/Images/edit.png" />
					</ItemTemplate>
					<ItemStyle HorizontalAlign="Center" />
				</asp:TemplateField>--%>
			</Columns>
		</asp:GridView>

	</div>

	<br />

	<asp:HiddenField ID="hdLinkType" runat="server" />
	<asp:HiddenField ID="HFRecruitment_ReqID" runat="server" />
	<asp:HiddenField ID="HFCandidateID" runat="server" />
	<asp:HiddenField ID="HFISLMID" runat="server" />
	<asp:HiddenField ID="HFCandidateScheduleRound_ID" runat="server" />
    <asp:HiddenField ID="HFPathresume" runat="server" />

	<script src="../js/dist/select2.min.js"></script>
	<link href="../js/dist/select2.min.css" rel="stylesheet" />

	<script type="text/javascript">      
		$(document).ready(function () {
			$(".DropdownListSearch").select2();
        });

        function DownloadResume(file) {
			var localFilePath = document.getElementById("<%=HFPathresume.ClientID%>").value;
			window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
			//window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
		}


	</script>

    

</asp:Content>


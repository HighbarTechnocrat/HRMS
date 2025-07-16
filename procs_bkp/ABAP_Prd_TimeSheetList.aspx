<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="ABAP_Prd_TimeSheetList.aspx.cs" Inherits="procs_ABAP_Prd_TimeSheetList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

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
						<asp:Label ID="lblheading" runat="server" Text="Object Completion List"></asp:Label>
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
						<a href="Timesheet.aspx" class="aaaa">Object Completion Index</a>
					</span>

				</div>
			</div>
		</div>
	</div>

	<div class="mobile_Savebtndiv" style="padding-bottom:5px">
	</div>

	
	<div class="manage_grid" style="width:100%; height: auto;">
		<asp:GridView ID="gvTimeSheetList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="solid" BorderWidth="1px"
			 DataKeyNames="MainID" CellPadding="3" AutoGenerateColumns="False" Width="100%" OnPageIndexChanging="gvTimeSheetList_PageIndexChanging" EditRowStyle-Wrap="false" PageSize="10" AllowPaging="True">
			<FooterStyle BackColor="White" ForeColor="#000066" />
			<HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
			<PagerStyle CssClass="gridpager" HorizontalAlign="Right" />
			<RowStyle ForeColor="#000066" />
			<SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
			<SortedAscendingCellStyle BackColor="#F1F1F1" />
			<SortedAscendingHeaderStyle BackColor="#007DBB"/>
			<SortedDescendingCellStyle BackColor="#CAC9C9"/>
			<SortedDescendingHeaderStyle BackColor="#00547E" />
            <PagerStyle HorizontalAlign = "Right" CssClass = "paging" />
			<Columns>
				<asp:BoundField HeaderText="Week Start Date" DataField="Start_Datee"
					ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="8%" />

                <asp:BoundField HeaderText="Week End Date" DataField="End_datee"
					HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" />
				<asp:BoundField HeaderText="Object Count" DataField="ProjectCount"
					HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%" />
				<%--<asp:BoundField HeaderText="Status" DataField="Status"
					HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%" />
				
				--%>
				<asp:TemplateField HeaderText="View" HeaderStyle-Width="8%">
					<ItemTemplate>
						<asp:ImageButton ID="lnkEdit" runat="server" Width="20px" Height="15px" OnClick="lnkEdit_Click"  ImageUrl="~/Images/edit.png" />
					</ItemTemplate>
					<ItemStyle HorizontalAlign="Center" />
				</asp:TemplateField>
			</Columns>
		</asp:GridView>

	</div>

	

	<br />

	<asp:HiddenField ID="hdLinkType" runat="server" />
	<asp:HiddenField ID="HFIDD" runat="server" />
	<asp:HiddenField ID="HFISLMID" runat="server" />
	

	<script src="../js/dist/select2.min.js"></script>
	<link href="../js/dist/select2.min.css" rel="stylesheet" />

	<script type="text/javascript">      
		$(document).ready(function () {
			$(".DropdownListSearch").select2();
		});
	</script>

</asp:Content>


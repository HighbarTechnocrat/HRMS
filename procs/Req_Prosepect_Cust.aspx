<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
	CodeFile="Req_Prosepect_Cust.aspx.cs" Inherits="procs_Req_Prosepect_Cust" MaintainScrollPositionOnPostback="true" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
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

		.ViewFiles:hover {
			color: red;
			background-color: transparent;
			text-decoration: none !important;
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
						<asp:Label ID="lblheading" runat="server" Text="Create Prospect Customers"></asp:Label>
					</span>
				</div>
				<div>
					<asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
				</div>
				<span>
					<a href="Requisition_Index.aspx" class="aaaa">Recruitment  Home</a>

				</span>
				<div class="edit-contact">
					<ul id="editform" runat="server">

						<li class="trvl_date">
							<span>Prospect Customers Name  </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtProsp" Enabled="false" runat="server" Text="PROSP_" Width="15%" style="padding-left: 5px !important;padding-right: 0px !important;margin-right: 0px !important;"></asp:TextBox>
						
							<asp:TextBox AutoComplete="off" ID="txtProsepectcust" runat="server" Width="70%" style="padding-left: 5px !important;" onkeypress="return blockSpecialChar(event)"></asp:TextBox>
						</li>
						<li class="trvl_date">
							<span>Active </span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<asp:CheckBox runat="server" ID="chkActive" Checked="true"></asp:CheckBox>
						</li>
						<li class="trvl_date">
							<span> Delivery Head</span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList runat="server" ID="lstEmpName" CssClass="DropdownListSearch">
							</asp:DropDownList>
							<br />
							<br />
						</li>
							<div class="trvl_date">
								<asp:LinkButton ID="trvl_accmo_btn" runat="server" Text="Save" ToolTip="Save" CssClass="Savebtnsve" OnClick="trvl_accmo_btn_Click" OnClientClick="return SaveMultiClick();">Save</asp:LinkButton>								
							<asp:LinkButton ID="btnTra_Details" runat="server" Text="Clear" ToolTip="Clear" CssClass="Savebtnsve" OnClick="btnTra_Details_Click" >Clear</asp:LinkButton>															
							</div>
						
						<div style="display:none">
						<li class="trvl_date">
							<span>Search Prospect Customers</span>&nbsp;&nbsp;<span style="color: red"></span>
							<br />
							<asp:DropDownList runat="server" ID="lstProsepectcust" CssClass="DropdownListSearch">
							</asp:DropDownList>
						</li>

						<div>
							<br />

							<asp:LinkButton ID="mobile_btnSave" Text="Search" ToolTip="Search" runat="server" OnClick="mobile_btnSave_Click"></asp:LinkButton>
							<asp:LinkButton ID="mobile_cancel" runat="server" Text="Clear Search" ToolTip="Clear Search" CssClass="Savebtnsve" OnClick="mobile_cancel_Click">Clear Search</asp:LinkButton>
							</div>
						</div>
					</ul>
				</div>
				<div class="manage_grid" style="width: 100%; height: auto; padding-top: 10px;">

					
                          <asp:GridView ID="gvMngTravelRqstList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                            DataKeyNames="Prosepect_Cust_ID"   CellPadding="3" AutoGenerateColumns="False" Width="70%"   EditRowStyle-Wrap="false" PageSize="10" AllowPaging="true"  OnPageIndexChanging="gvMngTravelRqstList_PageIndexChanging" >
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
							 <asp:TemplateField HeaderText="View" HeaderStyle-Width="5%">
								<ItemTemplate>
								<asp:ImageButton id="lnkEdit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkEdit_Click"/>
								</ItemTemplate>
								<ItemStyle HorizontalAlign="Center" />
							</asp:TemplateField>

                            <asp:BoundField HeaderText="Prospect Cust Name"
                                DataField="Prosepect_Cust_Name"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="30%" />

							<asp:BoundField HeaderText="Employee Name"
                                DataField="Emp_Name"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="20%" />
                          
                             <asp:BoundField HeaderText="IsActive"
                                DataField="IsActive"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="10%" /> 

							 
                        </Columns>
                    </asp:GridView>
                   

					<br />
					<br />
					<br />
				</div>

			</div>
			<asp:HiddenField ID="hdnProsepect_ID" runat="server" />
			<asp:HiddenField ID="hdnClaimsID" runat="server" />
			<asp:HiddenField ID="hdnYesNo" runat="server" />
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
		function blockSpecialChar(e) {
			debugger;
			var k;			
			document.all ? k = e.keyCode : k = e.which;
			if (k == 95)
				return false;
			else
				return true;
       // return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k >= 48 && k <= 57));
        }
       

	</script>
</asp:Content>

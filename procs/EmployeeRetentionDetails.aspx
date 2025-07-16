<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" 
	 CodeFile="EmployeeRetentionDetails.aspx.cs" Inherits="procs_EmployeeRetentionDetails" %>

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
			/*background: #dae1ed;*/
			background: #ebebe4;
		}

		#MainContent_lstApprover {
			overflow: hidden !important;
		}

		#MainContent_lnk_Update_Profile,
		#MainContent_lnk_Cancle,
		#MainContent_lnk_Update,
		#MainContent_lnk_ed_Save {
			background: #3D1956;
			color: #febf39 !important;
			padding: 0.5% 1.4%;
			margin: 0% 0% 0 0;
		}

		.noresize {
			resize: none;
		}

		/*input.select2-search__field {
			padding-left: 0px !important;
			height: 0px !important;
		}*/

		#MainContent_lnkRevisedFile:link {
			color: red;
			background-color: transparent;
			text-decoration: none;
			font-size: 14px;
		}

		#MainContent_lnkRevisedFile:visited
		 {
			color: red;
			background-color: transparent;
			text-decoration: none;
		}

		#MainContent_lnkRevisedFile:hover
		{
			color: green;
			background-color: transparent;
			text-decoration: none !important;
		}		
	</style>
	
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
	<script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
	<div class="commpagesdiv">
		<div class="commonpages">
			<div class="wishlistpagediv">
				<span>
					<a href="ExitProcess_Index.aspx" style="margin-right: 18px;" class="aaaa">Exit Process Home</a>&nbsp;&nbsp; 
				    <a href="MyRetentionInbox.aspx" style="margin-right: 18px;" visible="false" runat="server" id="btBack" class="aaaa">Back</a>&nbsp;&nbsp; 
				
				</span>

				<div class="editprofile" style="text-align: center; border: none;" id="divmsg" runat="server" visible="false">
				</div>
				<div class="userposts">
					<span>
						<asp:Label ID="lblheading" runat="server" Text="Employee Retention Details"></asp:Label>
					</span>
				</div>
	
				<div class="edit-contact">
					<ul id="editform" runat="server" visible="true">

						<li class="mobile_InboxEmpName">
							<span>Employee Name</span> <span style="color:red">*</span>
							<br />
							<asp:DropDownList runat="server" ID="DDLEmpName" CssClass="DropdownList" AutoPostBack="true" OnSelectedIndexChanged="DDLEmpName_SelectedIndexChanged">
							</asp:DropDownList>
						</li>
						<li class="mobile_InboxEmpName">
							<span>Project Name </span><span style="color:red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_ProjectCode" Enabled="false" runat="server"></asp:TextBox>
						</li>
						<li class="mobile_inboxEmpCode">
							<span>Employee Code</span><span style="color:red">*</span><br />
							<asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" Enabled="false"> </asp:TextBox>
						</li>
		
						<li class="mobile_inboxEmpCode">
							<span>Department  </span><span style="color:red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_Department" Enabled="false" runat="server"></asp:TextBox>
						</li>
						<li class="mobile_inboxEmpCode">
							<span>Designation  </span><span style="color:red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txt_Designation" Enabled="false" runat="server"></asp:TextBox>
						</li>

						<li class="mobile_inboxEmpCode">
							<span>Date of Joining  </span><span style="color:red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txt_DOJ" Enabled="false" runat="server"></asp:TextBox>
						</li>

						<li class="mobile_inboxEmpCode">
							<span>Retention Type</span><span style="color:red">*</span>
							<br />
							<asp:DropDownList runat="server" ID="ddlRetentionType" CssClass="DropdownList" AutoPostBack="true" OnSelectedIndexChanged="ddlRetentionType_SelectedIndexChanged">
							</asp:DropDownList>

						</li>
						<li class="mobile_inboxEmpCode">
							<span>With Effect From </span><span style="color:red" runat="server" id="SPEffect" visible="false" >*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txt_WithEffectFrom" runat="server" Enabled="false" CssClass="OfferDates" ></asp:TextBox>
							<ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txt_WithEffectFrom"
								 runat="server">
							</ajaxToolkit:CalendarExtender>
						</li>
						<li class="mobile_inboxEmpCode">
							<span>Retention From Date</span><span style="color:red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtRetentionFromDate" runat="server" CssClass="OfferDates" AutoPostBack="true" OnTextChanged="lstPeriod_SelectedIndexChanged" ></asp:TextBox>
							<ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtRetentionFromDate"
								 runat="server">
							</ajaxToolkit:CalendarExtender>
						</li>
						<li class="mobile_inboxEmpCode">
							<span>Retention Period </span>
							<br />
							<asp:DropDownList runat="server" ID="lstPeriod" CssClass="DropdownList" AutoPostBack="true" OnSelectedIndexChanged="lstPeriod_SelectedIndexChanged">
								<asp:ListItem Text="Select Retention Period" Value="0"></asp:ListItem>
								<asp:ListItem Text="1" Value="1">6 Month</asp:ListItem>
								<asp:ListItem Text="2" Value="2">1 Year</asp:ListItem>
								<asp:ListItem Text="3" Value="3">1.5 Year</asp:ListItem>
								<asp:ListItem Text="4" Value="4">2 Year</asp:ListItem>
								
							</asp:DropDownList>
						</li>

						<li class="mobile_inboxEmpCode">
							<span>Retention Till Date</span><span style="color:red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtRetentionTillDate" runat="server" CssClass="OfferDates" ></asp:TextBox>
							<ajaxToolkit:CalendarExtender ID="CalendarExtender3" Format="dd/MM/yyyy" TargetControlID="txtRetentionTillDate"
								 runat="server">
							</ajaxToolkit:CalendarExtender>
						</li>
						<li class="mobile_inboxEmpCode">
							<div runat="server" id="h1">
							<span>Attachments </span><span style="color:red" id="SPUpload" runat="server" >*</span>
							<br />
							<asp:FileUpload runat="server" ID="FileUpload" AllowMultiple="true"></asp:FileUpload></div>
							
						</li>
						<li class="mobile_inboxEmpCode">
							<span>Remarks </span><span style="color:red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtRemark" runat="server" Style="width: 84%;" CssClass="noresize"  Rows="5" TextMode="MultiLine" onKeyUp="javascript:CountRemark(this);" ></asp:TextBox>
						</li><li></li>
						<li>
							<div style="margin-top:0% !important">
							<asp:GridView ID="GRDRetentionfile" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="92%"
								DataKeyNames="FileID">
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
									<asp:BoundField HeaderText="Attachments"
										DataField="FileNames"
										ItemStyle-HorizontalAlign="left"
										HeaderStyle-HorizontalAlign="left"
										ItemStyle-Width="40%" />
									<asp:TemplateField HeaderText="Download File" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
										<ItemTemplate>
											<asp:ImageButton ID="lnkViewFiles" runat="server" ToolTip="File View" Width="15px" Height="15px" ImageUrl="~/Images/Download.png" OnClientClick=<%# "DownloadFile('" + Eval("FileNames") + "')" %> />
										</ItemTemplate>
										<ItemStyle HorizontalAlign="Center" />
									</asp:TemplateField>
								</Columns>
							</asp:GridView>
							
								</div>
</li>
						
					</ul>
					<ul id="UlModeration" runat="server" visible="false">
						<hr />
						<li class="mobile_inboxEmpCode">
							<br />
							<span><b>Moderation Details</b></span><br />
							<br />
						</li>
						<li></li>
						
						<li><span>Status  </span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtStatus" Enabled="false" runat="server"></asp:TextBox>
						</li>
						<li>
							<div id="ApprovalDate" runat="server" >
							<span>Approval Date</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtApprovalDate" Enabled="false" runat="server"></asp:TextBox>
						</div>
								</li>
						<li>
							<span>Remark  </span><span style="color:red"></span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtMRemark" Enabled="false" Style="width: 84%;" CssClass="noresize" runat="server" Rows="5" TextMode="MultiLine" onKeyUp="javascript:CountRemark(this);"></asp:TextBox>
						</li>
						<li></li>
						<li>
							<span>Revised Letter File </span><span style="color:red">*</span>
							<br /><br />
							<asp:LinkButton ID="lnkRevisedFile" runat="server" OnClientClick="DownloadMFile()"></asp:LinkButton>
							<br />
							</li>
						
					</ul>



					<div class="Req_Savebtndiv" style="text-align: center">
						<div style="padding-bottom: 15px;">
							<asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>

						</div>
						<asp:LinkButton ID="lnk_Update_Profile" Visible="true" runat="server" Text="Submit" ToolTip="Submit" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();" OnClick="lnk_Update_Profile_Click"></asp:LinkButton>
						<asp:LinkButton ID="lnk_Update" Visible="false" runat="server" Text="Update" ToolTip="Update" CssClass="Savebtnsve" OnClientClick="return UpdateMultiClick();" OnClick="lnk_Update_Click"></asp:LinkButton>
						<asp:LinkButton ID="lnk_Cancle" Visible="true" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/ExitProcess_Index.aspx"></asp:LinkButton>

						<br />
						<br />
					</div>
				</div>
				<br />
				<br />
			</div>

		</div>
	</div>
	<asp:HiddenField ID="hdnEmpCode" runat="server" />
	<asp:HiddenField ID="hdnModerationEmail" runat="server" />
	<asp:HiddenField ID="hdnModerationCode" runat="server" />
	<asp:HiddenField ID="hdnModerationName" runat="server" />
	<asp:HiddenField ID="hdnFileName" runat="server" />
	<asp:HiddenField ID="hdnFilePath" runat="server" />
	<asp:HiddenField ID="hdnAdminFile" runat="server" />
	<asp:HiddenField ID="hdnYesNo" runat="server" />
	<asp:HiddenField ID="hdnRetentionID" runat="server" />
	<asp:HiddenField ID="hdnStatus_ID" runat="server" />
	<asp:HiddenField ID="FilePath" runat="server" />

	<script src="../js/dist/jquery-3.2.1.min.js"></script>
	<script src="../js/dist/select2.min.js"></script>
	<link href="../js/dist/select2.min.css" rel="stylesheet" />

	<script type="text/javascript">      
		$(document).ready(function () {
			$(".DropdownList").select2();
			
		});
	</script>
	<script type="text/javascript">


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

		function CountRemark(text) {
			var maxlength = 1500;
			var object = document.getElementById(text.id)
			if (object.value.length > maxlength) {
				object.focus();
				object.value = text.value.substring(0, maxlength);
				object.scrollTop = object.scrollHeight;
				return false;
			}
			return true;
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

		function SaveMultiClick() {
			try {

				var retunboolean = true;
				var ele = document.getElementById('<%=lnk_Update_Profile.ClientID%>');

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

		function UpdateMultiClick() {
			try {

				var retunboolean = true;
				var ele = document.getElementById('<%=lnk_Update.ClientID%>');

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
		// Delete

		//End Delete
		function onCharOnlyNumber_EXP(e) {
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
		function onCharOnlyNumber_Mobile(e) {
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

		function fValidFileExt(vfilePath) {

			var vFileName = vfilePath.split('\\').pop();
			var varext = vFileName.substring(vFileName.lastIndexOf(".") + 1, vFileName.length).toLowerCase();
			var result = varext.toLowerCase();
			if (result != "pdf") {
				alert("File Extension Is InValid - Only Upload pdf file");
				$('#MainContent_uploadfilesigned').val('');
			}
		}
		function DownloadMFile() {
			var localFilePath = document.getElementById("<%=hdnFilePath.ClientID%>").value;
			var FileName = document.getElementById("<%=lnkRevisedFile.ClientID%>").value;
			window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
			//window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
		}
		function DownloadFile(FileName) {
			var localFilePath = document.getElementById("<%=hdnFilePath.ClientID%>").value;
			window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
			//window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
		}

		$('.OfferDates').keydown(function (e) {
			var k;
			document.all ? k = e.keyCode : k = e.which;
			if (k == 8 || k == 46)
				return false;
			else
				return true;

		});

	</script>

</asp:Content>


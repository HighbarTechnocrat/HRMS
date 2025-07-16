<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" ValidateRequest="false"
	CodeFile="App_Latter_Approval.aspx.cs" Inherits="procs_App_Latter_Approval" %>

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
		#MainContent_btn_FD_Cancel,
		#MainContent_lnk_ed_Save,
		#MainContent_lnk_Correction
		{
			background: #3D1956;
			color: #febf39 !important;
			padding: 0.5% 1.4%;
			margin: 0% 0% 0 0;
		}

		.noresize {
			resize: none;
		}

		input.select2-search__field {
			padding-left: 0px !important;
			height: 0px !important;
		}

		#MainContent_lnkuplodedfile:link ,
		#MainContent_lnkSinedFile:link {
			color: red;
			background-color: transparent;
			text-decoration: none;
			font-size: 14px;
		}

		#MainContent_lnkuplodedfile:visited ,
		#MainContent_lnkSinedFile:visited {
			color: red;
			background-color: transparent;
			text-decoration: none;
		}

		#MainContent_lnkuplodedfile:hover,
		#MainContent_lnkSinedFile:hover {
			color: green;
			background-color: transparent;
			text-decoration: none !important;
		}
.scrollbar-width-thin{
			width: 200%; height: 350px !important;  overflow-y: scroll; border: solid 1px #aaa; padding: 15px;
		}
		
		
	</style>
	
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
	<script src="../js/dist/jquery-3.2.1.min.js"></script>
	<script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
	<script src="../js/HtmlControl/jHtmlArea-0.8.min.js"></script>
	<link href="../js/HtmlControl/jHtmlArea.css" rel="stylesheet" />


	<div class="commpagesdiv">
		<div class="commonpages">
			<div class="wishlistpagediv">
				<span>
					<a href="App_Latter_Index.aspx" style="margin-right: 18px;" class="aaaa">APP Update</a>&nbsp;&nbsp; 
				</span>
				 <span>
                       <a href="App_Latter_M_Index.aspx?itype=Pending" title="Back" runat="server"  id="btnRecBack" style="margin-right: 10px;" class="aaaa">Back</a>
                    </span>
					<div class="editprofile" style="text-align: center; border: none;" id="divmsg" runat="server" visible="false">
					</div>
					<div class="userposts">
						<span>
							<asp:Label ID="lblheading" runat="server" Text="Appointment Letter Approval Details"></asp:Label>
						</span>
					</div>
				<div class="edit-contact">
					<ul id="editform" runat="server" visible="false">
						
						<li class="mobile_InboxEmpName">
							<span>Employee Name</span><br />
							<asp:TextBox AutoComplete="off" ID="txtEmpName" runat="server" Enabled="false"></asp:TextBox>
						</li>
						<li class="mobile_InboxEmpName">
							<span>Project Code </span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_ProjectCode" Enabled="false" runat="server"></asp:TextBox>
						</li>
						<li class="mobile_inboxEmpCode">
							<span>Employee Code</span><br />

							<asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" Enabled="false"> </asp:TextBox>

						</li>
						<li class="mobile_inboxEmpCode">
							<span>Band</span><br />

							<asp:TextBox AutoComplete="off" ID="Txt_Band" runat="server" Enabled="false"> </asp:TextBox>

						</li>
						<li class="mobile_inboxEmpCode">
							<span>Department  </span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_Department" Enabled="false" runat="server"></asp:TextBox>
						</li>
						<li class="mobile_inboxEmpCode">
							<span>Designation  </span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txt_Designation" Enabled="false" runat="server"></asp:TextBox>
						</li>

						<li class="mobile_inboxEmpCode">
							<span>Date of Joining  </span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txt_DOJ" Enabled="false" runat="server"></asp:TextBox>
						</li>

						<li class="mobile_inboxEmpCode">
							<span>Email Address </span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txt_EmailAddress" Enabled="false" runat="server"></asp:TextBox>
						</li>
						<li class="mobile_inboxEmpCode">
							<span>Appointment Issued Date </span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txt_App_Issued_Date" Enabled="false" runat="server"></asp:TextBox>
						</li>
						<li class="mobile_inboxEmpCode">
							<span>Appointment Letter File </span>
							<br />
							<asp:LinkButton ID="lnkuplodedfile" runat="server" OnClientClick="DownloadAdminFile()"></asp:LinkButton>

						</li>
						<li><span>Acceptance Date</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtAcceptanceDate" Enabled="false" runat="server"></asp:TextBox>
					</li>
						<li>
							<br />
							 Signed Appointment Letter  File<span style="color: red">* </span>
							<asp:FileUpload ID="uploadfilesigned" Visible="false" runat="server" AllowMultiple="false" accept=".pdf" onchange="fValidFileExt(this.value);" />
							<br />
							<asp:LinkButton ID="lnkSinedFile" runat="server" OnClientClick="DownloadFile()"></asp:LinkButton>
							
						</li>
						<li>
							<span>Terms and conditions</span>
							<div class="scrollbar-width-thin">
								<asp:Literal ID="ltTable" runat="server" />
							</div>
						</li>
						<li></li>
						<li style="width: 60%">
							<span style="color: red">*</span>
							<asp:CheckBox AutoComplete="off" Enabled="false" CssClass="AcceptedAgreement" Text="" ID="chk_Read_Acceptance" runat="server"></asp:CheckBox>
							<span>By clicking here, I state that I have read and understood the terms and conditions. </span>


						</li>
						<li></li>
						
						<hr />
						<li class="mobile_inboxEmpCode">
							<br />
                            <span><b>Moderation Details</b></span><br />
                            <br />
                        </li>
						<li>
						</li>
						<%--<li><span>Moderation Name  </span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtModerationName" Enabled="false" runat="server"></asp:TextBox>
					</li>--%>
						<li><span>Approval Date <span style="color:red"> * </span></span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtApprovalDate" Enabled="false" runat="server"></asp:TextBox>
					</li>
						<li><span>Status <span style="color:red"> * </span>  </span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtStatus" Enabled="false" runat="server"></asp:TextBox>
					</li>
						
						<li>
							<span>Remark  <span style="color:red"> * </span> </span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtRemark"  style="width:83%;"  onKeyUp="javascript:Count(this);" CssClass="noresize"  runat="server" Rows="5" TextMode="MultiLine" ></asp:TextBox>
				
						</li>
					</ul>
					
								
					<div class="Req_Savebtndiv" style="text-align:center">
						<div style="padding-bottom:15px;">
		<asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
				
							</div>		
					    <asp:LinkButton ID="lnk_Update_Profile" Visible="true" runat="server" Text="Approve" ToolTip="Approve" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();" OnClick="lnk_Update_Profile_Click"></asp:LinkButton>	
						<asp:LinkButton ID="lnk_Correction" Visible="true" runat="server" Text="Send For Correction" ToolTip="Send For Correction" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();" OnClick="lnk_Correction_Click"></asp:LinkButton>
						<asp:LinkButton ID="lnk_Cancle" Visible="true" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/App_Latter_M_Index.aspx?itype=Pending"></asp:LinkButton>

							<br />
							<br />
					</div>
					</div>
					<br />
					<br />
				</div>
			
		</div>
	</div>
	<asp:HiddenField ID="hdnvouno" runat="server" />
	<asp:HiddenField ID="hdnModerationEmail" runat="server" />
	<asp:HiddenField ID="hdnModerationCode" runat="server" />
	<asp:HiddenField ID="hdnModerationName" runat="server" />
	<asp:HiddenField ID="hdnAppointment_ID" runat="server" />
	<asp:HiddenField ID="hdnFileName" runat="server" />
	<asp:HiddenField ID="hdnFilePath" runat="server" />
	<asp:HiddenField ID="hdnAdminFile" runat="server" />
	<asp:HiddenField ID="hdnYesNo" runat="server" />
	<asp:HiddenField ID="hdnempcode" runat="server" />
	<asp:HiddenField ID="hdnType" runat="server" />
	<asp:HiddenField ID="hdnCreateByEmail" runat="server" />

	<asp:HiddenField ID="FilePath" runat="server" />

	<%--<script src="../js/dist/jquery-3.2.1.min.js"></script>--%>
	<script src="../js/dist/select2.min.js"></script>
	<link href="../js/dist/select2.min.css" rel="stylesheet" />

	<script type="text/javascript">      
		$(document).ready(function () {
			$("#MainContent_ddl_Relation").select2();
			$("#MainContent_txtJobDescription").htmlarea();
			//$("#MainContent_txtJobDescription").htmlarea({
			//	// Override/Specify the Toolbar buttons to show
			//	toolbar: [
			//		["bold", "italic", "underline", "|", "forecolor"],
			//		["p", "h1", "h2", "h3", "h4", "h5", "h6"],
			//		["link", "unlink", "|", "image"],
			//		[{
			//			// This is how to add a completely custom Toolbar Button
			//			css: "custom_disk_button",
			//			text: "Save",
			//			action: function (btn) {
			//				// 'this' = jHtmlArea object
			//				// 'btn' = jQuery object that represents the <A> "anchor" tag for the Toolbar Button
			//				alert('SAVE!\n\n' + this.toHtmlString());
			//			}
			//		}]
			//	],
			//});
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
		function DownloadAdminFile(FileName) {
			var localFilePath = document.getElementById("<%=hdnAdminFile.ClientID%>").value;
			var FileName = document.getElementById("<%=lnkuplodedfile.ClientID%>").innerText;
			window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
			//window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
		}
			function DownloadFile() {
			var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;
			var FileName = document.getElementById("<%=lnkSinedFile.ClientID%>").innerText;
			window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
			//window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
		}
	</script>

</asp:Content>



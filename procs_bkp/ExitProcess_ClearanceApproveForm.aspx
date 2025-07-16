<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="ExitProcess_ClearanceApproveForm.aspx.cs" 
    Inherits="procs_ExitProcess_ClearanceApproveForm" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
	Namespace="eWorld.UI" TagPrefix="ew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />

	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/ClaimMobile_css.css" type="text/css" media="all" />
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
	<script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
	<script type="text/javascript">
		var deprt;
		$(document).ready(function () {
		});
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
		function Confirm() {
			var confirm_value = document.createElement("INPUT");

			confirm_value.type = "hidden";
			confirm_value.name = "confirm_value";

			if (confirm("Do you want to save data?")) {
				confirm_value.value = "Yes";
			}
			else {
				confirm_value.value = "No";
			}

			document.forms[0].appendChild(confirm_value);
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

		function confirm_meth() {
			if (confirm("Do you want to submit data?") == true) {
				$("#mobile_btnSave").click();
			}
			else {

			}
		}

	</script>

	<div class="commpagesdiv">
		<div class="commonpages">
			<div class="wishlistpagediv">

				<div class="userposts">
					<span>
						<asp:Label ID="lblheading" runat="server" Text="Clearance Form"></asp:Label>
					</span>
				</div>
				<div class="edit-contact">
					<a href="ExitProcess_Index.aspx" class="aaa">Exit Process Menu</a>
					<span>
                       <a href="ExitProcess_ClearanceInbox.aspx" title="Back" runat="server" visible="false"  id="btnRecBack" style="margin-right: 10px;" class="aaaa">Back</a>
                    </span>
					<span>
                       <a href="ExitProcess_ApprovedClearance.aspx" title="Back" runat="server"  visible="false"  id="btnViewBack" style="margin-right: 10px;" class="aaaa">Back</a>
                    </span>
					<%--<asp:Panel ID="pnlSurvey" runat="server">
                        </asp:Panel>--%>
					<ul id="CreateExitSurveyform" runat="server" visible="true">
						<li>
							<asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
							<%--<asp:TextBox runat="server" ID="lblmessage1" Visible="False" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;" OnTextChanged="lblmessage1_TextChanged"></asp:TextBox>--%>
						</li>
						<br />
						<li><b>Project Name</b>
							<asp:TextBox ID="txtProjectName" runat="server" Enabled="false"></asp:TextBox>
						</li>
						<br />
						<li><b>Resignation Date</b>
							<asp:TextBox ID="txtResignationDate" runat="server" Enabled="false"></asp:TextBox>
						</li>
						<br />
						<li><b>Name</b>
							<asp:TextBox ID="txtName" runat="server" Enabled="false"></asp:TextBox>
						</li>
						<br />
						<li><b>Designation & Grade</b>
							<asp:TextBox ID="txtDesignationGrade" runat="server" Enabled="false"></asp:TextBox>
						</li>
						<br />
						<li><b>Date of Joining</b>
							<asp:TextBox ID="txtDoJ" runat="server" Enabled="false"></asp:TextBox>
						</li>
						<br />
						<li><b>Last Working Day</b>
							<asp:TextBox ID="txtLastWorkingDay" runat="server" Enabled="false"></asp:TextBox>
						</li>
						<br />
						<li><b>Release Date</b>
							<asp:TextBox ID="txtDateRelease" runat="server" Enabled="false"></asp:TextBox>
						</li>
					</ul>
					<div id="divCurrentDept" class="edit-contact" runat="server">
						<ul>
							<li>
								<div class="userposts">
									<span>
										<asp:Label ID="Label1" runat="server" Text="Current Department Clearance"></asp:Label>
									</span>
								</div>
							</li>
							<br />
							<li><b>(A) Files and document handed over to</b>&nbsp;<span style="color: red">*</span>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator29" ErrorMessage="This field is mandatory."
									ControlToValidate="txt1011" runat="server" ForeColor="Red" Display="Dynamic" />
								<asp:TextBox ID="txt1011" runat="server" MaxLength="200" AutoComplete="off" AutoCompleteType="Disabled"></asp:TextBox>

							</li>
							<br />
							<li><b>Files and document handed over on:</b>&nbsp;<span style="color: red">*</span>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="This field is mandatory."
									ControlToValidate="txt1012" runat="server" ForeColor="Red" Display="Dynamic" />
								<asp:TextBox AutoComplete="off" ID="txt1012" runat="server" AutoPostBack="false" AutoCompleteType="Disabled" MaxLength="10"></asp:TextBox>
								<ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txt1012"
									Format="dd/MM/yyyy" runat="server">
								</ajaxToolkit:CalendarExtender>

							</li>
							<br />
							<li><b>(B) Handing over Note submitted</b>&nbsp;<span style="color: red">*</span>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="This field is mandatory."
									ControlToValidate="txt1013" runat="server" ForeColor="Red" Display="Dynamic" />
								<asp:TextBox ID="txt1013" runat="server" MaxLength="200" AutoComplete="off" AutoCompleteType="Disabled"></asp:TextBox>

							</li>
							<br />
							<li><asp:Label ID="LblCurrentDept" runat="server" Visible="false"><b> Submitted By</b> </asp:Label>&nbsp;		
								<b><asp:Label ID="LblCurrentDeptCode" runat="server" Visible="false"></asp:Label></b>
								
							</li>
							<br />
							<li style="padding-top:20px">
								<asp:Label ID="LblCurrentDeptD" runat="server" Visible="false"><b> Submitted On</b> </asp:Label>&nbsp;		
								<b><asp:Label ID="LblCurrentDeptDate" runat="server" Visible="false"></asp:Label></b>
							</li>
							<br />

						</ul>
					</div>

                    <div id="DivHODCurrentDept" class="edit-contact" runat="server">
						<ul>
							<li>
								<div class="userposts">
									<span>
										<asp:Label ID="Label6" runat="server" Text="HOD Current Department Clearance"></asp:Label>
									</span>
								</div>
							</li>
							<br />
							<li><b>(A) Files and document handed over to</b>&nbsp;<span style="color: red">*</span>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator35" ErrorMessage="This field is mandatory."
									ControlToValidate="TextBox1061" runat="server" ForeColor="Red" Display="Dynamic" />
								<asp:TextBox ID="TextBox1061" runat="server" MaxLength="200" AutoComplete="off" AutoCompleteType="Disabled"></asp:TextBox>

							</li>
							<br />
							<li><b>Files and document handed over on:</b>&nbsp;<span style="color: red">*</span>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator36" ErrorMessage="This field is mandatory."
									ControlToValidate="TextBox1062" runat="server" ForeColor="Red" Display="Dynamic" />
								<asp:TextBox AutoComplete="off" ID="TextBox1062" runat="server" AutoPostBack="false" AutoCompleteType="Disabled" MaxLength="10"></asp:TextBox>
								<ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="TextBox1062"
									Format="dd/MM/yyyy" runat="server">
								</ajaxToolkit:CalendarExtender>

							</li>
							<br />
							<li><b>(B) Handing over Note submitted</b>&nbsp;<span style="color: red">*</span>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator37" ErrorMessage="This field is mandatory."
									ControlToValidate="TextBox1063" runat="server" ForeColor="Red" Display="Dynamic" />
								<asp:TextBox ID="TextBox1063" runat="server" MaxLength="200" AutoComplete="off" AutoCompleteType="Disabled"></asp:TextBox>

							</li>
							<br />
							<li><asp:Label ID="LblHODCurrentDept" runat="server" Visible="false"><b> Submitted By</b> </asp:Label>&nbsp;		
								<b><asp:Label ID="LblHODCurrentDeptCode" runat="server" Visible="false"></asp:Label></b>
								
							</li>
							<br />
							<li style="padding-top:20px">
								<asp:Label ID="LblHODCurrentDeptD" runat="server" Visible="false"><b> Submitted On</b> </asp:Label>&nbsp;		
								<b><asp:Label ID="LblHODCurrentDeptDate" runat="server" Visible="false"></asp:Label></b>
							</li>
							<br />

						</ul>
					</div>



					<div id="divAdminDept" class="edit-contact" runat="server">
						<ul>
							<li>
								<div class="userposts">
									<span>
										<asp:Label ID="Label2" runat="server" Text="Admin Department Clearance"></asp:Label>
									</span>
								</div>
							</li>
							<br />
							<li><b>(A) Guesthouse/Accommodation related recovery</b><span style="color: red">*</span>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="This field is mandatory."
									ControlToValidate="txt1021" runat="server" ForeColor="Red" Display="Dynamic" />
								<asp:TextBox AutoComplete="off" ID="txt1021" runat="server" MaxLength="10" AutoPostBack="false" AutoCompleteType="Disabled"></asp:TextBox>
								<%--<ajaxToolkit:CalendarExtender ID="CalendarExtender3" TargetControlID="txt1021"
									Format="dd/MM/yyyy" runat="server">
								</ajaxToolkit:CalendarExtender>--%>

							</li>
							<br />
							<li><b>(B) Canteen Charges due</b><span style="color: red">*</span>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator4" ErrorMessage="This field is mandatory."
									ControlToValidate="txt1022" runat="server" ForeColor="Red" Display="Dynamic" />
								<asp:TextBox ID="txt1022" runat="server" MaxLength="200" AutoComplete="off" AutoCompleteType="Disabled"></asp:TextBox>

							</li>
							<br />
							<li><b>(C) Any other outstanding dues/deficiency :- like stationery,calculators, furniture, keys, Files etc.</b><span style="color: red">*</span>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator5" ErrorMessage="This field is mandatory."
									ControlToValidate="txt1023" runat="server" ForeColor="Red" Display="Dynamic" />
								<asp:TextBox ID="txt1023" runat="server" MaxLength="200" AutoComplete="off" AutoCompleteType="Disabled"></asp:TextBox>

							</li>
							<br />
							<li><b>(D) Visiting Card submitted</b><span style="color: red">*</span>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator6" ErrorMessage="This field is mandatory."
									ControlToValidate="txt1024" runat="server" ForeColor="Red" Display="Dynamic" />
								<asp:TextBox ID="txt1024" runat="server" MaxLength="200" AutoComplete="off" AutoCompleteType="Disabled"></asp:TextBox>

							</li>
							<br />
							<li><b>(E) I – card submitted</b><span style="color: red">*</span>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator7" ErrorMessage="This field is mandatory."
									ControlToValidate="txt1025" runat="server" ForeColor="Red" Display="Dynamic" />
								<asp:TextBox ID="txt1025" runat="server" MaxLength="200" AutoComplete="off" AutoCompleteType="Disabled"></asp:TextBox>

							</li>
							<br />
							<li><b>(F) Mobile phone / connection submitted(where ever applicable )</b><span style="color: red">*</span>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator8" ErrorMessage="This field is mandatory."
									ControlToValidate="txt1026" runat="server" ForeColor="Red" Display="Dynamic" />
								<asp:TextBox ID="txt1026" runat="server" MaxLength="200" AutoComplete="off" AutoCompleteType="Disabled"></asp:TextBox>

							</li>
                            <br />
							<li><b>(G)Any Recovery Amount</b><span style="color: red">*</span>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator23" ErrorMessage="This field is mandatory."
									ControlToValidate="txt1027" runat="server" ForeColor="Red" Display="Dynamic" />
								<asp:TextBox ID="txt1027" runat="server" MaxLength="200" AutoComplete="off" AutoCompleteType="Disabled"></asp:TextBox>
							</li>

                            <br />
							<li><b>(H)Reason for Recovery</b><span style="color: red">*</span>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator33" ErrorMessage="This field is mandatory."
									ControlToValidate="txt1028" runat="server" ForeColor="Red" Display="Dynamic" />
								<asp:TextBox ID="txt1028" runat="server" MaxLength="700" AutoComplete="off" AutoCompleteType="Disabled"></asp:TextBox>
							</li>

                            <br />
							<li runat="server" id="ITAdminDeptuploadLI"><b>Reason for Recovery Upload file</b>
								<asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="true" />
								<asp:LinkButton ID="Linkupload_AdminDept"  runat="server" Visible="false"></asp:LinkButton>
                             </li>

                            <br />
							<li>
								<!--<span>Upload File</span>-->
								<span></span>

								<asp:GridView ID="GVAdminDept" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
									DataKeyNames="EMPEXIT_ID">
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
										<asp:TemplateField HeaderText="Reason for Recovery Upload file">
											<ItemTemplate>
												<asp:LinkButton ID="lnkviewfile" runat="server" OnClientClick=<%# "DownloadFileother('" + Eval("filepath") + "')" %> Text='<%# Eval("filepath") %>'>
												</asp:LinkButton>
											</ItemTemplate>
										</asp:TemplateField>

									</Columns>
								</asp:GridView>
							</li>

							<br />
							<br />
							<li><asp:Label ID="lbladminName" runat="server" Visible="false"><b> Submitted By</b> </asp:Label>&nbsp;		
								<b><asp:Label ID="lbladminName1" runat="server" Visible="false"></asp:Label></b>
								
							</li>
							<br />
							<li style="padding-top:20px">
								<asp:Label ID="lbladminSubOn" runat="server" Visible="false"><b> Submitted On</b> </asp:Label>&nbsp;		
								<b><asp:Label ID="lbladminSubOn1" runat="server" Visible="false"></asp:Label></b>
							</li>
							<br />
						</ul>
					</div>
					<div id="divITDept" class="edit-contact" runat="server">
						<ul>
							<li>
								<div class="userposts">
									<span>
										<asp:Label ID="Label3" runat="server" Text="IT Department Clearance"></asp:Label>
									</span>
								</div>
							</li>
							<br />
							<li><b>(A)  Hardware / Software material submitted</b><span style="color: red">*</span>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator9" ErrorMessage="This field is mandatory."
									ControlToValidate="txt1031" runat="server" ForeColor="Red" Display="Dynamic" />
								<asp:TextBox ID="txt1031" runat="server" MaxLength="200" AutoComplete="off" AutoCompleteType="Disabled"></asp:TextBox>

							</li>
                            <br />
							<li><b>(B)Any Recovery Amount</b><span style="color: red">*</span>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator24" ErrorMessage="This field is mandatory."
									ControlToValidate="txt1032" runat="server" ForeColor="Red" Display="Dynamic" />
								<asp:TextBox ID="txt1032" runat="server" MaxLength="200" AutoComplete="off" AutoCompleteType="Disabled"></asp:TextBox>
							</li>
                             <br />
							<li><b>(C)Reason for Recovery</b><span style="color: red">*</span>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator34" ErrorMessage="This field is mandatory."
									ControlToValidate="txt1033" runat="server" ForeColor="Red" Display="Dynamic" />
								<asp:TextBox ID="txt1033" runat="server" MaxLength="700" AutoComplete="off" AutoCompleteType="Disabled"></asp:TextBox>
							</li>
                             <br />
							<li runat="server" id="ITDeptLi"><b>Reason for Recovery Upload File</b>
								<asp:FileUpload ID="FileUpload2" runat="server" AllowMultiple="true" />
								<asp:LinkButton ID="LinkButton1"  runat="server" Visible="false"></asp:LinkButton>
                             </li>

                            <br />
							<li>
								<!--<span>Upload File</span>-->
								<span></span>
								<asp:GridView ID="GVITDept" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
									DataKeyNames="EMPEXIT_ID">
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
										<asp:TemplateField HeaderText="Reason for Recovery Upload file">
											<ItemTemplate>
												<asp:LinkButton ID="lnkviewfile" runat="server" OnClientClick=<%# "DownloadFileother('" + Eval("filepath") + "')" %> Text='<%# Eval("filepath") %>'>
												</asp:LinkButton>
											</ItemTemplate>
										</asp:TemplateField>

									</Columns>
								</asp:GridView>
							</li>

							<br />
							<li class="claimmob_upload" runat="server" id="claimmob_uploadf">
								<span>Upload File</span><br />
								<asp:FileUpload ID="uploadfile" runat="server" AllowMultiple="true" />
								<asp:LinkButton ID="lnkuplodedfile" OnClick="lnkuplodedfile_Click" runat="server" Visible="false"></asp:LinkButton>
							</li>
							<br />
							<li>
								<!--<span>Upload File</span>-->
								<span></span>

								<asp:GridView ID="gvuploadedFiles" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
									DataKeyNames="EMPEXIT_ID">
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
										<asp:TemplateField HeaderText="Uploaded Files">
											<ItemTemplate>
												<asp:LinkButton ID="lnkviewfile" runat="server" OnClientClick=<%# "DownloadFile('" + Eval("filepath") + "')" %> Text='<%# Eval("filepath") %>'>
												</asp:LinkButton>
											</ItemTemplate>
										</asp:TemplateField>

									</Columns>
								</asp:GridView>
							</li>
							<br />
							<li><asp:Label ID="lblITDept1" runat="server" Visible="false"><b> Submitted By</b> </asp:Label>&nbsp;		
								<b><asp:Label ID="lblITDept2" runat="server" Visible="false"></asp:Label></b>
								
							</li>
							<br />
							<li style="padding-top:20px">
								<asp:Label ID="lblITDept3" runat="server" Visible="false"><b> Submitted On</b> </asp:Label>&nbsp;		
								<b><asp:Label ID="lblITDept4" runat="server" Visible="false"></asp:Label></b>
							</li>
							<br />

						</ul>
					</div>
					<div id="divAccountDept" class="edit-contact" runat="server">
						<ul>
							<li>
								<div class="userposts">
									<span>
										<asp:Label ID="Label4" runat="server" Text="Account Department Clearance"></asp:Label>
									</span>
								</div>
							</li>
							<br />
							<li><b>(A) Outstanding advance against salary</b><span style="color: red">*</span>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator10" ErrorMessage="This field is mandatory."
									ControlToValidate="txt1041" runat="server" ForeColor="Red" Display="Dynamic" />
								<asp:TextBox AutoComplete="off" ID="txt1041" runat="server" MaxLength="200" AutoCompleteType="Disabled"></asp:TextBox>

							</li>
							<br />
							<li><b>(B) Outstanding travel advance</b><span style="color: red">*</span>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator11" ErrorMessage="This field is mandatory."
									ControlToValidate="txt1042" runat="server" ForeColor="Red" Display="Dynamic" />
								<asp:TextBox ID="txt1042" runat="server" MaxLength="200" AutoComplete="off" AutoCompleteType="Disabled"></asp:TextBox>

							</li>
							<br />
							<li><b>(C) Outstanding imprest</b><span style="color: red">*</span>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator12" ErrorMessage="This field is mandatory."
									ControlToValidate="txt1043" runat="server" ForeColor="Red" Display="Dynamic" />
								<asp:TextBox ID="txt1043" runat="server" MaxLength="200" AutoComplete="off" AutoCompleteType="Disabled"></asp:TextBox>

							</li>
							<br />
							<li><b>(D) Telephone / Electricity charges due</b><span style="color: red">*</span>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator13" ErrorMessage="This field is mandatory."
									ControlToValidate="txt1044" runat="server" ForeColor="Red" Display="Dynamic" />
								<asp:TextBox ID="txt1044" runat="server" MaxLength="200" AutoComplete="off" AutoCompleteType="Disabled"></asp:TextBox>

							</li>
							<br />
							<li runat="server" visible="false"><b>(E) I – card submitted</b><span style="color: red">*</span>
								<%--<asp:RequiredFieldValidator ID="RequiredFieldValidator14" ErrorMessage="This field is mandatory."
									ControlToValidate="txt1045" runat="server" ForeColor="Red" Display="Dynamic" />--%>
								<asp:TextBox ID="txt1045" runat="server" MaxLength="200" AutoComplete="off" AutoCompleteType="Disabled"></asp:TextBox>

							</li>
							<br />
							<li><b>(F) Mobile phone / connection submitted(where ever applicable )</b><span style="color: red">*</span>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator15" ErrorMessage="This field is mandatory."
									ControlToValidate="txt1046" runat="server" ForeColor="Red" Display="Dynamic" />
								<asp:TextBox ID="txt1046" runat="server" MaxLength="200" AutoComplete="off" AutoCompleteType="Disabled"></asp:TextBox>

							</li>
							<br />
							<li style="padding-bottom:15px;" runat="server" id="liBG" visible="false">
								<b>(G)BG Encashment</b><span style="color: red">*</span>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator21" ErrorMessage="This field is mandatory."
									ControlToValidate="lstBG" runat="server" InitialValue="0" ForeColor="Red" Display="Dynamic" />
								<asp:DropDownList runat="server" ID="lstBG">
									<asp:ListItem Text="Select BG" Value="0"></asp:ListItem>
									<asp:ListItem Text="Encashed" Value="1"></asp:ListItem>
									<asp:ListItem Text="Not Encashed" Value="2"></asp:ListItem>
									<asp:ListItem Text="Not Applicable" Value="3"></asp:ListItem>
								</asp:DropDownList>
							</li>
							<br />
							<li runat="server" id="liPDC"  visible="false" >
								<b>(H) PDC Encashment</b><span style="color: red" >*</span>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator22" ErrorMessage="This field is mandatory."
									ControlToValidate="lstPDC" runat="server" InitialValue="0" ForeColor="Red" Display="Dynamic" />
								<asp:DropDownList runat="server" ID="lstPDC">
									<asp:ListItem Text="Select PDC" Value="0"></asp:ListItem>
									<asp:ListItem Text="Encashed" Value="1"></asp:ListItem>
									<asp:ListItem Text="Bounce" Value="2"></asp:ListItem>
									<asp:ListItem Text="Not Applicable" Value="3"></asp:ListItem>
								</asp:DropDownList>
							</li>
                           <br />
							<li><b>(G)Any Recovery Amount</b><span style="color: red">*</span>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator25" ErrorMessage="This field is mandatory."
									ControlToValidate="txt1047" runat="server" ForeColor="Red" Display="Dynamic" />
								<asp:TextBox ID="txt1047" runat="server" MaxLength="200" AutoComplete="off" AutoCompleteType="Disabled"></asp:TextBox>

							</li>
                            <br />
							<li><b>(H)Additional payout</b><span style="color: red">*</span>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator26" ErrorMessage="This field is mandatory."
									ControlToValidate="txt1048" runat="server" ForeColor="Red" Display="Dynamic" />
								<asp:TextBox ID="txt1048" runat="server" MaxLength="200" AutoComplete="off" AutoCompleteType="Disabled"></asp:TextBox>
                             </li>

                             <br />
							<li><b>(I)Aditional payout Remark</b><span style="color: red">*</span>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator27" ErrorMessage="This field is mandatory."
									ControlToValidate="txt1049" runat="server" ForeColor="Red" Display="Dynamic" />
								<asp:TextBox ID="txt1049" runat="server" MaxLength="800" AutoComplete="off" AutoCompleteType="Disabled"></asp:TextBox>
                             </li>

                            <br />
							<li><b>(J)Reason for Recovery</b><span style="color: red">*</span>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator28" ErrorMessage="This field is mandatory."
									ControlToValidate="txt1050" runat="server" ForeColor="Red" Display="Dynamic" />
								<asp:TextBox ID="txt1050" runat="server" MaxLength="800" AutoComplete="off" AutoCompleteType="Disabled"></asp:TextBox>
                             </li>

                            <br />
							<li runat="server" id="AccountDeptLI"><b>Reason for Recovery Upload File</b>
								<asp:FileUpload ID="FileUpload3" runat="server" AllowMultiple="true" />
								<asp:LinkButton ID="LinkButton2"  runat="server" Visible="false"></asp:LinkButton>
                             </li>

                             <br />
							<li>
								<!--<span>Upload File</span>-->
								<span></span>
								<asp:GridView ID="GVAccountDept" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
									DataKeyNames="EMPEXIT_ID">
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
										<asp:TemplateField HeaderText="Reason for Recovery Upload file">
											<ItemTemplate>
												<asp:LinkButton ID="lnkviewfile" runat="server" OnClientClick=<%# "DownloadFileother('" + Eval("filepath") + "')" %> Text='<%# Eval("filepath") %>'>
												</asp:LinkButton>
											</ItemTemplate>
										</asp:TemplateField>

									</Columns>
								</asp:GridView>
							</li>

							<br />
							<li><asp:Label ID="lblAccDept1" runat="server" Visible="false"><b> Submitted By</b> </asp:Label>&nbsp;		
							<b><asp:Label ID="lblAccDept2" runat="server" Visible="false"></asp:Label></b>
								
							</li>
							<br />
							<li style="padding-top:20px">
								<asp:Label ID="lblAccDept3" runat="server" Visible="false"><b> Submitted On</b> </asp:Label>&nbsp;		
								<b><asp:Label ID="lblAccDept4" runat="server" Visible="false"></asp:Label></b>
							</li>
							<br />
						</ul>
					</div>
					<div id="divHRDept" class="edit-contact" runat="server">
						<ul>
							<li>
								<div class="userposts">
									<span>
										<asp:Label ID="Label5" runat="server" Text="HR Department Clearance"></asp:Label>
									</span>
								</div>
							</li>
							<%--<br />--%>
							<%--<li runat="server" visible="false"><b>(A) Paramount Mediclaim ID Cards Returned (Date)</b><span style="color: red" runat="server" id="HR1" >&nbsp &nbsp *&nbsp &nbsp &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp(Only For Regular Employee)</span><br />
								<asp:RequiredFieldValidator ID="RequiredFieldValidator16" ErrorMessage="This field is mandatory."
									ControlToValidate="txt1051" runat="server" ForeColor="Red" Display="Dynamic" />
								<asp:TextBox AutoComplete="off" ID="txt1051" runat="server" MaxLength="10" AutoPostBack="false" AutoCompleteType="Disabled"></asp:TextBox>
								<ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txt1051"
									Format="dd/MM/yyyy" runat="server">
								</ajaxToolkit:CalendarExtender>

							</li>--%>
							<br />
							<li><b>(A) Exit Form – A filled up</b><span style="color: red">*</span>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator17" ErrorMessage="This field is mandatory."
									ControlToValidate="txt1052" runat="server" ForeColor="Red" Display="Dynamic" />
								<asp:TextBox ID="txt1052" runat="server" MaxLength="200" AutoComplete="off" AutoCompleteType="Disabled"></asp:TextBox>

							</li>
							<br />
							<li><b>(B) Exit Form – B Submitted</b><span style="color: red">*</span>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator18" ErrorMessage="This field is mandatory."
									ControlToValidate="txt1053" runat="server" ForeColor="Red" Display="Dynamic" />
								<asp:TextBox ID="txt1053" runat="server" MaxLength="200" AutoComplete="off" AutoCompleteType="Disabled"></asp:TextBox>

							</li>
							<br />
							<li><b>(C) Correspondence Address</b>(Maximum 200 char)&nbsp;<span style="color: red">*</span><br />
								<asp:RequiredFieldValidator ID="RequiredFieldValidator19" ErrorMessage="This field is mandatory."
									ControlToValidate="txt1054" runat="server" ForeColor="Red" Display="Dynamic" />
								<asp:TextBox ID="txt1054" runat="server" MaxLength="500" AutoComplete="off" AutoCompleteType="Disabled"></asp:TextBox>

							</li>
							<br />
							<li><b>(D) Contact No. / E-mail Id</b><span style="color: red">*</span>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator20" ErrorMessage="This field is mandatory."
									ControlToValidate="txt1055" runat="server" ForeColor="Red" Display="Dynamic" />
								<asp:TextBox ID="txt1055" runat="server" MaxLength="200" AutoComplete="off" AutoCompleteType="Disabled"></asp:TextBox>

							</li>
                            <br />
							<li><b>(E) Notice period recovery amount</b><span style="color: red">*</span>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator16" ErrorMessage="This field is mandatory."
									ControlToValidate="txt1056" runat="server" ForeColor="Red" Display="Dynamic" />
								<asp:TextBox ID="txt1056" runat="server" MaxLength="200" AutoComplete="off" AutoCompleteType="Disabled"></asp:TextBox>

							</li>
                            <br />
							<li><b>(F) Notice period recovery remark</b><span style="color: red">*</span>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator30" ErrorMessage="This field is mandatory."
									ControlToValidate="txt1057" runat="server" ForeColor="Red" Display="Dynamic" />
								<asp:TextBox ID="txt1057" runat="server" MaxLength="200" AutoComplete="off" AutoCompleteType="Disabled"></asp:TextBox>

							</li>
                            <br />
							<li runat="server" id="HrDept1"><b>Notice period recovery Upload File</b>
								<asp:FileUpload ID="FileUpload4" runat="server" AllowMultiple="true" />
								<asp:LinkButton ID="LinkButton3"  runat="server" Visible="false"></asp:LinkButton>
                             </li>

                             <br />
							<li>
								<!--<span>Upload File</span>-->
								<span></span>
								<asp:GridView ID="GVHrDept1" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
									DataKeyNames="EMPEXIT_ID">
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
										<asp:TemplateField HeaderText="Notice period recovery Upload File">
											<ItemTemplate>
												<asp:LinkButton ID="lnkviewfile" runat="server" OnClientClick=<%# "DownloadFileother('" + Eval("filepath") + "')" %> Text='<%# Eval("filepath") %>'>
												</asp:LinkButton>
											</ItemTemplate>
										</asp:TemplateField>

									</Columns>
								</asp:GridView>
							</li>

                            <br />
							<li><b>(G) Additional payout</b><span style="color: red">*</span>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator31" ErrorMessage="This field is mandatory."
									ControlToValidate="txt1058" runat="server" ForeColor="Red" Display="Dynamic" />
								<asp:TextBox ID="txt1058" runat="server" MaxLength="200" AutoComplete="off" AutoCompleteType="Disabled"></asp:TextBox>

							</li>
                            <br />
							<li><b>(H) Aditional payout Remark</b><span style="color: red">*</span>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator32" ErrorMessage="This field is mandatory."
									ControlToValidate="txt1059" runat="server" ForeColor="Red" Display="Dynamic" />
								<asp:TextBox ID="txt1059" runat="server" MaxLength="200" AutoComplete="off" AutoCompleteType="Disabled"></asp:TextBox>

							</li>
                            <br />
							<li runat="server" id="HRDept2"><b>Aditional payout Upload File</b>
								<asp:FileUpload ID="FileUpload5" runat="server" AllowMultiple="true" />
								<asp:LinkButton ID="LinkButton4"  runat="server" Visible="false"></asp:LinkButton>
                             </li>

                             <br />
							<li>
								<!--<span>Upload File</span>-->
								<span></span>
								<asp:GridView ID="GVHRDept2" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
									DataKeyNames="EMPEXIT_ID">
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
										<asp:TemplateField HeaderText="Aditional payout Upload File">
											<ItemTemplate>
												<asp:LinkButton ID="lnkviewfile" runat="server" OnClientClick=<%# "DownloadFileother('" + Eval("filepath") + "')" %> Text='<%# Eval("filepath") %>'>
												</asp:LinkButton>
											</ItemTemplate>
										</asp:TemplateField>

									</Columns>
								</asp:GridView>
							</li>

							<br />
							<br />
							<li><asp:Label ID="lblHRDept1" runat="server" Visible="false"><b> Submitted By</b> </asp:Label>&nbsp;		
								<b><asp:Label ID="lblHRDept2" runat="server" Visible="false"></asp:Label></b>
								
							</li>
							<br />
							<li style="padding-top:20px">
								<asp:Label ID="lblHRDept3" runat="server" Visible="false"><b> Submitted On</b> </asp:Label>&nbsp;		
								<b> <asp:Label ID="lblHRDept4" runat="server" Visible="false"></asp:Label></b>
							</li>
							<br />
						</ul>
					</div>
				</div>
				<%--<asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />--%>
			</div>

		</div>
	</div>
	<div class="mobile_Savebtndiv">
		<%--<asp:LinkButton ID="btnSubmit" runat="server" Text="Submit" ToolTip="Submit" CssClass="Savebtnsve" OnClick="btnSubmit_Click">Submit</asp:LinkButton>--%>
		<%--<asp:Button ID="btnVerify" runat="server" Text="Verify" OnClick="btnVerify_Click" OnClientClick="Confirm()" CssClass="Savebtnsve" />--%>
        <asp:Label ID="lblmsgpendingApproval" runat="server" Visible="false" ForeColor="Red"></asp:Label>
        <br /><br />

		<asp:LinkButton ID="mobile_btnSave" runat="server" Text="Verify" ToolTip="Save" CssClass="Savebtnsve" OnClick="btnVerify_Click" OnClientClick="if(Page_ClientValidate()) confirm_meth()">Verify</asp:LinkButton>
		<asp:LinkButton ID="mobile_cancel" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="Savebtnsve">Cancel</asp:LinkButton>
		<asp:LinkButton ID="claimmob_btnSubmit" runat="server" Text="Back" Visible="false" ToolTip="Back" CssClass="Savebtnsve" >Back</asp:LinkButton>

		<asp:HiddenField ID="hdnDeptId" runat="server" />
		<asp:HiddenField ID="hdnEmpCode" runat="server" />

		<asp:HiddenField ID="FilePath" runat="server" />
        <asp:HiddenField ID="FilePathOther" runat="server" />
        <asp:HiddenField ID="HDPendingtaskYsNo" runat="server" />
		<br />
		<br />
	</div>
    <div>
        <asp:GridView ID="GVEmployeependingTask" runat="server" BackColor="White" BorderColor="Navy" 
            BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="70%" EditRowStyle-Wrap="false">
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
										<asp:BoundField HeaderText="Pending Request"
                                            DataField="Task"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Number of Request"
                                            DataField="TaskCount"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />
									</Columns>
								</asp:GridView>

    </div>
	<script type="text/javascript">
		function DownloadFile(file) {
			// alert(file);
			var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;

			//alert(localFilePath);
          //  window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
            window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
            
        }

        function DownloadFileother(file) {
			// alert(file);
			var localFilePath = document.getElementById("<%=FilePathOther.ClientID%>").value;

			//alert(localFilePath);
          //  window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
            window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
            
        }
	</script>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
	MaintainScrollPositionOnPostback="true"
	CodeFile="MyTask.aspx.cs" Inherits="MyTask" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
	Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

	<link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />

	<style>
		.myaccountpagesheading {
			text-align: center !important;
			text-transform: uppercase !important;
		}

		.aspNetDisabled {
			background: #ebebe4;
		}

		#MainContent_lstApprover {
			overflow: hidden !important;
		}

		.edit-contact input {
			padding-left: 0px !important;
		}

		#MainContent_View_Reprt {
			background: #3D1956;
			color: #febf39 !important;
			padding: 0.5% 1.4%;
			margin: 0% 0% 0 0;
		}

		.noresize {
			resize: none;
		}

		/* .select2-container .select2-selection--multiple {
            box-sizing: border-box;
            cursor: pointer;
            display: block;
            min-height: 32px;
            user-select: none;
            -webkit-user-select: none;
            height: 10px;
        }*/
	</style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

	<script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>

	<script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>

	<div class="commpagesdiv">
		<div class="commonpages">

			<div class="userposts">
				<span>
					<asp:Label ID="lblheading" runat="server" Text="My Task"></asp:Label>
				</span>
			</div>
			<div>
				<asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
			</div>
			<span runat="server" id="backToSPOC" visible="false">
				<a href="InboxServiceRequest.aspx" class="aaaa">Back</a>
			</span>
			<span runat="server" id="backToEmployee" visible="false">
				<a href="MyService_Req.aspx" class="aaaa">Back</a>
			</span>
			<span runat="server" id="backToArr" visible="false">
				<a href="InboxServiceRequest_Arch.aspx" class="aaaa">Back</a>
			</span>
			<span>
				<a href="TaskMonitoring.aspx" style="margin-right: 18px;" class="aaaa">Task Monitoring</a>&nbsp;&nbsp; 
			</span>
			<br />
			<br />
			<br />

			<ul id="editform" runat="server" visible="false">
				<%--  <li class="mobile_inboxEmpCode">
                    <span><b>My Task List</b></span><br />
                    <br />
                </li>
                <li></li>--%>

				<li style="width: 100%">

					<asp:GridView ID="gv_MyTask" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
						DataKeyNames="Task_Reference_ID" PageSize="20" AllowPaging="True" OnPageIndexChanging="gv_MyTask_PageIndexChanging">
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

							<asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
							<ItemTemplate>
											<asp:LinkButton ID="MyTask_Edit" runat="server" Visible="true" CommandArgument='<%# Eval("ID") %>' Width="35px" Height="15px" OnClick="MyTask_Edit_Click"><img src="../Images/edit.png" /></asp:LinkButton>
							</ItemTemplate>
							<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
							</asp:TemplateField>

							<asp:BoundField HeaderText="Task Reference Id"
								DataField="Task_Reference_ID"
								ItemStyle-HorizontalAlign="Left"
								HeaderStyle-HorizontalAlign="Left"
								ItemStyle-Width="16%" ItemStyle-BorderColor="Navy" />

							<asp:BoundField HeaderText="Task Reference Creation Date"
								DataField="Task_Reference_Date"
								ItemStyle-HorizontalAlign="Left"
								HeaderStyle-HorizontalAlign="Left"
								ItemStyle-Width="20%" ItemStyle-BorderColor="Navy" />
							<asp:BoundField HeaderText="Meeting/Discussion Title"
								DataField="Meeting_Discussion_Title"
								ItemStyle-HorizontalAlign="Left"
								HeaderStyle-HorizontalAlign="Left"
								ItemStyle-Width="25%" ItemStyle-BorderColor="Navy" />
							<asp:BoundField HeaderText="Meeting/Discussion Type"
								DataField="Name"
								ItemStyle-HorizontalAlign="Left"
								HeaderStyle-HorizontalAlign="Left"
								ItemStyle-Width="20%" ItemStyle-BorderColor="Navy" />
							<asp:BoundField HeaderText="IsDraft"
								DataField="isactive"
								ItemStyle-HorizontalAlign="Left"
								HeaderStyle-HorizontalAlign="Left"
								ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />
							<asp:BoundField HeaderText="Status"
								DataField="StatusTitle"
								ItemStyle-HorizontalAlign="Left"
								HeaderStyle-HorizontalAlign="Left"
								ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />

							 
							<asp:TemplateField HeaderText="Download Excel" HeaderStyle-Width="0%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
								<ItemTemplate>
									<asp:ImageButton ID="lnk_Download" runat="server" Width="15px" Height="15px" ToolTip="Download Excel" ImageUrl="~/images/Download.png" OnClick="lnk_Download_Click" />
								</ItemTemplate>
								<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
							</asp:TemplateField>
						</Columns>
					</asp:GridView>

				</li>
			</ul>

		</div>

	</div>
	<div class="mobile_Savebtndiv">
	</div>

	<asp:HiddenField ID="hdnvouno" runat="server" />
	<asp:HiddenField ID="hdnIsMarried" runat="server" />
	<asp:HiddenField ID="hdnTaskRefID" runat="server" />
	<asp:HiddenField ID="hdnAttendeeID" runat="server" />
	<asp:HiddenField ID="hdnTaskID" runat="server" />
	<asp:HiddenField ID="hdnCertificationDetailID" runat="server" />
	<asp:HiddenField ID="hdnProjectDetailID" runat="server" />
	<asp:HiddenField ID="hdnDomainDetailID" runat="server" />
	<asp:HiddenField ID="hdnFileDetailID" runat="server" />
	<asp:HiddenField ID="hdnFileName" runat="server" />
	<asp:HiddenField ID="hdnFilePath" runat="server" />
	<asp:HiddenField ID="hdnYesNo" runat="server" />
	<asp:HiddenField ID="hdnempcode" runat="server" />


	<asp:HiddenField ID="FilePath" runat="server" />
</asp:Content>

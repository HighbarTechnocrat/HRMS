<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" 
    CodeFile="ReviewDelayedTasks.aspx.cs" Inherits="procs_ReviewDelayedTasks" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
	Namespace="eWorld.UI" TagPrefix="ew" %>

<%@ Register TagName="calender" TagPrefix="ucical" Src="~/themes/creative1.0/LayoutControls/leaveCalender.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
	<style>
		.myaccountpagesheading {
			text-align: center !important;
			text-transform: uppercase !important;
		}

		.aspNetDisabled {
			/*background: #dae1ed;*/
			background: #ebebe4;
		}

		.Calender {
			float: left;
			padding: 5% 5% 5% 5% !important;
		}

		#MainContent_DueDate_Change_Inbox:link, #MainContent_DueDate_Change_Inbox:visited,
		#MainContent_My_ProcessedTask_Supervisor:link, #MainContent_My_ProcessedTask_Supervisor:visited,
		#MainContent_Clusore:link, #MainContent_Clusore:visited,
		#MainContent_My_Processed_Task:link, #MainContent_My_Processed_Task:visited,
		#MainContent_Inbox_Task:link, #MainContent_Inbox_Task:visited,
		#MainContent_My_Tasks:link, #MainContent_My_Tasks:visited,
		#MainContent_lnk_SupervisorReport:link, #MainContent_lnk_SupervisorReport:visited,
		#MainContent_lnk_AuditTrialReport:link, #MainContent_lnk_AuditTrialReport:visited,
		#MainContent_lnk_ExecuterReport:link, #MainContent_lnk_ExecuterReport:visited,
		#MainContent_lnk_MyCancelTask:link, #MainContent_lnk_MyCancelTask:visited,
		#MainContent_Create_Task:link, #MainContent_Create_Task:visited {
			background-color: #C7D3D4;
			color: #603F83 !important;
			border-radius: 10px;
			/*color: white;*/
			padding: 25px 5px;
			text-align: center;
			text-decoration: none;
			display: inline-block;
			width: 90% !important;
		}

			#MainContent_DueDate_Change_Inbox:hover, #MainContent_DueDate_Change_Inbox:active,
			#MainContent_My_ProcessedTask_Supervisor:hover, #MainContent_My_ProcessedTask_Supervisor:active,
			#MainContent_Clusore:hover, #MainContent_Clusore:active,
			#MainContent_My_Processed_Task:hover, #MainContent_My_Processed_Task:active,
			#MainContent_Inbox_Task:link:hover, #MainContent_Inbox_Task:active,
			#MainContent_My_Tasks:link:hover, #MainContent_My_Tasks:active,
			#MainContent_lnk_SupervisorReport:link:hover, #MainContent_lnk_SupervisorReport:active,
			#MainContent_lnk_AuditTrialReport:link:hover, #MainContent_lnk_AuditTrialReport:active,
			#MainContent_lnk_ExecuterReport:link:hover, #MainContent_lnk_ExecuterReport:active,
			#MainContent_lnk_MyCancelTask:link:hover, #MainContent_lnk_MyCancelTask:active,
			#MainContent_Create_Task:hover, #MainContent_Create_Task:active {
				/*background-color: #603F83;*/
				background-color: #3D1956;
				color: #C7D3D4 !important;
				border-color: #3D1956;
				border-width: 2pt;
				border-style: inset;
			}

		.EmpName {
			color: Blue;
			border-color: Navy;
			cursor: pointer !important;
		}

		.dgSupervisor {
			color: White !important;
			background-color: #669999;
			font-weight: bold;
		}

		.DelayedTH {
			display: none;
		}

		.EmpName1 {
			color: #F28820;
			font-weight: 600;
			font-size: 16px;
			border-color: Navy;
			cursor: pointer !important;
			
		}

		#MainContent_btnExcelDownload {
			background-attachment: scroll;
			background-clip: border-box;
			background-color: #3D1956;
			color: #febf39 !important;
			background-image: none;
			background-origin: padding-box;
			background-position-x: 0;
			background-position-y: 0;
			background-repeat: repeat;
			background-size: auto auto;
			padding-bottom: 6px;
			padding-left: 23px;
			padding-right: 23px;
			padding-top: 6px;
		}
		.dgDelayedHide {
			background-color:#fff !important;
		}
		.dgDelayedShow {
			background-color:#C7D3D4 !important;
		}
		#DelayedTBL > tbody > tr > td > div {
			margin-right:-2px !important;
		}
	</style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

	<script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
	<script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>


	<div class="commpagesdiv">
		<div class="commonpages">
			<div class="wishlistpagediv">
				<div class="wishlistpage">
					<div class="userposts">
						<span>
							<asp:Label ID="lblheading" runat="server" Text="Review Delayed Tasks List"></asp:Label>
						</span>
					</div>
					<div>
						<span>
							<a href="https://ess.highbartech.com/hrms/default.aspx" class="aaaa">Home</a>
						</span>
					</div>

					<div class="wrapper">
								<div class="leavegridMain" id="DvDelayedList" runat="server">
									<%--<h3 id="h4" runat="server">Delayed Task Details</h3>--%>
									<asp:GridView ID="GRDDelayedList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%"
										OnPageIndexChanging="GRDDelayedList_PageIndexChanging"  AllowPaging="True"  EditRowStyle-Wrap="false" PageSize="20">
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

											<asp:BoundField HeaderText="Executor Name"
												DataField="Emp_Name"
												ItemStyle-HorizontalAlign="left"
												HeaderStyle-HorizontalAlign="left"
												ItemStyle-Width="13%"
												ItemStyle-BorderColor="Navy" />

											<asp:BoundField HeaderText="Task ID"
												DataField="Task_ID"
												ItemStyle-HorizontalAlign="left"
												HeaderStyle-HorizontalAlign="left"
												ItemStyle-Width="12%"
												ItemStyle-BorderColor="Navy" />

											<asp:BoundField HeaderText="Task Description"
												DataField="Task_Description"
												ItemStyle-HorizontalAlign="left"
												HeaderStyle-HorizontalAlign="left"
												ItemStyle-Width="30%"
												ItemStyle-BorderColor="Navy" />
											<asp:BoundField HeaderText="Original Due Date"
												DataField="OriginalDueDate"
												ItemStyle-HorizontalAlign="center"
												ItemStyle-Width="6%"
												ItemStyle-BorderColor="Navy" />

											<asp:BoundField HeaderText="Revised Due Date"
												DataField="Due_Date"
												ItemStyle-HorizontalAlign="center"
												ItemStyle-Width="6%"
												ItemStyle-BorderColor="Navy" />

											<asp:BoundField HeaderText="Due Date Change Count"
												DataField="Due_DateCount"
												ItemStyle-HorizontalAlign="center"
												ItemStyle-Width="6%"
												ItemStyle-BorderColor="Navy" />

											<asp:BoundField HeaderText="Status"
												DataField="StatusName"
												ItemStyle-HorizontalAlign="center"
												ItemStyle-Width="6%"
												ItemStyle-BorderColor="Navy" />
											<asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
												<ItemTemplate>
													<asp:LinkButton ID="DelayTask_Edit" runat="server" ToolTip="View" Visible="true" CommandArgument='<%#Eval("ID") + "," + Eval("Task_Ref_id")%>' Width="15px" Height="15px" OnClick="DelayTask_Edit_Click"><img src="../Images/edit.png" /></asp:LinkButton>
												</ItemTemplate>
												<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
											</asp:TemplateField>
										</Columns>
									</asp:GridView>

									<br />
								</div>
								<div class="leavegridMain" id="dvSupervisor" runat="server" visible="false">
									<br />
								</div>

								<div class="leavegridMain" id="dvTaskDetails" runat="server" visible="false">
									<br />
								</div>
					</div>
					<br />

					
				</div>
			</div>
		</div>
	</div>
	<br />

	<asp:HiddenField ID="hflEmpCode" runat="server" />
</asp:Content>


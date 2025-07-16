<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
	CodeFile="TaskMonitoring.aspx.cs" Inherits="TaskMonitoring" %>

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
        #MainContent_lnk_Alltask:link, #MainContent_lnk_Alltask:visited,
		#MainContent_lnk_MyCancelTask:link, #MainContent_lnk_MyCancelTask:visited, 
        #MainContent_Lnk_CreateprojectSchedule:link, #MainContent_Lnk_CreateprojectSchedule:visited,
        #MainContent_lnk_ProjectDuration:link, #MainContent_lnk_ProjectDuration:visited,
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
            #MainContent_lnk_Alltask:link:hover, #MainContent_lnk_Alltask:active, 
            #MainContent_Lnk_CreateprojectSchedule:link:hover, #MainContent_Lnk_CreateprojectSchedule:active, 
            #MainContent_lnk_ProjectDuration:link:hover, #MainContent_lnk_ProjectDuration:active,
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
							<asp:Label ID="lblheading" runat="server" Text="Task Monitoring"></asp:Label>
						</span>
					</div>
					<div>
						<span>
							<a href="http://localhost/hrms/default.aspx" class="aaaa">Home</a>
						</span>
					</div>

					<div class="wrapper">
						<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildernAsTriggers="True" >
							<Triggers>
								<asp:PostBackTrigger ControlID="lnk_Download" />
							</Triggers>
							<ContentTemplate>
								<div class="leavegridMain edit-contact" id="dvDelayed"  runat="server" visible="false">
									
									<table style='width:33%;padding:0px !important;' id="DelayedTBL">
										<tr>
											<td style="width:32%;padding-right:0px !important;">
												<asp:GridView ID="dgDelayed" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%"
												AutoGenerateSelectButton="true" OnSelectedIndexChanged="dgDelayed_SelectedIndexChanged"  OnRowDataBound="dgDelayed_RowDataBound">
												<FooterStyle BackColor="White" ForeColor="#000066" />
												<HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
												<PagerStyle ForeColor="#000066" HorizontalAlign="left" BackColor="White" />
												<RowStyle ForeColor="#000066" />
												<%--<SelectedRowStyle BackColor="#C7D3D4" />--%>
												<SortedAscendingCellStyle BackColor="#F1F1F1" />
												<SortedAscendingHeaderStyle BackColor="#007DBB" />
												<SortedDescendingCellStyle BackColor="#CAC9C9" />
												<SortedDescendingHeaderStyle BackColor="#00547E" />
												<Columns>

													<asp:BoundField HeaderText=""  HeaderStyle-CssClass="DelayedTH" ItemStyle-CssClass="EmpName1"
														DataField="PendingTask"
														ItemStyle-HorizontalAlign="center"
														ItemStyle-Width="10%"
														ItemStyle-BorderColor="Navy" />
													<asp:BoundField HeaderText="" HeaderStyle-CssClass="DelayedTH" ItemStyle-CssClass="EmpName1"
														DataField="DelayedTask"
														ItemStyle-HorizontalAlign="Center"
														HeaderStyle-HorizontalAlign="left"
														ItemStyle-Width="3%" />
													<%--<asp:TemplateField HeaderText="" HeaderStyle-CssClass="DelayedTH" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
										<ItemTemplate>
													</ItemTemplate>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
									</asp:TemplateField>--%>
												</Columns>
											</asp:GridView>

											</td>
											<td style="width:1%;padding-left:0px !important;">
											<asp:ImageButton ID="lnk_Download"  runat="server" Style="padding:0px !important;padding-left:10px !important" Width="15px" Height="16px" ToolTip="Download Excel" OnClick="lnk_Download_Click" ImageUrl="~/images/Download.png" />
											<asp:LinkButton ID="btnExcelDownload" Visible="false"  Text="Download Excel" ToolTip="Download Excel" runat="server" OnClick="btnExcelDownload_Click" />
									</td>
										</tr>
									</table>			
									<br />
								</div>
								<div class="leavegridMain" id="DvDelayedList" runat="server" visible="false">
									<h3 id="h4" runat="server">Delayed Task Details</h3>
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

									<asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
									<ItemTemplate>
										<asp:LinkButton ID="DelayTask_Edit" runat="server" ToolTip="View" Visible="true" CommandArgument='<%#Eval("ID") + "," + Eval("Task_Ref_id")%>' Width="15px" Height="15px" OnClick="DelayTask_Edit_Click"><img src="../Images/edit.png" /></asp:LinkButton>
									</ItemTemplate>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
								</asp:TemplateField>

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
 
											 
										</Columns>
									</asp:GridView>

									<br />
								</div>
								<div class="leavegridMain" id="dvSupervisor" runat="server" visible="false">
									<h3 id="hheadyear" runat="server">Pending Task Assigned By Me</h3>
									<asp:GridView ID="dgSupervisor" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="30%"
										DataKeyNames="Task_Executer" OnSelectedIndexChanged="dgSupervisor_SelectedIndexChanged" AutoGenerateSelectButton="True"
										OnRowDataBound="dgSupervisor_RowDataBound">
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
											<asp:BoundField HeaderText="Executor Name" ItemStyle-CssClass="EmpName"
												DataField="Emp_Name"
												ItemStyle-HorizontalAlign="left"
												HeaderStyle-HorizontalAlign="left"
												ItemStyle-Width="20%" />

											<asp:BoundField HeaderText="Pending Task"
												DataField="Pending Task"
												ItemStyle-HorizontalAlign="center"
												ItemStyle-Width="13%"
												ItemStyle-BorderColor="Navy" />
										</Columns>
									</asp:GridView>
									<br />
								</div>

								<div class="leavegridMain" id="dvTaskDetails" runat="server" visible="false">
									<h3 id="h2" runat="server">Pending Task Details</h3>
									<asp:GridView ID="dgTaskDetails" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="90%">
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
											<asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
												<ItemTemplate>
												<asp:LinkButton ID="MyTask_Edit" runat="server" ToolTip="View" Visible="true" CommandArgument='<%#Eval("ID") + "," + Eval("Task_Ref_id")%>' Width="15px" Height="15px" OnClick="MyTask_Edit_Click"><img src="../Images/edit.png" /></asp:LinkButton>
												</ItemTemplate>
												<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
								</asp:TemplateField>
																			<asp:BoundField HeaderText="Task ID"
												DataField="Task_ID"
												ItemStyle-HorizontalAlign="left"
												HeaderStyle-HorizontalAlign="left"
												ItemStyle-Width="14%"
												ItemStyle-BorderColor="Navy" />

											<asp:BoundField HeaderText="Task Description"
												DataField="Task_Description"
												ItemStyle-HorizontalAlign="left"
												HeaderStyle-HorizontalAlign="left"
												ItemStyle-Width="25%"
												ItemStyle-BorderColor="Navy" />

											<asp:BoundField HeaderText="Emp Name" Visible="false"
												DataField="Emp_Name"
												ItemStyle-HorizontalAlign="left"
												HeaderStyle-HorizontalAlign="left"
												ItemStyle-Width="20%"
												ItemStyle-BorderColor="Navy" />

											<asp:BoundField HeaderText="Original Due Date"
												DataField="OriginalDueDate"
												ItemStyle-HorizontalAlign="center"
												ItemStyle-Width="8%"
												ItemStyle-BorderColor="Navy" />

											<asp:BoundField HeaderText="Revised Due Date"
												DataField="Due_Date"
												ItemStyle-HorizontalAlign="center"
												ItemStyle-Width="8%"
												ItemStyle-BorderColor="Navy" />

											<asp:BoundField HeaderText="Due Date Change Count"
												DataField="Due_DateCount"
												ItemStyle-HorizontalAlign="center"
												ItemStyle-Width="6%"
												ItemStyle-BorderColor="Navy" />
											
											 <asp:BoundField HeaderText="Action By"
											 DataField ="ActionBy" 
											 ItemStyle-HorizontalAlign="Left"
											 HeaderStyle-HorizontalAlign="Left"
											 ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

											<asp:BoundField HeaderText="Action"
											 DataField="Actionstatus" 
												ItemStyle-HorizontalAlign="Left"
												HeaderStyle-HorizontalAlign="Left"
												ItemStyle-Width="7%" ItemStyle-BorderColor="Navy" />

                                            <asp:BoundField HeaderText="Project/Location"
											 DataField="Location_name" 
												ItemStyle-HorizontalAlign="Left"
												HeaderStyle-HorizontalAlign="Left"
												ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />


											<asp:BoundField HeaderText="Status"
												DataField="StatusName"
												ItemStyle-HorizontalAlign="center"
												ItemStyle-Width="6%"
												ItemStyle-BorderColor="Navy" Visible="false" />
											 
										</Columns>
									</asp:GridView>
									<br />
								</div>
								<div class="leavegridMain" id="dvExecutor" runat="server" visible="false">
									<h3 id="h1" runat="server">Pending Task Assigned To Me / My Upcoming Task</h3>
									<asp:GridView ID="dgExecutor" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="30%"
										DataKeyNames="Task_Supervisor" OnSelectedIndexChanged="dgExecutor_SelectedIndexChanged" AutoGenerateSelectButton="True"
										OnRowDataBound="dgExecutor_RowDataBound">
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
											<asp:BoundField HeaderText="Supervisor Name"
												DataField="Emp_Name" ItemStyle-CssClass="EmpName"
												ItemStyle-HorizontalAlign="left"
												HeaderStyle-HorizontalAlign="left"
												ItemStyle-Width="20%" />
											<asp:BoundField HeaderText="Pending Task"
												DataField="Pending Task"
												ItemStyle-HorizontalAlign="center"
												ItemStyle-Width="13%"
												ItemStyle-BorderColor="Navy" />
										</Columns>
									</asp:GridView>
									<br />
								</div>

								<div class="leavegridMain" id="DivExecutor1" runat="server" visible="false">
									<h3 id="h3" runat="server">Pending Task Details</h3>
									<asp:GridView ID="dgExecutorDetails" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="90%">
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
											<asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
											<ItemTemplate>
											<asp:LinkButton ID="Task_Edit" runat="server" Visible="true" ToolTip="View" CommandArgument='<%#Eval("ID") + "," + Eval("Task_Ref_id")%>' Width="15px" Height="15px" OnClick="Task_Edit_Click"><img src="../Images/edit.png" /></asp:LinkButton>
											</ItemTemplate>
											<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
							               </asp:TemplateField>
											<asp:BoundField HeaderText="Task ID"
												DataField="Task_ID"
												ItemStyle-HorizontalAlign="left"
												HeaderStyle-HorizontalAlign="left"
												ItemStyle-Width="14%"
												ItemStyle-BorderColor="Navy" />

											<asp:BoundField HeaderText="Task Description"
												DataField="Task_Description"
												ItemStyle-HorizontalAlign="left"
												HeaderStyle-HorizontalAlign="left"
												ItemStyle-Width="25%"
												ItemStyle-BorderColor="Navy" />

											<asp:BoundField HeaderText="Emp Name" Visible="false"
												DataField="Emp_Name"
												ItemStyle-HorizontalAlign="left"
												HeaderStyle-HorizontalAlign="left"
												ItemStyle-Width="20%"
												ItemStyle-BorderColor="Navy" />

											<asp:BoundField HeaderText="Original Due Date"
												DataField="OriginalDueDate"
												ItemStyle-HorizontalAlign="center"
												ItemStyle-Width="8%"
												ItemStyle-BorderColor="Navy" />

											<asp:BoundField HeaderText="Revised Due Date"
												DataField="Due_Date"
												ItemStyle-HorizontalAlign="center"
												ItemStyle-Width="8%"
												ItemStyle-BorderColor="Navy" />

											<asp:BoundField HeaderText="Due Date Change Count"
												DataField="Due_DateCount"
												ItemStyle-HorizontalAlign="center"
												ItemStyle-Width="6%"
												ItemStyle-BorderColor="Navy" />

											 <asp:BoundField HeaderText="Action By"
												 DataField="ActionBy"
												 ItemStyle-HorizontalAlign="Left"
												 HeaderStyle-HorizontalAlign="Left"
												 ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

												<asp:BoundField HeaderText="Action"
												DataField="Actionstatus"
												ItemStyle-HorizontalAlign="Left"
												HeaderStyle-HorizontalAlign="Left"
												ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                            <asp:BoundField HeaderText="Project/Location"
											 DataField="Location_name" 
												ItemStyle-HorizontalAlign="Left"
												HeaderStyle-HorizontalAlign="Left"
												ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

											<asp:BoundField HeaderText="Status"
												DataField="StatusName"
												ItemStyle-HorizontalAlign="center"
												ItemStyle-Width="6%"
												ItemStyle-BorderColor="Navy" Visible="false" />
											 
										</Columns>
									</asp:GridView>
									<br />
								</div>
								<asp:HiddenField ID="hdnAssignedBy" runat="server" />
								<asp:HiddenField ID="hdnAssignedMI" runat="server" />
								<asp:HiddenField ID="hdnDelayedList" runat="server" />
							</ContentTemplate>
						</asp:UpdatePanel>
					</div>
					<br />

					<div class="editprofile" id="editform1" runat="server" visible="true">
						<div class="editprofileform">
							<asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="false" Style="font-size: medium; font-weight: bold;"></asp:Label>
							<table id="tbl_menu" runat="server">
								<tr style="padding-top: 1px; padding-bottom: 2px;">

									<td class="formtitle">
										<asp:LinkButton ID="Create_Task" Visible="true" runat="server" PostBackUrl="~/procs/Create_Task.aspx">Create Task</asp:LinkButton>
									</td>
									<td class="formtitle">
										<asp:LinkButton ID="My_Tasks" Visible="true" runat="server" PostBackUrl="~/procs/MyTask.aspx">My Tasks</asp:LinkButton>
									</td>
								</tr>
								<tr style="padding-top: 1px; padding-bottom: 2px;">

									<td class="formtitle">
										<asp:LinkButton ID="lnk_MyCancelTask" Visible="true" runat="server" PostBackUrl="~/procs/MyTask_Cancel.aspx">Cancel Task</asp:LinkButton>
									</td>
									<td class="formtitle">
                                    <asp:LinkButton ID="Lnk_CreateprojectSchedule" Visible="true" runat="server"  PostBackUrl="~/procs/Task_ProjectScheduleSettingInfo.aspx">Create Project Schedule</asp:LinkButton>
								
									</td>
								</tr>
								<hr />
								<tr style="padding-top: 1px; padding-bottom: 2px;"
									id="IsTaskExecuter" runat="server" visible="false">
									<td class="formtitle">
										<br />
										<span runat="server" style="font-size: 12pt; font-weight: bold; color: #3D1956;">Task Executor: </span>
									</td>
									<td></td>
								</tr>
								<tr style="padding-top: 1px; padding-bottom: 2px;" id="IsTaskExecuter1" runat="server" visible="false">

									<td class="formtitle">
										<asp:LinkButton ID="Inbox_Task" Visible="true" runat="server" PostBackUrl="~/procs/InboxExecuter.aspx">Inbox</asp:LinkButton>
									</td>
									<td class="formtitle">
										<asp:LinkButton ID="My_Processed_Task" Visible="true" runat="server" PostBackUrl="~/procs/ExecuterProcessedTask.aspx?app=ex">My Processed Task</asp:LinkButton>
									</td>
								</tr>
								<tr style="padding-top: 1px; padding-bottom: 2px;" id="IsTaskSupervisor" runat="server" visible="false">
									<td class="formtitle">
										<br />
										<span runat="server" style="font-size: 12pt; font-weight: bold; color: #3D1956;">Task Supervisor: </span>
									</td>
									<td></td>
								</tr>
								<tr style="padding-top: 1px; padding-bottom: 2px;" id="IsTaskSupervisor1" runat="server" visible="false">

									<td class="formtitle">
										<asp:LinkButton ID="Clusore" Visible="true" runat="server" PostBackUrl="~/procs/Task_Closure_Inbox.aspx">Task Closure Inbox</asp:LinkButton>
									</td>
									<td class="formtitle">
										<asp:LinkButton ID="My_ProcessedTask_Supervisor" Visible="true" runat="server" PostBackUrl="~/procs/ExecuterProcessedTask.aspx?app=su">My Processed Task</asp:LinkButton>
									</td>
								</tr>
								<tr id="IsTaskSupervisor2" runat="server" visible="false">
									<td class="formtitle">
										<asp:LinkButton ID="DueDate_Change_Inbox" Visible="true" runat="server" PostBackUrl="~/procs/Task_DueDateChange_Inbox.aspx">Due Date Change Request Inbox</asp:LinkButton>
									</td>
									<td></td>
								</tr>
								<tr style="padding-top: 1px; padding-bottom: 2px;" id="trReport" runat="server" visible="false">
									<td class="formtitle">
										<br />
										<span runat="server" style="font-size: 12pt; font-weight: bold; color: #3D1956;">Reports: </span>
									</td>
									<td></td>
								</tr>
								<tr style="padding-top: 1px; padding-bottom: 2px;" id="Tr1" runat="server" visible="true">
									<td class="formtitle">
										<asp:LinkButton ID="lnk_SupervisorReport" Visible="false" runat="server" PostBackUrl="~/procs/TaskMonitoringSupervisorReport.aspx">Task Supervisor - Report</asp:LinkButton>
									</td>
									<td class="formtitle">
										<asp:LinkButton ID="lnk_AuditTrialReport" Visible="false" runat="server" PostBackUrl="~/procs/TaskMonitoringAuditTrailReport.aspx">Audit Trail Report</asp:LinkButton>
									</td>
								</tr>
								<tr id="Tr2" runat="server" visible="true">
									<td class="formtitle">
										<asp:LinkButton ID="lnk_ExecuterReport" Visible="false" runat="server" PostBackUrl="~/procs/TaskMonitoringExecuterReport.aspx">Task Executor - Report</asp:LinkButton>
									</td>

									<td class="formtitle">
                                        	<asp:LinkButton ID="lnk_Alltask"   runat="server" PostBackUrl="~/procs/Alltask.aspx">All Task Report</asp:LinkButton>
									
									</td>
								</tr>

                                <tr id="Tr3" runat="server" visible="true">
									<td class="formtitle">
										<asp:LinkButton ID="lnk_ProjectDuration" Visible="false" runat="server"  OnClick="lnk_ProjectDuration_Click">Project Schedule - Report</asp:LinkButton>
                                         <asp:HiddenField ID="HFIDQString" runat="server" />
									</td>

									<td class="formtitle">
                                        
									</td>
								</tr>

								<tr style="padding-top: 1px; padding-bottom: 5px;">
									<td class="formtitle">
										<br />
										<br />
									</td>
									<td class="formtitle">
										<br />
										<br />
									</td>
								</tr>
							</table>


						</div>
					</div>
				</div>
			</div>
		</div>
	</div>

	<asp:TextBox ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>

	<br />

	<asp:HiddenField ID="hflEmpCode" runat="server" />
</asp:Content>

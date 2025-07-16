<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" 
	MaintainScrollPositionOnPostback="true"
    CodeFile="Alltask.aspx.cs" Inherits="procs_Alltask" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
	Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

	<link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
   
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leaveteamrpt.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Report.css" type="text/css" media="all" />

	<style>
		#content-container #gvMain {
			width: 215% !important;
		}

		.myaccountpagesheading {
			text-align: center !important;
			text-transform: uppercase !important;
		}


		.Dropdown {
			border-bottom: 2px solid #cccccc;
			/*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
			background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;
			cursor: default;
		}

		.grayDropdown {
			border-bottom: 2px solid #cccccc;
			/*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
			background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;
			background-color: #ebebe4;
		}

		.grayDropdownTxt {
			background-color: #ebebe4;
		}

		.taskparentclass2 {
			width: 29.5%;
			height: 112px;
		}

		.taskparentclasskkk {
			width: 29.5%;
			height: 72px;
			/*overflow:initial;*/
		}

		#MainContent_ListBox1, #MainContent_ListBox2 {
			padding: 0 0 0% 0 !important;
			/*overflow: unset;*/
		}

		#MainContent_lstApprover, #MainContent_lstIntermediate {
			padding: 0 0 33px 0 !important;
			/*overflow: unset;*/
		}

		.noresize {
			resize: none;
		}

		#MainContent_btnReset,#MainContent_btnReject, #MainContent_btnIn {
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
			padding-left: 21px;
			padding-right: 21px;
			padding-top: 6px;
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



	<div class="commpagesdiv">
		<div class="commonpages">

			<div class="userposts">
				<span>
					<asp:Label ID="lblheading" runat="server" Text="All Task"></asp:Label>
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
            <div class="edit-contact">
			<ul id="Ul1" runat="server" visible="true">
                <li class="leavedays" runat="server" visible="true">
							
							<span>Task Reference ID</span>&nbsp;&nbsp;<br />
							<asp:DropDownList runat="server" Enabled="true" ID="ddlTaskRefId"  ></asp:DropDownList>
						</li>
                <li>
                    <span>Task Created By</span>&nbsp;&nbsp;<br />
							<asp:DropDownList runat="server" Enabled="true" ID="DDlCreatedBy"  ></asp:DropDownList>
                </li>
                 <li>
                     <br />
                    <span style="display:none">Meeting Discussion Title</span>&nbsp;&nbsp;<br />
						<asp:DropDownList runat="server" Enabled="true" ID="DDLMeetingDiscussionTitle"  Visible="false"></asp:DropDownList>
                </li>
               
						<li class="date">
							<br />
							<span>Task Reference Date From </span>&nbsp;&nbsp;              
                                 <asp:TextBox ID="txtFromdate" Enabled="true" AutoComplete="off" Height="24px" CssClass="TxtHeight" runat="server" AutoPostBack="true" MaxLength="10"   OnTextChanged="txtFromdate_TextChanged" AutoCompleteType="Disabled"></asp:TextBox>
							<ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtFromdate" runat="server">
							</ajaxToolkit:CalendarExtender>
						</li>
						<li class="date">
							<br />
							<span>Task Reference Date To</span> &nbsp;&nbsp;        
                                 <asp:TextBox ID="txtToDate" AutoComplete="off" runat="server" Height="24px" CssClass="TxtHeight" AutoPostBack="true"  MaxLength="10" OnTextChanged="txtToDate_TextChanged" Enabled="true" AutoCompleteType="Disabled"></asp:TextBox>
							<ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtToDate" runat="server">
							</ajaxToolkit:CalendarExtender>

						</li>
                <li>
                    <span>Meeting Discussion Title</span>&nbsp;&nbsp;<br />
                     <asp:TextBox ID="TxtSearch_task"  runat="server" AutoComplete="off"  MaxLength="25"  Width="200px"></asp:TextBox>
                </li>
                <li></li>
              </ul>

                <div style="text-align: center">
						<asp:LinkButton ID="btnIn" runat="server" Visible="true" CssClass="Savebtnsve" Text="Search Record" OnClick="btnIn_Click" >Search Record</asp:LinkButton>
                    <asp:LinkButton ID="btnReject"  runat="server" Text="Reset" ToolTip="Reset" CssClass="Savebtnsve"  OnClick="btnReject_Click">Reset</asp:LinkButton>
						<asp:LinkButton ID="btnReset" runat="server" Visible="true" CssClass="Savebtnsve" Text="Download All Task"   OnClick="btnReset_Click">Download All Task</asp:LinkButton>
					</div>
              </div>
			<ul id="editform" runat="server" visible="false">
				<li style="width: 100%">
					<asp:GridView ID="gv_MyTask" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
						DataKeyNames="Task_Reference_ID" PageSize="20" AllowPaging="True" OnPageIndexChanging="gv_MyTask_PageIndexChanging">
						<FooterStyle BackColor="White" ForeColor="#000066" />
						<HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
						<%--<PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" />--%>
                         <PagerStyle HorizontalAlign = "Right" CssClass = "paging" />
						<RowStyle ForeColor="#000066" />
						<SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
						<SortedAscendingCellStyle BackColor="#F1F1F1" />
						<SortedAscendingHeaderStyle BackColor="#007DBB" />
						<SortedDescendingCellStyle BackColor="#CAC9C9" />
						<SortedDescendingHeaderStyle BackColor="#00547E" />
						<Columns>
							<asp:BoundField HeaderText="Task Reference Id"
								DataField="Task_Reference_ID"
								ItemStyle-HorizontalAlign="Left"
								HeaderStyle-HorizontalAlign="Left"
								ItemStyle-Width="16%" ItemStyle-BorderColor="Navy" />

                            <asp:BoundField HeaderText="Task Created by"
								DataField="Emp_Name"
								ItemStyle-HorizontalAlign="Left"
								HeaderStyle-HorizontalAlign="Left"
								ItemStyle-Width="23%" ItemStyle-BorderColor="Navy" />

							<asp:BoundField HeaderText="Task Reference Date"
								DataField="Task_Reference_Date"
								ItemStyle-HorizontalAlign="Left"
								HeaderStyle-HorizontalAlign="Left"
								ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

							<asp:BoundField HeaderText="Meeting/Discussion Title"
								DataField="Meeting_Discussion_Title"
								ItemStyle-HorizontalAlign="Left"
								HeaderStyle-HorizontalAlign="Left"
								ItemStyle-Width="30%" ItemStyle-BorderColor="Navy" />
							<asp:BoundField HeaderText="Meeting/Discussion Type"
								DataField="Name"
								ItemStyle-HorizontalAlign="Left"
								HeaderStyle-HorizontalAlign="Left"
								ItemStyle-Width="20%" ItemStyle-BorderColor="Navy" />
							<asp:BoundField HeaderText="Status"
								DataField="StatusTitle"
								ItemStyle-HorizontalAlign="Left"
								HeaderStyle-HorizontalAlign="Left"
								ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

							
							<asp:TemplateField HeaderText="Download Task" HeaderStyle-Width="0%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
								<ItemTemplate>
									<asp:ImageButton ID="lnk_Download" runat="server" Width="15px" Height="15px" ToolTip="Download Task" ImageUrl="~/images/Download.png" OnClick="lnk_Download_Click" />
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

    <script src="../js/dist/jquery-3.2.1.min.js"></script>
	<script src="../js/dist/select2.min.js"></script>
	<link href="../js/dist/select2.min.css" rel="stylesheet" />

   


	<script type="text/javascript">      
		$(document).ready(function () {
			$("#MainContent_ddlTaskRefId").select2();
			$("#MainContent_DDlCreatedBy").select2();
			$("#MainContent_DDLMeetingDiscussionTitle").select2();
		});
	</script>
    <asp:HiddenField ID="hdnTaskRefID" runat="server" />
	
</asp:Content>


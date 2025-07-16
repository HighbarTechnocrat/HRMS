<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" 
    MaintainScrollPositionOnPostback="true"
    CodeFile="ExecuterProcessedTask.aspx.cs" Inherits="ExecuterProcessedTask" %>

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

      #MainContent_mobile_btnSave,#MainContent_mobile_btnBack {
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
    </style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
  
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
       <div class="commpagesdiv">
               <div class="commonpages">
          
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="My Processed Task"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>
               
                <span>
                    <a href="TaskMonitoring.aspx" style="margin-right: 18px;" class="aaaa">Task Monitoring</a>&nbsp;&nbsp; 
                </span>
     
    
		 <div class="edit-contact">
					<ul id="Ul1" runat="server">					
						<li>
							<span runat="server" id="EmpType">Task Executor</span>&nbsp;&nbsp;
                             <br />
							<asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="ddlTaskExecuter" AutoPostBack="true" OnSelectedIndexChanged="ddlTaskExecuter_SelectedIndexChanged">
							</asp:DropDownList>
						</li>
						<li >
							<span>Task Reference ID</span>&nbsp;&nbsp;
                             <br />
							<asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="ddlTaskRefId" AutoPostBack="true" OnSelectedIndexChanged="ddlTaskRefId_SelectedIndexChanged">
							</asp:DropDownList>
						</li>
						<li style="padding-top: 10px">
							<span>Task ID</span>&nbsp;&nbsp;
                             <br />
							<asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="ddlTaskId">
							</asp:DropDownList>
						</li>
						<li style="padding-top: 10px" runat="server" id="liStatus">
							<span>Status</span>&nbsp;&nbsp;
                             <br />
							<asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="ddlStatus">
							</asp:DropDownList>
						</li>
						
					</ul>
				</div>


				<div class="mobile_Savebtndiv" style="padding-left:30%">
					<asp:LinkButton ID="mobile_btnSave" runat="server" Text="Submit" ToolTip="Search" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click">Search </asp:LinkButton>
					<asp:LinkButton ID="mobile_btnBack" runat="server" Text="Reset" OnClick="mobile_btnBack_Click" ToolTip="Clear Search" class="trvl_Savebtndiv">Reset</asp:LinkButton>
				</div>
				   <br />
				   

				   <asp:Label ID="lblcount" runat="server" style="color:red;font-size:15px;"></asp:Label>
                                <asp:GridView ID="gv_MyProcessedTaskExecuterList"  runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                    DataKeyNames="Task_Reference_ID" PageSize="20" AllowPaging="True" OnPageIndexChanging="gv_MyProcessedTaskExecuterList_PageIndexChanging">
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

                                          <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="MyProcessTaskExecuter_Edit" runat="server" Visible="true" CommandArgument='<%#Eval("ID") + "," + Eval("Task_Ref_id")%>' Width="15px" Height="35px" OnClick="MyProcessedTaskExecuter_Edit_Click"><img src="../Images/edit.png" /></asp:LinkButton> 
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Task Reference Id" Visible="false"
                                            DataField="Task_Reference_ID"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

										<asp:BoundField HeaderText="Meeting / Discussion Title"
                                            DataField="Meeting_Discussion_Title"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Meeting Date"
                                            DataField="Created_On"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="7%" ItemStyle-BorderColor="Navy" />
                                        
										<asp:BoundField HeaderText="Task Description "
                                            DataField="Task_Description"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="25%" ItemStyle-BorderColor="Navy" />

                                          <asp:BoundField HeaderText="Task Executor"
                                            DataField="Task_Executer"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="13%" ItemStyle-BorderColor="Navy" />
										
                                            <asp:BoundField HeaderText="Task Supervisor"
                                            DataField="Supervisor"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="13%" ItemStyle-BorderColor="Navy" />
                                         <asp:BoundField HeaderText="Task ID"
                                            DataField="Task_ID"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Task Due Date"
                                            DataField="Due_Date"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="7%" ItemStyle-BorderColor="Navy" />

                                       <%--  <asp:BoundField HeaderText="Task Description"
                                            DataField="Task_Description"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />--%>

                                         <asp:BoundField HeaderText="Status"
                                            DataField="StatusTitle"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />
                                        
                                          

                                    </Columns>
                                </asp:GridView>
                            </div>
              
		   <br />
		   <br />
         
     <asp:HiddenField ID="hdnTaskRefID" runat="server" />
	<asp:HiddenField ID="hdnType" runat="server" />
	<script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />
	<script>
		$(document).ready(function () {
				$(".DropdownListSearch").select2();
           
		});
</script>
</asp:Content>


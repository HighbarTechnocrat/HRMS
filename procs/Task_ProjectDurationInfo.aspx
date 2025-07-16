<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" 
    MaintainScrollPositionOnPostback="true"
    CodeFile="Task_ProjectDurationInfo.aspx.cs" Inherits="procs_Task_ProjectDurationInfo" %>

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

        .dgSupervisor {
			color: White !important;
			background-color: #669999;
			font-weight: bold;
		}
        .dgSupervisorRemove {
			color: White !important;
			background-color: none;
			font-weight: normal;
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

      #MainContent_mobile_btnSave,#MainContent_mobile_btnBack,#MainContent_BTN_Back {
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
                        <asp:Label ID="lblheading" runat="server" Text="Project Schedule Information"></asp:Label>
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
							<span runat="server" id="EmpType">Project / Location Code</span>&nbsp;&nbsp;
                             <br />
							<asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="ddlProjectLocation">
							</asp:DropDownList>
						</li>
						<li >
							
						</li>
						
						
					</ul>
				</div>


				<div class="mobile_Savebtndiv" style="padding-left:30%">
					<asp:LinkButton ID="mobile_btnSave" runat="server" Text="Submit" ToolTip="Search" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click" >Search </asp:LinkButton>
					<asp:LinkButton ID="mobile_btnBack" runat="server" Text="Reset"  ToolTip="Clear Search" class="trvl_Savebtndiv" OnClick="mobile_btnBack_Click">Reset</asp:LinkButton>
				</div>
				   <br />
				   

				   <asp:Label ID="lblcount" runat="server" style="color:red;font-size:15px;"></asp:Label>
                                <asp:GridView ID="gv_MyProcessedTaskExecuterList"  runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" 
                                 DataKeyNames="comp_code"   BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false" OnPageIndexChanging="gv_MyProcessedTaskExecuterList_PageIndexChanging"
                                   OnRowDataBound="gv_MyProcessedTaskExecuterList_RowDataBound" AutoGenerateSelectButton="True" OnSelectedIndexChanged="gv_MyProcessedTaskExecuterList_SelectedIndexChanged"   PageSize="20" AllowPaging="True">
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

                                          <asp:TemplateField HeaderText="File View" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnkEdit1" runat="server" ToolTip=" File View" Width="15px" Height="15px" ImageUrl="~/Images/Download.png" OnClientClick=<%# "DownloadFile('" + Eval("FileName") + "')" %> />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>  


										<asp:BoundField HeaderText="Project Location Code"
                                            DataField="comp_code" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Project Location Name"
                                            DataField="Location_name"  ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="23%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Latest task due date "
                                            DataField="Due_Date"  ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="9%" ItemStyle-BorderColor="Navy" />

										<asp:BoundField HeaderText="Latest Project schedule File"
                                            DataField="FileName" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="24%" ItemStyle-BorderColor="Navy" /> 

                                         
                                    </Columns>
                                </asp:GridView>

                   <br /><br />
                   <asp:Label ID="Label1" runat="server" style="font-size:15px; padding-bottom:10px" Visible="false">Project Schedule File History</asp:Label>
                   
                   <div class="mobile_Savebtndiv" style="padding-left:93%; padding-bottom:05px">
						<asp:LinkButton ID="BTN_Back" runat="server" Text="Back"  ToolTip="Clear Search" class="trvl_Savebtndiv" OnClick="BTN_Back_Click">Back</asp:LinkButton>
				</div>
                   
                   <asp:GridView ID="GVRecordsDetails"  runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" 
                                    BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false" 
                                     PageSize="20" AllowPaging="True">
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

										   <asp:TemplateField HeaderText="File View" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnkEdit1" runat="server" ToolTip=" File View" Width="15px" Height="15px" ImageUrl="~/Images/Download.png" OnClientClick=<%# "DownloadFileseprate('" + Eval("FileName") + "')" %> />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>  


                                        <asp:BoundField HeaderText="Task due date "  DataField="Due_Date"
                                            ItemStyle-HorizontalAlign="Left"  HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="9%" ItemStyle-BorderColor="Navy" />

										<asp:BoundField HeaderText="Project schedule File"
                                            DataField="FileName"  ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="24%" ItemStyle-BorderColor="Navy" />

                                        
                                       
                                         
                                    </Columns>
                                </asp:GridView>
                            </div>
              
		   <br />
		   <br />
         
     <asp:HiddenField ID="hdnTaskRefID" runat="server" />
	<asp:HiddenField ID="hdnType" runat="server" />
           <asp:HiddenField ID="FilePath" runat="server" />
           <asp:HiddenField ID="HFIDQString" runat="server" />
	<script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />
	<script>
		$(document).ready(function () {
				$(".DropdownListSearch").select2();
           
        });

        function DownloadFile(FileName) {

            var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;
            //alert(FileName);        
          //  window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
            window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
        }

         function DownloadFileseprate(FileName) {

            var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;
            //alert(FileName);        
          //  window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
            window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
        }


</script>
</asp:Content>


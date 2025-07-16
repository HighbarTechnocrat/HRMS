<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="KRA_TeamReport.aspx.cs" Inherits="KRA_TeamReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

  
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" /> --%>


    
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" /> 
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />    
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

        .taskparentclasskkk {
            width: 29.5%;
            height: 72px;
            /*overflow:initial;*/
        } 

        .Dropdown {
            border-bottom: 2px solid #cccccc;
            /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
            background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;
            cursor: default;
        }

        .textboxBackColor {
            background-color: cadetblue;
            color: aliceblue;
        } 

        .BtnShow {
            color: blue !important;
            background-color: transparent;
            text-decoration: none;
            font-size: 13px !important;
        }

            .BtnShow:visited {
                color: blue !important;
                background-color: transparent;
                text-decoration: none;
            }

            .BtnShow:hover {
                color: red !important;
                background-color: transparent;
                text-decoration: none !important;
            }

        

         .LableName {
            color: #F28820;
            font-size: 16px;
            font-weight: normal;
            text-align: left;
        }
         	  

        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
            background: #dae1ed;
        }

        .Dropdown {
            border-bottom: 2px solid #cccccc;
            /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
            background: url('../images/arrowdown.png') no-repeat right center;
            cursor: default;
        }
        input#ctl00_MainContent_ReportViewer1_ctl05_ctl03_ctl00 {
    display: none;
}

          .noresize {
            resize: none;
        }
          input.select2-search__field {
            height: 0px !important;
            padding-left: 0px !important;
        }
          #MainContent_btnback_mng{
            background: #3D1956;
            color: #febf39 !important;
            padding: 0.5% 1.4%;
            margin: 0% 0% 0 0;
        }
          
          .trvl_grid {
    width: 100% !important;
}
          #MainContent_btnback_mng{
    /*by Highbartech on 24-06-2020
        background: #febf39;*/
    background: #3D1956;
    color: #febf39 !important;
    padding: 8px 18px;
	margin: 21px 0 0px -393px !important;
    
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
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Team KRA Report"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>
                <div>
                    <span>
                        <a href="KRA_index.aspx" class="aaaa">KRA Home</a> 
                    </span>
                </div>
                <div class="edit-contact">

                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" Visible="false"></asp:LinkButton>
                        </div>
                    </div> 
                    <ul id="editform" runat="server" visible="true">
                              <li>
                                    <span>Select Period &nbsp;<span style="color: red">*</span></span><br />
                                     <asp:DropDownList runat="server" ID="lstPeriod" CssClass="DropdownListSearch" AutoPostBack="true" OnSelectedIndexChanged="lstPeriod_SelectedIndexChanged"></asp:DropDownList>
                                </li>
 
                                <li>
                                    <span>Select Employees</span><br />                                  
                                       <asp:ListBox SelectionMode="Multiple" runat="server" ID="ddl_Employees"  AppendDataBoundItems="false" AutoPostBack="false" >
                                    </asp:ListBox>
                                </li>
                                <li></li>
                                <li>
                                    <span>Approval Type</span><br />
                                     <asp:DropDownList runat="server" ID="DDLApprovalType" CssClass="DropdownListSearch">
                                         <asp:ListItem>Approval Type</asp:ListItem>
                                         <asp:ListItem Value="IS NULL" Text="Normal Approval"></asp:ListItem>
                                          <asp:ListItem Value="1" Text="Deemed Approval"></asp:ListItem>
                                     </asp:DropDownList>
                                </li> 
                        <li></li>

                        <li class="trvl_local">
                            <asp:LinkButton ID="btnback_mng" runat="server" Text="Generate Report" ToolTip="Generate Report" CssClass="Savebtnsve" OnClick="btnback_mng_Click">Generate Report</asp:LinkButton>                            
                        </li>
                        <li class="trvl_local">                             
                        </li>
                        <li class="trvl_local"></li>



                         <li class="trvl_grid" id="litrvlgrid" runat="server">
                            <div class="manage_grid" style="overflow: scroll; width: 100%; padding-top: 20px; margin-bottom: 30px;">

                                 <asp:GridView ID="dgKRA_Team" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                                  DataKeyNames="KRA_ID" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false">
                                      
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
                                            <asp:BoundField HeaderText="Emloyee Code"
                                            DataField="Emp_code"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="1%" ItemStyle-BorderColor="Navy" />  

                                        <asp:BoundField HeaderText="Emloyee Name"
                                            DataField="Emp_Name"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />  

                                          <asp:BoundField HeaderText="Band"
                                            DataField="band"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="2%" ItemStyle-BorderColor="Navy" />   
                                        
                                         <asp:BoundField HeaderText="Period"
                                            DataField="period_name"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />  
                                        
                                          <asp:BoundField HeaderText="Location"
                                            DataField="project_location"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />   

                                            <asp:BoundField HeaderText="Department"
                                            DataField="Department_Name"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />   
                                        
                                            <asp:BoundField HeaderText="Designation"
                                            DataField="DesginationName"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />   

                                           <asp:BoundField HeaderText="Submitted On"
                                            DataField="submitted_on"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="2%" ItemStyle-BorderColor="Navy" />   

                                          <asp:BoundField HeaderText="Approved On"
                                            DataField="approved_On"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="3%" ItemStyle-BorderColor="Navy" />  

                                        <asp:BoundField HeaderText="Deemed Approval"
                                            DataField="DeemedApproval"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="3%" ItemStyle-BorderColor="Navy" /> 
                                      

                                           <asp:TemplateField  HeaderText="View" ItemStyle-Width="1%">
                                            <ItemTemplate> 
                                                       <asp:ImageButton ID="lnkFileView" runat="server" ToolTip=" File View" Width="15px" Height="15px" ImageUrl="~/Images/Download.png" OnClientClick=<%# "DownloadFile('" + Eval("KRAFileName") + "')" %> />
                                                   
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                               <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField> 
                                    </Columns>
                                </asp:GridView> 
                            </div>
                        </li>
                        <li class="trvl_local">                             
                        </li>
                        <li class="trvl_local"></li>

                    </ul>
                </div>
            </div>
        </div>
    </div>
   <br /><br />
     <div style="width: 100%; overflow: auto">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="300px"
            Width="80%" ShowBackButton="False" SizeToReportContent="True"
            ShowCredentialPrompts="False" ShowDocumentMapButton="False"
            ShowPageNavigationControls="true" ShowRefreshButton="False">
        </rsweb:ReportViewer>
    </div>
    <asp:HiddenField ID="hdnloginempcode" runat="server" />
     <asp:HiddenField ID="FilePath" runat="server" />

 
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <script type="text/javascript">
  

        $(document).ready(function () {
           $(".DropdownListSearch").select2(); 
            $("#MainContent_ddl_Employees").select2();
        }); 

          function DownloadFile(KRAFileName) {
            var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;
             // alert(localFilePath + " " + KRAFileName);
            window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + KRAFileName);
			//window.open("http://192.168.21.193/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + KRAFileName);
			  //window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + KRAFileName);
        }

    </script>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="KRA_Reset.aspx.cs" Inherits="KRA_Reset" %>

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
                        <asp:Label ID="lblheading" runat="server" Text="Reset KRA"></asp:Label>
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
                                <asp:TextBox ID="txtKRA_Id" runat="server" Visible="false"></asp:TextBox>
                                    <span>Select Employees</span><br /> 
                                 <asp:DropDownList runat="server" ID="ddl_Employees" CssClass="DropdownListSearch" AutoPostBack="true" OnSelectedIndexChanged="ddl_Employees_SelectedIndexChanged" >
                                 </asp:DropDownList>
                                   <%--  <asp:ListBox SelectionMode="Multiple" runat="server" ID="ddl_Employees"  AppendDataBoundItems="false" AutoPostBack="false" >
                                    </asp:ListBox>--%>
                                </li>
                              <li>
                                    
                                     <asp:DropDownList runat="server" ID="lstPeriod" CssClass="DropdownListSearch" Visible="false" AutoPostBack="true"></asp:DropDownList>
                                </li> 
                                <li></li> 

                        <li class="trvl_local">                           
                        </li>
                        <li class="trvl_local">                             
                        </li>
                        <li class="trvl_local"></li>

                         <div>
                              <asp:LinkButton ID="emp_access_report_btnSubmit" runat="server" Text="Generate" ToolTip="Generate" OnClick="btnback_mng_Click" Visible="false">Generate</asp:LinkButton>
                                
                                <asp:LinkButton ID="claimfuel_btnDelete" runat="server" Text="Reset KRA" ToolTip="Reset KRA" OnClientClick=" return SaveMultiClick();" OnClick="claimfuel_btnDelete_Click">Reset KRA</asp:LinkButton>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:LinkButton ID="btnBack" runat="server" Text="Clear" ToolTip="Clear" OnClick="btnBack_Click" >Clear</asp:LinkButton>
                         </div>

                         <li class="trvl_grid" id="litrvlgrid" runat="server">
                            <div class="manage_grid" style="overflow: scroll; width: 100%; padding-top: 20px; margin-bottom: 30px;">

                                 <asp:GridView ID="dgEmployee_KRA" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                                  DataKeyNames="KRA_ID,Emp_code" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false">
                                      
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
                                          <asp:TemplateField  HeaderText="Select" ItemStyle-Width="1%" Visible="false">
                                            <ItemTemplate> 
                                                     <asp:CheckBox ID="chkSelect" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                               <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField> 

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
    <asp:HiddenField ID="hdnYesNo" runat="server" />
 
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <script type="text/javascript">
  

        $(document).ready(function () {
           $(".DropdownListSearch").select2(); 
            //$("#MainContent_ddl_Employees").select2();
        });

         function SaveMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=claimfuel_btnDelete.ClientID%>');

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
            if (confirm("Do you want to Reset KRA ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            } 
            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return;

        } 
         

    </script>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="EmployeeList_ELC.aspx.cs" 
    Inherits="procs_EmployeeList_ELC" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
     <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" /> 
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />

   

    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
             /*background: #dae1ed;*/
           background:#ebebe4;
        }

        #MainContent_lstApprover {
            overflow: hidden !important;
        }
        .gridpager, .gridpager td
    {
       padding-left: 5px;
       text-align: right;
    }  
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>

     
    <script src="../js/HtmlControl/jquery-1.3.2.js"></script>
    <script src="../js/dist/jquery-3.2.1.min.js"></script>       
    <script src="../js/HtmlControl/jHtmlArea-0.8.min.js"></script>
    <link href="../js/HtmlControl/jHtmlArea.css" rel="stylesheet"/>

    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Employee Lifecycle"></asp:Label>
                    </span>
                </div>
                     <div>
                        <asp:Label runat="server" ID="lblmessagesearch" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                    </div>
                <div>
                    <span style="margin-bottom: 20px">
                        <a href="ReportsMenu.aspx" class="aaaa">Report</a>
                    </span>
                </div>

                <div class="edit-contact">
                    <ul id="Ul1" runat="server">
                          <li style="padding-top:30px">
                               <span>Select Employee Name</span>&nbsp;&nbsp;
                             <br />
                           <asp:DropDownList runat="server" ID="lstEmployeeName" CssClass="DropdownListSearch"> 
                            </asp:DropDownList>
                            </li>
                        <li style="padding-top:30px">
                             <span>Select Department</span>&nbsp;&nbsp;
                             <br />
                           <asp:DropDownList runat="server" ID="LstDepartment" CssClass="DropdownListSearch"> 
                            </asp:DropDownList>
                        </li>
                         <li style="padding-top:30px">
                             <span>Select Designation</span>&nbsp;&nbsp;
                             <br />
                          <asp:DropDownList runat="server" ID="LstDesignation" CssClass="DropdownListSearch">
                            </asp:DropDownList>

                         </li>
                        <li style="padding-top:15px">
                            <span>Select Band</span>&nbsp;&nbsp;
                             <br />
                           <asp:DropDownList runat="server" ID="Lstband" CssClass="DropdownListSearch">
                            </asp:DropDownList>
                        </li>
                        <li style="padding-top:15px">
                            <span>Select Main Module</span>&nbsp;&nbsp;
                             <br />
                             <asp:DropDownList runat="server" ID="lstSkillSet" CssClass="DropdownListSearch">
                            </asp:DropDownList>
                           

                        </li>
                        <li style="padding-top:15px">
                        </li>
                         </ul>
                     <asp:HiddenField ID="hdCandidate_ID" runat="server" /> 
                </div>

                
            </div>
        </div>
    </div>

     <div class="mobile_Savebtndiv">
        <asp:LinkButton ID="mobile_btnSave" runat="server" Text="Submit" ToolTip="Search"  OnClick="mobile_btnSave_Click" CssClass="Savebtnsve">Search </asp:LinkButton>
        <asp:LinkButton ID="mobile_btnBack" runat="server" Text="Clear Search"  ToolTip="Clear Search"  OnClick="mobile_btnBack_Click" class="trvl_Savebtndiv">Clear Search</asp:LinkButton>
       </div>
    
    <br />
    <div class="manage_grid" style="width: 100%; height: auto;">
                          <asp:GridView ID="gvEmployeeListList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="solid" BorderWidth="1px" 
                            DataKeyNames="Emp_id"  OnPageIndexChanging="gvEmployeeListList_PageIndexChanging"   CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false" PageSize="20" AllowPaging="True">
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                      <PagerStyle CssClass="gridpager" HorizontalAlign="Right" />
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                        <Columns>
                            <asp:TemplateField HeaderText="View" HeaderStyle-Width="7%">
                            <ItemTemplate>
                            <asp:ImageButton id="lnkEdit" runat="server" Width="20px" Height="15px" OnClick="lnkEdit_Click"  ImageUrl="~/Images/edit.png"/>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                              <asp:BoundField HeaderText="Employee Code" DataField="Emp_Code"
                                ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="8%" />

                            <asp:BoundField HeaderText="Employee Name" DataField="Emp_Name"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="17%" />

                            <asp:BoundField HeaderText="Department" DataField="Department_Name"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="17%" />

                            <asp:BoundField HeaderText="Designation" DataField="DesginationName"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="17%" />

                               <asp:BoundField HeaderText="Band" DataField="Band"
                                ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="4%" />

                            <asp:BoundField HeaderText="Current RM"  DataField="RMName"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="15%" />

                            
                             
                        </Columns>
                    </asp:GridView>
                       
                </div>

    <br />
   
    
    <asp:HiddenField ID="HFEMP_ID" runat="server" />
    

     <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" /> 

     <script type="text/javascript">      
        $(document).ready(function () {
             $(".DropdownListSearch").select2();
        });
    </script>

</asp:Content>


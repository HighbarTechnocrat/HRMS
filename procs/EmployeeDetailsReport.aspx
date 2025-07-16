<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" 
    MaintainScrollPositionOnPostback="true"
    CodeFile="EmployeeDetailsReport.aspx.cs" Inherits="EmployeeDetailsReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
   
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
  <%-- 
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/manageleave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/ClaimMobile_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leaveteamrpt.css" type="text/css" media="all" />--%>
    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }
     /*   .select2-search__field{
            width:0.0em !important;
        }*/
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
    <script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>

    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Employee Details"></asp:Label>
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
                    <a href="ReportsMenu.aspx" style="margin-right: 18px;" class="aaaa">Employee Report</a>&nbsp;&nbsp; 
                </span>
                <div class="edit-contact">
                    <div class="editprofile" style="text-align: center; border: none;" id="divmsg" runat="server" visible="false">
                    </div>
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server"></asp:LinkButton>
                        </div>
                    </div>
                    <ul id="editform" runat="server" visible="false">

                        <li class="mobile_inboxEmpCode">
                            <span><b>Find Employee Details</b></span><br />
                            <br />
                        </li>
                        <li>
                            <asp:Label runat="server" ID="Label2" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                        </li>
                        <li>
                            <span>Department</span>
                            <br />
                            <asp:ListBox runat="server" ID="ddl_Department" CssClass="DropdownListSearch" SelectionMode="multiple"></asp:ListBox>
                        </li>
                        <li>
                            <br />
                            <span>Module</span>
                            <br />
                            <asp:ListBox runat="server" ID="ddl_Module" CssClass="DropdownListSearch" SelectionMode="multiple"></asp:ListBox>
                        </li>
                        <li>
                            <br />
                            <span>Band</span>
                            <br />
                            <asp:ListBox runat="server" ID="ddl_Band" CssClass="DropdownListSearch" SelectionMode="multiple"></asp:ListBox>
                        </li>
                       
                        <li>
                            <br />
                            <span>Project</span>
                            <br />
                            <asp:ListBox runat="server" ID="ddl_Project" CssClass="DropdownListSearch" SelectionMode="multiple"></asp:ListBox>
                        </li>
                        

                       
                        <li>
                            <br />
                            <span>Employee Name</span>
                            <br />
                            <asp:DropDownList runat="server" ID="ddl_Employee" AutoPostBack="false">
                            </asp:DropDownList>
                        </li>
                       
                       
                        <li style="width: 100%; text-align: center;">
                            <br /><br />
                                      <asp:LinkButton ID="View_Reprt" Visible="true" runat="server" Text="Search" ToolTip="Search" CssClass="Savebtnsve" OnClick="View_Report"></asp:LinkButton>
                       
                        </li>
                     
                        

                    </ul>
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" CssClass="report"  Height="200%" Width="100%"></rsweb:ReportViewer>
                    
                </div>
              
            </div>
               
        </div>

    </div>
             
    <div class="mobile_Savebtndiv">
    </div>


    <%-- <rsweb:ReportViewer ID="ReportViewer1" runat="server" CssClass="report"  Height="200%" Width="100%">   </rsweb:ReportViewer>--%>
   

    <asp:HiddenField ID="hdnvouno" runat="server" />
    <asp:HiddenField ID="hdnIsMarried" runat="server" />
    <asp:HiddenField ID="hdnFamilyDetailID" runat="server" />
    <asp:HiddenField ID="hdnEduactonDetailID" runat="server" />
    <asp:HiddenField ID="hdnCertificationDetailID" runat="server" />
    <asp:HiddenField ID="hdnProjectDetailID" runat="server" />
    <asp:HiddenField ID="hdnDomainDetailID" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="hdnempcode" runat="server" />


    <asp:HiddenField ID="FilePath" runat="server" />

    <%--<script src="../js/dist/jquery-3.2.1.min.js"></script>--%>
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <script type="text/javascript">      
        $(document).ready(function () {
            $("#MainContent_ddl_Department").select2();
         
            $("#MainContent_ddl_Project").select2();
            $("#MainContent_ddl_Module").select2();
          
            $("#MainContent_ddl_Employee").select2();
          
            $("#MainContent_lstProject").select2();
           
            $("#MainContent_ddl_Band").select2();
          
        });
    </script>
    <script type="text/javascript">

     
    </script>
</asp:Content>

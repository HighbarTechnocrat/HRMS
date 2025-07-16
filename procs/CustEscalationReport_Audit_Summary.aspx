<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" 
CodeFile="CustEscalationReport_Audit_Summary.aspx.cs" Inherits="CustEscalationReport_Audit_Summary"%>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
   <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leaveteamrpt.css" type="text/css" media="all" />
    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }
            .edit-contact input:focus {
                border-bottom: 2px solid rgb(51, 142, 201) !important;
            }

        .edit-contact input {
            padding-left: 30px !important;
            width: 83%;
        }

        .edit-contact > ul {
            padding: 0;
        }

        .taskparentclasskkk {
            width: 29.5%;
            height: 72px;
            /*overflow:initial;*/
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    
       <div class="userposts">
            <span>
                <asp:Label ID="lblheading" runat="server" Text="Customer Incident History Summary Report"></asp:Label>
            </span>
        </div>

       <div class="leavegrid">                    
             <a href="CustEscalation.aspx" class="aaa">Cust Incident Home</a>
        </div>  

       <div style="width:100%;overflow:auto;align-content:flex-start" >      
            <div class="editprofileform">                            
                <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
            </div>
       </div>
       <div class="edit-contact">
           <ul>
               <li>
                   <span style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">Department: </span><span style="color:red">*</span><br />
                   <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                      
                   </asp:DropDownList>
               </li>
               <li>
                   <span style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">Category:</span><br />
                   <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">                       
                   </asp:DropDownList>

               </li>
               <li>
                   <span style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">Assigned to: </span>
                   <br />
                   <asp:DropDownList ID="ddlAssignedto" runat="server" >
                   </asp:DropDownList>
               </li>
               
           </ul>
           <ul>
               <li>
                   <span style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">Status:</span><br />
                   <asp:DropDownList ID="ddlStatus" runat="server">
                   </asp:DropDownList>

               </li>
               <li>
                   <span style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">Escalated:</span><br />
                   <asp:DropDownList ID="ddlEscalated" runat="server">
                       <asp:ListItem Value="">Select Escalated</asp:ListItem>
                        <asp:ListItem Value="NO">No</asp:ListItem>
                        <asp:ListItem Value="AUTO">Yes-Auto</asp:ListItem>
                        <asp:ListItem Value="USER">Yes-Employee</asp:ListItem>  
                   </asp:DropDownList>

               </li>              
           </ul>
            <ul>
                <li>
                    <span style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">Creator Name:</span><br />
                    <asp:TextBox ID="txtemp" Visible="false" runat="server" CssClass="txtcls" TabIndex="0"> </asp:TextBox>
                    <asp:DropDownList ID="ddl_CreatedEmp" runat="server" >
                   </asp:DropDownList>
                </li>
                <li>
                    <span style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">Creation From Date:</span><br />
                    <asp:TextBox ID="txtFromdate" CssClass="txtcls"  AutoComplete="off" runat="server" AutoPostBack="true"  OnTextChanged="txtFromdate_TextChanged" MaxLength="10" AutoCompleteType="Disabled"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtFromdate" runat="server" >
                    </ajaxToolkit:CalendarExtender>
                </li>
                <li>
                    <span style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">Creation To Date:</span><br />
                    <asp:TextBox ID="txtToDate" CssClass="txtcls"  AutoComplete="off" runat="server" OnTextChanged="txtToDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                        runat="server">
                    </ajaxToolkit:CalendarExtender>
                </li>                          
            </ul>
           <ul>
                <li style="padding-top: 13px;">
                    <span style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">Service Request ID:</span><br />
                    <asp:TextBox ID="txtServiceRequestId" Visible="false" runat="server" CssClass="txtcls" TabIndex="0"> </asp:TextBox>
                     <asp:DropDownList ID="ddl_ServiceRequestID" runat="server" >
                   </asp:DropDownList>
                </li>
           </ul>
           <ul>
               <li>
                    <asp:LinkButton ID="btnSave" runat="server" Text="View Report" ToolTip="Reimbursement Report" CssClass="leaverpt" OnClick="btnSave_Click">Generate Report</asp:LinkButton>                        
               </li>
           </ul>
        </div> 
    

        <div style="width:100%;overflow:auto">
        
        <rsweb:ReportViewer ID="ReportViewer2" runat="server" Height="300px"
            Width="80%" ShowBackButton="False" SizeToReportContent="True"
            ShowCredentialPrompts="False" ShowDocumentMapButton="False"
            ShowPageNavigationControls="true" ShowFindControls="false" ShowExportControls="true" ShowRefreshButton="False" PageCountMode="Actual" >

        </rsweb:ReportViewer>
        </div>

<asp:HiddenField ID="hdnloginempcode" runat="server" />
    <asp:HiddenField ID="hflEmpCode" runat="server" />

    <asp:HiddenField ID="hflEmpDesignation" runat="server" />

    <asp:HiddenField ID="hflEmpDepartment" runat="server" />
     <asp:HiddenField ID="hflEmpDepartmentID" runat="server" />

    <asp:HiddenField ID="hflEmailAddress" runat="server" />

    <asp:HiddenField ID="hflGrade" runat="server" />
    <asp:HiddenField ID="hflCEO" runat="server" />
    <asp:HiddenField ID="hflDPTID" runat="server" />
    <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchEmployees" MinimumPrefixLength="1"
        CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtemp"
        ID="AutoCompleteExtender1" runat="server" FirstRowSelected="true">
    </ajaxToolkit:AutoCompleteExtender>

     <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchRequestId" MinimumPrefixLength="1"
        CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtServiceRequestId"
        ID="AutoCompleteExtender2" runat="server" FirstRowSelected="true">
    </ajaxToolkit:AutoCompleteExtender>

      <script src="../js/dist/jquery-3.2.1.min.js"></script>       
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" /> 
    
    <script type="text/javascript">      
        $(document).ready(function () {
            $("#MainContent_ddlDepartment").select2();
            $("#MainContent_ddlCategory").select2();
            $("#MainContent_ddlAssignedto").select2();
            $("#MainContent_ddlStatus").select2();
            $("#MainContent_ddlEscalated").select2();
            $("#MainContent_ddl_CreatedEmp").select2();
            $("#MainContent_ddl_ServiceRequestID").select2();
        });
    </script>

    <script type="text/javascript">

        function onCharOnlyNumber(e) {
            var keynum;
            var keychar;
            var numcheck = /[0123456789/]/;

            if (window.event) {
                keynum = e.keyCode;
            }
            else if (e.which) {
                keynum = e.which;
            }
            keychar = String.fromCharCode(keynum);
            return numcheck.test(keychar);
        }


        function call(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("calendar1");
                    cal._visibleDate = target.date;
                    cal.set_selectedDate(target.date);
                    cal._switchMonth(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged();
                    break;
            }
        }

        function call1(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("calendar2");
                    cal._visibleDate = target.date;
                    cal.set_selectedDate(target.date);
                    cal._switchMonth(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged();
                    break;
            }
        }


    </script>

</asp:Content>


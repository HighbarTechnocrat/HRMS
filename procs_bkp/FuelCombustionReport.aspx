<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="FuelCombustionReport.aspx.cs" Inherits="procs_FuelCombustionReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" 
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


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
                <asp:Label ID="lblheading" runat="server" Text="Fuel Combustion Report (Yearly)"></asp:Label>
            </span>
        </div>

       <div class="leavegrid">                    
             <a href="http://localhost/hrms/procs/ReportsMenu.aspx" class="aaa">Reports</a>
        </div>  

       <div style="width:100%;overflow:auto;align-content:flex-start" >      
            <div class="editprofileform">                            
                <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
            </div>
       </div>
       <div class="edit-contact">
           <ul>
               <li>
                   <span id="spn_dept" runat="server" style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">Year: </span><br />
                   <asp:DropDownList ID="ddlYear" runat="server">
                        <asp:ListItem Value="0" Text="-- Select --"></asp:ListItem>
                       <asp:ListItem Value="2015" Text="2015"></asp:ListItem>
                       <asp:ListItem Value="2016" Text="2016"></asp:ListItem>
                       <asp:ListItem Value="2017" Text="2017"></asp:ListItem>
                       <asp:ListItem Value="2018" Text="2018"></asp:ListItem>
                       <asp:ListItem Value="2019" Text="2019"></asp:ListItem>
                       <asp:ListItem Value="2020" Text="2020"></asp:ListItem>
                       <asp:ListItem Value="2021" Text="2021"></asp:ListItem>
                       <asp:ListItem Value="2022" Text="2022"></asp:ListItem>
                       <asp:ListItem Value="2023" Text="2023"></asp:ListItem>
                       <asp:ListItem Value="2024" Text="2024"></asp:ListItem>
                       <asp:ListItem Value="2025" Text="2025"></asp:ListItem>
                       <asp:ListItem Value="2026" Text="2026"></asp:ListItem>
                   </asp:DropDownList>
               </li>
           </ul>
          
           <ul>
               <li>
                    <asp:LinkButton ID="btnSave" runat="server" Text="View Report"  OnClick="btnSave_Click" CssClass="leaverpt">Generate Report</asp:LinkButton>                        
               </li>
           </ul>
        </div> 
    

        <div style="width:100%;overflow:auto">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" CssClass="report"  Height="200%" Width="100%"></rsweb:ReportViewer>
        
       <%-- <rsweb:ReportViewer ID="ReportViewer2" runat="server" Height="700px"
            Width="100%" ShowBackButton="False" SizeToReportContent="false"
            ShowCredentialPrompts="False" ShowDocumentMapButton="False"
            ShowPageNavigationControls="true" ShowFindControls="false" ShowExportControls="true" ShowRefreshButton="False" PageCountMode="Actual" >

        </rsweb:ReportViewer>--%>
        </div>

<asp:HiddenField ID="hdnloginempcode" runat="server" />
    <asp:HiddenField ID="hflEmpCode" runat="server" />

    <asp:HiddenField ID="hflEmpDesignation" runat="server" />

    <asp:HiddenField ID="hflEmpDepartment" runat="server" />
     <asp:HiddenField ID="hflEmpDepartmentID" runat="server" />

    <asp:HiddenField ID="hflEmailAddress" runat="server" />

    <asp:HiddenField ID="hflGrade" runat="server" />
    <asp:HiddenField ID="hflCEO" runat="server" />

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

        function noanyCharecters(e) {
            var keynum;
            var keychar;
            var numcheck = /[]/;


            if (window.event) {
                keynum = e.keyCode;
            }
            else if (e.which) {
                keynum = e.which;
            }
            var unicode = e.keyCode ? e.keyCode : e.charCode
            if (unicode == 8 || unicode == 46) {
                keychar = unicode;
            }
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


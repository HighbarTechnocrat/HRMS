<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="Update_Photo_Report.aspx.cs" Inherits="Update_Photo_Report" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">


    <div class="userposts">
        <span>
            <br /> 
            <asp:Label ID="lblheading" runat="server" Text="Update Photo Report"></asp:Label>
        </span>
    </div>

    <div class="leavegrid">
        <a href="Update_Photo_index.aspx" class="aaa">Index</a>
    </div>

    <div style="width: 100%; overflow: auto; align-content: flex-start">
        <div class="editprofileform">
            <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
        </div>
    </div>
    <div class="edit-contact">
        <ul>
                        <li style="display:none">
    <span id="Span1" runat="server" style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">Thank You Card : </span>
    <br />
    <%--<span style="color:red">*</span> --%>
    <asp:DropDownList ID="ddlthankyoucard" CssClass="DropdownListSearch" runat="server">
    </asp:DropDownList>
</li>

            <li >
                
                <span id="spn_dept"    runat="server"  style= "font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">Employee Name : </span>
                <br />
                <%--<span style="color:red">*</span> --%>
                <asp:DropDownList ID="ddlempname" CssClass="DropdownListSearch" runat="server"  >
                </asp:DropDownList>
                 
            </li>

             <li >
     
             <span id="status"    runat="server"  style= "font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">Employee Photo Status : </span>
             <br />
             <%--<span style="color:red">*</span> --%>
             <asp:DropDownList ID="emp_status" CssClass="DropdownListSearch" runat="server"  >
             </asp:DropDownList>
      
         </li>

                      
        </ul>
        
        <ul>
            <li>
                <asp:LinkButton ID="btnSave" runat="server" Text="View Report" ToolTip="Generate Report" CssClass="leaverpt" OnClick="btnSave_Click">Generate Report</asp:LinkButton>
            </li>
        </ul>
    </div>


                  
<rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="700px"
        Width="100%" ShowBackButton="False" SizeToReportContent="false"
        ShowCredentialPrompts="False" ShowDocumentMapButton="False"
        ShowPageNavigationControls="true" ShowFindControls="false" ShowExportControls="true" ShowRefreshButton="False" PageCountMode="Actual" >
</rsweb:ReportViewer>
    <br /><br />
     

    <asp:HiddenField ID="hdnloginempcode" runat="server" />
    <asp:HiddenField ID="hflEmpCode" runat="server" />

    <asp:HiddenField ID="hflEmpDesignation" runat="server" />

    <asp:HiddenField ID="hflEmpDepartment" runat="server" />
    <asp:HiddenField ID="HiddenFieldid" runat="server" />

    <asp:HiddenField ID="hflEmpDepartmentID" runat="server" />

    <asp:HiddenField ID="hflEmailAddress" runat="server" />

    <asp:HiddenField ID="hflGrade" runat="server" />
    <asp:HiddenField ID="hflCEO" runat="server" />

    <script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <script type="text/javascript">      
        $(document).ready(function () {

            $(".DropdownListSearch").select2();
        });

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


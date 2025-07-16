<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="Appreciation_Letter_Report.aspx.cs" Inherits="Appreciation_Letter_Report" %>

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
            <asp:Label ID="lblheading" runat="server" Text="Appreciation Letter Report"></asp:Label>
        </span>
    </div>

    <div class="leavegrid">
        <a href="http://localhost/hrms/procs/Appreciation_Letter_index.aspx" class="aaa">Appreciation Index</a>
    </div>

    <div style="width: 100%; overflow: auto; align-content: flex-start">
        <div class="editprofileform">
            <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
        </div>
    </div>
    <div class="edit-contact">
        <ul>
                         

            <li >
                
                <span id="spn_dept"    runat="server"  style= "font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">Employee Name : </span>
                <br /> 
                <asp:DropDownList ID="ddlempname" CssClass="DropdownListSearch" runat="server"  >
                </asp:DropDownList>
                 
            </li>

                                    <li>
    <span id="Span1" runat="server" style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">Appreciation Letter : </span>
    <br /> 
    <asp:DropDownList ID="ddlthankyoucard" CssClass="DropdownListSearch" runat="server">
    </asp:DropDownList>
</li>
                      
        </ul>
        <ul>
            <li >
                <span style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">From Date :  </span><br />
                <asp:TextBox ID="txtFromdate" CssClass="txtcls" AutoComplete="off" runat="server" AutoPostBack="false" OnTextChanged="txtFromdate_TextChanged" MaxLength="10" AutoCompleteType="Disabled"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtFromdate" runat="server">
                </ajaxToolkit:CalendarExtender>
            </li>
            <li >
                <span style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">To Date :</span><br />
                <asp:TextBox ID="txtToDate" CssClass="txtcls" AutoComplete="off" runat="server" OnTextChanged="txtToDate_TextChanged" AutoPostBack="false"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                    runat="server">
                </ajaxToolkit:CalendarExtender>
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


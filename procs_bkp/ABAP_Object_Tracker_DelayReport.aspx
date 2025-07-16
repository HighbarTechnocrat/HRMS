<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" EnableViewState="true"
    MaintainScrollPositionOnPostback="true" CodeFile="ABAP_Object_Tracker_DelayReport.aspx.cs" Inherits="ABAP_Object_Tracker_DelayReport" EnableSessionState="True" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2" Namespace="eWorld.UI" TagPrefix="ew" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <%--    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" /> --%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leaveteamrpt.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Report.css" type="text/css" media="all" />

    <style>
        #page-loader {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(255, 255, 255, 0.9);
            z-index: 9999;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .loader {
            border: 10px solid #f3f3f3;
            border-top: 10px solid #3D1956;
            border-radius: 50%;
            width: 40px;
            height: 40px;
            animation: spin 1.5s linear infinite;
        }

        #MainContent_LinkButton1, #MainContent_btnGenerateReport, #MainContent_btnReset {
            background: #3D1956;
            color: #febf39 !important;
            padding: 8px 18px;
            margin: 26px !important;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-loader">
        <div class="loader"></div>
    </div>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>

    <script src="../js/freeze/jquery-1.11.0.min.js"></script>

    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <br />
                        &nbsp;&nbsp;
                        <asp:Label ID="lblheading" runat="server" Text="ABAP Object Tracker Delay Report"></asp:Label>
                        <asp:LinkButton ID="LinkButton1" runat="server" Text="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/ABAP_Object_Tracker_Index.aspx" BackColor="#3D1956" Style="float: right; margin: 10px 20px 0 0 !IMPORTANT; font-size: 13Px;"><%=System.Configuration.ConfigurationManager.AppSettings["ABAPObjectPageTitle"]%></asp:LinkButton>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>
                <div class="edit-contact">

                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" Visible="false"></asp:LinkButton>
                        </div>

                    </div>
                    <ul id="editform1" runat="server">

                        <li class="trvl_date">
                            <span>Project / Location </span><span style="color: red">*</span><br />
                            <asp:DropDownList runat="server" ID="DDLProjectLocation" Style="width: 300px;" AutoPostBack="True" CssClass="DropdownListSearch" OnSelectedIndexChanged="DDLProjectLocation_SelectedIndexChanged">
                            </asp:DropDownList>
                        </li>

                        <li class="trvl_date">
                            <br />
                            <span>Consultant </span><span style="color: red">*</span>
                            <asp:DropDownList runat="server" ID="DDLFunctionalConsultant" Style="width: 300px;" AutoPostBack="false" CssClass="DropdownListSearch">
                            </asp:DropDownList>
                            <br />
                        </li>
                        <li></li>
                        <li class="trvl_date" style="width: 30% !important;">
                            <asp:LinkButton ID="btnGenerateReport" runat="server" ToolTip="Generate Report" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();" OnClick="btnGenerateReport_Click">Generate Report</asp:LinkButton>

                            <asp:LinkButton ID="btnReset" runat="server" Visible="true" CssClass="Savebtnsve" Text="Reset" OnClick="btnReset_Click">Reset</asp:LinkButton>

                        </li>

                    </ul>
                </div>
            </div>
        </div>
    </div>

    <br />
    <br />
    <div style="width: 100%; overflow: auto">
        <asp:ReportViewer ID="ReportViewer1" runat="server" Height="300px"
            Width="80%" ShowBackButton="False" SizeToReportContent="True"
            ShowCredentialPrompts="False" ShowDocumentMapButton="False"
            ShowPageNavigationControls="true" ShowRefreshButton="False" PageCountMode="Actual" ShowFindControls="false">
        </asp:ReportViewer>


    </div>

    <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
    <asp:TextBox AutoComplete="off" ID="txtIsSecurity_DepositInvoice" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
    <asp:TextBox AutoComplete="off" ID="txtEmpName" runat="server" Visible="false"></asp:TextBox>
    <asp:Label ID="testsanjay" runat="server" Visible="false"></asp:Label>
    <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />

    <asp:HiddenField ID="hflEmpName" runat="server" />
    <asp:HiddenField ID="hflEmpDesignation" runat="server" />
    <asp:HiddenField ID="hflEmpDepartment" runat="server" />
    <asp:HiddenField ID="hflEmailAddress" runat="server" />
    <asp:HiddenField ID="hflGrade" runat="server" />

    <asp:HiddenField ID="FilePath" runat="server" />
    <asp:HiddenField ID="hdnSrno" runat="server" />
    <asp:HiddenField ID="hdnpamentStatusid" runat="server" />
    <asp:HiddenField ID="hdnIGSTAmt" runat="server" />
    <asp:HiddenField ID="hdnCompCode" runat="server" />
    <asp:HiddenField ID="hdnVendorId" runat="server" />
    <asp:HiddenField ID="hdnPOWOID" runat="server" />
    <asp:HiddenField ID="hdnInvoiceId" runat="server" />
    <asp:HiddenField ID="hdnABAPODUploadId" runat="server" />


    <asp:HiddenField ID="hdnprojectManager" runat="server" />
    <asp:HiddenField ID="hdnprojectManagerEmail" runat="server" />

    <asp:HiddenField ID="hdnprogramnager" runat="server" />
    <asp:HiddenField ID="hdnprogramnagerEmail" runat="server" />

    <asp:HiddenField ID="hdnNewRGSConsultantEmailId" runat="server" />
    <asp:HiddenField ID="hdnNewRGSConsultantName" runat="server" />

    <asp:HiddenField ID="hdnNewFSConsultantEmailId" runat="server" />
    <asp:HiddenField ID="hdnNewFSConsultantName" runat="server" />

    <asp:HiddenField ID="hdnNewABAPConsultantEmailId" runat="server" />
    <asp:HiddenField ID="hdnNewABAPConsultantName" runat="server" />

    <asp:HiddenField ID="hdnNewHBTConsultantEmailId" runat="server" />
    <asp:HiddenField ID="hdnNewHBTConsultantName" runat="server" />

    <asp:HiddenField ID="hdnNewCTMConsultantEmailId" runat="server" />
    <asp:HiddenField ID="hdnNewCTMConsultantName" runat="server" />



    <asp:HiddenField ID="hdnOldRGSConsultantEmailId" runat="server" />
    <asp:HiddenField ID="hdnOldRGSConsultantName" runat="server" />

    <asp:HiddenField ID="hdnOldFSConsultantEmailId" runat="server" />
    <asp:HiddenField ID="hdnOldFSConsultantName" runat="server" />

    <asp:HiddenField ID="hdnOldABAPConsultantEmailId" runat="server" />
    <asp:HiddenField ID="hdnOldABAPConsultantName" runat="server" />

    <asp:HiddenField ID="hdnOldHBTConsultantEmailId" runat="server" />
    <asp:HiddenField ID="hdnOldHBTConsultantName" runat="server" />

    <asp:HiddenField ID="hdnOldCTMConsultantEmailId" runat="server" />
    <asp:HiddenField ID="hdnOldCTMConsultantName" runat="server" />

    <asp:HiddenField ID="hdnstype_Main" runat="server" />
    <asp:HiddenField ID="hdnMilestoneID" runat="server" />

    <asp:HiddenField ID="hdnPOTypeId" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />

    <asp:HiddenField ID="hdn_next_Appr_ID" runat="server" />
    <asp:HiddenField ID="hdn_next_Appr_Empcode" runat="server" />
    <asp:HiddenField ID="hdn_next_Appr_EmpEmail_ID" runat="server" />
    <asp:HiddenField ID="hdn_next_Appr_Emp_Name" runat="server" />

    <asp:HiddenField ID="hdn_curnt_Appr_ID" runat="server" />
    <asp:HiddenField ID="hdn_curnt_Appr_Empcode" runat="server" />
    <asp:HiddenField ID="hdn_curnt_Appr_EmpEmail_ID" runat="server" />
    <asp:HiddenField ID="hdn_curnt_Appr_Emp_Name" runat="server" />
    <asp:HiddenField ID="HDProjectLocation" runat="server" />

    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <script src="../js/freeze/jquery-ui.min.js"></script>
    <script src="../js/freeze/gridviewScroll.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            function applyGridViewScroll(selector, options) {
                $(selector).gridviewScroll(options);
            }
            $("#MainContent_DDLProjectLocation").select2();
            $(".DropdownListSearch").select2();
            const gridOptions = {
                width: 1070,
                height: 300,
                freezesize: 3,
                headerrowcount: 1
            };

            applyGridViewScroll('#MainContent_gvRGSDetails', gridOptions);
            applyGridViewScroll('#MainContent_gvFSDetails', gridOptions);
            applyGridViewScroll('#MainContent_gvABAPDevDetails', gridOptions);
            applyGridViewScroll('#MainContent_gvHBTTestDetails', gridOptions);
            applyGridViewScroll('#MainContent_gvCTMTestDetails', gridOptions);

        });

        document.onreadystatechange = function () {
            if (document.readyState === "complete") {
                document.getElementById("page-loader").style.display = "none";
            }
        };

        // For async postbacks
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(function () {
            document.getElementById("page-loader").style.display = "flex";
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            document.getElementById("page-loader").style.display = "none";
        });


       <%-- function SaveMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=btnback_mng.ClientID%>');

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
        }--%>

        function Confirm() {
            //Testing();
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Submit ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }

            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return;

        }

        function DownloadFile(file) {
            var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;

            //alert(localFilePath);
            //window.open("https://ess.highbartech.com/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
            //window.open("https://192.168.21.193/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
            window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
        }


    </script>
</asp:Content>

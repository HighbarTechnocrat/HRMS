<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="KRA_TemplateReport.aspx.cs" Inherits="KRA_TemplateReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

  
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
   <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" /> 
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leaveteamrpt.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Report.css" type="text/css" media="all" />

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
                        <asp:Label ID="lblheading" runat="server" Text="Template Status Report"></asp:Label>
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
                        
 
                       <li class="trvl_date" style="display:none">
                                    <span>Select Period &nbsp;<span style="color: red">*</span></span><br />
                                     <asp:DropDownList runat="server" ID="lstPeriod" CssClass="DropdownListSearch" ></asp:DropDownList>
                                </li>
                                <li class="trvl_date">
                                    <span>Select Department</span><br />
                                    <%--<asp:DropDownList runat="server" ID="lstDepartment" se   CssClass="DropdownListSearch" ></asp:DropDownList>--%>
									<asp:ListBox runat="server" ID="lstDepartment" SelectionMode="Multiple"   CssClass="DropdownListSearch" ></asp:ListBox>
                                </li>
                                  <li style="display:none">
                                    <span>Approval Type</span><br />
                                     <asp:DropDownList runat="server" ID="DDLApprovalType" CssClass="DropdownListSearch">
                                         <asp:ListItem>Approval Type</asp:ListItem>
                                         <asp:ListItem Value="No" Text="Normal Approval"></asp:ListItem>
                                          <asp:ListItem Value="Yes" Text="Deemed Approval"></asp:ListItem>
                                     </asp:DropDownList>
                                </li>
						 

                        <li class="trvl_local">
                            <asp:LinkButton ID="mobile_btnSave" runat="server" Text="Generate Report" ToolTip="Generate Report" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click">Generate Report</asp:LinkButton>                            
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
            Width="80%" ShowBackButton="False" SizeToReportContent="true" 
        ShowCredentialPrompts="False" ShowDocumentMapButton="False" 
        ShowPageNavigationControls="true" ShowRefreshButton="False" ShowExportControls="true"
	 PageCountMode="Actual">
			  </rsweb:ReportViewer>
    </div>
    <asp:HiddenField ID="hdnloginempcode" runat="server" />
     <asp:HiddenField ID="FilePath" runat="server" />

 
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <script type="text/javascript">
  

        $(document).ready(function () {
           $(".DropdownListSearch").select2(); 
        }); 

          function DownloadFile(KRAFileName) {
            var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;
             // alert(localFilePath + " " + KRAFileName);
            //window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
            // window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + KRAFileName);
			window.open("https://192.168.21.193/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + KRAFileName);

        }

    </script>
</asp:Content>


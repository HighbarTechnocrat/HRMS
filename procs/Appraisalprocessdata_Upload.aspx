<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="Appraisalprocessdata_Upload.aspx.cs" Inherits="Appraisalprocessdata_Upload" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />

    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
            background: #dae1ed;
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
                        <asp:Label ID="lblheading" runat="server" Text="Appraisal Data Process Report"></asp:Label>
                    </span>
                </div>

                <span>
                    <a href="appraisalindex.aspx" class="aaaa">Appraisal Index</a>
                </span>

              

                 <div>
                    <asp:Label runat="server" ID="lblmessage"  Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>

                <div class="edit-contact">
                   
                    <ul id="editform" runat="server">
                        <li style="padding-top: 30px">
                                <asp:FileUpload ID="ploadexpfile" Visible="true" runat="server" AllowMultiple="false" accept=".xls,.xlsx" Width="50%"></asp:FileUpload>
                        </li>
                        <li style="padding-top: 30px"> 
                        </li>
                        <li style="padding-top: 30px"> 
                        </li> 
                    </ul>
                </div>

                
                <div class="mobile_Savebtndiv" style="margin-top: 20px !important;"> 
                    <span style="display:inline">
                    <asp:LinkButton ID="mobile_btnSave" runat="server" Text="Upload Appraisal Process Data" ToolTip="Upload Appraisal Process Data" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click">Upload Process Data </asp:LinkButton>
                    </span> 
                <span>
                    <asp:LinkButton ID="mobile_btnBack" runat="server" Visible="false" Text="Clear Search"   ToolTip="Clear Search" class="trvl_Savebtndiv">Clear Report</asp:LinkButton>
                </span>
                <span>
                    <asp:LinkButton ID="trvl_btnSave" Visible="false" runat="server" Text="View & Send Email to Approver" ToolTip="View & Send E-mail" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();" PostBackUrl="~/procs/VSCB_SendEmailPendingPayments.aspx"></asp:LinkButton>
                </span>
            </div>
              
                <br />

                <div style="width: 100%; overflow: auto">
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="400px"
                        Width="100%" ShowBackButton="False" SizeToReportContent="false"
                        ShowCredentialPrompts="False" ShowDocumentMapButton="False"
                        ShowPageNavigationControls="true" ShowFindControls="false" ShowExportControls="true" ShowRefreshButton="False" PageCountMode="Actual">
                    </rsweb:ReportViewer>
                </div>
                <br />
                <br />
                <asp:TextBox ID="txtEmpCode" runat="server" Visible="false"></asp:TextBox>

                <asp:HiddenField ID="hdnPOWOID" runat="server" />
                <asp:HiddenField ID="hdnMilestoneId" runat="server" />
                <asp:HiddenField ID="hdnInvoiceId" runat="server" />
                <asp:HiddenField ID="hdnEmpCode" runat="server" />

            </div>
        </div>
    </div>

    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <script type="text/javascript">

        $(document).ready(function () {
            $(".DropdownListSearch").select2();
        });

    </script>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="VSCB_Rpt_VendorPaymenthistory.aspx.cs" Inherits="VSCB_Rpt_VendorPaymenthistory" %>

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
                        <asp:Label ID="lblheading" runat="server" Text="Report - Vendor Payment History"></asp:Label>
                    </span>
                </div>

                <span>
                     <a href="vscb_index.aspx" class="aaaa"><%=System.Configuration.ConfigurationManager.AppSettings["VSCBPageTitle"]%></a>
                </span>

                <div class="edit-contact">
                    <ul id="editform" runat="server">
                        <li style="padding-top: 30px">
                            <span>PO/ WO No.</span>&nbsp;&nbsp;<br />
                            <asp:DropDownList runat="server" ID="lstPOWONo" CssClass="DropdownListSearch"></asp:DropDownList>
                        </li>
                        <li style="padding-top: 30px">
                            <span>Vendor Name</span>&nbsp;&nbsp;<br />
                            <asp:DropDownList runat="server" ID="lstVendorName" CssClass="DropdownListSearch"></asp:DropDownList>                             
                        </li>
                        <li style="padding-top: 30px">
                            <span>Department</span>&nbsp;&nbsp;   <br />                           
                            <asp:DropDownList runat="server" ID="lstDepartment" CssClass="DropdownListSearch"></asp:DropDownList>                            
                        </li>


                        <li style="padding-top: 15px">
                            <span>Invoice No</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstInvoiceNo">
                            </asp:DropDownList>
                        </li>
                        <li style="padding-top: 15px">
                          <span>Payment Request No</span>&nbsp;&nbsp; <br />                             
                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstpaymentReqNo"></asp:DropDownList>                             
                        </li>
                        <li style="padding-top: 15px">
                            <span>Cost Center</span>&nbsp;&nbsp;<br />
                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstCostCenter"></asp:DropDownList>                             
                        </li>

                        <li style="padding-top: 15px">
                              <span>Invoice From Date</span>&nbsp;&nbsp;<br />
                             <asp:TextBox ID="txtInvoiceFrmDt" runat="server"  AutoComplete="off"></asp:TextBox>
                             <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd-MM-yyyy" TargetControlID="txtInvoiceFrmDt" runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>
                        <li style="padding-top: 15px">
                             <span>Invoice To Date</span>&nbsp;&nbsp;<br />                              
                              <asp:TextBox ID="txtInvoiceToDt" runat="server"  AutoComplete="off"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd-MM-yyyy" TargetControlID="txtInvoiceToDt" runat ="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>
                        <li style="padding-top: 15px">

                        </li>


                        <li style="padding-top: 15px">
                            <span>Payment Request From Date</span>&nbsp;&nbsp; <br />                             
                            <asp:TextBox ID="txtPaymntReqFrmDt" runat="server"  AutoComplete="off"></asp:TextBox>
                              <ajaxToolkit:CalendarExtender ID="CalendarExtender3" Format="dd-MM-yyyy" TargetControlID="txtPaymntReqFrmDt" runat ="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>
                        <li style="padding-top: 15px">
                              <span>Payment Request  To Date</span>&nbsp;&nbsp;<br />                              
                             <asp:TextBox ID="txtPaymntReqToDt" runat="server"  AutoComplete="off"></asp:TextBox>
                              <ajaxToolkit:CalendarExtender ID="CalendarExtender4" Format="dd-MM-yyyy" TargetControlID="txtPaymntReqToDt" runat ="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>
                        <li style="padding-top: 15px"></li>
                    </ul>
                </div>


                <div class="mobile_Savebtndiv" style="margin-top: 20px !important">
                    <asp:LinkButton ID="mobile_btnSave" runat="server" Text="View Report" ToolTip="Search" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click">View Report </asp:LinkButton>
                    <asp:LinkButton ID="mobile_btnBack" runat="server" Text="Clear Search" OnClick="mobile_btnBack_Click" ToolTip="Clear Search" class="trvl_Savebtndiv">Clear Report</asp:LinkButton>
                </div>

                 <div style="width:100%;overflow:auto">
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="700px"
            Width="100%" ShowBackButton="False" SizeToReportContent="false"
            ShowCredentialPrompts="False" ShowDocumentMapButton="False"
            ShowPageNavigationControls="true" ShowFindControls="false" ShowExportControls="true" ShowRefreshButton="False" PageCountMode="Actual" >
    </rsweb:ReportViewer>
        </div>
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


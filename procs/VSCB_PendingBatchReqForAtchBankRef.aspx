<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="VSCB_PendingBatchReqForAtchBankRef.aspx.cs" Inherits="VSCB_PendingBatchReqForAtchBankRef" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/manageleave_css.css" type="text/css" media="all" />

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

        .LableName {
            color: #F28820;
            font-size: 16px;
            font-weight: normal;
            text-align: left;
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
                        <asp:Label ID="lblheading" runat="server" Text="Pending Batch Requests for Attach Bank Ref."></asp:Label>
                    </span>
                </div>
                <div>
                    <span>
                     <a href="vscb_index.aspx" class="aaaa"><%=System.Configuration.ConfigurationManager.AppSettings["VSCBPageTitle"]%></a>
                </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                </div>
                 

                <div class="edit-contact">
                    <ul id="Ul1" runat="server">

                        <li style="padding-top: 30px;display:none">
                            <span>PO/ WO No.</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" ID="lstPOWONo" CssClass="DropdownListSearch">
                            </asp:DropDownList>

                        </li>
                        <li style="padding-top: 30px;display:none">
                            <span>PO/ WO Date</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstPOWODate">
                            </asp:DropDownList>
                        </li>
                        <li style="padding-top: 15px;display:none">
                            <span>Vendor Name</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" ID="lstVendorName" CssClass="DropdownListSearch">
                            </asp:DropDownList>
                        </li>
                        <li style="padding-top: 15px;display:none">
                            <span>Invoice No</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstInvoiceNo">
                            </asp:DropDownList>
                        </li>
                        <li style="padding-top: 15px;display:none">
                            <span>Payment Request No.</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstPaymentRequestNo">
                            </asp:DropDownList>
                        </li>
                        <li style="padding-top: 15px;display:none">
                            <span>Payment Request Date</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstpaymentRequestDate">
                            </asp:DropDownList>
                        </li>
                        <li style="padding-top: 15px;display:none">
                            <span>Payment Request Amt</span>&nbsp;&nbsp;
                             <br />
                            <asp:TextBox runat="server" ID="txtPaymentRequestamt" AutoComplete="off" CssClass="number"></asp:TextBox>
                        </li>
                        <li>
                            <span style="display:none;display:none">Status</span>&nbsp;&nbsp;                            
                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstStatus" Visible="false">
                            </asp:DropDownList>
                        </li>
                        <li></li>

                        <li style="display:none">
                            <span>
                                <asp:LinkButton ID="btnCorrection" runat="server" Text="Search" ToolTip="Search" CssClass="Savebtnsve"  OnClick="btnCorrection_Click">Search</asp:LinkButton>
                            </span>

                             <span>
                            <asp:LinkButton ID="btnback_mng" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/vscb_index.aspx">Back</asp:LinkButton>
                        </span>
                        </li>

                        <li class="trvl_grid" id="litrvlgrid" runat="server">
                            <div class="manage_grid" style="width: 100%; height: auto;">

                                <asp:GridView ID="gvMngTravelRqstList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                                    DataKeyNames="Batch_ID" CellPadding="3" AutoGenerateColumns="False" Width="80%" EditRowStyle-Wrap="false" PageSize="20" AllowPaging="True" OnPageIndexChanging="gvMngTravelRqstList_PageIndexChanging">
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                    <PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />

                                    <Columns>
                                           <asp:TemplateField ItemStyle-Width="2%" HeaderText="Details" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkLeaveDetails" runat="server"  ToolTip="View"  Text="View" OnClick="lnkLeaveDetails_Click" CssClass="BtnShow" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>   

                                        <asp:BoundField HeaderText="Sr.No"
                                            DataField="Srno"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="2%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Batch No"
                                            DataField="Batch_No"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Batch Date"
                                            DataField="Batch_Date"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Batch No Request"
                                            DataField="Batch_No_Requests"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Total Batch Amount"
                                            DataField="Batch_Total_Payament"
                                            ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />
 
                                   
                                    </Columns>
                                </asp:GridView>

                            </div>
                        </li>
                          

                    </ul>
                </div>
                <div class="trvl_Savebtndiv"> 

                     
                </div>

                <ul>
                    <li>
                        <asp:Label runat="server" ID="RecordCount" Style="color: red; font-size: 14px;"></asp:Label>
                    </li>
                    <li></li>
                    <li></li>
                </ul>



                <%--'<%#"~/Details.aspx?ID="+Eval("ID") %>'>--%>



                <div class="edit-contact">
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" OnClick="lnkcont_Click"></asp:LinkButton>
                        </div>
                    </div>
                    <ul id="editform" runat="server" visible="false">
                        <li></li>
                    </ul>
                </div>
                <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
                <asp:TextBox AutoComplete="off" ID="txtEmpName" runat="server" Visible="false"></asp:TextBox>
                <asp:HiddenField ID="hdnPOWOID" runat="server" />
                <asp:HiddenField ID="hdnMilestoneId" runat="server" />
                <asp:HiddenField ID="hdnInvoiceId" runat="server" />

                <asp:HiddenField ID="hflEmpName" runat="server" />
                <asp:HiddenField ID="hflEmpDesignation" runat="server" />
                <asp:HiddenField ID="hflEmpDepartment" runat="server" />
                <asp:HiddenField ID="hflEmailAddress" runat="server" />
                <asp:HiddenField ID="hflGrade" runat="server" />
                
                 <asp:HiddenField ID="hdnYesNo" runat="server" />


            </div>
        </div>
    </div>

    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <script type="text/javascript">      
        $(document).ready(function () {
            $(".DropdownListSearch").select2();
        });

        function textboxMultilineMaxNumber(txt, maxLen) {
            try {
                if (txt.value.length > (maxLen - 1)) return false;
            } catch (e) {
            }
        }


        function maxLengthPaste(field, maxChars) {
            event.returnValue = false;
            if ((field.value.length + window.clipboardData.getData("Text").length) > maxChars) {
                return false;
            }
            event.returnValue = true;
        }

        function Count(text) {
            var maxlength = 250;
            var object = document.getElementById(text.id)
            if (object.value.length > maxlength) {
                object.focus();
                object.value = text.value.substring(0, maxlength);
                object.scrollTop = object.scrollHeight;
                return false;
            }
            return true;
        }

          

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
            //alert(confirm_value.value);
            //document.forms[0].appendChild(confirm_value);
            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return;

        }

    </script>
</asp:Content>


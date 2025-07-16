<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="VSCB_Rpt_POWO_Pending_Status.aspx.cs" Inherits="procs_VSCB_Rpt_POWO_Pending_Status" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />
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

             input.select2-search__field {
            padding-left: 0px !important;
            height: 0px !important;
            border:0px !important;
        }

        li.select2-search--inline {
            float: left !important;
            width: 0px !important;
            height: 0px !important;
            border: 0px !important;
        }

        select2-search--dropdown {
            float: left !important;
            width: 0px !important;
            height: 0px !important;
            border: 0px !important;
        }

        select2-search--dropdown select2-search__field {
            padding-left: 0px !important;
            height: 40px !important;
        }
    </style>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>

    <script src="../js/freeze/jquery-1.11.0.min.js"></script>

    <div id="loader" class="myLoader" style="display: none">
        <div class="loaderContent">
            <span style="top: -30%; font-size: 17px; color: red; position: absolute;">Please  Do Not Refresh  or Close Browser</span>
            <img src="../images/loader.gif" />
        </div>

    </div>
    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">

                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="PO/WO Pending Status Report"></asp:Label>
                    </span>
                </div>

                 <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="false" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;">
                    </asp:Label>
                </div>

                <span>
                    <a href="vscb_index.aspx" class="aaaa"><%=System.Configuration.ConfigurationManager.AppSettings["VSCBPageTitle"]%></a>
                </span>

                <div class="edit-contact">
                    <ul id="editform" runat="server">
                        <li style="padding-top: 30px">
                            <span>PO/ WO No.</span>&nbsp;&nbsp;<br />
                            <asp:DropDownList runat="server" ID="lstPOWONo" CssClass="DropdownListSearch" AutoPostBack="true" OnSelectedIndexChanged="lstPOWONo_SelectedIndexChanged"></asp:DropDownList>
                        </li>
                        <li style="padding-top: 30px">
                            <span>Vendor Name</span>&nbsp;&nbsp;<br />
                            <asp:DropDownList runat="server" ID="lstVendorName" CssClass="DropdownListSearch" AutoPostBack="true"  OnSelectedIndexChanged="lstVendorName_SelectedIndexChanged"></asp:DropDownList>
                        </li>

                        <li style="padding-top: 30px">
                            <span>Cost Center</span>&nbsp;&nbsp;<br />
                            <asp:DropDownList runat="server" ID="lstCostCenter" CssClass="DropdownListSearch" AutoPostBack="true"  OnSelectedIndexChanged="lstCostCenter_SelectedIndexChanged"></asp:DropDownList>
                        </li>


                        <li style="padding-top: 30px">
                            <span>CC Employee Email Address</span>&nbsp;&nbsp;<br />
                            <%--<asp:DropDownList runat="server" ID="DropDownList1" CssClass="DropdownListSearch" AutoPostBack="true"></asp:DropDownList>--%>

                             <asp:ListBox runat="server" Width="300px"  ID="lstCCEmployeeEmailAddress" CssClass="DropdownListSearch" SelectionMode="multiple">
                             </asp:ListBox>
                        </li>
                        <li style="padding-top: 30px">
                              </li>

                        <li style="padding-top: 30px">
                         </li>
                    </ul>
                </div>


                <div class="mobile_Savebtndiv" style="margin-top: 20px !important">
                     <asp:LinkButton ID="mobile_cancel" runat="server" Text="View & Send Mail" OnClick="mobile_cancel_Click" ToolTip="Send Mail" OnClientClick="return SaveMultiClick();" class="trvl_Savebtndiv">Send Mail</asp:LinkButton>

                    <asp:LinkButton ID="mobile_btnSave" Visible="false" runat="server" Text="View Report" ToolTip="Search" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click">View Report </asp:LinkButton>
                    <asp:LinkButton ID="mobile_btnBack" runat="server" Text="Clear Search" OnClick="mobile_btnBack_Click" ToolTip="Clear Search" class="trvl_Savebtndiv">Clear Report</asp:LinkButton>
                    
                </div>
                <br />

                <div style="width: 100%;">
                    <asp:Label runat="server" ID="RecordCount" Style="color: red; font-size: 14px;"></asp:Label>

                    <asp:GridView ID="gvMngPaymentList_Batch" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                        DataKeyNames="POID" CellPadding="3" AutoGenerateColumns="False" Width="155%" EditRowStyle-Wrap="false" PageSize="20" AllowPaging="true" OnRowDataBound="gvMngPaymentList_Batch_RowDataBound" OnPageIndexChanging="gvMngPaymentList_Batch_PageIndexChanging">
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
                            <asp:BoundField HeaderText="PO/WO No"
                                DataField="PONumber"
                                ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left"  ItemStyle-BorderColor="Navy" ItemStyle-Width="10%" />
                               
                             <asp:BoundField HeaderText="PO/WO Date"
                                DataField="PODate"
                                ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" ItemStyle-BorderColor="Navy" ItemStyle-Width="10%" />
                           
                            <asp:BoundField HeaderText="PO/WO Amount"
                                DataField="POWOAmount"
                                ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" ItemStyle-BorderColor="Navy" ItemStyle-Width="9%" />
                            
                            <asp:BoundField HeaderText="PO/WO Upload Date"
                                DataField="POuploaddate"
                                ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" ItemStyle-BorderColor="Navy" ItemStyle-Width="10%" />
                            
                            <asp:BoundField HeaderText="PO/WO Created By"
                                DataField="POWOCreatedBy"
                                ItemStyle-HorizontalAlign="left"  HeaderStyle-HorizontalAlign="left" ItemStyle-BorderColor="Navy" ItemStyle-Width="10%" />
                            
                            <asp:BoundField HeaderText="Cost Center"
                                DataField="CostCenter"
                                ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" ItemStyle-BorderColor="Navy" ItemStyle-Width="15%" />
                            
                            <asp:BoundField HeaderText="Vendor Name"
                                DataField="VendorName"
                                ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" ItemStyle-BorderColor="Navy" ItemStyle-Width="10%" />
                          
                             <asp:BoundField HeaderText="Resource Name"
                                DataField="ResourceName"
                                ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" ItemStyle-BorderColor="Navy" ItemStyle-Width="10%" />
                          
                            <asp:BoundField HeaderText="Approver-1"
                                DataField="Approver1"
                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderColor="Navy" ItemStyle-Width="15%" />
                           
                            <asp:BoundField HeaderText="Approver-2"
                                DataField="Approver2"
                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderColor="Navy" ItemStyle-Width="15%" />
                           
                            <asp:BoundField HeaderText="Approver-3"
                                DataField="Approver3"
                                ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" ItemStyle-BorderColor="Navy" ItemStyle-Width="15%" />
                           
                            <asp:BoundField HeaderText="Approver-4"
                                DataField="Approver4"
                                ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" ItemStyle-BorderColor="Navy" ItemStyle-Width="15%" />
                            
                        </Columns>
                    </asp:GridView>
                </div>
                <br />
                <br />
                <asp:TextBox ID="txtEmpCode" runat="server" Visible="false"></asp:TextBox>

                <asp:HiddenField ID="hdnPOWOID" runat="server" />
                <asp:HiddenField ID="hdnMilestoneId" runat="server" />
                <asp:HiddenField ID="hdnInvoiceId" runat="server" />
                <asp:HiddenField ID="hdnEmpCode" runat="server" />
                <asp:HiddenField ID="hdnYesNo" runat="server" />

            </div>
        </div>
    </div>

    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <script src="../js/freeze/jquery-ui.min.js"></script>
    <script src="../js/freeze/gridviewScroll.min.js"></script>
    <link href="../includes/loader.css" rel="stylesheet" />

    <script type="text/javascript">

        $(document).ready(function () {
            $(".DropdownListSearch").select2();
            
		
		 $('#MainContent_gvMngPaymentList_Batch').gridviewScroll({
                width: 1060,
                height: 600,
                freezesize: 4, // Freeze Number of Columns.
                headerrowcount: 1, //Freeze Number of Rows with Header.
			});

			});
		function SaveMultiClick() {
			try {
				var retunboolean = true;
				var ele = document.getElementById('<%=mobile_cancel.ClientID%>');
				$('#loader').show();
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
		}
		function Confirm() {
			var confirm_value = document.createElement("INPUT");
			confirm_value.type = "hidden";
			confirm_value.name = "confirm_value";
			if (confirm("Do you want to Send Mail ?")) {
				confirm_value.value = "Yes";
			} else {
				confirm_value.value = "No";
			}
			document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
			return;

		}

    </script>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="VSCB_MyapprovedPOWO.aspx.cs" Inherits="VSCB_MyapprovedPOWO" %>

 
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

              .paging a {
            background-color: #C7D3D4;
            padding: 5px 7px;
            text-decoration: none;
            border: 1px solid #C7D3D4;
        }

            .paging a:hover {
                background-color: #E1FFEF;
                color: #00C157;
                border: 1px solid #C7D3D4;
            }

        .paging span {
            background-color: #E1FFEF;
            padding: 5px 7px;
            color: #00C157;
            border: 1px solid #C7D3D4;
        }

        tr.paging {
            background: none !important;
        }

            tr.paging tr {
                background: none !important;
            }

            tr.paging td {
                border: none;
            }

      
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>

    <script src="../js/freeze/jquery-1.11.0.min.js"></script>
	 <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" /> 


    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">

                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Approved PO/ WO"></asp:Label>
                    </span>
                </div>

                <span>
                     <a href="vscb_index.aspx" class="aaaa"><%=System.Configuration.ConfigurationManager.AppSettings["VSCBPageTitle"]%></a>
                </span>

                <span>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                </span>

                <div class="edit-contact">
                    <ul id="editform" runat="server">


                        <li style="padding-top: 30px">
                            <span>PO/ WO No.</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" ID="lstPOWONo" CssClass="DropdownListSearch">
                            </asp:DropDownList>
                        </li>
                        <li style="padding-top: 30px">
                            <span>PO/ WO Date</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstPOWODate">
                            </asp:DropDownList>
                        </li>
                        <li style="padding-top: 30px">
                            <span>Vendor Name</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" ID="lstVendorName" CssClass="DropdownListSearch">
                            </asp:DropDownList>
                        </li>

                        <li style="padding-top: 15px">
                            <span>Cost Center</span>&nbsp;&nbsp;
                             <br />
                             <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstCostcenter">
                            </asp:DropDownList>

                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstDepartment" Visible="false">
                            </asp:DropDownList>
                        </li>
                        <li style="padding-top: 15px">
                           <span>PO/WO Type</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstPoWoType">
                            </asp:DropDownList>
                            
                        </li>
                        <li style="padding-top: 15px">
                            
                        </li>
                    </ul>
                </div>


                <div class="mobile_Savebtndiv" style="margin-top: 20px !important">
                    <asp:LinkButton ID="mobile_btnSave" runat="server" Text="Submit" ToolTip="Search" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click">Search </asp:LinkButton>
                    <asp:LinkButton ID="mobile_btnBack" runat="server" Text="Clear Search" OnClick="mobile_btnBack_Click" ToolTip="Clear Search" class="trvl_Savebtndiv">Clear Search</asp:LinkButton>
                </div>

                <%--<div class="manage_grid" style="overflow: scroll; width: 100%; padding-top: 20px; margin-bottom: 30px;">--%>

                    <asp:Label runat="server" ID="RecordCount" Style="color: red; font-size: 14px;"></asp:Label>

                    <asp:GridView ID="gvMngTravelRqstList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                        DataKeyNames="POID" CellPadding="3" AutoGenerateColumns="False" Width="120%"  EditRowStyle-Wrap="false" PageSize="10" AllowPaging="True" OnPageIndexChanging="gvMngTravelRqstList_PageIndexChanging">
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                        <PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" />
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                        <PagerStyle HorizontalAlign = "Right" CssClass = "paging" />
                        <Columns> 

                             <asp:TemplateField  HeaderText="View">
                                <ItemTemplate>
                                    <%--<asp:LinkButton ID="lnkLeaveDetails" runat="server" Text='View' OnClick="lnkLeaveDetails_Click" >
                            </asp:LinkButton>--%>
                                    <asp:ImageButton ID="lnkLeaveDetails" ToolTip="View" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkLeaveDetails_Click" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" Width="4%" />
                            </asp:TemplateField>

                                 <asp:BoundField HeaderText="PO/ WO Created By."
                                DataField="powoCreatedBy"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="18%" ItemStyle-BorderColor="Navy" />

                            <asp:BoundField HeaderText="PO/ WO No."
                                DataField="PONumber"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />                         

                            <asp:BoundField HeaderText="PO/ WO Title"
                                DataField="POTitle"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="2%" ItemStyle-BorderColor="Navy" Visible="false" />

                            <asp:BoundField HeaderText="PO/ WO Type"
                                DataField="POType"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="13%" ItemStyle-BorderColor="Navy" />

                              <asp:BoundField HeaderText="PO/ WO Amount (Without GST)"
                                DataField="POWO_T_BaseAmt"
                                ItemStyle-HorizontalAlign="Right"
                                HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />

                            <asp:BoundField HeaderText="PO/ WO Amount (With GST)"
                                DataField="POWOAmt"
                                ItemStyle-HorizontalAlign="Right"
                                HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" /> 
                            
                                <asp:BoundField HeaderText="PO/ WO Date"
                                DataField="PODate"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />

                            <asp:BoundField HeaderText="Vendor Name"
                                DataField="Name"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="20%" ItemStyle-BorderColor="Navy" />


                            <asp:BoundField HeaderText="Cost Center"
                                DataField="costCenter"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />


                            <asp:BoundField HeaderText="Deparment"
                                DataField="Project_Name"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="15%" ItemStyle-BorderColor="Navy"  Visible="false"/>

                            <asp:BoundField HeaderText="Status"
                                DataField="Request_status"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="6%" ItemStyle-BorderColor="Navy" />
                        </Columns>
                    </asp:GridView>

                <br /><br /><br />
              <%--  </div>--%>


                <%--'<%#"~/Details.aspx?ID="+Eval("ID") %>'>--%>



                <div class="edit-contact">
                    <%-- <div class="editprofile" style="text-align: center; border: none;" id="divmsg" runat="server" visible="false">
                        <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: green; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                    </div>--%>
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" OnClick="lnkcont_Click"></asp:LinkButton>
                        </div>
                    </div>

                </div>
                <asp:HiddenField ID="hdnPOWOID" runat="server" />
                <asp:HiddenField ID="hdnMilestoneId" runat="server" />
                <asp:HiddenField ID="hdnInvoiceId" runat="server" />
                <asp:HiddenField ID="hdnEmpCode" runat="server" />

            </div>
        </div>
    </div>

<%--    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />
 --%>
    <script src="../js/freeze/jquery-ui.min.js"></script>
	<script src="../js/freeze/gridviewScroll.min.js"></script>

    <script type="text/javascript">

        $(document).ready(function ()
        {
            $(".DropdownListSearch").select2();

            	$('#MainContent_gvMngTravelRqstList').gridviewScroll({
                width: 1070,
                height: 600,
                freezesize: 5, // Freeze Number of Columns.
                headerrowcount: 1, //Freeze Number of Rows with Header.
			});

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

    </script>
</asp:Content>


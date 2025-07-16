<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="AppraisalProcess_Status.aspx.cs" Inherits="AppraisalProcess_Status" %>

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
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
    <script src="../js/freeze/jquery-1.11.0.min.js"></script>
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />
    <script type="text/javascript">
        var deprt;
        $(document).ready(function () {
            $("#MainContent_txtcity").autocomplete("<%=ReturnUrl("sitepathmain") %>Search_City.ashx", {
                inputClass: "ac_input",
                resultsClass: "ac_results",
                loadingClass: "ac_loading",
                minChars: 1,
                delay: 1,
                matchCase: false,
                matchSubset: false,
                matchContains: false,
                cacheLength: 0,
                max: 12,
                mustMatch: false,
                extraParams: {},
                selectFirst: false,
                formatItem: function (row) { return row[0] },
                formatMatch: null,
                autoFill: false,
                width: 0,
                multiple: false,
                scroll: false,
                scrollHeight: 180,
                success: function (data) {
                    response($.map(data, function (item) {
                        return {

                        }
                    }));
                },
                context: this
            });

            $("#MainContent_txtloc").autocomplete("<%=ReturnUrl("sitepathmain") %>Search_location.ashx", {
                inputClass: "ac_input",
                resultsClass: "ac_results",
                loadingClass: "ac_loading",
                minChars: 1,
                delay: 1,
                matchCase: false,
                matchSubset: false,
                matchContains: false,
                cacheLength: 0,
                max: 12,
                mustMatch: false,
                extraParams: {},
                selectFirst: false,
                formatItem: function (row) { return row[0] },
                formatMatch: null,
                autoFill: false,
                width: 0,
                multiple: false,
                scroll: false,
                scrollHeight: 180,
                success: function (data) {
                    response($.map(data, function (item) {
                        return {

                        }
                    }));
                },
                context: this
            });


            $("#MainContent_txtsubdept").autocomplete("<%=ReturnUrl("sitepathmain") %>Search_subdepartment.ashx?d=" + deprt, {
                inputClass: "ac_input",
                resultsClass: "ac_results",
                loadingClass: "ac_loading",
                minChars: 1,
                delay: 1,
                matchCase: false,
                matchSubset: false,
                matchContains: false,
                cacheLength: 0,
                max: 12,
                mustMatch: false,
                extraParams: { d: deprt },
                selectFirst: false,
                formatItem: function (row) { return row[0] },
                formatMatch: null,
                autoFill: false,
                width: 0,
                multiple: false,
                scroll: false,
                scrollHeight: 180,
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                        }
                    }));
                },

                context: this
            });
        });
    </script>
    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                         <asp:Label ID="lblheading" runat="server" Text="Appraisal Process Status"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>

                </div>

                <span>
                     <a href="Appraisalindex.aspx" class="aaaa">Appraisal Index</a>

                </span>
                <div class="edit-contact">
                    <ul id="editform" runat="server">
                        <li style="padding-top: 30px">
                            <span>Location Name.</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" ID="lstLocationName" CssClass="DropdownListSearch">
                            </asp:DropDownList>
                        </li>
                        <li style="padding-top: 30px">
                             <span>Department Name</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" ID="lstDepartmentName" CssClass="DropdownListSearch" Width="98%">
                            </asp:DropDownList>
                           
                        </li>
                        <li style="padding-top: 30px">
                             <span>Employee Name</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstEmployeeName">
                            </asp:DropDownList>
                        </li>

                        <li style="padding-top: 15px;" >
                             <span>Appraisal Status</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstStatus">
                            </asp:DropDownList> 
                            
                        </li>
                        <li style="padding-top: 15px;display:none">
                           <span>Payment Request Date</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstpaymentRequestDate">
                            </asp:DropDownList>
                        </li>
                        <li style="padding-top: 15px;display:none">
                               <span>Cost Center</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstCostCentre">
                            </asp:DropDownList>
                        </li>
                 
                        <li style="padding-top: 15px;display:none">
                               <span>Payment Request No.</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstPaymentRequestNo">
                            </asp:DropDownList>
                        </li>

                        <li style="padding-top: 15px;display:none">
                              <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstPOWODate" Visible="false">
                            </asp:DropDownList>
                        </li>

                    </ul>
                </div> 


                <div class="trvl_Savebtndiv">
                    <asp:LinkButton ID="trvl_btnSave" runat="server" Text="Submit" ToolTip="Search" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click">Search</asp:LinkButton>

                    <asp:LinkButton ID="btnCorrection" runat="server" Text="Clear Search" ToolTip="Clear Search" CssClass="Savebtnsve" OnClick="mobile_btnBack_Click">Clear Search</asp:LinkButton>

                    <asp:LinkButton ID="btnCancel" runat="server" Text="Export To Excel" ToolTip="Export To Excel" CssClass="Savebtnsve" OnClick="ExportToExcel" Visible="false">Export To Excel</asp:LinkButton>
                </div>
                <div class="manage_grid">
                    <asp:Label runat="server" ID="RecordCount" Style="color: red; font-size: 14px;"></asp:Label>
                    <center>
                        <asp:GridView ID="gvMngTravelRqstList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                            DataKeyNames="assess_id" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false" PageSize="20" AllowPaging="True" OnPageIndexChanging="gvMngTravelRqstList_PageIndexChanging">
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                            <PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" />
                            <RowStyle ForeColor="#000066" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                            <PagerStyle HorizontalAlign="Right" CssClass="paging" />

                            <Columns>
                           <%--  <asp:BoundField HeaderText="Sr No."
                                DataField="Srno"
                               ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="left"
                                ItemStyle-Width="2%" ItemStyle-BorderColor="Navy"/>

                             <asp:BoundField HeaderText="Year"
                                DataField="pyear"
                                ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="left"
                                ItemStyle-Width="4%" ItemStyle-BorderColor="Navy"/>--%>

                              <asp:BoundField HeaderText="Deaprtment"
                                DataField="Department"
                               ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="left"
                                ItemStyle-Width="8%" ItemStyle-BorderColor="Navy"/>

                            <asp:BoundField HeaderText="Location"
                                DataField="Location_Code"
                               ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="left"
                                ItemStyle-Width="8%" ItemStyle-BorderColor="Navy"/>

                            <asp:BoundField HeaderText="Employee Code"
                                DataField="Emp_code"
                                 ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="left"
                                ItemStyle-Width="8%" ItemStyle-BorderColor="Navy"/>

                              <asp:BoundField HeaderText="Band"
                                DataField="grade"
                                 ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="left"
                                ItemStyle-Width="8%" ItemStyle-BorderColor="Navy"/>

                             

                              <asp:BoundField HeaderText="DOJ"
                                DataField="emp_doj"
                                 ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="left"
                                ItemStyle-Width="8%" ItemStyle-BorderColor="Navy"/>

                           <asp:BoundField HeaderText="DOR"
                                DataField="Resig_Date"
                                 ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="left"
                                ItemStyle-Width="8%" ItemStyle-BorderColor="Navy"/>

                            <asp:BoundField HeaderText="Reviewee"
                                DataField="Reviewee"
                                 ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="left"
                                ItemStyle-Width="13%" ItemStyle-BorderColor="Navy"/>
                            
                            <asp:BoundField HeaderText="Reviewer (N1)"
                                DataField="Reviewer"
                                 ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="left"
                                ItemStyle-Width="13%" ItemStyle-BorderColor="Navy"/>
                            
                            <asp:BoundField HeaderText="FinalReviewer (N2)"
                                DataField="FinalReviewer"
                                  ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="left"
                                ItemStyle-Width="13%" ItemStyle-BorderColor="Navy"/>

                              <asp:BoundField HeaderText="Additional Reviewer"
                                DataField="AdditionalReviewer"
                                  ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="left"
                                ItemStyle-Width="8%" ItemStyle-BorderColor="Navy"/> 

                             <asp:BoundField HeaderText="Appraisal Status"
                                DataField="Appr_Status_Name"
                                  ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="left"
                                ItemStyle-Width="13%" ItemStyle-BorderColor="Navy"/>  
                        </Columns>
                        </asp:GridView>
                    </center>

                </div>
                <br />
                <br />
                <br />
                <asp:HiddenField ID="hdnInboxType" runat="server" />
                <asp:HiddenField ID="hdnEmpCode" runat="server" />
                <asp:HiddenField ID="hdnRecruitment_ReqID" runat="server" />
                <asp:HiddenField ID="FilePath" runat="server" />
                <asp:HiddenField ID="hdnPOID" runat="server" />

            </div>
        </div>
    </div>


    <script src="../js/freeze/jquery-ui.min.js"></script>
    <script src="../js/freeze/gridviewScroll.min.js"></script>
    <script type="text/javascript">      
        $(document).ready(function () {
            $(".DropdownListSearch").select2();
            //$('#MainContent_gvMngTravelRqstList').gridviewScroll({
            //    width: 1060,
            //    height: 1000,
            //    freezesize: 4, // Freeze Number of Columns.
            //    headerrowcount: 1, //Freeze Number of Rows with Header.
            //});

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
        $('.number').keypress(function (event) {
            if ((event.which != 46 || $(this).val().indexOf('.') != -1) &&
                ((event.which < 48 || event.which > 57) &&
                    (event.which != 0 && event.which != 8))) {
                event.preventDefault();
            }
            var text = $(this).val();
            if ((text.indexOf('.') != -1) &&
                (text.substring(text.indexOf('.')).length > 2) &&
                (event.which != 0 && event.which != 8) &&
                ($(this)[0].selectionStart >= text.length - 2)) {
                event.preventDefault();
            }
        });

    </script>
</asp:Content>



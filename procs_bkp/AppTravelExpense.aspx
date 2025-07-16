<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" EnableViewState="true"
    MaintainScrollPositionOnPostback="true" CodeFile="AppTravelExpense.aspx.cs" Inherits="AppTravelExpense" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <%--<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <%-- <link href="<%=ReturnUrl("css") %>calendar/calendar.css" rel="stylesheet" type="text/css" />
    <link href="sampleform_css.css" rel="stylesheet" type="text/css" />--%>
    <%--<ajaxToolkit:ToolkitScriptManager ID="srcp" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
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

        #MainContent_lstApprover, #MainContent_lstIntermediate {
            padding: 0 0 33px 0 !important;
            /*overflow: unset;*/
        }

        .Dropdown {
            border-bottom: 2px solid #cccccc;
            /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
            background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;
            cursor: default;
        }

        .grayDropdown {
            border-bottom: 2px solid #cccccc;
            /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
            background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;
            background-color: #ebebe4;
        }


        .lblNewJoineeExp_req {
            color: darkgreen;
        }

        .txt {
            padding-top: 10px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
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

            $('.txt_Amt_Release_By_Acc_PayByCom').keypress(function (e) {
                alert('test');
                var charCode = (e.which) ? e.which : event.keyCode

                if (String.fromCharCode(charCode).match(/[^0-9]/g))

                    return false;

            });
        });
    </script>
    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Travel Expense Voucher"></asp:Label>
                    </span>
                </div>
                <%--<a href="travelindex.aspx" class="aaab">Travel Index</a>--%>
                <span>
                    <a href="travelindex.aspx" class="aaaa">Travel Home</a>
                </span>

                <div class="edit-contact">
                    <%--                    <div class="editprofile" style="text-align: center; border: none;" id="divmsg" runat="server" visible="false">
                        <asp:Label runat="server" ID="lblmessage" Visible="true" Style="color: green; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                    </div>--%>
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" OnClick="lnkcont_Click"></asp:LinkButton>
                        </div>
                    </div>
                    <div class="editprofile btndiv">
                        <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                    </div>
                    <ul id="editform" runat="server" visible="false">

                        <li class="trvl_Reason">
                            <asp:CheckBox ID="chkIsNewJoinExp_req" runat="server" Text="New Joinee Travel Expense Request" Visible="false" Enabled="false" />
                            <asp:Label ID="lblNewJoineeExp_req" runat="server" Text="New Joinee Travel Expense Request" CssClass="lblNewJoineeExp_req" Visible="false">
                               
                            </asp:Label>

                        </li>

                        <li class="trvl_Reason"></li>
                        <li class="trvl_Reason"></li>

                        <li class="trvl_Reason"></li>
                        <li class="trvl_Reason"></li>
                        <li class="trvl_Reason"></li>

                        <li class="trvl_Reason">
                            <span>Employee Code</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="True" Enabled="false"> </asp:TextBox>
                        </li>
                        <li class="trvl_Reason">
                            <span>Employee Name </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtEmpName" runat="server" Visible="True" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="trvl_Reason">
                            <span>Band </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtGrade" runat="server" Visible="True" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="trvl_type">
                            <span>Travel Type</span>&nbsp;&nbsp;<span style="color: red">*</span><br />

                            <asp:TextBox AutoComplete="off" ID="txtTriptype" runat="server" CssClass="grayDropdown" Enabled="false"></asp:TextBox>
                            <asp:Panel ID="Panel3" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstTripType" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true">
                                            <%--<asp:ListItem>Domestic</asp:ListItem>
                                            <asp:ListItem>International</asp:ListItem>--%>
                                        </asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender2" PopupControlID="Panel3" TargetControlID="txtTriptype"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>
                            <%--   <asp:UpdatePanel ID="Upl_leavetype" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlTripType" runat="server" OnSelectedIndexChanged="ddlTripType_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlTripType" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>--%>

                        </li>

                        <li class="trvl_date">
                            <span>From </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />

                            <asp:TextBox AutoComplete="off" ID="txtFromdate" runat="server" AutoPostBack="True" Enabled="false"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtFromdate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>

                            <%--  <asp:TextBox AutoComplete="off" ID="txtsadate" runat="server" CssClass="txtdatepicker" placeholder="Start Date" OnTextChanged ="txtsadate_TextChanged" AutoPostBack ="true" ></asp:TextBox>
                                <span class="texticon tooltip" title="Select Start Date. Date should be MM/DD/YYYY format."><i class="fa fa-calendar" aria-hidden="true"></i></span>
                                <asp:Label ID="lblsdate" runat="server" Text="" Font-Size="15px"></asp:Label>--%>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="formerror" ControlToValidate="txtsadate" SetFocusOnError="True" ErrorMessage="Please enter start date" ValidationGroup="validate"></asp:RequiredFieldValidator>--%>

                        </li>
                        <li class="trvl_date">
                            <span>To</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtToDate" runat="server" AutoPostBack="True" Enabled="false"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>

                        <li class="trvl_Currency">
                            <span>Project Name </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_ProjectName" runat="server" Enabled="false"></asp:TextBox>
                        </li>

                        <li class="trvl_Currency">
                            <span>Department Name </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_DeptName" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="trvl_Reason">
                            <span>Reason for Travel </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtReason" runat="server" MaxLength="100" Enabled="false"></asp:TextBox>
                        </li>

                        <li class="trvL_detail">
                            <asp:LinkButton ID="btnTra_Details" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" Enabled="false" Visible="false"></asp:LinkButton>
                            <span>Travel Details</span>
                        </li>

                        <li class="trvl_grid">

                            <div>

                                <asp:GridView ID="dgTravelRequest" runat="server" Enabled="false" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false">
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
                                        <asp:BoundField HeaderText="Mode"
                                            DataField="trip_mode"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Dep Date"
                                            DataField="departure_date"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Dep Place"
                                            DataField="departure_place"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Arr Date"
                                            DataField="arrival_date"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Arr Place"
                                            DataField="arrival_place"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />
                                        <%-- <asp:BoundField HeaderText="Through Travel Desk"
                                            DataField="travel_through_desk"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="1%" />--%>
                                        <asp:TemplateField HeaderText="Through Travel Desk" ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkCOS" Visible="false" runat="server" Checked='<%#Convert.ToBoolean(Eval("tt")) %>' />
                                                <asp:Label ID="lblchkcos" runat="server" Text='<%#Eval("travel_through_desk")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Status" Visible="false"
                                            DataField="lbookedStatus"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="1%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Deviation" Visible="false"
                                            DataField="deviation"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="1%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Remarks"
                                            DataField="trip_remarks"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="25%" ItemStyle-BorderColor="Navy" />


                                        <%--            <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkTravelDetailsEdit" runat="server" Text='View' OnClick="lnkTravelDetailsEdit_Click">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>

                            </div>

                        </li>


                        <li class="trvl_Advances">
                            <span id="lbl_adv" visible="false" runat="server">Advances Taken: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" Visible="false" ID="txtAdvance" runat="server" MaxLength="100" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="trvl_Currency">
                            <span id="lbl_cur" runat="server" visible="false">Currency Required:</span>
                            <br />
                            <asp:TextBox AutoComplete="off" Visible="false" ID="txtreqCur" runat="server" MaxLength="100" Enabled="false"></asp:TextBox>
                        </li>
                        <li></li>
                        <li class="trvl_Accomodation">
                            <asp:LinkButton ID="trvl_accmo_btn" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" Visible="false"></asp:LinkButton>
                            <span>Accommodation: </span>
                        </li>

                        <li class="trvl_grid">

                            <div>

                                <asp:GridView ID="dgAccomodation" runat="server" Enabled="false" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false">
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
                                        <asp:BoundField HeaderText="From Date"
                                            DataField="From Date"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="To Date"
                                            DataField="To Date"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Accomodation Type"
                                            DataField="Accomodation_type"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="20%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Location"
                                            DataField="Location"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                                        <%-- <asp:BoundField HeaderText="Through COS"
                                            DataField="Through COS"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="2%" />--%>

                                        <asp:TemplateField HeaderText="Through COS" ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkCOS" runat="server" Visible="false" Checked='<%#Convert.ToBoolean(Eval("tt")) %>' />
                                                <asp:Label ID="lblchkcos" runat="server" Text='<%#Eval("travel_through_desk")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Status" Visible="false"
                                            DataField="lbookedStatus"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="1%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Deviation" Visible="false"
                                            DataField="deviation"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="1%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Remarks"
                                            DataField="local_accomodation_remarks"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="30%" ItemStyle-BorderColor="Navy" />
                                        <%--                <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkAccomodationdit" runat="server" Text='View' OnClick="lnkAccomodationdit_Click">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>

                            </div>

                        </li>

                        <li class="trvl_local">
                            <br />
                            <asp:LinkButton ID="trvl_localbtn" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" Visible="false"></asp:LinkButton>
                            <span>Local Travel: </span>
                        </li>

                        <li class="trvl_grid">

                            <div>

                                <asp:GridView ID="dgLocalTravel" runat="server" Enabled="false" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false">
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
                                        <asp:BoundField HeaderText="From Date"
                                            DataField="From Date"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="To Date"
                                            DataField="To Date"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Mode of Travel"
                                            DataField="trip_mode"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="20%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Location"
                                            DataField="Location"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                                        <%--  <asp:BoundField HeaderText="Through COS"
                                            DataField="Through COS"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="2%" />--%>

                                        <asp:TemplateField HeaderText="Through COS" ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkCOS" runat="server" Visible="false" Checked='<%#Convert.ToBoolean(Eval("tt")) %>' />
                                                <asp:Label ID="lblChkCOS" runat="server" Text='<%#Eval("Through_COS")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Status" Visible="false"
                                            DataField="lbookedStatus"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="1%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Deviation" Visible="false"
                                            DataField="deviation"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="1%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Remarks"
                                            DataField="remarks"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="30%" ItemStyle-BorderColor="Navy" />
                                        <%--                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkLocalTravleEdit" runat="server" Text='View' OnClick="lnkLocalTravleEdit_Click">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>

                            </div>

                        </li>
                        <li class="trvl_local">
                            <br />
                            <asp:LinkButton ID="LinkButton1" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" Visible="false"></asp:LinkButton>
                            <span>Other Expenses: </span>
                        </li>
                        <li class="trvl_grid">
                            <div>
                                <asp:GridView ID="gvOth" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid"
                                    BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                    DataKeyNames="trip_id,exp_id,exp_sr_no,trip_details">
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
                                        <asp:BoundField HeaderText="Date"
                                            DataField="trip_frm_date"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="20%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="To Date"
                                            DataField="trip_frm_date"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="1%" ItemStyle-BorderColor="Navy" Visible="false" />

                                        <asp:BoundField HeaderText="Description"
                                            DataField="Descr"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="40%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Remarks"
                                            DataField="exp_remarks"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="40%" ItemStyle-BorderColor="Navy" />
                                    </Columns>
                                </asp:GridView>

                            </div>
                        </li>
                        <li class="trvl_local">
                            <br />
                            <span id="Span1" runat="server" style="font-weight: bold">Expenses Summary: </span>
                            <br />
                            <br />

                            <asp:LinkButton ID="lnkbtn_expdtls" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" Visible="false"></asp:LinkButton>
                            <%--<span>Other Expenses : </span>--%>
                        </li>
                        <li>

                        </li>
                        <li class="trvl_grid">
                                                        <div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Label runat="server" ID="lblgrdmsg" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                                </div>
                            <div>

                                <asp:GridView ID="gvexpensdtls" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                    DataKeyNames="trip_id,exp_id,exp_sr_no,trip_details,descr" OnRowDataBound="gvexpensdtls_RowDataBound">
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
                                        <asp:BoundField HeaderText="Date"
                                            DataField="trip_frm_date"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="17%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="To Date"
                                            DataField="trip_frm_date"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="0%" ItemStyle-BorderColor="Navy" Visible="false" />

                                        <asp:BoundField HeaderText="Place"
                                            DataField="trip_destination"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Details"
                                            DataField="exp_name"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Description"
                                            DataField="Descr"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="19%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Paid by Company"
                                            DataField="paid_by_comp"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Paid by Employee"
                                            DataField="paid_emp"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Total Amount Claimed"
                                            DataField="totamt"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
                                        <asp:TemplateField HeaderText="Paid by Company (Amt Release By Account)" HeaderStyle-Width="40%" ControlStyle-Width="30%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txt_Amt_Release_By_Acc_PayByCom" runat="server" Text='<%#Eval("paid_by_comp")%>' OnTextChanged="txt_Amt_Release_By_Acc_PayByCom_TextChanged"
                                                            AutoPostBack="true" Width="70px" AutoComplete="off" />
                                                        <asp:Label Text='<%#Eval("paid_by_comp")%>' Visible="false" ID="lblAccPaid_by_comp" runat="server" />
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txt_Amt_Release_By_Acc_PayByCom" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Paid by Employee (Amt Release By Account)" HeaderStyle-Width="40%" ControlStyle-Width="30%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txt_Amt_Release_By_Acc_PayByEmp" runat="server" Text='<%#Eval("paid_emp")%>' OnTextChanged="txt_Amt_Release_By_Acc_PayByEmp_TextChanged"
                                                            AutoPostBack="true" Width="70px" AutoComplete="off" />
                                                        <asp:Label Text='<%#Eval("paid_emp")%>' Visible="false" ID="lblAccPaid_by_Emp" runat="server" />
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txt_Amt_Release_By_Acc_PayByEmp" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Account Remarks" HeaderStyle-Width="40%" ControlStyle-Width="30%" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtAccRemarks" runat="server" OnTextChanged="txtAccRemarks_TextChanged"
                                                            AutoPostBack="true" Width="70px" AutoComplete="off" MaxLength="200"/>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtAccRemarks" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                <%--                                                <asp:Label Text='<%#Eval("exp_id")%>' ID="lblexp_id" runat="server" Visible="false" />
                                                <asp:Label Text='<%#Eval("exp_sr_no")%>' ID="lblexp_sr_no" runat="server" Visible="false" />
                                                <asp:Label Text='<%#Eval("trip_details")%>' ID="lbltrip_details" runat="server" Visible="false" />
                                                <asp:Label Text='<%#Eval("descr")%>' ID="lbldescr" runat="server" Visible="false" />--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField HeaderText="Eligibility" Visible="false"
                                            DataField="eligibility"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="0%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Diff Amt" Visible="false"
                                            DataField="diff"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="0%" ItemStyle-BorderColor="Navy" />


                                    </Columns>

                                </asp:GridView>

                            </div>
                        </li>


                        <li class="trvl_local">
                            <span>Total Amount Claimed</span>
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:TextBox AutoComplete="off" ID="txtTotAmtClaimed" runat="server" MaxLength="10" Enabled="False"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <%--<span>Daily Halting Allowance (INR) </span>--%>
                            <asp:TextBox AutoComplete="off" Visible="false" ID="txtdailyhaltingallowance" runat="server" MaxLength="10" Enabled="False"></asp:TextBox>
                        </li>

                        <li class="trvl_local">
                            <span>Advance Taken</span>
                            <asp:TextBox AutoComplete="off" ID="txtLessAdvTaken" runat="server" MaxLength="10" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_local">
                            <span>Net Payable to Employee</span>
                            <asp:TextBox AutoComplete="off" ID="txtnetPaybltoEmp" runat="server" MaxLength="10" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_local">
                            <span>Net Payable to Company</span>
                            <asp:TextBox AutoComplete="off" ID="txtnetPaybltoComp" runat="server" MaxLength="10" Enabled="False"></asp:TextBox>
                        </li>



                        <li class="trvl_local">
                            <span>Remarks</span>
                            <asp:TextBox AutoComplete="off" ID="txtReasonDeviation" runat="server" MaxLength="100" Enabled="False"></asp:TextBox>
                        </li>

                        <li class="trvl_Reason">
                            <span id="SpnComments" runat="server">Comment </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtComments" runat="server" MaxLength="200"></asp:TextBox>
                        </li>

                        <li class="trvl_Reason">
                            <asp:CheckBox ID="chk_exception" runat="server" AutoPostBack="true" Text="Is Exception" OnCheckedChanged="chk_exception_CheckedChanged" />
                            <%--<asp:TextBox AutoComplete="off" ID="TextBox1" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>--%>
                        </li>
                        <li class="trvl_local">
                            <span id="SpnAccountAmount" runat="server" visible="False">Amount Released by Accounts </span>
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:TextBox AutoComplete="off" ID="txtAccountAmount" Enabled="false" runat="server" MaxLength="10" Visible="False"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </li>
                        <li class="trvl_Reason">
                            <span id="SpnAccountRemark" runat="server" visible="False">Remarks by Accounts *</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtAccountRemark" runat="server" MaxLength="200" Visible="False"></asp:TextBox>
                        </li>

                        <li class="trvl_local">
                            <span id="idspnuploadfile" runat="server" visible="false">Uploaded File</span>
                            <asp:FileUpload ID="ploadexpfile" runat="server" Enabled="false" Visible="false"></asp:FileUpload>
                            <asp:GridView ID="gvuploadedFiles" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                DataKeyNames="fileid">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                <PagerStyle ForeColor="#000066" HorizontalAlign="left" BackColor="White" />
                                <RowStyle ForeColor="#000066" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                                <Columns>
                                    <%--  <asp:BoundField HeaderText="Uploaded Files"
                                            DataField="filename"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="30%" />--%>

                                    <asp:TemplateField HeaderText="Uploaded Files">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkviewfile" runat="server" OnClientClick=<%# "DownloadFile('" + Eval("filename") + "')" %> Text='<%# Eval("filename") %>'>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                            <br />

                        </li>
                        <li></li>
                        <li></li>
                        <li class="trvl_Approver">
                            <span>Approver </span>
                            <br />
                            <asp:ListBox ID="lstApprover" runat="server" Enabled="false"></asp:ListBox>
                            <%--style="overflow-x:auto;"--%>
                        </li>

                        <li class="trvl_inter" style="display: none;">
                            <span>For Information To </span>
                            <br />
                            <asp:ListBox ID="lstIntermediate" runat="server"></asp:ListBox>
                        </li>

                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="trvl_Savebtndiv">
        <asp:LinkButton ID="trvl_btnSave" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve">Submit</asp:LinkButton>

    </div>
    <div class="trvl_Savebtndiv">

        <span>
            <asp:LinkButton ID="btnApprove" runat="server" Text="Approve" ToolTip="Approve" CssClass="Savebtnsve" OnClientClick="return ApproverMultiClick();" OnClick="btnApprove_Click">Approve</asp:LinkButton>

            <asp:LinkButton ID="btnApprove_ACC" runat="server" Text="Submit" Visible="false" ToolTip="Submit" CssClass="Savebtnsve" OnClientClick="return ACC_ApproverMultiClick();" OnClick="btnApprove_Click">Submit</asp:LinkButton>

        </span>

        <span>
            <asp:LinkButton ID="btnCorrection" runat="server" Text="Correction" ToolTip="" CssClass="Savebtnsve" OnClientClick="return SendforCorrectionMultiClick();" OnClick="btnCorrection_Click">Send for Modification</asp:LinkButton>
        </span>

        <span>
            <asp:LinkButton ID="btnReject" Visible="false" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClientClick="return RejectMultiClick();" OnClick="btnReject_Click">Reject</asp:LinkButton>
        </span>

        <span>
            <asp:LinkButton ID="btnBack" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="btnBack_Click">Back</asp:LinkButton>
        </span>
        <span>
            <asp:LinkButton ID="mobile_btnPrintPV" runat="server" Text="Print Voucher" ToolTip="Print Voucher" CssClass="Savebtnsve" OnClick="mobile_btnPrintPV_Click">Print Voucher</asp:LinkButton>
        </span>
        <%-- Following Popup for Approved Travel Expenses Request --%>
        <%-- <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" DisplayModalPopupID="mpe_Appr" TargetControlID="btnApprove">
        </ajaxToolkit:ConfirmButtonExtender>
        <ajaxToolkit:ModalPopupExtender ID="mpe_Appr" runat="server" PopupControlID="pnlPopup_Appr" TargetControlID="btnApprove" OkControlID = "btnYes_Appr"
            CancelControlID="btnNo_Appr" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup_Appr" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Confirmation
            </div>
            <div class="body">
                Do you want to Approve ?
            </div>
            <div class="footer" align="right">
                <asp:Button ID="btnNo_Appr" runat="server" Text="No" />
                <asp:Button ID="btnYes_Appr" runat="server" Text="Yes" />                
            </div>
        </asp:Panel>--%>


        <%-- Following Popup for Submit Travel Expenses Request For ACC --%>
        <%--  <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender4" runat="server" DisplayModalPopupID="mpe_Appr_ACC" TargetControlID="btnApprove_ACC">
        </ajaxToolkit:ConfirmButtonExtender>
        <ajaxToolkit:ModalPopupExtender ID="mpe_Appr_ACC" runat="server" PopupControlID="pnlPopup_Appr_ACC"  TargetControlID="btnApprove_ACC" OkControlID = "btnYes_Appr_ACC"
            CancelControlID="btnNo_Appr_ACC" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup_Appr_ACC" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Confirmation
            </div>
            <div class="body">
                Do you want to Submit ?
            </div>
            <div class="footer" align="right">
                <asp:Button ID="btnNo_Appr_ACC" runat="server" Text="No" />
                <asp:Button ID="btnYes_Appr_ACC" runat="server" Text="Yes" />                
            </div>
        </asp:Panel>--%>


        <%-- Following Popup for Send For Correction Travel Expenses Request --%>
        <%-- <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" DisplayModalPopupID="mpe_correction" TargetControlID="btnCorrection">
        </ajaxToolkit:ConfirmButtonExtender>
        <ajaxToolkit:ModalPopupExtender ID="mpe_correction" runat="server" PopupControlID="pnlPopup_correction" TargetControlID="btnCorrection" OkControlID = "btnYes_cr"
            CancelControlID="btnNo_cr" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup_correction" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Confirmation
            </div>
            <div class="body">
                Do you want to Send for Correction?
            </div>
            <div class="footer" align="right">
                <asp:Button ID="btnNo_cr" runat="server" Text="No" />
                <asp:Button ID="btnYes_cr" runat="server" Text="Yes" />                
            </div>
        </asp:Panel>--%>

        <%-- Following Popup for Reject Travel Expenses Request --%>
        <%-- <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" DisplayModalPopupID="mpe_reject" TargetControlID="btnReject">
        </ajaxToolkit:ConfirmButtonExtender>
        <ajaxToolkit:ModalPopupExtender ID="mpe_reject" runat="server" PopupControlID="pnlPopup_Reject" TargetControlID="btnReject" OkControlID = "btnYes_R"
            CancelControlID="btnNo_R" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup_Reject" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Confirmation
            </div>
            <div class="body">
                Do you want to Reject ?
            </div>
            <div class="footer" align="right">
                <asp:Button ID="btnNo_R" runat="server" Text="No" />
                <asp:Button ID="btnYes_R" runat="server" Text="Yes" />                
            </div>
        </asp:Panel>--%>
    </div>
    <%-- <div class="trvl_Savebtndiv">
        <asp:LinkButton ID="btnMod" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve">Modify</asp:LinkButton>
        <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="Savebtnsve">Cancel</asp:LinkButton>
        <asp:LinkButton ID="btnback_mng" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/ManageTravelRequest.aspx">Back</asp:LinkButton>
    </div>--%>


    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />

    <asp:HiddenField ID="FilePath" runat="server" />
    <asp:HiddenField ID="hdnvouno" runat="server" />
    <asp:HiddenField ID="hflEmpName" runat="server" />

    <asp:HiddenField ID="hdnApprId" runat="server" />

    <asp:HiddenField ID="hflEmpDesignation" runat="server" />

    <asp:HiddenField ID="hflEmpDepartment" runat="server" />

    <asp:HiddenField ID="hflloginApprEmail" runat="server" />

    <asp:HiddenField ID="hndloginempcode" runat="server" />

    <asp:HiddenField ID="hdnNextApprCode" runat="server" />

    <asp:HiddenField ID="hdnNextApprName" runat="server" />

    <asp:HiddenField ID="hdnNextApprEmail" runat="server" />

    <asp:HiddenField ID="hflGrade" runat="server" />

    <asp:HiddenField ID="hflapprcode" runat="server" />

    <asp:HiddenField ID="hdnDesk" runat="server" />

    <asp:HiddenField ID="hdnDestnation" runat="server" />

    <asp:HiddenField ID="hdnTripid" runat="server" />

    <asp:HiddenField ID="hdnAcctripid" runat="server" />

    <asp:HiddenField ID="hdnLcalTripid" runat="server" />

    <asp:HiddenField ID="hdnTraveltypeid" runat="server" />

    <asp:HiddenField ID="hdnDeptPlace" runat="server" />

    <asp:HiddenField ID="hdnTravelmode" runat="server" />

    <asp:HiddenField ID="hdnDeviation" runat="server" />

    <asp:HiddenField ID="hdnTrDetRequirements" runat="server" />

    <asp:HiddenField ID="hdnAccReq" runat="server" />

    <asp:HiddenField ID="hdnAccCOS" runat="server" />

    <asp:HiddenField ID="hdnlocaltrReq" runat="server" />

    <asp:HiddenField ID="hdnlocalTrCOS" runat="server" />

    <asp:HiddenField ID="hdnTravelConditionid" runat="server" />

    <asp:HiddenField ID="hdnNextApprId" runat="server" />

    <asp:HiddenField ID="hdnApprEmailaddress" runat="server" />
    <asp:HiddenField ID="hdnIntermediateEmail" runat="server" />
    <asp:HiddenField ID="hdnstaus" runat="server" />

    <asp:HiddenField ID="hdnEligible" runat="server" />
    <asp:HiddenField ID="hdnTrdays" runat="server" />
    <asp:HiddenField ID="hdnCurrentApprID" runat="server" />

    <asp:HiddenField ID="hdnReqEmailaddress" runat="server" />

    <asp:HiddenField ID="hdnExpid" runat="server" />
    <asp:HiddenField ID="hdnfrmdate_emial" runat="server" />
    <asp:HiddenField ID="hdntodate_emial" runat="server" />
    <asp:HiddenField ID="hdnActualTrvlDays" runat="server" />


    <asp:HiddenField ID="hdnApprovalACC_mail" runat="server" />
    <asp:HiddenField ID="hdnApprovalACC_Code" runat="server" />
    <asp:HiddenField ID="hdnApprovalACC_ID" runat="server" />
    <asp:HiddenField ID="hdnApprovalACC_Name" runat="server" />
    <asp:HiddenField ID="hdnisApproval_ACC_Status" runat="server" />
    <asp:HiddenField ID="hdnInboxType" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="hdncomp_code" runat="server" />
    <asp:HiddenField ID="hdndept_Id" runat="server" />
    <script type="text/javascript">

        function validateTripType(triptypeid) {
            if (triptypeid == "1") {
                document.getElementById("<%=txtreqCur.ClientID%>").value = "";
                //document.getElementById("<%=txtreqCur.ClientID%>").disabled = true;
                document.getElementById("<%=txtreqCur.ClientID%>").style.visibility = "hidden";
                document.getElementById("<%=lbl_cur.ClientID%>").style.visibility = "hidden";
            }
            else {
                document.getElementById("<%=txtreqCur.ClientID%>").value = "";
                //document.getElementById("<%=txtreqCur.ClientID%>").disabled = false;
                document.getElementById("<%=txtreqCur.ClientID%>").style.visibility = "visible";
                document.getElementById("<%=lbl_cur.ClientID%>").style.visibility = "visible";
                //document.getElementById("<%=txtreqCur.ClientID%>").style.backgroundColor = white;
            }

        }
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
            keychar = String.fromCharCode(keynum);
            return numcheck.test(keychar);
        }

        function ApproverMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=btnApprove.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        Approver_Confirm();
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        function ACC_ApproverMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=btnApprove_ACC.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        Approver_ACC_Confirm();
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }

        function SendforCorrectionMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=btnCorrection.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        SendforCorrection_Confirm();
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }

        function RejectMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=btnReject.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        Reject_Confirm();
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }


        function Approver_Confirm() {
            //Testing();
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Approve ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            //alert(confirm_value.value);
            //document.forms[0].appendChild(confirm_value);
            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return;

        }

        function Approver_ACC_Confirm() {
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
        function SendforCorrection_Confirm() {
            //Testing();
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Send for Correction ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            //alert(confirm_value.value);
            //document.forms[0].appendChild(confirm_value);
            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return;

        }

        function Reject_Confirm() {
            //Testing();
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Reject ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            //alert(confirm_value.value);
            //document.forms[0].appendChild(confirm_value);
            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return;

        }

        function onCharOnlyNumber(e) {
            var keynum;
            var keychar;
            var numcheck = /[0123456789.]/;

            if (window.event) {
                keynum = e.keyCode;
            }
            else if (e.which) {
                keynum = e.which;
            }
            keychar = String.fromCharCode(keynum);
            return numcheck.test(keychar);
        }
        function DownloadFile(file) {
            // alert(file);
            var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;

            //alert(localFilePath);
            window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
        }
    </script>
</asp:Content>

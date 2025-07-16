<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" EnableViewState="true"
    MaintainScrollPositionOnPostback="true" CodeFile="TravelRequest_exp.aspx.cs" Inherits="TravelRequest_exp" EnableSessionState="True" %>

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

        .MainContent_chkIsNewJoinExp_req {
            font-size: 12px;
        }
        #MainContent_btn_Draft {
            background: #3D1956;
            color: #febf39 !important;
            padding: 8px 18px;
            margin: 26px !important;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>

    <script src="../js/dist/jquery-3.2.1.min.js"></script>

    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <script type="text/javascript">      
        $(document).ready(function () {
            $(".DropdownListSearch").select2();
        });
    </script>


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
                        <asp:Label ID="lblheading" runat="server" Text="Travel Expense Voucher"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>
                <div>
                    <span>
                        <a href="travelindex.aspx" class="aaaa">Travel Home</a>
                    </span>
                </div>
                <div class="edit-contact">
                    <%--                    <div class="editprofile" style="text-align: center; border: none;" id="divmsg" runat="server" visible="false">
                        <asp:Label runat="server" ID="lblmessage" Visible="true" Style="color: green; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                    </div>--%>
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" Visible="false"></asp:LinkButton>
                        </div>
                    </div>

                    <ul id="editform" runat="server" visible="false">

                        <li class="trvl_date">
                            <asp:CheckBox ID="chkIsNewJoinExp_req" runat="server" CssClass="MainContent_chkIsNewJoinExp_req" Text="New Joinee Travel Expense Request" OnCheckedChanged="chkIsNewJoinExp_req_CheckedChanged" AutoPostBack="true" />
                        </li>

                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>


                        <li class="trvl_type">
                            <span>Travel Type</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                            <asp:DropDownList runat="server" ID="lstTripType" AutoPostBack="true" OnSelectedIndexChanged="lstTripType_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:TextBox AutoComplete="off" ID="txtTriptype" runat="server" CssClass="Dropdown" Visible="false"></asp:TextBox>

                            <%--                            <asp:Panel ID="Panel3" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstTripType" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstTripType_SelectedIndexChanged">
                                        </asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender2" PopupControlID="Panel3" TargetControlID="txtTriptype"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>--%>
                            <br />

                        </li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>

                        <li class="trvl_date">
                            <br />
                            <span>From </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />

                            <asp:TextBox AutoComplete="off" ID="txtFromdate" runat="server" AutoPostBack="false" OnTextChanged="txtFromdate_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtFromdate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>

                        </li>
                        <li class="trvl_date">
                            <br />
                            <span>To</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtToDate" runat="server" AutoPostBack="false" OnTextChanged="txtToDate_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>
                        <li class="trvl_Reason">
                            <span>Reason for Travel </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtReason" runat="server" MaxLength="100"></asp:TextBox>
                        </li>
                        <li class="trvl_Advances" id="litrvlAdvnce" runat="server">
                            <span id="spnadv" runat="server">Advances Taken: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtAdvance" runat="server" AutoPostBack="true" MaxLength="10"></asp:TextBox>
                        </li>
                        <li class="trvl_Currency" id="litrvlcurrency" runat="server" style="display: none;">
                            <span id="lbl_cur" runat="server" visible="false">Currency Required:</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtreqCur" runat="server" MaxLength="100" Visible="false" Enabled="false"></asp:TextBox>
                        </li>

                        <li class="trvl_Currency">
                            <span>Project Name </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <%--<asp:TextBox AutoComplete="off" ID="Txt_ProjectName" runat="server" AutoPostBack="true" 
                                OnTextChanged="Txt_ProjectName_TextChanged"></asp:TextBox>--%>
                            <asp:DropDownList ID="ddl_ProjectName" AutoPostBack="true" CssClass="DropdownListSearch" runat="server"
                                OnSelectedIndexChanged="ddl_ProjectName_SelectedIndexChanged">
                            </asp:DropDownList>



                        </li>

                        <li class="trvl_Currency">
                            <span>Department Name </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList ID="ddl_DeptName" AutoPostBack="true" OnSelectedIndexChanged="ddl_DeptName_SelectedIndexChanged" CssClass="DropdownListSearch" runat="server">
                            </asp:DropDownList>
                            <%-- <asp:TextBox AutoComplete="off" ID="Txt_DeptName" runat="server"></asp:TextBox>--%>
                        </li>
                        <li class="trvL_detail" id="litrvldetail" runat="server">
                            <asp:LinkButton ID="btnTra_Details" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" OnClick="btnTra_Details_Click"></asp:LinkButton>
                            <span id="spntrvldtls" runat="server">Travel Details</span>
                        </li>
                        <li></li>
                        <li></li>
                        <div id="DivTrvl" class="edit-contact" runat="server" visible="false">
                            <ul>
                                <li class="trvldetails_tripid" id="litripid" runat="server">
                                    <span id="spantripid" runat="server" visible="false">Trip ID</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtTripId" runat="server" Visible="false"></asp:TextBox>
                                </li>
                                <li class="trvldetails_type" id="litriptype" runat="server">
                                    <asp:TextBox AutoComplete="off" ID="txtTravelType" Visible="false" runat="server" CssClass="Dropdown" Enabled="false"></asp:TextBox>
                                </li>
                                <li>
                                    <asp:TextBox AutoComplete="off" ID="txtEmpCode_Trvl" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
                                </li>
                                <li>
                                    <span>Mode of Travel</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                    <asp:DropDownList runat="server" ID="lstTravelMode"
                                        OnSelectedIndexChanged="lstTravelMode_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:TextBox AutoComplete="off" ID="txtTravelMode" runat="server" CssClass="Dropdown" Visible="false"></asp:TextBox>
                                    <%--                                    <asp:Panel ID="Panel2" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:ListBox ID="lstTravelMode" runat="server" CssClass="taskparentclass2" AutoPostBack="true" OnSelectedIndexChanged="lstTravelMode_SelectedIndexChanged">
                                                </asp:ListBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>

                                    <ajaxToolkit:PopupControlExtender ID="PopupControlExtender3" PopupControlID="Panel2" TargetControlID="txtTravelMode"
                                        Position="Bottom" runat="server">
                                    </ajaxToolkit:PopupControlExtender>--%>
                                </li>

                                <li class="trvldetails_deviation">

                                    <%--                                    <span>Deviation</span>--%>
                                    <br />

                                    <asp:TextBox AutoComplete="off" ID="txtDeviation" Visible="false" Enabled="false" runat="server" CssClass="DropdownText"></asp:TextBox>

                                </li>
                                <li></li>
                                <li class="trvldetails_departuredate">
                                    <span>Departure Date </span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtFromdate_Trvl" runat="server"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" Format="dd/MM/yyyy" TargetControlID="txtFromdate_Trvl"
                                        runat="server">
                                    </ajaxToolkit:CalendarExtender>
                                </li>
                                <li class="trvldetails_place">

                                    <span>Place</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                    <asp:DropDownList ID="DDLOrigin" CssClass="DropdownListSearch" runat="server">
                                    </asp:DropDownList>


                                    <%-- <asp:TextBox AutoComplete="off" ID="txtOrigin" runat="server" placeholder="Select Place"></asp:TextBox>
                                    <asp:TextBox AutoComplete="off" ID="TextBox2" runat="server" Visible="false" CssClass="Dropdown"></asp:TextBox>--%>


                                    <%-- <asp:Panel ID="Panel4" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:ListBox ID="lstOrigin" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstOrigin_SelectedIndexChanged"></asp:ListBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>--%>

                                    <%--   <ajaxToolkit:PopupControlExtender ID="PopupControlExtender1" PopupControlID="Panel4" TargetControlID="TextBox2"
                                        Position="Bottom" runat="server">
                                    </ajaxToolkit:PopupControlExtender>

                                  <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchCities" MinimumPrefixLength="1"
                                    CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtOrigin"
                                    ID="AutoCompleteExtender1" runat="server" FirstRowSelected="true">
                                </ajaxToolkit:AutoCompleteExtender>--%>

                                </li>


                                <li class="trvldetails_departuredate">
                                    <span>Time (24 Hrs - HH:MM)</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />

                                    <asp:TextBox AutoComplete="off" ID="txtFromTime" runat="server" AutoPostBack="false" MaxLength="5"></asp:TextBox>

                                </li>
                                <li class="trvldetails_arrivaldate">
                                    <span>Arrival Date</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtToDate_Trvl" runat="server"></asp:TextBox>
                                    <!--OnTextChanged="txtToDate_TextChanged"-->
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" Format="dd/MM/yyyy" TargetControlID="txtToDate_Trvl"
                                        runat="server">
                                    </ajaxToolkit:CalendarExtender>
                                </li>

                                <li class="trvldetails_place2">

                                    <span>Place</span>&nbsp;&nbsp;<span style="color: red">*</span><br />

                                    <asp:DropDownList ID="DDLDestination" CssClass="DropdownListSearch" runat="server">
                                    </asp:DropDownList>

                                    <%-- <asp:TextBox AutoComplete="off" ID="txtDestination" runat="server" placeholder="Select Place">
                                    </asp:TextBox>--%>
                                    <%-- <asp:TextBox AutoComplete="off" ID="TextBox5" runat="server"  Visible="false" CssClass="Dropdown"></asp:TextBox>
                                    <asp:Panel ID="Panel5" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <asp:ListBox ID="lstDestination" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstDestination_SelectedIndexChanged"></asp:ListBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>

                                    <ajaxToolkit:PopupControlExtender ID="PopupControlExtender4" PopupControlID="Panel5" TargetControlID="TextBox5"
                                        Position="Bottom" runat="server">
                                    </ajaxToolkit:PopupControlExtender>

                                      <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchCities" MinimumPrefixLength="1"
                                    CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtDestination"
                                    ID="AutoCompleteExtender2" runat="server" FirstRowSelected="true">
                                </ajaxToolkit:AutoCompleteExtender>--%>


                                </li>


                                <li class="trvldetails_departuredate">
                                    <span>Time (24 Hrs - HH:MM)</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />

                                    <asp:TextBox AutoComplete="off" ID="txtToTime" runat="server" AutoPostBack="false" MaxLength="5"></asp:TextBox>
                                </li>
                                <li class="trvldetails_deviation" style="display: none;">
                                    <span>Eligibility</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtFoodEligibilty" runat="server" Visible="true"></asp:TextBox>

                                </li>

                                <li class="trvldetails_requirement">
                                    <span>Travel Fare: </span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtfare" runat="server" MaxLength="10"></asp:TextBox>
                                </li>
                                <li class="trvldetails_deviation">
                                    <span>Food Allowance</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtFoodAllowance" runat="server" Visible="true"></asp:TextBox>
                                </li>
                                <li class="trvldetails_bookthrough">
                                    <span>Remark:</span>

                                    <asp:TextBox AutoComplete="off" ID="txtRemark" runat="server" MaxLength="30"></asp:TextBox>
                                </li>
                                <li class="trvldetails_bookthrough">
                                    <span>Booked By Company:</span>

                                    <asp:CheckBox ID="chkCOS_Trvl" runat="server" Checked="false" />
                                </li>
                                <li class="trvldetails_bookthrough">
                                    <asp:TextBox AutoComplete="off" ID="TextBox3" runat="server" Visible="false" MaxLength="30"></asp:TextBox>
                                </li>
                                <li class="trvldetails_bookthrough">
                                    <asp:TextBox AutoComplete="off" ID="TextBox8" runat="server" Visible="false" MaxLength="30"></asp:TextBox>
                                </li>
                                <li class="trvldetails_bookthrough">
                                    <asp:LinkButton ID="trvldeatils_btnSave" runat="server" Text="Submit" ToolTip="Save"
                                        CssClass="Savebtnsve" OnClientClick=" return MultiClick_Trvl();" OnClick="trvldeatils_btnSave_Click"></asp:LinkButton>
                                </li>

                                <li class="trvldetails_bookthrough">
                                    <asp:LinkButton ID="trvldeatils_delete_btn" Visible="false" runat="server" Text="Delete" ToolTip="Delete" CssClass="Savebtnsve" OnClick="trvldeatils_delete_btn_Click"></asp:LinkButton>
                                </li>

                                <li class="trvldetails_bookthrough">
                                    <asp:LinkButton ID="trvldeatils_cancel_btn" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="trvldeatils_cancel_btn_Click"></asp:LinkButton>
                                </li>

                            </ul>
                        </div>
                        <li class="trvl_grid" id="litrvlgrid" runat="server">

                            <div>

                                <asp:GridView ID="dgTravelRequest" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                    DataKeyNames="trip_id,Exptrip_dtls_id" OnRowCreated="dgTravelRequest_RowCreated">
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
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Dep Date"
                                            DataField="departure_date"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Dep Place"
                                            DataField="departure_place"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Arr Date"
                                            DataField="arrival_date"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Arr Place"
                                            DataField="arrival_place"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
                                        <asp:TemplateField HeaderText="Through Travel Desk" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkCOS" Enabled="false" Visible="false" runat="server" Checked='<%#Convert.ToBoolean(Eval("tt")) %>' />
                                                <asp:Label ID="lblchkcos" runat="server" Text='<%#Eval("travel_through_desk")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Status"
                                            DataField="trvlbookstatus"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Deviation" Visible="false"
                                            DataField="deviation"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
                                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%--<asp:LinkButton ID="lnkTravelDetailsOldEdit" runat="server" Text='View' OnClick="lnkTravelDetailsEdit_Click">
                                                </asp:LinkButton>--%>
                                                <asp:ImageButton ID="lnkTravelDetailsEdit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkTravelDetailsEdit_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="New Column" ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubmitStatus" runat="server" Text='<%#Eval("trvl_submit_status")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btn_del_Trvl" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/delete.png" OnClick="btn_del_Trvl_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                            </div>

                        </li>
                        <li class="trvl_Accomodation" id="litrvlaccomodation" runat="server">
                            <asp:LinkButton ID="trvl_accmo_btn" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" OnClick="trvl_accmo_btn_Click"></asp:LinkButton>
                            <span id="spnaccomodation" runat="server">Accommodation: </span>
                        </li>
                        <li></li>
                        <li></li>
                        <div id="DivAccm" class="edit-contact" runat="server" visible="false">
                            <ul>
                                <li class="Accomodation_trip" style="display: none;">
                                    <asp:TextBox AutoComplete="off" ID="txtTripId_Accm" runat="server" Visible="false"></asp:TextBox>
                                </li>
                                <li class="Accomodation_type" style="display: none;">
                                    <%--<span>Travel Type</span><br />--%>
                                    <asp:TextBox AutoComplete="off" ID="txtTravelType_Accm" runat="server" CssClass="Dropdown" Visible="false"></asp:TextBox>
                                </li>
                                <li class="Accomodation_type" style="display: none;">
                                    <asp:TextBox AutoComplete="off" ID="txtEmpCode_Accm" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
                                </li>
                                <li class="Accomodation_type" style="display: none;">
                                    <span id="spnArrgment_accmodation" runat="server" visible="false">Accommodation Type</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />

                                    <asp:TextBox AutoComplete="off" ID="txtAcctype" runat="server" CssClass="Dropdown" Visible="false"></asp:TextBox>
                                    <asp:Panel ID="Panel8" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                            <ContentTemplate>
                                                <asp:ListBox ID="lstAccType" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true"
                                                    OnSelectedIndexChanged="lstAccType_SelectedIndexChanged" Visible="false">
                                                    <asp:ListItem Value="Hotel">Hotel</asp:ListItem>
                                                    <asp:ListItem Value="Guest House (Food)">Guest House (Food)  </asp:ListItem>
                                                    <asp:ListItem Value="Guest House (without Food)">Guest House (without Food)</asp:ListItem>
                                                    <asp:ListItem Value="Own Arrangement">Own Arrangement</asp:ListItem>

                                                </asp:ListBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>

                                    <ajaxToolkit:PopupControlExtender ID="PopupControlExtender8" PopupControlID="Panel8" TargetControlID="txtAcctype"
                                        Position="Bottom" runat="server">
                                    </ajaxToolkit:PopupControlExtender>
                                </li>
                                <li class="Arrangement">
                                    <span id="spnArrgment" runat="server">Accommodation Type</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <%--                                     <span id="spnArrgment_2" visible="false" runat="server"><br /> </span>--%>
                                    <asp:RadioButton ID="rdoAccomodation" runat="server" Text="Hotel" ToolTip="Hotel" GroupName="arrmgnts" OnCheckedChanged="rdoAccomodation_CheckedChanged" Checked="true" AutoPostBack="true" />
                                    <%--<span id="spnArrgment_3" visible="false" runat="server"><br /> <br /> <br /> </span>--%>
                                                        
                                </li>
                                <li class="Arrangement">
                                    <span id="Span6" runat="server"></span>
                                    <br />
                                    <asp:RadioButton ID="rdoOwnArgmnet" runat="server" Text="Own Arrangement" ToolTip="Own Arrangement" GroupName="arrmgnts" OnCheckedChanged="rdoAccomodation_CheckedChanged" AutoPostBack="true" />
                                </li>
                                <li></li>
                                <li class="Arrangement">
                                    <asp:RadioButton ID="rdoFood" runat="server" Text="Guest House (Food)" ToolTip="Guest House (with Food)" GroupName="arrmgnts" OnCheckedChanged="rdoAccomodation_CheckedChanged" AutoPostBack="true" />
                                </li>
                                <li class="Arrangement">
                                    <asp:RadioButton ID="rdoFoodAccomodation" runat="server" Text="Guest House (without Food)" ToolTip="Guest House (without Food)" GroupName="arrmgnts" OnCheckedChanged="rdoAccomodation_CheckedChanged" AutoPostBack="true" />
                                </li>
                                <li></li>
                                <li class="Accomodation_location">
                                    <span>Location </span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <asp:DropDownList ID="DDL_Location_Accm" CssClass="DropdownListSearch" runat="server">
                                    </asp:DropDownList>
                                    <%-- <asp:TextBox AutoComplete="off" ID="txtLocation_Accm" runat="server" MaxLength="100"  placeholder="Select Location"></asp:TextBox>
                                    <asp:TextBox AutoComplete="off" ID="TextBox1" runat="server" MaxLength="100" Visible="false" CssClass="Dropdown"></asp:TextBox>
                                       <asp:Panel ID="Panel6" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <asp:ListBox ID="lstLocation_Accm" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" 
                                                    OnSelectedIndexChanged="lstLocation_Accm_SelectedIndexChanged">
                                                   

                                                </asp:ListBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>

                                    <ajaxToolkit:PopupControlExtender ID="PopupControlExtender5" PopupControlID="Panel6" TargetControlID="TextBox1"
                                        Position="Bottom" runat="server">
                                    </ajaxToolkit:PopupControlExtender>

                                    <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchCities" MinimumPrefixLength="1"
                                    CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtLocation_Accm"
                                    ID="AutoCompleteExtender3" runat="server" FirstRowSelected="true">
                                </ajaxToolkit:AutoCompleteExtender>--%>
                                </li>
                                <li></li>
                                <li></li>
                                <li class="Accomodation_fromdate">
                                    <span>From </span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />

                                    <asp:TextBox AutoComplete="off" ID="txtFromdate_Accm" runat="server"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" Format="dd/MM/yyyy" TargetControlID="txtFromdate_Accm"
                                        runat="server">
                                    </ajaxToolkit:CalendarExtender>
                                </li>
                                <li class="Accomodation_date">
                                    <span>To</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtToDate_Accm" runat="server"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender6" Format="dd/MM/yyyy" TargetControlID="txtToDate_Accm"
                                        runat="server">
                                    </ajaxToolkit:CalendarExtender>
                                </li>
                                <li class="Accomodation_date">
                                    <span>Actual Stay Duration (in days)</span> &nbsp;&nbsp;<span style="color: red">*</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtnoofDays" runat="server" MaxLength="5"></asp:TextBox>
                                </li>
                                <li class="Accomodation_trip" id="liAccomdation_charges_1" runat="server">
                                    <span id="Span2" runat="server">Boarding and Lodging Charges: </span>
                                    <br />
                                </li>
                                <li class="Accomodation_trip" id="liAccomdation_charges_Blank1" runat="server"></li>
                                <li class="Accomodation_trip" id="liAccomdation_charges_Blank2" runat="server"></li>

                                <li class="Accomodation_requirement" id="liAccomdation_charges_charges" runat="server">
                                    <span>Amount (INR) hotel: </span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtCharges" runat="server"
                                        MaxLength="10"></asp:TextBox>
                                </li>
                                <li class="Accomodation_trip" id="liAccomdation_charges_chargesBlank1" runat="server"></li>
                                <li class="Accomodation_requirement" id="liAccomdation_Food_Deviation" runat="server">
                                    <%--<span>Deviation: </span>--%>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtDeviation_Accm" Visible="false" runat="server" Enabled="false" CssClass="graytextbox" ReadOnly="true" MaxLength="100"></asp:TextBox>
                                </li>

                                <li class="Accomodation_requirement" style="display: none;" id="liAccomdation_Food_Eligibility" runat="server">
                                    <span>Eligibility: </span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtEligibility_Accm" runat="server" Enabled="false" CssClass="graytextbox" ReadOnly="true" MaxLength="10"></asp:TextBox>
                                </li>
                                <li class="Accomodation_requirement" style="display: none;" id="liAccomdation_Food_Eligibility_Blank1" runat="server"></li>
                                <li class="Accomodation_requirement" style="display: none;" id="liAccomdation_Food_Eligibility_Blank2" runat="server"></li>
                                <li class="Accomodation_trip" id="liAccomdation_Food_exp_1" runat="server">
                                    <span id="Span3" runat="server">Additional Food Expenses: </span>
                                    <br />
                                    <br />
                                </li>
                                <li class="Accomodation_trip" id="liAccomdation_Food_exp_Blank1" runat="server"></li>
                                <li class="Accomodation_trip" id="liAccomdation_Food_exp_Blank2" runat="server"></li>
                                <li class="Accomodation_type" id="liAccomdation_Food_paidby" runat="server">
                                    <span>Paid By</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtAdditionalFoodExp_emp" runat="server" Enabled="false" Text="Employee"></asp:TextBox>
                                </li>


                                <li class="Accomodation_requirement" id="liAccomdation_Food_Charges" runat="server">
                                    <span>Amount (INR) add: </span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtAdditionalFoodExp_exp" runat="server"
                                        OnTextChanged="txtAdditionalFoodExp_exp_TextChanged" AutoPostBack="true" MaxLength="10"></asp:TextBox>
                                </li>
                                <li class="Accomodation_requirement" id="liAccomdation_Food_Charges_Blank" runat="server"></li>
                                <li class="Accomodation_trip" id="liAddintional_exps_1" runat="server">
                                    <%--<span>Flat Rate: </span><br /><br /> Additional Expenses--%>
                                    <span id="spnAdditionalExp" runat="server">Flat Rate:  </span>
                                    <br />
                                </li>
                                <%--<li id="liAddintional_exps_2" runat="server"></li>--%>
                                <li class="Accomodation_trip" id="liAddintional_exps_Blank1" runat="server"></li>
                                <li class="Accomodation_trip" id="liAddintional_exps_Blank2" runat="server"></li>
                                <li class="Accomodation_requirement" id="liAddintional_exps_Charges" runat="server">
                                    <%--<span>Deviation: </span>--%>
                                    <%-- <br />--%>
                                    <asp:TextBox AutoComplete="off" ID="txtFlatDev_Accm" runat="server" Visible="false" MaxLength="100"></asp:TextBox>
                                    <span id="spnAdditionalCharges" runat="server">Amount (INR): &nbsp;&nbsp;<span style="color: red">*</span></span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtFlatChg_Accm" runat="server" MaxLength="100"></asp:TextBox>
                                </li>

                                <li class="Accomodation_requirement" id="liAddintional_exps_eligibility" runat="server">
                                    <span id="spnAdditionaExp_eligibility" runat="server">Eligibility: </span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtFlatElg_Accm" runat="server" Enabled="false" CssClass="graytextbox" ReadOnly="true" MaxLength="10"></asp:TextBox>
                                </li>
                                <li class="Accomodation_requirement">
                                    <span id="liAddintional_exps_deviation_1" runat="server">Deviation: </span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtAdditional_exp_deviation_Accm" runat="server" Enabled="false" CssClass="graytextbox" ReadOnly="true" MaxLength="100"></asp:TextBox>
                                </li>
                                <li class="Accomodation_type" id="liAddintional_exps_flatpaid_1" runat="server">
                                    <%--  <span>Paid By</span><br />--%>
                                    <span>Additional Expenses</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtaddintionalExpens_Accm" runat="server"></asp:TextBox>
                                    <asp:TextBox AutoComplete="off" ID="txtFlatPaid_Accm" runat="server" Visible="false"></asp:TextBox>
                                    <asp:Panel ID="Panel7" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                            <ContentTemplate>
                                                <asp:ListBox ID="lstFlatPaid_Accm" runat="server" Visible="false" CssClass="taskparentclasskkk" AutoPostBack="true"
                                                    OnSelectedIndexChanged="lstFlatPaid_Accm_SelectedIndexChanged">
                                                    <asp:ListItem>Company</asp:ListItem>
                                                    <asp:ListItem>Employee</asp:ListItem>
                                                    <asp:ListItem></asp:ListItem>
                                                </asp:ListBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>

                                    <ajaxToolkit:PopupControlExtender ID="PopupControlExtender7" PopupControlID="Panel7" TargetControlID="txtFlatPaid_Accm"
                                        Position="Bottom" runat="server">
                                    </ajaxToolkit:PopupControlExtender>
                                </li>
                                <li class="Accomodation_type" id="liAddintional_exps_flatpaid_Blank1" runat="server"></li>
                                <li class="Accomodation_type" id="liAddintional_exps_flatpaid_Blank2" runat="server"></li>
                                <li class="Accomodation_requirement">
                                    <span>Remarks: </span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtRemarks_Accm" runat="server" MaxLength="30"></asp:TextBox>
                                </li>
                                <li></li>
                                <li></li>
                                <li class="Accomodation_type" id="liAccomdation_charges_paidby" runat="server">
                                    <span>Paid By</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                    <asp:DropDownList ID="lstPaidBy_Accm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lstPaidBy_Accm_SelectedIndexChanged">
                                        <%--                                        <asp:ListItem value="0" >Select Paid By</asp:ListItem>
                                        <asp:ListItem value="Company" >Company</asp:ListItem>
                                        <asp:ListItem Value="Employee">Employee</asp:ListItem>--%>
                                    </asp:DropDownList>
                                    <asp:TextBox AutoComplete="off" ID="txtPaidBy_Accm" runat="server" CssClass="Dropdown" Visible="false"></asp:TextBox>
                                    <%--<asp:Panel ID="Panel1" runat="server" CssClass="taskparentclasskkk">
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <ContentTemplate>
                                                <asp:ListBox ID="lstPaidBy_Accm" runat="server" CssClass="taskparentclasskkk" AutoPostBack="True" OnSelectedIndexChanged="lstPaidBy_Accm_SelectedIndexChanged">
                                                    <asp:ListItem value="0" >Select Paid By</asp:ListItem>
                                                    <asp:ListItem value="Company" >Company</asp:ListItem>
                                                    <asp:ListItem Value="Employee">Employee</asp:ListItem>

                                                </asp:ListBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>

                                    <ajaxToolkit:PopupControlExtender ID="PopupControlExtender6" PopupControlID="Panel1" TargetControlID="txtPaidBy_Accm"
                                        Position="Bottom" runat="server">
                                    </ajaxToolkit:PopupControlExtender>--%>
                                </li>
                                <li class="Accomodation_type" id="liAccomdation_charges_paidbyBlank1" runat="server"></li>
                                <li class="Accomodation_type" id="liAccomdation_charges_paidbyBlank2" runat="server"></li>
                                <li class="Accomodation_requirement">
                                    <asp:LinkButton ID="accmo_btnSave" OnClientClick=" return MultiClick_Accm();" runat="server" Text="Submit"
                                        ToolTip="Save" CssClass="Savebtnsve" OnClick="accmo_btnSave_Click">Submit</asp:LinkButton>
                                </li>
                                <li class="Accomodation_requirement">
                                    <asp:LinkButton ID="accmo_delete_btn" runat="server" Text="Delete" ToolTip="Delete" CssClass="Savebtnsve"
                                        OnClick="accmo_delete_btn_Click">Delete</asp:LinkButton>
                                </li>
                                <li class="Accomodation_requirement">
                                    <asp:LinkButton ID="accmo_cancel_btn" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve"
                                        OnClick="accmo_cancel_btn_Click">Back</asp:LinkButton>
                                </li>

                            </ul>
                        </div>
                        <li class="trvl_grid" id="litrvlgridAccomodation" runat="server">
                            <div>

                                <asp:GridView ID="dgAccomodation" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                    DataKeyNames="trip_id,local_accomodation_id" OnRowCreated="dgAccomodation_RowCreated">
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
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="To Date"
                                            DataField="To Date"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Accommodation Type"
                                            DataField="Accomodation_type"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Location"
                                            DataField="Location"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="20%" ItemStyle-BorderColor="Navy" />
                                        <asp:TemplateField HeaderText="Through COS" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkCOS" Enabled="false" Visible="false" runat="server" Checked='<%#Convert.ToBoolean(Eval("tt")) %>' />
                                                <asp:Label ID="lblchkcos" runat="server" Text='<%#Eval("travel_through_desk")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Status"
                                            DataField="trvl_accomstatus"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Deviation" Visible="false"
                                            DataField="deviation"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
                                        <asp:TemplateField HeaderText="Details" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%--<asp:LinkButton ID="lnkAccomodationolddit" runat="server" Text='View' OnClick="lnkAccomodationdit_Click">
                                                </asp:LinkButton>--%>
                                                <asp:ImageButton ID="lnkAccomodationdit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkAccomodationdit_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="New Column" ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubmitStatus" runat="server" Text='<%#Eval("trvl_submit_status")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btn_del_Accm" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/delete.png" OnClick="btn_del_Accm_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>

                        <li class="trvl_local">
                            <asp:LinkButton ID="trvl_localbtn" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" OnClick="trvl_localbtn_Click"></asp:LinkButton>
                            <span id="spnlocalTrvl" runat="server">Local Travel: </span>
                        </li>
                        <li></li>
                        <li></li>
                        <div id="Div_Locl" class="edit-contact" runat="server" visible="false">
                            <ul>
                                <li class="localtrvl_trip" id="litripid_Locl" runat="server" style="display: none;">
                                    <span id="spantripid_Locl" runat="server" visible="false">Trip ID</span><br />

                                    <asp:TextBox AutoComplete="off" ID="txtTripId_Locl" runat="server" Visible="false"></asp:TextBox>

                                </li>
                                <li class="localtrvl_type" style="display: none;">
                                    <span>Travel Type</span><br />

                                    <asp:TextBox AutoComplete="off" ID="txtTravelType_Locl" runat="server" CssClass="grayDropdown" Visible="false" Enabled="false"></asp:TextBox>
                                    <asp:Panel ID="Panel9" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                            <ContentTemplate>
                                                <asp:ListBox ID="lstTravelType_Locl" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" Visible="True" OnSelectedIndexChanged="lstTravelType_Locl_SelectedIndexChanged" Enabled="false"></asp:ListBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>

                                    <ajaxToolkit:PopupControlExtender ID="PopupControlExtender9" PopupControlID="Panel9" TargetControlID="txtTravelType_Locl"
                                        Position="Bottom" runat="server">
                                    </ajaxToolkit:PopupControlExtender>

                                </li>
                                <li class="localtrvl_trip" style="display: none;">
                                    <asp:TextBox AutoComplete="off" ID="txtEmpCode_Locl" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
                                </li>
                                <li>
                                    <span>Mode of Travel</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                    <asp:DropDownList ID="lstTravelMode_Locl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="lstTravelMode_Locl_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:TextBox AutoComplete="off" ID="txtTravelMode_Locl" runat="server" CssClass="Dropdown" Visible="false"></asp:TextBox>
                                    <%--                                    <asp:Panel ID="Panel11" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                            <ContentTemplate>
                                                <asp:ListBox ID="lstTravelMode_Locl" runat="server" CssClass="taskparentclass2" AutoPostBack="true" OnSelectedIndexChanged="lstTravelMode_Locl_SelectedIndexChanged"></asp:ListBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>

                                    <ajaxToolkit:PopupControlExtender ID="PopupControlExtender11" PopupControlID="Panel11" TargetControlID="txtTravelMode_Locl"
                                        Position="Bottom" runat="server">
                                    </ajaxToolkit:PopupControlExtender>--%>
                                </li>
                                <li class="localtrvl_fromdate"></li>
                                <li class="localtrvl_fromdate"></li>
                                <li class="localtrvl_fromdate">
                                    <span>From </span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtFromdate_Locl" runat="server"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender7" Format="dd/MM/yyyy" TargetControlID="txtFromdate_Locl"
                                        runat="server">
                                    </ajaxToolkit:CalendarExtender>
                                </li>

                                <li class="localtrvl_date">
                                    <span>To</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtToDate_Locl" runat="server"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender8" Format="dd/MM/yyyy" TargetControlID="txtToDate_Locl"
                                        runat="server">
                                    </ajaxToolkit:CalendarExtender>
                                </li>

                                <li class="localtrvl_location">
                                    <span>Location </span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <asp:DropDownList ID="DDL_Location_Locl" CssClass="DropdownListSearch" runat="server">
                                    </asp:DropDownList>

                                    <%--<asp:TextBox AutoComplete="off" ID="txtLocation_Locl" runat="server" placeholder="Select Location"   MaxLength="100"></asp:TextBox>
                                    <asp:TextBox AutoComplete="off" ID="TextBox4" runat="server" Visible="false" CssClass="Dropdown" MaxLength="100"></asp:TextBox>
                                        <asp:Panel ID="Panel10" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                            <ContentTemplate>
                                                <asp:ListBox ID="lstLocation_Locl" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" Visible="True" OnSelectedIndexChanged="lstLocation_Locl_SelectedIndexChanged">
                                                </asp:ListBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>

                                    <ajaxToolkit:PopupControlExtender ID="PopupControlExtender10" PopupControlID="Panel10" TargetControlID="TextBox4"
                                        Position="Bottom" runat="server">
                                    </ajaxToolkit:PopupControlExtender>
                                    <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchCities" MinimumPrefixLength="1"
                                    CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtLocation_Locl"
                                    ID="AutoCompleteExtender4" runat="server" FirstRowSelected="true">
                                    </ajaxToolkit:AutoCompleteExtender>--%>
                                </li>
                                <li class="localtrvl_requirement">
                                    <span>Amount (INR) : </span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtCharges_Locl" runat="server" MaxLength="10"></asp:TextBox>
                                </li>
                                <li class="localtrvl_requirement">
                                    <span>Remarks: </span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtRemarks_Locl" runat="server" MaxLength="30"></asp:TextBox>
                                </li>

                                <li class="localtrvl_requirement">
                                    <%--<span>Deviation: </span>--%>
                                    <asp:TextBox AutoComplete="off" ID="txtDeviation_Locl" Visible="false" runat="server" MaxLength="100" Enabled="false"></asp:TextBox>
                                    <span>Booked By Company:</span>
                                    <asp:CheckBox ID="Chk_COS_Locl" runat="server" Checked="false" />
                                </li>

                                <li class="localtrvl_trip">
                                    <asp:LinkButton ID="localtrvl_btnSave" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClientClick=" return MultiClick_Locl();" OnClick="localtrvl_btnSave_Click">Submit</asp:LinkButton>
                                </li>

                                <li class="localtrvl_trip">
                                    <asp:LinkButton ID="localtrvl_delete_btn" runat="server" Text="Delete" ToolTip="Cancel" CssClass="Savebtnsve" OnClick="localtrvl_delete_btn_Click"></asp:LinkButton>
                                </li>

                                <li class="localtrvl_trip">
                                    <asp:LinkButton ID="localtrvl_cancel_btn" runat="server" Text="back" ToolTip="back" CssClass="Savebtnsve" OnClick="localtrvl_cancel_btn_Click"></asp:LinkButton>
                                </li>

                            </ul>
                        </div>
                        <li class="trvl_grid">
                            <div>

                                <asp:GridView ID="dgLocalTravel" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                    DataKeyNames="trip_id,local_travel_id" OnRowCreated="dgLocalTravel_RowCreated">
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
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="To Date"
                                            DataField="To Date"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Mode of Travel"
                                            DataField="trip_mode"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="20%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Location"
                                            DataField="Location"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="20%" ItemStyle-BorderColor="Navy" />
                                        <asp:TemplateField HeaderText="Through COS" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkCOS" Enabled="false" Visible="false" runat="server" Checked='<%#Convert.ToBoolean(Eval("tt")) %>' />
                                                <asp:Label ID="lblChkCOS" runat="server" Text='<%#Eval("Through_COS")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Status"
                                            DataField="lbookedStatus"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Deviation" Visible="false"
                                            DataField="deviation"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
                                        <asp:TemplateField HeaderText="Details" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%--<asp:LinkButton ID="lnkLocalTravleOldEdit" runat="server" Text='View' OnClick="lnkLocalTravleEdit_Click">
                                                </asp:LinkButton>--%>
                                                <asp:ImageButton ID="lnkLocalTravleEdit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkLocalTravleEdit_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="New Column" ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubmitStatus" runat="server" Text='<%#Eval("trvl_submit_status")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btn_del_Locl" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/delete.png" OnClick="btn_del_Locl_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                            </div>
                        </li>


                        <li class="trvl_local">
                            <span id="Span1" runat="server" style="font-weight: bold">Expenses Summary </span>
                            <br />
                            <br />
                            <asp:LinkButton ID="lnkbtn_expdtls" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" OnClick="lnkbtn_expdtls_Click"></asp:LinkButton>
                            <span id="spnexpdtls" runat="server">Other Expenses&nbsp;&nbsp; </span>
                        </li>
                        <li></li>
                        <li></li>
                        <div id="Div_Oth" class="edit-contact" runat="server" visible="false">
                            <ul>
                                <li class="trvldetails_tripid" style="display: none;">
                                    <asp:TextBox AutoComplete="off" ID="txtTripId_Oth" runat="server" Visible="false"></asp:TextBox>
                                </li>
                                <li class="trvldetails_type" style="display: none;">
                                    <span>Type of Travel</span>&nbsp;&nbsp;<span style="color: red">*</span><br />


                                    <asp:TextBox AutoComplete="off" ID="txtTravelType_Oth" runat="server" CssClass="grayDropdown"></asp:TextBox>
                                    <asp:Panel ID="Panel12" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                            <ContentTemplate>
                                                <asp:ListBox ID="lstTravelType_Oth" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstTravelType_Oth_SelectedIndexChanged"></asp:ListBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>

                                    <ajaxToolkit:PopupControlExtender ID="PopupControlExtender12" PopupControlID="Panel12" TargetControlID="txtTravelType_Oth"
                                        Position="Bottom" runat="server">
                                    </ajaxToolkit:PopupControlExtender>

                                </li>
                                <li class="trvldetails_deviation" style="display: none;">
                                    <asp:TextBox AutoComplete="off" ID="txtEmpCode_Oth" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
                                </li>
                                <li>
                                    <span>Details</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                    <asp:DropDownList ID="lstExpdtls_Oth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="lstExpdtls_Oth_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:TextBox AutoComplete="off" ID="txtExpdtls_Oth" runat="server" CssClass="Dropdown" Visible="false"></asp:TextBox>
                                    <%--                                    <asp:Panel ID="Panel13" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                            <ContentTemplate>
                                                <asp:ListBox ID="lstExpdtls_Oth" runat="server" CssClass="taskparentclass2" AutoPostBack="true" OnSelectedIndexChanged="lstExpdtls_Oth_SelectedIndexChanged"></asp:ListBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>

                                    <ajaxToolkit:PopupControlExtender ID="PopupControlExtender13" PopupControlID="Panel13" TargetControlID="txtExpdtls_Oth"
                                        Position="Bottom" runat="server">
                                    </ajaxToolkit:PopupControlExtender>--%>

                                </li>
                                <li class="localtrvl_fromdate"></li>
                                <li class="localtrvl_fromdate"></li>
                                <li class="localtrvl_fromdate">
                                    <span>Date </span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtFromdate_Oth" runat="server"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender9" Format="dd/MM/yyyy" TargetControlID="txtFromdate_Oth"
                                        runat="server">
                                    </ajaxToolkit:CalendarExtender>
                                </li>
                                <li class="trvldetails_bookthrough">
                                    <span>Amount  (INR)</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <asp:TextBox AutoComplete="off" ID="txtAmt_Oth" runat="server" MaxLength="10"></asp:TextBox>

                                </li>
                                <li class="trvldetails_bookthrough">
                                    <span>Remarks</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <asp:TextBox AutoComplete="off" ID="txtRemarks_Oth" runat="server" MaxLength="30"></asp:TextBox>

                                </li>
                                <li class="trvldetails_deviation" style="display: none;">
                                    <asp:Label ID="lblnodays" runat="server" Visible="false" Text="Stay Duration (in days)"></asp:Label>
                                    <asp:TextBox AutoComplete="off" Visible="false" ID="txtnoofDays_Oth" runat="server" MaxLength="5" AutoPostBack="true" OnTextChanged="txtnoofDays_Oth_TextChanged"></asp:TextBox>

                                </li>
                                <li class="trvldetails_deviation" style="display: none;">
                                    <span style="display: none">Paid By: </span>
                                    <asp:TextBox AutoComplete="off" ID="txtpaidby" runat="server" Visible="false" Text="Employee" CssClass="Dropdown" Enabled="false"></asp:TextBox>
                                    <asp:Panel ID="Panel14" runat="server" CssClass="taskparentclasskkk" Visible="false">
                                        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                            <ContentTemplate>
                                                <asp:ListBox ID="lstpaidby" runat="server" CssClass="taskparentclasskkk" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="lstpaidby_SelectedIndexChanged">
                                                    <asp:ListItem Text="Employee" Value="Employee">Employee</asp:ListItem>
                                                    <asp:ListItem Text="Company" Value="Company">Company</asp:ListItem>
                                                </asp:ListBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>
                                    <ajaxToolkit:PopupControlExtender ID="PopupControlExtender14" PopupControlID="Panel14" TargetControlID="txtpaidby"
                                        Position="Bottom" runat="server">
                                    </ajaxToolkit:PopupControlExtender>

                                </li>
                                <li class="trvldetails_deviation">
                                    <asp:LinkButton ID="Oth_btnSave" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve"
                                        OnClientClick=" return MultiClick_Oth();" OnClick="Oth_btnSave_Click"></asp:LinkButton>
                                </li>
                                <li class="trvldetails_deviation">
                                    <asp:LinkButton ID="Oth_btnDelete" runat="server" Text="Delete" ToolTip="Delete" CssClass="Savebtnsve"
                                        OnClick="Oth_btnDelete_Click"></asp:LinkButton>
                                </li>
                                <li class="trvldetails_deviation">
                                    <asp:LinkButton ID="Oth_btnCancel" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve"
                                        OnClick="Oth_btnCancel_Click"></asp:LinkButton>
                                </li>
                            </ul>
                        </div>
                        <li class="trvl_grid">
                            <div>
                                <asp:GridView ID="gvexpensdtls" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                    DataKeyNames="trip_id,exp_id,exp_sr_no,trip_details" OnRowDataBound="gvexpensdtls_RowDataBound" OnRowCreated="gvexpensdtls_RowCreated">
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

                                        <asp:BoundField HeaderText="Place"
                                            DataField="trip_destination"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="20%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Details"
                                            DataField="exp_name"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Description"
                                            DataField="Descr"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="20%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Paid by Company"
                                            DataField="paid_by_comp"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Paid by Employee"
                                            DataField="paid_emp"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Total Amount Claimed"
                                            DataField="totamt"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Eligibility" Visible="false"
                                            DataField="eligibility"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="1%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Diff Amt" Visible="false"
                                            DataField="diff"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="1%" ItemStyle-BorderColor="Navy" />

                                        <asp:TemplateField HeaderText="Details">
                                            <ItemTemplate>
                                                <%--<asp:LinkButton ID="lnkAddOldExpense" runat="server" Text='View' OnClick="lnkAddExpense_Click">
                                                </asp:LinkButton>--%>
                                                <asp:ImageButton ID="lnkAddExpense" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkAddExpense_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                <asp:GridView ID="grdExpAccApproved" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                   DataKeyNames="trip_id,exp_id,exp_sr_no,trip_details" OnRowDataBound="grdExpAccApproved_RowDataBound">
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
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>

                                        <asp:BoundField HeaderText="To Date"
                                            DataField="trip_frm_date"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="0%" ItemStyle-BorderColor="Navy" Visible="false"/>

                                        <asp:BoundField HeaderText="Place"
                                            DataField="trip_destination"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>

                                        <asp:BoundField HeaderText="Details"
                                            DataField="exp_name"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>

                                        <asp:BoundField HeaderText="Description"
                                            DataField="Descr"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy"/>

                                         <asp:BoundField HeaderText="Paid by Company"
                                            DataField="paid_by_comp"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="8%" ItemStyle-BorderColor="Navy"/>

                                         <asp:BoundField HeaderText="Paid by Employee"
                                            DataField="paid_emp"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="8%" ItemStyle-BorderColor="Navy"/>

                                          <asp:BoundField HeaderText="Total Amount Claimed"
                                            DataField="totamt"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="8%" ItemStyle-BorderColor="Navy"/>
                                            
                                            <asp:BoundField HeaderText="Paid by Company (Amt Release By Account)"
                                            DataField="amtRelAccPayCom"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="8%" ItemStyle-BorderColor="Navy"/>
                                            
                                            <asp:BoundField HeaderText="Paid by Employee (Amt Release By Account)"
                                            DataField="amtRelAccPayEmp"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="8%" ItemStyle-BorderColor="Navy"/>
                                            
                                            <asp:BoundField HeaderText="Account Remarks"
                                            DataField="AccountRemarks"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="20%" ItemStyle-BorderColor="Navy"/>
                                            
                                            <asp:BoundField HeaderText="Eligibility" Visible="false"
                                            DataField="eligibility"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="0%" ItemStyle-BorderColor="Navy"/>

                                            <asp:BoundField HeaderText="Diff Amt" Visible="false"
                                            DataField="diff"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="0%" ItemStyle-BorderColor="Navy"/>
 

                                    </Columns>

                                </asp:GridView>
                            </div>
                        </li>
                        <li class="trvl_local">
                            <span id="spndailyallown" runat="server" visible="false">Daily Halting Allowance (INR) </span>
                            <asp:TextBox AutoComplete="off" ID="txtdailyhaltingallowance" runat="server" Visible="false" MaxLength="10"></asp:TextBox>
                        </li>
                        <li></li>
                        <li></li>
                        <li class="trvl_local">
                            <span>Total Amount Claimed</span>
                            <asp:TextBox AutoComplete="off" ID="txtTotAmtClaimed" runat="server" MaxLength="10" Enabled="False"></asp:TextBox>
                        </li>

                        <li class="trvl_local">
                            <span>Advance Taken</span>
                            <asp:TextBox AutoComplete="off" ID="txtLessAdvTaken" runat="server" MaxLength="10" Enabled="true"></asp:TextBox>
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
                            <span>Comments</span>
                            <asp:TextBox AutoComplete="off" ID="txtReasonDeviation" runat="server" MaxLength="30"></asp:TextBox>
                        </li>
                        <%--                        <li></li>--%>
                        
                        <li class="trvl_local">
                            <span id="SpnAccountAmount" runat="server" visible="false">Amount Released by Accounts </span>
                            <asp:TextBox AutoComplete="off" ID="txtAccountAmount" runat="server" MaxLength="10" Enabled="False" Visible="false"></asp:TextBox>
                        </li>
                        <li class="trvl_date"></li><li class="trvl_date"></li>
                        <li>
                            <asp:LinkButton ID="lnkCalculate" runat="server" Enabled="false" Visible="false" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClick="lnkCalculate_Click">Calculate</asp:LinkButton>
                        </li>
                        <li class="trvl_local">
                            <span>Upload File</span>
                            <asp:FileUpload ID="ploadexpfile" runat="server" AllowMultiple="true"></asp:FileUpload>
                            <asp:GridView ID="gvuploadedFiles" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                DataKeyNames="fileid" OnRowDataBound="gvuploadedFiles_RowDataBound">
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
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDeleteexpFile" runat="server" Text="Delete" OnClick="lnkDeleteexpFile_Click">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </li>
                        <li></li>
                        <li></li>

                        <li class="trvl_Approver" id="lsttrvlapprover" runat="server">
                            <span>Approver </span>
                            <br />
                            <asp:ListBox ID="lstApprover" runat="server"></asp:ListBox>
                            <%--style="overflow-x:auto;"--%>
                        
                        </li>

                        <li class="trvl_inter" id="lsttrvlIntermidates" style="display: none;" runat="server">
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
    </div>
    <div class="trvl_Savebtndiv">
        <asp:LinkButton ID="btnMod" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClick="btnMod_Click">Modify</asp:LinkButton>

        <%-- Following Popup for Modify Travel Expenses Request --%>
        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" DisplayModalPopupID="mpe_Mod" TargetControlID="btnMod">
        </ajaxToolkit:ConfirmButtonExtender>
        <ajaxToolkit:ModalPopupExtender ID="mpe_Mod" runat="server" PopupControlID="pnlPopup_Mod" TargetControlID="btnMod" OkControlID="btnYes_Mod"
            CancelControlID="btnNo_Mod" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup_Mod" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Confirmation
            </div>
            <div class="body">
                Do you want to Submit ?
            </div>
            <div class="footer" align="right">
                <asp:Button ID="btnNo_Mod" runat="server" Text="No" />
                <asp:Button ID="btnYes_Mod" runat="server" Text="Yes" />
            </div>
        </asp:Panel>

    </div>
    <div class="trvl_Savebtndiv" id="divBtnHide" runat="server">
        <asp:LinkButton ID="btn_Draft" runat="server" Text="Save As Draft" ToolTip="Save As Draft" CssClass="Savebtnsve" OnClientClick="return SaveDraftClick();" OnClick="btn_Draft_Click">Save As Draft</asp:LinkButton>
        <asp:LinkButton ID="trvl_btnSave" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();" OnClick="trvl_btnSave_Click">Submit</asp:LinkButton>
        <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="Savebtnsve" OnClientClick="return CancelMultiClick();" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
        <asp:LinkButton ID="btnback_mng" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/ManageTravel_expense.aspx">Back</asp:LinkButton>
        <asp:LinkButton ID="mobile_btnPrintPV" runat="server" Text="Print Voucher" ToolTip="Print Voucher" CssClass="Savebtnsve" OnClick="mobile_btnPrintPV_Click">Print Voucher</asp:LinkButton>
    </div>
    <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
    <asp:TextBox AutoComplete="off" ID="txtEmpName" runat="server" Visible="false"></asp:TextBox>
    <asp:Label ID="testsanjay" runat="server" Visible="false"></asp:Label>
    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />

    <asp:HiddenField ID="hdnvouno" runat="server" />
    <asp:HiddenField ID="hflEmpName" runat="server" />

    <asp:HiddenField ID="hflEmpDesignation" runat="server" />

    <asp:HiddenField ID="hflEmpDepartment" runat="server" />

    <asp:HiddenField ID="hflEmailAddress" runat="server" />

    <asp:HiddenField ID="hflGrade" runat="server" />
    <asp:HiddenField ID="hflEMPAGrade" runat="server" />

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

    <asp:HiddenField ID="hdnApprId" runat="server" />

    <asp:HiddenField ID="hdnApprEmailaddress" runat="server" />

    <asp:HiddenField ID="hdnEligible" runat="server" />
    <asp:HiddenField ID="hdnTrdays" runat="server" />
    <asp:HiddenField ID="hdnTravelDtlsId" runat="server" />
    <asp:HiddenField ID="hdnAccId" runat="server" />
    <asp:HiddenField ID="hdnLocalId" runat="server" />
    <asp:HiddenField ID="hdnTravelstatus" runat="server" />
    <asp:HiddenField ID="hdnexp_id" runat="server" />
    <asp:HiddenField ID="hdnexptrvldtls_id" runat="server" />
    <asp:HiddenField ID="hdnfileid" runat="server" />
    <asp:HiddenField ID="hdnfrmdate_emial" runat="server" />
    <asp:HiddenField ID="hdntodate_emial" runat="server" />
    <asp:HiddenField ID="hdnActualTrvlDays" runat="server" />
    <asp:HiddenField ID="hdnmainexpStatus" runat="server" />
    <asp:HiddenField ID="hdnApprovalStatusExp" runat="server" />
    <asp:HiddenField ID="hdn_apprStatus" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />

    <asp:HiddenField ID="hdnfromdate_Trvl" runat="server" />
    <asp:HiddenField ID="hdnTodate_Trvl" runat="server" />
    <asp:HiddenField ID="hdnTryiptypeid" runat="server" />
    <asp:HiddenField ID="hdnCOS" runat="server" />
    <asp:HiddenField ID="hdntrdetailsid" runat="server" />
    <asp:HiddenField ID="hdnTrvlBookdStatus" runat="server" />

    <asp:HiddenField ID="hdnAccdtlsid" runat="server" />
    <asp:HiddenField ID="hdnDaysDiff" runat="server" />
    <asp:HiddenField ID="hdnactualEligbility" runat="server" />
    <asp:HiddenField ID="hdnflatEligbility" runat="server" />
    <asp:HiddenField ID="hdnfromdate_Accm" runat="server" />
    <asp:HiddenField ID="hdnTodate_Accm" runat="server" />
    <asp:HiddenField ID="hdnactualdays" runat="server" />
    <asp:HiddenField ID="hdnIsThrughCOS" runat="server" />
    <asp:HiddenField ID="hdnAccomodationStatus" runat="server" />
    <asp:HiddenField ID="hdntripcharges_Accm" runat="server" />


    <asp:HiddenField ID="hdnCOS_Locl" runat="server" />

    <asp:HiddenField ID="hflGrade_Locl" runat="server" />

    <asp:HiddenField ID="hdnDeviation_Locl" runat="server" />
    <asp:HiddenField ID="hdnDesk_Locl" runat="server" />
    <asp:HiddenField ID="HiddenField1" runat="server" />

    <asp:HiddenField ID="hdnfromdate_Locl" runat="server" />
    <asp:HiddenField ID="hdnTodate_Locl" runat="server" />
    <asp:HiddenField ID="hdntrdetailsid_Locl" runat="server" />
    <asp:HiddenField ID="hdntrmodeid" runat="server" />

    <asp:HiddenField ID="hdndviation_s" runat="server" />

    <asp:HiddenField ID="hdnCOS_Oth" runat="server" />

    <asp:HiddenField ID="hflGrade_Oth" runat="server" />

    <asp:HiddenField ID="hdnDeviation_Oth" runat="server" />
    <asp:HiddenField ID="hdnDesk_Oth" runat="server" />

    <asp:HiddenField ID="hdnfromdate_Oth" runat="server" />
    <asp:HiddenField ID="hdnTodate_Oth" runat="server" />
    <asp:HiddenField ID="hdntrdetailsid_Oth" runat="server" />
    <asp:HiddenField ID="hdntxtAmt_Oth" runat="server" />
    <asp:HiddenField ID="hdnpagereqestfrm_Oth" runat="server" />
    <asp:HiddenField ID="hdnexpSrno_Oth" runat="server" />

    <asp:HiddenField ID="hdnDaysDiff_Oth" runat="server" />
    <asp:HiddenField ID="hdnIncidentalCharges_Oth" runat="server" />
    <asp:HiddenField ID="hdnselectionStatus_Oth" runat="server" />
    <asp:HiddenField ID="FilePath" runat="server" />
    <asp:HiddenField ID="hdncomp_code" runat="server" />
    <asp:HiddenField ID="hdncomp_name" runat="server" />
    <asp:HiddenField ID="hdndept_id" runat="server" />
    <asp:HiddenField ID="hdnIsDraft" runat="server" />

    <%-- <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchProject" MinimumPrefixLength="1"
        CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="Txt_ProjectName"
        ID="AutoCompleteExtender5" runat="server" FirstRowSelected="true">
    </ajaxToolkit:AutoCompleteExtender>

    <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchDepartment" MinimumPrefixLength="1"
        CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="Txt_DeptName"
        ID="AutoCompleteExtender6" runat="server" FirstRowSelected="true">
    </ajaxToolkit:AutoCompleteExtender>--%>

    <script type="text/javascript">
        function MultiClick_Oth() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=Oth_btnSave.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;

                if (ele != null)
                    ele.disabled = true;
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        function MultiClick_Locl() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=localtrvl_btnSave.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;

                if (ele != null)
                    ele.disabled = true;
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        function SetCntrsl(allowance, IncidentalCharges) {
            //alert(allowance)

            if (IncidentalCharges != "") {
                document.getElementById("<%=txtAmt_Oth.ClientID%>").value = IncidentalCharges;
                document.getElementById("<%=hdntxtAmt_Oth.ClientID%>").value = IncidentalCharges;
            }
            else {
                document.getElementById("<%=txtAmt_Oth.ClientID%>").value = "0";
                document.getElementById("<%=hdntxtAmt_Oth.ClientID%>").value = "0";
            }
            if (allowance == "Yes") {

                document.getElementById("<%=txtnoofDays_Oth.ClientID%>").disabled = false
                document.getElementById("<%=txtnoofDays_Oth.ClientID%>").style.backgroundColor = "#ffffff"; // backcolor                
                document.getElementById("<%=txtnoofDays_Oth.ClientID%>").style.display = "inline";
                document.getElementById("<%=lblnodays.ClientID%>").style.display = "inline";

            }
            else {
                document.getElementById("<%=txtnoofDays_Oth.ClientID%>").disabled = true
                document.getElementById("<%=txtnoofDays_Oth.ClientID%>").value = "";
                document.getElementById("<%=txtnoofDays_Oth.ClientID%>").style.backgroundColor = "#ebebe4"; // backcolor               
                document.getElementById("<%=txtnoofDays_Oth.ClientID%>").style.display = "none";
                document.getElementById("<%=lblnodays.ClientID%>").style.display = "none";

            }
            return;
        }
        function SetDeviation_Locl(Deviation) {
            if (Deviation != "") {
                document.getElementById("<%=txtDeviation_Locl.ClientID%>").value = Deviation;
                document.getElementById("<%=hdnDeviation_Locl.ClientID%>").value = Deviation;
            }
            return;
        }
        function validateTripType_Accm() {

            document.getElementById("<%=txtFlatChg_Accm.ClientID%>").value = "";
            document.getElementById("<%=txtDeviation_Accm.ClientID%>").value = "";
            document.getElementById("<%=txtCharges.ClientID%>").value = "";
        }
        function MultiClick_Accm() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=accmo_btnSave.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;

                if (ele != null)
                    ele.disabled = true;
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        function MultiClick_Trvl() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=trvldeatils_btnSave.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;

                if (ele != null)
                    ele.disabled = true;
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        function SetDeviation(Deviation) {
            if (Deviation != "") {
                document.getElementById("<%=txtDeviation.ClientID%>").value = Deviation;
            }
            return;
        }
        function onCharOnlyNumber_Time(e) {
            var keynum;
            var keychar;
            var numcheck = /[0123456789:]/;

            if (window.event) {
                keynum = e.keyCode;
            }
            else if (e.which) {
                keynum = e.which;
            }
            keychar = String.fromCharCode(keynum);
            return numcheck.test(keychar);
        }
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
            var unicode = e.keyCode ? e.keyCode : e.charCode
            if (unicode == 8 || unicode == 46) {
                keychar = unicode;
            }
            return numcheck.test(keychar);
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

        function SaveMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=trvl_btnSave.ClientID%>');

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
        function CancelMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=btnCancel.ClientID%>');

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
        function DownloadFile(file) {
            // alert(file);
            var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;

            //alert(localFilePath);
            window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
        }

        //
        function SaveDraftClick() {
        try {
            var retunboolean = true;
            var ele = document.getElementById('<%=btn_Draft.ClientID%>');

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
    </script>
</asp:Content>

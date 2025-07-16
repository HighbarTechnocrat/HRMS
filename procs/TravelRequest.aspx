<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" EnableViewState="true"
    CodeFile="TravelRequest.aspx.cs" Inherits="TravelRequest" EnableSessionState="True" %>

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
           background:#ebebe4;
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
        });
    </script>
    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Travel Request"></asp:Label>
                    </span>
                </div>
                <div>
                     <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>                
                <div>
                    <span>
                    <a href="travelindex.aspx" class="aaaa" >Travel Index</a>
                    </span>
                </div>
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
                        <li class="trvl_type">
                            <span>Travel Type</span>&nbsp;&nbsp;<span style="color: red">*</span><br />

                            <asp:TextBox ID="txtTriptype" AutoComplete="off" runat="server" CssClass="Dropdown"></asp:TextBox>
                            <asp:Panel ID="Panel3" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstTripType" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstTripType_SelectedIndexChanged">
                                            <%--<asp:ListItem>Domestic</asp:ListItem>
                                            <asp:ListItem>International</asp:ListItem>--%>
                                        </asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender2" PopupControlID="Panel3" TargetControlID="txtTriptype"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>
                        </li>
                        <li class="trvl_date">
                            <span>From </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />

                            <asp:TextBox AutoComplete="off" ID="txtFromdate" runat="server" AutoPostBack="True" OnTextChanged="txtFromdate_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtFromdate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>
                        <li class="trvl_date">
                            <span>To</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtToDate" runat="server" AutoPostBack="True" OnTextChanged="txtToDate_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>

                        <li class="trvl_Reason">
                            <span>Reason for Travel </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox ID="txtReason" AutoComplete="off" runat="server" MaxLength="30"></asp:TextBox>
                        </li>

                        <li class="trvl_Advances">
                            <span>Advances Required: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtAdvance" runat="server" MaxLength="10"></asp:TextBox>
                        </li>
                        <li class="trvl_Currency">
                            <span id="lbl_cur" runat="server" >Currency Required:</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtreqCur" runat="server" MaxLength="10"></asp:TextBox>
                        </li>

                        <li class="trvL_detail">
                            <asp:LinkButton ID="btnTra_Details" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" OnClick="btnTra_Details_Click"></asp:LinkButton>
                            <span>Travel Details:</span>
                        </li>
                        <li class="trvl_inboxEmpCode">
                            <asp:TextBox AutoComplete="off" ID="TextBox2" runat="server" Visible="false"></asp:TextBox>
                        </li>
                        <li class="trvl_inboxEmpCode">
                            <asp:TextBox AutoComplete="off" ID="TextBox5" runat="server" Visible="false"></asp:TextBox>
                        </li>

                        <div id="DivTrvl" class="edit-contact" runat="server" visible="false">
                            <ul>
                                <li>
                                    <span>Mode of Travel</span>&nbsp;&nbsp;<span style="color:red">*</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtTravelMode" runat="server" CssClass="Dropdown"></asp:TextBox>
                                    <asp:Panel ID="Panel2" Style="display: none;" runat="server" CssClass="taskparentclass3">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:ListBox ID="lstTravelMode" runat="server" CssClass="taskparentclass3" AutoPostBack="true" OnSelectedIndexChanged="lstTravelMode_SelectedIndexChanged"></asp:ListBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>

                                    <ajaxToolkit:PopupControlExtender ID="PopupControlExtender1" PopupControlID="Panel2" TargetControlID="txtTravelMode"
                                        Position="Bottom" runat="server">
                                    </ajaxToolkit:PopupControlExtender>
                                </li>

                                <li class="trvldetails_arrivaldate">
                                    <span>Entitlement</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtDeviation"  runat ="server" MaxLength="10" BackColor="#ebebe4"></asp:TextBox>
                                </li>
                                <li class="trvldetails_type">
                                    <%--<span>Type of Travel</span>&nbsp;&nbsp;<span style="color:red">*</span><br />--%>
                                    <asp:TextBox AutoComplete="off" ID="txtTravelType" Visible="false" runat="server" CssClass="grayDropdown"></asp:TextBox>
                                </li>
                                <li class="trvldetails_departuredate">
                                    <span>Departure Date </span>&nbsp;&nbsp;<span style="color:red">*</span>
                                    <br />

                                    <asp:TextBox AutoComplete="off" ID="txtFromdate_Trvl" runat="server" AutoPostBack="true" OnTextChanged="txtFromdate_Trvl_TextChanged"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" Format="dd/MM/yyyy" TargetControlID="txtFromdate_Trvl"
                                        runat="server">
                                    </ajaxToolkit:CalendarExtender>
                                    <%--  <asp:TextBox AutoComplete="off" ID="txtsadate" runat="server" CssClass="txtdatepicker" placeholder="Start Date" OnTextChanged ="txtsadate_TextChanged" AutoPostBack ="true" ></asp:TextBox>
                                        <span class="texticon tooltip" title="Select Start Date. Date should be MM/DD/YYYY format."><i class="fa fa-calendar" aria-hidden="true"></i></span>
                                        <asp:Label ID="lblsdate" runat="server" Text="" Font-Size="15px"></asp:Label>--%>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="formerror" ControlToValidate="txtsadate" SetFocusOnError="True" ErrorMessage="Please enter start date" ValidationGroup="validate"></asp:RequiredFieldValidator>--%>
                                </li>
                                <li class="trvldetails_place">
                                    <span>Place</span>&nbsp;&nbsp;<span style="color:red">*</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtOrigin" runat="server"  placeholder="Select Place"></asp:TextBox>
                                    <asp:TextBox AutoComplete="off" ID="TextBox14" runat="server" CssClass="Dropdown" Visible="false"></asp:TextBox>
                                    <asp:Panel ID="Panel4" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:ListBox ID="lstOrigin" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstOrigin_SelectedIndexChanged"></asp:ListBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>

                                    <ajaxToolkit:PopupControlExtender ID="PopupControlExtender3" PopupControlID="Panel4" TargetControlID="TextBox5"
                                        Position="Bottom" runat="server">
                                    </ajaxToolkit:PopupControlExtender>

                                <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchCities" MinimumPrefixLength="1"
                                    CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtOrigin"
                                    ID="AutoCompleteExtender1" runat="server" FirstRowSelected="true">
                                </ajaxToolkit:AutoCompleteExtender>

                                </li>

                                 <li class="trvldetails_requirement">
                                    <span>Estimated Departure Date / Time : </span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtExpDepartDate" runat="server" MaxLength="40"></asp:TextBox>
                                </li>

                                <li class="trvldetails_arrivaldate">
                                    <span>Arrival Date</span>&nbsp;&nbsp;<span style="color:red">*</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtToDate_Trvl" runat="server" AutoPostBack="True" OnTextChanged="txtToDate_Trvl_TextChanged"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" Format="dd/MM/yyyy" TargetControlID="txtToDate_Trvl"
                                        runat="server">
                                    </ajaxToolkit:CalendarExtender>
                                </li>

                                <li class="trvldetails_place2">

                                    <span>Place</span>&nbsp;&nbsp;<span style="color:red">*</span><br />

                                    <asp:TextBox AutoComplete="off" ID="txtDestination" runat="server" placeholder="Select Place"></asp:TextBox>
                                    <asp:TextBox AutoComplete="off" ID="TextBox16" runat="server" Visible="false" CssClass="Dropdown"></asp:TextBox>
                                    <asp:Panel ID="Panel5" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <asp:ListBox ID="lstDestination" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstDestination_SelectedIndexChanged"></asp:ListBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>

                                    <ajaxToolkit:PopupControlExtender ID="PopupControlExtender4" PopupControlID="Panel5" TargetControlID="TextBox16"
                                        Position="Bottom" runat="server">
                                    </ajaxToolkit:PopupControlExtender>

                                    <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchCities" MinimumPrefixLength="1"
                                    CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtDestination"
                                    ID="AutoCompleteExtender2" runat="server" FirstRowSelected="true">
                                </ajaxToolkit:AutoCompleteExtender>


                                </li>

                                <li class="trvldetails_requirement">
                                    <span>Estimated Arrival Date/ Time : </span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtExpArrivalDate" runat="server" MaxLength="40"></asp:TextBox>
                                </li>
                                <li class="trvldetails_requirement">
                                    <span>Other Requirements: </span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtRequirememt" runat="server" MaxLength="30"></asp:TextBox>
                                </li>
                                <li class="trvldetails_bookthrough">
                                    <span>Book through Travel Desk:</span>

                                    <asp:CheckBox ID="chkCOS" runat="server" Checked="True" />
                                </li>
                                <li class="trvldetails_deviation">

                                    <%-- <span>Deviation</span><br />--%>

                                    <asp:TextBox AutoComplete="off" ID="TextBox17" runat="server" Visible="false"></asp:TextBox>

                                </li>
                                <li>
                                    <asp:LinkButton ID="trvldeatils_btnSave" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve"  OnClientClick=" return MultiClick();"  OnClick="trvldeatils_btnSave_Click"></asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton ID="trvldeatils_delete_btn" runat="server" Text="Delete" ToolTip="Delete" CssClass="Savebtnsve" OnClick="trvldeatils_delete_btn_Click"></asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton ID="trvldeatils_cancel_btn" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="Savebtnsve" OnClick="trvldeatils_cancel_btn_Click"></asp:LinkButton>
                                </li>
                            </ul>
                        </div>

                        <li class="trvl_grid">

                            <div>

                                <asp:GridView ID="dgTravelRequest" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                     DataKeyNames="trip_id,trip_dtls_id" OnRowCreated="dgTravelRequest_RowCreated" OnRowDataBound="dgTravelRequest_RowDataBound">
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
                                            ItemStyle-Width="17%"
                                            ItemStyle-BorderColor="Navy"
                                             />

                                        <asp:BoundField HeaderText="Dep Date"
                                            DataField="departure_date"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" 
                                            ItemStyle-BorderColor="Navy"
                                             />

                                        <asp:BoundField HeaderText="Dep Place"
                                            DataField="departure_place"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" 
                                            ItemStyle-BorderColor="Navy"
                                             />

                                        <asp:BoundField HeaderText="Arr Date"
                                            DataField="arrival_date"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%"
                                            ItemStyle-BorderColor="Navy"
                                             />
                                        <asp:BoundField HeaderText="Arr Place"
                                            DataField="arrival_place"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy"/>
                                        <%-- <asp:BoundField HeaderText="Through Travel Desk"
                                            DataField="travel_through_desk"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="10%" />--%>
                                        <asp:TemplateField HeaderText="Through Travel Desk" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                               <%-- <asp:CheckBox ID="ChkCOS" runat="server"  Enabled="false" Checked='<%#Convert.ToBoolean(Eval("tt")) %>' />--%>
                                                <asp:Label ID="ChkCOS" runat="server"  Text='<%#Eval("travel_through_desk")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Deviation"
                                            DataField="deviation"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="8%" ItemStyle-BorderColor="Navy"/>
                                       
                                          <asp:BoundField HeaderText="Status"
                                            DataField="Status"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%"
                                            ItemStyle-BorderColor="Navy"
                                             />

                                        <asp:TemplateField HeaderText="Details" ItemStyle-HorizontalAlign="Center"> 
                                            <ItemTemplate>
                                                <%--<asp:LinkButton ID="lnkTravelDetailsoldEdit" runat="server" Text='View' OnClick="lnkTravelDetailsEdit_Click"></asp:LinkButton>--%>
                                                <asp:ImageButton id="lnkTravelDetailsEdit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkTravelDetailsEdit_Click"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:ImageButton id="btn_del_Trvl" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/delete.png" OnClick="btn_del_Trvl_Click"/>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>

                            </div>

                        </li>

                        <li class="trvl_Accomodation">
                            <asp:LinkButton ID="trvl_accmo_btn" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" OnClick="trvl_accmo_btn_Click"></asp:LinkButton>
                            <span>Accommodation: </span>
                        </li>
                        <li class="trvl_inboxEmpCode">
                            <asp:TextBox AutoComplete="off" ID="TextBox8" runat="server" Visible="false"></asp:TextBox>
                        </li>
                        <li class="trvl_inboxEmpCode">
                            <asp:TextBox AutoComplete="off" ID="TextBox9" runat="server" Visible="false"></asp:TextBox>
                        </li>

                        <div id="DivAccm" class="edit-contact" runat="server" visible="false">
                            <ul>
                                <li class="Accomodation_trip">
                                    <asp:TextBox AutoComplete="off" ID="txtTripId" runat="server" Visible="false"></asp:TextBox>
                                </li>
                                <li class="Accomodation_trip">
                                    <asp:TextBox AutoComplete="off" ID="txtTravelType_Accm" runat="server" CssClass="Dropdown" Visible ="false"></asp:TextBox>
                                </li>
                                <li class="trvl_inboxEmpCode">
                                    <asp:TextBox AutoComplete="off" ID="TextBox4" runat="server" Visible="false"></asp:TextBox>
                                </li>
                                <li class="Accomodation_fromdate">
                                    <span>From </span>&nbsp;&nbsp;<span style="color:red">*</span>
                                    <br />

                                    <asp:TextBox AutoComplete="off" ID="txtFromdate_Accm" runat="server" AutoPostBack="True" OnTextChanged="txtFromdate_Accm_TextChanged"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" Format="dd/MM/yyyy" TargetControlID="txtFromdate_Accm"
                                        runat="server">
                                    </ajaxToolkit:CalendarExtender>
                                </li>
                                <li class="Accomodation_date">
                                    <span>To</span>&nbsp;&nbsp;<span style="color:red">*</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtToDate_Accm" runat="server" AutoPostBack="True" OnTextChanged="txtToDate_Accm_TextChanged"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender6" Format="dd/MM/yyyy" TargetControlID="txtToDate_Accm"
                                        runat="server">
                                    </ajaxToolkit:CalendarExtender>
                                </li>
                                <li class="Accomodation_location">
                                    <span>Location </span>&nbsp;&nbsp;<span style="color:red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtLocation" runat="server" placeholder="Select Location" ></asp:TextBox>
                                    <asp:TextBox AutoComplete="off" ID="TextBox6" runat="server" Visible="false" CssClass="Dropdown"></asp:TextBox>
                                      <asp:Panel ID="Panel1" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <asp:ListBox ID="lstLocation" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstLocation_SelectedIndexChanged">
                                                    <%--   <asp:ListItem>Domestic</asp:ListItem>
                                                    <asp:ListItem>International</asp:ListItem>--%>

                                                </asp:ListBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>

                                    <ajaxToolkit:PopupControlExtender ID="PopupControlExtender5" PopupControlID="Panel1" TargetControlID="TextBox6"
                                        Position="Bottom" runat="server">
                                    </ajaxToolkit:PopupControlExtender>

                                     <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchCities" MinimumPrefixLength="1"
                                    CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtLocation"
                                    ID="AutoCompleteExtender3" runat="server" FirstRowSelected="true">
                                    </ajaxToolkit:AutoCompleteExtender>

                                </li>
                                <li class="Accomodation_requirement">
                                    <span>Requirement: </span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtRequirement" runat="server" MaxLength="30"></asp:TextBox>
                                </li>
                                <li class="acc_book">
                                    <span>Book through COS:</span>                          
                                    <span><asp:CheckBox ID="chkCos_Accm" runat ="server" Checked ="True" /></span> 
                                </li>
                                 <li class="Accomodation_trip">
                                    <asp:TextBox AutoComplete="off" ID="TextBox7" runat="server" Visible="false"></asp:TextBox>
                                </li>
                                <li>
                                    <asp:LinkButton ID="accmo_btnSave" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClientClick=" return MultiClickAccm();"  OnClick="accmo_btnSave_Click">Submit</asp:LinkButton>

                                </li>
                                <li>
                                    <asp:LinkButton ID="accmo_delete_btn" runat="server" Text="Delete" ToolTip="Delete" CssClass="Savebtnsve" OnClick="accmo_delete_btn_Click">Delete</asp:LinkButton>

                                </li>
                                <li>
                                    <asp:LinkButton ID="accmo_cancel_btn" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="Savebtnsve" OnClick="accmo_cancel_btn_Click">Cancel</asp:LinkButton>

                                </li>

                            </ul>
                        </div>
                        <li class="trvl_inboxEmpCode">
                            <asp:TextBox AutoComplete="off" ID="TextBox13" runat="server" Visible="false"></asp:TextBox>
                        </li>
                        <li class="trvl_grid">

                            <div>

                                <asp:GridView ID="dgAccomodation" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                   DataKeyNames="trip_id,local_accomodation_id" OnRowCreated="dgAccomodation_RowCreated" OnRowDataBound="dgAccomodation_RowDataBound">
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
                                            ItemStyle-Width="25%" 
                                            ItemStyle-BorderColor="Navy"
                                             />

                                        <asp:BoundField HeaderText="To Date"
                                            DataField="To Date"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="25%" 
                                            ItemStyle-BorderColor="Navy"
                                             />

                                        <asp:BoundField HeaderText="Location"
                                            DataField="Location"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="30%" 
                                            ItemStyle-BorderColor="Navy"
                                             />

                                        <asp:TemplateField HeaderText="Through COS" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%--<asp:CheckBox ID="ChkCOS" runat="server" Enabled="false" Checked='<%#Convert.ToBoolean(Eval("tt")) %>' />--%>
                                                <asp:Label ID="ChkCOS" runat="server"  Text='<%#Eval("travel_through_desk")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                          <asp:BoundField HeaderText="Status"
                                            DataField="Status"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%"
                                            ItemStyle-BorderColor="Navy"
                                             />

                                        <asp:TemplateField HeaderText="Details" ItemStyle-HorizontalAlign="Center"> 
                                            <ItemTemplate>
                                                <%--<asp:LinkButton ID="lnkAccomodationedit" runat="server" Text='View' OnClick="lnkAccomodationdit_Click"></asp:LinkButton>--%>
                                                <asp:ImageButton id="lnkAccomodationdit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkAccomodationdit_Click"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:ImageButton id="btn_del_Accm" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/delete.png" OnClick="btn_del_Accm_Click"/>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>

                            </div>

                        </li>

                        <li class="trvl_local">
                            <asp:LinkButton ID="trvl_localbtn" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" OnClick="trvl_localbtn_Click"></asp:LinkButton>
                            <span>Local Travel: </span>
                        </li>
                        <li class="trvl_inboxEmpCode">
                            <asp:TextBox AutoComplete="off" ID="TextBox10" runat="server" Visible="false"></asp:TextBox>
                        </li>
                        <li class="trvl_inboxEmpCode">
                            <asp:TextBox AutoComplete="off" ID="TextBox11" runat="server" Visible="false"></asp:TextBox>
                        </li>

                        <div id="Div_Locl" class="edit-contact" runat="server" visible="false">
                            <ul>
                                <li class="localtrvl_trip">
                                    <asp:TextBox AutoComplete="off" ID="txtTripId_Locl" runat="server" Visible="false"></asp:TextBox>
                                </li>
                                <li class="localtrvl_type">
                                    <asp:TextBox AutoComplete="off" ID="txtTravelType_Locl" runat="server" CssClass="Dropdown" Visible=" false"></asp:TextBox>
                                </li>
                                <li class="localtrvl_trip">
                                    <asp:TextBox AutoComplete="off" ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
                                </li>
                                <li class="localtrvl_fromdate">
                                    <span>From </span>&nbsp;&nbsp;<span style="color:red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtFromdate_Locl" runat="server" AutoPostBack="True" OnTextChanged="txtFromdate_Locl_TextChanged"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender7" Format="dd/MM/yyyy" TargetControlID="txtFromdate_Locl"
                                        runat="server">
                                    </ajaxToolkit:CalendarExtender>
                                </li>
                                <li class="localtrvl_date">
                                    <span>To</span>&nbsp;&nbsp;<span style="color:red">*</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtToDate_Locl" runat="server" AutoPostBack="True" OnTextChanged="txtToDate_Locl_TextChanged"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender8" Format="dd/MM/yyyy" TargetControlID="txtToDate_Locl"
                                        runat="server">
                                    </ajaxToolkit:CalendarExtender>
                                </li>
                                <li class="localtrvl_location">
                                    <span>Location </span>&nbsp;&nbsp;<span style="color:red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtLocation_Locl" runat="server" ></asp:TextBox>
                                    <asp:TextBox AutoComplete="off" ID="TextBox12" runat="server" Visible="false"  placeholder="Select Location"  CssClass="Dropdown"></asp:TextBox>
                                     <asp:Panel ID="Panel6" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <ContentTemplate>
                                                <asp:ListBox ID="lstLocation_Locl" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstLocation_Locl_SelectedIndexChanged">
                                                    <%--   <asp:ListItem>Domestic</asp:ListItem>
                                                    <asp:ListItem>International</asp:ListItem>--%>

                                                </asp:ListBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>

                                    <ajaxToolkit:PopupControlExtender ID="PopupControlExtender6" PopupControlID="Panel6" TargetControlID="TextBox12"
                                        Position="Bottom" runat="server">
                                    </ajaxToolkit:PopupControlExtender>

                                     <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchCities" MinimumPrefixLength="1"
                                    CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="TextBox12"
                                    ID="AutoCompleteExtender4" runat="server" FirstRowSelected="true">
                                </ajaxToolkit:AutoCompleteExtender>

                                </li>
                                <li class="localtrvl_requirement">
                                    <span>Requirement: </span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtRequirement_Locl" runat="server" MaxLength="30"></asp:TextBox>
                                </li>
                                <li class="localtrvl_book">
                                    <span>Book through COS:</span>
                                    <span>
                                    <asp:CheckBox ID="localtevl_chkCOS" runat="server" Checked="True" /></span>
                                </li>
                                <li class="localtrvl_requirement">
                                    <asp:TextBox AutoComplete="off" ID="TextBox3" runat="server" Visible="false" MaxLength="30"></asp:TextBox>
                                </li>

                                <li class="localtrvl_requirement">
                                    <asp:LinkButton ID="localtrvl_btnSave" runat="server" Text="Submit" ToolTip="Save"  OnClientClick=" return MultiClick_Locl();" CssClass="Savebtnsve" OnClick="localtrvl_btnSave_Click">Submit</asp:LinkButton>
                                </li>

                                <li class="localtrvl_requirement">
                                    <asp:LinkButton ID="localtrvl_delete_btn" runat="server" Text="Delete" ToolTip="Cancel" CssClass="Savebtnsve" OnClick="localtrvl_delete_btn_Click"></asp:LinkButton>
                                </li>

                                <li class="localtrvl_requirement">
                                    <asp:LinkButton ID="localtrvl_cancel_btn" runat="server" Text="back" ToolTip="back" CssClass="Savebtnsve" OnClick="localtrvl_cancel_btn_Click"></asp:LinkButton>
                                </li>

                            </ul>
                        </div>
                        <li class="trvl_inboxEmpCode">
                            <asp:TextBox AutoComplete="off" ID="TextBox15" runat="server" Visible="false"></asp:TextBox>
                        </li>
                        <li class="trvl_grid">

                            <div>

                                <asp:GridView ID="dgLocalTravel" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                    DataKeyNames="trip_id,local_travel_id" OnRowCreated="dgLocalTravel_RowCreated" OnRowDataBound="dgLocalTravel_RowDataBound">
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
                                            ItemStyle-Width="25%"
                                            ItemStyle-BorderColor="Navy"
                                             />

                                        <asp:BoundField HeaderText="To Date"
                                            DataField="To Date"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="25%"
                                            ItemStyle-BorderColor="Navy"
                                             />

                                        <asp:BoundField HeaderText="Location"
                                            DataField="Location"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="30%"
                                            ItemStyle-BorderColor="Navy"
                                             />
                                        <asp:TemplateField HeaderText="Through COS">
                                            <ItemTemplate>
                                                <%--<asp:CheckBox ID="ChkCOS" runat="server" Enabled="false" Checked='<%#Convert.ToBoolean(Eval("tt")) %>' />--%>
                                                <asp:Label ID="Label1" runat="server"  Text='<%#Eval("is_thorugh_cos")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                          <asp:BoundField HeaderText="Status"
                                            DataField="Status"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%"
                                            ItemStyle-BorderColor="Navy"
                                             />

                                        <asp:TemplateField HeaderText="Details" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%--<asp:LinkButton ID="lnkLocalTravleOldEdit" runat="server" Text='View' OnClick="lnkLocalTravleEdit_Click">--%>
                                                <asp:ImageButton id="lnkLocalTravleEdit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkLocalTravleEdit_Click"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:ImageButton id="btn_del_Locl" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/delete.png" OnClick="btn_del_Locl_Click"/>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>

                            </div>

                        </li>

                        <li class="trvl_Approver">
                            <span>Approver </span>
                            <br />
                            <asp:ListBox ID="lstApprover" runat="server"></asp:ListBox>
                        </li>

                        <li class="trvl_inter" style="display:none;">
                            <span>For Information To </span>
                            <br />
                            <asp:ListBox ID="lstIntermediate" runat="server"></asp:ListBox>
                        </li>
                           <li class="trvl_Approver">
                            <span id="sprtfunctions" runat="server" visible="false">Support Functions </span>
                            <br />
                            <asp:ListBox ID="lstApprover_suprt" runat="server" Enabled="false" Visible="false"></asp:ListBox>
                        </li>
                    </ul>
                </div> 
            </div>
        </div>
    </div>
    <div class="trvl_Savebtndiv">
        <asp:LinkButton ID="trvl_btnSave" runat="server" Text="Submit" ToolTip="Save" OnClientClick="return SaveMultiClick();" CssClass="Savebtnsve" OnClick="trvl_btnSave_Click">Submit</asp:LinkButton>
        <asp:LinkButton ID="lnksubTrvlReqst_exp" runat="server"  Visible="false"  CssClass="aaaa"  Text="Submit Travel Expenses" OnClick="lnksubTrvlReqst_exp_Click"></asp:LinkButton>

         <%-- Following Popup for Sending Travel Request --%>
        <%--<ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" DisplayModalPopupID="mpe" TargetControlID="trvl_btnSave">
        </ajaxToolkit:ConfirmButtonExtender>
        <ajaxToolkit:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="trvl_btnSave" OkControlID = "btnYes"
            CancelControlID="btnNo" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Confirmation
            </div>
            <div class="body">
                Do you want to Submit ?
            </div>
            <div class="footer" align="right">
                <asp:Button ID="btnNo" runat="server" Text="No" />
                <asp:Button ID="btnYes" runat="server" Text="Yes" />
            </div>
        </asp:Panel>--%>
        <%-- End Here --%>

    </div>
    <div  class="trvl_Savebtndiv">
        <asp:LinkButton ID="btnMod" runat="server" Text="Submit" ToolTip="Save"    CssClass="Savebtnsve" OnClientClick="return ModifyMultiClick();" OnClick="btnMod_Click">Modify</asp:LinkButton>
        <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="Savebtnsve" OnClientClick="return CancelMultiClick();"  OnClick="btnCancel_Click">Cancel</asp:LinkButton>
        <asp:LinkButton ID="btnback_mng" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/ManageTravelRequest.aspx" >Back</asp:LinkButton>

          <%-- Following Popup for Modify Travel Request --%>
   <%--     <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" DisplayModalPopupID="mpe_Mod" TargetControlID="btnMod">
        </ajaxToolkit:ConfirmButtonExtender>
        <ajaxToolkit:ModalPopupExtender ID="mpe_Mod" runat="server" PopupControlID="pnlPopup_Mod" TargetControlID="btnMod" OkControlID = "btnYes_Mod"
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
        </asp:Panel>--%>


        <%-- Following Popup for Cancel Travel Request --%>
      <%--  <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" DisplayModalPopupID="mpe_Cancel" TargetControlID="btnCancel">
        </ajaxToolkit:ConfirmButtonExtender>
        <ajaxToolkit:ModalPopupExtender ID="mpe_Cancel" runat="server" PopupControlID="pnlPopup_Cancel" TargetControlID="btnCancel" OkControlID = "btnYes_CLR"
            CancelControlID="btnNo_CLR" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup_Cancel" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Confirmation
            </div>
            <div class="body">
                Do you want to Submit?
            </div>
            <div class="footer" align="right">                                
                <asp:Button ID="btnNo_CLR" runat="server" Text="No" />
                <asp:Button ID="btnYes_CLR" runat="server" Text="Yes" />                
            </div>
        </asp:Panel>--%>
        <%-- End Here --%>
    </div>
    <asp:TextBox ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
    <asp:TextBox ID="txtEmpName" runat="server" Visible="false"></asp:TextBox>

    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />

    <asp:HiddenField ID="hflEmpName" runat="server" />

    <asp:HiddenField ID="hflEmpDesignation" runat="server" />

    <asp:HiddenField ID="hflEmpDepartment" runat="server" />

    <asp:HiddenField ID="hflEmailAddress" runat="server" />

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

    <asp:HiddenField ID="hdnApprId" runat="server" />

    <asp:HiddenField ID="hdnApprEmailaddress" runat="server" />

    <asp:HiddenField ID="hdnEligible" runat="server" />
    <asp:HiddenField ID="hdnTrdays" runat="server" />
    <asp:HiddenField ID="hdnTravelDtlsId" runat="server" />
    <asp:HiddenField ID="hdnAccId" runat="server" />
    <asp:HiddenField ID="hdnLocalId" runat="server" />
    <asp:HiddenField ID="hdnTravelstatus" runat="server" />
    <asp:HiddenField ID="hdnLeavestatusValue" runat="server" />
    <asp:HiddenField ID="hdnLeavestatusId" runat="server" />
    <asp:HiddenField ID="hdnIsApprover" runat="server" />
    <asp:HiddenField ID="hdnTRApprverstatus" runat="server" />
    <asp:HiddenField ID="hdnfrmdate_emial" runat="server" />
    <asp:HiddenField ID="hdntodate_emial" runat="server" />
    <asp:HiddenField ID="hdnActualTrvlDays" runat="server" />
    <asp:HiddenField ID="hdnTDCOSStatus" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="hdnbtnStatus" runat="server" />

    <asp:HiddenField ID="hdnTryiptypeid" runat="server" />
    <asp:HiddenField ID="hdnfromdate" runat="server" />
    <asp:HiddenField ID="hdnTodate" runat="server" />
    <asp:HiddenField ID="hdntrdetailsid" runat="server" />
    <asp:HiddenField ID="hdnCOS" runat="server" />
    <asp:HiddenField ID="hdnCOS_Locl" runat="server" />
    <asp:HiddenField ID="hdnCOS_Accm" runat="server" />
    <asp:HiddenField ID="hdnAccdtlsid" runat="server" />
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <script type="text/javascript">

        function validateTripType(triptypeid)
        {
          // alert(triptypeid);
            
        

         if (triptypeid == "1")
            {
                document.getElementById("<%=txtreqCur.ClientID%>").value = "";
                //document.getElementById("<%=txtreqCur.ClientID%>").disabled = true;
                document.getElementById("<%=txtreqCur.ClientID%>").style.visibility = "hidden";
                document.getElementById("<%=lbl_cur.ClientID%>").style.visibility = "hidden";
            }
            else
            {
                document.getElementById("<%=txtreqCur.ClientID%>").value = "";
                //document.getElementById("<%=txtreqCur.ClientID%>").disabled = false;
                document.getElementById("<%=txtreqCur.ClientID%>").style.visibility = "visible";
                document.getElementById("<%=lbl_cur.ClientID%>").style.visibility = "visible";
                //document.getElementById("<%=txtreqCur.ClientID%>").style.backgroundColor = white;
          } 
            return;

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

        function ModifyMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=btnMod.ClientID%>');

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


        function MultiClick() {
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
                document.getElementById("<%=hdnDeviation.ClientID%>").value = Deviation;

            }
            return;
        }

        function MultiClickAccm() {
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

    </script>
</asp:Content>

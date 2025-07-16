<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" EnableViewState="true"
    CodeFile="AppTravelrequest.aspx.cs" Inherits="AppTravelrequest" %>

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

        .grayDropdown {
            border-bottom: 2px solid #cccccc;
            /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
            background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;            
            background-color:#ebebe4;
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
                        <asp:Label ID="lblheading" runat="server" Text="Approver Travel Request View"></asp:Label>
                    </span>
                </div>

                    <%--<a href="travelindex.aspx" class="aaab">Travel Index</a>--%>
                <span id="idspnTemCalendar" runat="server">
                    <a href="teamcalender_Travel.aspx" class="aaaa" >Team Calendar</a>
                </span>

                <div class="edit-contact">
                    <%--<div class="editprofile" style="text-align: center; border: none;" id="divmsg" runat="server" visible="false">
                        <asp:Label runat="server" ID="lblmessage" Visible="true" Style="color: green; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                    </div>--%>
                   <div class="editprofile" style="text-align: center; border: none;" >
                        <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                    </div>
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" OnClick="lnkcont_Click"></asp:LinkButton>
                        </div>
                    </div>

                    <ul id="editform" runat="server" >
                       
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
                        </li>
                        <li class="trvl_date">
                            <span>From </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtFromdate" runat="server" AutoPostBack="True"  Enabled="false"></asp:TextBox>
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
                            <asp:TextBox AutoComplete="off" ID="txtToDate" runat="server" AutoPostBack="True"  Enabled="false"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>

                        <li class="trvl_Reason">
                            <span>Reason for Travel </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtReason" runat="server" MaxLength="100" Enabled="false"></asp:TextBox>
                        </li>

                        <li class="trvL_detail">
                            <asp:LinkButton ID="btnTra_Details" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve"  Visible="false" OnClick="btnTra_Details_Click"></asp:LinkButton>
                            <span id="spntrvldtls" runat="server">Travel Details: </span>
                        </li>

                        <li class="trvl_grid">

                            <div>
                                <asp:GridView ID="dgTravelRequest" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false" OnRowDataBound="dgTravelRequest_RowDataBound"
                                    DataKeyNames="trip_id,trip_dtls_id"  OnRowCreated="dgTravelRequest_RowCreated">
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
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy"/>

                                        <asp:BoundField HeaderText="Dep Date"
                                            DataField="departure_date"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>

                                        <asp:BoundField HeaderText="Dep Place"
                                            DataField="departure_place"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy"/>

                                        <asp:BoundField HeaderText="Arr Date"
                                            DataField="arrival_date"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>
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
                                                <asp:CheckBox ID="ChkCOS" runat="server" Visible="false" Checked='<%#Convert.ToBoolean(Eval("tt")) %>' />
                                                 <asp:Label ID="lblchkcos" runat="server"  Text='<%#Eval("travel_through_desk")%>' />
                                            </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Deviation"
                                            DataField="deviation"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="8%" ItemStyle-BorderColor="Navy"/>
                                        <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>                                                
                                                <asp:Label ID="lbltrvlbookstatus" runat="server" Text='<%#Eval("Status")%>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Details" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                  <asp:LinkButton ID="lnktravlsDtlsEidt" runat="server" Text="View" OnClick="lnktravlsDtlsEidt_Click">View</asp:LinkButton>                                                
                                            </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                        </asp:TemplateField>

                                      
                                    </Columns>
                                </asp:GridView>

                            </div>
   
                        </li>


                        <li class="trvl_Advances">
                            <span id="spnadvreq" runat="server">Advances Required: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtAdvance" runat="server" MaxLength="100" Enabled="false"></asp:TextBox>
                        </li>
                         <li class="trvl_Currency">
                            <span id="lbl_cur" runat="server">Currency Required:</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtreqCur" runat="server" MaxLength="100" Enabled="false"></asp:TextBox>
                        </li>
                         <li class="trvl_Advances">
                              <span id="Span1" runat="server">Account Details: </span>
                             <br />
                             <asp:TextBox AutoComplete="off" ID="txtAccountDetails" Width="50%" Height="50" runat="server"   TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                </li>
                        <li class="trvl_Currency"> </li>
                        <li class="trvl_Advances">
                              <span id="spnadvAmtbyAcc" visible="false" runat="server">Advance Amount by Account </span>
                            <br />
                            <asp:TextBox AutoComplete="off" visible="false" ID="txtAdv_Approved_Amt" runat="server" MaxLength="10"></asp:TextBox>
                        </li>
                        <li class="trvl_Currency">                         
                            <asp:TextBox AutoComplete="off" Visible="false" ID="TextBox2" runat="server" MaxLength="100" Enabled="false"></asp:TextBox>
                        </li>

                        <li class="trvl_Accomodation">
                            <asp:LinkButton ID="trvl_accmo_btn" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve"  Visible="false"></asp:LinkButton>
                            <span id="spnaccomd" runat="server">Accommodation: </span>
                        </li>
                        <li class="trvl_Currency">                         
                            <asp:TextBox AutoComplete="off" Visible="false" ID="TextBox1" runat="server" MaxLength="100" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="trvl_Currency">                         
                            <asp:TextBox AutoComplete="off" Visible="false" ID="TextBox3" runat="server" MaxLength="100" Enabled="false"></asp:TextBox>
                        </li>

                        <div id="DivAccm" class="edit-contact" runat="server" visible="false">
                            <ul>
                                <li class="Accomodation_trip">
                                    <%--<span>Trip ID</span><br />--%>
                                    <asp:TextBox AutoComplete="off" ID="txtTripId_Accm" runat="server" Visible="false"></asp:TextBox>
                                </li>
                                <li class="Accomodation_type" >
                                    <%--<span>Travel Type</span><br />--%>
                                    <asp:TextBox AutoComplete="off" ID="txtTravelType_Accm" runat="server" CssClass="Dropdown" Visible ="false"></asp:TextBox>
                                </li>
                                <li class="Accomodation_trip">
                                    <%--<span>Trip ID</span><br />--%>
                                    <asp:TextBox AutoComplete="off" ID="TextBox4" runat="server" Visible="false"></asp:TextBox>
                                </li>
                                <li class="Accomodation_fromdate">
                                    <span>From </span>&nbsp;&nbsp;<span style="color:red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtFromdate_Accm" runat="server" ></asp:TextBox>
                                </li>
                                <li class="Accomodation_date">
                                    <span>To</span>&nbsp;&nbsp;<span style="color:red">*</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtToDate_Accm" runat="server"></asp:TextBox>
                                </li>
                                <li class="Accomodation_location">
                                    <span>Location </span>&nbsp;&nbsp;<span style="color:red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtLocation_Accm" runat="server" CssClass="Dropdown"></asp:TextBox>
                                </li>
                                <li class="Accomodation_date">
                                    <span>Requirement: </span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtRequirement_Accm" runat="server" MaxLength="30"></asp:TextBox>
                                </li>
                                <li class="Accomodation_date">
                                    <asp:TextBox AutoComplete="off" ID="TextBox5" runat="server" MaxLength="30" Visible="false"></asp:TextBox>
                                </li>
                                <li class="Accomodation_date">
                                    <asp:TextBox AutoComplete="off" ID="TextBox6" runat="server" MaxLength="30" Visible="false"></asp:TextBox>
                                </li>
                                 <li class="trvldetails_bookthrough">
                                    <span>Status:</span>&nbsp;&nbsp;<span style="color:red">*</span>

                                    <asp:TextBox AutoComplete="off" ID="txtStatus_Accm" runat="server" MaxLength="100" CssClass="Dropdown"></asp:TextBox>
                                    <asp:Panel ID="Panel6" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <asp:ListBox ID="lstStatus_Accm" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstStatus_Accm_SelectedIndexChanged">
                                                    <asp:ListItem>Booked</asp:ListItem>
                                                    <asp:ListItem>Not Booked</asp:ListItem>
                                                </asp:ListBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>

                                    <ajaxToolkit:PopupControlExtender ID="PopupControlExtender5" PopupControlID="Panel6" TargetControlID="txtStatus_Accm"
                                        Position="Bottom" runat="server">
                                    </ajaxToolkit:PopupControlExtender>
                                </li>
                                <li class="Accomodation_type" >
                                    <span>Accommodation Type</span><br />

                                    <asp:TextBox AutoComplete="off" ID="txtAcctype_Accm" runat="server" CssClass="Dropdown" Visible ="True"></asp:TextBox>
                                    <asp:Panel ID="Panel4" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:ListBox ID="lstAccType_Accm" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstAccType_Accm_SelectedIndexChanged">
                                                    <asp:ListItem>Hotel</asp:ListItem>
                                                    <asp:ListItem>Guest House (with Food)  </asp:ListItem>
                                                    <asp:ListItem>Guest House (without Food)</asp:ListItem>
                                                </asp:ListBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>

                                    <ajaxToolkit:PopupControlExtender ID="PopupControlExtender3" PopupControlID="Panel4" TargetControlID="txtAcctype_Accm"
                                        Position="Bottom" runat="server">
                                    </ajaxToolkit:PopupControlExtender>
                                </li>
                                 <li class="trvldetails_requirement">
                                    <span>Amount (INR): </span>&nbsp;&nbsp;<%--<span style="color:red">*</span>--%>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtfare_Accm" runat="server" MaxLength="10"></asp:TextBox>
                                </li>

                                 <li class="trvldetails_requirement">
                                    <asp:LinkButton ID="accmo_btnSave" runat="server" Text="Confirmation" ToolTip="Confirmation" CssClass="Savebtnsve" OnClick="accmo_btnSave_Click">Confirmation</asp:LinkButton>
                                </li>
                                 <li class="trvldetails_requirement">
                                    <asp:LinkButton ID="accmo_cancel_btn" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="accmo_cancel_btn_Click">Back</asp:LinkButton>
                                </li>
                                 <li class="trvldetails_requirement">
                                    <asp:LinkButton ID="accmo_delete_btn" runat="server" Text="Delete" ToolTip="Delete" Visible="false" CssClass="Savebtnsve" OnClick="accmo_delete_btn_Click">Delete</asp:LinkButton>
                                </li>
                            </ul>
                        </div>

                        <li class="trvl_grid">

                            <div>

                                <asp:GridView ID="dgAccomodation" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false" OnRowDataBound="dgAccomodation_RowDataBound"
                                    DataKeyNames="trip_id,local_accomodation_id" OnRowCreated="dgAccomodation_RowCreated">
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
                                        <asp:BoundField HeaderText="From Date"
                                            DataField="From Date"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="20%" ItemStyle-BorderColor="Navy"/>

                                        <asp:BoundField HeaderText="To Date"
                                            DataField="To Date"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="20%" ItemStyle-BorderColor="Navy"/>

                                        <asp:BoundField HeaderText="Location"
                                            DataField="Location"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="20%" ItemStyle-BorderColor="Navy"/>

                                        <%-- <asp:BoundField HeaderText="Through COS"
                                            DataField="Through COS"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="20%" />--%>

                                        <asp:TemplateField HeaderText="Through COS" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkCOS" runat="server" Visible="false" Checked='<%#Convert.ToBoolean(Eval("tt")) %>' />
                                                <asp:Label ID="lblchkcos" runat="server"  Text='<%#Eval("travel_through_desk")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>                                                
                                                <asp:Label ID="lbltrvlAccbookstatus" runat="server" Text='<%#Eval("Status")%>' ></asp:Label>
                                            </ItemTemplate>                                               
                                             <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Details" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkAccomodationdit" runat="server" Text='View' OnClick="lnkAccomodationdit_Click">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                        </asp:TemplateField>

                                      
                                    </Columns>
                                </asp:GridView>

                            </div>

                        </li>

                        <li class="trvl_local">
                            <asp:LinkButton ID="trvl_localbtn" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" Visible="false"></asp:LinkButton>
                            <span id="spnlcolTrvl" runat="server">Local Travel: </span>
                        </li>

                        <li class="trvl_grid">

                            <div>

                                <asp:GridView ID="dgLocalTravel" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false" OnRowDataBound="dgLocalTravel_RowDataBound"
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
                                            ItemStyle-Width="20%" ItemStyle-BorderColor="Navy"/>

                                        <asp:BoundField HeaderText="To Date"
                                            DataField="To Date"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="20%" ItemStyle-BorderColor="Navy"/>

                                        <asp:BoundField HeaderText="Location"
                                            DataField="Location"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="20%" ItemStyle-BorderColor="Navy"/>

                                        <%--  <asp:BoundField HeaderText="Through COS"
                                            DataField="Through COS"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="20%" />--%>

                                        <asp:TemplateField HeaderText="Through COS" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkCOS" runat="server" Visible="false" Checked='<%#Convert.ToBoolean(Eval("tt")) %>' />
                                                <asp:Label ID="lblchkcos" runat="server"  Text='<%#Eval("Through_COS")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>                                                
                                                <asp:Label ID="lblLocaltrvlbookstatus" runat="server" Text='<%#Eval("Status")%>'></asp:Label>
                                            </ItemTemplate>                                               
                                             <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                        </asp:TemplateField>
 
                                   <asp:TemplateField HeaderText="Details" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkLocalTravleEdit" runat="server" Text='View' OnClick="lnkLocalTravleEdit_Click">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                       <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                            </div>

                        </li>

                          <li class="trvl_Reason">
                            <span>Comment </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtComments" runat="server" MaxLength="30" ></asp:TextBox>
                        </li>

                          <li class="trvl_Reason">
                            <span id="spnscrrctnRmkrs" runat="server">Correction/Rejection Remarks </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtRemarks" runat="server" MaxLength="30"></asp:TextBox>

                           
                        </li>

                        <li class="trvl_Approver">
                            <span>Approver </span>
                            <br />
                            <asp:ListBox ID="lstApprover" runat="server" Enabled="false"></asp:ListBox>
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
        <asp:LinkButton ID="trvl_btnSave" runat="server"  Text="Submit" ToolTip="Save" CssClass="Savebtnsve" >Submit</asp:LinkButton>

    </div>
    <div class="trvl_Savebtndiv">

        <span>
            <asp:LinkButton ID="btnApprove" runat="server" Text="Approve" ToolTip="Approve"  CssClass="Savebtnsve" OnClientClick="return ApproverMultiClick();" OnClick="btnApprove_Click">Approve</asp:LinkButton>
            <asp:LinkButton ID="btnApprove_TDCOS" runat="server" Text="Approve" ToolTip="Confirmation" Visible="false" OnClientClick="return TDCOSApproverMultiClick();"  CssClass="Savebtnsve" OnClick="btnApprove_Click">Confirmation</asp:LinkButton>
        </span>

        <span>
            <asp:LinkButton ID="btnCorrection" runat="server" Text="Correction" ToolTip="" CssClass="Savebtnsve" OnClientClick="return SendforCorrectionMultiClick();" OnClick="btnCorrection_Click">Send for Modification</asp:LinkButton>
        </span>

        <span>
            <asp:LinkButton ID="btnReject" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve"  OnClientClick="return RejectMultiClick();" OnClick="btnReject_Click" >Reject</asp:LinkButton>
        </span>

        <span>
            <asp:LinkButton ID="btnBack" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="btnBack_Click">Back</asp:LinkButton>
        </span>


        <%-- Following Popup for Approved Travel Request --%>
     <%--   <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" DisplayModalPopupID="mpe_Appr" TargetControlID="btnApprove">
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


         <%-- Following Popup for Approved Travel Request for TD COS & ACC--%>
       <%-- <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender4" runat="server" DisplayModalPopupID="mpe_Appr_TDCOSACC" TargetControlID="btnApprove_TDCOS">
        </ajaxToolkit:ConfirmButtonExtender>
        <ajaxToolkit:ModalPopupExtender ID="mpe_Appr_TDCOSACC" runat="server" PopupControlID="pnlPopup_Appr_TDCOSACC" TargetControlID="btnApprove_TDCOS" OkControlID = "btnYes_Appr_TDCOSACC"
            CancelControlID="btnNo_Appr_TDCOSACC" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup_Appr_TDCOSACC" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Confirmation
            </div>
            <div class="body">
              Do you want to Submit ?
                
            </div>
            <div class="footer" align="right">
                <asp:Button ID="btnNo_Appr_TDCOSACC" runat="server" Text="No" />
                <asp:Button ID="btnYes_Appr_TDCOSACC" runat="server" Text="Yes" />                
            </div>
        </asp:Panel>--%>


        <%-- Following Popup for Send For Correction Travel Request --%>
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

         <%-- Following Popup for Reject Travel Request --%>
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
    <%--<asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />--%>

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
    <asp:HiddenField ID="hdnApprovalTD_ID" runat="server" />
    <asp:HiddenField ID="hdnApprovalCOS_ID" runat="server" />    
    <asp:HiddenField ID="hdnApprovalTD_Code" runat="server" />
    <asp:HiddenField ID="hdnApprovalCOS_Code" runat="server" />
    <asp:HiddenField ID="hdnApproverTDCOS_status" runat="server" />

    <asp:HiddenField ID="hdnisBookthrugh_TD" runat="server" />
    <asp:HiddenField ID="hdnisBookthrugh_COS" runat="server" />
    

    <asp:HiddenField ID="hdnisApprover_TDCOS" runat="server" />

    <asp:HiddenField ID="hdnTravelDtlsId" runat="server" />
    <asp:HiddenField ID="hdnfrmdate_emial" runat="server" />
    <asp:HiddenField ID="hdntodate_emial" runat="server" />
    
    <asp:HiddenField ID="hdnApprovalACC_Code" runat="server" />   
    <asp:HiddenField ID="hdnApprovalACC_ID" runat="server" />
    <asp:HiddenField ID="hdnApproverTDACC_status" runat="server" />
    <asp:HiddenField ID="hdnInboxType" runat="server" />
    <asp:HiddenField ID="hdnCheckTRStatus_forACC" runat="server" />

    <asp:HiddenField ID="hdnApprovalTD_mail" runat="server" /> 
    <asp:HiddenField ID="hdnApprovalCOS_mail" runat="server" /> 
    <asp:HiddenField ID="hdnApprovalACC_mail" runat="server" /> 

    <asp:HiddenField ID="hdnApprovalTD_Name" runat="server" /> 
    <asp:HiddenField ID="hdnApprovalCOS_Name" runat="server" /> 
    <asp:HiddenField ID="hdnApprovalACC_Name" runat="server" /> 
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="hdnbtnStatus" runat="server" />

    <asp:HiddenField ID="hdndgTravelRequestMaxID" runat="server" />

    <asp:HiddenField ID="hdnTryiptypeid" runat="server" />
     <asp:HiddenField ID="hdnfromdate_Accm" runat="server" />
     <asp:HiddenField ID="hdnTodate_Accm" runat="server" />
     <asp:HiddenField ID="hdnAccdtlsid" runat="server" />

    <script type="text/javascript">

        //$(document).ready(function () {
        //    $("#btnApproved1").submit(function () {
        //        $(this).submit(function () {
        //            return false;
        //        });
        //        return true;
        //    });
        //});

        function SetCntrls(Deviation) {

            if (Deviation == "Booked") {

                document.getElementById("<%=txtfare_Accm.ClientID%>").disabled = false
                document.getElementById("<%=txtAcctype_Accm.ClientID%>").disabled = false
                document.getElementById("<%=txtAcctype_Accm.ClientID%>").style.backgroundColor = "#ffffff"; // backcolor
            }
            else {

                document.getElementById("<%=txtfare_Accm.ClientID%>").disabled = true
                document.getElementById("<%=txtfare_Accm.ClientID%>").value = "";

                document.getElementById("<%=txtAcctype_Accm.ClientID%>").disabled = true
                document.getElementById("<%=txtAcctype_Accm.ClientID%>").value = "";
                document.getElementById("<%=txtAcctype_Accm.ClientID%>").style.backgroundColor = "#ebebe4"; // backcolor
            }
            return;
        }

        function validateTripType_Accm(Deviation) {

            if (Deviation == "Hotel") {

                document.getElementById("<%=txtfare_Accm.ClientID%>").disabled = false
            }
            else {

                document.getElementById("<%=txtfare_Accm.ClientID%>").disabled = true
                document.getElementById("<%=txtfare_Accm.ClientID%>").value = "";
            }
            return;
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
            keychar = String.fromCharCode(keynum);
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
        function TDCOSApproverMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=btnApprove_TDCOS.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)                        
                        Approver_TDCOS_Confirm();
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

        function Approver_TDCOS_Confirm() {
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

         

    </script>
</asp:Content>

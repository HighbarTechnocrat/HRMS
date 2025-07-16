<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="travel_Exp.aspx.cs" Inherits="travel_Exp" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <%--<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_Exp_css.css" type="text/css" media="all" />
    <%-- <link href="<%=ReturnUrl("css") %>calendar/calendar.css" rel="stylesheet" type="text/css" />
    <link href="sampleform_css.css" rel="stylesheet" type="text/css" />--%>
    <%--<ajaxToolkit:ToolkitScriptManager ID="srcp" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
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
                <div class="wishlistpage">
                    <div class="myaccount" style="display: none;">
                        <div class="myaccountheading">My Account</div>
                        <div class="myaccountlist">
                            <ul>
                                <li class="listselected"><a href="<%=ReturnUrl("sitepathmain") %>procs/leaveindex" title="Edit Profile"><b>Edit Profile</b></a></li>
                                <li><a href="<%=ReturnUrl("sitepathmain") %>procs/wishlist" title="Favorites">Favorites</a></li>
                                <asp:Panel ID="Panel1" runat="server" Visible="false">
                                    <li><a href="<%=ReturnUrl("sitepathmain") %>procs/preference" title="Preference">Preference</a></li>

                                    <li id="lihistory" runat="server"><a href="<%=ReturnUrl("sitepathmain") %>procs/subscriptionhistory" title="Subscription History">Subscription History</a></li>
                                    <li><a href="<%=ReturnUrl("sitepathmain") %>procs/pthistory" title=" Reward Points">Reward Points</a></li>

                                    <li><a href="<%=ReturnUrl("sitepathmain") %>recommend" title="Recommendation">Recommendation</a></li>
                                </asp:Panel>
                                <li>
                                    <asp:LinkButton ID="btnSingOut" runat="server" ToolTip="Logout"
                                        Text="Logout" > </asp:LinkButton>
                                </li>
                            </ul>
                        </div>
                        <div class="myaccountlist-mobiletab">
                            <asp:DropDownList ID="ddlprofile" runat="server" AutoPostBack="true">
                                <asp:ListItem Selected="True" Value="edit">Edit Profile</asp:ListItem>
                                <asp:ListItem Value="pwd">Change Password</asp:ListItem>
                                <asp:ListItem Value="wishlist">Favorites</asp:ListItem>
                                <asp:ListItem Value="preference">preference</asp:ListItem>
                                <asp:ListItem Value="subscription">Subscription History</asp:ListItem>
                                <asp:ListItem Value="pthistory">Reward Points</asp:ListItem>
                                <asp:ListItem Value="logout">Logout</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="editprofile" id="editform1" runat="server" visible="false">
                        <div class="editprofileform">
                            <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="false" Style="margin-left: 135px"></asp:Label>
                            <table>
                                <tr>
                                    <td class="formtitle"></td>
                                    <td class="forminput">

                                        <br>
                                        <font>(Maximum 20 characters)</font>
                                        <div class="formerror" id="diverror" runat="server" visible="false">
                                            <asp:Label ID="lblfname" runat="server" Text=""></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formtitle"><font>*</font><span>Last Name:</span></td>
                                    <td class="forminput">

                                        <br>
                                        <font>(Maximum 20 characters)</font>
                                        <div class="formerror" id="diverror1" runat="server" visible="false">
                                            <asp:Label ID="lbllame" runat="server" Text=""></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formtitle"><span>Address:</span></td>
                                    <td class="forminput">

                                        <br>
                                        <font>(Maximum 100 characters)</font>
                                        <div class="formerror" id="diverror2" runat="server" visible="false">
                                            <asp:Label ID="lbladdress" runat="server" Text="Please enter Address"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formtitle"><span>Country:</span></td>
                                    <td class="forminput">

                                        <div class="formerror" id="diverror3" runat="server" visible="false">
                                            <asp:Label ID="lblcountry" runat="server" Text=""></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formtitle"><span>State:</span></td>
                                    <td class="forminput">

                                        <div class="formerror" id="diverror4" runat="server" visible="false">
                                            <asp:Label ID="lblstate" runat="server" Text=""></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formtitle"><span>Place:</span></td>
                                    <td class="forminput">
                                        <div class="formerror" id="diverror5" runat="server" visible="false">
                                        </div>
                                    </td>
                                </tr>
                                <asp:Panel runat="server" ID="pnlothercity" Visible="false">
                                    <tr>
                                        <td class="formtitle"></td>
                                        <td class="forminput">
                                            <%--<ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtothercity" WatermarkText="Add Other City" />--%>
                                            <asp:TextBox AutoComplete="off" ID="txtothercity" onblur="showtext(this)" Visible="false" Height="20" Width="256px" EnableTheming="True" ForeColor="#8B8B8B" runat="server" CssClass="medium" onfocus="cleartext(this);"> </asp:TextBox>

                                        </td>
                                        <td class="formerror"></td>
                                    </tr>
                                </asp:Panel>
                                <tr id="trpincode" runat="server" visible="false">
                                    <td class="formtitle"><font>*</font><span>Pin code:</span></td>
                                    <td class="forminput">

                                        <div class="formerror">
                                        </div>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="formtitle">
                                        <font>*</font><span>Date Of Birth:</span>
                                    </td>
                                    <td class="forminput">

                                        <br />
                                    </td>
                                </tr>

                                <tr>
                                    <td class="formtitle"><span>Mobile No:</span></td>
                                    <td class="forminput">
                                        <asp:TextBox AutoComplete="off" ID="txtmobile1" runat="server" Text="+91" class="countrycode" ReadOnly="true" Visible="false" ValidationGroup="validate"></asp:TextBox>

                                        <br>
                                        <font>(Maximum 16 digits)</font>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server" ErrorMessage="Please enter valid Mobile No" ValidationExpression="^(\(?\+?[0-9]*\)?)?[0-9_\- \(\)]*$" CssClass="error_field" ControlToValidate="txtmobile" Display="Dynamic" ValidationGroup="validate"></asp:RegularExpressionValidator><br />
                                        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtmobile" ID="RegularExpressionValidator19" ValidationExpression="^[\s\S]{10,16}$" runat="server" CssClass="error_field" ErrorMessage="Minimum 10 and Maximum 16 characters allowed." ValidationGroup="validate"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtmobile"
                                            ErrorMessage="Mobile No. is mandatory" ToolTip="Mobile No. is mandatory" ValidationGroup="validate"
                                            SetFocusOnError="true" CssClass="error_field" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <div class="formerror" id="diverror6" runat="server" visible="false">

                                            <asp:Label ID="lblmob" runat="server"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formtitle"><span>Profile Photo:</span></td>
                                    <td class="forminput">

                                        <asp:Label ID="lblstatus" runat="server" ForeColor="Green" Text=""></asp:Label>
                                        <br />
                                        <br />

                                    </td>
                                </tr>
                                <tr>
                                    <td class="formtitle"><span>Cover Photo:</span></td>
                                    <td class="forminput">


                                        <asp:Label ID="lblstatus2" runat="server" ForeColor="Green" Text=""></asp:Label>
                                        <br />
                                        <br />

                                    </td>
                                </tr>
                                <tr id="trtel" runat="server" visible="false">
                                    <td class="formtitle"><span>Telphone No:</span></td>
                                    <td class="forminput">
                                        <asp:TextBox AutoComplete="off" ID="txtphone1" MaxLength="10" Text="+91" ReadOnly="true" runat="server"
                                            CssClass="countrycode"> </asp:TextBox>
                                        <ew:NumericBox ID="txtphone2" MaxLength="5" runat="server" CssClass="citycode"> </ew:NumericBox>
                                    </td>
                                    <td class="formerror"></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td colspan="2"></td>
                                </tr>
                            </table>

                        </div>
                    </div>
                </div>
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Travel Request"></asp:Label>
                    </span>
                </div>

                <span>
                    <%--<a href="travelindex.aspx" class="aaa">Travel Index</a>--%>
                    <a href="travelindex.aspx" class="aaaa" >Travel Index</a>
                </span>



                <div class="edit-contact">
                    <div class="editprofile" style="text-align: center; border: none;" id="divmsg" runat="server" visible="false">
                        <asp:Label runat="server" ID="lblmessage" Visible="false" Style="color: green; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                    </div>
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" OnClick="lnkcont_Click"></asp:LinkButton>
                        </div>
                    </div>


                    <ul id="editform" runat="server" visible="false">


                        <li class="trvl_inboxEmpCode">
                            <span>Employee Code</span><br />

                            <asp:TextBox AutoComplete="off" ID="TextBox1" runat="server"></asp:TextBox>

                        </li>
                        <li class="trvl_inboxEmpCode">
                            <%--<span>Employee Code</span><br />--%>

                            <asp:TextBox AutoComplete="off" ID="TextBox3" runat="server" Visible="false"></asp:TextBox>

                        </li>
                        <li class="trvl_InboxEmpName">
                            <span>Employee Name</span><br />

                            <asp:TextBox AutoComplete="off" ID="txtEmpName" runat="server"></asp:TextBox>

                        </li>
                        <li class="trvl_inboxEmpCode">
                            <%--<span>Employee Code</span><br />--%>

                            <asp:TextBox AutoComplete="off" ID="TextBox10" runat="server" Visible="false"></asp:TextBox>

                        </li>
                        <li class="trvl_trip">
                            <span>Trip ID</span><br />

                            <asp:TextBox AutoComplete="off" ID="TextBox2" runat="server"></asp:TextBox>

                        </li>

                        <li class="trvl_inboxEmpCode">
                            <%--<span>Employee Code</span><br />--%>

                            <asp:TextBox AutoComplete="off" ID="TextBox11" runat="server" Visible="false"></asp:TextBox>

                        </li>
                        <li class="trvl_type">
                            <span>Travel Type</span><br />


                            <asp:DropDownList ID="ddlTripType" Visible="false" runat="server" AutoPostBack="True" Height="16px">
                                <asp:ListItem>Full Day</asp:ListItem>
                                <asp:ListItem>First Half</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox AutoComplete="off" ID="txtTriptype" runat="server" CssClass="Dropdown"></asp:TextBox>
                            <asp:Panel ID="Panel3" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="ListBox2" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true">
                                            <asp:ListItem>First Half</asp:ListItem>
                                            <asp:ListItem>Full Day</asp:ListItem>
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
                        <li></li>

                        <li class="trvl_date">
                            <span>From </span>
                            <br />

                            <asp:TextBox AutoComplete="off" ID="txtFromdate" runat="server" AutoPostBack="True"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtFromdate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>

                            <%--  <asp:TextBox AutoComplete="off" ID="txtsadate" runat="server" CssClass="txtdatepicker" placeholder="Start Date" OnTextChanged ="txtsadate_TextChanged" AutoPostBack ="true" ></asp:TextBox>
                                <span class="texticon tooltip" title="Select Start Date. Date should be MM/DD/YYYY format."><i class="fa fa-calendar" aria-hidden="true"></i></span>
                                <asp:Label ID="lblsdate" runat="server" Text="" Font-Size="15px"></asp:Label>--%>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="formerror" ControlToValidate="txtsadate" SetFocusOnError="True" ErrorMessage="Please enter start date" ValidationGroup="validate"></asp:RequiredFieldValidator>--%>

                        </li>

                        <li class="trvl_inboxEmpCode">
                            <%--<span>Employee Code</span><br />--%>

                            <asp:TextBox AutoComplete="off" ID="TextBox4" runat="server" Visible="false"></asp:TextBox>

                        </li>


                        <li class="trvl_date">
                            <span>To</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtToDate" runat="server" AutoPostBack="True"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>

                        <li class="trvl_inboxEmpCode">
                            <%--<span>Employee Code</span><br />--%>

                            <asp:TextBox AutoComplete="off" ID="TextBox5" runat="server" Visible="false"></asp:TextBox>

                        </li>
                        <li class="trvl_Reason">
                            <span>Reason for Travel </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtReason" runat="server" MaxLength="100"></asp:TextBox>
                        </li>
                        <li class="trvl_inboxEmpCode">
                            <%--<span>Employee Code</span><br />--%>

                            <asp:TextBox AutoComplete="off" ID="TextBox6" runat="server" Visible="false"></asp:TextBox>

                        </li>
                        <li class="trvL_detail">
                            <span>Travel Details</span>
                            <asp:LinkButton ID="btnTra_Details" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve"></asp:LinkButton>
                        </li>

                        <li class="trvl_grid">

                            <div>

                                <asp:GridView ID="dgTravelRequest" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="70%" EditRowStyle-Wrap="false">
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
                                        <asp:BoundField HeaderText="Mode"
                                            DataField="Leave Type"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>

                                        <asp:BoundField HeaderText="Dep Date"
                                            DataField="Opening Balance"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="20%" ItemStyle-BorderColor="Navy"/>

                                        <asp:BoundField HeaderText="Dep Place"
                                            DataField="Availed"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>

                                        <asp:BoundField HeaderText="Arr Date"
                                            DataField="Balance"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="20%" ItemStyle-BorderColor="Navy"/>
                                        <asp:BoundField HeaderText="Arr Place"
                                            DataField="Balance"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>
                                        <asp:BoundField HeaderText="Through Travel Desk"
                                            DataField="Balance"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>
                                        <asp:BoundField HeaderText="Deviation"
                                            DataField="Balance"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>
                                        <asp:BoundField HeaderText="Edit"
                                            DataField="Balance"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>

                                    </Columns>
                                </asp:GridView>

                            </div>

                        </li>


                        <li class="trvl_Advances">
                            <span>Advances Required: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtAdvance" runat="server" MaxLength="100"></asp:TextBox>
                        </li>
                        <li class="trvl_inboxEmpCode">
                            <%--<span>Employee Code</span><br />--%>

                            <asp:TextBox AutoComplete="off" ID="TextBox7" runat="server" Visible="false"></asp:TextBox>

                        </li>


                        <li class="trvl_Currency">
                            <span>Currency Required:</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtreqCur" runat="server" MaxLength="100"></asp:TextBox>
                        </li>
                        <li class="trvl_inboxEmpCode">
                            <%--<span>Employee Code</span><br />--%>

                            <asp:TextBox AutoComplete="off" ID="TextBox9" runat="server" Visible="false"></asp:TextBox>

                        </li>
                        <li class="trvl_Accomodation">
                            <span>Accommodation</span>
                            <asp:LinkButton ID="trvl_accmo_btn" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve"></asp:LinkButton>
                        </li>

                        <li class="trvl_grid">

                            <div>

                                <asp:GridView ID="dgAccomodation" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="70%" EditRowStyle-Wrap="false">
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
                                            DataField="Leave Type"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="30%" ItemStyle-BorderColor="Navy"/>

                                        <asp:BoundField HeaderText="To Date"
                                            DataField="Opening Balance"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="30%" ItemStyle-BorderColor="Navy"/>

                                        <asp:BoundField HeaderText="Location"
                                            DataField="Balance"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="20%" ItemStyle-BorderColor="Navy"/>

                                        <asp:BoundField HeaderText="Through COS"
                                            DataField="Balance"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>

                                        <asp:BoundField HeaderText="Edit"
                                            DataField="Balance"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>

                                    </Columns>
                                </asp:GridView>

                            </div>

                        </li>

                        <li class="trvl_local">
                            <span>Local Travel:</span>
                            <asp:LinkButton ID="trvl_localbtn" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve"></asp:LinkButton>
                        </li>

                        <li class="trvl_grid">

                            <div>

                                <asp:GridView ID="dgLocalTravel" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="70%" EditRowStyle-Wrap="false">
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
                                            DataField="Leave Type"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="30%" ItemStyle-BorderColor="Navy"/>

                                        <asp:BoundField HeaderText="To Date"
                                            DataField="Opening Balance"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="30%" ItemStyle-BorderColor="Navy"/>

                                        <asp:BoundField HeaderText="Location"
                                            DataField="Balance"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="20%" ItemStyle-BorderColor="Navy"/>

                                        <asp:BoundField HeaderText="Through COS"
                                            DataField="Balance"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>

                                        <asp:BoundField HeaderText="Edit"
                                            DataField="Balance"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>

                                    </Columns>
                                </asp:GridView>

                            </div>

                        </li>



                        <li class="trvlExp_local">
                            <span>Expense Details:</span>
                            <asp:LinkButton ID="lnkExpeseDetails" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve"></asp:LinkButton>
                        </li>
                        <li class="trvlExp_inboxEmpCode">
                            <%--<span>Employee Code</span><br />--%>

                            <asp:TextBox AutoComplete="off" ID="TextBox27" runat="server" Visible="false"></asp:TextBox>

                        </li>
                        <li class="trvlExp_grid">

                            <div>

                                <asp:GridView ID="dgExpenseDetails" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="70%" EditRowStyle-Wrap="false">
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
                                        <asp:BoundField HeaderText="Mode"
                                            DataField="Leave Type"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>

                                        <asp:BoundField HeaderText="Dep Date"
                                            DataField="Opening Balance"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="20%" ItemStyle-BorderColor="Navy"/>

                                        <asp:BoundField HeaderText="Dep Place"
                                            DataField="Availed"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>

                                        <asp:BoundField HeaderText="Arr Date"
                                            DataField="Balance"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="20%" ItemStyle-BorderColor="Navy"/>
                                        <asp:BoundField HeaderText="Arr Place"
                                            DataField="Balance"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>
                                        <asp:BoundField HeaderText="Through Travel Desk"
                                            DataField="Balance"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>
                                        <asp:BoundField HeaderText="Deviation"
                                            DataField="Balance"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>
                                        <asp:BoundField HeaderText="Edit"
                                            DataField="Balance"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>

                                        <asp:BoundField HeaderText="Deviation"
                                            DataField="Balance"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>
                                        <asp:BoundField HeaderText="Edit"
                                            DataField="Balance"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>


                                    </Columns>
                                </asp:GridView>

                            </div>

                        </li>

                        <li class="trvlExp_inboxEmpCode">
                            <%--<span>Employee Code</span><br />--%>

                            <asp:TextBox AutoComplete="off" ID="TextBox15" runat="server" Visible="false"></asp:TextBox>

                        </li>
                        <li class="trvlExp_Daily">
                            <span>Daily Halting allowance </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="TextBox8" runat="server" MaxLength="100"></asp:TextBox>
                        </li>
                        <li class="trvlExp_inboxEmpCode">
                            <%--<span>Employee Code</span><br />--%>

                            <asp:TextBox AutoComplete="off" ID="TextBox19" runat="server" Visible="false"></asp:TextBox>

                        </li>
                        <li class="trvlExp_totamount">
                            <span>Total Amount Claimed:</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="TextBox12" runat="server" MaxLength="100"></asp:TextBox>
                        </li>


                        <li class="trvlExp_inboxEmpCode">
                            <%--<span>Employee Code</span><br />--%>

                            <asp:TextBox AutoComplete="off" ID="TextBox20" runat="server" Visible="false"></asp:TextBox>

                        </li>
                        <li class="trvlExp_lessadv">
                            <span>Less Advance Taken:</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="TextBox13" runat="server" MaxLength="100"></asp:TextBox>

                            <li class="trvlExp_inboxEmpCode">
                                <%--<span>Employee Code</span><br />--%>

                                <asp:TextBox AutoComplete="off" ID="TextBox21" runat="server" Visible="false"></asp:TextBox>

                            </li>

                            <li class="trvlExp_NetPay">
                                <span>Net Payable to Company: </span>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="TextBox14" runat="server" MaxLength="100"></asp:TextBox>
                            </li>

                            <li class="trvlExp_inboxEmpCode">
                                <%--<span>Employee Code</span><br />--%>

                                <asp:TextBox AutoComplete="off" ID="TextBox23" runat="server" Visible="false"></asp:TextBox>

                            </li>

                            <li class="trvlExp_PayEmp">
                                <span>Net Payable to Employee: </span>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="TextBox16" runat="server" MaxLength="100"></asp:TextBox>
                            </li>
                            <li class="trvlExp_inboxEmpCode">
                                <%--<span>Employee Code</span><br />--%>

                                <asp:TextBox AutoComplete="off" ID="TextBox18" runat="server" Visible="false"></asp:TextBox>

                            </li>
                            <li class="trvlExp_Reason">
                                <span>Reason For Deviation:
                                </span>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="TextBox17" runat="server" MaxLength="100"></asp:TextBox>
                            </li>

                            <li class="trvlExp_inboxEmpCode">
                                <%--<span>Employee Code</span><br />--%>

                                <asp:TextBox AutoComplete="off" ID="TextBox22" runat="server" Visible="false"></asp:TextBox>

                            </li>

                            <li class="trvlExp_upload">
                                <span>Upload File</span><br />
                                <asp:FileUpload ID="uploadprofile" runat="server" />
                                <asp:TextBox AutoComplete="off" ID="txtprofile" runat="server" CssClass="mobilenotextbox popup" Visible="false"> </asp:TextBox>
                                <%--<span class="texticonselect tooltip" title="Please provide your profile picture."><i class="fa fa-file-image-o"></i></span>--%>

                            </li>
                            <li class="trvlExp_inboxEmpCode">
                                <%--<span>Employee Code</span><br />--%>

                                <asp:TextBox AutoComplete="off" ID="TextBox24" runat="server" Visible="false"></asp:TextBox>

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
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="trvl_Savebtndiv">
        <asp:LinkButton ID="trvl_btnSave" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" >Submit</asp:LinkButton>

    </div>
    <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>

    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />

    <asp:HiddenField ID="hflEmpName" runat="server" />

    <asp:HiddenField ID="hflEmpDesignation" runat="server" />

    <asp:HiddenField ID="hflEmpDepartment" runat="server" />

    <asp:HiddenField ID="hflEmailAddress" runat="server" />

    <asp:HiddenField ID="hflGrade" runat="server" />

    <asp:HiddenField ID="hflapprcode" runat="server" />



    <script type="text/javascript">
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

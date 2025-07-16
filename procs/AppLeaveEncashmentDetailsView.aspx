<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="AppLeaveEncashmentDetailsView.aspx.cs" Inherits="AppLeaveEncashmentDetailsView" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/AppLeaveRequest_css.css" type="text/css" media="all" />
    <%-- <link href="<%=ReturnUrl("css") %>calendar/calendar.css" rel="stylesheet" type="text/css" />
    <link href="sampleform_css.css" rel="stylesheet" type="text/css" />--%>
    <%--<ajaxToolkit:ToolkitScriptManager ID="srcp" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }


        .Dropdown {
            border-bottom: 2px solid #cccccc;
            /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
            background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;
            cursor: default;
            background-color:#ebebe4;
        }

        .taskparentclass2 {
            width: 29.5%;
            height: 112px;
        }

        .taskparentclasskkk {
            width: 29.5%;
            height: 72px;
            /*overflow:initial;*/
        }

        #MainContent_Upl_leavetype {
            width: 91% !important;
        }

        #MainContent_ddlLeaveType {
            padding: 15px 0 !important;
        }

        #MainContent_ListBox1, #MainContent_ListBox2 {
            padding: 0 0 0% 0 !important;
            /*overflow: unset;*/
        }

        #MainContent_lstApprover, #MainContent_lstIntermediate {
            padding: 0 0 33px 0 !important;
            /*overflow: unset;*/
        }

        /*.btnApprove {
            background-attachment: scroll;
            background-clip: border-box;
            background-color: #febf39;
            background-image: none;
            background-origin: padding-box;
            background-position-x: 0;
            background-position-y: 0;
            background-repeat: repeat;
            background-size: auto auto;
            padding-bottom: 8px;
            padding-left: 23px;
            padding-right: 23px;
            padding-top: 8px;
        }*/
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
                                <li class="listselected"><a href="<%=ReturnUrl("sitepathmain") %>procs/Leaves" title="Edit Profile"><b>Edit Profile</b></a></li>
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
                            <asp:DropDownList ID="ddlprofile" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlprofile_SelectedIndexChanged">
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
                                    <td class="formtitle"><span>City:</span></td>
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
                                            <asp:TextBox ID="txtothercity" onblur="showtext(this)" Visible="false" Height="20" Width="256px" EnableTheming="True" ForeColor="#8B8B8B" runat="server" CssClass="medium" onfocus="cleartext(this);"> </asp:TextBox>

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
                                        <asp:TextBox ID="txtmobile1" runat="server" Text="+91" class="countrycode" ReadOnly="true" Visible="false" ValidationGroup="validate"></asp:TextBox>

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
                                        <asp:TextBox ID="txtphone1" MaxLength="10" Text="+91" ReadOnly="true" runat="server"
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
                        <asp:Label ID="lblheading" runat="server" Text="Leave Request"></asp:Label>
                    </span>
                </div>
               


                <div class="leavegrid">

                    <%-- <a href="Leaves.aspx" class="aaa" >Leave Menu</a>--%>
                  
                    <a href="TeamReport.aspx" class="aaaa">Team Calendar</a>
              
                    <asp:GridView ID="dgLeaveBalance" Visible="True" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="70%">
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
                            <asp:BoundField HeaderText="Leave Type"
                                DataField="Leave Type"
                                ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="left"
                                    ItemStyle-Width="22%" 
                                    ItemStyle-BorderColor="Navy"
                                    />

                            <asp:BoundField HeaderText="Opening"
                                DataField="Opening Balance"
                                ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="13%" 
                                ItemStyle-BorderColor="Navy"
                                />

                            <asp:BoundField HeaderText="Earned"
                                DataField="Earned"
                                ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="13%" 
                                ItemStyle-BorderColor="Navy"
                                />

                            <asp:BoundField HeaderText="Availed"
                                DataField="Availed"
                                ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="13%" 
                                ItemStyle-BorderColor="Navy"
                                />

                            <asp:BoundField HeaderText="Pending"
                                DataField="Pending"
                                ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="13%" 
                                ItemStyle-BorderColor="Navy"
                                />

                            <asp:BoundField HeaderText="Future Leaves"
                                DataField="Future"
                                ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="13%" 
                                ItemStyle-BorderColor="Navy"
                                />

                            <asp:BoundField HeaderText="Balance"
                                DataField="Balance"
                                ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="13%" 
                                ItemStyle-BorderColor="Navy"
                                />

                        </Columns>
                    </asp:GridView>

                </div>






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


                    <ul id="editform" runat="server" visible="false">

                        <li>
                            <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                        </li>
                        <li></li>
                        <li class="Reason">
                            <span>Employee Code</span>
                            <br />
                            <asp:TextBox ID="txtempocde_lap" runat="server" MaxLength="50" Enabled="false"> </asp:TextBox>
                        </li>

                        <li class="Reason">
                            <span>Employee Name</span>
                            <br />
                            <asp:TextBox ID="txtEmpName" runat="server" MaxLength="100" Enabled="false"></asp:TextBox>
                        </li>

                        <li class="Reason" style="display:none;">
                            <span>Designation</span>
                            <br />
                            <asp:TextBox ID="txtDesig" runat="server" MaxLength="100" Enabled="false"></asp:TextBox>
                            
                        </li>

                        <li class="Reason" style="display:none;">
                            <span>Department</span>
                            <br />
                            <asp:TextBox ID="txtDepartment" runat="server" MaxLength="100" Enabled="false"></asp:TextBox>
                        </li>

                        <li>
                            <span>Leave Type</span><br />
                            <asp:UpdatePanel ID="Upl_leavetype" runat="server" Visible="false">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlLeaveType" runat="server" OnSelectedIndexChanged="ddlLeaveType_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlLeaveType" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <asp:TextBox ID="txtLeaveType" runat="server" Enabled="false" CssClass="Dropdown" OnTextChanged="txtLeaveType_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <%-- <i class="fa fa-caret-down" aria-hidden="true"></i>--%>
                            <asp:Panel ID="Panel4" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstLeaveType" runat="server" CssClass="taskparentclass2" AutoPostBack="true" OnSelectedIndexChanged="lstLeaveType_SelectedIndexChanged">
                                            <%-- <asp:ListItem>Privilege Leave</asp:ListItem>
                                            <asp:ListItem>Sick Leave</asp:ListItem>
                                            <asp:ListItem>Maternity Leave</asp:ListItem>
                                            <asp:ListItem>Leave Without Pay</asp:ListItem>
                                            <asp:ListItem>Time Off</asp:ListItem>--%>
                                        </asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender3" PopupControlID="Panel4" TargetControlID="txtLeaveType"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>
                        </li>
                        <li></li>

                        <!-- Commented by R1 on 11-10-2018 -->
                        <!--<li class="date">
                            <span>From Date</span><br />

                            <asp:TextBox ID="txtFromdate" runat="server" AutoPostBack="True" OnTextChanged="txtFromdate_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtFromdate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>

                            <%--  <asp:TextBox ID="txtsadate" runat="server" CssClass="txtdatepicker" placeholder="Start Date" OnTextChanged ="txtsadate_TextChanged" AutoPostBack ="true" ></asp:TextBox>
                                <span class="texticon tooltip" title="Select Start Date. Date should be MM/DD/YYYY format."><i class="fa fa-calendar" aria-hidden="true"></i></span>
                                <asp:Label ID="lblsdate" runat="server" Text="" Font-Size="15px"></asp:Label>--%>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="formerror" ControlToValidate="txtsadate" SetFocusOnError="True" ErrorMessage="Please enter start date" ValidationGroup="validate"></asp:RequiredFieldValidator>--%>

                        </li>

                        <li class="taskparentclass">
                            <span>For</span><br />

                            <asp:DropDownList ID="ddlFromFor" Visible="false" runat="server" OnSelectedIndexChanged="ddlFromFor_SelectedIndexChanged" AutoPostBack="True">
                                <asp:ListItem>Full Day</asp:ListItem>
                                <asp:ListItem>First Half</asp:ListItem>
                                <asp:ListItem>Second Half</asp:ListItem>
                            </asp:DropDownList>



                            <asp:TextBox ID="txtFromfor" runat="server" CssClass="Dropdown" ></asp:TextBox>
                            <%--<asp:TextBox ID="txtFromfor" runat="server" Enabled="false" ></asp:TextBox>--%>
                            <%--<i class="fa fa-caret-down" aria-hidden="true"></i>--%>
                            <asp:Panel ID="Panel2" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstFromfor" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstFromfor_SelectedIndexChanged">
                                            <asp:ListItem>First Half</asp:ListItem>
                                            <asp:ListItem>Second Half</asp:ListItem>
                                            <asp:ListItem>Full Day</asp:ListItem>
                                        </asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender1" PopupControlID="Panel2" TargetControlID="txtFromfor"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>

                        </li>

                        <li class="date">
                            <span>To Date</span><br />
                            <asp:TextBox ID="txtToDate" runat="server" AutoPostBack="True" OnTextChanged="txtToDate_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>

                        <li class="fordate">
                            <span>For</span><br />
                            <asp:DropDownList ID="ddlToFor" Visible="false" runat="server" AutoPostBack="True" Height="16px">
                                <asp:ListItem>Full Day</asp:ListItem>
                                <asp:ListItem>First Half</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="txtToFor" runat="server" CssClass="Dropdown"></asp:TextBox>                            
                            <%--<i class="fa fa-caret-down" aria-hidden="true"></i>--%>
                            <asp:Panel ID="Panel3" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstTofor" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstToFor_SelectedIndexChanged">
                                            <asp:ListItem>First Half</asp:ListItem>
                                            <asp:ListItem>Full Day</asp:ListItem>
                                        </asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender2" PopupControlID="Panel3" TargetControlID="txtToFor"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>
                        </li>-->
                        <!-- Commented by R1 on 11-10-2018 -->
                        <li class="leavedays">
                            <span>Leave Days</span><br />
                            <asp:TextBox ID="txtLeaveDays" Enabled="false" runat="server" AutoPostBack="True" OnTextChanged="txtLeaveDays_TextChanged"></asp:TextBox>
                        </li>

                        <%--<li ></li>--%>
                        <%--                        <li> 
                            <span></span>
                            <br />
                            <br />
                        </li>--%>

                        <li class="Reason">
                            <span>Remarks </span>
                            
                            <br />
                            <asp:TextBox ID="txtReason" runat="server" MaxLength="100"></asp:TextBox>
                        </li>

<%--                        <li class="upload">
                            <span>Upload File</span><br />
                            <asp:FileUpload ID="uploadfile" Visible="false" runat="server" Enabled="false" />
                            <asp:TextBox ID="txtprofile" runat="server" CssClass="mobilenotextbox popup" Visible="false"> </asp:TextBox>
                            <span class="texticonselect tooltip" title="Please provide your profile picture."><i class="fa fa-file-image-o"></i></span>
                            
                        </li>--%>

                        <li>
                            <span>Comment</span>&nbsp;&nbsp;<span style="color:red">*</span>
                            <br />
                            <asp:TextBox ID="txtComment" AutoComplete="off" runat="server" MaxLength="30"></asp:TextBox>
                        </li>
                        <li class="Reason">
                            <span id="idspnLWPTrnsfer_PL" runat="server">Transfer LWP to PL &nbsp;&nbsp; <span style="color:red">*</span> </span>
                            <br />
                            <asp:TextBox ID="txtLWP_To_PL" AutoComplete="off" runat="server" MaxLength="10"></asp:TextBox>
                        </li>
     
                        <li class="Approver">
                            <%--   <span>Approver </span>--%>
                            <asp:LinkButton ID="lnkfile"   runat="server" Text="asss" Visible="true"  OnClick="lnkfile_Click"></asp:LinkButton>
                            <br />
                            <%--<asp:ListBox ID="ListBox1" runat="server" Visible="false"></asp:ListBox>--%>
                        </li>

                        <li class="Approver">
                               <span>Approver </span>
                            <br />
                            <asp:ListBox ID="lstApprover" runat="server"></asp:ListBox>
                        </li>
                        <%--<li>
                               <span> </span>
                            <br />
                        </li>--%>
                     <%--   <li class="Approver">                          
                            <br />
                            <asp:ListBox ID="ListBox3" Visible="false" runat="server"></asp:ListBox>
                        </li>--%>
                        <!-- Commented by R1 on 11-10-2018 -->
                        <!--<li class="Approver">
                            <span>For Information To </span>
                            <br />
                            <asp:ListBox ID="lstIntermediate" Visible="true" runat="server"></asp:ListBox>
                        </li>-->
                        <!-- Commented by R1 on 11-10-2018 -->
                    </ul>
                </div>






            </div>
        </div>
    </div>
    <div>

        <span>
            <asp:LinkButton ID="btnApprove" runat="server" Text="Approve" ToolTip="Approve" CssClass="Savebtnsve" OnClick="btnApprove_Click" OnClientClick="return SaveMultiClick_Approved();">Approve</asp:LinkButton>
        </span>

        <span>
            <asp:LinkButton ID="btnCorrection" runat="server" Visible="false" Text="Correction" ToolTip="" CssClass="Savebtnsve" OnClick="btnCorrection_Click" OnClientClick="return SaveMultiClick_Correction();">Send for Correction</asp:LinkButton>
        </span>

        <span>
            <asp:LinkButton ID="btnReject" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClick="btnReject_Click" OnClientClick="return CancelMultiClick_Reject();">Reject</asp:LinkButton>
        </span>

        <span>
            <asp:LinkButton ID="btnBack" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="btnBack_Click">Back</asp:LinkButton>
        </span>
    </div>

    <asp:TextBox ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>

    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />

    <asp:HiddenField ID="hflEmpName" runat="server" />

    <asp:HiddenField ID="hflEmpDesignation" runat="server" />

    <asp:HiddenField ID="hflEmpDepartment" runat="server" />

    <asp:HiddenField ID="hflEmailAddress" runat="server" />



    <asp:HiddenField ID="hflGrade" runat="server" />

    <asp:HiddenField ID="hflapprcode" runat="server" />

    <asp:HiddenField ID="hdlDate" runat="server" />

    <asp:HiddenField ID="hdnReqid" runat="server" />

    <asp:HiddenField ID="hdnleaveType" runat="server" />

    <asp:HiddenField ID="hflLeavestatus" runat="server" />

    <asp:HiddenField ID="hflstatusid" runat="server" />

    <asp:HiddenField ID="hflApproverEmail" runat="server" />

    <asp:HiddenField ID="hdnOldLeaveCount" runat="server" />

    <asp:HiddenField ID="hdnInterEmail" runat="server" />

    <asp:HiddenField ID="hdnapprid" runat="server" />
    <asp:HiddenField ID="hdnnextappcode" runat="server" />

    <asp:HiddenField ID="hdnmpcode" runat="server" />

    <asp:HiddenField ID="hdnempname" runat="server" />
    <asp:HiddenField ID="hdnEmpEmail" runat="server" />
    <asp:HiddenField ID="hdnleaveid" runat="server" />
    <asp:HiddenField ID="hdnstaus" runat="server" />
    <asp:HiddenField ID="hdnLoginUserName" runat="server" />
    <asp:HiddenField ID="hdnCurrentID" runat="server" />
    <asp:HiddenField ID="hdnIntermediateEmail" runat="server" />
    <asp:HiddenField ID="hdnleaveconditiontypeid" runat="server" />
    
    <asp:HiddenField ID="hdnCorrectionStatus" runat="server" />
    <asp:HiddenField ID="hdnLoginEmpEmail" runat="server" />
    <asp:HiddenField ID="hdnApproverTDCOS_status" runat="server" />
    <asp:HiddenField ID="hdnisApprover_TDCOS" runat="server" />
    <asp:HiddenField ID="hdnhrappType" runat="server" />
    
    <asp:HiddenField ID="hdnApproverid_LWPPL" runat="server" />
    <asp:HiddenField ID="hdnApproverCode_LWPPL" runat="server" />
    <asp:HiddenField ID="hdnNewReqid" runat="server" />

    <asp:HiddenField ID="hdnFrmdate_LWP" runat="server" />
    <asp:HiddenField ID="hdnTodate_PL" runat="server" />
    <asp:HiddenField ID="hdnApproverid_LWPPLEmail" runat="server" />
    
    <asp:HiddenField ID="hdnfrmdate_emial" runat="server" />
    <asp:HiddenField ID="hdntodate_emial" runat="server" />
    <asp:HiddenField ID="hdnPreviousApprMails" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
        
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

        function onCharOnlyNumber(e) {
            var keynum;
            var keychar;
            var numcheck = /[0123456789./]/;

            if (window.event) {
                keynum = e.keyCode;
            }
            else if (e.which) {
                keynum = e.which;
            }
            keychar = String.fromCharCode(keynum);
            return numcheck.test(keychar);
        }

        function SaveMultiClick_Approved() {
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
                        Confirm();
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }

        function SaveMultiClick_Correction() {
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
                        Confirm();
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }



        function CancelMultiClick_Reject() {
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

    </script>
</asp:Content>

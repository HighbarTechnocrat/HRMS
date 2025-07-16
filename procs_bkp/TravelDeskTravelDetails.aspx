<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="TravelDeskTravelDetails.aspx.cs" Inherits="TravelDeskTravelDetails" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travelDetails_css.css" type="text/css" media="all" />
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

        .Dropdown {
            border-bottom: 2px solid #cccccc;
            /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
            background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;
            cursor: default;
        }

        .taskparentclasskkk {
            width: 32%;
            height: 55px;
            /*overflow:initial;*/
        }
        .grayDropdown {
            border-bottom: 2px solid #cccccc;
            /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
            background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;            
            background-color:#ebebe4;
        }

         .grayDropdownTxt
         {            
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
                                        Text="Logout"> </asp:LinkButton>
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
                        <asp:Label ID="lblheading" runat="server" Text="Travel Details  (Travel Desk)"></asp:Label>
                    </span>
                </div>

                <div class="edit-contact">
                    <div class="editprofile" style="text-align: left; border: none;" id="divmsg" runat="server">
                        
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
                        <li>
                            <asp:Label runat="server" ID="lblmessage" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                        </li>
                     <li></li>
                        <li class="trvldetails_tripid" id="litripid" runat="server">
                            <span id="spantripid" runat="server">Trip ID</span><br />

                            <asp:TextBox AutoComplete="off" ID="txtTripId" runat="server" CssClass="grayDropdownTxt"></asp:TextBox>

                        </li>
                        <li class="trvldetails_deviation">

                            <%-- <span>Deviation</span><br />--%>

                            <asp:TextBox AutoComplete="off" ID="TextBox2" runat="server" Visible="false"></asp:TextBox>

                        </li>

                        <li class="trvldetails_type" id="litriptype" runat="server">
                            <span id="spantriptype" runat="server">Type of Travel</span><br />


                            <asp:TextBox AutoComplete="off" ID="txtTravelType" runat="server" CssClass="grayDropdown"></asp:TextBox>
                            <asp:Panel ID="Panel3" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstTravelType" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstTravelType_SelectedIndexChanged">
                                            <%-- <asp:ListItem>Domestic</asp:ListItem>
                                            <asp:ListItem>International</asp:ListItem>--%>

                                        </asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender2" PopupControlID="Panel3" TargetControlID="txtTravelType"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>

                        </li>
                        <li class="trvldetails_deviation">

                            <%-- <span>Deviation</span><br />--%>

                            <asp:TextBox AutoComplete="off" ID="TextBox3" runat="server" Visible="false"></asp:TextBox>

                        </li>

                        <li>
                            <span>Mode of Travel</span><br />

                            <asp:TextBox AutoComplete="off" ID="txtTravelMode" runat="server" ></asp:TextBox>
                            <asp:Panel ID="Panel2" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstTravelMode" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstTravelMode_SelectedIndexChanged">
                                            <%--        <asp:ListItem>Air Economy</asp:ListItem>
                                            <asp:ListItem>Air Business Class</asp:ListItem>
                                            <asp:ListItem>Train ACI</asp:ListItem>
                                            <asp:ListItem>Train ACI</asp:ListItem>
                                            <asp:ListItem>Train ACII</asp:ListItem>
                                            <asp:ListItem>Train ACIII</asp:ListItem>
                                            <asp:ListItem>Train AC Chair Car</asp:ListItem>
                                            <asp:ListItem>Train Chair Car</asp:ListItem>
                                            <asp:ListItem>Train Sleeper</asp:ListItem>
                                            <asp:ListItem>By Road</asp:ListItem>--%>
                                        </asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender1" PopupControlID="Panel2" TargetControlID="txtTravelMode"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>


                        </li>

                        <li class="trvldetails_deviation">
                            <span>Deviation</span><br />

                            <asp:TextBox AutoComplete="off" ID="txtDeviation" runat="server" CssClass="grayDropdownTxt"></asp:TextBox>

                        </li>

                        <li class="trvldetails_departuredate">
                            <span>Departure Date </span>
                            <br />

                            <asp:TextBox AutoComplete="off" ID="txtFromdate" runat="server" AutoPostBack="True" CssClass="grayDropdownTxt"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtFromdate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>

                            <%--  <asp:TextBox AutoComplete="off" ID="txtsadate" runat="server" CssClass="txtdatepicker" placeholder="Start Date" OnTextChanged ="txtsadate_TextChanged" AutoPostBack ="true" ></asp:TextBox>
                                <span class="texticon tooltip" title="Select Start Date. Date should be MM/DD/YYYY format."><i class="fa fa-calendar" aria-hidden="true"></i></span>
                                <asp:Label ID="lblsdate" runat="server" Text="" Font-Size="15px"></asp:Label>--%>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="formerror" ControlToValidate="txtsadate" SetFocusOnError="True" ErrorMessage="Please enter start date" ValidationGroup="validate"></asp:RequiredFieldValidator>--%>

                        </li>
                        <li class="trvldetails_place">

                            <span>Place</span><br />


                            <asp:TextBox AutoComplete="off" ID="txtOrigin" runat="server" ></asp:TextBox>
                            <asp:Panel ID="Panel4" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstOrigin" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstOrigin_SelectedIndexChanged"></asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender3" PopupControlID="Panel4" TargetControlID="txtOrigin"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>

                        </li>


                        <li class="trvldetails_arrivaldate">
                            <span>Arrival Date</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtToDate" runat="server" AutoPostBack="false" CssClass="grayDropdownTxt"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>

                        <li class="trvldetails_place2">

                            <span>Place</span><br />

                            <asp:TextBox AutoComplete="off" ID="txtDestination" runat="server" ></asp:TextBox>
                            <asp:Panel ID="Panel5" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstDestination" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstDestination_SelectedIndexChanged"></asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender4" PopupControlID="Panel5" TargetControlID="txtDestination"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>

                        </li>



                        
                         <li class="trvldetails_requirement">
                            <span>Estimated Departure Date /Time : </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtExpDepartDate"  CssClass="grayDropdownTxt" runat="server" MaxLength="40"></asp:TextBox>
                        </li>
                        <li class="trvldetails_requirement">
                            <span>Estimated Arrival Date / Time : </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtExpArrivalDate" CssClass="grayDropdownTxt" runat="server" MaxLength="40"></asp:TextBox>
                        </li>
                        
                       
                         
                         <li class="trvldetails_requirement">
                            <span>Other Requirements: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtRequirememt" runat="server"   CssClass="grayDropdownTxt"  MaxLength="30" ></asp:TextBox>
                        </li>
                          <li class="trvldetails_requirement">
                            <span>Fare: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtfare" runat="server" MaxLength="10"></asp:TextBox>
                        </li>
                        <li class="trvldetails_requirement">
                            <span>Actual Departure Date : </span>
                            <br />
                             <asp:TextBox AutoComplete="off" ID="txtActualDeptDate" runat="server" AutoPostBack="True" OnTextChanged="txtActualDeptDate_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" Format="dd/MM/yyyy" TargetControlID="txtActualDeptDate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>

                        </li>
                         <li class="trvldetails_departuredate">
                            <span>Time (24 Hrs - HH:MM) </span>
                            <br />

                            <asp:TextBox AutoComplete="off" ID="txtFromTime" runat="server" AutoPostBack="false" MaxLength="5"></asp:TextBox>

                        </li>
                        <li class="trvldetails_requirement">
                            <span>Actual Arrival Date : </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtActualArrivalDate" runat="server" AutoPostBack="True" OnTextChanged="txtActualArrivalDate_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" Format="dd/MM/yyyy" TargetControlID="txtActualArrivalDate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>
                         <li class="trvldetails_departuredate">
                            <span>Time (24 Hrs - HH:MM)</span>
                            <br />

                            <asp:TextBox AutoComplete="off" ID="txtToTime" runat="server" AutoPostBack="false" MaxLength="5"></asp:TextBox>
                        </li>
                        <li class="trvldetails_bookthrough">
                            <span>Status:</span>
                            <asp:TextBox AutoComplete="off" ID="txtStatus" runat="server" MaxLength="100" CssClass="Dropdown"></asp:TextBox>
                            <asp:Panel ID="Panel6" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstStatus" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstStatus_SelectedIndexChanged">
                                            <asp:ListItem>Booked</asp:ListItem>
                                            <asp:ListItem>Not Booked</asp:ListItem>
                                        </asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender5" PopupControlID="Panel6" TargetControlID="txtStatus"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>
                        </li>
                      <li class="trvldetails_requirement">
                            <span>Remark for Travel Details: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtfortravelRequest" runat="server"     MaxLength="40" ></asp:TextBox>
                        </li>
                         <li class="trvldetails_bookthrough">
                            <span>Meal Included:</span>

                            <asp:CheckBox ID="chkMealInc" runat="server" Checked="True" />
                        </li>          <li></li>        
                        <li class="claimmob_upload">
                            <span>Upload Bill</span><br />
                            <asp:FileUpload ID="uploadfile" runat="server" Enabled="true" />
                            <asp:LinkButton ID="lnkuplodedfile"  runat="server" OnClick="lnkuplodedfile_Click"></asp:LinkButton>
                            
                        </li>
                           <li class="mobile_inboxEmpCode">    
                            <br />
                            <asp:TextBox AutoComplete="off" ID="TextBox6" runat="server" MaxLength="100" Visible="false" ></asp:TextBox>
                        </li>
                       
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="trvldetails_Savebtndiv">
        <asp:LinkButton ID="trvldeatils_btnSave" runat="server" Text="Confirmation" ToolTip="Save" CssClass="Savebtnsve" OnClick="trvldeatils_btnSave_Click"></asp:LinkButton>


        <asp:LinkButton ID="trvldeatils_delete_btn" runat="server" Text="Delete" ToolTip="Delete" CssClass="Savebtnsve" Visible="false" OnClick="trvldeatils_delete_btn_Click"></asp:LinkButton>

        <asp:LinkButton ID="trvldeatils_cancel_btn" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="trvldeatils_cancel_btn_Click"></asp:LinkButton>

    </div>
    <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>

    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />

    <asp:HiddenField ID="hdnTripid" runat="server" />
    <asp:HiddenField ID="hdnEligible" runat="server" />

    <asp:HiddenField ID="hdnTryiptypeid" runat="server" />

    <asp:HiddenField ID="hdnCOS" runat="server" />

    <asp:HiddenField ID="hdnchkMealInc" runat="server" />

    <asp:HiddenField ID="hflGrade" runat="server" />

    <asp:HiddenField ID="hdnDeviation" runat="server" />
    <asp:HiddenField ID="hdnDesk" runat="server" />
    <asp:HiddenField ID="HiddenField1" runat="server" />

    <asp:HiddenField ID="hdnfromdate" runat="server" />
    <asp:HiddenField ID="hdnTodate" runat="server" />
    <asp:HiddenField ID="hdntrdetailsid" runat="server" />
    <asp:HiddenField ID="hdnexp_id" runat="server" />
    <asp:HiddenField ID="hdnInboxType" runat="server" />
        <asp:HiddenField ID="hdnFreshByTD" runat="server" />

     <asp:HiddenField ID="hdnactualDeparturedate" runat="server" />
     <asp:HiddenField ID="hdnactualArrivaldate" runat="server" />
     <asp:HiddenField ID="hdnaddedbyTDFlg" runat="server" />

    <script type="text/javascript">

        function SetDeviation(Deviation) {
            if (Deviation != "") {
                document.getElementById("<%=txtDeviation.ClientID%>").value = Deviation;
            }
            return;
        }

        function SetCntrls(Deviation) {
          
            if (Deviation == "Booked")
            {   
                document.getElementById("<%=txtFromTime.ClientID%>").disabled = false
                document.getElementById("<%=txtToTime.ClientID%>").disabled = false
                document.getElementById("<%=txtfare.ClientID%>").disabled = false
                document.getElementById("<%=txtDestination.ClientID%>").disabled = false
                document.getElementById("<%=txtOrigin.ClientID%>").disabled = false
                document.getElementById("<%=txtActualDeptDate.ClientID%>").disabled = false
                document.getElementById("<%=txtActualArrivalDate.ClientID%>").disabled = false
                document.getElementById("<%=txtfortravelRequest.ClientID%>").disabled = false
               
            }
            else
            {
                document.getElementById("<%=txtFromTime.ClientID%>").disabled = true
                document.getElementById("<%=txtToTime.ClientID%>").disabled = true
                document.getElementById("<%=txtfare.ClientID%>").disabled = true
                document.getElementById("<%=txtDestination.ClientID%>").disabled = true
                document.getElementById("<%=txtOrigin.ClientID%>").disabled = true
                document.getElementById("<%=txtActualDeptDate.ClientID%>").disabled = true
                document.getElementById("<%=txtActualArrivalDate.ClientID%>").disabled = true
                document.getElementById("<%=txtfortravelRequest.ClientID%>").disabled = true
              

                document.getElementById("<%=txtFromTime.ClientID%>").value = "";
                document.getElementById("<%=txtToTime.ClientID%>").value = "";
                document.getElementById("<%=txtfare.ClientID%>").value = "";
                document.getElementById("<%=txtActualDeptDate.ClientID%>").value = "";
                document.getElementById("<%=txtActualArrivalDate.ClientID%>").value = "";
                document.getElementById("<%=txtfortravelRequest.ClientID%>").value = "";
                

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
    </script>
</asp:Content>

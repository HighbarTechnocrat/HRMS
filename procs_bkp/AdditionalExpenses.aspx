<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="AdditionalExpenses.aspx.cs" Inherits="myaccount_AdditionalExpenses" %>


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
            height: 80px;
            /*overflow:initial;*/
        }
        .taskparentclass2 {
            width: 29.5%;
            height: 112px !important;
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
                        <asp:Label ID="lblheading" runat="server" Text="Expenses Details"></asp:Label>
                    </span>
                </div>

                <div class="edit-contact">
                    <div class="editprofile" style="text-align: center; border: none;" id="divmsg" runat="server" visible="true">
                        <asp:Label runat="server" ID="lblmessages" Visible="true" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
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
                          <asp:Label runat="server" ID="lblmessage"  Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                        </li>

                          <li class="trvldetails_deviation">

                            <%-- <span>Deviation</span><br />--%>

                            <asp:TextBox AutoComplete="off" ID="TextBox1" runat="server" Visible="false"></asp:TextBox>

                        </li>

                        <li class="trvldetails_tripid">
                            <%--<span>Trip ID</span><br />--%>

                            <asp:TextBox AutoComplete="off" ID="txtTripId_Oth" runat="server" Visible="false"></asp:TextBox>

                        </li>
                        <li class="trvldetails_deviation">

                            <%-- <span>Deviation</span><br />--%>

                            <asp:TextBox AutoComplete="off" ID="TextBox2" runat="server" Visible="false"></asp:TextBox>

                        </li>

                        <li class="trvldetails_type">
                            <span>Type of Travel</span>&nbsp;&nbsp;<span style="color:red">*</span><br />


                            <asp:TextBox AutoComplete="off" ID="txtTravelType_Oth" runat="server" CssClass="grayDropdown"></asp:TextBox>
                            <asp:Panel ID="Panel3" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstTravelType_Oth" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstTravelType_Oth_SelectedIndexChanged">
                                            <%--   <asp:ListItem>Domestic</asp:ListItem>
                                            <asp:ListItem>International</asp:ListItem>--%>

                                        </asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender2" PopupControlID="Panel3" TargetControlID="txtTravelType_Oth"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>

                        </li>
                        <li class="trvldetails_deviation">

                            <%-- <span>Deviation</span><br />--%>

                            <asp:TextBox AutoComplete="off" ID="TextBox3" runat="server" Visible="false"></asp:TextBox>

                        </li>

                        <li>
                            <span>Details</span>&nbsp;&nbsp;<span style="color:red">*</span><br />

                            <asp:TextBox AutoComplete="off" ID="txtExpdtls_Oth" runat="server" CssClass="Dropdown"></asp:TextBox>
                            <asp:Panel ID="Panel2" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstExpdtls_Oth" runat="server" CssClass="taskparentclass2" AutoPostBack="true" OnSelectedIndexChanged="lstExpdtls_Oth_SelectedIndexChanged"></asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender1" PopupControlID="Panel2" TargetControlID="txtExpdtls_Oth"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>

                        </li>
                        <li class="trvldetails_deviation">

                            <%-- <span>Deviation</span><br />--%>

                            <asp:TextBox AutoComplete="off" ID="TextBox6" runat="server" Visible="false"></asp:TextBox>

                        </li>

                        <li class="trvldetails_bookthrough">
                            <span>Amount  (INR)</span>&nbsp;&nbsp;<span style="color:red">*</span>
                           <asp:TextBox AutoComplete="off" ID="txtAmt_Oth" runat="server" MaxLength="10" ></asp:TextBox>
                             
                        </li>
                        <li class="trvldetails_deviation">
                          <asp:Label ID="lblnodays" runat="server" Text="Stay Duration (in days)"></asp:Label>
                            <asp:TextBox AutoComplete="off" ID="txtnoofDays_Oth" runat="server" MaxLength="5" AutoPostBack="true" OnTextChanged="txtnoofDays_Oth_TextChanged"></asp:TextBox>

                        </li>
                         <li class="trvldetails_bookthrough">
                            <span>Remarks</span>
                           <asp:TextBox AutoComplete="off" ID="txtRemarks_Oth" runat="server" MaxLength="30"></asp:TextBox>
                             
                        </li>

                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div style="display:none">
        <ul>
                        <li class="trvldetails_deviation" style="display:none">
                             <span style="visibility:hidden">Deviation</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtDeviation"  Visible="false" runat ="server" MaxLength="10" BackColor="#c0c0c0"></asp:TextBox>
                        </li>
                        <li class="trvldetails_deviation" style="display:none">

                            <%-- <span>Deviation</span><br />--%>

                            <asp:TextBox AutoComplete="off" ID="TextBox7" runat="server" Visible="false"></asp:TextBox>

                        </li>
                        <li class="trvldetails_departuredate" style="display:none">
                            <span>Date </span>&nbsp;&nbsp;<span style="color:red">*</span>
                            <br />

                            <asp:TextBox AutoComplete="off" ID="txtFromdate" runat="server" AutoPostBack="true" OnTextChanged="txtFromdate_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtFromdate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>
                        <li class="trvldetails_deviation" style="display:none">

                            <%-- <span>Deviation</span><br />--%>

                            <asp:TextBox AutoComplete="off" ID="TextBox8" runat="server" Visible="false"></asp:TextBox>

                        </li>
                        <li class="trvldetails_place" style="display:none">

                            <span style="visibility:hidden">Place</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtOrigin" runat="server" CssClass="Dropdown" Visible="false"></asp:TextBox>
                            <asp:Panel ID="Panel4" Style="display: none;" runat="server" CssClass="taskparentclasskkk" Visible="false">
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
                        <li class="trvldetails_deviation" style="display:none">

                            <%-- <span>Deviation</span><br />--%>

                            <asp:TextBox AutoComplete="off" ID="TextBox9" runat="server" Visible="false"></asp:TextBox>

                        </li>

                        <li class="trvldetails_arrivaldate" style="display:none">
                            <span>Location </span>&nbsp;&nbsp;<span style="color:red">*</span><br />
                            <asp:TextBox AutoComplete="off" ID="txttrvlLocation" runat="server" MaxLength="150"  placeholder="Select Location"   ></asp:TextBox>
                            <asp:TextBox AutoComplete="off" ID="TextBox5" runat="server" MaxLength="150" Visible="false" CssClass="Dropdown"></asp:TextBox>
                             <asp:Panel ID="Panel7" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstLocation" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstLocation_SelectedIndexChanged">
                                            <%--   <asp:ListItem>Domestic</asp:ListItem>
                                            <asp:ListItem>International</asp:ListItem>--%>
                                        </asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>  

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender6" PopupControlID="Panel7" TargetControlID="TextBox5"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>
                                     <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchCities" MinimumPrefixLength="1"
                        CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="txttrvlLocation"
                        ID="AutoCompleteExtender1" runat="server" FirstRowSelected="true">
                    </ajaxToolkit:AutoCompleteExtender>
                        </li>
                        <li class="trvldetails_deviation" style="display:none">

                            <%-- <span>Deviation</span><br />--%>

                            <asp:TextBox AutoComplete="off" ID="TextBox10" runat="server" Visible="false"></asp:TextBox>

                        </li>

                        <li class="trvldetails_place2">

                            <span style="visibility:hidden">Place</span><br />

                            <asp:TextBox AutoComplete="off" ID="txtDestination" runat="server" CssClass="Dropdown" Visible="false"></asp:TextBox>
                            <asp:Panel ID="Panel5" Style="display: none;" runat="server" CssClass="taskparentclasskkk"  Visible="false">
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
                        <li class="trvldetails_deviation">

                            <%-- <span>Deviation</span><br />--%>

                            <asp:TextBox AutoComplete="off" ID="TextBox11" runat="server" Visible="false"></asp:TextBox>

                        </li>
        </ul>
    </div>
    <div class="trvldetails_Savebtndiv">
        <asp:LinkButton ID="Oth_btnSave" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClientClick=" return MultiClick_Oth();"  OnClick="Oth_btnSave_Click"></asp:LinkButton>


        <asp:LinkButton ID="Oth_btnDelete" runat="server" Text="Delete" ToolTip="Delete" CssClass="Savebtnsve" OnClick="Oth_btnDelete_Click"></asp:LinkButton>

        <asp:LinkButton ID="Oth_btnCancel" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="Oth_btnCancel_Click"></asp:LinkButton>

    </div>
    <asp:TextBox AutoComplete="off" ID="txtEmpCode_Oth" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>

    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />
  <ul>
        <li class="trvldetails_requirement">
            <span style="display:none">Paid By: </span>
            <asp:TextBox AutoComplete="off" ID="txtpaidby" runat="server" Visible="false" Text="Employee" CssClass="Dropdown" Enabled="false"></asp:TextBox>
            <asp:Panel ID="Panel6" runat="server" CssClass="taskparentclasskkk">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:ListBox ID="lstpaidby" runat="server" CssClass="taskparentclasskkk"  Visible="false" AutoPostBack="true" OnSelectedIndexChanged="lstpaidby_SelectedIndexChanged">
                            <asp:ListItem Text="Employee" Value="Employee">Employee</asp:ListItem>
                            <asp:ListItem Text="Company" Value="Company">Company</asp:ListItem>
                        </asp:ListBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>                            
            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender5" PopupControlID="Panel6" TargetControlID="txtpaidby"
                Position="Bottom" runat="server">
            </ajaxToolkit:PopupControlExtender>

        </li>
                        <li class="trvldetails_deviation">

                            <%-- <span>Deviation</span><br />--%>

                            <asp:TextBox AutoComplete="off" ID="TextBox4" runat="server" Visible="false"></asp:TextBox>

                        </li>
  </ul>
    <asp:HiddenField ID="hdnTryiptypeid" runat="server" />
         <asp:HiddenField ID="hdnTripid" runat="server" />
    <asp:HiddenField ID="hdnexp_id" runat="server" />

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

    <script type="text/javascript">


        function SetIncidentalCharges(IncidentalCharges) {

            //alert("Ok : ");//IncidentalCharges);

            if (IncidentalCharges != "") {
                document.getElementById("<%=txtAmt_Oth.ClientID%>").value = IncidentalCharges;
                document.getElementById("<%=hdntxtAmt_Oth.ClientID%>").value = IncidentalCharges;
            }
            return;
        }


        function SetDeviation(Deviation)
        {
            //alert(Deviation);
           

            if (Deviation != "")
            {
                document.getElementById("<%=txtDeviation.ClientID%>").value = Deviation;
                //document.getElementById("<%=txtnoofDays_Oth.ClientID%>").disabled = false
                document.getElementById("<%=txtnoofDays_Oth.ClientID%>").style.backgroundColor = "#ebebe4"; //"#ffffff"; // backcolor
            }
            else
            {
                //document.getElementById("<%=txtnoofDays_Oth.ClientID%>").disabled = true
                document.getElementById("<%=txtnoofDays_Oth.ClientID%>").value = "";
                document.getElementById("<%=txtnoofDays_Oth.ClientID%>").style.backgroundColor = "#ebebe4"; // backcolor
            }
            
            return;
        }
        function SetCntrsl(allowance, IncidentalCharges) {
            //alert(allowance)

            if (IncidentalCharges != "") {
                document.getElementById("<%=txtAmt_Oth.ClientID%>").value = IncidentalCharges;
                document.getElementById("<%=hdntxtAmt_Oth.ClientID%>").value = IncidentalCharges;
            }
            else
            {
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
        


        function textboxMultilineMaxNumber(txt, maxLen) {
            try {
                if (txt.value.length > (maxLen - 1)) return false;
            } catch (e) {
            }
            return;
        }


        function maxLengthPaste(field, maxChars) {
            event.returnValue = false;
            if ((field.value.length + window.clipboardData.getData("Text").length) > maxChars) {
                return false;
            }
            event.returnValue = true;
            return;
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

    </script>
</asp:Content>

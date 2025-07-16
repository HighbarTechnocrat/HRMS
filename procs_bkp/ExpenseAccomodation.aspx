<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="ExpenseAccomodation.aspx.cs" Inherits="ExpenseAccomodation" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
   
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
<%--    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
      <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Accomodation_css.css" type="text/css" media="all" />  
  
    <style>
         .Dropdown {
            border-bottom: 2px solid #cccccc;
            /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
            background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;
            
            cursor: default;
        }
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
             /*background: #dae1ed;*/
           background:#ebebe4;
        }
        .taskparentclasskkk {
            width: 32%;
            height: 72px;
            /*overflow:initial;*/
        }
        .graytextbox {
            border-bottom: 2px solid #cccccc;
            /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/            
            background-color:#ebebe4;
        }

        .accmo_test
        {
            cursor: pointer !important;
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
                            <asp:DropDownList ID="ddlprofile" runat="server" AutoPostBack="true" >
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
                       <asp:Label ID="lblheading" runat="server" Text=" Expenses Accommodation"></asp:Label>
                    </span>
                </div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>

                <div class="edit-contact">
                    <div class="editprofile" style="text-align: center; border: none;" id="divmsg" runat="server" >
                       
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

                        <li class="Accomodation_trip">
                            <%--<span>Trip ID</span><br />--%>

                            <asp:TextBox AutoComplete="off" ID="txtTripId" runat="server" Visible="false"></asp:TextBox>

                        </li>
                        
                        <li class="Accomodation_trip">
                          <%--  <span>Trip ID</span><br />--%>

                            <asp:TextBox AutoComplete="off" ID="TextBox1" runat="server" Visible="False"></asp:TextBox>

                        </li>
                        <li class="Accomodation_type" >
                            <span>Travel Type</span><br />

                            <asp:TextBox AutoComplete="off" ID="txtTravelType" runat="server" CssClass="Dropdown" Visible ="True"></asp:TextBox>
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


                        <li class="Accomodation_type" >
                            <span id="spnArrgment_accmodation" runat="server" visible="false" >Accommodation Type</span><br />

                            <asp:TextBox AutoComplete="off" ID="txtAcctype" runat="server" CssClass="Dropdown" Visible="false"></asp:TextBox>
                            <asp:Panel ID="Panel2" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstAccType" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstAccType_SelectedIndexChanged" Visible="false">
                                             <asp:ListItem>Hotel</asp:ListItem>
                                            <asp:ListItem>Guest House (with Food)  </asp:ListItem>
                                            <asp:ListItem>Guest House (without Food)</asp:ListItem>
                                            <asp:ListItem>Own Arrangement</asp:ListItem>
                                            
                                        </asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender1" PopupControlID="Panel2" TargetControlID="txtAcctype"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>
                        </li>
                        <li class="Arrangement">
                            <span id="spnArrgment" runat="server">Accommodation Type</span><br />      
                             <span id="spnArrgment_2" visible="false" runat="server"><br /> </span>

                                <asp:RadioButton ID="rdoAccomodation" runat="server" Text="Hotel" ToolTip="Hotel"   GroupName="arrmgnts" OnCheckedChanged="rdoAccomodation_CheckedChanged" Checked="true" CssClass="accmo_test"  AutoPostBack="true" />
                                <asp:RadioButton ID="rdoFood" runat="server" Text="Guest House (with Food)" ToolTip="Guest House (with Food)" GroupName="arrmgnts" OnCheckedChanged="rdoAccomodation_CheckedChanged" CssClass="accmo_test2" AutoPostBack="true"/>
                         
                                <asp:RadioButton ID="rdoFoodAccomodation" runat="server" Text="Guest House (without Food)" ToolTip="Guest House (without Food)" GroupName="arrmgnts" OnCheckedChanged="rdoAccomodation_CheckedChanged" CssClass="accmo_test3" AutoPostBack="true"/>
                                <asp:RadioButton ID="rdoOwnArgmnet" runat="server" Text="Own Arrangement" ToolTip="Own Arrangement" GroupName="arrmgnts" OnCheckedChanged="rdoAccomodation_CheckedChanged" CssClass="accmo_test4" AutoPostBack="true"/>
                             
                            <span id="spnArrgment_3" visible="false" runat="server"><br /> <br /> <br /> </span>
                                                        
                        </li>
                         
                        
                        <li class="Accomodation_trip">
                            <%--<span>Actual </span><br />--%>
                            <asp:TextBox AutoComplete="off" ID="TextBox3" runat="server" Visible="false"></asp:TextBox>
                        </li>

                        <li class="Accomodation_location">
                            <span>Location </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtLocation" runat="server" MaxLength="100"  placeholder="Select Location"></asp:TextBox>
                            <asp:TextBox AutoComplete="off" ID="TextBox2" runat="server" MaxLength="100" Visible="false" CssClass="Dropdown"></asp:TextBox>
                               <asp:Panel ID="Panel6" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstLocation" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstLocation_SelectedIndexChanged">
                                            <%--   <asp:ListItem>Domestic</asp:ListItem>
                                            <asp:ListItem>International</asp:ListItem>--%>

                                        </asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender5" PopupControlID="Panel6" TargetControlID="TextBox2"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>

                                   <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchCities" MinimumPrefixLength="1"
                            CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtLocation"
                            ID="AutoCompleteExtender1" runat="server" FirstRowSelected="true">
                        </ajaxToolkit:AutoCompleteExtender>
                        </li>

                     <li class="Accomodation_trip">
                            <%--<span>Actual </span><br />--%>

                            <asp:TextBox AutoComplete="off" ID="TextBox4" runat="server" Visible="false"></asp:TextBox>

                        </li>
                     <li class="Accomodation_fromdate">
                            <span>From </span>
                            <br />

                            <asp:TextBox AutoComplete="off" ID="txtFromdate" runat="server" AutoPostBack="True" OnTextChanged="txtFromdate_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtFromdate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>

                            <%--  <asp:TextBox AutoComplete="off" ID="txtsadate" runat="server" CssClass="txtdatepicker" placeholder="Start Date" OnTextChanged ="txtsadate_TextChanged" AutoPostBack ="true" ></asp:TextBox>
                                <span class="texticon tooltip" title="Select Start Date. Date should be MM/DD/YYYY format."><i class="fa fa-calendar" aria-hidden="true"></i></span>
                                <asp:Label ID="lblsdate" runat="server" Text="" Font-Size="15px"></asp:Label>--%>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="formerror" ControlToValidate="txtsadate" SetFocusOnError="True" ErrorMessage="Please enter start date" ValidationGroup="validate"></asp:RequiredFieldValidator>--%>

                    </li>

                 

                        <li class="Accomodation_date">
                            <span>To</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtToDate" runat="server" AutoPostBack="True" OnTextChanged="txtToDate_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>
                       
                         <li class="Accomodation_date">
                            <span>Actual Stay Duration (in days)</span> &nbsp;&nbsp;<span style="color:red">*</span><br />
                              <asp:TextBox AutoComplete="off" ID="txtnoofDays" runat="server" MaxLength="5" AutoPostBack="true" OnTextChanged="txtnoofDays_TextChanged"></asp:TextBox>
                        </li>
                         <li class="Accomodation_date">                            
                            <asp:TextBox Visible="false" AutoComplete="off" ID="TextBox10" runat="server" AutoPostBack="True" OnTextChanged="txtToDate_TextChanged"></asp:TextBox>                            
                        </li>
                         <li class="Accomodation_trip" id="liAccomdation_charges_1" runat="server">
                           <%-- <span id="spnActual" runat="server" >Accommodation charges </span><br /><br />--%>
                             <span id="Span2" runat="server" >Boarding and Lodging Charges </span><br /><br />
                              
                           <%-- <asp:TextBox AutoComplete="off" ID="TextBox5" runat="server" Visible="false"></asp:TextBox>--%>
                        </li>

                        <li class="Accomodation_trip" id="liAccomdation_charges_2" runat="server">
                            <%--<span>Actual </span><br />--%>
                            <asp:TextBox AutoComplete="off" ID="TextBox5" runat="server" Visible="false"></asp:TextBox>
                        </li>

                        <li class="Accomodation_type"  id="liAccomdation_charges_paidby" runat="server">
                            <span>Paid By</span><br />

                            <asp:TextBox AutoComplete="off" ID="txtPaidBy" runat="server" CssClass="Dropdown" Visible ="True"></asp:TextBox>
                            <asp:Panel ID="Panel4" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstPaidBy" runat="server" CssClass="taskparentclasskkk" AutoPostBack="True" OnSelectedIndexChanged="lstPaidBy_SelectedIndexChanged">
                                            <asp:ListItem>Company</asp:ListItem>
                                            <asp:ListItem>Employee</asp:ListItem>

                                        </asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender3" PopupControlID="Panel4" TargetControlID="txtPaidBy"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>
                        </li>
                        
                        
                         <li class="Accomodation_requirement" id="liAccomdation_charges_charges" runat="server">
                            <span>Amount (INR): </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtCharges" runat="server"  OnTextChanged="txtAdditionalFoodExp_exp_TextChanged" AutoPostBack="true"  MaxLength="10"></asp:TextBox>
                        </li>

                        <li class="Accomodation_trip" id="liAccomdation_Food_exp_1" runat="server">
                            <span id="Span1" runat="server" >Additional Food Expenses </span><br /><br />
                        </li>
                        <li class="Accomodation_trip" id="liAccomdation_Food_exp_2" runat="server">
                            <%--<span>Actual </span><br />--%>
                            <asp:TextBox AutoComplete="off" ID="TextBox7" runat="server" Visible="false"></asp:TextBox>
                        </li>
                          <li class="Accomodation_type" id="liAccomdation_Food_paidby" runat="server">
                            <span>Paid By</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtAdditionalFoodExp_emp" runat="server"  Enabled="false" Text="Employee"></asp:TextBox>                                                       
                        </li>
                        
                        
                         <li class="Accomodation_requirement" id="liAccomdation_Food_Charges" runat="server">
                            <span>Amount (INR): </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtAdditionalFoodExp_exp" runat="server" OnTextChanged="txtAdditionalFoodExp_exp_TextChanged" AutoPostBack="true" MaxLength="10"></asp:TextBox>
                        </li>                        

                         <li class="Accomodation_requirement"  id="liAccomdation_Food_Deviation" runat="server">
                            <span>Deviation: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtDeviation" runat="server"  CssClass="graytextbox" ReadOnly="true" MaxLength="100"></asp:TextBox>
                        </li>
                        
                         <li class="Accomodation_requirement" id="liAccomdation_Food_Eligibility" runat="server">
                            <span>Eligibility: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtEligibility" runat="server" CssClass="graytextbox" ReadOnly="true" MaxLength="10"></asp:TextBox>
                        </li>

                        <%--<!-- For Flat Rate --%>

                         <li class="Accomodation_trip" id="liAddintional_exps_1" runat="server">
                            <%--<span>Flat Rate: </span><br /><br /> Additional Expenses--%>
                             <span id="spnAdditionalExp" runat="server"> Flat Rate:  </span><br /><br />                             
                            <asp:TextBox AutoComplete="off" ID="TextBox6" runat="server" Visible="false"></asp:TextBox>
                        </li>
                        <li id="liAddintional_exps_2" runat="server"></li>
                        <li class="Accomodation_type"  id="liAddintional_exps_flatpaid_1" runat="server" >
                          <%--  <span>Paid By</span><br />--%>
                               <span>Additional Expenses</span><br /> 
                                <asp:TextBox AutoComplete="off" ID="txtaddintionalExpens" runat="server" OnTextChanged="txtaddintionalExpens_TextChanged" AutoPostBack="true"></asp:TextBox> 
                                  
                            <asp:TextBox AutoComplete="off" ID="txtFlatPaid" runat="server"  Visible ="false"></asp:TextBox>                               
                            <asp:Panel ID="Panel5" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstFlatPaid" runat="server" Visible="false" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstFlatPaid_SelectedIndexChanged">
                                            <asp:ListItem>Company</asp:ListItem>
                                            <asp:ListItem>Employee</asp:ListItem>

                                        </asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender4" PopupControlID="Panel5" TargetControlID="txtFlatPaid"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>
                        </li>
      
                         <li class="Accomodation_requirement" id="liAddintional_exps_flatpaid_2" runat="server" >
                           
                        </li>
                        
                         <li class="Accomodation_requirement" id="liAddintional_exps_Charges" runat="server">
                            <%--<span>Deviation: </span>--%>
                           <%-- <br />--%>
                            <asp:TextBox AutoComplete="off" ID="txtFlatDev" runat="server" Visible="false" MaxLength="100"></asp:TextBox>
                              <span id="spnAdditionalCharges" runat="server">Amount (INR): </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtFlatChg" runat="server" MaxLength="100" AutoPostBack="true" OnTextChanged="txtFlatChg_TextChanged"></asp:TextBox>
                        </li>
                        
                         <li class="Accomodation_requirement" id="liAddintional_exps_eligibility" runat="server" >
                            <span id="spnAdditionaExp_eligibility" runat="server">Eligibility: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtFlatElg" runat="server"   CssClass="graytextbox" ReadOnly="true" MaxLength="10"></asp:TextBox>
                        </li>
                         <li class="Accomodation_requirement"  id="liAddintional_exps_deviation_1" runat="server">
                            <span>Deviation: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtAdditional_exp_deviation" runat="server"  CssClass="graytextbox" ReadOnly="true" MaxLength="100"></asp:TextBox>
                        </li>
                        
                         <li class="Accomodation_requirement" id="liAddintional_exps_deviation_2" runat="server">                            
                            <asp:TextBox AutoComplete="off" ID="textbox8" Visible="false" runat="server" CssClass="graytextbox" MaxLength="10"></asp:TextBox>
                        </li>
                         <li class="Accomodation_requirement">
                            <span>Remarks: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtRemarks" runat="server" MaxLength="30"></asp:TextBox>
                        </li>

                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="accmo_Savebtndiv">
          <asp:LinkButton ID="accmo_btnSave" OnClientClick=" return MultiClick();" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve"  OnClick="accmo_btnSave_Click">Submit</asp:LinkButton>
          <asp:LinkButton ID="accmo_delete_btn" runat="server" Text="Delete" ToolTip="Delete" CssClass="Savebtnsve" OnClick="accmo_delete_btn_Click">Delete</asp:LinkButton>
          <asp:LinkButton ID="accmo_cancel_btn" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="accmo_cancel_btn_Click">Back</asp:LinkButton>
    </div>
    <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>

    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />

    
    <asp:HiddenField ID="hdnTryiptypeid" runat="server" />

    <asp:HiddenField ID="hdnCOS" runat="server" />
     <asp:HiddenField ID="hdnTripid" runat="server" />
     <asp:HiddenField ID="hdnfromdate" runat="server" />
     <asp:HiddenField ID="hdnTodate" runat="server" />
     <asp:HiddenField ID="hdnAccdtlsid" runat="server" />
    <asp:HiddenField ID="hdnexp_id" runat="server" />
    <asp:HiddenField ID="hdnactualdays" runat="server" />
    <asp:HiddenField ID="hdntripcharges" runat="server" />

    <asp:HiddenField ID="hdnactualEligbility" runat="server" />
    <asp:HiddenField ID="hdnflatEligbility" runat="server" />
    <asp:HiddenField ID="hdnIsThrughCOS" runat="server" />
    <asp:HiddenField ID="hdnAccomodationStatus" runat="server" />
    <asp:HiddenField ID="hdnDaysDiff" runat="server" />

   
    <script type="text/javascript">
        function validateTripType() {
                
            document.getElementById("<%=txtFlatChg.ClientID%>").value = "";
            document.getElementById("<%=txtDeviation.ClientID%>").value = ""; 
            document.getElementById("<%=txtCharges.ClientID%>").value = "";  
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
        function setRadioBtns_disable()
        {
            var radios_1 = document.getElementById('rdoAccomodation');
                radios.disabled = true;
            
            var radios_2 = document.getElementById('rdoFoodAccomodation');
            radios_2.disabled = true;

            var radios_3 = document.getElementById('rdoFood');
            radios_3.disabled = true;

            var radios_4 = document.getElementById('rdoOwnArgmnet');
            radios_4.disabled = true;


        }

        function MultiClick() {
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

    </script>
</asp:Content>

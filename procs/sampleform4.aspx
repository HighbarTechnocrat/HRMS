<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sampleform4.aspx.cs" Inherits="myaccount_sampleform4" %>--%>

<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sampleform7.aspx.cs" Inherits="myaccount_sampleform7" %>--%>

<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sampleform6.aspx.cs" Inherits="myaccount_sampleform6" %>--%>

<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sampleform2.aspx.cs" Inherits="sampleform2" %>--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="sampleform4.aspx.cs" Inherits="myaccount_SampleForm7" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link href="<%=ReturnUrl("css") %>calendar/calendar.css" rel="stylesheet" type="text/css" />
     <link href="sampleform_css.css" rel="stylesheet" type="text/css" />
    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }
        
        /*Jayesh_Prajyot Added below  background color code 13sep2017 */
           .aspNetDisabled {
            background: #dae1ed;
        }
        /*Jayesh_Prajyot Added below  background color code 13sep2017 */
        
        /*Jayesh_Prajyot
        .aspNetDisabled {
            background: #dae1ed;
        }
       */
        /*.editprofile {
            margin: 0 !important;
            width: auto !important;
            float: none !important;
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
                                <li class="listselected"><a href="<%=ReturnUrl("sitepathmain") %>procs/SampleForm" title="Edit Profile"><b>Edit Profile</b></a></li>
                                <li><a href="<%=ReturnUrl("sitepathmain") %>procs/wishlist" title="Favorites">Favorites</a></li>
                                <asp:Panel ID="Panel1" runat="server" Visible="false">
                                    <li><a href="<%=ReturnUrl("sitepathmain") %>procs/preference" title="Preference">Preference</a></li>

                                    <li id="lihistory" runat="server"><a href="<%=ReturnUrl("sitepathmain") %>procs/subscriptionhistory" title="Subscription History">Subscription History</a></li>
                                    <li><a href="<%=ReturnUrl("sitepathmain") %>procs/pthistory" title=" Reward Points">Reward Points</a></li>

                                    <li><a href="<%=ReturnUrl("sitepathmain") %>recommend" title="Recommendation">Recommendation</a></li>
                                </asp:Panel>
                                <li>
                                    <asp:LinkButton ID="btnSingOut" runat="server" ToolTip="Logout"
                                        Text="Logout" OnClick="btnSingOut_Click"> </asp:LinkButton>
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
                                            <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtothercity" WatermarkText="Add Other City" />
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
                        <%--prajyot added this for heading--%>
                        <asp:Label ID="lblheading" runat="server" Text="Manage Attendance Regularization"></asp:Label>
                    </span>
                </div>
                 <%-- JAYESH_PRAJYOT ADDED BELOW CODE FOR REQUEST  FORM 18sep2017--%>
               <%-- <div class="containerDiv">


  <div class="rowDivHeader rowDivHeader1">
    <div class="cellDivHeader cellDivHeader1">Date</div>
    <div class="cellDivHeader cellDivHeader2">Type</div>
    <div class="cellDivHeader cellDivHeader3">Time</div>
    <div class="cellDivHeader cellDivHeader4"> Staatus</div>
    <div class="cellDivHeader cellDivHeader5">Category*</div>
      <div class="cellDivHeader cellDivHeader6">Reason / Remark</div>
      <div class="cellDivHeader cellDivHeader7">Select</div>
  </div>

  <div class="rowDiv">
    <div class="cellDiv cell1">'01/09/2017</div>
    <div class="cellDiv cell2">Out</div>
    <div class="cellDiv cell3">04:30:00 PM</div>
    <div class="cellDiv cell4">ABSENT</div>
      <div class="cellDiv cell5">Early Going</div>
    <div class="cellDiv cell6">Early Going</div>
      <div class="cellDiv cell "></div>
    
  </div>
   <div class="rowDiv">
   <div class="cellDiv cellwpl">'01/09/2017</div>
    <div class="cellDiv cellwp2">Out</div>
    <div class="cellDiv cellwp3">04:30:00 PM</div>
    <div class="cellDiv ">ABSENT</div>
      <div class="cellDiv ">Early Going</div>
    <div class="cellDiv ">ttt   </div>
      <div class="cellDiv celldiv2 "></div>
    
  </div>
   <div class="rowDiv">
   <div class="cellDiv cellwpl">'01/09/2017</div>
    <div class="cellDiv cellwp2">Out</div>
    <div class="cellDiv cellwp3">04:30:00 aM</div>
    <div class="cellDiv ">ABSENT</div>
      <div class="cellDiv ">Early Going</div>
    <div class="cellDiv ">ttt</div>
      <div class="cellDiv celldiv2 "></div>
    
  </div>
   <div class="rowDiv">
    <div class="cellDiv cellwpl">'01/09/2017</div>
    <div class="cellDiv cellwp2">Out</div>
    <div class="cellDiv cellwp3">04:30:00 PM</div>
    <div class="cellDiv ">ABSENT</div>
      <div class="cellDiv ">Early Going</div>
    <div class="cellDiv ">ttt</div>
      <div class="cellDiv celldiv2 "></div>
    
  </div>
   <div class="rowDiv">
    <div class="cellDiv cellwpl">'01/09/2017</div>
    <div class="cellDiv cellwp2">Out</div>
    <div class="cellDiv cellwp3">04:30:00 PM</div>
    <div class="cellDiv ">ABSENT</div>
      <div class="cellDiv ">Early Going</div>
    <div class="cellDiv ">ttt</div>
      <div class="cellDiv celldiv2 "></div>
    
  </div>
     <div class="rowDiv">
   <div class="cellDiv cellwpl">'01/09/2017</div>
    <div class="cellDiv cellwp2">Out</div>
    <div class="cellDiv cellwp3">04:30:00 PM</div>
    <div class="cellDiv ">ABSENT</div>
      <div class="cellDiv ">Early Going</div>
    <div class="cellDiv ">ttt</div>
      <div class="cellDiv celldiv2 "></div>
  </div>
    <div class="rowDiv">
  <div class="cellDiv cellwpl">'01/09/2017</div>
    <div class="cellDiv cellwp2">Out</div>
    <div class="cellDiv cellwp3">04:30:00 PM</div>
    <div class="cellDiv ">ABSENT</div>
      <div class="cellDiv ">Early Going</div>
    <div class="cellDiv ">ttt</div>
      <div class="cellDiv celldiv2 "></div>
    
  </div>
    <div class="rowDiv">
  <div class="cellDiv cellwpl">'01/09/2017</div>
    <div class="cellDiv cellwp2">Out</div>
    <div class="cellDiv cellwp3">04:30:00 PM</div>
    <div class="cellDiv ">ABSENT</div>
      <div class="cellDiv ">Early Going</div>
    <div class="cellDiv ">ttt</div>
      <div class="cellDiv celldiv2 "></div>
    
  </div>
</div>--%>
<%--SAGAR ADDED THIS FOR ADDING BUTTON 21SEPT2017--%>
 <%-- <a href="#" class="aaa" onclick="">view calender</a>--%>

 <%-- JAYESH_PRAJYOT ADDED BELOW CODE FOR REQUEST  FORM 18sep2017--%>

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
                        <%--<li>
                            <span><span>*&nbsp;</span>Employee Code</span><br />
                            <asp:TextBox ID="txtfirstname" runat="server" MaxLength="50"> </asp:TextBox>
                            <span class="texticon tooltip" title="Please provide your text here."><i class="fa fa-user"></i></span>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtfirstname" SetFocusOnError="True" ErrorMessage="Please enter first name" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtfirstname" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />
                        </li>--%>
                        <%--<li>
                            <span><span>*&nbsp;</span>Employee Name</span><br />
                            <asp:TextBox ID="txtlastname" runat="server" Columns="30" MaxLength="50"> </asp:TextBox>
                            <span class="texticon tooltip" title="Please provide your last name."><i class="fa fa-user"></i></span>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="formerror" ControlToValidate="txtlastname" SetFocusOnError="True" ErrorMessage="Please enter last name" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="regName1" runat="server" CssClass="formerror" ControlToValidate="txtlastname" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />
                        </li>--%>
                       <%-- <li>
                            <span><span>*&nbsp;</span>Designation</span><br />
                            <asp:TextBox ID="txtemail" runat="server" Columns="30" MaxLength="20"> </asp:TextBox>
                            <span class="texticon tooltip" title="Please provide your User Name."><i class="fa fa-user" aria-hidden="true"></i></span><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="formerror" ControlToValidate="txtemail" SetFocusOnError="True" ErrorMessage="Please enter User Name" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
                        </li>--%>
                        
                        <li style="display: none;">
                            <span>Country</span><br />
                            <asp:UpdatePanel ID="Upl_country" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlcountry" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlcountry" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <span class="texticon"><i class="fa fa-globe"></i></span>
                        </li>
                        <li style="display: none;">
                            <span>State</span><br />
                            <asp:UpdatePanel ID="Upl_state" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlstate" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged"></asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlstate" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <span class="texticon"><i class="fa fa-globe"></i></span>
                        </li>
                     <%--   <li>
                            <span>Department</span><br />
                            <asp:TextBox ID="txtcity" runat="server"> </asp:TextBox>
                            <asp:UpdatePanel ID="Upl_city" runat="server" Visible="false">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlcity1" runat="server" OnSelectedIndexChanged="ddlcity1_SelectedIndexChanged1" AutoPostBack="True"></asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlcity1" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <span class="texticon tooltip" title="Please enter your city."><i class="fa fa-globe"></i></span>
                            <asp:Label ID="lblcity" runat="server" Text=""></asp:Label>
                        </li>--%>
                       <%-- <li class="profile-edit">
                            <span>Application Type</span><br />
                            <ew:NumericBox ID="txtpincode" runat="server" MaxLength="6" CssClass="medium"> </ew:NumericBox>
                            <span class="texticon tooltip" title="Please provide your area pincode."><i class="fa fa-code"></i></span>
                            <br />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="Dynamic" ControlToValidate="txtpincode" ErrorMessage="Please enter valid Pin Code!" SetFocusOnError="true" CssClass="formerror"
                                ValidationExpression="\d{6}(-\d{4})?$" ValidationGroup="validate"></asp:RegularExpressionValidator>

                        </li>--%>
                        <%-- <li class="profile-edit1 lihight">
                            <span><span>*&nbsp;</span>Leave Type:</span><br />
                           
                            <span class="gender"><span class="gendertexticon"><i class="fa fa-male"></i></span>
                                <asp:RadioButton ID="rbtnmale" Text="Male" runat="server" GroupName="gender" Checked="true" CssClass="rbtnprofile" />
                                <span class="gendertexticon"><i class="fa fa-female"></i></span>
                                <asp:RadioButton ID="rbtnfemale" Text="Female" runat="server" GroupName="gender" CssClass="rbtnprofile" /></span>
                        </li>--%>

                        <li class="profile-edit" id="divloc" runat="server" visible="false">
                            <span>Location</span><br />
                            <asp:TextBox ID="txtloc" runat="server"> </asp:TextBox>
                            <asp:UpdatePanel ID="upl_loc" runat="server" Visible="false">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlloc" runat="server" OnSelectedIndexChanged="ddlloc_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlloc" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <span class="texticon tooltip" title="Please enter your location."><i class="fa fa-globe"></i></span>
                            <asp:Label ID="lblloc" runat="server" Text=""></asp:Label>
                        </li>
                        <%--<li class="profile-edit">
                            <span><span>*&nbsp;</span>From</span><br />

                             
                            <asp:TextBox ID="txtdob1" runat="server"></asp:TextBox>
                            <span class="texticon tooltip" title="Please provide your From Date."><i class="fa fa-calendar"></i></span>
                            <br />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtdob1" Display="Dynamic" SetFocusOnError="True" CssClass="formerror" ValidationExpression="^(([0-9])|([0-2][0-9])|([3][0-1]))\/(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec|jan|feb|mar|apr|may|jun|jul|aug|sep|oct|nov|dec)\/\d{4}$"
                                ErrorMessage="Date should be in DD/MMM/YYYY format" ValidationGroup="validate" />--%>


                   <%--         <asp:TextBox ID="txtdept" runat="server"> </asp:TextBox>
                            <span class="texticon tooltip" title="Please enter your department."><i class="fa fa-info-circle"></i></span>
                            <asp:Label ID="lbldept" runat="server" Text=""></asp:Label><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="formerror" ControlToValidate="txtdept" SetFocusOnError="True" ErrorMessage="Please enter department" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>--%>
                        </li>
                        <li class="profile-edit" id="divsubdept" runat="server" visible="false">
                            <span>To</span><br />
                            <asp:TextBox ID="txtsubdept" runat="server"> </asp:TextBox>
                            <asp:UpdatePanel ID="upl_subdept" runat="server" Visible="false">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlsubdept" runat="server" OnSelectedIndexChanged="ddlsubdept_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlsubdept" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                            
                            <span class="texticon tooltip" title="Please enter your sub-department."><i class="fa fa-info-circle"></i></span>
                            <asp:Label ID="lblsubdept" runat="server" Text=""></asp:Label>
                        </li>
                        <%--<li class="profile-edit">
                            <span><span>*&nbsp;</span>Leave Days</span><br />
                            <asp:TextBox ID="txtdesg" runat="server"> </asp:TextBox>
                            <span class="texticon tooltip" title="Please enter your designation."><i class="fa fa-info-circle"></i></span>
                            <asp:Label ID="lbldesg" runat="server" Text=""></asp:Label><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" CssClass="formerror" ControlToValidate="txtdesg" SetFocusOnError="True" ErrorMessage="Please enter designation" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
                        </li>--%>

                      <%--  <li class="profile-edit">
                            <span>Reason for Leave</span><br />
                            <asp:FileUpload ID="uploadprofile" runat="server" />
                            <asp:TextBox ID="txtprofile" runat="server" CssClass="mobilenotextbox popup" Visible="false"> </asp:TextBox>
                            <span class="texticonselect tooltip" title="Please provide your profile picture."><i class="fa fa-file-image-o"></i></span>
                            <br />
                            <img id="imgprofile" runat="server" style="width: 35px; height: 35px; vertical-align: middle; border-radius: 2px; margin: 5px 0;" />
                            <img id="imgprofilecrop" runat="server" visible="false" />
                            <asp:LinkButton ID="removeprofile" runat="server" OnClick="removeprofile_Click"><span></span>&nbsp;&nbsp;Remove</asp:LinkButton>
                        </li>--%>

                       <%-- <li class="profile-edit">
                            <span>Upload File</span><br />
                            <asp:FileUpload ID="uploadcover" runat="server" />
                              <asp:TextBox ID="txtcover" runat="server" CssClass="mobilenotextbox popup" Visible="false"> </asp:TextBox>
                            <span class="texticonselect tooltip" title="Please provide your cover picture."><i class="fa fa fa-file-image-o"></i></span>
                            <br />
                            <img id="imgcover" runat="server" style="width: 35px; height: 35px; margin: 5px 0; border-radius: 2px;" />
                            <img id="imgcovercrop" runat="server" visible="false" />
                            <asp:LinkButton ID="removecover" runat="server" OnClick="removecover_Click"><span></span>&nbsp;&nbsp;Remove</asp:LinkButton>
                       <%-- </li>--%>
                       <%-- <li class="profile-edit">
                            <span>Request Dates</span><br />
                            <asp:TextBox ID="txtdob" runat="server"></asp:TextBox>
                            <span class="texticon tooltip" title="Please provide your birthdate."><i class="fa fa-calendar"></i></span>
                            <br />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtdob" Display="Dynamic" SetFocusOnError="True" CssClass="formerror" ValidationExpression="^(([0-9])|([0-2][0-9])|([3][0-1]))\/(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec|jan|feb|mar|apr|may|jun|jul|aug|sep|oct|nov|dec)\/\d{4}$"
                                ErrorMessage="Date should be in DD/MMM/YYYY format" ValidationGroup="validate" />
                        </li>--%>
                      <%--   <li>
                            <span><span>*&nbsp;</span>Intermediate Levels</span><br />
                            <asp:TextBox ID="txtemailadress" runat="server" MaxLength="150"> </asp:TextBox>
                            <span class="texticon tooltip" title="Please provide your email id."><i class="fa fa-envelope-o"></i></span><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="formerror" ControlToValidate="txtemailadress" SetFocusOnError="True" ErrorMessage="Please enter emailid" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
                        </li>--%>
                       
                        
                       <%-- <li class="profile-edit">

                            <span><span>*&nbsp;</span>TextBox 13</span><br />
                            <asp:TextBox ID="txtmobile" runat="server" CssClass="mobilenotextbox" MaxLength="16"> </asp:TextBox>
                            <span class="texticon tooltip" title="Please provide your mobile no."><i class="fa fa-mobile"></i></span>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="formerror" ControlToValidate="txtmobile" SetFocusOnError="True" ErrorMessage="Please enter mobile no." Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter valid mobile number" ControlToValidate="txtmobile" ValidationExpression="^([0-9\s-+]*\d{10}(-\d{9})?)$" ValidationGroup="validate" SetFocusOnError="true" CssClass="formerror"></asp:RegularExpressionValidator>
                        </li>--%>
                       <%-- <li class="profile-edit3">
                            <span>TextBOx 14</span><br />
                            <asp:TextBox ID="txtoffno" runat="server" CssClass="mobilenotextbox"> </asp:TextBox>
                            <span class="texticon tooltip" title="Please provide your office mobile no."><i class="fa fa-mobile"></i></span>
                            <br />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Enter valid mobile number" ControlToValidate="txtoffno" ValidationExpression="^([0-9\(\)\/\+ \-]*\d{10}(-\d{9})?)$" ValidationGroup="validate" SetFocusOnError="true" CssClass="formerror"></asp:RegularExpressionValidator>
                        </li>--%>
                       <%-- <li class="profile-edit3">
                            <span>TextBox 15</span><br />
                            <asp:TextBox ID="txtaltno" runat="server" CssClass="mobilenotextbox"> </asp:TextBox>
                            <span class="texticon tooltip" title="Please provide your altenate mobile no."><i class="fa fa-mobile"></i></span>
                            <br />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Enter valid mobile number" ControlToValidate="txtaltno" ValidationExpression="^([0-9\(\)\/\+ \-]*\d{10}(-\d{9})?)$" ValidationGroup="validate" SetFocusOnError="true" CssClass="formerror"></asp:RegularExpressionValidator>
                        </li>--%>
                       <%-- <li class="profile-edit3">
                            <span>TextBox 16</span><br />
                            <asp:TextBox ID="txtphone" runat="server" CssClass="mobilenotextbox" MaxLength="16"> </asp:TextBox>
                            <span class="texticon tooltip" title="Please provide your telephone no."><i class="fa fa-phone"></i></span>
                            <br />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Enter valid phone number" ControlToValidate="txtphone" ValidationExpression="^([0-9\(\)\/\+ \-]*\d{10}(-\d{9})?)$" ValidationGroup="validate" SetFocusOnError="true" CssClass="formerror"></asp:RegularExpressionValidator>
                        </li>
                        <li class="profile-edit3">
                            <span>TextBox 17</span><br />
                            <asp:TextBox ID="txtoffphone" runat="server" CssClass="mobilenotextbox" MaxLength="16"> </asp:TextBox>
                            <span class="texticon tooltip" title="Please provide your office telephone no."><i class="fa fa-phone"></i></span>
                            <br />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Enter valid phone number" ControlToValidate="txtoffphone" ValidationExpression="^([0-9\(\)\/\+ \-]*\d{10}(-\d{9})?)$" ValidationGroup="validate" SetFocusOnError="true" CssClass="formerror"></asp:RegularExpressionValidator>
                        </li>
                        <li class="profile-edit3">
                            <span>TextBox 18</span><br />
                            <asp:TextBox ID="txtextension" runat="server" CssClass="mobilenotextbox" MaxLength="6"> </asp:TextBox>
                            <span class="texticon tooltip" title="Please provide your office extension no."><i class="fa fa-phone"></i></span>
                            <br />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ErrorMessage="Enter valid phone number" ControlToValidate="txtextension" ValidationExpression="\d+" ValidationGroup="validate" SetFocusOnError="true" CssClass="formerror"></asp:RegularExpressionValidator>
                        </li>
                        <li class="profile-edit3" id="divfax" runat="server" visible="false">
                            <span>FAX No</span><br />
                            <asp:TextBox ID="txtfaxno" runat="server" CssClass="mobilenotextbox" MaxLength="16"> </asp:TextBox>
                            <span class="texticon tooltip" title="Please provide your Fax no."><i class="fa fa-phone"></i></span>
                            <br />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ErrorMessage="Enter valid Fax number" ControlToValidate="txtfaxno" ValidationExpression="^([0-9\(\)\/\+ \-]*\d{8}(-\d{7})?)$" ValidationGroup="validate" SetFocusOnError="true" CssClass="formerror"></asp:RegularExpressionValidator>
                        </li>
                        <li class="profile-edit3" id="divaltemail" runat="server" visible="false">
                            <span>Alternate Email ID</span><br />
                            <asp:TextBox ID="txtaltemail" runat="server" Columns="30" MaxLength="100"> </asp:TextBox>
                            <span class="texticon tooltip" title="Please provide your alternate email id."><i class="fa fa-envelope-o"></i></span>
                            <br />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ErrorMessage="Please Enter Valid Email ID"
                                ValidationGroup="validate" ControlToValidate="txtaltemail" CssClass="formerror" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$">  </asp:RegularExpressionValidator>
                        </li>
                        <li class="profile-edit2">
                            <span><span></span>TextBox 19 </span><br />
                            <asp:TextBox ID="txtaddress1" runat="server" MaxLength="250" TextMode="MultiLine" ValidationGroup="validate" onkeypress="return textboxMultilineMaxNumber(this,250)" onpaste='return maxLengthPaste(this,"250");' CssClass="medium" onKeyUp="javascript:Count(this);" onChange="javascript:Count(this);"></asp:TextBox>
                            <span class="texticon tooltip" title="Please provide your permanent address."><i class="fa fa-map-marker"></i></span>
                        </li>
                        <li class="profile-edit2">
                            <span><span></span>TextBox 20</span><br />
                            <asp:TextBox ID="txttempaddress" runat="server" MaxLength="250" TextMode="MultiLine" ValidationGroup="validate" onkeypress="return textboxMultilineMaxNumber(this,250)" onpaste='return maxLengthPaste(this,"250");' CssClass="medium" onKeyUp="javascript:Count(this);" onChange="javascript:Count(this);"></asp:TextBox>
                            <span class="texticon tooltip" title="Please provide your temporary address."><i class="fa fa-map-marker"></i></span>
                        </li>
                        <li>--%></li>
                       <%-- <li class="proviewbtn btndiv" style="float:left !important;">--%>
                           
                         <%-- JAYESH COMMNETED BELOW 19sep2017--%>
                            <%--  <div class="cancelbtndiv">
                                <asp:LinkButton ID="btnsubmit" runat="server" Text="Update" ToolTip="Update" ValidationGroup="validate" CssClass="submitbtnupdate" OnClick="btnSaveChanges_Click"><i class="fa fa-pencil" aria-hidden="true"></i>Update</asp:LinkButton>
                            </div>--%>
                <%--           JAYESH COMMNETED ABOVE to disabled the functionality of save19sep2017 --%>

                            


                           

                        </li>
                        
                    </ul>
                </div>
            </div>
        </div>
    </div>

    <%--prajyot add this for grids table--%>

<div class="container4">

  <div class="rowDivAttendance">
    <div class="cellDivManage1">Request Date</div>
    <div class="cellDivManage1"> Action</div>
   
  </div>

    <div class="rowDiv">
   <div class="cellDivManage">'01/09/2017</div>
    <div class="cellDivManage">View</div>
   
  </div>
        <div class="rowDiv">
   <div class="cellDivManage">'01/09/2017</div>
    <div class="cellDivManage">View</div>
   
  </div>
       <div class="rowDiv">
   <div class="cellDivManage">'01/09/2017</div>
    <div class="cellDivManage">View</div>
   
  </div>
        <div class="rowDiv">
   <div class="cellDivManage">'01/09/2017</div>
    <div class="cellDivManage">View</div>
   
  </div>
           <div class="rowDiv">
   <div class="cellDivManage">'01/09/2017</div>
    <div class="cellDivManage">View</div>
   
  </div>
             <div class="rowDiv">
   <div class="cellDivManage">'01/09/2017</div>
    <div class="cellDivManage">View</div>
   
  </div>
          
</div>
  <%--   <div class="Savebtndiv">
                                <asp:LinkButton ID="btnSave" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClick="btnSave_Click">Submit</asp:LinkButton>
                                 <asp:LinkButton ID="LinkButton1" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="btnSave_Click">Back</asp:LinkButton>
                            </div>--%>
   
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



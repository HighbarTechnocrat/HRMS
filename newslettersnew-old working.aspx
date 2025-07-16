<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="newslettersnew.aspx.cs" Inherits="newsletters" %>--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="newsletters.aspx.cs" Inherits="newsletters" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css"
        type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css"
        type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css"
        media="all" />
    <link href="<%=ReturnUrl("css") %>calendar/calendar.css" rel="stylesheet" type="text/css" />
    <link href="sampleform_css.css" rel="stylesheet" type="text/css" />
    <style>
        .myaccountpagesheading
        {
            text-align: center !important;
            text-transform: uppercase !important;
        }
        
        /*Jayesh_Prajyot Added below  background color code 13sep2017 */
        .aspNetDisabled
        {
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
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js"
        type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js"
        type="text/javascript"></script>
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
                        <div class="myaccountheading">
                            My Account</div>
                        <div class="myaccountlist">
                            <ul>
                                <li class="listselected"><a href="<%=ReturnUrl("sitepathmain") %>procs/SampleForm"
                                    title="Edit Profile"><b>Edit Profile</b></a></li>
                                <li><a href="<%=ReturnUrl("sitepathmain") %>procs/wishlist" title="Favorites">Favorites</a></li>
                                <asp:Panel ID="Panel1" runat="server" Visible="false">
                                    <li><a href="<%=ReturnUrl("sitepathmain") %>procs/preference" title="Preference">
                                        Preference</a></li>
                                    <li id="lihistory" runat="server"><a href="<%=ReturnUrl("sitepathmain") %>procs/subscriptionhistory"
                                        title="Subscription History">Subscription History</a></li>
                                    <li><a href="<%=ReturnUrl("sitepathmain") %>procs/pthistory" title=" Reward Points">
                                        Reward Points</a></li>
                                    <li><a href="<%=ReturnUrl("sitepathmain") %>recommend" title="Recommendation">Recommendation</a></li>
                                </asp:Panel>
                                <li>
                                    <asp:LinkButton ID="btnSingOut" runat="server" ToolTip="Logout" Text="Logout" OnClick="btnSingOut_Click"> </asp:LinkButton>
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
                                    <td class="formtitle">
                                    </td>
                                    <td class="forminput">
                                        <br>
                                        <font>(Maximum 20 characters)</font>
                                        <div class="formerror" id="diverror" runat="server" visible="false">
                                            <asp:Label ID="lblfname" runat="server" Text=""></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formtitle">
                                        <font>*</font><span>Last Name:</span>
                                    </td>
                                    <td class="forminput">
                                        <br>
                                        <font>(Maximum 20 characters)</font>
                                        <div class="formerror" id="diverror1" runat="server" visible="false">
                                            <asp:Label ID="lbllame" runat="server" Text=""></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formtitle">
                                        <span>Address:</span>
                                    </td>
                                    <td class="forminput">
                                        <br>
                                        <font>(Maximum 100 characters)</font>
                                        <div class="formerror" id="diverror2" runat="server" visible="false">
                                            <asp:Label ID="lbladdress" runat="server" Text="Please enter Address"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formtitle">
                                        <span>Country:</span>
                                    </td>
                                    <td class="forminput">
                                        <div class="formerror" id="diverror3" runat="server" visible="false">
                                            <asp:Label ID="lblcountry" runat="server" Text=""></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formtitle">
                                        <span>State:</span>
                                    </td>
                                    <td class="forminput">
                                        <div class="formerror" id="diverror4" runat="server" visible="false">
                                            <asp:Label ID="lblstate" runat="server" Text=""></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formtitle">
                                        <span>City:</span>
                                    </td>
                                    <td class="forminput">
                                        <div class="formerror" id="diverror5" runat="server" visible="false">
                                        </div>
                                    </td>
                                </tr>
                                <asp:Panel runat="server" ID="pnlothercity" Visible="false">
                                    <tr>
                                        <td class="formtitle">
                                        </td>
                                        <td class="forminput">
                                            <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtothercity"
                                                WatermarkText="Add Other City" />
                                            <asp:TextBox ID="txtothercity" onblur="showtext(this)" Visible="false" Height="20"
                                                Width="256px" EnableTheming="True" ForeColor="#8B8B8B" runat="server" CssClass="medium"
                                                onfocus="cleartext(this);"> </asp:TextBox>
                                        </td>
                                        <td class="formerror">
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <tr id="trpincode" runat="server" visible="false">
                                    <td class="formtitle">
                                        <font>*</font><span>Pin code:</span>
                                    </td>
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
                                    <td class="formtitle">
                                        <span>Mobile No:</span>
                                    </td>
                                    <td class="forminput">
                                        <asp:TextBox ID="txtmobile1" runat="server" Text="+91" class="countrycode" ReadOnly="true"
                                            Visible="false" ValidationGroup="validate"></asp:TextBox>
                                        <br>
                                        <font>(Maximum 16 digits)</font>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server"
                                            ErrorMessage="Please enter valid Mobile No" ValidationExpression="^(\(?\+?[0-9]*\)?)?[0-9_\- \(\)]*$"
                                            CssClass="error_field" ControlToValidate="txtmobile" Display="Dynamic" ValidationGroup="validate"></asp:RegularExpressionValidator><br />
                                        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtmobile" ID="RegularExpressionValidator19"
                                            ValidationExpression="^[\s\S]{10,16}$" runat="server" CssClass="error_field"
                                            ErrorMessage="Minimum 10 and Maximum 16 characters allowed." ValidationGroup="validate"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtmobile"
                                            ErrorMessage="Mobile No. is mandatory" ToolTip="Mobile No. is mandatory" ValidationGroup="validate"
                                            SetFocusOnError="true" CssClass="error_field" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <div class="formerror" id="diverror6" runat="server" visible="false">
                                            <asp:Label ID="lblmob" runat="server"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formtitle">
                                        <span>Profile Photo:</span>
                                    </td>
                                    <td class="forminput">
                                        <asp:Label ID="lblstatus" runat="server" ForeColor="Green" Text=""></asp:Label>
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formtitle">
                                        <span>Cover Photo:</span>
                                    </td>
                                    <td class="forminput">
                                        <asp:Label ID="lblstatus2" runat="server" ForeColor="Green" Text=""></asp:Label>
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr id="trtel" runat="server" visible="false">
                                    <td class="formtitle">
                                        <span>Telphone No:</span>
                                    </td>
                                    <td class="forminput">
                                        <asp:TextBox ID="txtphone1" MaxLength="10" Text="+91" ReadOnly="true" runat="server"
                                            CssClass="countrycode"> </asp:TextBox>
                                        <ew:NumericBox ID="txtphone2" MaxLength="5" runat="server" CssClass="citycode"> </ew:NumericBox>
                                    </td>
                                    <td class="formerror">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td colspan="2">
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Newsletter"></asp:Label><br />
                        <%-- JAYESH_PRAJYOT ADDED BELOW CODE FOR REQUEST  FORM 18sep2017--%>
                </div>
            </div>
        </div>
        
       <%-- <div class="projectlistddl">
            <asp:DropDownList ID="projectddl" runat="server" CssClass="msgselect1" TabIndex="1">
                 <asp:ListItem Text="Transportation"></asp:ListItem>
                <asp:ListItem Text="Email-ID"></asp:ListItem>
                <asp:ListItem Text="Department"></asp:ListItem>
                <asp:ListItem Text="Designation"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="project-text-box">   <asp:TextBox ID="txtbox1" runat="server" placeholder="Type here to search record..."></asp:TextBox></div>
        <div class="projectbtn">
            <span class="projectsearch">
                <asp:LinkButton ID="projectsearchbtn" OnClick="lnkstarcontact_Click" ToolTip="Search Contact"
                    TabIndex="3" ValidationGroup="valgrp"><i class="fa fa-search" ></i></asp:LinkButton></span>
            <span class="directoryreset">
                <asp:LinkButton ID="projectresetbtn" OnClick="lnkreset_Click" ToolTip="Reset Search"
                    TabIndex="4"><i class="fa fa-undo" aria-hidden="true"></i>
                </asp:LinkButton></span>--%>


        </div>
        <!--<div class="financialresults">
            <ul>
                <li><a href="#" class="financialresultsheadertitle"> Year </a><a href="#" class="financialresultsheaderauthor">Date</a><a href="#" class="financialresultsheaderyear">Title </a><%--<i class="fa fa-plus" aria-hidden="true">
                </i></li>--%>
                <li><a href="#" class="financialresultstitle">2017 </a><a href="#" class="financialresultsauthornew">August </a><a href="files\Newsletter\HCC News August 2017- Kishanganga HEP.pdf" class="financialresultsyear"><img src="http://localhost/hrmsadmin/images/bigproduct/metrorail_20171012_114957.jpg" title="Kishanganga Hydro Power Project" width="100" height="62" /></a><%--<i class="fa fa-plus" aria-hidden="true"></i></li>--%>
                    <li><a href="#" class="financialresultstitlenew">2017 </a><a href="#" class="financialresultsauthornew2">May</a><a href="files\Newsletter\HCC News May 2017 - Harvesting Hydro Power.pdf" class="financialresultsyearnew"><img src="http://localhost/hrmsadmin/images/bigproduct/vaitarana_20171012_114846.jpg" title="Harvesting Hydro Power" width="100" height="62" /></a><%--<i class="fa fa-plus" aria-hidden="true"></i></li>--%>
               <li><a href="#" class="financialresultstitlenew23">2017 </a><a href="#" class="financialresultsauthornew23">February</a><a href="files\Newsletter\HCC News February 2017 -  Connecting the dots in Bridge Construction.pdf" class="financialresultsyearnew23"><img src="http://localhost/hrmsadmin/images/bigproduct/vaitarana_20171012_114846.jpg" title="Harvesting Hydro Power" width="100" height="62"></a><%--<i class="fa fa-plus" aria-hidden="true"></i></li>--%>
               
               
            </ul>
        </div>--->
		<div class="rownew">
 <div class="columnnew">
    <span>Year</span>
    
  </div>
  <div class="columnnew" >
    <span>Date</span>
    
  </div>
<div class="columnnew">
    <span>Title</span>
    
  </div>
</div>
		<!--<div class="rownew2">
 <div class="columnnew2">
    <span>2017</span>
    
  </div>
  <div class="columnnew2">
   <span>August </span>
    
  </div>
<div class="columnnew2">
	 <a href="files\Newsletter\HCC News August 2017- Kishanganga HEP.pdf"><img src="http://localhost/hrms/images/Newsletter/kishanganganew.png" title="Kishanganga Hydro Power Project" width="75" height="90" /></a>
    
  </div>
</div>
		<div class="rownew2">
 <div class="columnnew2">
    <span>2017</span>
    
  </div>
  <div class="columnnew2">
   <span>May</span>
    
  </div>
<div class="columnnew2">
	 <a href="files\Newsletter\HCC News May 2017 - Harvesting Hydro Power.pdf"><img src="http://localhost/hrms/images/Newsletter/newHarvesting Hydro Power.png" title="Harvesting Hydro Power" width="75" height="90" /></a>
    
  </div>
</div>
		<div class="rownew2">
 <div class="columnnew2">
    <span>2017</span>
    
  </div>
  <div class="columnnew2">
   <span>February</span>
    
  </div>
<div class="columnnew2">
	 <a href="files\Newsletter\HCC News February 2017 -  Connecting the dots in Bridge Construction.pdf"><img src="http://localhost/hrms/images/Newsletter/dotbridgeconstruction.png" title="Connecting the dots in Bridge Construction" width="75" height="90" /></a>
    
  </div>
</div>-->

<div class="rownew2">
 <div class="columnnew2">
    <span>2017</span>
  </div>
  <div class="columnnew2">
   <span>November</span>
  </div>
<div class="columnnew2">
	<a href="files\Newsletter\HCC News November 2017.pdf"><img src="http://localhost/hrms/images/Newsletter/HCC News November 2017.jpg" title="Concreting Surge Shaft using Slipform" width="75" height="90" /></a>
  </div>
</div>


<div class="rownew2">
 <div class="columnnew2">
    <span>2017</span>
  </div>
  <div class="columnnew2">
   <span>August</span>
  </div>
<div class="columnnew2">
	<a href="files\Newsletter\HCC News August 2017.pdf"><img src="http://localhost/hrms/images/Newsletter/HCC News August 2017.jpg" title="Kishanganga Hydro Power Project" width="75" height="90" /></a>
  </div>
</div>


<div class="rownew2">
 <div class="columnnew2">
    <span>2017</span>
  </div>
  <div class="columnnew2">
   <span>May</span>
  </div>
<div class="columnnew2">
	<a href="files\Newsletter\HCC News May 2017.pdf"><img src="http://localhost/hrms/images/Newsletter/HCC News May 2017.jpg" title="Harvesting Hydro Power" width="75" height="90" /></a>
  </div>
</div>


<div class="rownew2">
 <div class="columnnew2">
    <span>2017</span>
  </div>
  <div class="columnnew2">
   <span>February</span>
  </div>
<div class="columnnew2">
	<a href="files\Newsletter\HCC News February 2017.pdf"><img src="http://localhost/hrms/images/Newsletter/HCC News February 2017.jpg" title="Connecting the dots in Bridge Construction" width="75" height="90" /></a>
  </div>
</div>



<!-- Add New article code above -->

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


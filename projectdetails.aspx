
<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="projectdetails.aspx.cs" Inherits="projectdetails" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link href="<%=ReturnUrl("css") %>calendar/calendar.css" rel="stylesheet" type="text/css" />
     <link href="sampleform_css.css" rel="stylesheet" type="text/css" />
     <script>
         $(document).ready(function () {
             $('.customer-logos').slick({
                 slidesToShow: 4,
                 slidesToScroll: 1,
                 autoplay: true,
                 autoplaySpeed: 1000,
                 arrows: false,
                 dots: false,
                 pauseOnHover: false,
                 responsive: [{
                     breakpoint: 768,
                     settings: {
                         slidesToShow: 3
                     }
                 }, {
                     breakpoint: 520,
                     settings: {
                         slidesToShow: 2
                     }
                 }]
             });
         });
    </script>
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
                extraParams: {d:deprt},
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
                        <asp:Label ID="lblheading" runat="server" Text="Kishanganga Hydro Power"></asp:Label><br/>
 <div class="projectdetailas">
<%--<span class="hydropower">Kishanganga Hydro Power</span>--%>
<p class="hydropara">The 350 mw  KishanGanga Hydro Power Project is located on the river kishanGanga the tributary river of jhelum in Bandipore district of of Jammu & Kasmir. This was the first mega hydro power project awarded bt NHPC to HCC on Engineering, Procurement and Construction(EPC) basis.</p>
<%--<div class="imgpro">
<p>Project Manager</p>
<img class="profiledetail" src="http://localhost/hrms/images/profile/image.jpg" alt="" title="">
<span class="proname">Arackal Benny</span>

</div>--%>
<div class="imgpro1">
<p>Project Manager</p>
<img class="profiledetail1" src="http://localhost/hrms/images/profile/image.jpg" alt="" title="">
<span class="proname1">Arackal Benny</span>

</div>
<p class="procli">Client: <span class="detailscli">NHPC Ltd.</span></p>
<p class="proclivalu">Value: <span class="detailsval">Rs 2726.19 crore</span></p>
<div class="promethod">
<span class="scopedetl">Scope of Work:</span><ul><li class="designpro">Design, engineering, procurement and construction of 35m high concrete face rock fill dam (CFRD), 560 m long diversion tunnel, 23.65 km long head race tunnel (HRT) cnotructed using teo methologies (8.9 km tunnel constructed by drill and blast method and 14.75 km costructed using tunnel boring machine), 862 m long tail race tunnel (TRT), 44 m long open channel, 100.7 m deep and 18.75 m diameter surge shaftg, pressure  shaft, three penstocks, power house and surface switchyard.</li>

</ul>
</div><div class="Achievements">
<span class="majordetail">Major Achievements :</span> <ul><li class="create">Hcc team created a world record of the first successful TBM operations in the Himalayan region and accomplished a national record by achieving 'highest tunnel progress in a month' of 816 meter in November 2012.</li>
<li class="several ">The team successfully overcame several geological , Logistics and engineering Challenges</span></li>

</div>

<%--<div class="daitialphotogallry">

<div class="prodetailglry">
<img class="datialgallery1" src="http://localhost/hrms/images/profile/images2.jpg" alt="" title="">
<p>Dam at kishnganga HEP</p>
</div>
<div class="prodetailglry">
<img class="datialgallery2" src="http://localhost/hrms/images/profile/worli_sea_link.jpg" alt="" title="">
<p>World Rrecord of first successful TBM </p>
</div>
<div class="prodetailglry">
<img class="datialgallery3" src="http://localhost/hrms/images/profile/worli_sea_link.jpg" alt="" title="">
<p>Dam at kishnganga</p>
</div>
</div>
--%>
<%--<div class="customer-logos">
  <div class="slide"><img src="http://localhost/hrms/images/profile/images2.jpg"></div>
  <div class="slide"><img src="http://localhost/hrms/images/profile/worli_sea_link.jpg"></div>
  <div class="slide"><img src="http://localhost/hrms/images/profile/worli_sea_link.jpg"></div>
 <div class="slide"><img src="http://localhost/hrms/images/profile/worli_sea_link.jpg"></div>
  <div class="slide"><img src="http://localhost/hrms/images/profile/images2.jpg"></div>
  <div class="slide"><img src="http://localhost/hrms/images/profile/worli_sea_link.jpg"></div>
  <div class="slide"><img src="http://localhost/hrms/images/profile/images2.jpg"></div>
  <div class="slide"><img src="http://localhost/hrms/images/profile/images2.jpg"></div>
</div>--%>
                <span class="photogallery">Progress Photograph</span>
              <div class="prodetaols1">
    

                  <a href="http://localhost/hrmsadmin/images/bigproduct/DSC09399_20171014_101852.jpg">
         <img src="http://localhost/hrmsadmin/images/450x300/DSC09399_20171014_101852.jpg"> </span></a>

                     <a href="http://localhost/hrmsadmin/images/bigproduct/chamera_20171016_101653.jpg">
         <img src="http://localhost/hrmsadmin/images/450x300/chamera_20171016_101653.jpg"> </span></a>

                     <a href="http://localhost/hrmsadmin/images/bigproduct/IMG_5709_20171014_102428.JPG">
         <img src="http://localhost/hrmsadmin/images/450x300/IMG_5709_20171014_102428.JPG"> </span></a>

                    <%-- <a href="http://localhost/hrmsadmin/images/bigproduct/2008718175158648_20171016_101828.JPG">
         <img src="http://localhost/hrmsadmin/images/450x300/2008718175158648_20171016_101828.JPG"> </span></a>--%>
                        
                  </div>

				
<div class="prostatus">
<span class="photogallery">Project Status :<span>
<ul>
<li>Tunnelling work of 14.6 km with TBM is complete & 8.09 km out of 8.6 km of HRT lining is complete.</li>
<li>Cut of wall construction is also complete.</li>
<li>Spillway construction , CFRD embankment works and power house concreting work is well progressing.</li>
<li>Water impounding activity is also taken up.</li>
</ul>
</div> 

           
            </div>
        </div>
    </div>
            
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


<%@ Page Title="" Language="C#" MasterPageFile="~/Recruitments.master" AutoEventWireup="true"
     CodeFile="RecruitmentsPositions.aspx.cs" Inherits="RecruitmentsPositions" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/manageleave_css.css" type="text/css" media="all" />        
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Recruitment_details_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Recruitment_css.css" type="text/css" media="all" /> 

<%--      
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css" />
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>--%>

    <%-- <link href="<%=ReturnUrl("css") %>calendar/calendar.css" rel="stylesheet" type="text/css" />
    <link href="sampleform_css.css" rel="stylesheet" type="text/css" />--%>
    <%--<ajaxToolkit:ToolkitScriptManager ID="srcp" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
            background: #dae1ed;
        }

         .Dropdown {
            border-bottom: 2px solid #cccccc;
            /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
            background: url('../images/arrowdown.png') no-repeat right center;
            
            cursor: default;
        }
         .trbodycalss
         {
            color: black;
            font-size: 13px;
            /* font-style: oblique; */
            font-family: initial;
         }
         .viewresume
         {
             color: black;
            font-size: 13px;
            /* font-style: oblique; */
            font-family: initial;
         }
         .trheadclass
         {
             color: black;
            font-size: 14px;
         }
 
            table, th, td 
            {
              border: 1px solid black;
              border-collapse: collapse;
            }
            th, td 
            {
              padding: 5px;
              text-align: left;
            }
            table#t01 tr:nth-child(even) {
              background-color: #eee;
            }
            table#t01 tr:nth-child(odd) 
            {
                 background-color: #fff;
            }
            table#t01 th 
            {
              background-color: black;
              color: white;
            }
            .linkcolor
            {
                color: white !important;
            }
            .linkcolor_openfullfill
            {
                color: yellow !important;
            }
            .downloadlink
            {
                color: black;
                font-size: 13px;
                /* font-style: oblique; */
                font-family: initial;
            }
            .lblPostionAssigntto
            {
                margin: -42px 13px 0 0 !important;
                direction: rtl;
                height: 0px !important;
                color: yellow !important;
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
                        <asp:Label ID="lblheading" runat="server" Text="Position Details"></asp:Label>
                    </span>
                </div>

                <span>
                    <a href="Recruitment_index.aspx" class="aaaa">Recruitment Index</a>
                </span>
                 <div class="wrapper" style="width:1360px">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                          <ContentTemplate>
                             <asp:Repeater ID="rptpositions" runat="server" Visible="true" OnItemDataBound="rptpositions_ItemCreated">
                                 <ItemTemplate>
                                    <asp:UpdatePanel ID="updreview" runat="server" UpdateMode="Always">
                                        <ContentTemplate>
                                                      <div class="half">
                                                                <div class="tab blue">
                                                                     
                                                                    <input id='<%# DataBinder.Eval(Container, "DataItem.pos_id") %>' type="radio"  style="display:none" name="tabs2"/>
                                                                    <label  for='<%# DataBinder.Eval(Container, "DataItem.pos_id") %>'>                                                                        
                                                                        <a href='<%#getFileUrl(Eval("pos_id"),Eval("Recruiter_code")) %>' title="Attached Resume" class="linkcolor"> Position Name :-  <%# DataBinder.Eval(Container, "DataItem.position_tile") %></a>
                                                                       <div class="lblPostionAssigntto">
                                                                        <asp:Label ID="lblPositionAssignto" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Recruiter_Name") %>'></asp:Label>                                                                           
                                                                        </div>
                                                                         <asp:Label ID="lblposid" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.pos_id")%>'></asp:Label>
                                                                            <br />
                                                                            <a href="#" class="linkcolor">Position Description :-  <%# DataBinder.Eval(Container, "DataItem.position_desp") %> </a> 
                                                                            <br />   
                                                                            <a href="#" class="linkcolor"> No. of Position : -   <%# DataBinder.Eval(Container, "DataItem.nopositions") %> </a>
                                                                             &nbsp;&nbsp;&nbsp; <a href="#" class="linkcolor_openfullfill"> Open : -   <%# DataBinder.Eval(Container, "DataItem.opena") %> </a>
                                                                            &nbsp;&nbsp;&nbsp; <a href="#" class="linkcolor"> Fullfill : -   <%# DataBinder.Eval(Container, "DataItem.fullfill") %> </a>
                                                                            <br />
                                                                           <%-- No.of Attched Resume : -   <%# DataBinder.Eval(Container, "DataItem.resumeAttach") %> --%>
                                                                           <%--<a href='<%#getAttcehedUrl(Eval("pos_id")) %>' title="View attached Resume" class="linkcolor"> Attched Resume : -   <%# DataBinder.Eval(Container, "DataItem.resumeAttach") %></a>--%>
                                                                            <a title="View attached Resume" class="linkcolor"> Attched Resume : -   <%# DataBinder.Eval(Container, "DataItem.resumeAttach") %></a>
                                                                    </label>
                                                                    <div class="tab-content">                            
                                                                        <div style="width:100%">
                                                                        <asp:Repeater ID="rptdtppositions" runat="server"> 
                                                                             <HeaderTemplate>
                                                                                 <asp:Table ID="tbldptlocations" runat="server" Width="100%"  class="table">
                                                                                <asp:TableHeaderRow CssClass="trheadclass">
                                                                                    <asp:TableHeaderCell  Width="6%" HorizontalAlign="Left">Sr.No</asp:TableHeaderCell>
                                                                                    <asp:TableHeaderCell  Width="21%" HorizontalAlign="Left">Candidate Name</asp:TableHeaderCell>
                                                                                    <asp:TableHeaderCell  Width="22%" HorizontalAlign="Left">Qualification</asp:TableHeaderCell>
                                                                                    <asp:TableHeaderCell  Width="10%" HorizontalAlign="Left">Year of Experience</asp:TableHeaderCell>
                                                                                    <asp:TableHeaderCell  Width="15%" HorizontalAlign="Left">Institute</asp:TableHeaderCell>
                                                                                    <asp:TableHeaderCell  Width="9%" HorizontalAlign="Left">Status</asp:TableHeaderCell>
                                                                                    <asp:TableHeaderCell  Width="9%" HorizontalAlign="Left">Resume</asp:TableHeaderCell>
                                                                                    <asp:TableHeaderCell  Width="8%" HorizontalAlign="Left">View</asp:TableHeaderCell>
                                                                                </asp:TableHeaderRow>
                                                                                    </asp:Table>
                                                                             </HeaderTemplate>
                                                                            <ItemTemplate>

                                                                                <asp:Table ID="tblrows" runat="server" Width="100%">
                                                                                        <asp:TableRow  CssClass="trbodycalss">
                                                                                            <asp:TableCell Width="6%" HorizontalAlign="Left"><%# DataBinder.Eval(Container, "DataItem.srno")%></asp:TableCell>
                                                                                            <asp:TableCell Width="21%" HorizontalAlign="Left"><%# DataBinder.Eval(Container, "DataItem.cand_name")%></asp:TableCell>
                                                                                            <asp:TableCell Width="22%" HorizontalAlign="Left"><%# DataBinder.Eval(Container, "DataItem.Qualification")%></asp:TableCell>
                                                                                            <asp:TableCell Width="10%" HorizontalAlign="Left"><%# DataBinder.Eval(Container, "DataItem.cand_exp")%></asp:TableCell>
                                                                                            <asp:TableCell Width="15%" HorizontalAlign="Left"><%# DataBinder.Eval(Container, "DataItem.qualification_institute")%></asp:TableCell>
                                                                                            <asp:TableCell Width="9%" HorizontalAlign="Left"><%# DataBinder.Eval(Container, "DataItem.status")%></asp:TableCell>
                                                                                            <asp:TableCell Width="9%" HorizontalAlign="Left">                
                                                                                                    <asp:LinkButton ID="lbldownloadResume"  runat="server" OnClick="lbldownloadResume_Click" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ResumeFile")%>'><span class="downloadlink">Download</span></asp:LinkButton>
                                                                                            </asp:TableCell>
                                                                                            <asp:TableCell Width="8%" HorizontalAlign="Left">
                                                                                                <a href='<%#getAttcehedUrl(Eval("interview_id"),Eval("cand_attach_position"),Eval("attach_pos_no"))%>' title="View attached Resume" class="downloadlink">View</a>
                                                                                            </asp:TableCell>
                                                                                        </asp:TableRow>
                                                                                   </asp:Table>

                                                                                <%--<asp:Table ID="tblrows" runat="server" Width="100%">
                                                                                <asp:TableRow  CssClass="trbodycalss">
                                                                                    <asp:TableCell Width="6%" HorizontalAlign="Left"><%# DataBinder.Eval(Container, "DataItem.pos_srno") %></asp:TableCell>
                                                                                    <asp:TableCell Width="25%" HorizontalAlign="Left"><%# DataBinder.Eval(Container, "DataItem.dept_name") %></asp:TableCell>
                                                                                    <asp:TableCell Width="30%" HorizontalAlign="Left"><%# DataBinder.Eval(Container, "DataItem.loc_name") %></asp:TableCell>
                                                                                    <asp:TableCell Width="15%" HorizontalAlign="Left"><%# DataBinder.Eval(Container, "DataItem.fromdate") %></asp:TableCell>
                                                                                    <asp:TableCell Width="15%" HorizontalAlign="Left"><%# DataBinder.Eval(Container, "DataItem.todate") %></asp:TableCell>
                                                                                    <asp:TableCell Width="9%" HorizontalAlign="Left"><%# DataBinder.Eval(Container, "DataItem.status") %></asp:TableCell>
                                                                                </asp:TableRow>
                                                                                    </asp:Table>--%>

                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                        </div>                                                                                                                           
                                                                    </div>
                                                                </div>
                                                      </div>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ItemTemplate>
                            </asp:Repeater>
                         </ContentTemplate>
                        </asp:UpdatePanel>
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

                        <li>
                            <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                        </li>

                    </ul>
                </div>
                <asp:HiddenField ID="hdnRemid" runat="server" />
                <asp:HiddenField ID="hdnClaimsID" runat="server" />
               <asp:HiddenField ID="hdnIsPostionCreator" runat="server" />
                <asp:HiddenField ID="hdnempcode" runat="server" />
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


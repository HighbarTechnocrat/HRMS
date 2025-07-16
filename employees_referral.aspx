<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="employees_referral.aspx.cs" Inherits="employees_referral" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/fuel_RemRequest_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
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
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
    
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
                        <asp:Label ID="lblheading" runat="server" Text="Employees Referral "></asp:Label>
                    </span>
                </div>

                <span>
                 <%--   <a href="default.aspx" class="aaaa">Reimbursement Index</a>--%>
                </span>


                <div class="edit-contact">
                    <div class="editprofile" style="text-align: center; border: none;" id="divmsg" runat="server" visible="false">
                        
                    </div>
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" ></asp:LinkButton>
                        </div>
                    </div>


                    <ul id="editform" runat="server">
                        
                         <li class="fuel_inboxEmpCode">
                           <asp:Label runat="server" ID="Label1" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                        </li>
                        <li class="fuel_claimed">
                            <br />
                            <asp:TextBox AutoComplete="off" ID="TextBox1" runat="server" MaxLength="100" Visible="false"></asp:TextBox>
                        </li>

                     
                        <li class="fuel_inboxEmpCode">
                             <span>Name:</span> <span style="color:red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_Candidate_Name" runat="server" MaxLength="100"></asp:TextBox>
                        </li>
                        <li class="fuel_InboxEmpName">
                            <asp:TextBox AutoComplete="off" ID="txtEmpName" runat="server" Visible="false"></asp:TextBox>
                        </li>

                        <li class="fuel_date">
                           <span>Gender: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtGender" runat="server" CssClass="Dropdown" ></asp:TextBox>
                            <asp:Panel ID="Panel5" Style="display: none;" runat="server" CssClass="taskparentclasskkk">                              
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstGender" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="lstGender_SelectedIndexChanged">
                                                <asp:ListItem Selected="False" Text="Male">Male</asp:ListItem>
                                                <asp:ListItem Selected="False" Text="Female">Female</asp:ListItem>
                                        </asp:ListBox>
                                    </ContentTemplate>
                                    <Triggers>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </asp:Panel>
                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender4" PopupControlID="Panel5" TargetControlID="txtGender"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>
                        </li>
                        <li class="fuel_claimed">
                           <span>Birth Date: </span>  <span style="color:red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_BirthDate" runat="server" AutoPostBack="true" OnTextChanged="Txt_BirthDate_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2"  TargetControlID="Txt_BirthDate"
                             Format="dd/MM/yyyy" runat="server">
                            </ajaxToolkit:CalendarExtender>

                            <asp:TextBox AutoComplete="off" ID="txt_age" runat="server" MaxLength="5" CssClass="aspNetDisabled" Visible="false"> </asp:TextBox>
                        </li>

                        <li class="fuel_detail">                            
                             <span>Email-Id: </span> <span style="color:red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_email" runat="server" MaxLength="100"></asp:TextBox>
                        </li>
                        <li class="fuel_detail">
                            <span>Mobile No.: </span> <span style="color:red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_mobile" runat="server" MaxLength="100"></asp:TextBox>
                        </li>
                        <li class="fuel_detail">
                               <span>Experience (Years)  (eg 04.06): </span><span style="color:red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_Experience" runat="server" MaxLength="5"></asp:TextBox>
                        </li>
                        <li class="fuel_detail">
                           <span>Current CTC (Rs.in Lac)(eg 09.60) </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_curntCTC" runat="server" MaxLength="10"></asp:TextBox>     
                        </li>
                        <li class="fuel_detail">                            
                           <span>Address: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_Address" TextMode="MultiLine" runat="server" MaxLength="300" Height="100px"></asp:TextBox>
                        </li>
                        <li class="fuel_detail">
                        </li>
                   
                         
                          
                        <li class="fuel_upload">                            
                            <span>Upload Resume </span> <span style="color:red">*</span><br />
                            <asp:FileUpload ID="uploadfile" runat="server" AllowMultiple="true" accept=".pdf,.doc,.docx" />
                            <asp:LinkButton ID="lnkuplodedfile" OnClick="lnkuplodedfile_Click" runat="server" Visible="false"></asp:LinkButton>
                        </li>
                        <li class="fuel_upload">

                        </li>
                        
                    </ul>


                </div>
            </div>
        </div>
    </div>
    <div class="fuel_Savebtndiv">
        <asp:LinkButton ID="fuel_btnSave" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve"  OnClick="fuel_btnSave_Click" OnClientClick="return SaveMultiClick();">Submit</asp:LinkButton>       
        <asp:LinkButton ID="fuel_btnBack" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="default.aspx" >Back</asp:LinkButton>
        <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="False"> </asp:TextBox>
    </div>
       

    <asp:HiddenField ID="hflEmpDesignation" runat="server" />
    <asp:HiddenField ID="hflEmpDepartment" runat="server" />
    <asp:HiddenField ID="hflEmailAddress" runat="server" />
    <asp:HiddenField ID="hflGrade" runat="server" />
    <asp:HiddenField ID="hflapprcode" runat="server" />
    <asp:HiddenField ID="hdnDesk" runat="server" />
    <asp:HiddenField ID="hdnDestnation" runat="server" />
    
    <asp:HiddenField ID="hdnClaimsID" runat="server" />
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
    <asp:HiddenField ID="hdnApprId" runat="server" />
    <asp:HiddenField ID="hdnApprEmailaddress" runat="server" />
    <asp:HiddenField ID="hdnEligible" runat="server" />
    <asp:HiddenField ID="hdnTrdays" runat="server" />
     <asp:HiddenField ID="hdnTravelDtlsId" runat="server" />
     <asp:HiddenField ID="hdnAccId" runat="server" />
     <asp:HiddenField ID="hdnLocalId" runat="server" />
      <asp:HiddenField ID="hdnTravelstatus" runat="server" />
    <asp:HiddenField ID="hdnLeavestatusValue" runat="server" />
    <asp:HiddenField ID="hdnLeavestatusId" runat="server" />
    <asp:HiddenField ID="hdnIsApprover" runat="server" />
    <asp:HiddenField ID="hdnFuelReimbursementType" runat="server" />

    <asp:HiddenField ID="hdnApprovalCOS_Code" runat="server" />
    <asp:HiddenField ID="hdnApprovalCOS_ID" runat="server" />
    <asp:HiddenField ID="hdnApprovalCOS_mail" runat="server" />
    <asp:HiddenField ID="hdnApprovalCOS_Name" runat="server" />
     
    <asp:HiddenField ID="hdnMobRemStatusM" runat="server" />
    <asp:HiddenField ID="hdnMobRemStatus_dtls" runat="server" /> 
    <asp:HiddenField ID="hdnClaimDate" runat="server" /> 
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="hdnfrmdate_emial" runat="server" />
    <asp:HiddenField ID="hdntodate_emial" runat="server" />
    <asp:HiddenField ID="hdnremid" runat="server" />

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

       
        function SaveMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=fuel_btnSave.ClientID%>');

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

        function onCalendarShown() {

            var cal = $find("calendar1");
            //Setting the default mode to month
            cal._switchMode("months", true);

            //Iterate every month Item and attach click event to it
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }

        function onCalendarHidden() {
            var cal = $find("calendar1");
            //Iterate every month Item and remove click event from it
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }

        }
        function call(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("calendar1");
                    cal._visibleDate = target.date;
                    cal.set_selectedDate(target.date);
                    cal._switchMonth(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged();
                    break;
            }
        }


        var d = new Date();
        var monthArray = new Array();
        monthArray[0] = "January";
        monthArray[1] = "February";
        monthArray[2] = "March";
        monthArray[3] = "April";
        monthArray[4] = "May";
        monthArray[5] = "June";
        monthArray[6] = "July";
        monthArray[7] = "August";
        monthArray[8] = "September";
        monthArray[9] = "October";
        monthArray[10] = "November";
        monthArray[11] = "December";
        for (m = 0; m <= 11; m++) {
            var optn = document.createElement("OPTION");
            optn.text = monthArray[m];
            // server side month start from one
            optn.value = (m + 1);

            // if june selected
            if (m == 5) {
                optn.selected = true;
            }

            document.getElementById('txt_parkwash_month').options.add(optn);
        }

    </script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="AppRej_AttendanceReqst.aspx.cs" Inherits="AppRej_AttendanceReqst" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Attendence_css.css" type="text/css" media="all" />
    <%-- <link href="<%=ReturnUrl("css") %>calendar/calendar.css" rel="stylesheet" type="text/css" />
    <link href="sampleform_css.css" rel="stylesheet" type="text/css" />--%>
    <%--<ajaxToolkit:ToolkitScriptManager ID="srcp" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }
        
        #main-content input[type="checkbox"]
        {
            height: 20px !important;
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
                        <asp:Label ID="lblheading" runat="server" Text="Attendance Regularization Request"></asp:Label>
                    </span>
                </div>

                 <div class="leavegrid">
                    <a href="Leaves.aspx" class="aaab">Leave Menu</a>
                </div>


                <div class="edit-contact">
                    <div class="editprofile" style="text-align: center; border: none;" id="divmsg" runat="server" visible="false">
                        
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
                        <li class="EmpCode">
                            <asp:Label runat="server" ID="lblmessage"   Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                        </li>
                         <li class="EmpName">
                           <%-- <span>Employee Name</span><br />--%>
                            <asp:TextBox ID="TextBox1" runat="server" Visible="false" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="EmpCode">
                            <span>Employee Code</span><br />
                            <asp:TextBox ID="txtEmpCode" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="EmpName">
                            <span>Employee Name</span><br />
                            <asp:TextBox ID="txtEmpName" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="Designation">
                           <span>Designation</span><br />
                            <asp:TextBox ID="txtDesignation" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="Department">
                            <span>Department</span><br />
                            <asp:TextBox ID="txtDepartment" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="Requestdate">
                            <span>Request Date</span><br />
                            <asp:TextBox ID="txtRequest_Date" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="dg_grid">
                            <span style="color:blue">Regularization Approved&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <img src="../images/chk_approved.png" /></span><br />
                            <span style="color:blue">Regularization Reject/Send for Correction&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img src="../images/chk_reject.png" /></span><br /><br />
                            <div class="att_grid">
                                <asp:GridView ID="dgAttendance_AppReject" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" RowStyle-Wrap="False"
                                    DataKeyNames="att_id" OnRowDataBound="dgAttendance_AppReject_RowDataBound" OnRowCreated="dgAttendance_AppReject_RowCreated">
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" VerticalAlign="Top" />
                                    <PagerStyle ForeColor="#000066" HorizontalAlign="left" BackColor="White" />
                                    <RowStyle ForeColor="#000066" VerticalAlign="Top" Height="10px" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />

                                   <Columns>
                            <asp:BoundField HeaderText="Date"
                                DataField="att_date"
                                ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="left"
                                ItemStyle-Width="5%" />

                            <asp:BoundField HeaderText="Type"
                                DataField="att_type"
                                 ItemStyle-HorizontalAlign="Left"
                                 
                                ItemStyle-Width="5%" />

                            <asp:BoundField HeaderText="Time"
                                DataField="att_time"
                                 ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="5%" />

                            <asp:BoundField HeaderText="Status"
                                DataField="att_status"
                                 ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="5%" HtmlEncode="False" />

                              <asp:BoundField HeaderText="Category"
                                    DataField="att_category"
                                     ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="5%" />

                                 <asp:BoundField HeaderText="Reason / Remark"
                                    DataField="att_reason_remark"
                                     ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="17%" ItemStyle-Wrap="true" />
                                 <asp:TemplateField HeaderText="Approver 1"
                                     HeaderStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblApprover1" Text="Approver1" runat="server"></asp:Label>   
                                                    <asp:CheckBox ID="chkApprover1_All"  runat="server" AutoPostBack="true"  OnCheckedChanged="chkApprover1_All_CheckedChanged"/>
                                                </HeaderTemplate>
                                                <ItemTemplate>                                                                                                          
                                                    <asp:CheckBox ID="chkApprover1" runat="server"/>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approver 2" HeaderStyle-HorizontalAlign="Center">
                                                 <HeaderTemplate>                                                       
                                                     <asp:Label ID="lblApprover2" Text="Approver2" runat="server"></asp:Label>
                                                    <asp:CheckBox ID="chkApprover2_All" runat="server"  AutoPostBack="true" OnCheckedChanged="chkApprover2_All_CheckedChanged"/>                                                     
                                                </HeaderTemplate>
                                                <ItemTemplate>                                                                                                         
                                                    <asp:CheckBox ID="chkApprover2" runat="server"/>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approver 3" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>                                                
                                                    <asp:Label ID="lblApprover3" Text="Approver3" runat="server"></asp:Label>        
                                                    <asp:CheckBox ID="chkApprover3_All" runat="server" AutoPostBack="true"  OnCheckedChanged="chkApprover3_All_CheckedChanged"/>                                                    
                                                </HeaderTemplate>
                                                <ItemTemplate>                                                     
                                                    
                                                    <asp:CheckBox ID="chkApprover3" runat="server"/>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approver Remark">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtReason" runat="server" AutoComplete="off" Width="65%" MaxLength="50" Text='<%# Bind("remarks") %>'></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="25%" />
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Reason / Remark" Visible="false">
                                                <ItemTemplate>
                                                        <asp:TextBox ID="txtapproverstatus1" runat="server" Text='<%# Bind("approverFstatus") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Reason / Remark" Visible="false">
                                                <ItemTemplate>
                                                        <asp:TextBox ID="txtapproverstatus2" runat="server" Text='<%# Bind("approverSstatus") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="Reason / Remark" Visible="false">
                                                    <ItemTemplate>
                                                            <asp:TextBox ID="txtapproverstatus3" runat="server" Text='<%# Bind("approverTstatus") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="1%" />
                                    </asp:TemplateField>

                               
                        
                        </Columns>
                                </asp:GridView>

                            </div>
                        </li>

                        <li class="Att_Approver">
                            <span>Approver </span>
                            <br />
                            <asp:ListBox ID="lstApprover" runat="server" ></asp:ListBox>
                        </li>

                        <li class="Att_inter" style="display:none;">
                          <%--  <span>For Information To </span>--%>
                            <br />
                            <asp:ListBox ID="lstIntermediate" runat="server" Visible="false" ></asp:ListBox>
                        </li>

                        <li class="Att_Approver">
                            <span> LC - Late Coming </span><br />
                            <span> EG - Early Going </span><br />
                            <span> OD - On Duty </span><br />
                            <span> FS - Forgot to Swipe </span><br />
                            <span> LV - Leave </span><br />
                        </li>
                    </ul>
                </div>
            </div>

        </div>
    </div>


    <div class="att_buttons">

        <asp:LinkButton ID="att_submit" OnClick="btnSave_Click"  runat ="server" OnClientClick="return SaveMultiClick();">Submit</asp:LinkButton>

        <asp:LinkButton ID="att_back" runat="server" OnClick="att_back_Click">Back</asp:LinkButton>

    </div>





    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />

    <asp:HiddenField ID="hflEmpName" runat="server" />

    <asp:HiddenField ID="hflEmpDesignation" runat="server" />

    <asp:HiddenField ID="hflEmpDepartment" runat="server" />

    <asp:HiddenField ID="hflEmailAddress" runat="server" />

    <asp:HiddenField ID="hflGrade" runat="server" />

    <asp:HiddenField ID="hflapprcode" runat="server" />

     <asp:HiddenField ID="hdnApproverMailid" runat="server" />
        <asp:HiddenField ID="hdnemp_email" runat="server" />
     <asp:HiddenField ID="hdnattid" runat="server" />
        <asp:HiddenField ID="hdnattndt" runat="server" />
        <asp:HiddenField ID="hdnreqid" runat="server" />
        <asp:HiddenField ID="hdnempid" runat="server" />
        <asp:HiddenField ID="hdnreqdt" runat="server" />
        <asp:HiddenField ID="hdnnxtapprovercode" runat="server" />
        <asp:HiddenField ID="hdnnxtapproverName" runat="server" />         
        <asp:HiddenField ID="hdnnxtapproverEmail" runat="server" />
        <asp:HiddenField ID="hdnnxtapproverid" runat="server" />
        <asp:HiddenField ID="hdnapplevel" runat="server" />
        <asp:HiddenField ID="hdnPreviousApprovercode" runat="server" />
         <asp:HiddenField ID="hdnleaveid" runat="server" />
        <asp:HiddenField ID="hdnleaveconditiontypeid" runat="server" />
        <asp:HiddenField ID="hdnCurrentID" runat="server" />
          <asp:HiddenField ID="hdnCurrentAppName" runat="server" />
        <asp:HiddenField ID="hdnPreviousApprovermails" runat="server" />
   
     <asp:HiddenField ID="hdnHR_EMailID" runat="server" />
    <asp:HiddenField ID="hdnHR_Appr_id" runat="server" />
    <asp:HiddenField ID="hdnHR_Appr_Name" runat="server" />
    <asp:HiddenField ID="hdnHR_ApproverCode" runat="server" />

    <asp:HiddenField ID="hdnstaus" runat="server" />
    <asp:HiddenField ID="hdncrntApp_id" runat="server" />

    <asp:HiddenField ID="hdnApproverid_F" runat="server" />
    <asp:HiddenField ID="hdnApproverid_S" runat="server" />
    <asp:HiddenField ID="hdnApproverid_T" runat="server" />
    <asp:HiddenField ID="hdnappr_type" runat="server" />
    <asp:HiddenField ID="hdnAppr_Cnt" runat="server" />
    <asp:HiddenField ID="hdnisSelfAppr" runat="server" />

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


        function SaveMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=att_submit.ClientID%>');

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

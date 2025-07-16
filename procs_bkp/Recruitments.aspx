<%@ Page Title="" Language="C#" MasterPageFile="~/Recruitments.master" AutoEventWireup="true"
    CodeFile="Recruitments.aspx.cs" Inherits="Recruitments" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />    
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Recruitment_details_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Recruitment_css.css" type="text/css" media="all" /> 
    
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
            background: url('../images/arrowdown.png') no-repeat right center;
            
            cursor: default;
        }
        .graytextbox {
            border-bottom: 2px solid #cccccc;
            /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/            
            background-color:#ebebe4;
        }
        .taskparentclass3 {
            width: 29.5%;
            height: 112px !important;
        }
        .noneditableColor {
            color:red
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
                            <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="green" Visible="false" Style="margin-left: 135px"></asp:Label>
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
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtmobile" 
                                            ErrorMessage="Mobile No. is mandatory" ToolTip="Mobile No. is mandatory" ValidationGroup="validate"
                                            SetFocusOnError="true" CssClass="error_field" Display="Dynamic"></asp:RequiredF>--%>
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
                        <asp:Label ID="lblheading" runat="server" Text="Candidate Details"></asp:Label>
                    </span>
                </div>
                <div>
                    <!--<asp:Label runat="server" ID="lblmessage"  Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>-->
                    <asp:Label runat="server" ID="Label1" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>
                <div>     
                    <span>
                        <a href="Recruitment_index.aspx" class="aaaa">Recruitment Index</a>
                    </span>
                </div>
              
                <div class="edit-contact">
                  
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" ></asp:LinkButton>
                        </div>
                    </div>


                    <ul id="editform" runat="server" visible="false">
                         <li class="mobile_inboxEmpCode">                           
                            <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="False"> </asp:TextBox>
                         </li>

                        <li class="mobile_InboxEmpName">                            
                            <asp:TextBox AutoComplete="off" ID="txtEmpName" runat="server" Visible="false"></asp:TextBox>
                        </li>
                        
                        <li class="mobile_detail">  
                            <asp:Label ID="lblnoneditable" runat="server" Text="Following Fields are non Editable" CssClass="noneditableColor" Visible="false"></asp:Label>                         
                            <br /><br />
                             <span id="spmdet" runat="server" visible="false"  class="qualificationdtls"> Details (Recruitment Process)</span>
                            <asp:LinkButton ID="btnTra_Details" Visible="false" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" OnClick="btnTra_Details_Click"></asp:LinkButton>

                            
                        </li>

                        <li class="mobile_inboxEmpCode">                         
                            <asp:TextBox AutoComplete="off" ID="TextBox7" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
                        </li>

                        <li class="mobile_grid">
                            <br />
                            <div>
                                <asp:GridView ID="dgMobileClaim" runat="server"  Visible="false" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%"
                                    DataKeyNames="com_sr_no,interview_id" OnRowCreated="dgMobileClaim_RowCreated">
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                    <PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />

                                    <Columns>
                                        <asp:BoundField HeaderText="Date"
                                            DataField="Rem_Month"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" />

                                        <asp:BoundField HeaderText="Activity"
                                            DataField="pv"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="15%" />

                                        <asp:BoundField HeaderText="Sub-Type"
                                            DataField="Particulars"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="15%" />

                                        <asp:BoundField HeaderText="Status"
                                            DataField="status_name"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="20%" />

                                        <asp:BoundField HeaderText="Recruiter Name"
                                            DataField="Emp_Name"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="30%" />
                                        

                                        <asp:TemplateField HeaderText="Details" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" Text='View' OnClick="lnkEdit_Click">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%"/>
                                        </asp:TemplateField>


                                    </Columns>
                                </asp:GridView>
                              </div>
                        </li>

                        <li class="mobile_inboxEmpCode">
                            <asp:LinkButton ID="Link_Cand" runat="server" Enabled="false" Text="CANDIDATE DETAILS:" ToolTip="CANDIDATE DETAILS"></asp:LinkButton>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span> </span>
                         </li>

                        <li class="mobile_inboxEmpCode">
                            <span>Name:</span> <span style="color:red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_Candidate_Name" runat="server" MaxLength="100"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span> </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="TextBox4" runat="server" MaxLength="100" Visible="false"></asp:TextBox>
                         </li>

                        <li class="mobile_inboxEmpCode">
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
                        <li class="mobile_inboxEmpCode">
                            <span>Birth Date: </span>  <span style="color:red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_BirthDate" runat="server" AutoPostBack="false" OnTextChanged="Txt_BirthDate_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2"  TargetControlID="Txt_BirthDate"
                             Format="dd/MM/yyyy" runat="server">
                            </ajaxToolkit:CalendarExtender>
                         </li>

                        <li class="mobile_inboxEmpCode">
                            <span>Email-Id: </span> <span style="color:red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_email" runat="server" MaxLength="100"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Alternate Email-Id: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_alternate_email" runat="server" MaxLength="100"></asp:TextBox>
                         </li>

                        <li class="mobile_inboxEmpCode">
                            <span>Mobile No.: </span> <span style="color:red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_mobile" runat="server" MaxLength="100"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Alternate Mobile No.: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_alternate_mobile" runat="server" MaxLength="100"></asp:TextBox>
                         </li>

                        <li class="mobile_inboxEmpCode">
                            <span>Address: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_Address" TextMode="MultiLine" runat="server" MaxLength="300" Height="100px"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Profile Details: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_Profile" TextMode="MultiLine" runat="server" MaxLength="300" Height="100px"></asp:TextBox>
                         </li>

                        <li class="mobile_inboxEmpCode">
                            <span style="display:none"> Qualification: </span>
                             <span>Age(Years)  (eg 26.06): </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_Qualification" runat="server" MaxLength="100" Visible="false"></asp:TextBox>
                            <asp:TextBox AutoComplete="off" ID="txt_age" runat="server" MaxLength="5"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Experience (Years)  (eg 04.06): </span><span style="color:red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_Experience" runat="server" MaxLength="5"></asp:TextBox>
                         </li>

                        <li class="mobile_inboxEmpCode">
                            <span>Source: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_Source" runat="server" MaxLength="100" CssClass="Dropdown"></asp:TextBox>
                            <asp:Panel ID="Panel6" Style="display: none;" runat="server" CssClass="taskparentclasskkk">                              
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lst_Source" runat="server" AutoPostBack="true" OnSelectedIndexChanged="lst_Source_SelectedIndexChanged" >
                                        </asp:ListBox>
                                    </ContentTemplate>
                                    <Triggers>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </asp:Panel>
                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender5" PopupControlID="Panel6" TargetControlID="Txt_Source"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Source Details: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_Source_Details" runat="server" MaxLength="100"></asp:TextBox>
                         </li>
                         <li class="mobile_inboxEmpCode">
                            <span>Search Keywords:(, keywords) </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_search_keywords" runat="server" MaxLength="100"></asp:TextBox>                        
                        </li>
                        <li class="mobile_inboxEmpCode">
                         </li>
                        <li class="mobile_inboxEmpCode">
                            <br />
                            <span class="qualificationdtls">Qualification Details: </span>
                            &nbsp;&nbsp;  <asp:LinkButton ID="lnk_addQualification" runat="server" ToolTip="Add Qualification" Text="+" OnClick="lnk_addQualification_Click" CssClass="qualificationdtls">+</asp:LinkButton>                           
                        </li>
                        
                          <li class="mobile_grid">                            
                            <div>
                                <asp:GridView ID="gvCandidate_Qualifications" runat="server"  Visible="false" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%"
                                    DataKeyNames="pos_srno,interview_id">
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                    <PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />

                                    <Columns>
                                        <asp:BoundField HeaderText="Qualification Type"
                                            DataField="qualification_type"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="10%" />

                                        <asp:BoundField HeaderText="Qualfication"
                                            DataField="qualification_name"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="30%" />

                                        <asp:BoundField HeaderText="Type"
                                            DataField="pg_grdute_others_type"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="15%" />

                                        <asp:BoundField HeaderText="Premium institute"
                                            DataField="qualification_institute"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="35%" />                                     

                                        <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkQualificationView" runat="server" Text='View' OnClick="lnkQualificationView_Click">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%"/>
                                        </asp:TemplateField>


                                    </Columns>
                                </asp:GridView>
                              </div>
                        </li>

                       
                            <li class="mobile_inboxEmpCode">  
                                                 
                          </li>
                          <li class="mobile_inboxEmpCode">                            
                         </li>

                         <li class="mobile_inboxEmpCode">                            
                            <span class="qualificationdtls"> Functions </span> &nbsp;&nbsp;  <asp:LinkButton ID="addfunctions" runat="server" ToolTip="Add Functions" Text="+" OnClick="addfunctions_Click" CssClass="qualificationdtls">+</asp:LinkButton>
                            <asp:TextBox AutoComplete="off" ID="txt_functions" runat="server" CssClass="Dropdown" ></asp:TextBox>
                            <asp:Panel ID="Panel2" Style="display: none;" runat="server" CssClass="taskparentclasskkk">                              
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lst_functions" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="lst_functions_SelectedIndexChanged">
                                                       
                                        </asp:ListBox>
                                    </ContentTemplate>
                                    <Triggers>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </asp:Panel>
                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender1" PopupControlID="Panel2" TargetControlID="txt_functions"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>
                             <asp:LinkButton ID="lnk_addFunctions" runat="server" Text="Add" ToolTip="Add Functions" Visible="false" CssClass="Savebtnsve" OnClientClick=" return MultiClick();"  OnClick="lnk_addFunctions_Click" >Add</asp:LinkButton>
                         </li>
                         <li class="mobile_inboxEmpCode">   
                                                           
                         </li>

                        <li class="mobile_grid">                           
                              <div>
                            <asp:GridView ID="gv_candidates_functions" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="80%"
                                     DataKeyNames="function_id,interview_id" >
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" HorizontalAlign="Center" />
                                    <PagerStyle ForeColor="#000066" HorizontalAlign="left" BackColor="White" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />

                                    <Columns>
                                          <asp:BoundField HeaderText="Sr.No"
                                            DataField="function_sr_no"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="10%" />

                                        <asp:BoundField HeaderText="Function Name"
                                            DataField="function_name"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="60%" />
                                         
                                        
                                        <asp:TemplateField HeaderText="Remove Function" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                        <asp:LinkButton ID="lnkRemove_function" runat="server" Text='Remove' OnClick="lnkRemove_function_Click" Width="10%" >
                                        </asp:LinkButton>
                                        </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                   

                                    </Columns>
                            </asp:GridView>
                            </div>
                         </li>
                        
                         <li class="mobile_inboxEmpCode">   
                              &nbsp;&nbsp;&nbsp;                             
                         </li>
                         <li class="mobile_inboxEmpCode">   
                              &nbsp;&nbsp;&nbsp;                             
                         </li>

                          <li class="mobile_inboxEmpCode">                            
                           <span class="qualificationdtls">   Positions  </span> &nbsp;&nbsp; <asp:LinkButton ID="lnk_addPositions" runat="server" ToolTip="Add Positions" Text="+" OnClick="lnk_addPositions_Click" CssClass="qualificationdtls"> +</asp:LinkButton>

                               <asp:TextBox AutoComplete="off" ID="txt_positions" runat="server" CssClass="Dropdown" ></asp:TextBox>
                            <asp:Panel ID="Panel3" Style="display: none;" runat="server" CssClass="taskparentclasskkk">                              
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lst_positions" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="lst_positions_SelectedIndexChanged">
                                                        
                                        </asp:ListBox>
                                    </ContentTemplate>
                                    <Triggers>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </asp:Panel>
                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender2" PopupControlID="Panel3" TargetControlID="txt_positions"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>

                              <asp:LinkButton ID="lnk_addPostions_tmp" runat="server" Text="Add" ToolTip="Add Position" Visible="false" CssClass="Savebtnsve" OnClientClick=" return MultiClick();"  OnClick="lnk_addPostions_tmp_Click" >Add</asp:LinkButton>

                         </li>
                         <li class="mobile_inboxEmpCode">                            
                         </li>

                        <li class="mobile_grid">
                             <div>
                            <asp:GridView ID="gv_candidates_positions" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="80%"
                                     DataKeyNames="position_id,interview_id" >
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" HorizontalAlign="Center" />
                                    <PagerStyle ForeColor="#000066" HorizontalAlign="left" BackColor="White" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />

                                    <Columns>
                                          <asp:BoundField HeaderText="Sr.No"
                                            DataField="position_sr_no"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="10%" />

                                        <asp:BoundField HeaderText="Position Name"
                                            DataField="position_name"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="60%" />
                                         
                                        
                                        <asp:TemplateField HeaderText="Remove Position" ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <ItemTemplate>
                                        <asp:LinkButton ID="lnkRemove_position" runat="server" Text='Remove' OnClick="lnkRemove_position_Click" Width="10%" >
                                        </asp:LinkButton>
                                        </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                            </asp:GridView>
                            </div>
                       
                         </li>
                         <li class="mobile_inboxEmpCode">                            
                         </li>

                        <li class="mobile_inboxEmpCode" style="display:none">
                            <asp:LinkButton ID="Link_opn" runat="server" Enabled="false" Text="OPENING DETAILS:" ToolTip="OPENING DETAILS"></asp:LinkButton>
                            <!--<span style="color:blue;text-underline-position:below;text-decoration:underline">OPENING DETAILS: </span>-->
                            <br />
                        </li>
                        <li class="mobile_inboxEmpCode">                                                        
                         </li>
                        <li>
                            <span id="idisconfidential" runat="server" visible="false">IS Confidential</span> 
                            <asp:CheckBox ID="chkConfidential" runat="server" Visible="false" />                            
                            <asp:TextBox AutoComplete="off" ID="txt_confidential_resume_recuriter" runat="server"  placeholder="Type recuriter Name for help" Visible="false"></asp:TextBox>
                        </li>

                        <li>      
                            <span style="display:none">For Position  *</span><br />
                            <asp:TextBox AutoComplete="off" ID="Txt_Position" runat="server" placeholder="Type Position Name for help" Visible="false"></asp:TextBox>
                        </li>
                        
                        <li class="claimmob_upload">
                            <br />
                            <span>Upload Resume </span> <span style="color:red">*</span><br />
                            <asp:FileUpload ID="uploadfile" runat="server" AllowMultiple="true" />
                            <asp:LinkButton ID="lnkuplodedfile" OnClick="lnkuplodedfile_Click" runat="server" Visible="false"></asp:LinkButton>
                        </li>
                          <li class="mobile_inboxEmpCode">    
                            <br />
                            <asp:TextBox AutoComplete="off" ID="TextBox6" runat="server" MaxLength="100" Visible="false" ></asp:TextBox>
                        </li>
                        <li class="mobile_grid">
                            <div>
                            <asp:GridView ID="gvfuel_pvFiles" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%"
                                     DataKeyNames="t_id" >
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" HorizontalAlign="Center" />
                                    <PagerStyle ForeColor="#000066" HorizontalAlign="left" BackColor="White" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />

                                    <Columns>
                                        <asp:BoundField HeaderText="Candidate Resume"
                                            DataField="file_name"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="80%" />
                                         
                                        
                                        <asp:TemplateField HeaderText="Files" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                        <asp:LinkButton ID="lnkViewFiles" runat="server" Text='View' OnClick="lnkViewFiles_Click" Width="20%" >
                                        </asp:LinkButton>
                                        </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                   

                                    </Columns>
                            </asp:GridView>
                            </div>
                        </li>
                       <li>
                           &nbsp;
                       </li>
                        <li>&nbsp;</li>
                          <li>                           
                            <asp:LinkButton ID="trvl_accmo_btn" Visible="false" runat="server" Text="Extend (Recruitment Process)" ToolTip="Extend (Recruitment Process)" CssClass="Savebtnsve" OnClick="lnkExtendRights_Click"></asp:LinkButton>                             
                        </li>
                          <li class="mobile_grid">
                            <div>
                            <asp:GridView ID="gvExtendRights" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%"
                                     DataKeyNames="emp_code" >
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" HorizontalAlign="Center" />
                                    <PagerStyle ForeColor="#000066" HorizontalAlign="left" BackColor="White" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />

                                    <Columns>
                                        <asp:TemplateField HeaderText="Select Recruiter" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkExtend" runat="server" />
                                        </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" 
                                            Width="5%"/>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Recruiter Name"
                                            DataField="Emp_Name"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="40%" />
                                      <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkView" runat="server" />
                                        </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" 
                                            Width="5%"/>
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="Update" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkUpdate" runat="server" />
                                        </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" 
                                            Width="5%"/>
                                        </asp:TemplateField>                                  
                                        
                                    </Columns>
                            </asp:GridView>
                            </div>
                        </li>
                                   

                          <li>
                            <span id="spn_release" runat="server">Release &nbsp;&nbsp;&nbsp;</span><asp:CheckBox id="chkRealse" runat="server" /> 
                        </li>
                        <li>                                 
                            <span style="display:none">For Department&nbsp;&nbsp;*</span>
                            <asp:TextBox AutoComplete="off" ID="Txt_Dept" runat="server" placeholder="Type Department Name for help" Visible="false"></asp:TextBox>
                            
                        </li>
                          <li class="mobile_grid">      
                              <br />
                            <span id="spn_position_recuriter" runat="server">Position by Recuriter</span><br />                            
                              <div>
                            <asp:GridView ID="gv_Recuriter_postnDtls" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%"
                                     DataKeyNames="pos_id,dept_id,loc_code,isattachsubpos" OnRowDataBound="gv_Recuriter_postnDtls_RowDataBound">
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" HorizontalAlign="Center" />
                                    <PagerStyle ForeColor="#000066" HorizontalAlign="left" BackColor="White" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />

                                    <Columns>
                                        <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkDeptLocation" runat="server"/>
                                        </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" 
                                            Width="10%"/>
                                        </asp:TemplateField>

                                        <asp:BoundField HeaderText="Position Name"
                                            DataField="position_tile"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="30%" />


                                        <asp:BoundField HeaderText="Department Name"
                                            DataField="dept_name"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="30%" />
                                      
                                        <asp:BoundField HeaderText="Location Name"
                                            DataField="loc_name"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="30%" />             
                                        
                                    </Columns>
                            </asp:GridView>
                                  <asp:TextBox AutoComplete="off" ID="txt_position_by_recuriter" runat="server" placeholder="Type Position Name for help" Visible="false"></asp:TextBox>
                            </div>

                        </li>
                        <!--<li class="claimmob_upload">
                            <span>Upload Receipt</span><br />
                            <asp:FileUpload ID="uploadRcpt" runat="server" />
                            <asp:LinkButton ID="lnkuploadRcpt" OnClick="lnkuploadRcpt_Click" runat="server"></asp:LinkButton>
                        </li>-->

                        <li>
                            <span style="display:none">Posting Location *</span><br />
                            <br />                            
                            <asp:TextBox ID="txtPosting_Location" runat="server" placeholder="Type Location Name for help" Visible="false"></asp:TextBox>

                        </li>
                        <li class="mobile_inboxEmpCode">                           
                            <asp:TextBox AutoComplete="off" ID="TextBox8" runat="server" MaxLength="100" Visible="false"></asp:TextBox>
                        </li>


                    </ul>
                </div>
            </div>
        </div>

    <div class="mobiletrvl_Savebtndiv">
        <asp:LinkButton ID="mobile_btnSave" runat="server" Text="Save" ToolTip="Save" CssClass="Savebtnsve" OnClientClick=" return MultiClick();"  OnClick="mobile_btnSave_Click1" >Save</asp:LinkButton>
        <!--<asp:LinkButton ID="mobile_cancel" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="Savebtnsve" >Cancel</asp:LinkButton>-->
        <asp:LinkButton ID="claimmob_btnBack" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick ="claimmob_btnBack_Click" ></asp:LinkButton>

    </div>

    <br />
     <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchLocations" MinimumPrefixLength="3"
        CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtPosting_Location"
        ID="AutoCompleteExtender2" runat="server" FirstRowSelected="true">
        </ajaxToolkit:AutoCompleteExtender>
     <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchDepartment" MinimumPrefixLength="3"
        CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="Txt_Dept"
        ID="AutoCompleteExtender1" runat="server" FirstRowSelected="true">
        </ajaxToolkit:AutoCompleteExtender>
     <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchPosition" MinimumPrefixLength="3"
        CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="txt_position_by_recuriter"
        ID="AutoCompleteExtender3" runat="server" FirstRowSelected="true">
        </ajaxToolkit:AutoCompleteExtender>

     <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchRecruiter" MinimumPrefixLength="3"
        CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="txt_confidential_resume_recuriter"
        ID="AutoCompleteExtender4" runat="server" FirstRowSelected="true">
        </ajaxToolkit:AutoCompleteExtender>

        
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />

    <asp:HiddenField ID="hdnGrade" runat="server" />
    <asp:HiddenField ID="hdnsptype" runat="server" />
    <asp:HiddenField ID="hdnclaimid" runat="server" />
    <asp:HiddenField ID="hdnclaimqry" runat="server" />
    <asp:HiddenField ID="hdnremid" runat="server" />
    <asp:HiddenField ID="hdnDeviation" runat="server" />
    <asp:HiddenField ID="hdnclaimidO" runat="server" />    

    <asp:HiddenField ID="hflGrade" runat="server" />
    <asp:HiddenField ID="hdnTrdays" runat="server" />
    <asp:HiddenField ID="hdnMobRemStatusM" runat="server" />
    <asp:HiddenField ID="hdnMobRemStatus_dtls" runat="server" />
    <asp:HiddenField ID="hdnTravelConditionid" runat="server" />
<asp:HiddenField ID="hflEmpDesignation" runat="server" />
        <asp:HiddenField ID="hflEmpDepartment" runat="server" />
        <asp:HiddenField ID="hflEmailAddress" runat="server" />
        <asp:HiddenField ID="hdnYesNo" runat="server" />
        <asp:HiddenField ID="hdnLocation" runat="server" />
        <asp:HiddenField ID="hdnDept" runat="server" />
        <asp:HiddenField ID="hdnPosition" runat="server" />
        <asp:HiddenField ID="hdnSourceid" runat="server" />
        <asp:HiddenField ID="hdnisRecuirterlogin" runat="server" />
        <asp:HiddenField ID="hdnpageType" runat="server" />
        <asp:HiddenField ID="hdnAttachPosNo" runat="server" />
        <asp:HiddenField ID="hdnposid" runat="server" />
        
    <script type="text/javascript">
        
        function textboxMultilineMaxNumber(txt, maxLen) {
            try {
                if (txt.value.length > (maxLen - 1)) return false;
            } catch (e) {
            }
        }
        function SetDeviation(pv) {
            if (pv != "") {
                document.getElementById("<%=hdnSourceid.ClientID%>").value = pv;
            }
            return;
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

            document.getElementById('txtFromdate1').options.add(optn);
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


        function onCharOnly(e) {
            var keynum;
            var keychar;
            var numcheck = /[abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ ]/;

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
            var numcheck = /[0123456789]/;

            if (window.event) {
                keynum = e.keyCode;
            }
            else if (e.which) {
                keynum = e.which;
            }
            keychar = String.fromCharCode(keynum);
            return numcheck.test(keychar);
        }
        function onCharOnlyNumber_e(e) {
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

        function MultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=mobile_btnSave.ClientID%>');

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

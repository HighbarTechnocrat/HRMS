<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
     CodeFile="ManageRecruitments.aspx.cs" Inherits="ManageRecruitments" %>

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
                        <asp:Label ID="lblheading" runat="server" Text="Manage Candidate Details"></asp:Label>
                    </span>
                </div>

                <span>
                    <a href="Recruitment_index.aspx" class="aaaa">Recruitment Index</a>
                </span>
                 
                 <div class="edit-contact">

                    <ul id="Ul1" runat="server" visible="true">
                         <li>
                             <span> Search by Location: </span>
                            <br /> 
                            <div>
                            <asp:TextBox ID="txtsrchBy_Location" runat="server" placeholder="Type Location Name for help"></asp:TextBox>
                            </div>
                         </li>

                        <li>
                            <span> Search by Department: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtsrchBy_Dept" runat="server" placeholder="Type Department Name for help"></asp:TextBox>
                        </li>
                        <li>
                            <span> Search by Position: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtsrchBy_Position" runat="server" placeholder="Type Position Name for help"></asp:TextBox>

                        </li>
                        <li>
                            <span> Search by Recruiter Name: </span>
                            <br />                    
                            <asp:TextBox ID="txtsrchBy_Recruiter" runat="server" placeholder="Type Location Name for help"></asp:TextBox>
                        </li>

                         <li class="mobile_inboxEmpCode">      
                             <span> Search by Name: </span>
                            <br />                     
                            <asp:TextBox AutoComplete="off" ID="txtsrchBy_Name" runat="server"> </asp:TextBox>
                         </li>

                        <li class="mobile_InboxEmpName">   
                            <span> Search by Email-Id: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtsrchBy_Email" runat="server"></asp:TextBox>
                        </li>
                        
                        <li class="mobile_detail">                           
                           <span> Search by mobile No: </span>
                            <br />                    
                            <asp:TextBox AutoComplete="off" ID="txtsrchBy_Mobile" runat="server" MaxLength="10"> </asp:TextBox>
                        </li>

                        <li class="mobile_inboxEmpCode">     
                           <span> Search by Experience (Years): </span>
                            <br />                    
                            <asp:TextBox AutoComplete="off" ID="txtsrchBy_Exprens" runat="server" MaxLength="10"> </asp:TextBox>
                        </li>
                        
                        <li class="mobile_detail">
                            
                            <span> Search by Gender: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtsrchBy_Gender" runat="server" CssClass="Dropdown" ></asp:TextBox>
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
                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender4" PopupControlID="Panel5" TargetControlID="txtsrchBy_Gender"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>

                        </li>

                        <li class="mobile_inboxEmpCode">
                            <span> Search by Status: </span>                             
                            <asp:TextBox AutoComplete="off" ID="txtsrchBy_Status" runat="server" CssClass="Dropdown"></asp:TextBox>
                            <asp:Panel ID="Panel4" Style="display: none;" runat="server" CssClass="taskparentclass3">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lst_Status" runat="server" CssClass="taskparentclass3" AutoPostBack="true" OnSelectedIndexChanged="lst_Status_SelectedIndexChanged"></asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender3" PopupControlID="Panel4" TargetControlID="txtsrchBy_Status"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>
                        </li>
                        
                        <li>
                         
                               <span>Source: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtsrchBy_Source" runat="server" MaxLength="100" CssClass="Dropdown"></asp:TextBox>
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
                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender5" PopupControlID="Panel6" TargetControlID="txtsrchBy_Source"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>

                         </li>

                        <li class="mobile_inboxEmpCode">
                            <span> Search by Qualification: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtsrchBy_Qualification" runat="server"  placeholder="Type Qualification Name for help" MaxLength="100"></asp:TextBox>
                             
                        </li> 
                        
                         <li class="mobile_inboxEmpCode">
                            <span>By Key Words </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_srchkeywords" runat="server" MaxLength="100"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>  </span>                          
                            <asp:TextBox AutoComplete="off" ID="TextBox20" runat="server" MaxLength="100" Visible="false" ></asp:TextBox>
                         </li>                                                
                        
                    </ul>
                </div>

                
    <div class="mobiletrvl_Savebtndiv">
        <asp:LinkButton ID="mobile_btnSave" runat="server" Text="Search" ToolTip="Search" CssClass="Savebtnsve"  OnClick="mobile_btnSave_Click" >Search</asp:LinkButton>
        <!--<asp:LinkButton ID="mobile_cancel" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="Savebtnsve" >Cancel</asp:LinkButton>-->
        <asp:LinkButton ID="claimmob_btnBack" runat="server" Text="Reset" ToolTip="Reset" CssClass="Savebtnsve" OnClick ="claimmob_btnBack_Click" ></asp:LinkButton>

    </div>

     
                 

                <div class="manage_grid" style="width: 100%; height: auto;">
                    <center>
                          <asp:GridView ID="gvMngTravelRqstList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                            DataKeyNames="interview_id"   CellPadding="3" AutoGenerateColumns="False" Width="100%"   EditRowStyle-Wrap="false" PageSize="20" AllowPaging="True">
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
                            <asp:BoundField HeaderText="Candidate Name"
                                DataField="cand_name"
                                ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="20%" />
                             
                            <asp:BoundField HeaderText="Candidate e-mail"
                                DataField="cand_email"
                                ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="20%" />

                            <asp:BoundField HeaderText="Candidate Mobile"
                                DataField="cand_mob"
                                 ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="10%" />

                            <asp:BoundField HeaderText="Experience"
                                DataField="cand_exp"
                                 ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="8%" />


                            <asp:BoundField HeaderText="Status"
                                DataField="status_name"
                                 ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="12%" />
   

                            <asp:BoundField HeaderText="Joining Status"
                                DataField="Recruiter_Status"
                                 ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="22%" />

                           <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            <asp:LinkButton ID="lnkLeaveDetails" runat="server" Text='View' OnClick="lnkLeaveDetails_Click">
                            </asp:LinkButton>                           
                            </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="8%"/>
                            </asp:TemplateField>


                        </Columns>
                    </asp:GridView>
                    </center>
                </div>


                <%--'<%#"~/Details.aspx?ID="+Eval("ID") %>'>--%>



                <div class="edit-contact">
                    <%-- <div class="editprofile" style="text-align: center; border: none;" id="divmsg" runat="server" visible="false">
                        <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: green; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                    </div>--%>
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
               <%-- <asp:HiddenField ID="hdnleaveTypeid" runat="server" />
                <asp:HiddenField ID="hdnleaveType" runat="server" />--%>
            </div>
        </div>
    </div>



                     <br />
     <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchLocations" MinimumPrefixLength="3"
        CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtsrchBy_Location"
        ID="AutoCompleteExtender2" runat="server" FirstRowSelected="true" >
        </ajaxToolkit:AutoCompleteExtender>
     <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchDepartment" MinimumPrefixLength="3"
        CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtsrchBy_Dept"
        ID="AutoCompleteExtender1" runat="server" FirstRowSelected="true">
        </ajaxToolkit:AutoCompleteExtender>
        <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchPosition" MinimumPrefixLength="3"
        CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtsrchBy_Position"
        ID="AutoCompleteExtender3" runat="server" FirstRowSelected="true">
        </ajaxToolkit:AutoCompleteExtender>
        
          <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchRecruiter" MinimumPrefixLength="3"
        CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtsrchBy_Recruiter"
        ID="AutoCompleteExtender4" runat="server" FirstRowSelected="true">
        </ajaxToolkit:AutoCompleteExtender>


      <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchQualification" MinimumPrefixLength="3"
        CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtsrchBy_Qualification"
        ID="AutoCompleteExtender5" runat="server" FirstRowSelected="true">
        </ajaxToolkit:AutoCompleteExtender>
             

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


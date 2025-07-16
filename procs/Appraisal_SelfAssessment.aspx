<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"  MaintainScrollPositionOnPostback="true"
    CodeFile="Appraisal_SelfAssessment.aspx.cs" Inherits="Appraisal_SelfAssessment" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />  
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Appraisal_2.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Appraisal.css" type="text/css" media="all" />
    <%--   <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Appraisal_RemRequest.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/AppraisalDetails_css.css" type="text/css" media="all" />
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
            background: #ebebe4;
        }

        #MainContent_lstApprover {
            overflow: hidden !important;
        }

        .BtnShow {
            color: blue !important;
            background-color: transparent;
            text-decoration: none;
            font-size: 13px !important;
        }

            .BtnShow:visited {
                color: blue !important;
                background-color: transparent;
                text-decoration: none;
            }

            .BtnShow:hover {
                color: red !important;
                background-color: transparent;
                text-decoration: none !important;
            }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>

    <script src="../js/freeze/jquery-1.11.0.min.js"></script>

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
                            <asp:DropDownList ID="ddlprofile" runat="server">
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
                        <asp:Label ID="lblheading" runat="server" Text=""></asp:Label>
                    </span>
                </div>
                
                <%-- <div>
                <span>
                    <a href="Appraisalindex.aspx" class="aaaa">Appraisal Index</a>
                </span>
                 </div>--%>


                <div class="edit-contact">

                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server"></asp:LinkButton>
                        </div>
                    </div>
                    <ul>
                     <li class="mobile_deviation" style="display:none">
                              <span id="Span5" runat="server"></span>
                            <asp:LinkButton ID="lnkEmpDtl" Visible="false" runat="server" Text="Employee Details >>" ToolTip="Browse" CssClass="Savebtnsve" OnClick="lnkEmpDtl_Click"></asp:LinkButton>
                        </li>
                      </ul>

                    <ul id="editform" runat="server" visible="false"> 

                        <li class="trvl_type">
                            <span>Appraisal Year :</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                            <asp:TextBox ID="txtAppYearid" AutoComplete="off" runat="server" CssClass="graytextbox"></asp:TextBox>
                        </li>
                        <li class="trvl_type">
                            <span>For the Period :</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                            <asp:TextBox ID="txtPeriod" Enabled="false" AutoComplete="off" runat="server" CssClass="graytextbox"></asp:TextBox>
                        </li>
                        <li class="claimmob_fromdate" runat="server" id="lifrom">
                            <span>From </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <asp:TextBox AutoComplete="off" ID="txtFromdate" runat="server" AutoPostBack="True" OnTextChanged="txtFromdate_TextChanged"></asp:TextBox>
                           
                        </li>
                        <li class="mobile_inboxEmpCode" runat="server" id="liTo"> 
                            <span>To</span>
                            <asp:TextBox AutoComplete="off" ID="txtToDate" runat="server" AutoPostBack="True" OnTextChanged="txtToDate_TextChanged"></asp:TextBox> 
                        </li>
                         <li class="mobile_inboxEmpCode"><span>
                                    
                                    Employee Code :</span>
                                    <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="true" CssClass="graytextbox"> </asp:TextBox>
                                </li>
                                <li class="mobile_InboxEmpName"><span>Employee Name :</span>
                                    <asp:TextBox AutoComplete="off" ID="txtEmpName" runat="server" Visible="true" CssClass="graytextbox"></asp:TextBox>
                                </li>


                                <li class="trvl_type">
                                    <span>Department :</span><br />
                                    <asp:TextBox ID="txtdept" Enabled="false" AutoComplete="off" runat="server" CssClass="graytextbox"></asp:TextBox>
                                </li>
                                <li class="trvl_type">
                                    <span>Designation :</span><br />
                                    <asp:TextBox ID="txtdesig" Enabled="false" AutoComplete="off" runat="server" CssClass="graytextbox"></asp:TextBox> 
                                </li>

                                <li class="trvl_type">
                                    <span>Grade :</span><br />
                                    <asp:TextBox ID="txtposition" Enabled="false" AutoComplete="off" runat="server" CssClass="graytextbox"></asp:TextBox>
                                </li>
                                <li class="trvl_type"> 
                                    <asp:LinkButton ID="lnkfile_KRA" runat="server" OnClientClick="Download_ApprovedKRAFile()" CssClass="BtnShow" Visible="false" Text="Download Approved KRA"></asp:LinkButton> 
                                    <asp:TextBox ID="txtgrade" Visible="false" Enabled="false" AutoComplete="off" runat="server" CssClass="graytextbox"></asp:TextBox>
                                </li>
                             
                       
                            <li class="trvl_type">
                                <span>Appraisal Type :</span><br />
                                 <asp:TextBox ID="lblAppraisalType" Enabled="false" AutoComplete="off" runat="server" CssClass="graytextbox"></asp:TextBox>                                  
                            </li>
                            <li class="trvl_type">
                            </li>
                        <%-- <li class="trvldetails_type">
                            <span id="SpanKRATEMP" runat="server">Select KRA Template</span>&nbsp;&nbsp;<br />
                            <asp:TextBox AutoComplete="off" ID="txtKRATemplate" runat="server" CssClass="grayDropdown"></asp:TextBox>
                            <asp:Panel ID="Panel3" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstKRATemplate" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstKRATemplate_SelectedIndexChanged">                                          
                                        </asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender2" PopupControlID="Panel3" TargetControlID="txtKRATemplate"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>

                        </li>
                        <li>
                            <asp:LinkButton ID="mobile_btnAddKRA" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClick="mobile_btnAddKRA_Click" OnClientClick="return addKRADetailsClick();" >Add KRA Template</asp:LinkButton>
                        </li>--%>

                        <li class="mobile_detail">
                           <br />
                            <asp:LinkButton ID="btnTra_Details" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" OnClick="btnTra_Details_Click"></asp:LinkButton>
                            <span>Section 1.A: Target Evaluation Sheet :<br />
                            </span>
                        </li>
                        <li></li>

                        <li class="mobile_deviation" style="width: 100%; height: 100%">
                            <div style="width: 100%; height: 100%; overflow: no-content">
                                <asp:GridView ID="dgTargetEvaluation" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" Height="100%"
                                    DataKeyNames="Assess_id,KRA_Det_id,RevKRA_Points_Alloted,KRA_Target_Achieved_Points" OnRowDataBound="dgTargetEvaluation_RowDataBound"
                                    OnRowCreated="dgTargetEvaluation_RowCreated" ShowFooter="true">

                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                    <PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    <FooterStyle BackColor="#f2f2f2" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Description"
                                            DataField="KRA_Description"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-VerticalAlign="Top"
                                            ItemStyle-Width="10%" />

                                        <asp:BoundField HeaderText="Points Allotted"
                                            DataField="KRA_Points_Alloted"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-VerticalAlign="Top"
                                            ItemStyle-Width="5%" />


                                        <asp:BoundField HeaderText="Description"
                                            DataField="KRA_AchievedDesc"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-VerticalAlign="Top"
                                            ItemStyle-Width="10%" />

                                        <asp:BoundField HeaderText="Self Assessment"
                                            DataField="KRA_Target_Achieved_Points"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-VerticalAlign="Top"
                                            ItemStyle-Width="5%" />

                                        <asp:TemplateField HeaderText="Points Allotted" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_RevKRA_Points_Alloted" Text='<%# Eval("RevKRA_Points_Alloted") %>' oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*)\./g, '$1');" AutoComplete="off" runat="server" CssClass="txtWidth" AutoPostBack="false" MaxLength="5" Width="35px" ></asp:TextBox>
                                            <%--OnTextChanged="txt_RevKRA_Points_Alloted_TextChanged"--%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="8%" />
                                        </asp:TemplateField>

                                        <%-- <asp:BoundField HeaderText="Points Allotted"
                                            DataField="RevKRA_Points_Alloted"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                             ItemStyle-VerticalAlign="Top"
                                            ItemStyle-Width="5%" />--%>

                                        <%--  <asp:BoundField HeaderText="Description"
                                            DataField="KRA_AchievedReviewerDesc"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="25%" />--%>

                                        <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_KRA_AchievedReviewerDesc" Text='<%# Eval("KRA_AchievedReviewerDesc") %>' AutoComplete="off" runat="server" CssClass="txtWidth" AutoPostBack="false" TextMode="MultiLine" Height="250px" placeholder="Maximum 500 characters" onkeyup="ValidateLimit(this,500);"></asp:TextBox>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="top" Width="10%" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Assessment" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_Target_Achieved_Points_Reviewer" Text='<%# Eval("Target_Achieved_Points_Reviewer") %>' oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*)\./g, '$1');" AutoComplete="off" runat="server" CssClass="txtWidth" AutoPostBack="true" MaxLength="5" Width="35px"  OnTextChanged="txt_Target_Achieved_Points_Reviewer_TextChanged"></asp:TextBox>
                                            <%--    OnTextChanged="txt_Target_Achieved_Points_Reviewer_TextChanged"--%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="8%" />
                                        </asp:TemplateField>

                                        <%--  <asp:BoundField HeaderText="Assessment"
                                            DataField="Target_Achieved_Points_Reviewer"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="5%" />--%>

                                        <asp:TemplateField HeaderText="Details"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-VerticalAlign="Top"
                                            ItemStyle-Width="8%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" Style="text-decoration: underline" Text='View' OnClick="lnkEdit_Click">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>  
                                    </Columns>
                                </asp:GridView>

                             
                            </div>
                        </li>

                       <li class="mobile_deviation" id="liTargetEvalutionFiles_1" runat="server" visible="false">
                           <br />
							<span id="spnSupportinFiles" runat="server">Target Evaluation Supporting Documents</span>
							<asp:GridView ID="gvuploadedFiles" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false">
								<FooterStyle BackColor="White" ForeColor="#000066" />
								<HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
								<PagerStyle ForeColor="#000066" HorizontalAlign="left" BackColor="White" />
								<RowStyle ForeColor="#000066" />
								<SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
								<SortedAscendingCellStyle BackColor="#F1F1F1" />
								<SortedAscendingHeaderStyle BackColor="#007DBB" />
								<SortedDescendingCellStyle BackColor="#CAC9C9" />
								<SortedDescendingHeaderStyle BackColor="#00547E" />
								<Columns>
									<asp:TemplateField HeaderText="File Name">
										<ItemTemplate>
											<asp:LinkButton ID="lnkviewfile" CssClass="BtnShow" runat="server" OnClientClick=<%# "DownloadFile('" + Eval("file_name") + "')" %> Text='<%# Eval("Target_Evaluation_File") %>'>
											</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateField> 
								</Columns>
							</asp:GridView>
						</li>
                          <li class="mobile_deviation" id="liTargetEvalutionFiles_2" runat="server" visible="false">
                            </li>
                        <li class="mobile_detail" id="licompetancy" runat="server">
                            <br />
                            <asp:LinkButton ID="btnTra_competancy" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" OnClick="btnTra_competancy_Click"></asp:LinkButton>
                            <div id="iddivAttributes" runat="server" style="width: 277% !important; height: 30px; background-color: #3D1956; color: white; vertical-align: middle">
                                <asp:Label ID="lblSection1Attributes" runat="server" Text="Section 1.B: Attributes :"></asp:Label>
                            </div>
                            <br /> 
                        </li>


                        <li class="trvl_local" style="width: 90%;" id="licompetancy_2" runat="server">
                            <div>
                                <asp:GridView ID="gvattributeLegend" runat="server" BackColor="#f2f2f2" BorderColor="#f2f2f2" BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%">
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
                                        <asp:BoundField HeaderText="Rating"
                                            DataField="descript"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" />
                                        <asp:BoundField HeaderText="Definition"
                                            DataField="desc"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="90%" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <br />
                        </li>

                        <li class="trvl_local" style="width: 90%;"  id="licompetancy_3" runat="server">
                            <asp:GridView ID="gvCompetency" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                DataKeyNames="Comp_Buk_id,Comp_head_id,comp_det_id,rating" OnDataBound="OnDataBound" OnRowDataBound="gvCompetency_RowDataBound">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                <PagerStyle ForeColor="#000066" HorizontalAlign="left" BackColor="White" />
                                <RowStyle ForeColor="#000066" />

                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                                <Columns>
                                    <asp:BoundField DataField="Comp_header" HeaderText="Attributes" ItemStyle-Width="20%" HeaderStyle-HorizontalAlign="Center" />

                                    <%--<asp:BoundField DataField="Comp_Detail_Desc"   HeaderText="Descriptors for Attributes"  ItemStyle-Width="40%" ItemStyle-CssClass ="display: table;"  HeaderStyle-HorizontalAlign="Center" />--%>
                                    <%--<asp:TemplateField HeaderText="Descriptors for Attributes"  ItemStyle-VerticalAlign="Middle"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50%">
                                            <ItemTemplate><div class="newdivp1">
                                               <asp:TextBox css="newdivp" TextMode="MultiLine"  Enabled="false" BackColor="White"  
                                                   style="resize:none;height:auto; max-height:300px;min-height:190px; border: 0px solid !important;font-size: 14px; font-weight: normal;color: #000066;font-family: Roboto; "
                                                     ID="lblheader" runat="server" Text='<%# Eval("Comp_Detail_Desc") %>' ></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Descriptors for Attributes" ItemStyle-Width="50%" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <div>
                                                <%# this.GetPlainText(Eval("Comp_Detail_Desc")) %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="Comp_Detail_Desc"  HeaderText="Rating" ItemStyle-Width="50" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center"/>--%>
                                    <asp:TemplateField HeaderText="Reviewee Rating" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <%-- <asp:CheckBox ID="chkRowReviewee"  Checked='<%# Eval("Reviewee_RatingID") %>'  runat="server" ItemStyle-Width="70" />--%>
                                            <asp:DropDownList ID="ddlRowReviewee" runat="server" Style="padding-left: 33px !important; border-top: solid; border-top-color: #05568B; border-top-width: 1.5pt; border-left: solid; border-left-color: #05568B; border-left-width: 1.5pt; border-right: solid; border-right-color: #05568B; border-right-width: 1.5pt;">
                                            </asp:DropDownList>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Reviewer Rating" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <%--<asp:CheckBox ID="chkRowReviewer"  Checked='<%# Eval("Reviewer_RatingID") %>'  runat="server" ItemStyle-Width="50" />--%>
                                            <asp:DropDownList ID="ddlRowReviewer" runat="server" Style="padding-left: 33px !important; border-top: solid; border-top-color: #05568B; border-top-width: 1.5pt; border-left: solid; border-left-color: #05568B; border-left-width: 1.5pt; border-right: solid; border-right-color: #05568B; border-right-width: 1.5pt;">
                                            </asp:DropDownList>

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView> 
                          
                        </li>
                        <li class="trvl_local" style="width: 90%;">
                                <asp:GridView ID="gvCompetency_New" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                DataKeyNames="Comp_Buk_id,Comp_head_id" OnRowDataBound="gvCompetency_New_RowDataBound">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                <PagerStyle ForeColor="#000066" HorizontalAlign="left" BackColor="White" />
                                <RowStyle ForeColor="#000066" />

                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                                <Columns>
                                    <asp:BoundField DataField="Comp_header" HeaderText="Attributes" ItemStyle-Width="20%" HeaderStyle-HorizontalAlign="Center" />
 
                                    <asp:TemplateField HeaderText="Descriptors for Attributes" ItemStyle-Width="50%" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                             <asp:GridView ID="gvCompetency_AttributeDesc" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid" ShowHeader="false"
                                                 DataKeyNames="Comp_Det_id">
                                                <Columns>
                                                       <asp:BoundField DataField="Comp_Detail_Desc" ItemStyle-Width="20%" HeaderStyle-HorizontalAlign="Center" />
                                               
                                                    <asp:TemplateField HeaderText="Reviewer Rating" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                             <asp:RadioButton ID="rbtnRating" runat="server" onclick="singleRbtnSelect(this);" Text='<%#Eval("descript") %>' />
                                                              <asp:HiddenField ID="hidRatingID" runat="server" Value='<%#Eval("id")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>

                                            </asp:GridView>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     
                                </Columns>
                            </asp:GridView> 
                            <asp:Button ID="btnCheck" runat="server" Text="Check"  OnClick="btnCheck_Click" Visible="false"/>
                        </li>

                        <li class="mobile_deviation" style="width: 100%">
                            <div>
                                <asp:GridView ID="dgCompetancyEvaluation" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%"
                                    DataKeyNames="Assess_id,Comp_Buk_id" ShowFooter="true">
                                    <%--DataKeyNames="Assess_id,Comp_Buk_id,Comp_Head_id,Comp_Det_id"--%>
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                    <PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    <FooterStyle BackColor="#f2f2f2" HorizontalAlign="Right" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Attributes"
                                            DataField="Comp_Header"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="20%" />

                                        <asp:BoundField HeaderText="Reviewee Competency Description"
                                            DataField="Reviewee_Desc"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="30%" />
                                        <asp:BoundField HeaderText="Reviewee Rating"
                                            DataField="Reviewee_Rating"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="5%" />
                                        <asp:BoundField HeaderText="Reviewer Competency Description"
                                            DataField="Reviewer_Desc"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="30%" DataFormatString="{0:N2}" />


                                        <asp:BoundField HeaderText="Reviewer Rating"
                                            DataField="Reviewer_Rating"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="5%" DataFormatString="{0:N2}" />

                                    </Columns>

                                </asp:GridView>
                            </div>
                        </li>
                        <li class="mobile_deviation" style="width: 100%;">
                            <br />
                            <span>Overall comments of the Reviewee :</span>
                            <!--Additional Comments of Reviewee-->
                            <asp:TextBox AutoComplete="off" placeholder="Maximum 500 characters" ID="txtOverallComment" Enabled="true" runat="server" Height="100" TextMode="MultiLine" onkeyup="ValidateLimit(this,500);" OnTextChanged="txtOverallComment_TextChanged"></asp:TextBox>
                        </li>
                        <li class="mobile_deviation" style="width: 90%" runat="server" id="liPRD">
                            <asp:LinkButton ID="btnlnkPRD" runat="server" Text="Section 1.C: Performance Review Discussion (PRD)" ToolTip="Browse" CssClass="Savebtnsve"></asp:LinkButton><%--OnClick="btnlnkPRD_Click"--%>
                            <span id="SpanPRD" runat="server"></span>
                            <div id="divPRD" runat="server"></div>
                        </li>
                        <%--         <ul>--%>
                        <li class="mobile_deviation" style="width: 100%;" runat="server" id="liPRD5">
                            <span>
                                <br />
                                To be filled by the Reviewer in discussion with the Reviewee
                                <br />
                                <i>(Specific data or examples need to be cited)</i> </span>
                        </li>
                        <li class="mobile_deviation" style="width: 100%;" runat="server" id="liPRD1">
                            <br />
                            <span>I. Tasks accomplished successfully :</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <asp:TextBox AutoComplete="off" ID="txtPRD1" placeholder="Maximum 500 characters" Enabled="true" runat="server" Height="100" TextMode="MultiLine" onkeyup="ValidateLimit(this,500);" OnTextChanged="txtPRD1_TextChanged"></asp:TextBox>
                        </li>
                        <li class="mobile_deviation" style="width: 100%;" runat="server" id="liPRD2">
                            <br />
                            <span>II. Tasks that could have been handled in a better way :</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <asp:TextBox AutoComplete="off" ID="txtPRD2" placeholder="Maximum 500 characters" Enabled="true" runat="server" Height="100" TextMode="MultiLine" onkeyup="ValidateLimit(this,500);" OnTextChanged="txtPRD2_TextChanged"></asp:TextBox>
                        </li>
                        <li class="mobile_deviation" style="width: 100%;" runat="server" id="liPRD3">
                            <div style="display:none">
                            <span>III. Efforts taken to demonstrate the brand behaviors :</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <asp:TextBox AutoComplete="off" ID="txtPRD3" placeholder="Maximum 500 characters" Enabled="true" runat="server" Height="100" TextMode="MultiLine" onkeyup="ValidateLimit(this,500);" OnTextChanged="txtPRD3_TextChanged"></asp:TextBox>
                            </div>
                                </li>
                        <li class="mobile_deviation" style="width: 100%;" runat="server" id="liPRD4">
                            <br />
                            <span>III. Any concerns that is hampering the performance of the Reviewee :</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <asp:TextBox AutoComplete="off" ID="txtPRD4" placeholder="Maximum 500 characters" Enabled="true" runat="server" Height="100" TextMode="MultiLine" onkeyup="ValidateLimit(this,500);" OnTextChanged="txtPRD4_TextChanged"></asp:TextBox>
                        </li>
                        <%--  </ul> 
                        --%>

                        <li class="mobile_deviation" style="width: 90%" id="liIDP_1" runat="server">
                            <br />
                            <asp:LinkButton ID="btnlnkIDP" runat="server" Text="IV. Individual Development Plan (IDP) " ToolTip="Browse" CssClass="Savebtnsve"></asp:LinkButton><%--OnClick="btnlnkIDP_Click"--%>
                            <span id="SpanIDP" runat="server"></span>
                            <div id="divIDP" runat="server"></div>
                            <br />
                        </li>
                        <%--  <ul>--%>
                        <li class="mobile_deviation" style="width: 75%" runat="server" id="liStrengthadd"> 
                            <asp:LinkButton ID="lnkStrengthAdd" runat="server" Text="+ " ToolTip="Browse" CssClass="Savebtnsve" OnClick="txtIDP1_TextChanged"></asp:LinkButton> 
                             <div id="div1" runat="server" style="width:133% !important; height: 30px; background-color: #3D1956; color: white; vertical-align: middle">
                               <span id="SpanStrengthdesc" runat="server">Strength of Reviewee (Top three strengths):</span> &nbsp;&nbsp;<span style="color: red" id="SpanStrength" runat="server">*</span>
                                 <br />
                            </div> 
                            <asp:TextBox ID="txtIDP1" AutoComplete="off" placeholder="First type Strength here,and then click (+) to add. Maximum 50 characters" Enabled="true" runat="server" MaxLength="50"></asp:TextBox>
                              
                             
                            <%--<br />  <asp:TextBox ID="txtIDP2" AutoComplete="off" Enabled="true" runat="server"  MaxLength="100" OnTextChanged="txtIDP2_TextChanged" ></asp:TextBox>
                                         <br />  <asp:TextBox ID="txtIDP3" AutoComplete="off" Enabled="true" runat="server"  MaxLength="100" OnTextChanged="txtIDP3_TextChanged"></asp:TextBox>--%>
                        </li>
                         <li class="mobile_deviation" runat="server" id="liStrengthadd_1"> 
                             <br />
                               <asp:DropDownList ID="ddlStrengths"  Width="350px" runat="server" OnSelectedIndexChanged="ddlStrengths_SelectedIndexChanged" AutoPostBack="true" CssClass="DropdownListSearch">
                              </asp:DropDownList>
                        </li>
                        <li class="mobile_deviation" style="width: 80%" runat="server" id="ligvStrength">
                            <div>
                                <br />
                                <asp:GridView ID="gvStrength" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%"
                                    DataKeyNames="Assess_id,Strengthid" EmptyDataText="Strength not yet Added">
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
                                        <asp:BoundField HeaderText="Sr. No"
                                            DataField="Srno"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" />
                                        <asp:BoundField HeaderText="Strength"
                                            DataField="Strength"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="80%" />

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEditStrength" runat="server" Style="text-decoration: underline" Text='Delete' OnClick="lnkEditStrength_Click">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>

                        <li class="mobile_detail" runat="server" id="liDevPlan">
                            <br />
                          
                            <asp:LinkButton ID="btnTra_DevelopmentPlan" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" OnClick="btnTra_DevelopmentPlan_Click"></asp:LinkButton>

                            <div id="divDevelopmentPlan" runat="server" style="width:277% !important; height: 30px; background-color: #3D1956; color: white; vertical-align: middle">
                                <span>Development Plan :<br />
                                </span>
                            </div>
                            <br /> 
                        </li> 

                        <li class="trvl_local" style="width: 89%;" id="DevelopmentArea_1" runat="server"> 
                            <span>Development Areas of the Reviewee</span>
                            <br />
                            <asp:TextBox AutoComplete="off" placeholder="Maximum 100 characters" ID="txtDevArea" runat="server" MaxLength="150" Height="50" TextMode="MultiLine" onkeyup="ValidateLimit(this,100);"></asp:TextBox>
                        </li>

                         <li class="trvl_local" id="DevelopmentArea_2" runat="server"> 
                            <span>Timelines</span><br />

                             <asp:DropDownList ID="lstTimeLines" runat="server" CssClass="DropdownListSearch" Width="350px"></asp:DropDownList>
                             <br /><br />
                           <div style="display:none">
                             <asp:TextBox AutoComplete="off" ID="txtTimeLines" runat="server" ReadOnly="true" CssClass="grayDropdown"></asp:TextBox>
                            <asp:Panel ID="Panel2" style="display:none" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstTimeLines_1" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstTimeLines_SelectedIndexChanged"></asp:ListBox>
                                                                            </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender1" PopupControlID="Panel2" TargetControlID="txtTimeLines"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>
                               </div>
                        </li>

                        <li class="trvl_local" id="DevelopmentArea_3" runat="server">      
                            <div style="display:none">
                            <span style="display:none">Method</span>
                            <asp:TextBox AutoComplete="off" ID="txtMethod" runat="server" ReadOnly="true" CssClass="grayDropdown"></asp:TextBox>
                            <asp:Panel ID="Panel3" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstMethod" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstMethod_SelectedIndexChanged"></asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender2" PopupControlID="Panel3" TargetControlID="txtMethod"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>

                            </div> 
                        </li>
                        
                        <li class="trvl_local" id="DevelopmentArea_4" runat="server"> 
                            <span>Training Program</span>
                            <!-- Training Code-->
                            <asp:DropDownList ID="lstTraining" runat="server" CssClass="DropdownListSearch" Width="350px"></asp:DropDownList>

                            <div style="display:none">
                            <asp:TextBox AutoComplete="off" ID="txtTraining" runat="server" ReadOnly="true" CssClass="grayDropdown"></asp:TextBox>
                            <asp:Panel ID="Panel4" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstTraining_1" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstTraining_SelectedIndexChanged"></asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender3" PopupControlID="Panel4" TargetControlID="txtTraining"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>
                                </div>
                        </li>
                        <li id="DevelopmentArea_5" runat="server"> 
                            <span> Training Program Name </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtTrainingOther" runat="server" placeholder="Maximum 100 characters"  onkeyup="ValidateLimit(this,100);"></asp:TextBox>

                        </li>


                        <li class="trvl_local" style="width: 89%;" id="liMethodDescription" runat="server">
                            <span style="display:none">Method Description </span> <span style="color: red;display:none" id="Span9" runat="server">*</span><br />
                            <asp:TextBox Visible="false" AutoComplete="off" placeholder="Maximum 100 characters" ID="txtMethodDesc" runat="server" MaxLength="150" Height="50" TextMode="MultiLine" onkeyup="ValidateLimit(this,100);"></asp:TextBox>

                        </li>

                        <div class="mobiletrvl_Savebtndiv" id="DevelopmentArea_6" runat="server">

                            <asp:LinkButton ID="btnSubmit" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClientClick=" return MultiClick();" OnClick="btnSubmit_Click">Add Development Plan</asp:LinkButton>

                            <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" ToolTip="Delete" CssClass="Savebtnsve" Visible="false" OnClick="btnDelete_Click">Delete</asp:LinkButton>
                             <asp:LinkButton ID="btnReset" runat="server"  Text="Reset Development Plan" ToolTip="Reset Development Plan" CssClass="Savebtnsve" OnClick="btnReset_Click">Reset Development Plan</asp:LinkButton>
                             
                        </div>
                        <li id="DevelopmentArea_7" runat="server"></li>
                        <li id="DevelopmentArea_8" runat="server"></li>

                        <li class="mobile_deviation" style="width: 80%" runat="server" id="ligvDevPlan">
                            <div>
                                <asp:GridView ID="gvDevPlan" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%"
                                    DataKeyNames="Assess_id,Assess_Dev_Plan_id" EmptyDataText="Please click to add development plan">
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
                                        <asp:BoundField HeaderText="Development Area"
                                            DataField="Dev_area"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="40%" />

                                        <asp:BoundField HeaderText="Methods"
                                            DataField="Method_desc"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="25%" Visible="false" />

                                        <asp:BoundField HeaderText="Timelines"
                                            DataField="Timelines_desc"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="20%" />

                                        <asp:BoundField HeaderText="Training Program"
                                            DataField="TrainingCodeDesc"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="30%" />

                                        <asp:TemplateField HeaderText="Details">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEditDevPlan" runat="server" Style="text-decoration: underline" Text='View' OnClick="lnkEditDevPlan_Click">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                        <li class="mobile_deviation" style="width: 85%;" runat="server" id="liMethodDesc">
                            <span id="Span7" runat="server">Methods : </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="TextBox1" Enabled="false" ReadOnly="true" Style="border: 0px solid !important; resize: none" runat="server" Height="35" TextMode="MultiLine"></asp:TextBox>
                        </li>
                        <%--   </ul> --%>



                        <%--<span>Reason for Deviation </span>--%>
                        <li class="mobile_deviation" style="width: 89%;" runat="server" id="liRevieweecomments">
                            <br />
                            <asp:LinkButton ID="btnlnkrediss" runat="server" Text="Reviewee's comments" ToolTip="Browse" CssClass="Savebtnsve"></asp:LinkButton>

                        </li>
                        <li class="mobile_deviation" style="width: 100%;" runat="server" id="lichkRevieweeAgree">
                            <br />
                            <asp:CheckBox ID="chkRevieweeAgree" runat="server" AutoPostBack="true" OnCheckedChanged="chkRevieweeAgree_CheckedChanged" /><span id="Span3" visible="true" runat="server" class="checkboxspan">Agree with Reviewer Assessment </span>
                        </li>
                        <li class="mobile_deviation" runat="server" id="liSpan2">
                            <br />
                            <span id="Span2" visible="false" runat="server">Agree with Reviewer Assessment : </span>
                        </li>
                        <li class="mobile_deviation" style="width: 100%;" runat="server" id="lichkRevieweeDisAgree">
                            <br />
                            <asp:CheckBox ID="chkRevieweeDisAgree" runat="server" AutoPostBack="true" OnCheckedChanged="chkRevieweeDisAgree_CheckedChanged" /><span id="Span4" visible="true" runat="server" class="checkboxspan">Disagree with Reviewer Assessment</span>
                        </li>
                        <li class="mobile_deviation" runat="server" id="liSpan6">
                            <br />
                            <span id="Span6" visible="false" runat="server">Agree with Reviewer Assessment : </span>
                        </li>
                        <li class="mobile_deviation" style="width: 89%;" runat="server" id="liSpanRevieweeDis">
                            <br />
                            <span id="SpanRevieweeDis" runat="server">Reviewee's comments, in case of any disagreements : </span>&nbsp;&nbsp;<span style="color: red" id="Span11" runat="server">*</span>
                            <asp:TextBox AutoComplete="off" ID="txtRevieweeDis" placeholder="Maximum 256 characters" Enabled="true" runat="server" Height="100" TextMode="MultiLine" onkeyup="ValidateLimit(this,300);" OnTextChanged="txtRevieweeDis_TextChanged"></asp:TextBox>
                        </li>
                        <li class="claimmob_fromdate" runat="server" id="liSpanDissHeldOn">
                            <br />
                            <span id="SpanDissHeldOn" runat="server">Discussion held on : </span>&nbsp;&nbsp;<span style="color: red" id="SpanDiscussion" runat="server">*</span>
                            <asp:TextBox AutoComplete="off" ID="txtDissHeldOn" runat="server" AutoPostBack="True" OnTextChanged="txtDissHeldOn_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" Format="dd/MM/yyyy" TargetControlID="txtDissHeldOn"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>
                        <li></li>

                        <li class="mobile_deviation" style="width:89%;" runat="server" id="liRecoDissN1">
                            <br />
                            <span id="RecoDissN1" runat="server">Reviewer's comments based on disagreements (Including any changes made to the initial assessment):</span>&nbsp;&nbsp;<span style="color: red" id="Span8" runat="server">*</span>
                            <asp:TextBox AutoComplete="off" ID="txtRecoDissN1" placeholder="Maximum 256 characters" Enabled="true" runat="server" Height="100" TextMode="MultiLine" onkeyup="ValidateLimit(this,300);" OnTextChanged="txtRecoDissN1_TextChanged"></asp:TextBox>
                        </li>
                        <li class="mobile_deviation" style="width: 89%;" runat="server" id="liRecoDissN2">
                            <br />
                            <span id="RecoDissN2" runat="server">Final Reviewer's comments based on disagreements (Including any changes made to the initial assessment):</span>
                            <asp:TextBox AutoComplete="off" ID="txtRecoDissN2" placeholder="Maximum 256 characters" Enabled="true" runat="server" Height="100" TextMode="MultiLine" onkeyup="ValidateLimit(this,300);" OnTextChanged="txtRecoDissN2_TextChanged"></asp:TextBox>
                        </li>
                        <li class="mobile_deviation" style="width: 99%;" runat="server" id="liRecoDissAD1">
                            <br />
                            <span id="RecoDissAD1" runat="server">Additional Reviewer's comments based on disagreements (Including any changes made to the initial assessment):</span>
                            <asp:TextBox AutoComplete="off" ID="txtRecoDissAD1" placeholder="Maximum 256 characters" Enabled="true" runat="server" Height="100" TextMode="MultiLine" onkeyup="ValidateLimit(this,300);" OnTextChanged="txtRecoDissAD1_TextChanged"></asp:TextBox>
                        </li>


                        <li class="mobile_deviation" style="width: 90%" runat="server" id="liRecommendations">
                            <br />
                            <asp:LinkButton ID="btnlnkRecommendation" runat="server" Text="Section 2: Recommendations " ToolTip="Browse" CssClass="Savebtnsve"></asp:LinkButton><%--OnClick="btnlnkRecommendation_Click"--%>
                            <span id="Span1" runat="server">
                                <br />
                            </span>
                        </li> 


                      <li class="trvl_local" style="width: 89%;" runat="server" id="liRecommendations_1" visible="false">
                           <br />
                         <span>A. Overall Performance Rating : <%--[Specify the rating on a (1-6)# scale in the appropriate box]--%></span>&nbsp;&nbsp;<span id="span10" runat="server" style="color:red">*</span>
                     </li> 
                      <li class="trvl_local" runat="server" id="liRecommendations_2" visible="false">  
                            <asp:DropDownList ID="lstPerformance" runat="server" CssClass="DropdownListSearch" Width="350px"></asp:DropDownList>
                          
                          <div style="display:none" >
                            <asp:TextBox AutoComplete="off" ID="txtPerformance" ReadOnly="true" runat="server" CssClass="grayDropdown"></asp:TextBox>
                           <asp:Panel ID="Panel5"  runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstPerformance_1" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstPerformance_SelectedIndexChanged">                                          
                                        </asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                             </asp:Panel>  
                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender4" PopupControlID="Panel5" TargetControlID="txtPerformance"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>  
                          </div>
                     </li>
                      <li runat="server" id="liRecommendations_3" visible="false"></li> 
                        <li class="trvl_local" style="width: 100%;" runat="server" id="liRecommendations_4" visible="false">  
                            <div>
                                <br />
                                <asp:GridView ID="gvPerformanceLegend" runat="server"  BackColor="#f2f2f2" BorderColor="#f2f2f2" BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="90%">
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
                                        <asp:BoundField HeaderText="Rating"
                                            DataField="Rating"
                                            ItemStyle-HorizontalAlign="center"                                            
                                            HeaderStyle-HorizontalAlign="center"
                                            ItemStyle-Width="10%" />
                                        <asp:BoundField HeaderText="Definition"
                                            DataField="descript"
                                            ItemStyle-HorizontalAlign="Left"                                            
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="90%" />                                    
                                    </Columns>
                                </asp:GridView>
                              </div><br />
                            <%-- <span>Legend </span>                            
                            <asp:ListBox ID="lstApprover" runat="server" Height="120"></asp:ListBox>--%>
                        </li>

                      <li class="trvl_local" style="width: 89%;" runat="server" id="liRecommendations_5" visible="false">                       
                            <span>Overall Performance Comments :</span>&nbsp;&nbsp;<span id="span16" runat="server" style="color:red">*</span> <br />
                          <asp:TextBox AutoComplete="off" placeholder="Maximum 500 characters" ID="txtOverallPerformance_Comments" runat="server" MaxLength="150" height="50" TextMode="MultiLine" onkeyup="ValidateLimit(this,500);"></asp:TextBox>
                     </li>   

                     <li class="trvl_local" style="width: 89%;" runat="server" id="liRecommendations_6" visible="false">  
                         <span id="SpanCompeAttribute" runat="server" >B. Overall Personal Attributes Assessment Rating : </span>&nbsp;&nbsp;<span id="span12" runat="server" style="color:red">*</span>
                     </li>
                      <li class="trvl_local" runat="server" id="liRecommendations_7" visible="false">
                          <asp:DropDownList ID="lstattribute" runat="server" CssClass="DropdownListSearch" Width="350px"></asp:DropDownList>

                          <div style="display:none">
                            <asp:TextBox AutoComplete="off" ID="txtattribute" ReadOnly="true" runat="server" CssClass="grayDropdown"></asp:TextBox>
                            <asp:Panel ID="Panel6" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstattribute_1" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstattribute_SelectedIndexChanged">                                          
                                        </asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender5" PopupControlID="Panel6" TargetControlID="txtattribute"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>     
                            </div>
                     </li>
                        <li class="trvl_local" runat="server" id="liRecommendations_8" visible="false"></li>
                        <li class="trvl_local" style="width: 100%;" runat="server" id="liRecommendations_9" visible="false"> 
                            <div>
                                <br />
                                <asp:GridView ID="gvattributeLegend_Reco" runat="server"  BackColor="#f2f2f2" BorderColor="#f2f2f2" BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="90%">
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
                                        <asp:BoundField HeaderText="Rating"
                                            DataField="descript"
                                            ItemStyle-HorizontalAlign="Center"                                            
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" />                                    
                                        <asp:BoundField HeaderText="Definition"
                                            DataField="desc"
                                            ItemStyle-HorizontalAlign="Left"                                            
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="90%" />                                   
                                    </Columns>
                                </asp:GridView>
                              </div><br />
                           
                        </li>

                          <li class="trvl_local" style="width: 89%;" runat="server" id="liRecommendations_10" visible="false">                       
                            <span>Overall Personal Attributes Comments :</span>&nbsp;&nbsp;<span id="span17" runat="server" style="color:red">*</span> <br />
                          <asp:TextBox AutoComplete="off" placeholder="Maximum 500 characters" ID="txtOverallPersonalAttributes_Comments" runat="server" MaxLength="150" height="50" TextMode="MultiLine" onkeyup="ValidateLimit(this,500);"></asp:TextBox>
                        </li>   

                     <li class="trvl_local" style="width: 89%;" runat="server" id="liRecommendations_11" visible="false">  
                         <span>C. Promotion Recommendation : </span>
                     </li>
                     <li class="trvl_local" runat="server" id="liRecommendations_12" visible="false"> 
                         <br /><span>C 1. Post / Grade Promotion :</span> &nbsp;&nbsp;<span id="span13" runat="server" style="color:red">*</span>

                          <asp:DropDownList ID="lstPromotype" runat="server" CssClass="DropdownListSearch" Width="350px"></asp:DropDownList>
                         <div style="display:none">
                          <asp:TextBox AutoComplete="off" ID="txtPromotype" ReadOnly="true" runat="server" CssClass="grayDropdown"></asp:TextBox>
                            <asp:Panel ID="Panel7" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstPromotype_1" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstPromotype_SelectedIndexChanged">                                          
                                        </asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender6" PopupControlID="Panel7" TargetControlID="txtPromotype"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender> 
                            </div>
                     </li>
                     <li class="trvl_local" runat="server" id="liRecommendations_13" visible="false"> 
                         <span>C 2. Promotion Recommendation :</span> &nbsp;&nbsp;<span id="span14" runat="server" style="color:red">*</span>
                          <asp:DropDownList ID="lstPromotion" runat="server" CssClass="DropdownListSearch" Width="350px"></asp:DropDownList>

                         <div style="display:none">

                          <asp:TextBox AutoComplete="off" ID="txtPromotion" ReadOnly="true" runat="server" CssClass="grayDropdown"></asp:TextBox>
                            <asp:Panel ID="Panel8" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstPromotion_1" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstPromotion_SelectedIndexChanged">                                          
                                        </asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender7" PopupControlID="Panel8" TargetControlID="txtPromotion"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>  
                             
                            </div>
                     </li>
                      <li class="trvl_local" style="width: 89%;" runat="server" id="liRecommendations_14" visible="false">     
                          <br />
                            <span>Overall Promotion Recommendation Comments :</span>&nbsp;&nbsp;<span id="span18" runat="server" style="color:red">*</span> <br />
                          <asp:TextBox AutoComplete="off" placeholder="Maximum 500 characters" ID="txtOverallPromotionRec_Comments" runat="server" MaxLength="150" height="50" TextMode="MultiLine" onkeyup="ValidateLimit(this,500);"></asp:TextBox>
                        </li>  
                          
                     <li class="trvl_local" style="width: 89%;" runat="server" id="liRecommendations_15" visible="false">                       
                     <span>Overall Comments :</span>&nbsp;&nbsp;<span id="span15" runat="server" style="color:red">*</span> <br />
                          <asp:TextBox AutoComplete="off" placeholder="Maximum 500 characters" ID="txtOverAllComment_Rec" runat="server" MaxLength="150" height="50" TextMode="MultiLine" onkeyup="ValidateLimit(this,500);"></asp:TextBox>
                     </li>                     
                     
                        <li class="mobile_deviation" style="width: 100%" runat="server" id="ligvRecomm" >
                            <div>
                                <asp:GridView ID="gvRecomm" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%"
                                    DataKeyNames="Assess_id,Reviewer_Type_Desc,Appr_Emp_Code,RecommDate,Reviewer_Type_id" OnRowDataBound="gvRecomm_RowDataBound">
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
                                        <asp:BoundField HeaderText=""
                                            DataField="Reviewer_Type"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="10%" />

                                        <asp:BoundField HeaderText="Name "
                                            DataField="Emp_Name"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left" />

                                        <%-- <asp:BoundField HeaderText="Recommendation Date"
                                            DataField="RecommDate"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="30%" />--%>
                                        <asp:BoundField HeaderText="Overall Performance Rating"
                                            DataField="PerfrateDesc"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="10%" />
                                        <asp:BoundField HeaderText="Overall Personal Attributes Assessment Rating"
                                            DataField="Comp_Rating"
                                            ItemStyle-HorizontalAlign="center"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="15%" />
                                        <asp:BoundField HeaderText="Promotion Type"
                                            DataField="PromoTypeDesc"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="15%" />
                                        <asp:BoundField HeaderText="Promotion Recommendation"
                                            DataField="PromorateDesc"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="15%" />


                                        <asp:TemplateField HeaderText="Details"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="5%">
                                            <ItemTemplate>
                                             >
                                                <asp:LinkButton ID="lnkRecomm" runat="server" Style="text-decoration: underline" Text='View' OnClick="lnkRecomm_Click">
                                                </asp:LinkButton>
                                                  
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                            <br />
                        </li>


                        <li class="mobile_deviation" style="width: 100%;display:inline">
                            <span>Status : </span>
                            <asp:ListBox ID="lstApprover" runat="server" Height="130"></asp:ListBox>
                        </li>
                    
                    </ul>
                </div>
            </div>
        </div>
    </div>
      
    <div class="mobile_Savebtndiv">
         <div>
              <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
         </div>

        <asp:LinkButton ID="btnSave" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClick="btnSave_Click" OnClientClick="return SaveMultiClick();">Submit</asp:LinkButton>
                 
        <asp:LinkButton ID="mobile_cancel" runat="server" Visible="false" Text="Save as Draft" ToolTip="Save as Draft" CssClass="Savebtnsve" OnClick="mobile_cancel_Click">Save as Draft</asp:LinkButton>

        <asp:LinkButton ID="btnBack" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="btnBack_Click">Back</asp:LinkButton> 
                            

       <asp:LinkButton ID="btnPrintPV" runat="server" Text="Reset" Visible="false" ToolTip="Reset Developemnt Plan" CssClass="Savebtnsve" OnClick="mobile_btnPrintPV_Click">View My Appraisal Form</asp:LinkButton>

    </div>
    
    <br />
    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />--%>


     <asp:HiddenField ID="hdnloginEmpCode" runat="server" />
    <asp:HiddenField ID="hdnGrade" runat="server" />
    <asp:HiddenField ID="hdnAppYearTypeid" runat="server" />
    <asp:HiddenField ID="hdnAppYearStartDate" runat="server" />
    <asp:HiddenField ID="hdnAppYearEndDate" runat="server" />
    <asp:HiddenField ID="hdnAppAppType" runat="server" />
    <asp:HiddenField ID="hdnAssessid" runat="server" />
    <asp:HiddenField ID="hdnAssessKRAdtlid" runat="server" />
    <asp:HiddenField ID="hdnDevplandtlid" runat="server" />
    <asp:HiddenField ID="hdnPosMappid" runat="server" />
    <asp:HiddenField ID="hdnPositionID" runat="server" />
    <asp:HiddenField ID="hdnAssessStartDt" runat="server" />
    <asp:HiddenField ID="hdnAssessEndDt" runat="server" />
    <asp:HiddenField ID="hdnsptype" runat="server" />
    <asp:HiddenField ID="mngAssess" runat="server" />
    <asp:HiddenField ID="hdnAssessType" runat="server" />
    <asp:HiddenField ID="hdnKRAtempid" runat="server" />
    <asp:HiddenField ID="hdnTotalPtAlloted" runat="server" />
    <asp:HiddenField ID="hdnTotalPtAchieved" runat="server" />
    <asp:HiddenField ID="hdnTargetTotalPtAchieved" runat="server" />
    <asp:HiddenField ID="hdnRevPointsAlloted" runat="server" />
    <asp:HiddenField ID="hdnRevRevieweeAchieved" runat="server" />

    <asp:HiddenField ID="hdnCompetancyCount" runat="server" />
    <asp:HiddenField ID="hdnRevieweeCompetancyCount" runat="server" />
    <asp:HiddenField ID="hdnReviewerCompetancyCount" runat="server" />
    <asp:HiddenField ID="hdnrevieweeKRADetailCount" runat="server" />
    <asp:HiddenField ID="hdnReviewerKRADetailCount" runat="server" />
    <asp:HiddenField ID="hdnReviewerKRADetailDescCount" runat="server" />
    <asp:HiddenField ID="hdnRevieweeReleasedDate" runat="server" />
    <asp:HiddenField ID="hdnReviewerTypeidRECO" runat="server" />
    <asp:HiddenField ID="hdnApprEmpCode" runat="server" />
    <asp:HiddenField ID="hdnselfassessmentReleaseon" runat="server" />
    <asp:HiddenField ID="hdnToEmailID" runat="server" />
    <asp:HiddenField ID="hdnCCEmailID" runat="server" />

    <asp:HiddenField ID="hdnAssessTypeId" runat="server" />
    <asp:HiddenField ID="hdnReviewerTypeId" runat="server" />
    <asp:HiddenField ID="hdnAssessStatus" runat="server" />
    <asp:HiddenField ID="hdnNextReviewer" runat="server" />
    <asp:HiddenField ID="hdnNextAssessTypeId" runat="server" />
    <asp:HiddenField ID="hdnNextReviewerTypeId" runat="server" />
    <asp:HiddenField ID="hdnNextAssessstatus" runat="server" />
    <asp:HiddenField ID="hflGrade" runat="server" />
    <asp:HiddenField ID="hflEmpDesignation" runat="server" />
    <asp:HiddenField ID="hflEmpDepartment" runat="server" />
    <asp:HiddenField ID="hflEmailAddress" runat="server" />
    <asp:HiddenField ID="hdnEmpNameCurrentSession" runat="server" />

    <asp:HiddenField ID="hdngetrecoCount" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="hdnStrengthid" runat="server" />
    <asp:HiddenField ID="hdnlocationname" runat="server" />
    <asp:HiddenField ID="hdntargetEvalutionFilePath" runat="server" />
    <asp:HiddenField ID="hdnApprovedKRAFilePath" runat="server" />
    <asp:HiddenField ID="hdnApprovedKRAFile" runat="server" />
    <asp:HiddenField ID="hdnPreviousApproverEmails" runat="server" />

    	<script src="../js/dist/select2.min.js"></script>
	<link href="../js/dist/select2.min.css" rel="stylesheet" />



    <script src="../js/freeze/jquery-ui.min.js"></script>
    <script src="../js/freeze/gridviewScroll.min.js"></script>
    
    <script type="text/javascript">

        function singleRbtnSelect(chb) {
            $(chb).closest("table").find("input:radio").prop("checked", false);
            $(chb).prop("checked", true);

        }


        $(document).ready(function () {
            $(".DropdownListSearch").select2();

        });

        //      $(document).ready(function () { 


        //      	$(document).ready(function () { 

        //	$('#MainContent_dgTargetEvaluation').gridviewScroll({
        //		width: 1070,
        //		height: 600,
        //		freezesize: 6, // Freeze Number of Columns.
        //		headerrowcount: 1, //Freeze Number of Rows with Header.
        //	}); 
        //});




        window.onload = function () {
            var textarea = document.getElementById('<%=txtOverallComment.ClientID %>');
            textarea.scrollTop = textarea.scrollHeight;
        }

        function ShowHideDiv(chkRevieweeAgree) {
            var dvPassport = document.getElementById("txtRevieweeDis");
            dvPassport.style.display = chkRevieweeAgree.checked ? "block" : "none";
        }
        function ValidateLimit(obj, maxchar) {
            if (this.id) obj = this;
            var remaningChar = maxchar - obj.value.length;

            if (remaningChar <= 0) {
                obj.value = obj.value.substring(maxchar, 0);

            }
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

        function SaveMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=btnSave.ClientID%>');

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

        <%--function addKRADetailsClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=mobile_btnAddKRA.ClientID%>');

                var KRAT = document.getElementById('<%=txtKRATemplate.ClientID%>');
                try {
                    if (KRAT.value.length < 1)
                    {
                        alert('Select KRA Template');
                        return false;
                    }
                } catch (e) {
                }

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        ConfirmKRA();
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }--%>

        function CancelMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=mobile_cancel.ClientID%>');

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

        function ConfirmKRA() {
            //Testing();
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Add KRA From Template, then Existing Target Evaluation will be deleted?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
                return;
            }
            //alert(confirm_value.value);
            //document.forms[0].appendChild(confirm_value);
            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return;

        }




        function ValidateLimit(obj, maxchar) {
            if (this.id) obj = this;
            var remaningChar = maxchar - obj.value.length;

            if (remaningChar <= 0) {
                obj.value = obj.value.substring(maxchar, 0);

            }
        }

        function SetCntrls(Deviation) {

            if (Deviation == "OTH") {
                document.getElementById("<%=txtTrainingOther.ClientID%>").disabled = false;
                document.getElementById("<%=txtTrainingOther.ClientID%>").style.backgroundColor = "#ffffff"; // backcolor
                document.getElementById("<%=txtTrainingOther.ClientID%>").value = "";

            }
            else {
                //document.getElementById("<%=txtTrainingOther.ClientID%>").setAttribute("readOnly", "true");
                document.getElementById("<%=txtTrainingOther.ClientID%>").disabled = true;
                document.getElementById("<%=txtTrainingOther.ClientID%>").value = document.getElementById("<%=lstTraining.ClientID%>").options[document.getElementById("<%=lstTraining.ClientID%>").selectedIndex].innerHTML;
                document.getElementById("<%=txtTrainingOther.ClientID%>").style.backgroundColor = "#D3D3D3"; // backcolor
            }

            return;
        }

        function SetMethod(Deviation) {

            if (Deviation == "Training") {
                //document.getElementById("<%=txtTrainingOther.ClientID%>").disabled = false;
                document.getElementById("<%=txtTraining.ClientID%>").disabled = false;
                document.getElementById("<%=txtTraining.ClientID%>").style.backgroundColor = "#ffffff"; // backcolor
                //document.getElementById("<%=txtTrainingOther.ClientID%>").style.backgroundColor = "#ffffff"; // backcolor
                document.getElementById("<%=txtMethodDesc.ClientID%>").disabled = true;
                document.getElementById("<%=txtMethodDesc.ClientID%>").value = "";
                document.getElementById("<%=txtMethodDesc.ClientID%>").style.backgroundColor = "#D3D3D3"; // backcolor
                document.getElementById("<%=SpanStrength.ClientID%>").style.display = "none";


            }
            else {
                document.getElementById("<%=txtTraining.ClientID%>").disabled = true;
                document.getElementById("<%=txtTraining.ClientID%>").value = "";
                document.getElementById("<%=txtTraining.ClientID%>").style.backgroundColor = "#D3D3D3"; // backcolor
                document.getElementById("<%=txtTrainingOther.ClientID%>").disabled = true;
                document.getElementById("<%=txtTrainingOther.ClientID%>").value = "";
                document.getElementById("<%=txtTrainingOther.ClientID%>").style.backgroundColor = "#D3D3D3"; // backcolor
                document.getElementById("<%=txtMethodDesc.ClientID%>").disabled = false;
                document.getElementById("<%=txtMethodDesc.ClientID%>").style.backgroundColor = "#ffffff"; // backcolor
                document.getElementById("<%=SpanStrength.ClientID%>").style.display = "inline";

            }

            return;
        }

        function DownloadFile(file) {
            // alert(file);
            var localFilePath = document.getElementById("<%=hdntargetEvalutionFilePath.ClientID%>").value;

            //alert(localFilePath);
            //window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
            //window.open("https://192.168.21.193/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
            window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
        }

        function Download_ApprovedKRAFile() {
            // alert(file);
            var localFilePath = document.getElementById("<%=hdnApprovedKRAFilePath.ClientID%>").value;
            var localFileName = document.getElementById("<%=hdnApprovedKRAFile.ClientID%>").value;

            //alert(localFilePath);
            window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + localFileName);

            // window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + localFileName);

        }

    </script>
</asp:Content>



<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="Appraisal_Recommendation.aspx.cs" Inherits="Appraisal_Recommendation" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
     <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Appraisal_RemRequest.css" type="text/css" media="all" /> 
     <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Appraisal.css" type="text/css" media="all" />
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/localtravel_css.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/ClaimMobile_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Appraisal_RemRequest.css" type="text/css" media="all" /> 
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travelDetails_css.css" type="text/css" media="all" />

    <%--<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />--%> 
    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
             /*background: #dae1ed;*/
           background:#ebebe4;
        }
        .graytextbox {
            border-bottom: 2px solid #cccccc;
            /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/            
            background-color:#ebebe4;
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

                                        <br/>
                                        <font>(Maximum 16 digits)</font>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server" ErrorMessage="Please enter valid Mobile No" ValidationExpression="^(\(?\+?[0-9]*\)?)?[0-9_\- \(\)]*$" CssClass="error_field" ControlToValidate="txtmobile" Display="Dynamic" ValidationGroup="validate"></asp:RegularExpressionValidator><br />
                                        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtmobile" ID="RegularExpressionValidator19" ValidationExpression="^[\s\S]{10,16}$" runat="server" CssClass="error_field" ErrorMessage="Minimum 10 and Maximum 16 characters allowed." ValidationGroup="validate"></asp:RegularExpressionValidator>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtmobile"
                                            ErrorMessage="Mobile No. is mandatory" ToolTip="Mobile No. is mandatory" ValidationGroup="validate"
                                            SetFocusOnError="true" CssClass="error_field" Display="Dynamic"></asp:RequiredFieldValidator>--%>
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
                        <asp:Label ID="lblheading" runat="server" Text="Recommendation"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage"  Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>
                <div>     
                    <span>
                        <%--<a href="Appraisalindex.aspx" class="aaaa">Appraisal Index</a>--%>
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
                     
                     <li class="trvl_local" style="width: 89%;">  
                         <span>A. Overall Performance Rating : <%--[Specify the rating on a (1-6)# scale in the appropriate box]--%></span>&nbsp;&nbsp;<span id="span4" runat="server" style="color:red">*</span>
                     </li>
                      <li class="trvl_local">
                            <asp:TextBox AutoComplete="off" ID="txtPerformance" ReadOnly="true" runat="server" CssClass="grayDropdown"></asp:TextBox>
                            <asp:Panel ID="Panel3" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstPerformance" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstPerformance_SelectedIndexChanged">                                          
                                        </asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender2" PopupControlID="Panel3" TargetControlID="txtPerformance"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>                     
                     </li><li></li>
                        <li class="trvl_local" style="width: 100%;">  
                            <div>
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
                     <li class="trvl_local" style="width: 89%;" runat="server" id="liB">  
                         <span id="SpanCompeAttribute" runat="server" >B. Overall Personal Attributes Assessment Rating : </span>&nbsp;&nbsp;<span id="span5" runat="server" style="color:red">*</span>
                     </li>
                      <li class="trvl_local" runat="server" id="liB1">
                            <asp:TextBox AutoComplete="off" ID="txtattribute" ReadOnly="true" runat="server" CssClass="grayDropdown"></asp:TextBox>
                            <asp:Panel ID="Panel5" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstattribute" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstattribute_SelectedIndexChanged">                                          
                                        </asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender4" PopupControlID="Panel5" TargetControlID="txtattribute"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>                     
                     </li><li class="trvl_local" runat="server" id="liB3"></li>
                        <li class="trvl_local" style="width: 100%;" runat="server" id="liB2"> 
                            <div>
                                <asp:GridView ID="gvattributeLegend" runat="server"  BackColor="#f2f2f2" BorderColor="#f2f2f2" BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="90%">
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

                     <li class="trvl_local" style="width: 89%;">  
                         <span>C. Promotion Recommendation : </span>
                     </li>
                     <li class="trvl_local"> 
                         <br /><span>C 1. Post / Grade Promotion :</span> &nbsp;&nbsp;<span id="span2" runat="server" style="color:red">*</span>
                          <asp:TextBox AutoComplete="off" ID="txtPromotype" ReadOnly="true" runat="server" CssClass="grayDropdown"></asp:TextBox>
                            <asp:Panel ID="Panel2" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstPromotype" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstPromotype_SelectedIndexChanged">                                          
                                        </asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender1" PopupControlID="Panel2" TargetControlID="txtPromotype"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>                       
                     </li>
                     <li class="trvl_local"> 
                         <span>C 2. Promotion Recommendation :</span> &nbsp;&nbsp;<span id="span1" runat="server" style="color:red">*</span>
                          <asp:TextBox AutoComplete="off" ID="txtPromotion" ReadOnly="true" runat="server" CssClass="grayDropdown"></asp:TextBox>
                            <asp:Panel ID="Panel4" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstPromotion" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstPromotion_SelectedIndexChanged">                                          
                                        </asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender3" PopupControlID="Panel4" TargetControlID="txtPromotion"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>                       
                     </li>
                          
                     <li class="trvl_local" style="width: 89%;">                       
                     <span>Overall Comments :</span>&nbsp;&nbsp;<span id="span3" runat="server" style="color:red">*</span> <br />
                          <asp:TextBox AutoComplete="off" placeholder="Maximum 256 characters" ID="txtOverAllComment" runat="server" MaxLength="150" height="50" TextMode="MultiLine" onkeyup="ValidateLimit(this,300);"></asp:TextBox>
                     </li>                     
                    
                        <li class="trvl_local"> 
                          <span id="SpanCreatedby" runat="server">Created By :</span>
                            <asp:TextBox AutoComplete="off" ID="txtcreatedby" runat="server" MaxLength="150" Enabled="false"></asp:TextBox>
                     </li>  
                         <li class="trvl_local"> 
                        <span id="SpanCreatedon" runat="server">Created On :</span>
                              <asp:TextBox AutoComplete="off" ID="txtcreatedon" runat="server" MaxLength="150" Enabled="false"></asp:TextBox>
                     </li>  
                        
                    </ul>
                </div>
            </div>
        </div>

    <div class="mobiletrvl_Savebtndiv">

        <asp:LinkButton ID="btnSubmit" runat="server" Text="Save" ToolTip="Save" CssClass="Savebtnsve" OnClientClick=" return MultiClick();"  OnClick="btnSubmit_Click" >Submit</asp:LinkButton>

        

        <asp:LinkButton ID="btnBack" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick ="btnBack_Click" ></asp:LinkButton>

    </div>
    <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>

    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />

    <asp:HiddenField ID="hdnGrade" runat="server" />
    <asp:HiddenField ID="hdnsptype" runat="server" />
    <asp:HiddenField ID="hdnclaimid" runat="server" />
    <asp:HiddenField ID="hdnremid" runat="server" />
    <asp:HiddenField ID="hdnAppYearTypeid" runat="server" />
    <asp:HiddenField ID="hdnAppYearStartDate" runat="server" />
    <asp:HiddenField ID="hdnAppYearEndDate" runat="server" />
    <asp:HiddenField ID="hdnAppAppType" runat="server" />
    <asp:HiddenField ID="hdnfileid" runat="server" />
    <asp:HiddenField ID="hdnTotalPtAlloted" runat="server" />
    <asp:HiddenField ID="hdnTotalPtAchieved" runat="server" />

    <asp:HiddenField ID="hdnAssessid" runat="server" />
    <asp:HiddenField ID="hdndtlid" runat="server" />
    <asp:HiddenField ID="mngAssess" runat="server" />
    <asp:HiddenField ID="hdnAssessType" runat="server" />
    <asp:HiddenField ID="hdnflg" runat="server" />
        <asp:HiddenField ID="mngAssessOld" runat="server" />

    <script type="text/javascript">
        function textboxMultilineMaxNumber(txt, maxLen) {
            try {
                if (txt.value.length > (maxLen - 1)) return false;
            } catch (e) {
            }
        }

        function ValidateLimit(obj, maxchar) {
            if (this.id) obj = this;
            var remaningChar = maxchar - obj.value.length;

            if (remaningChar <= 0) {
                obj.value = obj.value.substring(maxchar, 0);

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

        function MultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=btnSubmit.ClientID%>');

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

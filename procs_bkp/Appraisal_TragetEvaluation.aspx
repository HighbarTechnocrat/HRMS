<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="Appraisal_TragetEvaluation.aspx.cs" Inherits="Appraisal_TragetEvaluation" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Appraisal.css" type="text/css" media="all" />
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/localtravel_css.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/ClaimMobile_css.css" type="text/css" media="all" />

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
                        <asp:Label ID="lblheading" runat="server" Text="Section 1.A: Target Evaluation Sheet"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage"  Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>
              <%--  <div>     
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
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" ></asp:LinkButton>
                        </div>
                    </div>


                    <ul id="editform" runat="server" visible="false">
                    <li class="trvl_local" style="width: 89%;">                       
                     <span>Agreed Targets</span> <br /><br />
                      <%--     , if required <asp:TextBox AutoComplete="off" ID="txtKRA" runat="server" MaxLength="150" height="50" TextMode="MultiLine"></asp:TextBox>--%>
                     </li>

                     <li class="trvl_local" style="width: 89%;">                       
                         <span>Description</span>&nbsp;&nbsp;<span style="color:red">*</span> <br />
                          <br/><asp:TextBox placeholder="Maximum 500 characters, please attach additional sheet" AutoComplete="off" ID="txtDescription" runat="server" height="100" TextMode="MultiLine"  onkeyup="ValidateLimit(this,500);" ></asp:TextBox>
                     </li>
                    <%-- <li class="trvl_local" style="width: 89%;">                       
                         <span>Key Performance Indicator</span>&nbsp;&nbsp;<span style="color:red">*</span> <br />
                          <asp:TextBox AutoComplete="off" ID="txtKPI" runat="server"  MaxLength="150" height="50" TextMode="MultiLine"></asp:TextBox>
                     </li>--%>
                  
                    <%-- <li class="trvl_local"> 
                         <span>Target Unit</span> &nbsp;&nbsp;<span style="color:red">*</span>
                          <asp:TextBox AutoComplete="off" ID="txtUnit" runat="server" CssClass="grayDropdown"></asp:TextBox>
                            <asp:Panel ID="Panel3" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstUnit" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstUnit_SelectedIndexChanged">                                          
                                        </asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender2" PopupControlID="Panel3" TargetControlID="txtUnit"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>                     
                     </li>--%>
                     <%--<li class="trvl_local"> 
                         <span>Target Quantity</span> 
                          <asp:TextBox AutoComplete="off" ID="txtQuantity" runat="server" CssClass="grayDropdown"></asp:TextBox>
                            <asp:Panel ID="Panel2" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstQuantity" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstQuantity_SelectedIndexChanged">                                          
                                        </asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender1" PopupControlID="Panel2" TargetControlID="txtQuantity"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>                       
                     </li>--%>
                     <li class="trvl_local">
                        <span>Points Allotted</span>&nbsp;&nbsp;<span style="color:red">*</span>
                         <asp:TextBox AutoComplete="off" ID="txtpointsAlloted" runat="server" MaxLength="5" ></asp:TextBox>
                     </li>
                    <li class="trvl_local">
                        <span id="Span6" runat="server">Revised : Points Allotted</span>
                         <asp:TextBox AutoComplete="off" ID="txtRevisedpointsAlloted" runat="server" MaxLength="5" ></asp:TextBox>
                     </li>

<%--                     <li class="claimmob_fromdate">
                           <span id="SpanDissHeldOn" runat="server">Date : </span>
                            <asp:TextBox AutoComplete="off" ID="txtDissHeldOn" runat="server" AutoPostBack="True" OnTextChanged="txtDissHeldOn_TextChanged" ></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" Format="dd/MM/yyyy" TargetControlID="txtDissHeldOn"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>--%>
                     <li class="trvl_local" style="width: 89%;" runat="server" id="lireviewee1">                       
                     <span>Reviewee : Targets Achieved</span> <br /><br />
                     
                     </li>
                    <li class="trvl_local" style="width: 89%;" runat="server" id="lireviewee2">
                        <%--<span>Reason For Deviation</span>--%>
                         <span>Description</span>&nbsp;&nbsp;<span style="color:red">*</span><br />
                        <br />  <asp:TextBox placeholder="Maximum 500 characters, please attach additional sheet, if required" AutoComplete="off" ID="txtTargetAchDescription" runat="server"  height="100" TextMode="MultiLine" onkeyup="ValidateLimit(this,500);"></asp:TextBox>
                     </li>
                   
                     <li class="trvl_local" runat="server" id="lireviewee3">
                        <span>Points Achieved</span>&nbsp;&nbsp;<span style="color:red">*</span>
                         <asp:TextBox AutoComplete="off" ID="txtTargetAchPointAlloted" runat="server" MaxLength="5" ></asp:TextBox>
                     </li> <li class="trvl_local">
                        <span id="Span7" runat="server"><%--Revised : Points Achieved--%></span>
                         <asp:TextBox AutoComplete="off" ID="txtRevisedPointAchieved" runat="server" MaxLength="5" ></asp:TextBox>
                     </li>
                     <li class="trvl_local"  style="width: 89%;" runat="server" id="lireviewee4">
                         <%--, if required --%>
                        <span runat="server" id="spanuploadfile" >Upload File</span> &nbsp;&nbsp;<span style="color:red">*</span><br />
                        <span runat="server" id="spanuploadfiletext" style="background-color:#f2f2f2;font-size:10px">Attach additional sheet.<br /> Maximum 3 files in PDF, Word or Excel format can be uploaded. The total files size should not exceed 10 MB.<br />
                            </span><br />
                         <asp:FileUpload ID="ploadexpfile" runat="server" AllowMultiple="true" accept=".pdf,.xls,.xlsx,.doc,.docx" Width="50%"></asp:FileUpload>
                          <asp:GridView  ID="gvuploadedFiles" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="60%" EditRowStyle-Wrap="false"
                                   DataKeyNames="File_Id,File_Dtl_Id"  OnRowDataBound="gvuploadedFiles_RowDataBound" EmptyDataText="No File Added">
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
                                      <%--  <asp:BoundField HeaderText="Uploaded Files"
                                            DataField="filename"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="30%" />--%>
                                        <asp:TemplateField HeaderText="Uploaded Files">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkviewfile" runat="server"  Text='<%# Eval("file_name") %>'  OnClick="lnkviewfile_Click">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDeleteexpFile" runat="server" Text="Delete" OnClick="lnkDeleteexpFile_Click">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                        </asp:GridView>
                      </li>
                      <li class="trvl_local" style="width: 89%;">                       
                     <br /><span id="span5" runat="server">Reviewer : Targets Achieved</span> <br />
                     
                     </li>
                      <li class="trvl_local" style="width: 89%;">
                        <%--<span>Reason For Deviation</span>--%>
                         <span id="span1" runat="server"><br />Description</span >&nbsp;&nbsp;<span id="span3" runat="server" style="color:red">*</span>
                        <br /> <br /> <asp:TextBox placeholder="Maximum 256 characters" AutoComplete="off" ID="txtTargetAchReviewer" runat="server"  height="100" TextMode="MultiLine" onkeyup="ValidateLimit(this,300);"></asp:TextBox>
                      </li>
                   
                     <li class="trvl_local" >
                        <span id="span2" runat="server">Points Achieved</span  >&nbsp;&nbsp;<span id="span4" runat="server" style="color:red">*</span>
                         <asp:TextBox AutoComplete="off" ID="txtTargetAchPointAllotedReviewer" runat="server" MaxLength="3" ></asp:TextBox>
                     </li><li>                         
                    </li>
                    </ul>
                </div>
            </div>
        </div>

    <div class="mobiletrvl_Savebtndiv">

        <asp:LinkButton ID="btnSubmit" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClientClick=" return MultiClick();"  OnClick="btnSubmit_Click" >Submit</asp:LinkButton>

        <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" ToolTip="Delete" CssClass="Savebtnsve" OnClick="btnDelete_Click">Delete</asp:LinkButton>

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
    <asp:HiddenField ID="hdnTargetTotalPtAchieved" runat="server" />
    <asp:HiddenField ID="hdnRevPointsAlloted" runat="server" />
    <asp:HiddenField ID="hdnRevRevieweeAchieved" runat="server" />

    <asp:HiddenField ID="hdnAssessid" runat="server" />
    <asp:HiddenField ID="hdnAssessKRAdtlid" runat="server" />
    <asp:HiddenField ID="mngAssess" runat="server" />
         <asp:HiddenField ID="hdnUnit" runat="server" />

    <script type="text/javascript">
       <%-- function SetCntrls(Deviation) {

            if (Deviation == "Dates") {
                document.getElementById("<%=txtDissHeldOn.ClientID%>").disabled = false;
                document.getElementById("<%=txtQuantity.ClientID%>").disabled = true;
               // document.getElementById("<%=txtQuantity.ClientID%>").value = "";     
               // document.getElementById("<%=txtDissHeldOn.ClientID%>").style.backgroundColor = "#ffffff"; // backcolor
            }
            if (Deviation == "Numbers") {
                document.getElementById("<%=txtQuantity.ClientID%>").disabled = false;
                document.getElementById("<%=txtDissHeldOn.ClientID%>").disabled = true;
                //document.getElementById("<%=txtDissHeldOn.ClientID%>").value = "";       
                //document.getElementById("<%=txtQuantity.ClientID%>").style.backgroundColor = "#ffffff"; // backcolor
            }
            return;
        }--%>

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

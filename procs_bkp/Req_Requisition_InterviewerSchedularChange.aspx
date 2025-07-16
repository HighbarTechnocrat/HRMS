<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" 
    MaintainScrollPositionOnPostback="true" CodeFile="Req_Requisition_InterviewerSchedularChange.aspx.cs" Inherits="procs_Req_Requisition_InterviewerSchedularChange" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Req_Requisition.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />

    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
            /*background: #dae1ed;*/
            background: #ebebe4;
        }

        .taskparentclasskkk {
            width: 29.5%;
            height: 72px;
            /*overflow:initial;*/
        }

        #MainContent_lstApprover, #MainContent_lstIntermediate {
            padding: 0 0 33px 0 !important;
            /*overflow: unset;*/
        }

        .Dropdown {
            border-bottom: 2px solid #cccccc;
            /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
            background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;
            cursor: default;
        }

        #MainContent_lnkuplodedfile:link {
            color: red;
            background-color: transparent;
            text-decoration: none;
            font-size: 14px;
        }

        #MainContent_lnkuplodedfile:visited {
            color: red;
            background-color: transparent;
            text-decoration: none;
        }

        #MainContent_lnkuplodedfile:hover {
            color: green;
            background-color: transparent;
            text-decoration: none !important;
        }
         iframe1 {
            pointer-events: none !important;           
            /*//opacity: 0.8 !important;*/       
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
  <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/HtmlControl/jquery-1.3.2.js"></script>
    <script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="../js/HtmlControl/jHtmlArea-0.8.min.js"></script>
    <link href="../js/HtmlControl/jHtmlArea.css" rel="stylesheet" />
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>

   

    <script type="text/javascript">
        // $(function() {
        $(document).ready(function () {
            $("#MainContent_txtJobDescription").htmlarea(); // Initialize jHtmlArea's with all default values           
            //window.setTimeout(function() { $("form").submit(); }, 3000);                      
        });
    </script>
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
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="New Recruitment Requisition"></asp:Label>
                    </span>

                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>
                <div>
                    <span>
                        <a href="Requisition_Index.aspx" class="aaaa">Recruitment Home</a>
                    </span>
					<span>
                       <a href="Req_InterviewerSchedularChangeIndex.aspx?itype=APP" title="Back" runat="server"  id="btnRecBack" style="margin-right: 10px;" class="aaaa">Back</a>
                    </span>
                </div>

                <div class="edit-contact">

                    <ul id="editform" runat="server">

                        <%--<li class="trvL_detail" id="litrvldetail" runat="server">
                            <asp:LinkButton ID="btnTra_Details" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" OnClick="btnTra_Details_Click"></asp:LinkButton>
                            <span id="spntrvldtls" runat="server">Requisition Details</span>
                        </li>--%>
                        <%--<li></li>
                        <li></li>--%>
                        <div id="DivTrvl" class="edit-contact" runat="server" visible="true">
                            <ul>
                                <li class="trvl_date">
                                    <span>Requisition Number  </span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtReqNumber" runat="server" AutoPostBack="True" Enabled="false"></asp:TextBox>
                                </li>

                                <li class="trvl_date">
                                    <span>Requisition Date </span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtFromdate" runat="server" AutoPostBack="True" Enabled="false"></asp:TextBox>
                                </li>

                                <li class="trvl_date">
                                    <span>Name </span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtReqName" runat="server" Enabled="false"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span>Department </span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtReqDept" runat="server" Enabled="false"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span>Designation </span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtReqDesig" runat="server" Enabled="false"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span>Email </span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtReqEmail" runat="server" Enabled="false"></asp:TextBox>
                                </li>
                                <li class="trvl_date Req_Position">

                                    <span>Position Title</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <asp:DropDownList runat="server" ID="lstPositionName" AutoPostBack="true" OnSelectedIndexChanged="lstPositionName_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <br />


                                </li>


                                <li class="trvl_date Req_Position">
                                    <span>Position Criticality</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                    <asp:DropDownList runat="server" ID="lstPositionCriti">
                                    </asp:DropDownList>
                                    <br />
                                </li>
                                <li class="trvl_date Req_Position">

                                    <span>Main Skillset </span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <asp:DropDownList runat="server" ID="lstSkillset" AutoPostBack="true" OnSelectedIndexChanged="lstSkillset_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <br />

                                </li>


                                <li class="trvl_date Req_Position">
                                    <span>Department Name</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                    <asp:DropDownList runat="server" ID="lstPositionDept">
                                    </asp:DropDownList>
                                    <br />
                                </li>



                               
                                <li class="trvl_date Req_Position">
                                    <span>Position Location</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                    <asp:DropDownList runat="server" ID="lstPositionLoca">
                                    </asp:DropDownList>
                                    <br />
                                </li>

                                 <li class="trvl_date Req_Position">
                                     <span style="display:none">
                                    <span>Position Designation</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                    <asp:DropDownList runat="server" ID="lstPositionDesign">
                                    </asp:DropDownList>
                                    <br />
                                         </span>
                                </li>

                                <li class="trvl_date" style="display:none">
                                    <span>Other Department</span>&nbsp;&nbsp;<span style="color: red"></span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtOtherDept" runat="server"></asp:TextBox>
                                    <br />
                                </li>
                                <li class="trvl_date" style="display:none">
                                    <span>Position Designation Other</span>&nbsp;&nbsp;<span style="color: red"></span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtPositionDesig" runat="server"></asp:TextBox>
                                </li>

                                <li class="trvl_date">
                                    <span>No Of Position</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtNoofPosition" runat="server" MaxLength="2"></asp:TextBox>
                                </li>



                                <li class="trvl_date">
                                    <span>Additional Skillset</span>&nbsp;&nbsp;<span style="color: red"> </span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtAdditionSkill" runat="server"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span>To Be Filled In(Days)</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txttofilledIn" runat="server" MaxLength="3"></asp:TextBox>
                                </li>
                                <li class="trvl_date Req_Salary" style="display: none">
                                    <div style="display: none">
                                        <span>Salary Range(Lakh/Year)</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                        <asp:TextBox AutoComplete="off" ID="txtSalaryRangeFrom" runat="server" MaxLength="4"></asp:TextBox>
                                        &nbsp;  To  &nbsp;
                            <asp:TextBox AutoComplete="off" ID="txtSalaryRangeTo" runat="server" MaxLength="5"></asp:TextBox>
                                    </div>
                                </li>

                                <li class="trvl_date Req_Position">
                                    <span>Reason For Requisition</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                    <asp:DropDownList runat="server" ID="lstReasonForRequi">
                                    </asp:DropDownList>
                                </li>
                                <li class="trvl_date Req_Position">

                                    <span>Preferred Employment Type</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                    <asp:DropDownList runat="server" ID="lstPreferredEmpType">
                                    </asp:DropDownList>
                                </li>
                                <li class="trvl_date Req_Position">
                                    <span>Band</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                    <asp:DropDownList runat="server" ID="lstPositionBand">
                                    </asp:DropDownList>
                                </li>
                                <li class="trvl_date Req_Salary">
                                    <span>Required Experience(Year)</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtRequiredExperiencefrom" runat="server" MaxLength="4"></asp:TextBox>
                                    &nbsp; To &nbsp;
                            <asp:TextBox AutoComplete="off" ID="txtRequiredExperienceto" runat="server" MaxLength="4"></asp:TextBox>

                                </li>

                            </ul>
                        </div>

                        <li class="trvl_Accomodation" id="litrvlaccomodation" runat="server" visible="false">
                            <asp:LinkButton ID="trvl_accmo_btn" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" OnClick="LinkButton1_Click"></asp:LinkButton>
                            <span id="spnaccomodation" runat="server">Position Details </span>
                        </li>



                        <li class="trvl_local">
                            <br />
                            <asp:LinkButton ID="trvl_localbtn" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" OnClick="trvl_localbtn_Click"></asp:LinkButton>
                            <span id="spnlocalTrvl" runat="server">Other Details </span>
                        </li>
                        <li></li>
                        <li></li>
                        <div id="Div_Locl" class="edit-contact" runat="server" visible="false">
                            <ul>


                                <li class="Req_Requi_Esse">
                                    <span>Essential Qualification</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtEssentialQualifi" runat="server" Rows="7" TextMode="MultiLine"></asp:TextBox>
                                </li>
                                <li class="Req_Requi_Esse">
                                    <span>Desired Qualification</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtDesiredQualifi" runat="server" Rows="7" TextMode="MultiLine"></asp:TextBox>
                                </li>

                                <li class="trvl_date">
                                    <span style="display:none">
                                    <span>Recommended Person</span>&nbsp;&nbsp;<span style="color: red"></span><br />
                                    <%--<asp:TextBox AutoComplete="off" ID="txtRecommPerson" runat="server"></asp:TextBox>--%>
                                     <asp:DropDownList runat="server" ID="lstRecommPerson"> </asp:DropDownList>
                                    <br /> <br />
                                        </span>
                                </li>
                                <li></li>
                                <li></li>
                                <li class="Req_Job_Desc">
                                    <span>Job Description</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtJobDescription" runat="server" Rows="7" TextMode="MultiLine"></asp:TextBox>
                                </li>
                                <li></li>
                                <li></li>
                                <li></li>

                                <li>
                                    <asp:LinkButton ID="localtrvl_delete_btn" runat="server" Text="Questionnaire" ToolTip="Get JD From Bank" CssClass="Savebtnsve"> Get JD From Bank  </asp:LinkButton>
                                    <%--PostBackUrl="~/procs/Req_JD_Bank_Search.aspx"--%>
                                </li>
                                <li></li>
                                <li class="trvl_date">
                                    <span>Assign Questionnaire</span>&nbsp;&nbsp;<span style="color: red"></span>
                                    <br />
                                    <br />
                                </li>
                                <%--<li class="trvl_date">
                                    <asp:LinkButton ID="accmo_cancel_btn" runat="server" Text="Questionnaire" ToolTip="Select Questionnaire" CssClass="Savebtnsve"> Select Questionnaire  </asp:LinkButton>
                                    <br />
                                   
                                </li>--%>
                                <li>

                                    <asp:LinkButton ID="lnkuplodedfile" runat="server" OnClientClick="DownloadFile1()"></asp:LinkButton>
                                    <br />
                                    <br />
                                </li>

                                <li class="Req_Requi_Cmt" id="lsttrvlapprover" runat="server">
                                    <span>Comments</span>&nbsp;&nbsp;<span style="color: red"></span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtComments" runat="server" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                </li>
                                <li class="trvl_date Req_Position">
                                    <span>Screening By</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                    <asp:DropDownList runat="server" ID="lstInterviewerOne">
                                    </asp:DropDownList></li>

                                <div style="display: none">
                                    <li class="trvl_date Req_Position">
                                        <span>Interviewer (2st Level)</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                        <asp:DropDownList runat="server" ID="lstInterviewerTwo">
                                        </asp:DropDownList>

                                    </li>
                                    <li></li>
                                    <li class="trvl_date">
                                        <span>Interviewer (1st Level)</span>&nbsp;&nbsp;<span style="color: red"></span><br />
                                        <asp:TextBox AutoComplete="off" ID="txtInterviewerOptOne" runat="server"></asp:TextBox>
                                    </li>

                                    <li class="trvl_date">
                                        <span>Interviewer (2st Level)</span>&nbsp;&nbsp;<span style="color: red"></span><br />
                                        <asp:TextBox AutoComplete="off" ID="txtInterviewerOptTwo" runat="server"></asp:TextBox>
                                    </li>
                                </div>
                            </ul>
                        </div>
                        <li class="trvl_local" style="display: none">
                            <br />
                            <asp:LinkButton ID="lnkbtn_expdtls" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" OnClick="lnkbtn_expdtls_Click"></asp:LinkButton>
                            <span id="spnexpdtls" runat="server">Approver Details&nbsp;&nbsp; </span>
                        </li>
                        <li></li>
                        <li></li>
                        <div id="Div_Oth" class="edit-contact" runat="server" visible="true">
                            <ul>
                                <li class="trvl_date Req_Position" id="Recruiter" runat="server" visible="true">
                                    <span>Interviewer Schedular</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                    <asp:DropDownList runat="server" ID="listRecruiter">
                                    </asp:DropDownList></li>

                                 <li class="trvl_date Req_Position" id="Li2" runat="server" visible="true">
                                    <span>Interviewer Schedular Change</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                    <asp:DropDownList runat="server" ID="listRecruiterChange">
                                    </asp:DropDownList></li>

                                <li class="Req_Requi_Cmt" id="Li1" runat="server" style="display: none">
                                    <span>Approver Comments</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtApprovercmt" runat="server" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                </li>
                            </ul>

                        </div>
                        <div>
                            <br />

                            <asp:GridView ID="DgvApprover" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="75%">
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
                                    <asp:BoundField HeaderText="Approver Name"
                                        DataField="tName"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="left"
                                        ItemStyle-Width="25%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Status"
                                        DataField="Status"
                                        ItemStyle-HorizontalAlign="left"
                                        ItemStyle-Width="12%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Approved on"
                                        DataField="tdate"
                                        ItemStyle-HorizontalAlign="left"
                                        ItemStyle-Width="12%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Approver Remarks"
                                        DataField="Comment"
                                        ItemStyle-HorizontalAlign="left"
                                        ItemStyle-Width="46%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="APPR_ID"
                                        DataField="APPR_ID"
                                        ItemStyle-HorizontalAlign="center"
                                        ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy"
                                        Visible="false" />

                                    <asp:BoundField HeaderText="Emp_Name"
                                        DataField="Emp_Name"
                                        ItemStyle-HorizontalAlign="center"
                                        ItemStyle-Width="1%"
                                        ItemStyle-BorderColor="Navy"
                                        Visible="false" />

                                    <asp:BoundField HeaderText="Emp_Emailaddress"
                                        DataField="Emp_Emailaddress"
                                        ItemStyle-HorizontalAlign="center"
                                        ItemStyle-Width="1%"
                                        ItemStyle-BorderColor="Navy"
                                        Visible="false" />

                                    <asp:BoundField HeaderText="A_EMP_CODE"
                                        DataField="A_EMP_CODE"
                                        ItemStyle-HorizontalAlign="center"
                                        ItemStyle-Width="1%"
                                        ItemStyle-BorderColor="Navy"
                                        Visible="false" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ul>
                </div>
            </div>
        </div>

    </div>

     <br />
     <br />
    <br />
    <div class="Req_Savebtndiv" style="margin-left:150px;">
       <%-- <asp:LinkButton ID="accmo_delete_btn" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/Req_RequisitionIndex.aspx?itype=APP">  Back  </asp:LinkButton>
       --%>
         <asp:LinkButton ID="mobile_btnSave" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve"  OnClick="mobile_btnSave_Click"  OnClientClick="return SubmitMultiClick();">Submit</asp:LinkButton>
       
        <asp:LinkButton ID="mobile_btnBack" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/Req_InterviewerSchedularChangeIndex.aspx?itype=APP" >Back</asp:LinkButton>
           
        </div>

    <br />
    <br />


    <asp:HiddenField ID="hflEmpDesignation" runat="server" />
    <asp:HiddenField ID="hflEmpDepartment" runat="server" />
    <asp:HiddenField ID="hflEmailAddress" runat="server" />
    <asp:HiddenField ID="hdnRecruitment_ReqID" runat="server" />
    <asp:HiddenField ID="hdnEmpCpde" runat="server" />
    <asp:HiddenField ID="hdnEmpCodePrve" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="hdnBankDetailID" runat="server" />
    <asp:HiddenField ID="hdnAssignQuestiID" runat="server" />
    <asp:HiddenField ID="hdnFilter" runat="server" />
    <asp:HiddenField ID="FilePath" runat="server" />
    <asp:HiddenField ID="FileName" runat="server" />
    <asp:HiddenField ID="hdnCurrentID" runat="server" />
    <asp:HiddenField ID="hflapprcode" runat="server" />
    <asp:HiddenField ID="hdnLoginUserName" runat="server" />
    <asp:HiddenField ID="hdnLoginEmpEmail" runat="server" />
    <asp:HiddenField ID="hdnnextappcode" runat="server" />
    <asp:HiddenField ID="hdnapprid" runat="server" />
    <asp:HiddenField ID="hflApproverEmail" runat="server" />
    <asp:HiddenField ID="hdnIntermediateEmail" runat="server" />
    <asp:HiddenField ID="hdnstaus" runat="server" />
    <asp:HiddenField ID="hdnPreviousApprMails" runat="server" />
    <asp:HiddenField ID="hdnisApprover_TDCOS" runat="server" />
    <asp:HiddenField ID="hdnhrappType" runat="server" />
    <asp:HiddenField ID="hdnApproverid_LWPPLEmail" runat="server" />
    <asp:HiddenField ID="hdnApproverTDCOS_status" runat="server" />
    <asp:HiddenField ID="hdnHRDept" runat="server" />
    <asp:HiddenField ID="hdnHOD" runat="server" />
    <asp:HiddenField ID="hflStatusID" runat="server" />
    <asp:HiddenField ID="hdnExtraAPP" runat="server" />

     <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" /> 

     <script type="text/javascript">      
        $(document).ready(function () {
            $("#MainContent_listRecruiter").select2();
             $("#MainContent_listRecruiterChange").select2();
        });
    </script>

    <script type="text/javascript">

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
                var ele = document.getElementById('<%=trvl_accmo_btn.ClientID%>');

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

        function SubmitMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=mobile_btnSave.ClientID%>');

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



        function DownloadFile(FileName) {
            var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;

            window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
            //window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
        }
        function DownloadFile1() {
            var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;
            var FileName = document.getElementById("<%=FileName.ClientID%>").value;

            window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
            //window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
        }


    </script>
</asp:Content>


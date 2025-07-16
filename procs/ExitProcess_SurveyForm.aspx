<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="ExitProcess_SurveyForm.aspx.cs" Inherits="procs_ExitProcess_SurveyForm" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/ClaimMobile_css.css" type="text/css" media="all" />
    <style>
        form label, #buddypress .standard-form label, #buddypress .standard-form span.label {
            font-weight: 300 !important;
            text-transform: none !important;
        }
        /*.myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }*/


        /*.Dropdown {
            border-bottom: 2px solid #cccccc;*/
        /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
        /*background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;
            cursor: default;
        }

        .grayDropdown {
            border-bottom: 2px solid #cccccc;*/
        /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
        /*background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;
            background-color: #ebebe4;
        }

        .grayDropdownTxt {
            background-color: #ebebe4;
        }

        .taskparentclass2 {
            width: 29.5%;
            height: 112px;
        }

        .taskparentclasskkk {
            width: 29.5%;
            height: 72px;*/
        /*overflow:initial;*/
        /*}

        #MainContent_ListBox1, #MainContent_ListBox2 {
            padding: 0 0 0% 0 !important;*/
        /*overflow: unset;*/
        /*}

        #MainContent_lstApprover, #MainContent_lstIntermediate {
            padding: 0 0 33px 0 !important;*/

        /*overflow: unset;*/
        /*}*/

        #cssTable td {
            text-align: center;
            vertical-align: middle;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
    <script type="text/javascript">
        var deprt;
        $(document).ready(function () {
            <%--if ($.trim($('#<%=rdbSiteShape.ClientID %> input:checked').val()) == '') {
                alert('Site Shape is Required');
            }--%>
        });

        function ValidateModuleList12(source, args) {
            var chkListModules = document.getElementById('<%= checklst12.ClientID %>');
            var chkListinputs = chkListModules.getElementsByTagName("input");
            for (var i = 0; i < chkListinputs.length; i++) {
                if (chkListinputs[i].checked) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }
        function ValidateModuleList13(source, args) {
            var chkListModules = document.getElementById('<%= checklst13.ClientID %>');
            var chkListinputs = chkListModules.getElementsByTagName("input");
            for (var i = 0; i < chkListinputs.length; i++) {
                if (chkListinputs[i].checked) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }

        function submitform() {
            if (!Page_IsValid) {
                return false;
            }
            return confirm_meth();
        }

        function confirm_meth() {
            if (confirm("Do you want to submit data?") == true) {
                $("#mobile_btnSave").click();
            }
            else {

            }
        }
        function Confirm() {
            var confirm_value = document.createElement("INPUT");

            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";

            if (confirm("Do you want to save data?")) {
                confirm_value.value = "Yes";
            }
            else {
                confirm_value.value = "No";
            }

            document.forms[0].appendChild(confirm_value);
        }

    </script>

    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">

                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Exit Survey Form"></asp:Label>
                    </span>
                </div>
                <div class="edit-contact">
                    <a href="ExitProcess_Index.aspx" class="aaa">Exit Process Menu</a>
                    <%--<asp:Panel ID="pnlSurvey" runat="server">
                        </asp:Panel>--%>
                    <ul id="CreateExitSurveyform" runat="server" visible="true">
                        <li>
                            <asp:Label runat="server" ID="lblmessage" ClientIDMode="Static" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label><br />
                            <asp:Label runat="server" ID="lblmsg2" ClientIDMode="Static" Visible="True" Text="All fields are mandatory" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label><br />
                            <%--<asp:TextBox runat="server" ID="lblmessage1" Visible="False" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;" OnTextChanged="lblmessage1_TextChanged"></asp:TextBox>--%>
                        </li>
                        <br />
                        <li><b>Employee Full name</b>
                            <asp:TextBox ID="txtfullName" runat="server" Enabled="false"></asp:TextBox>
                        </li>

                        <li><b>Employee Code /Emp ID</b>
                            <asp:TextBox ID="txtempCode" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li><b>Current Designation</b>
                            <asp:TextBox ID="txtDesignation" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li><b>Band</b>
                            <asp:TextBox ID="txtBand" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li><b>Department</b>
                            <asp:TextBox ID="txtDept" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li><b>Full name of your immediate supervisor/reporting manager</b>
                            <asp:TextBox ID="txtRMName" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li><b>Employee Mobile number</b>
                            <asp:TextBox ID="txtMobileNo" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <br />
                        <li style="width: 650px;"><b>1. Enter full address for future correspondence (include building no., street name, city, district, state & pin code)</b>&nbsp;<span style="color: red">*</span><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator29" ErrorMessage="This field is mandatory"
                                ControlToValidate="txtAddress" runat="server" ForeColor="Red" Display="Dynamic" />
                            <asp:TextBox ID="txtAddress" AutoComplete="off" AutoCompleteType="Disabled" runat="server" ClientIDMode="Static" TextMode="MultiLine" MaxLength="500"></asp:TextBox>
                        </li>
                        <br />
                        <br />
                        <li style="width: 700px;"><b>2. Primary Reasons for leaving Highbar. Please tick all applicable boxes</b>&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:CustomValidator runat="server" ID="cvmodulelist"
                                ClientValidationFunction="ValidateModuleList12"
                                ErrorMessage="Please Select at least one reason" ForeColor="Red"></asp:CustomValidator>
                            <asp:CheckBoxList ID="checklst12" runat="server" RepeatDirection="Vertical">
                                <asp:ListItem Text="Higher studies" Value="1" />
                                <asp:ListItem Text="Family problems" Value="2" />
                                <asp:ListItem Text="Personal medical reasons" Value="3" />
                                <asp:ListItem Text="Working conditions" Value="4" />
                                <asp:ListItem Text="Compensation related issues" Value="5" />
                                <asp:ListItem Text="Unethical conduct at workplace" Value="6" />
                                <asp:ListItem Text="Better opportunity (IT company)" Value="7" />
                                <asp:ListItem Text="Better opportunity (Non IT company)" Value="8" />
                                <asp:ListItem Text="Lack of recognition" Value="9" />
                                <asp:ListItem Text="Problems with colleagues/Dissatisfaction with senior" Value="10" />
                                <asp:ListItem Text="Unsatisfactory job profile" Value="11" />
                                <asp:ListItem Text="Appraisal/increment not as per performance" Value="12" />
                                <asp:ListItem Text="Job Stress" Value="13" />
                                <asp:ListItem Text="Benefits offered by Highbar" Value="14" />
                                <asp:ListItem Text="Other" Value="15" />
                            </asp:CheckBoxList>

                            <asp:TextBox ID="txtOther12" runat="server" Visible="false" />
                        </li>
                        <li style="width: 700px;"><b>3. Rate the following aspects regarding the company</b>&nbsp;<span style="color: red">*</span>
                            <br />
                            <br />
                            <table id="cssTable" style="width: 100%;">
                                <tr>
                                    <td align="center" style="width: 30%;"></td>
                                    <td align="center" style="width: 10%;">Very Poor</td>
                                    <td align="center" style="width: 10%;">Poor</td>
                                    <td align="center" style="width: 10%;">Fair</td>
                                    <td align="center" style="width: 10%;">Very good</td>
                                    <td align="center" style="width: 10%;">Exceptional</td>
                                    <td style="width: 25%;"></td>
                                </tr>

                                <tr>
                                    <td style="vertical-align: middle; text-align: left; width: 300px; padding: 0px 0px 10px 10px;">Quality of policies</td>
                                    <td colspan="5" style="text-align: center">
                                        <asp:RadioButtonList ID="rdlst_91" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Text="" />
                                            <asp:ListItem Value="2" Text="" />
                                            <asp:ListItem Value="3" Text="" />
                                            <asp:ListItem Value="4" Text="" />
                                            <asp:ListItem Value="5" Text="" />
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Please select at least one rating option"
                                            ControlToValidate="rdlst_91" runat="server" ForeColor="Red" Display="Dynamic" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: middle; text-align: left; width: 300px; padding: 0px 0px 10px 10px;">Administration of policies</td>
                                    <td colspan="5">
                                        <asp:RadioButtonList ID="rdlst_92" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Text="" />
                                            <asp:ListItem Value="2" Text="" />
                                            <asp:ListItem Value="3" Text="" />
                                            <asp:ListItem Value="4" Text="" />
                                            <asp:ListItem Value="5" Text="" />
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Please select at least one rating option"
                                            ControlToValidate="rdlst_92" runat="server" ForeColor="Red" Display="Dynamic" />
                                    </td>

                                </tr>
                                <tr>
                                    <td style="vertical-align: middle; text-align: left; width: 300px; padding: 0px 0px 10px 10px;">Performance appraisal</td>
                                    <td colspan="5">
                                        <asp:RadioButtonList ID="rdlst_93" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Text="" />
                                            <asp:ListItem Value="2" Text="" />
                                            <asp:ListItem Value="3" Text="" />
                                            <asp:ListItem Value="4" Text="" />
                                            <asp:ListItem Value="5" Text="" />
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Please select at least one rating option"
                                            ControlToValidate="rdlst_93" runat="server" ForeColor="Red" Display="Dynamic" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: middle; text-align: left; width: 300px; padding: 0px 0px 10px 10px;">Training & Development</td>
                                    <td colspan="5">
                                        <asp:RadioButtonList ID="rdlst_94" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Text="" />
                                            <asp:ListItem Value="2" Text="" />
                                            <asp:ListItem Value="3" Text="" />
                                            <asp:ListItem Value="4" Text="" />
                                            <asp:ListItem Value="5" Text="" />
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ErrorMessage="Please select at least one rating option"
                                            ControlToValidate="rdlst_94" runat="server" ForeColor="Red" Display="Dynamic" />
                                    </td>

                                </tr>
                                <tr>
                                    <td style="vertical-align: middle; text-align: left; width: 300px; padding: 0px 0px 10px 10px;">Physical working conditions</td>
                                    <td colspan="5">
                                        <asp:RadioButtonList ID="rdlst_95" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Text="" />
                                            <asp:ListItem Value="2" Text="" />
                                            <asp:ListItem Value="3" Text="" />
                                            <asp:ListItem Value="4" Text="" />
                                            <asp:ListItem Value="5" Text="" />
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ErrorMessage="Please select at least one rating option"
                                            ControlToValidate="rdlst_95" runat="server" ForeColor="Red" Display="Dynamic" />
                                    </td>

                                </tr>
                                <tr>
                                    <td style="vertical-align: middle; text-align: left; width: 300px; padding: 0px 0px 10px 10px;">Site Facilities</td>
                                    <td colspan="5">
                                        <asp:RadioButtonList ID="rdlst_96" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Text="" />
                                            <asp:ListItem Value="2" Text="" />
                                            <asp:ListItem Value="3" Text="" />
                                            <asp:ListItem Value="4" Text="" />
                                            <asp:ListItem Value="5" Text="" />
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ErrorMessage="Please select at least one rating option"
                                            ControlToValidate="rdlst_96" runat="server" ForeColor="Red" Display="Dynamic" />
                                    </td>

                                </tr>
                                <tr>
                                    <td style="vertical-align: middle; text-align: left; width: 300px; padding: 0px 0px 10px 10px;">Overall Culture</td>
                                    <td colspan="5">
                                        <asp:RadioButtonList ID="rdlst_97" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Text="" />
                                            <asp:ListItem Value="2" Text="" />
                                            <asp:ListItem Value="3" Text="" />
                                            <asp:ListItem Value="4" Text="" />
                                            <asp:ListItem Value="5" Text="" />
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ErrorMessage="Please select at least one rating option"
                                            ControlToValidate="rdlst_97" runat="server" ForeColor="Red" Display="Dynamic" />
                                    </td>

                                </tr>
                            </table>
                        </li>
                        <br />
                        <br />
                        <li style="width: 700px;"><b>4. Rate the following aspects regarding your immediate supervisor</b>&nbsp;<span style="color: red">*</span>
                            <br />
                            <br />
                            <table id="cssTable" style="width: 100%;">
                                <tr>
                                    <td align="center" style="width: 30%;"></td>
                                    <td align="center" style="width: 10%;">Very Poor</td>
                                    <td align="center" style="width: 10%;">Poor</td>
                                    <td align="center" style="width: 10%;">Fair</td>
                                    <td align="center" style="width: 10%;">Very good</td>
                                    <td align="center" style="width: 10%;">Exceptional</td>
                                    <td style="width: 25%;"></td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: middle; text-align: left; width: 300px; padding: 0px 0px 10px 10px;">Shows fair and equal treatment</td>
                                    <td colspan="5">
                                        <asp:RadioButtonList ID="rdlst_101" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Text="" />
                                            <asp:ListItem Value="2" Text="" />
                                            <asp:ListItem Value="3" Text="" />
                                            <asp:ListItem Value="4" Text="" />
                                            <asp:ListItem Value="5" Text="" />
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ErrorMessage="Please select at least one rating option"
                                            ControlToValidate="rdlst_101" runat="server" ForeColor="Red" Display="Dynamic" />
                                    </td>

                                </tr>
                                <tr>
                                    <td style="vertical-align: middle; text-align: left; width: 300px; padding: 0px 0px 10px 10px;">Gives appropriate recognition</td>
                                    <td colspan="5">
                                        <asp:RadioButtonList ID="rdlst_102" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Text="" />
                                            <asp:ListItem Value="2" Text="" />
                                            <asp:ListItem Value="3" Text="" />
                                            <asp:ListItem Value="4" Text="" />
                                            <asp:ListItem Value="5" Text="" />
                                        </asp:RadioButtonList>

                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ErrorMessage="Please select at least one rating option"
                                            ControlToValidate="rdlst_102" runat="server" ForeColor="Red" Display="Dynamic" />
                                    </td>

                                </tr>
                                <tr>
                                    <td style="vertical-align: middle; text-align: left; width: 300px; padding: 0px 0px 10px 10px;">Resolves complaints/problems on time</td>
                                    <td colspan="5">
                                        <asp:RadioButtonList ID="rdlst_103" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Text="" />
                                            <asp:ListItem Value="2" Text="" />
                                            <asp:ListItem Value="3" Text="" />
                                            <asp:ListItem Value="4" Text="" />
                                            <asp:ListItem Value="5" Text="" />
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ErrorMessage="Please select at least one rating option"
                                            ControlToValidate="rdlst_103" runat="server" ForeColor="Red" Display="Dynamic" />
                                    </td>

                                </tr>
                                <tr>
                                    <td style="vertical-align: middle; text-align: left; width: 300px; padding: 0px 0px 10px 10px;">Encourages feedback</td>
                                    <td colspan="5">
                                        <asp:RadioButtonList ID="rdlst_104" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Text="" />
                                            <asp:ListItem Value="2" Text="" />
                                            <asp:ListItem Value="3" Text="" />
                                            <asp:ListItem Value="4" Text="" />
                                            <asp:ListItem Value="5" Text="" />
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ErrorMessage="Please select at least one rating option"
                                            ControlToValidate="rdlst_104" runat="server" ForeColor="Red" Display="Dynamic" />
                                    </td>

                                </tr>
                                <tr>
                                    <td style="vertical-align: middle; text-align: left; width: 300px; padding: 0px 0px 10px 10px;">Follows policies & procedures</td>
                                    <td colspan="5">
                                        <asp:RadioButtonList ID="rdlst_105" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Text="" />
                                            <asp:ListItem Value="2" Text="" />
                                            <asp:ListItem Value="3" Text="" />
                                            <asp:ListItem Value="4" Text="" />
                                            <asp:ListItem Value="5" Text="" />
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ErrorMessage="Please select at least one rating option"
                                            ControlToValidate="rdlst_105" runat="server" ForeColor="Red" Display="Dynamic" />
                                    </td>

                                </tr>
                                <tr>
                                    <td style="vertical-align: middle; text-align: left; width: 300px; padding: 0px 0px 10px 10px;">Discusses performance & Career growth</td>
                                    <td colspan="5">
                                        <asp:RadioButtonList ID="rdlst_106" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Text="" />
                                            <asp:ListItem Value="2" Text="" />
                                            <asp:ListItem Value="3" Text="" />
                                            <asp:ListItem Value="4" Text="" />
                                            <asp:ListItem Value="5" Text="" />
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ErrorMessage="Please select at least one rating option"
                                            ControlToValidate="rdlst_106" runat="server" ForeColor="Red" Display="Dynamic" />
                                    </td>

                                </tr>
                                <tr>
                                    <td style="vertical-align: middle; text-align: left; width: 300px; padding: 0px 0px 10px 10px;">Connects on regular basis</td>
                                    <td colspan="5">
                                        <asp:RadioButtonList ID="rdlst_107" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Text="" />
                                            <asp:ListItem Value="2" Text="" />
                                            <asp:ListItem Value="3" Text="" />
                                            <asp:ListItem Value="4" Text="" />
                                            <asp:ListItem Value="5" Text="" />
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ErrorMessage="Please select at least one rating option"
                                            ControlToValidate="rdlst_107" runat="server" ForeColor="Red" Display="Dynamic" />
                                    </td>

                                </tr>
                            </table>
                        </li>
                        <br />
                        <br />
                        <li style="width: 700px;"><b>5. Rate the following aspects regarding other aspects of your experience at Highbar</b>&nbsp;<span style="color: red">*</span>
                            <br />
                            <br />
                            <table id="cssTable" style="width: 100%;">
                                <tr>
                                    <td align="center" style="width: 30%;"></td>
                                    <td align="center" style="width: 10%;">Very Poor</td>
                                    <td align="center" style="width: 10%;">Poor</td>
                                    <td align="center" style="width: 10%;">Fair</td>
                                    <td align="center" style="width: 10%;">Very good</td>
                                    <td align="center" style="width: 10%;">Exceptional</td>
                                    <td style="width: 25%;"></td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: middle; text-align: left; width: 300px; padding: 0px 0px 10px 10px;">Satisfaction with Job profile currently holding</td>
                                    <td colspan="5">
                                        <asp:RadioButtonList ID="rdlst_111" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Text="" />
                                            <asp:ListItem Value="2" Text="" />
                                            <asp:ListItem Value="3" Text="" />
                                            <asp:ListItem Value="4" Text="" />
                                            <asp:ListItem Value="5" Text="" />
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ErrorMessage="Please select atleast one rating option"
                                            ControlToValidate="rdlst_111" runat="server" ForeColor="Red" Display="Dynamic" />
                                    </td>

                                </tr>
                                <tr>
                                    <td style="vertical-align: middle; text-align: left; width: 300px; padding: 0px 0px 10px 10px;">Role clarity</td>
                                    <td colspan="5">
                                        <asp:RadioButtonList ID="rdlst_112" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Text="" />
                                            <asp:ListItem Value="2" Text="" />
                                            <asp:ListItem Value="3" Text="" />
                                            <asp:ListItem Value="4" Text="" />
                                            <asp:ListItem Value="5" Text="" />
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ErrorMessage="Please select atleast one rating option"
                                            ControlToValidate="rdlst_112" runat="server" ForeColor="Red" Display="Dynamic" />
                                    </td>

                                </tr>
                                <tr>
                                    <td style="vertical-align: middle; text-align: left; width: 300px; padding: 0px 0px 10px 10px;">Encouraging feedback</td>
                                    <td colspan="5">
                                        <asp:RadioButtonList ID="rdlst_113" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Text="" />
                                            <asp:ListItem Value="2" Text="" />
                                            <asp:ListItem Value="3" Text="" />
                                            <asp:ListItem Value="4" Text="" />
                                            <asp:ListItem Value="5" Text="" />
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" ErrorMessage="Please select at least one rating option"
                                            ControlToValidate="rdlst_113" runat="server" ForeColor="Red" Display="Dynamic" />
                                    </td>

                                </tr>
                                <tr>
                                    <td style="vertical-align: middle; text-align: left; width: 300px; padding: 0px 0px 10px 10px;">Benefits provided by the Company</td>
                                    <td colspan="5">
                                        <asp:RadioButtonList ID="rdlst_114" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Text="" />
                                            <asp:ListItem Value="2" Text="" />
                                            <asp:ListItem Value="3" Text="" />
                                            <asp:ListItem Value="4" Text="" />
                                            <asp:ListItem Value="5" Text="" />
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" ErrorMessage="Please select at least one rating option"
                                            ControlToValidate="rdlst_114" runat="server" ForeColor="Red" Display="Dynamic" />
                                    </td>

                                </tr>
                                <tr>
                                    <td style="vertical-align: middle; text-align: left; width: 300px; padding: 0px 0px 10px 10px;">Prospects for promotion & growth</td>
                                    <td colspan="5">
                                        <asp:RadioButtonList ID="rdlst_115" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Text="" />
                                            <asp:ListItem Value="2" Text="" />
                                            <asp:ListItem Value="3" Text="" />
                                            <asp:ListItem Value="4" Text="" />
                                            <asp:ListItem Value="5" Text="" />
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" ErrorMessage="Please select at least one rating option"
                                            ControlToValidate="rdlst_115" runat="server" ForeColor="Red" Display="Dynamic" />
                                    </td>

                                </tr>
                                <tr>
                                    <td style="vertical-align: middle; text-align: left; width: 300px; padding: 0px 0px 10px 10px;">Timely Recognition for job well done</td>
                                    <td colspan="5">
                                        <asp:RadioButtonList ID="rdlst_116" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Text="" />
                                            <asp:ListItem Value="2" Text="" />
                                            <asp:ListItem Value="3" Text="" />
                                            <asp:ListItem Value="4" Text="" />
                                            <asp:ListItem Value="5" Text="" />
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" ErrorMessage="Please select at least one rating option"
                                            ControlToValidate="rdlst_116" runat="server" ForeColor="Red" Display="Dynamic" />
                                    </td>

                                </tr>
                                <tr>
                                    <td style="vertical-align: middle; text-align: left; width: 300px; padding: 0px 0px 10px 10px;">Team work & Team spirit</td>
                                    <td colspan="5">
                                        <asp:RadioButtonList ID="rdlst_117" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Text="" />
                                            <asp:ListItem Value="2" Text="" />
                                            <asp:ListItem Value="3" Text="" />
                                            <asp:ListItem Value="4" Text="" />
                                            <asp:ListItem Value="5" Text="" />
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" ErrorMessage="Please select at least one rating option"
                                            ControlToValidate="rdlst_117" runat="server" ForeColor="Red" Display="Dynamic" />
                                    </td>

                                </tr>
                                <tr>
                                    <td style="vertical-align: middle; text-align: left; width: 300px; padding: 0px 0px 10px 10px;">Feedback discussion on training & development needs</td>
                                    <td colspan="5">
                                        <asp:RadioButtonList ID="rdlst_118" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Text="" />
                                            <asp:ListItem Value="2" Text="" />
                                            <asp:ListItem Value="3" Text="" />
                                            <asp:ListItem Value="4" Text="" />
                                            <asp:ListItem Value="5" Text="" />
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" ErrorMessage="Please select at least one rating option"
                                            ControlToValidate="rdlst_118" runat="server" ForeColor="Red" Display="Dynamic" />
                                    </td>

                                </tr>
                                <tr>
                                    <td style="vertical-align: middle; text-align: left; width: 300px; padding: 0px 0px 10px 10px;">Handling of complaints & issues by other departments</td>
                                    <td colspan="5">
                                        <asp:RadioButtonList ID="rdlst_119" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Text="" />
                                            <asp:ListItem Value="2" Text="" />
                                            <asp:ListItem Value="3" Text="" />
                                            <asp:ListItem Value="4" Text="" />
                                            <asp:ListItem Value="5" Text="" />
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" ErrorMessage="Please select at least one rating option"
                                            ControlToValidate="rdlst_119" runat="server" ForeColor="Red" Display="Dynamic" />
                                    </td>

                                </tr>
                                <tr>
                                    <td style="vertical-align: middle; text-align: left; width: 300px; padding: 0px 0px 10px 10px;">Feeling of belongingness</td>
                                    <td colspan="5">
                                        <asp:RadioButtonList ID="rdlst_1110" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Text="" />
                                            <asp:ListItem Value="2" Text="" />
                                            <asp:ListItem Value="3" Text="" />
                                            <asp:ListItem Value="4" Text="" />
                                            <asp:ListItem Value="5" Text="" />
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" ErrorMessage="Please select at least one rating option"
                                            ControlToValidate="rdlst_1110" runat="server" ForeColor="Red" Display="Dynamic" />
                                    </td>

                                </tr>
                            </table>
                        </li>

                        <li style="width: 700px;"><b>6. If you are leaving the company to accept another job then please tell us what was the attractive factor in the new offer.</b>&nbsp;<span style="color: red">*</span>
                            <br />

                            <asp:CustomValidator runat="server" ID="CustomValidator1"
                                ClientValidationFunction="ValidateModuleList13"
                                ErrorMessage="Please Select at least one option" ForeColor="Red"></asp:CustomValidator>
                            <asp:CheckBoxList ID="checklst13" runat="server" RepeatDirection="Vertical">
                                <asp:ListItem Text="Higher Salary" Value="1" />
                                <asp:ListItem Text="Better benefits" Value="2" />
                                <asp:ListItem Text="Job Role" Value="3" />
                                <asp:ListItem Text="Location flexibility/work from home" Value="4" />
                                <asp:ListItem Text="None of the above - I don't have any counter offer" Value="5" />
                                <asp:ListItem Text="Other" Value="6" />
                            </asp:CheckBoxList>

                            <asp:TextBox ID="txtOther13" runat="server" Visible="false" />
                        </li>
                        <br />
                        <li style="width: 680px;"><b>7. Please tell us the % hike offered by the new company (only applicable if you are joining another company)</b>&nbsp;<span style="color: red">*</span><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator25" ErrorMessage="This field is mandatory."
                                ControlToValidate="txt14" runat="server" ForeColor="Red" Display="Dynamic" />
                            <asp:TextBox ID="txt14" TextMode="MultiLine" runat="server" MaxLength="200"></asp:TextBox><br />

                        </li>
                        <br />
                        <li style="width: 680px;"><b>8. Would you like to consider yourself for re-employment at Highbar? If No, Please state the reason</b>(Maximum 200 char)&nbsp;<span style="color: red">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator26" ErrorMessage="This field is mandatory."
                                ControlToValidate="txt15" runat="server" ForeColor="Red" Display="Dynamic" />
                            <asp:TextBox ID="txt15" TextMode="MultiLine" runat="server" MaxLength="200"></asp:TextBox><br />

                        </li>
                        <li style="width: 680px;"><b>9. What can Highbar offer you which will encourage you to stay and withdraw your resignation</b>(Maximum 500 char)&nbsp;<span style="color: red">*</span><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator27" ErrorMessage="This field is mandatory."
                                ControlToValidate="txt16" runat="server" ForeColor="Red" Display="Dynamic" />
                            <asp:TextBox ID="txt16" TextMode="MultiLine" runat="server" MaxLength="500"></asp:TextBox><br />

                        </li>
                        <li style="width: 680px;"><b>10. Any other suggestion/remarks</b>(Maximum 500 char)&nbsp;<span style="color: red">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator28" ErrorMessage="This field is mandatory."
                                ControlToValidate="txt17" runat="server" ForeColor="Red" Display="Dynamic" />
                            <asp:TextBox ID="txt17" TextMode="MultiLine" runat="server" MaxLength="500"></asp:TextBox><br />

                        </li>
                    </ul>

                </div>
                <%--<asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />--%>
            </div>

        </div>
    </div>
    <div class="mobile_Savebtndiv">
        <asp:LinkButton ID="mobile_btnSave" runat="server" Text="Submit" ToolTip="Save" CausesValidation="true" CssClass="Savebtnsve" OnClick="btnSubmit_Click" OnClientClick="if(Page_ClientValidate()) submitform();">Submit</asp:LinkButton>
        <asp:LinkButton ID="mobile_cancel" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="Savebtnsve" OnClick="mobile_cancel_Click">Cancel</asp:LinkButton>
    </div>
    <asp:HiddenField ID="hdnResId" runat="server" />
    <br />
</asp:Content>


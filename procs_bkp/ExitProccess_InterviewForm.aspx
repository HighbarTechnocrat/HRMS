<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="ExitProccess_InterviewForm.aspx.cs" Inherits="procs_ExitProccess_InterviewForm" %>

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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
    <script type="text/javascript">
        var deprt;
        $(document).ready(function () {
        });

        function ValidateModuleList7(source, args) {
            var chkListModules = document.getElementById('<%= checklst7.ClientID %>');
            var chkListinputs = chkListModules.getElementsByTagName("input");
            for (var i = 0; i < chkListinputs.length; i++) {
                if (chkListinputs[i].checked) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }

          function SaveMultiClick() {
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
            var confirmval = false;
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Are you sure you want to submit?")) {
                confirm_value.value = "Yes";
                confirmval = true;
            } else {
                confirm_value.value = "No";
                confirmval = false;
            }
            //alert(confirm_value.value);
            //document.forms[0].appendChild(confirm_value);
            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return confirmval;

        }

    </script>

    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">

                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Exit Interview Form"></asp:Label>
                    </span>
                </div>
                <div class="edit-contact">
                    <a href="ExitProcess_Index.aspx" class="aaa">Exit Process Menu</a>
                    <%--<asp:Panel ID="pnlSurvey" runat="server">
                        </asp:Panel>--%>
                    <ul id="CreateExitSurveyform" runat="server" visible="true">
                        <li>
                            <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 12px; font-weight: 400; text-align: center;"></asp:Label>
                            <br />
                            <asp:Label runat="server" ID="lblmsg2" ClientIDMode="Static" Visible="True" Text="All fields are mandatory" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label><br />
                            <%--<asp:TextBox runat="server" ID="lblmessage1" Visible="False" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;" OnTextChanged="lblmessage1_TextChanged"></asp:TextBox>--%>
                        </li>
                        <br />
                        <li><b>Full name</b>
                            <asp:TextBox ID="txtfullName" runat="server" Enabled="false"></asp:TextBox>
                        </li>

                        <li><b>Employee Code /Emp ID</b>
                            <asp:TextBox ID="txtempCode" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li><b>Full name of your resigned reportee for whom you are providing feedback</b>
                            <asp:TextBox ID="txtFullNameResignedReportee" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li><b>Employee Code/ Emp ID of resigned reportee</b>
                            <asp:TextBox ID="txtEmpCodeResignedReportee" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li><b>Last Working Day of Reportee</b>
                            <asp:TextBox ID="txtLWD" runat="server" Enabled="false"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator29" ErrorMessage="Please enter Last Working Day of Reportee."
                                ControlToValidate="txtLWD" runat="server" ForeColor="Red" Display="Dynamic" />--%>
                        </li>
                        <br />
                        <li style="width: 700px"><b>1. Current Accomodation status of the Resigned Reportee</b>&nbsp;<span style="color: red">*</span>
                        </li>
                        <li>
                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Please select atleast one"
                                ControlToValidate="rdbtnlst6" runat="server" ForeColor="Red" Display="Dynamic" />--%>
                            <asp:Label runat="server" ID="lblAccomodationmsg" Visible="True" Style="color: red; font-size: 12px; font-weight: 400; text-align: center;"></asp:Label>
                            <asp:RadioButtonList ID="rdbtnlst6" runat="server">
                                <asp:ListItem Text="Self" Value="1" />
                                <asp:ListItem Text="Company Rental" Value="2" />
                                <asp:ListItem Text="Paying Guest" Value="3" />
                                <asp:ListItem Text="Guest house" Value="4" />
                                <asp:ListItem Text="Other" Value="5" />
                            </asp:RadioButtonList>
                        </li>
                        <br />

                        <li style="width: 750px;"><b>2. According to you what are the primary reasons behind the resigned reportee's exit from Highbar. Please tick all applicable boxes</b>&nbsp;<span style="color: red">*</span>

                            <br />
                            <%--<asp:CustomValidator runat="server" ID="cvmodulelist"
                                ClientValidationFunction="ValidateModuleList7"
                                ErrorMessage="Please Select atleast one" ForeColor="Red"></asp:CustomValidator>--%>
                            <asp:Label runat="server" ID="lblExitMsg" Visible="True" Style="color: red; font-size: 12px; font-weight: 400; text-align: center;"></asp:Label>
                            <asp:CheckBoxList ID="checklst7" runat="server" RepeatDirection="Vertical">
                                <asp:ListItem Text="Higher studies" Value="1" />
                                <asp:ListItem Text="Family problems" Value="2" />
                                <asp:ListItem Text="Personal medical reasons" Value="3" />
                                <asp:ListItem Text="Working conditions" Value="4" />
                                <asp:ListItem Text="Compensation related issues" Value="5" />
                                <asp:ListItem Text="Unethical conduct at workplace" Value="6" />
                                <asp:ListItem Text="Better opportunity (IT company)" Value="7" />
                                <asp:ListItem Text="Better opportunity (Non IT company)" Value="8" />
                                <asp:ListItem Text="Lack of recognition" Value="9" />
                                <asp:ListItem Text="Problems with Colleagues/Dissatisfaction with senior" Value="10" />
                                <asp:ListItem Text="Unsatisfactory job profile" Value="11" />
                                <asp:ListItem Text="Appraisal/increment not as per performance" Value="12" />
                                <asp:ListItem Text="Job stress" Value="13" />
                                <asp:ListItem Text="Asked to go/Terminated" Value="14" />
                                <asp:ListItem Text="Intergrity issues" Value="15" />
                                <asp:ListItem Text="Benefits offered by Highbar" Value="16" />
                                <asp:ListItem Text="Other" Value="17" />
                            </asp:CheckBoxList>
                            <asp:TextBox ID="txtOther12" runat="server" Visible="false" />
                        </li>
                        <br />
                        <li style="width: 600px;"><b>3. What development took place leading to your reportee's exit</b>(Maximum 200 char)&nbsp;<span style="color: red">*</span>
                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="This field is mandatory."
                                ControlToValidate="txt8" runat="server" ForeColor="Red" Display="Dynamic" />--%>
                               <br />
                            <asp:Label runat="server" ID="lbltxt8msg" Visible="True" Style="color: red; font-size: 12px; font-weight: 400; text-align: center;"></asp:Label>
                            <asp:TextBox ID="txt8" TextMode="MultiLine" runat="server" MaxLength="200"></asp:TextBox>
                          

                        </li>
                        <br />
                        <li style="width: 600px;"><b>4. Did you connect with him/her to have retention discussion post resignation? If Yes, what was being offered in order to retain the employee? If No, State the reason</b>(Maximum 200 char)&nbsp;<span style="color: red">*</span>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" ErrorMessage="This field is mandatory."
                                ControlToValidate="txt9" runat="server" ForeColor="Red" Display="Dynamic" />--%>
                             <br />
                            <asp:Label runat="server" ID="lbltxt9msg" Visible="True" Style="color: red; font-size: 12px; font-weight: 400; text-align: center;"></asp:Label>
                            <asp:TextBox ID="txt9" TextMode="MultiLine" runat="server" MaxLength="200"></asp:TextBox>

                        </li>
                        <li style="width: 650px;"><b>5. What are his/her suggestions regarding any areas of improvement within the organisation?</b>(Maximum 500 char)&nbsp;<span style="color: red">*</span><br />
                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ErrorMessage="This field is mandatory."
                                ControlToValidate="txt10" runat="server" ForeColor="Red" Display="Dynamic" />--%>
                            <br />
                            <asp:Label runat="server" ID="lbltxt10msg" Visible="True" Style="color: red; font-size: 12px; font-weight: 400; text-align: center;"></asp:Label>
                            <asp:TextBox ID="txt10" TextMode="MultiLine" runat="server" MaxLength="500"></asp:TextBox>

                        </li>
                        <li style="width: 600px;"><b>6. Any other suggestion/remarks</b>(Maximun 500 char)&nbsp;<span style="color: red">*</span>
                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ErrorMessage="This field is mandatory."
                                ControlToValidate="txt11" runat="server" ForeColor="Red" Display="Dynamic" />--%>
                            <br />
                            <asp:Label runat="server" ID="lbltxt11msg" Visible="True" Style="color: red; font-size: 12px; font-weight: 400; text-align: center;"></asp:Label>
                            <asp:TextBox ID="txt11" TextMode="MultiLine" runat="server" MaxLength="500"></asp:TextBox>

                        </li>
                    </ul>


                </div>
                <%--<asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />--%>
            </div>

        </div>
    </div>
    <div class="mobile_Savebtndiv">
        <%--<asp:LinkButton ID="btnSubmit" runat="server" Text="Submit" ToolTip="Submit" CssClass="Savebtnsve" OnClick="btnSubmit_Click">Submit</asp:LinkButton>--%>
        <%--<asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="Confirm()" CssClass="Savebtnsve" />--%>

        <asp:LinkButton ID="mobile_btnSave" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClick="btnSubmit_Click" OnClientClick="return SaveMultiClick();">Submit</asp:LinkButton>
        <asp:LinkButton ID="mobile_cancel" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="Savebtnsve" OnClick="mobile_cancel_Click">Cancel</asp:LinkButton>

        <asp:LinkButton ID="mobile_btnReject" runat="server" Text="Save as Draft" ToolTip="Save as Draft" CssClass="Savebtnsve" OnClick="mobile_btnReject_Click" OnClientClick="return SaveMultiClick();">Save as Draft</asp:LinkButton>

        <asp:LinkButton ID="claimmob_btnSubmit" runat="server" Text="Back" ToolTip="Retain" CssClass="Savebtnsve">Back</asp:LinkButton>
    </div>
    <br />

    <asp:HiddenField ID="hdnYesNo" runat="server" />
</asp:Content>


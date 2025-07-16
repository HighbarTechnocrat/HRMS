<%@ Control Language="C#" AutoEventWireup="true" CodeFile="register.ascx.cs" Inherits="Themes_SecondTheme_LayoutControls_register" %>
<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

     <script language="javascript" type="text/javascript">
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
             //asp.net textarea maxlength doesnt work; do it by hand
             var maxlength = 100; //set your value here (or add a parm and pass it in)
             var object = document.getElementById(text.id)  //get your object
             if (object.value.length > maxlength) {
                 object.focus(); //set focus to prevent jumping
                 object.value = text.value.substring(0, maxlength); //truncate the value
                 object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
                 return false;
             }
             return true;
         }


    </script>

<div class="registerbox">
    <div class="registerboxheading">
        Quick Sign Up</div>
    <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" OnCreatedUser="CreateUserWizard1_CreatedUser"
        OnCreatingUser="CreateUserWizard1_CreatingUser" CreateUserButtonType="Image"
        align="center">
        <WizardSteps>
            <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
                <ContentTemplate>
                    <div class="registerformbox">
                    <table>
                    	<tr>
                        	<td>
                        <asp:Label ID="lblfname" runat="server" AssociatedControlID="txtfirstname" Visible="true"
                            class="registerlabel"><span >*</span> First Name:</asp:Label>
                            </td>
                            <td>
                        <asp:TextBox ID="txtfirstname" runat="server" TabIndex="1" MaxLength="20" class="registerinput" onkeydown="javascript:if((event.which&amp;&amp;event.which==13)||(event.keyCode&amp;&amp;event.keyCode==13)){document.forms[0].elements['MainContent_log_in_m_uxregister_CreateUserWizard1_CreateUserStepContainer_CreateUserButton'].click();return false;}else return true;"></asp:TextBox>
                        <font size="2px" color="black">(Maximum 20 characters)</font>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Font-Bold="false"
                            ControlToValidate="txtfirstname" ErrorMessage="First name is required" ToolTip="First name is required"
                            ValidationGroup="CreateUserWizard1" CssClass="registererror" Style="margin-left: 2px"
                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Enter the character only"
                            ControlToValidate="txtfirstname" ValidationGroup="CreateUserWizard1" Style="margin-left: -113px"
                            ValidationExpression="^[A-Za-z ]*$" CssClass="registererror" SetFocusOnError="true"
                            ToolTip="Enter the character only"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                    </table>
                    </div>
                    <div class="registerformbox">
                    <table>
                    	<tr>
                        	<td>
                        <asp:Label ID="lbllname" runat="server" AssociatedControlID="txtlastname" Visible="true"
                            class="registerlabel"><span>*</span> Last Name:</asp:Label>
                            </td>
                            <td>
                        <asp:TextBox ID="txtlastname" runat="server" TabIndex="2" MaxLength="20" class="registerinput" onkeydown="javascript:if((event.which&amp;&amp;event.which==13)||(event.keyCode&amp;&amp;event.keyCode==13)){document.forms[0].elements['MainContent_log_in_m_uxregister_CreateUserWizard1_CreateUserStepContainer_CreateUserButton'].click();return false;}else return true;"></asp:TextBox>
                        <font style="color:#181818;">(Maximum 20 characters)</font>
                        <asp:RequiredFieldValidator ID="LastNameRequired" runat="server" Font-Bold="false"
                            ControlToValidate="txtlastname" ErrorMessage="Last name is required" ToolTip="Last name is required"
                            ValidationGroup="CreateUserWizard1" Style="margin-left: 2px" CssClass="registererror"
                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Enter the character only"
                            ControlToValidate="txtlastname" ValidationGroup="CreateUserWizard1" Style="margin-left: -121px"
                            ValidationExpression="^[A-Za-z ]*$" CssClass="registererror" SetFocusOnError="true"
                            ToolTip="Enter the character only"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                    </table>
                    </div>
                    <div class="registerformbox">
                        <asp:UpdatePanel ID="UpdatePanel_username" runat="server">
                            <ContentTemplate>
                            	<table>
                    				<tr>
                        				<td>
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" Visible="true"
                                    class="registerlabel"><span>*</span> E-mail Id:</asp:Label>
                                    	</td>
                            			<td>
                                <asp:TextBox ID="UserName" runat="server" TabIndex="3" AutoPostBack="true" class="registerinput"
                                    OnTextChanged="UserName_TextChanged" onkeydown="javascript:if((event.which&amp;&amp;event.which==13)||(event.keyCode&amp;&amp;event.keyCode==13)){document.forms[0].elements['MainContent_log_in_m_uxregister_CreateUserWizard1_CreateUserStepContainer_CreateUserButton'].click();return false;}else return true;"></asp:TextBox>
                                <font style="color:#181818;">[Example: abc@xyz.com]&nbsp;&nbsp;</font>
                                <asp:RequiredFieldValidator ID="username_validate" runat="server" ControlToValidate="UserName"
                                    ErrorMessage="Email id is required." ToolTip="Email id is required." ValidationGroup="CreateUserWizard1"
                                    SetFocusOnError="true" CssClass="registererror" Style="margin-left: 2px"></asp:RequiredFieldValidator>
                                <asp:Label ID="lblstatus" runat="server" AutoPostBack="true" Visible="false" CssClass="registererror"
                                    Style="margin-left: 2px; margin-top: -17px; float: left" ValidationGroup="CreateUserWizard1"></asp:Label>
                                    	</td>
                        			</tr>
                    			</table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="UserName" EventName="TextChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:RegularExpressionValidator ID="EmailValidator" runat="server" ErrorMessage="Email id is not vaild."
                            CssClass="registererror" ControlToValidate="UserName" ValidationExpression="^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$"
                            SetFocusOnError="true" Style="margin-left: 115px; margin-top: -18px; float: left"
                            ValidationGroup="CreateUserWizard1" Display="Dynamic"></asp:RegularExpressionValidator>
                    </div>
                    <div class="registerformbox">
                    <table>
                    	<tr>
                        	<td>
                        <asp:Label ID="PasswordLabel" runat="server" Visible="true" class="registerlabel"><span>*</span> Password:</asp:Label>
                        	</td>
                            <td>
                        <asp:TextBox ID="Password" runat="server" TextMode="Password" TabIndex="4" class="registerinput"
                            onkeydown="javascript:if((event.which&amp;&amp;event.which==13)||(event.keyCode&amp;&amp;event.keyCode==13)){document.forms[0].elements['MainContent_log_in_m_uxregister_CreateUserWizard1_CreateUserStepContainer_CreateUserButton'].click();return false;}else return true;"></asp:TextBox>
                        <font style="color:#181818;">(Use atleast 7 characters)&nbsp;&nbsp;</font>
                        <asp:RequiredFieldValidator ID="Pwdvalidator" ControlToValidate="Password" ErrorMessage="Password is required"
                            runat="server" Text="" SetFocusOnError="true" ValidationGroup="CreateUserWizard1"
                            CssClass="registererror" Style="margin-left: 2px"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="valPasswordLength" runat="server" ControlToValidate="Password"
                            SetFocusOnError="true" ValidationExpression="^([a-zA-Z0-9'@&#.\s^$%*!,?<>]{7,})$"
                            ErrorMessage="Minimum 7 characters" ToolTip="Minimum 7 characters"
                            ValidationGroup="CreateUserWizard1" CssClass="registererror" Style="margin-left: -111px"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                    </table>
                    </div>
                    <div class="registerformbox" style="height:6%">
                    <table>
                    	<tr>
                        	<td>
                        <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword"
                            Visible="true" class="registerlabel"><span>*</span> Confirm Password:</asp:Label>
                            </td>
                            <td>
                        <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" TabIndex="5" class="registerinput"
                            onkeydown="javascript:if((event.which&amp;&amp;event.which==13)||(event.keyCode&amp;&amp;event.keyCode==13)){document.forms[0].elements['MainContent_log_in_m_uxregister_CreateUserWizard1_CreateUserStepContainer_CreateUserButton'].click();return false;}else return true;"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="validate_confirmpwd" ControlToValidate="ConfirmPassword"
                            ErrorMessage="Confirm password is required" runat="server" Text="" SetFocusOnError="true"
                            ValidationGroup="CreateUserWizard1" CssClass="registererror" Style="margin-left: 2px"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password"
                            CssClass="registererror" ControlToValidate="ConfirmPassword" ErrorMessage="Password does not match."
                            ValidationGroup="CreateUserWizard1" Style="margin-left: -156px"></asp:CompareValidator>
                            </td>
                        </tr>
                    </table>
                    </div>
                    <div class="registerformbox">
                    <table>
                    	<tr>
                        	<td>
                        <asp:Label ID="lblmobileno" runat="server" AssociatedControlID="txtmobileno" Visible="true"
                            class="registerlabel"><span>*</span> Mobile No:</asp:Label>
                            </td>
                            <td>
                        <ew:NumericBox ID="txtmobileno" runat="server" MaxLength="12" TabIndex="6" class="registerinput" onkeydown="javascript:if((event.which&amp;&amp;event.which==13)||(event.keyCode&amp;&amp;event.keyCode==13)){document.forms[0].elements['MainContent_log_in_m_uxregister_CreateUserWizard1_CreateUserStepContainer_CreateUserButton'].click();return false;}else return true;"> </ew:NumericBox>
                        <font style="color:#181818;">(Maximum 12 digits)&nbsp;&nbsp;</font>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtmobileno"
                            ErrorMessage="Mobile no. is required" ToolTip="Mobile no. is required" ValidationGroup="CreateUserWizard1"
                            SetFocusOnError="true" Display="Dynamic" Style="margin-left: 2px;"
                            CssClass="registererror"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                       <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtmobileno"
                            ErrorMessage="Please enter valid mobile number" ValidationExpression="[6789]\d{9}"
                            ValidationGroup="CreateUserWizard1" CssClass="registererror" Style="margin-left: 2px;
                            "></asp:RegularExpressionValidator>--%>
                             
                    </div>
                    <div class="registerformbox">
                    <table>
                    	<tr>
                        	<td>
                        <asp:Label ID="lblcountry" runat="server" AssociatedControlID="ddlcountry" Visible="true"
                            class="registerlabel"><span>*</span> Country:</asp:Label>
                            </td>
                            <td>
                        <asp:UpdatePanel ID="UpdatePanel_country" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlcountry" AutoPostBack="true" TabIndex="7" class="registerinputselect" runat="server"
                                    OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator CssClass="registererror" ID="validator_ddlcountry" runat="server"
                                    ErrorMessage="Please select country" ControlToValidate="ddlcountry" SetFocusOnError="true"
                                    Display="Dynamic" InitialValue="0" ValidationGroup="CreateUserWizard1" Style="float: left;
                                    margin-left: 2px;">
                                </asp:RequiredFieldValidator>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlcountry" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                        	</td>
                        </tr>
                    </table>
                    </div>
                    <div class="registerformbox">
                    <table>
                    	<tr>
                        	<td>
                        <asp:Label ID="lblstate" runat="server" AssociatedControlID="ddlstate" Visible="true"
                            class="registerlabel"><span>*</span> State:</asp:Label>
                            </td>
                            <td>
                        <asp:UpdatePanel ID="UpdatePanel_state" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlstate" AutoPostBack="true" TabIndex="8" class="registerinputselect" runat="server"
                                    OnSelectedIndexChanged="ddlstate_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator CssClass="registererror" ID="RequiredFieldValidator3"
                                    runat="server" ErrorMessage="Please select state" ControlToValidate="ddlstate"
                                    SetFocusOnError="true" Display="Dynamic" InitialValue="0" ValidationGroup="CreateUserWizard1"
                                    Style="float: left; margin-left: 2px;">
                                </asp:RequiredFieldValidator>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlstate" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                        	</td>
                        </tr>
                    </table>
                    </div>
                    <div class="registerformbox">
                    <table>
                    	<tr>
                        	<td>
                        <asp:Label ID="lblcity" runat="server" AssociatedControlID="ddlcity" Visible="true"
                            class="registerlabel"><span>*</span> City:</asp:Label>
                            </td>
                            <td>
                        <asp:UpdatePanel ID="UpdatePanel_city" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlcity" AutoPostBack="true" TabIndex="9" class="registerinputselect" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator CssClass="registererror" ID="RequiredFieldValidator5"
                                    runat="server" ErrorMessage="Please select city" ControlToValidate="ddlcity"
                                    SetFocusOnError="true" Display="Dynamic" InitialValue="0" ValidationGroup="CreateUserWizard1"
                                    Style="float: left; margin-left: 2px;">
                                </asp:RequiredFieldValidator>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlcity" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                        	</td>
                        </tr>
                    </table>
                    </div>
                    <div class="registerformbox">
                    <table>
                    	<tr>
                        	<td>
                        <asp:Label ID="lbladd" runat="server" AssociatedControlID="txtaddress1" Visible="true"
                            class="registerlabel"><span>*</span> Address:</asp:Label>
                            </td>
                            <td>
                        <asp:TextBox ID="txtaddress1" runat="server" class="registerinput" TabIndex="10" MaxLength="100"
                            TextMode="MultiLine" onKeyUp="javascript:Count(this);" onChange="javascript:Count(this);"></asp:TextBox>
                        <font style="color:#181818;">(Maximum 100 characters)&nbsp;&nbsp;</font>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Font-Bold="false"
                            ControlToValidate="txtaddress1" ErrorMessage="Address is required" ToolTip="Address is required"
                            ValidationGroup="CreateUserWizard1" CssClass="registererror" Style="margin-left: 2px"
                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    </div>
                    <asp:Panel ID="emailsdf" runat="server" Visible="false">
                        <asp:TextBox ID="Email" runat="server"  onkeydown="javascript:if((event.which&amp;&amp;event.which==13)||(event.keyCode&amp;&amp;event.keyCode==13)){document.forms[0].elements['MainContent_log_in_m_uxregister_CreateUserWizard1_CreateUserStepContainer_CreateUserButton'].click();return false;}else return true;"></asp:TextBox>
                        <asp:Label ID="lblchkavailable" runat="server" Width="200" Font-Bold="true" ForeColor="Red"
                            Visible="false"></asp:Label>
                        <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"
                            ErrorMessage="E-mail is required." ToolTip="E-mail is required." ValidationGroup="CreateUserWizard1"
                            CssClass="registererrormsg" Style="float: left">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="ValidateEmial" runat="server" ErrorMessage="Email is not vaild."
                            CssClass="registererrormsg" ControlToValidate="Email" ValidationExpression="^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$"
                            ValidationGroup="CreateUserWizard1" Style="float: left; margin-left: -85px"></asp:RegularExpressionValidator>
                    </asp:Panel>
                    <asp:Panel ID="pnlname" runat="server" Visible="false">
                        <asp:Label ID="lblfirstName" runat="server" AssociatedControlID="FirstName">First Name</asp:Label>
                        <asp:TextBox ID="FirstName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="firstNameRequired" runat="server" ControlToValidate="FirstName"
                            ErrorMessage="First Name is required." ToolTip="First Name is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                        <asp:Label ID="lblLastName" runat="server" AssociatedControlID="LastName">Last Name</asp:Label>
                        <asp:TextBox ID="Lastname" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Lastname"
                            ErrorMessage="Last Name is required." ToolTip="Last Name is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                    </asp:Panel>
                    <asp:Panel ID="pnlpwd" runat="server" Visible="false">
                        <asp:Label ID="QuestionLabel" runat="server" AssociatedControlID="Question">Security Question:</asp:Label>
                        <asp:TextBox ID="Question" runat="server" value="NULL"></asp:TextBox>
                        <asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer">Security Answer:</asp:Label>
                        <asp:TextBox ID="Answer" runat="server" value="NULL"></asp:TextBox>
                    </asp:Panel>
                    <div class="registerremember" id="divagree" runat="server" visible="false">
                        <asp:CheckBox ID="chk_agree" runat="server" Checked="true" />
                        <span>I have read and I agree to the <a href="termscondition.aspx" target="_blank">Terms of use</a> </span>
                        
                    </div>
                    <asp:Label ID="lbl_chkagree" runat="server" Visible="true" Style="float: right; margin-right: 97px"
                        ValidationGroup="CreateUserWizard1" Text="Please tick the above checkbox to agree Terms & Conditions."></asp:Label>
                    <div class="registerbtndiv">
                        <asp:Button ID="CreateUserButton" CommandName="MoveNext" ValidationGroup="CreateUserWizard1"
                            runat="server" Text="Sign Up" ToolTip="Sign Up" CssClass="registerbtn" />
                    </div>
                </ContentTemplate>
                <CustomNavigationTemplate>
                    <%--<asp:Panel ID="navig" runat="server" Visible="false">
<table border="0" cellspacing="5" style="width:100%;height:100%">
<tr align="left">
<td width="20%">
&nbsp; &nbsp;
</td>
<td align="left" colspan="0" width="80%">
</td>
</tr>
</table>
</asp:Panel>--%>
                </CustomNavigationTemplate>
            </asp:CreateUserWizardStep>
            <%-- <asp:CompleteWizardStep runat="server">
</asp:CompleteWizardStep>--%>
        </WizardSteps>
    </asp:CreateUserWizard>
</div>

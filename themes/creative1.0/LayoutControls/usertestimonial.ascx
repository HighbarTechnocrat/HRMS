<%@ Control Language="C#" AutoEventWireup="true" CodeFile="usertestimonial.ascx.cs" Inherits="themes_creative1_0_LayoutControls_usertestimonial" %>

<table class="feedback-box-content_feedback-cfc" width="100%" border="0" cellspacing="0"
        cellpadding="0" align="center">
        <tr>
            <td>
                <asp:Label ID="lblError" runat="server" Width="219px" Font-Bold="True" style="margin-left:220px"></asp:Label>
            </td>
        </tr>
               <asp:Panel ID="pnlwritetest" runat="server" >
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" align="left">
                    <tr>
                        <td align="center">
                            <tr>
                                <td align="center" valign="top">
                                    <div id="divplann" style="font-size:12px" runat="server">
                                        <span>Fields marked with (<asp:Label ID="lbl1" runat="server" Text="*"
                                            ForeColor="Red"></asp:Label>) are mandatory.</span>
                                    </div>
                                    <br />
                                    <fieldset>
                                        <legend>Testimonial Form</legend>
                                        <br />
                                        <center>
                                            <table  align="center" border="0" cellpadding="0" cellspacing="0">
                                                <tr align="left" valign="top">
                                                    <td>
                                                      Name: 
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtname" runat="server" Enabled="false" CssClass="input14"></asp:TextBox><br />
    
                                                    </td>
                                                </tr>
                                               
                                                <tr align="left" valign="top">
                                                    <td>
                                                       Subject: <font color="red">*</font>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtsubject" runat="server" TextMode="SingleLine" CssClass="input14" MaxLength="100"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtsubject"
                                                            Display="Dynamic" Font-Bold="false" Font-Size="11px" ErrorMessage="Please enter subject." ValidationGroup="validate"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr align="left" valign="top">
                                                    <td>
                                                     Comments: <font color="red">*</font> &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtcomment" runat="server" TextMode="MultiLine" CssClass="input14" onkeypress="return textboxMultilineMaxNumber(this,1000)" onpaste='return maxLengthPaste(this,"1000");'></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtcomment"
                                                            Display="Dynamic" Font-Bold="false" Font-Size="11px" ErrorMessage="Please give some comment" ValidationGroup="validate"></asp:RequiredFieldValidator>
                                                        &nbsp; &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 120px" valign="top">
                                                    </td>
                                                    <td align="left">
                                                        <asp:Button ID="btnSend" CssClass="login-box-registerbtn" Text="Send" runat="server"
                                                           ValidationGroup="validate" ToolTip="Send"/>
                                                    </td>
                                                </tr>
                                            </table>
                                        </center>
                                    </fieldset>
                                </td>
                            </tr>
                </table>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        </asp:Panel>
    </table>
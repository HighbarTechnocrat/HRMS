<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="postenquiry.aspx.cs" Inherits="postenquiry" %>

<%--<%@ Register Src="~/Themes/creative1.0/LayoutControls/basicbreadcum.ascx" TagName="basicbreadcrumb"
    TagPrefix="ucbasicbreadcrumb" %>--%>
<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript">
       jQuery("document").ready(function (e) {
        })
    </script>
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

    </script>

<%--    <ucbasicbreadcrumb:basicbreadcrumb ID="basicbreadcrumb" runat="server" />--%>

        <asp:Panel ID="pnlmsg" runat="server" Visible="false">
         <center>  
    <asp:Label ID="lblmessage" runat="server" Text="Enquiry Sent Successfully" ForeColor="Green" Font-Bold="true" Font-Size="18px"></asp:Label>  
    </center>
    </asp:Panel>
        <asp:Panel ID="pnlenquiry" runat="server" >
            <div id="postenquiry-form">
            <div class="postenquiryheading">
            
            Fields marked with an asterisk (<span>*</span>) are mandatory.
            </div>
                <div id="wrapping" class="clearfix">
                    <section class="aligned">
                        <div class="maindiv">Your Details</div>
                            <div class="fielddivbox">
        <div class="fieldnameheading"><font>*</font> Name:</div>
        <div class="fieldtextbox">
        <asp:TextBox ID="txtname" runat="server" placeholder="" autocomplete="off"
            TabIndex="1" CssClass="txtinput" MaxLength="50"></asp:TextBox><font>(Maximum 50 characters) </font>
            <br />
        
        </div>
        <div class="fieldtextboxerror">
            <asp:RequiredFieldValidator ID="reqname" ValidationGroup="postenquiry" display="dynamic"  SetFocusOnError="true" ControlToValidate="txtname" runat="server" ErrorMessage="Please enter name."></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" display="dynamic" ErrorMessage="Please enter the character only" ControlToValidate="txtname" ValidationGroup="postenquiry" ValidationExpression="^[A-Za-z ]*$" SetFocusOnError="true" ToolTip="Please enter the character only"></asp:RegularExpressionValidator>
        </div>
		</div>
                            <div class="fielddivbox">
         <div class="fieldnameheading"><font>*</font> Contact no:</div>
         <div class="fieldtxtboxcc">
         <asp:TextBox ID="txtcountrycode" runat="server" MaxLength="3" TabIndex="2" CssClass="txtinputcc txtinput"></asp:TextBox>
          <font>[eg. +91 ]</font><br /><font>(Country code)</font>
         </div>
        <div class="fieldtxtboxcn">
        <ew:NumericBox ID="txtcontact" runat="server" placeholder="" autocomplete="off"
            TabIndex="3" CssClass="txtinputcn txtinput" MaxLength="12"></ew:NumericBox>
            <font>[eg. 02221638089 or 9871408167]</font><br /><font>(Max 12 digits)</font>
            </div>
            <div class="fieldtextboxerror">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="postenquiry"  SetFocusOnError="true" display="dynamic"  ControlToValidate="txtcountrycode" runat="server" ErrorMessage="Please enter country code. "></asp:RequiredFieldValidator>
        &nbsp;&nbsp;<asp:RequiredFieldValidator ID="reqcontact" ValidationGroup="postenquiry"  SetFocusOnError="true" display="dynamic"  ControlToValidate="txtcontact" runat="server" ErrorMessage=" Please enter contact no."></asp:RequiredFieldValidator>&nbsp;&nbsp;
        
       
        </div>
		</div>
                            <div class="fielddivbox">
         <div class="fieldnameheading"><font>*</font> Email id:</div>
         <div class="fieldtextbox">
        <asp:TextBox ID="txtemailid" runat="server" placeholder="" autocomplete="off"
            TabIndex="4" CssClass="txtinput"></asp:TextBox><font>[eg. abc@xyz.com]</font><br />
            </div>
            <div class="fieldtextboxerror">
        <asp:RequiredFieldValidator ID="reqemailid" ValidationGroup="postenquiry"  SetFocusOnError="true" display="dynamic" ControlToValidate="txtemailid" runat="server" ErrorMessage="Please enter e-mail address."></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="EmailValidator" runat="server" ErrorMessage="Email address is not vaild." CssClass="registererror" ControlToValidate="txtemailid" forecolor="red" ValidationExpression="^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$" SetFocusOnError="true" ValidationGroup="postenquiry" Display="Dynamic"></asp:RegularExpressionValidator>
        </div>
        </div>
                    </section>
                    <section class="aligned">
                        <div class="maindiv">Company details (For corporate)</div>
	                        <div class="fielddivbox">
         <div class="fieldnameheading"> Company name:</div>
        <div class="fieldtextbox">
        <asp:TextBox ID="txtcname" runat="server" placeholder="" MaxLength="50" autocomplete="off" CssClass="txtinput"
            TabIndex="5"></asp:TextBox><font>(Maximum 50 characters) </font>
      <%--  <asp:RequiredFieldValidator ID="reqcname" ValidationGroup="postenquiry" display="dynamic"  SetFocusOnError="true" forecolor="red"  ControlToValidate="txtcname" runat="server" ErrorMessage="Please enter company name."></asp:RequiredFieldValidator>--%>
        </div>
</div>
                            <div class="fielddivbox">
         <div class="fieldnameheading"> Address 1:</div>
         <div class="fieldtextbox">
        <asp:TextBox ID="txtadd1" runat="server" placeholder=""  MaxLength="500" autocomplete="off"
            TabIndex="6" CssClass="txtinput"></asp:TextBox><font>(Maximum 500 characters)</font>
       <%-- <asp:RequiredFieldValidator ID="reqadd1" ValidationGroup="postenquiry"  SetFocusOnError="true" display="dynamic" forecolor="red"  ControlToValidate="txtadd1" runat="server" ErrorMessage="Please enter address 1."></asp:RequiredFieldValidator>--%>
        </div>
</div>
                            <div class="fielddivbox">
                 <div class="fieldnameheading"> Address 2:</div>
                 <div class="fieldtextbox">
        <asp:TextBox ID="txtadd2" runat="server" placeholder="" MaxLength="500" autocomplete="off" CssClass="txtinput"
            TabIndex="7"></asp:TextBox><font>(Maximum 500 characters)</font> 
            </div>
</div>
                            <div class="fielddivbox">
             <div class="fieldnameheading"> Post code:</div>
             <div class="fieldtextbox">
         <ew:NumericBox ID="txtpostcode" runat="server" placeholder="" MaxLength="6" autocomplete="off" CssClass="txtinput"
            TabIndex="8"></ew:NumericBox><font>(Max 6 digits)</font> 
            </div>
            </div>
                            <div class="fielddivbox">
            <div class="fieldnameheading"> Country:</div>
            
             <asp:UpdatePanel ID="Upl_country" runat="server">
             
                        <ContentTemplate>
                        <div class="fieldtextbox">
        <asp:DropDownList ID="ddlcountry" runat="server" placeholder="" AutoPostBack="true" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged" autocomplete="off" CssClass="selmenu"
            TabIndex="9">
        </asp:DropDownList>
        </div>

        <div class="fieldtextboxerror">
        <%-- <asp:RequiredFieldValidator CssClass="registererror" ID="validator_ddlcountry"
                                                                        runat="server" ErrorMessage="Please select country" ControlToValidate="ddlcountry"
                                                                        SetFocusOnError="true" Display="Dynamic" InitialValue="0" ValidationGroup="postenquiry" style="float:left;margin-left:2px;">
                                                                    </asp:RequiredFieldValidator>--%>
                                                                            </div>
                                                                     </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlcountry" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>

</div>
                            <div class="fielddivbox">
         <div class="fieldnameheading"> State:</div>
        
          <asp:UpdatePanel ID="Upl_state" runat="server">
                        <ContentTemplate>
                         <div class="fieldtextbox">
        <asp:DropDownList ID="ddlstate"  runat="server" placeholder="Select State" Enabled="false" autocomplete="off" AutoPostBack="true"
        OnSelectedIndexChanged="ddlstate_SelectedIndexChanged" CssClass="selmenu"
            TabIndex="10">
        </asp:DropDownList>
           </div>
        <div class="fieldtextboxerror">
      <%--  <asp:RequiredFieldValidator CssClass="registererror" ID="RequiredFieldValidator3"
                                                                        runat="server" ErrorMessage="Please select state" ControlToValidate="ddlstate"
                                                                        SetFocusOnError="true" Display="Dynamic" InitialValue="0" ValidationGroup="postenquiry" style="float:left;margin-left:2px;">
                                                                    </asp:RequiredFieldValidator>--%>

                                                                    </div>
                                                                     </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlstate" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
     
</div>
                            <div class="fielddivbox">
         <div class="fieldnameheading"> City:</div>
         
          <asp:UpdatePanel ID="Upl_city" runat="server">
                        <ContentTemplate>
                        <div class="fieldtextbox">
        <asp:DropDownList ID="ddlcity" runat="server" placeholder="Select City"  Enabled="false" autocomplete="off" CssClass="selmenu"
            TabIndex="11">
        </asp:DropDownList>
        </div>
        <div class="fieldtextboxerror">

       <%--  <asp:RequiredFieldValidator CssClass="registererror" ID="RequiredFieldValidator5"
                                                                        runat="server" ErrorMessage="Please select city" ControlToValidate="ddlcity"
                                                                        SetFocusOnError="true" Display="Dynamic" InitialValue="0" ValidationGroup="postenquiry" style="float:left;margin-left:2px;">
</asp:RequiredFieldValidator>--%>
</div>
 </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlcity" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
        
        </div>
                            <div class="fielddivbox">

         <div class="fieldnameheading">Preferred method of contact:</div>
       <div class="fieldtextbox">
        <asp:DropDownList ID="ddlmcontact" runat="server" placeholder="Preferred method of contact" autocomplete="off"
            TabIndex="12" CssClass="selmenu">
        <asp:ListItem Text="Email" Value="Email"></asp:ListItem>
        <asp:ListItem Text="SMS" Value="SMS"></asp:ListItem>
        <asp:ListItem Text="Call" Value="Call"></asp:ListItem>
        </asp:DropDownList>
        </div>
</div>
	                        <div class="fielddivbox">
                <div class="fieldnameheading">Mobile / Telephone:</div>
                <div class="fieldtxtboxcc">
                  <asp:TextBox ID="txtccode" runat="server" MaxLength="3" CssClass="txtinputcc txtinput" ></asp:TextBox>
                    <font>[eg. +91 ]</font><br /><font>(Country code)</font>
                 
            </div>
            <div class="fieldtxtboxcn">
                <ew:NumericBox ID="txtcmobile" runat="server" placeholder="" MaxLength="12" autocomplete="off" CssClass="txtinputcn txtinput"
            TabIndex="15"></ew:NumericBox>
            <font>[eg. 02221638089 or 9871408167]</font><br /><font>(Max 12 digits)</font>

            </div>
            </div>
                            <div class="fielddivbox">
             <div class="fieldnameheading">Email id:</div>
             <div class="fieldtextbox">
        <asp:TextBox ID="txtcemail" runat="server" placeholder="" autocomplete="off" CssClass="txtinput"
            TabIndex="16"></asp:TextBox><font>[eg. abc@xyz.com]</font>

             <asp:RegularExpressionValidator ID="EmailValidator1" runat="server" ErrorMessage="Email id is not vaild." CssClass="registererror" ControlToValidate="txtcemail" ValidationExpression="^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$" SetFocusOnError="true" Style="margin-left:155px;margin-top:-15px;float:left" ValidationGroup="postenquiry" forecolor="red" Display="Dynamic"></asp:RegularExpressionValidator>
             </div>
             </div>
	                </section>
                    <section class="aligned">
                        <div class="maindiv">Event details:</div>
                        <div class="fielddivbox">
    
        <div class="fieldcal">
        <div class="fieldnameheading">&nbsp;</div>
        <font style="color:#ff0000;font-size:24px;float:left;">*&nbsp;</font> <asp:Label ID="lblestart" runat="server" Text="Event Start Date:"></asp:Label>
        <asp:TextBox ID="txtstartdate" runat="server" TabIndex="17" ></asp:TextBox>
        <asp:ImageButton ID="btnfromdate" runat="server" ImageUrl="~/themes/creative1.0/images/Calendar_image.png" style="width:16px !important;height:16px !important;border:0;box-shadow:none;float:left;margin-right:10px;" />
        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtstartdate"
            PopupButtonID="btnfromdate" Format="M/d/yyyy">
        </ajaxToolkit:CalendarExtender>
        <asp:RequiredFieldValidator ID="reqstartdate" ValidationGroup="postenquiry"  SetFocusOnError="true" display="dynamic"  ControlToValidate="txtstartdate" runat="server" ErrorMessage="Please enter Event Start Date." style="width:220px;" CssClass="fieldcalerror"></asp:RequiredFieldValidator>
         <asp:CompareValidator ID="CompareValidator5" ControlToValidate="txtstartdate" Type="Date"
                                    Operator="DataTypeCheck" runat="server" ForeColor="Red" ValidationGroup="ValGrpAddProduct" display="dynamic" style="width:220px;"  CssClass="fieldcalerror"  ErrorMessage="Value is not in Date format"
                                    SetFocusOnError="True"></asp:CompareValidator>
     <asp:Label ID="lblstartdate" runat="server" Visible="false" Text=""  style="width:313px;" CssClass="fieldcalerror"></asp:Label>
       </div>
       
     
       <div class="fieldcal">
       <div class="fieldnameheading">&nbsp;</div>
        <font style="color:#ff0000;font-size:24px;float:left;">*&nbsp;</font> <asp:Label ID="lbleend" runat="server" Text="Event End Date:"></asp:Label>
        <asp:TextBox ID="txtenddate" runat="server" TabIndex="18"></asp:TextBox>
        <asp:ImageButton ID="btnenddate" runat="server" ImageUrl="~/themes/creative1.0/images/Calendar_image.png" style="width:16px !important;height:16px !important;border:0;box-shadow:none;float:left;margin-right:10px;" />
        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtenddate"
            PopupButtonID="btnenddate" Format="M/d/yyyy">
        </ajaxToolkit:CalendarExtender>
    
       <asp:RequiredFieldValidator ID="reqenddate" ValidationGroup="postenquiry" display="dynamic"  SetFocusOnError="true"  ControlToValidate="txtenddate" runat="server" ErrorMessage="Please enter Event End Date." style="width:220px;" CssClass="fieldcalerror"></asp:RequiredFieldValidator>
       <asp:CompareValidator ID="CompareValidator1" ControlToValidate="txtenddate" Type="Date"
                                    Operator="DataTypeCheck" runat="server" ValidationGroup="ValGrpAddProduct" display="dynamic" style="width:220px;"  CssClass="fieldcalerror" ErrorMessage="Value is not in Date format"
                                    SetFocusOnError="True"></asp:CompareValidator>
 <asp:Label ID="lblenddate" runat="server" Visible="false" Text=""  style="width:313px;" CssClass="fieldcalerror"></asp:Label>
       </div>
       </div>
                        <div class="fielddivbox">
        <div class="fieldnameheading"><font>*</font> Number of attendees:</div>
        <div class="fieldtextbox">
        <ew:NumericBox ID="txtnattence" runat="server" placeholder="" autocomplete="off" MaxLength="8" CssClass="txtinput"
            TabIndex="19"></ew:NumericBox><font>(Max 8 digits) </font>
            </div>
            <div class="fieldtextboxerror">
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="postenquiry" display="dynamic"  SetFocusOnError="true" forecolor="red"  ControlToValidate="txtnattence" runat="server" ErrorMessage="Please enter number of attendees."></asp:RequiredFieldValidator>
        </div>
            </div>
                        <div class="fielddivbox">
              <div class="fieldnameheading"><font>*</font> Venue:</div>
              <div class="fieldtextbox">
        <asp:TextBox ID="txtvenue" runat="server" placeholder="" autocomplete="off" CssClass="txtblock"
            TabIndex="20" TextMode="MultiLine"  onkeypress="return textboxMultilineMaxNumber(this,500)" onpaste='return maxLengthPaste(this,"500");' Rows="4"></asp:TextBox>
            <font>(Maximum 500 characters) </font>
            </div>
            <div class="fieldtextboxerror">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="postenquiry" display="dynamic"  SetFocusOnError="true" ControlToValidate="txtvenue" runat="server" ErrorMessage="Please enter venue."></asp:RequiredFieldValidator>
            </div>
</div>
                        <div class="fielddivbox">
        <div class="fieldnameheading"><font>*</font> Event type:</div>
        <div class="fieldtextbox">
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
          <asp:ListBox ID="ddletype1" runat="server" placeholder="" SelectionMode="multiple" AutoPostBack="true" OnSelectedIndexChanged="ddletype_SelectedIndexChanged" autocomplete="off" CssClass="selmenu"
            TabIndex="21">
        </asp:ListBox>
         <font>(Use Ctrl or Shift button for multiple selection for event type)</font>
         
         </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddletype1" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>

                     <div class="fieldtextboxerror">
            <asp:RequiredFieldValidator CssClass="registererror" ID="RequiredFieldValidator6"
                                                                        runat="server" ErrorMessage="Please select event type" ControlToValidate="ddletype1"
                                                                        SetFocusOnError="true" Display="Dynamic"  ValidationGroup="postenquiry" style="float:left;margin-left:2px;"></asp:RequiredFieldValidator>
</div>
       
        </div>
</div>

   <div class="fielddivbox">
        <div class="fieldnameheading"> <font>*</font> Product:</div>
        <div class="fieldtextbox">
           <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
        <asp:ListBox ID="ddlproductlist" runat="server" placeholder="Select Product" SelectionMode="multiple" autocomplete="off" CssClass="selmenu"
            TabIndex="22">        
        </asp:ListBox>
        <font>(Use Ctrl or Shift button for multiple selection for product)</font>
         </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlproductlist" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>

                      <div class="fieldtextboxerror">
            <asp:RequiredFieldValidator CssClass="registererror" ID="RequiredFieldValidator7"
                                                                        runat="server" ErrorMessage="Please select product" ControlToValidate="ddlproductlist"
                                                                        SetFocusOnError="true" Display="Dynamic"  ValidationGroup="postenquiry" style="float:left;margin-left:2px;"></asp:RequiredFieldValidator>
</div>
        </div>
</div>

                        <div class="fielddivbox">
        <div class="fieldnameheading"><font>*</font> Layout:</div>
        <div class="fieldtextbox">
        <asp:DropDownList ID="ddlLayout" runat="server" placeholder="Select Layout" autocomplete="off" CssClass="selmenu"
            TabIndex="23">
        <asp:ListItem Text="Auditorium Seating" Value="A"></asp:ListItem>
        <asp:ListItem Text="C-shape Seating" Value="C"></asp:ListItem>
        <asp:ListItem Text="Cocktail Seating" Value="Co"></asp:ListItem>
        <asp:ListItem Text="Theatre Style Seating" Value="T"></asp:ListItem>
        </asp:DropDownList>
        </div>
</div>
                        <div class="fielddivbox">

        <div class="fieldnameheading"><font>*</font> AV required:</div>
        <div class="fieldtextbox">
        <asp:DropDownList ID="ddlavrequired" runat="server" placeholder="Select Layout" autocomplete="off" CssClass="selmenu"
            TabIndex="24">
        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
        <asp:ListItem Text="No" Value="No"></asp:ListItem>
        </asp:DropDownList>
        </div>

</div>
                        <div class="fielddivbox">

<div class="fieldnameheading"><font>*</font> Budget from:</div>
<div class="fieldtextbox">
        <ew:NumericBox ID="txtbfrom" runat="server" TabIndex="25"  placeholder="" autocomplete="off" CssClass="txtinput" MaxLength="12"></ew:NumericBox><font>(Max 12 digits) </font>
        </div>
        <div class="fieldtextboxerror">
         <asp:RequiredFieldValidator ID="reqbudgetfrom" ValidationGroup="postenquiry" display="dynamic"  SetFocusOnError="true" forecolor="red" ControlToValidate="txtbfrom" runat="server" ErrorMessage="Please enter budget from."></asp:RequiredFieldValidator>
         </div>
         <asp:Label ID="lblbudgetfrom" runat="server" Visible="false" Text=""  style="width:313px;" CssClass="fieldcalerror"></asp:Label>
</div>
                        <div class="fielddivbox">
<div class="fieldnameheading"><font>*</font> Budget to:</div>
<div class="fieldtextbox">
        <ew:NumericBox ID="txtbto" runat="server"  placeholder="" autocomplete="off" CssClass="txtinput" MaxLength="12" TabIndex="26"></ew:NumericBox><font>(Max 12 digits)</font>
        </div>
        <div class="fieldtextboxerror">
        <asp:RequiredFieldValidator ID="reqbudgetto" ValidationGroup="postenquiry" display="dynamic"  SetFocusOnError="true" forecolor="red" ControlToValidate="txtbto" runat="server" ErrorMessage="Please enter budget to."></asp:RequiredFieldValidator>
        </div>
        <asp:Label ID="lblbudgetto" runat="server" Visible="false" Text=""  style="width:313px;" CssClass="fieldcalerror"></asp:Label>
</div>
                    </section>
                    <section>
                        <div class="fielddivbox">
        <div class="fieldnameheading">&nbsp;</div>
        <div class="submitbtndiv">
        <asp:Button ID="submitbtn" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="postenquiry"
                OnClick="submitbtn_Click" CssClass="submitbtn" TabIndex="27" />
                </div>
                <div class="cancelbtndiv">
        <asp:Button ID="btncancel1" runat="server" Text="Cancel" ToolTip="Cancel" OnClick="btncancel_Click" TabIndex="28" CssClass="cancelbtn"></asp:Button>
                </div>
        </div>
                    </section>
    </section>
                </div>
            </div>
            </asp:Panel>

       <%--</ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlcountry" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlstate" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlcity" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddletype1" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>--%>
</asp:Content>

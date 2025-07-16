<%@ Control Language="C#" AutoEventWireup="true" CodeFile="prodenquiry.ascx.cs" Inherits="themes_creative1_0_LayoutControls_prodenquiry" %>


    <%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<script type="text/javascript">
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


    function Count1(text) {
        //asp.net textarea maxlength doesnt work; do it by hand
        var maxlength = 500; //set your value here (or add a parm and pass it in)
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
<div class="postenquiryheading">Product Enquiry</div>
<asp:Panel ID="pnlmsg" runat="server" Visible="false">
         <center>  
    <asp:Label ID="lblmessage" runat="server" Text="Enquiry Sent Successfully" CssClass="enquirysentsuccessfully"></asp:Label>  
    </center>
    </asp:Panel>

 <asp:Panel ID="pnlenquiry" runat="server" >
<div id="postenquiry-form">
   

   <div class="postenquirynote">
           Note: All the fields mention below will be mandatory.
            </div>

 <div id="wrapping" class="clearfix">
 <section class="aligned">
  <div class="fielddivbox">
        <div class="fieldnameheading">User name:</div>
        <div class="fieldtextbox">
        <asp:TextBox ID="txtname" runat="server" placeholder="" 
            TabIndex="1" CssClass="txtinput" MaxLength="50"></asp:TextBox> 
            <br>
            <font>(Maximum 50 characters) </font>
        </div>
        <div class="fieldtextboxerror">
            <asp:RequiredFieldValidator ID="reqname" ValidationGroup="postenquiry" display="dynamic"  SetFocusOnError="true" ControlToValidate="txtname" runat="server" ErrorMessage="Please enter user name."></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" display="dynamic" ErrorMessage="Enter the character only" ControlToValidate="txtname" ValidationGroup="postenquiry" ValidationExpression="^[A-Za-z ]*$" SetFocusOnError="true" ToolTip="Enter the character only"></asp:RegularExpressionValidator>
        </div>
		</div>


         <div class="fielddivbox">
         <div class="fieldnameheading">Email id:</div>
         <div class="fieldtextbox">
        <asp:TextBox ID="txtemailid" runat="server" placeholder="" autocomplete="off"
            TabIndex="2" CssClass="txtinput"></asp:TextBox> 
            <br>
            <font>[eg. abc@xyz.com]</font>
            </div>
           <div class="fieldtextboxerror">
        <asp:RequiredFieldValidator ID="reqemailid" ValidationGroup="postenquiry"  SetFocusOnError="true" display="dynamic" ControlToValidate="txtemailid" runat="server" ErrorMessage="Please enter email id."></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="EmailValidator" runat="server" ErrorMessage="Email id is not vaild." CssClass="registererror" ControlToValidate="txtemailid" forecolor="red" ValidationExpression="^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$" SetFocusOnError="true" ValidationGroup="postenquiry" Display="Dynamic"></asp:RegularExpressionValidator>
        </div>
        </div>
         <div class="fielddivbox">
         <div class="fieldnameheading">Address:</div>
         <div class="fieldtextbox">
        <asp:TextBox ID="txtadd" runat="server" placeholder="" TextMode="MultiLine"  MaxLength="100" autocomplete="off"
            TabIndex="3" CssClass="txtinput" onKeyUp="javascript:Count(this);" onChange="javascript:Count(this);" Rows="4"></asp:TextBox> 
        <br>    
        <font>(Maximum 100 characters)</font>
        </div>
        <div class="fieldtextboxerror">
        <asp:RequiredFieldValidator ID="reqadd1" ValidationGroup="postenquiry"  SetFocusOnError="true" display="dynamic" forecolor="red"  ControlToValidate="txtadd" runat="server" ErrorMessage="Please enter address ."></asp:RequiredFieldValidator>
        </div>
        </div>
         <div class="fielddivbox">
         <div class="fieldnameheading">Contact no:</div>       
        <div class="fieldtxtboxcn">
        <ew:NumericBox ID="txtcontact" runat="server" placeholder="" autocomplete="off"
            TabIndex="4" CssClass="txtinputcn txtinput" MaxLength="12"></ew:NumericBox>
            <br>
           <font>[eg. 02221638089 or 9871408167] (Maximum 12 digits)</font>
           </div>
           <div class="fieldtextboxerror">
          <asp:RequiredFieldValidator ID="reqcontact" ValidationGroup="postenquiry"  SetFocusOnError="true" display="dynamic"  ControlToValidate="txtcontact" runat="server" ErrorMessage=" Please enter contact no."></asp:RequiredFieldValidator>&nbsp;&nbsp;               
        </div>
		</div>
        

         <div class="fielddivbox">
        <div class="fieldnameheading">Product name:</div>
        <div class="fieldtextbox">
        <asp:Label ID="txtproduct" runat="server" placeholder="" 
            TabIndex="5" CssClass="proname" ></asp:Label>
            <br />        
        </div>
		</div>


         <div class="fielddivbox">
              <div class="fieldnameheading">Comment:</div>
              <div class="fieldtextbox">
        <asp:TextBox ID="txtcomment" runat="server" placeholder="" autocomplete="off" CssClass="txtblock"
            TabIndex="6" TextMode="MultiLine" onKeyUp="javascript:Count1(this);" onChange="javascript:Count1(this);" Rows="4"></asp:TextBox>
            <br>
             <font>(Maximum 500 characters) </font>
              
            </div>
          <div class="fieldtextboxerror">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="postenquiry" display="dynamic"  SetFocusOnError="true" ControlToValidate="txtcomment" runat="server" ErrorMessage="Please enter comment."></asp:RequiredFieldValidator>
            </div>
            </div>


        <div class="fielddivbox">
        <div class="fieldnameheading">&nbsp;</div>
           <div class="submitbtndiv">
        <asp:Button ID="submitbtn" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="postenquiry" OnClick="btnsubmit_Click"
               CssClass="submitbtn" TabIndex="6" />
                </div>
                <div class="cancelbtndiv">
        <asp:Button ID="btncancel1" runat="server" Text="Cancel" ToolTip="Cancel" OnClick="btncancel_Click" TabIndex="28" CssClass="cancelbtn"></asp:Button>
                </div>

                </div>

</section>

 </div>

 </div>

 </asp:Panel>
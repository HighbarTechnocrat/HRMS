<%@ Control Language="C#" AutoEventWireup="true" CodeFile="checkout.ascx.cs" Inherits="themes_creative1_LayoutControls_checkout" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<script type="text/javascript">    function allownumbers(c) { var a = window.event ? c.keyCode : c.which; var d = String.fromCharCode(a); var b = new RegExp("[0-9]"); if (c.keyCode == 8) { d = String.fromCharCode(a); return d } else { if (c.keyCode == 46) { d = String.fromCharCode(a); return d } else { if (c.keyCode == 35) { d = String.fromCharCode(a); return d } else { if (c.keyCode == 36) { d = String.fromCharCode(a); return d } else { if (c.keyCode == 37) { d = String.fromCharCode(a); return d } else { if (c.keyCode == 38) { d = String.fromCharCode(a); return d } else { if (c.keyCode == 39) { d = String.fromCharCode(a); return d } else { if (c.keyCode == 40) { d = String.fromCharCode(a); return d } else { return b.test(d) } } } } } } } } } function Showalert() { alert("Invalid quantity") } function MainChanges() { };

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

<div class="cartlistpagesheading">
           Checkout
        </div>
<div class="checkoutpagediv">
<div class="checkouttabs">
<ul class="tabs">
<li id="tab_1" runat="server">
<div class="tabno">
<span>1</span></div>
<asp:Button ID="Tab1" runat="server" Text="Shipping Address" OnClick="Tab1_Click">
</asp:Button></li>
<li id="tab_2" runat="server">
<div class="tabno">
<span>2</span></div>
<asp:Button ID="Tab2" runat="server" Text="Order Summary" OnClick="Tab2_Click"></asp:Button></li>
<asp:Panel ID="pnltab3" runat="server" Visible="false">
<li id="tab_3" runat="server">

<div class="tabno">
<span>3</span></div>
<asp:Button ID="Tab3" runat="server" Text="Payment Options" OnClick="Tab3_Click">
</asp:Button></li>
</asp:Panel>
</ul>
<div class="tab_container">
<asp:MultiView ID="MainView" runat="server">
<asp:View ID="View1" runat="server">
<div class="shippingaddressdiv">
<div class="savedshippingaddress">
<div class="address-seperator">
<asp:Panel ID="pnlor" runat="server" Visible="false" >
<div class="address-seperator-text">
or</div>
</asp:Panel>
<div class="address-seperator-line">
</div>
</div>
<div class="saved-shippingaddress-div">
<asp:Panel ID="pnlprevadd" runat="server" Visible="false" >
<div class="saved-address-heading">
Select from previous shipping addresses</div>
</asp:Panel>


<asp:Repeater ID="rptdefaultadd" runat="server" OnItemCommand="rptdefaultaddress_ItemCommand" >
<ItemTemplate>
<div class="prev-saved-address">
<a href="javascript:void(0)">
<div class="saved-address">

<asp:LinkButton ID="lnkaddress" runat="server" CommandName="address" CommandArgument='<%# Eval("indexid") %>' ToolTip="Select">
<div class="saved-address-name">
<%# DataBinder.Eval(Container, "DataItem.firstname") %>&nbsp;<%# DataBinder.Eval(Container, "DataItem.lastname") %></div>
<div class="saved-address-adrs">
<%# DataBinder.Eval(Container, "DataItem.address") %></div>
<div class="saved-address-checkbox">
&nbsp;</div>

</asp:LinkButton>
</div>
</a>
</div>
</ItemTemplate>
</asp:Repeater>




<asp:Repeater ID="rptaddress" runat="server" OnItemCommand="rptaddress_ItemCommand">
<ItemTemplate>
<div class="prev-saved-address">
<a href="javascript:void(0)">
<div class="saved-address">
<div class="removeshipadd">
<asp:LinkButton ID="lnkaddressremove" runat="server" Text="Remove" CommandName="removeaddress" CommandArgument='<%# Eval("indexid") %>' ToolTip="Remove" CssClass="removeshipaddbtn"></asp:LinkButton>
</div>
<asp:LinkButton ID="lnkaddress" runat="server" CommandName="address" CommandArgument='<%# Eval("indexid") %>' ToolTip="Select">
<div class="saved-address-name">
<%# DataBinder.Eval(Container, "DataItem.firstname") %>&nbsp;<%# DataBinder.Eval(Container, "DataItem.lastname") %></div>
<div class="saved-address-adrs">
<%# DataBinder.Eval(Container, "DataItem.address") %></div>
<div class="saved-address-checkbox">
&nbsp;</div>

</asp:LinkButton>
</div>
</a>
</div>
</ItemTemplate>
</asp:Repeater>
</div>
</div>
<div class="newshippingaddress">
<div class="newshippingaddress-heading">
Enter a new shipping address</div>

<div class="postenquiryheading">
 Fields marked with an asterisk (<span>*</span>) are mandatory.
</div>

<form>
<div class="newshippingaddressformbox">
<label class="newshippingaddresslabel">
<font>*</font> First Name:</label>
<asp:TextBox ID="txtfirstname" runat="server" class="newshippingaddressinput" MaxLength="20">
</asp:TextBox>
<font>(Maximum 20 characters)</font><br />
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtfirstname" ErrorMessage="Enter the first name" CssClass="formerror" runat="server" Display="Dynamic"  SetFocusOnError="true" ValidationGroup="Group1"></asp:RequiredFieldValidator>
 <asp:RegularExpressionValidator ID="reqchar1" runat="server" ErrorMessage="Enter the character only"
                            ControlToValidate="txtfirstname" ValidationGroup="Group1"
                            ValidationExpression="^[A-Za-z ]*$" CssClass="formerror" Display="Dynamic"  SetFocusOnError="true"></asp:RegularExpressionValidator>
</div>
<div class="newshippingaddressformbox">
<label class="newshippingaddresslabel">
<font>*</font> Last Name:</label>
<asp:TextBox ID="txtlastname" runat="server" class="newshippingaddressinput" Columns="30" MaxLength="20">
</asp:TextBox>
<font>(Maximum 20 characters)</font><br />
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtlastname" ErrorMessage="Enter the last name" CssClass="formerror" runat="server" Display="Dynamic" ValidationGroup="Group1"></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="reqchar2" runat="server" ErrorMessage="Enter the character only"
                            ControlToValidate="txtlastname" ValidationGroup="Group1"
                            ValidationExpression="^[A-Za-z ]*$" CssClass="formerror" Display="Dynamic" SetFocusOnError="true"></asp:RegularExpressionValidator>
</div>
<div class="newshippingaddressformbox">
<label class="newshippingaddresslabel">
<font>*</font> Pincode:</label>
<ew:NumericBox ID="txtpincode" runat="server" CssClass="newshippingaddressinput" MaxLength="6">
</ew:NumericBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtpincode" ErrorMessage="Enter the pin code" runat="server" CssClass="formerror" Display="Dynamic" ValidationGroup="Group1"></asp:RequiredFieldValidator>
</div>
<div class="newshippingaddressformbox">
<label class="newshippingaddresslabel">
<font>*</font> Address:</label>
<asp:TextBox ID="txtaddress1" runat="server" MaxLength="100" TextMode="MultiLine" onKeyUp="javascript:Count(this);" onChange="javascript:Count(this);" CssClass="newshippingaddressinput" Style="height:100px"></asp:TextBox>
<font>(Maximum 100 characters)&nbsp;&nbsp;</font><br />
<asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtaddress1" ErrorMessage="Enter the address" runat="server" CssClass="formerror" Display="Dynamic" ValidationGroup="Group1"></asp:RequiredFieldValidator>
</div>
<div class="newshippingaddressformbox">
<label class="newshippingaddresslabel">
&nbsp;&nbsp;&nbsp;Landmark:</label>
<asp:TextBox ID="txtlandmark" runat="server" CssClass="newshippingaddressinput" MaxLength="50">
</asp:TextBox>
<font>(Maximum 50 characters)&nbsp;&nbsp;</font><br />

</div>
<div class="newshippingaddressformbox">
<label class="newshippingaddresslabel">
<font>*</font> Country:</label>
 <asp:UpdatePanel ID="UpdatePanel_country" runat="server">
                            <ContentTemplate>
<asp:DropDownList ID="ddlcountry" CssClass="newshippingaddressinput" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged" AutoPostBack="true" runat="server">
</asp:DropDownList>
<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please Select Country" ControlToValidate="ddlcountry" CssClass="formerror" Display="Dynamic" ValidationGroup="Group1" InitialValue="0" SetFocusOnError="true" Font-Bold="false"> </asp:RequiredFieldValidator>
</ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlcountry" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
</div>
<div class="newshippingaddressformbox">
<label class="newshippingaddresslabel">
<font>*</font> State:</label>
 <asp:UpdatePanel ID="UpdatePanel_state" runat="server">
                            <ContentTemplate>
<asp:DropDownList ID="ddlstate" CssClass="newshippingaddressinput" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged" AutoPostBack="true" runat="server">
</asp:DropDownList>
<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please Select State" ControlToValidate="ddlstate" Display="Dynamic" CssClass="formerror" ValidationGroup="Group1" InitialValue="0"  SetFocusOnError="true" Font-Bold="false"></asp:RequiredFieldValidator>
 </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlstate" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
</div>
<div class="newshippingaddressformbox">
<label class="newshippingaddresslabel">
<font>*</font> City:</label>
  <asp:UpdatePanel ID="UpdatePanel_city" runat="server">
                            <ContentTemplate>
<asp:DropDownList ID="ddlcity1" CssClass="newshippingaddressinput" AutoPostBack="true" runat="server">
</asp:DropDownList>
<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please Select City" ControlToValidate="ddlcity1" Display="Dynamic" CssClass="formerror" ValidationGroup="Group1" InitialValue="0"  SetFocusOnError="true" Font-Bold="false"></asp:RequiredFieldValidator>
  </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlcity1" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
</div>
<div class="newshippingaddressformbox">
<label class="newshippingaddresslabel">
<font>*</font> Mobile No:</label>
<input class="newshippingaddresscountrycode" type="text" value="+91" id="txtcountrycode" runat="server" visible="false">
<ew:NumericBox runat="server" ID="txtmobile" CssClass="newshippingaddressinput" Text="" MaxLength="12"></ew:NumericBox>
 <font>[eg. 9871408167]</font><br /><font>(Max 12 digits)</font><br />
<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please enter Mobile no" ControlToValidate="txtmobile" Display="Dynamic" CssClass="formerror" Font-Bold="false" ValidationGroup="Group1"></asp:RequiredFieldValidator>

</div>
<div class="newshippingaddressbtn">
<asp:Button ID="btntab1save" runat="server" Text="Save &amp; Continue" ValidationGroup="Group1" ToolTip="Save &amp; Continue" OnClick="btntab1save_Click" CssClass="saveandcontbtn"/>
</div>
</form>
</div>
</div>
</asp:View>
<asp:View ID="View2" runat="server">
<div class="revieworder">
<div class="revieworder-text">
Review your Order</div>
<div class="productdeletemsg">
<asp:Label ID="deletemsg" runat="server" Text="product successfully deleted from the list"></asp:Label></div>
<table border="0" class="productordersummery">
<tr>
<th width="10%" scope="col">
Image
</th>
<th align="left" width="50%" scope="col">
Item Description
</th>
<th align="left" width="10%" scope="col">
Quantity
</th>
<th width="13%" scope="col" id="thprice" runat="server" visible="false">
Price
</th>
<th width="13%" scope="col">
Price
</th>
<th width="4%" scope="col">
</th>
</tr>
<asp:Repeater runat="server" ID="rptcartlist" OnItemCommand="rptcartlist_ItemCommand" OnItemDataBound="rptcartlist_ItemDataBound">
<ItemTemplate>
<tr>
<td align="center">
<asp:Label ID="lblid" runat="server" Text='<%# Eval("productname") %>' Visible="false"></asp:Label>
<asp:Label ID="lblproductid" runat="server" Text='<%# Eval("productid") %>' Visible="false"></asp:Label>
<asp:Label ID="lblpnumber" runat="server" Text='<%# Eval("productnumber") %>' Visible="false"></asp:Label>
<asp:Label ID="lblshiprate" runat="server" Text='<%# Eval("shippingrate") %>' Visible="false"></asp:Label>

<asp:HyperLink runat="server" ID="hlnkimage" NavigateUrl='<%# CheckCorrectAnswer(Eval("Productname"), Eval("productid")) %>'>
<img src='<%= ConfigurationManager.AppSettings["adminsitepath"]%>images/smallproduct/<%# Eval("smallimage") %>' alt="" />
</asp:HyperLink>
</td>
<td>
<%# Eval("productname") %>
</td>
<td class="quantity" >
<asp:TextBox ID="txtquantity" runat="server" Text='<%# Bind("Qty") %>' CssClass="inputbox" onkeypress="javascript:return allownumbers(event)" MaxLength="3"></asp:TextBox>
<asp:LinkButton ID="lnkchange" runat="server" CommandName="change" CommandArgument='<%# Eval("id") %>' Text="Change" ToolTip="Update Quantity"></asp:LinkButton>
</td>
<td align="center" id="tdprice" runat="server" visible="false">
$
<asp:Label ID="lblourprice" runat="server" Text='<%# Bind("ourprice","{0:F0}") %>'></asp:Label>
</td>
<td align="center">
$
<asp:Label ID="lblsubtotal" runat="server" Text=""></asp:Label>
</td>
<td align="center">
<asp:LinkButton ID="lnkdelete" runat="server" CommandName="delete" CommandArgument='<%# Eval("id") %>'>
<div class="wishlistdeleteproduct">
</div></asp:LinkButton>
</td>
</tr>
</ItemTemplate>
</asp:Repeater>
</table>



</div>
<div class="amountpayble">
<div id="divstotal" runat="server" visible="false">
Sub Total: 
<div>
<span>$</span>
<asp:Label ID="lblsubtotal" runat="server" Text=""></asp:Label>
</div>
</div>
<div id="divsrate" runat="server" visible="false">
Shipping Rate:
<div>
<span>$</span>
<asp:Label ID="lblsrate" runat="server" Text=""></asp:Label>
</div>
</div>
<div>
Total:
<div>
<span>$</span>
<asp:Label ID="lblamtpay" runat="server" Text=""></asp:Label>
</div>
</div>
</div>

<div class="sendsms" id="divsms" runat="server" visible="false">
<div class="sms">
<asp:CheckBox ID="chkordersms" runat="server" Checked="true" />
Send Order Confirmation SMS alert to&nbsp;&nbsp;
</div>
<div class="mobileno">
<input type="text" value="+91" style="width:5%" >
<asp:TextBox ID="txtsendordersms" runat="server"></asp:TextBox>
</div>
</div>
<div class="ordersummeryContinuebtn">
<asp:Button ID="btncontinue" runat="server" Text="Continue" ToolTip="Continue" OnClick="btncontinue_Click" CssClass="continuebtn" />
</div>
</asp:View>
<asp:View ID="View3" runat="server">
<div class="paymentoptiondiv">
<div class="paymentoptiontable">
<asp:RadioButtonList ID="rbtpaymentlist" runat="server">
<asp:ListItem Text="Credit Card" Selected="True" Value="CC"></asp:ListItem>
<asp:ListItem Text="Debit Card" Value="DC"></asp:ListItem>
<asp:ListItem Text="Net Banking" Value="NB"></asp:ListItem>

</asp:RadioButtonList>
</div>
<div class="ordersummeryContinuebtn">
<asp:Button ID="btnpaycontinue" runat="server" Text="Place Order" ToolTip="Place Order" OnClick="btnpaycontinue_Click" CssClass="placeorderbtn" />
</div>
</div>
</asp:View>
</asp:MultiView>
</div>
</div>
<div class="orderdetail">
<div class="orderdetail-heading">
Order Summery</div>
<div class="orderdetail-summery">
<div class="orderdetail-items">
<div class="orderdetail-itemstext">
Items</div>
<div class="orderdetail-itemstext">
:
<asp:Label ID="lbltotalitem" runat="server"></asp:Label></div>
</div>


<div class="orderdetail-items">
<div class="orderdetail-itemstext">
Sub Total</div>
<div class="orderdetail-itemstext">
: $
<asp:Label ID="lbltotalsubtotal" runat="server"></asp:Label></div>
</div>
<div class="orderdetail-items" id="divship" runat="server" visible="false">
<div class="orderdetail-itemstext">
Shipping rate</div>
<div class="orderdetail-itemstext">
: $
<asp:Label ID="lblship" runat="server"></asp:Label></div>
</div>

<div class="orderdetail-items">
<div class="orderdetail-itemstext">
Total</div>
<div class="orderdetail-itemstext">
: $
<asp:Label ID="lblgrandtotal" runat="server"></asp:Label></div>
</div>
</div>
</div>
<asp:Panel runat="server" ID="pnlshipaddresss" Visible="false">
<div class="addrsdetail">
<div class="addrsdetail-heading">
Address detail</div>
<div class="addrsdetail-summery">
<div class="addrsdetail-items">
<div class="addrsdetail-itemstext">
<asp:Label runat="server" ID="lbladdress"></asp:Label>
<asp:Label runat="server" ID="lbladdressid" Visible="false"></asp:Label><br />
<asp:LinkButton ID="lnkchangeadd" runat="server" Text="Change Address" ToolTip="Change Address" OnClick="lnkchangeadd_Click" ></asp:LinkButton>
</div>
</div>
</div>
</div>
</asp:Panel>
</div>
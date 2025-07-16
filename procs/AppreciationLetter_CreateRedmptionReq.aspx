<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="AppreciationLetter_CreateRedmptionReq.aspx.cs" Inherits="AppreciationLetter_CreateRedmptionReq" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<%@ Register TagName="calender" TagPrefix="ucical" Src="~/themes/creative1.0/LayoutControls/leaveCalender.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server"> 
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/manageleave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/ModifyLeaveRequest_css.css" type="text/css" media="all" /> 
    

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
    
    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">

                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Create Redmption Request"></asp:Label>
                    </span>
                </div>
                   <div style="width: 100%; overflow: auto; align-content: flex-start">
                    <div class="editprofileform">
                        <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                    </div>
                </div>
                <div class="leavegrid">
                 <br />
                <a href="Appreciation_Letter_index.aspx" class="aaa">Appreciation Index</a>
            </div>

                
                <div class="edit-contact">
                   <ul>  
                       <li> 
                   <div class="manage_grid" style="width:90%; height: auto;">
   
          <asp:GridView ID="gvMngLeaveRqstList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" 
          CellPadding="3" AutoGenerateColumns="False" Width="90%"  AllowPaging="true" PageSize="15" EditRowStyle-Wrap="false">
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
        <PagerStyle ForeColor="#000066" HorizontalAlign="left" BackColor="White" />
        <RowStyle ForeColor="#000066" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#007DBB" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#00547E" />
                <PagerStyle HorizontalAlign = "Right" CssClass = "paging" />

        <Columns>
             
            <asp:BoundField HeaderText="Redeem Point"
                DataField="TotalPoints"
                ItemStyle-HorizontalAlign="left"
                HeaderStyle-HorizontalAlign="left"
                ItemStyle-Width="15%" 
                ItemStyle-BorderColor="Navy" />

            <asp:BoundField HeaderText="Redeemed"
                DataField="RedeemedPoints"
                  ItemStyle-HorizontalAlign="left"
                HeaderStyle-HorizontalAlign="left"                                 
                ItemStyle-Width="10%" 
                ItemStyle-BorderColor="Navy" />

            <asp:BoundField HeaderText="Balance"
                DataField="RemainingPoints"
                  ItemStyle-HorizontalAlign="left"
                HeaderStyle-HorizontalAlign="left"                                 
                ItemStyle-Width="10%" 
                ItemStyle-BorderColor="Navy" />
             
        </Columns>
    </asp:GridView> 
</div>
    </li>
                       <li></li>
      <li id="Redeempont" visible="false" runat="server"> <br />                            
          <span>Redeem Point</span>&nbsp;&nbsp;<span style="color: red">*</span>  <br />              
          <asp:TextBox ID="txtRedeemPoint" width="100px"  runat="server" onkeypress="return isNumberKey(event);"></asp:TextBox>
      </li>
     <li></li>  
     <li id="remark" visible="false" runat="server">  
    <span>Remarks</span>&nbsp;&nbsp;
    <span style="color: red; font-size: 10px; font-weight: normal; font-style: italic;">Maximum 500 Characters</span>
    <br />
    <asp:TextBox AutoComplete="off" ID="txtRemark" runat="server" MaxLength="400" TextMode="MultiLine" Height="80px" ></asp:TextBox>

</li>
 <li></li>
      <li> 
          <asp:LinkButton ID="btnIn" runat="server" CssClass="Savebtnsve" Text="Submit"  Visible="False" OnClick="mobile_btnSave_Click" OnClientClick="return SaveMultiClick();">Submit</asp:LinkButton>
          
      </li>
      <li></li>
      <li></li>
      <li>
          <br />
          <br />
          <br />
      </li>
  </ul>
                     
                </div>
                <asp:HiddenField ID="hdnEmpCode" runat="server" />
                <asp:HiddenField ID="hdnYesNo" runat="server" />
               <asp:HiddenField ID="hdnEmp_Name" runat="server" />
               <asp:HiddenField ID="hdn_AdminName" runat="server" />
               <asp:HiddenField ID="hdn_AdminEmail" runat="server" />

            </div>
        </div>
    </div>

    <script type="text/javascript">

         
    function isNumberKey(evt) {
        var charCode = evt.which ? evt.which : evt.keyCode;
        // Allow only numbers (0–9)
        return charCode >= 48 && charCode <= 57;
    } 
        function openCity(evt, cityName) {
            
            var i, tabcontent, tablinks;
            tabcontent = document.getElementsByClassName("tabcontent");
            for (i = 0; i < tabcontent.length; i++) {
                tabcontent[i].style.display = "none";
            }
            tablinks = document.getElementsByClassName("tablinks");
            for (i = 0; i < tablinks.length; i++) {
                tablinks[i].className = tablinks[i].className.replace(" active", "");
            }
            document.getElementById(cityName).style.display = "block";
            evt.currentTarget.className += " active";
        }
    </script>

    <script type="text/javascript">
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
         
        function SaveMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=btnIn.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        ConfirmIn();
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
          
        function ConfirmIn() {
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
    </script>
</asp:Content>

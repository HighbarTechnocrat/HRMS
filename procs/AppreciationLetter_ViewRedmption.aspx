<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="AppreciationLetter_ViewRedmption.aspx.cs" Inherits="AppreciationLetter_ViewRedmption" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<%@ Register TagName="calender" TagPrefix="ucical" Src="~/themes/creative1.0/LayoutControls/leaveCalender.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server"> 
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/manageleave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/ModifyLeaveRequest_css.css" type="text/css" media="all" /> 
    <style>

        .backbtn
            {
              background: #3D1956;
              color: #febf39 !important;
              padding: 9px 7px; 
            } 
        .aspNetDisabled {
            background: #dae1ed;
        } 
        
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
    
    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">

                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Redmption Request"></asp:Label>
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
                ItemStyle-Width="10%" 
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
      <li> <br />                            
          <span >Request Redeem Point</span>  <br />              
          <asp:TextBox ID="txtRedeemPoint" width="100px" Enabled="false" runat="server" onkeypress="return isNumberKey(event);"></asp:TextBox>
      </li>
     <li>
         <span id="RequistRedeemPoint" runat="server">Redeem Point</span>  <br />              
<asp:TextBox ID="txtAdmin_Point" width="100px" Enabled="false" runat="server" onkeypress="return isNumberKey(event);"></asp:TextBox>
      
     </li>  
     <li class="trvl_date">  
    <span>Remarks by Employee</span> <br />
    <asp:TextBox AutoComplete="off" ID="txtRemark" Enabled="false" runat="server" MaxLength="400" TextMode="MultiLine" Height="80px" ></asp:TextBox>

</li>
 <li>
     <span id="RemarksbyAdmin" runat="server">Remarks by Admin</span> <br />
<asp:TextBox AutoComplete="off" ID="txtremark_admin" Enabled="false" runat="server" MaxLength="400" TextMode="MultiLine" Height="80px" ></asp:TextBox>

 </li>
      
      <li>
           <span>
         <a href="AppreciationLetter_ViewRedmptionReq.aspx?itype=0"  class="backbtn">Back</a>
        </span>   
      </li>
      <li></li> <li> </li> <li> <br /><br /> </li>
  </ul>
                     
                </div>
                <asp:HiddenField ID="hdnEmpCode" runat="server" />
                <asp:HiddenField ID="hdnYesNo" runat="server" />
               <asp:HiddenField ID="hdnReqid" runat="server" />

            </div>
        </div>
    </div>

   
</asp:Content>

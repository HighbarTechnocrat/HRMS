<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="AppreciationLetter_ApprovedRedeemedReq.aspx.cs" Inherits="AppreciationLetter_ApprovedRedeemedReq" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<%@ Register TagName="calender" TagPrefix="ucical" Src="~/themes/creative1.0/LayoutControls/leaveCalender.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/manageleave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/ModifyLeaveRequest_css.css" type="text/css" media="all" />
      <style>
        
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
                        <asp:Label ID="lblheading" runat="server" Text="Approved Redmption Request"></asp:Label>
                    </span>
                </div>
 
                <span>
                    <a href="Appreciation_Letter_index.aspx" style="margin-right: 10px;" class="aaaa">Appreciation Index</a>
                </span>

                

                
                <div class="edit-contact"> 
                    
                    <ul id="editform" runat="server" visible="false"> 
                        <li class="date"> 
                            <span>Employee Code</span>                
                            <asp:TextBox ID="txt_EmpCode" Enabled="false" AutoComplete="off" runat="server"></asp:TextBox>
                        </li>
                        <li class="leavedays">                            
                            <span>Employee Name</span>                
                            <asp:TextBox ID="txtEmp_Name" Enabled="false" AutoComplete="off" runat="server"></asp:TextBox>
                        </li>
  
                        <li>                             
                            <span>Designation</span>                
                            <asp:TextBox ID="txtEmp_Desigantion" Enabled="false" AutoComplete="off" runat="server"></asp:TextBox>
                        </li>

                        <li class="leavedays">                            
                            <span>Department</span>                
                            <asp:TextBox ID="txtEmp_Department" Enabled="false" AutoComplete="off" runat="server"></asp:TextBox>
                        </li>
                       
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
          <span>Request Redeem Point</span>  <br />              
          <asp:TextBox ID="txtRedeemPoint" width="100px" Enabled="false" runat="server" onkeypress="return isNumberKey(event);"></asp:TextBox>
      </li> 
     <li><span>Redeem Point</span>  <br />              
<asp:TextBox ID="txtAdmin_Point" width="100px" Enabled="false" runat="server" onkeypress="return isNumberKey(event);"></asp:TextBox>
     </li>   
     <li class="trvl_date">  
    <span>Remarks by Employee</span> <br />
    <asp:TextBox AutoComplete="off" ID="txtRemark" Enabled="false" runat="server" MaxLength="400" TextMode="MultiLine" Height="80px" ></asp:TextBox>

</li> 
 <li>
     <span>Remarks by Admin</span>  <br />
<asp:TextBox AutoComplete="off" ID="txtremark_admin" Enabled="false"  runat="server" MaxLength="400" TextMode="MultiLine" Height="80px" ></asp:TextBox>

 </li>    
 
                        <li> 
                             <asp:LinkButton ID="btnIn" runat="server" CssClass="Savebtnsve" Text="Back"  Visible="true" OnClick="btnback_mng_Click" OnClientClick="return SaveMultiClick();">Back</asp:LinkButton>
                              
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
                <asp:HiddenField ID="hdnReqid" runat="server" /> 
                <asp:HiddenField ID="hdnYesNo" runat="server" /> 

            </div>
        </div>
    </div>
 
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="PVreimbursmentReport.aspx.cs" Inherits="myaccount_PVreimbursmentReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leaveteamrpt.css" type="text/css" media="all" />
    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }
            .edit-contact input:focus {
                border-bottom: 2px solid rgb(51, 142, 201) !important;
            }

        .edit-contact input {
            padding-left: 30px !important;
            width: 83%;
        }

        .edit-contact > ul {
            padding: 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    
       <div class="userposts">
            <span>
                <asp:Label ID="lblheading" runat="server" Text="Payment Voucher Claims Report"></asp:Label>
            </span>
        </div>

       <div class="leavegrid">                    
             <a href="Voucher.aspx" class="aaa">Payment Voucher Home</a>
        </div>  

       <div style="width:100%;overflow:auto;align-content:flex-start" >      
            <div class="editprofileform">                            
                <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
            </div>
       </div>
       <div class="edit-contact">
            <ul>
                <li>
                    <span style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">Enter Employee Name:</span><br />
                    <asp:TextBox ID="txtemp" runat="server" CssClass="txtcls" TabIndex="0"> </asp:TextBox>
                </li>
                <li>
                    <span style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">Claim Status:</span><br />
                    <asp:DropDownList ID="ddlClaimStatus" Visible="false" runat="server">
                            <asp:ListItem>Pending</asp:ListItem>
                            <asp:ListItem>Approved</asp:ListItem>                                
                    </asp:DropDownList>

                    <asp:TextBox ID="txtClaimStatus" runat="server" AutoPostBack="true" CssClass="txtcls" ></asp:TextBox>
                    <%--<i class="fa fa-caret-down" aria-hidden="true"></i>--%>
                    <asp:Panel ID="Panel2" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:ListBox ID="lstFromfor" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstFromfor_SelectedIndexChanged">
                                            
                                    <asp:ListItem Selected="True" Text="-- Select Status---" Value="">-- Select Status---</asp:ListItem>
                                    <asp:ListItem>Pending</asp:ListItem>
                                    <asp:ListItem>Approved</asp:ListItem>
                                    <asp:ListItem Enabled ="false"> </asp:ListItem>
                                            
                                          
                                </asp:ListBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>

                    <ajaxToolkit:PopupControlExtender ID="PopupControlExtender1" PopupControlID="Panel2" TargetControlID="txtClaimStatus"
                        Position="Bottom" runat="server">
                    </ajaxToolkit:PopupControlExtender>

                </li>
            </ul>
            <ul>
                <li>
                    <span style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">From Date:</span><br />
                    <asp:TextBox ID="txtFromdate" CssClass="txtcls"  AutoComplete="off" runat="server" AutoPostBack="true"  OnTextChanged="txtFromdate_TextChanged" MaxLength="10" AutoCompleteType="Disabled"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtFromdate" runat="server" >
                    </ajaxToolkit:CalendarExtender>
                </li>
                <li>
                    <span style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">To Date:</span><br />
                    <asp:TextBox ID="txtToDate" CssClass="txtcls"  AutoComplete="off" runat="server" OnTextChanged="txtToDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                        runat="server">
                    </ajaxToolkit:CalendarExtender>
                </li>                          
            </ul>
           <ul>
               <li>
                    <asp:LinkButton ID="btnSave" runat="server" Text="View Report" ToolTip="Reimbursement Report" CssClass="leaverpt" OnClick="btnSave_Click">Generate Report</asp:LinkButton>                        
               </li>
           </ul>
        </div> 
    

        <div style="width:100%;overflow:auto">
        
        <rsweb:ReportViewer ID="ReportViewer2" runat="server" Height="300px"  
        Width="80%" ShowBackButton="False" SizeToReportContent="True" ShowCredentialPrompts="False"
        ShowDocumentMapButton="False" 
        ShowPageNavigationControls="False" ShowRefreshButton="False" ShowFindControls="False">

        </rsweb:ReportViewer>
        </div>

<asp:HiddenField ID="hdnloginempcode" runat="server" />

</asp:Content>


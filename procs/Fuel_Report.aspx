<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="Fuel_Report.aspx.cs" Inherits="myaccount_FuelreimbursmentReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leaveteamrpt.css" type="text/css" media="all" />
    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    
       <div class="userposts">
            <span>
                <asp:Label ID="lblheading" runat="server" Text="Fuel Reimbursement Report"></asp:Label>
            </span>
        </div>

        <div class="leavegrid">                    
             <a href="Fuel.aspx" class="aaa">Fuel Home</a>
        </div>  

    
       <div class="leavegrid">
           
       </div>
       <div style="width:100%;overflow:auto;align-content:flex-start" >      
            <div class="editprofileform">                            
                <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="false" Style="margin-left: 135px"></asp:Label>
            </div>
           <table style="width:20%">
                <tr>
                    <td style="text-align:left" class="auto-style1"> </td>
                </tr>
                <tr>
                    <td>         
                        <span>Enter Employee Name</span>
                          <asp:TextBox ID="txtemp" runat="server" Width="200px" CssClass="dlleavereport" TabIndex="0"> </asp:TextBox>                     
                        <asp:LinkButton ID="btnSave" runat="server" Text="View Report" ToolTip="Reimbursement Report" CssClass="leaverpt"  OnClick="btnSave_Click">Generate Report</asp:LinkButton>                        
                    </td>
                </tr>
           </table>
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


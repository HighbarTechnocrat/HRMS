<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="LeavesReport.aspx.cs" Inherits="LeavesReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    
     <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <%--<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leaveteamrpt.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Report.css" type="text/css" media="all" />
    
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    

       <div class="userposts">
            <span>
                <asp:Label ID="lblheading" runat="server" Text="Leave Report"></asp:Label>
            </span>
        </div>


        <div class="leavegrid">                    
            <a href="Leaves.aspx" class="aaa">Leave Menu</a>
        </div>    

    
       <div class="leavegrid">
           
        </div>
       <div style="width:100%;overflow:auto;align-content:flex-start" >      
            <div class="editprofileform">                            
            <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="false" Style="margin-left: 135px"></asp:Label>
                </div>
           <table style="width:80%">
                <tr>
                <td style="width:35%;">
                     <span>Select Leave Type</span>&nbsp;&nbsp;       <%--<span style="color:red">*</span>--%>
                    <br />                       
                     <asp:DropDownList ID="DropDownList1"  runat="server" CssClass="dlleavereport DropDownList" TabIndex="0"> </asp:DropDownList>                               
               </td>
               <td style="width:30%;"> 
                      <span>Select Year</span>&nbsp;&nbsp;       <%--<span style="color:red">*</span>--%>
                   <br />
                      <asp:DropDownList ID="ddlYear" runat="server" CssClass="dlleavereport DropDownList" TabIndex="0"> </asp:DropDownList>      
               </td>
               <td style="width:35%;"> 
                   <span>&nbsp;</span>
                   <asp:LinkButton ID="btnSave" runat="server" Text="View Leave Report" ToolTip="Leave Report" CssClass="leaverpt"  OnClick="btnSave_Click">Generate Report</asp:LinkButton>                        
               </td>
               </tr>
           </table>
        </div> 
    



        <div style="width:100%;overflow:auto">
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="300px" 
        width="80%" showbackbutton="False" sizetoreportcontent="True"
                        showcredentialprompts="False" showdocumentmapbutton="False"
                        showpagenavigationcontrols="true" showfindcontrols="false" showexportcontrols="true" showrefreshbutton="False" pagecountmode="Actual">
    </rsweb:ReportViewer>
			<%--Width="80%" ShowBackButton="False" SizeToReportContent="True" 
        ShowCredentialPrompts="False" ShowDocumentMapButton="False" 
        ShowPageNavigationControls="False" ShowRefreshButton="False">--%>
        </div>
<asp:HiddenField ID="hdnloginempcode" runat="server" />
	<script src="../js/dist/jquery-3.2.1.min.js"></script>       
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" /> 
	<script type="text/javascript">      
        $(document).ready(function () {
            $(".DropDownList").select2();
        });
    </script>
</asp:Content>


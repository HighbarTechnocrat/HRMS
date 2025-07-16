<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="FuelReport_Audit.aspx.cs" Inherits="FuelReport_Audit" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <%--<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leaveteamrpt.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Report.css" type="text/css" media="all" />
	
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

     <script src="../js/dist/jquery-3.2.1.min.js"></script>
     <script src="../js/dist/select2.min.js"></script>
	<link href="../js/dist/select2.min.css" rel="stylesheet" />

     <script type="text/javascript">      
		$(document).ready(function () {			
			$(".DropdownListSearch").select2();		
		});
        </script>
    
       <div class="userposts">
            <span>
                <asp:Label ID="lblheading" runat="server" Text="Fuel Claim Audit Report"></asp:Label>
            </span>
        </div>

       <div class="leavegrid">                    
             <a href="fuel.aspx" class="aaa">Fuel Claim Home</a>
        </div>  

       <div style="width:100%;overflow:auto;align-content:flex-start" >      
            <div class="editprofileform">                            
                <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
            </div>
       </div>
       <div class="edit-contact">
            <ul>
                <li>
                    <span style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">Claim Status:<span style="color:red"> *</span></span><br />
                    <asp:DropDownList ID="ddlClaimStatus" runat="server" CssClass="DropdownListSearch"> 
                        <asp:ListItem Value="">All</asp:ListItem>
                        <asp:ListItem Value="Pending">Pending</asp:ListItem>
                        <asp:ListItem Value="Approved">Approved</asp:ListItem>  
                        <asp:ListItem Value="Correction">Correction</asp:ListItem>  
                        <asp:ListItem Value="Cancelled">Cancelled</asp:ListItem>  
                        <asp:ListItem Value="Reject">Reject</asp:ListItem>                                                        
                    </asp:DropDownList>

<%--                    <asp:TextBox ID="txtClaimStatus" runat="server" AutoPostBack="true" CssClass="txtcls" ></asp:TextBox>
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
                    </ajaxToolkit:PopupControlExtender>--%>

                </li>
            </ul>
            <ul>
                <li>
                    <span style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">Enter Employee Name:</span><br />
                    <%--<asp:TextBox ID="txtemp" runat="server" CssClass="txtcls" TabIndex="0"> </asp:TextBox>--%>
                    <asp:DropDownList ID="DDL_txtemp"  CssClass="DropdownListSearch" runat="server" TabIndex="0"> 
                    </asp:DropDownList>


                </li>
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
           
				   <div style="margin:0px !important; float:left;width:50%">
				   <asp:LinkButton ID="btnSave" style="margin:0px !important;" runat="server" Text="View Report" ToolTip="Generate Report" CssClass="leaverpt" OnClick="btnSave_Click">Generate Report</asp:LinkButton>       
					   <br />
					   <br />
       </div>
		   
        </div> 
    

        <div style="width:100%;overflow:auto">
        
        <rsweb:ReportViewer ID="ReportViewer2" runat="server" Height="600px"
           Width="100%" ShowBackButton="False" SizeToReportContent="false" 
        ShowCredentialPrompts="False" ShowDocumentMapButton="False" 
        ShowPageNavigationControls="true" ShowRefreshButton="False" ShowExportControls="true"
	 PageCountMode="Actual">
        </rsweb:ReportViewer>
        </div>

<asp:HiddenField ID="hdnloginempcode" runat="server" />
    
    <%--<ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchEmployees" MinimumPrefixLength="1"
        CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtemp"
        ID="AutoCompleteExtender1" runat="server" FirstRowSelected="true">
    </ajaxToolkit:AutoCompleteExtender>--%>

</asp:Content>


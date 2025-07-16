<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="Custs_Service.aspx.cs" Inherits="Custs_Service" %>



<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<%@ Register TagName="calender" TagPrefix="ucical" Src="~/themes/creative1.0/LayoutControls/leaveCalender.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />

    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
            /*background: #dae1ed;*/
            background: #ebebe4;
        }

        .Calender {
            float: left;
            padding: 5% 5% 5% 5% !important;
        }

        #MainContent_lnk_leaverequest:link, #MainContent_lnk_leaverequest:visited,
        #MainContent_lnk_mng_leaverequest:link, #MainContent_lnk_mng_leaverequest:visited,
        #MainContent_lnk_reimbursmentReport:link, #MainContent_lnk_reimbursmentReport:visited,
        #MainContent_lnk_leaveinbox:link, #MainContent_lnk_leaveinbox:visited,
        #MainContent_lnk_MobACC:link, #MainContent_lnk_MobACC:visited,
        #MainContent_lnk_MobCFO:link, #MainContent_lnk_MobPastApproved_ACC:link, #MainContent_lnk_MobCFO:visited, #MainContent_lnk_MobPastApproved_ACC:visited,
        #MainContent_lnk_reimbursmentReport_1:link, #MainContent_lnk_reimbursmentReport_1:visited,
        #MainContent_lnk_summary_report:link, #MainContent_lnk_summary_report:visited,
        #MainContent_lnkbtnEscalated:link, #MainContent_lnkbtnEscalated:visited,
        #MainContent_lnk_CustomerSericeInbox:link,#MainContent_lnk_CustomerSericeInbox:visited,
        #MainContent_Lnk_CustomerServiceFirstAllRequest:link,#MainContent_Lnk_CustomerServiceFirstAllRequest:visited,
         #MainContent_lnk_CustomerSericeInbox_Processed:link,#MainContent_lnk_CustomerSericeInbox_Processed:visited,
          #MainContent_lnk_CustomerSericePendingInbox_CS:link,#MainContent_lnk_CustomerSericePendingInbox_CS:visited
        
        
        {
            background-color: #C7D3D4;
            color: #603F83 !important;
            border-radius: 10px;
            /*color: white;*/
            padding: 25px 5px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            width: 90% !important;
        }

        #MainContent_lnk_leaverequest:hover, #MainContent_lnk_leaverequest:active,
        #MainContent_lnk_mng_leaverequest:hover, #MainContent_lnk_mng_leaverequest:active,
        #MainContent_Lnk_CustomerServiceFirstAllRequest:hover, #MainContent_Lnk_CustomerServiceFirstAllRequest:active,
        #MainContent_lnk_reimbursmentReport:hover, #MainContent_lnk_reimbursmentReport:active,
        #MainContent_lnk_leaveinbox:hover, #MainContent_lnk_leaveinbox:active,
        #MainContent_lnk_MobACC:hover, #MainContent_lnk_MobACC:active,
        #MainContent_lnk_MobCFO:hover, #MainContent_lnk_MobPastApproved_ACC:hover, #MainContent_lnk_MobCFO:active, #MainContent_lnk_MobPastApproved_ACC:active,
        #MainContent_lnk_reimbursmentReport_1:hover, #MainContent_lnk_reimbursmentReport_1:active,
        #MainContent_lnk_summary_report:hover, #MainContent_lnk_summary_report:active,
        #MainContent_lnkbtnEscalated:hover, #MainContent_lnkbtnEscalated:active,
        #MainContent_lnk_CustomerSericeInbox:hover,#MainContent_lnk_CustomerSericeInbox:active,
         #MainContent_lnk_CustomerSericeInbox_Processed:hover,#MainContent_lnk_CustomerSericeInbox_Processed:active,
         #MainContent_lnk_CustomerSericePendingInbox_CS:hover,#MainContent_lnk_CustomerSericePendingInbox_CS:active         
      {
				/*background-color: #603F83;*/
				background-color: #3D1956;
				color: #C7D3D4 !important;
				border-color: #3D1956;
				border-width: 2pt;
				border-style: inset;
			}

		.EmpName {
			color: Blue;
			border-color: Navy;
			cursor: pointer !important;
		}

		.dgSupervisor {
			color: White !important;
			background-color: #669999;
			font-weight: bold;
		}

		.DelayedTH {
			display: none;
		}

		.EmpName1 {
			color: #F28820;
			font-weight: 600;
			font-size: 16px;
			border-color: Navy;
			cursor: pointer !important;
			
		}

		#MainContent_btnExcelDownload {
			background-attachment: scroll;
			background-clip: border-box;
			background-color: #3D1956;
			color: #febf39 !important;
			background-image: none;
			background-origin: padding-box;
			background-position-x: 0;
			background-position-y: 0;
			background-repeat: repeat;
			background-size: auto auto;
			padding-bottom: 6px;
			padding-left: 23px;
			padding-right: 23px;
			padding-top: 6px;
		}
		.dgDelayedHide {
			background-color:#fff !important;
		}
		.dgDelayedShow {
			background-color:#C7D3D4 !important;
		}
		#DelayedTBL > tbody > tr > td > div {
			margin-right:-2px !important;
		}
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
   
    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="wishlistpage">
                    <div class="userposts">
                        <span>
                            <asp:Label ID="lblheading" runat="server" Text="CUSTOMERFIRST Service Request"></asp:Label>
                        </span>
                    </div>
                    <%--  <div class="leavegrid">
                        <a href="http://localhost/hrms/Service.aspx" class="aaa" >Service Request Menu</a>
                     </div>--%>
                    <div class="editprofile" id="editform1" runat="server" visible="true">
                        <div class="editprofileform">
                            <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="false" Style="margin-left: 135px"></asp:Label>

                            <table style='width:33%; padding-left:15px; padding-bottom:5px' id="DGPendingRequesttbl">
										<tr>
											<td style="width:32%;padding-right:0px !important;">
												<asp:GridView ID="dgDelayed" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" 
                                               OnRowDataBound="dgDelayed_RowDataBound" OnSelectedIndexChanged="dgDelayed_SelectedIndexChanged" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%"
												AutoGenerateSelectButton="true"   >
												<FooterStyle BackColor="White" ForeColor="#000066" />
												<HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
												<PagerStyle ForeColor="#000066" HorizontalAlign="left" BackColor="White" />
												<RowStyle ForeColor="#000066" />
												<%--<SelectedRowStyle BackColor="#C7D3D4" />--%>
												<SortedAscendingCellStyle BackColor="#F1F1F1" />
												<SortedAscendingHeaderStyle BackColor="#007DBB" />
												<SortedDescendingCellStyle BackColor="#CAC9C9" />
												<SortedDescendingHeaderStyle BackColor="#00547E" />
												<Columns>

													<asp:BoundField HeaderText=""  HeaderStyle-CssClass="DelayedTH" ItemStyle-CssClass="EmpName1"
														DataField="PendingRequest"
														ItemStyle-HorizontalAlign="center"
														ItemStyle-Width="10%"
														ItemStyle-BorderColor="Navy" />
													<asp:BoundField HeaderText="" HeaderStyle-CssClass="DelayedTH" ItemStyle-CssClass="EmpName1"
														DataField="PendingRequestCount"
														ItemStyle-HorizontalAlign="Center"
														HeaderStyle-HorizontalAlign="left"
														ItemStyle-Width="3%" />
												
												</Columns>
											</asp:GridView>

											</td>
											<td style="width:1%;padding-left:0px !important;">
											</td>
										</tr>
									</table>	

                   <div class="manage_grid" style="width: 100%; height: auto; padding-left:17px;padding-bottom:05px" runat="server" id="DivPendingRequest" visible="false">
                   <h3 id="h4" runat="server">Pending Customer Service Request</h3>
                <asp:GridView ID="gvMngTravelRqstList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" 
                DataKeyNames="Id" CellPadding="3" AutoGenerateColumns="False" Width="100%"   EditRowStyle-Wrap="false" PageSize="20" AllowPaging="True" OnPageIndexChanging="gvMngTravelRqstList_PageIndexChanging">
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" HorizontalAlign="Center"/>
            <PagerStyle ForeColor="#000066" HorizontalAlign="left" BackColor="White" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
             <Columns>
                 <asp:TemplateField HeaderText="View" HeaderStyle-Width="5%">
                            <ItemTemplate>
                            <asp:ImageButton id="lnkEdit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkEdit_Click"/>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                         <asp:BoundField HeaderText="Project/ Location"
                                DataField="Location_name"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="15%" />
                 
                            <asp:BoundField HeaderText="Customer Name"
                                DataField="EmployeeName"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="15%" />

                             <asp:BoundField HeaderText="Created On"
                                DataField="ServiceRequestDate"
                                ItemStyle-HorizontalAlign="Center"                                
                                ItemStyle-Width="12%" />


                   <asp:BoundField HeaderText="Assigned To"
                                DataField="AssignedTo"
                                 ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="15%" />

                   <asp:BoundField HeaderText="Assignment Date"
                                DataField="AssignmentDate"
                                 ItemStyle-HorizontalAlign="Center"                                
                                ItemStyle-Width="12%" />

                            <asp:BoundField HeaderText="Service Request ID"
                                DataField="ServicesRequestID"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="15%" />                             

                            <asp:BoundField HeaderText="Status"
                                DataField="Status"
                                 ItemStyle-HorizontalAlign="Center"                                
                                ItemStyle-Width="10%" />

                            
                        </Columns>
        </asp:GridView>
           
                   </div>

                        <table style='width:33%; padding-left:15px; padding-bottom:20px' id="DGResolveRequesttbl">
										<tr>
											<td style="width:32%;padding-right:0px !important;">
												<asp:GridView ID="DGRsolvedRequest" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" 
                                               OnRowDataBound="DGRsolvedRequest_RowDataBound"   OnSelectedIndexChanged="DGRsolvedRequest_SelectedIndexChanged1" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%"
												AutoGenerateSelectButton="true">
												<FooterStyle BackColor="White" ForeColor="#000066" />
												<HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
												<PagerStyle ForeColor="#000066" HorizontalAlign="left" BackColor="White" />
												<RowStyle ForeColor="#000066" />
												<%--<SelectedRowStyle BackColor="#C7D3D4" />--%>
												<SortedAscendingCellStyle BackColor="#F1F1F1" />
												<SortedAscendingHeaderStyle BackColor="#007DBB" />
												<SortedDescendingCellStyle BackColor="#CAC9C9" />
												<SortedDescendingHeaderStyle BackColor="#00547E" />
												<Columns>

													<asp:BoundField HeaderText=""  HeaderStyle-CssClass="DelayedTH" ItemStyle-CssClass="EmpName1"
														DataField="PendingRequest"
														ItemStyle-HorizontalAlign="center"
														ItemStyle-Width="10%"
														ItemStyle-BorderColor="Navy" />
													<asp:BoundField HeaderText="" HeaderStyle-CssClass="DelayedTH" ItemStyle-CssClass="EmpName1"
														DataField="PendingRequestCount"
														ItemStyle-HorizontalAlign="Center"
														HeaderStyle-HorizontalAlign="left"
														ItemStyle-Width="3%" />
												
												</Columns>
											</asp:GridView>

											</td>
											<td style="width:1%;padding-left:0px !important;">
											</td>
										</tr>
									</table>         
                           

                  <div class="manage_grid" style="width: 100%; height: auto; padding-left:17px;padding-bottom:20px" runat="server" id="DivResolveRequest" visible="false">
                 <h3 id="h1" runat="server">Resolved Customer Service Request</h3>
                <asp:GridView ID="gvMngTravelRqstListResolve" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" 
                DataKeyNames="Id" CellPadding="3" AutoGenerateColumns="False" Width="100%"   EditRowStyle-Wrap="false" PageSize="20" AllowPaging="True" 
                    OnPageIndexChanging="gvMngTravelRqstListResolve_PageIndexChanging">
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" HorizontalAlign="Center"/>
            <PagerStyle ForeColor="#000066" HorizontalAlign="left" BackColor="White" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
             <Columns>
                    <asp:TemplateField HeaderText="View" HeaderStyle-Width="5%">
                            <ItemTemplate>
                            <%--<asp:LinkButton ID="lnkEdit" runat="server" Text='View' OnClick="lnkEdit_Click1"></asp:LinkButton>--%>
                            <asp:ImageButton id="lnkEditResolve" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png"   OnClick="lnkEditResolve_Click1"/>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                         <asp:BoundField HeaderText="Project/ Location"
                                DataField="Location_name"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="15%" />
                 
                            <asp:BoundField HeaderText="Customer Name"
                                DataField="EmployeeName"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="15%" />

                             <asp:BoundField HeaderText="Created On"
                                DataField="ServiceRequestDate"
                                ItemStyle-HorizontalAlign="Center"                                
                                ItemStyle-Width="12%" />


                   <asp:BoundField HeaderText="Assigned To"
                                DataField="AssignedTo"
                                 ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="15%" />

                   <asp:BoundField HeaderText="Assignment Date"
                                DataField="AssignmentDate"
                                 ItemStyle-HorizontalAlign="Center"                                
                                ItemStyle-Width="12%" />

                            <asp:BoundField HeaderText="Service Request ID"
                                DataField="ServicesRequestID"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="15%" />                             

                            <asp:BoundField HeaderText="Status"
                                DataField="Status"
                                 ItemStyle-HorizontalAlign="Center"                                
                                ItemStyle-Width="10%" />

                          
                        </Columns>
        </asp:GridView>
          
                   </div>

                            <table>

                               

                                <tr style="padding-top: 1px; padding-bottom: 2px;display:none">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_leaverequest" runat="server" OnClick="lnk_leaverequest_Click">Create Service Request</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_mng_leaverequest" runat="server" PostBackUrl="~/procs/MyService_Req.aspx">My Service Request</asp:LinkButton>
                                    </td>
                                </tr>

                                <tr style="padding-top: 1px; padding-bottom: 2px;display:none" >
                                    <td class="formtitle" >
                                        <asp:LinkButton ID="lnkbtnEscalated" runat="server" PostBackUrl="~/procs/MyEscalatedService.aspx">My Escalated Service</asp:LinkButton>
                                    </td>
                                    <td class="formtitle" >
                                        
                                    </td>
                                </tr>

                                <tr style="padding-top: 1px; padding-bottom: 2px; display: none">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_reimbursmentReport" runat="server" Visible="True" Text="" PostBackUrl="~/procs/ClaimsReport_Mobile.aspx">Report - Self</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;display:none">
                                    <td class="formtitle">
                                        <br />
                                        <span id="span_App_head" runat="server" style="font-size: 12pt; font-weight: bold; color: #3D1956;">Approver: </span>
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;display:none">
                                    <td class="formtitle">
                                        <%--<asp:LinkButton ID="lnk_leaveinbox" runat="server" Text="" PostBackUrl="~/procs/InboxServiceRequest.aspx?app=APP">Inbox</asp:LinkButton>--%>
                                        <asp:LinkButton ID="lnk_leaveinbox" runat="server" Text="" PostBackUrl="~/procs/InboxServiceRequest.aspx">Inbox</asp:LinkButton>
                                    </td>

                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;display:none">
                                    <td class="formtitle" runat="server" id="showPSR">
                                        <%--<asp:LinkButton ID="lnk_leaveinbox" runat="server" Text="" PostBackUrl="~/procs/InboxServiceRequest.aspx?app=APP">Inbox</asp:LinkButton>--%>
                                        <asp:LinkButton ID="lnk_MobACC" runat="server" Text="" PostBackUrl="~/procs/InboxServiceRequest_Arch.aspx">Processed Service Requests</asp:LinkButton>
                                    </td>
                                    <td class="formtitle" runat="server" id="Td1">
                                        <%--<asp:LinkButton ID="lnk_leaveinbox" runat="server" Text="" PostBackUrl="~/procs/InboxServiceRequest.aspx?app=APP">Inbox</asp:LinkButton>--%>
                                        <asp:LinkButton ID="lnk_reimbursmentReport_1" runat="server" Text="" Visible="false" PostBackUrl="~/procs/ServiceRequestReport_Audit.aspx">Service Request History Report</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle" runat="server" id="Td2">
                                        <%--<asp:LinkButton ID="lnk_leaveinbox" runat="server" Text="" PostBackUrl="~/procs/InboxServiceRequest.aspx?app=APP">Inbox</asp:LinkButton>--%>
                                        <asp:LinkButton ID="lnk_summary_report" runat="server" Text="" Visible="false" PostBackUrl="~/procs/ServiceRequestReport_Audit_Summary.aspx">Service Request History Summary Report</asp:LinkButton>
                                    </td>
                                </tr>
                              
                               

                                 <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <br />
                                        <span id="span_CustomerServiceHead" runat="server" style="font-size: 12pt; font-weight: bold; color: #3D1956;">CUSTOMERFIRST Service: </span>
                                    </td>                                   
                                </tr>

                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                         <asp:LinkButton ID="lnk_CustomerSericeInbox" runat="server" Text="" PostBackUrl="~/procs/Custs_InboxServiceRequest.aspx">CUSTOMERFIRST Service Approval Inbox</asp:LinkButton>
                                    </td>
                                      <td class="formtitle">
                                         <asp:LinkButton ID="lnk_CustomerSericePendingInbox_CS" runat="server" Text="" PostBackUrl="~/procs/Custs_InboxPendingServiceRequest.aspx">CUSTOMERFIRST Service Inbox Pending</asp:LinkButton>
                                         <asp:LinkButton ID="lnk_CustomerSericeInbox_Processed" runat="server" Text="" PostBackUrl="~/procs/Custs_InboxServiceRequest_Arch.aspx">Processed CUSTOMERFIRST Service Requests</asp:LinkButton>
                                    </td>
                                </tr>

                                 <%--<tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                          <asp:LinkButton ID="Lnk_CustomerServiceFirstAllRequest" runat="server" Text="" PostBackUrl="~/procs/Custs_InboxService_AllRequest.aspx">CUSTOMERFIRST Service All Requests</asp:LinkButton>
                                    </td>
                                      <td class="formtitle">
                                    </td>
                                </tr>--%>

                               
                             
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>

    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />

    <asp:HiddenField ID="hflEmpCode" runat="server" />

    <asp:HiddenField ID="hflEmpDesignation" runat="server" />

    <asp:HiddenField ID="hflEmpDepartment" runat="server" />

    <asp:HiddenField ID="hflEmailAddress" runat="server" />

    <asp:HiddenField ID="hflGrade" runat="server" />
    <asp:HiddenField ID="hdnRemid" runat="server" />


    <asp:HiddenField ID="hflapprcode" runat="server" />
    <asp:HiddenField ID="hdnClaimDate" runat="server" />


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

        function Count(text) {
            var maxlength = 250;
            var object = document.getElementById(text.id)
            if (object.value.length > maxlength) {
                object.focus();
                object.value = text.value.substring(0, maxlength);
                object.scrollTop = object.scrollHeight;
                return false;
            }
            return true;
        }

    </script>
</asp:Content>

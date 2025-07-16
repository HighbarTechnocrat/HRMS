<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
	 CodeFile="CustEscalationView_Index.aspx.cs" Inherits="CustEscalationView_Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
      <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leaveteamrpt.css" type="text/css" media="all" />
    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
             /*background: #dae1ed;*/
           background:#ebebe4;
        }

        #MainContent_lstApprover {
            overflow: hidden !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

 <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
  <script src="../js/dist/jquery-3.2.1.min.js"></script>
	
  <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
   <script src="../js/jquery.sumoselect-3.3.29/jquery.sumoselect.min.js"></script>
    <link href="../js/jquery.sumoselect-3.3.29/sumoselect.css" rel="stylesheet" />
	<script type="text/javascript">
        var deprt;
        $(document).ready(function () {
            $("#MainContent_txtcity").autocomplete("<%=ReturnUrl("sitepathmain") %>Search_City.ashx", {
                inputClass: "ac_input",
                resultsClass: "ac_results",
                loadingClass: "ac_loading",
                minChars: 1,
                delay: 1,
                matchCase: false,
                matchSubset: false,
                matchContains: false,
                cacheLength: 0,
                max: 12,
                mustMatch: false,
                extraParams: {},
                selectFirst: false,
                formatItem: function (row) { return row[0] },
                formatMatch: null,
                autoFill: false,
                width: 0,
                multiple: false,
                scroll: false,
                scrollHeight: 180,
                success: function (data) {
                    response($.map(data, function (item) {
                        return {

                        }
                    }));
                },
                context: this
            });

            $("#MainContent_txtloc").autocomplete("<%=ReturnUrl("sitepathmain") %>Search_location.ashx", {
                inputClass: "ac_input",
                resultsClass: "ac_results",
                loadingClass: "ac_loading",
                minChars: 1,
                delay: 1,
                matchCase: false,
                matchSubset: false,
                matchContains: false,
                cacheLength: 0,
                max: 12,
                mustMatch: false,
                extraParams: {},
                selectFirst: false,
                formatItem: function (row) { return row[0] },
                formatMatch: null,
                autoFill: false,
                width: 0,
                multiple: false,
                scroll: false,
                scrollHeight: 180,
                success: function (data) {
                    response($.map(data, function (item) {
                        return {

                        }
                    }));
                },
                context: this
            });

            $("#MainContent_txtsubdept").autocomplete("<%=ReturnUrl("sitepathmain") %>Search_subdepartment.ashx?d=" + deprt, {
                inputClass: "ac_input",
                resultsClass: "ac_results",
                loadingClass: "ac_loading",
                minChars: 1,
                delay: 1,
                matchCase: false,
                matchSubset: false,
                matchContains: false,
                cacheLength: 0,
                max: 12,
                mustMatch: false,
                extraParams: { d: deprt },
                selectFirst: false,
                formatItem: function (row) { return row[0] },
                formatMatch: null,
                autoFill: false,
                width: 0,
                multiple: false,
                scroll: false,
                scrollHeight: 180,
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                        }
                    }));
                },

                context: this
            });
        });
    </script>
    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Customer Incident Index"></asp:Label>
                    </span>
                </div>
                 <div>
                        <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                    </div>
             <div>
                <span>
                    <a href="CustEscalation.aspx" class="aaaa">Cust Incident Home</a>
                </span>
                 </div>

                <div class="edit-contact">
      
                    <ul id="editform" runat="server" >
							
                        <li class="mobile_InboxEmpName">                            
                             <span>Project Name</span>&nbsp;&nbsp;<span style="color: red"></span><br />
                                  
							  <asp:listbox runat="server" id="ddlProjectName"  selectionmode="Multiple">
                            </asp:listbox>
                                    <br />   <br />     
                        </li>
                         <li class="mobile_InboxEmpName">                            
                             <span>Customer Satisfaction Index</span>&nbsp;&nbsp;<span style="color: red"></span><br />
                                    <asp:listbox runat="server" ID="ddlCustSatisfaction" selectionmode="Multiple"  CssClass="DropdownListSearch">
                                    </asp:listbox>
                                    <br />      <br />                     

                         </li>  
                        <li class="mobile_InboxEmpName">      
                        <span>Severity</span>&nbsp;&nbsp;<span style="color: red"></span><br />
                                    <asp:listbox runat="server" ID="ddlSeverity" selectionmode="Multiple"  CssClass="DropdownListSearch">
                                    </asp:listbox>
                                    <br /> <br />
                            </li>
                        <li class="mobile_InboxEmpName">      
                        <span> Impact on Project </span>&nbsp;&nbsp;<span style="color: red"></span><br />
                                    <asp:listbox runat="server" ID="ddlImpactProject" selectionmode="Multiple"  CssClass="DropdownListSearch">
                                    </asp:listbox>
                                    <br /> <br />
                            </li>
						<li class="mobile_InboxEmpName">      
                        <span>Incident Owner </span>&nbsp;&nbsp;<span style="color: red"></span><br />
                                    <asp:listbox runat="server" ID="ddlIncidentOwner" selectionmode="Multiple"  CssClass="DropdownListSearch">
                                    </asp:listbox>
                                    <br /> <br />
                            </li>
						<li class="mobile_InboxEmpName">      
                        <span> Status Name </span>&nbsp;&nbsp;<span style="color: red"></span><br />
                                    <asp:listbox runat="server" ID="ddlStatus" selectionmode="Multiple"  CssClass="DropdownListSearch">
                                    </asp:listbox>
                                    <br /> <br />
                            </li>	
						<li class="mobile_InboxEmpName">      
                        <span> Incident No </span>&nbsp;&nbsp;<span style="color: red"></span><br />
                                    <asp:listbox runat="server" ID="lstIncidentNo" selectionmode="Multiple"  CssClass="DropdownListSearch">
                                    </asp:listbox>
                                    <br /> <br />
                            </li>					
                    </ul>
					 <div class="mobile_Savebtndiv" style="text-align:center; margin-top:-20px !important";" >
        <asp:LinkButton ID="mobile_btnSave" runat="server" Text="Search" ToolTip="Search"  CssClass="Savebtnsve" OnClick="mobile_btnSave_Click" >Search</asp:LinkButton>
		 <%--<asp:LinkButton ID="mobile_cancel" runat="server" Text="Clear" ToolTip="Clear" CssClass="Savebtnsve" OnClick="mobile_cancel_Click" >Clear</asp:LinkButton>--%>          
          <asp:LinkButton ID="mobile_btnBack" runat="server" Text="Clear Search" ToolTip="Clear Search" CssClass="Savebtnsve" OnClick="mobile_btnBack_Click">Clear Search</asp:LinkButton>
		 
       
    </div>
					 <div class="manage_grid" style="width: 100%; height: auto;padding-top:20px">
                    <center>
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
                            <asp:BoundField HeaderText="Customer Request ID"
                                DataField="ServicesRequestID"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="11%" />

							 <asp:BoundField HeaderText="Project Name"
                                DataField="Location_name"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="15%" />

                            <asp:BoundField HeaderText="Creation Date"
                                DataField="ServiceRequestDate"
                                ItemStyle-HorizontalAlign="Center"                                
                                ItemStyle-Width="7%" />

                            <asp:BoundField HeaderText="Employee Name"
                                DataField="EmployeeName"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="14%" />

							 <asp:BoundField HeaderText="Name of Person (Client)"
                                DataField="EscalationRaisedBy"
                                 ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="13%" />

							<asp:BoundField HeaderText="Incident Owner"
                                DataField="IncidentOwner"
                                 ItemStyle-HorizontalAlign="Left"                                
                                ItemStyle-Width="14%" />

                            <asp:BoundField HeaderText="Date of Closure"
                                DataField="DeliveryDate"
                                 ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"                             
                                ItemStyle-Width="8%" />
                                                        
                            <%--<asp:BoundField HeaderText="Assigned To"
                                DataField="AssignedTo"
                                 ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="15%" />

                            <asp:BoundField HeaderText="Assignment Date"
                                DataField="AssignmentDate"
                                 ItemStyle-HorizontalAlign="Center"                                
                                ItemStyle-Width="12%" />--%>

                            <asp:BoundField HeaderText="Status"
                                DataField="Status"
                                 ItemStyle-HorizontalAlign="Center"                                
                                ItemStyle-Width="10%" />

                            <asp:TemplateField HeaderText="View"  HeaderStyle-Width="5%">
                            
								<ItemTemplate>							
                             <asp:ImageButton id="lnkEdit" runat="server" Width="15px" Height="15px" style="padding:0!important;" ImageUrl="~/Images/edit.png" OnClick="lnkFuelDetails_Click"/>                     
							</ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  /> 
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                        </center>
					<br />
					<br />
                </div>

                </div>
            </div>
        </div>
       
    </div>
   
	<br />
    <br />
    
       
    <asp:HiddenField ID="hflEmpDesignation" runat="server" />
    <asp:HiddenField ID="hflEmpDepartment" runat="server" />
    <asp:HiddenField ID="hflEmailAddress" runat="server" />
    <asp:HiddenField ID="hdnJobSitesID" runat="server" />
    <asp:HiddenField ID="hdnGrade" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="FilePath" runat="server" />
    
  

	<script src="../js/jquery.sumoselect-3.3.29/jquery.sumoselect.min.js"></script>
    <link href="../js/jquery.sumoselect-3.3.29/sumoselect.css" rel="stylesheet" />

    <script type="text/javascript">      
        $(document).ready(function () {
           
			$(<%=ddlProjectName.ClientID%>).SumoSelect({
				selectAll: true,
				placeholder: 'Select Project',
				search: true
			});
			$(<%=ddlSeverity.ClientID%>).SumoSelect({
				selectAll: true,
				placeholder: 'Select Severity',
				search: true
			});
			$(<%=ddlCustSatisfaction.ClientID%>).SumoSelect({
				selectAll: true,
				placeholder: 'Select Cust Satisfaction',
				search: true
			});
			$(<%=ddlImpactProject.ClientID%>).SumoSelect({
				selectAll: true,
				placeholder: 'Select Impact Project',
				search: true
			});
			$(<%=ddlIncidentOwner.ClientID%>).SumoSelect({
				selectAll: true,
				placeholder: 'Select Incident Owner',
				search: true
			});
			$(<%=ddlStatus.ClientID%>).SumoSelect({
				selectAll: true,
				placeholder: 'Select Status',
				search: true
			});
			$(<%=lstIncidentNo.ClientID%>).SumoSelect({
				selectAll: true,
				placeholder: 'Select Incident No',
				search: true
			});

        });
   
  
        function SaveMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=mobile_btnSave.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                       Confirm();
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }

        function Confirm() {
            //Testing();
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Submit ?")) {
                confirm_value.value = "Yes";
			}
			else
			{
                confirm_value.value = "No";
            }
            
            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return;

		}
		function noanyCharecters(e) {
			var keynum;
			var keychar;
			var numcheck = /[]/;


			if (window.event) {
				keynum = e.keyCode;
			}
			else if (e.which) {
				keynum = e.which;
			}
			var unicode = e.keyCode ? e.keyCode : e.charCode
			if (unicode == 8 || unicode == 46) {
				keychar = unicode;
			}
			return numcheck.test(keychar);
		}
        
		
        $('.OfferDates').keydown(function (e) {  
			var k;			
			document.all ? k = e.keyCode : k = e.which;
			if (k == 8 ||k == 46)
				return false;
			else
				return true;
			
});
    </script>
</asp:Content>





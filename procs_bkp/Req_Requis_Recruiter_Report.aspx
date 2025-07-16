<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" 
	CodeFile="Req_Requis_Recruiter_Report.aspx.cs" Inherits="Req_Requis_Recruiter_Report" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
      <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <%--<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" /> --%>
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
                        <asp:Label ID="lblheading" runat="server" Text="Recruiter wise Summary Report"></asp:Label>
                    </span>
                </div>
                 <div>
                        <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                    </div>
             <div>
                <span>
                    <a href="Requisition_Index.aspx" class="aaaa">Recruitment  Home</a>
                </span>
                 </div>

                <div class="edit-contact">
      
                    <ul id="editform" runat="server" >
						<li class="mobile_InboxEmpName">      
                        <span>From Date</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                    <asp:TextBox AutoComplete="off" CssClass="txtcls OfferDates"  ID="txtfromdate" runat="server" AutoPostBack="true"  MaxLength="10" OnTextChanged="txtfromdate_TextChanged"   ></asp:TextBox>  
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" Format="dd/MM/yyyy" TargetControlID="txtfromdate" OnClientDateSelectionChanged="checkOffeDate"
                                        runat="server">
                                    </ajaxToolkit:CalendarExtender>
                                  
                            </li>
                        <li class="mobile_InboxEmpName">      
                        <span>To Date</span>&nbsp;&nbsp;<span style="color: red"></span><br />
                                     <asp:TextBox AutoComplete="off" CssClass="txtcls OfferDates"  ID="txttodate" runat="server" AutoPostBack="true" MaxLength="10"  OnTextChanged="txtToDate_TextChanged" ></asp:TextBox>  
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txttodate"  OnClientDateSelectionChanged="checkOffeDate"
                                        runat="server">
                                    </ajaxToolkit:CalendarExtender>                                                                     
                            </li>
                        <li class="mobile_inboxEmpCode">                            
                             <span>Department Name</span>&nbsp;&nbsp;<span style="color: red"></span><br />
                                    <asp:DropDownList runat="server" ID="lstPositionDept" CssClass="DropdownListSearch">
                                    </asp:DropDownList>
                                    <br /> <br />
                        </li>
                         <li class="mobile_InboxEmpName">                            
                             <span>Position Location</span>&nbsp;&nbsp;<span style="color: red"></span><br />
                                    <asp:DropDownList runat="server" ID="lstPositionLoca"  CssClass="DropdownListSearch">
                                    </asp:DropDownList>
                                    <br />  <br />                     

                         </li>  
                        <li class="mobile_InboxEmpName">      
                        <span>Position Criticality</span>&nbsp;&nbsp;<span style="color: red"></span><br />
                                    <asp:DropDownList runat="server" ID="lstPositionCriti"  CssClass="DropdownListSearch">
                                    </asp:DropDownList>
                                    <br /> <br />
                            </li>
                        <li class="mobile_InboxEmpName">      
                        <span> Recruiter Name</span>&nbsp;&nbsp;<span style="color: red"></span><br />
                                    <asp:DropDownList runat="server" ID="lstRecruiter"  CssClass="DropdownListSearch">
                                    </asp:DropDownList>
                                    <br /> <br />
                            </li>
                        
                    </ul>
                </div>
            </div>
        </div>
       
    </div>
     <div class="mobile_Savebtndiv" style="text-align:center">
        <asp:LinkButton ID="mobile_btnSave" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click" >Submit</asp:LinkButton>
         <asp:LinkButton ID="mobile_btnBack" runat="server" Text="Clear" ToolTip="Clear" CssClass="Savebtnsve" OnClick="mobile_btnBack_Click">Clear</asp:LinkButton>
		 
    </div>
	<br />
    <div style="width:100%;overflow:auto">
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="300px" 
        Width="80%" ShowBackButton="False" SizeToReportContent="True" 
        ShowCredentialPrompts="False" ShowDocumentMapButton="False" 
        ShowPageNavigationControls="False" ShowRefreshButton="False">

    </rsweb:ReportViewer>
        </div>
         
   
    <br />
    
       
    <asp:HiddenField ID="hflEmpDesignation" runat="server" />
    <asp:HiddenField ID="hflEmpDepartment" runat="server" />
    <asp:HiddenField ID="hflEmailAddress" runat="server" />
    <asp:HiddenField ID="hdnJobSitesID" runat="server" />
    <asp:HiddenField ID="hdnGrade" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="FilePath" runat="server" />
    
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <script type="text/javascript">      
        $(document).ready(function () {
            
            $(".DropdownListSearch").select2();
            //$("#MainContent_lstInterviewerTwo").select2();

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
            } else {
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
        
		function checkOffeDate(sender, args)
		{
			if (sender._selectedDate >= new Date()) {
				alert("You can not select a future date than today!");
				sender._selectedDate = new Date();
				sender._textbox.set_Value(sender._selectedDate.format(sender._format))
			}
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


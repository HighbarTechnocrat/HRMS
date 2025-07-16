<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
	CodeFile="Ref_Candidate_Approval.aspx.cs" Inherits="procs_Ref_Candidate_Approval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
     <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />

    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .noresize {
            resize: none;
        }

        .aspNetDisabled {
            /*background: #dae1ed;*/
            background: #ebebe4;
        }

        #MainContent_lstApprover {
            overflow: hidden !important;
        }

        .Req_Position {
            padding-bottom: 10px !important;
        }

        #MainContent_Txt_CandidatePAN {
            text-transform: uppercase;
        }

        .DivDropdownlist {
            margin: 0px;
            margin-top: 0px !important;
            padding: 0px !important;
        }
		#MainContent_btnReject
		{
			background: #3D1956;
			color: #febf39 !important;
			padding: 0.5% 1.4%;
			margin: 0% 0% 0 0;
		}
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
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
                        <asp:Label ID="lblheading" runat="server" Text="Refer Candidate Apporval"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>
                <div>
                    <span>
                         <a href="Ref_Employee_Index.aspx" class="aaaa">Emp Referral Home</a>
                    </span>

					<span>
                         <a href="Ref_Moderator_Index.aspx" id="lnkback"  style="margin-right:20px" runat="server"  class="aaaa">Back</a>
                    </span>
					

                </div>
                <div class="edit-contact">
                   <ul id="editform" runat="server">
						<li class="trvl_date" style="padding-bottom: 20px">
							<span style="font-size: 15px; text-decoration: underline">Personal Details: </span>&nbsp;&nbsp;
                            <br />
						</li>
						<li></li>
						<li></li>
						<li class="mobile_inboxEmpCode">
							<span>Name </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_CandidateName" runat="server" MaxLength="50"></asp:TextBox>
						</li>
						<li class="mobile_inboxEmpCode">
							<span>Email </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_CandidateEmail" MaxLength="50" CssClass="email" runat="server"></asp:TextBox>
						</li>
						<li class="mobile_InboxEmpName">
							<span>Mobile </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_CandidateMobile" MaxLength="20" runat="server"></asp:TextBox>
						</li>

					   <li class="mobile_InboxEmpName" style="margin-bottom: 10px">
							<span>Marital Status </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList runat="server" ID="lstMaritalStatus" Enabled="false" CssClass="DropDownSerach">
								<asp:ListItem Value="0" Text="Select Status"></asp:ListItem>
								<asp:ListItem Value="1" Text="Married"></asp:ListItem>
								<asp:ListItem Value="2" Text="UnMarried"></asp:ListItem>
							</asp:DropDownList>
						</li>
						<li class="mobile_InboxEmpName" style="margin-bottom: 10px">
							<span>Gender </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />

							<asp:DropDownList runat="server" ID="lstCandidategender" CssClass="DropDownSerach" Width="98%">
								<asp:ListItem Value="0" Text="Select Gender"></asp:ListItem>
								<asp:ListItem Value="1" Text="Male"></asp:ListItem>
								<asp:ListItem Value="2" Text="Female"></asp:ListItem>
							</asp:DropDownList>

						</li>

						
						<li class="mobile_InboxEmpName" style="margin-bottom: 10px">
							<span>Main Skillset </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:DropDownList runat="server" ID="lstMainSkillset" CssClass="DropDownSerach" Width="98%">
							</asp:DropDownList>
						</li>
					  
						<li>
							<span>Total Experience In(Year) </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_CandidateExperence" Class="number" MaxLength="5" runat="server"></asp:TextBox>
						</li>
					    <li>
							<span>Relevant Experience In(Year) </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="TxtRelevantExpYrs" Class="number" MaxLength="5" runat="server"></asp:TextBox>
						</li>
						  <li></li>
						<li class="mobile_InboxEmpName">
							<span>Additional Skillset </span>&nbsp;&nbsp;<span style="color: red">*</span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_AdditionalSkillset" MaxLength="200" runat="server" TextMode="MultiLine" Rows="5" CssClass="noresize" Width="86%" onKeyUp="javascript:Count(this);"></asp:TextBox>
						</li>
						<li class="mobile_inboxEmpCode">
							<span>Comments </span>
							<br />
							<asp:TextBox AutoComplete="off" ID="Txt_Comments" runat="server" MaxLength="200" TextMode="MultiLine" Rows="5" CssClass="noresize" Width="86%" onKeyUp="javascript:Count(this);"></asp:TextBox>
						</li>
					  
                      <br />
                        <li class="upload">
                            <span>Upload Resume</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                            
                            <asp:LinkButton ID="lnkuplodedfile" runat="server" ForeColor="#ff0000" Width="140%" OnClientClick="return DownloadFile();"></asp:LinkButton>
                        </li>
                        <li></li> <li></li> 
					   <br /> <br />
                       <li class="mobile_inboxEmpCode">
							<span>Apporval Comments </span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtApporvalcmd" runat="server" MaxLength="200" TextMode="MultiLine" Rows="5" CssClass="noresize" Width="86%" onKeyUp="javascript:Count(this);"></asp:TextBox>
						</li>
                    </ul>
					<div style="padding-left:35px">
					<span>Referral Status </span><br />
					<asp:GridView ID="DgvApprover" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="35%">
								<FooterStyle BackColor="White" ForeColor="#000066" />
								<HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
								<PagerStyle ForeColor="#000066" HorizontalAlign="left" BackColor="White" />
								<RowStyle ForeColor="#000066" />
								<SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
								<SortedAscendingCellStyle BackColor="#F1F1F1" />
								<SortedAscendingHeaderStyle BackColor="#007DBB" />
								<SortedDescendingCellStyle BackColor="#CAC9C9" />
								<SortedDescendingHeaderStyle BackColor="#00547E" />

								<Columns>
									<asp:BoundField HeaderText="Approver Name"
										DataField="StatusName"
										ItemStyle-HorizontalAlign="left"
										HeaderStyle-HorizontalAlign="left"
										ItemStyle-Width="25%"
										ItemStyle-BorderColor="Navy" Visible="false" />

									<asp:BoundField HeaderText="Status"
										DataField="StatusName"
										ItemStyle-HorizontalAlign="Center"
										HeaderStyle-HorizontalAlign="Center"
										ItemStyle-Width="20%"
										ItemStyle-BorderColor="Navy" />

									<asp:BoundField HeaderText="Action Date"
										DataField="ActionDate"
										ItemStyle-HorizontalAlign="Center"
										HeaderStyle-HorizontalAlign="Center"
										ItemStyle-Width="15%"
										ItemStyle-BorderColor="Navy" />

									<asp:BoundField HeaderText="Approver Remarks"
										DataField="Comment"
										ItemStyle-HorizontalAlign="left"
										ItemStyle-Width="46%"
										ItemStyle-BorderColor="Navy" Visible="false" />
									
									

									
								</Columns>
							</asp:GridView>
                   </div>
                </div>
            </div>
        </div>

    </div>
    
    <div class="mobile_Savebtndiv">
        <asp:LinkButton ID="mobile_btnSave" runat="server"  Text="Accept" ToolTip="Accept" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();" OnClick="mobile_btnSave_Click">Accept</asp:LinkButton>
        <asp:LinkButton ID="btnReject" runat="server"  Text="Submit" ToolTip="Reject" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();" OnClick="btnReject_Click">Reject</asp:LinkButton>
		<asp:LinkButton ID="mobile_cancel" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/Ref_Moderator_Index.aspx">Back</asp:LinkButton>
    </div>
    <br />
   

    <asp:HiddenField ID="hdCandidate_ID" runat="server" />
    <asp:HiddenField ID="hdnRemid" runat="server" />
	<asp:HiddenField ID="hdnEmpcode" runat="server" />
	<asp:HiddenField ID="hdnCreatedDate" runat="server" />
    <asp:HiddenField ID="hdnsptype" runat="server" />
    <asp:HiddenField ID="hdfilefath" runat="server" />
    <asp:HiddenField ID="hdfilename" runat="server" />
    <asp:HiddenField ID="hdnCandEducationID" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />

    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <script type="text/javascript">

        $(document).ready(function () {
            $(".DropDownSerach").select2();

        });
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
		          
        function DownloadFile() {
            var localFilePath = document.getElementById("<%=hdfilefath.ClientID%>").value;
			var file = document.getElementById("<%=hdfilename.ClientID%>").value;
			
            window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
            // window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
        }

        function DownloadFilemultiple(file) {
            var localFilePath = document.getElementById("<%=hdfilefath.ClientID%>").value;       
           window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
            //window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
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
            keychar = String.fromCharCode(keynum);
            return numcheck.test(keychar);
        }

        function onCharOnlyNumber(e) {
            var keynum;
            var keychar;
            var numcheck = /[0123456789.]/;

            if (window.event) {
                keynum = e.keyCode;
            }
            else if (e.which) {
                keynum = e.which;
            }
            keychar = String.fromCharCode(keynum);
            return numcheck.test(keychar);
        }

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
           
            //document.forms[0].appendChild(confirm_value);
            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return;

        }

    
        $('.number').keypress(function (event) {
            if ((event.which != 46 || $(this).val().indexOf('.') != -1) &&
                ((event.which < 48 || event.which > 57) &&
                    (event.which != 0 && event.which != 8))) {
                event.preventDefault();
            }
            var text = $(this).val();
            if ((text.indexOf('.') != -1) &&
                (text.substring(text.indexOf('.')).length > 2) &&
                (event.which != 0 && event.which != 8) &&
                ($(this)[0].selectionStart >= text.length - 2)) {
                event.preventDefault();
            }
		});

		$(".email").change(function () {    
			var inputvalues = $(this).val();    
			var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
			$('#MainContent_Txt_CandidateEmail').css("background-color", "white");
			if (!regex.test(inputvalues))
			{
				alert("Please enter valid email id");
				 $('#MainContent_Txt_CandidateEmail').css("background-color", "red");
				//$(".email").val("");
			return regex.test(inputvalues);    
			}    
			});    

    </script>
</asp:Content>


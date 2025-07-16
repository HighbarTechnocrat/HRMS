<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="EmployeeReferralView.aspx.cs" 
    Inherits="procs_EmployeeReferralView" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
	Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

	<link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Req_Requisition.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />
	<%-- Rec_AddjoinDetailbtn--%>
	<style>
		.OfferFileUplad {
			min-width: 0;
			min-height: 0;
			word-wrap: break-word;
			word-break: break-all;
			max-width: 100%;
		}

		.myaccountpagesheading {
			text-align: center !important;
			text-transform: uppercase !important;
		}

		.aspNetDisabled {
			/*background: #dae1ed;*/
			background: #ebebe4;
		}

		.taskparentclasskkk {
			width: 29.5%;
			height: 72px;
			/*overflow:initial;*/
		}

		#MainContent_lstApprover, #MainContent_lstIntermediate {
			padding: 0 0 33px 0 !important;
			/*overflow: unset;*/
		}

		.Dropdown {
			border-bottom: 2px solid #cccccc;
			/*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
			background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;
			cursor: default;
		}

		iframe1 {
			pointer-events: none !important;
			/*//opacity: 0.8 !important;*/
		}

		.noresize {
			resize: none;
		}
	</style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

	<script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
	<script src="../js/HtmlControl/jquery-1.3.2.js"></script>
	<script src="../js/dist/jquery-3.2.1.min.js"></script>
	<script src="../js/HtmlControl/jHtmlArea-0.8.min.js"></script>
	<link href="../js/HtmlControl/jHtmlArea.css" rel="stylesheet" />
	<script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>

	<div class="commpagesdiv">
		<div class="commonpages">
			<div class="wishlistpagediv">
				<div class="userposts">
					<span>
						<asp:Label ID="lblheading" runat="server" Text="Employee Referral - Information"></asp:Label>
					</span>
				</div>
				<div>
					<asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
				</div>
				<div>
					<span style="margin-bottom: 20px">
						<a href="Requisition_Index.aspx" class="aaaa">Recruitment Home</a>
					</span>
					<span>
						<a href="Ref_Employee_Index.aspx" title="Back" runat="server" id="btnRecBack" style="margin-right: 10px;" class="aaaa">Back</a>
					</span>
					<br />
				</div>
				<div class="edit-contact">

					<ul id="editform" runat="server">
						<li></li>
						<li></li>
						<li></li>
						<li class="trvl_date">
							<span>Position Title  </span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtPostionTitle" runat="server" Enabled="false"></asp:TextBox>
						</li>

						<li class="trvl_date">
							<span>No. of Positions </span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtNoofPostions" runat="server" Enabled="false"></asp:TextBox>
						</li>

						<li class="trvl_date">
							<span>Skills Required </span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtSkillRequired" runat="server" Enabled="false"></asp:TextBox>
						</li>
						<li class="trvl_date">
							<span>Exp Required </span>
							<br />
							<asp:TextBox AutoComplete="off" ID="txtExpRequired" runat="server" Enabled="false"></asp:TextBox>
						</li>
						<li class="trvl_date">
						</li>
						<li class="trvl_date">
						</li>
						<div id="DivRecruitment" class="edit-contact" runat="server">

							<ul id="Ul3" runat="server">

								<li class="Req_Requi_Esse">
									</li>
								<li class="Req_Requi_Esse">
									</li>

								<li class="Req_Job_Desc">
                                    	<span>Job Description</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
									<asp:TextBox AutoComplete="off" ID="txtJobDescription" runat="server" CssClass="noresize" Rows="7" TextMode="MultiLine" Enabled="false"></asp:TextBox>
								
								</li>
								<li></li>
								<li></li>
								<li></li>

							</ul>
						</div>
					</ul>
				</div>
			</div>
			<br />
			<br />
			
			<br />
		</div>
	</div>

	<asp:HiddenField ID="hdnSaveStatusFlag" runat="server" />
	<asp:HiddenField ID="hdPostiontitleID" runat="server" />
    <asp:HiddenField ID="HDModuleID" runat="server" />
	<asp:HiddenField ID="hdnYesNo" runat="server" />
	
	<script src="../js/dist/select2.min.js"></script>
	<link href="../js/dist/select2.min.css" rel="stylesheet" />

	<script type="text/javascript">      
		$(document).ready(function () {
			$("#MainContent_txtJobDescription").htmlarea();
			
		});
	</script>

	
</asp:Content>


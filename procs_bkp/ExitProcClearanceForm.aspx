<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="ExitProcClearanceForm.aspx.cs" Inherits="procs_ExitProcClearanceForm" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/ClaimMobile_css.css" type="text/css" media="all" />
    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }


        .Dropdown {
            border-bottom: 2px solid #cccccc;
            /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
            background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;
            cursor: default;
        }

        .grayDropdown {
            border-bottom: 2px solid #cccccc;
            /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
            background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;
            background-color: #ebebe4;
        }

        .grayDropdownTxt {
            background-color: #ebebe4;
        }

        .taskparentclass2 {
            width: 29.5%;
            height: 112px;
        }

        .taskparentclasskkk {
            width: 29.5%;
            height: 72px;
            /*overflow:initial;*/
        }

        #MainContent_ListBox1, #MainContent_ListBox2 {
            padding: 0 0 0% 0 !important;
            /*overflow: unset;*/
        }

        #MainContent_lstApprover, #MainContent_lstIntermediate {
            padding: 0 0 33px 0 !important;
            /*overflow: unset;*/
        }

        #cssTable td {
            text-align: center;
            vertical-align: middle;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
    <script type="text/javascript">
        var deprt;
        $(document).ready(function () {
        });


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
            var confirmval = false;
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Are you sure you want to submit?")) {
                confirm_value.value = "Yes";
                confirmval = true;
            } else {
                confirm_value.value = "No";
                confirmval = false;
            }
            //alert(confirm_value.value);
            //document.forms[0].appendChild(confirm_value);
            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return confirmval;

        }

    </script>

	<div id="loader" class="myLoader" style="display:none">
        <div class="loaderContent">
			<span class="DONot">Please  Do Not Refresh  or Close Page</span>
			<img src="../images/loader.gif" ></div>
		
    </div>
    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">

                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Clearance Form"></asp:Label>
                    </span>
                </div>
                <div class="edit-contact">
                    <a href="ExitProcess_Index.aspx" class="aaa">Exit Process Menu</a>
                    <%--<asp:Panel ID="pnlSurvey" runat="server">
                        </asp:Panel>--%>
                    <ul id="CreateExitSurveyform" runat="server" visible="true">
                        <li>
                            <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                            <%--<asp:TextBox runat="server" ID="lblmessage1" Visible="False" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;" OnTextChanged="lblmessage1_TextChanged"></asp:TextBox>--%>
                        </li>
                        <br />
                        <li><b>Project Name</b>
                            <asp:TextBox ID="txtProjectName" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <br />
                        <li><b>Resignation Date</b>
                            <asp:TextBox ID="txtResignationDate" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <br />
                        <li><b>Name</b>
                            <asp:TextBox ID="txtName" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <br />
                        <li><b>Designation & Grade</b>
                            <asp:TextBox ID="txtDesignationGrade" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <br />
                        <li><b>Date of Joining</b>
                            <asp:TextBox ID="txtDoJ" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <br />
                        <li><b>Last Working Day</b>
                            <asp:TextBox ID="txtLastWorkingDay" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <br />
                        <li><b>Release Date</b>
                            <asp:TextBox ID="txtReleaseDate" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <br />
                        <li id="RelDt" runat="server" visible="false"><b>Release Date</b>
                            <asp:TextBox ID="txtDateRelease" runat="server" Enabled="false" AutoPostBack="true" OnTextChanged="txtDateRelease_TextChanged" AutoCompleteType="Disabled" autocomplete="off" MaxLength="10"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDateRelease"
                                Format="dd/MM/yyyy" runat="server">
                            </ajaxToolkit:CalendarExtender>

                        </li>
                    </ul>

                </div>
                <%--<asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />--%>
            </div>

        </div>
    </div>
    <div class="mobile_Savebtndiv">
        <%--<asp:LinkButton ID="btnSubmit" runat="server" Text="Submit" ToolTip="Submit" CssClass="Savebtnsve" OnClick="btnSubmit_Click">Submit</asp:LinkButton> OnClientClick="return SaveMultiClick();"--%>
        <%--<asp:Button ID="btnSubmit" runat="server" Text="Submit For Approval" OnClick="btnSubmit_Click" OnClientClick="Confirm()" CssClass="Savebtnsve" />--%>

        <asp:LinkButton ID="mobile_btnSave" runat="server" Text="Submit For Approval" ToolTip="Save" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click" OnClientClick="StartLoader()" >Submit</asp:LinkButton>
        <asp:LinkButton ID="mobile_cancel" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="Savebtnsve" OnClick="mobile_cancel_Click">Cancel</asp:LinkButton>
    </div>
    <asp:HiddenField ID="hdnRsID" runat="server" />
        <asp:HiddenField ID="hdnYesNo" runat="server" />
    <br />

	<link href="../includes/loader.css" rel="stylesheet" />

    <script type="text/javascript">
  

        $(document).ready(function () {
            $(".DropdownListSearch").select2(); 
        }); 

        function StartLoader() {
			
			$('#loader').show();
		}
		function StopLoader() {
				$('#loader').hide();
		}

    </script>

</asp:Content>


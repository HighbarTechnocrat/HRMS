<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="Rec_RecruiterCandidateView.aspx.cs" 
      MaintainScrollPositionOnPostback="true" Inherits="Rec_RecruiterCandidateView" %>


<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
     <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" /> 
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />

   

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
        .gridpager, .gridpager td
    {
       padding-left: 5px;
       text-align: right;
    }  
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>

     
    <script src="../js/HtmlControl/jquery-1.3.2.js"></script>
    <script src="../js/dist/jquery-3.2.1.min.js"></script>       
    <script src="../js/HtmlControl/jHtmlArea-0.8.min.js"></script>
    <link href="../js/HtmlControl/jHtmlArea.css" rel="stylesheet"/>


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
                        <asp:Label ID="lblheading" runat="server" Text="Verify Candidate Data"></asp:Label>
                    </span>
                </div>
                <%-- <div>
                        <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                    </div>--%>
                     <div>
                        <asp:Label runat="server" ID="lblmessagesearch" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                    </div>
             <div>
                <span style="margin-bottom:20px">
                    <a href="Requisition_Index.aspx" class="aaaa">Recruitment Home</a>
                </span>
				 
                 </div>

                <div class="edit-contact">
                    <ul id="Ul1" runat="server" visible="true">
                        <li>
                            <span><b runat="server" id="lblAssgineTitle">Candidate Details</b></span>
                            <br />
                        </li>
                        <li style="visibility: hidden"></li>
                        <li style="visibility: hidden"></li>
                        <li class="claimmob_Amount">
                            <span>Salutation: </span>&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList ID="DDl_Salutation" runat="server" Width="220px" Enabled="false" >
                                <asp:ListItem Selected="True" Text="Select Type" Value="0">Select Salutation</asp:ListItem>
                                <asp:ListItem Text="Mr." Value="Mr.">Mr.</asp:ListItem>
                                <asp:ListItem Text="Ms." Value="Ms.">Ms.</asp:ListItem>
                                <asp:ListItem Text="Mrs." Value="Mrs.">Mrs.</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li style="visibility: hidden"></li>
                        <li style="visibility: hidden"></li>
                        <li class="mobile_inboxEmpCode">
                            <br />
                            <span>First Name </span>&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox Enabled="false" AutoComplete="off" ID="txtFirstName" runat="server" Visible="true" MaxLength="40"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Middel Name </span>&nbsp;
                            <br />
                            <asp:TextBox Enabled="false" AutoComplete="off" ID="txtMiddelName" runat="server" Visible="true" MaxLength="40"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Last Name </span>&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox Enabled="false" AutoComplete="off" ID="txtLastName" runat="server" Visible="true" MaxLength="40"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Mother Name </span>&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox Enabled="false" AutoComplete="off" ID="txtMotherName" runat="server" Visible="true" MaxLength="100"></asp:TextBox>
                        </li>
                        <li class="claimmob_Amount">
                            <span>Gender: </span>&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList ID="ddlGender" runat="server" Width="220px" Enabled="false" onchange="jsFunction_Gender();">
                                <asp:ListItem Selected="True" Text="Select Type" Value="0">Select Gender</asp:ListItem>
                                <asp:ListItem Text="Male" Value="Male">Male</asp:ListItem>
                                <asp:ListItem Text="Female" Value="Female">Female</asp:ListItem>
                            </asp:DropDownList>
                        </li>

                        <li class="mobile_inboxEmpCode">
                            <span>Marrital Status:</span>&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList Enabled="false" ID="DDl_Mar_Stat" runat="server" Width="220px">
                                <asp:ListItem Selected="True" Text="Select Type" Value="0">Select Marrital Status</asp:ListItem>
                                <asp:ListItem Text="Single" Value="Single">Single</asp:ListItem>
                                <asp:ListItem Text="Married" Value="Married">Married</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li>
                            <span style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">Birthdate:</span>&nbsp;<span style="color: red">*</span><br />
                            <asp:TextBox Enabled="false" ID="txtDOB" AutoComplete="off" runat="server" AutoPostBack="true" OnTextChanged="txtDOB_TextChanged" MaxLength="10" AutoCompleteType="Disabled"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd-MM-yyyy" TargetControlID="txtDOB"
                                runat="server"></ajaxToolkit:CalendarExtender>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Mobile No: </span>&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox Enabled="false" AutoComplete="off" ID="txtMobileNo" runat="server" Visible="true" MaxLength="15"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Personal Email ID: </span>&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox  Enabled="false" AutoComplete="off" ID="txtEmailAddress" runat="server" Visible="true" MaxLength="150"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Telephone No: </span>
                            <br />
                            <asp:TextBox Enabled="false" AutoComplete="off" ID="txtTelephoneNo" runat="server" Visible="true" MaxLength="20"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Emergency Contact Person: </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox Enabled="false" AutoComplete="off" ID="txtEmergencyCName" runat="server" Visible="true" MaxLength="100"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Emergency Contact No: </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox Enabled="false" AutoComplete="off" ID="txtEmergencyCNo" runat="server" Visible="true" MaxLength="20"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Name As Per PAN : </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox Enabled="false" AutoComplete="off" ID="txtNameAsPerPAN" runat="server" Visible="true" MaxLength="100"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>PAN Number: </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox Enabled="false" AutoComplete="off" ID="txtPAN" runat="server" Visible="true" MaxLength="20"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Blood Group: </span>&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox Enabled="false" AutoComplete="off" ID="txtBloodGroup" runat="server" Visible="true" MaxLength="10"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Name As Per Aadhaar: </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox Enabled="false" AutoComplete="off" ID="txtNameAsPerAadhaar" runat="server" Visible="true" MaxLength="100"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Aadhaar Number: </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox Enabled="false" AutoComplete="off" ID="txtAadhaarNo" runat="server" Visible="true" MaxLength="20"></asp:TextBox>
                        </li>
                        <li style="visibility: hidden"></li>
                        <li class="claimmob_Amount">
                            <span>PF account No with previous employer(Yes/No): </span>&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList Enabled="false" ID="ddlPFAccount" runat="server" Width="220px" AutoPostBack="true" OnSelectedIndexChanged="ddlPFAccount_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Text="Select Type" Value="0">Select PF account No with previous employer</asp:ListItem>
                                <asp:ListItem Text="Yes" Value="Yes">Yes</asp:ListItem>
                                <asp:ListItem Text="No" Value="No">No</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li class="mobile_inboxEmpCode" runat="server" id="IsPFAccount">
                            <span>PF Account Number: </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox Enabled="false" AutoComplete="off" ID="txtPFNumber" runat="server" Visible="true" MaxLength="50"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode" runat="server" id="IsPFAccount1">
                            <span>UAN Number:: </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox  AutoComplete="off" Enabled="false" ID="txtUANNumber" runat="server" Visible="true" MaxLength="50"></asp:TextBox>
                        </li>
                        <li class="claimmob_Amount">
                            <span>Pension Account With Previous employer(Yes/No): </span>&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList Enabled="false" ID="ddlPensionAccount" runat="server" Width="220px" AutoPostBack="true" OnSelectedIndexChanged="ddlPensionAccount_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Text="Select Type" Value="0">Select Pension Account With Previous employer:</asp:ListItem>
                                <asp:ListItem Text="Yes" Value="Yes">Yes</asp:ListItem>
                                <asp:ListItem Text="No" Value="No">No</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                        </li>
                        <li class="mobile_inboxEmpCode" runat="server" id="IsPensionAccount">
                            <span>Pention account no: </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txtPentionAccNo" runat="server" Visible="true" MaxLength="50"></asp:TextBox>
                        </li>
                        <li style="visibility: hidden"></li>
                        <li class="mobile_inboxEmpCode">
                            <span>Bank Name: </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txtBankName1" runat="server" Visible="true" MaxLength="100"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Bank Account Number: </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txtBankAccNo" runat="server" Visible="true" MaxLength="50"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Name As Per Bank Account  : </span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txtBankAccName" runat="server" Visible="true" MaxLength="100"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>IFSC Code: </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txtIFSCCode" runat="server" Visible="true" MaxLength="50"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>MICR Code: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txtMICRCode" runat="server" Visible="true" MaxLength="50"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Branch Number: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txtBranchNumber" runat="server" Visible="true" MaxLength="50"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Bank Address: </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txtBankAddress" runat="server" CssClass="noresize" Visible="true" MaxLength="250" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </li>
                        <li style="visibility: hidden"></li>
                        <li style="visibility: hidden"></li>
                        <li>
                            <span>Do you have passport?</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:CheckBox TextAlign="Right" Enabled="false" Visible="false" Text="Do you have passport?" ID="chk_IsPassport" runat="server" AutoPostBack="true" OnCheckedChanged="chk_IsPassport_CheckedChanged" />
                             <asp:DropDownList runat="server" Enabled="false"  ID="ddl_Passport" AutoPostBack="true" OnSelectedIndexChanged="ddl_Passport_SelectedIndexChanged">
                                <asp:ListItem Value="0" Text="Do you have passport Yes/No? ">Do you have passport Yes/No?</asp:ListItem>
                                <asp:ListItem Value="Yes" Text="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="No" Text="No">No</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Name as per Passport: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txtNameAsPerPassport" runat="server" Visible="true" MaxLength="100"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Date Of Issue  : </span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txtDateOfIssue" AutoPostBack="true" OnTextChanged="txtDateOfIssue_TextChanged" runat="server" Visible="true" MaxLength="15"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd-MM-yyyy" TargetControlID="txtDateOfIssue"
                                runat="server"></ajaxToolkit:CalendarExtender>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Expiry Date: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" AutoPostBack="true" ID="txtDateOfExpiry" OnTextChanged="txtDateOfExpiry_TextChanged" runat="server" Visible="true" MaxLength="15"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" Format="dd-MM-yyyy" TargetControlID="txtDateOfExpiry"
                                runat="server"></ajaxToolkit:CalendarExtender>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Place Of Issue: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txtPlaceOfIssue" runat="server" Visible="true" MaxLength="100"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Passport No: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txtPassportNo" runat="server" Visible="true" MaxLength="50"></asp:TextBox>
                        </li>
                        
                        <li>
                            <span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b runat="server" id="B1">&nbsp;&nbsp;&nbsp;Permanent Address</b></span>
                            <br />
                        </li>
                        <li style="width:50% !important">
                            <span><b runat="server" id="B2">&nbsp;&nbsp;&nbsp;Current Address</b></span>
                            &nbsp;&nbsp;&nbsp;<asp:CheckBox Enabled="false" TextAlign="Right" Text="Same As Permanent Address" ID="chkSameAsPer" runat="server" AutoPostBack="true" OnCheckedChanged="chkSameAsPer_CheckedChanged" />
                            <br />
                        </li>
                        <%--<li style="visibility: hidden"></li>--%>
                        <li class="clent_address">
                            <br />
                            <span>Address </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txtPAddress" runat="server" CssClass="noresize" Visible="true" MaxLength="300" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </li>
                        <li class="clent_address">
                            <br />
                            <span>Address </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtCAddress" Enabled="false" runat="server" CssClass="noresize" Visible="true" MaxLength="300" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </li>
                        <li style="visibility: hidden"></li>
                        <li class="mobile_inboxEmpCode">
                            <span>City: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txtPCity" runat="server" Visible="true" MaxLength="100"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>City: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txtCCity" runat="server" Visible="true" MaxLength="100"></asp:TextBox>
                        </li>
                        <li style="visibility: hidden"></li>
                        <li class="mobile_inboxEmpCode">
                            <span>State: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txtPState" runat="server" Visible="true" MaxLength="100"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>State: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txtCState" runat="server" Visible="true" MaxLength="100"></asp:TextBox>
                        </li>
                        <li style="visibility: hidden"></li>
                        <li class="mobile_inboxEmpCode">
                            <span>Country: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txtPCountry" runat="server" Visible="true" MaxLength="100"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Country: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txtCCountry" runat="server" Visible="true" MaxLength="100"></asp:TextBox>
                        </li>
                        <li style="visibility: hidden"></li>
                        <li class="mobile_inboxEmpCode">
                            <span>PIN: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txtPPIN" runat="server" Visible="true" MaxLength="10"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>PIN: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txtCPIN" runat="server" Visible="true" MaxLength="10"></asp:TextBox>
                        </li>
                        <li style="visibility: hidden"></li>                       
                        <hr />
                        <li class="mobile_inboxEmpCode" style="width: 100% !important">
                            <span><b>Family Details</b> </span><br />                           
                        </li>                     
                        <li style="width: 100%">
                            <br />
                            <div>
                                <asp:GridView ID="dg_FimalyDetails" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                    DataKeyNames="Id,RelationId">
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                    <PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Name"
                                            DataField="Name"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="40%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Relation"
                                            DataField="RelationName"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Date Of Birth"
                                            DataField="DOB"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Contact No."
                                            DataField="ContactNo"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="30%" ItemStyle-BorderColor="Navy" />

                                     <%--   <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnk_FD_Edit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnk_FD_Edit_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnk_FD_Delete" runat="server" Width="15px" Height="15px" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure delete?');" OnClick="lnk_FD_Delete_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>--%>

                                    </Columns>
                                </asp:GridView>
                            </div>
                            <br />
                        </li>
                        <hr />
                        <li class="mobile_inboxEmpCode">
                            <span><b>Education Details</b></span><br />                          
                        </li>
                        <li>
                            <asp:Label runat="server" ID="Label2" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                        </li>
                        <li style="width: 100%">
                            <br />
                            <div>
                                <asp:GridView ID="gv_EducationDetails" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                    DataKeyNames="Id,QualificationId">
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                    <PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Qualification"
                                            DataField="EducationType"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="7%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="University/Institute"
                                            DataField="University_Institute"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="13%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Board"
                                            DataField="Board"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="13%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Degree"
                                            DataField="Degree"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="7%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Stream"
                                            DataField="Stream"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="7%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Date of Passing"
                                            DataField="YearOfPassing"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="9%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Marks Obtained"
                                            DataField="MarksObtained"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="7%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Total  Marks"
                                            DataField="TotalMarks"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="7%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="% Marks"
                                            DataField="Grade"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />

                                       <%-- <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="0%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                 <asp:Label ID="lblQualificationId" runat="server" Text='<%# Eval("QualificationId") %>' Visible="false" />
                                                <asp:ImageButton ID="lnk_ED_Edit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnk_ED_Edit_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnk_ED_Delete" runat="server" Width="15px" Height="15px" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure delete?');" OnClick="lnk_ED_Delete_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <br />
                        </li>
                        <hr />
                        <li>
                            <span><b>Project Details </b></span>
                        </li>
                        <li>
                            <asp:Label runat="server" ID="Label4" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                        </li>
                        <li></li>
                        <li style="width:100%">
                            <br />
                            <div>
                                <asp:GridView ID="gv_ProjectDetails" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                    DataKeyNames="Id">
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                    <PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Project Type"
                                            DataField="ProjectType"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="9%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Client Name"
                                            DataField="ClientName"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Organisation Name"
                                            DataField="OrganisationName"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Organisation Type"
                                            DataField="OrganisationType"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="9%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Industry Type"
                                            DataField="IndustryType"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Modules Integration"
                                            DataField="ModuleDesc"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Role/Designation"
                                            DataField="Designation"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="From Date"
                                            DataField="FromDate"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="9%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="To Date"
                                            DataField="ToDate"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="9%" ItemStyle-BorderColor="Navy" />
                                        <%--  <asp:BoundField HeaderText="Brief Summary of Role"
                                            DataField="BriefSummaryOfRole"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />--%>
                                       <%-- <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                               
                                                <asp:ImageButton ID="lnk_PD_Edit" runat="server" Width="13px" Height="13px" ImageUrl="~/Images/edit.png" OnClick="lnk_PD_Edit_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnk_PD_Delete" runat="server" Width="13px" Height="13px" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure delete?');" OnClick="lnk_PD_Delete_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <br />
                        </li>
                        <hr />
                        <li>
                            <span><b runat="server" id="B3">Upload Document</b></span>
                            <br />
                        </li>
                        <li style="visibility: hidden"></li>
                        <li style="visibility: hidden"></li>
                        <li style="width: 100% !important">
                            <br />
                            <asp:GridView ID="gvCandidateDocument" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%"
                                DataKeyNames="Id,IsRequired" AllowPaging="false">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                <PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" />
                                <RowStyle ForeColor="#000066" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />

                                <Columns>
                                    <asp:BoundField HeaderText="Document Type"
                                        DataField="DocumentType"
                                        ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="15%" />
                                      <asp:BoundField HeaderText="File Name"
                                        DataField="DocumentPath"
                                        ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="15%" />
                                    <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" HeaderText="File Descripation" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("IsRequired") %>' Visible="false" />
                                            <asp:Label ID="lblid" runat="server" Text='<%# Eval("Id") %>' Visible="false" />
                                            <asp:Label ID="lblDocumentType" runat="server" Text='<%# Eval("DocumentType") %>' Visible="false" />
                                            <asp:TextBox Enabled="false" CssClass="test12" AutoComplete="off" ID="txtFileDesc" Text='<%# Bind("Document_desc") %>' runat="server" Width="81%" Visible="true" MaxLength="10"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                 
                                    <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkEdit1" runat="server" ToolTip='<%# Bind("DocumentPath") %>' Width="15px" Height="15px" ImageUrl="~/Images/Download.png" OnClientClick=<%#"DownloadFile('" + Eval("DocumentPath") + "')" %> />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                    </asp:TemplateField>
                                   <%-- <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkDelete" runat="server" Width="15px" Height="15px" ImageUrl="~/images/delete.png" OnClientClick="return DeleteClick();" OnClick="lnkDelete_Click" />

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>
                        </li>
                        <hr />
                        <li>
                            <span>Select Status</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList runat="server" ID="ddl_Status" AutoPostBack="false" OnSelectedIndexChanged="ddl_Status_SelectedIndexChanged">
                                <asp:ListItem Value="0" Text="Select Status ">Please Select Status?</asp:ListItem>
                                <asp:ListItem Value="Completed" Text="Completed">Completed</asp:ListItem>
                                <asp:ListItem Value="In-Completed" Text="In-Complete">Correction</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li class="mobile_inboxEmpCode" runat="server" id="showRemarkDiv">
                            <span>Remark: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="true" ID="txtRemark_1" runat="server" Visible="true" MaxLength="100"></asp:TextBox>
                        </li>
                        <li>
                            <asp:Label runat="server" ID="lblErrorMessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                        </li>
                        <li>
                            <asp:LinkButton ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" CssClass="Savebtnsve" OnClick="btnSave_Click">Submit</asp:LinkButton>
                            <%--<asp:LinkButton ID="btnEdit" runat="server" Text="Save As Draft" ToolTip="Save As Draft" CssClass="Savebtnsve" OnClick="btnEdit_Click" OnClientClick="return EditMultiClick();">Save As Draft</asp:LinkButton>--%>
                            <asp:LinkButton ID="btnBack" Visible="true" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/Rec_RecruiterCandidateInbox.aspx">Back</asp:LinkButton>
                            <br />
                            <br />
                          
                            <br />
                        </li>
                        <li ></li>
                        <li ></li>
                    </ul>
                     <asp:HiddenField ID="hdCandidate_ID" runat="server" /> 
                </div>
            </div>
        </div>
    </div>
    <br />
   
    <br />
   
    <asp:HiddenField ID="hdLinkType" runat="server" />
    <asp:HiddenField ID="HFRecruitment_ReqID" runat="server" />
    <asp:HiddenField ID="HFCandidateID" runat="server" />
    <asp:HiddenField ID="HFISLMID" runat="server" />
     <asp:HiddenField ID="HFCandidateScheduleRound_ID" runat="server" />
     <asp:HiddenField ID="FilePath" runat="server" />
     <asp:HiddenField ID="login_EmpCode" runat="server" />
     <asp:HiddenField ID="login_EmpName" runat="server" />
     <asp:HiddenField ID="login_EmpEmail" runat="server" />

     <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" /> 

     <script type="text/javascript">      
        $(document).ready(function () {
            $(".DropdownListSearch").select2();
            $("#MainContent_DDl_Salutation").select2();
            $("#MainContent_DDl_Mar_Stat").select2();
            $("#MainContent_ddlGender").select2();
            $("#MainContent_ddlPFAccount").select2();
            $("#MainContent_ddlPensionAccount").select2();
            $("#MainContent_ddl_Passport").select2();
            $("#MainContent_ddl_Status").select2();
        });
         function SaveMultiClick() {
             //alert();
             var msg = "Sure to submit this status?"
             try {
                 var retunboolean = true;
                 var ele = document.getElementById('<%=btnSave.ClientID%>');

                 if (ele != null && !ele.disabled)
                     retunboolean = true;
                 else
                     retunboolean = false;
                 if (ele != null) {
                     ele.disabled = true;
                     if (retunboolean == true)
                         retunboolean = Confirm(msg);

                     if (retunboolean == false)
                         ele.disabled = false;
                 }
             }
             catch (err) {
                 alert(err.description);
             }
             return retunboolean;
         }
        
         function DownloadFile(FileName) {

             var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;
                     //alert(FileName);        
                     //window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
                     window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
                 }
    </script>

</asp:Content>


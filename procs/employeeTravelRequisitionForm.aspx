<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="employeeTravelRequisitionForm.aspx.cs"
    Inherits="procs_employeeTravelRequisitionForm" EnableViewState="true" ValidateRequest="false" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <%-- <link href="<%=ReturnUrl("css") %>calendar/calendar.css" rel="stylesheet" type="text/css" />
    <link href="sampleform_css.css" rel="stylesheet" type="text/css" />--%>
    <%--<ajaxToolkit:ToolkitScriptManager ID="srcp" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <style>
        .myaccountpagesheading {
            text-align: center;
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

        .textboxBackColor {
            background-color: cadetblue;
            color: aliceblue;
            text-align: right;
        }

        .textboxAlignAmount {
            text-align: right;
        }


        .BtnShow {
            color: blue !important;
            background-color: transparent;
            text-decoration: none;
            font-size: 13px !important;
        }

            .BtnShow:visited {
                color: blue !important;
                background-color: transparent;
                text-decoration: none;
            }

            .BtnShow:hover {
                color: red !important;
                background-color: transparent;
                text-decoration: none !important;
            }

        .POWOContentTextArea {
            width: 783px !important;
            height: 400px !important;
            overflow: auto;
        }

        .LableName {
            color: #F28820;
            font-size: 16px;
            font-weight: normal;
            text-align: left;
        }

        .boxTest {
            BORDER-RIGHT: black 1px solid;
            BORDER-TOP: black 1px solid;
            BORDER-LEFT: black 1px solid;
            BORDER-BOTTOM: black 1px solid;
            BACKGROUND-COLOR: White;
        }

        .GoalDecriptionTextArea {
            width: 722px !important;
            height: 250px !important;
        }

        .txtRemarks {
            width: 623px;
            height: 77px;
        }

        .BtnShow {
            color: blue !important;
            background-color: transparent;
            text-decoration: none;
            font-size: 13px !important;
        }

            .BtnShow:visited {
                color: blue !important;
                background-color: transparent;
                text-decoration: none;
            }

            .BtnShow:hover {
                color: red !important;
                background-color: transparent;
                text-decoration: none !important;
            }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="commonpages">
        <div class="wishlistpagediv">
            <div class="userposts">
                <span>
                    <br />
                    <asp:Label ID="Label2" runat="server" Text="Employee Travel Requisition Form"></asp:Label>
                </span>


            </div>
            <span>
                <a href="TravelRequisition_Index.aspx" class="aaaa">Travel Requisition Index</a>
            </span>

            <div>
                <asp:Label runat="server" ID="Label3" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
            </div>
            <div class="edit-contact">

                <div class="editprofile btndiv" id="div1" runat="server" visible="false">
                    <div class="cancelbtndiv">
                        <asp:LinkButton ID="LinkButton1" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                    </div>
                    <div class="cancelbtndiv">
                        <asp:LinkButton ID="LinkButton2" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" Visible="false"></asp:LinkButton>
                    </div>
                </div>
            </div>

        </div>
    </div>




    <div class="commonpages">
        <div class="wishlistpagediv">
            <div class="userposts">
            </div>
            <div>
                <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
            </div>
            <div class="edit-contact">

                <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                    <div class="cancelbtndiv">
                        <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                    </div>
                    <div class="cancelbtndiv">
                        <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" Visible="false"></asp:LinkButton>
                    </div>
                </div>

                <ul id="editform" runat="server" visible="true">
                    <li class="trvl_type">
                        <span>Employee Code</span>&nbsp;&nbsp;<br />
                        <asp:TextBox AutoComplete="off" ID="txtEmpcode" runat="server" ReadOnly="true" MaxLength="150" Enabled="False"></asp:TextBox>

                    </li>
                    <li class="trvl_date">
                        <span>Employee Name</span>&nbsp;&nbsp;<br />
                        <asp:TextBox AutoComplete="off" ID="txtEmpName" runat="server" MaxLength="15" ReadOnly="true" Enabled="False"></asp:TextBox>
                    </li>
                    <li class="trvl_date">
                        <span>Department </span>
                        <br />
                        <asp:TextBox AutoComplete="off" ID="txtDepartment" runat="server" MaxLength="45" ReadOnly="true" Enabled="False"></asp:TextBox>
                    </li>


                    <li class="trvl_date">
                        <span>Designation</span>&nbsp;&nbsp;<br />
                        <asp:TextBox AutoComplete="off" ID="txtDesignation" runat="server" MaxLength="45" ReadOnly="true" Enabled="False"></asp:TextBox>
                    </li>

                    <li class="trvl_date">
                        <span>Band</span>&nbsp;&nbsp;<br />
                        <asp:TextBox AutoComplete="off" ID="txtband" runat="server" MaxLength="45" ReadOnly="true" Enabled="False"></asp:TextBox>
                    </li>

                    <li class="trvl_type">
                        <span>Name As Per Aadhar</span>&nbsp;&nbsp;<br />
                        <asp:TextBox AutoComplete="off" ID="txtEmpName_Aadhar" runat="server" MaxLength="150" Enabled="true"></asp:TextBox>

                    </li>
                    <li class="trvl_date">
                        <span>Mobile No.</span>&nbsp;&nbsp;<br />
                        <asp:TextBox AutoComplete="off" ID="txtMobile" runat="server" MaxLength="15" Enabled="true"></asp:TextBox>
                    </li>

                    <li class="trvl_date">

                        <span>From Date</span> &nbsp;&nbsp;<span style="color: red">*</span>
                        <asp:TextBox AutoComplete="off" ID="txtFromDate" runat="server" AutoPostBack="true" OnTextChanged="txtFromDate_TextChanged" MaxLength="15" AutoCompleteType="Disabled"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd-MM-yyyy" TargetControlID="txtFromDate"
                            runat="server">
                        </ajaxToolkit:CalendarExtender>
                    </li>

                    <li class="trvl_date">
                        <span>Return Date (If Return Date Confirmed)</span>
                        <asp:TextBox AutoComplete="off" ID="txttodate" runat="server" MaxLength="100" AutoPostBack="true" OnTextChanged="txttodate_TextChanged"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd-MM-yyyy" TargetControlID="txttodate"
                            runat="server">
                        </ajaxToolkit:CalendarExtender>
                    </li>

                    <li class="trvl_date">
                        <span>Travel Project</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                        <asp:DropDownList ID="lstProjectlist" runat="server" CssClass="DropdownListSearch" Style="width: 251px !important" OnSelectedIndexChanged="txttravelproject_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        <asp:TextBox AutoComplete="off" ID="TextBox1" runat="server" MaxLength="45" Visible="false"></asp:TextBox>
                    </li>

                    <li class="trvl_date">
                        <span id="spn_prm" runat="server">Program Manger</span>
                        <asp:TextBox AutoComplete="off" ID="txtprogrammanger_Name" runat="server" Enabled="false"></asp:TextBox>
                        <asp:TextBox AutoComplete="off" ID="txtprogrammanger_Email" runat="server" Enabled="false" Visible="false"></asp:TextBox>
                    </li>

                    <li class="trvl_date">
                        <span id="spn_pm" runat="server">Project Manger</span>
                        <asp:TextBox AutoComplete="off" ID="txtprojectmanger_Name" runat="server" Enabled="false"></asp:TextBox>
                        <asp:TextBox AutoComplete="off" ID="txtprojectmanger_Email" runat="server" Enabled="false" Visible="false"></asp:TextBox>
                    </li>

                    <li class="trvl_date">
                        <span>From Location</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                        <asp:DropDownList ID="lstFromLocation" runat="server" CssClass="DropdownListSearch" Style="width: 251px !important"></asp:DropDownList>
                        <asp:TextBox AutoComplete="off" ID="txtFromLocation" runat="server" MaxLength="45" Visible="false"></asp:TextBox>
                    </li>


                    <li class="trvl_date">
                        <span>To Location</span> &nbsp;&nbsp;<span style="color: red">*</span><br />
                        <asp:DropDownList ID="lstToLocation" runat="server" CssClass="DropdownListSearch" Style="width: 251px !important"></asp:DropDownList>
                        <asp:TextBox AutoComplete="off" ID="txtEmailAddress" runat="server" Visible="false" MaxLength="100"></asp:TextBox>

                    </li>
                    <li class="trvl_date">
                        <span>Boarding Point (Station/Airport)</span> &nbsp;&nbsp;<span style="color: red">*</span><br />
                        <asp:TextBox AutoComplete="off" ID="txtbaselocation" runat="server" MaxLength="100"></asp:TextBox>
                    </li>
                    <li class="trvl_date">
                        <span>Prefer Time (Booking)</span> &nbsp;&nbsp;<span style="color: red">*</span>
                        <asp:DropDownList ID="lstPreferTime" runat="server" CssClass="DropdownListSearch" Style="width: 251px !important">
                        </asp:DropDownList>

                    </li>

                    <li class="trvl_date">
                        <span>Travel Mode</span> &nbsp;&nbsp;<span style="color: red">*</span><br />
                        <asp:DropDownList ID="lstTravelMode" runat="server" CssClass="DropdownListSearch" Style="width: 251px !important"></asp:DropDownList>
                    </li>
                    <li class="trvl_date">
                        <span>Accomodation </span>&nbsp;&nbsp;<span style="color: red">*</span><br />

                        <div class="form-check-inline">
                            <asp:RadioButton runat="server" class="form-check-input" ID="rdoYes" Text="Yes" AutoPostBack="true" OnCheckedChanged="rdoYes_CheckedChanged" GroupName="optradio" />
                            &nbsp;&nbsp; &nbsp;&nbsp;
                              <asp:RadioButton runat="server" class="form-check-input" ID="rdoNo" Checked="true" Text="No" AutoPostBack="true" OnCheckedChanged="rdoYes_CheckedChanged" GroupName="optradio" />
                        </div>
                    </li>
                    <li class="trvl_date">
                        <div id="divAcc" runat="server" visible="false">
                            <span>Accomodation Location</span> &nbsp;&nbsp;<span style="color: red">*</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtaccomodationl" runat="server" MaxLength="100"></asp:TextBox>
                        </div>
                    </li>

                    <li class="trvl_date">
                        <div id="divdate" runat="server" visible="false">
                            <span>Accomodation From Date</span> &nbsp;&nbsp;<span style="color: red">*</span>
                            <asp:TextBox AutoComplete="off" ID="txtaccdate" runat="server" AutoPostBack="true" OnTextChanged="txtAccomodationFromDate_TextChanged" MaxLength="15" AutoCompleteType="Disabled"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" Format="dd-MM-yyyy" TargetControlID="txtaccdate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </div>
                    </li>


                    <li class="trvl_date">
                        <div id="divtodate" runat="server" visible="false">
                            <span>Accomodation To Date</span><span style="color: red">*</span>
                            <asp:TextBox AutoComplete="off" ID="txtacctodate" runat="server" MaxLength="100" AutoPostBack="true" OnTextChanged="txtAccomodationtodate_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" Format="dd-MM-yyyy" TargetControlID="txtacctodate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </div>
                    </li>


                    <li class="trvl_date">
                        <span>Remarks</span>&nbsp;&nbsp;
                        <span style="color: red; font-size: 10px; font-weight: normal; font-style: italic;">Maximum 500 Characters</span>
                        <br />
                        <asp:TextBox AutoComplete="off" ID="txtremarks" runat="server" MaxLength="400" TextMode="MultiLine" Height="80px" OnTextChanged="TxtVendorAddress_TextChanged"></asp:TextBox>

                    </li>
                    <li class="trvl_date"></li>
                    <li class="trvl_date"></li>

                    <li class="trvl_date">
                        <div id="divUploadAadhar" runat="server" visible="false">
                            <span>Upload Aadhar Card</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <asp:FileUpload ID="uploadAaadharCard" runat="server" accept="pdf/*" AllowMultiple="false" Visible="true"></asp:FileUpload>
                            <br />
                        </div>
                        <asp:LinkButton ID="lnkfile_Vendor" runat="server" OnClientClick="DownloadFile_S()" Visible="false" CssClass="BtnShow"></asp:LinkButton>

                    </li>
                    <li class="trvl_date"></li>
                    <li class="trvl_date"></li>


                    <asp:CheckBox ID="chkISActive" runat="server" Text="Vendor Is Active" Checked="true" Visible="false" />

                </ul>

            </div>

        </div>
    </div>
    <div class="trvl_Savebtndiv">
        <asp:LinkButton ID="trvl_btnSave" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();" OnClick="trvl_btnSave_Click">Submit</asp:LinkButton>

        <asp:LinkButton ID="btnCorrection" Visible="false" runat="server" Text="Cancel Travel Request" ToolTip="Cancel Travel Request" OnClientClick="return CancelMultiClick();" CssClass="Savebtnsve" OnClick="btnCorrection_Click">Cancel Travel Request</asp:LinkButton>

        <asp:LinkButton ID="btnback_mng" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="MyTravelRequisitions.aspx">Back</asp:LinkButton>

        <%--
         <asp:LinkButton ID="btnApprove"  runat="server" Text="View Draft Copy" ToolTip="View Draft Copy" Visible="false" CssClass="Savebtnsve" OnClick="btnApprove_Click" >View Draft Copy</asp:LinkButton>
        <asp:LinkButton ID="btnReject"  runat="server" Visible="false" Text="View Draft Copy" ToolTip="View Draft Copy"  CssClass="Savebtnsve"  OnClick="btnReject_Click" >View Draft Copy</asp:LinkButton>
        MyTravelRequisitions--%>
    </div>


    <asp:HiddenField ID="hdnCondintionId" runat="server" />
    <asp:HiddenField ID="hdnTravel_id" runat="server" />
    <asp:HiddenField ID="HDVendorCode" runat="server" />
    <asp:HiddenField ID="hdnShortClose_Cancelled" runat="server" />
    <asp:HiddenField ID="hdnIsShMilestoneClick" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="FilePath" runat="server" />
    <asp:HiddenField ID="hdnAadharCardPath" runat="server" />
    <asp:HiddenField ID="hdnAadharCardFileName" runat="server" />
    <asp:HiddenField ID="hdnNameAsPerAadhar" runat="server" />
    <asp:HiddenField ID="hdnAdmin_EmailsId" runat="server" />
    <asp:HiddenField ID="hdnAdmin_HODEmailId" runat="server" />
    <asp:HiddenField ID="hdnAdmin_PrmEmailId" runat="server" />

    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <script type="text/javascript">

        $(document).ready(function () {
            $(".DropdownListSearch").select2();
            $("#MainContent_txtPOWO_Content").htmlarea();
            $("#MainContent_txt_POWOContent_description").htmlarea();
        });


        function SaveMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=trvl_btnSave.ClientID%>');

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

        function CancelMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=btnCorrection.ClientID%>');

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


        function checkEmail() {

            var email = document.getElementById('<%=txtEmailAddress.ClientID%>');
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (email.value != '') {
                if (!filter.test(email.value)) {
                    alert('Please provide a valid email address');
                    email.focus;
                    return false;
                }
            }

        }

        function Confirm() {

            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Submit ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            //alert(confirm_value.value);
            //document.forms[0].appendChild(confirm_value);
            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return;

        }


        function DownloadFile_S() {

            var localFilePath = document.getElementById("<%=hdnAadharCardPath.ClientID%>").value;
            var localFileName = document.getElementById("<%=hdnAadharCardFileName.ClientID%>").value;

            //alert(localFilePath);
            //alert(localFileName);

            window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + localFileName);
          //  window.open("https://192.168.21.193/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
            // window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + localFileName);
        }
          
        function onCharOnlyNumber(e) {
            var keynum;
            var keychar;
            var numcheck = /[0123456789]/;

            if (window.event) {
                keynum = e.keyCode;
            }
            else if (e.which) {
                keynum = e.which;
            }
            keychar = String.fromCharCode(keynum);
            return numcheck.test(keychar);
        }


    </script>
</asp:Content>





<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="ResignationForm.aspx.cs" Inherits="ResignationForm" %>

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

        .aspNetDisabled {
            /*background: #dae1ed;*/
            background: #ebebe4;
        }

        #MainContent_lstApprover {
            overflow: hidden !important;
        }

        .noresize {
            resize: none;
        }
        .hiddencol {
           display: none;
       }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
       
     <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Resignation Form (To be filled by employee)"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="Label1" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>
                <div>
                    <span>
                        <a href="ExitProcess_Index.aspx" class="aaaa">Exit Process Home</a>
                    </span>
                </div>


                <div class="edit-contact">

                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server"></asp:LinkButton>
                        </div>
                    </div>

               <ul id="editform" runat="server" visible="true">
				   <asp:Label runat="server" ID="lblRetention" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
            
            <li>
                <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
            </li>
           <li></li>
                   <li class="claimmob_fromdate">
                            <br />
                            <span>Resignation Date </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtFromdate_N" Enabled="false" runat="server" AutoPostBack="True" OnTextChanged="txtFromdate_N_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2"  TargetControlID="txtFromdate_N"
                             Format="dd/MM/yyyy" runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>
            <li></li>
            <li class="mobile_inboxEmpCode">
                <span>Notice Period</span>
                 <asp:TextBox ID="txtNoticePeriod" runat="server" Text="90(day)s"  Enabled="false"></asp:TextBox>
            </li>
             <li></li>
            <%-- <li class="mobile_inboxEmpCode">
                <span>Last Working Day</span>
                 <asp:TextBox ID="txtLWDay" runat="server" Enabled="false"></asp:TextBox>
            </li>--%>
                    <li class="claimmob_fromdate">
                            <span>Last Working Day </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtTodate_N" runat="server" Enabled="false" AutoPostBack="True"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3"  TargetControlID="txtTodate_N"
                             Format="dd/MM/yyyy" runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>
            <li></li>
            <li class="mobile_inboxEmpCode"  >
                <span>Reason Of Resignation</span>&nbsp;&nbsp;<span style="color:red">*</span>
                      <asp:DropDownList ID="ddlResignationReason" runat="server"></asp:DropDownList><br />
            </li>
            <li></li>
                 
            <li class="mobile_inboxEmpCode">
               <br /> <span>Employee Comment</span>&nbsp;&nbsp;<span style="color:red">*</span>
                 <asp:TextBox ID="txtEmpComment" runat="server" TextMode="MultiLine" MaxLength="200" Width="100%" Height="90px"></asp:TextBox><br />
            </li>
             <li></li>
            <%--<li>
                Attachment
                <asp:FileUpload ID="uploadfile" runat="server"  AllowMultiple="true" />
                <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Please Select Attachment File"
                    ControlToValidate="uploadfile" Display="Dynamic" ForeColor="Red" ValidationGroup="EditNews"></asp:RequiredFieldValidator>
            </li>  --%>
                   <li class="claimmob_upload">
                            <span>Attachment</span>&nbsp;&nbsp;<br />
                            <asp:FileUpload ID="uploadfile" runat="server" />
                            <asp:LinkButton ID="lnkuplodedfile" visible="true" OnClick="lnkuplodedfile_Click" OnClientClick="DownloadFile()" runat="server"></asp:LinkButton>
                            
                        </li>
            <li><br /></li>
             </ul>
                </div>
               </div>
            </div>
        </div>
     <div class="mobile_Savebtndiv">
        <asp:LinkButton ID="mobile_btnSave" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click" OnClientClick="return SaveMultiClick();">Submit</asp:LinkButton>
<%--        <asp:LinkButton ID="mobile_btnSave" runat="server" Text="Save or Later" ToolTip="Save or Later" CssClass="Savebtnsve">Save or Later</asp:LinkButton>--%>
        <asp:LinkButton ID="claimmob_btnSubmit" runat="server" Text="Retain" ToolTip="Retain" CssClass="Savebtnsve" OnClick="claimmob_btnSubmit_Click" OnClientClick="return RetainMultiClick();">Retain</asp:LinkButton>
        <asp:LinkButton ID="mobile_cancel" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="Savebtnsve" OnClick="mobile_cancel_Click">Back</asp:LinkButton>
       </div>
    <br />

    <asp:HiddenField ID="hdnRemid" runat="server" />
    <asp:HiddenField ID="hdnEmpCode" runat="server" />                
    <asp:HiddenField ID="hdnId" runat="server" />
    <asp:HiddenField ID="FilePath" runat="server" />
    <asp:HiddenField ID="hdn_Attchment" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="hflapprcode" runat="server" />                
    <asp:HiddenField ID="hdnApprEmailaddress" runat="server" />
    <asp:HiddenField ID="hdnIsResigSubmitted" runat="server" />
    <asp:HiddenField ID="ResigStatus" runat="server" />
    <asp:HiddenField ID="hdnResigId" runat="server" />
    <asp:HiddenField ID="hdnEmpType" runat="server" />


    <script src="../js/dist/jquery-3.2.1.min.js"></script>       
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" /> 

    <script type="text/javascript">      
        $(document).ready(function () {
            $("#MainContent_ddlResignationReason").select2();
        });
    </script>
    <script type="text/javascript">

        function DownloadFile() {
            //alert(file);
            var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;
            var file = document.getElementById("<%=hdn_Attchment.ClientID%>").value;
            //alert(localFilePath);
           window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + file);
			//window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
	
		}
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
        //below funcations for calendar
        function onCalendarShown() {

            var cal = $find("calendar1");
            //Setting the default mode to month
            cal._switchMode("months", true);

            //Iterate every month Item and attach click event to it
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }

        function onCalendarHidden() {
            var cal = $find("calendar1");
            //Iterate every month Item and remove click event from it
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }

        }
        function call(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("calendar1");
                    cal._visibleDate = target.date;
                    cal.set_selectedDate(target.date);
                    cal._switchMonth(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged();
                    break;
            }
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


        function CancelMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=mobile_cancel.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        var retunboolean = ConfirmCancel();

                    if (retunboolean == false)
                        ele.disabled = false;
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        function RetainMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=claimmob_btnSubmit.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        var retunboolean = ConfirmRetain();

                    if (retunboolean == false)
                        ele.disabled = false;
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
        function ConfirmCancel() {
            //Testing();
            var confirmval = false;
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Are you sure you want to cancel?")) {
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
        function ConfirmRetain() {
            //Testing();
            var confirmval = false;
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Are you sure you want to retain?")) {
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
</asp:Content>


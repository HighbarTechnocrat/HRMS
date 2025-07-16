<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="ResignationForm_App.aspx.cs" Inherits="ResignationForm_App" %>

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
                        <asp:Label ID="lblheading" runat="server" Text="Resignation Form"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="Label1" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>
                <div id="dvhome" runat="server">
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
                        <li>
                            <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                        </li>
                        <li></li>
                        <li class="mobile_inboxEmpCode">
                            <span>Employee Code</span>
                            <asp:TextBox ID="txtEmpCode" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Employee Name</span>
                            <asp:TextBox ID="txtEmpName" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Project/Location</span>
                            <asp:TextBox ID="txtEmpLocation" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li><span>Department</span>
                            <asp:TextBox ID="txtEmpDept" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Band</span>
                            <asp:TextBox ID="txtBand" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Designation</span>
                            <asp:TextBox ID="txtEmpDesig" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Date Of Joining </span>
                            <asp:TextBox ID="txtDoj" runat="server" Enabled="false"></asp:TextBox>
                        </li>

                        <li class="claimmob_fromdate">
                            <span>Resignation Date </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtFromdate_N" runat="server" Enabled="false" AutoPostBack="True"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtFromdate_N"
                                Format="dd/MM/yyyy" runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>
                        <li class="claimmob_fromdate">
                            <span>Last Working Day </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtTodate_N" runat="server" Enabled="false" AutoPostBack="True"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" TargetControlID="txtTodate_N"
                                Format="dd/MM/yyyy" runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Notice Period</span>
                            <asp:TextBox ID="txtNoticePeriod" runat="server" Text="90(day)s" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Reason Of Resignation</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <asp:DropDownList ID="ddlResignationReason" runat="server" Enabled="false"></asp:DropDownList><br />
                        </li>
                        <li></li>

                        <li class="mobile_inboxEmpCode">
                            <br />
                            <span>Employee Comment</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <asp:TextBox ID="txtEmpComment" runat="server" Enabled="false" TextMode="MultiLine" MaxLength="200" Width="100%" Height="90px"></asp:TextBox><br />
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <br />
                            <span>Approver Remark</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <asp:TextBox ID="txtAppComment" runat="server" TextMode="MultiLine" MaxLength="200" Width="100%" Height="90px"></asp:TextBox><br />
                        </li>
                        <%--<li>
                Attachment
                <asp:FileUpload ID="uploadfile" runat="server"  AllowMultiple="true" />
                <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Please Select Attachment File"
                    ControlToValidate="uploadfile" Display="Dynamic" ForeColor="Red" ValidationGroup="EditNews"></asp:RequiredFieldValidator>
            </li>  --%>
                        <li class="claimmob_upload">
                            <span>Attachment</span>&nbsp;&nbsp;<br />
                            <%-- <asp:FileUpload ID="uploadfile" runat="server" />--%>
                            <asp:LinkButton ID="lnkuplodedfile" Visible="true" OnClientClick="DownloadFile()" runat="server"></asp:LinkButton>
                            <br />
                        </li>
                        <li>
                            <br />
                        </li>
                        <hr />
                        <li class="Approver">
                            <span>Approver </span>
                            <br />
                            <br />
                            <asp:ListBox Visible="false" ID="lstApprover" runat="server"></asp:ListBox>
                            <asp:GridView ID="DgvApprover" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%">
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
                                        DataField="tName"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="left"
                                        ItemStyle-Width="25%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Status"
                                        DataField="Status"
                                        ItemStyle-HorizontalAlign="left"
                                        ItemStyle-Width="12%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Approved on"
                                        DataField="tdate"
                                        ItemStyle-HorizontalAlign="left"
                                        ItemStyle-Width="12%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Approver Remarks"
                                        DataField="Comment"
                                        ItemStyle-HorizontalAlign="left"
                                        ItemStyle-Width="46%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="APPR_ID"
                                        DataField="APPR_ID"
                                        ItemStyle-HorizontalAlign="center"
                                        ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy"
                                        Visible="false" />

                                    <asp:BoundField HeaderText="Emp_Name"
                                        DataField="Emp_Name"
                                        ItemStyle-HorizontalAlign="center"
                                        ItemStyle-Width="1%"
                                        ItemStyle-BorderColor="Navy"
                                        Visible="false" />

                                    <asp:BoundField HeaderText="Emp_Emailaddress"
                                        DataField="Emp_Emailaddress"
                                        ItemStyle-HorizontalAlign="center"
                                        ItemStyle-Width="1%"
                                        ItemStyle-BorderColor="Navy"
                                        Visible="false" />

                                    <asp:BoundField HeaderText="A_EMP_CODE"
                                        DataField="A_EMP_CODE"
                                        ItemStyle-HorizontalAlign="center"
                                        ItemStyle-Width="1%"
                                        ItemStyle-BorderColor="Navy"
                                        Visible="false" />
                                </Columns>
                            </asp:GridView>
                        </li>

                    </ul>

                </div>
            </div>
        </div>
    </div>
    <div class="mobile_Savebtndiv">
        <asp:LinkButton ID="mobile_btnSave" runat="server" Text="Accept" ToolTip="Accept" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click" OnClientClick="return SaveMultiClick();">Accept</asp:LinkButton>
        <asp:LinkButton ID="mobile_btnBack" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="mobile_btnBack_Click">Back</asp:LinkButton>
        <asp:LinkButton ID="claimmob_btnSubmit" runat="server" Text="Reject" ToolTip="Reject" CssClass="Savebtnsve" OnClick="claimmob_btnSubmit_Click" OnClientClick="return RejectMultiClick();">Reject</asp:LinkButton>
        <asp:LinkButton ID="mobile_cancel" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="Savebtnsve" OnClick="mobile_cancel_Click">Cancel</asp:LinkButton>
    </div>
    <br />
    <br />
    <div>

        <hr />
        <span>Resignation History</span>
        <br />
        <br />
        <asp:ListBox Visible="false" ID="ListBox1" runat="server"></asp:ListBox>
        <asp:GridView ID="gvHistory" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%">
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
                <asp:BoundField HeaderText="Sr No"
                    DataField="SrNo"
                    ItemStyle-HorizontalAlign="left"
                    HeaderStyle-HorizontalAlign="left"
                    ItemStyle-Width="8%"
                    ItemStyle-BorderColor="Navy" />

                <asp:BoundField HeaderText="Resignation Date"
                    DataField="ResignationDate"
                    ItemStyle-HorizontalAlign="left"
                    HeaderStyle-HorizontalAlign="left"
                    ItemStyle-Width="15%"
                    ItemStyle-BorderColor="Navy" />

                <asp:BoundField HeaderText="Request status"
                    DataField="Request_status"
                    ItemStyle-HorizontalAlign="left"
                    HeaderStyle-HorizontalAlign="left"
                    ItemStyle-Width="25%"
                    ItemStyle-BorderColor="Navy" />
                <asp:BoundField HeaderText="Status Date"
                    DataField="StatusDate"
                    ItemStyle-HorizontalAlign="left"
                    HeaderStyle-HorizontalAlign="left"
                    ItemStyle-Width="15%"
                    ItemStyle-BorderColor="Navy" DataFormatString="{0:MM/dd/yyyy}" />
            </Columns>
        </asp:GridView>


    </div>
    <asp:HiddenField ID="hdnRemid" runat="server" />
    <asp:HiddenField ID="hdnEmpCode" runat="server" />
    <asp:HiddenField ID="hdnId" runat="server" />
    <asp:HiddenField ID="FilePath" runat="server" />
    <asp:HiddenField ID="hdn_Attchment" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="hflapprcode" runat="server" />
    <asp:HiddenField ID="hdnCurrentID" runat="server" />
    <asp:HiddenField ID="hdnnextappcode" runat="server" />
    <asp:HiddenField ID="hdnapprid" runat="server" />
    <asp:HiddenField ID="hflApproverEmail" runat="server" />
    <asp:HiddenField ID="hdnIntermediateEmail" runat="server" />
    <asp:HiddenField ID="hdnstaus" runat="server" />
    <asp:HiddenField ID="hdnResignationID" runat="server" />
    <asp:HiddenField ID="hdnApprEmailaddress" runat="server" />

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

          function RejectMultiClick() {
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
                        ConfirmReject();
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
                        var retunboolean = Confirm();

                    if (retunboolean == false)
                        ele.disabled = false;
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }


         function ConfirmReject() {
            //Testing();
            var confirmval = false;
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Are you sure you want to Reject?")) {
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


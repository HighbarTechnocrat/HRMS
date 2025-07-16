<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="TaskOwnerAction.aspx.cs" Inherits="TaskOwnerAction" %>


<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/manageleave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/ClaimMobile_css.css" type="text/css" media="all" />
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
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
   <%-- <div class="userposts">
        <span>
            <asp:Label ID="lblheading" runat="server" Text="Task Owner Action"></asp:Label>
        </span>
    </div>--%>
       
    <div class="edit-contact">
        <%-- <div class="editprofile" style="text-align: center; border: none;" id="divmsg" runat="server" visible="false">
            <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: green; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
        </div>--%>
<%--        <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
            <div class="cancelbtndiv">
                <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
            </div>
            <div class="cancelbtndiv">
                <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" OnClick="lnkcont_Click"></asp:LinkButton>
            </div>
        </div>--%>


        <ul id="editform" runat="server" visible="false">
            <li>
                <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
            </li>
        </ul>
        <ul>
            <li><b>Task Details</b></li>
           <li></li>
            <li></li>
            <li>
                Task Description
                <asp:TextBox ID="txtDescription" runat="server" CssClass ="txtcls"  Enabled="false" ></asp:TextBox>
            </li>   
           
            <li>
                Due Date<br />
                 <asp:TextBox ID="txtDueDate"  AutoComplete="off" runat="server" AutoPostBack="true" MaxLength="10" AutoCompleteType="Disabled"  Enabled="false"></asp:TextBox>
            </li>
            
            <li>
                 Task Owner
                 <asp:TextBox ID="txtTaskOwner" runat="server"  Enabled="false"></asp:TextBox>
            </li>
             <li>
                 Task Supervisor1<br />
                 <asp:TextBox ID="txtSupervisor1" runat="server"  Enabled="false"></asp:TextBox>
            </li>
            
            <li>
                 Task Supervisor2<br />
                <asp:TextBox ID="txtSupervisor2" runat="server"  Enabled="false"></asp:TextBox>
            </li>
           <li></li>
            <li>
                Remarks<br />
                <asp:TextBox ID="txtRemarks" runat="server"  Height="50px" MaxLength ="300" TextMode="MultiLine" Enabled="false"></asp:TextBox>
            </li>  
           
            <li>
                <asp:LinkButton ID="lnkviewfile" Font-Bold="true" Visible="true" runat="server" CssClass="Savebtnsve" OnClientClick="DownloadFile();">View File</asp:LinkButton>
            </li>  
            <li></li>
             
            <li><b>Close Task Notification</b> </li>
            <li></li>
            <li></li>
             <li>
                <span style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">Action Date</span><br />
                 <asp:TextBox ID="txtActionDate"  AutoComplete="off" runat="server" AutoPostBack="true" MaxLength="10" AutoCompleteType="Disabled"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" Format="dd-MM-yyyy" TargetControlID="txtActionDate"
                                      runat="server">
                                </ajaxToolkit:CalendarExtender>                
             </li>
             <li></li>
            <li></li>
            <li>
                Owner Remarks<br />
                <asp:TextBox ID="txtOwnerRemark" runat="server" MaxLength="100" Height="100px" TextMode="MultiLine"></asp:TextBox>
            </li>  
             <li></li>
            <li></li>
      
            <li>
                Upload Files<br />
                <%--<asp:button ID="btnUploadFiles" runat="server" CssClass="Savebtnsve"></asp:button>--%>
                <asp:FileUpload ID="uploadfile" runat="server"  AllowMultiple="false" />
                <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Please Select Attachment File"
                    ControlToValidate="uploadfile" Display="Dynamic" ForeColor="Red" ValidationGroup="EditNews"></asp:RequiredFieldValidator>
            </li>  
            
        </ul>
        <ul>
            <%--<li></li>--%>
            <li>
                <br />
                <asp:LinkButton ID="claimmob_btnSubmit" runat="server" Text="Close Task Notification" ToolTip="Search" CssClass="Savebtnsve" OnClick="claimmob_btnSubmit_Click">Close Task Notification</asp:LinkButton>
            </li>
        </ul>
    </div>


    <asp:HiddenField ID="hdnRemid" runat="server" />
    <asp:HiddenField ID="hdnleaveTypeid" runat="server" />
    <asp:HiddenField ID="hdnleaveType" runat="server" />
    <asp:HiddenField ID="hdnEmpCode" runat="server" />                
    <asp:HiddenField ID="hdnApproverType" runat="server" />
    <asp:HiddenField ID="hdnSubTaskId" runat="server" />
    <asp:HiddenField ID="hdntaskId" runat="server" />
    <asp:HiddenField ID="FilePath" runat="server" />
    <asp:HiddenField ID="hdn_Attchment" runat="server" />
    <asp:HiddenField ID="hdnTaskOwnerId" runat="server" />
    <asp:HiddenField ID="hdnTaskSupervisor1Id" runat="server" />
    <asp:HiddenField ID="hdnTaskSupervisor2Id" runat="server" />
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
    </script>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="ITAssetTermsAndConditions.aspx.cs" Inherits="ITAssetTermsAndConditions" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
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
        .bTop {
           border-top: 2px solid blue;
           /*background-color:lightblue;*/
        }
        .bBottom {
           border-top: 2px solid blue;
        }
         .bBkgColour {
           background-color:lightblue;
        }
         .bRight {
           border-right: 2px solid blue;
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
                        <asp:Label ID="lblheading" runat="server" Text="New Joinee IT Asset Declaration Form"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="Label1" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>
                <div>
                    <span id="hmlink" runat="server" visible="false">
                        <a href="ITAssetService.aspx" class="aaaa">IT Inventory Home</a>
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
          
            <li class="mobile_inboxEmpCode"><span><b>Employee Details</b></span><br /><br /></li>
            <li></li>
            <li class="mobile_inboxEmpCode">
                <span>Employee Code</span>
                 <asp:TextBox ID="txtEmpCode" runat="server" Enabled="false"></asp:TextBox>
            </li>
            <li><span>Department</span>
                 <asp:TextBox ID="txtEmpDept" runat="server" Enabled="false"></asp:TextBox>
            </li>
             <li class="mobile_inboxEmpCode">
                <span>Employee Name</span>
                 <asp:TextBox ID="txtEmpName" runat="server" Enabled="false"></asp:TextBox>
            </li>
            <li class="mobile_inboxEmpCode">           
                <span>Designation</span>
                 <asp:TextBox ID="txtEmpDesig" runat="server" Enabled="false"></asp:TextBox>
            </li>
            <hr runat="server" id="Hr1" />
            <li class="mobile_inboxEmpCode"><span><b>Asset Details</b></span><br /><br /></li>
            <li></li>
            <li class="mobile_inboxEmpCode">
                <span>Asset Number</span>
                <asp:TextBox ID="txtAssetNo" runat="server" CssClass ="txtcls"  Enabled="false" ></asp:TextBox>
            </li>
            <li class="mobile_inboxEmpCode">
               <span>Sr. Number</span> 
                 <asp:TextBox ID="txtSrNo" AutoComplete="off" runat="server" AutoPostBack="true" MaxLength="10" AutoCompleteType="Disabled"  Enabled="false"></asp:TextBox>
            </li>
            <li class="mobile_inboxEmpCode">
                <span>Asset Type</span>
                <asp:TextBox ID="txtAssetType" runat="server" CssClass ="txtcls"  Enabled="false" ></asp:TextBox>
            </li>
            <li class="mobile_inboxEmpCode">
               <span>Own/Rental</span> 
                 <asp:TextBox ID="txtAssetProperty" AutoComplete="off" runat="server" AutoPostBack="true" MaxLength="10" AutoCompleteType="Disabled"  Enabled="false"></asp:TextBox>
            </li>
            <li class="mobile_inboxEmpCode">
                <span>Brand</span>
                <asp:TextBox ID="txtBrand" runat="server" CssClass ="txtcls"  Enabled="false" ></asp:TextBox>
            </li>
            <li class="mobile_inboxEmpCode">
               <span>Model</span> 
                 <asp:TextBox ID="txtModel" AutoComplete="off" runat="server" AutoPostBack="true" MaxLength="10" AutoCompleteType="Disabled"  Enabled="false"></asp:TextBox>
            </li>
            <li class="mobile_inboxEmpCode">
               <span>Allocation Date</span> 
                 <asp:TextBox ID="txtAssignedDate" AutoComplete="off" runat="server" AutoPostBack="true" MaxLength="10" AutoCompleteType="Disabled"  Enabled="false"></asp:TextBox>
            </li>
           <hr runat="server" id="Hr2" />
           <li style="width:100%"></li>
            <li><b>Terms & Conditions Of Usage</b><br /><br /></li>
            <li class="bTop" style="width:100%"><br /></li>
            <li style="width:100%">• The asset has been issued to you with the below mentioned understanding</li>
            <li ></li>
            <li style="width: 100%">• The laptop / desktop / any other asset issued is for solely official purpose</li>
            <li ></li>
            <li style="width: 100%">• The employee shall be fully accountable for theft, loss or damage of the property</li>
            <li></li>
            <li style="width:100%">• If the asset is lost, stolen or damaged, the incident employee shall report to local police as well to IT department within 24 hours</li>
            <li></li>
            <li style="width:100%">• If the lost, stolen or damaged asset is determined to be caused by negligence or intentional misuse, employee shall be responsible for repair costs or fair market value of the asset</li>
            <li></li>
            <li style="width: 100%">• Employee can mention necessary specifications needed for their job function before taking handover from the IT Department</li>
            <li></li>
            <li style="width:100%">• Any additional software / hardware required by employee (before or after taking handover) should be clearly communicated through mail to the IT Department with approval from respective HOD’s</li>
            <li></li>
            <li style="width:100%">• Management is at the sole discretion on approving such requests</li>
            <li></li>
            <li style="width:100%">• In case of any malfunction, employees are required to report the same to the IT department</li>
            <li></li>
            <li style="width:100%">• Employees may not take the asset for repair to any external agency or vendor at any point of time without prior approval from issuing authority</li>
            <li></li>
            <li style="width:100%">• The laptop / desktop / any other asset issued is for solely official purpose</li>
            <li></li>
            <li style="width:100%">• The asset should be returned to the IT department in case of leaving the organization or if they do not intend to use it for any reason</li>
            <li></li>
            <li style="width:100%">• The employee shall be liable to replace or pay an equivalent amount to the organization in case of theft, loss or damage to the asset. The organization retains the right to deduct the same from the salary in case of such an event</li>
            <li></li>
            <li style="width:100%">• Employee shall not install and / or download any unauthorized software and / or applications</li>
            <li></li>
            <li style="width:100%">• Employee shall not allow the asset to be used oy an unknown or unauthorized person</li>
            <li></li>
            <li style="width:100%">• Employee shall not delete official documents saved on the asset prior to return</li>
            <li class="bBottom" style="width:100%"></li>
            </ul>
                    </div>
                 </div>
            </div>
        </div>
    <div class="mobile_Savebtndiv">
        <asp:CheckBox ID="chkSelect" runat="server" Text="I have read and I agree with the terms and conditions."/>
        <%--<asp:LinkButton ID="mobile_cancel" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="Savebtnsve">Cancel</asp:LinkButton>--%>
        </div>
    <div class="mobile_Savebtndiv">
        <asp:LinkButton ID="mobile_btnSave" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click" OnClientClick="return SaveMultiClick();">Submit</asp:LinkButton>
        <%--<asp:LinkButton ID="mobile_cancel" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="Savebtnsve" OnClick="mobile_cancel_Click" OnClientClick="return CancelMultiClick();">Cancel</asp:LinkButton>--%>
        <%--<asp:LinkButton ID="mobile_btnBack" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/Service.aspx">Back</asp:LinkButton>--%>
        </div>
    <br />

    <asp:HiddenField ID="hdnRemid" runat="server" />
    <asp:HiddenField ID="hdnleaveTypeid" runat="server" />
    <asp:HiddenField ID="hdnleaveType" runat="server" />
    <asp:HiddenField ID="hdnEmpCode" runat="server" />                
    <asp:HiddenField ID="hdnApproverType" runat="server" />
    <asp:HiddenField ID="hdnSubTaskId" runat="server" />
    <asp:HiddenField ID="hdnId" runat="server" />
    <asp:HiddenField ID="FilePath" runat="server" />
    <asp:HiddenField ID="hdn_Attchment" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="hdnOfficeLocation" runat="server" />
    <asp:HiddenField ID="hdnDoj" runat="server" />
    <asp:HiddenField ID="hdnRMgr" runat="server" />
    <asp:HiddenField ID="hdnHod" runat="server" />
    <asp:HiddenField ID="hdnEmpEmail" runat="server" />

    

    <script src="../js/dist/jquery-3.2.1.min.js"></script>       
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" /> 

    <script type="text/javascript">      
        $(document).ready(function () {
            $("#MainContent_ddl_AssetNumber").select2();
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
            //alert();
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
                        retunboolean = Confirm();

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
    </script>
</asp:Content>


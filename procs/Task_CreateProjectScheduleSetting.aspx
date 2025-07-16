<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" 
    MaintainScrollPositionOnPostback="true"
    CodeFile="Task_CreateProjectScheduleSetting.aspx.cs" Inherits="procs_Task_CreateProjectScheduleSetting" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <%--   <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <%-- <link href="<%=ReturnUrl("css") %>calendar/calendar.css" rel="stylesheet" type="text/css" />
    <link href="sampleform_css.css" rel="stylesheet" type="text/css" />--%>
    <%--<ajaxToolkit:ToolkitScriptManager ID="srcp" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
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

        #MainContent_btn_Ref_Add,
        #MainContent_btn_FD_Cancel,
        #MainContent_btn_ATT_Save,
        #MainContent_btn_ATT_Update,
        #MainContent_btn_Org_Save,   
		#MainContent_btn_Org_Update,
        #MainContent_lnk_Task_Create,
        #MainContent_lnk_Task_Update,
        #MainContent_lnk_Task_Cancel,
        #MainContent_lnk_CD_Update,
        #MainContent_lnk_CD_Cancel,
        #MainContent_lnk_PD_Save,
        #MainContent_lnk_PD_Update,
        #MainContent_lnk_PD_Cancel,
        #MainContent_lnk_DD_Save,
        #MainContent_lnk_DD_Update,
        #MainContent_lnk_DD_TaskIntimationUpdate,
        #MainContent_lnk_DD_TaskIntimationSave,
        #MainContent_lnk_DD_Cancel,
        #MainContent_lnk_DE_Save,
        #MainContent_lnk_DE_Update,
        #MainContent_lnk_FinalSubmit,
        #MainContent_lnk_FileSave,
        #MainContent_lnk_FileUpdate,
        #MainContent_lnk_FileCancel,
        #MainContent_lnk_Final_Submit {
            background: #3D1956;
            color: #febf39 !important;
            padding: 0.5% 1.4%;
            margin: 0% 0% 0 0;
        }

        .noresize {
            resize: none;
        }
        input.select2-search__field {
            height: 0px !important;
            padding-left: 0px !important;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <%--   <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>--%>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
    <script src="../js/HtmlControl/jHtmlArea-0.8.min.js"></script>
    <link href="../js/HtmlControl/jHtmlArea.css" rel="stylesheet" />

    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Create Project Schedule"></asp:Label>
                    </span>
                </div>
              
                <span runat="server" id="backToSPOC" visible="false">
                    <a href="InboxServiceRequest.aspx" class="aaaa">Back</a>
                </span>
                <span runat="server" id="backToEmployee" visible="false">
                    <a href="MyService_Req.aspx" class="aaaa">Back</a>
                </span>
                <span runat="server" id="backToArr" visible="false">
                    <a href="InboxServiceRequest_Arch.aspx" class="aaaa">Back</a>
                </span>
                <span>
                    <a href="TaskMonitoring.aspx" style="margin-right: 18px;" class="aaaa">Task Monitoring</a>&nbsp;&nbsp; 
                </span>
                  <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>
                <div class="edit-contact">
                    <div class="editprofile" style="text-align: center; border: none;" id="divmsg" runat="server" visible="false">
                    </div>
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server"></asp:LinkButton>
                        </div>
                    </div>
                    <ul id="editform" runat="server" >
                      <%--  <li class="mobile_inboxEmpCode">
                            <span><b>Task Details</b></span><br />
                            <br />
                        </li>
                        <li></li>--%>
                        <li class="mobile_inboxEmpCode">
                           <br />
                            <br />
                        </li>
                        <li><asp:Label runat="server" ID="Label2" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label></li>
                        <li style="padding-bottom:15px">
                            <span> Project/ Location code </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList runat="server" ID="ddlProjectLocation"  Width="350px" AppendDataBoundItems="false" AutoPostBack="true" >
                            </asp:DropDownList>
                        </li>
                        <li></li>
                        <li class="mobile_InboxEmpName">
                            <span>Project Start Date</span><span style="color: red">*</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_ProjectStartDate" MaxLength="250" onkeypress="return false;" onkeydown="return false;" runat="server" ></asp:TextBox>
                             <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txt_ProjectStartDate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>

                        </li>
                        <li class="mobile_InboxEmpName">
                            <span>Project End Date</span><span style="color: red">*</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_ProjectEndDate" onkeypress="return false;" AutoPostBack="true" onkeydown="return false;" OnTextChanged="txt_ProjectEndDate_TextChanged"  MaxLength="15" runat="server" ></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txt_ProjectEndDate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>

                         <li class="mobile_InboxEmpName" style="padding-bottom:10px">
                            <span>Frequency (in Days)</span><span style="color: red">*</span><br />
                            <asp:TextBox AutoComplete="off" ID="Txt_Durationindays" MaxLength="3" onkeypress="return isNumberKey(event)" runat="server"></asp:TextBox>
                        </li>
                        <li class="mobile_InboxEmpName" style="padding-bottom:10px">
                            <span>Task Creation before due date (in Days)</span><span style="color: red">*</span><br />
                            <asp:TextBox AutoComplete="off" ID="Txt_Taskcreationbeforedateindays" MaxLength="3" onkeypress="return isNumberKey(event)" runat="server"></asp:TextBox>
                           
                        </li>

                        <li class="mobile_InboxEmpName" style="padding-bottom:10px">
                           <span runat="server" id="Labelchk_ISActive">IS Active</span><br />
                            <asp:CheckBox runat="server" ID="chk_ISActive"  Checked="true" Enabled="false"  />
                        </li>
                        <li>
                        </li>
                       
                        <li class="mobile_InboxEmpName" style="padding-bottom:10px" runat="server" id="LIDeActivationRemarks">
                           <span>Remarks</span><span style="color: red">*</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_DeactivationRemark" MaxLength="500" CssClass="noresize" TextMode="MultiLine" Rows="4" runat="server"></asp:TextBox>
                        </li>
                        <li>
                        </li>
                       

						<li runat="server" id="LIDeActivationRemark1">
                          
                        </li>
						<li style="margin-top: 10px; align-items: center;">
                            <asp:LinkButton ID="btn_ATT_Save"    runat="server" Text="Save" ToolTip="Save"  CssClass="Savebtnsve" OnClientClick="return SaveFD7Click();" OnClick="btn_ATT_Save_Click" ></asp:LinkButton>
                            <asp:LinkButton ID="btn_ATT_Update"  runat="server"  Text="Cancel" ToolTip="Cancel"  CssClass="Savebtnsve" OnClick="btn_ATT_Update_Click" ></asp:LinkButton>
                           </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="mobile_Savebtndiv">
    </div>

    <br />
 
   
    <asp:HiddenField ID="hdnAttendeeID" runat="server" />
    <asp:HiddenField ID="hdnTaskID" runat="server" />
   <asp:HiddenField ID="hdnYesNo" runat="server" />
  <asp:HiddenField ID="HDID" runat="server" />
    <asp:HiddenField ID="HFDateendDB" runat="server" />
     <asp:HiddenField ID="HFFrequencyDays" runat="server" />
     <asp:HiddenField ID="HFCreationDays" runat="server" />


    <asp:HiddenField ID="FilePath" runat="server" />

    <%--<script src="../js/dist/jquery-3.2.1.min.js"></script>--%>
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <script type="text/javascript">      
        $(document).ready(function () {
            $("#MainContent_ddlProjectLocation").select2();
           
        });
    </script>
    <script type="text/javascript">

        function onCalendarShown() {
            var cal = $find("calendar1");
            cal._switchMode("years", true);
            if (cal._yearsBody) {
                for (var i = 0; i < cal._yearsBody.rows.length; i++) {
                    var row = cal._yearsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }

         function SaveFD7Click() {
            try {
                var msg = "Do you want to Submit?";
                var retunboolean = true;
                var ele = document.getElementById('<%=btn_ATT_Save.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        Confirm(msg);
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }

         function Confirm(msg) {
            //Testing();
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm(msg)) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }

            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return;
        }

        function onCalendarHidden() {
            var cal = $find("calendar1");
            if (cal._yearsBody) {
                for (var i = 0; i < cal._yearsBody.rows.length; i++) {
                    var row = cal._yearsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }
        function call(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "year":
                    var cal = $find("calendar1");
                    cal.set_selectedDate(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged(); break;
            }
        }
       

         function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        // Allow only numbers (charCode 48-57 are 0-9)
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
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

    </script>
</asp:Content>


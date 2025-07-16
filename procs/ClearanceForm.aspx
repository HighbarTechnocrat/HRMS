<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="ClearanceForm.aspx.cs" Inherits="procs_ClearanceForm" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/manageleave_css.css" type="text/css" media="all" />
    <%-- <link href="<%=ReturnUrl("css") %>calendar/calendar.css" rel="stylesheet" type="text/css" />
    <link href="sampleform_css.css" rel="stylesheet" type="text/css" />--%>
    <%--<ajaxToolkit:ToolkitScriptManager ID="srcp" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
            background: #dae1ed;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
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
                        <asp:Label ID="lblheading" runat="server" Text="Clearance Form"></asp:Label>
                    </span>
                </div>

                <span>
                     <a href="default.aspx" class="aaaa" >Home Menu</a>
                </span>
                <div class="edit-contact">
                  
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server"></asp:LinkButton>
                        </div>
                    </div>
                    <div class="editprofile" id="editform1" runat="server" visible="true">
                            <div class="editprofileform">
                            </div>
                    </div>
                    <ul id="editform" runat="server" visible="false">
                        <li>
                            <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                         </li>
                        <li></li>
                        <li class="date">
                            <br />
                            <span>Emp Code</span>                
                            <asp:TextBox ID="txt_EmpCode" Enabled="false" AutoComplete="off" runat="server"></asp:TextBox>
                        </li>
                        <li class="leavedays">
                             <br />
                            <span>Emp Name</span>                
                            <asp:TextBox ID="txtEmp_Name" Enabled="false" AutoComplete="off" runat="server"></asp:TextBox>
                        </li>
                        <li>
                             <br />
                            <span>Designation</span>                
                            <asp:TextBox ID="txtEmp_Desigantion" Enabled="false" AutoComplete="off" runat="server"></asp:TextBox>
                        </li>
                        <li class="leavedays">
                             <br />
                            <span>Department</span>                
                            <asp:TextBox ID="txtEmp_Department" Enabled="false" AutoComplete="off" runat="server"></asp:TextBox>
                        </li>
                          <li class="leavedays">
                             <br />
                            <span>Location / Project</span>    <br />            
                            <asp:TextBox ID="txt_Project" Enabled="false" AutoComplete="off" runat="server"></asp:TextBox>
                        </li>

                        <li class="leavedays">
                        </li>

                        <li class="Approver">
                            <%--<span>Approver </span>--%>
                            <br />
                            
                        </li>
                        <li>

                        </li>
                        <li>
                             </li>
                            <li class="upload">
                           <span>Upload Exit Survey Form</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                            <asp:FileUpload ID="FileUploadExitSurvey" runat="server" />
                        </li>

                         <li>

                        </li>
                            <li class="upload">
                                  <span>Upload Exit Form A</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                            <asp:FileUpload ID="FileUploadExitFormA" runat="server" />
                        </li>
                         <li>
                        </li>
                       
                            <li class="upload">
                             <span>Upload Clearance Form</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                            <asp:FileUpload ID="uploadfile" runat="server" />
                        </li>

                        <li>
                         <br />
                            <asp:LinkButton ID="btnIn" runat="server" CssClass="Savebtnsve" Text="Submit"  OnClick="btnIn_Click" OnClientClick="return SaveMultiClick();">Submit</asp:LinkButton>
                        </li>
                        <li></li>
                        <li></li>
                        <li>
                            <br />
                            <br />
                            <br />
                        </li>
                    </ul>
                </div>
                <asp:HiddenField ID="hdnReqid" runat="server" />
                <asp:HiddenField ID="hdnEMPIDEXit" runat="server" />
                <asp:HiddenField ID="hdnYesNo" runat="server" />
                <%--<asp:HiddenField ID="hdnLeaveCount" runat="server" />
                <asp:HiddenField ID="hdnLeaveCountFix" runat="server" />--%>
                <asp:HiddenField ID="HDNResignValue" runat="server" />
            </div>
        </div>
    </div>


    <script type="text/javascript">
        $(document).ready(function () {
            $(".numericInputTextBox").each(function () {
                $(this).attr("MaxLength", "100");
            });
            $('.numericInputTextBox').on("cut copy paste", function (e) {
                e.preventDefault();
            });
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
        function SaveMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=btnIn.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        ConfirmIn();
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        function ConfirmIn() {
            //Testing();
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
    </script>
</asp:Content>


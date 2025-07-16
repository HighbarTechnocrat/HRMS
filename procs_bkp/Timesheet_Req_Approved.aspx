<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="Timesheet_Req_Approved.aspx.cs" Inherits="procs_Timesheet_Req_Approved" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>


<%@ Register TagName="calender" TagPrefix="ucical" Src="~/themes/creative1.0/LayoutControls/leaveCalender.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

      <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/manageleave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/ModifyLeaveRequest_css.css" type="text/css" media="all" />
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

        #content-container table {
            width: 125% !important;
        }

        /* Style the tab */
        .tab {
            overflow: hidden !important;
            border: 1px solid black !important;
            background-color: #C7D3D4 !important;
            position: relative !important;
            margin-bottom: 1px !important;
            width: 284% !important;
            color: #fff !important;
            overflow: hidden !important;
        }

            /* Style the buttons inside the tab */
            .tab button {
                background-color: inherit;
                float: left;
                border: none;
                outline: none;
                cursor: pointer;
                padding: 14px 16px;
                transition: 0.3s;
                font-size: 17px;
            }

                /* Change background color of buttons on hover */
                .tab button:hover {
                    background-color: #3D1956 !important;
                    color: #febf39 !important;
                }

                /* Create an active/current tablink class */
                .tab button.active {
                   background-color: #3D1956 !important;
                   color: #febf39 !important;
                }

        /* Style the tab content */
        .tabcontent {
            display: none;
            padding: 6px 12px;
            border: 1px solid black;
            border-top: none;
            width:278% !important;
        }
        .tablinks{
          background-color:#C7D3D4 !important;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

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
                        <asp:Label ID="lblheading" runat="server" Text="Approval Timesheet"></asp:Label>
                        <asp:HiddenField ID="HFSubmitedDate" runat="server" />
                    </span>
                </div>

                <span>
                     <a href="Timesheet.aspx" class="aaaa" >Timesheet Menu</a>
                </span>
                <span>
                    <a href="ApprovedTimesheet_Req.aspx" style="margin-right: 10px;" class="aaaa">Back</a>
                </span>

                <%--<div class="manage_grid" style="width: 100%; height: auto;">
                    
                    <center>
                          
                        </center>
                </div>--%>


                <%--'<%#"~/Details.aspx?ID="+Eval("ID") %>'>--%>


                
                <div class="edit-contact">
                    <%-- <div class="editprofile" style="text-align: center; border: none;" id="divmsg" runat="server" visible="false">
                        <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: green; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                    </div>--%>
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
<%--                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" OnClick="lnkcont_Click"></asp:LinkButton>
                        </div>--%>
                    </div>

                    <div class="editprofile" id="editform1" runat="server" visible="true">
                            <div class="editprofileform">
                                <%--<ucical:calender ID="icalender" runat="server"></ucical:calender>--%>
                            </div>
                    </div>

                    <ul id="editform" runat="server" visible="false">

                        <li>
                            <asp:Label runat="server" ID="lblmessage" Visible="True" Style="                                    color: red;
                                    font-size: 14px;
                                    font-weight: 400;
                                    text-align: center;"></asp:Label>
                            <%--<asp:TextBox runat="server" ID="lblmessage1" Visible="False" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;" OnTextChanged="lblmessage1_TextChanged"></asp:TextBox>--%>
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
                        <li class="leavedays">
                             <br />
                            <span>Module</span>                
                            <asp:TextBox ID="txt_SapModule" Enabled="false" AutoComplete="off" runat="server"></asp:TextBox>
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
                             <br />
                            <span>Date</span>                
                            <br />
                            <asp:TextBox ID="txtDate" Enabled="false" AutoComplete="off" runat="server"></asp:TextBox>
                        </li>
                        <li>
                            <br/><table style="width: 100%;">
                                <tr>
                                    <td>Legends</td>
                                    <td class="anlign">
                                        <asp:Image ID="imgLeavePending" runat="server"
                                            ImageUrl="~/images/Calendar/PendLeave.png" Height="13px" Width="25px" Style="margin-bottom: 0px " />
                                        &nbsp;Leave
                                    </td>
                                    <td class="anlign">
                                        <asp:Image ID="HolidayImg" runat="server"
                                            ImageUrl="~/images/Calendar/Holiday_1.png" Height="13px" Width="25px" />
                                        &nbsp;Holiday                   
                           
                                    </td>
                                    <td class="anlign">
                                        <asp:Image ID="imgLeaveApproved" runat="server"
                                            ImageUrl="~/images/Calendar/weekend.png" Height="13px" Width="25px" Style=" margin-bottom: 0px" />
                                        &nbsp;Weekend
                                    </td>
                                </tr>
                               
                            </table>
                        </li>
                        <li></li>
                        <li class="Approver">
                            <%--<span>Approver </span>--%>
                            <br />
                            <asp:PlaceHolder ID="PlaceHolder1" runat="server" />
                            <%--  <div style="width:100%;overflow:auto">--%>
                            <div id="div_gridholder" style="width: 100%; overflow: auto;">
                                <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333"
                                    GridLines="Both" Style="display: none;" Width="100%"
                                    AutoGenerateColumns="true" OnRowDataBound="GridView1_RowDataBound">
                                </asp:GridView>

                            </div>                           
                        </li>
                        <li>
                            <br />
                            <span>Timesheet Details</span>
                            <br />
                            <div class="tab">
                                <button class="tablinks active" style="border-right: 1px solid !important;" runat="server" id="btn1" onclick="openCity(event, 'tab1');return false;"></button>
                                <button class="tablinks" style="border-right: 1px solid !important" runat="server" id="btn2" onclick="openCity(event, 'tab2');return false;"></button>
                                <button class="tablinks" style="border-right: 1px solid !important" runat="server" id="btn3" onclick="openCity(event, 'tab3');return false;"></button>
                                <button class="tablinks" style="border-right: 1px solid !important" runat="server" id="btn4" onclick="openCity(event, 'tab4');return false;"></button>
                                <button class="tablinks" style="border-right: 1px solid !important" runat="server" id="btn5" onclick="openCity(event, 'tab5');return false;"></button>
                                <button class="tablinks" style="border-right: 1px solid !important" runat="server" id="btn6" onclick="openCity(event, 'tab6');return false;"></button>
                                <button class="tablinks" style="border-right: 1px solid !important" runat="server" id="btn7" onclick="openCity(event, 'tab7');return false;"></button>
                            </div>

                            <div id="tab1" class="tabcontent active" style="display:block">
                                <asp:PlaceHolder ID="PlaceHolder2" runat="server" />
                            </div>

                            <div id="tab2" class="tabcontent">
                               <asp:PlaceHolder ID="PlaceHolder3" runat="server" />
                            </div>

                            <div id="tab3" class="tabcontent">
                               <asp:PlaceHolder ID="PlaceHolder4" runat="server" />
                            </div>
                            <div id="tab4" class="tabcontent">
                                <asp:PlaceHolder ID="PlaceHolder5" runat="server" />
                            </div>

                            <div id="tab5" class="tabcontent">
                               <asp:PlaceHolder ID="PlaceHolder6" runat="server" />
                            </div>

                            <div id="tab6" class="tabcontent">
                               <asp:PlaceHolder ID="PlaceHolder7" runat="server" />
                            </div>
                            <div id="tab7" class="tabcontent">
                                <asp:PlaceHolder ID="PlaceHolder8" runat="server" />
                            </div>
                        </li>
                        <li></li>
                        <li>
                             <br />
                             <br />
                            <span>Remark</span>                
                            <br />
                            <asp:TextBox ID="txtRemark" MaxLength="100" Enabled="true" AutoComplete="off" runat="server"></asp:TextBox>
                        </li>
                        <li></li>
                        <%--<li></li>--%>
                        <li>
                           
                            <%--<asp:LinkButton ID="btnIn" runat="server" CssClass="Savebtnsve" Text="Approve" OnClick="btnIn_Click" 
                                OnClientClick="return SaveMultiClick();">Approve</asp:LinkButton>--%>

                            <asp:LinkButton ID="btnback_mng" runat="server" CssClass="Savebtnsve" OnClientClick="return SaveCurrectionClick();" Text="Correction" OnClick="btnback_mng_Click">Correction</asp:LinkButton>
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
                <asp:HiddenField ID="hdnleaveTypeid" runat="server" />
                <asp:HiddenField ID="hdnleaveType" runat="server" />
                <asp:HiddenField ID="hdnYesNo" runat="server" />
                <asp:HiddenField ID="hdnEmpEmail" runat="server" />
                <asp:HiddenField ID="hdnStartDate" runat="server" />
                <asp:HiddenField ID="hdnEndDate" runat="server" />

            </div>
        </div>
    </div>

    <script type="text/javascript">
        function openCity(evt, cityName) {
            
            var i, tabcontent, tablinks;
            tabcontent = document.getElementsByClassName("tabcontent");
            for (i = 0; i < tabcontent.length; i++) {
                tabcontent[i].style.display = "none";
            }
            tablinks = document.getElementsByClassName("tablinks");
            for (i = 0; i < tablinks.length; i++) {
                tablinks[i].className = tablinks[i].className.replace(" active", "");
            }
            document.getElementById(cityName).style.display = "block";
            evt.currentTarget.className += " active";
        }
    </script>

    <script type="text/javascript">
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
       <%-- function SaveMultiClick() {
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
        }--%>
        function SaveCurrectionClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=btnback_mng.ClientID%>');

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


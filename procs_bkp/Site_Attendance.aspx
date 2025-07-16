<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="Site_Attendance.aspx.cs" Inherits="Site_Attendance" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Attendence_css.css" type="text/css" media="all" />
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
           background:#ebebe4;
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
                        <asp:Label ID="lblheading" runat="server" Text="Site Attendance"></asp:Label>
                    </span>
                </div>

                <%--<span>
                    
                </span>--%>
                <div class="leavegrid">
                    <a href="Attendance.aspx" class="aaa">Attendance Menu</a>
                </div>
 
                 <asp:Label runat="server" ID="lblmessage" Visible="true" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                <div class="edit-contact">
                    <div class="editprofile" style="text-align: center; border: none;" id="divmsg" runat="server" visible="false">
                       
                    </div>
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" ></asp:LinkButton>
                        </div>
                    </div>


                    <ul id="editform" runat="server" visible="false">


                        <li class="EmpCode" style="display:none">
                            <span>Employee Code</span><br />
                            <asp:TextBox ID="txtEmpCode" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="EmpCode" style="display:none">
                            <span>Employee Name</span><br />
                            <asp:TextBox ID="txtEmpName" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="Designation" style="display:none">
                            <span>Designation</span><br />
                            <asp:TextBox ID="txtDesignation" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="Department" style="display:none">
                            <span>Department</span><br />
                            <asp:TextBox ID="txtDepartment" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="Requestdate" style="display:none">
                            <span>Request Date</span><br />
                            <asp:TextBox ID="txtRequest_Date" runat="server" AutoPostBack="True" Enabled="false"></asp:TextBox>
<%--                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtRequest_Date"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>--%>
                        </li>


                        <li class="dg_grid">
                            <div class="att_grid"  style="overflow:auto;height:550px">

                                <asp:GridView ID="dgAttendanceReg" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="90%" RowStyle-Wrap="False"
                                    DataKeyNames="Emp_code" OnRowDataBound="dgAttendanceReg_RowDataBound">
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                    <PagerStyle ForeColor="#000066" HorizontalAlign="left" BackColor="White" />
                                    <RowStyle ForeColor="#000066"  VerticalAlign="Top" HorizontalAlign="Left" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />

                                    <Columns>
                                        <asp:BoundField HeaderText="Location"
                                            DataField="Loc_Code"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="14.2%" 
                                            ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Employee Code"
                                            DataField="Emp_code"
                                            ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="14.2%" 
                                            ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Month"
                                            DataField="Month"
                                            ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="14.2%" 
                                            ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Working Days"
                                            DataField="Working_Days"
                                            ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="14.2%" 
                                            ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Applied Leaves"
                                            DataField="Emp_leaves"
                                            ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="14.2%" 
                                            ItemStyle-BorderColor="Navy" />
                                        
                                        <asp:TemplateField HeaderText="Absent Days">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAbsent"  AutoComplete="off" Width="50px"  runat="server" Text='<%# Bind("Absent_Days") %>' MaxLength="30" ></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="14.2%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Present Days">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPresent_Days"  AutoComplete="off"  Width="50px" runat="server" Text='<%# Bind("Present_Days") %>' MaxLength="30" ></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="14.2%" />
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>

                            </div>
                        </li>

                        <li class="Att_Approver">
                            <asp:ListBox ID="lstApprover" runat="server" Visible="true"></asp:ListBox>
                        </li>

                        <li class="Att_inter">
                            <asp:ListBox ID="lstIntermediate" runat="server" Visible="false"></asp:ListBox>
                        </li>
                        <li class="Att_Approver">
                            <span> LC - Late Coming </span><br />
                            <span> EG - Early Going </span><br />
                            <span> OD - On Duty </span><br />
                            <span> FS - Forgot to Swipe </span><br />
                            <span> LV - Leave </span><br />

                        </li>

                    </ul>
                </div>
            </div>

        </div>
    </div>


    <div class="att_buttons">

        <asp:LinkButton ID="att_submit" runat="server" OnClick="btnsubmit_Click" OnClientClick="return SaveMultiClick();">Submit</asp:LinkButton>

        <asp:LinkButton ID="att_back" runat="server" PostBackUrl="~/procs/Leaves.aspx">Back</asp:LinkButton>

    </div>





    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />

    <asp:HiddenField ID="hflEmpName" runat="server" />

    <asp:HiddenField ID="hflEmpDesignation" runat="server" />

    <asp:HiddenField ID="hflEmpDepartment" runat="server" />

    <asp:HiddenField ID="hflEmailAddress" runat="server" />

    <asp:HiddenField ID="hflGrade" runat="server" />

    <asp:HiddenField ID="hflapprcode" runat="server" />

    <asp:HiddenField ID="hdnApproverMailid" runat="server" />
    <asp:HiddenField ID="hdnApproverid" runat="server" />
    <asp:HiddenField ID="hdnemp_email" runat="server" />
    <asp:HiddenField ID="hdnReqid" runat="server" />
    <asp:HiddenField ID="hdnAppr_Cnt" runat="server" />

    <asp:HiddenField ID="hdnisSelfAppr" runat="server" />
     <asp:HiddenField ID="hdnHR_EMailID" runat="server" />
    <asp:HiddenField ID="hdnHR_Appr_id" runat="server" />
    <asp:HiddenField ID="hdnHR_Appr_Name" runat="server" />
    <asp:HiddenField ID="hdnHR_ApproverCode" runat="server" />
    
    <asp:HiddenField ID="hdnleaveconditiontypeid" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" /> 

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

        function onCharOnlyNumber(e) {
            var keynum;
            var keychar;
            var numcheck = /[0123456789./]/;

            if (window.event) {
                keynum = e.keyCode;
            }
            else if (e.which) {
                keynum = e.which;
            }
            keychar = String.fromCharCode(keynum);
            return numcheck.test(keychar);
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
                var ele = document.getElementById('<%=att_submit.ClientID%>');

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

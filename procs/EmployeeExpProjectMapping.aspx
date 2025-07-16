<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="EmployeeExpProjectMapping.aspx.cs" ValidateRequest="false" Inherits="EmployeeExpProjectMapping" %>

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

        #MainContent_btn_FD_Save,
        #MainContent_btn_FD_Update,
        #MainContent_btn_FD_Cancel,
        #MainContent_lnk_ed_Save,
        #MainContent_lnk_ed_Update,
        #MainContent_lnk_ed_Cancel,
        #MainContent_lnk_CD_Save,
        #MainContent_lnk_CD_Update,
        #MainContent_lnk_CD_Cancel,
        #MainContent_lnk_PD_Save,
        #MainContent_lnk_PD_Update,
        #MainContent_lnk_PD_Cancel,
        #MainContent_lnk_DD_Save,
        #MainContent_lnk_DD_Update,
        #MainContent_lnk_DD_Cancel,
        #MainContent_lnk_DE_Save,
        #MainContent_lnk_DE_Update,
        #MainContent_lnk_FinalSubmit,
        #MainContent_lnk_FileSave,
        #MainContent_lnk_FileUpdate,
        #MainContent_lnk_Search,
        #MainContent_lnk_FileCancel {
            background: #3D1956;
            color: #febf39 !important;
            padding: 0.5% 1.4%;
            margin: 0% 0% 0 0;
        }

        .noresize {
            resize: none;
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
    <%--<script type="text/javascript">
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
    </script>--%>
    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Employee Expenses Project Mapping"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
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
                    <a href="Voucher.aspx" class="aaaa">Payment Voucher Home</a>&nbsp;&nbsp; 
                </span>
                <div class="edit-contact">
                    <div class="editprofile" style="text-align: center; border: none;" id="divmsg" runat="server" visible="false">
                    </div>
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server"></asp:LinkButton>
                        </div>
                    </div>
                    <ul id="editform" runat="server" visible="false">
                        <li class="mobile_inboxEmpCode">
                            <span><b>Employee Expenses Mapping</b></span><br />
                            <br />

                        </li>
                        <li></li>                                        
                        <li class="mobile_InboxEmpName">
                            <br />
                            <span>Select Project Name</span>&nbsp;&nbsp;
                            <br />
                            <asp:DropDownList runat="server" ID="ddl_ProjectName" AutoPostBack="true" OnSelectedIndexChanged="ddl_ProjectName_SelectedIndexChanged"></asp:DropDownList>
                        </li>
                        <li class="mobile_InboxEmpName">
                            <br />
                            <span>Select Expenses Type</span>&nbsp;&nbsp;
                            <br />
                            <asp:DropDownList Enabled="false" runat="server" ID="ddl_Expenses" AutoPostBack="false" OnSelectedIndexChanged="ddl_Expenses_SelectedIndexChanged"></asp:DropDownList>
                        </li>
                         <li class="mobile_InboxEmpName">
                            <br />
                            <span>Select Employee Name</span>&nbsp;&nbsp;
                            <br />
                            <asp:ListBox  disabled="disabled" SelectionMode="multiple" runat="server" ID="ddl_EmpName" AutoPostBack="false" OnSelectedIndexChanged="ddl_EmpName_SelectedIndexChanged"></asp:ListBox>
                        </li>
                        <li></li> 
                        <li>
                            <br />
                            <asp:LinkButton ID="btn_FD_Save" Visible="true" runat="server" Text="Submit" ToolTip="Submit" CssClass="Savebtnsve" OnClientClick="return SaveFinalProfileClick_2();" OnClick="btn_FD_Save_Click"></asp:LinkButton>
                        </li>
                        <li></li>
                        <hr />
                        
                        <li>
                            <span>Project Name</span>
                            <br />
                            <asp:ListBox SelectionMode="multiple" runat="server" ID="ddl_Search_Project" AutoPostBack="false" >
                            </asp:ListBox>
                        </li>
                        <li>
                            <span>Expenses Type</span>
                            <br />
                            <asp:ListBox SelectionMode="multiple" runat="server" ID="ddl_Search_Exp" AutoPostBack="false" >
                            </asp:ListBox>
                        </li>
                        <li>
                            <span>Employee Name</span>
                            <br />
                           <%-- <asp:DropDownList runat="server" ID="ddl_Search_Employee" AutoPostBack="false">
                            </asp:DropDownList>--%>
                            <asp:ListBox SelectionMode="multiple" runat="server" ID="ddl_Search_Employee" AutoPostBack="false" ></asp:ListBox>
                        </li>
                        <li></li>
                        <li>
                            <br />
                            <asp:LinkButton ID="lnk_Search" Visible="true" runat="server" Text="Search" ToolTip="Submit" CssClass="Savebtnsve" OnClick="lnk_Search_Click"></asp:LinkButton>
                        </li>
                        <li></li>
                        <hr />
                        <li style="width: 100%; height: auto;">
                            <div class="manage_grid" style="                                    width: 100%;
                                    height: auto;">
                                <asp:GridView ID="gvExpMappingList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="solid" BorderWidth="1px"
                                    DataKeyNames="Id,Status" CellPadding="3" AutoGenerateColumns="False" Width="100%" OnPageIndexChanging="gvExpMappingList_PageIndexChanging" EditRowStyle-Wrap="false" PageSize="20" AllowPaging="True">
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                    <PagerStyle CssClass="gridpager" HorizontalAlign="Right" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Employee Code" DataField="EMP_CODE"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="8%" />

                                        <asp:BoundField HeaderText="Employee Name" DataField="Emp_Name"
                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="15%" />

                                       <%-- <asp:BoundField HeaderText="Employee Email" DataField="Emp_Emailaddress"
                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="10%" />--%>

                                        <asp:BoundField HeaderText="Band" DataField="grade"
                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="4%" />

                                        <asp:BoundField HeaderText="Designation" DataField="Designation"
                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="12%" />

                                        <asp:BoundField HeaderText="Employee Location" DataField="emp_projectName"
                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="15%" />

                                        <asp:BoundField HeaderText="Access To Expenses Location" DataField="AccessProject"
                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="15%" />

                                        <asp:BoundField HeaderText="Expenses Type" DataField="Expenses"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="15%" />
                                       <asp:BoundField HeaderText="Status" DataField="Status"
                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="8%" />

                                        <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" id="Chk_IsStatus" OnClientClick="return confirm('Are you sure update this record?');" OnCheckedChanged="Chk_IsStatus_CheckedChanged" AutoPostBack="true" Checked='<%# Eval("Status").ToString()=="Active" ? true : false %>' />
                                                <asp:ImageButton ID="lnkEdit" Visible="false" runat="server" Width="20px" Height="15px" OnClientClick="return confirm('Are you sure update this record?');" OnClick="lnkEdit_Click" ImageUrl="~/Images/edit.png" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnk_Delete" runat="server" Width="15px" Height="15px" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure delete?');" OnClick="lnk_Delete_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                            </div>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="mobile_Savebtndiv">
    </div>

    <br />
    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>--%>
    <%--<asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />--%>

    <asp:HiddenField ID="hdnvouno" runat="server" />
    <asp:HiddenField ID="hdnIsMarried" runat="server" />
    <asp:HiddenField ID="hdnFamilyDetailID" runat="server" />
    <asp:HiddenField ID="hdnEduactonDetailID" runat="server" />
    <asp:HiddenField ID="hdnCertificationDetailID" runat="server" />
    <asp:HiddenField ID="hdnProjectDetailID" runat="server" />
    <asp:HiddenField ID="hdnDomainDetailID" runat="server" />
    <asp:HiddenField ID="hdnFileDetailID" runat="server" />
    <asp:HiddenField ID="hdnFileName" runat="server" />
    <asp:HiddenField ID="hdnFilePath" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="hdnempcode" runat="server" />


    <asp:HiddenField ID="FilePath" runat="server" />

    <%--<script src="../js/dist/jquery-3.2.1.min.js"></script>--%>
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <script type="text/javascript">      
        $(document).ready(function () {
            $("#MainContent_ddl_EmpName").select2();
            $("#MainContent_ddl_ProjectName").select2();
            $("#MainContent_ddl_Expenses").select2();
            $("#MainContent_ddl_Search_Project").select2();
            $("#MainContent_ddl_Search_Employee").select2();
            $("#MainContent_ddl_Search_Exp").select2();
            //$("#MainContent_ddl_Qualification").select2();
            //$("#MainContent_ddl_Board").select2();
            //$("#MainContent_ddl_Degree").select2();
            //$("#MainContent_ddl_CD_Certification").select2();
            //$("#MainContent_ddl_CD_Module").select2();
            //$("#MainContent_ddl_PD_ProjectType").select2();
            //$("#MainContent_lst_PD_IndustryType").select2();
            //$("#MainContent_ddl_PD_Role").select2();
            //$("#MainContent_ddl_PD_Module").select2();
            //$("#MainContent_lst_DD_IndustryType").select2();
            //$("#MainContent_ddl_DD_Domain").select2();
            //$("#MainContent_ddl_DD_Role").select2();
            //$("#MainContent_ddl_Stream").select2();
            //$("#MainContent_ddl_PD_OrgType").select2();
            //$("#MainContent_ddl_PD_OrgName").select2();
            //$("#MainContent_ddl_DD_OrgType").select2();
            //$("#MainContent_ddl_DD_OrgName").select2();
            //$("#MainContent_ddl_DocumentName").select2();
            $("#MainContent_txtJobDescription").htmlarea();

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
        function checkProjectEndDate(sender, args) {
            if (sender._selectedDate >= new Date()) {
                alert("You can not select a future date than today!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
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



        function onCharOnlyNumber(e) {
            var keynum;
            var keychar;
            var numcheck = /[0123456789.]/;

            if (window.event) {
                keynum = e.keyCode;
            }
            else if (e.which) {
                keynum = e.which;
            }
            keychar = String.fromCharCode(keynum);
            return numcheck.test(keychar);
        }

        //End Delete
        function onCharOnlyNumber_EXP(e) {
            var keynum;
            var keychar;
            var numcheck = /[0123456789.]/;

            if (window.event) {
                keynum = e.keyCode;
            }
            else if (e.which) {
                keynum = e.which;
            }
            keychar = String.fromCharCode(keynum);
            return numcheck.test(keychar);
        }
        function onCharOnlyNumber_Mobile(e) {
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

        function DownloadFile(FileName) {

            var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;
            //alert(FileName);        
            window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
            //  window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
        }
        function SaveFinalProfileClick_2() {
            try {
                var msg = "Do you want to submit?";
                var retunboolean = true;
                var ele = document.getElementById('<%=btn_FD_Save.ClientID%>');

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
        //End Document Details

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
    </script>
</asp:Content>

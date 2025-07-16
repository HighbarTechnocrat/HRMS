<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="ABAP_Prd_TimeSheetAdd.aspx.cs"
    Inherits="procs_ABAP_Prd_TimeSheetAdd" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/ClaimMobile_css.css" type="text/css" media="all" />


    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />--%>

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

        .Savebtnsve {
            background: #3D1956;
            color: #febf39 !important;
            padding: 0.5% 1.4%;
            margin: 0% 0% 0 0;
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
                        <asp:Label ID="lblheading" runat="server" Text="ABAP Object Completion"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="Label1" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>
                <div>
                    <span>
                        <a href="Timesheet.aspx" class="aaaa">ABAP Object Completion Index</a>
                    </span>
                </div>


                <div class="edit-contact">



                    <ul id="editform" runat="server" visible="true">
                        <asp:Label runat="server" ID="lblRetention" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>

                        <li>
                             
                        </li>
                        <li></li>
                        <li class="claimmob_fromdate">
                            <br />
                            <span>Employee Code </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtEmpCode" Enabled="false" runat="server"></asp:TextBox>

                        </li>
                        <li>
                            <span>Employee Name </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtEmpName" Enabled="false" runat="server"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Designation</span>
                            <asp:TextBox ID="txtDesignation" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li>
                            <span>Department</span>
                            <asp:TextBox ID="txtDepartment" runat="server" Enabled="false"></asp:TextBox>
                        </li>

                        <li class="mobile_inboxEmpCode">
                            <span>Week Start Date</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <asp:TextBox ID="txtFromdate" AutoComplete="off" runat="server" AutoPostBack="true" OnTextChanged="txtFromdate_TextChanged" MaxLength="10" AutoCompleteType="Disabled"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender OnClientDateSelectionChanged="detect_sunday" ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtFromdate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>

                            <br />
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Week End Date</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <asp:TextBox ID="txtToDate" AutoComplete="off" runat="server" Enabled="false" MaxLength="10" AutoCompleteType="Disabled"></asp:TextBox>
                            <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>--%>
                        </li>

                        <li class="mobile_inboxEmpCode">

                             <asp:Label ID="lblMilestoneCostCenter_Err" runat="server" Visible="True" Style="color: red; font-size: 13px; font-weight: 500; text-align: center;"></asp:Label>
                            
                             
                        </li>
                        <li></li>

                        <li class="trvl_grid" runat="server" id="idliCostCenterList_ACC">
                            <br />
                             <asp:LinkButton ID="trvldeatils_btnSave" runat="server" Text="Add New Row" ToolTip="Add New Row"
                                CssClass="Savebtnsve" OnClick="trvldeatils_btnSave_Click" ForeColor="Blue"></asp:LinkButton>
                           <br /> <br />
                            <%--<asp:LinkButton ID="LinkButton1" runat="server" Text="Add Cost Center" ToolTip="Add Cost Center" 
                                CssClass="Savebtnsve" OnClick="trvldeatils_btnSave_Click"></asp:LinkButton>--%>

                            <asp:GridView ID="dgTimesheet" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                                AutoGenerateColumns="False" Width="100%" OnRowDataBound="dgTimesheet_RowDataBound">
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
                                    <asp:TemplateField HeaderText="Project Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprojectname" Visible="false" runat="server" Text='<%# Eval("Comp_Code") %>'></asp:Label>
                                            <asp:DropDownList ID="lstProjectName" runat="server" CssClass="DropdownListSearch" Width="200px"></asp:DropDownList>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="8%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Object No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAppr_Status" Visible="false" runat="server" Text='<%# Eval("Appr_Status") %>'></asp:Label>
                                            <asp:Label ID="lblIDEdit" Visible="false" runat="server" Text='<%# Eval("IDEdit") %>'></asp:Label>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" Width="5%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Is Completed" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblIsCompleted" Visible="false" runat="server" Text='<%# Eval("IsCompleted") %>'></asp:Label>
                                            <asp:DropDownList ID="lstIsCompleted" runat="server" CssClass="DropdownListSearch" Width="80px">
                                                <%--<asp:ListItem Text="Yes" Value="Yes" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="No"></asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="8%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Object Name (TCode)" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtObjectCode" Style="width: 200px !important" AutoComplete="off" MaxLength="100" runat="server" Text='<%# Eval("ObjectCode") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="10%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Object Type" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblobjectType" Visible="false" runat="server" Text='<%# Eval("ObjectType") %>'></asp:Label>
                                            <asp:DropDownList ID="lstObjectType" runat="server" CssClass="DropdownListSearch" Width="200px"></asp:DropDownList>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="8%" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Object Description" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtObjectDescription" MaxLength="200" Style="width: 280px !important" Text='<%# Eval("ObjectDescription") %>' AutoComplete="off" runat="server" Width="100px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="20%" />
                                    </asp:TemplateField>


                                </Columns>
                            </asp:GridView>

                            <asp:GridView ID="dgTimesheetView" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                                AutoGenerateColumns="False" Width="100%" Visible="false">
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

                                    <asp:BoundField HeaderText="Project Name" DataField="Comp_Code"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="10%" />

                                    <asp:TemplateField HeaderText="Object No." HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="100" ItemStyle-CssClass="gridviewpaddingleft">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" Width="3%" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Is Completed" DataField="IsCompleted"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-CssClass="gridviewpaddingleft" />

                                    <asp:BoundField HeaderText="Object Name" DataField="ObjectCode"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="15%" ItemStyle-CssClass="gridviewpaddingleft" />

                                      <asp:BoundField HeaderText="Object Type" DataField="ObjectTypeName"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="8%" ItemStyle-CssClass="gridviewpaddingleft" />


                                    <asp:BoundField HeaderText="Object Description" DataField="ObjectDescription"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="15%" ItemStyle-CssClass="gridviewpaddingleft" />

                                    <asp:BoundField HeaderText="PM Status" DataField="Appr_Status"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-CssClass="gridviewpaddingleft" />

                                    <asp:BoundField HeaderText="PM Remarks" DataField="Appr_Remakrs"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="15%" ItemStyle-CssClass="gridviewpaddingleft" />

                                </Columns>
                            </asp:GridView>


                            <br />

                        </li>

                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="mobile_Savebtndiv">
        <asp:LinkButton ID="mobile_btnSave" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();" OnClick="mobile_btnSave_Click">Submit</asp:LinkButton>
        <%--        <asp:LinkButton ID="mobile_btnSave" runat="server" Text="Save or Later" ToolTip="Save or Later" CssClass="Savebtnsve">Save or Later</asp:LinkButton>--%>
        <asp:LinkButton ID="mobile_cancel" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="Savebtnsve" OnClick="mobile_cancel_Click">Back</asp:LinkButton>
    </div>
    <br />


    <asp:HiddenField ID="hdnEmpCode" runat="server" />
    <asp:HiddenField ID="hdnId" runat="server" />
    <asp:HiddenField ID="HDStatus" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="hdnProjectRowCnt" runat="server" />

    <script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <script type="text/javascript">      
        $(document).ready(function () {
            //$("#MainContent_DDlMonthYear").select2();

            $(".DropdownListSearch").select2();
        });
    </script>
    <script type="text/javascript">

        function detect_sunday(sender, args) {
            if (sender._selectedDate.getDay() == 0) {
                sender._selectedDate = new Date();
                // set the date back to the current date
                sender._textbox.set_Value(sender._selectedDate.format(sender._format));
                alert("You can't select sunday!");
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

    </script>


</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="MyAttend_Req.aspx.cs" Inherits="MyAttend_Req" %>

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
                        <asp:Label ID="lblheading" runat="server" Text="Regularize Attendance"></asp:Label>
                    </span>
                </div>

                <span>
                     <a href="Attendance.aspx" class="aaaa" >Attendance Menu</a>
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
                            <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
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
                             <%--<br />
                            <span>Project</span>                
                            <asp:TextBox ID="TextBox4" Enabled="false" AutoComplete="off" runat="server"></asp:TextBox>--%>
                        </li>

                        <li class="Approver">
                            <%--<span>Approver </span>--%>
                            <br />
                            <asp:GridView ID="gvMngLeaveRqstList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="solid" BorderWidth="1px"
                                DataKeyNames="Req_Id" CellPadding="3" OnRowDataBound="gvMngLeaveRqstList_DataBound" AutoGenerateColumns="False" Width="135%" EditRowStyle-Wrap="false" PageSize="20" AllowPaging="True">
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
                                    <asp:BoundField HeaderText="Date"
                                        DataField="att_date"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="In / Out"
                                        DataField="IN_OUT_TYPE"
                                        ItemStyle-HorizontalAlign="center"
                                        HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%"
                                        ItemStyle-BorderColor="Navy" />
                                     <asp:BoundField HeaderText="Time"
                                        DataField="IN_OUT_TIME"
                                        ItemStyle-HorizontalAlign="center"
                                         HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="15%"
                                        ItemStyle-BorderColor="Navy" />
                                     <asp:BoundField HeaderText="Status"
                                        DataField="IN_OUT_STATUS"
                                        ItemStyle-HorizontalAlign="center"
                                         HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="15%"
                                        ItemStyle-BorderColor="Navy" />
                                     <asp:BoundField HeaderText="Leave Type"
                                        DataField="Leave_Type"
                                        ItemStyle-HorizontalAlign="center"
                                         HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />
                                    <asp:BoundField HeaderText="Days"
                                        DataField="LeaveDays"
                                        ItemStyle-HorizontalAlign="center"
                                         HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />
                                    <asp:TemplateField HeaderText="Regularization" HeaderStyle-Width="20%"  ItemStyle-HorizontalAlign="center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLeaveId" runat="server" Text='<%# Eval("Req_Id") %>' Visible="false" />
                                            <asp:Label ID="lblDate" runat="server" Text='<%# Eval("att_date") %>' Visible="false" />
                                            <asp:DropDownList ID="ddlLeaveType" AutoPostBack="true" OnSelectedIndexChanged="ddlLeaveType_SelectedIndexChanged" runat="server">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Remark" HeaderStyle-Width="20%"  ItemStyle-HorizontalAlign="center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                          <asp:TextBox runat="server" CssClass="numericInputTextBox"  ID="txtRemark" MaxLength="50" Width="75%"></asp:TextBox>
                                            <asp:TextBox ID="lblatt_id" runat="server" Text='<%# Eval("Req_Id") %>' Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </li>
                        <li>

                        </li>
                        <li></li>
                        <%--<li></li>--%>
                        <li>
                            
                            <br />
                            <asp:LinkButton ID="btnIn" runat="server" CssClass="Savebtnsve" Text="Submit" OnClick="btnIn_Click" OnClientClick="return SaveMultiClick();">Submit</asp:LinkButton>
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
                <asp:HiddenField ID="hdnLeaveCount" runat="server" />
                <asp:HiddenField ID="hdnLeaveCountFix" runat="server" />
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

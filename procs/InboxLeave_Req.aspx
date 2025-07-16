<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="InboxLeave_Req.aspx.cs" Inherits="InboxLeave_Req" %>

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
            /*background: #dae1ed;*/
            background: #ebebe4;
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
                        <asp:Label ID="lblheading" runat="server" Text="Inbox Leave Request"></asp:Label>
                    </span>
                </div>

                <span>
                    <a href="Leaves.aspx" class="aaaa">Leave Menu</a>
                </span>

                <div class="manage_grid" style="width: 100%; height: auto;">
                    <center>
                          <asp:GridView ID="gvMngLeaveRqstList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" 
                           DataKeyNames="Req_id,leaveTypeid,wrk_schedule,LRType"   CellPadding="3" AutoGenerateColumns="False" Width="100%" OnPageIndexChanging ="gvMngLeaveRqstList_PageIndexChanging"  EditRowStyle-Wrap="false">
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
                             <asp:TemplateField HeaderText="View" HeaderStyle-Width="10%">
                             <ItemTemplate>
                             <asp:ImageButton id="lnkEdit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkLeaveDetails_Click"/>
                             </ItemTemplate>
                             <ItemStyle HorizontalAlign="Center" />
                         </asp:TemplateField>
                            <asp:BoundField HeaderText="Applied On"
                                DataField="RequestedDate"
                                ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="left"
                                ItemStyle-Width="10%" 
                                ItemStyle-BorderColor="Navy" />

                            <asp:BoundField HeaderText="Leave Type"
                                DataField="LeaveType"
                                 ItemStyle-HorizontalAlign="center"                                 
                                ItemStyle-Width="15%" 
                                ItemStyle-BorderColor="Navy" />

                            <asp:BoundField HeaderText="For Date"
                                DataField="Period"
                                 ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="20%" 
                                ItemStyle-BorderColor="Navy" />

                              <asp:BoundField HeaderText="Employee Name"
                                DataField="Applicant"
                                 ItemStyle-HorizontalAlign="center" />

                            <asp:BoundField HeaderText="Status"
                                DataField="Status"
                                 ItemStyle-HorizontalAlign="center" 
                                ItemStyle-Width="6%" 
                                ItemStyle-BorderColor="Navy" />

                             <asp:BoundField HeaderText="Days"
                                DataField="LeaveDays"
                                 ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="8%" 
                                ItemStyle-BorderColor="Navy" />
                                                        
                            <asp:TemplateField HeaderText="Select ALL" ItemStyle-Width="2%">
                            <HeaderTemplate>
                                <span>Select</span><br />
                                <asp:CheckBox ID="chkAll" ToolTip="Select ALL" runat="server" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK_clearancefromSubmitted" runat="server" AutoPostBack="false" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                        </center>

                    <ul>
                        <li class="Approver" style="display: none;">
                            <span>For Information To </span>
                            <br />
                            <asp:ListBox ID="lstIntermediate" Visible="true" runat="server"></asp:ListBox>
                        </li>
                    </ul>

                    <br />
                    <asp:LinkButton ID="btnIn" Visible="true" runat="server" CssClass="Savebtnsve" Text="Submit" OnClick="btnIn_Click" OnClientClick="return SaveMultiClick();">Submit</asp:LinkButton>

                    <br />

                </div>


                <%--'<%#"~/Details.aspx?ID="+Eval("ID") %>'>--%>



                <div class="edit-contact">
                    <%-- <div class="editprofile" style="text-align: center; border: none;" id="divmsg" runat="server" visible="false">
                        <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: green; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                    </div>--%>
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server"></asp:LinkButton>
                        </div>
                    </div>
                    <div style="display: none">

                        <li class="Reason">
                            <span id="idspnLWPTrnsfer_PL" runat="server">Transfer LWP to PL &nbsp;&nbsp; <span style="color: red">*</span> </span>
                            <br />
                            <asp:TextBox ID="txtLWP_To_PL" AutoComplete="off" runat="server" MaxLength="10"></asp:TextBox>
                        </li>

                        <span>Approver </span>
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

                    </div>

                    <ul id="editform" runat="server" visible="false">

                        <li>
                            <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                        </li>

                    </ul>
                </div>
                <asp:HiddenField ID="hdnReqid" runat="server" />
                <asp:HiddenField ID="hdnleaveTypeid" runat="server" />
                <asp:HiddenField ID="hdnleaveType" runat="server" />
                <asp:HiddenField ID="hdnApproverType" runat="server" />
                <asp:HiddenField ID="hdnEmpCode" runat="server" />
                <asp:HiddenField ID="hdnhrappType" runat="server" />
                <asp:HiddenField ID="hdnYesNo" runat="server" />
                <asp:HiddenField ID="hdnLoginUserName" runat="server" />
                <asp:HiddenField ID="hdnCurrentID" runat="server" />
                <asp:HiddenField ID="hdnIntermediateEmail" runat="server" />
                <asp:HiddenField ID="hdnOldLeaveCount" runat="server" />
                <asp:HiddenField ID="hdnEmpEmail" runat="server" />
                <asp:HiddenField ID="hdnleaveconditiontypeid" runat="server" />
                <asp:HiddenField ID="hflGrade" runat="server" />
                <asp:HiddenField ID="hdnfrmdate_emial" runat="server" />
                <asp:HiddenField ID="hdntodate_emial" runat="server" />
                <asp:HiddenField ID="hdnleaveid" runat="server" />
                <asp:HiddenField ID="hdnstaus" runat="server" />
                <asp:HiddenField ID="hdnisApprover_TDCOS" runat="server" />
                <asp:HiddenField ID="hdnnextappcode" runat="server" />
                <asp:HiddenField ID="hdnapprid" runat="server" />
                <asp:HiddenField ID="hdnApproverid_LWPPLEmail" runat="server" />
                <asp:HiddenField ID="hflApproverEmail" runat="server" />
                <asp:HiddenField ID="hflEmailAddress" runat="server" />
                <asp:HiddenField ID="hflEmpName" runat="server" />
                <asp:HiddenField ID="hdnPreviousApprMails" runat="server" />
                <asp:HiddenField ID="hdnFrmdate_LWP" runat="server" />
                <asp:HiddenField ID="hdnTodate_PL" runat="server" />
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:HiddenField ID="HDEmpCode" runat="server" />
                 <asp:HiddenField ID="HDLeaveDays" runat="server" />


            </div>
        </div>
    </div>


    <script type="text/javascript">

        function SaveMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=btnIn.ClientID%>');

                $('#loader').show();

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
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Submit ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return;

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

    </script>
</asp:Content>

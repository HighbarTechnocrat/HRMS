<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="Leave_Req_View.aspx.cs" Inherits="Leave_Req_View" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/ModifyLeaveRequest_css.css" type="text/css" media="all" />
    <%-- <link href="<%=ReturnUrl("css") %>calendar/calendar.css" rel="stylesheet" type="text/css" />
    <link href="sampleform_css.css" rel="stylesheet" type="text/css" />--%>
    <%--<ajaxToolkit:ToolkitScriptManager ID="srcp" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }


        .Dropdown {
            border-bottom: 2px solid #cccccc;
            /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
            background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;
            cursor: default;
        }

        .taskparentclass2 {
            width: 29.5%;
            height: 112px;
        }

        .taskparentclasskkk {
            width: 29.5%;
            height: 72px;
            /*overflow:initial;*/
        }

        #MainContent_Upl_leavetype {
            width: 91% !important;
        }

        #MainContent_ddlLeaveType {
            padding: 15px 0 !important;
        }

        #MainContent_ListBox1, #MainContent_ListBox2 {
            padding: 0 0 0% 0 !important;
            /*overflow: unset;*/
        }

        #MainContent_lstApprover, #MainContent_lstIntermediate {
            padding: 0 0 33px 0 !important;
            /*overflow: unset;*/
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
                        <asp:Label ID="lblheading" runat="server" Text="Leave Request"></asp:Label>
                    </span>
                </div>

                <div class="leavegrid">

                    <%-- <a href="Leaves.aspx" class="aaa" >Leave Menu</a>--%>
                    <asp:GridView ID="dgLeaveBalance" Visible="true" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="70%">
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
                            <asp:BoundField HeaderText="Leave Type"
                                DataField="Leave Type"
                                ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="left"
                                    ItemStyle-Width="22%" 
                                    ItemStyle-BorderColor="Navy"
                                    />

                            <asp:BoundField HeaderText="Opening"
                                DataField="Opening Balance"
                                ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="13%" 
                                ItemStyle-BorderColor="Navy"
                                />

                            <asp:BoundField HeaderText="Earned"
                                DataField="Earned"
                                ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="13%" 
                                ItemStyle-BorderColor="Navy"
                                />

                            <asp:BoundField HeaderText="Availed"
                                DataField="Availed"
                                ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="13%" 
                                ItemStyle-BorderColor="Navy"
                                />

                            <asp:BoundField HeaderText="Pending"
                                DataField="Pending"
                                ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="13%" 
                                ItemStyle-BorderColor="Navy"
                                />

                            <asp:BoundField HeaderText="Future Leaves"
                                DataField="Future"
                                ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="13%" 
                                ItemStyle-BorderColor="Navy"
                                />

                            <asp:BoundField HeaderText="Balance"
                                DataField="Balance"
                                ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="13%" 
                                ItemStyle-BorderColor="Navy"
                                />
                        </Columns>
                    </asp:GridView>

                </div>






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


                    <ul id="editform" runat="server" visible="false">

                        <li>
                            <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                        </li>
                        <li></li>
                        <li>
                            <span>Leave Type</span><br />
                            <asp:UpdatePanel ID="Upl_leavetype" runat="server" Visible="false">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlLeaveType" runat="server" AutoPostBack="True">
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlLeaveType" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <asp:TextBox ID="txtLeaveType" runat="server" Enabled="false" CssClass="Dropdown" OnTextChanged="txtLeaveType_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <%-- <i class="fa fa-caret-down" aria-hidden="true"></i>--%>
                            <asp:Panel ID="Panel4" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstLeaveType" runat="server" CssClass="taskparentclass2" AutoPostBack="true" OnSelectedIndexChanged="lstLeaveType_SelectedIndexChanged">
                                            <%-- <asp:ListItem>Privilege Leave</asp:ListItem>
                                            <asp:ListItem>Sick Leave</asp:ListItem>
                                            <asp:ListItem>Maternity Leave</asp:ListItem>
                                            <asp:ListItem>Leave Without Pay</asp:ListItem>
                                            <asp:ListItem>Time Off</asp:ListItem>--%>
                                        </asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender3" PopupControlID="Panel4" TargetControlID="txtLeaveType"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>
                        </li>
                        <li></li>

                        <li class="date">
                            <span>From Date</span><br />

                            <asp:TextBox ID="txtFromdate" runat="server" AutoPostBack="True" OnTextChanged="txtFromdate_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtFromdate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>

                            <%--  <asp:TextBox ID="txtsadate" runat="server" CssClass="txtdatepicker" placeholder="Start Date" OnTextChanged ="txtsadate_TextChanged" AutoPostBack ="true" ></asp:TextBox>
                                <span class="texticon tooltip" title="Select Start Date. Date should be MM/DD/YYYY format."><i class="fa fa-calendar" aria-hidden="true"></i></span>
                                <asp:Label ID="lblsdate" runat="server" Text="" Font-Size="15px"></asp:Label>--%>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="formerror" ControlToValidate="txtsadate" SetFocusOnError="True" ErrorMessage="Please enter start date" ValidationGroup="validate"></asp:RequiredFieldValidator>--%>

                        </li>

                        <li class="taskparentclass">
                            <span>For</span><br />

                            <asp:DropDownList ID="ddlFromFor" Visible="false" runat="server" OnSelectedIndexChanged="ddlFromFor_SelectedIndexChanged" AutoPostBack="True">
                                <asp:ListItem>Full Day</asp:ListItem>
                                <asp:ListItem>First Half</asp:ListItem>
                                <asp:ListItem>Second Half</asp:ListItem>
                            </asp:DropDownList>



                            <asp:TextBox ID="txtFromfor" runat="server" CssClass="Dropdown"></asp:TextBox>
                            <%--<i class="fa fa-caret-down" aria-hidden="true"></i>--%>
                            <asp:Panel ID="Panel2" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstFromfor" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstFromfor_SelectedIndexChanged">
                                            <asp:ListItem>First Half</asp:ListItem>
                                            <asp:ListItem>Second Half</asp:ListItem>
                                             <asp:ListItem Selected="True" Text="Full Day" Value="Full Day">Full Day</asp:ListItem>
                                        </asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender1" PopupControlID="Panel2" TargetControlID="txtFromfor"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>

                        </li>

                        <li class="date">
                            <span>To Date</span><br />
                            <asp:TextBox ID="txtToDate" runat="server" AutoPostBack="True" OnTextChanged="txtToDate_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>

                        <li class="fordate">
                            <span>For</span><br />
                            <asp:DropDownList ID="ddlToFor" Visible="false" runat="server" AutoPostBack="True" Height="16px">
                                <asp:ListItem>Full Day</asp:ListItem>
                                <asp:ListItem>First Half</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="txtToFor" runat="server" CssClass="Dropdown"></asp:TextBox>
                            <%--<i class="fa fa-caret-down" aria-hidden="true"></i>--%>
                            <asp:Panel ID="Panel3" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstTofor" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true" OnSelectedIndexChanged="lstToFor_SelectedIndexChanged">
                                            <asp:ListItem>First Half</asp:ListItem>
                                            <asp:ListItem Selected="True" Text="Full Day" Value="Full Day">Full Day</asp:ListItem>
                                        </asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender2" PopupControlID="Panel3" TargetControlID="txtToFor"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>
                        </li>

                        <li class="leavedays">
                            <span>Leave Days</span><br />
                            <asp:TextBox ID="txtLeaveDays" Enabled="false" runat="server" AutoPostBack="True" OnTextChanged="txtLeaveDays_TextChanged"></asp:TextBox>
                        </li>

                        <%--<li ></li>--%>
                        <%--                        <li> 
                            <span></span>
                            <br />
                            <br />
                        </li>--%>

                        <li class="Reason">
                            <span>Remarks </span>
                            <br />
                            <asp:TextBox ID="txtReason" runat="server" MaxLength="100"></asp:TextBox>
                        </li>

                        <li class="upload">
                            <%--<span>Upload File</span><br />--%>
                            <asp:FileUpload ID="uploadfile" Visible="true" runat="server" Enabled="false" />
                            <asp:TextBox ID="txtprofile" runat="server" CssClass="mobilenotextbox popup" Visible="false"> </asp:TextBox>
                            <%--<span class="texticonselect tooltip" title="Please provide your profile picture."><i class="fa fa-file-image-o"></i></span>--%>

                        </li>

                              <li class="Approver">
                            <%--   <span>Approver </span>--%>
                            <br /> 
                            <asp:ListBox ID="ListBox1" runat="server" Visible ="false" ></asp:ListBox>
                        </li>

                        <li class="Approver">
                            <%--   <span>Approver </span>--%>
                            <br />
                            <asp:ListBox Visible="false" ID="lstApprover" runat="server" ></asp:ListBox>
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
                                ItemStyle-BorderColor="Navy"
                                />

                            <asp:BoundField HeaderText="Status"
                                DataField="Status"
                                ItemStyle-HorizontalAlign="left"
                                ItemStyle-Width="12%" 
                                ItemStyle-BorderColor="Navy"
                                />

                            <asp:BoundField HeaderText="Approved on"
                                DataField="tdate"
                                ItemStyle-HorizontalAlign="left"
                                ItemStyle-Width="12%" 
                                ItemStyle-BorderColor="Navy"
                                />

                            <asp:BoundField HeaderText="Approver Remarks"
                                DataField="Comment"
                                ItemStyle-HorizontalAlign="left"
                                ItemStyle-Width="46%" 
                                ItemStyle-BorderColor="Navy"
                                />
                            
                            <asp:BoundField HeaderText="APPR_ID"
                                DataField="APPR_ID"
                                ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="2%" 
                                ItemStyle-BorderColor="Navy"
                                Visible="false"
                                />
                            
                            <asp:BoundField HeaderText="Emp_Name"
                                DataField="Emp_Name"
                                ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="1%" 
                                ItemStyle-BorderColor="Navy"
                                Visible="false"
                                />

                            <asp:BoundField HeaderText="Emp_Emailaddress"
                                DataField="Emp_Emailaddress"
                                ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="1%" 
                                ItemStyle-BorderColor="Navy"
                                Visible="false"
                                />

                            <asp:BoundField HeaderText="A_EMP_CODE"
                                DataField="A_EMP_CODE"
                                ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="1%" 
                                ItemStyle-BorderColor="Navy"
                                Visible="false"
                                />
                        </Columns>
                    </asp:GridView>
                        </li>

                        <li class="Approver">
                         <%--   <span>Approver </span>--%>
                            <br />
                            <asp:ListBox ID="ListBox3" visible="false" runat="server"></asp:ListBox>
                        </li>

                        <li class="inter" style="display:none;">
                           <%-- <span>For Information To </span>--%>
                            <br />
                            <asp:ListBox ID="lstIntermediate" visible="False" runat="server"></asp:ListBox>
                        </li>

                    </ul>
                </div>



              


            </div>
        </div>
    </div>
    <div>
        <asp:LinkButton ID="btnMod" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClick="btnSave_Click1">Modify</asp:LinkButton>
        <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel"  CssClass="Savebtnsve" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
        <asp:LinkButton ID="btnBack" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="btnBack_Click">Back</asp:LinkButton>
    </div>
    <asp:TextBox ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
    <asp:TextBox ID="TextBox1" runat="server" MaxLength="50" Visible="false" AutoPostBack="true" OnTextChanged="TextBox1_TextChanged"> </asp:TextBox>
    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />

    <asp:HiddenField ID="hflEmpName" runat="server" />

    <asp:HiddenField ID="hflEmpDesignation" runat="server" />

    <asp:HiddenField ID="hflEmpDepartment" runat="server" />

    <asp:HiddenField ID="hflEmailAddress" runat="server" />

    <asp:HiddenField ID="hflGrade" runat="server" />

    <asp:HiddenField ID="hflapprcode" runat="server" />

    <asp:HiddenField ID="hdlDate" runat="server" />

    <asp:HiddenField ID="hdnReqid" runat="server" />

    <asp:HiddenField ID="hdnleaveType" runat="server" />

     <asp:HiddenField ID="hflLeavestatus" runat="server" />

    <asp:HiddenField ID="hflstatusid" runat="server" />

    <asp:HiddenField ID="hflApproverEmail" runat="server" />

    <asp:HiddenField ID="hdnOldLeaveCount" runat="server" />
    <asp:HiddenField ID="hdnleaveid" runat="server" />
    
     <asp:HiddenField ID="hdnleavedayschk" runat="server" />
    <asp:HiddenField ID="hdnleaveconditiontypeid" runat="server" />
    <asp:HiddenField ID="hdnFromFor" runat="server" />
    
       <asp:HiddenField ID="hdnlstfromfor" runat="server" />
    <asp:HiddenField ID="hdnlsttofor" runat="server" />
    <asp:HiddenField ID="hdnToDate" runat="server" />
    <asp:HiddenField ID="hdnleavedays" runat="server" />
    <asp:HiddenField ID="htnleavetypeid" runat="server" />
    <asp:HiddenField ID="hdnaproverid" runat="server" />



    <script type="text/javascript">


        function validateLeaveType(leavetypeid) {

            document.getElementById("<%=txtToDate.ClientID%>").disabled = false;
             document.getElementById("<%=txtToFor.ClientID%>").disabled = false;
             document.getElementById("<%=txtToDate.ClientID%>").value = "";
             document.getElementById("<%=txtFromdate.ClientID%>").value = "";
             document.getElementById("<%=txtLeaveDays.ClientID%>").value = "";
             document.getElementById("<%=lblmessage.ClientID%>").value = "";
             document.getElementById("<%=txtToFor.ClientID%>").value = "Full Day";
             document.getElementById("<%=txtFromfor.ClientID%>").value = "Full Day";
             document.getElementById("<%=txtReason.ClientID%>").value = "";
             document.getElementById("<%=hdnlstfromfor.ClientID%>").value = "";
             document.getElementById("<%=hdnlsttofor.ClientID%>").value = "";
             document.getElementById("<%=hdnToDate.ClientID%>").value = "";
             document.getElementById("<%=hdnleavedays.ClientID%>").value = "";

             if (leavetypeid == "5") {
                 document.getElementById("<%=txtToDate.ClientID%>").disabled = true;
            }

            return;

        }

        function validateFromFor(leavetypeid, leavetypeFSH, tt, todate) {
            document.getElementById("<%=txtToDate.ClientID%>").disabled = false;
            if (leavetypeid == "5") {
                document.getElementById("<%=txtToDate.ClientID%>").disabled = true;
                document.getElementById("<%=txtToFor.ClientID%>").disabled = true;
            }
            else {
                if (leavetypeFSH == "First Half") {

                    document.getElementById("<%=txtToDate.ClientID%>").disabled = true;
                    document.getElementById("<%=txtToFor.ClientID%>").disabled = true;

                }
                else {
                    document.getElementById("<%=txtToDate.ClientID%>").disabled = false;
                    document.getElementById("<%=txtToFor.ClientID%>").disabled = false;

                }
            }
            if (todate != "") {
                document.getElementById("<%=txtToDate.ClientID%>").value = todate;
            }
            document.getElementById("<%=txtLeaveDays.ClientID%>").value = tt;
            return;
        }

        function validateToFor(tt) {
            document.getElementById("<%=txtLeaveDays.ClientID%>").value = tt;
            return;
        }


        function validateFromFor_old(leavetypeid, leavetypeFSH, tt) {



            document.getElementById("<%=txtToDate.ClientID%>").disabled = false;
             if (leavetypeid == "5") {
                 document.getElementById("<%=txtToDate.ClientID%>").disabled = true;
               document.getElementById("<%=txtToFor.ClientID%>").disabled = true;

               //document.getElementById("<%=txtToDate.ClientID%>").value = document.getElementById("<%=txtFromdate.ClientID%>").value;
           }
           else {

               if (leavetypeFSH == "First Half") {

                   document.getElementById("<%=txtToDate.ClientID%>").disabled = true;
                   document.getElementById("<%=txtToFor.ClientID%>").disabled = true;
                   //  document.getElementById("<%=txtToDate.ClientID%>").value = document.getElementById("<%=txtFromdate.ClientID%>").value;
               }
               else {
                   document.getElementById("<%=txtToDate.ClientID%>").disabled = false;
                   document.getElementById("<%=txtToFor.ClientID%>").disabled = false;

               }
           }


           document.getElementById("<%=txtLeaveDays.ClientID%>").value = tt;
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

    </script>
</asp:Content>

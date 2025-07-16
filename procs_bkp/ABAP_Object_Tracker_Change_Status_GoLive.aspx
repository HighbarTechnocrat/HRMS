<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" EnableSessionState="True"
    CodeFile="ABAP_Object_Tracker_Change_Status_GoLive.aspx.cs"
    Inherits="ABAP_Object_Tracker_Change_Status_GoLive" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <style>
        #MainContent_btnABAPPlanSubmit, #MainContent_updatergs, #MainContent_updatefs {
            background: #3D1956;
            color: #febf39 !important;
            font-size: medium;
        }

        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        #MainContent_lstApprover, #MainContent_lstIntermediate {
            padding: 0 0 33px 0 !important;
        }

        #MainContent_LinkBtnSavePopup, #MainContent_LinkBtnBackPopup, #MainContent_LinkButton1, #MainContent_LinkButton2 {
            background: #3D1956;
            color: #febf39 !important;
            padding: 8px 18px;
            margin: 26px !important;
        }

        .Dropdown {
            border-bottom: 2px solid #cccccc;
            background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;
            cursor: default;
        }

        div#left-content {
            max-width: 100% !important;
            max-height: 100% !important;
        }

        .blue-background {
            background-color: #ADD8E6;
            color: #000000;
        }

        .green-background {
            background-color: #90EE90;
            color: #000000;
        }

        #page-loader {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(255, 255, 255, 0.9);
            z-index: 9999;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .loader {
            border: 10px solid #f3f3f3;
            border-top: 10px solid #3D1956;
            border-radius: 50%;
            width: 40px;
            height: 40px;
            animation: spin 1.5s linear infinite;
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }

        select#MainContent_AllStatus {
            background: white !important;
        }

        #content-container .intern-padding {
            padding: 0px 0px !important;
            cursor: auto !important;
        }


        .testABAP {
            position: fixed !important;
            top: 13% !important;
            left: 7% !important;
            background-color: white;
            border: 1px solid rgb(204, 204, 204);
            width: 85% !important;
            overflow: scroll;
            height: 75% !important;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-loader">
        <div class="loader"></div>
    </div>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>

    <script src="../js/freeze/jquery-1.11.0.min.js"></script>
    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Go-Live - Update Status"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;margin:0px 0px 0px 10px !important;"></asp:Label>
                </div>
                <span style="float: right;">
                    <%--<asp:LinkButton ID="LinkButton1" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/ABAP_Object_Tracker_Index.aspx" BackColor="#3D1956">Back</asp:LinkButton>--%>
                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="Savebtnsve" PostBackUrl="~/procs/ABAP_Object_Tracker_Index.aspx" BackColor="#3D1956" style="margin: 0px 20px 0px 0px !important;"><%=System.Configuration.ConfigurationManager.AppSettings["ABAPObjectPageTitle"]%></asp:LinkButton>
                </span>

                <asp:LinkButton ID="localtrvl_delete_btn" runat="server"
                    CssClass="Savebtnsve" Style="display: none"></asp:LinkButton>

                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderIRSheet" runat="server"
                    BackgroundCssClass="modalBackground" TargetControlID="localtrvl_delete_btn" PopupControlID="PnlIrSheet" RepositionMode="RepositionOnWindowResizeAndScroll"
                    OnOkScript="ok()" CancelControlID="LinkBtnBackPopup" />

                <%--<asp:Panel ID="PnlIrSheet" runat="server" Style="display: none; position: fixed; top: 20%; left: 20%; transform: translate(-20%, -30%,-30%); width: 80%; padding: 20px; background-color: white; border: 1px solid #ccc; z-index: 1000;" Height="500px">--%>
                <asp:Panel ID="PnlIrSheet" runat="server" CssClass="testABAP">
                    <div runat="server" id="DivIDPopup" visible="false" style="max-height: 500px; overflow: auto;">
                        <span style="float: right;">
                            <asp:LinkButton ID="LinkButton2" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="LinkBtnBackPopup_Click" BackColor="#3D1956">Back</asp:LinkButton>
                        </span>
                        <div class="edit-contact">
                            <div style="padding-left: 200px">
                                <asp:Label runat="server" ID="lblmessagesub" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                            </div>
                            <br />
                            <br />
                            <ul runat="server" id="editform1">

                                <li class="trvl_date">
                                    <span>Development Description</span>
                                    <br />
                                    <asp:TextBox ID="Txt_DevelopmentDescription" Enabled="false" runat="server" Width="860px"></asp:TextBox>
                                </li>
                                <li></li>
                                <li></li>

                                <li class="trvl_date">
                                    <span>Module</span>
                                    <br />
                                    <asp:TextBox ID="Txt_Module" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                                </li>
                                <li>
                                    <span>Interface</span>
                                    <br />
                                    <asp:TextBox ID="Txt_Interface" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                                </li>
                                <li>
                                    <span>FRICE Category</span>
                                    <br />
                                    <asp:TextBox ID="Txt_FRICECategory" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                                </li>

                                <li class="trvl_date">
                                    <span>Priority / Order</span>
                                    <br />
                                    <asp:TextBox ID="Txt_Order" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span>Priority</span>
                                    <br />
                                    <asp:TextBox ID="Txt_Priority" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                                </li>
                                <li>
                                    <span>Complexity</span>
                                    <br />
                                    <asp:TextBox ID="Txt_Complexity" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                                </li>

                                <li class="trvl_date">
                                    <asp:Label ID="Label1" runat="server" Text="Planned Date"></asp:Label>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="Txt_PlannedDate" runat="server" Width="200px" MaxLength="15" Enabled="false"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <asp:Label ID="label2" runat="server" Text="Revised Date"></asp:Label>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txt_RevisedDate" runat="server" Width="200px" MaxLength="15" oncopy="return false" onpaste="return false"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" Format="dd/MM/yyyy" TargetControlID="txt_RevisedDate" runat="server">
                                    </ajaxToolkit:CalendarExtender>
                                </li>
                                <li>

                                    <asp:Label ID="Label3" runat="server" Text="Actual Date"></asp:Label>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="Txt_ActualDate" Width="200px" runat="server"
                                        MaxLength="15" oncopy="return false" onpaste="return false"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender6" Format="dd/MM/yyyy" TargetControlID="Txt_ActualDate" runat="server">
                                    </ajaxToolkit:CalendarExtender>

                                </li>

                                <li class="trvl_date" runat="server" id="UATDisplay1">
                                    <asp:Label ID="Label4" runat="server" Text="Final Script Sub_Date"></asp:Label>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="Txt_Final_Script_Sub_Date" Width="200px" runat="server" MaxLength="15"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender7" Format="dd/MM/yyyy" TargetControlID="Txt_Final_Script_Sub_Date" runat="server">
                                    </ajaxToolkit:CalendarExtender>

                                </li>
                                <li class="trvl_date" runat="server" id="UATDisplay2"></li>
                                <li runat="server" id="UATDisplay3"></li>

                                <li class="trvl_date">
                                    <asp:Label ID="lbl5" runat="server" Text="UAT Status"></asp:Label>
                                    <br />
                                    <asp:DropDownList runat="server" ID="AllStatus" Width="250px" OnSelectedIndexChanged="AllStatus_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </li>
                                <li></li>
                                <li></li>

                                <li>
                                    <div class="trvl_date" runat="server" id="divchkShowFields" visible="false">
                                        <br />
                                        <asp:CheckBox ID="chkShowFields" runat="server" Text="Is Reusable" OnCheckedChanged="chkShowFields_CheckedChanged" AutoPostBack="true" />
                                    </div>
                                </li>
                                <li></li>
                                <li></li>

                                <li class="trvl_date" runat="server" id="divReusableClientName" visible="false">
                                    <br />
                                    <span>Reusable Client Name</span>
                                    <br />
                                    <asp:TextBox ID="txtReusableClientName" runat="server" />
                                </li>
                                <li class="trvl_date" runat="server" id="divReusableRemark" visible="false">

                                    <span>Reusable Remark</span>
                                    <br />
                                    <asp:TextBox ID="txtReusableRemark" runat="server" />

                                </li>
                                <li class="trvl_date" runat="server" id="divReusableAdditonalEffrot" visible="false">
                                    <span>Additional Efforts</span>
                                    <br />
                                    <asp:TextBox ID="txtReusableAdditonalEffrot" runat="server" />
                                </li>

                                <li class="trvl_date">
                                    <span>Remarks</span>
                                    <asp:TextBox ID="Txt_Remarks" runat="server" Height="40px" Width="460px" TextMode="MultiLine"></asp:TextBox>
                                </li>

                                <li></li>
                                <li id="PlnEmptyDate" runat="server"></li>

                                <li class="trvl_date">
                                    <asp:LinkButton ID="LinkBtnSavePopup" runat="server" Text="Submit" ToolTip="Submit" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();" OnClick="LinkBtnSavePopup_Click">Submit</asp:LinkButton>
                                    <asp:LinkButton ID="LinkBtnBackPopup" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="LinkBtnBackPopup_Click">Back</asp:LinkButton>
                                </li>
                                <li></li>
                            </ul>
                        </div>
                    </div>
                </asp:Panel>



                <div class="edit-contact">

                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" Visible="false"></asp:LinkButton>
                        </div>
                    </div>

                    <ul id="editform2" runat="server">
                        <li class="trvl_date">
                            <span>Project / Location</span><br />
                            <asp:DropDownList runat="server" ID="DDLProjectLocation" AutoPostBack="True" CssClass="DropdownListSearch" OnSelectedIndexChanged="DDLProjectLocation_SelectedIndexChanged">
                            </asp:DropDownList>
                        </li>
                        <li class="trvl_date" id="liProjectMangaer" runat="server" visible="false">
                            <span>Project Manager</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPRM" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li></li>

                        <li class="trvl_date">
                            <asp:Label runat="server" ID="lbl_FSConsultant_Error" Visible="True" Style="color: red; text-align: center;"></asp:Label>
                            <br />
                        </li>
                        <li></li>
                        <li class="trvl_grid" id="lifsdetails" runat="server">
                            <br />
                            <asp:Label runat="server" ID="lbl_error" Visible="True" Style="color: red; text-align: center;"></asp:Label>
                            <br />
                            <span class="fs" runat="server" visible="true" id="golive">GO Live</span>
                            <asp:GridView ID="gvGoliveDetails" runat="server" BackColor="White" BorderColor="Navy"
                                BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                                Width="100%" EditRowStyle-Wrap="true" DataKeyNames="ABAPGoLiveId" ShowHeaderWhenEmpty="True">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                <PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" />
                                <RowStyle ForeColor="#000066" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                                <Columns>

                                    <asp:TemplateField ItemStyle-Width="6%">
                                        <HeaderTemplate>
                                            View
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkEdit" runat="server" Height="15px" OnClick="lnkEdit_Click1" OnClientClick="showPopup();" ImageUrl="~/Images/edit.png" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField HeaderText="Development Description"
                                        DataField="Development_Desc"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="20%" HeaderStyle-Width="20%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="GO Live Status"
                                        DataField="GOLStatus"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%" HeaderStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Module"
                                        DataField="ModuleDesc"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Interface"
                                        DataField="Interface"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Scope"
                                        DataField="Scope"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="7%" HeaderStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="FRICE Category"
                                        DataField="FCategoryName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="6%" HeaderStyle-Width="6%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Priority/ Order"
                                        DataField="Priority"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="4%" HeaderStyle-Width="4%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Priority"
                                        DataField="PriorityName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="7%" HeaderStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Complexity"
                                        DataField="ComplexityName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="7%" HeaderStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Date"
                                        DataField="PlannedDate"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%" HeaderStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Revised Date"
                                        DataField="ABAPRevisedDate"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%" HeaderStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Actual Date"
                                        DataField="ActualDate"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%" HeaderStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />



                                </Columns>
                                <EmptyDataTemplate>No Record Available</EmptyDataTemplate>

                            </asp:GridView>
                            <br />

                        </li>
                        <li class="trvl_date">
                            <asp:LinkButton ID="trvl_btnSave" runat="server" Visible="false" Text="Submit" ToolTip="Submit" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();">Submit</asp:LinkButton>
                            <asp:LinkButton ID="btnback_mng" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/ABAP_Object_Tracker_Index.aspx">Back</asp:LinkButton>
                        </li>

                        <li></li>
                    </ul>

                </div>

            </div>
        </div>
    </div>


    <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
    <asp:TextBox AutoComplete="off" ID="txtIsSecurity_DepositInvoice" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
    <asp:TextBox AutoComplete="off" ID="txtEmpName" runat="server" Visible="false"></asp:TextBox>
    <asp:Label ID="testsanjay" runat="server" Visible="false"></asp:Label>
    <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />

    <asp:HiddenField ID="HDID" runat="server" />
    <asp:HiddenField ID="HDStatus" runat="server" />
    <asp:HiddenField ID="HDStatusCheck" runat="server" />
    <asp:HiddenField ID="HDStatusCheckFlag" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="FilePath" runat="server" />
    <asp:HiddenField ID="HDProjectLocation" runat="server" />

    <asp:HiddenField ID="hdnprojectManager" runat="server" />
    <asp:HiddenField ID="hdnprojectManagerEmail" runat="server" />
    <asp:HiddenField ID="hdnprogramnager" runat="server" />
    <asp:HiddenField ID="hdnprogramnagerEmail" runat="server" />
    <asp:HiddenField ID="hdnDeliveryHeadMail" runat="server" />


    <asp:HiddenField ID="hdnABAPperEmail" runat="server" />
    <asp:HiddenField ID="hdnABAPperName" runat="server" />
    <asp:HiddenField ID="hdnFSConsultantEmail" runat="server" />
    <asp:HiddenField ID="hdnFSConsultantName" runat="server" />
    <asp:HiddenField ID="hdnHBTConsultantEmail" runat="server" />
    <asp:HiddenField ID="hdnHBTConsultantName" runat="server" />
    <asp:HiddenField ID="hdnCTMConsultantEmail" runat="server" />
    <asp:HiddenField ID="hdnCTMConsultantName" runat="server" />

    <asp:HiddenField ID="HDDateCheck1" runat="server" />
    <asp:HiddenField ID="hdnRevisedDate" runat="server" />
    <asp:HiddenField ID="HdnActualDate" runat="server" />
    <asp:HiddenField ID="HDDateCheck4" runat="server" />
    <asp:HiddenField ID="HDRemarkcheck" runat="server" />

    <asp:HiddenField ID="HDDateCheckCTMTraingStart" runat="server" />
    <asp:HiddenField ID="HDDateCheckCTMTraingEND" runat="server" />

    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <script src="../js/freeze/jquery-ui.min.js"></script>
    <script src="../js/freeze/gridviewScroll.min.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {
            function applyGridViewScroll(selector, options) {
                $(selector).gridviewScroll(options);
            }
            $("#MainContent_DDLProjectLocation").select2();
            $(".DropdownListSearch").select2();
            const gridOptions = {
                width: 1070,
                height: 300,
                freezesize: 3,
                headerrowcount: 1
            };

            applyGridViewScroll('#MainContent_gvGoliveDetails', gridOptions);

        });

        document.onreadystatechange = function () {
            if (document.readyState === "complete") {
                document.getElementById("page-loader").style.display = "none";
            }
        };

        // For async postbacks
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(function () {
            document.getElementById("page-loader").style.display = "flex";
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            document.getElementById("page-loader").style.display = "none";
        });

        function SaveMultiClick() {
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

            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return;

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

        function DownloadFile(file) {
            // alert(file);
            var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;

            //alert(localFilePath);
            //window.open("https://ess.highbartech.com/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
            //window.open("https://192.168.21.193/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
            window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
        }

    </script>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="ABAP_Object_Tracker_Change_Status_ABAP_Timesheet.aspx.cs"
    Inherits="ABAP_Object_Tracker_Change_Status_ABAP_Timesheet" EnableSessionState="True" %>

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

        #MainContent_LinkBtnSavePopup, #MainContent_LinkBtnBackPopup, #MainContent_LinkButton1, #MainContent_LinkButton2, #MainContent_btn_SumitTimesheet, #MainContent_LinkButton3, #MainContent_LinkButton45 {
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

        input#MainContent_uplCTMTestCases {
            width: 200px;
        }

        input#MainContent_uplUATSingOff {
            width: 200px;
        }

        div#left-content {
            max-width: 100% !important;
            max-height: 100% !important;
        }

        /*.modalBackground {
    background-color: rgba(0, 0, 0, 0.5);
     
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    z-index: 1000;
}*/

        .testABAP {
            position: fixed !important;
            top: 13% !important;
            left: 16% !important;
            background-color: white;
            border: 1px solid rgb(204, 204, 204);
            width: 75% !important;
            overflow: scroll;
            height: 75% !important;
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
                    <asp:Label ID="lblheading" runat="server" Text="Submit Timesheet"></asp:Label>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblTimesheetErrorMsg" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center; margin: 0 0 0 10px !important;"></asp:Label>
                </div>
                <span style="float: right;">
                    <asp:LinkButton ID="LinkButton1" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/ABAP_Object_Tracker_Change_Status_ABAP.aspx" BackColor="#3D1956">Back</asp:LinkButton>
                </span>

                <asp:LinkButton ID="localtrvl_delete_btn" runat="server" CssClass="Savebtnsve" Style="display: none"></asp:LinkButton>
                <asp:LinkButton ID="LinkButton6" runat="server" CssClass="Savebtnsve" Style="display: none"></asp:LinkButton>
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
                        <li class="trvl_grid" id="lifsdetails" runat="server">
                            <asp:Label runat="server" ID="lbl_FSConsultant_Error" Visible="True" Style="color: red; text-align: center;"></asp:Label>
                            <asp:Label runat="server" ID="lblNote" Visible="True" Style="color: red; font-size: 14px; font-weight: 500; text-align: center;"> Note: Please enter timesheet hours(12 Hrs - HH:MM)</asp:Label>
                            <br />
                            <br />
                            <span class="ABAP" runat="server" visible="false" id="ABAP">ABAP Object Details</span>
                            <asp:GridView ID="gvTimesheet" runat="server" CssClass="timsheet" DataKeyNames="ABAPDetailsId" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                                CellPadding="3" AutoGenerateColumns="False" Width="100%" Height="30%" ShowHeaderWhenEmpty="True" OnRowDataBound="gvTimesheet_RowDataBound">
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
                                    <asp:BoundField HeaderText="Sr No"
                                        DataField="SrNo"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="20%" HeaderStyle-Width="20%"
                                        ItemStyle-BorderColor="Navy" Visible="false" />

                                    <asp:BoundField HeaderText="ABAP Details ID"
                                        DataField="ABAPDetailsId"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="20%" HeaderStyle-Width="20%"
                                        ItemStyle-BorderColor="Navy" Visible="false" />

                                    <asp:BoundField HeaderText="Development Description"
                                        DataField="Development_Desc"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="30%" HeaderStyle-Width="30%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:TemplateField HeaderText="Editable Name" ItemStyle-Width="8%" HeaderStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtfirst" runat="server" Width="70%" Style="margin: 0 0 0 8px !important; padding: 5px !important;" />
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Editable Name" ItemStyle-Width="8%" HeaderStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSecond" runat="server" Width="70%" Style="margin: 0 0 0 8px !important; padding: 5px !important;" />
                                        </ItemTemplate>

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Editable Name" ItemStyle-Width="8%" HeaderStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtThird" runat="server" Width="70%" Style="margin: 0 0 0 8px !important; padding: 5px !important;" />
                                        </ItemTemplate>

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Editable Name" ItemStyle-Width="8%" HeaderStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtFourth" runat="server" Width="70%" Style="margin: 0 0 0 8px !important; padding: 5px !important;" />
                                        </ItemTemplate>

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Editable Name" ItemStyle-Width="8%" HeaderStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtFifth" runat="server" Width="70%" Style="margin: 0 0 0 8px !important; padding: 5px !important;" />
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Editable Name" ItemStyle-Width="8%" HeaderStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSixth" runat="server" Width="70%" Style="margin: 0 0 0 8px !important; padding: 5px !important;" />
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Editable Name" ItemStyle-Width="8%" HeaderStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSeventh" runat="server" Width="70%" Style="margin: 0 0 0 8px !important; padding: 5px !important;" />
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </li>
                        <li style="width: 50%;">
                            <asp:LinkButton ID="btn_SumitTimesheet" runat="server" Text="Submit Timesheet" ToolTip="Submit Timesheet" BackColor="#3D1956" OnClick="btnSubmit_Click1">Submit Timesheet</asp:LinkButton>
                            <%--<asp:LinkButton ID="LinkButton2" runat="server" CssClass="Savebtnsve" Text="Submit" OnClick="btnSubmit_Click1" OnClientClick="return SaveOutClick();">Submit</asp:LinkButton>--%>

                            <asp:LinkButton ID="btnback_mng" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/ABAP_Object_Tracker_Change_Status_ABAP.aspx">Back</asp:LinkButton>
                        </li>

                        <li></li>
                    </ul>

                </div>

            </div>
        </div>
    </div>

    <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>

    <br />

    <asp:HiddenField ID="HDID" runat="server" />
    <asp:HiddenField ID="HDStatus" runat="server" />
    <asp:HiddenField ID="HDStatusCheck" runat="server" />
    <asp:HiddenField ID="HDStatusCheckFlag" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="FilePath" runat="server" />
    <asp:HiddenField ID="HDProjectLocation" runat="server" />
    <asp:HiddenField ID="hdnABAPDetailsId" runat="server" />
    <asp:HiddenField ID="hdnCommaSeperABAPDetailsId" runat="server" />

    <asp:HiddenField ID="HDDateCheck1" runat="server" />
    <asp:HiddenField ID="HDDateCheck2" runat="server" />
    <asp:HiddenField ID="HDDateCheck3" runat="server" />
    <asp:HiddenField ID="HDDateCheck4" runat="server" />
    <asp:HiddenField ID="HDRemarkcheck" runat="server" />

    <asp:HiddenField ID="hdnStartDate" runat="server" />
    <asp:HiddenField ID="hdnEndDate" runat="server" />


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


        function validateTimesheet(textboxId) {
            var txtBox = document.getElementById(textboxId);
            var value = txtBox.value.trim();
            var timePattern = /^([0-1]?[0-9]|2[0-4]):([0-5][0-9])$/;
            var label = document.getElementById('<%= lblTimesheetErrorMsg.ClientID %>');
            if (!timePattern.test(value)) {
                if (value != "") {
                    txtBox.style.backgroundColor = "#FF0000";
                    label.innerText = "Please enter a valid hours in the format HH:MM.";
                }
                return false;
            }
            else {
                label.innerText = "";
                txtBox.style.backgroundColor = "";
                return true;
            }
        }


        function restrictInput(event) {
            const inputValue = event.target.value;
            const filteredValue = inputValue.replace(/[^0-9:]/g, "");

            if (filteredValue.length > 5) {
                event.target.value = filteredValue.slice(0, 5);
            } else {
                event.target.value = filteredValue;
            }
        }

        window.addEventListener('DOMContentLoaded', () => {

            const textboxes = document.querySelectorAll('input[type="text"]');
            textboxes.forEach((textbox) => {
                textbox.addEventListener('input', restrictInput);
            });
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



    </script>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="ABAP_Object_Tracker_All_List.aspx.cs" Inherits="ABAP_Object_Tracker_All_List" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>









<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <style>
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

        .modalBackground {
            background-color: rgba(0, 0, 0, 0.5);
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            z-index: 1000;
        }

        #MainContent_LinkBtnSavePopup, #MainContent_LinkBtnBackPopup, #MainContent_LinkButton1, #MainContent_LinkButton2, #MainContent_btn_SumitTimesheet, #MainContent_LinkButton3 {
            background: #3D1956;
            color: #febf39 !important;
            padding: 8px 18px;
            margin: 26px !important;
        }

        #MainContent_lnk_Index,
        #MainContent_lnk_Index {
            float: right;
            background: #3D1956;
            color: #febf39 !important;
            padding: 0.5% 1.4%;
        }

        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
            background: #dae1ed;
        }

        .paging a {
            background-color: #C7D3D4;
            padding: 5px 7px;
            text-decoration: none;
            border: 1px solid #C7D3D4;
        }

            .paging a:hover {
                background-color: #E1FFEF;
                color: #00C157;
                border: 1px solid #C7D3D4;
            }

        .paging span {
            background-color: #E1FFEF;
            padding: 5px 7px;
            color: #00C157;
            border: 1px solid #C7D3D4;
        }

        tr.paging {
            background: none !important;
        }

            tr.paging tr {
                background: none !important;
            }

            tr.paging td {
                border: none;
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

        .clscommon th {
            font-weight: bold !important;
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
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">

                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Approved Plans"></asp:Label>
                    </span>
                </div>

                <%--<asp:LinkButton ID="lnk_Index" runat="server" Visible="true" PostBackUrl="~/procs/ABAP_Object_Tracker_Index.aspx">Back</asp:LinkButton>--%>
                <asp:LinkButton ID="lnk_Index" runat="server" CssClass="Savebtnsve" PostBackUrl="~/procs/ABAP_Object_Tracker_Index.aspx" BackColor="#3D1956"><%=System.Configuration.ConfigurationManager.AppSettings["ABAPObjectPageTitle"]%></asp:LinkButton>
                <br />
                <span>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                </span>

                <asp:Label runat="server" ID="RecordCount" Style="color: red; font-size: 14px;"></asp:Label>
                <asp:GridView ID="gvABAPObjectPlanList" CssClass="clscommon" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                    DataKeyNames="ABAPODUploadId" OnRowDataBound="GridView_RowDataBound"
                    CellPadding="3" AutoGenerateColumns="False" EditRowStyle-Wrap="false" PageSize="15" AllowPaging="True">
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                    <PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                    <PagerStyle HorizontalAlign="Right" CssClass="paging" />
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="5%" HeaderText="View" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:ImageButton ID="lnkABAPPlanDetails" ToolTip="View" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkABAPPlanDetails_Click" />

                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:BoundField HeaderText="Project/Location Code"
                            DataField="ProjectLocation"
                            ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-BorderColor="Navy" ItemStyle-Width="18%" />

                        <asp:BoundField HeaderText="Program Manager"
                            DataField="Approver"
                            ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-BorderColor="Navy" ItemStyle-Width="28%" />

                        <asp:BoundField HeaderText="Project Manager"
                            DataField="ProjectManager"
                            ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-BorderColor="Navy" ItemStyle-Width="28%" />

                        <asp:BoundField HeaderText="Status"
                            DataField="PlanStatus"
                            ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-BorderColor="Navy" ItemStyle-Width="12%" />

                        <asp:TemplateField >
                            <HeaderTemplate>New Object</HeaderTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="lnkEdit" runat="server" Height="15px" OnClick="lnkEdit_Click"
                                    ImageUrl="~/Images/edit.png" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="4%" />
                            <HeaderStyle Width="6%" />
                        </asp:TemplateField>

                         
                    </Columns>
                </asp:GridView>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />


                <div class="edit-contact">

                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" OnClick="lnkcont_Click"></asp:LinkButton>
                        </div>
                    </div>

                </div>
                <asp:LinkButton ID="localtrvl_delete_btn" runat="server" CssClass="Savebtnsve" Style="display: none"></asp:LinkButton>

                <asp:HiddenField ID="HDID" runat="server" />

                <asp:HiddenField ID="hdnABAPODId" runat="server" />
                <asp:HiddenField ID="hdnEmpCode" runat="server" />

            </div>
        </div>
    </div>

    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />


    <script src="../js/freeze/jquery-ui.min.js"></script>
    <script src="../js/freeze/gridviewScroll.min.js"></script>

    <script type="text/javascript">



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

        $(document).ready(function () {
            function applyGridViewScroll(selector, options) {
                $(selector).gridviewScroll(options);
            }
            $("#MainContent_DDLProjectLocation").select2();
            $(".DropdownListSearch").select2();
            const gridOptions = {
                width: 1070,
                height: 400,
                freezesize: 3,
                headerrowcount: 1
            };

            applyGridViewScroll('#MainContent_gvRGSDetails', gridOptions);
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


        function isValidInput(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((charCode >= 48 && charCode <= 57) || charCode == 58) {
                return true;
            }
            else {
                return false;
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

    </script>
</asp:Content>


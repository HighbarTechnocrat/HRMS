<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" EnableViewState="true"
    MaintainScrollPositionOnPostback="true" CodeFile="Invoicecollection_projectlocation.aspx.cs" Inherits="Invoicecollection_projectlocation" EnableSessionState="True" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
            /*background: #dae1ed;*/
            background: #ebebe4;
        }

        .taskparentclasskkk {
            width: 29.5%;
            height: 72px;
            /*overflow:initial;*/
        }

        #MainContent_lstApprover, #MainContent_lstIntermediate {
            padding: 0 0 33px 0 !important;
            /*overflow: unset;*/
        }

        .Dropdown {
            border-bottom: 2px solid #cccccc;
            /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
            background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;
            cursor: default;
        }

        .LableName {
            color: #F28820;
            font-size: 16px;
            font-weight: normal;
            text-align: left;
        }

        .textboxBackColor {
            background-color: cadetblue;
            color: aliceblue;
        }

        .BtnShow {
            color: blue !important;
            background-color: transparent;
            text-decoration: none;
            font-size: 13px !important;
        }

            .BtnShow:visited {
                color: blue !important;
                background-color: transparent;
                text-decoration: none;
            }

            .BtnShow:hover {
                color: red !important;
                background-color: transparent;
                text-decoration: none !important;
            }

        .POWOContentTextArea {
            width: 750px !important;
            height: 400px !important;
            overflow: auto;
        }

        .InvoiceRemarksText {
            width: 755px;
            height: 100px;
            overflow: auto;
        }

        .InvoiceMilestonegrid {
            margin: -24px 0 0 0 !important;
        }

        .txtWidth {
            padding: 2px !important;
            width: 100px !important;
            height: 21px !important;
            text-align: right !important;
        }

        a#MainContent_trvldeatils_btnSave {
            margin: 3px 0 6px 0px !important;
        }

        .AmtTextAlign {
            text-align: right
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
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>

    <script src="../js/freeze/jquery-1.11.0.min.js"></script>

    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Project & Location Mapping for Invoice Collection Report"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>


                <div class="edit-contact">

                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" Visible="false"></asp:LinkButton>
                        </div>
                    </div>

                    <ul id="editform" runat="server" visible="true">
                        <li class="trvl_type">
                            <span>Project Name</span><br />
                            <asp:TextBox ID="txtProjectName" MaxLength="150" runat="server"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>Location Code</span><br />
                            <asp:DropDownList runat="server" ID="lstLocationCode" AutoPostBack="true" CssClass="DropdownListSearch" OnSelectedIndexChanged="lstLocationCode_SelectedIndexChanged">
                            </asp:DropDownList>
                        </li>
                        <li class="trvl_date"></li>

                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>


                        <li class="trvl_date">
                            <div class="trvl_Savebtndiv">
                                <span>
                                    <asp:LinkButton ID="trvl_btnSave" runat="server" Text="Submit" ToolTip="Submit" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();" OnClick="trvl_btnSave_Click">Submit</asp:LinkButton>
                                </span>
                                <span>
                                    <asp:LinkButton Visible="false" ID="btnCancel" runat="server" Text="Reject" ToolTip="Reject" CssClass="Savebtnsve" OnClientClick="return CancelMultiClick();">Reject</asp:LinkButton>
                                </span>
                                <span>
                                    <asp:LinkButton Visible="false" ID="btnCorrection" runat="server" Text="Correction" ToolTip="" CssClass="Savebtnsve" OnClientClick="return SendforCorrectionMultiClick();">Send for Correction</asp:LinkButton>
                                </span>

                            </div>
                        </li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>


                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>

                        <li class="Approver">

                            <asp:GridView ID="Dvproject" runat="server" BackColor="White" BorderColor="Navy" PageSize="15" AllowPaging="True" OnPageIndexChanging="DgvApprover_PageIndexChanging" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="141%" CssClass="paging"
                                DataKeyNames="Location_No">
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
                                    <asp:BoundField HeaderText="Project Name"
                                        DataField="Project_Name"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="20%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Location Code"
                                        DataField="Location_Code"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="20%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkEdit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkEdit_Click" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>

                        </li>
                    </ul>
                    <br />
                    <br />
                    <br />
                    <br />
                </div>
            </div>
        </div>
    </div>

    <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
    <asp:TextBox AutoComplete="off" ID="txtEmpName" runat="server" Visible="false"></asp:TextBox>


    <asp:HiddenField ID="hflEmpName" runat="server" />
    <asp:HiddenField ID="hflEmpDesignation" runat="server" />
    <asp:HiddenField ID="hflEmpDepartment" runat="server" />
    <asp:HiddenField ID="hflEmailAddress" runat="server" />
    <asp:HiddenField ID="hflGrade" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="hdnInvoiceId" runat="server" />
    <asp:HiddenField ID="hdnReqid" runat="server" />
    <asp:HiddenField ID="hflEmpCode" runat="server" />


    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <script src="../js/freeze/jquery-ui.min.js"></script>
    <script src="../js/freeze/gridviewScroll.min.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {
            $(".DropdownListSearch").select2();
        });


        function SaveMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=trvl_btnSave.ClientID%>');

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

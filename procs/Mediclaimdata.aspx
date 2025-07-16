<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="Mediclaimdata.aspx.cs" Inherits="Mediclaimdata" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
            background: #dae1ed;
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
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
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
                        <asp:Label ID="lblheading" runat="server" Text="Mediclaim Nomination Data"></asp:Label>
                    </span>
                </div>

                <span>
                    <a href="vscb_index.aspx" class="aaaa">My Corner</a>
                </span>

                <span>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                </span>

                <div class="edit-contact">
                    <ul id="editform" runat="server">

                        <li style="padding-top: 30px">
                            <span>Employee</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" ID="lstEmployees" CssClass="DropdownListSearch"></asp:DropDownList>
                        </li>
                        <li style="padding-top: 30px">
                            <span>Nominee</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstNominees"></asp:DropDownList>
                        </li>
                        <li style="padding-top: 30px">
                            <span>Type</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" ID="lstMediclaimStatus" CssClass="DropdownListSearch"></asp:DropDownList>
                        </li>

                    </ul>
                </div>


                <div class="mobile_Savebtndiv" style="margin-top: 20px !important">
                    <asp:LinkButton ID="mobile_btnSave" runat="server" Text="Submit" ToolTip="Search" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click">Search </asp:LinkButton>
                    <asp:LinkButton ID="mobile_btnBack" runat="server" Text="Clear Search" ToolTip="Clear Search" class="trvl_Savebtndiv" OnClick="mobile_btnBack_Click">Clear</asp:LinkButton>
                </div>



                <asp:Label runat="server" ID="RecordCount" Style="color: red; font-size: 14px;"></asp:Label>


                <div class="edit-contact" style="display: none">
                    <ul id="Ul1" runat="server">

                        <li style="padding-top: 30px">
                            <span>Select Policy No</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" ID="lstPolicyNo" CssClass="DropdownListSearch"></asp:DropDownList>
                        </li>
                        <li style="padding-top: 30px">
                            <span>Email (Send employee mediclaim data to insurance company</span>&nbsp;&nbsp;
                             <br />
                            <asp:TextBox AutoComplete="off" ID="txtEmail" Style="padding: 3px 0px 5px 3px !important; height: 20px !important;" runat="server" MaxLength="200" Width="200px"></asp:TextBox></td>
                        </li>
                        <li style="padding-top: 30px"></li>

                    </ul>
                </div>

                <asp:GridView ID="gvproject" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                    DataKeyNames="Emp_Code" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false" PageSize="20" AllowPaging="False">
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

                        <asp:TemplateField HeaderText="Select ALL" ItemStyle-Width="5%">
                            <HeaderTemplate>
                                <span>Select</span><br />
                                <asp:CheckBox ID="chkAll" ToolTip="Select ALL" runat="server" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK_clearancefromSubmitted" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:BoundField HeaderText="Employee Code"
                            DataField="Emp_Code"
                            ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-Width="6%" ItemStyle-BorderColor="Navy" />

                        <asp:BoundField HeaderText="Employee DOJ (dd/mm/yyyy)"
                            DataField="emp_doj"
                            ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />

                        <asp:BoundField HeaderText="Employee Status"
                            DataField="emp_status"
                            ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />

                        <asp:BoundField HeaderText="Member Name"
                            DataField="MemberName"
                            ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                        <asp:BoundField HeaderText="Member Relation"
                            DataField="Member_Rel"
                            ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />

                        <asp:BoundField HeaderText="Gender"
                            DataField="Member_Sex"
                            ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />

                        <asp:BoundField HeaderText="Birth Date (dd/mm/yyyy)"
                            DataField="BirthDate"
                            ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />

                        <asp:BoundField HeaderText="Age"
                            DataField="Age"
                            ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />

                        <asp:BoundField HeaderText="Type"
                            DataField="medical_type"
                            ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />

                    </Columns>
                </asp:GridView>



                <br />
                <br />
                <br />
                

                <div class="fuel_Savebtndiv">
                    <asp:LinkButton ID="mobile_cancel" runat="server" Text="Submit" ToolTip="Submit" CssClass="Savebtnsve" OnClick="fuel_btnSave_Click">Submit</asp:LinkButton>

                </div>

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
                <asp:HiddenField ID="hdnEmpCode" runat="server" />

            </div>
        </div>
    </div>


    <script type="text/javascript">


        $(document).ready(function () {
            $(".DropdownListSearch").select2();
        });



    </script>
</asp:Content>


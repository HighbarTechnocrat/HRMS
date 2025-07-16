<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" EnableViewState="true"
    MaintainScrollPositionOnPostback="true" CodeFile="ABAP_Object_Tracker_TreeView.aspx.cs" Inherits="ABAP_Object_Tracker_TreeView" EnableSessionState="True" %>


<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2" Namespace="eWorld.UI" TagPrefix="ew" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="asp" %>


<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <style>
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

        #MainContent_LinkButton1, #MainContent_btnGenerateReport, #MainContent_btnReset {
            background: #3D1956;
            color: #febf39 !important;
            padding: 8px 18px;
            margin: 26px !important;
        }

        #MainContent_btnabapobject {
            background: #3D1956 !important;
            color: #febf39 !important;
            padding: 6px 16px !important;
        }

        .treeview-custom .treeNode {
            display: inline-block;
            width: 300px;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
        }

        .treeview-custom img {
            width: 15px;
            height: 15px;
            vertical-align: super !important;
        }

        .treeview-custom .treeNode a:before {
            content: "";
            display: inline-block;
            background-repeat: no-repeat;
            background-position: center;
        }

        .treeview-custom .treeNode.collapsed a:before {
            background-image: url('~/images/folder-closed1.png');
        }

        .treeview-custom .treeNode.expanded a:before {
            background-image: url('~/images/folder-open1.png');
        }

        .treeview-custom .treeNode.leaf a:before {
            background-image: none;
        }

        .wrap-text {
            word-wrap: break-word;
            white-space: normal;
        }
    </style>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
                        <br />
                        &nbsp;&nbsp;
                        <asp:Label ID="lblheading" runat="server" Text="ABAP Object Tree View"></asp:Label>
                        <asp:LinkButton ID="LinkButton1" runat="server" Text="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/ABAP_Object_Tracker_Index.aspx" BackColor="#3D1956" Style="float: right; margin: 10px 20px 0 0 !IMPORTANT; font-size: 13Px;"><%=System.Configuration.ConfigurationManager.AppSettings["ABAPObjectPageTitle"]%></asp:LinkButton>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;padding:0px 0px 0px 20px !important"></asp:Label>
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
                    <ul id="editform1" runat="server">

                        <li class="trvl_date">
                            <span>Project / Location </span>
                            <br />
                            <asp:DropDownList runat="server" ID="DDLProjectLocation" CssClass="DropdownListSearch">
                            </asp:DropDownList>
                        </li>
                        <li class="trvl_date">
                            <span>Search Object Name </span>
                            <asp:TextBox runat="server" ID="txtSearchedABAPObject" CssClass="DropdownListSearch"></asp:TextBox>

                        </li>
                        <li></li>
                        <li class="trvl_date" style="width: 30% !important;">
                            <asp:LinkButton ID="btnGenerateReport" runat="server" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();" OnClick="SearchABAPObjectDataList_Click">Search</asp:LinkButton>
                            <asp:LinkButton ID="btnReset" runat="server" Visible="true" CssClass="Savebtnsve" Text="Reset" OnClick="btnReset_Click">Clear Search</asp:LinkButton>
                        </li>
                        <li></li>
                        <li></li>

                        <li class="trvl_date" style="display: flex; flex-wrap: nowrap; width: 100%;">
                            <div style="width: 40%;">
                                <%-- <asp:TreeView ID="ulABAPObjects" runat="server" CssClass="treeview-custom" ShowExpandCollapse="false" NodeStyle-CssClass="treeNode" ShowLines="false" ExpandDepth="1" OnSelectedNodeChanged="ulABAPObjects_SelectedNodeChanged"
                                    AutoPostBack="True" OnTreeNodeExpanded="ulABAPObjects_TreeNodeExpanded" OnTreeNodeCollapsed="ulABAPObjects_TreeNodeCollapsed">
                                </asp:TreeView>--%>
                                <asp:TreeView ID="ulABAPObjects" runat="server" CssClass="treeview-custom"
                                    ShowExpandCollapse="true" NodeStyle-CssClass="treeNode" ShowLines="false"
                                    ExpandDepth="1" OnSelectedNodeChanged="ulABAPObjects_SelectedNodeChanged" AutoPostBack="True" CollapseImageUrl="~/images/folder-open.png"
                                    ExpandImageUrl="~/images/folder-closed.png" NoExpandImageUrl="~/images/folder-closed.png">
                                </asp:TreeView>
                            </div>
                            <div style="width: 60%; margin-left: 10%" id="Div1" runat="server" visible="false">
                                <span id="spnprojectlocation" runat="server">Project / Location: </span>
                                <asp:Label ID="txt_locationname" runat="server" Style="font-weight: bold"></asp:Label>
                                <br />
                                <br>
                                <span id="spnobjectdesc" runat="server">Object Description: </span>
                                <asp:Label ID="txt_objectDesc" runat="server" Style="font-weight: bold"></asp:Label>

                                <br />
                                <br />
                                <asp:Label ID="errormsg" runat="server" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                                <span id="spnrgs" runat="server" style="background-color: lightgray; width: 40%">RGS </span>
                                <asp:GridView ID="gvRGSuploadedFiles" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                    DataKeyNames="RGSDetailsId" Visible="false">
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

                                        <asp:BoundField HeaderText="Consultant"
                                            DataField="Emp_Name"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="40%" ItemStyle-Width="40%"
                                            ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Uploaded Files"
                                            DataField="RGSFileName"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="55%" ItemStyle-Width="55%"
                                            ItemStyle-BorderColor="Navy" />

                                        <asp:TemplateField HeaderText="Download" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnkDownload" runat="server" Style="text-align: center;" Height="15px" CommandArgument='<%# Eval("RGSFileName") %>' OnClick="ibdownloadbtn_Click" ImageUrl="~/Images/Download.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>


                                <br />
                                <span id="spnfs" runat="server" style="background-color: lightgray; width: 40%">FS </span>
                                <asp:GridView ID="gvFSuploadedFiles" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                    DataKeyNames="FSDetailsId" Visible="false">
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
                                        <asp:BoundField HeaderText="Consultant"
                                            DataField="Emp_Name"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="40%" ItemStyle-Width="40%"
                                            ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="File Version"
                                            DataField="Srno"
                                            DataFormatString="V{0}"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="5%" ItemStyle-Width="5%"
                                            ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Uploaded Files"
                                            DataField="FSFileName"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="50%" ItemStyle-Width="50%"
                                            ItemStyle-BorderColor="Navy" />

                                        <asp:TemplateField HeaderText="Download" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnkDownload" runat="server" Height="15px" CommandArgument='<%# Eval("FSFileName") %>' OnClick="ibdownloadbtn_Click" ImageUrl="~/Images/Download.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                <br />
                                <span id="spnabap" runat="server" style="background-color: lightgray; width: 40%">ABAP </span>
                                <asp:GridView ID="gvABAPuploadedFiles" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                    DataKeyNames="ABAPDetailsId" Visible="false">
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
                                        <asp:BoundField HeaderText="Consultant"
                                            DataField="Emp_Name"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="40%" ItemStyle-Width="40%"
                                            ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Uploaded Files"
                                            DataField="ABAPSourceFileName"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="55%" ItemStyle-Width="55%"
                                            ItemStyle-BorderColor="Navy" />

                                        <asp:TemplateField HeaderText="Download" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnkDownload" runat="server" Height="15px" CommandArgument='<%# Eval("ABAPSourceFileName") %>' OnClick="ibdownloadbtn_Click" ImageUrl="~/Images/Download.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                <br />
                                <span id="spnhbt" runat="server" style="background-color: lightgray; width: 40%">HBT Testing </span>
                                <asp:GridView ID="gvHBTuploadedFiles" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                    DataKeyNames="HBTDetailsId" Visible="false">
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
                                        <asp:BoundField HeaderText="Consultant"
                                            DataField="Emp_Name"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="40%" ItemStyle-Width="40%"
                                            ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Stages"
                                            DataField="FileType"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="5%" ItemStyle-Width="5%"
                                            ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="File Version"
                                            DataField="Srno"
                                            DataFormatString="V{0}"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="5%" ItemStyle-Width="5%"
                                            ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Uploaded Files"
                                            DataField="HBTFileName"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="35%" ItemStyle-Width="35%"
                                            ItemStyle-Wrap="true"
                                            ItemStyle-CssClass="wrap-text"
                                            ItemStyle-BorderColor="Navy" />

                                        <asp:TemplateField HeaderText="Download" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnkDownload" runat="server" Height="15px" CommandArgument='<%# Eval("HBTFileName") %>' OnClick="ibdownloadbtn_Click" ImageUrl="~/Images/Download.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                <br />
                                <span id="spnctm" runat="server" style="background-color: lightgray; width: 40%">CTM Testing </span>
                                <asp:GridView ID="gvCTMuploadedFiles" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                    DataKeyNames="CTMDetailsId" Visible="false">
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
                                        <asp:BoundField HeaderText="Consultant"
                                            DataField="Emp_Name"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="40%" ItemStyle-Width="40%"
                                            ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Uploaded Files"
                                            DataField="CTMFileName"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="55%" ItemStyle-Width="55%"
                                            ItemStyle-BorderColor="Navy" />

                                        <asp:TemplateField HeaderText="Download">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnkDownload" runat="server" Height="15px" CommandArgument='<%# Eval("CTMFileName") %>' OnClick="ibdownloadbtn_Click" ImageUrl="~/Images/Download.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                <br />
                                <span id="spnuat" runat="server" style="background-color: lightgray; width: 40%">UAT Sign Off </span>
                                <asp:GridView ID="gvUATuploadedFiles" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                    DataKeyNames="UATSignOffId" Visible="false">
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

                                        <asp:BoundField HeaderText="Uploaded Files"
                                            DataField="UATFileName"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="95%" ItemStyle-Width="95%"
                                            ItemStyle-BorderColor="Navy" />

                                        <asp:TemplateField HeaderText="Download" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnkDownload" runat="server" Height="15px" CommandArgument='<%# Eval("UATFileName") %>' OnClick="ibdownloadbtn_Click" ImageUrl="~/Images/Download.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                        <li class="trvl_date" id="lidata" runat="server" visible="false"></li>
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



    <asp:HiddenField ID="HDProjectLocation" runat="server" />



    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <script src="../js/freeze/jquery-ui.min.js"></script>
    <script src="../js/freeze/gridviewScroll.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#MainContent_DDLProjectLocation").select2();
            $(".DropdownListSearch").select2();

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


       <%-- function SaveMultiClick() {
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
        }--%>

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

            return;

        }

    //    function onLiClick(value) {
    //    __doPostBack('LiClick', value);
    //}


    </script>





</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="upload_gallery_images.aspx.cs" Inherits="upload_gallery_images" EnableEventValidation="false" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/manageleave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/ModifyLeaveRequest_css.css" type="text/css" media="all" />

    <script src="../js/HtmlControl/jHtmlArea-0.8.min.js"></script>
    <link href="../js/HtmlControl/jHtmlArea.css" rel="stylesheet" />

    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
            background: #dae1ed;
        }

        .select#MainContent_gvMngLeaveRqstList_ddlAproveReject_0 {
            padding-left: 0px;
        }

        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
            /*background: #dae1ed;*/
            background: #ebebe4;
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
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
    <script src="../js/HtmlControl/jquery-1.3.2.js"></script>
    <script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="../js/HtmlControl/jHtmlArea-0.8.min.js"></script>
    <link href="../js/HtmlControl/jHtmlArea.css" rel="stylesheet" />


    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">

                <div class="userposts">
                    <br />
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Upload Gallery / Banner Images"></asp:Label>
                    </span>
                </div>

                <span>
                    <a href="https://ess.highbartech.com/hrms/PersonalDocuments.aspx" class="aaaa">My Corner</a>
                </span>




                <div class="edit-contact">

                    <ul id="editform" runat="server" visible="false" width="100%">

                        <li>
                            <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label></li>
                        <li></li>

                        <li class="trvl_date">
                            <span>Select Category</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList ID="ddl_category" CssClass="DropdownListSearch" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_category_SelectedIndexChanged">
                            </asp:DropDownList>
                        </li>
                        <li></li>

                        <li class="leavedays">
                            <br />
                            <span>Title/ Tool Tip</span>&nbsp;&nbsp;<span id="Span1" runat="server" style="color: red">*</span>
                            <asp:TextBox ID="txt_tooltip" AutoComplete="off" runat="server"></asp:TextBox>
                        </li>

                        <li>
                            <span>URL</span>
                            <asp:TextBox ID="txt_url" AutoComplete="off" runat="server"></asp:TextBox>
                        </li>

                        <li class="trvl_type" id="file" runat="server">
                            <br />
                            <span id="spnsportingfiles" runat="server">Upload Image</span>&nbsp;&nbsp;<span id="spnsportingfiles_1" runat="server" style="color: red">*</span>
                            <asp:FileUpload ID="uplodmultiple" runat="server"></asp:FileUpload>
                        </li>
                        <li></li>
                        <li class="mobile_InboxEmpName">
                            <asp:RadioButton runat="server" ID="rbtnActive" Visible="false" GroupName="status" Text="Active" Checked="true" />
                            <asp:RadioButton runat="server" ID="rbtnDeactive" Visible="false" GroupName="status" Text="Deactive" />
                        </li>

                        <li></li>
                        <li>
                            <br />
                            <br />
                            <asp:LinkButton ID="btnIn" runat="server" CssClass="Savebtnsve" Visible="true" Text="Submit" OnClick="btnIn_Click" OnClientClick="return SaveMultiClick();">Submit</asp:LinkButton>
                            &nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="btnBack" runat="server" Text="Reset" ToolTip="Reset" CssClass="Savebtnsve" OnClick="btnBack_Click">Reset</asp:LinkButton>
                            <br />
                            <br />
                        
                        </li>

                        <asp:GridView ID="DgvApprover" runat="server" BackColor="White" 
                            DataKeyNames="id" BorderColor="Navy" PageSize="20" AllowPaging="True" OnPageIndexChanging="DgvApprover_PageIndexChanging" BorderStyle="Solid" BorderWidth="1px" CellPadding="5" AutoGenerateColumns="False" Width="100%">
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                            <PagerStyle ForeColor="#000066" HorizontalAlign="left" BackColor="White" />
                            <RowStyle ForeColor="#000066" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                            <PagerStyle HorizontalAlign="Right" CssClass="paging" />
                            <Columns>

                                <asp:BoundField HeaderText="Category"
                                    DataField="Category"
                                    ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="10%"
                                    HeaderStyle-Width="10%"
                                    ItemStyle-BorderColor="Navy" />

                                <asp:BoundField HeaderText="Image Tool Tip"
                                    DataField="Image_Des"
                                    ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="20%"
                                    HeaderStyle-Width="20%"
                                    ItemStyle-BorderColor="Navy" />

                                <asp:BoundField HeaderText="Image Name"
                                    DataField="Image_Name"
                                    ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="20%"
                                    HeaderStyle-Width="20%"
                                    ItemStyle-BorderColor="Navy" />

                                <asp:BoundField HeaderText="Image URL"
                                    DataField="Image_URL"
                                    ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="20%"
                                    HeaderStyle-Width="20%"
                                    ItemStyle-BorderColor="Navy"  Visible="false"/>

                                <asp:BoundField HeaderText="Active/Deactive"
                                    DataField="isActive"
                                    ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="5%"
                                    HeaderStyle-Width="5%"
                                    ItemStyle-BorderColor="Navy" />

                                <asp:TemplateField HeaderText="View" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="lnkEdit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkEdit_Click" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                        <li>
                            <br />
                            <br />
                        </li>
                    </ul>


                </div>
                <asp:HiddenField ID="hdnReqid" runat="server" />
                <asp:HiddenField ID="hdnId" runat="server" />
                <asp:HiddenField ID="hdnTravel_id" runat="server" />
                <asp:HiddenField ID="hdncurrid" runat="server" />
                <asp:HiddenField ID="FilePath" runat="server" />
                <asp:HiddenField ID="hdnYesNo" runat="server" />
            </div>
        </div>
    </div>

    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />
    <script type="text/javascript">

        $(document).ready(function () {
            $("#MainContent_ddl_letter").htmlarea();
            $(".DropdownListSearch").select2();
            $("#MainContent_txtPOWO_Content").htmlarea();
            $("#MainContent_txt_POWOContent_description").htmlarea();
        });



        function Confirm() {

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

        function DownloadFile(file) {
            // alert(file);
            var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;

            //alert(localFilePath);
            // window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
            window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);

        }


        function onCharOnlyNumber(e) {
            var keynum;
            var keychar;
            var numcheck = /[0123456789]/;

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

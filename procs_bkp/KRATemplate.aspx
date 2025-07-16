<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="KRATemplate.aspx.cs" Inherits="KRATemplate" %>

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
                        <asp:Label ID="lblheading" runat="server" Text="Assigned Template"></asp:Label>
                    </span>
                </div>

                <span>
                    <a href="KRA_Index.aspx" class="aaaa">KRA Home</a>
                </span>

                <span>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                </span>
 

 
                    <asp:Label runat="server" ID="RecordCount" Style="color: red; font-size: 14px;"></asp:Label>

                    <asp:GridView ID="gvMngTravelRqstList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                        DataKeyNames="KRA_ID,Role_Id" CellPadding="3" AutoGenerateColumns="False" Width="60%" EditRowStyle-Wrap="false" PageSize="20" AllowPaging="True" OnPageIndexChanging="gvMngTravelRqstList_PageIndexChanging">
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                        <PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" CssClass = "paging" />
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />

                        <Columns>                            

                            <asp:BoundField HeaderText="Role"
                                DataField="Role_Name"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="20%" ItemStyle-BorderColor="Navy" />                         
                             
                             <asp:BoundField HeaderText="Template Assigned to No of Employees"
                                DataField="noOfAssignedTemplate"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />   
                            
                             <asp:BoundField HeaderText="Status"
                                DataField="status"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />   

                              <asp:TemplateField  HeaderText="View" ItemStyle-Width="8%">
                                <ItemTemplate> 
                                    <asp:ImageButton ID="lnkLeaveDetails" ToolTip="View" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkLeaveDetails_Click" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                            </asp:TemplateField>

                           
                        </Columns>
                    </asp:GridView>

                <br /><br /><br />
            


                <div class="edit-contact">
                    <%-- <div class="editprofile" style="text-align: center; border: none;" id="divmsg" runat="server" visible="false">
                        <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: green; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                    </div>--%>
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" OnClick="lnkcont_Click"></asp:LinkButton>
                        </div>
                    </div>

                </div>
                <asp:HiddenField ID="hdnPOWOID" runat="server" />
                <asp:HiddenField ID="hdnMilestoneId" runat="server" />
                <asp:HiddenField ID="hdnInvoiceId" runat="server" />
                <asp:HiddenField ID="hdnEmpCode" runat="server" />

            </div>
        </div>
    </div>

 <%-- <script src="../js/freeze/jquery-ui.min.js"></script>
    <script src="../js/freeze/gridviewScroll.min.js"></script>--%>

    <script type="text/javascript"> 

   //      $(document).ready(function () {
            
   //          $('#MainContent_gvMngTravelRqstList').gridviewScroll({
   //             width: 600,
   //             height: 500,
   //             freezesize:5, // Freeze Number of Columns.
   //             headerrowcount: 1, //Freeze Number of Rows with Header.
			//});

   //     });

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


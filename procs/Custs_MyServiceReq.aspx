<%@ Page Title="" Language="C#" MasterPageFile="~/CustSinner.master" AutoEventWireup="true"
    CodeFile="Custs_MyServiceReq.aspx.cs" Inherits="Custs_MyServiceReq" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/manageleave_css.css" type="text/css" media="all" />
    <style>
        .myaccountpagesheading {
            background: #3D1956;
            color: #febf39 !important;
            padding: 9px 7px;
        }

        .aspNetDisabled {
            /*background: #dae1ed;*/
            background: #ebebe4;
        }

        #MainContent_lnk_leaverequest:link, #MainContent_lnk_leaverequest:visited,
        #MainContent_lnk_ServiceActionPending:link, #MainContent_lnk_ServiceActionPending:visited {
            background-color: #C7D3D4;
            color: #603F83 !important;
            border-radius: 10px;
            padding: 20px 5px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            width: 30% !important;
        }


        #MainContent_lnk_leaverequest:hover, #MainContent_lnk_leaverequest:active,
        #MainContent_lnk_ServiceActionPending:hover, #MainContent_lnk_ServiceActionPending:active {
            background-color: #3D1956;
            color: #C7D3D4 !important;
            border-color: #3D1956;
            border-width: 2pt;
            border-style: inset;
        }

        .createServiceRequest {
            color: #071952 !important;
            font-size: 22px !important;
            font-weight: normal;
            text-align: left;
            padding: 0 0 0 8px !important;
        }

        .userposts {
            color: #921A40 !important;
            font-size: 22px !important;
            font-weight: normal;
            text-align: left;
            padding: 0 0 0 8px !important;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>

    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                 <br />
                <br />

                <table>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lnk_leaverequest" Visible="false" runat="server" PostBackUrl="Cust_Service_Req.aspx">Create Service Request</asp:LinkButton>

                            <span>
                                <asp:LinkButton ID="lnk_createServiceRequest" runat="server" PostBackUrl="Cust_Service_Req.aspx" CssClass="createServiceRequest">Click here to Create Service Request</asp:LinkButton>
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lnk_ServiceActionPending" Visible="false" runat="server" PostBackUrl="Custs_MyServicePendingReq.aspx">Service Request Pending (0)</asp:LinkButton>

                            <asp:LinkButton ID="lnk_ServiceActionPending_New" Visible="false" runat="server" OnClick="LinkButton111_Click" CssClass="userposts">Actionable Service Request</asp:LinkButton>

                        </td>
                    </tr>
                </table>

                <br />
                <br />
                <br />
                <div>
                    <span>
                        <asp:Label ID="Label2" runat="server" CssClass="userposts" Text="Actionable Service Request"></asp:Label>
                    </span>
                </div>
                <div class="manage_grid" style="width: 100%; height: auto;">
                    <asp:Label runat="server" ID="Label1" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                    <asp:GridView ID="gvMngTravelRqstList_Pending" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                        DataKeyNames="Id" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false">
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" HorizontalAlign="Center" />
                        <PagerStyle ForeColor="#000066" HorizontalAlign="left" BackColor="White" />
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />

                        <Columns>
                            <asp:BoundField HeaderText="Service Request ID"
                                DataField="ServicesRequestID"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="20%" />

                            <asp:BoundField HeaderText="Creation Date"
                                DataField="ServiceRequestDate"
                                ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="15%" />


                            <asp:BoundField HeaderText="Assigned To"
                                DataField="AssignedTo"
                                ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="20%" />

                            <asp:BoundField HeaderText="Assignment Date"
                                DataField="AssignmentDate"
                                ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="15%" />

                            <asp:BoundField HeaderText="Service Status"
                                DataField="Status"
                                ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="10%" />

                            <asp:TemplateField HeaderText="View" HeaderStyle-Width="5%">
                                <ItemTemplate>

                                    <asp:ImageButton ID="lnkEdit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkFuelDetails_Click" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                </div>


                <br />
                <br />
                <br />
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="History"></asp:Label>
                    </span>
                </div>




                <div class="manage_grid" style="width: 100%; height: auto;">
                    <asp:Label runat="server" ID="lblmessage" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                    <asp:GridView ID="gvMngTravelRqstList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                        DataKeyNames="Id" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false">
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" HorizontalAlign="Center" />
                        <PagerStyle ForeColor="#000066" HorizontalAlign="left" BackColor="White" />
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />

                        <Columns>
                            <asp:BoundField HeaderText="Service Request ID"
                                DataField="ServicesRequestID"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="20%" />

                            <asp:BoundField HeaderText="Creation Date"
                                DataField="ServiceRequestDate"
                                ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="15%" />

                            <%--<asp:BoundField HeaderText="Employee Name"
                                DataField="EmployeeName"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="10%" />--%>

                            <asp:BoundField HeaderText="Assigned To"
                                DataField="AssignedTo"
                                ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="20%" />

                            <asp:BoundField HeaderText="Assignment Date"
                                DataField="AssignmentDate"
                                ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="15%" />
                            <%--  <asp:BoundField HeaderText="Action To"
                                DataField="AssignedTo"
                                 ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="15%" />

                            <asp:BoundField HeaderText="Action Date"
                                DataField="AssignmentDate"
                                 ItemStyle-HorizontalAlign="Center"                                
                                ItemStyle-Width="15%" />--%>

                            <asp:BoundField HeaderText="Status"
                                DataField="Status"
                                ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="10%" />

                            <asp:TemplateField HeaderText="View" HeaderStyle-Width="5%">
                                <ItemTemplate>
                                    <%--<asp:LinkButton ID="lnkEdit" runat="server" Text='View' OnClick="lnkEdit_Click1"></asp:LinkButton>--%>
                                    <asp:ImageButton ID="lnkEdit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkFuelDetails_Old_Click" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                </div>


                <%--'<%#"~/Details.aspx?ID="+Eval("ID") %>'>--%>
                <div class="cancelbtndiv">
                    <asp:LinkButton ID="lnkcont" Text="Load More.." ToolTip="Load More.." CssClass="submitbtn" runat="server" OnClick="lnkcont_Click"></asp:LinkButton>
                </div>


                <div class="edit-contact">

                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                    </div>


                    <ul id="editform" runat="server" visible="false">

                        <li></li>

                    </ul>
                </div>
                <asp:HiddenField ID="hdnRemid" runat="server" />
                <asp:HiddenField ID="hdnleaveTypeid" runat="server" />
                <asp:HiddenField ID="hdnleaveType" runat="server" />
                <asp:HiddenField ID="hdnEmpCode" runat="server" />
                <asp:HiddenField ID="hdnApproverType" runat="server" />
                <asp:HiddenField ID="hdninboxtype" runat="server" />
            </div>
        </div>
    </div>


    <script type="text/javascript">
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

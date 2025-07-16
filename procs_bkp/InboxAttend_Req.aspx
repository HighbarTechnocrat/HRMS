<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="InboxAttend_Req.aspx.cs" Inherits="InboxAttend_Req" %>


<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/manageleave_css.css" type="text/css" media="all" />
    <%-- <link href="<%=ReturnUrl("css") %>calendar/calendar.css" rel="stylesheet" type="text/css" />
    <link href="sampleform_css.css" rel="stylesheet" type="text/css" />--%>
    <%--<ajaxToolkit:ToolkitScriptManager ID="srcp" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
            /*background: #dae1ed;*/
            background: #ebebe4;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>

    <div id="loader" class="myLoader" style="display: none">
        <div class="loaderContent">
            <span style="top: -30%; font-size: 17px; color: red; position: absolute;">Please  Do Not Refresh  or Close Browser</span>
            <img src="../images/loader.gif">
        </div>
    </div>



    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Inbox Regularization"></asp:Label>
                    </span>
                </div>

                <span>
                    <a href="Attendance.aspx" class="aaaa">Attendance Menu</a>
                </span>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                </div>
                <div class="manage_grid" style="width: 100%; height: auto;">
                    <%--<center>--%>
                    <asp:GridView ID="gvMngLeaveRqstList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                        DataKeyNames="Reg_id,Emp_Code,Emp_Name,Leave_Req_id,Leave_Status" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false" OnRowDataBound="gvMngLeaveRqstList_DataBounds">
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
                            <asp:TemplateField HeaderText="View" HeaderStyle-Width="1%">
                                <ItemTemplate>
                                    <asp:ImageButton ID="lnkEdit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkLeaveDetails_Click" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Submitted on"
                                DataField="RequestedDate"
                                ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="left"
                                ItemStyle-Width="8%"
                                ItemStyle-BorderColor="Navy" />

                            <asp:BoundField HeaderText="Employee Code"
                                DataField="Emp_Code"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="6%" ItemStyle-BorderColor="Navy" />

                            <asp:BoundField HeaderText="Employee Name"
                                DataField="Emp_Name"
                                ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="left"
                                ItemStyle-Width="13%"
                                ItemStyle-BorderColor="Navy" />

                            <asp:BoundField HeaderText="Date"
                                DataField="att_date"
                                ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="7%"
                                ItemStyle-BorderColor="Navy" />

                            <asp:BoundField HeaderText="In / Out"
                                DataField="IN_OUT_TYPE"
                                ItemStyle-HorizontalAlign="center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="5%"
                                ItemStyle-BorderColor="Navy" />
                            <asp:BoundField HeaderText="Time"
                                DataField="IN_OUT_TIME"
                                ItemStyle-HorizontalAlign="center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="11%"
                                ItemStyle-BorderColor="Navy" />
                            <asp:BoundField HeaderText="Status"
                                DataField="IN_OUT_STATUS"
                                ItemStyle-HorizontalAlign="center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="12%"
                                ItemStyle-BorderColor="Navy" />
                            <asp:BoundField HeaderText="Deduction"
                                DataField="deduction"
                                ItemStyle-HorizontalAlign="center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="5%"
                                ItemStyle-BorderColor="Navy" />
                            <asp:BoundField HeaderText="Regularization"
                                DataField="Leave_Status"
                                ItemStyle-HorizontalAlign="center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="4%"
                                ItemStyle-BorderColor="Navy" />
                            <asp:BoundField HeaderText="Employee Remark"
                                DataField="Remark"
                                ItemStyle-HorizontalAlign="center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="12%"
                                ItemStyle-BorderColor="Navy" />

                            <asp:TemplateField HeaderText="Select ALL" ItemStyle-Width="2%">
                                <HeaderTemplate>
                                    <span>Select</span><br />
                                    <asp:CheckBox ID="chkAll" ToolTip="Select ALL" runat="server" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CHK_clearancefromSubmitted" runat="server" AutoPostBack="false" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>


                        </Columns>
                    </asp:GridView>
                    <%--</center>--%>

                    <br />
                    <asp:LinkButton ID="btnIn" Visible="false" runat="server" CssClass="Savebtnsve" Text="Submit" OnClick="btnIn_Click" OnClientClick="return SaveMultiClick();">Submit</asp:LinkButton>

                    <br />
                    <br />
                    <br />
                    <br />

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



                </div>

                <asp:HiddenField ID="hdnReqid" runat="server" />
                <asp:HiddenField ID="hdnleaveTypeid" runat="server" />
                <asp:HiddenField ID="hdnleaveType" runat="server" />
                <asp:HiddenField ID="hdnApproverType" runat="server" />
                <asp:HiddenField ID="hdnEmpCode" runat="server" />
                <asp:HiddenField ID="hdnhrappType" runat="server" />
                <asp:HiddenField ID="hdnReqids" runat="server" />
                <asp:HiddenField ID="hdnYesNo" runat="server" />

                <link href="../includes/loader.css" rel="stylesheet" />



            </div>
        </div>
    </div>


    <script type="text/javascript">      

        function SaveMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=btnIn.ClientID%>');

                $('#loader').show();

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


    </script>

</asp:Content>

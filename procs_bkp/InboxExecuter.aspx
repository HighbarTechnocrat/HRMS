<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true"
    CodeFile="InboxExecuter.aspx.cs" Inherits="InboxExecuter" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />

    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
            background: #ebebe4;
        }

        #MainContent_lstApprover {
            overflow: hidden !important;
        }

        .edit-contact input {
            padding-left: 0px !important;
        }

        #MainContent_View_Reprt {
            background: #3D1956;
            color: #febf39 !important;
            padding: 0.5% 1.4%;
            margin: 0% 0% 0 0;
        }

        .noresize {
            resize: none;
        }

        /* .select2-container .select2-selection--multiple {
            box-sizing: border-box;
            cursor: pointer;
            display: block;
            min-height: 32px;
            user-select: none;
            -webkit-user-select: none;
            height: 10px;
        }*/
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
    <div class="commpagesdiv">
        <div class="commonpages">

            <div class="userposts">
                <span>
                    <asp:Label ID="lblheading" runat="server" Text="Task Executor Inbox"></asp:Label>
                </span>
            </div>
            <div>
                <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
            </div>
            <span runat="server" id="backToSPOC" visible="false">
                <a href="InboxServiceRequest.aspx" class="aaaa">Back</a>
            </span>
            <span runat="server" id="backToEmployee" visible="false">
                <a href="MyService_Req.aspx" class="aaaa">Back</a>
            </span>
            <span runat="server" id="backToArr" visible="false">
                <a href="InboxServiceRequest_Arch.aspx" class="aaaa">Back</a>
            </span>
            <span>
                <a href="TaskMonitoring.aspx" style="margin-right: 18px;" class="aaaa">Task Monitoring</a>&nbsp;&nbsp; 
            </span>
            <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                <div class="cancelbtndiv">
                    <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                </div>
                <div class="cancelbtndiv">
                    <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server"></asp:LinkButton>
                </div>
            </div>
            <ul id="editform" runat="server" visible="false">
                <li style="width: 100%">
                    <br />
                    <div>

                        <asp:GridView ID="gv_TaskExecuterList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                            DataKeyNames="Task_ID" PageSize="20" AllowPaging="True" OnPageIndexChanging="gv_TaskExecuterList_PageIndexChanging">
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
                                <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="TaskExecuter_Edit" runat="server" Visible="true" CommandArgument='<%#Eval("ID") +","+ Eval("Task_Ref_id") + "," + Eval("Task_ID")%>' Width="15px" Height="15px" OnClientClick="return SubmitClick();" OnClick="TaskExecuter_Edit_Click"><img src="../Images/edit.png" /></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                </asp:TemplateField>

                                <asp:BoundField HeaderText="Task Reference Id"
                                    DataField="Task_Reference_ID"
                                    ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" Visible="false" />

                                <asp:BoundField HeaderText="Meeting/ Discussion Date"
                                    DataField="Created_On"
                                    ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />

                                <asp:BoundField HeaderText="Meeting / Discussion Title"
                                    DataField="Meeting_Discussion_Title"
                                    ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                                <asp:BoundField HeaderText="Task ID"
                                    DataField="Task_ID"
                                    ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                <asp:BoundField HeaderText="Task Due Date"
                                    DataField="Due_Date"
                                    ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="9%" ItemStyle-BorderColor="Navy" />

                                <asp:BoundField HeaderText="Task Description"
                                    DataField="Task_Description"
                                    ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                                <asp:BoundField HeaderText="Project / Location"
                                    DataField="Location_name"
                                    ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                <asp:BoundField HeaderText="Action By"
                                    DataField="ActionBy"
                                    ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                                <asp:BoundField HeaderText="Action"
                                    DataField="Actionstatus"
                                    ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                <asp:BoundField HeaderText="Status"
                                    DataField="StatusName"
                                    ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="4%" ItemStyle-BorderColor="Navy" /> 
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
            </ul>
        </div>
    </div>




</asp:Content>

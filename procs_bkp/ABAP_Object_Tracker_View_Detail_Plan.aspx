<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" EnableViewState="true"
    MaintainScrollPositionOnPostback="true" CodeFile="ABAP_Object_Tracker_View_Detail_Plan.aspx.cs" Inherits="ABAP_Object_Tracker_View_Detail_Plan" EnableSessionState="True" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <style>
        #btnList_abaplist, #btnTra_Details {
            background: #3D1956;
            color: #febf39 !important;
            font-size: medium;
            text-align: left;
        }

        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        #MainContent_lstApprover, #MainContent_lstIntermediate {
            padding: 0 0 33px 0 !important;
        }

        .Dropdown {
            border-bottom: 2px solid #cccccc;
            background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;
            cursor: default;
        }

        input#MainContent_btnViewABAPObjectPlanDetails {
            margin: 0 0 -6px 0 !important;
        }

        div#left-content {
            max-width: 100% !important;
            max-height: 100% !important;
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

        .Milestones th {
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

    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <br />
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="ABAP Object Detail Plan"></asp:Label>
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

                    <ul id="editform1" runat="server">
                        <li class="trvl_date">
                            <span>Project / Location</span><br />
                            <asp:DropDownList runat="server" ID="DDLProjectLocation" AutoPostBack="false" CssClass="DropdownListSearch">
                            </asp:DropDownList>
                            <asp:TextBox AutoComplete="off" ID="txtPOtype" runat="server" CssClass="Dropdown" Visible="false"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>Project Manager</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPRM" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>

                        </li>

                        <li class="trvl_grid" id="lirgsdetails" runat="server">
                            <span class="abapobject" runat="server" visible="true" id="abapobject">ABAP Object Detail Plan</span>
                            <asp:GridView ID="gvDetailPlan" runat="server" CssClass="Milestones" DataKeyNames="RGSDetailsId" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                                CellPadding="3" AutoGenerateColumns="False">
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
                                    <asp:BoundField HeaderText="Development Description"
                                        DataField="Development_Desc"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="8%" ItemStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Module"
                                        DataField="ModuleDesc"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderColor="Navy"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%" />

                                    <asp:BoundField HeaderText="Interface"
                                        DataField="Interface"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="FRICE Category"
                                        DataField="FCategoryName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Priority /Order"
                                        DataField="Priority"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        HeaderStyle-Width="1%" ItemStyle-Width="1%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Priority"
                                        DataField="PriorityName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="1%" ItemStyle-Width="1%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Complexity"
                                        DataField="ComplexityName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="RGS Consultant"
                                        DataField="RGSConsultant"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="5%" ItemStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Preparation Start Date"
                                        DataField="PlannedPrepStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Preparation Finish Date"
                                        DataField="PlannedPrepFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Submission Date"
                                        DataField="PlannedSubmit"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Approval Date"
                                        DataField="PlannedApprove"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Revised/ Actual Preparation Start Date"
                                        DataField="RevActualPrepStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Revised/ Actual Preparation Finish Date"
                                        DataField="RevActualPrepFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Revised/ Actual Submit Date"
                                        DataField="RevActualSubmit"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Revised/ Actual Approve Date"
                                        DataField="RevActualApprove"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Remark"
                                        DataField="Remark"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="3%" ItemStyle-Width="3%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Status"
                                        DataField="RGSStatusName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="3%" ItemStyle-Width="3%"
                                        ItemStyle-BorderColor="Navy" />

                                    <%--FS Sections--%>
                                    <asp:BoundField HeaderText="FS Consultant"
                                        DataField="FSConsultant"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="8%" ItemStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="FS Planned Start Date"
                                        DataField="FSPlannedStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="FS Planned Finish Date"
                                        DataField="FSPlannedFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="FS Actual Start Date"
                                        DataField="FSActualStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="FS Revised Finish Date"
                                        DataField="FSRevisedFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="FS Actual Finish Date"
                                        DataField="FSActualFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText=" FS Remark"
                                        DataField="Remark"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="3%" ItemStyle-Width="3%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText=" FS Status"
                                        DataField="FSStatusName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="3%" ItemStyle-Width="3%"
                                        ItemStyle-BorderColor="Navy" />

                                    <%--ABAP Section--%>
                                    <asp:BoundField HeaderText="ABAP Consultant"
                                        DataField="ABAPDevConsultant"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="8%" ItemStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="ABAP Duration (Days)"
                                        DataField="DevDuaration"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="ABAP Planned Start Date"
                                        DataField="ABAPPlannedStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="ABAP Planned Finish Date"
                                        DataField="ABAPPlannedFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="ABAP Revised Start Date"
                                        DataField="ABAPRevisedStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="ABAP Revised Finish Date"
                                        DataField="ABAPRevisedFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />


                                    <asp:BoundField HeaderText="ABAP Planned Finish Date"
                                        DataField="ABAPActualStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />


                                    <asp:BoundField HeaderText="ABAP Actual Finish Date"
                                        DataField="ABAPActualFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="ABAP Remark"
                                        DataField="Remark"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="5%" ItemStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="ABAP Status"
                                        DataField="ABAPDevStatusName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="3%" ItemStyle-Width="3%"
                                        ItemStyle-BorderColor="Navy" />


                                    <asp:BoundField HeaderText="ABAP Functional Status"
                                        DataField="ABAPFunctionalStatusName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="3%" ItemStyle-Width="3%"
                                        ItemStyle-BorderColor="Navy" Visible="false" />

                                    <%-- HBT Section --%>
                                    <asp:BoundField HeaderText="HBT Consultant"
                                        DataField="HBTConsultant"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="8%" ItemStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="HBT Planned Start Date"
                                        DataField="HBTPlannedStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="HBT Planned Finish Date"
                                        DataField="HBTPlannedFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="HBT Revised Start Date"
                                        DataField="hbtRevisedStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="HBT Revised Finish Date"
                                        DataField="hbtRevisedFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="HBT Actual Start Date"
                                        DataField="hbtActualStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="HBT Actual Finish Date"
                                        DataField="hbtActualFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="HBT Remark"
                                        DataField="Remark"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="5%" ItemStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="HBT Status"
                                        DataField="HBTTestStatusName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="3%" ItemStyle-Width="3%"
                                        ItemStyle-BorderColor="Navy" />

                                    <%-- CTM Section --%>
                                    <asp:BoundField HeaderText="CTM Consultant"
                                        DataField="CTMConsultant"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="8%" ItemStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Client Person"
                                        DataField="CTMName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="10%" ItemStyle-Width="10%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="CTM Duration (Days)"
                                        DataField="Duration"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        HeaderStyle-Width="3%" ItemStyle-Width="3%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Start Date"
                                        DataField="CTMPlannedStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Finish Date"
                                        DataField="CTMPlannedFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="CTM Revised Start Date"
                                        DataField="CTMRevisedStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Finish Date"
                                        DataField="CTMRevisedFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="CTM Actual Start Date"
                                        DataField="CTMActualStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="CTM Actual Finish Date"
                                        DataField="CTMActualFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Duration"
                                        DataField="Duration"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Days"
                                        DataField="PlannedDays"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Remark"
                                        DataField="Remark"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="5%" ItemStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Status"
                                        DataField="CTMTestStatusName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="3%" ItemStyle-Width="3%"
                                        ItemStyle-BorderColor="Navy" />


                                    <%-- UAT Section --%>

                                    <asp:BoundField HeaderText="UAT Planned Date"
                                        DataField="UATPlanned_Date"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="UAT Revised Date"
                                        DataField="UATRevisedDate"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="UAT Actual Date"
                                        DataField="UATActualDate"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Remark"
                                        DataField="Remark"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="5%" ItemStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="UAT Status"
                                        DataField="UATStatusName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="3%" ItemStyle-Width="3%"
                                        ItemStyle-BorderColor="Navy" />

                                    <%--Go Live--%>

                                    <asp:BoundField HeaderText="GL Planned Date"
                                        DataField="GLPlannedDate"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="GL Revised Date"
                                        DataField="GLRevisedDate"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="GL Actual Date"
                                        DataField="GLActualDate"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Remark"
                                        DataField="Remark"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="5%" ItemStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Status"
                                        DataField="GoLiveStatusName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="3%" ItemStyle-Width="3%"
                                        ItemStyle-BorderColor="Navy" />

                                </Columns>
                            </asp:GridView>
                            <br />
                        </li>


                        <li class="trvl_date">
                            <br />
                            <asp:LinkButton ID="btnTra_Details" runat="server" Visible="true" Text="BACK" ToolTip="BACK" CssClass="Savebtnsve">BACK</asp:LinkButton>
                        </li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>

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

    <asp:HiddenField ID="hflEmpName" runat="server" />
    <asp:HiddenField ID="hflEmpDesignation" runat="server" />
    <asp:HiddenField ID="hflEmpDepartment" runat="server" />
    <asp:HiddenField ID="hflEmailAddress" runat="server" />
    <asp:HiddenField ID="hflGrade" runat="server" />

    <asp:HiddenField ID="FilePath" runat="server" />
    <asp:HiddenField ID="hdnSrno" runat="server" />
    <asp:HiddenField ID="hdnpamentStatusid" runat="server" />
    <asp:HiddenField ID="hdnIGSTAmt" runat="server" />
    <asp:HiddenField ID="hdnCompCode" runat="server" />
    <asp:HiddenField ID="hdnVendorId" runat="server" />
    <asp:HiddenField ID="hdnPOWOID" runat="server" />
    <asp:HiddenField ID="hdnInvoiceId" runat="server" />
    <asp:HiddenField ID="hdnABAPODUploadId" runat="server" />

    <asp:HiddenField ID="hdnstype_Main" runat="server" />
    <asp:HiddenField ID="hdnMilestoneID" runat="server" />

    <asp:HiddenField ID="hdnPOTypeId" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />

    <asp:HiddenField ID="hdn_next_Appr_ID" runat="server" />
    <asp:HiddenField ID="hdn_next_Appr_Empcode" runat="server" />
    <asp:HiddenField ID="hdn_next_Appr_EmpEmail_ID" runat="server" />
    <asp:HiddenField ID="hdn_next_Appr_Emp_Name" runat="server" />

    <asp:HiddenField ID="hdn_curnt_Appr_ID" runat="server" />
    <asp:HiddenField ID="hdn_curnt_Appr_Empcode" runat="server" />
    <asp:HiddenField ID="hdn_curnt_Appr_EmpEmail_ID" runat="server" />
    <asp:HiddenField ID="hdn_curnt_Appr_Emp_Name" runat="server" />

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
                freezesize: 7,
                headerrowcount: 1
            };

            applyGridViewScroll('#MainContent_gvDetailPlan', gridOptions);

            // var element = document.getElementById("div#left-content");
            //element.style.maxWidth = " !important";
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


        function SaveMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=btnTra_Details.ClientID%>');

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

        function DownloadFile(file) {
            // alert(file);
            var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;

            //alert(localFilePath);
            //window.open("https://ess.highbartech.com/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
            //window.open("https://192.168.21.193/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
            window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
        }


    </script>
</asp:Content>

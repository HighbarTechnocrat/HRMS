<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" EnableViewState="true"
    MaintainScrollPositionOnPostback="true" CodeFile="ABAP_Object_Tracker_Plan_View.aspx.cs" Inherits="ABAP_Object_Tracker_Plan_View" EnableSessionState="True" %>

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

        input#MainContent_btnViewABAPObjectDetailPlan {
            margin: 0 19px -7px 0 !important;
            color: white !important;
        }

        a#MainContent_btnRGS_Details, #MainContent_btnFS_Details, #MainContent_btnABAPDev_Details, #MainContent_btnHBT_Details, #MainContent_btnCTM_Details, #MainContent_btnUATSignOff_Details, #MainContent_btnGoLive_Details {
            background: #3D1956;
            color: #febf39 !important;
            padding: 6px 16px;
        }

        a#MainContent_lnk_addconsultantctmtest {
            margin: 0 0 -10px 0 !important;
        }

        div#left-content {
            max-width: 100% !important;
            max-height: 100% !important;
        }

        a#MainContent_btnABAPPlanSubmit {
            background: #3D1956;
            color: #febf39 !important;
            padding: 8px 18px;
            margin: 0 0 0 0 !important;
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

        #content-container .intern-padding {
            padding: 0px 0px !important;
            cursor: auto !important;
        }

        #MainContent_lnk_Index {
            float: right;
            background: #3D1956;
            color: #febf39 !important;
            padding: 0.5% 1.4%;
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
                <div class="userposts" style="width: 50% !important">
                    <br />
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="View ABAP Object Plan"></asp:Label>
                    </span>
                </div>
                <asp:LinkButton ID="lnk_Index" runat="server" CssClass="Savebtnsve" PostBackUrl="~/procs/ABAP_Object_Tracker_Index.aspx" BackColor="#3D1956" Style="margin: 30px 20px 0px 0px !important;"><%=System.Configuration.ConfigurationManager.AppSettings["ABAPObjectPageTitle"]%></asp:LinkButton>

                <div runat="server" visible="false">
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center; margin: 0px 0px 0px 10px !important;"></asp:Label>
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
                        <li style="width: 70%" runat="server" id="licolortext">
                            <div style="width: 30px; height: 15px; background-color: #FFE699; display: inline-block; margin: 0 0 -4px 0 !important;"></div>
                            <span style="display: inline-block; vertical-align: middle;">- RGS | </span>
                            <div style="width: 30px; height: 15px; background-color: #A9D08E; display: inline-block; margin: 0 0 -4px 0 !important;"></div>
                            <span style="display: inline-block; vertical-align: middle;">- FS | </span>
                            <div style="width: 30px; height: 15px; background-color: #F4B084; display: inline-block; margin: 0 0 -4px 0 !important;"></div>
                            <span style="display: inline-block; vertical-align: middle;">- ABAP Dev | </span>
                            <div style="width: 30px; height: 15px; background-color: #8EA9DB; display: inline-block; margin: 0 0 -4px 0 !important;"></div>
                            <span style="display: inline-block; vertical-align: middle;">- HBT Test | </span>
                            <div style="width: 30px; height: 15px; background-color: #BF8F00; display: inline-block; margin: 0 0 -4px 0 !important;"></div>
                            <span style="display: inline-block; vertical-align: middle;">- CTM Test</span>
                        </li>
                        <li class="trvl_grid" id="li1" runat="server">
                            <span class="rgs" runat="server" visible="true" id="Span2">ABAP Object Detail Plan</span>

                            <asp:LinkButton ID="trvl_btnSave" runat="server" Text="Submit" ToolTip="Submit" CssClass="Savebtnsve" OnClick="btnExportToExcel_Click">Export to Excel</asp:LinkButton>
                            <asp:GridView ID="gvDetailPlan" runat="server" CssClass="Milestones" DataKeyNames="ABAPODId" OnRowDataBound="GridView_RowDataBound" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
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

                                    <asp:TemplateField ItemStyle-Width="2%" HeaderText="View" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkABAPPlanDetails" ToolTip="View" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkABAPPlanDetails_Click" />

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

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
                                        HeaderStyle-Width="3%" ItemStyle-Width="3%"
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
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
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
                                        HeaderStyle-Width="3%" ItemStyle-Width="3%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#FFE699" />
                                        <HeaderStyle BackColor="#FFE699" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Planned Preparation Start Date"
                                        DataField="PlannedPrepStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#FFE699" />
                                        <HeaderStyle BackColor="#FFE699" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Planned Preparation Finish Date"
                                        DataField="PlannedPrepFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#FFE699" />
                                        <HeaderStyle BackColor="#FFE699" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Planned Submission Date"
                                        DataField="PlannedSubmit"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#FFE699" />
                                        <HeaderStyle BackColor="#FFE699" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Planned Approval Date"
                                        DataField="PlannedApprove"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#FFE699" />
                                        <HeaderStyle BackColor="#FFE699" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Revised/ Actual Preparation Start Date"
                                        DataField="RevActualPrepStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#FFE699" />
                                        <HeaderStyle BackColor="#FFE699" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Revised/ Actual Preparation Finish Date"
                                        DataField="RevActualPrepFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#FFE699" />
                                        <HeaderStyle BackColor="#FFE699" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Revised/ Actual Submit Date"
                                        DataField="RevActualSubmit"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#FFE699" />
                                        <HeaderStyle BackColor="#FFE699" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Revised/ Actual Approve Date"
                                        DataField="RevActualApprove"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#FFE699" />
                                        <HeaderStyle BackColor="#FFE699" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Remark"
                                        DataField="Remark"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="3%" ItemStyle-Width="3%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#FFE699" />
                                        <HeaderStyle BackColor="#FFE699" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Status"
                                        DataField="RGSStatusName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#FFE699" />
                                        <HeaderStyle BackColor="#FFE699" />
                                    </asp:BoundField>

                                    <%--FS Sections--%>
                                    <asp:BoundField HeaderText="FS Consultant"
                                        DataField="FSConsultant"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="3%" ItemStyle-Width="3%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#A9D08E" />
                                        <HeaderStyle BackColor="#A9D08E" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="FS Planned Start Date"
                                        DataField="FSPlannedStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#A9D08E" />
                                        <HeaderStyle BackColor="#A9D08E" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="FS Planned Finish Date"
                                        DataField="FSPlannedFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#A9D08E" />
                                        <HeaderStyle BackColor="#A9D08E" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="FS Revised Start Date"
                                        DataField="FSRevisedStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#A9D08E" />
                                        <HeaderStyle BackColor="#A9D08E" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="FS Revised Finish Date"
                                        DataField="FSRevisedFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#A9D08E" />
                                        <HeaderStyle BackColor="#A9D08E" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="FS Actual Start Date"
                                        DataField="FSActualStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#A9D08E" />
                                        <HeaderStyle BackColor="#A9D08E" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="FS Actual Finish Date"
                                        DataField="FSActualFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#A9D08E" />
                                        <HeaderStyle BackColor="#A9D08E" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="FS Remark"
                                        DataField="Remark"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="3%" ItemStyle-Width="3%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#A9D08E" />
                                        <HeaderStyle BackColor="#A9D08E" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="FS Status"
                                        DataField="FSStatusName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="3%" ItemStyle-Width="3%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#A9D08E" />
                                        <HeaderStyle BackColor="#A9D08E" />
                                    </asp:BoundField>

                                    <%--ABAP Section--%>
                                    <asp:BoundField HeaderText="ABAP Consultant"
                                        DataField="ABAPDevConsultant"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="3%" ItemStyle-Width="3%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#F4B084" />
                                        <HeaderStyle BackColor="#F4B084" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="ABAP Duration (Days)"
                                        DataField="DevDuaration"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#F4B084" />
                                        <HeaderStyle BackColor="#F4B084" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Reusable Client Name"
                                        DataField="ReusableClientName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#F4B084" />
                                        <HeaderStyle BackColor="#F4B084" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Reusable Status"
                                        DataField="ResuableStatusName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#F4B084" />
                                        <HeaderStyle BackColor="#F4B084" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Reusable Addtional Efforts"
                                        DataField="ReusableAdditonalEffort"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#F4B084" />
                                        <HeaderStyle BackColor="#F4B084" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Reusable Percentage"
                                        DataField="ReusablePercent"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#F4B084" />
                                        <HeaderStyle BackColor="#F4B084" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Reusable Reamarks"
                                        DataField="ReusableDetailsRemarks"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#F4B084" />
                                        <HeaderStyle BackColor="#F4B084" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="TCode"
                                        DataField="Custom_Tcode"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#F4B084" />
                                        <HeaderStyle BackColor="#F4B084" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="ABAP Planned Start Date"
                                        DataField="ABAPPlannedStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#F4B084" />
                                        <HeaderStyle BackColor="#F4B084" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="ABAP Planned Finish Date"
                                        DataField="ABAPPlannedFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#F4B084" />
                                        <HeaderStyle BackColor="#F4B084" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="ABAP Revised Start Date"
                                        DataField="ABAPRevisedStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#F4B084" />
                                        <HeaderStyle BackColor="#F4B084" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="ABAP Revised Finish Date"
                                        DataField="ABAPRevisedFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#F4B084" />
                                        <HeaderStyle BackColor="#F4B084" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="ABAP Actual Start Date"
                                        DataField="ABAPActualStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#F4B084" />
                                        <HeaderStyle BackColor="#F4B084" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="ABAP Actual Finish Date"
                                        DataField="ABAPActualFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#F4B084" />
                                        <HeaderStyle BackColor="#F4B084" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="ABAP Remark"
                                        DataField="Remark"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="5%" ItemStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#F4B084" />
                                        <HeaderStyle BackColor="#F4B084" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="ABAP Status"
                                        DataField="ABAPDevStatusName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="3%" ItemStyle-Width="3%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#F4B084" />
                                        <HeaderStyle BackColor="#F4B084" />
                                    </asp:BoundField>


                                    <asp:BoundField HeaderText="ABAP Functional Status"
                                        DataField="ABAPFunctionalStatusName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="3%" ItemStyle-Width="3%"
                                        ItemStyle-BorderColor="Navy" Visible="false">
                                        <ItemStyle BackColor="#F4B084" />
                                        <HeaderStyle BackColor="#F4B084" />
                                    </asp:BoundField>

                                    <%-- HBT Section --%>
                                    <asp:BoundField HeaderText="HBT Consultant"
                                        DataField="HBTConsultant"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="3%" ItemStyle-Width="3%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#8EA9DB" />
                                        <HeaderStyle BackColor="#8EA9DB" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="HBT Planned Start Date"
                                        DataField="HBTPlannedStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#8EA9DB" />
                                        <HeaderStyle BackColor="#8EA9DB" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="HBT Planned Finish Date"
                                        DataField="HBTPlannedFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#8EA9DB" />
                                        <HeaderStyle BackColor="#8EA9DB" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="HBT Revised Start Date"
                                        DataField="hbtRevisedStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#8EA9DB" />
                                        <HeaderStyle BackColor="#8EA9DB" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="HBT Revised Finish Date"
                                        DataField="hbtRevisedFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#8EA9DB" />
                                        <HeaderStyle BackColor="#8EA9DB" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="HBT Actual Start Date"
                                        DataField="hbtActualStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#8EA9DB" />
                                        <HeaderStyle BackColor="#8EA9DB" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="HBT Actual Finish Date"
                                        DataField="hbtActualFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#8EA9DB" />
                                        <HeaderStyle BackColor="#8EA9DB" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="HBT Remark"
                                        DataField="Remark"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="5%" ItemStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#8EA9DB" />
                                        <HeaderStyle BackColor="#8EA9DB" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="HBT Status"
                                        DataField="HBTTestStatusName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="3%" ItemStyle-Width="3%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#8EA9DB" />
                                        <HeaderStyle BackColor="#8EA9DB" />
                                    </asp:BoundField>

                                    <%-- CTM Section --%>
                                    <asp:BoundField HeaderText="CTM Consultant"
                                        DataField="CTMConsultant"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="3%" ItemStyle-Width="3%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#BF8F00" />
                                        <HeaderStyle BackColor="#BF8F00" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Client Person"
                                        DataField="CTMName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="5%" ItemStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#BF8F00" />
                                        <HeaderStyle BackColor="#BF8F00" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="CTM Duration (Days)"
                                        DataField="Duration"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#BF8F00" />
                                        <HeaderStyle BackColor="#BF8F00" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="CTM Planned Start Date"
                                        DataField="CTMPlannedStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#BF8F00" />
                                        <HeaderStyle BackColor="#BF8F00" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="CTM Planned Finish Date"
                                        DataField="CTMPlannedFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#BF8F00" />
                                        <HeaderStyle BackColor="#BF8F00" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="CTM Revised Start Date"
                                        DataField="CTMRevisedStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#BF8F00" />
                                        <HeaderStyle BackColor="#BF8F00" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="CTM Revised Finish Date"
                                        DataField="CTMRevisedFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#BF8F00" />
                                        <HeaderStyle BackColor="#BF8F00" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="CTM Actual Start Date"
                                        DataField="CTMActualStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#BF8F00" />
                                        <HeaderStyle BackColor="#BF8F00" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="CTM Actual Finish Date"
                                        DataField="CTMActualFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#BF8F00" />
                                        <HeaderStyle BackColor="#BF8F00" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Duration"
                                        DataField="Duration"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#BF8F00" />
                                        <HeaderStyle BackColor="#BF8F00" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Planned Days"
                                        DataField="PlannedDays"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#BF8F00" />
                                        <HeaderStyle BackColor="#BF8F00" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Remark"
                                        DataField="Remark"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="5%" ItemStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#BF8F00" />
                                        <HeaderStyle BackColor="#BF8F00" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Status"
                                        DataField="CTMTestStatusName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#BF8F00" />
                                        <HeaderStyle BackColor="#BF8F00" />
                                    </asp:BoundField>


                                    <%-- UAT Section --%>

                                    <asp:BoundField HeaderText="UAT Planned Date"
                                        DataField="UATPlanned_Date"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#FCE4D6" />
                                        <HeaderStyle BackColor="#FCE4D6" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="UAT Revised Date"
                                        DataField="UATRevisedDate"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#FCE4D6" />
                                        <HeaderStyle BackColor="#FCE4D6" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="UAT Actual Date"
                                        DataField="UATActualDate"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#FCE4D6" />
                                        <HeaderStyle BackColor="#FCE4D6" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Remark"
                                        DataField="Remark"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="5%" ItemStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#FCE4D6" />
                                        <HeaderStyle BackColor="#FCE4D6" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="UAT Status"
                                        DataField="UATStatusName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="3%" ItemStyle-Width="3%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#FCE4D6" />
                                        <HeaderStyle BackColor="#FCE4D6" />
                                    </asp:BoundField>

                                    <%--Go Live--%>

                                    <asp:BoundField HeaderText="Planned Date"
                                        DataField="GLPlannedDate"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#C6E0B4" />
                                        <HeaderStyle BackColor="#C6E0B4" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Revised Date"
                                        DataField="GLRevisedDate"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#C6E0B4" />
                                        <HeaderStyle BackColor="#C6E0B4" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Actual Date"
                                        DataField="GLActualDate"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#C6E0B4" />
                                        <HeaderStyle BackColor="#C6E0B4" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Remark"
                                        DataField="Remark"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="5%" ItemStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#C6E0B4" />
                                        <HeaderStyle BackColor="#C6E0B4" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText=" Go Live Status"
                                        DataField="GoLiveStatusName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="3%" ItemStyle-Width="3%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#C6E0B4" />
                                        <HeaderStyle BackColor="#C6E0B4" />
                                    </asp:BoundField>

                                </Columns>
                            </asp:GridView>
                            <br />
                        </li>
                        <%-- <li>
                            <asp:button id="btnviewabapobjectdetailplan" runat="server" text="view abap object detail plan" onclick="btnviewabapobjectdetailplan_click" />
                        </li>--%>
                        <li style="width: 100%;">
                            <br />
                            <br />

                            <asp:LinkButton ID="btnAllDetails" runat="server" Text="CLICK TO SEE STAGE-WISE DETAILS" OnClick="btnAllDetails_Click" ToolTip="CLICK TO SEE STAGE-WISE DETAILS"
                                CssClass="Savebtnsve" Font-Size="Large" Font-Bold="true"></asp:LinkButton>
                        </li>
                        <li style="width: 100%" runat="server" id="colorshades" visible="false">
                            <br />
                            <div style="width: 30px; height: 15px; background-color: #FFFFFF; margin: 5px; display: inline-block; border: solid; border-color: black; border-width: 1px;"></div>
                            <span style="display: inline-block; vertical-align: middle; margin: -17px 0 0 0 !important;">- Not Started |</span>
                            <div style="width: 30px; height: 15px; background-color: #FF4500; margin: 5px; display: inline-block; border: solid; border-color: black; border-width: 1px;"></div>
                            <span style="display: inline-block; vertical-align: middle; margin: -17px 0 0 0 !important;">- Hold | </span>
                            <div style="width: 30px; height: 15px; background-color: #FFA500; margin: 5px; display: inline-block; border: solid; border-color: black; border-width: 1px;"></div>
                            <span style="display: inline-block; vertical-align: middle; margin: -17px 0 0 0 !important;">- Started | </span>
                            <div style="width: 30px; height: 15px; background-color: #90EE90; margin: 5px; display: inline-block; border: solid; border-color: black; border-width: 1px;"></div>
                            <span style="display: inline-block; vertical-align: middle; margin: -17px 0 0 0 !important;">- Submitted | </span>
                            <div style="width: 30px; height: 15px; background-color: #CD5C5C; margin: 5px; display: inline-block; border: solid; border-color: black; border-width: 1px;"></div>
                            <span style="display: inline-block; vertical-align: middle; margin: -17px 0 0 0 !important;">- Delayed | </span>
                            <div style="width: 30px; height: 15px; background-color: #FEFE97; margin: 5px; display: inline-block; border: solid; border-color: black; border-width: 1px;"></div>
                            <span style="display: inline-block; vertical-align: middle; margin: -17px 0 0 0 !important;">- Send For Approval | </span>
                            <div style="width: 30px; height: 15px; background-color: #4CAF50; margin: 5px; display: inline-block; border: solid; border-color: black; border-width: 1px;"></div>
                            <span style="display: inline-block; vertical-align: middle; margin: -17px 0 0 0 !important;">- Approved | </span>
                            <div style="width: 30px; height: 15px; background-color: #ADD8E6; margin: 5px; display: inline-block; border: solid; border-color: black; border-width: 1px;"></div>
                            <span style="display: inline-block; vertical-align: middle; margin: -17px 0 0 0 !important;">- Submit For Functional Testing | </span>
                            <br />
                            <div style="width: 30px; height: 15px; background-color: #3CB371; margin: 5px; display: inline-block; border: solid; border-color: black; border-width: 1px;"></div>
                            <span style="display: inline-block; vertical-align: middle; margin: -17px 0 0 0 !important;">- Passed | </span>
                            <div style="width: 30px; height: 15px; background-color: #FF0000; margin: 5px; display: inline-block; border: solid; border-color: black; border-width: 1px;"></div>
                            <span style="display: inline-block; vertical-align: middle; margin: -17px 0 0 0 !important;">- Failed | </span>
                            <div style="width: 30px; height: 15px; background-color: #006400; margin: 5px; display: inline-block; border: solid; border-color: black; border-width: 1px;"></div>
                            <span style="display: inline-block; vertical-align: middle; margin: -17px 0 0 0 !important;">- Accept | </span>
                            <div style="width: 30px; height: 15px; background-color: #8B0000; margin: 5px; display: inline-block; border: solid; border-color: black; border-width: 1px;"></div>
                            <span style="display: inline-block; vertical-align: middle; margin: -17px 0 0 0 !important;">- Reject | </span>
                            <div style="width: 30px; height: 15px; background-color: #4169E1; margin: 5px; display: inline-block; border: solid; border-color: black; border-width: 1px;"></div>
                            <span style="display: inline-block; vertical-align: middle; margin: -17px 0 0 0 !important;">- Go Live </span>
                        </li>
                        <li class="trvl_grid" id="lirgsdetails" runat="server" visible="false">
                            <br />
                            <asp:LinkButton ID="btnRGS_Details" runat="server" Text="+" OnClick="btnRGS_Details_Click" ToolTip="Browse" CssClass="Savebtnsve"></asp:LinkButton>
                            <span class="rgs" runat="server" visible="true" id="rgs">RGS </span>
                            <asp:GridView ID="gvRGSDetails" runat="server" CssClass="Milestones" DataKeyNames="RGSDetailsId" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                                CellPadding="3" AutoGenerateColumns="False" Width="130%" OnRowDataBound="gvRGSDetails_RowDataBound">
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

                                    <asp:BoundField HeaderText="Consultant"
                                        DataField="RGSConsultant"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="10%" HeaderStyle-Width="10%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="RGSDetailsId"
                                        DataField="RGSDetailsId"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="10%" HeaderStyle-Width="10%"
                                        ItemStyle-BorderColor="Navy" Visible="False" />

                                    <asp:BoundField HeaderText="Development Description"
                                        DataField="Development_Desc"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="20%" HeaderStyle-Width="20%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Status"
                                        DataField="RGSStatusName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Module"
                                        DataField="ModuleDesc"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Interface"
                                        DataField="Interface"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="FRICE Category"
                                        DataField="FCategoryName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="6%" HeaderStyle-Width="6%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Priority/ Order"
                                        DataField="Priority"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="3%" HeaderStyle-Width="3%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Priority"
                                        DataField="PriorityName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Complexity"
                                        DataField="ComplexityName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Preparation Start Date"
                                        DataField="PlannedPrepStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Preparation Finish  Date"
                                        DataField="PlannedPrepFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Submission Date"
                                        DataField="PlannedSubmit"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Approval Date"
                                        DataField="PlannedApprove"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Revised/ Actual Preparation Start Date"
                                        DataField="RevActualPrepStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Revised/ Actual Preparation Finish Date"
                                        DataField="RevActualPrepFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Revised/ Actual Submit Date"
                                        DataField="RevActualSubmit"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Revised/ Actual Approve Date"
                                        DataField="RevActualApprove"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Remark"
                                        DataField="Remark"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%" HeaderStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />




                                </Columns>
                            </asp:GridView>
                            <br />
                        </li>
                        <li></li>

                        <li class="trvl_grid" id="lifsdetails" runat="server" visible="false">
                            <asp:LinkButton ID="btnFS_Details" runat="server" Text="+" OnClick="btnFS_Details_Click" ToolTip="Browse" CssClass="Savebtnsve"></asp:LinkButton>
                            <span class="fs" runat="server" visible="true" id="fs">FS</span>
                            <asp:GridView ID="gvFSDetails" CssClass="Milestones" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                                Width="130%" EditRowStyle-Wrap="true" DataKeyNames="FSDetailsId" OnRowDataBound="gvFSDetails_RowDataBound">
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

                                    <asp:BoundField HeaderText="Consultant"
                                        DataField="FSConsultant"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="10%" HeaderStyle-Width="10%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Development Description"
                                        DataField="Development_Desc"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="20%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Status"
                                        DataField="FSStatusName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Module"
                                        DataField="ModuleDesc"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Interface"
                                        DataField="Interface"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="FRICE Category"
                                        DataField="FCategoryName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="6%" HeaderStyle-Width="6%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Priority/ Order"
                                        DataField="Priority"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="3%" HeaderStyle-Width="3%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Priority"
                                        DataField="PriorityName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Complexity"
                                        DataField="ComplexityName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Start  Date"
                                        DataField="FSPlannedStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Finish Date"
                                        DataField="FSPlannedFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="FS Actual Start Date"
                                        DataField="FSActualStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="FS Revised Finish Date"
                                        DataField="FSRevisedFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="FS Actual Finish Date"
                                        DataField="FSActualFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Remark"
                                        DataField="Remark"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%" HeaderStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />



                                </Columns>
                            </asp:GridView>
                            <br />
                        </li>
                        <li></li>


                        <li class="trvl_grid" id="liabapdev" runat="server" visible="false">
                            <asp:LinkButton ID="btnABAPDev_Details" runat="server" Text="+" OnClick="btnABAPDev_Details_Click" ToolTip="Browse" CssClass="Savebtnsve"></asp:LinkButton>
                            <span class="abapdev" runat="server" visible="true" id="abapdev">ABAP Object Development</span>
                            <asp:GridView ID="gvABAPDevDetails" CssClass="Milestones" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                AutoGenerateColumns="False" Width="130%" EditRowStyle-Wrap="true" DataKeyNames="ABAPDetailsId">
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

                                    <asp:BoundField HeaderText="Consultant"
                                        DataField="ABAPDevConsultant"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="10%" HeaderStyle-Width="10%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Development Description"
                                        DataField="Development_Desc"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="20%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Module"
                                        DataField="ModuleDesc"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Interface"
                                        DataField="Interface"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="FRICE Category"
                                        DataField="FCategoryName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="6%" HeaderStyle-Width="6%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Priority/ Order"
                                        DataField="Priority"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="3%" HeaderStyle-Width="3%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Priority"
                                        DataField="PriorityName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Complexity"
                                        DataField="ComplexityName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Reusable Client Name"
                                        DataField="ReusableClientName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="7%" HeaderStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Reusable Status"
                                        DataField="ResuableStatusName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Reusable Addtional Efforts"
                                        DataField="ReusableAdditonalEffort"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Reusable Percentage"
                                        DataField="ReusablePercent"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="7%" HeaderStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Reusable Reamarks"
                                        DataField="ReusableDetailsRemarks"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="7%" HeaderStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="TCode"
                                        DataField="Custom_Tcode"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="7%" HeaderStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />


                                    <asp:BoundField HeaderText="ABAP Consultant"
                                        DataField="PlannedABAPerId"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%" HeaderStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Duration (Days)"
                                        DataField="DevDuaration"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Start Date"
                                        DataField="ABAPPlannedStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Finish Date"
                                        DataField="ABAPPlannedFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Revised Start Date"
                                        DataField="ABAPRevisedStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Revised Finish Date"
                                        DataField="ABAPRevisedFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />


                                    <asp:BoundField HeaderText="Actual Start Date"
                                        DataField="ABAPActualStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />


                                    <asp:BoundField HeaderText="Actual Finish Date"
                                        DataField="ABAPActualFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%" HeaderStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Remark"
                                        DataField="Remark"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%" HeaderStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Status"
                                        DataField="ABAPDevStatusName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />


                                    <asp:BoundField HeaderText="ABAP Functional Status"
                                        DataField="ABAPFunctionalStatusName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%" HeaderStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" Visible="false" />


                                </Columns>
                            </asp:GridView>
                            <br />
                        </li>
                        <li></li>


                        <li class="trvl_grid" id="lihbttest" runat="server" visible="false">
                            <asp:LinkButton ID="btnHBT_Details" runat="server" Text="+" OnClick="btnHBT_Details_Click" ToolTip="Browse" CssClass="Savebtnsve"></asp:LinkButton>
                            <span class="hbttest" runat="server" visible="true" id="hbttest">HBT Testing</span>
                            <asp:GridView ID="gvHBTTestDetails" CssClass="Milestones" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                AutoGenerateColumns="False" Width="130%" EditRowStyle-Wrap="true" DataKeyNames="HBTDetailsId">
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

                                    <asp:BoundField HeaderText="Consultant"
                                        DataField="HBTConsultant"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="10%" HeaderStyle-Width="10%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Development Description"
                                        DataField="Development_Desc"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="20%" HeaderStyle-Width="20%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Module"
                                        DataField="ModuleDesc"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Interface"
                                        DataField="Interface"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="FRICE Category"
                                        DataField="FCategoryName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="6%" HeaderStyle-Width="6%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Priority/ Order"
                                        DataField="Priority"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="3%" HeaderStyle-Width="3%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Priority"
                                        DataField="PriorityName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Complexity"
                                        DataField="ComplexityName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Start Date"
                                        DataField="HBTPlannedStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Finish Date"
                                        DataField="HBTPlannedFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Revised Start Date"
                                        DataField="hbtRevisedStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Revised Finish Date"
                                        DataField="hbtRevisedFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Actual Start Date"
                                        DataField="hbtActualStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Actual Finish Date"
                                        DataField="hbtActualFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Remark"
                                        DataField="Remark"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%" HeaderStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Status"
                                        DataField="HBTTestStatusName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                </Columns>
                            </asp:GridView>
                            <br />

                        </li>
                        <li></li>


                        <li class="trvl_grid" id="lictmtest" runat="server" visible="false">
                            <asp:LinkButton ID="btnCTM_Details" runat="server" Text="+" OnClick="btnCTM_Details_Click" ToolTip="Browse" CssClass="Savebtnsve"></asp:LinkButton>
                            <span class="ctmtesting" runat="server" visible="true" id="ctmtesting">CTM Testing</span>
                            <asp:GridView ID="gvCTMTestDetails" CssClass="Milestones" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                AutoGenerateColumns="False" Width="120%" EditRowStyle-Wrap="true" DataKeyNames="CTMDetailsId">
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

                                    <asp:BoundField HeaderText="Consultant"
                                        DataField="CTMConsultant"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="10%" HeaderStyle-Width="10%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Client Person"
                                        DataField="CTMName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="10%" HeaderStyle-Width="10%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Development Description"
                                        DataField="Development_Desc"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="20%" HeaderStyle-Width="20%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Module"
                                        DataField="ModuleDesc"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Interface"
                                        DataField="Interface"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="FRICE Category"
                                        DataField="FCategoryName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="6%" HeaderStyle-Width="6%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Priority/ Order"
                                        DataField="Priority"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Priority"
                                        DataField="PriorityName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Complexity"
                                        DataField="ComplexityName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="CTM Duration (Days)"
                                        DataField="Duration"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="8%" HeaderStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Start Date"
                                        DataField="CTMPlannedStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Finish Date"
                                        DataField="CTMPlannedFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Revised Start Date"
                                        DataField="CTMRevisedStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Finish Date"
                                        DataField="CTMRevisedFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Actual Start Date"
                                        DataField="CTMActualStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Actual Finish Date"
                                        DataField="CTMActualFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Duration"
                                        DataField="Duration"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Days"
                                        DataField="PlannedDays"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Test Case File"
                                        DataField="TestCaseFileAttached"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="10%" HeaderStyle-Width="10%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Remark"
                                        DataField="Remark"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="10%" HeaderStyle-Width="10%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Status"
                                        DataField="CTMTestStatusName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                </Columns>
                            </asp:GridView>
                            <br />
                        </li>
                        <li></li>


                        <li class="trvl_grid" id="liuatsignoff" runat="server" visible="false">
                            <asp:LinkButton ID="btnUATSignOff_Details" runat="server" Text="+" OnClick="btnUATSignOff_Details_Click" ToolTip="Browse" CssClass="Savebtnsve"></asp:LinkButton>
                            <span class="uatsignoff" runat="server" visible="true" id="uatsignoff">UAT Sign Off</span>
                            <asp:GridView ID="gvUATSignOffDetails" CssClass="Milestones" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="true" DataKeyNames="UATSignOffId">
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
                                        ItemStyle-Width="31%" HeaderStyle-Width="31%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Module"
                                        DataField="ModuleDesc"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Interface"
                                        DataField="Interface"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="7%" HeaderStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="FRICE Category"
                                        DataField="FCategoryName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%" HeaderStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Priority/ Order"
                                        DataField="Priority"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Priority"
                                        DataField="PriorityName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Complexity"
                                        DataField="ComplexityName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="7%" HeaderStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Date"
                                        DataField="UATPlanned_Date"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="7%" HeaderStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Revised Date"
                                        DataField="UATRevisedDate"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="7%" HeaderStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Actual Date"
                                        DataField="UATActualDate"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="7%" HeaderStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Remark"
                                        DataField="Remark"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%" HeaderStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="UAT Status"
                                        DataField="UATStatusName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%" HeaderStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                </Columns>
                            </asp:GridView>
                            <br />
                        </li>
                        <li></li>


                        <li class="trvl_grid" id="ligolive" runat="server" visible="false">
                            <asp:LinkButton ID="btnGoLive_Details" runat="server" Text="+" OnClick="btnGoLive_Details_Click" ToolTip="Browse" CssClass="Savebtnsve"></asp:LinkButton>
                            <span class="golive" runat="server" visible="true" id="golive">Go-Live</span>
                            <asp:GridView ID="gvGoLiveDetails" CssClass="Milestones" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="true" DataKeyNames="ABAPODId">
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
                                        ItemStyle-Width="31%" HeaderStyle-Width="31%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Module"
                                        DataField="ModuleDesc"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Interface"
                                        DataField="Interface"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="7%" HeaderStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="FRICE Category"
                                        DataField="FCategoryName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%" HeaderStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Priority/ Order"
                                        DataField="Priority"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Priority"
                                        DataField="PriorityName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Complexity"
                                        DataField="ComplexityName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="7%" HeaderStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Date"
                                        DataField="GLPlannedDate"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%" HeaderStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Revised Date"
                                        DataField="GLRevisedDate"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%" HeaderStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Actual Date"
                                        DataField="GLActualDate"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%" HeaderStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Remark"
                                        DataField="Remark"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="10%" HeaderStyle-Width="10%"
                                        ItemStyle-BorderColor="Navy" />

                                </Columns>
                            </asp:GridView>
                            <br />
                        </li>
                        <li></li>

                        <li class="trvl_Approver">
                            <br />
                            <asp:GridView ID="DgvApprover" CssClass="Milestones" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="75%">
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
                                    <asp:BoundField HeaderText="Approver Name"
                                        DataField="ApproverName"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="left"
                                        ItemStyle-Width="33%" HeaderStyle-Width="33%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Status"
                                        DataField="Action"
                                        ItemStyle-HorizontalAlign="left"
                                        ItemStyle-Width="12%" HeaderStyle-Width="12%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Approved on"
                                        DataField="Rel_Date"
                                        ItemStyle-HorizontalAlign="left"
                                        ItemStyle-Width="15%" HeaderStyle-Width="15%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Approver Remarks"
                                        DataField="Remarks"
                                        ItemStyle-HorizontalAlign="left"
                                        ItemStyle-Width="37%" HeaderStyle-Width="37%"
                                        ItemStyle-BorderColor="Navy" />

                                    <%--<asp:BoundField HeaderText="APPR_ID"
                                        DataField="Appr_ID"
                                        ItemStyle-HorizontalAlign="center"
                                        ItemStyle-Width="1%"
                                        ItemStyle-BorderColor="Navy"
                                        Visible="false" />

                                    <asp:BoundField HeaderText="Emp_Emailaddress"
                                        DataField="Emp_Emailaddress"
                                        ItemStyle-HorizontalAlign="center"
                                        ItemStyle-Width="1%"
                                        ItemStyle-BorderColor="Navy"
                                        Visible="false" />--%>

                                    <asp:BoundField HeaderText="A_EMP_CODE"
                                        DataField="ApprovedBy"
                                        ItemStyle-HorizontalAlign="center"
                                        ItemStyle-Width="1%" HeaderStyle-Width="1%"
                                        ItemStyle-BorderColor="Navy"
                                        Visible="false" />
                                </Columns>
                            </asp:GridView>
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

    <asp:HiddenField ID="ABAPODId" runat="server" />
    <asp:HiddenField ID="hdnABAPODIdId" runat="server" />

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
                freezesize: 3,
                headerrowcount: 1
            };

            applyGridViewScroll('#MainContent_gvRGSDetails', gridOptions);
            applyGridViewScroll('#MainContent_gvFSDetails', gridOptions);
            applyGridViewScroll('#MainContent_gvABAPDevDetails', gridOptions);
            applyGridViewScroll('#MainContent_gvHBTTestDetails', gridOptions);
            applyGridViewScroll('#MainContent_gvCTMTestDetails', gridOptions);
            applyGridViewScroll('#MainContent_gvDetailPlan', gridOptions);
            applyGridViewScroll('#MainContent_gvUATSignOffDetails', gridOptions);
            applyGridViewScroll('#MainContent_gvGoLiveDetails', gridOptions);



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

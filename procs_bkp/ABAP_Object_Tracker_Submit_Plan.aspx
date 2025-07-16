<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" EnableViewState="true"
    MaintainScrollPositionOnPostback="true" CodeFile="ABAP_Object_Tracker_Submit_Plan.aspx.cs" Inherits="ABAP_Object_Tracker_Submit_Plan" EnableSessionState="True" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <style>
        #MainContent_lnk_Index, #MainContent_updatergs, #MainContent_lnk_addconsultantfs,
        #MainContent_lnk_addconsultantabapdev, #MainContent_lnk_addconsultanthbttest, #MainContent_lnk_addconsultantctmtest {
            background: #3D1956;
            color: #febf39 !important;
            padding: 8px 18px;
            margin: 26px !important;
        }

        #MainContent_addconsultant_rgs_btnSave_Click, #MainContent_updatergs, #MainContent_lnk_addconsultantfs, #MainContent_lnk_addconsultantabapdev, #MainContent_lnk_addconsultanthbttest, #MainContent_lnk_addconsultantctmtest {
            float: left;
            background: #3D1956;
            color: #febf39;
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

        .clslnkDownload:hover {
            text-decoration: underline !important;
            color: blue !important;
            cursor: pointer !important;
        }



        a#MainContent_lnk_addconsultantctmtest {
            margin: -25px 0 0 30px !important;
        }

        a#MainContent_updatergs {
            margin: 0 0 -10px 0px !important;
        }

        a#MainContent_lnk_addconsultantfs {
            margin: 0 0 -10px 0 !important;
        }

        a#MainContent_lnk_addconsultantabapdev {
            margin: 0 0 -10px 0 !important;
        }

        a#MainContent_lnk_addconsultanthbttest {
            margin: 0 0 -10px 0 !important;
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
            background-color: #ADD8E6; /* Light Blue */
            color: #000000; /* Black text */
        }

        .green-background {
            background-color: #90EE90; /* Light Green */
            color: #000000; /* Black text */
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

        .clsgvDetailPlan th {
            font-weight: bold !important;
        }

        .Milestones th {
            font-weight: bold !important;
        }

        #content-container .intern-padding {
            padding: 0px 0px !important;
            cursor: auto !important;
        }

        #MainContent_LinkButton1 {
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
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="View & Update ABAP Object Plan"></asp:Label>
                    </span>
                </div>
                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="Savebtnsve" PostBackUrl="~/procs/ABAP_Object_Tracker_Index.aspx" BackColor="#3D1956" Style="margin: 5px 20px 0px 0px !important;"><%=System.Configuration.ConfigurationManager.AppSettings["ABAPObjectPageTitle"]%></asp:LinkButton>

                <div class="edit-contact">

                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" Visible="false"></asp:LinkButton>
                        </div>
                    </div>
                    <asp:Label runat="server" ID="lbl_ctmtest_Error" Visible="True" Style="color: red; text-align: center;"></asp:Label>

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
                        <li>
                            <asp:LinkButton ID="lnkDownload" runat="server" OnClick="DownloadFile" Text="Download ABAP Object Plan" CssClass="clslnkDownload" />
                            <%--<a id="lnkDownload" onclick="DownloadFile">Download ABAP Object Plan</a>--%>
                            <%--<asp:Button id="btnDownload" runat="server" Text="Download ABAP Object Plan" OnClick="DownloadFile" />--%>
                            <%--<a id="btnDownload" runat="server" onclick="DownloadFile" >Download ABAP Object Plan</a>--%>
                            
                        </li>
                        <%-- <li>
                            <asp:Button ID="btnViewABAPObjectDetailPlan" runat="server" Text="View ABAP Object Detail Plan" OnClick="btnViewABAPObjectDetailPlan_Click" />
                        </li>--%>
                        <li style="width: 70%">
                            <br />
                            <div style="width: 30px; height: 15px; background-color: #FFE699; margin: 5px; display: inline-block;"></div>
                            <span style="display: inline-block; vertical-align: middle; margin: -17px 0 0 0 !important;">- RGS | </span>
                            <div style="width: 30px; height: 15px; background-color: #A9D08E; margin: 5px; display: inline-block;"></div>
                            <span style="display: inline-block; vertical-align: middle; margin: -17px 0 0 0 !important;">- FS | </span>
                            <div style="width: 30px; height: 15px; background-color: #F4B084; margin: 5px; display: inline-block;"></div>
                            <span style="display: inline-block; vertical-align: middle; margin: -17px 0 0 0 !important;">- ABAP Dev | </span>
                            <div style="width: 30px; height: 15px; background-color: #8EA9DB; margin: 5px; display: inline-block;"></div>
                            <span style="display: inline-block; vertical-align: middle; margin: -17px 0 0 0 !important;">- HBT Test | </span>
                            <div style="width: 30px; height: 15px; background-color: #BF8F00; margin: 5px; display: inline-block;"></div>
                            <span style="display: inline-block; vertical-align: middle; margin: -17px 0 0 0 !important;">- CTM Test</span>

                        </li>
                        <li>
                            <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center; margin: 0px 0px 0px 10px !important;"></asp:Label>
                        </li>

                        <li class="trvl_grid" id="li1" runat="server">
                            <br />
                            <span class="rgs" runat="server" visible="true" id="Span2">ABAP Object Detail Plan</span>
                            <asp:GridView ID="gvDetailPlan" runat="server" CssClass="clsgvDetailPlan" DataKeyNames="RGSDetailsId" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                                CellPadding="3" AutoGenerateColumns="False">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#C7D3D4" ForeColor="#3D1956" />
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
                                        ItemStyle-BorderColor="Navy"></asp:BoundField>

                                    <asp:BoundField HeaderText="Module"
                                        DataField="ModuleDesc"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderColor="Navy"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"></asp:BoundField>

                                    <asp:BoundField HeaderText="Interface"
                                        DataField="Interface"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy"></asp:BoundField>

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
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy"></asp:BoundField>

                                    <asp:BoundField HeaderText="Priority"
                                        DataField="PriorityName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy"></asp:BoundField>

                                    <asp:BoundField HeaderText="Complexity"
                                        DataField="ComplexityName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy"></asp:BoundField>

                                    <asp:BoundField HeaderText="RGS Consultant"
                                        DataField="RGSConsultant"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="4%" ItemStyle-Width="4%"
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
                                        HeaderStyle-Width="3%" ItemStyle-Width="3%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#FFE699" />
                                        <HeaderStyle BackColor="#FFE699" />
                                    </asp:BoundField>

                                    <%--FS Sections--%>
                                    <asp:BoundField HeaderText="FS Consultant"
                                        DataField="FSConsultant"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="4%" ItemStyle-Width="4%"
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

                                    <asp:BoundField HeaderText="FS Actual Start Date"
                                        DataField="FSActualStart"
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

                                    <asp:BoundField HeaderText="FS Actual Finish Date"
                                        DataField="FSActualFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#A9D08E" />
                                        <HeaderStyle BackColor="#A9D08E" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText=" FS Remark"
                                        DataField="Remark"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="3%" ItemStyle-Width="3%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#A9D08E" />
                                        <HeaderStyle BackColor="#A9D08E" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText=" FS Status"
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
                                        HeaderStyle-Width="4%" ItemStyle-Width="4%"
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


                                    <asp:BoundField HeaderText="ABAP Planned Finish Date"
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
                                        HeaderStyle-Width="4%" ItemStyle-Width="4%"
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
                                        HeaderStyle-Width="4%" ItemStyle-Width="4%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#BF8F00" />
                                        <HeaderStyle BackColor="#BF8F00" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Client Person"
                                        DataField="CTMName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="10%" ItemStyle-Width="10%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#BF8F00" />
                                        <HeaderStyle BackColor="#BF8F00" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="CTM Duration (Days)"
                                        DataField="Duration"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        HeaderStyle-Width="3%" ItemStyle-Width="3%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#BF8F00" />
                                        <HeaderStyle BackColor="#BF8F00" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Planned Start Date"
                                        DataField="CTMPlannedStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="2%" ItemStyle-Width="2%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#BF8F00" />
                                        <HeaderStyle BackColor="#BF8F00" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Planned Finish Date"
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

                                    <asp:BoundField HeaderText="Planned Finish Date"
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
                                        HeaderStyle-Width="3%" ItemStyle-Width="3%"
                                        ItemStyle-BorderColor="Navy">
                                        <ItemStyle BackColor="#BF8F00" />
                                        <HeaderStyle BackColor="#BF8F00" Font-Bold="True" />
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
                        </li>
                        <li style="width: 100%;">
                            <br />
                            <br />

                            <asp:LinkButton ID="btnAllDetails" runat="server" Text="CLICK HERE TO SEE STAGE-WISE DETAILS" OnClick="btnAllDetails_Click" ToolTip="CLICK TO SEE STAGE-WISE DETAILS"
                                CssClass="Savebtnsve" Font-Size="Large" Font-Bold="true"></asp:LinkButton>
                        </li>


                        <li class="trvl_grid" id="lirgsdetails" runat="server" visible="false">
                            <br />
                            <br />
                            <asp:LinkButton ID="btnRGS_Details" runat="server" Text="+" OnClick="btnRGS_Details_Click" ToolTip="View RGS Details" CssClass="Savebtnsve"></asp:LinkButton>
                            <span class="rgs" runat="server" visible="true" id="rgs">View RGS Details </span>
                            <asp:GridView ID="gvRGSDetails" runat="server" CssClass="Milestones" DataKeyNames="RGSDetailsId" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                                CellPadding="3" AutoGenerateColumns="False" Width="110%" OnRowDataBound="RGS_RowDataBound">
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
                                    <asp:TemplateField ItemStyle-Width="3%" Visible="false">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="RGSchkSelect" runat="server" ItemStyle-HorizontalAlign="left"
                                                HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="3%" HeaderStyle-Width="3%"
                                                ItemStyle-BorderColor="Navy" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

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
                                        ItemStyle-Width="30%" HeaderStyle-Width="30%"
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
                                        ItemStyle-Width="7%" HeaderStyle-Width="7%"
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

                                    <asp:BoundField HeaderText="Planned Preparation Start Date"
                                        DataField="PlannedPrepStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="6%" HeaderStyle-Width="6%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Preparation Finish  Date"
                                        DataField="PlannedPrepFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="6%" HeaderStyle-Width="6%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Submission Date"
                                        DataField="PlannedSubmit"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="6%" HeaderStyle-Width="6%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Approval Date"
                                        DataField="PlannedApprove"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="6%" HeaderStyle-Width="6%"
                                        ItemStyle-BorderColor="Navy" />

                                </Columns>
                            </asp:GridView>
                        </li>

                        <li class="trvl_date" runat="server" visible="false">
                            <div id="divrgsconsultant" runat="server">
                                <asp:Label runat="server" ID="lbl_RGSConsultant_Error" Visible="True" Style="color: red; text-align: center;"></asp:Label>
                                <br />
                                <span>Functional Consultant</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                <br />
                                <asp:DropDownList runat="server" ID="DDLRGSFunctionalConsultant" AutoPostBack="false" CssClass="DropdownListSearch">
                                </asp:DropDownList>
                            </div>
                        </li>

                        <li class="trvl_date" runat="server" visible="false">
                            <asp:LinkButton ID="updatergs" runat="server" Text="Update RGS" ToolTip="Update RGS" OnClick="addconsultant_rgs_btnSave_Click">Update RGS</asp:LinkButton>
                        </li>



                        <li class="trvl_grid" id="lifsdetails" runat="server" visible="false">
                            <br />
                            <asp:LinkButton ID="btnFS_Details" runat="server" Text="+" OnClick="btnFS_Details_Click" ToolTip="View FS Details" CssClass="Savebtnsve"></asp:LinkButton>
                            <span class="fs" runat="server" visible="true" id="fs">View FS Details</span>
                            <asp:GridView ID="gvFSDetails" CssClass="Milestones" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                                Width="110%" EditRowStyle-Wrap="true" DataKeyNames="FSDetailsId" OnRowDataBound="FS_RowDataBound">
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
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="FSchkSelect" runat="server" ItemStyle-HorizontalAlign="left"
                                                HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="3%" HeaderStyle-Width="3%"
                                                ItemStyle-BorderColor="Navy" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

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
                                        ItemStyle-Width="30%" HeaderStyle-Width="30%"
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
                                        ItemStyle-Width="7%" HeaderStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Priority/ Order"
                                        DataField="Priority"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="4%" HeaderStyle-Width="4%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Priority"
                                        DataField="PriorityName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="7%" HeaderStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Complexity"
                                        DataField="ComplexityName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="9%" HeaderStyle-Width="9%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Start  Date"
                                        DataField="FSPlannedStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%" HeaderStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Finish Date"
                                        DataField="FSPlannedFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%" HeaderStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                </Columns>
                            </asp:GridView>

                        </li>

                        <li class="trvl_date" runat="server" visible="false">
                            <div id="divfsconsultant" runat="server">
                                <asp:Label runat="server" ID="lbl_fs_Error" Visible="True" Style="color: red; text-align: center;"></asp:Label>
                                <br />
                                <span>FS Functional Consultant</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                <br />
                                <asp:DropDownList runat="server" ID="DDLFSFunctionalConsultant" AutoPostBack="false" CssClass="DropdownListSearch">
                                </asp:DropDownList>
                            </div>
                        </li>

                        <li runat="server" visible="false">
                            <asp:LinkButton ID="lnk_addconsultantfs" runat="server" Text="Update FS" ToolTip="Update FS" CssClass="Savebtnsve" OnClick="addconsultant_fs_btnSave_Click">Update FS</asp:LinkButton>
                        </li>



                        <li class="trvl_grid" id="liabapdev" runat="server" visible="false">
                            <br />
                            <asp:LinkButton ID="btnABAPDev_Details" runat="server" Text="+" OnClick="btnABAPDev_Details_Click" ToolTip="View ABAP Development Details" CssClass="Savebtnsve"></asp:LinkButton>
                            <span class="abapdev" runat="server" visible="true" id="abapdev">View ABAP Development Details</span>
                            <br />
                            <asp:GridView ID="gvABAPDevDetails" CssClass="Milestones" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                AutoGenerateColumns="False" Width="110%" EditRowStyle-Wrap="true" DataKeyNames="ABAPDetailsId" OnRowDataBound="ABAPDev_RowDataBound">
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
                                    <asp:TemplateField ItemStyle-Width="3%" Visible="false">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ABAPDevchkSelect" runat="server" ItemStyle-HorizontalAlign="left"
                                                HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="3%" HeaderStyle-Width="3%"
                                                ItemStyle-BorderColor="Navy" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField HeaderText="Plan ABAP Consultant"
                                        DataField="ABAPDevConsultant"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="10%" HeaderStyle-Width="10%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Development Description"
                                        DataField="Development_Desc"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="30%" HeaderStyle-Width="30%"
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
                                        ItemStyle-Width="7%" HeaderStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Priority/ Order"
                                        DataField="Priority"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="4%" HeaderStyle-Width="4%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Priority"
                                        DataField="PriorityName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="7%" HeaderStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Complexity"
                                        DataField="ComplexityName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="7%" HeaderStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Duration (Days)"
                                        DataField="DevDuaration"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="6%" HeaderStyle-Width="6%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Start Date"
                                        DataField="ABAPPlannedStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="6%" HeaderStyle-Width="6%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Finish Date"
                                        DataField="ABAPPlannedFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="6%" HeaderStyle-Width="6%"
                                        ItemStyle-BorderColor="Navy" />

                                </Columns>
                            </asp:GridView>

                        </li>
                        <li class="trvl_date" runat="server" visible="false">
                            <div id="divAABAPDevconsultant" runat="server">
                                <asp:Label runat="server" ID="lbl_abapdev_Error" Visible="True" Style="color: red; text-align: center;"></asp:Label>
                                <br />
                                <span>Plan ABAP Consultant</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                <br />
                                <asp:DropDownList runat="server" ID="DDLPlanABAPConsultant" AutoPostBack="false" CssClass="DropdownListSearch">
                                </asp:DropDownList>
                            </div>
                        </li>
                        <li class="trvl_date" runat="server" visible="false">
                            <asp:LinkButton ID="lnk_addconsultantabapdev" runat="server" Text="Update ABAP Consultant" ToolTip="Update ABAP Consultant" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();" OnClick="addconsultant_abapdev_btnSave_Click">Update ABAP Consultant</asp:LinkButton>
                        </li>


                        <li class="trvl_grid" id="lihbttest" runat="server" visible="false">
                            <br />
                            <asp:LinkButton ID="btnHBT_Details" runat="server" Text="+" OnClick="btnHBT_Details_Click" ToolTip="View HBT Test Details" CssClass="Savebtnsve"></asp:LinkButton>
                            <span class="abapdev" runat="server" visible="true" id="Span1">View HBT Test Details</span>
                            <br />
                            <asp:GridView ID="gvHBTTestDetails" CssClass="Milestones" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                AutoGenerateColumns="False" Width="110%" EditRowStyle-Wrap="true" DataKeyNames="HBTDetailsId" OnRowDataBound="HBT_RowDataBound">
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
                                    <asp:TemplateField ItemStyle-Width="3%" Visible="false">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="HBTchkSelect" runat="server" ItemStyle-HorizontalAlign="left"
                                                HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="3%" HeaderStyle-Width="3%"
                                                ItemStyle-BorderColor="Navy" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

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
                                        ItemStyle-Width="30%" HeaderStyle-Width="30%"
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
                                        ItemStyle-Width="7%" HeaderStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Priority/ Order"
                                        DataField="Priority"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="4%" HeaderStyle-Width="5%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Priority"
                                        DataField="PriorityName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="7%" HeaderStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Complexity"
                                        DataField="ComplexityName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="9%" HeaderStyle-Width="9%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Start Date"
                                        DataField="HBTPlannedStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%" HeaderStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Finish Date"
                                        DataField="HBTPlannedFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%" HeaderStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                </Columns>
                            </asp:GridView>

                        </li>
                        <li class="trvl_date" runat="server" visible="false">
                            <div runat="server" id="divhbtconsultant">
                                <asp:Label runat="server" ID="lbl_hbttest_Error" Visible="True" Style="color: red; text-align: center;"></asp:Label>
                                <br />
                                <span>Plan Consultant (For Testing)</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                <br />
                                <asp:DropDownList runat="server" ID="DDLPlanConsultantTesting" AutoPostBack="false" CssClass="DropdownListSearch">
                                </asp:DropDownList>
                            </div>
                        </li>

                        <li class="trvl_date" runat="server" visible="false">
                            <asp:LinkButton ID="lnk_addconsultanthbttest" runat="server" Text="Update Consultant Testing" ToolTip="Update Consultant Testing" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();" OnClick="addconsultant_hbttest_btnSave_Click">Update Consultant Testing</asp:LinkButton>
                        </li>


                        <li class="trvl_grid" id="lictmtest" runat="server" visible="false">
                            <br />
                            <asp:LinkButton ID="btnCTM_Details" runat="server" Text="+" OnClick="btnCTM_Details_Click" ToolTip="View CTM Test Details" CssClass="Savebtnsve"></asp:LinkButton>
                            <span class="ctmtesting" runat="server" visible="true" id="ctmtesting">View CTM Test Details</span>
                            <br />
                            <asp:GridView ID="gvCTMTestDetails" CssClass="Milestones" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                AutoGenerateColumns="False" Width="120%" EditRowStyle-Wrap="true" DataKeyNames="CTMDetailsId" OnRowDataBound="CTM_RowDataBound">
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
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CTMchkSelect" runat="server" ItemStyle-HorizontalAlign="left"
                                                HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="4%" HeaderStyle-Width="4%"
                                                ItemStyle-BorderColor="Navy" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField HeaderText="Client Person"
                                        DataField="CTMName"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="15%" HeaderStyle-Width="15%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Consultant"
                                        DataField="CTMConsultant"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="10%" HeaderStyle-Width="10%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Development Description"
                                        DataField="Development_Desc"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="30%" HeaderStyle-Width="30%"
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
                                        ItemStyle-Width="4%" HeaderStyle-Width="4%"
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
                                        ItemStyle-Width="6%" HeaderStyle-Width="6%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Start Date"
                                        DataField="CTMPlannedStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="6%" HeaderStyle-Width="6%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Finish Date"
                                        DataField="CTMPlannedFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="6%" HeaderStyle-Width="6%"
                                        ItemStyle-BorderColor="Navy" />

                                </Columns>
                            </asp:GridView>

                        </li>
                        <li class="trvl_date" runat="server" visible="false" id="lictmname">
                            <div runat="server" id="divCTMName">
                                <br />
                                <span>Client Person</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="txt_CTMName" runat="server" MaxLength="50"></asp:TextBox>
                            </div>
                        </li>
                        <li runat="server" visible="false" id="lictmmail">
                            <div runat="server" id="divCTMEmail">
                                <span>Client Email Id</span>
                                <br />

                                <asp:TextBox AutoComplete="off" ID="txt_CTMEmail" runat="server" MaxLength="100"></asp:TextBox>
                                <br />
                            </div>
                        </li>

                        <li class="trvl_date" runat="server" visible="false" id="liupdatectmdetails">
                            <asp:LinkButton ID="lnk_addconsultantctmtest" runat="server" Text="Update RGS" ToolTip="Update CTM Testing" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();" OnClick="addconsultant_ctmtest_btnSave_Click">Update CTM Testing</asp:LinkButton>
                        </li>

                        <li class="trvl_grid" id="liuatsignoff" runat="server" visible="false">
                            <br />
                            <asp:LinkButton ID="btnUATSignOff_Details" runat="server" Text="+" OnClick="btnUATSignOff_Details_Click" ToolTip="View UAT Sign Off Details" CssClass="Savebtnsve"></asp:LinkButton>
                            <span class="uatsignoff" runat="server" visible="true" id="uatsignoff">View UAT Sign Off Details</span>
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
                                        ItemStyle-Width="30%" HeaderStyle-Width="30%"
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
                                        ItemStyle-Width="7%" HeaderStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Priority/ Order"
                                        DataField="Priority"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="4%" HeaderStyle-Width="4%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Priority"
                                        DataField="PriorityName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="7%" HeaderStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Complexity"
                                        DataField="ComplexityName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="7%" HeaderStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="UAT Planned Date"
                                        DataField="UATPlanned_Date"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="6%" HeaderStyle-Width="6%"
                                        ItemStyle-BorderColor="Navy" />

                                </Columns>
                            </asp:GridView>
                        </li>
                        <%-- <li class="trvl_date">
                            <br />
                            <span>UAT Sign Off By</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList runat="server" ID="DDLUATConsultant" AutoPostBack="false" CssClass="DropdownListSearch">
                            </asp:DropDownList>
                            <asp:TextBox AutoComplete="off" ID="TextBox7" runat="server" CssClass="Dropdown" Visible="false"></asp:TextBox>
                            <br />
                        </li>

                        <li class="trvl_date">
                            <asp:LinkButton ID="LinkButton5" runat="server" Text="Update RGS" ToolTip="Update UAT Sign Off By" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();" OnClick="updatergs_btnSave_Click">Update UAT Sign Off By</asp:LinkButton>
                        </li>--%>

                        <li class="trvl_grid" id="ligolive" runat="server" visible="false">
                            <br />
                            <asp:LinkButton ID="btnGoLive_Details" runat="server" Text="+" OnClick="btnGoLive_Details_Click" ToolTip="View Go-Live" CssClass="Savebtnsve"></asp:LinkButton>

                            <span class="golive" runat="server" visible="true" id="golive">View Go-Live</span>
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
                                        ItemStyle-Width="30%" HeaderStyle-Width="30%"
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
                                        ItemStyle-Width="7%" HeaderStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Priority/ Order"
                                        DataField="Priority"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="4%" HeaderStyle-Width="4%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Priority"
                                        DataField="PriorityName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="7%" HeaderStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Complexity"
                                        DataField="ComplexityName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="7%" HeaderStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="GL Planned Date"
                                        DataField="GLPlannedDate"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="6%" HeaderStyle-Width="6%"
                                        ItemStyle-BorderColor="Navy" />

                                </Columns>
                            </asp:GridView>
                        </li>


                        <li class="trvl_Approver">
                            <br />
                            <asp:GridView ID="DgvApprover" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="75%">
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
                                        ItemStyle-Width="1%"
                                        ItemStyle-BorderColor="Navy"
                                        Visible="false" />
                                </Columns>
                            </asp:GridView>
                        </li>
                        <li>
                            <asp:LinkButton ID="btnABAPPlanSubmit" runat="server" Visible="true" OnClientClick="return SaveMultiClick();" OnClick="btnABAPPlanSubmit_Click">Submit Plan</asp:LinkButton>

                            <asp:LinkButton ID="lnk_Index" runat="server" Visible="true" PostBackUrl="~/procs/ABAP_Object_Tracker_All_List.aspx">Back</asp:LinkButton>
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

    <asp:HiddenField ID="hdnProgramManagerMail" runat="server" />
    <asp:HiddenField ID="hdndowloadfile" runat="server" />
    <asp:HiddenField ID="hdnProgramManagerName" runat="server" />
    <asp:HiddenField ID="hdnProjectManagerName" runat="server" />
    <asp:HiddenField ID="hdnDeliveryHeadMail" runat="server" />




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


        function CheckUncheckAll(source) {
            var checkboxes = document.querySelectorAll('#<%= gvRGSDetails.ClientID %> input[type="checkbox"]');
            alert(checkboxes);
            for (var i = 0; i < checkboxes.length; i++) {
                alert(checkboxes[i]);

                if (checkboxes[i] != source) {
                    checkboxes[i].checked = source.checked;
                }
            }
        }



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
            dockument.getElementById("page-loader").style.display = "none";
        });


        function SaveMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=lnk_Index.ClientID%>');

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
    </script>
</asp:Content>

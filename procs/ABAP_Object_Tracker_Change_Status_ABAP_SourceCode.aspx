<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="ABAP_Object_Tracker_Change_Status_ABAP_SourceCode.aspx.cs"
    Inherits="ABAP_Object_Tracker_Change_Status_ABAP_SourceCode" EnableSessionState="True" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <style>
        #MainContent_btnABAPPlanSubmit, #MainContent_updatergs, #MainContent_updatefs {
            background: #3D1956;
            color: #febf39 !important;
            font-size: medium;
        }

        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        #MainContent_lstApprover, #MainContent_lstIntermediate {
            padding: 0 0 33px 0 !important;
        }

        #MainContent_LinkBtnSavePopup, #MainContent_LinkBtnBackPopup, #MainContent_LinkButton1, #MainContent_LinkButton2, #MainContent_btn_SumitTimesheet, #MainContent_LinkButton3, #MainContent_LinkButton45, #MainContent_btnRedirect {
            background: #3D1956;
            color: #febf39 !important;
            padding: 8px 18px;
            margin: 26px !important;
        }

        .Dropdown {
            border-bottom: 2px solid #cccccc;
            background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;
            cursor: default;
        }

        input#MainContent_uplCTMTestCases {
            width: 200px;
        }

        input#MainContent_uplUATSingOff {
            width: 200px;
        }

        div#left-content {
            max-width: 100% !important;
            max-height: 100% !important;
        }

        /*.modalBackground {
    background-color: rgba(0, 0, 0, 0.5);
     
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    z-index: 1000;
}*/

        #content-container .intern-padding {
            padding: 0px 0px !important;
            cursor: auto !important;
        }


        .testABAP {
            position: fixed !important;
            top: 13% !important;
            left: 7% !important;
            background-color: white;
            border: 1px solid rgb(204, 204, 204);
            width: 85% !important;
            overflow: scroll;
            height: 75% !important;
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

        select#MainContent_AllStatus {
            background: white !important;
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
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="ABAP Development - Upload Soruce Code"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center; margin: 0 0 0 10px !important;"></asp:Label>
                </div>
                <span style="float: right;">
                    <%--<asp:LinkButton ID="LinkButton1" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/ABAP_Object_Tracker_Index.aspx" BackColor="#3D1956">Back</asp:LinkButton>--%>
                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="Savebtnsve" PostBackUrl="~/procs/ABAP_Object_Tracker_Index.aspx" BackColor="#3D1956" style="margin: 0px 20px 0px 0px !important;"><%=System.Configuration.ConfigurationManager.AppSettings["ABAPObjectPageTitle"]%></asp:LinkButton>
                </span>


                <asp:LinkButton ID="localtrvl_delete_btn" runat="server" CssClass="Savebtnsve" Style="display: none"></asp:LinkButton>
                <asp:LinkButton ID="LinkButton6" runat="server" CssClass="Savebtnsve" Style="display: none"></asp:LinkButton>



                <%--  <asp:Panel ID="PnlIrSheet" runat="server" Style="display: none; position: fixed; top: 20%; left: 20%; transform: translate(-20%, -30%,-30%); background-color: white; border: 1px solid #ccc; z-index: 1000; overflow:scroll">--%>
                <asp:Panel ID="PnlIrSheet" runat="server" CssClass="testABAP">
                    <div runat="server" id="DivIDPopup" visible="false" style="height: auto">
                        <span style="float: right;">
                            <asp:LinkButton ID="LinkButton2" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="LinkBtnBackPopup_Click" BackColor="#3D1956">Back</asp:LinkButton>
                        </span>
                        <div class="edit-contact">
                            <div style="padding-left: 200px; padding-top: 10px">
                                <asp:Label runat="server" ID="lblmessagesub" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                            </div>
                            <br />
                            <br />
                            <ul runat="server" id="editform1">

                                <li class="trvl_date">
                                    <span>Development Description</span>
                                    <br />
                                    <asp:TextBox ID="Txt_DevelopmentDescription" Enabled="false" runat="server" Width="860px"></asp:TextBox>
                                </li>
                                <li></li>
                                <li></li>

                                <li class="trvl_date">
                                    <span>Module</span>
                                    <br />
                                    <asp:TextBox ID="Txt_Module" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                                </li>
                                <li>
                                    <span>Interface</span>
                                    <br />
                                    <asp:TextBox ID="Txt_Interface" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                                </li>
                                <li>
                                    <span>FRICE Category</span>
                                    <br />
                                    <asp:TextBox ID="Txt_FRICECategory" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                                </li>

                                <li class="trvl_date">
                                    <span>Priority / Order</span>
                                    <br />
                                    <asp:TextBox ID="Txt_Order" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span>Priority</span>
                                    <br />
                                    <asp:TextBox ID="Txt_Priority" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                                </li>
                                <li>
                                    <span>Complexity</span>
                                    <br />
                                    <asp:TextBox ID="Txt_Complexity" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                                </li>



                                <li class="trvl_date">
                                    <asp:Label ID="Label1" runat="server" Text="Planned Preparation Start Date"></asp:Label>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="Txt_PlannedPreparationStart" runat="server" Width="200px" MaxLength="15" Enabled="false"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <asp:Label ID="Label2" runat="server" Text="Planned Preparation Finish Date"></asp:Label>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txt_PlannedPreparationFinish" Width="200px" runat="server" MaxLength="15" Enabled="false"></asp:TextBox>
                                </li>
                                <li></li>


                                <li class="trvl_date" id="PlnSubmitDate" runat="server">
                                    <asp:Label ID="Label3" runat="server" Text="Planned Submission"></asp:Label>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="Txt_PlannedSubmission" Width="200px" runat="server" MaxLength="15" Enabled="false"></asp:TextBox>
                                </li>

                                <li class="trvl_date" id="PlnApprDate" runat="server">
                                    <asp:Label ID="Label4" runat="server" Text="Planned Approval Date"></asp:Label>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txt_PlannedApproval" Width="200px" runat="server" MaxLength="15" Enabled="false"></asp:TextBox>
                                </li>
                                <li id="PlnEmptyDate" runat="server"></li>


                                <li class="trvl_date">
                                    <asp:Label ID="lbl1" runat="server" Text="Revised / Actual Preparation Start Date"></asp:Label>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="Txt_RevisedActualPreparationStart" runat="server" Width="200px"
                                        MaxLength="15" oncopy="return false" onpaste="return false"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="Txt_RevisedActualPreparationStart" runat="server">
                                    </ajaxToolkit:CalendarExtender>
                                </li>
                                <li class="trvl_date">
                                    <asp:Label ID="lbl2" runat="server" Text="Revised / Actual Preparation Finish"></asp:Label>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txt_RevisedActualPreparationFinish" Width="200px" runat="server" MaxLength="15" oncopy="return false" onpaste="return false"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txt_RevisedActualPreparationFinish" runat="server">
                                    </ajaxToolkit:CalendarExtender>

                                </li>
                                <li></li>


                                <li class="trvl_date">
                                    <asp:Label ID="lbl3" runat="server" Text="Revised / Actual Submission"></asp:Label><span style="color: red" runat="server" id="submitmendatory" visible="false">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="Txt_RevisedActualSubmission" Width="200px" runat="server" MaxLength="15" oncopy="return false" onpaste="return false"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" Format="dd/MM/yyyy" TargetControlID="Txt_RevisedActualSubmission" runat="server">
                                    </ajaxToolkit:CalendarExtender>
                                </li>

                                <li class="trvl_date">
                                    <asp:Label ID="lbl4" runat="server" Text="Revised / Actual Approval"></asp:Label><span style="color: red" runat="server" id="approvemendatory" visible="false">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txt_RevisedActualApproval" Width="200px" runat="server" MaxLength="15" oncopy="return false" onpaste="return false"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" Format="dd/MM/yyyy" TargetControlID="txt_RevisedActualApproval" runat="server">
                                    </ajaxToolkit:CalendarExtender>
                                </li>
                                <li></li>

                                <li class="trvl_date">
                                    <asp:Label ID="lbl5" runat="server" Text="Status"></asp:Label>
                                    <br />
                                    <asp:DropDownList runat="server" ID="AllStatus" Width="250px" OnSelectedIndexChanged="AllStatus_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

                                </li>
                                <li></li>
                                <li></li>

                                <li runat="server" id="lireason1" visible="false">
                                    <br />
                                    <asp:Label ID="lblreason" runat="server" Text="FS Failed Reason"></asp:Label>
                                    <asp:TextBox AutoComplete="off" ID="txt_Reason" Width="200px" runat="server" Enabled="false"></asp:TextBox>
                                </li>
                                <li runat="server" id="lireason2" visible="false"></li>
                                <li runat="server" id="lireason3" visible="false"></li>

                                <li class="trvl_date" id="liupload_1" runat="server" visible="false">
                                    <br />
                                    <asp:Label runat="server" ID="lbl_Upload_Error" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label><b></b>
                                    <span>FS Attachment:</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <asp:FileUpload ID="uplFSAttachment" runat="server" accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" AllowMultiple="true"></asp:FileUpload>

                                </li>
                                <li id="liupload_2" runat="server" visible="false"></li>
                                <li id="liupload_3" runat="server" visible="false"></li>


                                <li class="trvl_date" id="liUploadCTMTestCase" runat="server" visible="false">
                                    <br />
                                    <span runat="server" id="Span1">CTM Test Cases:</span>&nbsp;&nbsp;<span style="color: red" runat="server" id="Span2">*</span>
                                    <asp:Label runat="server" ID="Label5" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label><b></b>
                                    <asp:FileUpload ID="uplCTMTestCases" runat="server" accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" AllowMultiple="true"></asp:FileUpload>
                                </li>

                                <li class="trvl_date" id="liUploadUATSingOff" runat="server" visible="false">
                                    <span runat="server" id="spnuatattach">UAT Sing Off Attachment:</span>&nbsp;&nbsp;<span style="color: red" runat="server" id="spnmedatory">*</span>
                                    <asp:Label runat="server" ID="Label6" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label><b></b>
                                    <asp:FileUpload ID="uplUATSingOff" runat="server" accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" AlsowMultiple="false"></asp:FileUpload>
                                </li>
                                <li id="liUploadCTMTestCase_3" runat="server" visible="false"></li>

                                <li class="trvl_date" id="li_ddl_stage_1" runat="server" visible="false">
                                    <br />
                                    <span>Select Stage</span>
                                    <br />
                                    <asp:DropDownList ID="ddlStage" runat="server" Width="250px">
                                        <asp:ListItem Text="Select a Stage" Value="" />
                                        <asp:ListItem Text="FS" Value="FS" />
                                        <asp:ListItem Text="ABAP" Value="ABAP" />
                                    </asp:DropDownList>
                                </li>
                                <li id="li_ddl_stage_2" runat="server" visible="false"></li>
                                <li id="li_ddl_stage_3" runat="server" visible="false"></li>

                                 <li class="trvl_date" id="li_sc_upload1" runat="server" visible="false">
                                    <br />
                                    <asp:Label runat="server" ID="Label7" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label><b></b>
                                    <span>Upload Source Code:</span>
                                    <asp:FileUpload ID="fuSourceCode" runat="server" accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" AllowMultiple="true"></asp:FileUpload>
                                </li>
                                <li id="li_sc_upload2" runat="server" visible="false"></li>
                                <li id="li_sc_upload3" runat="server" visible="false"></li>


                                <li class="trvl_date">
                                    <br />
                                    <span>Remarks</span>
                                    <br />
                                    <asp:TextBox ID="Txt_Remarks" runat="server" Height="40px" Width="460px" TextMode="MultiLine"></asp:TextBox>
                                </li>
                                <li></li>
                                <li></li>


                                <li style="width: 40%">
                                    <span runat="server" id="spnfsdocs" visible="false">Function Spefication Doucment</span>
                                    <asp:GridView ID="gvuploadedFiles" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                        DataKeyNames="Srno">
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
                                            <asp:BoundField HeaderText="File Version"
                                                DataField="Srno"
                                                ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="10%" HeaderStyle-Width="10%"
                                                ItemStyle-BorderColor="Navy" />

                                            <asp:BoundField HeaderText="Functional Specification Document"
                                                DataField="FileName"
                                                ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="30%" HeaderStyle-Width="30%"
                                                ItemStyle-BorderColor="Navy" />


                                            <asp:TemplateField HeaderText="Download" ItemStyle-Width="20%" HeaderStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="lnkDownload" runat="server" Width="20px" ToolTip="Download Test Cases" Height="15px" CommandArgument='<%# Eval("FileName") %>' OnClick="ibdownloadbtn_Click" ImageUrl="~/Images/Download.png" />
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </li>

                                <li style="width: 40%; margin: 0 0 0 50px !important">
                                    <br />
                                    <span runat="server" id="spnfailedhbt" visible="false">Failed HBT Test Cases Files</span>
                                    <br />
                                    <asp:GridView ID="gvHBTTestCaseFailedFiles" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                        AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false" DataKeyNames="Srno">
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
                                            <asp:BoundField HeaderText="File Version"
                                                DataField="Srno"
                                                ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="11%" HeaderStyle-Width="11%"
                                                ItemStyle-BorderColor="Navy" />

                                            <asp:BoundField HeaderText="Failed HBT Test Cases Files"
                                                DataField="FileName"
                                                ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="30%" HeaderStyle-Width="30%"
                                                ItemStyle-BorderColor="Navy" />


                                            <asp:TemplateField HeaderText="Download" ItemStyle-Width="20%" HeaderStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="lnkhbttestcaseDownload" runat="server" Width="20px" ToolTip="Download Test Cases" Height="15px" CommandArgument='<%# Eval("FileName") %>' OnClick="lnkhbttestcaseDownload_Click" ImageUrl="~/Images/Download.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </li>

                                 <li style="width: 40%">
                                    <br />
                                    <span runat="server" id="spnsourcecode" visible="false">Source Code</span>
                                    <asp:GridView ID="gvSourceCode" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
                                        AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false">
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
                                            <asp:BoundField HeaderText="Source Code"
                                                DataField="SourceCodeFile"
                                                ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="30%" HeaderStyle-Width="30%"
                                                ItemStyle-BorderColor="Navy" />

                                            <asp:TemplateField HeaderText="Download" ItemStyle-Width="20%" HeaderStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="lnkDownload" runat="server" Width="20px" ToolTip="Download Source Code" Height="15px" CommandArgument='<%# Eval("SourceCodeFile") %>' OnClick="ibdownloadSourceCode_Click" ImageUrl="~/Images/Download.png" />
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </li>
                                <li></li>
                                <li></li>
                                <li class="trvl_date">

                                    <asp:LinkButton ID="LinkBtnSavePopup" runat="server" Text="Submit" ToolTip="Submit" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();" OnClick="LinkBtnSavePopup_Click">Submit</asp:LinkButton>

                                    <asp:LinkButton ID="LinkBtnBackPopup" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="LinkBtnBackPopup_Click">Back</asp:LinkButton>

                                </li>
                                <li></li>
                                <li></li>
                            </ul>
                        </div>
                    </div>

                </asp:Panel>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderIRSheet" runat="server"
                    BackgroundCssClass="modalBackground" TargetControlID="localtrvl_delete_btn" PopupControlID="PnlIrSheet" RepositionMode="RepositionOnWindowResizeAndScroll"
                    OnOkScript="ok()" CancelControlID="LinkBtnBackPopup" />




                <%----Timesheet popup Section --%>
                <%--   <asp:Panel ID="PnlABAPTimeSheet" runat="server" CssClass="testABAP">
                    <div runat="server" id="DivIDABAPTimesheetPopup" visible="false" style="overflow: scroll;">

                        <div class="edit-contact">
                            <div style="padding-left: 200px; padding-top: 10px">
                                <asp:Label runat="server" ID="lblTimesheetErrorMsg" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                            </div>
                            <ul runat="server" id="Ul1">
                                <li>
                                    <asp:Label runat="server" Text="Submit Timesheet" Font-Size="Large"></asp:Label>
                                </li>
                                <li></li>
                                <li>
                                    <asp:LinkButton ID="LinkButton3" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="LinkBtnABAPBackPopup_Click" BackColor="#3D1956">Back</asp:LinkButton>
                                </li>

                                <li style="width: 100% !important">
                                    <asp:GridView ID="gvTimesheet" runat="server" CssClass="Milestones" DataKeyNames="ABAPDetailsId" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" ShowHeaderWhenEmpty="True">
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
                                        </Columns>
                                    </asp:GridView>
                                </li>

                                <li>
                                    <asp:LinkButton ID="btnBack" runat="server" CssClass="Savebtnsve" Text="Submit" OnClick="btnSubmit_Click1" OnClientClick="return SaveOutClick();">Submit</asp:LinkButton>
                                    <asp:LinkButton ID="LinkButton45" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="LinkBtnABAPBackPopup_Click" BackColor="#3D1956">Back</asp:LinkButton>
                                </li>
                            </ul>
                        </div>
                    </div>

                </asp:Panel>

                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderABAPTimesheet" runat="server"
                    BackgroundCssClass="modalBackground" TargetControlID="LinkButton6" PopupControlID="PnlABAPTimeSheet" RepositionMode="RepositionOnWindowResizeAndScroll"
                    CancelControlID="LinkButton3" />--%>


                <div class="edit-contact">

                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" Visible="false"></asp:LinkButton>
                        </div>
                    </div>

                    <ul id="editform2" runat="server">
                        <li class="trvl_date">
                            <span runat="server" id="spnlabel">Project / Location</span><br />
                            <asp:DropDownList runat="server" ID="DDLProjectLocation" AutoPostBack="True" CssClass="DropdownListSearch" OnSelectedIndexChanged="DDLProjectLocation_SelectedIndexChanged">
                            </asp:DropDownList>
                        </li>
                        <li class="trvl_date" id="liProjectMangaer" runat="server" visible="false">
                            <span>Project Manager</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPRM" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_grid" id="lifsdetails" runat="server">
                            <asp:Label runat="server" ID="lbl_FSConsultant_Error" Visible="True" Style="color: red; text-align: center;"></asp:Label>
                            <asp:Label runat="server" ID="lbl_error" Visible="True" Style="color: red; text-align: center;"></asp:Label>
                            <br />
                            <span class="ABAP" runat="server" visible="false" id="ABAP">ABAP Object List</span>
                            <asp:GridView ID="gvABAPDevDetails" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                                Width="100%" EditRowStyle-Wrap="true" DataKeyNames="ABAPDetailsId" ShowHeaderWhenEmpty="True">
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

                                    <asp:TemplateField ItemStyle-Width="6%">
                                        <HeaderTemplate>
                                            View
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkEdit" runat="server" Height="15px" OnClick="lnkEdit_Click2" OnClientClick="showPopup();" ImageUrl="~/Images/edit.png" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField HeaderText="Development Description"
                                        DataField="Development_Desc"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="20%" HeaderStyle-Width="20%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Development Status"
                                        DataField="ABAPDevStatuss"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="10%" HeaderStyle-Width="10%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Module"
                                        DataField="ModuleDesc"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="7%" HeaderStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Interface"
                                        DataField="Interface"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="6%" HeaderStyle-Width="6%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Scope"
                                        DataField="Scope"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="7%" HeaderStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="FRICE Category"
                                        DataField="FCategoryName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="10%" HeaderStyle-Width="10%"
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

                                    <asp:BoundField HeaderText="Planned Start Date"
                                        DataField="ABAPPlannedStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%" HeaderStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Finish Date"
                                        DataField="ABAPPlannedFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%" HeaderStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Revised Start Date"
                                        DataField="ABAPRevisedStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%" HeaderStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Revised Finish Date"
                                        DataField="ABAPRevisedFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%" HeaderStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Actual Start Date"
                                        DataField="ABAPActualStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%" HeaderStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Actual Finish Date"
                                        DataField="ABAPActualFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%" HeaderStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />



                                </Columns>
                                <EmptyDataTemplate>No Record Available</EmptyDataTemplate>

                            </asp:GridView>

                        </li>
                        <li style="width: 50%;">
                            <%--<asp:LinkButton ID="btn_SumitTimesheet" runat="server" Text="Submit Timesheet" ToolTip="Submit Timesheet" BackColor="#3D1956" OnClick="btn_SumitTimesheet_Click" Visible="true">Submit Timesheet</asp:LinkButton>--%>
                            <asp:LinkButton ID="btnRedirect" runat="server" Text="Fill the timesheet" OnClick="btnRedirect_Click" Visible="false"></asp:LinkButton>

                            <asp:LinkButton ID="btnback_mng" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/ABAP_Object_Tracker_Index.aspx">Back</asp:LinkButton>
                        </li>

                        <li></li>
                    </ul>

                </div>

            </div>
        </div>
    </div>

    <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>

    <br />

    <asp:HiddenField ID="HDID" runat="server" />
    <asp:HiddenField ID="HDStatus" runat="server" />
    <asp:HiddenField ID="HDStatusCheck" runat="server" />
    <asp:HiddenField ID="HDStatusCheckFlag" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="FilePath" runat="server" />
    <asp:HiddenField ID="HDProjectLocation" runat="server" />
    <asp:HiddenField ID="hdnABAPDetailsId" runat="server" />
    <asp:HiddenField ID="hdnCommaSeperABAPDetailsId" runat="server" />
    <asp:HiddenField ID="hdnctmstatus" runat="server" />

    <asp:HiddenField ID="hdnprojectManager" runat="server" />
    <asp:HiddenField ID="hdnprojectManagerEmail" runat="server" />
    <asp:HiddenField ID="hdnprogramnager" runat="server" />
    <asp:HiddenField ID="hdnprogramnagerEmail" runat="server" />
    <asp:HiddenField ID="hdnABAPperEmail" runat="server" />
    <asp:HiddenField ID="hdnABAPperName" runat="server" />
    <asp:HiddenField ID="hdnFSConsultantEmail" runat="server" />
    <asp:HiddenField ID="hdnFSConsultantName" runat="server" />
    <asp:HiddenField ID="hdnHBTConsultantEmail" runat="server" />
    <asp:HiddenField ID="hdnHBTConsultantName" runat="server" />
    <asp:HiddenField ID="hdnReason" runat="server" />

    <asp:HiddenField ID="hdnABAPRevisedStartDt" runat="server" />
    <asp:HiddenField ID="hdnABAPRevisedFinishDt" runat="server" />
    <asp:HiddenField ID="HDDateCheck3" runat="server" />
    <asp:HiddenField ID="HDDateCheck4" runat="server" />
    <asp:HiddenField ID="HDRemarkcheck" runat="server" />

    <asp:HiddenField ID="hdnStartDate" runat="server" />
    <asp:HiddenField ID="hdnEndDate" runat="server" />


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

            applyGridViewScroll('#MainContent_gvABAPDevDetails', gridOptions);

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

        function isValidInput(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((charCode >= 48 && charCode <= 57) || charCode == 58) {
                return true;
            }
            else {
                return false;
            }
        }

        function SaveMultiClick() {
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

        function noanyCharecters(e) {
            var keynum;
            var keychar;
            var numcheck = /[]/;

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


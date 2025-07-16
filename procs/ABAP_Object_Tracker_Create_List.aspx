<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="ABAP_Object_Tracker_Create_List.aspx.cs" Inherits="ABAP_Object_Tracker_Create_List" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <style>
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

        .modalBackground {
            background-color: rgba(0, 0, 0, 0.5);
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            z-index: 1000;
        }

        #MainContent_LinkBtnSavePopup, #MainContent_LinkBtnBackPopup, #MainContent_LinkButton1, #MainContent_LinkButton2, #MainContent_btn_SumitTimesheet, #MainContent_LinkButton3 {
            background: #3D1956;
            color: #febf39 !important;
            padding: 8px 18px;
            margin: 26px !important;
        }

        #MainContent_lnk_Index,
        #MainContent_lnk_Index {
            float: right;
            background: #3D1956;
            color: #febf39 !important;
            padding: 0.5% 1.4%;
        }

        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
            background: #dae1ed;
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

        .clscommon th {
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
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">

                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Create ABAP Object Plan"></asp:Label>
                    </span>
                </div>
                <asp:LinkButton ID="lnk_Index" runat="server" CssClass="Savebtnsve" PostBackUrl="~/procs/ABAP_Object_Tracker_Index.aspx" BackColor="#3D1956"><%=System.Configuration.ConfigurationManager.AppSettings["ABAPObjectPageTitle"]%></asp:LinkButton>
                <br />
                <span>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                </span>

                <div runat="server" id="DivIDPopup" style="height: auto">
                    <%--<span style="float: right;">
                   <asp:LinkButton ID="LinkButton2" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="LinkBtnBackPopup_Click" BackColor="#3D1956">Back</asp:LinkButton>
               </span>--%>
                    <div class="edit-contact">

                        <ul runat="server" id="editform1">
                            <li class="trvl_date">
                                <span>Development Description</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="txtDescription" Width="740px" runat="server" TextMode="MultiLine" Style="font-family: Arial;"></asp:TextBox>
                            </li>
                            <li></li>
                            <li></li>
                            <li class="trvl_date">
                                <span>Module</span> &nbsp;&nbsp;<span style="color: red">*</span><br />
                                <asp:DropDownList ID="ddlModule" runat="server" CssClass="DropdownListSearch" Style="width: 251px !important"></asp:DropDownList>
                            </li>

                            <li class="trvl_date">
                                <span>Interface</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="txtInterface" Width="200px" runat="server"></asp:TextBox>
                            </li>


                            <li class="trvl_date">
                                <span>Scope</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="txtScope" Width="200px" runat="server"></asp:TextBox>
                            </li>

                            <li class="trvl_date">
                                <span>Frice Category</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                <br />
                                <asp:DropDownList ID="ddlFRICE_Category" runat="server" CssClass="DropdownListSearch" Style="width: 251px !important"></asp:DropDownList>
                            </li>
                            <li class="trvl_date">
                                <span>Priority</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="txtPriority" Width="200px" runat="server" onkeypress="return isNumberKey(event);"></asp:TextBox>
                            </li>
                            <li class="trvl_date">
                                <span>Priority Type</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                <br />
                                <asp:DropDownList ID="ddlPriorityType" runat="server" CssClass="DropdownListSearch" Style="width: 251px !important"></asp:DropDownList>
                            </li>
                            <li class="trvl_date">
                                <span>Complexity</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                <br />
                                <asp:DropDownList ID="ddlComplexity" runat="server" CssClass="DropdownListSearch" Style="width: 251px !important"></asp:DropDownList>
                                <br />
                            </li>
                            <li class="trvl_date">
                                <span>Functional Consultant (RGS/FS/HBT/CTM)</span> &nbsp;&nbsp;<span style="color: red">*</span><br />
                                <asp:DropDownList ID="ddlFunctional_Consultant" runat="server" CssClass="DropdownListSearch" Style="width: 251px !important"></asp:DropDownList>
                            </li>
                            <li></li>
                            <li></li>
                            <li></li>
                            <li style="border-top: 2px solid black; width: 100%; margin: 10px 0; list-style: none;"></li>
                            <li>
                                <span>
                                    <asp:Label ID="Label5" runat="server" Text="RGS" Style="color: orange; font-size: 16px; font-weight: 400; text-align: center;"></asp:Label>
                                </span></li>
                            <li></li>
                            <li></li>


                            <li class="trvl_date">
                                <br />
                                <span>Planned Preparation Start Date</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="datergsstart" Width="200px" Enabled="true" runat="server" oncopy="return false" onpaste="return false"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" Format="dd/MM/yyyy" TargetControlID="datergsstart" runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </li>
                            <li class="trvl_date">
                                <span>Planned Preparation Finish Date</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="datergsfinish" Width="200px" Enabled="true" runat="server" oncopy="return false" onpaste="return false"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="datergsfinish" runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </li>
                            <li class="trvl_date">
                                <span>Planned Submission Date</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="datergssub" Width="200px" runat="server" Enabled="true" oncopy="return false" onpaste="return false"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="datergssub" runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </li>
                            <li class="trvl_date">
                                <span>Planned Approval Date</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="datergsapprovel" Width="200px" runat="server" Enabled="true" oncopy="return false" onpaste="return false"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" Format="dd/MM/yyyy" TargetControlID="datergsapprovel" runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </li>
                            <li class="trvl_date" id="RgsRevActualPrepStartdate" runat="server" visible="false">
                                <br />
                                <span>Revised Preparation Start Date</span> 
                                <br />
                                <asp:TextBox AutoComplete="off" ID="RgsRevActualPrepStart" Width="200px" Enabled="true" runat="server" oncopy="return false" onpaste="return false"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender15" Format="dd/MM/yyyy" TargetControlID="RgsRevActualPrepStart" runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </li>
                            <li class="trvl_date" id="RgsRevActualPrepFinishdate" runat="server" visible="false">
                                <span>Revised Preparation Finish Date</span> 
                                <br />
                                <asp:TextBox AutoComplete="off" ID="RgsRevActualPrepFinish" Width="200px" Enabled="true" runat="server" oncopy="return false" onpaste="return false"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender16" Format="dd/MM/yyyy" TargetControlID="RgsRevActualPrepFinish" runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </li>
                            <li class="trvl_date" id="RevActualSubmitdate" runat="server" visible="false">
                                <br />
                                <span>Revised Submission Date</span> 
                                <br />
                                <asp:TextBox AutoComplete="off" ID="RevActualSubmit" Width="200px" Enabled="true" runat="server" oncopy="return false" onpaste="return false"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender17" Format="dd/MM/yyyy" TargetControlID="RevActualSubmit" runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </li>
                            <li class="trvl_date" id="RevActualApprovedate" runat="server" visible="false">
                                <span>Revised Approval Date</span> 
                                <br />
                                <asp:TextBox AutoComplete="off" ID="RevActualApprove" Width="200px" Enabled="true" runat="server" oncopy="return false" onpaste="return false"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender18" Format="dd/MM/yyyy" TargetControlID="RevActualApprove" runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </li>
                            <li></li>

                            <li style="border-top: 2px solid black; width: 100%; margin: 10px 0; list-style: none;"></li>
                            <li>
                                <span>
                                    <asp:Label ID="Label9" runat="server" Text="FS" Style="color: orange; font-size: 16px; font-weight: 400; text-align: center;"></asp:Label>
                                </span></li>
                            <li></li>
                            <li></li>
                            <li class="trvl_date">
                                <br />
                                <span>Planned Start Date</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="datefsstart" Width="200px" runat="server" Enabled="true" oncopy="return false" onpaste="return false"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender5" Format="dd/MM/yyyy" TargetControlID="datefsstart" runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </li>
                            <li class="trvl_date">
                                <span>Planned Finish Date</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="datefsfinish" Width="200px" runat="server" Enabled="true" oncopy="return false" onpaste="return false"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender6" Format="dd/MM/yyyy" TargetControlID="datefsfinish" runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </li>
                            <li class="trvl_date" id="FsRevisedStartdate" runat="server" visible="false">
                                <br />
                                <span>Revised Start Date</span> 
                                <br />
                                <asp:TextBox AutoComplete="off" ID="txtFsRevisedStartdate" Width="200px" Enabled="true" runat="server" oncopy="return false" onpaste="return false"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender19" Format="dd/MM/yyyy" TargetControlID="txtFsRevisedStartdate" runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </li>
                            <li class="trvl_date" id="FsRevisedFinishdate" runat="server" visible="false">
                                <span>Revised Finish Date</span> 
                                <br />
                                <asp:TextBox AutoComplete="off" ID="txtFsRevisedFinishdate" Width="200px" Enabled="true" runat="server" oncopy="return false" onpaste="return false"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender20" Format="dd/MM/yyyy" TargetControlID="txtFsRevisedFinishdate" runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </li>
                            <li></li>
                            <li></li>
                            <li style="border-top: 2px solid black; width: 100%; margin: 10px 0; list-style: none;"></li>

                            <li>
                                <span>
                                    <asp:Label ID="Label12" runat="server" Text="ABAP Development" Style="color: orange; font-size: 16px; font-weight: 400; text-align: center;"></asp:Label>
                                </span></li>
                            <li></li>
                            <li></li>
                            <li class="trvl_date">
                                <br />
                                <span>Planned ABAP Consultant</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                <asp:DropDownList ID="ddlabapConsultant" runat="server" CssClass="DropdownListSearch" Enabled="true" Style="width: 251px !important"></asp:DropDownList>
                                <br />
                            </li>

                            <li class="trvl_date">
                                <span>Duration</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="txtDuration" Width="200px" Enabled="true" runat="server" onkeypress="return isNumberKey(event);"></asp:TextBox>
                            </li>
                            <li class="trvl_date">
                                <span>Planned Start Date</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="dateabapstart" Width="200px" runat="server" Enabled="true" oncopy="return false" onpaste="return false"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender7" Format="dd/MM/yyyy" TargetControlID="dateabapstart" runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </li>
                            <li class="trvl_date">
                                <span>Planned Finish Date</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="dateabapfinish" Width="200px" runat="server" Enabled="true" oncopy="return false" onpaste="return false"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender8" Format="dd/MM/yyyy" TargetControlID="dateabapfinish" runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </li>
                            <li class="trvl_date" id="AbapRevisedStartdate" runat="server" visible="false">
                                <br />
                                <span>Revised Start Date</span> 
                                <br />
                                <asp:TextBox AutoComplete="off" ID="txtAbapRevisedStart" Width="200px" Enabled="true" runat="server" oncopy="return false" onpaste="return false"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender21" Format="dd/MM/yyyy" TargetControlID="txtAbapRevisedStart" runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </li>
                            <li class="trvl_date" id="AbapRevisedFinishdate" runat="server" visible="false">
                                <span>Revised Finish Date</span> 
                                <br />
                                <asp:TextBox AutoComplete="off" ID="txtAbapRevisedFinish" Width="200px" Enabled="true" runat="server" oncopy="return false" onpaste="return false"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender22" Format="dd/MM/yyyy" TargetControlID="txtAbapRevisedFinish" runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </li>
                            <li style="border-top: 2px solid black; width: 100%; margin: 10px 0; list-style: none;"></li>

                            <li>
                                <span>
                                    <asp:Label ID="Label16" runat="server" Text="HBT Testing" Style="color: orange; font-size: 16px; font-weight: 400; text-align: center;"></asp:Label>
                                </span></li>
                            <li></li>
                            <li></li>

                            <li class="trvl_date">
                                <br />
                                <span>Planned Start Date</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="datehbtstart" Width="200px" runat="server" Enabled="true" oncopy="return false" onpaste="return false"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender9" Format="dd/MM/yyyy" TargetControlID="datehbtstart" runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </li>
                            <li class="trvl_date">
                                <span>Planned Finish Date</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="datehbtfinish" Width="200px" runat="server" Enabled="true" oncopy="return false" onpaste="return false"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender10" Format="dd/MM/yyyy" TargetControlID="datehbtfinish" runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </li>
                            <li class="trvl_date" id="RevisedHbtStartDate" runat="server" visible="false">
                                <br />
                                <span>Revised Start Date</span> 
                                <br />
                                <asp:TextBox AutoComplete="off" ID="txtRevisedHbtStartDate" Width="200px" Enabled="true" runat="server" oncopy="return false" onpaste="return false"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender23" Format="dd/MM/yyyy" TargetControlID="txtRevisedHbtStartDate" runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </li>
                            <li class="trvl_date" id="RevisedHbtFinishDate" runat="server" visible="false">
                                <span>Revised Finish Date</span> 
                                <br />
                                <asp:TextBox AutoComplete="off" ID="txtRevisedHbtFinishDate" Width="200px" Enabled="true" runat="server" oncopy="return false" onpaste="return false"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender24" Format="dd/MM/yyyy" TargetControlID="txtRevisedHbtFinishDate" runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </li>
                            <li></li>
                            <li></li>
                            <li style="border-top: 2px solid black; width: 100%; margin: 10px 0; list-style: none;"></li>
                            <li>
                                <span>
                                    <asp:Label ID="Label19" runat="server" Text="CTM Testing" Style="color: orange; font-size: 16px; font-weight: 400; text-align: center;"></asp:Label>
                                </span></li>
                            <li></li>
                            <li></li>

                            <li class="trvl_date">
                                <br />
                                <span>Planned Start Date</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="datectmstart" Width="200px" runat="server" Enabled="true" oncopy="return false" onpaste="return false"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender11" Format="dd/MM/yyyy" TargetControlID="datectmstart" runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </li>
                            <li class="trvl_date">
                                <span>Planned Finish Date</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="datectmfinish" Width="200px" runat="server" Enabled="true" oncopy="return false" onpaste="return false"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender12" Format="dd/MM/yyyy" TargetControlID="datectmfinish" runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </li>
                            <li class="trvl_date" id="RevisedCtmStartDate" runat="server" visible="false">
                                <br />
                                <span>Revised Start Date</span> 
                                <br />
                                <asp:TextBox AutoComplete="off" ID="txtRevisedCtmStartDate" Width="200px" Enabled="true" runat="server" oncopy="return false" onpaste="return false"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender25" Format="dd/MM/yyyy" TargetControlID="txtRevisedCtmStartDate" runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </li>
                            <li class="trvl_date" id="RevisedCtmFinishDate" runat="server" visible="false">
                                <span>Revised Finish Date</span> 
                                <br />
                                <asp:TextBox AutoComplete="off" ID="txtRevisedCtmFinishDate" Width="200px" Enabled="true" runat="server" oncopy="return false" onpaste="return false"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender26" Format="dd/MM/yyyy" TargetControlID="txtRevisedCtmFinishDate" runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </li>
                            <li></li>
                            <li></li>
                            <li style="border-top: 2px solid black; width: 100%; margin: 10px 0; list-style: none;"></li>

                            <li>
                                <span>
                                    <asp:Label ID="Label22" runat="server" Text="UAT Sign Off" Style="color: orange; font-size: 16px; font-weight: 400; text-align: center;"></asp:Label>
                                </span></li>
                            <li></li>
                            <li></li>

                            <li class="trvl_date">
                                <br />
                                <span>Planned Date</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="dateplanned" Width="200px" runat="server" Enabled="true" oncopy="return false" onpaste="return false"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender13" Format="dd/MM/yyyy" TargetControlID="dateplanned" runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </li>
                            <li class="trvl_date" id="RevisedUatdate" runat="server" visible="false">
                                <span>Revised Date</span> 
                                <br />
                                <asp:TextBox AutoComplete="off" ID="txtRevisedUatdate" Width="200px" Enabled="true" runat="server" oncopy="return false" onpaste="return false"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender27" Format="dd/MM/yyyy" TargetControlID="txtRevisedUatdate" runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </li>
                            <li></li>
                            <li style="border-top: 2px solid black; width: 100%; margin: 10px 0; list-style: none;"></li>
                            <li>
                                <span>
                                    <asp:Label ID="Label1" runat="server" Text="Go-Live" Style="color: orange; font-size: 16px; font-weight: 400; text-align: center;"></asp:Label>
                                </span></li>
                            <li></li>
                            <li></li>
                            <li class="trvl_date">
                                <br />
                                <span>Go-Live Planned Date</span>&nbsp;&nbsp;<span style="color: red">*</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="dategolive" Width="200px" runat="server" Enabled="true"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender14" Format="dd/MM/yyyy" TargetControlID="dategolive" runat="server">
                                </ajaxToolkit:CalendarExtender>

                            </li>
                            <li class="trvl_date" id="RevisedGoliveDate" runat="server" visible="false">
                                <span>Revised Date</span> 
                                <br />
                                <asp:TextBox AutoComplete="off" ID="txtRevisedGoliveDate" Width="200px" Enabled="true" runat="server" oncopy="return false" onpaste="return false"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender28" Format="dd/MM/yyyy" TargetControlID="txtRevisedGoliveDate" runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </li>
                            <li></li>
                            <li style="border-top: 2px solid black; width: 100%; margin: 10px 0; list-style: none;"></li>


                            <li>
                                <span>
                                    <asp:Label ID="Label25" runat="server" Text="Reusability" Style="color: orange; font-size: 16px; font-weight: 400; text-align: center;"></asp:Label>
                                </span></li>
                            <li></li>
                            <li></li>

                            <li class="trvl_date">
                                <br />
                                <span>Reusable Client Name</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="txtclientname" Width="200px" runat="server"></asp:TextBox>
                            </li>
                            <li class="trvl_date">
                                <span>Status</span>
                                <br />
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="DropdownListSearch" Style="width: 251px !important">
                                    <asp:ListItem Text="Select Status" Value="0" />
                                    <asp:ListItem Text="Copied" Value="1" />
                                    <asp:ListItem Text="WIP" Value="2" />
                                </asp:DropDownList><br />
                            </li>

                            <li class="trvl_date">
                                <span>Additional Efforts</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="txtefforts" Width="200px" runat="server" onkeypress="return isNumberKey(event);"></asp:TextBox>
                            </li>
                            <li class="trvl_date">
                                <span>Percentage</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="txtPercentage" Width="200px" runat="server" onkeypress="return isNumberKey(event);"></asp:TextBox>
                            </li>
                            <li class="trvl_date">
                                <span>Remarks/ Details</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="txtremark" Width="200px" runat="server"></asp:TextBox>
                            </li>
                            <li class="trvl_date">
                                <span>Custom Tcode</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="txttcode" Width="200px" runat="server"></asp:TextBox>
                            </li>
                            <li class="trvl_date">

                                <asp:LinkButton ID="LinkBtnSavePopup" runat="server" Text="Submit" ToolTip="Submit" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();" OnClick="LinkBtnSavePopup_Click"></asp:LinkButton>
                                <asp:LinkButton ID="LinkBtnBackPopup" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="LinkBtnBackPopup_Click">Back</asp:LinkButton>
                                <br />
                                <br />
                                <br />
                            </li>
                            <li></li>
                            <li></li>
                        </ul>
                    </div>
                </div>

                <asp:HiddenField ID="hdnABAPODIdId" runat="server" />
                <asp:HiddenField ID="HDID" runat="server" />
                <asp:HiddenField ID="hdnABAPODUploadId" runat="server" />

                <asp:HiddenField ID="hdnABAPODId" runat="server" />
                <asp:HiddenField ID="hdnEmpCode" runat="server" />

            </div>
        </div>
    </div>

    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />


    <script src="../js/freeze/jquery-ui.min.js"></script>
    <script src="../js/freeze/gridviewScroll.min.js"></script>



    <script type="text/javascript">



        function isNumberKey(evt) {
            var charCode = evt.which ? evt.which : evt.keyCode;
            // Allow only numbers (0–9)
            return charCode >= 48 && charCode <= 57;
        }

        $(document).ready(function () {
            $(".DropdownListSearch").select2();
            $("#MainContent_txtPOWO_Content").htmlarea();
            $("#MainContent_txt_POWOContent_description").htmlarea();
        });

        $(document).ready(function () {
            function applyGridViewScroll(selector, options) {
                $(selector).gridviewScroll(options);
            }
            $("#MainContent_DDLProjectLocation").select2();
            // $(".DropdownListSearch").select2();
            const gridOptions = {
                width: 1070,
                height: 400,
                freezesize: 3,
                headerrowcount: 1
            };

            applyGridViewScroll('#MainContent_gvRGSDetails', gridOptions);
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

        //$(document).ready(function () {
        //    $(".DropdownListSearch").select2();

        //    $('#MainContent_gvMngTravelRqstList').gridviewScroll({
        //        width: 1070,
        //        height: 600,
        //        freezesize: 5, // Freeze Number of Columns.
        //        headerrowcount: 1, //Freeze Number of Rows with Header.
        //    });

        //});

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

        $(document).ready(function () {
            function applyGridViewScroll(selector, options) {
                $(selector).gridviewScroll(options);
            }
            $("#MainContent_DDLProjectLocation").select2();
            $(".DropdownListSearch").select2();
            const gridOptions = {
                width: 1070,
                height: 400,
                freezesize: 3,
                headerrowcount: 1
            };

            applyGridViewScroll('#MainContent_gvRGSDetails', gridOptions);
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


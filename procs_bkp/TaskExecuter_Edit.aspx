<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="TaskExecuter_Edit.aspx.cs" ValidateRequest="false" Inherits="TaskExecuter_Edit" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <%--   <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
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

        #MainContent_lstApprover {
            overflow: hidden !important;
        }

        #MainContent_btn_Ref_Add,
        #MainContent_btn_FD_Cancel,
        #MainContent_btn_ATT_Save,
        #MainContent_btn_ATT_Update,
        #MainContent_btn_ATT_Cancel,
        #MainContent_lnk_Task_Create,
        #MainContent_lnk_CD_Update,
        #MainContent_lnk_CD_Cancel,
        #MainContent_lnk_PD_Save,
        #MainContent_lnk_PD_Update,
        #MainContent_lnk_PD_Cancel,
        #MainContent_lnk_DD_Save,
        #MainContent_lnk_DD_Update,
        #MainContent_lnk_DD_Cancel,
        #MainContent_lnk_DE_Save,
        #MainContent_lnk_DE_Update,
        #MainContent_lnk_FinalSubmit,
        #MainContent_lnk_FileSave,
        #MainContent_lnk_FileUpdate,
        #MainContent_lnk_FileCancel {
            background: #3D1956;
            color: #febf39 !important;
            padding: 0.5% 1.4%;
            margin: 0% 0% 0 0;
        }

        .noresize {
            resize: none;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <%--   <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>--%>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
    <script src="../js/HtmlControl/jHtmlArea-0.8.min.js"></script>
    <link href="../js/HtmlControl/jHtmlArea.css" rel="stylesheet" />

    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Task Executor"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>
                <span runat="server" id="backToSPOC" visible="false">
                    <a href="ExecuterProcessedTask.aspx?app=ex" class="aaaa">Back</a>
                </span>
                <span runat="server" id="backToEmployee" visible="false">
                    <a href="InboxExecuter.aspx" class="aaaa">Back</a>
                </span>
                <span runat="server" id="backToArr" visible="false">
                    <a href="ExecuterProcessedTask.aspx?app=su" class="aaaa">Back</a>
                </span>
				<span runat="server" id="BanktoIndex" visible="false">
                    <a href="TaskMonitoring.aspx" class="aaaa">Back</a>
                </span>
                <span>
                    <a href="TaskMonitoring.aspx" style="margin-right: 18px;" class="aaaa">Task Monitoring</a>&nbsp;&nbsp; 
                </span>
                <div class="edit-contact">
                    <div class="editprofile" style="text-align: center; border: none;" id="divmsg" runat="server" visible="false">
                    </div>
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server"></asp:LinkButton>
                        </div>
                    </div>
                    <ul id="editform" runat="server" visible="false">

                        <li class="mobile_inboxEmpCode">
                            <span><b>Task Reference</b></span><br />
                            <br />
                        </li>
                        <li>
                            <asp:Label runat="server" ID="Label2" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label></li>
                        <li class="mobile_InboxEmpName">
                            <span>Task Reference Id</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_TaskRefId" runat="server" Visible="True" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="mobile_InboxEmpName">
                            <span>Task Reference Date</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_Ref_Date" runat="server" Visible="True" Enabled="false"></asp:TextBox>
                        </li>
                      
                      
                        <li class="mobile_InboxEmpName">
                            <span>Meeting/Discussion Date</span><span style="color: red">*</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_Metting_Dis" Enabled="false" MaxLength="15" runat="server" OnTextChanged="txt_Metting_Dis_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txt_Metting_Dis"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>
                        <li>
                            <span>Meeting/Discussion Type </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList runat="server" ID="ddl_Meeting_Type" Enabled="false" DataTextField="Name"
                                DataValueField="Name" AppendDataBoundItems="false" AutoPostBack="false">
                            </asp:DropDownList>
                        </li>
                          <li class="mobile_InboxEmpName">
                            <span>Meeting/Discussion Title</span><span style="color: red">*</span><br />
                            <asp:TextBox AutoComplete="off" TextMode="MultiLine" CssClass="noresize" Rows="6" ID="txt_Metting_Title" Enabled="false" runat="server" Visible="True"></asp:TextBox>
                        </li>
                        <hr />
                        <li class="mobile_inboxEmpCode">
                            <span><b>Attendees List</b></span><br />
                            <br />
                        </li>
                        <li></li>
                        <li>
                            <span>Organizer</span>
                            <br />
                            <asp:TextBox ID="txt_Attendees" runat="server" Enabled="false" AutoPostBack="false" Visible="true"></asp:TextBox>
                        </li>
                        <li></li>
                        <li>
                            <span>Attendees</span>
                            <br />
                            <asp:TextBox ID="txt_Attendeess_Member" Enabled="false" CssClass="noresize" runat="server" TextMode="MultiLine" Rows="6" AutoPostBack="false" Visible="true"></asp:TextBox>
                        </li>

                         
                        <hr />
                        <li class="mobile_inboxEmpCode">
                            <span><b>Intimations List</b></span><br />
                            <br />
                        </li>
                        <li></li>
                        <li>
                            <span>Task Intimations</span>
                            <br />
                            <asp:TextBox ID="Txt_TaskIntimation" Enabled="false" CssClass="noresize" runat="server" TextMode="MultiLine" Rows="6" AutoPostBack="false" Visible="true"></asp:TextBox>
                        </li>

                        <li style="width: 50%">
                            <br />
                            <div>
                                <asp:GridView ID="dg_ATT_Details" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                    DataKeyNames="Id">
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
                                        <asp:BoundField HeaderText="Attendee Name"
                                            DataField="Attendee_Name"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="40%" ItemStyle-BorderColor="Navy" />

                                        <asp:TemplateField HeaderText="Organizer" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chk_Ref_Select" Checked='<%# Eval("IsOrganizer") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnk_Atend_Edit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnk_Atend_Edit_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnk_Atend_Delete" runat="server" Width="15px" Height="15px" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure delete?');" OnClick="lnk_Atend_Delete_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                            <br />
                        </li>
                        <hr />
                        <li class="mobile_inboxEmpCode">
                            <span><b>Task Details</b></span><br />
                            <br />
                        </li>
                        <li>
                            <asp:Label runat="server" ID="Label3" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>

                        </li>
                           <li class="mobile_InboxEmpName">
                            <span>Task Id</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_TaskId" runat="server" Visible="True" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="mobile_InboxEmpName">
                            <span>Task Creation Date</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_Created_Date" runat="server" Visible="True" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="mobile_InboxEmpName">
                            <span>Task Created By</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_Created_By" runat="server" Visible="True" Enabled="false"></asp:TextBox>
                        </li>
                          <li class="mobile_InboxEmpName">
                            <span>For Information Only</span><br />
                            <asp:CheckBox runat="server" Enabled="false" ID="chk_for_Info" />
                        </li>
                        <li>
                            <span>Task Supervisor</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox runat="server" ID="txt_Task_Supervisor" Enabled="false" AppendDataBoundItems="false" AutoPostBack="false">
                            </asp:TextBox>
                        </li>
                      
                        <li> 
                            <span>Task Executor</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox runat="server" ID="txt_Task_Executor" Enabled="false" AppendDataBoundItems="false" AutoPostBack="false">
                            </asp:TextBox>
                        </li>
                        <li> 
                            <span>Project / Location</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox runat="server" ID="TxtProjectLocation" Enabled="false" AppendDataBoundItems="false" AutoPostBack="false">
                            </asp:TextBox>
                        </li>
                        <li></li>
                       

                        <li class="mobile_InboxEmpName">
                            <span>Task Description</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_TaskDescripation" Enabled="false" CssClass="noresize" TextMode="MultiLine" Rows="4" runat="server" Visible="True"></asp:TextBox>
                        </li>
                        <li class="mobile_InboxEmpName">
                            <span>Task Remarks</span><span style="color: red">*</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_TaskRemark" Enabled="false" CssClass="noresize" TextMode="MultiLine" Rows="4" runat="server" Visible="True"></asp:TextBox>
                        </li>
                         <li class="mobile_InboxEmpName">
                            <span>Task Due Date</span><span style="color: red">*</span><br />
                            <asp:TextBox AutoComplete="off" AutoPostBack="true" ID="txt_Due_Date" Enabled="false" MaxLength="15" runat="server" OnTextChanged="txt_R_Due_Date_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txt_Due_Date"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>
                        <li></li>
                        <li>
                            <span>Reminder Before Due Date</span>
                            <br />
                            <asp:CheckBox AutoPostBack="false" Enabled="false" runat="server" ID="chk_Reminder_Due_Date" />
                        </li>
                        <li class="mobile_InboxEmpName">
                            <span>Reminder Before Days</span><br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txt_Reminder_Day" runat="server" Visible="True" MaxLength="2"></asp:TextBox>
                        </li>
                        <li>
                            <span>Escalation Mail After Due Date</span>
                            <br />
                            <asp:CheckBox AutoPostBack="false" Enabled="false" runat="server" ID="chk_Escalation_Due_Date" />
                        </li>
                        <li>
                            <span>Repeated Escalation Mail</span>
                            <br />
                            <asp:CheckBox AutoPostBack="false" Enabled="false" runat="server" ID="chk_Escalation_Repeate" />
                        </li>
                        <li class="mobile_InboxEmpName">
                            <span>Escalation Mail Frequency (Days)</span><br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txt_Reminder_Repe_Day" runat="server" Visible="True" MaxLength="2"></asp:TextBox>
                        </li>
                        <li></li>
                        <li class="mobile_inboxEmpCode" id="UploadedFile" runat="server" visible="false">
                            <span><b>Uploaded Files</b></span>
                            <br />
                        </li>
                        <li></li>
                        <li style="width: 50%">
                            <br />
                            <div>
                                <asp:GridView ID="gv_Documents" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                    DataKeyNames="Id,FileName">
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

                                        <asp:BoundField HeaderText="File Uploaded"
                                            DataField="FileName"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />
                                        <asp:TemplateField HeaderText="File View" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnkEdit1" runat="server" ToolTip=" File View" Width="15px" Height="15px" ImageUrl="~/Images/Download.png" OnClientClick=<%# "DownloadFile('" + Eval("FileName") + "')" %> />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                            <br />
                        </li>
                        <hr />
                        <%--<!-->--%>
                        <li class="mobile_InboxEmpName" id="actionHide" runat="server">
                            <span>Action</span><span style="color: red">*</span>
                            <br />
                            <asp:DropDownList runat="server" ID="ddl_Action_Type" Visible="True" AppendDataBoundItems="false" AutoPostBack="true" OnSelectedIndexChanged="ActionTypeDropDownList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <asp:Label runat="server" ID="Label1" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                        </li>
                        <li>
                            <br />
                            <span style="font-weight: bold" id="lblAssignTask" runat="server"></span>
                            <br />
                        </li>
                        <li>
                            <br />
                        </li>
                        <%--//Assgine Task--%>

                        <li runat="server" visible="false" id="AssgineTask1">
                            <br />
                            <span>Task Supervisor</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox runat="server" Enabled="false" ID="txt_Supervisor" Visible="true">
                            </asp:TextBox>
                        </li>
                        <li runat="server" id="AssgineTask2" visible="false" class="mobile_InboxEmpName">
                            <span>Task Executor</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList runat="server" ID="ddl_Executer" AppendDataBoundItems="false" AutoPostBack="false">
                            </asp:DropDownList>
                        </li>

                        <li runat="server" id="AssgineTask3" visible="false">
                            <span>Due Date</span><span style="color: red">*</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_R_Due_Date" MaxLength="15" runat="server" AutoPostBack="true" OnTextChanged="txt_R_Due_Date_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" Format="dd/MM/yyyy" TargetControlID="txt_R_Due_Date"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>
                        <li runat="server" id="AssgineTask4" visible="false">
                            <span>Reminder Before Due Date</span>
                            <br />
                            <asp:CheckBox AutoPostBack="false" Checked="true" runat="server" ID="Chk_R_Before_Due_Date" />
                        </li>
                        <li class="mobile_InboxEmpName" runat="server" visible="false" id="AssgineTask5">
                            <span>Reminder Before Days</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_R_Reminder_Days" Text="1" runat="server" MaxLength="2"></asp:TextBox>
                        </li>
                        <li runat="server" id="AssgineTask6" visible="false">
                            <span>Escalation Mail After Due Date</span>
                            <br />
                            <asp:CheckBox AutoPostBack="false" Checked="true" runat="server" ID="Chk_R_Escalation_Due_Date" />
                        </li>
                        <li class="mobile_InboxEmpName" runat="server" id="AssgineTask7" visible="false">
                            <span>Escalation Mail Frequency (Days)</span><br />
                            <asp:TextBox AutoComplete="off" Text="1" ID="txt_R_Escalation_Mail_Freq" runat="server" MaxLength="2"></asp:TextBox>
                        </li>
                        <li runat="server" id="AssgineTask8" visible="false">
                            <span style="display:none" >Repeated Escalation Mail</span>
                            <br />
                            <asp:CheckBox AutoPostBack="false" Checked="true" Style="display:none" runat="server" ID="Chk_R_RepeatedEscalation" />
                        </li>
                        <%-- Due Date Change request --%>
                        <li class="mobile_InboxEmpName" runat="server" id="IsShowDueDate" visible="false">
                            <span>New Due Date</span><span style="color: red">*</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_R_NewDue_Date" MaxLength="15" runat="server" AutoPostBack="true" OnTextChanged="txt_R_NewDue_DateTextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" Format="dd/MM/yyyy" TargetControlID="txt_R_NewDue_Date"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>
                        <li runat="server" id="NewDueDate1" visible="false"></li>
                        <li id="IsShowActionRemark" runat="server" visible="false">
                            <br />
                            <span>Action Remarks</span><span style="color: red">*</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_ActionRemarks" MaxLength="500" CssClass="noresize" TextMode="MultiLine" Rows="5" runat="server" Visible="true"></asp:TextBox>
                        </li>
                        <li id="IsShowUploadFile" runat="server" visible="false">
                            <br />
                            <span>Upload File</span><span style="color: red" runat="server" visible="false" id="IDUploadFileSpanProjectSchedule">*</span>
                            <br />
                            <asp:FileUpload ID="uploadfile" runat="server"  AllowMultiple="true" Visible="false"/>
                             <asp:FileUpload ID="UploadFileProject" runat="server"  AllowMultiple="false" Visible="false"/>
                        </li>
                        <li></li>
                        <li style="margin-top: 10px; align-items: center; width: 100%; margin-left: 25%;" runat="server" visible="false" id="IsShowButton">
                            <asp:LinkButton ID="lnk_Task_Create" Visible="true" runat="server" Text="Submit" ToolTip="Submit" OnClientClick="return SubmitClick();" CssClass="Savebtnsve" OnClick="lnk_Task_Submit_Click"></asp:LinkButton>

                        </li>

                         <%-- <li style="width: 50%">
                            <span id="Span1" runat="server" visible="false"><b>Uploaded Files</b></span><br />
                            <br />
                            <div>
                                <asp:GridView ID="GV_UploadExecute" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                    DataKeyNames="Id,FileName">
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

                                        <asp:BoundField HeaderText="File Uploaded"
                                            DataField="FileName"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />
                                        <asp:TemplateField HeaderText="File View" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnkEdit1" runat="server" ToolTip=" File View" Width="15px" Height="15px" ImageUrl="~/Images/Download.png" OnClientClick=<%# "DownloadFileExecute('" + Eval("FileName") + "')" %> />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>--%>

                        <hr />

                        <%-- Task History List--%>
                        <li class="mobile_inboxEmpCode">
                            <span><b>Task History</b></span><br />
                            <br />
                        </li>
                        <li style="width: 100%">
                            <div>
                                <asp:GridView ID="dv_TaskList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                    DataKeyNames="Task_ID">
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
                                            <asp:LinkButton ID="lnk_TaskList" runat="server" Visible="true" CommandArgument='<%#Eval("ID")%>' Width="15px" Height="15px" OnClick="lnk_TaskList_Click"><img src="../Images/edit.png" /></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                    </asp:TemplateField>
                                        <asp:BoundField HeaderText="Task ID"
                                            DataField="Task_ID"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="13%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Action By"
                                            DataField="ActionBy"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Action Date"
                                            DataField="Action_Date"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="7%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Action"
                                            DataField="StatusName"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Task Due Date"
                                            DataField="Due_Date"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="7%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Executor"
                                            DataField="Executer"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Supervisor"
                                            DataField="Supervisor"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Project / Location"  DataField="Location_name"
                                            ItemStyle-HorizontalAlign="Left"  HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                         

                                    </Columns>
                                </asp:GridView>
                            </div>
                            <br />
                        </li>

                        <%--   Task History List End--%>

                        <li id="IsShowTaskHistory" runat="server" visible="false">
                            <span><b>Task History Details</b></span><br />
                            <br />
                        </li>
                        <li id="IsShowTaskHistory1" runat="server" visible="false"></li>
                        <li class="mobile_InboxEmpName" id="IsShowTaskHistory2" runat="server" visible="false">
                            <span>Action Date</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_ActionDate_Hdtls" runat="server" Visible="True" Enabled="false"></asp:TextBox>
                        </li>

                        <li class="mobile_InboxEmpName" id="IsShowTaskHistory3" runat="server" visible="false">
                            <span>Task Due Date</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_DueDate_Hdtls" runat="server" Visible="True" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="mobile_InboxEmpName" id="IsShowTaskHistory4" runat="server" visible="false">
                            <span>Action</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_Action_Hdtls" runat="server" Visible="True" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="mobile_InboxEmpName" id="IsShowTaskHistory5" runat="server" visible="false">
                            <span>Supervisor</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_Supervisor_Hdtls" runat="server" Visible="True" Enabled="false"></asp:TextBox>
                        </li>

                        <li class="mobile_InboxEmpName" id="IsShowTaskHistory6" runat="server" visible="false">
                            <span>Action By</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_ActionBy_Hdtls" runat="server" Visible="True" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="mobile_InboxEmpName" id="IsShowTaskHistory7" runat="server" visible="false">
                            <span>Executor</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_Executer_Hdtls" runat="server" Visible="True" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="mobile_InboxEmpName" id="IsShowTaskHistory8" runat="server" visible="false">
                            <span>Remarks</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_Remarks_Hdtls" CssClass="noresize" TextMode="MultiLine" Rows="4" runat="server" Visible="True" Enabled="false"></asp:TextBox>
                        </li>


                        <br />
                        <hr id="Assigntskhr" runat="server" />
                        <li class="mobile_inboxEmpCode" id="AssignSubTask" runat="server" visible="false">
                            <span><b>Assigned Sub Tasks</b></span><br />
                            <br />
                        </li>
                        <li style="width: 100%">
                            <asp:GridView ID="gv_SubTaskList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                DataKeyNames="Task_ID">
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
                                            <asp:LinkButton ID="TaskExecuter_Edit" runat="server" Visible="true" CommandArgument='<%#Eval("ID")+","+ Eval("Task_Ref_id")%>' Width="15px" Height="15px" OnClick="TaskExecuter_Edit_Click"><img src="../Images/edit.png" /></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Sub Task Id"
                                        DataField="Task_ID"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="25%" ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Executor"
                                        DataField="Executor"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Supervisor"
                                        DataField="Supervisor"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Project / Location"  DataField="Location_name"
                                            ItemStyle-HorizontalAlign="Left"  HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Task Description"
                                        DataField="Task_Description"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="25%" ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Task Due Date"
                                        DataField="Due_Date"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Reminder Before"
                                        DataField="ReminderBefore"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Status"
                                        DataField="StatusName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
                                </Columns>
                            </asp:GridView>
                        </li>
                        <br />
                        <hr id="SubTaskDtl" runat="server" />
                        <li class="mobile_inboxEmpCode" id="SubTaskHistory" runat="server" visible="false">
                            <span><b>Sub Tasks History</b></span>
                            <br />
                        </li>
                        <li style="width: 100%">
                            <br />
                            <div>

                                <asp:GridView ID="dv_SubTaskHistoryList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                    DataKeyNames="Task_ID">
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
                                        <asp:BoundField HeaderText="Task ID"
                                            DataField="Task_ID"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="25%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Action By"
                                            DataField="ActionBy"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Action Date"
                                            DataField="Action_Date"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Action"
                                            DataField="StatusName"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Due Date"
                                            DataField="Due_Date"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Executor"
                                            DataField="Executer"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Supervisor"
                                            DataField="Supervisor"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                                         <asp:BoundField HeaderText="Project / Location"  DataField="Location_name"
                                            ItemStyle-HorizontalAlign="Left"  HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                        <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="TaskExecuterSubTask_Edit" runat="server" Visible="true" CommandArgument='<%#Eval("Task_ID")+","+ Eval("ID")%>' Width="15px" Height="15px" OnClick="TaskExecuterSubTask_Edit_Click"><img src="../Images/edit.png" /></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <br />
                        </li>
                        <hr id="subtask" runat="server" />


                        <li id="SubTskHistoruDetails" runat="server" visible="false">
                            <span><b>Sub Task History Details</b></span><br />
                            <br />
                        </li>

                        <li></li>
                        <li class="mobile_InboxEmpName" id="ActionDate" runat="server">
                            <span>Action Date</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_ActionDate_SubHistory" runat="server" Visible="True" Enabled="false"></asp:TextBox>
                        </li>

                        <li class="mobile_InboxEmpName" id="Status" runat="server">
                            <span>Status</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_Status_SubHistory" runat="server" Visible="True" Enabled="false"></asp:TextBox>
                        </li>

                        <li class="mobile_InboxEmpName" id="ActionBy" runat="server">
                            <span>Action By</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_ActionBy_SubHistory" runat="server" Visible="True" Enabled="false"></asp:TextBox>
                        </li>
                        <li></li>
                        <li class="mobile_InboxEmpName" id="Remarks" runat="server">
                            <span>Remarks</span><br />
                            <asp:TextBox AutoComplete="off" CssClass="noresize" TextMode="MultiLine" Rows="4" ID="txt_Remarks_SubHistory" runat="server" Visible="True" Enabled="false"></asp:TextBox>
                        </li>
                        <li></li>

                        <li style="width: 50%">
                            <span id="file" runat="server"><b>Uploaded Files</b></span><br />
                            <br />
                            <div>
                                <asp:GridView ID="gv_Document" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                    DataKeyNames="Id,FileName">
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

                                        <asp:BoundField HeaderText="File Uploaded"
                                            DataField="FileName"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />
                                        <asp:TemplateField HeaderText="File View" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnkEdit1" runat="server" ToolTip=" File View" Width="15px" Height="15px" ImageUrl="~/Images/Download.png" OnClientClick=<%# "DownloadFile('" + Eval("FileName") + "')" %> />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="mobile_Savebtndiv">
    </div>

    <br />
    <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>--%>
    <%--    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />--%>

    <asp:HiddenField ID="hdnvouno" runat="server" />
    <asp:HiddenField ID="hdnIsMarried" runat="server" />
    <asp:HiddenField ID="hdnTaskRefID" runat="server" />
    <asp:HiddenField ID="hdnAttendeeID" runat="server" />
    <asp:HiddenField ID="hdnTaskID" runat="server" />
    <asp:HiddenField ID="hdnCertificationDetailID" runat="server" />
    <asp:HiddenField ID="hdnProjectDetailID" runat="server" />
    <asp:HiddenField ID="hdnDomainDetailID" runat="server" />
    <asp:HiddenField ID="hdnFileDetailID" runat="server" />
    <asp:HiddenField ID="hdnFileName" runat="server" />
    <asp:HiddenField ID="hdnFilePath" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="hdnempcode" runat="server" />
    <asp:HiddenField ID="hdnTask_Executer" runat="server" />
    <asp:HiddenField ID="hdnTask_Supervisor" runat="server" />
    <asp:HiddenField ID="hdnTaskSub_Id" runat="server" />
    <asp:HiddenField ID="hdnTaskStatus_Id" runat="server" />
    <asp:HiddenField ID="HDMeeting_Type_Id" runat="server" />

    <asp:HiddenField ID="FilePath" runat="server" />

    <%--<script src="../js/dist/jquery-3.2.1.min.js"></script>--%>
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <script type="text/javascript">      
        $(document).ready(function () {
            $("#MainContent_ddl_Meeting_Type").select2();
            $("#MainContent_ddl_Meeting_Id").select2();
            $("#MainContent_ddl_Attendees").select2();
            $("#MainContent_ddl_Task_Supervisor").select2();
            $("#MainContent_ddl_Task_Executor").select2();
            $("#MainContent_ddl_Supervisor").select2();
            $("#MainContent_ddl_Executer").select2();
            $("#MainContent_ddl_Action_Type").select2();

        });
    </script>
    <script type="text/javascript">

        function onCalendarShown() {
            var cal = $find("calendar1");
            cal._switchMode("years", true);
            if (cal._yearsBody) {
                for (var i = 0; i < cal._yearsBody.rows.length; i++) {
                    var row = cal._yearsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }

        function onCalendarHidden() {
            var cal = $find("calendar1");
            if (cal._yearsBody) {
                for (var i = 0; i < cal._yearsBody.rows.length; i++) {
                    var row = cal._yearsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }
        function call(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "year":
                    var cal = $find("calendar1");
                    cal.set_selectedDate(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged(); break;
            }
        }
        function checkProjectEndDate(sender, args) {
            if (sender._selectedDate >= new Date()) {
                alert("You can not select a future date than today!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }

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



        function onCharOnlyNumber(e) {
            var keynum;
            var keychar;
            var numcheck = /[0123456789.]/;

            if (window.event) {
                keynum = e.keyCode;
            }
            else if (e.which) {
                keynum = e.which;
            }
            keychar = String.fromCharCode(keynum);
            return numcheck.test(keychar);
        }
        //Confirmation Attendee
        //Cinfirmation Family Details
        function SaveFDClick() {
           <%-- try {
                var msg = "Do you want to Submit";
                var retunboolean = true;
                var ele = document.getElementById('<%=btn_ATT_Save.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        Confirm(msg);
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;--%>
        }
        function SaveFD2Click() {
           <%-- try {
                var msg = "Do you want to update?";
                var retunboolean = true;
                var ele = document.getElementById('<%=btn_ATT_Update.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        Confirm(msg);
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;--%>
        }

        //CreateTaskClick()
        function SubmitClick() {
            try {
                var msg = "Do you want to submit?";
                var retunboolean = true;
                var ele = document.getElementById('<%=lnk_Task_Create.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true) {
                        Confirm(msg);
                        <%--var confirm_value = document.createElement("INPUT");
                        confirm_value.type = "hidden";
                        confirm_value.name = "confirm_value";
                        confirm_value.value = "Yes";
                        document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;--%>
                    }
                }

            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }



        function Confirm(msg) {
            //Testing();
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm(msg)) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }

            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return;
        }
        // Delete

        //End Delete




        function onCharOnlyNumber_EXP(e) {
            var keynum;
            var keychar;
            var numcheck = /[0123456789.]/;

            if (window.event) {
                keynum = e.keyCode;
            }
            else if (e.which) {
                keynum = e.which;
            }
            keychar = String.fromCharCode(keynum);
            return numcheck.test(keychar);
        }
        function onCharOnlyNumber_Mobile(e) {
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

        function DownloadFile(FileName) {

            var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;
            //alert(FileName);        
            //window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
            window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
        }

       
    </script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="Task_D_DateChange.aspx.cs" ValidateRequest="false" Inherits="Task_D_DateChange" %>

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
        #MainContent_lnk_Task_Update,
        #MainContent_lnk_Task_Cancel,
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
        #MainContent_lnk_FileCancel,
        #MainContent_lnk_Final_Submit {
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
                        <asp:Label ID="lblheading" runat="server" Text="Due Date Change Request"></asp:Label>
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
                <span runat="server" id="backToArr" visible="true">
                    <a href="Task_DueDateChange_Inbox.aspx" class="aaaa">Back</a>
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
                        <%--<li class="mobile_inboxEmpCode">
                            <span><b>Task Details</b></span><br />
                            <br />
                        </li>
                        <li></li>--%>
                        <li class="mobile_inboxEmpCode">
                            <span><b>Task Reference</b></span><br />
                            <br />
                        </li>
                        <li><asp:Label runat="server" ID="Label2" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label></li>
                        <li class="mobile_InboxEmpName">
                            <span>Task Reference Id</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_Ref_Id" runat="server" Visible="True" Enabled="false"></asp:TextBox>
                        </li>
                        <li>
                            <span>Task Reference Date</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_Ref_Date" runat="server" Visible="True" Enabled="false"></asp:TextBox>
                        </li>
                      
                        <li class="mobile_InboxEmpName">    
                            <span>Meeting/Discussion Date</span><br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txt_Metting_Dis" MaxLength="15" runat="server"></asp:TextBox>
                          
                        </li>
                        <li>
                            <span>Meeting/Discussion Type</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_MeetingType" Enabled="false" runat="server" Visible="True"></asp:TextBox>
                        </li>
                          <li class="mobile_InboxEmpName">
                            <span>Meeting/Discussion Title</span><br />
                            <asp:TextBox AutoComplete="off" TextMode="MultiLine" CssClass="noresize" Rows="6" ID="txt_Metting_Title" Enabled="false" runat="server" Visible="True"></asp:TextBox>
                        </li>
                       <hr />
                        <li class="mobile_inboxEmpCode">
                            <span><b>Attendees List</b></span><br />
                            <br />
                        </li>
                        <li>
                            <asp:Label runat="server" ID="Label1" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label></li>
                        <li>
                            <span>Organizer</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_Organizer" Enabled="false" runat="server" Visible="True"></asp:TextBox>
                        </li>
                        <li></li>
                        <li>
                            <span>Attendees</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_Attendees" Enabled="false" runat="server" CssClass="noresize" TextMode="MultiLine" Rows="4" Visible="True"></asp:TextBox>
                        </li>      
                        <li></li>

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
                         <li></li>
                        <hr />
                        <li class="mobile_inboxEmpCode">
                            <span><b>Task Details</b></span><br />
                            <br />
                        </li>
                        <li><asp:Label runat="server" ID="Label3" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label></li>
                       <li class="mobile_InboxEmpName">
                            <span>Task Id</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_Task_Id" runat="server" Visible="True" Enabled="false"></asp:TextBox>
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
                            <asp:CheckBox runat="server" ID="chk_for_Info" Enabled="false"/>
                        </li>
                        <li>
                            <span>Task Supervisor</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_Supervisor" runat="server" Visible="True" Enabled="false"></asp:TextBox>

                        </li>
                        <li>
                            <span>Task Executor</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_Executor" runat="server" Visible="True" Enabled="false"></asp:TextBox>
                        </li>

                        <li>
                            <span>Project / Location</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtprojectLocation" runat="server" Visible="True" Enabled="false"></asp:TextBox>
                        </li>
                      <li></li>

                        <li class="mobile_InboxEmpName">
                            <span>Task Description</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_TaskDescripation" CssClass="noresize" Enabled="false" TextMode="MultiLine" Rows="4" runat="server" Visible="True"></asp:TextBox>
                        </li>
                        <li class="mobile_InboxEmpName">
                            <span>Task Remarks</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_TaskRemark" runat="server" Enabled="false" CssClass="noresize" TextMode="MultiLine" Rows="4" Visible="True"></asp:TextBox>
                        </li>
                        
                        <li style="width:30" >
                            <br />
                            <span runat="server" id="IsShowUploadFile" visible="false">Uploaded File</span>  
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
                                        
                                        <asp:BoundField HeaderText="File Name"
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
                                       <%-- <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnk_File_Delete" runat="server" Width="15px" Height="15px" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure delete?');" OnClick="lnk_File_Delete_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <br />
                        </li>                      
                        <li></li>
                        <li class="mobile_InboxEmpName">
                            <span>Current Status</span><br />
                            <asp:TextBox AutoComplete="off" Text="Assigned" ID="txt_Current_Status" runat="server" Visible="True" Enabled="false"></asp:TextBox>
                        </li>
                        <li></li>
                          <li class="mobile_InboxEmpName">
                            <span>Task Due Date</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_Due_Date" MaxLength="15" runat="server" Enabled="false" ></asp:TextBox>
                           
                        </li>
                        <li class="mobile_InboxEmpName">
                            <span>New Task Due Date</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_NewDueDate" MaxLength="15" runat="server" Enabled="false"></asp:TextBox>
                           
                        </li>
                         <li class="mobile_InboxEmpName">
                            <span>Task Executor Remarks</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_Executer_Remarks" CssClass="noresize" Enabled="false" TextMode="MultiLine" Rows="4" runat="server" Visible="True"></asp:TextBox>
                        </li>
                        
                        <li class="mobile_InboxEmpName">
                            <span>Task Remarks</span><span style="color: red">*</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_Current_remark" runat="server"  CssClass="noresize" TextMode="MultiLine" Rows="4" Visible="True"></asp:TextBox>
                        </li>
                        <li style="margin-top: 10px; align-items: center; width: 100%; margin-left: 20%;">
                            <asp:LinkButton ID="lnk_Task_Create" Visible="true" runat="server" Text="Approve Due Date Change Request" ToolTip="Create" OnClientClick="return CreateTaskClick();" CssClass="Savebtnsve" OnClick="lnk_Task_Create_Click"></asp:LinkButton>
                            <asp:LinkButton ID="lnk_Task_Update" Visible="true" runat="server" Text="Reject Due Date Change Request" ToolTip="Update" OnClientClick="return UpdateTaskClick();" CssClass="Savebtnsve" OnClick="lnk_Task_Update_Click"></asp:LinkButton>
                        </li>                       
                        <hr />                        
                        <li class="mobile_inboxEmpCode">
                            <span><b>Task History</b></span><br />
                            <br />
                        </li>
                        <li></li>
                        <li style="width: 100%">
                            <br />
                            <div>

                                <asp:GridView ID="gv_TaskHistory" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
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
                                         <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                         <ItemTemplate>
                                             <asp:ImageButton ID="lnk_View_History" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnk_View_History_Click" />
                                         </ItemTemplate>
                                         <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                     </asp:TemplateField>     
                                        <asp:BoundField HeaderText="Task ID"
                                            DataField="Task_ID"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />                                        
                                      
                                        <asp:BoundField HeaderText="Action By"
                                            DataField="ActionBy"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
                                       <asp:BoundField HeaderText="Action Date"
                                            DataField="ActionDate"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Action"
                                            DataField="StatusName"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
                                         <asp:BoundField HeaderText="Task Due Date"
                                            DataField="DueDate"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="7%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Executor"
                                            DataField="ExecutorName"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Supervisor"
                                            DataField="SupervisorName"
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
                         <li class="mobile_inboxEmpCode" runat="server" id="IsShowHistoryDetails" visible="false">
                            <span><b>Task History Details</b></span><br />
                            <br />
                        </li>
                        <li></li>
                       <li runat="server" id="s1" visible="false" class="mobile_InboxEmpName">
                            <span>Action Date</span><br />
                            <asp:TextBox AutoComplete="off" ID="TextBox1" runat="server" Visible="True" Enabled="false"></asp:TextBox>
                        </li>
                        <li runat="server" id="s2" visible="false" class="mobile_InboxEmpName">
                            <span>Due Date</span><br />
                            <asp:TextBox AutoComplete="off" ID="TextBox2" runat="server" Visible="True" Enabled="false"></asp:TextBox>
                        </li>
                        <li runat="server" id="s3" visible="false" class="mobile_InboxEmpName">
                            <span>Action</span><br />
                            <asp:TextBox AutoComplete="off" ID="TextBox3" runat="server" Visible="True" Enabled="false"></asp:TextBox>
                        </li>
                        <li runat="server" id="s4" visible="false" class="mobile_InboxEmpName">
                            <span>Supervisor</span><br />
                            <asp:TextBox AutoComplete="off" ID="TextBox4" runat="server" Visible="True" Enabled="false"></asp:TextBox>
                        </li>
                         <li runat="server" id="s5" visible="false" class="mobile_InboxEmpName">
                            <span>Action By</span><br />
                            <asp:TextBox AutoComplete="off" ID="TextBox5" runat="server" Visible="True" Enabled="false"></asp:TextBox>
                        </li>
                        <li runat="server" id="s6" visible="false" class="mobile_InboxEmpName">
                            <span>Executor</span><br />
                            <asp:TextBox AutoComplete="off" ID="TextBox6" runat="server" Visible="True" Enabled="false"></asp:TextBox>
                        </li>
                         <li runat="server" id="s7" visible="false" class="mobile_InboxEmpName">
                            <span>Remarks</span><br />
                            <asp:TextBox AutoComplete="off" ID="TextBox7" CssClass="noresize" TextMode="MultiLine" Rows="4" runat="server" Visible="True" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="mobile_InboxEmpName">
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
    <asp:HiddenField ID="hdnExCode" runat="server" />
    <asp:HiddenField ID="hdnSupcode" runat="server" />


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
      
        //CreateTaskClick()
        function CreateTaskClick() {
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

        function UpdateTaskClick() {
            try {
                var msg = "Do you want to submit?";
                var retunboolean = true;
                var ele = document.getElementById('<%=lnk_Task_Update.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true) {
                        Confirm(msg);
                       <%-- var confirm_value = document.createElement("INPUT");
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
           // window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
           window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
        }
    </script>
</asp:Content>

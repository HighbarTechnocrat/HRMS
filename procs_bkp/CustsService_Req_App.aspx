<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="CustsService_Req_App.aspx.cs" Inherits="CustsService_Req_App" %>

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

        #MainContent_service_btnAssgine,
        #MainContent_service_btnClose, 
        #MainContent_service_btnEscelateToHOD,
        #MainContent_Service_ChangetargetDate,
        #MainContent_service_btnEscelateToCEO,
        #MainContent_service_btnClearText,
        #MainContent_service_NewTargetDateApprove,
        #MainContent_service_NewTargetDateReject,
        #MainContent_service_btnSendSPOC,
        #MainContent_service_ReOpen,
        #MainContent_mobile_btnCorrection {
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
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
 
    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Employee Service request"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>
                <span runat="server" id="backToSPOC" visible="false">
                    <a href="Custs_InboxServiceRequest.aspx" class="aaaa">Back</a>
                </span>
                <span runat="server" id="backToEmployee" visible="false">
                  
                </span>
                <span runat="server" id="backToArr" visible="false">
                    <a href="Custs_InboxServiceRequest_Arch.aspx" class="aaaa">Back</a>
                </span>
                <span runat="server" id="backToEscalatedService" visible="false">
                    <a href="Custs_Service.aspx" class="aaaa">Back</a>
                </span>
                <span>
                    <a href="Custs_Service.aspx" style="margin-right: 18px;" class="aaaa">CUSTOMERFIRST Service Request</a>&nbsp;&nbsp; 
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

                        <li class="mobile_inboxEmpCode" style="display:none">                          

                            <asp:TextBox AutoComplete="off" ID="Txt_" runat="server" MaxLength="50" Visible="false" Enabled="false"> </asp:TextBox>
                        </li>
                        <li class="mobile_InboxEmpName"  style="display:none">
                            <span>Employee Name</span> 
                            <asp:TextBox AutoComplete="off" ID="TextBox3" runat="server" Visible="True" Enabled="false"></asp:TextBox>

                        </li>
                        <li class="mobile_InboxEmpName">
                            <span>Customer Name</span><br />

                            <asp:TextBox AutoComplete="off" ID="txtEmpName" runat="server" Visible="True" Enabled="false"></asp:TextBox>

                        </li>
                        <li class="mobile_InboxEmpName">
                            <span>Project/ Location</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_DeptName" Enabled="false" runat="server"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span style="display: none">Employee Code  
                            <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="True" Enabled="false"> </asp:TextBox>
                            </span>

                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span style="display: none">Designation   
                            <br />
                                <asp:TextBox AutoComplete="off" ID="Txt_Designation" Enabled="false" runat="server" AutoPostBack="true"></asp:TextBox>
                            </span>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Customer Email</span><br />
                            <asp:TextBox AutoComplete="off" ID="Txt_EmpEmail" runat="server" MaxLength="50" Visible="True" Enabled="false"> </asp:TextBox>

                        </li>
                        <li class="mobile_inboxEmpCode">
                             <span>Created Date</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtFirstCreatedDate" runat="server" MaxLength="100" Enabled="false"></asp:TextBox>
                            <span style="display: none">Employee Mobile                           
                            <asp:TextBox AutoComplete="off" ID="Txt_EmpMobile" Enabled="false" runat="server" AutoPostBack="true"></asp:TextBox>
                            </span>
                        </li>
                        <li>
                            <span style="display: none">Service Request Raised For Department                            
                            <asp:TextBox AutoComplete="off" ID="txtSelectedDepartment" runat="server" MaxLength="100" Enabled="false"></asp:TextBox>
                            </span>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Service Category</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_category" runat="server" MaxLength="100" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode" style="visibility: hidden"></li>
                        <li class="mobile_inboxEmpCode">
                            <span>Service Request Description</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtFirstDeacription" runat="server" TextMode="MultiLine" Rows="10" Width="188%" CssClass="noresize" Enabled="false"></asp:TextBox>
                        </li>
                        <li style="visibility: hidden"></li>

                        <li class="mobile_inboxEmpCode">
                            <span>System Target Date</span>
                            <br />
                             <asp:TextBox AutoComplete="off" ID="TxtSystemTargetDate" Enabled="false" runat="server"></asp:TextBox>
                            
                        </li>
                        <li >
                            <span  id="SpanNewTagetDate" runat="server">New Target Date</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtNewtargetDate"  onkeypress="return false;" onkeydown="return false;" OnTextChanged="txtNewtargetDate_TextChanged"  AutoPostBack="true" runat="server"></asp:TextBox>
                             <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd-MM-yyyy"  TargetControlID="txtNewtargetDate" runat="server">
                                </ajaxToolkit:CalendarExtender>

                        </li>

                        <li class="mobile_grid" id="liuploadedFiles1" runat="server">
                            <span>Uploaded Files</span>
                            <br />
                            <a href="#" onclick="return DownloadFileEmployee()"><span runat="server" id="lblCreateFile"></span></a>
                        </li>
                        <li id="liuploadedFiles2" runat="server"></li>
                        
                        <li class="mobile_date" runat="server" id="empShow1">
                            <span><b runat="server" id="lblAssgineTitle">Assignment Details </b></span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtFromdate" runat="server" AutoPostBack="True" Visible="false" Enabled="false"></asp:TextBox>
                        </li>

                        <li class="mobile_date" style="visibility: hidden"></li>
                          <li class="claimmob_fromdate" runat="server" id="empShow12">
                             <span runat="server" id="lblActionAssginDate">Assignment Date</span>&nbsp;&nbsp;<br />
                            <asp:TextBox AutoComplete="off" ID="TXT_AssginmentDate" Enabled="false" runat="server"></asp:TextBox>
                            <div style="display:none"> 
                            <span>Assigned To Category </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList ID="ddlCategory" Enabled="false" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                            </asp:DropDownList>
                            </div>  
                        </li>

                        <li class="claimmob_fromdate" runat="server" id="empShow">
                            <span style="display: none">
                            <span>Assigned To Employee </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList ID="ddl_AssginmentEMP" runat="server" AutoPostBack="true">
                            </asp:DropDownList>
                            </span>
                            <span style="display: none">
                                <br />
                                <span>Assigned To Department </span>&nbsp;&nbsp; 
                            <br />
                                <asp:DropDownList ID="ddlDepartment" runat="server" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                </asp:DropDownList>
                            </span>

                        </li>
                      

                        <li class="mobile_Amount" runat="server" id="empShow2">
                           

                        </li>
                        <li class="claimmob_fromdate" runat="server" id="empShow3"></li>
                        <li runat="server" id="divsh"></li>
                        <li class="claimmob_Amount" runat="server" id="empShow4">
                            <span runat="server" id="lblActionAssginComment">Comments </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_SPOCComment" Enabled="true" runat="server" TextMode="MultiLine" MaxLength="500" Rows="6" Width="188%" CssClass="noresize"></asp:TextBox>

                        </li>


                        <li class="mobile_Amount" style="visibility: hidden"></li>

                        <li class="claimmob_Amount" runat="server" id="Li1">
                            <span runat="server" id="SpanApproverComment" visible="false">Approver Comments &nbsp;&nbsp;<span style="color: red">*</span></span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="TxtApproverComment" visible="false" Enabled="true" runat="server" TextMode="MultiLine" MaxLength="500" Rows="6" Width="188%" CssClass="noresize"></asp:TextBox>

                        </li>


                        <li class="mobile_Amount" style="visibility: hidden"></li>

                        <li class="mobile_inboxEmpCode" runat="server" id="empShow5">
                            <%-- <span>Reason: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtReason" runat="server" MaxLength="100" ></asp:TextBox>--%>
                            <span>Upload File</span>&nbsp;&nbsp;<%--<span style="color:red">*</span>--%><br />
                            <asp:FileUpload AutoComplete="off" ID="uploadfile" runat="server" AllowMultiple="true" />
                            <asp:LinkButton ID="lnkuplodedfile" OnClick="lnkuplodedfile_Click" runat="server"></asp:LinkButton>
                        </li>


                        <li class="mobile_inboxEmpCode" style="visibility: hidden"></li>

                        <li class="mobile_inboxEmpCode" style="width: 100%" runat="server" id="empShow6">
                            <asp:LinkButton ID="service_btnAssgine" Visible="false" runat="server" Text="Assgine Service Request" ToolTip="Assgine Service Request" CssClass="Savebtnsve" OnClientClick="return SendForAssigne();" OnClick="service_btnAssgine_Click">Assgine Service Request</asp:LinkButton>
                           <asp:LinkButton ID="Service_ChangetargetDate" Visible="false" runat="server" Text="Change Target Date" ToolTip="Change Target Date" CssClass="Savebtnsve"  OnClick="Service_ChangetargetDate_Click" >Change Target Date</asp:LinkButton>
                            <asp:LinkButton ID="service_btnClose" Visible="false" runat="server" Text="Close Service Request" ToolTip="Close Service Request" CssClass="Savebtnsve" OnClick="service_btnClose_Click" OnClientClick="return SendForClose();">Close Service Request</asp:LinkButton>
                            <asp:LinkButton ID="service_btnEscelateToHOD" Visible="false" runat="server" Text="Escelate to HOD" ToolTip="Escelate to HOD" CssClass="Savebtnsve" OnClick="service_btnEscelateToHOD_Click" OnClientClick="return SendForHOD();">Escelate to HOD</asp:LinkButton>
                            <asp:LinkButton ID="service_btnEscelateToCEO" Visible="false" runat="server" Text="Not Satisfied" ToolTip="Escelate To CEO" CssClass="Savebtnsve" OnClick="service_btnEscelateToCEO_Click" OnClientClick="return SendForCEO();">Not Satisfied</asp:LinkButton>
                            <asp:LinkButton ID="service_btnSendSPOC" Visible="false" runat="server" Text="Send Back SPOC" ToolTip="Send Back To SPOC" CssClass="Savebtnsve" OnClick="service_btnSendSPOC_Click" OnClientClick="return SendForSPOC();">Send Back To SPOC</asp:LinkButton>
                            <asp:LinkButton ID="service_ReOpen" Visible="false" runat="server" Text="ReOpen Service Request" ToolTip="ReOpen Service Request" CssClass="Savebtnsve" OnClick="service_ReOpen_Click" OnClientClick="return SendForReopen();">ReOpen Service Request</asp:LinkButton>
                           <asp:LinkButton ID="service_NewTargetDateApprove" Visible="false" runat="server" Text="NewTarget Date Approve"  CssClass="Savebtnsve"  OnClick="service_NewTargetDateApprove_Click">NewTarget Date Approve</asp:LinkButton>
                            <asp:LinkButton ID="service_NewTargetDateReject" runat="server" Visible="false" Text="NewTarget Date Reject"  CssClass="Savebtnsve" OnClick="service_NewTargetDateReject_Click" >NewTarget Date Reject</asp:LinkButton>
                            <asp:LinkButton ID="service_btnClearText" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="service_btnClearText_Click">Back</asp:LinkButton>
                            

                            <%--<asp:LinkButton ID="mobile_btnPrintPV" runat="server" Text="Print Payment Voucher" ToolTip="Print Payment Voucher" CssClass="Savebtnsve" OnClick="mobile_btnPrintPV_Click">Print Payment Voucher</asp:LinkButton>--%>

                        </li>
                        <hr runat="server" id="empShow7" />
                        <li>
                            <span><b>Service Request History</b> </span>
                        </li>
                        <li style="visibility: hidden"></li>
                        <li style="width: 100%;">
                            <!--<span>Upload File</span>-->
                            <span></span>
                            <asp:ListBox Visible="false" ID="lstApprover" runat="server"></asp:ListBox>
                            <asp:GridView ID="gvServiceHistory" runat="server" BackColor="White" DataKeyNames="Id" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%">
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
                                    <asp:BoundField HeaderText="Action By"
                                        DataField="ActionBy"
                                        ItemStyle-HorizontalAlign="left"
                                        ItemStyle-Width="20%"
                                        ItemStyle-BorderColor="Navy" />


                                    <asp:BoundField HeaderText="Received Date"
                                        DataField="ReceivedDate"
                                        ItemStyle-HorizontalAlign="left"
                                        ItemStyle-Width="18%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Action"
                                        DataField="StatusName"
                                        ItemStyle-HorizontalAlign="left"
                                        ItemStyle-Width="20%"
                                        ItemStyle-BorderColor="Navy" />


                                    <asp:BoundField HeaderText="Action Date"
                                        DataField="ActionDate"
                                        ItemStyle-HorizontalAlign="left"
                                        ItemStyle-Width="18%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Assigned To"
                                        DataField="AssignedBY"
                                        ItemStyle-HorizontalAlign="left"
                                        ItemStyle-Width="26%"
                                        ItemStyle-BorderColor="Navy" />
                                    <asp:TemplateField HeaderText="View" HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkEdit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkEdit_Click" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </li>
                        <li class="mobile_inboxEmpCode" style="visibility: hidden">
                            <asp:TextBox AutoComplete="off" ID="TextBox2" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
                        </li>
                        <hr />
                        <li class="mobile_Approver">
                            <span><b>Service Request History Details</b> </span>
                            <br />

                        </li>
                        <li style="visibility: hidden" class="mobile_inboxEmpCode"></li>
                        <li style="visibility: hidden" class="mobile_inboxEmpCode"></li>
                        <li class="mobile_inboxEmpCode">
                            <span>Action Date</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_AssgimentShowDate" runat="server" ReadOnly="true" Enabled="false"> </asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Action By</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtActionBy" runat="server" ReadOnly="true" Enabled="false"> </asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Action</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_ASDate" runat="server" ReadOnly="true" Enabled="false"> </asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Assigned To</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_Assigne_By" runat="server" ReadOnly="true" Enabled="false"> </asp:TextBox>
                            <asp:TextBox AutoComplete="off" ID="txt_Assign_By_EmpCode" Visible="false" runat="server" ReadOnly="true" Enabled="false"> </asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Service Request Description</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_Service_Description" runat="server" TextMode="MultiLine" Rows="6" Width="188%" CssClass="noresize" Enabled="false"></asp:TextBox>
                        </li>
                        <li style="visibility: hidden" class="mobile_inboxEmpCode"></li>
                        <li class="mobile_grid" id="liUploadedFiles3" runat="server">
                            <span>Uploaded Files</span>
                            <br />
                            <a href="#" onclick="return DownloadFile()"><span runat="server" id="bindFilePath"></span></a>
                              <asp:GridView ID="gvuploadedFiles_History" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
								DataKeyNames="Srno" >
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
									<asp:TemplateField HeaderText="File Name">
										<ItemTemplate>
											<asp:LinkButton ID="lnkviewfile" CssClass="BtnShow" runat="server" OnClientClick=<%#"CustomerDownloadFile('" + Eval("filename") + "')" %> Text='<%# Eval("filename") %>'>
											</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateField>
								 
								</Columns>
							</asp:GridView>
                        </li>
                        <li id="liUploadedFiles4" runat="server"></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="mobile_Savebtndiv">


        <%-- Following Popup for Approve Mobile Rem Request 
        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" DisplayModalPopupID="mpe" TargetControlID="mobile_btnSave">
        </ajaxToolkit:ConfirmButtonExtender>
        <ajaxToolkit:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="mobile_btnSave" OkControlID = "btnYes"
            CancelControlID="btnNo" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Confirmation
            </div>
            <div class="body">
                Do you want to Approve ?
            </div>
            <div class="footer" align="right">
                <asp:Button ID="btnNo" runat="server" Text="No" />
                <asp:Button ID="btnYes" runat="server" Text="Yes" />
            </div>
        </asp:Panel>
          End Here --%>


        <%-- Following Popup for Approve Mobile Rem Request  
        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" DisplayModalPopupID="mpe_COSACC" TargetControlID="mobile_btnSave_COSACC">
        </ajaxToolkit:ConfirmButtonExtender>
        <ajaxToolkit:ModalPopupExtender ID="mpe_COSACC" runat="server" PopupControlID="pnlPopup_COSACC" TargetControlID="mobile_btnSave_COSACC" OkControlID = "btnYes_COSACC"
            CancelControlID="btnNo_COSACC" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup_COSACC" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Confirmation
            </div>
            <div class="body">
                Do you want to Submit ?
            </div>
            <div class="footer" align="right">
                <asp:Button ID="btnNo_COSACC" runat="server" Text="No" />
                <asp:Button ID="btnYes_COSACC" runat="server" Text="Yes" />
            </div>
        </asp:Panel>
          End Here --%>



        <%-- Following Popup for Reject Mobile Rem Requestt  
        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" DisplayModalPopupID="mpe_Cancel" TargetControlID="mobile_btnReject">
        </ajaxToolkit:ConfirmButtonExtender>
        <ajaxToolkit:ModalPopupExtender ID="mpe_Cancel" runat="server" PopupControlID="pnlPopup_Cancel" TargetControlID="mobile_btnReject" OkControlID = "btnYes_CLR"
            CancelControlID="btnNo_CLR" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup_Cancel" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Confirmation
            </div>
            <div class="body">
                Do you want to Reject Mobile Reimbursement Request ?
            </div>
            <div class="footer" align="right">                                
                <asp:Button ID="btnNo_CLR" runat="server" Text="No" />
                <asp:Button ID="btnYes_CLR" runat="server" Text="Yes" />                
            </div>
        </asp:Panel>
         End Here --%>
    </div>

    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />

    <asp:HiddenField ID="hdnvouno" runat="server" />

    <asp:HiddenField ID="hflEmpDesignation" runat="server" />

    <asp:HiddenField ID="hflEmpDepartment" runat="server" />

    <asp:HiddenField ID="hflEmailAddress" runat="server" />

    <asp:HiddenField ID="hflGrade" runat="server" />

    <asp:HiddenField ID="hflapprcode" runat="server" />

    <asp:HiddenField ID="hdnDesk" runat="server" />

    <asp:HiddenField ID="hdnDestnation" runat="server" />

    <asp:HiddenField ID="hdnRemid" runat="server" />
    <asp:HiddenField ID="hdnRemid_Type" runat="server" />

    <asp:HiddenField ID="hdnClaimid" runat="server" />

    <asp:HiddenField ID="hdnLcalTripid" runat="server" />

    <asp:HiddenField ID="hdnTraveltypeid" runat="server" />

    <asp:HiddenField ID="hdnDeptPlace" runat="server" />

    <asp:HiddenField ID="hdnTravelmode" runat="server" />

    <asp:HiddenField ID="hdnDeviation" runat="server" />

    <asp:HiddenField ID="hdnTrDetRequirements" runat="server" />

    <asp:HiddenField ID="hdnAccReq" runat="server" />

    <asp:HiddenField ID="hdnAccCOS" runat="server" />

    <asp:HiddenField ID="hdnlocaltrReq" runat="server" />

    <asp:HiddenField ID="hdnlocalTrCOS" runat="server" />

    <asp:HiddenField ID="hdnTravelConditionid" runat="server" />

    <asp:HiddenField ID="hdnApprId" runat="server" />

    <asp:HiddenField ID="hdnApprEmailaddress" runat="server" />

    <asp:HiddenField ID="hdnEligible" runat="server" />
    <asp:HiddenField ID="hdnTrdays" runat="server" />
    <asp:HiddenField ID="hdnTravelDtlsId" runat="server" />
    <asp:HiddenField ID="hdnAccId" runat="server" />
    <asp:HiddenField ID="hdnLocalId" runat="server" />
    <asp:HiddenField ID="hdnTravelstatus" runat="server" />
    <asp:HiddenField ID="hdnLeavestatusValue" runat="server" />
    <asp:HiddenField ID="hdnLeavestatusId" runat="server" />
    <asp:HiddenField ID="hdnIsApprover" runat="server" />

    <asp:HiddenField ID="hflempName" runat="server" />
    <asp:HiddenField ID="hdnempcode" runat="server" />

    <asp:HiddenField ID="hdnCurrentApprID" runat="server" />
    <asp:HiddenField ID="hdnReqEmailaddress" runat="server" />
    <asp:HiddenField ID="hdnFuelReimbursementType" runat="server" />
    <asp:HiddenField ID="hdnApprovalTD_Code" runat="server" />

    <asp:HiddenField ID="hdnApproverTDCOS_status" runat="server" />

    <asp:HiddenField ID="hdnisBookthrugh_TD" runat="server" />
    <asp:HiddenField ID="hdnisBookthrugh_COS" runat="server" />
    <asp:HiddenField ID="hdnisApprover_TDCOS" runat="server" />
    <asp:HiddenField ID="hdnNextApprId" runat="server" />

    <asp:HiddenField ID="HiddenField29" runat="server" />
    <asp:HiddenField ID="hdnIntermediateEmail" runat="server" />
    <asp:HiddenField ID="hdnstaus" runat="server" />


    <asp:HiddenField ID="hdnNextApprCode" runat="server" />

    <asp:HiddenField ID="hdnNextApprName" runat="server" />

    <asp:HiddenField ID="hdnNextApprEmail" runat="server" />


    <asp:HiddenField ID="hdnApprovalCOS_mail" runat="server" />
    <asp:HiddenField ID="hdnApprovalCOS_Code" runat="server" />
    <asp:HiddenField ID="hdnApprovalCOS_ID" runat="server" />
    <asp:HiddenField ID="hdnApprovalCOS_Name" runat="server" />

    <asp:HiddenField ID="hdnApprovalACCHOD_mail" runat="server" />
    <asp:HiddenField ID="hdnApprovalACCHOD_Code" runat="server" />
    <asp:HiddenField ID="hdnApprovalACCHOD_ID" runat="server" />
    <asp:HiddenField ID="hdnApprovalACCHOD_Name" runat="server" />
    <asp:HiddenField ID="hdnInboxType" runat="server" />
    <asp:HiddenField ID="hdnloginemployee_name" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="hdnYearlymobileAmount" runat="server" />
    <asp:HiddenField ID="hdnAssgineEMP" runat="server" />
    <asp:HiddenField ID="hdnDepartmentID" runat="server" />
    <asp:HiddenField ID="hdnCategoryId" runat="server" />
    <asp:HiddenField ID="hdnDepartmentName" runat="server" />
    <asp:HiddenField ID="hdnCurrentAssgineEMP" runat="server" />
    <asp:HiddenField ID="hdnServiceRequestNo" runat="server" />
    <asp:HiddenField ID="hdnIsExceletd" runat="server" />
    <asp:HiddenField ID="FilePath" runat="server" />
     <asp:HiddenField ID="HdnStatusIDServicelast" runat="server" />
    <asp:HiddenField ID="HdnStatusActionLog" runat="server" />
     <asp:HiddenField ID="HDnNewtargetDate" runat="server" />

    <asp:HiddenField ID="hdnCompcode" runat="server" />

    <script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <script type="text/javascript">      
        $(document).ready(function () {
            $("#MainContent_ddlDepartment").select2();
            $("#MainContent_ddlCategory").select2();
            $("#MainContent_ddl_AssginmentEMP").select2();
        });
    </script>
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

        function SaveMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=service_btnClose.ClientID%>');

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

        function SaveMultiClick_COSSACC() {
           <%-- try {
                var retunboolean = true;
                var ele = document.getElementById('<%=mobile_btnSave_COSACC.ClientID%>');

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
            return retunboolean;--%>
        }

        function SendforCorrectionMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=service_btnAssgine.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        SendforCorrection_Confirm();
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }

        function SendforCorrection_Confirm() {
            //Testing();
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Assgine Service Request ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return;

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
        //



        ///
        function SendForAssigne() {
            try {
                var msg = "Are you sure you want to assign request?";
                var retunboolean = true;
                var ele = document.getElementById('<%=service_btnAssgine.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        retunboolean = ConfirmToSend(msg);
                    if (retunboolean == false)
                        ele.disabled = false;
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        //

        function SendForClose() {
            try {
                var msg = "Are you sure you want to close request?";
                var retunboolean = true;
                var ele = document.getElementById('<%=service_btnClose.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;

                    if (retunboolean == true)
                        retunboolean = ConfirmToSend(msg);

                    if (retunboolean == false)
                        ele.disabled = false;
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }

        function SendForCEO() {
            try {
                var msg = "Are you sure you want to to submit?";//To Be Change
                var retunboolean = true;
                var ele = document.getElementById('<%=service_btnEscelateToCEO.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        retunboolean = ConfirmToSend(msg);
                    if (retunboolean == false)
                        ele.disabled = false;
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }

        function SendForHOD() {
            try {
                var msg = "Are you sure you want to send to HOD?";//To Be Change
                var retunboolean = true;
                var ele = document.getElementById('<%=service_btnEscelateToHOD.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        retunboolean = ConfirmToSend(msg);
                    if (retunboolean == false)
                        ele.disabled = false;
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }

        function SendForSPOC() {
            try {
                var msg = "Are you sure you want to sent back to SPOC?";//To Be Change
                var retunboolean = true;
                var ele = document.getElementById('<%=service_btnSendSPOC.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        retunboolean = ConfirmToSend(msg);
                    if (retunboolean == false)
                        ele.disabled = false;

                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }

        function SendForReopen() {
            try {
                var msg = "Are you sure you want to ReOpen Service Request?";//To Be Change
                var retunboolean = true;
                var ele = document.getElementById('<%=service_ReOpen.ClientID%>'); 

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        retunboolean = ConfirmToSend(msg);
                    if (retunboolean == false)
                        ele.disabled = false;

                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }

        function SendForReject() {
            try {
                var msg = "Are you sure you want to Reject Service Request?";//To Be Change
                var retunboolean = true;
                var ele = document.getElementById('<%=service_ReOpen.ClientID%>'); 

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        retunboolean = ConfirmToSend(msg); 
                    if (retunboolean == false)
                        ele.disabled = false;

                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }

        function SendForClear() {
            try {
                var msg = "Are you sure you want to clear this text?";//To Be Change
                var retunboolean = true;
                var ele = document.getElementById('<%=service_btnClearText.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        retunboolean = ConfirmToSend(msg);

                    if (retunboolean == false)
                        ele.disabled = false;
                }

            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        // Confirm To All Button
        function ConfirmToSend(msg) {
            //Testing();
            var isConfirm = false;
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm(msg)) {
                confirm_value.value = "Yes";
                isConfirm = true;
            } else {
                confirm_value.value = "No";
                isConfirm = false;
            }
            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return isConfirm;
        }

        function DownloadFile() {
            // alert(file);
            var fileName = document.getElementById("<%=bindFilePath.ClientID%>").innerText;
            //alert(fileName);
            var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;

            //alert(localFilePath);

            window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + fileName);

            // window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + fileName);
        }

        function DownloadFileEmployee() {
            // alert(file);
            var fileName = document.getElementById("<%=lblCreateFile.ClientID%>").innerText;
            //alert(fileName);
            var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;

            //alert(localFilePath);
            // window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + fileName);

            window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + fileName);
            // window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + fileName);
        }
        function CustomerDownloadFile(file) {
		 
			var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;
            	 
			window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
		}
    </script>
</asp:Content>

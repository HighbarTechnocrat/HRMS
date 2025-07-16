<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="travelRequisitionBooking_Form.aspx.cs"
    Inherits="procs_travelRequisitionBooking_Form" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <%-- <link href="<%=ReturnUrl("css") %>calendar/calendar.css" rel="stylesheet" type="text/css" />
    <link href="sampleform_css.css" rel="stylesheet" type="text/css" />--%><a href="travelRequisitionBooking_Form.aspx">travelRequisitionBooking_Form.aspx</a>
    <%--<ajaxToolkit:ToolkitScriptManager ID="srcp" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <style>
        .myaccountpagesheading {
            text-align: center;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
            /*background: #dae1ed;*/
            background: #ebebe4;
        }

        .taskparentclasskkk {
            width: 29.5%;
            height: 72px;
            /*overflow:initial;*/
        }

        #MainContent_lstApprover, #MainContent_lstIntermediate {
            padding: 0 0 33px 0 !important;
            /*overflow: unset;*/
        }

        .Dropdown {
            border-bottom: 2px solid #cccccc;
            /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
            background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;
            cursor: default;
        }

        .textboxBackColor {
            background-color: cadetblue;
            color: aliceblue;
            text-align: right;
        }

        .textboxAlignAmount {
            text-align: right;
        }


        .BtnShow {
            color: blue !important;
            background-color: transparent;
            text-decoration: none;
            font-size: 13px !important;
        }

            .BtnShow:visited {
                color: blue !important;
                background-color: transparent;
                text-decoration: none;
            }

            .BtnShow:hover {
                color: red !important;
                background-color: transparent;
                text-decoration: none !important;
            }

        .POWOContentTextArea {
            width: 783px !important;
            height: 400px !important;
            overflow: auto;
        }

        .LableName {
            color: #F28820;
            font-size: 16px;
            font-weight: normal;
            text-align: left;
        }

        .boxTest {
            BORDER-RIGHT: black 1px solid;
            BORDER-TOP: black 1px solid;
            BORDER-LEFT: black 1px solid;
            BORDER-BOTTOM: black 1px solid;
            BACKGROUND-COLOR: White;
        }

        .GoalDecriptionTextArea {
            width: 722px !important;
            height: 250px !important;
        }

        .txtRemarks {
            width: 623px;
            height: 77px;
        }

        .BtnShow {
            color: blue !important;
            background-color: transparent;
            text-decoration: none;
            font-size: 13px !important;
        }

            .BtnShow:visited {
                color: blue !important;
                background-color: transparent;
                text-decoration: none;
            }

            .BtnShow:hover {
                color: red !important;
                background-color: transparent;
                text-decoration: none !important;
            }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="commonpages">
        <div class="wishlistpagediv">
            <div class="userposts">
                <span>
                    <br />
                    <asp:Label ID="Label2" runat="server" Text="Travel Requisition Booking Form"></asp:Label>
                </span>


            </div>
            <span>
                <a href="TravelRequisition_Index.aspx" class="aaaa">Travel Requisition Index</a>
            </span>

            <div>
                <asp:Label runat="server" ID="Label3" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
            </div>
            <div class="edit-contact">

                <div class="editprofile btndiv" id="div1" runat="server" visible="false">
                    <div class="cancelbtndiv">
                        <asp:LinkButton ID="LinkButton1" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                    </div>
                    <div class="cancelbtndiv">
                        <asp:LinkButton ID="LinkButton2" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" Visible="false"></asp:LinkButton>
                    </div>
                </div>
            </div>

        </div>
    </div>




    <div class="commonpages">
        <div class="wishlistpagediv">
            <div class="userposts">
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

                <ul id="editform" runat="server" visible="true">
                    <li class="trvl_type">
                        <span>Employee Code</span>&nbsp;&nbsp;<br />
                        <asp:TextBox AutoComplete="off" ID="txtEmpcode" runat="server" ReadOnly="true" MaxLength="150" Enabled="False"></asp:TextBox>
                    </li>
                    <li class="trvl_type">
                        <span>Employee Name</span>&nbsp;&nbsp;<br />
                        <asp:TextBox AutoComplete="off" ID="txtEmpName" runat="server" MaxLength="15" ReadOnly="true" Enabled="False"></asp:TextBox>
                        <asp:TextBox AutoComplete="off" ID="txtEmailAddress" runat="server" Visible="false" MaxLength="100"></asp:TextBox> 
                    </li>
                    <li class="trvl_type">
                        <span>Department </span>
                        <br />
                        <asp:TextBox AutoComplete="off" ID="txtDepartment" runat="server" MaxLength="45" ReadOnly="true" Enabled="False"></asp:TextBox>
                    </li>


                    <li class="trvl_type">
                        <span>Designation</span>&nbsp;&nbsp;<br />
                        <asp:TextBox AutoComplete="off" ID="txtDesignation" runat="server" MaxLength="45" ReadOnly="true" Enabled="False"></asp:TextBox>
                    </li>
                    <li class="trvl_type">
                        <span>Band</span>&nbsp;&nbsp;<br />
                        <asp:TextBox AutoComplete="off" ID="band" runat="server" MaxLength="45" ReadOnly="true" Enabled="False"></asp:TextBox>
                    </li>
                    <li class="trvl_type">
                        <span>Name As Per Aadhar</span>&nbsp;&nbsp;<br />
                        <asp:TextBox AutoComplete="off" ID="txtEmpName_Aadhar" runat="server" Enabled="false"></asp:TextBox>
                    </li>


                    <li class="trvl_type">
                        <span>Mobile No.</span>&nbsp;&nbsp;<br />
                        <asp:TextBox AutoComplete="off" ID="txtMobile" runat="server" MaxLength="15" ReadOnly="true" Enabled="false"></asp:TextBox>
                    </li>
                    <li class="trvl_type">
                        <span>From Date</span>
                        <asp:TextBox AutoComplete="off" ID="txtFromDate" runat="server" AutoPostBack="true" OnTextChanged="txtFromDate_TextChanged" MaxLength="15" AutoCompleteType="Disabled"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd-MM-yyyy" TargetControlID="txtFromDate"
                            runat="server">
                        </ajaxToolkit:CalendarExtender>
                    </li>
                    <li class="trvl_type">
                        <span>Return Date (If Return Date Confirmed)</span>
                        <asp:TextBox AutoComplete="off" ID="txttodate" runat="server" MaxLength="100" AutoPostBack="true" OnTextChanged="txttodate_TextChanged"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd-MM-yyyy" TargetControlID="txttodate"
                            runat="server">
                        </ajaxToolkit:CalendarExtender>
                    </li>


                    <li class="trvl_type">
                        <span>Travel Project</span>
                        <asp:TextBox AutoComplete="off" ID="txt_travel_project" runat="server" Enabled="false"></asp:TextBox>
                    </li>
                    <li class="trvl_type">
                        <span id="spn_prm" runat="server">Program Manger</span>
                        <asp:TextBox AutoComplete="off" ID="txtprogrammanger_Name" runat="server" Enabled="false"></asp:TextBox>
                        <asp:TextBox AutoComplete="off" ID="txtprogrammanger_Email" runat="server" Enabled="false" Visible="false"></asp:TextBox>
                    </li>
                    <li class="trvl_type">
                        <span id="spn_pm" runat="server">Project Manger</span>
                        <asp:TextBox AutoComplete="off" ID="txtprojectmanger_Name" runat="server" Enabled="false"></asp:TextBox>
                        <asp:TextBox AutoComplete="off" ID="txtprojectmanger_Email" runat="server" Enabled="false" Visible="false"></asp:TextBox>
                    </li>


                    <li class="trvl_type">
                        <span>From Location</span>
                        <asp:DropDownList ID="lstFromLocation" runat="server" CssClass="DropdownListSearch" Style="width: 251px !important"></asp:DropDownList>
                        <asp:TextBox AutoComplete="off" ID="txtFromLocation" runat="server" MaxLength="45" Visible="false"></asp:TextBox>
                    </li>
                    <li class="trvl_type">
                        <span>To Location</span>
                        <asp:DropDownList ID="lstToLocation" runat="server" CssClass="DropdownListSearch" Style="width: 251px !important"></asp:DropDownList>
                    </li>
                    <li class="trvl_type">
                        <span>Base Location</span>
                        <asp:TextBox AutoComplete="off" ID="txtbaselocation" runat="server" MaxLength="100"></asp:TextBox> 
                    </li>


                    <li class="trvl_type">
                        <span>Prefer Time (Booking)</span>
                        <asp:DropDownList ID="lstPreferTime" runat="server" CssClass="DropdownListSearch" Style="width: 251px !important">
                        </asp:DropDownList>
                    </li>
                    <li class="trvl_type">
                        <span>Travel Mode By Employee</span>
                        <asp:DropDownList ID="lstTravelMode" runat="server" CssClass="DropdownListSearch" Style="width: 251px !important"></asp:DropDownList>
                    </li>
                    <li class="trvl_type">
                        <span>Accomodation </span>
                        <div class="form-check-inline">
                            <asp:RadioButton runat="server" class="form-check-input" ID="rdoYes" Text="Yes" AutoPostBack="true" OnCheckedChanged="rdoYes_CheckedChanged" GroupName="optradio" />
                            &nbsp;&nbsp; &nbsp;&nbsp;
                              <asp:RadioButton runat="server" class="form-check-input" ID="rdoNo" Checked="true" Text="No" AutoPostBack="true" OnCheckedChanged="rdoYes_CheckedChanged" GroupName="optradio" />
                        </div>
                    </li>

                    <li class="trvl_type">
                        <div id="divAcc" runat="server" visible="false">
                            <span>Accomodation Location</span>
                            <asp:TextBox AutoComplete="off" ID="txtaccomodationl" runat="server" MaxLength="100"></asp:TextBox>
                        </div>
                    </li>
                    <li class="trvl_type">
                        <div id="div2" runat="server" visible="false">
                            <span>Accomodation From Date</span>
                            <asp:TextBox AutoComplete="off" ID="txtaccfromdate" runat="server" AutoPostBack="true" OnTextChanged="txtAccomodationFromDate_TextChanged" MaxLength="15" AutoCompleteType="Disabled"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" Format="dd-MM-yyyy" TargetControlID="txtaccfromdate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </div>
                    </li>
                    <li class="trvl_type">
                        <div id="div3" runat="server" visible="false">
                            <span>Accomodation To Date</span>
                            <asp:TextBox AutoComplete="off" ID="txtacctodate" runat="server" MaxLength="100" AutoPostBack="true" OnTextChanged="txtAccomodationtodate_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" Format="dd-MM-yyyy" TargetControlID="txtacctodate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </div>
                    </li>


                    <li class="trvl_type">
                        <span>Remarks By Employee</span>
                        <asp:TextBox AutoComplete="off" ID="txtremarks" runat="server" Height="40px"></asp:TextBox>
                    </li>
                    <li>
                        <span>Travel Status</span> &nbsp;&nbsp;<span style="color: red">*</span>
                        <asp:DropDownList ID="lstTravelStatus" runat="server" CssClass="DropdownListSearch" Style="width: 251px !important" OnSelectedIndexChanged="lstTravelStatus_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </li>
                    <li class="trvl_type" id="li6" runat="server">
                        <span>Travel Mode</span>&nbsp;&nbsp;<span style="color: red">*</span>
                        <asp:DropDownList ID="lsttravelbook" runat="server" CssClass="DropdownListSearch" Style="width: 251px !important" Enabled="false"></asp:DropDownList>
                    </li>



                    <li class="trvl_type">
                        <span>Travel Booked Amount  </span>&nbsp;&nbsp;<br />
                        <asp:TextBox AutoComplete="off" ID="txtbookamt" runat="server" MaxLength="500" Enabled="false"></asp:TextBox>
                       
                    </li>
                    <li class="trvl_type">
                        <div id="divAccBookingAmt_1" runat="server" visible="false">
                        <span>Accomodation Booked Amount </span>&nbsp;&nbsp;<br />
                        <asp:TextBox AutoComplete="off" ID="txtaccamt" runat="server" MaxLength="500" Enabled="false"></asp:TextBox>
                         </div>
                    </li>
                    <li class="trvl_type">
                        <div id="divAccBookingAmt_2" runat="server" visible="false">
                         <span>Confirm Accomodation Location </span>&nbsp;&nbsp;<br />
                        <asp:TextBox AutoComplete="off" ID="txtconfirmAccomodation" runat="server" MaxLength="500"></asp:TextBox>
                        </div>
                    </li>


                    <li class="trvl_type" id="li_Admin_Cancel_Amt_1" runat="server" visible="false">
                        <span>Cancle Booked Amount </span>&nbsp;&nbsp;<br />
                        <asp:TextBox AutoComplete="off" ID="txtcancleamt" runat="server" MaxLength="500" Enabled="false"></asp:TextBox>
                    </li>
                    <li id="li_Admin_Cancel_Amt_2" runat="server" visible="false"></li>
                    <li id="li_Admin_Cancel_Amt_3" runat="server" visible="false"></li>
                        
                    
                    <li class="trvl_type">
                        <span id="uplodedticket" runat="server">Uploded Travel Ticket</span>&nbsp;&nbsp;
                        <span id="travelticket" runat="server" style="color: red">*</span>
                        <asp:FileUpload ID="uploadtravelticket" runat="server" accept="pdf/*" AllowMultiple="true" Enabled="false"></asp:FileUpload>
                       <br />
                         <asp:GridView ID="dg_bookedTickets" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                            DataKeyNames="t_id">
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
                                <asp:TemplateField HeaderText="Uploded Travel Ticket">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkviewfile" CssClass="BtnShow" runat="server" OnClientClick=<%# "DownloadFile('" + Eval("file_name") + "')" %> Text='<%# Eval("file_name") %>'>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                     
                        <asp:LinkButton ID="lnkfile_Vendor" runat="server" OnClientClick="DownloadFile_S()" Visible="false" CssClass="BtnShow" OnSelectedIndexChanged="lstTravelStatus_SelectedIndexChanged" AutoPostBack="true"></asp:LinkButton>  
                       <br /> 
                    </li>          
                     <li class="trvl_type"></li>
                    <li class="trvl_type"></li>


                    <li class="trvl_type">
                        <span id="spnsportingfiles" runat="server">Upload Supporting Files</span>&nbsp;&nbsp;<span id="spnsportingfiles_1" runat="server" style="color: red">*</span>
                        <asp:FileUpload ID="uplodmultiple" runat="server" AllowMultiple="true" Enabled="false"></asp:FileUpload>
                        <br /> 
                        <asp:GridView ID="gvuploadedFiles" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                            DataKeyNames="t_id">
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
                                        <asp:LinkButton ID="lnkviewfile" CssClass="BtnShow" runat="server" OnClientClick=<%# "DownloadFile('" + Eval("file_name") + "')" %> Text='<%# Eval("file_name") %>'>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </li>
                    <li class="trvl_type"></li>
                    <li class="trvl_type"></li>
                     

                    <li class="trvl_type">
                        <br />
                        <span>Remarks By Admin</span>&nbsp;&nbsp;<span style="color: red">*</span>
                        <asp:TextBox AutoComplete="off" ID="txtremarksby" runat="server" Height="40px" TextMode="MultiLine"></asp:TextBox>
                    </li> 
                    <li class="trvl_type">
                        <div id="divcancleremarks" runat="server" visible="false">
                            <br />
                            <span>Remarks For Cancellation </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <asp:TextBox AutoComplete="off" ID="txtremarkscancel" runat="server" Height="40px" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </li>    
                    <li class="trvl_type"></li>


                    <li class="trvl_type">
                        <asp:LinkButton ID="lnk_downloadAadhar" runat="server" OnClientClick="DownloadFile_Aadhar()" CssClass="BtnShow">Download Aadhar Card</asp:LinkButton>
                    </li> 
                    <li class="trvl_type"></li>
                    <li class="trvl_type"></li>
               
                    <li class="trvl_type">
                        <div id="divcreditnote" runat="server" visible="false">
                            <span id="creditnote" runat="server" >Upload Credit Notes</span>&nbsp;&nbsp;<span id="Span1" runat="server" style="color: red">*</span>
                                <asp:FileUpload ID="uploadCreditNotes" runat="server" AllowMultiple="true"  ></asp:FileUpload>
                        </div>
                            <asp:GridView ID="GridView1" runat="server" Visible="false" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                            DataKeyNames="t_id">
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
                                        <asp:LinkButton ID="lnkviewfile" CssClass="BtnShow" runat="server" OnClientClick=<%# "DownloadFile('" + Eval("file_name") + "')" %> Text='<%# Eval("file_name") %>'>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        
                       <%--</div> --%>
                    </li>
                    <li class="trvl_type"></li>
                    <li class="trvl_type"></li>
                </ul>
            </div>

        </div>

    </div>

    <div class="trvl_Savebtndiv">
        <asp:LinkButton ID="trvl_btnSave" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();" OnClick="trvl_btnSave_Click">Submit</asp:LinkButton>
        <asp:LinkButton ID="btnCorrection" Visible="false" runat="server" Text="Cancel Travel Request" ToolTip="Cancel Travel Request" OnClientClick="return CancelMultiClick();" CssClass="Savebtnsve" OnClick="btnCorrection_Click">Cancel Travel Request</asp:LinkButton>
        <asp:LinkButton ID="btnback_mng" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="btnback_mng_Click">Back</asp:LinkButton>

        <%--<asp:LinkButton ID="btnCorrection" Visible="false" runat="server" Text="Download PO/ WO" ToolTip="Download PO/ WO" CssClass="Savebtnsve" OnClick="btnCorrection_Click">Download PO/ WO</asp:LinkButton>
         <asp:LinkButton ID="btnApprove"  runat="server" Text="View Draft Copy" ToolTip="View Draft Copy" Visible="false" CssClass="Savebtnsve" OnClick="btnApprove_Click" >View Draft Copy</asp:LinkButton>
        <asp:LinkButton ID="btnReject"  runat="server" Visible="false" Text="View Draft Copy" ToolTip="View Draft Copy"  CssClass="Savebtnsve"  OnClick="btnReject_Click" >View Draft Copy</asp:LinkButton>
        --%>
    </div>


    <asp:HiddenField ID="hdnCondintionId" runat="server" />
    <asp:HiddenField ID="hdnTravel_id" runat="server" />
    <asp:HiddenField ID="HDVendorCode" runat="server" />
    <asp:HiddenField ID="hdnShortClose_Cancelled" runat="server" />
    <asp:HiddenField ID="hdnIsShMilestoneClick" runat="server" />

    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="hdnEmployee_Email" runat="server" />
    <asp:HiddenField ID="FilePath" runat="server" />
    <asp:HiddenField ID="hdnAadharCardPath" runat="server" />
    <asp:HiddenField ID="hdnAadharCardFileName" runat="server" />
    <asp:HiddenField ID="hdnAdmin_EmailsId" runat="server" />
    <asp:HiddenField ID="hdnAdmin_HODEmailId" runat="server" />
    <asp:HiddenField ID="hdnLoginEmployee_Code" runat="server" />
    <asp:HiddenField ID="hdnIsAdmin_Employee" runat="server" />

    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <script type="text/javascript">

        $(document).ready(function () {
            $(".DropdownListSearch").select2();
            $("#MainContent_txtPOWO_Content").htmlarea();
            $("#MainContent_txt_POWOContent_description").htmlarea();
        });


        function SaveMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=trvl_btnSave.ClientID%>');

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

        function CancelMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=btnCorrection.ClientID%>');

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

        function checkEmail() {

            var email = document.getElementById('<%=txtEmailAddress.ClientID%>');
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (email.value != '') {
                if (!filter.test(email.value)) {
                    alert('Please provide a valid email address');
                    email.focus;
                    return false;
                }
            }

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
            //alert(confirm_value.value);
            //document.forms[0].appendChild(confirm_value);
            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return;

        }

        function DownloadFile(file) {
			// alert(file);
			var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;

			//alert(localFilePath);
           // window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
             window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);

        }


        function DownloadFile_S() {

            var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;
             var localFileName = document.getElementById("<%=lnkfile_Vendor.ClientID%>").innerText; 
             
           //  window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + localFileName); 
            window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + localFileName);
        }

        function DownloadFile_Aadhar() {

            var localFilePath = document.getElementById("<%=hdnAadharCardPath.ClientID%>").value;
            var localFileName = document.getElementById("<%=hdnAadharCardFileName.ClientID%>").value;

           // window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + localFileName); 
           window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + localFileName);
        }



        function onCharOnlyNumber(e) {
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


    </script>
</asp:Content>





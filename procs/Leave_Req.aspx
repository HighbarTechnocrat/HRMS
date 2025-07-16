<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="Leave_Req.aspx.cs" Inherits="Leave_Req" EnableEventValidation="false" %>


<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>
<%@ Register TagName="calender" TagPrefix="ucical" Src="~/themes/creative1.0/LayoutControls/leaveCalender.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/ModifyLeaveRequest_css.css" type="text/css" media="all" />
    <%-- <link href="<%=ReturnUrl("css") %>calendar/calendar.css" rel="stylesheet" type="text/css" />
    <link href="sampleform_css.css" rel="stylesheet" type="text/css" />--%>
    <%--<ajaxToolkit:ToolkitScriptManager ID="srcp" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    
    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }


        .Dropdown {
            border-bottom: 2px solid #cccccc;
            /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
            background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;
            cursor: default;
        }

         .grayDropdown {
            border-bottom: 2px solid #cccccc;
            /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
            background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;            
            background-color:#ebebe4;
        }

         .grayDropdownTxt
         {            
            background-color:#ebebe4;
            }

        .taskparentclass2 {
            width: 29.5%;
            height: 112px;
        }

        .taskparentclasskkk {
            width: 29.5%;
            height: 72px;
            /*overflow:initial;*/
        }

        #MainContent_ListBox1, #MainContent_ListBox2 {
            padding: 0 0 0% 0 !important;
            /*overflow: unset;*/
        }

        #MainContent_lstApprover, #MainContent_lstIntermediate {
            padding: 0 0 33px 0 !important;
            /*overflow: unset;*/
        }


     

    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    &nbsp;<script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script><script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script><script type="text/javascript">
        var deprt;
        $(document).ready(function () {
            $("#MainContent_txtcity").autocomplete("<%=ReturnUrl("sitepathmain") %>Search_City.ashx", {
                inputClass: "ac_input",
                resultsClass: "ac_results",
                loadingClass: "ac_loading",
                minChars: 1,
                delay: 1,
                matchCase: false,
                matchSubset: false,
                matchContains: false,
                cacheLength: 0,
                max: 12,
                mustMatch: false,
                extraParams: {},
                selectFirst: false,
                formatItem: function (row) { return row[0] },
                formatMatch: null,
                autoFill: false,
                width: 0,
                multiple: false,
                scroll: false,
                scrollHeight: 180,
                success: function (data) {
                    response($.map(data, function (item) {
                        return {

                        }
                    }));
                },
                context: this
            });

            $("#MainContent_txtloc").autocomplete("<%=ReturnUrl("sitepathmain") %>Search_location.ashx", {
                inputClass: "ac_input",
                resultsClass: "ac_results",
                loadingClass: "ac_loading",
                minChars: 1,
                delay: 1,
                matchCase: false,
                matchSubset: false,
                matchContains: false,
                cacheLength: 0,
                max: 12,
                mustMatch: false,
                extraParams: {},
                selectFirst: false,
                formatItem: function (row) { return row[0] },
                formatMatch: null,
                autoFill: false,
                width: 0,
                multiple: false,
                scroll: false,
                scrollHeight: 180,
                success: function (data) {
                    response($.map(data, function (item) {
                        return {

                        }
                    }));
                },
                context: this
            });


            $("#MainContent_txtsubdept").autocomplete("<%=ReturnUrl("sitepathmain") %>Search_subdepartment.ashx?d=" + deprt, {
                inputClass: "ac_input",
                resultsClass: "ac_results",
                loadingClass: "ac_loading",
                minChars: 1,
                delay: 1,
                matchCase: false,
                matchSubset: false,
                matchContains: false,
                cacheLength: 0,
                max: 12,
                mustMatch: false,
                extraParams: { d: deprt },
                selectFirst: false,
                formatItem: function (row) { return row[0] },
                formatMatch: null,
                autoFill: false,
                width: 0,
                multiple: false,
                scroll: false,
                scrollHeight: 180,
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                        }
                    }));
                },

                context: this
            });
        });
    </script>

<div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Leave Application"></asp:Label>
                    </span>

                    <%--PostBackUrl="~/procs/Index.aspx"--%>
                </div>

                <div class="leavegrid">
                    <h3 id="hheadyear" runat="server">Leave Card - 2020</h3>
                    <a href="Leaves.aspx" class="aaa">Leave Menu</a>
                    <asp:GridView ID="dgLeaveBalance" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="70%">
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
                            <asp:BoundField HeaderText="Leave Type"
                                DataField="Leave Type"
                                ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="left"
                                    ItemStyle-Width="22%" 
                                    ItemStyle-BorderColor="Navy"
                                    />

                            <asp:BoundField HeaderText="Opening"
                                DataField="Opening Balance"
                                ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="10%" 
                                ItemStyle-BorderColor="Navy"
                                />

                            <asp:BoundField HeaderText="Earned"
                                DataField="Earned"
                                ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="10%" 
                                ItemStyle-BorderColor="Navy"
                                />

                            <asp:BoundField HeaderText="Availed"
                                DataField="Availed"
                                ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="10%" 
                                ItemStyle-BorderColor="Navy"
                                />

                            <asp:BoundField HeaderText="Pending"
                                DataField="Pending"
                                ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="10%" 
                                ItemStyle-BorderColor="Navy"
                                />

                            <asp:BoundField HeaderText="Future Leaves"
                                DataField="Future"
                                ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="10%" 
                                ItemStyle-BorderColor="Navy"
                                />

                            <asp:BoundField HeaderText="Balance"
                                DataField="Balance"
                                ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="10%" 
                                ItemStyle-BorderColor="Navy"
                                />

                             <asp:BoundField HeaderText="Accrued Leave Balance"
                                DataField="Accrued_Leaves"
                                ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="10%" 
                                ItemStyle-BorderColor="Navy"
                                />

                        </Columns>
                    </asp:GridView>

                </div>


                <div class="edit-contact">
                    <%-- <div class="editprofile" style="text-align: center; border: none;" id="divmsg" runat="server" visible="false">
                        <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: green; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                    </div>--%>
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
<%--                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" OnClick="lnkcont_Click"></asp:LinkButton>
                        </div>--%>
                    </div>

                    <div class="editprofile" id="editform1" runat="server" visible="true">
                            <div class="editprofileform">
                                <ucical:calender ID="icalender" runat="server"></ucical:calender>
                            </div>
                    </div>

                    <ul id="editform" runat="server" visible="false">

                        <li>
                            <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                            <%--<asp:TextBox runat="server" ID="lblmessage1" Visible="False" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;" OnTextChanged="lblmessage1_TextChanged"></asp:TextBox>--%>
                        </li>


                        <li></li>

                        <li>
                            <span>Leave Type</span>&nbsp;&nbsp;<span style="color:red">*</span>
                            <br />
                            <asp:DropDownList runat="server" ID="lstLeaveType" OnSelectedIndexChanged="lstLeaveType_SelectedIndexChanged" AutoPostBack="true">
                                
                            </asp:DropDownList>
                            
                            <%-- <i class="fa fa-caret-down" aria-hidden="true"></i>--%>
                         </li>
                        <li>
                            <span id="spn_OL" runat="server" visible="false">Select Optional Leave Date</span> 
                            <br />
                            <asp:DropDownList Visible="false" runat="server" ID="lstOptionalLeaveDates" AutoPostBack="true" OnSelectedIndexChanged="lstOptionalLeaveDates_SelectedIndexChanged">
                                
                            </asp:DropDownList>
                            
                        </li>

                        <li class="date">
                            <br />
                            <span>From Date</span>&nbsp;&nbsp;<span style="color:red">*</span>                
                                <asp:TextBox ID="txtFromdate"  AutoComplete="off" runat="server" AutoPostBack="true" OnTextChanged="txtFromdate_TextChanged" MaxLength="10" AutoCompleteType="Disabled"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtFromdate"
                                      runat="server">
                                </ajaxToolkit:CalendarExtender>

                                <%--  <asp:TextBox ID="txtsadate" runat="server" CssClass="txtdatepicker" placeholder="Start Date" OnTextChanged ="txtsadate_TextChanged" AutoPostBack ="true" ></asp:TextBox>
                                <span class="texticon tooltip" title="Select Start Date. Date should be MM/DD/YYYY format."><i class="fa fa-calendar" aria-hidden="true"></i></span>
                                <asp:Label ID="lblsdate" runat="server" Text="" Font-Size="15px"></asp:Label>--%>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="formerror" ControlToValidate="txtsadate" SetFocusOnError="True" ErrorMessage="Please enter start date" ValidationGroup="validate"></asp:RequiredFieldValidator>--%>

                        </li>

                        <li class="taskparentclass">
                            <span>For</span><br />
                                        
                            <div class="form-check-inline">
                                <asp:RadioButton runat="server" class="form-check-input" ID="Fullday" Text="Full Day" AutoPostBack="true" OnCheckedChanged="Fullday_CheckedChanged" GroupName="optradio" />
                                <asp:RadioButton runat="server" class="form-check-input" ID="FirstHalf" Text="First Half" AutoPostBack="true" OnCheckedChanged="Fullday_CheckedChanged"  GroupName="optradio" />
                                <asp:RadioButton runat="server" class="form-check-input" ID="SecondHalf" Text="Second Half" AutoPostBack="true" OnCheckedChanged="Fullday_CheckedChanged"  GroupName="optradio" />
                            </div>
                            <asp:TextBox Visible="false" ID="txtFromfor" runat="server" AutoPostBack="true" OnTextChanged="txtFromfor_TextChanged" CssClass="Dropdown"></asp:TextBox>
                        </li>

                        <li class="date">
                            <span>To Date</span>&nbsp;&nbsp;<span style="color:red">*</span><br />
                            <asp:TextBox ID="txtToDate" AutoComplete="off" runat="server" OnTextChanged="txtToDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>

                        <li class="fordate">
                            <span>For</span><br />
                            <div class="form-check-inline">
                                <asp:RadioButton runat="server" class="form-check-input" ID="Radio1" Text="Full Day" AutoPostBack="true" OnCheckedChanged="Fullday_CheckedChanged" GroupName="Toradio" />
                                <asp:RadioButton runat="server" class="form-check-input" ID="Radio2" Text="First Half" AutoPostBack="true" OnCheckedChanged="Fullday_CheckedChanged" GroupName="Toradio" />
                            </div>
                            <asp:TextBox Visible="false" ID="txtToFor" runat="server" AutoComplete="off" CssClass="Dropdown" AutoPostBack="true" OnTextChanged="txtToFor_TextChanged"></asp:TextBox>
                        </li>

                        <li class="leavedays">
                            <span>Leave Days</span><br />
                            <asp:TextBox ID="txtLeaveDays" Enabled="false" runat="server"></asp:TextBox>
                        </li>

                        <%--<li ></li>--%>
                        <%--                        <li> 
                            <span></span>
                            <br />
                            <br />
                        </li>--%>

                        <li class="Reason">
                            <span>Remarks</span>&nbsp;&nbsp;<span style="color:red">*</span>
                            <br />
                            <asp:TextBox ID="txtReason" AutoComplete="off" runat="server" MaxLength="30" ></asp:TextBox>
                        </li>

                        <li class="upload">
                            <span>Upload File</span><br />
                            <%--<asp:FileUpload ID="uploadfile"  runat="server" EnableViewState="true"/>--%>
                            <asp:TextBox ID="txtprofile" runat="server" CssClass="mobilenotextbox popup" Visible="false"> </asp:TextBox>
                            <%--<span class="texticonselect tooltip" title="Please provide your profile picture."><i class="fa fa-file-image-o"></i></span>--%>

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                            <asp:FileUpload ID="uploadfile" runat="server"></asp:FileUpload>
                            <asp:Button ID="btnUpload" runat="server" Text="Upload Image"  OnClick="Upload"   Style="display: none"/>
                            </ContentTemplate>
                            <Triggers>
                             <asp:PostBackTrigger ControlID="btnUpload" />
                            </Triggers>
                            </asp:UpdatePanel>

                               <asp:LinkButton ID="lnkfile_SL"   runat="server"  OnClick="lnkfile_SL_Click" Visible="false"></asp:LinkButton>
                        </li>

                        <li class="Reason">                                                         
                            <span ></span>
                            <br />
                            <asp:LinkButton ID="lnkfile"   runat="server"  OnClick="lnkfile_Click" Visible="false"></asp:LinkButton>
                            <%--<asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>                        

                        </li>
                       
                        <li>                                                         
                            <span id="idspnreasnforCancellation" runat="server">Reason for Leave Cancellation </span>
                            <br />
                            <asp:TextBox ID="txtLeaveCancelReason" AutoComplete="off" runat="server" MaxLength="30"></asp:TextBox>
                        </li>

                        <li class="Reason">                                                         
                            <span > </span>
                            <br />
                        </li>
                        <li class="Approver">
                            <%--<span>Approver </span>--%>
                            <br />
                            <asp:ListBox Visible="false" ID="lstApprover" runat="server"></asp:ListBox>
                    <asp:GridView ID="DgvApprover" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%">
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
                                DataField="tName"
                                ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="left"
                                ItemStyle-Width="25%" 
                                ItemStyle-BorderColor="Navy"
                                />

                            <asp:BoundField HeaderText="Status"
                                DataField="Status"
                                ItemStyle-HorizontalAlign="left"
                                ItemStyle-Width="12%" 
                                ItemStyle-BorderColor="Navy"
                                />

                            <asp:BoundField HeaderText="Approved on"
                                DataField="tdate"
                                ItemStyle-HorizontalAlign="left"
                                ItemStyle-Width="12%" 
                                ItemStyle-BorderColor="Navy"
                                />

                            <asp:BoundField HeaderText="Approver Remarks"
                                DataField="Comment"
                                ItemStyle-HorizontalAlign="left"
                                ItemStyle-Width="46%" 
                                ItemStyle-BorderColor="Navy"
                                />
                            
                            <asp:BoundField HeaderText="APPR_ID"
                                DataField="APPR_ID"
                                ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="2%" 
                                ItemStyle-BorderColor="Navy"
                                Visible="false"
                                />
                            
                            <asp:BoundField HeaderText="Emp_Name"
                                DataField="Emp_Name"
                                ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="1%" 
                                ItemStyle-BorderColor="Navy"
                                Visible="false"
                                />

                            <asp:BoundField HeaderText="Emp_Emailaddress"
                                DataField="Emp_Emailaddress"
                                ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="1%" 
                                ItemStyle-BorderColor="Navy"
                                Visible="false"
                                />

                            <asp:BoundField HeaderText="A_EMP_CODE"
                                DataField="A_EMP_CODE"
                                ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="1%" 
                                ItemStyle-BorderColor="Navy"
                                Visible="false"
                                />
                        </Columns>
                    </asp:GridView>
                        </li>
<%--                       <li>

                       </li>
                        <li class="Approver">
                            <span>For Information To </span>
                            <br />
                            <asp:ListBox ID="lstIntermediate" runat="server"></asp:ListBox>
                        </li>--%>

                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="Savebtndiv"   style="padding-top:20px">
        <asp:LinkButton ID="btnSave"  runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClick="btnSave_Click1" OnClientClick="return SaveMultiClick();">Submit</asp:LinkButton>
        <asp:LinkButton ID="btnBack" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="btnBack_Click">Back</asp:LinkButton>
          
        <%-- Following Popup for Sending Leave Request 
        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" DisplayModalPopupID="mpe" TargetControlID="btnSave">
        </ajaxToolkit:ConfirmButtonExtender>
        <ajaxToolkit:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="btnSave" OkControlID = "btnYes"
            CancelControlID="btnNo" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Confirmation
            </div>
            <div class="body">              
                Do you want to Submit ?
            </div>
            <div class="footer" align="right">
                <asp:Button ID="btnNo" runat="server" Text="No" />
                <asp:Button ID="btnYes" runat="server" Text="Yes" />
            </div>
        </asp:Panel>
        End Here --%>
    </div>
    <div>
        <asp:LinkButton ID="btnMod" runat="server"  Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClick="btnMod_Click" OnClientClick="return SaveMultiClick_Modify();">Modify</asp:LinkButton>
        <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel"  CssClass="Savebtnsve" OnClick="btnCancel_Click" OnClientClick="return CancelMultiClick();" >Cancel</asp:LinkButton>
        <asp:LinkButton ID="btnback_mng" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/MyLeave_Req.aspx">Back</asp:LinkButton>
                
        <%-- Following Popup for Modify Leave Request 
        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" DisplayModalPopupID="mpe_Mod" TargetControlID="btnMod">
        </ajaxToolkit:ConfirmButtonExtender>
        <ajaxToolkit:ModalPopupExtender ID="mpe_Mod" runat="server" PopupControlID="pnlPopup_Mod" TargetControlID="btnMod" OkControlID = "btnYes_Mod"
            CancelControlID="btnNo_Mod" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup_Mod" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Confirmation
            </div>
            <div class="body">                                
                Do you want to Update ?
            </div>
            <div class="footer" align="right">
                <asp:Button ID="btnNo_Mod" runat="server" Text="No" />
                <asp:Button ID="btnYes_Mod" runat="server" Text="Yes" />                
            </div>
        </asp:Panel>
        End Here --%>

        <%-- Following Popup for Cancel Leave Request  
        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" DisplayModalPopupID="mpe_Cancel" TargetControlID="btnCancel">
        </ajaxToolkit:ConfirmButtonExtender>
        <ajaxToolkit:ModalPopupExtender ID="mpe_Cancel" runat="server" PopupControlID="pnlPopup_Cancel" TargetControlID="btnCancel" OkControlID = "btnYes_CLR"
            CancelControlID="btnNo_CLR" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup_Cancel" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Confirmation
            </div>
            <div class="body">
               
                Do you want to Cancel ?
            </div>
            <div class="footer" align="right">                                
                <asp:Button ID="btnNo_CLR" runat="server" Text="No" />
                <asp:Button ID="btnYes_CLR" runat="server" Text="Yes" />                
            </div>
        </asp:Panel>
         End Here --%>

    </div>
    <asp:TextBox ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>

    <asp:TextBox ID="TextBox1" runat="server" MaxLength="50" Visible="false" AutoPostBack="true" > </asp:TextBox>

    <asp:TextBox Visible="false" ID="txtLeaveType" AutoComplete="off" ReadOnly="true" runat="server" CssClass="Dropdown" OnTextChanged="txtLeaveType_TextChanged" AutoPostBack="true"></asp:TextBox>

    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />

    <asp:HiddenField ID="hflEmpName" runat="server" />

    <asp:HiddenField ID="hflEmpDesignation" runat="server" />

    <asp:HiddenField ID="hflEmpDepartment" runat="server" />

    <asp:HiddenField ID="hflEmailAddress" runat="server" />

    <asp:HiddenField ID="hflGrade" runat="server" />

    <asp:HiddenField ID="hflapprcode" runat="server" />

    <asp:HiddenField ID="hdnFromFor" runat="server" />


    <asp:HiddenField ID="hdlDate" runat="server" />
    
    <asp:HiddenField ID="hdnleaveconditiontypeid" runat="server" />
    <asp:HiddenField ID="htnleavetypeid" runat="server" />
    <asp:HiddenField ID="hdnleavedays" runat="server" />

    <asp:HiddenField ID="hdnlstfromfor" runat="server" />
    <asp:HiddenField ID="hdnlsttofor" runat="server" />
    <asp:HiddenField ID="hdnToDate" runat="server" />
    <asp:HiddenField ID="hdnReqid" runat="server" />
    <asp:HiddenField ID="hdnOldLeaveCount" runat="server" />
    <asp:HiddenField ID="hdnLeaveStatus" runat="server" />
    <asp:HiddenField ID="hflLeavestatus" runat="server" />
    <asp:HiddenField ID="hflstatusid" runat="server" />
    <asp:HiddenField ID="hdnAppr_status" runat="server" />

    <asp:HiddenField ID="hdnmsg" runat="server" />
    <asp:HiddenField ID="hdnfrmdate_emial" runat="server" />
    <asp:HiddenField ID="hdntodate_emial" runat="server" />
    <asp:HiddenField ID="hdnHRMailId_MLLWP" runat="server" />
    <asp:HiddenField ID="hdnPLwithSL_succession" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />

    <script type="text/javascript">

        function validateFromFor(leavetypeid, leavetypeFSH, tt, todate, fromdate, msg) {

            if (leavetypeFSH == "First Half") {

                document.getElementById("<%=txtToDate.ClientID%>").disabled = true;
                document.getElementById("<%=txtToFor.ClientID%>").disabled = true;
                document.getElementById("<%=txtToFor.ClientID%>").value = "";
            }
            else {

                document.getElementById("<%=txtToDate.ClientID%>").disabled = false;
                document.getElementById("<%=txtToFor.ClientID%>").disabled = false;
            }
            if (todate == fromdate) {

                document.getElementById("<%=txtToFor.ClientID%>").disabled = true;
                document.getElementById("<%=txtToFor.ClientID%>").value = "";
            }
            else {

                document.getElementById("<%=txtToFor.ClientID%>").disabled = false;

            }
            //}  
            if (leavetypeid == "5") {
                document.getElementById("<%=txtToFor.ClientID%>").disabled = true;
                document.getElementById("<%=txtToDate.ClientID%>").disabled = true;
            }

            if (todate != "") {
                document.getElementById("<%=txtToDate.ClientID%>").value = todate;
            }
            if (tt == "0") {
                document.getElementById("<%=txtLeaveDays.ClientID%>").value = "";
            }
            else {
                document.getElementById("<%=txtLeaveDays.ClientID%>").value = tt;
            }
            document.getElementById("<%=lblmessage.ClientID%>").innerHTML = msg;
            return;
        }

        function validateToFor(tt, msg) {

            document.getElementById("<%=txtLeaveDays.ClientID%>").value = tt;
            document.getElementById("<%=lblmessage.ClientID%>").innerHTML = msg;
            return;
        }

        function textboxMultilineMaxNumber(txt, maxLen) {
            try {
                if (txt.value.length > (maxLen - 1)) return false;
            } catch (e) {
            }
        }

        function validateLeaveType(leavetypeid)
        {
           
            document.getElementById("<%=txtToDate.ClientID%>").value = "";
            document.getElementById("<%=txtFromdate.ClientID%>").value = "";
            document.getElementById("<%=txtLeaveDays.ClientID%>").value = "";
            document.getElementById("<%=lblmessage.ClientID%>").innerHTML = "";
            document.getElementById("<%=txtToFor.ClientID%>").value = "Full Day";
            document.getElementById("<%=txtFromfor.ClientID%>").value = "Full Day";
            document.getElementById("<%=txtReason.ClientID%>").value = "";
            document.getElementById("<%=hdnlstfromfor.ClientID%>").value = "";
            document.getElementById("<%=hdnlsttofor.ClientID%>").value = "";
            document.getElementById("<%=hdnToDate.ClientID%>").value = "";
            document.getElementById("<%=hdnleavedays.ClientID%>").value = "";

            document.getElementById("<%=txtFromfor.ClientID%>").disabled = false;
            document.getElementById("<%=txtFromdate.ClientID%>").disabled = false;
            document.getElementById("<%=txtToFor.ClientID%>").disabled = false;
            document.getElementById("<%=txtToDate.ClientID%>").disabled = false;

            if (leavetypeid == "5") {
                document.getElementById("<%=txtToFor.ClientID%>").disabled = true;
                 document.getElementById("<%=txtToDate.ClientID%>").disabled = true;
             }

            return;

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
            var numcheck = /[0123456789./]/;

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

        function UploadFile(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnUpload.ClientID %>").click();
        }
        }

        function SaveMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=btnSave.ClientID%>');

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

        function SaveMultiClick_Modify() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=btnMod.ClientID%>');

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
                var ele = document.getElementById('<%=btnCancel.ClientID%>');

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
            //alert(confirm_value.value);
            //document.forms[0].appendChild(confirm_value);
            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return;

        }

    </script>
    
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="ApprovedTravelReqst_Acc.aspx.cs" Inherits="myaccount_ApprovedTravelReqst_Acc" %>


<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <%--<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <%-- <link href="<%=ReturnUrl("css") %>calendar/calendar.css" rel="stylesheet" type="text/css" />
    <link href="sampleform_css.css" rel="stylesheet" type="text/css" />--%>
    <%--<ajaxToolkit:ToolkitScriptManager ID="srcp" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <style>
        .myaccountpagesheading {
            text-align: center !important;
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

        .grayDropdown {
            border-bottom: 2px solid #cccccc;
            /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
            background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;
            background-color: #ebebe4;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
    <script type="text/javascript">
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
                        <asp:Label ID="lblheading" runat="server" Text="Travel Expense Voucher"></asp:Label>
                    </span>
                </div>
                    <%--<a href="travelindex.aspx" class="aaab">Travel Index</a>--%>
                <span>
                    <a href="travelindex.aspx" class="aaaa" >Travel Home</a>
                </span>

                <div class="edit-contact">
                    <%--<div class="editprofile" style="text-align: center; border: none;" id="divmsg" runat="server" visible="false">
                        <asp:Label runat="server" ID="lblmessage" Visible="true" Style="color: green; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                    </div>--%>
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" OnClick="lnkcont_Click"></asp:LinkButton>
                        </div>
                    </div>
                    <div class="editprofile btndiv"
                     <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                        </div>
                    <ul id="editform" runat="server" visible="false">

                          <li class="trvl_Reason">
                            <span>Employee Code</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br /> 
                            <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="True" Enabled="false"> </asp:TextBox>
                        </li>
                          <li class="trvl_Reason">
                            <span>Employee Name </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtEmpName" runat="server" Visible="True" Enabled="false"></asp:TextBox>
                        </li>

                        <li class="trvl_type">
                            <span>Travel Type</span>&nbsp;&nbsp;<span style="color: red">*</span><br />

                            <asp:TextBox AutoComplete="off" ID="txtTriptype" runat="server" CssClass="grayDropdown" Enabled="false"></asp:TextBox>
                            <asp:Panel ID="Panel3" Style="display: none;" runat="server" CssClass="taskparentclasskkk">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstTripType" runat="server" CssClass="taskparentclasskkk" AutoPostBack="true">
                                            <%--<asp:ListItem>Domestic</asp:ListItem>
                                            <asp:ListItem>International</asp:ListItem>--%>
                                        </asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <ajaxToolkit:PopupControlExtender ID="PopupControlExtender2" PopupControlID="Panel3" TargetControlID="txtTriptype"
                                Position="Bottom" runat="server">
                            </ajaxToolkit:PopupControlExtender>
                            <%--   <asp:UpdatePanel ID="Upl_leavetype" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlTripType" runat="server" OnSelectedIndexChanged="ddlTripType_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlTripType" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>--%>

                        </li>

                        <li class="trvl_date">
                            <span>From </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />

                            <asp:TextBox AutoComplete="off" ID="txtFromdate" runat="server" AutoPostBack="True"  Enabled="false"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtFromdate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>

                            <%--  <asp:TextBox AutoComplete="off" ID="txtsadate" runat="server" CssClass="txtdatepicker" placeholder="Start Date" OnTextChanged ="txtsadate_TextChanged" AutoPostBack ="true" ></asp:TextBox>
                                <span class="texticon tooltip" title="Select Start Date. Date should be MM/DD/YYYY format."><i class="fa fa-calendar" aria-hidden="true"></i></span>
                                <asp:Label ID="lblsdate" runat="server" Text="" Font-Size="15px"></asp:Label>--%>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="formerror" ControlToValidate="txtsadate" SetFocusOnError="True" ErrorMessage="Please enter start date" ValidationGroup="validate"></asp:RequiredFieldValidator>--%>

                        </li>
                        <li class="trvl_date">
                            <span>To</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtToDate" runat="server" AutoPostBack="True"  Enabled="false"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>

                        <li class="trvl_Reason">
                            <span>Reason for Travel </span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtReason" runat="server" MaxLength="100" Enabled="false"></asp:TextBox>
                        </li>

                        <li class="trvl_Advances" style="display:none;">
                            <span id="lbl_adv" runat="server" >Advances Required: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtAdvance" runat="server" MaxLength="100" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="trvl_Currency" style="display:none;">
                            <span id="lbl_cur" runat="server" >Currency Required:</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtreqCur" runat="server" MaxLength="100" Enabled="false"></asp:TextBox>
                        </li>
                        <li style="display:none;"></li>
                        <li class="trvl_Currency">                            
                            <span >Project Name </span>&nbsp;&nbsp;<span style="color:red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_ProjectName" runat="server" Enabled="false"></asp:TextBox>
                        </li>

                        <li class="trvl_Currency">                            
                            <span >Department Name </span>&nbsp;&nbsp;<span style="color:red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_DeptName" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li></li>
                       <li class="trvl_local">
                            <asp:LinkButton ID="lnkbtn_expdtls" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" Visible="false"></asp:LinkButton>
                            <span>Expenses Details: </span>
                        </li>
                           <li class="trvl_grid">
                            <div>
                                <asp:GridView ID="gvexpensdtls" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                   DataKeyNames="trip_id,exp_id,exp_sr_no,trip_details" OnRowDataBound="gvexpensdtls_RowDataBound">
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
                                        <asp:BoundField HeaderText="Date"
                                            DataField="trip_frm_date"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>

                                        <asp:BoundField HeaderText="To Date"
                                            DataField="trip_frm_date"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="0%" ItemStyle-BorderColor="Navy" Visible="false"/>

                                        <asp:BoundField HeaderText="Place"
                                            DataField="trip_destination"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>

                                        <asp:BoundField HeaderText="Details"
                                            DataField="exp_name"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>

                                        <asp:BoundField HeaderText="Description"
                                            DataField="Descr"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy"/>

                                         <asp:BoundField HeaderText="Paid by Company"
                                            DataField="paid_by_comp"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="8%" ItemStyle-BorderColor="Navy"/>

                                         <asp:BoundField HeaderText="Paid by Employee"
                                            DataField="paid_emp"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="8%" ItemStyle-BorderColor="Navy"/>

                                          <asp:BoundField HeaderText="Total Amount Claimed"
                                            DataField="totamt"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="8%" ItemStyle-BorderColor="Navy"/>
                                            
                                            <asp:BoundField HeaderText="Paid by Company (Amt Release By Account)"
                                            DataField="amtRelAccPayCom"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="8%" ItemStyle-BorderColor="Navy"/>
                                            
                                            <asp:BoundField HeaderText="Paid by Employee (Amt Release By Account)"
                                            DataField="amtRelAccPayEmp"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="8%" ItemStyle-BorderColor="Navy"/>
                                            
                                            <asp:BoundField HeaderText="Account Remarks"
                                            DataField="AccountRemarks"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="20%" ItemStyle-BorderColor="Navy"/>
                                            
                                            <asp:BoundField HeaderText="Eligibility" Visible="false"
                                            DataField="eligibility"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="0%" ItemStyle-BorderColor="Navy"/>

                                            <asp:BoundField HeaderText="Diff Amt" Visible="false"
                                            DataField="diff"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="0%" ItemStyle-BorderColor="Navy"/>
 

                                    </Columns>

                                </asp:GridView>

                            </div>
                        </li>

                  
                     
                     <li class="trvl_local">
                        <span>Total Amount Claimed</span>
                         <asp:TextBox AutoComplete="off" ID="txtTotAmtClaimed" runat="server" MaxLength="10"  Enabled="False"></asp:TextBox>
                     </li>
                     
                     <li class="trvl_local">
                        <span>Less Advance Taken</span>
                         <asp:TextBox AutoComplete="off" ID="txtLessAdvTaken" runat="server" MaxLength="10" Enabled="False"></asp:TextBox>
                     </li>
                   
                     <li class="trvl_local">
                        <span>Net Payable to Company</span>
                         <asp:TextBox AutoComplete="off" ID="txtnetPaybltoComp" runat="server" MaxLength="10" Enabled="False"></asp:TextBox>
                     </li>
                     
                     <li class="trvl_local">
                        <span>Net Payable to Employee</span>
                         <asp:TextBox AutoComplete="off" ID="txtnetPaybltoEmp" runat="server" MaxLength="10" Enabled="False"></asp:TextBox>
                     </li>
                    
                     <li class="trvl_local">
                        <span>Comments</span>
                         <asp:TextBox AutoComplete="off" ID="txtReasonDeviation" runat="server" MaxLength="100" Enabled="False"></asp:TextBox>
                     </li>
                    
                    <li class="trvl_Reason">
                            <%--<span>Comments </span>--%>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtComments" Visible="false" Enabled="false" runat="server" MaxLength="30" ></asp:TextBox>
                        </li>
                    <li class="trvl_Reason">
                             <asp:CheckBox ID="chk_exception" runat="server" Enabled="false" Text="Is Exception" />
                            <%--<asp:TextBox AutoComplete="off" ID="TextBox1" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>--%>
                        </li>                        
                      <li class="trvl_local">
                        <span id="SpnAccountAmount" runat="server" Visible="true">Amount Released by Accounts </span>
                         <asp:TextBox AutoComplete="off" ID="txtAccountAmount" runat="server" MaxLength="10" Enabled="False"></asp:TextBox>
                     </li>
                         <li class="trvl_Reason">
                            <span id="SpnAccountRemark" runat="server" Visible="true">Remarks by Accounts </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtAccountRemark" runat="server" MaxLength="200" Enabled="False"></asp:TextBox>
                        </li>
                     <li class="trvl_local">
                        <span id="idspnuploadfile" runat="server" visible="false">Upload File</span>
                         <asp:FileUpload ID="ploadexpfile" runat="server" Enabled="false" Visible="false"></asp:FileUpload>
                          <asp:GridView ID="gvuploadedFiles" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                   DataKeyNames="fileid" >
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
                                      <%--  <asp:BoundField HeaderText="Uploaded Files"
                                            DataField="filename"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="30%" />--%>

                                        <asp:TemplateField HeaderText="Uploaded Files">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkviewfile" runat="server" OnClientClick=<%# "DownloadFile('" + Eval("filename") + "')" %>  Text='<%# Eval("filename") %>' >
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                      
                                    </Columns>
                        </asp:GridView>
                     </li>

                     

                        <li class="trvl_Approver">
                            <span>Approver </span>
                            <br />
                            <asp:ListBox ID="lstApprover" runat="server" Enabled="false"></asp:ListBox>
                        </li>

                        <li class="trvl_inter" style="display:none;">
                            <span>For Information To </span>
                            <br />
                            <asp:ListBox ID="lstIntermediate" runat="server"></asp:ListBox>
                        </li>

                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="trvl_Savebtndiv">
        <asp:LinkButton ID="trvl_btnSave" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" >Submit</asp:LinkButton>

        <div style="display:none">
              <li class="trvL_detail">
                            <asp:LinkButton ID="btnTra_Details"  runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" Enabled="false" Visible="false"></asp:LinkButton>
                            <span style="display:none">Travel Details</span>
              </li>
                   <li class="trvl_grid">

                            <div>

                                <asp:GridView ID="dgTravelRequest" Visible="false" runat="server" Enabled="false" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false">
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
                                        <asp:BoundField HeaderText="Mode"
                                            DataField="trip_mode"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy"/>

                                        <asp:BoundField HeaderText="Dep Date"
                                            DataField="departure_date"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>

                                        <asp:BoundField HeaderText="Dep Place"
                                            DataField="departure_place"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>

                                        <asp:BoundField HeaderText="Arr Date"
                                            DataField="arrival_date"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>
                                        <asp:BoundField HeaderText="Arr Place"
                                            DataField="arrival_place"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>
                                        <%-- <asp:BoundField HeaderText="Through Travel Desk"
                                            DataField="travel_through_desk"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="10%" />--%>
                                        <asp:TemplateField HeaderText="Through Travel Desk">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkCOS" Visible="false" runat="server" Checked='<%#Convert.ToBoolean(Eval("tt")) %>' />
                                                <asp:Label ID="lblchkcos" runat="server"  Text='<%#Eval("travel_through_desk")%>' />
                                            </ItemTemplate>
                                              <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="10%" />
                                        </asp:TemplateField>
                                         <asp:BoundField HeaderText="Status"
                                            DataField="lbookedStatus"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>
                                        <asp:BoundField HeaderText="Deviation"
                                            DataField="deviation"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>
                                        <%--            <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkTravelDetailsEdit" runat="server" Text='View' OnClick="lnkTravelDetailsEdit_Click">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>

                            </div>

                        </li>

             <li class="trvl_Accomodation">
                            <asp:LinkButton ID="trvl_accmo_btn" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve"  Visible="false"></asp:LinkButton>
                            <span style="display:none">Accommodation: </span>
                        </li>

                        <li class="trvl_grid">

                            <div>
                                <asp:GridView ID="dgAccomodation" runat="server" Visible="false" Enabled="false" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false">
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
                                        <asp:BoundField HeaderText="From Date"
                                            DataField="From Date"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy"/>

                                        <asp:BoundField HeaderText="To Date"
                                            DataField="To Date"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy"/>

                                        <asp:BoundField HeaderText="Location"
                                            DataField="Location"
                                           ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy"/>

                                        <%-- <asp:BoundField HeaderText="Through COS"
                                            DataField="Through COS"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="20%" />--%>

                                        <asp:TemplateField HeaderText="Through COS">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkCOS" runat="server"  Visible="false"  Checked='<%#Convert.ToBoolean(Eval("tt")) %>' />
                                                <asp:Label ID="lblchkcos" runat="server"  Text='<%#Eval("travel_through_desk")%>' />
                                            </ItemTemplate>
                                              <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"  Width="10%"/>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Status"
                                        DataField="lbookedStatus"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                        ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>

                                          <asp:BoundField HeaderText="Deviation"
                                            DataField="deviation"
                                           ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>

                                        <%--                <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkAccomodationdit" runat="server" Text='View' OnClick="lnkAccomodationdit_Click">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>

                            </div>

                        </li>

                        <li class="trvl_local">
                            <asp:LinkButton ID="trvl_localbtn" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" Visible="false"></asp:LinkButton>
                            <span style="display:none">Local Travel: </span>
                        </li>

                        <li class="trvl_grid">

                            <div>

                                <asp:GridView ID="dgLocalTravel" Visible="false" runat="server" Enabled="false" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false">
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
                                        <asp:BoundField HeaderText="From Date"
                                            DataField="From Date"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy"/>

                                        <asp:BoundField HeaderText="To Date"
                                            DataField="To Date"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy"/>

                                        <asp:BoundField HeaderText="Location"
                                            DataField="Location"
                                           ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy"/>

                                        <%--  <asp:BoundField HeaderText="Through COS"
                                            DataField="Through COS"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="20%" />--%>

                                        <asp:TemplateField HeaderText="Through COS">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkCOS" runat="server" Visible="false" Checked='<%#Convert.ToBoolean(Eval("tt")) %>' />
                                                <asp:Label ID="lblChkCOS" runat="server"  Text='<%#Eval("Through_COS")%>' />
                                            </ItemTemplate>
                                              <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="10%"/>
                                        </asp:TemplateField>
                                            <asp:BoundField HeaderText="Status"
                                                DataField="lbookedStatus"
                                                ItemStyle-HorizontalAlign="left"
                                                HeaderStyle-HorizontalAlign="left"
                                                ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>
                                          <asp:BoundField HeaderText="Deviation"
                                            DataField="deviation"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy"/>
                                        <%--                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkLocalTravleEdit" runat="server" Text='View' OnClick="lnkLocalTravleEdit_Click">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>

                            </div>

                        </li>


           <li class="trvl_local">
                        <span>Daily Halting Allowance (INR) </span>
                         <asp:TextBox AutoComplete="off" ID="txtdailyhaltingallowance" runat="server" MaxLength="10" Enabled="False"></asp:TextBox>
            </li>
            </div>       

    </div>
    <div class="trvl_Savebtndiv">

        <span>
            <asp:LinkButton ID="btnApprove" Visible="false" runat="server" Text="Approve" ToolTip="Approve" CssClass="Savebtnsve">Approve</asp:LinkButton>
        </span>

        <span>
            <asp:LinkButton ID="btnCorrection" Visible="false" runat="server" Text="Correction" ToolTip="" CssClass="Savebtnsve">Send for Modification</asp:LinkButton>
        </span>

        <span>
            <asp:LinkButton ID="btnReject"  Visible="false" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve">Reject</asp:LinkButton>
        </span>

        <span>
            <asp:LinkButton ID="btnBack" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="btnBack_Click">Back</asp:LinkButton>
        </span>
        <span>
            <asp:LinkButton ID="mobile_btnPrintPV" runat="server" Text="Print Voucher" ToolTip="Print Voucher" CssClass="Savebtnsve" OnClick="mobile_btnPrintPV_Click">Print Voucher</asp:LinkButton>
        </span>

          <%-- Following Popup for Approved Travel Expenses Request --%>
        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" DisplayModalPopupID="mpe_Appr" TargetControlID="btnApprove">
        </ajaxToolkit:ConfirmButtonExtender>
        <ajaxToolkit:ModalPopupExtender ID="mpe_Appr" runat="server" PopupControlID="pnlPopup_Appr" TargetControlID="btnApprove" OkControlID = "btnYes_Appr"
            CancelControlID="btnNo_Appr" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup_Appr" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Confirmation
            </div>
            <div class="body">
                Do you want to Approve Travel Expenses Request?
            </div>
            <div class="footer" align="right">
                <asp:Button ID="btnNo_Appr" runat="server" Text="No" />
                <asp:Button ID="btnYes_Appr" runat="server" Text="Yes" />                
            </div>
        </asp:Panel>


        <%-- Following Popup for Send For Correction Travel Expenses Request --%>
        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" DisplayModalPopupID="mpe_correction" TargetControlID="btnCorrection">
        </ajaxToolkit:ConfirmButtonExtender>
        <ajaxToolkit:ModalPopupExtender ID="mpe_correction" runat="server" PopupControlID="pnlPopup_correction" TargetControlID="btnCorrection" OkControlID = "btnYes_cr"
            CancelControlID="btnNo_cr" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup_correction" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Confirmation
            </div>
            <div class="body">
                Do you want to Send Travel Expenses Request for Correction?
            </div>
            <div class="footer" align="right">
                <asp:Button ID="btnNo_cr" runat="server" Text="No" />
                <asp:Button ID="btnYes_cr" runat="server" Text="Yes" />                
            </div>
        </asp:Panel>

         <%-- Following Popup for Reject Travel Expenses Request --%>
        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" DisplayModalPopupID="mpe_reject" TargetControlID="btnReject">
        </ajaxToolkit:ConfirmButtonExtender>
        <ajaxToolkit:ModalPopupExtender ID="mpe_reject" runat="server" PopupControlID="pnlPopup_Reject" TargetControlID="btnReject" OkControlID = "btnYes_R"
            CancelControlID="btnNo_R" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup_Reject" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Confirmation
            </div>
            <div class="body">
                Do you want to Reject Travel Expenses Request?
            </div>
            <div class="footer" align="right">
                <asp:Button ID="btnNo_R" runat="server" Text="No" />
                <asp:Button ID="btnYes_R" runat="server" Text="Yes" />                
            </div>
        </asp:Panel>

    </div>
   <%-- <div class="trvl_Savebtndiv">
        <asp:LinkButton ID="btnMod" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve">Modify</asp:LinkButton>
        <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="Savebtnsve">Cancel</asp:LinkButton>
        <asp:LinkButton ID="btnback_mng" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/ManageTravelRequest.aspx">Back</asp:LinkButton>
    </div>--%>


    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />

    <asp:HiddenField ID="FilePath" runat="server" />
    <asp:HiddenField ID="hflEmpName" runat="server" />

     <asp:HiddenField ID="hdnApprId" runat="server" />

     <asp:HiddenField ID="hflEmpDesignation" runat="server" />

    <asp:HiddenField ID="hflEmpDepartment" runat="server" />

    <asp:HiddenField ID="hflloginApprEmail" runat="server" />

    <asp:HiddenField ID="hndloginempcode" runat="server" />

    <asp:HiddenField ID="hdnNextApprCode" runat="server" />

    <asp:HiddenField ID="hdnNextApprName" runat="server" />

    <asp:HiddenField ID="hdnNextApprEmail" runat="server" />

    <asp:HiddenField ID="hflGrade" runat="server" />

    <asp:HiddenField ID="hflapprcode" runat="server" />

    <asp:HiddenField ID="hdnDesk" runat="server" />

    <asp:HiddenField ID="hdnDestnation" runat="server" />

    <asp:HiddenField ID="hdnTripid" runat="server" />

    <asp:HiddenField ID="hdnAcctripid" runat="server" />

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

    <asp:HiddenField ID="hdnNextApprId" runat="server" />

    <asp:HiddenField ID="hdnApprEmailaddress" runat="server" />
    <asp:HiddenField ID="hdnIntermediateEmail" runat="server" />
     <asp:HiddenField ID="hdnstaus" runat="server" />
    
    <asp:HiddenField ID="hdnEligible" runat="server" />
    <asp:HiddenField ID="hdnTrdays" runat="server" />
    <asp:HiddenField ID="hdnCurrentApprID" runat="server" />

    <asp:HiddenField ID="hdnReqEmailaddress" runat="server" />

    <asp:HiddenField ID="hdnExpid" runat="server" />
    <asp:HiddenField ID="hdnfrmdate_emial" runat="server" />
    <asp:HiddenField ID="hdntodate_emial" runat="server" />
    <asp:HiddenField ID="hdnActualTrvlDays" runat="server" />

    
    <asp:HiddenField ID="hdnApprovalACC_mail" runat="server" /> 
    <asp:HiddenField ID="hdnApprovalACC_Code" runat="server" /> 
    <asp:HiddenField ID="hdnApprovalACC_ID" runat="server" /> 
    <asp:HiddenField ID="hdnApprovalACC_Name" runat="server" /> 
    <asp:HiddenField ID="hdnisApproval_ACC_Status" runat="server" /> 
    <asp:HiddenField ID="hdnInboxType" runat="server" /> 
     <asp:HiddenField ID="hdncomp_code" runat="server" />
	<asp:HiddenField ID="hdndept_Id" runat="server" /> 
    <script type="text/javascript">

        function validateTripType(triptypeid) {
            if (triptypeid == "1") {
                document.getElementById("<%=txtreqCur.ClientID%>").value = "";
                //document.getElementById("<%=txtreqCur.ClientID%>").disabled = true;
                document.getElementById("<%=txtreqCur.ClientID%>").style.visibility = "hidden";
                document.getElementById("<%=lbl_cur.ClientID%>").style.visibility = "hidden";
            }
            else {
                document.getElementById("<%=txtreqCur.ClientID%>").value = "";
                //document.getElementById("<%=txtreqCur.ClientID%>").disabled = false;
                document.getElementById("<%=txtreqCur.ClientID%>").style.visibility = "visible";
                document.getElementById("<%=lbl_cur.ClientID%>").style.visibility = "visible";
                //document.getElementById("<%=txtreqCur.ClientID%>").style.backgroundColor = white;
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
        function DownloadFile(file) {
            // alert(file);
            var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;

            //alert(localFilePath);
            window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
        }

    </script>
</asp:Content>

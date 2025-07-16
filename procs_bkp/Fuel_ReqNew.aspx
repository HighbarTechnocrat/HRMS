<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="Fuel_ReqNew.aspx.cs" 
    MaintainScrollPositionOnPostback="true"
    Inherits="procs_Fuel_ReqNew" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
   
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/fuel_RemRequest_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />

    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
             /*background: #dae1ed;*/
           background:#ebebe4;
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
                        <asp:Label ID="lblheading" runat="server" Text="Fuel Bill"></asp:Label>
                    </span>
                </div>

                <span>
                    <a href="Fuel.aspx" class="aaaa">Fuel Claim Home</a>
                </span>


                <div class="edit-contact">
                    <div class="editprofile" style="text-align: center; border: none;" id="divmsg" runat="server" visible="false">
                        
                    </div>
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" ></asp:LinkButton>
                        </div>
                    </div>


                    <ul id="editform" runat="server" visible="false">

                         <li class="fuel_inboxEmpCode">

                           <asp:Label runat="server" ID="lblmessage" Visible="False" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>

                        </li>
                        <li class="fuel_claimed">
                            <br />
                            <asp:TextBox AutoComplete="off" ID="TextBox1" runat="server" MaxLength="100" Visible="false"></asp:TextBox>
                        </li>

                        <li class="fuel_inboxEmpCode" style="display:none;">

                            <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="False" > </asp:TextBox>

                        </li>
                        <li class="fuel_InboxEmpName" style="display:none;">

                            <asp:TextBox AutoComplete="off" ID="txtEmpName" runat="server" Visible="false"></asp:TextBox>

                        </li>

                        <li class="fuel_date" style="display:none;">
                           <%--  Commented by R1 on 01-10-2018 <span>Submission On </span>   
                            <br />--%>

                            <asp:TextBox AutoComplete="off" ID="txtFromdateMain" runat="server" AutoPostBack="True" Visible="false" ></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtFromdateMain"
                                runat="server">
                            </ajaxToolkit:CalendarExtender> 
                        </li>
                           <li class="fuel_claimed" style="display:none;">
                         <%--   <span>Total Quantity Claimed: </span>--%>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="TextBox11" runat="server" MaxLength="100" Visible="false"></asp:TextBox>
                        </li>
                        <li class="fuel_detail" >
                            <span style="font-size:12pt;font-weight:bold; text-decoration-line:underline;">Add Fuel Bills: </span>
<%--                            <asp:LinkButton ID="lnk_fuel_drop" runat="server" Text="Add Fuel Bills" CssClass="Savebtnsve" ToolTip="Browse" OnClientClick="Show_Hide()" ></asp:LinkButton>
                            <asp:ImageButton id="img_fuel_drop" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/arrowdown.png" />--%>
                        </li>
                        <li class="claimfuel_Amount">

                            <asp:TextBox AutoComplete="off" ID="TextBox2" Visible="false" runat="server"  MaxLength="50"></asp:TextBox>
                        </li>

                        <li class="claimfuel_fromdate">
                            <span>Bill Date </span>&nbsp;&nbsp;<span style="color:red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtFromdate" runat="server" AutoPostBack="True" OnTextChanged="txtFromdate_TextChanged" ></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2"  Format="dd/MM/yyyy" TargetControlID="txtFromdate" OnClientDateSelectionChanged="CheckDateEalier" 
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>
                        <li class="claimfuel_Reimbursement" style="display:none;">
                            <span >
                            <span>Rate</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtRate" ReadOnly="true" Enabled="False" runat="server" MaxLength="100"></asp:TextBox>
                        </span>
                           </li>
                      
                        <li class="claimfuel_Amount" style="display:none;font-family:'Trebuchet MS' ">
                            <span>Quantity (Ltrs.)</span>&nbsp;&nbsp;<span style="color:red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtQuantity" runat="server"  MaxLength="10"></asp:TextBox>
                        </li>
                        <li class="claimfuel_ElgAmount">
                            <span>Amount </span>&nbsp;&nbsp;<span style="color:red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtElgAmnt" runat="server"  MaxLength="10"></asp:TextBox>
                        </li>

                         <li class="claimfuel_Amount">
                            <span>Balance Amount (Current Month)-(A)</span>&nbsp;&nbsp;
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_BalanceAmountCurrentMonth" ReadOnly="true" Enabled="False" runat="server" MaxLength="10"></asp:TextBox>
                        </li>
                        <li class="claimfuel_ElgAmount">
                            <span>Balance Amount (Previous Month)-(B)</span>&nbsp;&nbsp;
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_BalanceAmountPreviousMonth" ReadOnly="true" Enabled="False" runat="server" MaxLength="10"></asp:TextBox>
                        </li>

                        <li class="claimfuel_Reimbursement" style="display:none;">
                            <asp:TextBox AutoComplete="off" ID="txtEligibleQty_Monthly" runat="server" Visible="false"  ReadOnly="true" Enabled="False" MaxLength="100" ></asp:TextBox>
                        </li>

                        <li class="claimfuel_Reason" style="display:none;">
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtEligibleQty" runat="server" Visible="false" ReadOnly="true" Enabled="False" MaxLength="100"></asp:TextBox>
                        </li>
                        <li class="claimfuel_Reason" style="display:none;">
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtBalanceQty" runat="server" Visible="false" ReadOnly="true" Enabled="False" MaxLength="100"></asp:TextBox>
                        </li>
                        <li class="claimfuel_Reason" style="display:none;">
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtTollCharges" runat="server" MaxLength="10" Visible="false"></asp:TextBox>
                        </li>

                        <li class="fuel_detail">                            
                            <asp:LinkButton ID="btnfuel_Details" runat="server" Text="Add Fuel Bills" ToolTip="Add Fuel Bills" Font-Bold="false" CssClass="Savebtnsve" OnClick="btnfuel_Details_Click"></asp:LinkButton>
                            <%--<span>Claim Details</span>--%>
                        </li>

                        <li class="fuel_grid">
                            <div>
                                <asp:GridView ID="dgFuelClaim" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" 
                                    BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="70%"
                                     DataKeyNames="Rem_id,FuelClaims_id">
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" HorizontalAlign="Center" />
                                    <PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />

                                    <Columns>
                                        <asp:BoundField HeaderText="Date"
                                            DataField="Rem_Month"
                                            ItemStyle-HorizontalAlign="Center"                                       
                                            ItemStyle-Width="15%" />

                                        <asp:BoundField HeaderText="Bill No"
                                            DataField="BillNumber"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="15%"  Visible="false"/>

                                        <asp:BoundField HeaderText="Amount"
                                            DataField="Amount"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="15%" />

                                            <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                            <asp:ImageButton id="lnkEdit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkEdit_Click1"/>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                            <asp:ImageButton id="btn_del" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/delete.png" OnClick="Del_Fuel_bill"/>
                                            </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                   

                                    </Columns>
                                </asp:GridView>
                            </div>

                        </li>
                        <br /><br />
                        <li class="fuel_claimed">
                             <span> Airport Parking (If Any) </span>
                            <asp:TextBox AutoComplete="off" ID="txt_airport_parking" Text="0.00" runat="server" MaxLength="10" OnTextChanged="txt_airport_parking_TextChanged" AutoPostBack="true"></asp:TextBox>
                            
                        </li>

                        <li class="fuel_claimed">
                             <span> Toll Pass / Charges (entitlement / month) </span>
                            <asp:TextBox AutoComplete="off" ID="txtTollCharges_Ent" Text="0.00" Enabled="false" runat="server" MaxLength="10" ></asp:TextBox>
                            <asp:TextBox AutoComplete="off" ID="txtTollCharges_Bal" Text="0.00" Visible="false" runat="server" MaxLength="10" ></asp:TextBox>
                            
                        </li>
                        <li class="fuel_claimed">
                            <span id="Span_Toll_Balance" runat="server"> Toll Pass / Charges </span>
                            <asp:TextBox AutoComplete="off" ID="txtTollChargesMain" Text="0.00" runat="server"  MaxLength="10" OnTextChanged="txtTollChargesMain_TextChanged" AutoPostBack="true"></asp:TextBox>                   
                        </li>
                        <li class="fuel_claimed">   
                            <span id="idspnParkWash_month" runat="server" visible="false">Parking / Washing & Toll Charges for the Month </span>
                                 <asp:TextBox AutoComplete="off" ID="txt_parkwash_month" runat="server" AutoPostBack="True" Visible="false" OnTextChanged="txt_parkwash_month_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" Format="MMM/yyyy" OnClientHidden="onCalendarHidden"  OnClientShown="onCalendarShown" BehaviorID="calendar1" TargetControlID="txt_parkwash_month" OnClientDateSelectionChanged="CheckDateWashing" 
                                runat="server">
                            </ajaxToolkit:CalendarExtender> 
                            
                        </li>

                         <li class="fuel_claimed">
                            <span id="Span1" runat="server"> Washing Allowance </span>
                            <asp:TextBox AutoComplete="off" ID="Txt_WashingAllowance" Text="0.00" runat="server" ReadOnly="true" MaxLength="10"  Enabled="False"></asp:TextBox>                   
                        </li>
                        <li class="fuel_claimed">
                         </li>

                        <li class="claimfuel_Reason">
                            <span>Total Claim Amount</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_TotalClaimAmount" runat="server" ReadOnly="true" Enabled="False" MaxLength="100"></asp:TextBox>
                        </li>
                        <li class="claimfuel_Reason">
                             <span>Cumulative Balance Amount- C=(A+B)</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_BlanceAmount" runat="server" MaxLength="10" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        
                          <li class="fuel_upload">
                            <span>Upload File</span><br />
                            <asp:FileUpload ID="uploadfile" runat="server"  AllowMultiple="true"/>
                            <asp:TextBox AutoComplete="off" ID="txtprofile" runat="server" CssClass="mobilenotextbox popup" Visible="false"> </asp:TextBox>
                              <asp:LinkButton ID="lnkuplodedfile" OnClick="lnkuplodedfile_Click" runat="server" Visible="false"></asp:LinkButton>
                             
                              <asp:GridView ID="gvfuel_claimsFiles" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="70%"
                                     DataKeyNames="t_id,file_sr_no">
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
                                        <asp:BoundField HeaderText="Claim Files"
                                            DataField="file_name"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="20%" />
                                         
                                        
                                        <asp:TemplateField HeaderText="Files" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                        <asp:LinkButton ID="lnkViewFiles" runat="server" Text='View' OnClientClick=<%# "DownloadFile('" + Eval("file_name") + "')" %>  ></asp:LinkButton>
                                        </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                   
                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                            <asp:LinkButton ID="btn_del_File" runat="server" Text='Delete' OnClick="btn_del_File_Click1" ></asp:LinkButton>
                                             </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                        </li>
                           <li class="fuel_claimed">
                            <br />
                            <asp:TextBox AutoComplete="off" ID="TextBox17" runat="server" MaxLength="100" Visible="false"></asp:TextBox>
                        </li>
                        <li class="fuel_Approver">
                            <span>Approver </span>
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
                                ItemStyle-Width="26%" 
                                ItemStyle-BorderColor="Navy"
                                />

                            <asp:BoundField HeaderText="Status"
                                DataField="Status"
                                ItemStyle-HorizontalAlign="left"
                                ItemStyle-Width="20%" 
                                ItemStyle-BorderColor="Navy"
                                />

                            <asp:BoundField HeaderText="Approved on"
                                DataField="tdate"
                                ItemStyle-HorizontalAlign="left"
                                ItemStyle-Width="20%" 
                                ItemStyle-BorderColor="Navy"
                                />

                            <asp:BoundField HeaderText="Approver Remarks"
                                DataField="Comment"
                                ItemStyle-HorizontalAlign="left"
                                ItemStyle-Width="34%" 
                                ItemStyle-BorderColor="Navy"
                                />
                        </Columns>
                    </asp:GridView>
                        </li>

                     

                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="fuel_Savebtndiv">
        <asp:LinkButton ID="fuel_btnSave" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClick="fuel_btnSave_Click" OnClientClick="return SaveMultiClick();">Submit</asp:LinkButton>
        <asp:LinkButton ID="fuel_btnBack" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/MyFuel_Req.aspx" >Back</asp:LinkButton>
        <asp:LinkButton ID="fuel_btncancel" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="Savebtnsve" OnClientClick="return CancelMultiClick();" OnClick="fuel_btncancel_Click">Cancel</asp:LinkButton>
         <asp:LinkButton ID="mobile_btnPrintPV" runat="server" Text="Print Payment Voucher" ToolTip="Print Payment Voucher" CssClass="Savebtnsve" OnClick="mobile_btnPrintPV_Click">Print Payment Voucher</asp:LinkButton>
 
        </div>

       
    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />

    <asp:HiddenField ID="FilePath" runat="server" />

    <asp:HiddenField ID="hdnempKMAvg" runat="server" />
    <asp:HiddenField ID="hdnActualTotalFuelConsump" runat="server" />
    <asp:HiddenField ID="hdnsptype" runat="server" />
    <asp:HiddenField ID="hdnfuelQty" runat="server" />
        <asp:HiddenField ID="hflEmpDesignation" runat="server" />

    <asp:HiddenField ID="hflEmpDepartment" runat="server" />
    <asp:HiddenField ID="hflEmailAddress" runat="server" />
    <asp:HiddenField ID="hflGrade" runat="server" />
    <asp:HiddenField ID="hflapprcode" runat="server" />
    <asp:HiddenField ID="hdnRemid" runat="server" />
    <asp:HiddenField ID="hdnClaimsID" runat="server" />
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
    <asp:HiddenField ID="hdnFuelReimbursementType" runat="server" />

    <asp:HiddenField ID="hdnApprovalCOS_Code" runat="server" />
    <asp:HiddenField ID="hdnApprovalCOS_ID" runat="server" />
    <asp:HiddenField ID="hdnApprovalCOS_mail" runat="server" />
    <asp:HiddenField ID="hdnApprovalCOS_Name" runat="server" />
     
     <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="hdnMobRemStatusM" runat="server" />
    <asp:HiddenField ID="hdnMobRemStatus_dtls" runat="server" /> 
    <asp:HiddenField ID="hdnClaimDate" runat="server" /> 
    <asp:HiddenField ID="hdnfrmdate_emial" runat="server" />
    <asp:HiddenField ID="hdntodate_emial" runat="server" />
    <asp:HiddenField ID="hdnIs_GFHCCEOGFO" runat="server" />

    <asp:HiddenField ID="HDBalnceAmtCurrentMonth" runat="server" />
    <asp:HiddenField ID="HDAmountCurrentclaimed" runat="server" />
    <asp:HiddenField ID="HDYearEligibility" runat="server" />
     <asp:HiddenField ID="HDEligibility" runat="server" /> 
    <asp:HiddenField ID="HDTotaltillAmount" runat="server" /> 

    <asp:HiddenField ID="HDMainAmountCurrentMonth" runat="server" /> 
    <asp:HiddenField ID="HDMainAmountMonthPrevious" runat="server" /> 
    <asp:HiddenField ID="HDFlagCheck" runat="server" /> 
     <asp:HiddenField ID="HDEditFag" runat="server" /> 

    <script type="text/javascript">
        function textboxMultilineMaxNumber(txt, maxLen) {
            try {
                if (txt.value.length > (maxLen - 1)) return false;
            } catch (e) {
            }
        }
        function Show_Hide() {
            var li_fuel1 = document.getElementById("li_fuel1");
            if (li_fuel1.style.display == "none") {
                li_fuel1.style.display = "block";
            } else {
                li_fuel1.style.display = "none";
            }
            var li_fuel2 = document.getElementById("li_fuel2");
            if (li_fuel2.style.display == "none") {
                li_fuel2.style.display = "block";
            } else {
                li_fuel2.style.display = "none";
            }
            var li_fuel3 = document.getElementById("li_fuel3");
            if (li_fuel3.style.display == "none") {
                li_fuel3.style.display = "block";
            } else {
                li_fuel3.style.display = "none";
            }
            var li_fuel4 = document.getElementById("li_fuel4");
            if (li_fuel4.style.display == "none") {
                li_fuel4.style.display = "block";
            } else {
                li_fuel4.style.display = "none";
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

        function SaveMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=fuel_btnSave.ClientID%>');

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
                var ele = document.getElementById('<%=fuel_btncancel.ClientID%>');

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

        function onCalendarShown() {

            var cal = $find("calendar1");
            //Setting the default mode to month
            cal._switchMode("months", true);

            //Iterate every month Item and attach click event to it
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }

        function onCalendarHidden() {
            var cal = $find("calendar1");
            //Iterate every month Item and remove click event from it
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }

        }
        function call(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("calendar1");
                    cal._visibleDate = target.date;
                    cal.set_selectedDate(target.date);
                    cal._switchMonth(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged();
                    break;
            }
        }


        var d = new Date();
        var monthArray = new Array();
        monthArray[0] = "January";
        monthArray[1] = "February";
        monthArray[2] = "March";
        monthArray[3] = "April";
        monthArray[4] = "May";
        monthArray[5] = "June";
        monthArray[6] = "July";
        monthArray[7] = "August";
        monthArray[8] = "September";
        monthArray[9] = "October";
        monthArray[10] = "November";
        monthArray[11] = "December";
        for (m = 0; m <= 11; m++) {
            var optn = document.createElement("OPTION");
            optn.text = monthArray[m];
            // server side month start from one
            optn.value = (m + 1);

            // if june selected
            if (m == 5) {
                optn.selected = true;
            }

            document.getElementById('txt_parkwash_month').options.add(optn);
        }
        function DownloadFile(file) {
            // alert(file);
            var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;
            var localFilefolder = document.getElementById("<%=hdnRemid.ClientID%>").value;
                    //alert(localFilePath);
            window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + localFilefolder + "/" + file);
        }

		function CheckDateEalier(sender, args) {
            var toDate= new Date();
			var CurrentYear=toDate.getFullYear();
			var PreviousYear = sender._selectedDate.getFullYear();
            /*if (PreviousYear != CurrentYear)
            {
                alert("You can claim Fuel Bills only for the Current Year!");
                sender._selectedDate = "";
                //set the date back to the current date
				sender._textbox.set_Value("");
            }*/
		}
		function CheckDateOutstation(sender, args) {
            var toDate= new Date();
			var CurrentYear=toDate.getFullYear();
			var PreviousYear = sender._selectedDate.getFullYear();
            /*if ( PreviousYear != CurrentYear) {
                alert("You can claim Outstation Details only for the Current Year!");
                sender._selectedDate = "";
                //set the date back to the current date
				sender._textbox.set_Value("");
            }*/
		}
		function CheckDateWashing(sender, args) {
            var toDate= new Date();
			var CurrentYear=toDate.getFullYear();
			var PreviousYear = sender._selectedDate.getFullYear();
            /*if ( PreviousYear != CurrentYear) {
                alert("You can claim Parking / Washing & Toll Charges only for the Current Year!");
                sender._selectedDate = "";
                //set the date back to the current date
				sender._textbox.set_Value("");
            }*/
        }
    </script>
</asp:Content>


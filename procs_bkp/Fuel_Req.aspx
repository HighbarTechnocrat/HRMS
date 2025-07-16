<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="Fuel_Req.aspx.cs" Inherits="Fuel_Req" MaintainScrollPositionOnPostback="true" %>

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
                        <li class="claimfuel_Reimbursement">
                            <span>Rate</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtRate" ReadOnly="true" Enabled="False" runat="server" MaxLength="100"></asp:TextBox>
                         </li>
                        <li class="claimfuel_Amount">
                            <span>Quantity (Ltrs.)</span>&nbsp;&nbsp;<span style="color:red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtQuantity" runat="server" AutoPostBack="false" OnTextChanged="txtElgAmnt_TextChanged"   MaxLength="10"></asp:TextBox>
                        </li>
                        <li class="claimfuel_ElgAmount">
                            <span>Amount </span>&nbsp;&nbsp;<span style="color:red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtElgAmnt" runat="server" OnTextChanged="txtElgAmnt_TextChanged" AutoPostBack="true" MaxLength="10"></asp:TextBox>
                        </li>

                        <li class="claimfuel_Reimbursement" style="display:none;">
                             <%--<span>Monthly Eligible Qty (Ltrs.) </span>--%>
                            <asp:TextBox AutoComplete="off" ID="txtEligibleQty_Monthly" runat="server" Visible="false"  ReadOnly="true" Enabled="False" MaxLength="100" ></asp:TextBox>
                        </li>

                        <li class="claimfuel_Reason" style="display:none;">
                            <%--<span>Yearly Eligible Qty (Ltrs.) </span>--%>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtEligibleQty" runat="server" Visible="false" ReadOnly="true" Enabled="False" MaxLength="100"></asp:TextBox>
                        </li>
                        <li class="claimfuel_Reason" style="display:none;">
                            <%--<span>Yearly Balance Qty (Ltrs.)</span>--%>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtBalanceQty" runat="server" Visible="true" ReadOnly="true" Enabled="False" MaxLength="100"></asp:TextBox>
                        </li>
                        <li class="claimfuel_Reason" style="display:none;">
                         <%--   <span>Toll Charges </span>--%>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtTollCharges" runat="server" MaxLength="10" Visible="false"></asp:TextBox>
                        </li>

                        <li class="fuel_detail">                            
                            <asp:LinkButton ID="btnfuel_Details" runat="server" Text="Add Fuel Bills" ToolTip="Add Fuel Bills" Font-Bold="false" CssClass="Savebtnsve" OnClick="btnfuel_Details_Click"></asp:LinkButton>
                            <%--<span>Claim Details</span>--%>
                        </li>

                        <li class="fuel_grid">

                            <div>

                                <asp:GridView ID="dgFuelClaim" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="70%"
                                     DataKeyNames="Rem_id,FuelClaims_id" OnRowCreated="dgFuelClaim_RowCreated" >
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

                                        <asp:BoundField HeaderText="Quantity"
                                            DataField="Quantity"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="15%" />

                                        <asp:BoundField HeaderText="Amount"
                                            DataField="Amount"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="15%" />

                                        <asp:BoundField HeaderText="Rate"
                                            DataField="Rate"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="15%" />

                                         <asp:BoundField HeaderText="Toll Charges"
                                            DataField="TollCharges"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="1%" Visible="false" />

                                        <asp:BoundField HeaderText="COS Approved Amount"
                                            DataField="cosAppvrdAmt"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="15%" />

                                        <asp:BoundField HeaderText="COS Rate"
                                            DataField="cosRate"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="15%" />

                                            <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                            <%--<asp:LinkButton ID="lnkEdit" runat="server" Text='View' OnClick="lnkEdit_Click1"></asp:LinkButton>--%>
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

                        <li class="fuel_detail" >
                            <br />
                            <span style="font-size:12pt;font-weight:bold; text-decoration-line:underline;">Add Outstation Details</span>
<%--                            <asp:LinkButton ID="lnk_out_drop" runat="server" Text="Add Outstation Details" CssClass="Savebtnsve" ToolTip="Browse" ></asp:LinkButton>
                            <asp:ImageButton id="img_out_drop" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/arrowdown.png" />--%>
                        </li>
                        
                        <li class="claimfuel_Amount">

                            <asp:TextBox AutoComplete="off" ID="TextBox4" Visible="false" runat="server"  MaxLength="50"></asp:TextBox>
                        </li>
                        <li class="claimfuel_fromdate">
                            <span>Date </span>&nbsp;&nbsp;<span style="color:red">*</span>
                            <asp:TextBox AutoComplete="off" ID="txtFromdateOut" runat="server" AutoPostBack="True"  OnTextChanged="txtFromdateOut_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" Format="dd/MM/yyyy" TargetControlID="txtFromdateOut" OnClientDateSelectionChanged="CheckDateOutstation"
                                runat="server"> 
                            </ajaxToolkit:CalendarExtender>
                        </li>
                        <li class="claimfuel_Amount">
                            <span>Place </span>&nbsp;&nbsp;<span style="color:red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtPlace" runat="server"  MaxLength="50"></asp:TextBox>
                        </li>
                        <li class="claimfuel_ElgAmount">
                            <span> Kilometers Travelled </span>&nbsp;&nbsp;<span style="color:red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtEnd_Km" runat="server" MaxLength="10"></asp:TextBox>
                            
                        </li>
                        <li class="claimfuel_Reimbursement">
                            <!--<span>End Kilometers </span>-->
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtStart_Km" runat="server" AutoPostBack="false" Visible="false" MaxLength="10"></asp:TextBox>
                        </li>
                        <li class="claimfuel_Reason">
                            <span>Car Operating Allowance </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtDrv_All" runat="server" MaxLength="10"></asp:TextBox>
                        </li>
                        <li class="claimfuel_Reason">
                            <span>Parking/Toll Charges </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtParking" runat="server" MaxLength="10"></asp:TextBox>
                        </li>

                        <li class="fuel_detail">                            
                            <asp:LinkButton ID="btnOut_Details" runat="server" Text="Add Outstation Details" ToolTip="Add Outstation Details" CssClass="Savebtnsve" OnClick="btnOut_Details_Click"></asp:LinkButton>
                            <%--<span>Outstation Details</span>--%>
                        </li>

                        
                        <li class="fuel_grid">

                            <div>

                                <asp:GridView ID="dgFuelOut" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="70%"
                                     DataKeyNames="Rem_id,FuelClaims_id" OnRowCreated="dgFuelOut_RowCreated" >
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
                                        
                                        <asp:BoundField HeaderText="Date"
                                            DataField="Rem_Month"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" />

                                        <asp:BoundField HeaderText="Place"
                                            DataField="Place"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="25%" />

                                        <asp:BoundField HeaderText="Start KM"
                                            DataField="Start_km"
                                            ItemStyle-HorizontalAlign="center"
                                            Visible="false"
                                            ItemStyle-Width="1%" />

                                        <asp:BoundField HeaderText="Kilometers"
                                            DataField="End_km"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="10%" />

                                        <asp:BoundField HeaderText="Fuel Consumed"
                                            DataField="Fuel_Qty"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="15%" />

                                         <asp:BoundField HeaderText="Other Charges"
                                            DataField="otherCharges"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="25%" />

                                  
                                        <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                            <%--<asp:LinkButton ID="lnkEdit" runat="server" Text='View' OnClick="lnkEdit_Click1"></asp:LinkButton>--%>
                                            <asp:ImageButton id="lnkEdit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkEdit_Click2"/>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                            <asp:ImageButton id="btn_del" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/delete.png" OnClick="Del_Outstation"/>
                                            </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                            </div>

                        </li>

                           
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
                            

                              <div id="div1" runat="server" style="display:none">                        
                                <span>Parking / Washing Charges </span>
                                    <asp:TextBox AutoComplete="off" ID="txt_parkwash_claimed"  Text="0.00"   runat="server" MaxLength="10" OnTextChanged="txt_parkwash_claimed_TextChanged" AutoPostBack="true"></asp:TextBox>
                              </div>
                        </li>

                        <li class="fuel_grid">

                            <div>
                                <asp:GridView ID="dgSummary" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" AutoGenerateColumns="false" CellPadding="3"  Width="70%">
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
                                        <asp:BoundField HeaderText="Fuel & Other Expenses"
                                            DataField="COL1"
                                            ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-Width="35%" />

                                        <asp:BoundField HeaderText="Amount (INR)"
                                            DataField="COL2"
                                            HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right"
                                            ItemStyle-Width="15%" />

                                        <asp:BoundField HeaderText="Fuel Quantity Summary"
                                            DataField="COL3"
                                            HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right"
                                            ItemStyle-Width="35%" />

                                        <asp:BoundField HeaderText="Quantity (Litrs)"
                                            DataField="COL4"
                                            HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right"
                                            ItemStyle-Width="15%" />

                                    </Columns>
                                    
                                </asp:GridView>
                            </div>
                        </li>
                        

                        <li class="fuel_claimed" style="display:none">
                            <span> Parking / Washing Allowance </span>
                            <asp:TextBox AutoComplete="off" ID="txt_parkwash_elg" Text="0.00" runat="server" MaxLength="10" ReadOnly="true" Enabled="false"></asp:TextBox>

                        </li>
                           
                        <li class="fuel_claimed" style="display:none">                            
                            <span> Yearly Eligibility (Ltrs.)</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtEligible" Text="0.00" runat="server" MaxLength="100"  ReadOnly="true" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="fuel_claimed" style="display:none">
                            <span>Fuel Charges</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_OnlyFuel_Charges" Text="0.00" runat="server"  ReadOnly="false" Enabled="false" MaxLength="100"></asp:TextBox>
                        </li>
                        <li class="fuel_Amount" style="display:none">
                            <span>Quantity Claimed for the Month (Ltrs.) </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtQuantityClaim" Text="0.00" runat="server" MaxLength="100"></asp:TextBox>
                        </li>
                        <li class="fuel_outstation" style="display:none">
                            <span> Outstation Expenses</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtouttrvl" Visible="false" runat="server" MaxLength="100" AutoPostBack="true" Enabled="false" OnTextChanged="txtouttrvl_TextChanged"></asp:TextBox>
                            <asp:TextBox AutoComplete="off" ID="Txt_OutstationExp" Text="0.00" runat="server" MaxLength="100" AutoPostBack="true" Enabled="false" ></asp:TextBox>
                        </li>
                        <li class="mobile_quantity" style="display:none">
                            <span> Outstation Quantity for the Month (Ltrs.)</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtOutQty" Text="0.00" runat="server" MaxLength="100"></asp:TextBox>
                        </li>
                        <li class="fuel_eligibility" style="display:none">
                            <span> Toll Pass </span>
                            <asp:TextBox AutoComplete="off" ID="txtTollChargesMainold" Text="0.00" Enabled="false" runat="server"  MaxLength="10" ></asp:TextBox>
                        </li>
                        <li class="fuel_total" style="display:none">
                            <span> Actual Quantity (except Outstation) </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Text_Actual_Qty" Text="0.00" Enabled="false" runat="server"  MaxLength="10" AutoPostBack="true"></asp:TextBox>
                            <asp:TextBox AutoComplete="off" ID="txttotalElig" Visible="false" runat="server" ReadOnly="false" Enabled="false" MaxLength="100"></asp:TextBox>
                        </li>

                        <li class="fuel_bal" style="display:none">
                            <span>Amount Claimed for the Month </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtAmount" Text="0.00" runat="server" MaxLength="100"></asp:TextBox>
                        </li>
                         <li class="fuel_claimed" style="display:none">
                            <span>Quantity Claimed till date (Ltrs.)</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtTotalClaimTillDt" Text="0.00" runat="server"  ReadOnly="false" Enabled="false" MaxLength="100"></asp:TextBox>
                        </li>
                        <li class="fuel_bal" style="display:none">
                            <%--<span> Balance Eligibility (Ltrs.) </span>--%>
                            <br />
                            <asp:TextBox AutoComplete="off" Visible="false" ID="TextBox3" runat="server" MaxLength="100"></asp:TextBox>
                        </li>
                         <li class="fuel_claimed" style="display:none">
                            <span> Balance Eligibility (Ltrs.) </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtBalElig" Text="0.00" runat="server" MaxLength="100"></asp:TextBox>
                        </li>
                          <li class="fuel_upload">
                            <span>Upload File</span><br />
                            <asp:FileUpload ID="uploadfile" runat="server"  AllowMultiple="true"/>
                            <asp:TextBox AutoComplete="off" ID="txtprofile" runat="server" CssClass="mobilenotextbox popup" Visible="false"> </asp:TextBox>
                              <asp:LinkButton ID="lnkuplodedfile" OnClick="lnkuplodedfile_Click" runat="server" Visible="false"></asp:LinkButton>
                            <%--<span class="texticonselect tooltip" title="Please provide your profile picture."><i class="fa fa-file-image-o"></i></span>--%>

                              <asp:GridView ID="gvfuel_claimsFiles" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="70%"
                                     DataKeyNames="t_id,file_sr_no" OnRowCreated="gvfuel_claimsFiles_RowCreated" >
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
       <asp:LinkButton ID="fuel_btncancel" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="Savebtnsve" OnClientClick="return CancelMultiClick();" OnClick="fuel_btncancel_Click">Cancel</asp:LinkButton>
        <asp:LinkButton ID="fuel_btnBack" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/MyFuel_Req.aspx" >Back</asp:LinkButton>
        <asp:LinkButton ID="mobile_btnPrintPV" runat="server" Text="Print Payment Voucher" ToolTip="Print Payment Voucher" CssClass="Savebtnsve" OnClick="mobile_btnPrintPV_Click">Print Payment Voucher</asp:LinkButton>
    </div>

       
        <%-- Following Popup for Fuel Mobile Rem Requestt --%>
<%--        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" DisplayModalPopupID="mpe_Cancel" TargetControlID="fuel_btncancel">
        </ajaxToolkit:ConfirmButtonExtender>
        <ajaxToolkit:ModalPopupExtender ID="mpe_Cancel" runat="server" PopupControlID="pnlPopup_Cancel" TargetControlID="fuel_btncancel" OkControlID = "btnYes_CLR"
            CancelControlID="btnNo_CLR" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup_Cancel" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Confirmation
            </div>
            <div class="body">
                Do you want to Cancel Fuel Reimbursement Request ?
            </div>
            <div class="footer" align="right">                                
                <asp:Button ID="btnNo_CLR" runat="server" Text="No" />
                <asp:Button ID="btnYes_CLR" runat="server" Text="Yes" />                
            </div>
        </asp:Panel>--%>
        <%-- End Here --%>

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

    <asp:HiddenField ID="hdnDesk" runat="server" />

    <asp:HiddenField ID="hdnDestnation" runat="server" />

    <asp:HiddenField ID="hdnRemid" runat="server" />

    <asp:HiddenField ID="hdnClaimsID" runat="server" />

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
    <asp:HiddenField ID="hdnFuelReimbursementType" runat="server" />

    <asp:HiddenField ID="hdnApprovalCOS_Code" runat="server" />
    <asp:HiddenField ID="hdnApprovalCOS_ID" runat="server" />
    <asp:HiddenField ID="hdnApprovalCOS_mail" runat="server" />
    <asp:HiddenField ID="hdnApprovalCOS_Name" runat="server" />
     
    <asp:HiddenField ID="hdnMobRemStatusM" runat="server" />
    <asp:HiddenField ID="hdnMobRemStatus_dtls" runat="server" /> 
    <asp:HiddenField ID="hdnClaimDate" runat="server" /> 
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="hdnfrmdate_emial" runat="server" />
    <asp:HiddenField ID="hdntodate_emial" runat="server" />
    <asp:HiddenField ID="hdnIs_GFHCCEOGFO" runat="server" />

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

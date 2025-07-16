<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="Fuel_Req_App.aspx.cs" Inherits="Fuel_Req_App" %>

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
                        <asp:Label ID="lblheading" runat="server" Text="Approve Fuel Claim"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="False" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
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
                            <span>Employee Code</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="True" Enabled="false" > </asp:TextBox>

                        </li>                        
                        <li class="fuel_InboxEmpName">
                            <span>Employee Name</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtEmpName" runat="server" Visible="True" Enabled="false"></asp:TextBox>

                        </li>
                        <li class="fuel_date">
                            <span>Submission On </span>
                            <br />

                            <asp:TextBox AutoComplete="off" ID="txtFromdate" runat="server" AutoPostBack="True" Enabled="false"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtFromdate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>
                           <li class="fuel_claimed">
                             <span>Fuel Type: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtFuelType" runat="server" MaxLength="100" Enabled="false" ></asp:TextBox>
                        </li>
                        <li class="fuel_inboxEmpCode">
                            <span>Claim Month</span><br />
                            <asp:TextBox AutoComplete="off" ID="Txt_ClaimMonth" runat="server" MaxLength="50" Visible="True" Enabled="false" > </asp:TextBox>

                        </li>                        
                        <li class="fuel_InboxEmpName">
                            <%--<span>Employee Name</span><br />--%>
                            <asp:TextBox AutoComplete="off" ID="TextBox5" runat="server" Visible="false" ></asp:TextBox>

                        </li>
                        <li class="fuel_detail">
                            <span style="font-size:12pt;font-weight:bold; text-decoration-line:underline;" id="FuelBills_Heading" runat="server">Fuel Bills: </span>
                            <%--<asp:LinkButton ID="btnfuel_Details" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" OnClick="btnfuel_Details_Click" Enabled="false" Visible="false"></asp:LinkButton>--%>
                        </li>

                        <li class="fuel_grid">
                            <div>

                                <asp:GridView ID="dgFuelClaim" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="70%"
                                     DataKeyNames="Rem_id,FuelClaims_id" OnRowCreated="dgFuelClaim_RowCreated">
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
                                            ItemStyle-Width="20%" Visible="false"/>

                                         <asp:BoundField HeaderText="COS Approved Amount"
                                            DataField="cosAppvrdAmt"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="20%" />

                                        <asp:BoundField HeaderText="COS Rate"
                                            DataField="cosRate"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="20%" />
                                    </Columns>
                                </asp:GridView>

                            </div>
                        </li>

                          <li class="fuel_detail">
                              <br />
                            <span style="font-size:12pt;font-weight:bold; text-decoration-line:underline;" id="OutBills_Heading" runat="server">Outstations Details: </span>

                            <%--<asp:LinkButton ID="btnfuel_Details_outstn" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve"  Enabled="false" Visible="false"></asp:LinkButton>--%>
                        </li>

                        <li class="fuel_grid">
                            <div>
                                <asp:GridView ID="dgFuelOut" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="70%"
                                     DataKeyNames="Rem_id,FuelClaims_id" OnRowCreated="dgFuelOut_RowCreated">
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
                                            ItemStyle-Width="20%" />

                                        <asp:BoundField HeaderText="Place"
                                            DataField="Place"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="20%" />

                                        <asp:BoundField HeaderText="Start KM"
                                            DataField="Start_km"
                                            ItemStyle-HorizontalAlign="center"
                                            Visible="false"
                                            ItemStyle-Width="15%" />

                                        <asp:BoundField HeaderText="KMs Travelled"
                                            DataField="End_km"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="15%" />

                                        <asp:BoundField HeaderText="Fuel Consumed"
                                            DataField="Fuel_Qty"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="20%" />

                                         <asp:BoundField HeaderText="Other Charges"
                                            DataField="otherCharges"
                                            ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="20%" />

                                    </Columns>
                                </asp:GridView>

                            </div>
                        </li>
                        
                        <li class="fuel_detail">
                            <br />
                            <span style="font-size:12pt;font-weight:bold; text-decoration-line:underline;">Claim Summary:</span>
                            <%--<asp:LinkButton ID="LinkButton1" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve"  Enabled="false" Visible="false"></asp:LinkButton>--%>
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

                        <li class="fuel_claimed" style="display:none;">
                            <span> Toll Charges </span>
                            <asp:TextBox AutoComplete="off" ID="txtTollCharges" runat="server" MaxLength="10"  Enabled="false"></asp:TextBox>
                        </li>
                        <li class="fuel_claimed" style="display:none;">
                            <div id="div1" runat="server" style="display:none">                       
                              <span> Toll Parking / Washing Charges </span>
                            <asp:TextBox AutoComplete="off" ID="txt_parkwash_claimed" runat="server" MaxLength="10"  Enabled="false"></asp:TextBox>
                                 </div>
                        </li>
                           
                        <li class="fuel_claimed" style="display:none;">
                            <span> Airport Parking(If Any) </span>
                            <asp:TextBox AutoComplete="off" ID="txt_airport_parking" runat="server" MaxLength="10"  Enabled="false"></asp:TextBox>
                          
                        </li>
                        <li class="fuel_claimed" style="display:none;">
                            <span> Toll Parking / Washing Eligibility </span>
                            <asp:TextBox AutoComplete="off" ID="txt_parkwash_elg" runat="server" MaxLength="10" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="fuel_claimed" style="display:none;">
                            <span>Quantity Claimed for the Month (Ltrs.) </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtQuantity" runat="server" MaxLength="100" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="fuel_Amount" style="display:none;">
                            <span>Amount Claimed for the Month </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtAmount" runat="server" MaxLength="100" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="fuel_outstation" style="display:none;">
                            <span> Outstation Travel for the Month (Km.)</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtouttrvl" runat="server" MaxLength="100" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="mobile_quantity" style="display:none;">
                            <span> Outstation Quantity for the Month (Ltrs.)</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtOutQty" runat="server" MaxLength="100" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="fuel_eligibility" style="display:none;">
                            <span>Yearly Eligibility (Ltrs.)</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtEligible" runat="server" MaxLength="100" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="fuel_total" style="display:none;">
                        <%--    <span> Total Eligibility </span>--%>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txttotalElig" Visible="false" runat="server" MaxLength="100" Enabled="false"></asp:TextBox>
                        </li>
                          <li class="fuel_bal" style="display:none;">
                            <span>Total Qty Claimed till date (Ltrs.)</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtTotalClaimTillDt" runat="server"  ReadOnly="false" Enabled="false" MaxLength="100"></asp:TextBox>
                        </li>
                        <li class="fuel_bal" style="display:none;">
                            <span> Balance Eligibility (Ltrs.) </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtBalElig" runat="server" MaxLength="100" Enabled="false"></asp:TextBox>
                        </li>

                        <li class="fuel_CosAmount">
                           <%-- <span>COS Approved Amount </span>--%>
                            <asp:Label ID="lblcostappamt" runat="server"  Text="COS Approved Amount"></asp:Label>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtCosApprovedAmt" runat="server"  ReadOnly="false" Enabled="false" MaxLength="100" ></asp:TextBox>
                        </li>
                        <li class="fuel_CosAmount">
                           
                            <br />
                            <asp:TextBox AutoComplete="off" ID="TextBox4" runat="server" MaxLength="100" Enabled="false" Visible="false"></asp:TextBox>
                        </li>

                        <li class="fuel_claimed" style="display:none;">
                            <br />
                            <asp:TextBox AutoComplete="off" ID="TextBox16" runat="server" MaxLength="100" Visible="false"></asp:TextBox>
                        </li>

                         <li class="fuel_claimed" style="display:none;">
                            <br />
                            <asp:TextBox AutoComplete="off" ID="TextBox3" runat="server" MaxLength="100" Visible="false"></asp:TextBox>
                        </li>


                          <li class="fuel_upload">
                            <span>Uploaded File</span><br />
                            <asp:FileUpload ID="uploadfile" runat="server" Visible="false" Enabled="false"/>
                              <asp:LinkButton ID="lnkuplodedfile" OnClick="lnkuplodedfile_Click" runat="server" Visible="false"></asp:LinkButton>
                            <asp:TextBox AutoComplete="off" ID="txtprofile" runat="server" CssClass="mobilenotextbox popup" Visible="false"> </asp:TextBox>
                            <%--<span class="texticonselect tooltip" title="Please provide your profile picture."><i class="fa fa-file-image-o"></i></span>--%>
                              
                              <asp:GridView ID="gvfuel_claimsFiles" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="70%"
                                     DataKeyNames="t_id" >
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
                                        <asp:LinkButton ID="lnkViewFiles" runat="server" Text='View' OnClientClick=<%# "DownloadFile('" + Eval("file_name") + "')" %> >
                                        </asp:LinkButton>
                                        </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                   

                                    </Columns>
                                </asp:GridView>


                        </li>
                           <li class="fuel_claimed">
                            <br />
                            <asp:TextBox AutoComplete="off" ID="TextBox17" runat="server" MaxLength="100" Visible="false"></asp:TextBox>
                        </li>
                         <li class="fuel_bal">
                            <span> Comment </span>&nbsp;&nbsp;<span style="color:red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtComment" runat="server" MaxLength="100" Enabled="True"></asp:TextBox>

                        </li>
                         <li class="fuel_claimed">
                            <br />
                            <asp:TextBox AutoComplete="off" ID="TextBox1" runat="server" MaxLength="100" Visible="false"></asp:TextBox>
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
        <asp:LinkButton ID="fuel_btnSave" runat="server" Text="Approve" ToolTip="Approve" CssClass="Savebtnsve" OnClick="fuel_btnSave_Click" OnClientClick="return SaveMultiClick();">Approve</asp:LinkButton>
        <asp:LinkButton ID="fuel_btnSave_ACC" runat="server" Text="Submit" Visible="false" ToolTip="Submit" CssClass="Savebtnsve" OnClick="fuel_btnSave_Click" OnClientClick="return SaveMultiClick_ACC();">Submit</asp:LinkButton>
          <asp:LinkButton ID="fuel_btnReject" runat="server" Text="Reject" ToolTip="Save" Visible="false" CssClass="Savebtnsve" OnClick="fuel_btnReject_Click" OnClientClick="return CancelMultiClick();">Reject</asp:LinkButton>
          
        
          <asp:LinkButton ID="fuel_btnCorrection" runat="server" Text="Send For Correction" ToolTip="Send For Correction" CssClass="Savebtnsve"  OnClientClick="return SendforCorrectionMultiClick();" OnClick="fuel_btnCorrection_Click" >Send For Correction</asp:LinkButton>

          <asp:LinkButton ID="fuel_btnBack" runat="server" Text="Back" ToolTip="Save" CssClass="Savebtnsve" OnClick="fuel_btnBack_Click">Back</asp:LinkButton>
       <asp:LinkButton ID="mobile_btnPrintPV" runat="server" Text="Print Payment Voucher" ToolTip="Print Payment Voucher" CssClass="Savebtnsve" OnClick="mobile_btnPrintPV_Click">Print Payment Voucher</asp:LinkButton>
    </div>

  

    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />

    <asp:HiddenField ID="FilePath" runat="server" />

    <asp:HiddenField ID="hflempName" runat="server" />

        <asp:HiddenField ID="hflEmpDesignation" runat="server" />

    <asp:HiddenField ID="hflEmpDepartment" runat="server" />

    <asp:HiddenField ID="hflEmailAddress" runat="server" />

    <asp:HiddenField ID="hflGrade" runat="server" />

    <asp:HiddenField ID="hflapprcode" runat="server" />

    <asp:HiddenField ID="hdnempcode" runat="server" />

    <asp:HiddenField ID="hdnDestnation" runat="server" />

    <asp:HiddenField ID="hdnRemid" runat="server" />

    <asp:HiddenField ID="hdnClaimsID" runat="server" />

    <asp:HiddenField ID="hdnCurrentApprID" runat="server" />

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

        <asp:HiddenField ID="hdnReqEmailaddress" runat="server" />
    <asp:HiddenField ID="hdnFuelReimbursementType" runat="server" />
    <asp:HiddenField ID="hdnApprovalCOS_ID" runat="server" />    
    <asp:HiddenField ID="hdnApprovalTD_Code" runat="server" />
    <asp:HiddenField ID="hdnApprovalCOS_Code" runat="server" />
    <asp:HiddenField ID="hdnApproverTDCOS_status" runat="server" />

    <asp:HiddenField ID="hdnisBookthrugh_TD" runat="server" />
    <asp:HiddenField ID="hdnisBookthrugh_COS" runat="server" />
    <asp:HiddenField ID="hdnisApprover_TDCOS" runat="server" />
      <asp:HiddenField ID="hdnNextApprId" runat="server" />

    <asp:HiddenField ID="HiddenField2" runat="server" />
    <asp:HiddenField ID="hdnIntermediateEmail" runat="server" />
     <asp:HiddenField ID="hdnstaus" runat="server" />
    <asp:HiddenField ID="HiddenField1" runat="server" />

        <asp:HiddenField ID="hdnNextApprCode" runat="server" />

    <asp:HiddenField ID="hdnNextApprName" runat="server" />

    <asp:HiddenField ID="hdnNextApprEmail" runat="server" />

    <asp:HiddenField ID="hdnInboxType" runat="server" />
    <asp:HiddenField ID="hdnloginemployee_name" runat="server" />

    <asp:HiddenField ID="hdnApprovalACCHOD_Code" runat="server" />
    <asp:HiddenField ID="hdnApprovalACCHOD_mail" runat="server" />
    <asp:HiddenField ID="hdnApprovalACCHOD_ID" runat="server" />
    <asp:HiddenField ID="hdnApprovalACCHOD_Name" runat="server" />    
    <asp:HiddenField ID="hdnfrmdate_emial" runat="server" />    
    <asp:HiddenField ID="hdnYesNo" runat="server" />    
      
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

        function SaveMultiClick_ACC() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=fuel_btnSave_ACC.ClientID%>');

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


        function SendforCorrectionMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=fuel_btnCorrection.ClientID%>');

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
            if (confirm("Do you want to Send for Correction ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            //alert(confirm_value.value);
            //document.forms[0].appendChild(confirm_value);
            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return;

        }


        function CancelMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=fuel_btnReject.ClientID%>');

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

		function ViewColor(evt) {
            evt.style.color = "#ff0000";
            
        }
		
		function DownloadFile(file) {
		    // alert(file);
		    var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;
		    var localFilefolder = document.getElementById("<%=hdnRemid.ClientID%>").value;
                //alert(localFilePath);
		    window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + localFilefolder + "/" + file);
        }
    </script>
</asp:Content>

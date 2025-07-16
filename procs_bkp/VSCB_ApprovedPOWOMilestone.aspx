<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" EnableViewState="true"
    MaintainScrollPositionOnPostback="true" CodeFile="VSCB_ApprovedPOWOMilestone.aspx.cs" Inherits="VSCB_ApprovedPOWOMilestone" EnableSessionState="True" ValidateRequest="false" %>

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

        .textboxBackColor {
            background-color: cadetblue;
            color: aliceblue;
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

          .boxTest
            {        
                BORDER-RIGHT: black 1px solid;
                BORDER-TOP: black 1px solid;
                BORDER-LEFT: black 1px solid;
                BORDER-BOTTOM: black 1px solid;
                BACKGROUND-COLOR: White;
            }
        .AmtTextAlign
        {
            text-align:right
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
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
                        <asp:Label ID="lblheading" runat="server" Text="Approved PO/ WO Milestones"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>
                <div>
                    <span>
                          <a href="vscb_index.aspx" class="aaaa"><%=System.Configuration.ConfigurationManager.AppSettings["VSCBPageTitle"]%></a>
                    </span>
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
                            <span>PO/ WO Number</span>&nbsp;&nbsp;<span style="color: red">*</span><br />                           
                             <asp:TextBox AutoComplete="off" ID="txtTriptype" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox> 
                          
                        </li>
                        <li class="trvl_date">
                            <span>PO/ WO Type</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                            <asp:DropDownList runat="server" ID="lstPOType" AutoPostBack="true" CssClass="DropdownListSearch">
                            </asp:DropDownList>
                            <asp:TextBox AutoComplete="off" ID="txtPOtype" runat="server" CssClass="Dropdown" Visible="false"  ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>PO/ WO Date </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtFromdate" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>


                        <li class="trvl_date">
                             <span>Vendor Name</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtVendor" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>

                            <span style="display:none">PO/ WO Title</span> 
                            <asp:TextBox AutoComplete="off" ID="txtPOTitle" runat="server" ReadOnly="true" Enabled="False" Visible="false"></asp:TextBox>

                        </li>
                        <li class="trvl_date">
                              <span>GSTIN No.</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtGSTIN_No" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                               <span id="spnPOWOStatus" runat="server" visible="false">PO/ WO Payment Status</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPOStatus" Visible="false" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>



                        <li class="trvl_date">
                             <span>Cost Center</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtCostCenter" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                             
                            <asp:TextBox AutoComplete="off" ID="txtProject" runat="server" ReadOnly="true" Enabled="False" Visible="false"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>Currency </span><br />
                            <asp:TextBox AutoComplete="off" ID="txtCurrency" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date">

                            <span>PO/ WO Amount (Without GST)</span><br /> 
                             <asp:TextBox AutoComplete="off" ID="txtBasePOWOWAmt" runat="server" ReadOnly="true" Enabled="False" CssClass="AmtTextAlign"></asp:TextBox>

                            
                        </li>


                        <li class="trvl_date">
                              <span id="spnAmountWithTax" runat="server" visible="false">PO/ WO Amount (With GST) (A)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPOWOAmt" Visible="false" runat="server" ReadOnly="true" Enabled="False" CssClass="textboxBackColor AmtTextAlign"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                             <span id="spnPOWODirectTaxAmt" runat="server" visible="false">Direct Tax Amount (B)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtDirectTaxAmt" runat="server" ReadOnly="true" Enabled="False" Visible="false" CssClass="textboxBackColor AmtTextAlign"></asp:TextBox>

                        </li>
                        <li class="trvl_date">
                               <span id="spnPOWOPaidAmt" runat="server" visible="false">PO/ WO Paid Amount (C)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPOWOPaidAmt" runat="server" ReadOnly="true" Enabled="False" Visible="false" CssClass="textboxBackColor AmtTextAlign"></asp:TextBox>
                             <asp:TextBox AutoComplete="off" ID="txtPoPaidAmt_WithOutDT" runat="server" ReadOnly="true" Enabled="False" Visible="false" CssClass="textboxBackColor AmtTextAlign"></asp:TextBox>
                        </li>

                        <li class="trvl_date">
                            <span id="spnPOWOSettelmentAmt" runat="server" visible="false"> Settlement Amount (D)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPOWOSettelmentAmt" runat="server" ReadOnly="true" Enabled="False" Visible="false" CssClass="textboxBackColor AmtTextAlign"></asp:TextBox>

                        </li>
                        <li class="trvl_date">
                             
                              <span id="spnPOWOBalAmt" runat="server" visible="false">PO/ WO Balance Amount(A-B-C-D)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPOWOBalanceAmt" runat="server" ReadOnly="true" Enabled="False" Visible="false" CssClass="textboxBackColor AmtTextAlign"></asp:TextBox>

                        </li>
                        <li class="trvl_date">
                             <span id="spnPOWODShortClosedAmt" runat="server" visible="false">PO/ WO Short Closed Amount</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPOWOShortClosedAmt" runat="server" ReadOnly="true" Enabled="False" Visible="false" CssClass="textboxBackColor AmtTextAlign"></asp:TextBox>
                          </li>
 
                         <li class="trvl_date" id="liSettlement_1" runat="server">
                            <div class="manage_grid" style="overflow: hidden; width: 130%; margin-bottom: 4px;">
                                <span id="spnSettlemnt_N" style="font-size: 13px !important">Settlement = Discount/Correction/Settlement/Deduction </span>
                            </div>
                        </li>
                        <li class="trvl_date" id="liSettlement_2" runat="server"></li>
                        <li class="trvl_date" id="liSettlement_3" runat="server"></li>


                         <li class="trvl_type">
                            <span>PO / WO Title</span>&nbsp;&nbsp;<br />
                           <asp:TextBox AutoComplete="off" ID="txtPOWOTitle" runat="server"  Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date">                            
                           <div id="divScurity_Diposit" runat="server" visible="false">
                            <span>Security Deposit Amount</span>&nbsp;&nbsp;<br />
                            <asp:TextBox AutoComplete="off" ID="txtSecurity_DepositAmt" runat="server" Enabled="False" CssClass="AmtTextAlign"></asp:TextBox>
                            </div>             
                        </li>
                        <li class="trvl_date"></li>

                         <li class="trvl_date" id="liPOWOContent_1" runat="server" visible="false">                            
                            <span class="LableName">PO/ WO Content</span>
                            <br />
                             <div  class="boxTest" style="width:785px">
                               <div  class="POWOContentTextArea">
                                   <div style="width:770px">
                                     <span id="lblPOWO_Content"  runat="server"></span>
                                       </div>
                              </div>
                                </div>
                        </li>  

                        <li class="trvl_date" id="liPOWOContent_2" runat="server" visible="false"></li>
                        <li class="trvl_date" id="liPOWOContent_3" runat="server" visible="false"></li>

                        <li class="trvl_date" id="liViewPOWO_SignCopy_1" runat="server" visible="false">                            
                            <span class="LableName" id="spnViewSingCopy" runat="server" visible="false">View PO/ WO Content Sign Copy</span>
                             <br />
                            <asp:LinkButton ID="lnkViewPOWO_SignCopy" runat="server"  OnClientClick="Download_POSignCopyFile()" Visible="false" CssClass="BtnShow"></asp:LinkButton>
                            
                        </li>  

                       <li class="trvl_date" id="liViewPOWO_SignCopy_2" runat="server" visible="false"></li>
                        <li class="trvl_date" id="liViewPOWO_SignCopy_3" runat="server" visible="false"></li>
                         
                       
                         <li class="trvl_date"></li>
                        <li class="trvl_date"></li>
                         <li class="trvl_date"></li> 
                         


                        <li class="trvL_detail" id="litrvldetail" runat="server">
                            <asp:Label runat="server" ID="lblMilestoneMsg" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>                         
                            <span id="spntrvldtls" runat="server" class="LableName">Milestones </span>
                        </li> 
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>

                        <div id="DivTrvl" class="edit-contact" runat="server" visible="false">
                            <ul>
                                <li class="trvl_date">
                                    <span>Milestone Particulars</span>&nbsp;&nbsp;<span style="color: red">* <span id="spncharecter" runat="server" style="color: red; font-size: 10px; font-weight: normal; font-style: italic;">Maximum 200 Characters</span></span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtMilestone" runat="server" MaxLength="200" TextMode="MultiLine"></asp:TextBox>
                                </li>
                                <li class="trvl_date"></li>
                                <li class="trvl_date"></li>

                                <li class="trvl_date">
                                    <span>Quantity</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtQty" runat="server" MaxLength="5" OnTextChanged="txtRate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span>Rate</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtRate" runat="server" MaxLength="10" OnTextChanged="txtRate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span>Amount</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtAmount" runat="server" ReadOnly="true" Enabled="False" OnTextChanged="txtRate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </li>

                                <li class="trvl_date">
                                    <span>CGST(%)</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtCGSTPer" runat="server" AutoPostBack="true" MaxLength="5" OnTextChanged="txtCGSTPer_TextChanged"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span>SGST(%)</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtSGSTPer" runat="server" AutoPostBack="true" MaxLength="5" OnTextChanged="txtSGSTPer_TextChanged"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span>IGST(%)</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtIGSTPer" runat="server" AutoPostBack="true" MaxLength="5" OnTextChanged="txtIGSTPer_TextChanged"></asp:TextBox>
                                </li>


                                <li class="trvl_date">
                                    <span>CGST Amount</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtCGST_Amt" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span>SGST Amount</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtSGST_Amt" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span>IGST Amount</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtIGST_Amt" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                                </li>


                                <li class="trvl_date">
                                    <span>Amount (with GST)</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtAmtWithTax" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span id="spnMilestonePaidAmt" runat="server" visible="false">Milestone Paid Amount</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtMilestonePaidAmt" runat="server" ReadOnly="true" Enabled="False" Visible="false"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span id="spnMilestoneDirectTaxAmt" runat="server" visible="false">Milestone Direct Tax Amount</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtMilestoneDirectTaxAmt" runat="server" ReadOnly="true" Enabled="False" Visible="false"></asp:TextBox>
                                </li>

                                <li class="trvl_date">
                                    <span id="spnMilestoneBalAmt" runat="server" visible="false">Milestone Balance Amount</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtMilestoneBalanceAmt" runat="server" ReadOnly="true" Enabled="False" Visible="false"></asp:TextBox>
                                </li>
                                <li class="trvl_date"></li>
                                <li class="trvl_date"></li>



                                <li class="trvldetails_bookthrough"></li>

                                <li class="trvldetails_bookthrough"></li>


                                <li class="trvldetails_bookthrough"></li>
                            </ul>
                            <div>   
                                <asp:LinkButton ID="trvldeatils_cancel_btn" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="trvldeatils_cancel_btn_Click"></asp:LinkButton>
                            </div>
                        </div>


                        <li class="trvl_grid" id="litrvlgrid" runat="server">
                            <div class="manage_grid" style="overflow: scroll; width: 100%; padding-top: 10px; margin-bottom: 10px;">

                                <asp:GridView ID="dgTravelRequest" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                    DataKeyNames="srno,PaymentStatusID,IsShortClone" OnRowDataBound="dgTravelRequest_RowDataBound">
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

                                        <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkTravelDetailsEdit" runat="server" CssClass="BtnShow" Text='View' OnClick="lnkTravelDetailsEdit_Click">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="5%" />
                                        </asp:TemplateField>

                                        <asp:BoundField HeaderText="Milestone No"
                                            DataField="srno"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Mliestone Particular"
                                            DataField="MilestoneName"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Quantity"
                                            DataField="Quantity"
                                            ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Rate"
                                            DataField="Rate"
                                            ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Amount"
                                            DataField="Amount"
                                            ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="CGST Amount"
                                            DataField="CGST_Amt"
                                            ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="SGST Amount"
                                            DataField="SGST_Amt"
                                            ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="IGST Amount"
                                            DataField="IGST_Amt"
                                            ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Total Amount"
                                            DataField="AmtWithTax"
                                            ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Payment Status"
                                            DataField="PyamentStatus"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />


                                    </Columns>
                                </asp:GridView>

                            </div>
                        </li>


                        <li class="trvl_local" style="display:none">
                            <span style="display:none">Uploaded PO File</span>                            
                            <br />
                            <asp:LinkButton ID="lnkfile_PO" runat="server" OnClick="lnkfile_PO_Click" Visible="false" CssClass="BtnShow"></asp:LinkButton>
                        </li>
                        <li class="trvl_date" style="display:none"> </li>
                        <li class="trvl_date" style="display:none"></li>


                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>

                        <li class="trvl_local">
                            <span id="spnSupportingFiles" runat="server" visible="false">Uploaded Supporting Files</span>
                             
                            <asp:GridView ID="gvuploadedFiles" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                DataKeyNames="Srno" OnRowDataBound="gvuploadedFiles_RowDataBound">
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
                                    <asp:TemplateField HeaderText="Uploaded Files">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkviewfile" runat="server" OnClientClick=<%# "DownloadFile('" + Eval("filename") + "')" %> Text='<%# Eval("filename") %>'>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                </Columns>
                            </asp:GridView>
                        </li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>


                           <li class="trvl_Approver">
                            <asp:GridView ID="DgvApprover" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="75%">
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
                                        DataField="ApproverName"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="left"
                                        ItemStyle-Width="33%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Status"
                                        DataField="Status"
                                        ItemStyle-HorizontalAlign="left"
                                        ItemStyle-Width="12%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Approved on"
                                        DataField="approved_on"
                                        ItemStyle-HorizontalAlign="left"
                                        ItemStyle-Width="15%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Approver Remarks"
                                        DataField="Remarks"
                                        ItemStyle-HorizontalAlign="left"
                                        ItemStyle-Width="37%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="APPR_ID"
                                        DataField="Appr_ID"
                                        ItemStyle-HorizontalAlign="center"
                                        ItemStyle-Width="1%"
                                        ItemStyle-BorderColor="Navy"
                                        Visible="false" />

                                    <asp:BoundField HeaderText="Emp_Emailaddress"
                                        DataField="Emp_Emailaddress"
                                        ItemStyle-HorizontalAlign="center"
                                        ItemStyle-Width="1%"
                                        ItemStyle-BorderColor="Navy"
                                        Visible="false" />

                                    <asp:BoundField HeaderText="A_EMP_CODE"
                                        DataField="approver_emp_code"
                                        ItemStyle-HorizontalAlign="center"
                                        ItemStyle-Width="1%"
                                        ItemStyle-BorderColor="Navy"
                                        Visible="false" />
                                </Columns>
                            </asp:GridView>
                        </li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="trvl_Savebtndiv">
        <asp:LinkButton ID="btnback_mng" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/VSCB_MyapprovedPOWO.aspx">Back</asp:LinkButton>
    
        <asp:LinkButton ID="btnApprove"  runat="server" Text="View Draft Copy" ToolTip="View Draft Copy" Visible="false" CssClass="Savebtnsve" OnClick="btnApprove_Click" >View Draft Copy</asp:LinkButton>
        

    <asp:LinkButton ID="btnCorrection" Visible="false" runat="server" Text="Download PO/ WO" ToolTip="Download PO/ WO" CssClass="Savebtnsve" OnClientClick="Download_ApprovedPO()">Download PO/ WO</asp:LinkButton> <%--  OnClick="btnCorrection_Click" "--%>
        

    </div>

    <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
    <asp:TextBox AutoComplete="off" ID="txtEmpName" runat="server" Visible="false"></asp:TextBox>

    <asp:Label ID="testsanjay" runat="server" Visible="false"></asp:Label>
    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />


    <asp:HiddenField ID="FilePath" runat="server" />
    <asp:HiddenField ID="hdnSrno" runat="server" />
    <asp:HiddenField ID="hdnpamentStatusid" runat="server" />
    <asp:HiddenField ID="hdnIGSTAmt" runat="server" />
    <asp:HiddenField ID="hdnCompCode" runat="server" />
    <asp:HiddenField ID="hdnVendorId" runat="server" />
    <asp:HiddenField ID="hdnPrj_Dept_Id" runat="server" />
    <asp:HiddenField ID="hdnPOWOID" runat="server" />
    <asp:HiddenField ID="hdnstype_Main" runat="server" />

    <asp:HiddenField ID="hdnMstoneId" runat="server" />
    <asp:HiddenField ID="hdnPOTypeId" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="hdnIsShorClose" runat="server" />
    <asp:HiddenField ID="hdnfileid" runat="server" />
    <asp:HiddenField ID="hdnCondintionId" runat="server" />
    <asp:HiddenField ID="hdnRangeId" runat="server" />
    <asp:HiddenField ID="hdnAppr_StatusId" runat="server" />

    <asp:HiddenField ID="hdnApprovedPO_FileName" runat="server" />
    <asp:HiddenField ID="hdnApprovedPO_FilePath" runat="server" />

     <asp:HiddenField ID="hdnSingPOCopyFilePath" runat="server" /> 
     <asp:HiddenField ID="hdnSingPOCopyFileName" runat="server" />

    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <script type="text/javascript">

        $(document).ready(function () {
            $(".DropdownListSearch").select2();
             $("#MainContent_txtPOWO_Content").htmlarea();  
        });


        function DownloadFile(file) {
            // alert(file);
            var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;

            //alert(localFilePath);
            window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);

           // window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
        }


        function Download_ApprovedPO() {
            // alert(file);
            var localFilePath = document.getElementById("<%=hdnApprovedPO_FilePath.ClientID%>").value;
             var localFileName = document.getElementById("<%=hdnApprovedPO_FileName.ClientID%>").value;

            //alert(localFilePath);
           window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + localFileName);

           //window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + localFileName);

        }

        function Download_POSignCopyFile() {
            // alert(file);
             var localFilePath = document.getElementById("<%=hdnSingPOCopyFilePath.ClientID%>").value;
             var localFileName = document.getElementById("<%=hdnSingPOCopyFileName.ClientID%>").value;

            //alert(localFilePath);
            window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + localFileName);

             //window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + localFileName);

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
        function onCharOnlyNumber_dot(e) {
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

        
 

    </script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="VSCB_POWODelete.aspx.cs" 
    Inherits="procs_VSCB_POWODelete" EnableViewState="true" ValidateRequest="false"  MaintainScrollPositionOnPostback="true" %>

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
            text-align:right;
        }

        .textboxAlignAmount {
           
            text-align:right;
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
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
   

          <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
    <script src="../js/freeze/jquery-1.11.0.min.js"></script>


    <script src="../js/HtmlControl/jHtmlArea-0.8.min.js"></script>
    <link href="../js/HtmlControl/jHtmlArea.css" rel="stylesheet" />


  


    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="PO / WO Detail"></asp:Label>
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
                            <asp:DropDownList runat="server" ID="lstTripType" AutoPostBack="true" CssClass="DropdownListSearch" OnSelectedIndexChanged="lstTripType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </li>
                        <li class="trvl_date">
                            <span>PO/ WO Type</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPOtype" runat="server" Visible="true" ReadOnly="true" Enabled="False"></asp:TextBox>
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
                            <span>Cost Center</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtCostCenter" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>



                        <li class="trvl_date">
                              <span>Project/Department</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtProject" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                                <span id="spnPOWOStatus" runat="server" visible="false">PO/ WO Payment Status</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPOStatus" Visible="false" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                        
                        </li>


                        <li class="trvl_date">
                            <span>PO/ WO Amount (Without GST)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtBasePOWOWAmt" runat="server" CssClass="textboxAlignAmount" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span id="spnAmountWithTax" runat="server" visible="false">PO/ WO Amount (With GST) (A)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPOWOAmt" Visible="false" runat="server" ReadOnly="true" Enabled="False" CssClass="textboxBackColor"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span id="spnPOWODShortClosedAmt" runat="server" visible="false">PO/ WO Short Closed Amount</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPOWOShortClosedAmt" runat="server" ReadOnly="true" Enabled="False" Visible="false" CssClass="textboxBackColor"></asp:TextBox>
                        </li>

                        <li class="trvl_date">
                            <span id="spnPOWODirectTaxAmt" runat="server" visible="false">Direct Tax Amount (B)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtDirectTaxAmt" runat="server" ReadOnly="true" Enabled="False" Visible="false" CssClass="textboxBackColor"></asp:TextBox>

                        </li>
                        <li class="trvl_date">
                            <span id="spnPOWOPaidAmt" runat="server" visible="false">Paid Amount (C)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPOWOPaidAmt" runat="server" ReadOnly="true" Enabled="False" Visible="false" CssClass="textboxBackColor"></asp:TextBox>
                            <asp:TextBox AutoComplete="off" ID="txtPoPaidAmt_WithOutDT" runat="server" ReadOnly="true" Enabled="False" Visible="false" CssClass="textboxBackColor"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span id="spnPOWOBalAmt" runat="server" visible="false">Balance Amount (A-B-C)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPOWOBalanceAmt" runat="server" ReadOnly="true" Enabled="False" Visible="false" CssClass="textboxBackColor"></asp:TextBox>
                        </li>

                        <li class="trvl_date">
                            <span class="LableName">PO/ WO Content</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_POWOContent_description" runat="server" Rows="20" TextMode="MultiLine" MaxLength="15" CssClass="GoalDecriptionTextArea" onkeyup="countChar(this)"></asp:TextBox>
                        </li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>

                        <li class="trvl_date" id="liPOWOContent_1" runat="server" visible="false">

                            <div class="boxTest" style="width: 785px">
                                <div class="POWOContentTextArea">
                                    <div style="width: 770px">
                                        <span id="lblPOWO_Content" runat="server"></span>
                                    </div>
                                </div>
                            </div>
                        </li>
                        <li class="trvl_date" id="liPOWOContent_2" runat="server" visible="false"></li>
                        <li class="trvl_date" id="liPOWOContent_3" runat="server" visible="false"></li>

                        <li class="trvl_date" id="liViewPOWO_SignCopy_1" runat="server" visible="false">
                            <span class="LableName">View PO/ WO Content Sign Copy</span>
                            <br />
                            <asp:LinkButton ID="lnkViewPOWO_SignCopy" runat="server" OnClick="lnkViewPOWO_SignCopy_Click" Visible="false" CssClass="BtnShow"></asp:LinkButton>
                        </li>
                        <li class="trvl_date" id="liViewPOWO_SignCopy_2" runat="server" visible="false"></li>
                        <li class="trvl_date" id="liViewPOWO_SignCopy_3" runat="server" visible="false"></li>


                        <li class="trvL_detail" id="litrvldetail" runat="server">
                            <asp:Label runat="server" ID="lblMilestoneMsg" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                            <br />
                            <asp:LinkButton ID="btnTra_Details" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" OnClick="btnTra_Details_Click"></asp:LinkButton>
                            <span id="spntrvldtls" runat="server" class="LableName">PO/WO Milestones </span>
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
                                    <span>Milestone Due Date </span>&nbsp;&nbsp;<span style="color: red">*</span>
                                    <br />
                                    <asp:TextBox AutoComplete="off" ID="txtMilestoneDueDate" runat="server"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtMilestoneDueDate"
                                        Format="dd/MM/yyyy" runat="server">
                                    </ajaxToolkit:CalendarExtender>
                                </li>
                                <li class="trvl_date"></li>
                                <li class="trvl_date"></li>



                                <li class="trvl_date">
                                    <span>Quantity</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtQty" runat="server" MaxLength="5"  OnTextChanged="txtRate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span>Rate</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtRate" runat="server" MaxLength="10" CssClass="textboxAlignAmount" OnTextChanged="txtRate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span>Amount</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtAmount" runat="server" ReadOnly="true" Enabled="False" CssClass="textboxAlignAmount" OnTextChanged="txtRate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </li>

                                <li class="trvl_date">
                                    <span>CGST(%)</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtCGSTPer" runat="server" AutoPostBack="true" CssClass="textboxAlignAmount" OnTextChanged="txtCGSTPer_TextChanged"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span>SGST(%)</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtSGSTPer" runat="server" AutoPostBack="true" CssClass="textboxAlignAmount" OnTextChanged="txtSGSTPer_TextChanged"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span>IGST(%)</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtIGSTPer" runat="server" AutoPostBack="true" CssClass="textboxAlignAmount" OnTextChanged="txtIGSTPer_TextChanged"></asp:TextBox>
                                </li>


                                <li class="trvl_date">
                                    <span>CGST Amount</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtCGST_Amt" runat="server" ReadOnly="true" CssClass="textboxAlignAmount" Enabled="False"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span>SGST Amount</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtSGST_Amt" runat="server" ReadOnly="true" CssClass="textboxAlignAmount" Enabled="False"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span>IGST Amount</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtIGST_Amt" runat="server" ReadOnly="true" CssClass="textboxAlignAmount" Enabled="False"></asp:TextBox>
                                </li>


                                <li class="trvl_date">
                                    <span>Amount (With GST)</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtAmtWithTax" runat="server" CssClass="textboxAlignAmount" ReadOnly="true" Enabled="False"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span id="spnMilestonePaidAmt" runat="server" visible="false">Milestone Paid Amount</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtMilestonePaidAmt" runat="server" CssClass="textboxAlignAmount" ReadOnly="true" Enabled="False" Visible="false"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span id="spnMilestoneDirectTaxAmt" runat="server" visible="false">Milestone Direct Tax Amount</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtMilestoneDirectTaxAmt" runat="server" CssClass="textboxAlignAmount" ReadOnly="true" Enabled="False" Visible="false"></asp:TextBox>
                                </li>

                                <li class="trvl_date">
                                    <span id="spnMilestoneBalAmt" runat="server" visible="false">Milestone Balance Amount</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txtMilestoneBalanceAmt" runat="server" CssClass="textboxAlignAmount" ReadOnly="true" Enabled="False" Visible="false"></asp:TextBox>
                                </li>
                                <li class="trvl_date"></li>
                                <li class="trvl_date"></li>



                                <li class="trvldetails_bookthrough"></li>

                                <li class="trvldetails_bookthrough"></li>


                                <li class="trvldetails_bookthrough"></li>
                            </ul>
                            <div>
                                <asp:LinkButton ID="trvldeatils_btnSave" runat="server" Text="Add Milestone" ToolTip="Add Milestone" CssClass="Savebtnsve" OnClientClick=" return MultiClick_Trvl();" OnClick="trvldeatils_btnSave_Click"></asp:LinkButton>
                                <asp:LinkButton ID="trvldeatils_delete_btn" Visible="false" runat="server" Text="Short Close" ToolTip="Short Close" CssClass="Savebtnsve" OnClick="trvldeatils_delete_btn_Click"></asp:LinkButton>
                                <asp:LinkButton ID="trvldeatils_cancel_btn" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="trvldeatils_cancel_btn_Click"></asp:LinkButton>
                            </div>
                        </div>

                        <li class="trvl_date" id="lilblShortClosemsg_1" runat="server" visible="false">
                            <asp:Label runat="server" ID="lblShortCloseMsg" Visible="false" Style="color: red; font-size: 13px; font-weight: 500; text-align: center;"></asp:Label>
                        </li>
                        <li class="trvl_date" id="lilblShortClosemsg_2" runat="server" visible="false"></li>
                        <li class="trvl_date" id="lilblShortClosemsg_3" runat="server" visible="false"></li>

                        <li class="trvl_grid" id="litrvlgrid" runat="server">
                            <div class="manage_grid" style="overflow: scroll; width: 100%; margin-bottom: 5px;">

                                <asp:GridView ID="dgTravelRequest" HeaderStyle-CssClass="GVFixedHeader" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
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

                                        <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkTravelDetailsEdit" runat="server" CssClass="BtnShow" Text='View' OnClick="lnkTravelDetailsEdit_Click" Visible='<%# Eval("PaymentStatusID").ToString() == "2" ? false : true %>'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="5%" />
                                        </asp:TemplateField>

                                        <asp:BoundField HeaderText="Milestone No"
                                            DataField="srno"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="3%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Mliestone Particular"
                                            DataField="MilestoneName"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Milestone Due Date"
                                            DataField="Milestone_due_date"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="7%" ItemStyle-BorderColor="Navy" />

                                       <asp:BoundField HeaderText="UOM"
                                            DataField="UOM"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="3%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Quantity"
                                            DataField="Quantity"
                                            ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-Width="3%" ItemStyle-BorderColor="Navy" />

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


                        <li class="trvl_local" style="display: none">
                            <span>Upload PO File</span> &nbsp;&nbsp;<span style="color: red">*</span>
                            <asp:FileUpload ID="POUploadfile" runat="server" AllowMultiple="false"></asp:FileUpload>
                            <br />
                            <asp:LinkButton ID="lnkfile_PO" runat="server" OnClick="lnkfile_PO_Click" Visible="false" CssClass="BtnShow"></asp:LinkButton>
                        </li>
                        <li class="trvl_date" style="display: none"></li>
                        <li class="trvl_date" style="display: none"></li>


                        <li class="trvl_local">
                            <span>Upload Supporting Files</span>
                            <asp:FileUpload ID="ploadexpfile" runat="server" AllowMultiple="true"></asp:FileUpload>
                            <div class="manage_grid" style="overflow: scroll; width: 100%;">
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
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDeleteexpFile" runat="server" Text="Delete" OnClick="lnkDeleteexpFile_Click">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>

                          <li class="trvl_local" id="liRemarks_1" runat="server" visible="false">
                            <asp:Label runat="server" ID="lblCancellationRemarks_msg" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                            <br />
                            <span>Remark</span><span style="color: red; font-size: 10px; font-weight: normal; font-style: italic;"> Maximum 200 Characters</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_remakrs" runat="server" Enabled="false" TextMode="MultiLine" MaxLength="200" CssClass="txtRemarks"></asp:TextBox>
                        </li>
                         <li class="trvl_local" id="liRemarks_2" runat="server" visible="false"></li>
                          <li class="trvl_local" id="liRemarks_3" runat="server" visible="false"></li>
                         

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
        <asp:LinkButton ID="trvl_btnSave" runat="server" Text="Delete" ToolTip="Save" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();" OnClick="trvl_btnSave_Click">Delete</asp:LinkButton>
       
        <asp:LinkButton ID="btnback_mng" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="btnback_mng_Click">Back</asp:LinkButton>
        
        <asp:LinkButton ID="btnCorrection" Visible="false" runat="server" Text="Download PO/ WO" ToolTip="Download PO/ WO" CssClass="Savebtnsve"  OnClientClick="Download_ApprovedPO()">Download PO/ WO</asp:LinkButton> <%--OnClick="btnCorrection_Click"--%>
         <asp:LinkButton ID="btnApprove"  runat="server" Text="View Draft Copy" ToolTip="View Draft Copy" Visible="false" CssClass="Savebtnsve" OnClick="btnApprove_Click" >View Draft Copy</asp:LinkButton>
        <asp:LinkButton ID="btnReject"  runat="server" Visible="false" Text="View Draft Copy" ToolTip="View Draft Copy"  CssClass="Savebtnsve"  OnClick="btnReject_Click" >View Draft Copy</asp:LinkButton>
    </div>


    <ul>
          <li class="trvl_grid" id="li1" runat="server">
                           <%-- <div class="manage_grid" style="overflow: scroll; width: 100%; margin-bottom: 5px;"> --%>
                                <span id="spnPaymentHistory" runat="server" visible="false" class="LableName">Payment History </span>
                                <asp:GridView ID="dgPaymentHistory" runat="server" CssClass="Milestones" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="120%">
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

                                           <asp:BoundField HeaderText="Sr.no"
                                            DataField="Rowno"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Payment Request Created By"
                                            DataField="paymentReqCreatedBy"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="18%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Vendor Name"
                                            DataField="vendorname"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Invoice No"
                                            DataField="InvoiceNo"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                       <asp:BoundField HeaderText="Invoice Amount (With GST) (A)"
                                            DataField="AmtWithTax"
                                            ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Direct Tax Amount (B)"
                                            DataField="DirectTax_Amount"
                                            ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Invoice Payable Amount (A-B)"
                                            DataField="Payable_Amt_With_Tax"
                                            ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Payment Request No"
                                            DataField="PaymentReqNo"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="18%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Payment Request Amount"
                                            DataField="TobePaidAmtWithtax"
                                            ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                         <asp:BoundField HeaderText="Batch No"
                                            DataField="Batch_No"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                           <asp:BoundField HeaderText="Payment Batch Approver"
                                            DataField="PaymentBatchApprover"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="18%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Payment Batch Status"
                                            DataField="Request_status"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />

                                     

                                    </Columns>
                                </asp:GridView>

                            <%-- </div>--%>
                        </li>
    </ul>

    <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
    <asp:TextBox AutoComplete="off" ID="txtEmpName" runat="server" Visible="false"></asp:TextBox>

    <asp:Label ID="testsanjay" runat="server" Visible="false"></asp:Label>
    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />

    <asp:DropDownList runat="server" ID="lstPOType" AutoPostBack="true" Visible="false" CssClass="DropdownListSearch" OnSelectedIndexChanged="lstPOType_ForApproval_SelectedIndexChanged">
    </asp:DropDownList>
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
    <asp:HiddenField ID="hdnShortClose_Cancelled" runat="server" />
    <asp:HiddenField ID="hdnIsShMilestoneClick" runat="server" />

    <asp:HiddenField ID="hdnApprovedPO_FileName" runat="server" />
    <asp:HiddenField ID="hdnApprovedPO_FilePath" runat="server" />

    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

        <script src="../js/freeze/jquery-ui.min.js"></script>
    <script src="../js/freeze/gridviewScroll.min.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {
            $(".DropdownListSearch").select2();
            $("#MainContent_txtPOWO_Content").htmlarea();
            $("#MainContent_txt_POWOContent_description").htmlarea();

            $('#MainContent_dgPaymentHistory').gridviewScroll({
            width: 1070,
            height: 600,
            freezesize:4, // Freeze Number of Columns.
            headerrowcount: 1, //Freeze Number of Rows with Header.
                });

             

        });


        function DownloadFile(file) {
            // alert(file);
            var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;

            //alert(localFilePath);
            window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
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


        

        function Confirm() {
            //Testing();
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Delete ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            //alert(confirm_value.value);
            //document.forms[0].appendChild(confirm_value);
            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return;

        }
         function Download_ApprovedPO() {
            // alert(file);
             var localFilePath = document.getElementById("<%=hdnApprovedPO_FilePath.ClientID%>").value;
             var localFileName = document.getElementById("<%=hdnApprovedPO_FileName.ClientID%>").value;
            
            window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + localFileName);
          //  window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + localFileName);

        }

    </script>
</asp:Content>


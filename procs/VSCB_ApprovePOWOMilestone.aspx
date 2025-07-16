<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" EnableViewState="true"
    MaintainScrollPositionOnPostback="true" CodeFile="VSCB_ApprovePOWOMilestone.aspx.cs" Inherits="VSCB_ApprovePOWOMilestone" EnableSessionState="True" ValidateRequest="false" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <%--<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />
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
            width: 750px !important;
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

     <div id="loader" class="myLoader" style="display:none">
        <div class="loaderContent">
			<span style="top:-30%;font-size: 17px;color:red;position: absolute;">Please  Do Not Refresh  or Close Browser</span>
			<img src="../images/loader.gif" ></div>
		
    </div>
		
   
    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Approve PO/ WO Milestone"></asp:Label>
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
                            <span>PO/ WO Number</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPOWO_Number" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>PO/ WO Type</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPOtype" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
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
                            <asp:TextBox AutoComplete="off" ID="txtProject" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                            <span style="display:none">Cost Center</span> 
                            <asp:TextBox AutoComplete="off" ID="txtCostCenter" runat="server" ReadOnly="true" Enabled="False" Visible="false"></asp:TextBox>
                        </li>



                        <li class="trvl_date"> 
                             <span>PO/ WO Amount (Without GST)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtBasePOWOWAmt" runat="server" ReadOnly="true" Enabled="False" CssClass="AmtTextAlign"></asp:TextBox>

                            <span id="spnPOWOStatus" runat="server" visible="false">PO/ WO Payment Status</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPOStatus" Visible="false" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>

                        </li>
                        <li class="trvl_date">
                             <span id="spnAmountWithTax" runat="server" visible="false">PO/ WO Amount (With GST)</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPOWOAmt" Visible="false" runat="server" ReadOnly="true" Enabled="False" CssClass="textboxBackColor AmtTextAlign"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                             <span>Currency </span><br />
                            <asp:TextBox AutoComplete="off" ID="txtCurrency" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox> 
                        </li> 

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
                            <div class="boxTest" style="width: 760px">
                                <div class="POWOContentTextArea">
                                    <div style="width: 720px">
                                        <span id="lblPOWO_Content" runat="server"></span>
                                    </div>
                                </div>
                            </div>
                        </li>
                        <li class="trvl_date" id="liPOWOContent_2" runat="server" visible="false"></li>
                        <li class="trvl_date" id="liPOWOContent_3" runat="server" visible="false"></li>

                        <li class="trvl_date" id="liPOWOContent_Download_1" runat="server" visible="false">
                            <asp:LinkButton ID="lnkDownload_POContent" runat="server" OnClick="lnkDownload_POContent_Click" Visible="false" CssClass="BtnShow">Download PO/WO Content</asp:LinkButton>
                        </li>
                        <li class="trvl_date" id="liPOWOContent_Download_2" runat="server" visible="false"></li>
                        <li class="trvl_date" id="liPOWOContent_Download_3" runat="server" visible="false"></li>



                        <li class="trvl_grid" id="litrvlgrid" runat="server">
                            <div class="manage_grid" style="overflow: scroll; width: 100%; padding-top: 20px; margin-bottom: 10px;">
                                <span class="LableName" runat="server" id="spMilestones">PO/ WO Milestones</span>
                                <asp:GridView ID="dgTravelRequest" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                    DataKeyNames="srno,PaymentStatusID,IsShortClone">
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
                                        <asp:BoundField HeaderText="Milestone No"
                                            DataField="srno"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="3%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Mliestone Particular"
                                            DataField="MilestoneName"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="25%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Milestone Due Date"
                                            DataField="Milestone_due_date"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="7%" ItemStyle-BorderColor="Navy" />

                                            <asp:BoundField HeaderText="UOM"
                                            DataField="UOM"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="6%" ItemStyle-BorderColor="Navy" />

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
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>

                        <li class="trvl_local" id="liPOUploadedFile_1" runat="server" visible="false">
                            <span id="spnUploadedPOWOFile" runat="server" visible="false">Uploaded PO/ WO File</span>
                            <asp:LinkButton ID="lnkfile_PO" runat="server" OnClick="lnkfile_PO_Click" Visible="false" CssClass="BtnShow"></asp:LinkButton>
                        </li>
                        <li class="trvl_date"  id="liPOUploadedFile_2" runat="server" visible="false">></li>
                        <li class="trvl_date"  id="liPOUploadedFile_3" runat="server" visible="false">></li>

                        <li class="trvl_date" id="liPOSignCopy_1" runat="server" visible="false"> 
                            <span runat="server" id="spnUploadSignCopy" visible="false">Upload PO/ WO Sign File &nbsp;&nbsp;<span style="color: red">*</span></span>
                            <asp:FileUpload ID="POWO_SignCopyUploadfile" runat="server" AllowMultiple="false" Visible="false"></asp:FileUpload>
                        </li>
                        <li class="trvl_date"  id="liPOSignCopy_2" runat="server" visible="false"></li>
                        <li class="trvl_date"  id="liPOSignCopy_3" runat="server" visible="false"> ></li>

                        <li class="trvl_local" id="liSupportingDoc_1" runat="server" visible="false">
                            <div class="manage_grid" style="overflow: scroll; width: 100%; padding-top: 5px; margin-bottom: 5px;">
                                <span id="spnUploadedSupportingFiles" visible="false" runat="server">Supporting Files</span>
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
                            </div>
                        </li>
                        <li class="trvl_date" id="liSupportingDoc_2" runat="server" visible="false"></li>
                        <li class="trvl_date" id="liSupportingDoc_3" runat="server" visible="false"></li>


                        <li class="trvl_local">
                            <span>Remarks</span><span style="color: red; font-size: 10px; font-weight: normal; font-style: italic;"> Maximum 100 Characters</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_remakrs" runat="server" TextMode="MultiLine" MaxLength="200"></asp:TextBox>
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
        
        <asp:LinkButton ID="trvl_btnSave" runat="server" Text="Approve" ToolTip="Approve" CssClass="Savebtnsve"   OnClientClick="return SaveMultiClick();" OnClick="trvl_btnSave_Click">Approve</asp:LinkButton> <%--OnClientClick="StartLoader()"--%>

        <asp:LinkButton ID="btnCorrection" runat="server" Text="Correction" ToolTip="" CssClass="Savebtnsve" OnClientClick="return SendforCorrectionMultiClick();" OnClick="btnCorrection_Click">Send for Correction</asp:LinkButton>

        <asp:LinkButton ID="btnCancel" runat="server" Text="Reject" ToolTip="Reject" CssClass="Savebtnsve" OnClientClick="return SaveMultiRejectClick();" OnClick="btnCancel_Click">Reject</asp:LinkButton>

        <asp:LinkButton ID="btnback_mng" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/VSCB_InboxPOWO.aspx">Back</asp:LinkButton>

        <asp:LinkButton ID="btnApprove"  runat="server" Text="View Draft Copy" ToolTip="View Draft Copy" CssClass="Savebtnsve" OnClick="btnApprove_Click" >View Draft Copy</asp:LinkButton>
        

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
    <asp:HiddenField ID="hdnRejectYesNo" runat="server" />
    <asp:HiddenField ID="hdnCorrectionYesNo" runat="server" />
    <asp:HiddenField ID="hdnIsShorClose" runat="server" />
    <asp:HiddenField ID="hdnfileid" runat="server" />
    <asp:HiddenField ID="hdnCondintionId" runat="server" />
    <asp:HiddenField ID="hdnRangeId" runat="server" />
    <asp:HiddenField ID="hdnAppr_StatusId" runat="server" />
    <asp:HiddenField ID="hdnPOTypeId_ForApproval" runat="server" />

    <asp:HiddenField ID="hdnemplogin_type" runat="server" />
    <asp:HiddenField ID="hdncurrent_Appr_Name" runat="server" />
    <asp:HiddenField ID="hdncurrent_Appr_Empcode" runat="server" />
    <asp:HiddenField ID="hdncurrent_Appr_EmpEmail" runat="server" />
    <asp:HiddenField ID="hdncurrent_Appr_Id" runat="server" />


    <asp:HiddenField ID="hdnNext_Appr_Name" runat="server" />
    <asp:HiddenField ID="hdnNext_Appr_Empcode" runat="server" />
    <asp:HiddenField ID="hdnNext_Appr_EmpEmail" runat="server" />
    <asp:HiddenField ID="hdnNext_Appr_Id" runat="server" />
    <asp:HiddenField ID="hdnIsFinalApprover" runat="server" />

    <asp:HiddenField ID="hdnPOWOCreator_Name" runat="server" />
    <asp:HiddenField ID="hdnPOWOCreator_Email" runat="server" />


    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" /> 
    <link href="../includes/loader.css" rel="stylesheet" />
    <script type="text/javascript">

        $(document).ready(function () {
            $(".DropdownListSearch").select2();
            $("#MainContent_txtPOWO_Content").htmlarea();
        });

        function SaveMultiClick() {
            try {



                var retunboolean = true;
                var ele = document.getElementById('<%=trvl_btnSave.ClientID%>');

                if (Page_ClientValidate()) {	
				$('#loader').show();
			   // $('#loader').fadeOut(60000);
			}

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

  //      function StartLoader() {
			
		//	if (Page_ClientValidate()) {	
		//		$('#loader').show();
		//	   // $('#loader').fadeOut(60000);
		//	}
		//}
		function StopLoader() {
				$('#loader').hide();
		}


        function DownloadFile(file) {
            // alert(file);
            var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;

            //alert(localFilePath);
            window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
        }




        

        function SendforCorrectionMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=btnCorrection.ClientID%>');

                 if (Page_ClientValidate()) {	
				$('#loader').show();
			   // $('#loader').fadeOut(60000);
			}


                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        CorrectionConfirm();
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }


        function SaveMultiRejectClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=btnCancel.ClientID%>');

                 if (Page_ClientValidate()) {	
				$('#loader').show();
			   // $('#loader').fadeOut(60000);
			}


                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        RejectConfirm();
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }

        function RejectConfirm() {
            //Testing();
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Reject ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.getElementById("<%=hdnRejectYesNo.ClientID%>").value = confirm_value.value;
            return;

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

        function CorrectionConfirm() {
            //Testing();
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Submit ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.getElementById("<%=hdnCorrectionYesNo.ClientID%>").value = confirm_value.value;
            return;

        }

         function popupwindow(url) {
          var left = (screen.width/2)-(1366/2);
          var top = (screen.height/2)-(768/2);
          return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width='+w+', height='+h+', top='+top+', left='+left);
        } 


    </script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="Payment_Req_Arch.aspx.cs" Inherits="Payment_Req_Arch" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />
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

        #MainContent_lstApprover {
            overflow: hidden !important;
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
                        <asp:Label ID="lblheading" runat="server" Text="Payment Voucher"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>
                <span>
                    <a href="Voucher.aspx" class="aaaa">Payment Voucher Home</a>
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


                        <li class="mobile_inboxEmpCode">
                            <span>Employee Code</span><br />

                            <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="True" Enabled="false"> </asp:TextBox>

                        </li>                        
                        <li class="mobile_InboxEmpName">
                            <span>Employee Name</span><br />

                            <asp:TextBox AutoComplete="off" ID="txtEmpName" runat="server" Visible="True" Enabled="false"></asp:TextBox>

                        </li>
                        
                        <li class="mobile_inboxEmpCode">   
                            <span >Contact no. </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_Contact" runat="server" Enabled="false"></asp:TextBox>                                                    
                        </li>

                        <li class="mobile_InboxEmpName">                            
                            <span >Alternate Contact no. </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_Alt_Contact" runat="server" MaxLength="10" Enabled="false"> </asp:TextBox>
                        </li>

                        <li class="mobile_inboxEmpCode">                            
                            <span >Project Name </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_ProjectName" runat="server" Enabled="false"></asp:TextBox>
                        </li>

                        <li class="mobile_InboxEmpName">                            
                            <span >Department Name </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_DeptName" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <%--<li class="mobile_inboxEmpCode">

                            <asp:TextBox AutoComplete="off" ID="TextBox2" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>

                        </li>
                        <li class="mobile_trip">


                            <asp:TextBox AutoComplete="off" ID="txtReimID" runat="server" Visible="false"></asp:TextBox>

                        </li>--%>
<%--                        <li class="mobile_inboxEmpCode">
                            <asp:TextBox AutoComplete="off" ID="TextBox3" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>

                        </li>--%>


                        <li class="mobile_date">
                            <span>Claim Date </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtFromdate" runat="server" AutoPostBack="True" ReadOnly="true"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="MM/yyyy" TargetControlID="txtFromdate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>


                        </li>
                        <li class="mobile_inboxEmpCode">
                            <%-- <span>Employee Code</span><br />--%>

                            <asp:TextBox AutoComplete="off" ID="TextBox4" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>

                        </li>

                        <li class="mobile_detail">
                            <span>Claim Details</span>
                            <asp:LinkButton ID="btnTra_Details" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" OnClick="btnTra_Details_Click" Visible="false"></asp:LinkButton>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <%-- <span>Employee Code</span><br />--%>

                            <asp:TextBox AutoComplete="off" ID="TextBox5" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>

                        </li>
                        <li class="Payment_grid">

                            <div>

                                <asp:GridView ID="dgMobileClaim" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"  CellPadding="3" AutoGenerateColumns="False" Width="100%" Enabled="false">
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
                                        <asp:BoundField HeaderText="Month"
                                            DataField="Rem_Month"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" />

                                        <asp:BoundField HeaderText="Expenses"
                                            DataField="pv"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="30%" />

                                        <asp:BoundField HeaderText="Bill/Receipt no."
                                            DataField="Billno"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="15%"  />

                                        <asp:BoundField HeaderText="Bill Amount"
                                            DataField="Amount"
                                            ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-Width="15%" />

                                        <asp:BoundField HeaderText="Particulars"
                                            DataField="Particulars"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="30%" />

                                    </Columns>
                                </asp:GridView>

                            </div>
                            </br>
                        </li>
<%--                        <li class="mobile_inboxEmpCode">

                            <asp:TextBox AutoComplete="off" ID="TextBox6" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>

                        </li>--%>

                        <li class="mobile_Amount">
                            <span>Total Amount Claimed: </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtAmount" runat="server" MaxLength="100" ReadOnly="true"></asp:TextBox>
                        </li>

                        <li class="mobile_inboxEmpCode">
                             <span runat="server" id="COS_Rem" visible="false" >Recommendation by COS </span><br />
                            <asp:TextBox AutoComplete="off" ID="Txt_COSRecommended" runat="server" MaxLength="30" Visible="false" ReadOnly="true" Enabled="false"> </asp:TextBox>
                        </li>

                        <li class="mobile_Amount">
                            <span runat="server" id="Spn_HOD_Recm_Amt" visible="false">Recommended Amount </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_HOD_Recm_Amt" runat="server" MaxLength="100" Visible ="false" Enabled="false"></asp:TextBox>
                        </li>

                        <li class="mobile_inboxEmpCode">
                            <span runat="server" id="Spn_CFO_Recm_Amt" visible="false">Recommended Amount </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_CFO_Recm_Amt" runat="server" MaxLength="100" Visible ="false" Enabled="false"></asp:TextBox>
                        </li>
                       
                        <li class="mobile_inboxEmpCode" style="display:none">
                             <span>Comments </span><br />
                            <asp:TextBox AutoComplete="off" ID="txtComment" runat="server" MaxLength="30" Enabled="false"> </asp:TextBox>
                            <asp:TextBox AutoComplete="off" ID="txtRecommendation_COS" runat="server" Visible="false" MaxLength="30"> </asp:TextBox>
                        </li>
                      
                         <li class="mobile_inboxEmpCode" style="display:none">
                            <%-- <span>Employee Code</span><br />--%>
                            <asp:TextBox AutoComplete="off" ID="TextBox1" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
                        </li>

                        <li>
                            <!--<span>Upload File</span>-->
                            <span> </span>
                         
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
                                        <asp:TemplateField HeaderText="Uploaded Files">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkviewfile" runat="server" OnClientClick=<%# "DownloadFile('" + Eval("filename") + "')" %> Text='<%# Eval("filename") %>' >
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                    </Columns>
                        </asp:GridView>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <%-- <span>Employee Code</span><br />--%>
                            <asp:TextBox AutoComplete="off" ID="TextBox2" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
                        </li>
                        <li class="mobile_Approver">
                            <span>Approver</span>
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
    <div class="mobile_Savebtndiv">
        <asp:LinkButton ID="mobile_btnSave" Visible="false" runat="server" Text="Approve" ToolTip="Approve" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click" OnClientClick="return SaveMultiClick();">Approve</asp:LinkButton>
        <asp:LinkButton ID="mobile_btnSave_COSACC" Visible="false" runat="server" Text="Submit" ToolTip="Submit" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click" OnClientClick="return SaveMultiClick_COSSACC();">Submit</asp:LinkButton>
         <asp:LinkButton ID="mobile_btnReject" Visible="false" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClick="mobile_btnReject_Click" OnClientClick="return CancelMultiClick();">Reject</asp:LinkButton>
         <asp:LinkButton ID="mobile_btnBack" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClick="mobile_btnBack_Click">Back</asp:LinkButton>
        <asp:LinkButton ID="mobile_btnPrintPV" runat="server" Text="Print Payment Voucher" ToolTip="Print Payment Voucher" CssClass="Savebtnsve" OnClick="mobile_btnPrintPV_Click">Print Payment Voucher</asp:LinkButton>

        


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



     
    <asp:HiddenField ID="FilePath" runat="server" />

    <asp:HiddenField ID="hflEmpDesignation" runat="server" />

    <asp:HiddenField ID="hflEmpDepartment" runat="server" />

    <asp:HiddenField ID="hflEmailAddress" runat="server" />

    <asp:HiddenField ID="hflGrade" runat="server" />

    <asp:HiddenField ID="hflapprcode" runat="server" />

    <asp:HiddenField ID="hdnDesk" runat="server" />

    <asp:HiddenField ID="hdnDestnation" runat="server" />

    <asp:HiddenField ID="hdnRemid" runat="server" />

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
      <asp:HiddenField ID="Comp_Code" runat="server" />
    <asp:HiddenField ID="hdnDept_Id" runat="server" />
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
                var ele = document.getElementById('<%=mobile_btnSave.ClientID%>');

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
            try {
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
            return retunboolean;
        }




        function CancelMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=mobile_btnReject.ClientID%>');

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
        function DownloadFile(file) {
            // alert(file);
            var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;

                    //alert(localFilePath);
                    window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
                }
    </script>
</asp:Content>

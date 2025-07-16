<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="VSCB_ApproveBatch.aspx.cs" Inherits="VSCB_ApproveBatch" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/manageleave_css.css" type="text/css" media="all" />

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />

    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
            background: #dae1ed;
        }

        .LableName {
            color: #F28820;
            font-size: 16px;
            font-weight: normal;
            text-align: left;
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
        .Remarks{
            height: 95px;
            width: 633px;
        }

          .paging a {
            background-color: #C7D3D4;
            padding: 5px 7px;
            text-decoration: none;
            border: 1px solid #C7D3D4;
        }

            .paging a:hover {
                background-color: #E1FFEF;
                color: #00C157;
                border: 1px solid #C7D3D4;
            }

        .paging span {
            background-color: #E1FFEF;
            padding: 5px 7px;
            color: #00C157;
            border: 1px solid #C7D3D4;
        }

        tr.paging {
            background: none !important;
        }

            tr.paging tr {
                background: none !important;
            }

            tr.paging td {
                border: none;
            }

    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>

      <script src="../js/freeze/jquery-1.11.0.min.js"></script>
	 <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" /> 

     <div id="loader" class="myLoader" style="display: none">
        <div class="loaderContent">
            <span style="top: -30%; font-size: 17px; color: red; position: absolute;">Please  Do Not Refresh  or Close Browser</span>
            <img src="../images/loader.gif">
        </div> 
    </div>


    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">

                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Approve Payment Batch"></asp:Label>
                    </span>
                </div>
                <div>
                    <span>
                         <a href="vscb_index.aspx" class="aaaa"><%=System.Configuration.ConfigurationManager.AppSettings["VSCBPageTitle"]%></a>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                </div>



                <div class="edit-contact">
                    <ul id="Ul1" runat="server">

                        <li style="padding-top: 15px">
                            <span class="LableName">Batch Details</span>
                        </li>
                        <li style="padding-top: 15px"></li>
                        <li style="padding-top: 15px"></li>

                        <li class="trvl_date" style="padding-top: 15px">
                            <span>Batch Created Date:</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtbatchCreateDate" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>Batch Created by:</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtbatchCreatedBy" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>Batch No.:</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtbatchNo" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>


                        <li class="trvl_date">
                            <span>Batch no. of Requests:</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtbatchNoOfRequest" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>Batch Total Payament:</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtbatchTotalPayment" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                         <li class="trvl_date"> 
                            <span>Bank Name:</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtBank_name" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>

                         
                          <li class="trvl_date">
                            <span>Bank Ref.No:</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtBankRefNo" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>Bank Ref.Date:</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtBankRefDate" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox> 
                        </li>
                        <li class="trvl_date">
                            <span style="display:none">Bank Ref.Link:</span>
                           <a id="lnkBank" runat="server" class="BtnShow" target="_blank"></a>
                            <asp:TextBox AutoComplete="off" ID="txtBankRef_Link" runat="server" ReadOnly="true" Enabled="False" Visible="false"></asp:TextBox>
                        </li>

                         <li class="trvl_date">
                        <span id="spnInvoicefile" runat="server" class="LableFiles" visible="false">Batch Transaction File</span>&nbsp;&nbsp;<br />
                        <asp:LinkButton ID="lnkfile_Invoice" runat="server" OnClick="lnkfile_Invoice_Click" CssClass="BtnShow" Visible="false"></asp:LinkButton><br /><br /> 
                        </li>
                           <li class="trvl_date">
                       
                        </li>
                           <li class="trvl_date">
                      
                        </li>
                      
                        <li class="trvl_grid">
                         <%--  <div class="manage_grid" style="overflow: scroll; width: 100%; padding-top: 20px; margin-bottom: 30px;">--%>
                                <span class="LableName">Voucher Details</span>
                                <asp:GridView ID="gvMngPaymentList_Batch" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                                    DataKeyNames="Payment_ID,InvoiceID,POID,paymentType_id" CellPadding="3" AutoGenerateColumns="False" Width="170%" EditRowStyle-Wrap="false" PageSize="10" AllowPaging="True" OnPageIndexChanging="gvMngPaymentList_Batch_PageIndexChanging">
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                    <PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    <PagerStyle HorizontalAlign = "Right" CssClass = "paging" />
                                    <Columns>

                                        <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                        <asp:LinkButton ID="lnkLeaveDetails" runat="server" Text='View' OnClick="lnkLeaveDetails_Click1" CssClass="BtnShow">                                             
                                        </asp:LinkButton>
                                        </ItemTemplate>
                                           <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        
                                        <asp:BoundField HeaderText="Payment Request Type"
                                            DataField="paymentType"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                           ItemStyle-BorderColor="Navy" ItemStyle-Width="5%"/> 

                                          <asp:BoundField HeaderText="Payment request Created By"
                                            DataField="PayementRequestCreatedBy"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left" 
                                             ItemStyle-BorderColor="Navy"  ItemStyle-Width="10%"/>

                                          <asp:BoundField HeaderText="Payment request approved by"
                                            DataField="PaymentApproverName"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                           ItemStyle-BorderColor="Navy" ItemStyle-Width="10%" /> 


                                           <asp:BoundField HeaderText="Amount to be paid"
                                            DataField="Amt_paid_Account"
                                            ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-HorizontalAlign="Right"
                                             ItemStyle-BorderColor="Navy"  ItemStyle-Width="5%" /> 

                                        <asp:BoundField HeaderText="Vendor Name"
                                            DataField="Name"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                           ItemStyle-BorderColor="Navy" ItemStyle-Width="10%"/>                                       


                                         <asp:BoundField HeaderText="Milestone Particular"
                                            DataField="MilestoneName"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                             ItemStyle-BorderColor="Navy" ItemStyle-Width="18%" />


                                        <asp:BoundField HeaderText="Cost Center"
                                            DataField="Project_Dept_Name"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                             ItemStyle-BorderColor="Navy" ItemStyle-Width="10%" />

                                        
                                        <asp:BoundField HeaderText="PO/ WO Number"
                                            DataField="PONumber"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-BorderColor="Navy" ItemStyle-Width="10%"  /> 

                                        <asp:BoundField HeaderText="Invoice No"
                                            DataField="InvoiceNo"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                             ItemStyle-BorderColor="Navy" ItemStyle-Width="8%"  />

                                        <asp:BoundField HeaderText="Invoice approved by"
                                            DataField="InvoiceApproverName"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left" 
                                             ItemStyle-BorderColor="Navy"  ItemStyle-Width="10%" />


                                        <asp:BoundField HeaderText="Payment request Date"
                                            DataField="PaymentReqDate"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                             ItemStyle-BorderColor="Navy" ItemStyle-Width="6%" />

                                       

                                        <asp:BoundField HeaderText="Bank A/c No"
                                            DataField="Acc_no"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-BorderColor="Navy" ItemStyle-Width="7%" />

                                        <asp:BoundField HeaderText="IFSC Code"
                                            DataField="IFSC_Code"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                              ItemStyle-BorderColor="Navy"  ItemStyle-Width="6%" />

                                        <asp:BoundField HeaderText="Acc.Remarks"
                                            DataField="AccountRemarks"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-BorderColor="Navy" ItemStyle-Width="10%"   />

                                        <asp:TemplateField HeaderText="Bank Details" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>                                               
                                            <asp:LinkButton ID="lnkBankdetails"  OnClientClick=<%#"downloadBankdetails('" + Eval("bankfilename") + "')" %> CssClass="BtnShow" runat="server" Text='View' Visible='<%#Eval("showbankdtls").ToString() == "1" ? true : false %>'>

                                            </asp:LinkButton>
                                            </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="8%" />
                                    </asp:TemplateField>
                                          

                                    </Columns>
                                </asp:GridView>

                           <%-- </div>--%>
                        </li> 
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>


                        <li class="trvl_Approver">
                          <span id="spnremarks" runat="server">Remarks </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtRemarks" runat="server" MaxLength="100" CssClass="Remarks" TextMode="MultiLine"></asp:TextBox>
                        </li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>

                           <li class="trvl_date"></li>
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

                <div class="trvl_Savebtndiv">
                    <span>
                          <asp:LinkButton ID="trvl_btnSave"   runat="server" Text="Approve" ToolTip="Approve" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();" OnClick="mobile_btnSave_Click"></asp:LinkButton>
                    </span>
                    <span>
                        <asp:LinkButton ID="btnCancel" runat="server" Text="Reject" ToolTip="Reject" CssClass="Savebtnsve" OnClientClick="return CancelMultiClick();" OnClick="btnCancel_Click">Reject</asp:LinkButton>
                    </span> 

                    <asp:LinkButton ID="btnback_mng" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="btnback_mng_Click">Back</asp:LinkButton>
                </div>


                <div class="edit-contact">
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" OnClick="lnkcont_Click"></asp:LinkButton>
                        </div>
                    </div>
                    <ul id="editform" runat="server" visible="false">
                        <li></li>
                    </ul>
                </div>
                <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
                <asp:TextBox AutoComplete="off" ID="txtEmpName" runat="server" Visible="false"></asp:TextBox>
                <asp:HiddenField ID="hdnPOWOID" runat="server" />
                <asp:HiddenField ID="hdnBatchId" runat="server" />

                <asp:HiddenField ID="hdnMilestoneId" runat="server" />
                <asp:HiddenField ID="hdnInvoiceId" runat="server" />

                <asp:HiddenField ID="hflEmpName" runat="server" />
                <asp:HiddenField ID="hflEmpDesignation" runat="server" />
                <asp:HiddenField ID="hflEmpDepartment" runat="server" />
                <asp:HiddenField ID="hflEmailAddress" runat="server" />
                <asp:HiddenField ID="hflGrade" runat="server" />
                 <asp:HiddenField ID="hdnYesNo" runat="server" />
                <asp:HiddenField ID="HDDateDifferencehours" runat="server" />


                <asp:HiddenField ID="hdn_Checker_Appr_Id" runat="server" />
                <asp:HiddenField ID="hdn_Checker_Appr_EmpCode" runat="server" />
                <asp:HiddenField ID="hdn_Checker_Appr_EmailId" runat="server" />
                <asp:HiddenField ID="hdn_Checker_Appr_EmpName" runat="server" />

                <asp:HiddenField ID="hdn_Approver1_Appr_Id" runat="server" />
                <asp:HiddenField ID="hdn_Approver1_Appr_EmpCode" runat="server" />
                <asp:HiddenField ID="hdn_Approver1_Appr_EmailId" runat="server" />
                <asp:HiddenField ID="hdn_Approver1_Appr_EmpName" runat="server" />

                <asp:HiddenField ID="hdn_Approver2_Appr_Id" runat="server" />
                <asp:HiddenField ID="hdn_Approver2_Appr_EmpCode" runat="server" />
                <asp:HiddenField ID="hdn_Approver2_Appr_EmailId" runat="server" />
                <asp:HiddenField ID="hdn_Approver2_Appr_EmpName" runat="server" />
                
                <asp:HiddenField ID="hdnApprovertype" runat="server" />
                 <asp:HiddenField ID="hdnUrlType" runat="server" />
                   <asp:HiddenField ID="hdnVendorBankDetails" runat="server" />

            </div>
        </div>
    </div>

<%--    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />--%>

      <script src="../js/freeze/jquery-ui.min.js"></script>
    <script src="../js/freeze/gridviewScroll.min.js"></script>    
    <link href="../includes/loader.css" rel="stylesheet" />

    <script type="text/javascript">      
        $(document).ready(function () {
            $(".DropdownListSearch").select2();

            	$('#MainContent_gvMngPaymentList_Batch').gridviewScroll({
                width: 1060,
                height: 600,
                freezesize:5, // Freeze Number of Columns.
                headerrowcount: 1, //Freeze Number of Rows with Header.
                });

        });

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
                var ele = document.getElementById('<%=trvl_btnSave.ClientID%>');

                $('#loader').show();

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

           function downloadBankdetails(filename) {
          // alert(filename);
             var localFilePath = document.getElementById("<%=hdnVendorBankDetails.ClientID%>").value;
            var localFileName = filename;

            //alert(localFilePath);
            window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + localFileName);

             //window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + localFileName);

        }
    </script>
</asp:Content>


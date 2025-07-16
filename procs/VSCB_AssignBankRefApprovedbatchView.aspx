<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="VSCB_AssignBankRefApprovedbatchView.aspx.cs" Inherits="procs_VSCB_AssignBankRefApprovedbatchView" %>

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

        .txtAligntextBox {
            text-align: right;
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
                        <asp:Label ID="lblheading" runat="server" Text="Assign Payment Ref. No. and Send Email as Payment Advice"></asp:Label>
                    </span>
                </div>
                <div>
                    <span>
                        <a href="vscb_index.aspx" class="aaaa"><%=System.Configuration.ConfigurationManager.AppSettings["VSCBPageTitle"]%></a>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Width="250px" Style="color: red; font-size: 14px; font-weight: 400; text-align: left;"></asp:Label>
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
                            <span id="spnBatchNo" runat="server" visible="false">Batch No.:</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtbatchNo" runat="server" ReadOnly="true" Enabled="False" Visible="false"></asp:TextBox>
                        </li>


                        <li class="trvl_date">
                            <span>No. of Requests in Batch:</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtbatchNoOfRequest" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>Batch Total Payament:</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtbatchTotalPayment" runat="server" ReadOnly="true" Enabled="False" CssClass="txtAligntextBox"></asp:TextBox>
                        </li>
                        <li class="trvl_date" id="idBankRef_1" runat="server" visible="false">
                            <span>Bank Ref.No:</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtBankRefNo" runat="server" Enabled="False"></asp:TextBox>
                        </li>

                        <li class="trvl_date" id="idBankRef_2" runat="server" visible="false">
                            <span>Bank Ref.Date:</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtBankRefDate" runat="server" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date" id="idBankRef_3" runat="server" visible="false">
                            <span>Bank Name</span>
                            <br />
                            <asp:TextBox ID="txtBankname" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li >
                           <asp:LinkButton ID="trvl_btnSave"   runat="server" Text="DownLoad Template" ToolTip="DownLoad Template" CssClass="Savebtnsve" OnClick="trvl_btnSave_Click"></asp:LinkButton>
                         
                        </li>



                        <li class="trvl_date">
                            <span>upload Transaction</span>&nbsp;&nbsp;
                            <asp:FileUpload ID="Uploadfile" runat="server"  accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" AllowMultiple="false"></asp:FileUpload>
                        </li>
                        <li class="trvl_date">
                               <asp:LinkButton ID="trvldeatils_btnSave" runat="server" Text="Upload File" ToolTip="Upload File" CssClass="Savebtnsve" OnClick="trvldeatils_btnSave_Click"></asp:LinkButton>
                            
                        </li>
                        <li class="trvl_date"></li>

                        <li class="trvl_grid">

                            <asp:GridView ID="gvMngPaymentList_Batch" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                                DataKeyNames="POID,MstoneID,Payment_ID,InvoiceID,paymentType_id" CellPadding="3" AutoGenerateColumns="False" Width="150%" EditRowStyle-Wrap="false" PageSize="100" AllowPaging="false" OnRowDataBound="gvMngPaymentList_Batch_RowDataBound" OnPageIndexChanging="gvMngPaymentList_Batch_PageIndexChanging">
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

                                    <asp:TemplateField HeaderText="Batch" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkTravelDetailsEdit" CssClass="BtnShow" runat="server" Text='Delete'>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="4%" />
                                    </asp:TemplateField>

                                    <asp:BoundField HeaderText="Payment Request Type" DataField="paymentType" ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="left" ItemStyle-BorderColor="Navy" ItemStyle-Width="5%" />

                                    <asp:TemplateField HeaderText="Payment Ref. No">
                                        <ItemTemplate>
                                            <span style="width: 100px">
                                                <asp:TextBox ID="TxtPaymentRefNo"  AutoComplete="off" runat="server" Text='<%#Eval("PaymentRefNo")%>' MaxLength="100" Style="padding-left: 0 !important; width: 145px !important"></asp:TextBox>
                                                <asp:Label ID="HFEmail" Visible="false" runat="server" Text='<%#Eval("VendorEmailAddress")%>'></asp:Label>
                                                <asp:Label ID="HDVendorName" Visible="false" runat="server" Text='<%#Eval("VendorName")%>'></asp:Label>
                                                <asp:Label ID="LblAmounttobepaid" Visible="false" runat="server" Text='<%#Eval("Amt_paid_Account")%>'></asp:Label>
                                                <asp:Label ID="lblPaymentCreatorEmailaddress" Visible="false" runat="server" Text='<%#Eval("PaymnetCreaterEmailAddress")%>'></asp:Label>
                                                <asp:Label ID="lblAmountINWord" Visible="false" runat="server" Text='<%#Eval("RupeesAmtpaidAmount")%>'></asp:Label>
                                                <asp:Label ID="LblvendorAcNo" Visible="false" runat="server" Text='<%#Eval("Acc_no")%>'></asp:Label>
                                                <asp:Label ID="LblVendorIFSCCode" Visible="false" runat="server" Text='<%#Eval("IFSC_Code")%>'></asp:Label>
                                                <asp:Label ID="lblVendorCode" Visible="false" runat="server" Text='<%#Eval("VendorCode")%>'></asp:Label>

                                                <asp:Label ID="lblInvoiceno" Visible="false" runat="server" Text='<%#Eval("InvoiceNo")%>'></asp:Label>
                                                <asp:Label ID="LblAccountno" Visible="false" runat="server" Text='<%#Eval("Acc_no")%>'></asp:Label>

                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField HeaderText="Vendor Name" DataField="VendorName" ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="left" ItemStyle-BorderColor="Navy" ItemStyle-Width="10%" />

                                    <asp:BoundField HeaderText="Vendor Email Address" DataField="VendorEmailAddress" ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="left" ItemStyle-BorderColor="Navy" ItemStyle-Width="15%" />

                                    <asp:BoundField HeaderText="Payment request Created By" DataField="PayementRequestCreatedBy"
                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-BorderColor="Navy" ItemStyle-Width="10%" />

                                    <asp:BoundField HeaderText="Payment request Date" DataField="PaymentReqDate" ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="left" ItemStyle-BorderColor="Navy" ItemStyle-Width="8%" />

                                    <asp:BoundField HeaderText="Amount to be paid" DataField="Amt_paid_Account" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right" ItemStyle-BorderColor="Navy" ItemStyle-Width="8%" />

                                    <asp:BoundField HeaderText="Payment request approved by" DataField="PaymentApproverName" ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="left" ItemStyle-BorderColor="Navy" ItemStyle-Width="10%" />

                                    <asp:BoundField HeaderText="Invoice No" DataField="InvoiceNo" ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="left" ItemStyle-BorderColor="Navy" ItemStyle-Width="8%" />

                                    <asp:BoundField HeaderText="Invoice Amount" DataField="InvoiceAmt" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right" ItemStyle-BorderColor="Navy" ItemStyle-Width="8%" />

                                    <asp:BoundField HeaderText="Invoice approved by" DataField="InvoiceApproverName" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderColor="Navy" ItemStyle-Width="13%" />

                                    <asp:BoundField HeaderText="Milestone Particular" DataField="MilestoneParticular" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderColor="Navy" ItemStyle-Width="15%" />

                                </Columns>
                            </asp:GridView>
                        </li>



                    </ul>
                </div>

                <div class="trvl_Savebtndiv">
                    <span>
                        <asp:LinkButton ID="btnCancel" runat="server" Text="Send Payment Advice" ToolTip="Send Payment Advice to Vednor" CssClass="Savebtnsve" OnClientClick="return CancelMultiClick();" OnClick="btnCancel_Click">Send Payment Advice</asp:LinkButton>
                    </span>
                    <asp:LinkButton ID="btnback_mng" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/VSCB_AssignBankRefApprovedbatch.aspx">Back</asp:LinkButton>
                </div>
                <br />
                <br />
                <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
                <asp:TextBox AutoComplete="off" ID="txtEmpName" runat="server" Visible="false"></asp:TextBox>
                <asp:HiddenField ID="hdnPOWOID" runat="server" />
                <asp:HiddenField ID="hdnMilestoneId" runat="server" />
                <asp:HiddenField ID="hdnInvoiceId" runat="server" />
                <asp:HiddenField ID="HDPaymentRelDate" runat="server" />

                <asp:HiddenField ID="hflEmpName" runat="server" />
                <asp:HiddenField ID="hflEmpDesignation" runat="server" />
                <asp:HiddenField ID="hflEmpDepartment" runat="server" />
                <asp:HiddenField ID="hflEmailAddress" runat="server" />
                <asp:HiddenField ID="hflGrade" runat="server" />
                <asp:HiddenField ID="hdnYesNo" runat="server" />
                <asp:HiddenField ID="hdncurId" runat="server" />


            </div>
        </div>
    </div>

    
    <%--  <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />--%>

    <script src="../js/freeze/jquery-ui.min.js"></script>
    <script src="../js/freeze/gridviewScroll.min.js"></script>
    <link href="../includes/loader.css" rel="stylesheet" />

    <script type="text/javascript">      
        $(document).ready(function () {
            $(".DropdownListSearch").select2();

            $('#MainContent_gvMngTravelRqstList').gridviewScroll({
                width: 1060,
                height: 600,
                freezesize: 4, // Freeze Number of Columns.
                headerrowcount: 1, //Freeze Number of Rows with Header.
            });

            $('#MainContent_gvMngPaymentList_Batch').gridviewScroll({
                width: 1060,
                height: 600,
                freezesize: 3, // Freeze Number of Columns.
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

        function CancelMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=btnCancel.ClientID%>');

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
            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return;

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


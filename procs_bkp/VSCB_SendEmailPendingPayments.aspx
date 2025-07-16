<%--<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="VSCB_SendEmailPendingPayments.aspx.cs" Inherits="VSCB_SendEmailPendingPayments" %>--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" EnableViewState="true"
    MaintainScrollPositionOnPostback="true" CodeFile="VSCB_SendEmailPendingPayments.aspx.cs" Inherits="VSCB_SendEmailPendingPayments" EnableSessionState="True" ValidateRequest="false" %>


<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />
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


    <script src="../js/freeze/jquery-1.11.0.min.js"></script>
   <%-- <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />--%>

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
                        <asp:Label ID="lblheading" runat="server" Text="Pending Payment Requests List"></asp:Label>
                    </span>
                </div>

                <span>
                    <a href="vscb_index.aspx" class="aaaa"><%=System.Configuration.ConfigurationManager.AppSettings["VSCBPageTitle"]%></a>
                </span>

                <span>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                </span>

                <div class="edit-contact">
                    <ul id="editform" runat="server">

                        <li style="padding-top: 30px">
                            <span>PO/ WO Number.</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" ID="lstPOWONo" CssClass="DropdownListSearch" AutoPostBack="true" OnSelectedIndexChanged="lstPOWONo_SelectedIndexChanged">
                            </asp:DropDownList>
                        </li>
                        <li style="padding-top: 30px">
                            <span>PO/ WO Date</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstPOWODate"  AutoPostBack="true" OnSelectedIndexChanged="lstPOWONo_SelectedIndexChanged">
                            </asp:DropDownList>
                        </li>
                        <li style="padding-top: 30px">
                            <span>Vendor Name</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" ID="lstVendorName" CssClass="DropdownListSearch"  AutoPostBack="true" OnSelectedIndexChanged="lstPOWONo_SelectedIndexChanged">
                            </asp:DropDownList>
                        </li>

                        <li style="padding-top: 15px">
                            <span>Invoice No</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstInvoiceNo"  AutoPostBack="true" OnSelectedIndexChanged="lstPOWONo_SelectedIndexChanged">
                            </asp:DropDownList>
                        </li>
                        <li style="padding-top: 15px; display: none;">
                            <span>Invoice Date</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstpInvoiceDate"  AutoPostBack="true" OnSelectedIndexChanged="lstPOWONo_SelectedIndexChanged">
                            </asp:DropDownList>
                        </li>
                        <li style="padding-top: 15px; display: none;">
                            <span>Status</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstStatus"  AutoPostBack="true" OnSelectedIndexChanged="lstPOWONo_SelectedIndexChanged">
                            </asp:DropDownList>
                        </li>

                        <li style="padding-top: 15px">
                            <span>Cost Center</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstcostCenter"  AutoPostBack="true" OnSelectedIndexChanged="lstPOWONo_SelectedIndexChanged">
                            </asp:DropDownList>

                            <span style="display: none">Project/Department</span>&nbsp;&nbsp;
                            
                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstDepartment" Visible="false">
                            </asp:DropDownList>
                        </li>
                        <li style="padding-top: 15px"></li>
                        <li style="padding-top: 15px"></li>

                        <li style="padding-top: 15px"></li>

                        <%--<li style="padding-top: 15px"></li>--%>
                    </ul>

                    <div class="mobile_Savebtndiv" style="margin-top: 20px !important">
                           <asp:LinkButton ID="trvl_btnSave" Visible="false" runat="server" Text="Send Email to Approver" ToolTip="Send E-mail" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();" OnClick="trvl_btnSave_Click"></asp:LinkButton>
                        
                        <asp:LinkButton ID="mobile_btnSave"   Visible="false" runat="server" Text="Submit" ToolTip="Search" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click">Search </asp:LinkButton>
                        <asp:LinkButton ID="mobile_btnBack" runat="server" Text="Clear Search" OnClick="mobile_btnBack_Click" ToolTip="Clear Search" class="trvl_Savebtndiv">Clear Search</asp:LinkButton>

                       
                          

                    </div>


                    <asp:Label runat="server" ID="RecordCount" Style="color: red; font-size: 14px;"></asp:Label>

                    <asp:GridView ID="gvMngTravelRqstList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                        DataKeyNames="InvoiceID" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false" PageSize="10" AllowPaging="false" OnPageIndexChanging="gvMngTravelRqstList_PageIndexChanging">
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                        <PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" />
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                        <PagerStyle HorizontalAlign="Right" CssClass="paging" />
                        <Columns>


                            <asp:BoundField HeaderText="PO No"
                                DataField="PONumber"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-BorderColor="Navy" ItemStyle-Width="15%" />

                            <asp:BoundField HeaderText="PO created by"
                                DataField="POCreated_By_Name"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="13%" ItemStyle-BorderColor="Navy" />

                            <asp:BoundField HeaderText="Cost Center"
                                DataField="CostCenter"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                            <asp:BoundField HeaderText="Vendor Name"
                                DataField="VendorName"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                            <asp:BoundField HeaderText="Resource Name"
                                DataField="ResourceName"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />


                            <asp:BoundField HeaderText="Milestone Description"
                                DataField="MilestoneParticular"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="20%" ItemStyle-BorderColor="Navy" />

                            <asp:BoundField HeaderText="Milestone Due date"
                                DataField="Milestone_due_date"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />

                            <asp:BoundField HeaderText="Invoice No"
                                DataField="InvoiceNo"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                            <asp:BoundField HeaderText="Invoice Date"
                                DataField="InvoiceDate"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" Visible="false" />

                            <asp:BoundField HeaderText="Payment Request No"
                                DataField="PaymentReqNo"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="20%" ItemStyle-BorderColor="Navy" />

                            <asp:BoundField HeaderText="Payment Request Date"
                                DataField="PaymentReqDate"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="8%" ItemStyle-BorderColor="Navy" />

                            <asp:BoundField HeaderText="Payment Request Creation"
                                DataField="PaymentCreatedBy"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                            <asp:BoundField HeaderText="Approver 1"
                                DataField="Approver1"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="20%" ItemStyle-BorderColor="Navy" />

                            <asp:BoundField HeaderText="Approver 2"
                                DataField="Approver2"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="20%" ItemStyle-BorderColor="Navy" />


                            <asp:BoundField HeaderText="Approver 3"
                                DataField="Approver3"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="20%" ItemStyle-BorderColor="Navy" />


                            <asp:BoundField HeaderText="Approver 4"
                                DataField="Approver4"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="20%" ItemStyle-BorderColor="Navy" />

                            <asp:BoundField HeaderText="Approver 4"
                                DataField="Approver5"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="20%" ItemStyle-BorderColor="Navy" />

                        </Columns>
                    </asp:GridView>


                    <asp:GridView ID="gv_emailAddress" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                        CellPadding="3" AutoGenerateColumns="False" Width="50%" EditRowStyle-Wrap="false" AllowPaging="false" Visible="false">
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                        <PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" />
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                        <PagerStyle HorizontalAlign="Right" CssClass="paging" />
                        <Columns>


                            <asp:BoundField HeaderText="Emailaddress"
                                DataField="Emp_Emailaddress"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-BorderColor="Navy" ItemStyle-Width="50%" />


                        </Columns>
                    </asp:GridView>

                    <br />
                    <br />
                    <br />
                </div>




                <div class="edit-contact" style="background-color: yellow">
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server"></asp:LinkButton>
                        </div>
                    </div>

                </div>
                <asp:HiddenField ID="hdnPOWOID" runat="server" />
                <asp:HiddenField ID="hdnMilestoneId" runat="server" />
                <asp:HiddenField ID="hdnInvoiceId" runat="server" />
                <asp:HiddenField ID="hdnEmpCode" runat="server" />

            </div>
        </div>
    </div>

    <asp:HiddenField ID="hdnYesNo" runat="server" />


    <script src="../js/freeze/jquery-ui.min.js"></script>
    <script src="../js/freeze/gridviewScroll.min.js"></script> 

     <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />  
    <link href="../includes/loader.css" rel="stylesheet" />
    <script type="text/javascript">


        $(document).ready(function () {
            $(".DropdownListSearch").select2();

            $('#MainContent_gvMngTravelRqstList').gridviewScroll({
                width: 1070,
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

                
                //if (Page_ClientValidate()) {	
				$('#loader').show();
			   // $('#loader').fadeOut(60000);
			//}


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

    </script>
</asp:Content>


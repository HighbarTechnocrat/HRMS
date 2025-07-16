<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="VSCB_InvoiceAppMatrixList.aspx.cs" Inherits="VSCB_InvoiceAppMatrixList" %>

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
            background: #dae1ed;
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
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>


    <script src="../js/freeze/jquery-1.11.0.min.js"></script>
	 <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" /> 

    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">

                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Invoice Approval Matrix"></asp:Label>
                    </span>
                </div>

                <span>
                     <a href="vscb_index.aspx" class="aaaa"><%=System.Configuration.ConfigurationManager.AppSettings["VSCBPageTitle"]%></a>
                </span>

                <span>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                </span>

                <div class="edit-contact" id="divSearchCostCenter" runat="server">
                    <ul id="editform" runat="server">
                          <li style="padding-top: 10px">
                             <span>PO/ WO Type</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" ID="lstPOType" CssClass="DropdownListSearch" AutoPostBack="true"  OnSelectedIndexChanged="lstCostCenter_SelectedIndexChanged"></asp:DropDownList>
                        </li> 
                        <li style="padding-top: 10px">
                             <span>Cost Center</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" ID="lstCostCenter" CssClass="DropdownListSearch" AutoPostBack="true" OnSelectedIndexChanged="lstCostCenter_SelectedIndexChanged"></asp:DropDownList>
                        </li> 
                        
                    </ul>
                </div>


                <div class="mobile_Savebtndiv" style="margin-top: 20px !important" id="divSeachButtons" runat="server">
                    <asp:LinkButton ID="mobile_btnSave" runat="server" Text="Submit" ToolTip="Search" Visible="false" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click">Search </asp:LinkButton>
                    <asp:LinkButton ID="mobile_btnBack" runat="server" Text="Clear Search" OnClick="mobile_btnBack_Click" ToolTip="Clear Search" class="trvl_Savebtndiv">Clear Search</asp:LinkButton>
                </div>

              <%--  <div class="manage_grid" style="overflow: scroll; width: 100%; padding-top: 20px; margin-bottom: 30px;">--%>

                    <asp:Label runat="server" ID="RecordCount" Style="color: red; font-size: 14px;"></asp:Label>

                    <asp:GridView ID="gvMngTravelRqstList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                    DataKeyNames="Tallycode,CostCentre,department,POTypeID" CellPadding ="3" AutoGenerateColumns="False" Width="120%" EditRowStyle-Wrap="false" PageSize="20" AllowPaging="True" OnPageIndexChanging="gvMngTravelRqstList_PageIndexChanging">
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

                            <asp:TemplateField ItemStyle-Width="3%" HeaderText="Details" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkLeaveDetails" runat="server" Text='View' OnClick="lnkLeaveDetails_Click" CssClass="BtnShow">
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                           <asp:BoundField HeaderText="PO/ WO Type"
                                DataField="POType"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />


                            <asp:BoundField HeaderText="Cost Center"
                                DataField="CostCentre"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                            <asp:BoundField HeaderText="Department"
                                DataField="department"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="14%" ItemStyle-BorderColor="Navy" />

                            <asp:BoundField HeaderText="Approver Level 1"
                                DataField="APPR_Emp_Code1"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="20%" ItemStyle-BorderColor="Navy" />

                            <asp:BoundField HeaderText="Approver Level 2"
                                DataField="APPR_Emp_Code2"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="20%" ItemStyle-BorderColor="Navy" />

                            <asp:BoundField HeaderText="Approver Level 3"
                                DataField="APPR_Emp_Code3"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="20%" ItemStyle-BorderColor="Navy" />

                            <asp:BoundField HeaderText="Approver Level 4"
                                DataField="APPR_Emp_Code4"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="20%" ItemStyle-BorderColor="Navy" />
 

                        </Columns>
                    </asp:GridView>

                
                
                 <div class="edit-contact" id="divApprovers" runat="server" visible="false"> 
                    <ul id="ulApprovers" runat="server">
                         <li style="padding-top: 10px">                            
                              <span>PO/ WO Type</span>&nbsp;&nbsp;
                             <br />
                             <asp:TextBox AutoComplete="off" ID="txtPOType" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
 
                        <li style="padding-top: 10px">
                             <span>Cost Center</span>&nbsp;&nbsp;
                             <br />
                             <asp:TextBox AutoComplete="off" ID="txtCostCenter" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                             
                        </li>
                         <li style="padding-top: 10px">
                             <span>Department</span>&nbsp;&nbsp;
                             <br />
                            <asp:TextBox AutoComplete="off" ID="txtProject" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                           
                        </li>
                       



                          <li style="padding-top: 10px">
                             <span>Approver Level 1</span>&nbsp;&nbsp;
                             <br />
                            <asp:DropDownList runat="server" ID="lstAppr1" CssClass="DropdownListSearch"></asp:DropDownList>
                            <br />
                            <asp:CheckBox id="chkApprover1" runat="server" Visible="true" Text="Is Selected"></asp:CheckBox>
                        </li>
                        <li style="padding-top: 10px">
                              <span>Approver Level 2</span>&nbsp;&nbsp;
                             <br />
                         <asp:DropDownList runat="server" ID="lstAppr2" CssClass="DropdownListSearch"></asp:DropDownList>
                             <br />
                            <asp:CheckBox id="chkApprover2" runat="server" Visible="true" Text="Is Selected"></asp:CheckBox>
                        </li>
                        <li style="padding-top: 10px">
                            
                        </li>
                        <li style="padding-top: 10px">
                           <span>Approver Level 3</span>&nbsp;&nbsp;
                             <br />
                         <asp:DropDownList runat="server" ID="lstAppr3" CssClass="DropdownListSearch"></asp:DropDownList>
                             <br />
                            <asp:CheckBox id="chkApprover3" runat="server" Visible="true" Text="Is Selected"></asp:CheckBox>
                        </li>
                        <li style="padding-top: 10px">
                                <span>Approver Level 4</span>&nbsp;&nbsp;
                             <br />
                         <asp:DropDownList runat="server" ID="lstAppr4" CssClass="DropdownListSearch"></asp:DropDownList>
                             <br />
                            <asp:CheckBox id="chkApprover4" runat="server" Visible="true" Text="Is Selected"></asp:CheckBox>
                         </li>
                    </ul>

                       <div class="trvl_Savebtndiv">
                        <asp:LinkButton ID="trvl_btnSave" runat="server" Text="Submit" ToolTip="Submit" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();" OnClick="trvl_btnSave_Click" >Submit</asp:LinkButton>
                      
                        <asp:LinkButton ID="btnback_mng" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="btnback_mng_Click">Back</asp:LinkButton>
                    </div>

                </div>
                
                
                <br /><br /><br />
           

                <div class="edit-contact"> 
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" OnClick="lnkcont_Click"></asp:LinkButton>
                        </div>
                    </div>

                </div>
                <asp:HiddenField ID="hdnPOWOID" runat="server" />
                <asp:HiddenField ID="hdnMilestoneId" runat="server" />
                <asp:HiddenField ID="hdnInvoiceId" runat="server" />
                <asp:HiddenField ID="hdnEmpCode" runat="server" />

                <asp:HiddenField ID="hdnCostCenter" runat="server" />
                <asp:HiddenField ID="hdnTallycode" runat="server" /> 
                <asp:HiddenField ID="hdn_OldEmp_Approver_Code" runat="server" />
                <asp:HiddenField ID="hdnPOTypeID" runat="server" />
               <asp:HiddenField ID="hdnFisrtAppr_Code" runat="server" />
                <asp:HiddenField ID="hdnSecondAppr_Code" runat="server" />
                <asp:HiddenField ID="hdnThirdAppr_Code" runat="server" />
                <asp:HiddenField ID="hdnFourthAppr_Code" runat="server" />

               <asp:HiddenField ID="hdnFisrtAppr_ID" runat="server" />
                <asp:HiddenField ID="hdnSecondAppr_ID" runat="server" />
                <asp:HiddenField ID="hdnThirdAppr_ID" runat="server" />
                <asp:HiddenField ID="hdnFourthAppr_ID" runat="server" />


            </div>
        </div>
    </div>



    <%--    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />--%>

    <script src="../js/freeze/jquery-ui.min.js"></script>
    <script src="../js/freeze/gridviewScroll.min.js"></script>

    <script type="text/javascript">

       
       $(document).ready(function ()
        {
            $(".DropdownListSearch").select2();

            	$('#MainContent_gvMngTravelRqstList').gridviewScroll({
                width: 1070,
                height: 600,
                freezesize:3, // Freeze Number of Columns.
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

    </script>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" EnableViewState="true"
    MaintainScrollPositionOnPostback="true" CodeFile="ABAP_Object_Tracker_HBT_Test_Cases_Index.aspx.cs" Inherits="ABAP_Object_Tracker_HBT_Test_Cases_Index" EnableSessionState="True" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Req_Requisition.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />

    <style>
        .Dropdown {
            border-bottom: 2px solid #cccccc;
            background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;
            cursor: default;
        }

        #MainContent_lnk_Index,
        #MainContent_lnk_Index {
            float: right;
            background: #3D1956;
            color: #febf39 !important;
            padding: 0.5% 1.4%;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>

    <script src="../js/freeze/jquery-1.11.0.min.js"></script>


    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <br />
                        <asp:Label ID="lblheading" runat="server" Text="HBT Test Cases Approval List"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>
                
                <asp:LinkButton ID="lnk_Index" runat="server" Visible="true" PostBackUrl="~/procs/ABAP_Object_Tracker_Index.aspx">Back</asp:LinkButton>


                <asp:LinkButton ID="localtrvl_delete_btn" runat="server"
                    CssClass="Savebtnsve" Style="display: none"></asp:LinkButton>

               <%-- <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderIRSheet" runat="server"
                    BackgroundCssClass="modalBackground" TargetControlID="localtrvl_delete_btn" PopupControlID="PnlIrSheet" RepositionMode="RepositionOnWindowResizeAndScroll"
                    OnOkScript="ok()" CancelControlID="LinkBtnBackPopup" />--%>


               <%-- <asp:Panel ID="PnlIrSheet" runat="server" Style="display: none; position: fixed; top: 20%; left: 20%; transform: translate(-20%, -30%,-30%); width: 70%; padding: 20px; background-color: white; border: 1px solid #ccc; z-index: 1000;"
                    Height="60%">--%>
                <asp:Panel ID="PnlIrSheet" runat="server" CssClass="IRmodalPopup" Style="display: none" Height="400px">
		        	<div id="Div2" runat="server" style="max-height: 500px; overflow: auto;">
                       <div class="edit-contact">
                            <div style="padding-left: 200px">
                                <asp:Label runat="server" ID="lblmessagesub" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                            </div>
                            <div class="userposts">
                                <span>
                                    <asp:Label ID="Label1" runat="server" Text="HBT Testing Uplaod Test Cases & Update Status"></asp:Label>
                                </span>
                            </div>
                            <br />
                            <ul runat="server" id="Ul1" >
                                <li class="trvl_date"> 
                                    <span>Consultant</span>
                                    <asp:TextBox AutoComplete="off" ID="txtConsultant" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                                     </li>
                                <li class="trvl_date">
                                    <span>Interface</span>
                                    <asp:TextBox AutoComplete="off" ID="txtInterface" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>

                                </li>
                                <li class="trvl_date">
                                    <span>Dev Desc</span>
                                    <asp:TextBox AutoComplete="off" ID="txtDevDesc" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                                 </li>
                                <li class="trvl_date">
                                    <span">Frice Category</span>
                                    <asp:TextBox AutoComplete="off" ID="txtFCategory" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>

                                </li>
                                <li class="trvl_date">
                                    <span">Module </span>
                                    <asp:TextBox AutoComplete="off" ID="txtModule" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                                </li>
                                <li class="trvl_date">
                                    <span>Priotity/ Order </span>
                                    <asp:TextBox AutoComplete="off" ID="txtPriorityOrder" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                                </li>

                               <li class="trvl_date">
                                    <span">Priority</span>
                                    <asp:TextBox AutoComplete="off" ID="txtPriority" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>

                                </li>
                                <li class="trvl_date">
                                    <span>Complexity</span>
                                    <asp:TextBox AutoComplete="off" ID="txtComplexity" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>

                                </li>
                                <li class="trvl_date">
                                    <span>Planned Start Date</span>
                                    <asp:TextBox AutoComplete="off" ID="txtPlannedStart" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>

                                </li>

                                <li class="trvl_date">
                                    <span>Planned Finish Date</span>
                                    <asp:TextBox AutoComplete="off" ID="txtPlannedFinish" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>

                                </li>
                                <li class="trvl_date">
                                    <span>Revised Start </span>
                                    <asp:TextBox AutoComplete="off" ID="txtRevisedStart" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>

                                </li>
                                <li class="trvl_date">
                                    <span>Revised Finish</span>
                                    <asp:TextBox AutoComplete="off" ID="txtRevisedFinish" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>

                                </li>
                                <li class="trvl_date">
                                    <span>Actual Start </span>
                                    <asp:TextBox AutoComplete="off" ID="txtActualStart" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>

                                </li>
                                <li class="trvl_date">
                                    <span>Actual Finish</span>
                                    <asp:TextBox AutoComplete="off" ID="txtActualFinish" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>

                                </li>
                                <li class="trvl_date">
                                    <span>Remark</span>
                                    <asp:TextBox AutoComplete="off" ID="txtRemark" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>

                                </li>
                                <li class="trvl_date">
                                    <span>HBT Test Status</span>
                                    <asp:TextBox AutoComplete="off" ID="txtHBTStatus" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>

                                </li>
                                <li class="trvl_date">
                                    <span style="width:80%">Download Test Case</span>
                                    <asp:ImageButton ID="lnkIRsheetExport" runat="server" Width="20px" ToolTip="Download Test Cases" Height="15px" CommandArgument='<%# Eval("TestCaseFileAttached") %>' OnClick="ibdownloadbtn_Click" ImageUrl="~/Images/Download.png" />
                                </li>
                                <li></li>
                                <li class="trvl_date">
                                    <span">Approval Remark</span>
                                    <asp:Label runat="server" ID="lblRemarkError" />
                                    <asp:TextBox AutoComplete="off" ID="TxtApprovalRemark" runat="server"></asp:TextBox>

                                </li>
                                <li></li>
                                <li></li>
                                <li>
                                    <asp:Label runat="server" ID="lblSubmitError" />
                                    <span>
                                        <asp:LinkButton ID="trvl_btnSave" runat="server" Text="Submit" ToolTip="Approve" CssClass="Savebtnsve" OnClick="lnkbtnclick_ApproveHbtTestCases">Approve</asp:LinkButton>
                                    </span>
                                </li>
                                 <li>
                                    <span>
                                        <asp:LinkButton ID="lnk_SendforCorrection" runat="server" Text="Submit" ToolTip="Send for Correction" CssClass="Savebtnsve" onclick="lnkbtnclick_SendForCottectHbtTestCases">Send for Correction</asp:LinkButton>
                                    </span>
                                </li>
                                <li>
                                    <span>
                                        <asp:LinkButton ID="btnback_mng" runat="server" Text="Back" ToolTip="Back" OnClick="LinkBtnBackPopup_Click">Back</asp:LinkButton>
                                    </span>
                                </li>
                                <li></li>
                            </ul>
                        </div>
                    </div>
                </asp:Panel>

                <div class="edit-contact">

                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" Visible="false"></asp:LinkButton>
                        </div>

                    </div>

                    <ul id="editform1" runat="server">
                        <li class="trvl_grid" id="lifsdetails" runat="server">
                            <br />
                            <br />
                            <asp:Label runat="server" ID="lbl_error" Visible="True" Style="color: red; text-align: center;"></asp:Label>
                            <br />
                            <br />

                            <asp:GridView ID="gvHBTTestDetails" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                AutoGenerateColumns="False" Width="110%" EditRowStyle-Wrap="true" DataKeyNames="HBTDetailsId" ShowHeaderWhenEmpty="true" OnRowDataBound="GridView1_RowDataBound">
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
                                    <asp:TemplateField ItemStyle-Width="6%">
                                        <HeaderTemplate>
                                            View
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkEdit" runat="server" Height="15px" OnClick="lnkEdit_Click" ImageUrl="~/Images/edit.png" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:BoundField HeaderText="Project/Location"
                                        DataField="Location_name"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="14%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Consultant"
                                        DataField="ConsultantName"
                                        ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="14%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Development Description"
                                        DataField="Development_Desc"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="15%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Module"
                                        DataField="ModuleDesc"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Interface"
                                        DataField="Interface"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="FRICE Category"
                                        DataField="FCategoryName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Priority/ Order"
                                        DataField="Priority"
                                        ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="7%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Priority"
                                        DataField="PriorityName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="7%" ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Complexity"
                                        DataField="ComplexityName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="7%" ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Start Date"
                                        DataField="HBTPlannedStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Planned Finish Date"
                                        DataField="HBTPlannedFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Revised Start"
                                        DataField="RevisedStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Revised Finish"
                                        DataField="RevisedFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Actual Start"
                                        DataField="ActualStart"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Actual Finish"
                                        DataField="ActualFinish"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Reason"
                                        DataField="Reason"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="10%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="HBT Testing Status"
                                        DataField="StatusName"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="10%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="File Name"
                                        DataField="TestCaseFileAttached"
                                        ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:TemplateField HeaderText="Download Test Cases" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%" Visible ="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkIRsheetExport" runat="server" Width="20px" ToolTip="Download Test Cases" Height="15px" CommandArgument='<%# Eval("TestCaseFileAttached") %>' OnClick="ibdownloadbtn_Click" ImageUrl="~/Images/Download.png" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField >

                                </Columns>
                                <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                            </asp:GridView>


                        </li>
                        <li></li>


                        <li></li>
                    </ul>

                </div>
            </div>
        </div>
    </div>



    <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
    <asp:TextBox AutoComplete="off" ID="txtIsSecurity_DepositInvoice" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
    <asp:TextBox AutoComplete="off" ID="txtEmpName" runat="server" Visible="false"></asp:TextBox>
    <asp:Label ID="testsanjay" runat="server" Visible="false"></asp:Label>
    <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
    <br />

    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderIRSheet" runat="server"
			TargetControlID="localtrvl_delete_btn" PopupControlID="PnlIrSheet" RepositionMode="RepositionOnWindowResizeAndScroll"
			BackgroundCssClass="modalBackground" DropShadow="true" OkControlID="Oth_btnDelete1"
			OnOkScript="ok()" CancelControlID="btBack" />


    <asp:HiddenField ID="hdnHBTDetailId" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />



    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <script src="../js/freeze/jquery-ui.min.js"></script>
    <script src="../js/freeze/gridviewScroll.min.js"></script>
    <script type="text/javascript">


        $(document).ready(function () {
            function applyGridViewScroll(selector, options) {
                $(selector).gridviewScroll(options);
            }
            $("#MainContent_DDLProjectLocation").select2();
            $(".DropdownListSearch").select2();
            const gridOptions = {
                width: 1070,
                height: 600,
                freezesize: 3,
                headerrowcount: 1
            };

            applyGridViewScroll('#MainContent_gvHBTTestDetails', gridOptions);
        });



        function openModal(hbtDetailsId) {
            debugger;


            document.getElementById("myModal").style.display = "block";
            document.getElementById("overlay").style.display = "block";
        }

        function closeModal() {
            // Hide the modal and overlay
            document.getElementById("myModal").style.display = "none";
            document.getElementById("overlay").style.display = "none";
        }





        function SaveMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=btnback_mng.ClientID%>');

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


    </script>
</asp:Content>

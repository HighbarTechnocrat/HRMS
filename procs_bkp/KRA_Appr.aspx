<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" EnableViewState="true"
    MaintainScrollPositionOnPostback="true" CodeFile="KRA_Appr.aspx.cs" Inherits="KRA_Appr" EnableSessionState="True" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

 
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
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

        a#MainContent_btnback_mng {
            margin: 25px 0 0 0 !important;
        }


        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
            background: #dae1ed;
        }

        .Dropdown {
            border-bottom: 2px solid #cccccc;
            /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
            background: url('../images/arrowdown.png') no-repeat right center;
            cursor: default;
        }

        table {
            background-color: white;
            border-color: navy;
            border-width: 1px;
            border-style: solid;
            border-collapse: collapse
        }

        .tdbackcolor {
            color: #3D1956;
            background-color: #C7D3D4;
            font-weight: bold;
        }

        .tdcolor {
            color: #000066;
        }




        table, th, td {
            border: 1px solid black;
            border-collapse: collapse;
        }

        th, td {
            padding: -1px;
        }

        .tdstyle {
            text-align: left;
            height: 110px;
            vertical-align: text-top;
        }

            td {
    vertical-align: top;
}
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>

    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Review KRA"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>
                <div>
                    <span>
                        <a href="KRA_index.aspx" class="aaaa">KRA Home</a>
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
                            <span>Employee Code</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtemp_code" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_type">
                            <span>Employee Name</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtEmployee_name" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>                            
                            <asp:TextBox AutoComplete="off" ID="txtEmp_email" runat="server" ReadOnly="true" Enabled="False" Visible="false"></asp:TextBox>
                        </li>
                        <li class="trvl_type">
                               <span>Period</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPeriod" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>


                        <li class="trvl_date">
                              <span>Location</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtLocation" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>                        
                        </li>
                        <li class="trvl_date">
                            <span>Department</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtDepartment" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                                 <span>Designation</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPosition" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                         

                        <li class="trvl_date">
                            <span>From Date</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtKRAFromDt" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>To Date</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtKTATodt" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                             <span>KRA Submitted Date</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtKRA_SubmitDt" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>

                         

                        <li class="trvl_grid" id="litrvlgrid" runat="server">
                            <div class="manage_grid" style="overflow: scroll; width: 100%; padding-top: 20px; margin-bottom: 30px;">


                                  <asp:GridView ID="dgKRA_Details" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                                         DataKeyNames="KRA_ID,Goal_Id" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false" OnDataBound="dgKRA_Details_DataBound" OnRowDataBound="dgKRA_Details_RowDataBound">
                                      
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
                                            <asp:BoundField HeaderText="Sr.No"
                                            DataField="goal_seq_no"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="1%" ItemStyle-BorderColor="Navy" />  

                                          <asp:TemplateField  HeaderText="Goal Title" ItemStyle-Width="13%">
                                            <ItemTemplate> 
                                               <asp:Label ID="lblGoalTitle" runat="server" 
                                                Text='<%# HttpUtility.HtmlDecode(Eval("Goal_title").ToString()) %>'>
                                                </asp:Label>        
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                           <asp:BoundField HeaderText="Weightage"
                                            DataField="Weightage"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="1%" ItemStyle-BorderColor="Navy" />   


                                         <asp:TemplateField  HeaderText="Measurement Details" ItemStyle-Width="23%">
                                            <ItemTemplate> 
                                               <asp:Label ID="lblMeasurements_details" runat="server" 
                                                Text='<%# HttpUtility.HtmlDecode(Eval("Measurement_Details").ToString()) %>'>
                                                </asp:Label>        
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                         <asp:BoundField HeaderText="Unit"
                                            DataField="unit_short_name"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="1%" ItemStyle-BorderColor="Navy" />  

                                         <asp:BoundField HeaderText="Quantity"
                                            DataField="Quantity"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="1%" ItemStyle-BorderColor="Navy" />   
                                          

                                            <asp:BoundField HeaderText="Remarks"
                                            DataField="remarks"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" Visible="false" />   

                                    </Columns>
                                </asp:GridView>
                                 

                            </div>
                        </li>


                        <li class="trvl_local">
                            <span id="spnFileupload" runat="server" >Upload File</span>
                            <asp:FileUpload ID="uploadFile_KRA" runat="server" AllowMultiple="false" Visible="false"></asp:FileUpload>
                            <br />
                            <asp:LinkButton ID="lnk_download_kra_file" runat="server" OnClick="lnk_download_kra_file_Click" CssClass="BtnShow" Visible="false"></asp:LinkButton>
                        </li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>


                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>

                        <li class="trvl_local">
                            <span>Remakrs</span><span style="color: red; font-size: 10px; font-weight: normal; font-style: italic;"> Maximum 100 Characters</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_remakrs" runat="server" TextMode="MultiLine" MaxLength="200"></asp:TextBox>
                        </li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>



                        <li class="trvl_Approver">
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
                                    <asp:BoundField HeaderText="Reviewer Name"
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

                                    <asp:BoundField HeaderText="Reviewed on"
                                        DataField="approved_on"
                                        ItemStyle-HorizontalAlign="left"
                                        ItemStyle-Width="15%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Reviewer Remarks"
                                        DataField="Remarks"
                                        ItemStyle-HorizontalAlign="left"
                                        ItemStyle-Width="37%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="APPR_ID"
                                        DataField="approver_id"
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
                                        DataField="Approver_emp_code"
                                        ItemStyle-HorizontalAlign="center"
                                        ItemStyle-Width="1%"
                                        ItemStyle-BorderColor="Navy"
                                        Visible="false" />
                                </Columns>
                            </asp:GridView>
                        </li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>


                        <li class="trvl_local"></li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>


                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="trvl_Savebtndiv">
        <asp:LinkButton ID="trvl_btnSave" runat="server" Text="Approve" ToolTip="Approve" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();" OnClick="trvl_btnSave_Click">Approve</asp:LinkButton>
        <asp:LinkButton ID="btnCancel" Visible="false" runat="server" Text="Send for Correction" ToolTip="Send for Correction" CssClass="Savebtnsve" OnClientClick="return CancelMultiClick();" OnClick="btnCancel_Click">Send for Correction</asp:LinkButton>
        <asp:LinkButton ID="btnback_mng" runat="server" Text="Download KRA" ToolTip="Download KRA" CssClass="Savebtnsve" OnClick="btnback_mng_Click">Download KRA</asp:LinkButton>
    </div>

    <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
    <asp:TextBox AutoComplete="off" ID="txtEmpName" runat="server" Visible="false"></asp:TextBox>
    <asp:Label ID="testsanjay" runat="server" Visible="false"></asp:Label>
    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />
 
         <%-- <rsweb:reportviewer id="ReportViewer2" runat="server">
          </rsweb:reportviewer>--%>


    <asp:HiddenField ID="FilePath" runat="server" />
    <asp:HiddenField ID="hdngoal_id" runat="server" />
    <asp:HiddenField ID="hdnMeasurement_id" runat="server" />
    <asp:HiddenField ID="hdnKRA_id" runat="server" />
    <asp:HiddenField ID="hdn_flg_goal" runat="server" />
    <asp:HiddenField ID="hdn_flg_measurement" runat="server" />
    <asp:HiddenField ID="hdnPeriod_id" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="hdnstype_Main" runat="server" />

    <asp:HiddenField ID="hdnemplogin_type" runat="server" />

    <asp:HiddenField ID="hdncurrent_Appr_Name" runat="server" />
    <asp:HiddenField ID="hdncurrent_Appr_Empcode" runat="server" />
        <asp:HiddenField ID="hdncurrent_Appr_EmpEmail" runat="server" />
    <asp:HiddenField ID="hdncurrent_Appr_Id" runat="server" />


    <asp:HiddenField ID="hdnNext_Appr_Name" runat="server" />
    <asp:HiddenField ID="hdnNext_Appr_Empcode" runat="server" />
      <asp:HiddenField ID="hdnNext_Appr_EmpEmail" runat="server" />
    <asp:HiddenField ID="hdnNext_Appr_Id" runat="server" />


    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <script type="text/javascript">

        $(document).ready(function () {
            $(".DropdownListSearch").select2();
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

    </script>
</asp:Content>

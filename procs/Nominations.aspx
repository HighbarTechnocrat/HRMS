<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="Nominations.aspx.cs" Inherits="Nominations" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/fuel_RemRequest_css.css" type="text/css" media="all" />
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


        /* styles.css */

        body {
            font-family: Arial, sans-serif;
            padding: 20px;
            background-color: #f4f4f4;
        }

        .table-wrapper {
            /* overflow-x: auto;*/
            -webkit-overflow-scrolling: touch;
            margin: 6px 22px 0 135px !important;
        }

        .responsive-table {
            width: 85% !important;
            border-collapse: collapse;
            margin-bottom: 20px;
        }

            .responsive-table th, .responsive-table td {
                padding: 2px;
                text-align: left;
                border-bottom: 1px solid #ddd;
            }

            .responsive-table th {
                background-color: #f2f2f2;
            }

        @media screen and (max-width: 600px) {
            .responsive-table thead {
                display: none;
            }

            .responsive-table, .responsive-table tbody, .responsive-table tr, .responsive-table td {
                display: block;
                width: 100%;
            }

                .responsive-table tr {
                    margin-bottom: 15px;
                }

                .responsive-table td {
                    text-align: right;
                    padding-left: 50%;
                    position: relative;
                }

                    .responsive-table td::before {
                        content: attr(data-label);
                        position: absolute;
                        left: 0;
                        width: 50%;
                        padding-left: 15px;
                        font-weight: bold;
                        text-align: left;
                    }
        }
           .medicliamdata {
                   background: #3D1956;
    color: #febf39 !important;
    padding: 9px 7px;
    /* margin: -2% 6% 0% 0% !important; */
   
                }

           
    </style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server"> 
    


    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Nominations"></asp:Label>
                    </span>
                    <br />  
                    <span runat="server" id="messgaeSpan">
                        <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="#ff0000" Visible="false"></asp:Label>
                    </span>

                </div>

                <span>
                    <a href="https://ess.highbartech.com/hrms/PersonalDocuments.aspx" class="aaaa">My Corner</a>
                </span>


                <div class="edit-contact" runat="server" id="Div3">
                    <div class="editprofile" style="text-align: center; border: none;" id="div4" runat="server">
                    </div>
                    
                    <div class="table-wrapper">
                         <span id="spnMediclaimData" runat="server" visible="false">
                            <a href="Mediclaimdata.aspx" class="medicliamdata">Submit Mediclaim Data</a>
                        </span>
                        <br /><br />
                        <div>
                        <asp:Label runat="server" ID="lblmessage" Visible="False" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                        </div>
                        <br />
                        <table class="responsive-table">
                            <thead>
                                <tr>
                                    <th>Member</th>
                                    <th>Name</th>
                                    <th>Birth Date</th>
                                    <th>Gender</th>
                                    <th>Age</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>Self</td>
                                    <td>
                                        <asp:TextBox AutoComplete="off" ID="txtMember_Name_self" Style="padding: 3px 0px 5px 3px !important; height: 20px !important;" runat="server" MaxLength="100" Width="200px"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox AutoComplete="off" MaxLength="10" Width="100px" Style="padding: 3px 0px 5px 3px !important; height: 20px !important;" ID="txt_Birthdate_self" runat="server" AutoPostBack="True" OnTextChanged="txt_Birthdate_self_TextChanged"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" Format="dd/MM/yyyy" TargetControlID="txt_Birthdate_self"
                                            runat="server">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_Gender_self" runat="server" Style="height: 28px; width: 128px; padding: 3px 0px 5px 3px !important; margin: -10px 0 -1px 0 !important;">
                                            <asp:ListItem Value="0">Select Gender</asp:ListItem>
                                            <asp:ListItem Value="Male">Male</asp:ListItem>
                                            <asp:ListItem Value="Female">Female</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox AutoComplete="off" ReadOnly="true" Enabled="False" ID="txt_Age_self" runat="server" Width="100px" Style="padding: 3px 0px 5px 3px !important; height: 20px !important;"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Spouse</td>
                                    <td>
                                        <asp:TextBox AutoComplete="off" ID="txtMember_Name_spouse" Style="padding: 3px 0px 5px 3px !important; height: 20px !important;" runat="server" MaxLength="100" Width="200px"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox AutoComplete="off" MaxLength="10" Width="100px" Style="padding: 3px 0px 5px 3px !important; height: 20px !important;" ID="txt_Birthdate_spouse" runat="server" AutoPostBack="True" OnTextChanged="txt_Birthdate_spouse_TextChanged"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender8" Format="dd/MM/yyyy" TargetControlID="txt_Birthdate_spouse"
                                            runat="server">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_Gender_spouse" runat="server" Style="height: 28px; width: 128px; padding: 3px 0px 5px 3px !important; margin: -10px 0 -1px 0 !important;">
                                            <asp:ListItem Value="0">Select Gender</asp:ListItem>
                                            <asp:ListItem Value="Male">Male</asp:ListItem>
                                            <asp:ListItem Value="Female">Female</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox AutoComplete="off" ReadOnly="true" Enabled="False" ID="txt_Age_spouse" runat="server" Width="100px" Style="padding: 3px 0px 5px 3px !important; height: 20px !important;"></asp:TextBox>

                                    </td>
                                </tr>

                                <tr>
                                    <td>Childern1</td>
                                    <td>
                                        <asp:TextBox AutoComplete="off" ID="txtMember_Name_childern1" Style="padding: 3px 0px 5px 3px !important; height: 20px !important;" runat="server" MaxLength="100" Width="200px"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox AutoComplete="off" MaxLength="10" Width="100px" Style="padding: 3px 0px 5px 3px !important; height: 20px !important;" ID="txt_Birthdate_childern1" runat="server" AutoPostBack="True" OnTextChanged="txt_Birthdate_childern1_TextChanged"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender9" Format="dd/MM/yyyy" TargetControlID="txt_Birthdate_childern1"
                                            runat="server">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_Gender_childern1" runat="server" Style="height: 28px; width: 128px; padding: 3px 0px 5px 3px !important; margin: -10px 0 -1px 0 !important;">
                                            <asp:ListItem Value="0">Select Gender</asp:ListItem>
                                            <asp:ListItem Value="Male">Male</asp:ListItem>
                                            <asp:ListItem Value="Female">Female</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox AutoComplete="off" ReadOnly="true" Enabled="False" ID="txt_Age_childern1" runat="server" Width="100px" Style="padding: 3px 0px 5px 3px !important; height: 20px !important;"></asp:TextBox>

                                    </td>
                                </tr>

                                <tr>
                                    <td>Childern2</td>
                                    <td>
                                        <asp:TextBox AutoComplete="off" ID="txtMember_Name_childern2" Style="padding: 3px 0px 5px 3px !important; height: 20px !important;" runat="server" MaxLength="100" Width="200px"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox AutoComplete="off" MaxLength="10" Width="100px" Style="padding: 3px 0px 5px 3px !important; height: 20px !important;" ID="txt_Birthdate_childern2" runat="server" AutoPostBack="true" OnTextChanged="txt_Birthdate_childern2_TextChanged"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender10" Format="dd/MM/yyyy" TargetControlID="txt_Birthdate_childern2"
                                            runat="server">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_Gender_childern2" runat="server" Style="height: 28px; width: 128px; padding: 3px 0px 5px 3px !important; margin: -10px 0 -1px 0 !important;">
                                            <asp:ListItem Value="0">Select Gender</asp:ListItem>
                                            <asp:ListItem Value="Male">Male</asp:ListItem>
                                            <asp:ListItem Value="Female">Female</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox AutoComplete="off" ReadOnly="true" Enabled="False" ID="txt_Age_childern2" runat="server" Width="100px" Style="padding: 3px 0px 5px 3px !important; height: 20px !important;"></asp:TextBox>

                                    </td>
                                </tr>
                            </tbody>
                        </table>

                                     <div class="fuel_Savebtndiv" runat="server" id="Div2">
                                    <asp:LinkButton ID="fuel_btnSave" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClick="fuel_btnSave_Click" OnClientClick="return SaveMultiClick();">Submit</asp:LinkButton>
                                    <asp:LinkButton ID="fuel_btncancel" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="Savebtnsve" OnClientClick="return CancelMultiClick();" OnClick="fuel_btncancel_Click">Cancel</asp:LinkButton>
                                    <asp:LinkButton ID="fuel_btnBack" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/PersonalDocuments.aspx">Back</asp:LinkButton>
                                </div>



                        <ul id="Ul1" runat="server" style="display: none">

                            <asp:DropDownList Visible="false" ID="ddlMaritalStatus" runat="server" Style="height: 28px; width: 128px; padding: 3px 0px 5px 3px !important; margin: -10px 0 -1px 0 !important;">
                                <asp:ListItem Value="0">Select Gender</asp:ListItem>
                                <asp:ListItem Value="Male">Male</asp:ListItem>
                                <asp:ListItem Value="Female">Female</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox AutoComplete="off" ID="txtMember_Name" Style="padding: 3px 0px 5px 3px !important; height: 20px !important;" runat="server" MaxLength="100" Width="200px"></asp:TextBox></td>
                        <asp:TextBox AutoComplete="off" MaxLength="10" Width="100px" Style="padding: 3px 0px 5px 3px !important; height: 20px !important;" ID="txtBirthdatedate_Med" runat="server" AutoPostBack="True" OnTextChanged="txtFromdateOut_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender6" Format="dd/MM/yyyy" TargetControlID="txtBirthdatedate_Med"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>

                            <asp:DropDownList ID="txtgender" runat="server" Style="height: 28px; width: 128px; padding: 3px 0px 5px 3px !important; margin: -10px 0 -1px 0 !important;">
                                <asp:ListItem Value="0">Select Gender</asp:ListItem>
                                <asp:ListItem Value="Male">Male</asp:ListItem>
                                <asp:ListItem Value="Female">Female</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox AutoComplete="off" ReadOnly="true" Enabled="False" ID="txtAge_Med" runat="server" Width="100px" Style="padding: 3px 0px 5px 3px !important; height: 20px !important;"></asp:TextBox>



                            <li class="claimfuel_Reason"></li>

                            <li class="claimfuel_fromdate"></li>
                            <li class="claimfuel_ElgAmount"></li>
                            <li class="claimfuel_Amount">
                                <span>Age:</span>
                                <br />

                            </li>

                            <li class="claimfuel_Reason">
                                <span>Spouse </span>&nbsp;<span style="color: red">*</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="TextBox3" runat="server" MaxLength="100"></asp:TextBox>
                            </li>

                            <li class="claimfuel_ElgAmount">
                                <span>Gender</span>&nbsp;<span style="color: red">*</span>
                                <br />
                                <div class="form-check-inline">
                                    <asp:RadioButton runat="server" class="form-check-input" ID="RadioButton1" Text="Male" AutoPostBack="true" GroupName="Spouseradio" Checked="true" />
                                    <asp:RadioButton runat="server" class="form-check-input" ID="RadioButton2" Text="Female" AutoPostBack="true" GroupName="Spouseradio" />
                                </div>
                            </li>
                            <li class="claimfuel_Amount">
                                <span>Age:</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ReadOnly="true" Enabled="False" ID="TextBox6" runat="server" MaxLength="50"></asp:TextBox>
                            </li>
                            <li class="claimfuel_Reason">
                                <span>Childer1 </span>&nbsp;<span style="color: red">*</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="TextBox7" runat="server" MaxLength="100"></asp:TextBox>
                            </li>


                            <li class="claimfuel_ElgAmount">
                                <span>Gender</span>&nbsp;<span style="color: red">*</span>
                                <br />
                                <div class="form-check-inline">
                                    <asp:RadioButton runat="server" class="form-check-input" ID="RadioButton3" Text="Male" AutoPostBack="true" GroupName="Childer1radio" Checked="true" />
                                    <asp:RadioButton runat="server" class="form-check-input" ID="RadioButton4" Text="Female" AutoPostBack="true" GroupName="Childer1radio" />
                                </div>
                            </li>
                            <li class="claimfuel_Amount">
                                <span>Age:</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ReadOnly="true" Enabled="False" ID="TextBox9" runat="server" MaxLength="50"></asp:TextBox>
                            </li>
                            <li class="claimfuel_Reason">
                                <span>Childer2 </span>&nbsp;<span style="color: red">*</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="TextBox10" runat="server" MaxLength="100"></asp:TextBox>
                            </li>


                            <li class="claimfuel_ElgAmount">
                                <span>Gender</span>&nbsp;<span style="color: red">*</span>
                                <br />
                                <div class="form-check-inline">
                                    <asp:RadioButton runat="server" class="form-check-input" ID="RadioButton5" Text="Male" AutoPostBack="true" GroupName="Childer2radio" Checked="true" />
                                    <asp:RadioButton runat="server" class="form-check-input" ID="RadioButton6" Text="Female" AutoPostBack="true" GroupName="Childer2radio" />
                                </div>
                            </li>
                            <li class="claimfuel_Amount">
                                <span>Age:</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ReadOnly="true" Enabled="False" ID="TextBox13" runat="server" MaxLength="50"></asp:TextBox>
                            </li>

                        </ul>
                         <div class="fuel_Savebtndiv" runat="server" id="Div5" style="display: none">
     <asp:LinkButton ID="LinkButton1" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClick="btnSave_Click" OnClientClick="return SaveMultiClick();">Submit</asp:LinkButton>

     <asp:LinkButton ID="LinkButton2" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/PersonalDocuments.aspx">Back</asp:LinkButton>
 </div>
                    </div>
                </div>
            </div>
        </div>

            

        <div class="form-check-inline" style="display: none">
            <asp:RadioButton runat="server" class="form-check-inline" ID="Med_male" Text="Male" AutoPostBack="true" GroupName="Selfradio" Checked="true" />
            <asp:RadioButton runat="server" class="form-check-input" ID="Med_female" Text="Female" AutoPostBack="true" GroupName="Selfradio" />
        </div>
        <div class="fuel_Savebtndiv" runat="server" id="Div6" style="display:none">
            <asp:LinkButton ID="LinkButton5" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClick="fuel_btnSave_Click" OnClientClick="return SaveMultiClick();">Submit</asp:LinkButton>

            <asp:LinkButton ID="LinkButton7" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/PersonalDocuments.aspx">Back</asp:LinkButton>
        </div>

        <div class="commpagesdiv" style="display: none">
            <div class="commonpages">
                <div class="wishlistpagediv">
                    <div class="userposts">
                    </div>

                    <span>
                        <a href="http://localhost/hrms/PersonalDocuments.aspx" class="aaaa">My Corner</a>
                    </span>


                    <div class="edit-contact" runat="server" id="Div1">
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

                            <li class="fuel_inboxEmpCode">

                                
                            </li>
                            <li class="fuel_claimed" hidden="hidden">
                                <br />
                                <asp:TextBox AutoComplete="off" ID="TextBox1" runat="server" MaxLength="100" Visible="false"></asp:TextBox>
                            </li>

                            <li class="fuel_inboxEmpCode" hidden="hidden" style="display: none;">

                                <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="False"> </asp:TextBox>

                            </li>
                            <li class="fuel_InboxEmpName" hidden="hidden" style="display: none;">

                                <asp:TextBox AutoComplete="off" ID="txtEmpName" runat="server" Visible="false"></asp:TextBox>

                            </li>

                            <li class="fuel_date" style="display: none;">
                                <%--  Commented by R1 on 01-10-2018 <span>Submission On </span>   
                            <br />--%>

                                <asp:TextBox AutoComplete="off" ID="txtFromdateMain" runat="server" AutoPostBack="True" Visible="false"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtFromdateMain"
                                    runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </li>
                            <li class="fuel_claimed" hidden="hidden" style="display: none;">
                                <%--   <span>Total Quantity Claimed: </span>--%>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="TextBox11" runat="server" MaxLength="100" Visible="false"></asp:TextBox>
                            </li>
                            <li class="fuel_detail" style="display: none;">
                                <span hidden="hidden" style="font-size: 12pt; font-weight: bold; text-decoration-line: underline;">Provident Fund Nominations</span>
                                <%--                            <asp:LinkButton ID="lnk_fuel_drop" runat="server" Text="Add Fuel Bills" CssClass="Savebtnsve" ToolTip="Browse" OnClientClick="Show_Hide()" ></asp:LinkButton>
                            <asp:ImageButton id="img_fuel_drop" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/arrowdown.png" />--%>
                            </li>
                            <li class="claimfuel_Amount" style="display: none;">

                                <asp:TextBox AutoComplete="off" ID="TextBox2" Visible="false" runat="server" MaxLength="50"></asp:TextBox>
                            </li>

                            <li class="claimfuel_Amount" hidden="hidden" style="display: none;">
                                <span hidden="hidden">Nominee Name:</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="txtNominee" runat="server" Visible="false" AutoPostBack="false" MaxLength="100"></asp:TextBox>
                            </li>
                            <li class="claimfuel_ElgAmount" style="display: none;">
                                <span hidden="hidden">Relation with Employee:</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="txtRelationPF" runat="server" Visible="false" AutoPostBack="true" MaxLength="50"></asp:TextBox>
                            </li>

                            <li class="claimfuel_fromdate" style="display: none;">
                                <span hidden="hidden">Birth Date:</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="txtBirthdatedate_PF" runat="server" Visible="false" AutoPostBack="True" OnTextChanged="txtFromdate_TextChanged"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtBirthdatedate_PF"
                                    runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </li>
                            <li class="claimfuel_Reimbursement" style="display: none;">
                                <span hidden="hidden">Age:</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ID="txtAge_PF" ReadOnly="true" Visible="false" Enabled="False" runat="server" MaxLength="100"></asp:TextBox>
                            </li>

                            <li class="claimfuel_Amount" style="display: none;">
                                <span hidden="hidden">Gender:</span>
                                <br />
                                <div class="form-check-inline" hidden="hidden" style="display: none;">
                                    <asp:RadioButton runat="server" class="form-check-input" ID="PF_male" Text="Male" AutoPostBack="true" GroupName="PFradio" Checked="true" />
                                    <asp:RadioButton runat="server" class="form-check-input" ID="PF_female" Text="Female" AutoPostBack="true" GroupName="PFradio" />
                                </div>

                            </li>
                            <li class="claimfuel_ElgAmount" hidden="hidden" style="display: none;">
                                <asp:LinkButton ID="btnfuel_Details" runat="server" Visible="false" Text="Add" ToolTip="Add" Font-Bold="false" CssClass="Savebtnsve" OnClick="btnfuel_Details_Click"></asp:LinkButton>
                            </li>
                            <li class="fuel_grid" style="display: none;">

                                <div>

                                    <asp:GridView ID="dgFuelClaim" Visible="false" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="70%"
                                        DataKeyNames="Nom_id,PFNom_id" OnRowCreated="dgFuelClaim_RowCreated">
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" HorizontalAlign="Center" />
                                        <PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />

                                        <Columns>
                                            <asp:BoundField HeaderText="Nominee Name"
                                                DataField="NomineeName"
                                                ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="15%" />

                                            <asp:BoundField HeaderText="Nominee Relation"
                                                DataField="Nominee_Rel"
                                                ItemStyle-HorizontalAlign="center"
                                                ItemStyle-Width="15%" />

                                            <asp:BoundField HeaderText="Gender"
                                                DataField="Nominee_Sex"
                                                ItemStyle-HorizontalAlign="center"
                                                ItemStyle-Width="15%" />

                                            <asp:BoundField HeaderText="BirthDate"
                                                DataField="BirthDate"
                                                ItemStyle-HorizontalAlign="center"
                                                ItemStyle-Width="15%" />

                                            <asp:BoundField HeaderText="Age"
                                                DataField="Age"
                                                ItemStyle-HorizontalAlign="center"
                                                ItemStyle-Width="1%" />

                                            <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <%--<asp:LinkButton ID="lnkEdit" runat="server" Text='View' OnClick="lnkEdit_Click1"></asp:LinkButton>--%>
                                                    <asp:ImageButton ID="lnkEdit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkEdit_Click1" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btn_del" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/delete.png" OnClick="Del_Fuel_bill" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>


                                        </Columns>
                                    </asp:GridView>

                                </div>

                            </li>

                            <li class="fuel_detail">
                                <br />
                                <span style="font-size: 12pt; font-weight: bold; text-decoration-line: underline;">Mediclaim Policy Declaration:</span>
                                <%--                            <asp:LinkButton ID="lnk_out_drop" runat="server" Text="Add Outstation Details" CssClass="Savebtnsve" ToolTip="Browse" ></asp:LinkButton>
                            <asp:ImageButton id="img_out_drop" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/arrowdown.png" />--%>
                            </li>

                            <li class="claimfuel_Amount">

                                <asp:TextBox AutoComplete="off" ID="TextBox4" Visible="false" runat="server" MaxLength="50"></asp:TextBox>
                            </li>
                            <li class="claimfuel_Reason">
                                <span>Marital Status</span> &nbsp;<span style="color: red">*</span>
                                <br />
                                <%--<asp:TextBox AutoComplete="off" ID="txtRelationMed" runat="server" MaxLength="10"></asp:TextBox>--%>
                                <asp:DropDownList ID="DropDownList4" runat="server" Style="height: 28px; width: 128px; padding: 3px 0px 5px 3px !important; margin: -10px 0 -1px 0 !important;">
                                    <asp:ListItem Value="0">Select Gender</asp:ListItem>
                                    <asp:ListItem Value="Male">Male</asp:ListItem>
                                    <asp:ListItem Value="Female">Female</asp:ListItem>
                                </asp:DropDownList>
                            </li>
                            <li></li>
                            <li class="claimfuel_Reason">
                                <br />
                                <span>Relation with Employee</span>&nbsp;<span style="color: red">*</span>
                                <br />
                                <%--<asp:TextBox AutoComplete="off" ID="txtRelationMed" runat="server" MaxLength="10"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlRelationMed" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRelationMed_SelectedIndexChanged" Height="28">
                                    <asp:ListItem Value="Select" Selected="True">Select</asp:ListItem>
                                    <asp:ListItem Value="Self">Self</asp:ListItem>
                                    <asp:ListItem Value="Spouse">Spouse</asp:ListItem>
                                    <asp:ListItem Value="Children1">Children1</asp:ListItem>
                                    <asp:ListItem Value="Children2">Children2</asp:ListItem>
                                </asp:DropDownList>
                            </li>
                            <li class="claimfuel_Reason">
                                <span>Member Name</span>&nbsp;<span style="color: red">*</span>
                                <br />
                                <asp:TextBox AutoComplete="off" runat="server" MaxLength="100"></asp:TextBox>
                            </li>

                            <li class="claimfuel_fromdate">
                                <span>Birth Date </span>&nbsp;<span style="color: red">*</span>
                                <br />
                                <asp:TextBox AutoComplete="off" runat="server" AutoPostBack="True" OnTextChanged="txtFromdateOut_TextChanged"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" Format="dd/MM/yyyy" TargetControlID="txtBirthdatedate_Med"
                                    runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </li>
                            <li class="claimfuel_Amount">
                                <span>Age:</span>
                                <br />
                                <asp:TextBox AutoComplete="off" ReadOnly="true" Enabled="False" runat="server" MaxLength="50"></asp:TextBox>
                            </li>
                            <li class="claimfuel_ElgAmount">
                                <span>Gender</span>&nbsp;<span style="color: red">*</span>
                                <br />
                                <div class="form-check-inline">
                                    <asp:RadioButton runat="server" class="form-check-input" Text="Male" AutoPostBack="true" GroupName="MEDradio" Checked="true" />
                                    <asp:RadioButton runat="server" class="form-check-input" Text="Female" AutoPostBack="true" GroupName="MEDradio" />
                                </div>
                            </li>
                            <li class="claimfuel_Reimbursement">
                                <!--<span>End Kilometers </span>-->
                                <br />
                                <asp:LinkButton ID="btnOut_Details" runat="server" Text="Add" ToolTip="Add" CssClass="Savebtnsve" OnClick="btnOut_Details_Click"></asp:LinkButton>
                            </li>

                            <li class="fuel_detail">
                                <%--<span>Outstation Details</span>--%>
                            </li>


                            <li class="fuel_grid">

                                <div>

                                    <asp:GridView ID="dgFuelOut" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="70%"
                                        DataKeyNames="Nom_id,MedNom_id" OnRowCreated="dgFuelOut_RowCreated">
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

                                            <asp:BoundField HeaderText="Member Name"
                                                DataField="MemberName"
                                                ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="15%" />

                                            <asp:BoundField HeaderText="Member Relation"
                                                DataField="Member_Rel"
                                                ItemStyle-HorizontalAlign="center"
                                                ItemStyle-Width="25%" />

                                            <asp:BoundField HeaderText="Gender"
                                                DataField="Member_Sex"
                                                ItemStyle-HorizontalAlign="center"
                                                ItemStyle-Width="1%" />

                                            <asp:BoundField HeaderText="BirthDate"
                                                DataField="BirthDate"
                                                ItemStyle-HorizontalAlign="center"
                                                ItemStyle-Width="15%" />

                                            <asp:BoundField HeaderText="Age"
                                                DataField="Age"
                                                ItemStyle-HorizontalAlign="center"
                                                ItemStyle-Width="25%" DataFormatString="{0:0}" />


                                            <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <%--<asp:LinkButton ID="lnkEdit" runat="server" Text='View' OnClick="lnkEdit_Click1"></asp:LinkButton>--%>
                                                    <asp:ImageButton ID="lnkEdit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkEdit_Click2" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btn_del" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/delete.png" OnClick="Del_Outstation" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                </div>

                            </li>
                            <li class="fuel_upload" hidden="hidden">
                                <span hidden="hidden">Upload Photo Id proofs for all members:</span><br />
                                <asp:FileUpload ID="uploadfile" runat="server" Visible="false" AllowMultiple="true" />
                                <asp:TextBox AutoComplete="off" ID="txtprofile" runat="server" CssClass="mobilenotextbox popup" Visible="false"> </asp:TextBox>
                                <asp:LinkButton ID="lnkuplodedfile" OnClick="lnkuplodedfile_Click" runat="server" Visible="false"></asp:LinkButton>
                                <%--<span class="texticonselect tooltip" title="Please provide your profile picture."><i class="fa fa-file-image-o"></i></span>--%>

                                <asp:GridView ID="gvfuel_claimsFiles" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="70%"
                                    DataKeyNames="t_id">
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
                                        <asp:BoundField HeaderText="Claim Files"
                                            DataField="file_name"
                                            ItemStyle-HorizontalAlign="left"
                                            HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-Width="20%" />


                                        <asp:TemplateField HeaderText="Files" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%--<asp:LinkButton ID="lnkViewFiles1" runat="server" Text='View' OnClick="lnkViewFiles_Click" >
                                        </asp:LinkButton>--%>
                                                <asp:LinkButton ID="lnkViewFiles" runat="server" Text='View' OnClientClick=<%# "DownloadFile('" + Eval("file_name") + "')" %>>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                    </Columns>
                                </asp:GridView>

                            </li>
                            <li class="fuel_claimed">
                                <br />
                                <asp:TextBox AutoComplete="off" ID="TextBox17" runat="server" MaxLength="100" Visible="false"></asp:TextBox>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
       


        <%-- Following Popup for Fuel Mobile Rem Requestt --%>
        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" DisplayModalPopupID="mpe_Cancel" TargetControlID="fuel_btncancel">
        </ajaxToolkit:ConfirmButtonExtender>
        <ajaxToolkit:ModalPopupExtender ID="mpe_Cancel" runat="server" PopupControlID="pnlPopup_Cancel" TargetControlID="fuel_btncancel" OkControlID="btnYes_CLR"
            CancelControlID="btnNo_CLR" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup_Cancel" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Confirmation
            </div>
            <div class="body">
                Do you want to Cancel Fuel Reimbursement Request ?
            </div>
            <div class="footer" align="right">
                <asp:Button ID="btnNo_CLR" runat="server" Text="No" />
                <asp:Button ID="btnYes_CLR" runat="server" Text="Yes" />
            </div>
        </asp:Panel>
        <%-- End Here --%>
    </div>


     

    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />

    <asp:HiddenField ID="FilePath" runat="server" />
      
    <asp:HiddenField ID="hdnsptype" runat="server" />
    <asp:HiddenField ID="hdnfuelQty" runat="server" />
    <asp:HiddenField ID="hflEmpDesignation" runat="server" />

    <asp:HiddenField ID="hflEmpDepartment" runat="server" />

    <asp:HiddenField ID="hflEmailAddress" runat="server" />

    <asp:HiddenField ID="hflGrade" runat="server" />

    <asp:HiddenField ID="hflapprcode" runat="server" />

    <asp:HiddenField ID="hdnDesk" runat="server" />

    <asp:HiddenField ID="hdnDestnation" runat="server" />

    <asp:HiddenField ID="hdnRemid" runat="server" />

    <asp:HiddenField ID="hdnClaimsID" runat="server" />

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
    <asp:HiddenField ID="hdnFuelReimbursementType" runat="server" />

    <asp:HiddenField ID="hdnApprovalCOS_Code" runat="server" />
    <asp:HiddenField ID="hdnApprovalCOS_ID" runat="server" />
    <asp:HiddenField ID="hdnApprovalCOS_mail" runat="server" />
    <asp:HiddenField ID="hdnApprovalCOS_Name" runat="server" />

    <asp:HiddenField ID="hdnMobRemStatusM" runat="server" />
    <asp:HiddenField ID="hdnMobRemStatus_dtls" runat="server" />
    <asp:HiddenField ID="hdnClaimDate" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="hdnfrmdate_emial" runat="server" />
    <asp:HiddenField ID="hdntodate_emial" runat="server" /> 
    
    <asp:HiddenField ID="hdnNominiId" runat="server" />

</asp:Content>

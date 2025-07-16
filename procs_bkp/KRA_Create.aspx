<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" EnableViewState="true"
    MaintainScrollPositionOnPostback="true" CodeFile="KRA_Create.aspx.cs" Inherits="KRA_Create" EnableSessionState="True" ValidateRequest="false" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

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

         .LableName {
            color: #F28820;
            font-size: 16px;
            font-weight: normal;
            text-align: left;
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

        .GoalDecriptionTextArea {
            width: 661px !important;
            height: 158px !important;
        }

        .RemarksTextArea {
            width: 476px !important;
            height: 65px !important;
        }

        td {
                vertical-align: top;
            }
        .msgwidth
        {
            width:200% !important;
        }



 
    /*.modal
    {
        position: fixed;
        z-index: 0;
        height: 100%;
        width: 100%;
        top: 0;
        background-color: Black;
        filter: alpha(opacity=60);
        opacity: 0.6;
    
    }
    .center
    {
        z-index: 1000;
        margin: 300px auto;
        padding: 10px;
        width: 130px;
        background-color: White;
        border-radius: 10px;
        filter: alpha(opacity=100);
        opacity: 1;
    
    }
    .center img
    {
        height: 128px;
        width: 128px;
    }*/

    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
    <script src="../js/HtmlControl/jHtmlArea-0.8.min.js"></script>
    <link href="../js/HtmlControl/jHtmlArea.css" rel="stylesheet" />

    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Submit KRA"></asp:Label>
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
                        </li>
                        <li class="trvl_type">
                               <span>Role Name</span><br />
                             <asp:TextBox AutoComplete="off" ID="txtTemplate_Role_Name" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                          <asp:TextBox AutoComplete="off" ID="txtTempKRA_ID" runat="server" ReadOnly="true" Enabled="False" Visible="false"></asp:TextBox>
                          <asp:TextBox AutoComplete="off" ID="txtRole_ID" runat="server" ReadOnly="true" Enabled="False" Visible="false"></asp:TextBox>
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
                             <span>Desgination</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPosition" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                          

                        <li class="trvl_date">
                                <span>Period</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtPeriod" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                           
                        </li>
                        <li class="trvl_date">
                              <span>From Date</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtKRAFromDt" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>

                           
                        </li>
                        <li class="trvl_date">
                              <span>To Date</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtKTATodt" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>



                        <li class="trvl_date" style="display: none">
                            <span>KRA Submit Date</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtKRA_SubmitDt" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date" style="display: none"></li>
                        <li class="trvl_date" style="display: none"></li>


                        <li id="ligoalmsg" runat="server">
                            <span id="Span2" runat="server" class="LableName">Goal Details</span>
                        </li>
                         <li id="ligoalmsg1" runat="server"></li>
                        <li id="ligoalmsg2" runat="server"></li>


                        <li class="trvl_date" id="liGoalTitle" runat="server">
                            <span>Goal Title</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_goal_title" MaxLength="100" runat="server"></asp:TextBox>
                        </li>
                        <li class="trvl_date" id="liWeightage" runat="server">
                            <span>Weightage</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_Weightage" runat="server" MaxLength="5" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="trvl_date" id="liblank1" runat="server"></li>

                        <li class="trvl_date" id="ligoadexcription" runat="server">
                            <span>Goal Description</span> <span id="Span1" runat="server" style="color: red; font-size: 10px; font-weight: normal; font-style: italic;">Maximum 200 Characters</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_goal_description" runat="server" Rows="20" TextMode="MultiLine" maxlength="15" CssClass="GoalDecriptionTextArea" onkeyup="countChar(this)"></asp:TextBox>
                        </li>
                        <li class="trvl_date" id="liblank2" runat="server">
                            <asp:TextBox AutoComplete="off" ID="TextBox1" runat="server" MaxLength="3" Visible="false"></asp:TextBox>
                        </li>
                        <li class="trvl_date" id="liblank3" runat="server">
                            <asp:TextBox AutoComplete="off" ID="TextBox2" runat="server" MaxLength="3" Visible="false"></asp:TextBox>
                        </li>

                        <li class="trvl_date" id="ligoalSeq" runat="server">
                            <span>Goal Sequence Number</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_goal_seq_no" runat="server" MaxLength="3" ></asp:TextBox>
                        </li>
                        <li class="trvl_date" id="liblank4" runat="server">
                            <asp:TextBox AutoComplete="off" ID="TextBox3" runat="server" MaxLength="3" Visible="false"></asp:TextBox>
                        </li>
                        <li class="trvl_date" id="liblank5" runat="server">
                            <asp:TextBox AutoComplete="off" ID="TextBox4" runat="server" MaxLength="3" Visible="false"></asp:TextBox>
                        </li>


                        <li class="trvL_detail" id="litrvldetail" runat="server">
                            <asp:Label runat="server" ID="lblMilestoneMsg" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                            <br />
                            <asp:LinkButton ID="btnTra_Details" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" OnClick="btnTra_Details_Click"></asp:LinkButton>
                            <span id="spntrvldtls" runat="server">Add Measurement Details</span>
                        </li>
                        <li class="trvl_date" id="liblank6" runat="server"></li>
                        <li class="trvl_date" id="liblank7" runat="server"></li>

                        <div id="DivTrvl" class="edit-contact" runat="server" visible="false">
                            <ul>
                                <li class="trvl_date">
                                    <span>Measurement Details</span>&nbsp;&nbsp;<span style="color: red">* <span style="color: red; font-size: 10px; font-weight: normal; font-style: italic;">Maximum 250 Characters</span></span><br />
                                    <asp:TextBox AutoComplete="off" ID="txt_Measurement_dtls" runat="server" Rows="20" MaxLength="500" TextMode="MultiLine" CssClass="GoalDecriptionTextArea"></asp:TextBox>
                                </li>
                                <li class="trvl_date"></li>
                                <li class="trvl_date"></li>

                                <li class="trvl_date">
                                    <span>Unit</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                    <asp:DropDownList runat="server" ID="lstUnit" CssClass="DropdownListSearch" AutoPostBack="true" OnSelectedIndexChanged="lstUnit_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </li>
                                <li class="trvl_date">
                                    <span>Quantity</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txt_Mqty" runat="server" MaxLength="6"></asp:TextBox>
                                </li>
                                <li class="trvl_date"></li>


                                <li class="trvl_date">
                                    <span style="display:none">Remakrs</span><span style="display:none;color: red; font-size: 10px; font-weight: normal; font-style: italic;"> Maximum 200 Characters</span><br />
                                    <asp:TextBox AutoComplete="off" Visible="false" ID="txt_remakrs" runat="server" TextMode="MultiLine" MaxLength="200" CssClass="RemarksTextArea"></asp:TextBox>
                                </li>
                                <li class="trvl_date"></li>
                                <li class="trvl_date"></li>


                                <li class="trvl_date">
                                    <span>Measurement Sequence Number</span><br />
                                    <asp:TextBox AutoComplete="off" ID="txt_measurement_seq_no" runat="server" MaxLength="3"></asp:TextBox>
                                </li>
                                <li class="trvl_date"></li>
                                <li class="trvl_date"></li>


                            </ul>
                            <div>
                                 <asp:Label runat="server" ID="lbl_Measurement_msg" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                            </div>
                            <div>
                                <asp:LinkButton ID="trvldeatils_btnSave" runat="server" Text="Add Measurement" ToolTip="Add Measurement" CssClass="Savebtnsve" OnClientClick=" return MultiClick_Trvl();" OnClick="trvldeatils_btnSave_Click"></asp:LinkButton>
                                
                                <asp:LinkButton ID="trvldeatils_cancel_btn" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="trvldeatils_cancel_btn_Click"></asp:LinkButton>
                              
                            </div>
                        </div>

                        <li class="trvl_grid" id="li1" runat="server">
                            <div class="manage_grid" style="overflow: scroll; width: 100%; padding-top: 0px; margin-bottom: -30px;">
                                <asp:GridView ID="dgMeasurementslist" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="5" CellSpacing="5" AutoGenerateColumns="False" Width="80%" EditRowStyle-Wrap="false"
                                    DataKeyNames="Measurement_id,unit_id">
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

                                        <asp:BoundField HeaderText="Measurement Sequence Number"
                                            DataField="measurement_seq_no"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />
 
                                       <asp:TemplateField  HeaderText="GoalMeasurement Detail" ItemStyle-Width="15%">
                                            <ItemTemplate> 
                                               <asp:Label ID="lblMeasurementDetails" runat="server" 
                                                Text='<%# HttpUtility.HtmlDecode(Eval("Measurement_Details").ToString()) %>'>
                                                </asp:Label>        
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>


                                        <asp:BoundField HeaderText="Unit"
                                            DataField="unit_short_name"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />


                                        <asp:BoundField HeaderText="Quantity"
                                            DataField="Quantity"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" /> 

                                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_edit_Measurement" runat="server"   CssClass="BtnShow" Text=' Edit ' OnClick="lnk_edit_Measurement_Click">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="5%" Height="22px" />
                                        </asp:TemplateField>


                                    </Columns>
                                </asp:GridView>
                            </div>

                        </li>
                         <li class="trvl_local" id="lilblgoalerrorMsg" runat="server">
                             <div class="msgwidth">
                                <asp:Label runat="server" ID="lbl_goal_msg" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                             </div>
                         </li>
                         <li class="trvl_local"  id="lilblgoalerrorMsg_2" runat="server">
                         </li>
                         <li class="trvl_local"  id="lilblgoalerrorMsg_3" runat="server">
                         </li>

                        <div>
                            <br />
                            <asp:Label runat="server" ID="lblAddGoalMsg" Visible="false" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;" Text="Please click on Update Goal button to save the changes"></asp:Label>
                         </div>
                         <div>                                
                            <asp:LinkButton ID="btnback_mng" runat="server" Text="Add Goal" ToolTip="Add Goal" CssClass="Savebtnsve" OnClick="btnback_mng_Click">Add Goal</asp:LinkButton>                            
                         </div>

                        <div>
                            <asp:LinkButton ID="trvldeatils_delete_btn" Visible="false" runat="server" Text="Delete Goal" ToolTip="Delete Goal" CssClass="Savebtnsve" OnClick="trvldeatils_delete_btn_Click1" OnClientClick="return DeleteMultiClick();"></asp:LinkButton>                       
                        </div>
                      
                     


                        <li class="trvl_grid" id="litrvlgrid" runat="server">
                            <div class="manage_grid" style="overflow: scroll; width: 100%; padding-top: 10px; margin-bottom: 10px;">

                                 <asp:GridView ID="dgKRA_Details" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                                         DataKeyNames="KRA_ID,Goal_Id" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false" OnDataBound="dgKRA_Details_DataBound" OnRowDataBound="dgKRA_Details_RowDataBound" >
                                      
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

                                          <asp:TemplateField  HeaderText="Goal Title" ItemStyle-Width="15%">
                                            <ItemTemplate> 
                                               <asp:Label ID="lblGoalTitle" runat="server" 
                                                Text='<%# HttpUtility.HtmlDecode(Eval("Goal_title").ToString()) %>'>
                                                </asp:Label>        
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="left"/>
                                        </asp:TemplateField>

                                          <asp:BoundField HeaderText="Weightage"
                                            DataField="Weightage"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="2%" ItemStyle-BorderColor="Navy" />    
                                        

                                         <asp:TemplateField  HeaderText="Measurement Details" ItemStyle-Width="20%">
                                            <ItemTemplate> 
                                               <asp:Label ID="lblMeasurements_details" runat="server" 
                                                Text='<%# HttpUtility.HtmlDecode(Eval("Measurement_Details").ToString()) %>'>
                                                </asp:Label>        
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                             <HeaderStyle HorizontalAlign="left"/>
                                        </asp:TemplateField>

                                         <asp:BoundField HeaderText="Unit"
                                            DataField="unit_short_name"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="1%" ItemStyle-BorderColor="Navy" />  

                                         <asp:BoundField HeaderText="Quantity"
                                            DataField="Quantity"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="1%" ItemStyle-BorderColor="Navy" />   

                                         <asp:BoundField HeaderText="Remarks"
                                            DataField="remarks"
                                            ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" Visible="false" />     
                                       

                                           <asp:TemplateField  HeaderText="Goal Edit" ItemStyle-Width="2%">
                                            <ItemTemplate> 
                                                   <asp:LinkButton ID="lnkedit_goal" runat="server" CssClass="BtnShow" Text='Edit' OnClick="lnkedit_goal_Click">
                                                </asp:LinkButton>    
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                  
                                       

                                    </Columns>
                                </asp:GridView> 

                                <asp:GridView ID="dgKRAView" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" Visible="false"
                                         DataKeyNames="KRA_ID,Goal_Id" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false" OnDataBound="dgKRAView_DataBound" OnRowDataBound="dgKRAView_RowDataBound">
                                      
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

                                          <asp:TemplateField  HeaderText="Goal Title" ItemStyle-Width="15%">
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
                                            ItemStyle-Width="2%" ItemStyle-BorderColor="Navy" />    


                                         <asp:TemplateField  HeaderText="Measurement Details" ItemStyle-Width="20%">
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
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>

                        <li class="trvl_local">
                            <span>Upload File</span>
                            <asp:FileUpload ID="uploadFile_KRA" runat="server" AllowMultiple="false"></asp:FileUpload>
                            <br />
                            <asp:LinkButton ID="lnk_download_kra_file" runat="server" OnClick="lnk_download_kra_file_Click" CssClass="BtnShow" Visible="false"></asp:LinkButton>
                        </li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>
                          
                        <li class="trvl_Approver">
                            <asp:GridView ID="DgvApprover" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%"
                                DataKeyNames="Approver_emp_code,approver_id">
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


                      <li class="trvl_local" id="libtngoal" runat="server">
                            
                        </li>
                        <li class="trvl_local" id="libtngoal_1" runat="server">
                              
                        </li>
                        <li class="trvl_local" id="libtngoal_2" runat="server"></li>

                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="trvl_Savebtndiv"> 
          <asp:LinkButton ID="trvl_btnSave" runat="server" Text="Accept" ToolTip="Accept" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();" OnClick="trvl_btnSave_Click">Accept</asp:LinkButton>
        <asp:LinkButton ID="btnCancel" Visible="false" runat="server" Text="Save as Draft" ToolTip="Save as Draft" CssClass="Savebtnsve" OnClientClick="return CancelMultiClick();" OnClick="btnCancel_Click">Save as Draft</asp:LinkButton>
          
        <asp:LinkButton ID="accmo_delete_btn" runat="server" Visible="false" Text="Download KRA" ToolTip="Download KRA" OnClick="accmo_delete_btn_Click" CssClass="Savebtnsve"> Download KRA </asp:LinkButton>
    </div>

    <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
    <asp:TextBox AutoComplete="off" ID="txtEmpName" runat="server" Visible="false"></asp:TextBox>
    <asp:Label ID="testsanjay" runat="server" Visible="false"></asp:Label>
    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />

 
 



    <asp:HiddenField ID="FilePath" runat="server" />
    <asp:HiddenField ID="hdngoal_id" runat="server" />
    <asp:HiddenField ID="hdnMeasurement_id" runat="server" />
    <asp:HiddenField ID="hdnKRA_id" runat="server" />
    <asp:HiddenField ID="hdn_flg_goal" runat="server" />
    <asp:HiddenField ID="hdn_flg_measurement" runat="server" />
    <asp:HiddenField ID="hdnPeriod_id" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="hdnstype_Main" runat="server" />

    <asp:HiddenField ID="hdnAppr_empcode" runat="server" />
    <asp:HiddenField ID="hdnAppr_empName" runat="server" />
    <asp:HiddenField ID="hdnAppr_empEmail" runat="server" />

    <asp:HiddenField ID="hdnGoal_Title_old" runat="server" />
    <asp:HiddenField ID="hdnGoal_Seq_no_old" runat="server" />
   <asp:HiddenField ID="hdnMeasurement_Seq_no_old" runat="server" />
    <asp:HiddenField ID="hdnWwighrage_old" runat="server" />
    <asp:HiddenField ID="hdnAppr_id" runat="server" />
    <asp:HiddenField ID="hdnKRA_IsApproved" runat="server" />
     <asp:HiddenField ID="hdn_IsSelfApprover" runat="server" />
    
    <asp:HiddenField ID="hdnIsKRAView" runat="server" />
    <asp:HiddenField ID="hdnKRA_FilePath" runat="server" />

    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

  
    <script type="text/javascript">
  

        

        $(document).ready(function () {
            $(".DropdownListSearch").select2();
            $("#MainContent_txt_Measurement_dtls").htmlarea();
            $("#MainContent_txt_goal_description").htmlarea();  
           
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

         function DeleteMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=trvldeatils_delete_btn.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        DeleteConfirm();
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

         function DeleteConfirm() {
            //Testing();
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Delete this goal ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            //alert(confirm_value.value);
            //document.forms[0].appendChild(confirm_value);
            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
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


    </script>
</asp:Content>

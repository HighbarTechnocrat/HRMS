<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="ITAssetService_RepairReplaceReq.aspx.cs" Inherits="ITAssetService_RepairReplaceReq" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/ClaimMobile_css.css" type="text/css" media="all" />

    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }
        Repa
        .aspNetDisabled {
            /*background: #dae1ed;*/
            background: #ebebe4;
        }

        #MainContent_lstApprover {
            overflow: hidden !important;
        }

        .noresize {
            resize: none;
        }
        .hiddencol {
           display: none;
       }
    </style>

</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
       
     <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Create Repair/Replacement Request"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="Label1" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>
                <div>
                    <span>
                        <a href="ITAssetService.aspx" class="aaaa">IT Inventory Home</a>
                    </span>
                </div>


                <div class="edit-contact">

                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server"></asp:LinkButton>
                        </div>
                    </div>

        <ul id="editform" runat="server" visible="true">
            <li>
                <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
            </li>
          <li></li>
          <li class="mobile_inboxEmpCode"> <span><b>Asset Repair/Replacement Request Details</b></span><br /><br /></li>
          <li></li>
            <li class="mobile_inboxEmpCode">
                <span>Asset Repair/Replacement Request</span>
                <asp:TextBox ID="txtAssetAlloReqNo" runat="server" CssClass ="txtcls"  Enabled="false" ></asp:TextBox>
            </li>
            <li class="mobile_inboxEmpCode">
               <span>Request Date</span> 
                 <asp:TextBox ID="txtReqDate" AutoComplete="off" runat="server" AutoPostBack="true" MaxLength="10" AutoCompleteType="Disabled"  Enabled="false"></asp:TextBox>
            </li>
            <hr runat="server" id="Hr2" />
            <li class="mobile_inboxEmpCode"> <span><b>Request Type</b></span><br /><br /></li>
            <li></li>
            <li class="mobile_inboxEmpCode">  
                <span>Request Type</span>&nbsp;&nbsp;<br />
                  <asp:DropDownList Visible="true" ID="ddlReqType" autopostback="true" runat="server" Width="200px" OnSelectedIndexChanged="ddlReqType_SelectedIndexChanged"> 
                    <asp:ListItem Text="Repair/Upgrade" Value="Repair/Upgrade">Repair/Upgrade</asp:ListItem>
                    <asp:ListItem Text="Replacement" Value="Replacement">Replacement</asp:ListItem>
                 </asp:DropDownList><br /><br />
            </li>
            <li></li>
           
            <hr runat="server" id="Hr3" />
            <li class="mobile_inboxEmpCode"> <span><b>Employee Details</b></span><br /><br /></li>
            <li></li>
            <li class="mobile_inboxEmpCode">
                <span>Search Employee</span>&nbsp;&nbsp;<br />
                  <asp:DropDownList Visible="true" ID="ddl_Employee" autopostback="true" runat="server" OnSelectedIndexChanged="ddl_Employee_SelectedIndexChanged"> 
                 </asp:DropDownList><br /><br />
            </li>
            <li></li>
            <li class="mobile_inboxEmpCode">
                <span>Employee Code</span>
                 <asp:TextBox ID="txtEmpCode" runat="server" Enabled="false"></asp:TextBox>
            </li>
            <li class="mobile_inboxEmpCode">
                <span>Employee Location</span>
                 <asp:TextBox ID="txtEmpLocation" runat="server" Enabled="false"></asp:TextBox>
            </li>
             <li class="mobile_inboxEmpCode">
                <span>Employee Name</span>
                 <asp:TextBox ID="txtEmpName" runat="server" Enabled="false"></asp:TextBox>
            </li>
            <li><span>Employee Department</span>
                 <asp:TextBox ID="txtEmpDept" runat="server" Enabled="false"></asp:TextBox>
            </li>
            <li class="mobile_inboxEmpCode">
                <span>Employee Type</span>
                 <asp:TextBox ID="txtEmpType" runat="server" Enabled="false"></asp:TextBox>
            </li>
            <li class="mobile_inboxEmpCode">           
                <span>Employee Designation</span>
                 <asp:TextBox ID="txtEmpDesig" runat="server" Enabled="false"></asp:TextBox>
            </li>
            <li class="mobile_inboxEmpCode"> 
                <span>Date Of Joining </span>
                 <asp:TextBox ID="txtDoj" runat="server" Enabled="false"></asp:TextBox>
            </li>
            <li class="mobile_inboxEmpCode"> 
                <span>Main Module </span>
                 <asp:TextBox ID="txtMainModule" runat="server" Enabled="false"></asp:TextBox>
            </li>
            <li class="mobile_inboxEmpCode"> 
                <span>Reporting Manager</span>
                 <asp:TextBox ID="txtRMgr" runat="server" Enabled="false"></asp:TextBox>
            </li>
             <li class="mobile_inboxEmpCode"> 
                 <span>Email-Id</span>
                 <asp:TextBox ID="txtEmail" runat="server" Enabled="false" MaxLength="60"></asp:TextBox>
            </li>
            <li class="mobile_inboxEmpCode"> 
                <span>HOD</span>
                 <asp:TextBox ID="txtHOD" runat="server" Enabled="false"></asp:TextBox>
            </li>
             <li class="mobile_inboxEmpCode">       
                <span>Mobile No</span>
                 <asp:TextBox ID="txtMobileNo" runat="server" Enabled="false"></asp:TextBox>
            </li>
            <li class="mobile_inboxEmpCode">  
                 <span>Current Address</span>
                 <asp:TextBox ID="txtCurAddress" runat="server" CssClass="noresize" Enabled="false" Height="50px" MaxLength ="800" TextMode="MultiLine"></asp:TextBox>
            </li>
            <li class="mobile_inboxEmpCode">  
                 <span>Permanent Address</span>
                 <asp:TextBox ID="txtPerAddress" runat="server" CssClass="noresize" Enabled="false" Height="50px" MaxLength ="800" TextMode="MultiLine"></asp:TextBox>
            </li>
            <hr runat="server" id="empShow7" />
            <li><span><br /><b>Assigned Assets</b></span><br /></li>
            <li></li>
            <li style="width:100%">
           <br/><div>
            <asp:GridView ID="gvproject" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" 
             DataKeyNames="Id" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false" PageSize="5" AllowPaging="True" OnPageIndexChanging="gvproject_PageIndexChanging">
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" HorizontalAlign="Center"/>
            <PagerStyle ForeColor="#000066" HorizontalAlign="left" BackColor="White" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
          
            <Columns>
            <asp:TemplateField HeaderText="Add To Repair/Replacement Grid" HeaderStyle-HorizontalAlign="Center">
            <ItemTemplate>
            <asp:RadioButton ID="rbSelect"  runat="server" AutoPostBack="true"  Width ="15px" Height="15px" OnCheckedChanged="ChckedChanged" onclick="javascript:CheckOtherIsCheckedByGVID(this);" /> 
            <%--<asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true"  Width ="15px" Height="15px" CssClass="rowCheckBox" OnCheckedChanged="ChckedChanged" onclick="javascript:CheckOtherIsCheckedByGVID(this);" />--%> 
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
             </asp:TemplateField>
                <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-CssClass="hiddencol" >
                <ItemStyle HorizontalAlign="Center"  />
                <HeaderStyle HorizontalAlign="Center" Width="5%" CssClass="hiddencol"/>
            </asp:BoundField>
            <asp:BoundField DataField="AssetNumber" HeaderText="Asset Number">
                <ItemStyle HorizontalAlign="Center"  />
                <HeaderStyle HorizontalAlign="Center" Width="20%"/>
            </asp:BoundField>
            <asp:BoundField DataField="AssetType" HeaderText="Asset Type">
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="10%"/>
            </asp:BoundField>
            <asp:BoundField DataField="AssetDesc" HeaderText="Asset Description">
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="20%"/>
            </asp:BoundField>
            <asp:BoundField DataField="SrNo" HeaderText="Sr. Number">
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="10%"/>
            </asp:BoundField>
             <asp:BoundField DataField="BrandName" HeaderText="Manufacturer/Brand" >
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="15%" />
            </asp:BoundField>
            <asp:BoundField DataField="Model" HeaderText="Model">
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="10%"/>
            </asp:BoundField>
                <asp:BoundField DataField="AgeOfAsset" HeaderText="Age Of Asset">
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="13%"/>
               </asp:BoundField>
                <asp:BoundField DataField="CPUMakeAndModel" HeaderText="CPU" >
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="15%"/>
               </asp:BoundField>
                    <asp:BoundField DataField="RAM" HeaderText="RAM" >
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="15%"/>
               </asp:BoundField>
                    <asp:BoundField DataField="HDD" HeaderText="HDD" >
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="15%"/>
               </asp:BoundField>
        </Columns>
        <PagerSettings NextPageText="&amp;lt;" Mode="NumericFirstLast"  />
    <PagerStyle HorizontalAlign="Right" VerticalAlign="Middle" CssClass="pagination" />
    </asp:GridView>
    </div>
      </li><li></li>
            <hr runat="server" id="Hr4" />
            <li id="lblAssetOptions" visible="false"  runat ="server"><span><br/><b>Asset To Repair/Upgrade</b></span><br /></li>
            <li id="lblAssetOps" visible="false"  runat ="server"><span><br/><b>Asset Options</b></span><br /></li>
            <li></li>
            <li id="liOldAssetOptions" visible="false"  runat ="server"><span><br/><b>Old Asset Details</b></span><br /></li>
            <li></li>
            <li style="width:100%">
         <div>
            <br />
            <asp:GridView ID="gvAssetOption" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" 
             DataKeyNames="Id" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false" PageSize="5" AllowPaging="True">
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" HorizontalAlign="Center"/>
            <PagerStyle ForeColor="#000066" HorizontalAlign="left" BackColor="White" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />

                <Columns>
                <asp:BoundField DataField="id" HeaderText="Id" ItemStyle-CssClass="hiddencol" >
                <ItemStyle HorizontalAlign="Center"  />
                <HeaderStyle HorizontalAlign="Center" Width="5%" CssClass="hiddencol"/>
                </asp:BoundField>
                <asp:BoundField DataField="AssetNumber" HeaderText="Asset Number">
                <ItemStyle HorizontalAlign="Center"  />
                <HeaderStyle HorizontalAlign="Center" Width="15%"/>
                </asp:BoundField>
                <asp:BoundField DataField="AssetType" HeaderText="Asset Type">
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="10%"/>
                </asp:BoundField>
                <asp:BoundField DataField="AssetDesc" HeaderText="Asset Description">
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="30%"/>
                </asp:BoundField>
                <asp:BoundField DataField="SrNo" HeaderText="Sr. Number">
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="10%"/>
                </asp:BoundField>
                 <asp:BoundField DataField="BrandName" HeaderText="Manufacturer/Brand" >
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                </asp:BoundField>
                <asp:BoundField DataField="Model" HeaderText="Model">
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="8%"/>
                </asp:BoundField>
                <asp:BoundField DataField="AgeOfAsset" HeaderText="Age Of Asset">
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="15%"/>
               </asp:BoundField>
                <asp:BoundField DataField="CPUMakeAndModel" HeaderText="CPU" >
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="13%"/>
               </asp:BoundField>
                    <asp:BoundField DataField="RAM" HeaderText="RAM" >
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="15%"/>
               </asp:BoundField>
                    <asp:BoundField DataField="HDD" HeaderText="HDD" >
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="15%"/>
               </asp:BoundField>
        </Columns>
            <PagerSettings NextPageText="&amp;lt;" Mode="NumericFirstLast"  />
            <PagerStyle HorizontalAlign="Right" VerticalAlign="Middle" CssClass="pagination" />
        </asp:GridView>
        </div><br />
       </li>
            <li id="lblNewAssetOptions" visible="false"  runat ="server"><span><br/><b>New Asset Options</b></span><br /></li>

            <li style="width:100%">
         <div>
            <br />
            <asp:GridView ID="gvNewAssetOptions" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" 
             DataKeyNames="Id" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false" PageSize="5" AllowPaging="True" OnRowCommand="gvNewAssetOptions_RowCommand">
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" HorizontalAlign="Center"/>
            <PagerStyle ForeColor="#000066" HorizontalAlign="left" BackColor="White" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />

                <Columns>
                <asp:BoundField DataField="id" HeaderText="Id" ItemStyle-CssClass="hiddencol" >
                <ItemStyle HorizontalAlign="Center"  />
                <HeaderStyle HorizontalAlign="Center" Width="5%" CssClass="hiddencol"/>
                </asp:BoundField>
                <asp:BoundField DataField="AssetNumber" HeaderText="Asset Number">
                <ItemStyle HorizontalAlign="Center"  />
                <HeaderStyle HorizontalAlign="Center" Width="15%"/>
                </asp:BoundField>
                <asp:BoundField DataField="AssetType" HeaderText="Asset Type">
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="10%"/>
                </asp:BoundField>
                <asp:BoundField DataField="AssetDesc" HeaderText="Asset Description">
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="30%"/>
                </asp:BoundField>
                <asp:BoundField DataField="SrNo" HeaderText="Sr. Number">
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="10%"/>
                </asp:BoundField>
                 <asp:BoundField DataField="BrandName" HeaderText="Manufacturer/Brand" >
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                </asp:BoundField>
                <asp:BoundField DataField="Model" HeaderText="Model">
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="8%"/>
                </asp:BoundField>
                <asp:BoundField DataField="AgeOfAsset" HeaderText="Age Of Asset">
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="15%"/>
               </asp:BoundField>
                <asp:BoundField DataField="CPUMakeAndModel" HeaderText="CPU" >
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="13%"/>
               </asp:BoundField>
                    <asp:BoundField DataField="RAM" HeaderText="RAM" >
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="15%"/>
               </asp:BoundField>
                    <asp:BoundField DataField="HDD" HeaderText="HDD" >
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="15%"/>
               </asp:BoundField>
               <asp:TemplateField HeaderText="Delete">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<% #Eval("id") %>' CommandName="DeleteItem" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');">
                    <asp:Image ID="imgedeletemap" runat="server" ToolTip="Delete" ImageUrl="~/Images/delete.png"/></asp:LinkButton>
                </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
            <PagerSettings NextPageText="&amp;lt;" Mode="NumericFirstLast"  />
            <PagerStyle HorizontalAlign="Right" VerticalAlign="Middle" CssClass="pagination" />
        </asp:GridView>
        </div><br />
       </li>
            <li id="liddlAsetNo" runat="server"  visible="false" class ="mobile_inboxEmpCode">  
                 <span>Asset Number</span>
                  <asp:DropDownList ID="ddl_AssetNumber" runat="server" AutoPostBack="true" OnSelectedIndexChanged ="ddl_AssetNumber_SelectedIndexChanged">
                   </asp:DropDownList>
            </li>
           <li></li>
             <li id="litxtSrNo" runat="server" visible="false" class="mobile_inboxEmpCode">  
                <br /> <span>Sr. Number</span>
                 <asp:TextBox ID="txtSrNo" runat="server"></asp:TextBox>
            </li>
            <li></li>
            <li id="litxtDesc" runat="server" visible="false" class="mobile_inboxEmpCode">  
                <span>Description</span>
                 <asp:TextBox ID="txtDesc" runat="server"></asp:TextBox>
            </li>
               <li></li>
            <li>
                <br/>
                <asp:LinkButton ID="claimmob_btnSubmit" visible="false" runat="server" Text="Search Asset" ToolTip="Search Asset" CssClass="Savebtnsve" OnClick="SearchbtnSubmit_Click">Search Asset</asp:LinkButton>
            </li>
            <li></li>
            <li style="width:100%">
         <div>
            <br />
            <asp:GridView ID="gvSearchedAsset" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" 
             DataKeyNames="Id" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false" PageSize="5" AllowPaging="True" OnPageIndexChanging="gvSearchedAsset_PageIndexChanging">
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" HorizontalAlign="Center"/>
            <PagerStyle ForeColor="#000066" HorizontalAlign="left" BackColor="White" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />

            <Columns>
            <asp:TemplateField HeaderText="Select" HeaderStyle-HorizontalAlign="Center">
            <ItemTemplate>
            <asp:CheckBox ID="chkSelect" runat="server" Width="15px" Height="15px" CssClass="rowCheckBox" />
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
             </asp:TemplateField>
                <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-CssClass="hiddencol" >
                <ItemStyle HorizontalAlign="Center"  />
                <HeaderStyle HorizontalAlign="Center" Width="5%" CssClass="hiddencol"/>
            </asp:BoundField>
            <asp:BoundField DataField="AssetNumber" HeaderText="Asset Number">
                <ItemStyle HorizontalAlign="Center"  />
                <HeaderStyle HorizontalAlign="Center" Width="20%"/>
            </asp:BoundField>
            <asp:BoundField DataField="AssetType" HeaderText="Asset Type">
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="10%"/>
            </asp:BoundField>
            <asp:BoundField DataField="AssetDesc" HeaderText="Asset Description">
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="20%"/>
            </asp:BoundField>
            <asp:BoundField DataField="SrNo" HeaderText="Sr. Number">
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="10%"/>
            </asp:BoundField>
             <asp:BoundField DataField="BrandName" HeaderText="Manufacturer/Brand" >
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="15%" />
            </asp:BoundField>
            <asp:BoundField DataField="Model" HeaderText="Model">
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="10%"/>
            </asp:BoundField>
                <asp:BoundField DataField="AgeOfAsset" HeaderText="Age Of Asset">
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="13%"/>
               </asp:BoundField>
                <asp:BoundField DataField="CPUMakeAndModel" HeaderText="CPU" >
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="15%"/>
               </asp:BoundField>
                    <asp:BoundField DataField="RAM" HeaderText="RAM" >
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="15%"/>
               </asp:BoundField>
                    <asp:BoundField DataField="HDD" HeaderText="HDD" >
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" Width="15%"/>
               </asp:BoundField>
        </Columns>
            <PagerSettings NextPageText="&amp;lt;" Mode="NumericFirstLast"  />
            <PagerStyle HorizontalAlign="Right" VerticalAlign="Middle" CssClass="pagination" />
        </asp:GridView>
        </div><br /></li>
            <li></li>
            <li>
                <br/>
                <asp:LinkButton ID="mobile_btnBack" runat="server" Visible="false" Text="Add Asset To Option List" ToolTip="Add Asset To Option List" CssClass="Savebtnsve" OnClick="claimmob_btnSubmit_Click">Add Asset To Option List</asp:LinkButton>
            </li>
            <li></li>
            <hr runat="server" id="Hr1" />
            <li>
                <span>Remarks</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                <asp:TextBox ID="txtRemarks" runat="server" CssClass="noresize" Height="50px" MaxLength="300" TextMode="MultiLine"></asp:TextBox>
            </li> 
            <li></li>
            <li id="CustViewFiles" runat="server" hidden="hidden">
                Uploaded Files<br />
                <div>
                    <asp:GridView id="gvViewFiles" runat="server" CellPadding="4" BorderColor="black" AutoGenerateColumns="False" Width="100%">
                          <Columns>
                                <asp:BoundField DataField="id" HeaderText="Sr.No" Visible="false">
                                    <ItemStyle HorizontalAlign="Left"  />
                                    <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Files" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                        <asp:LinkButton ID="lnkViewFiles" runat="server" Text='<%# Eval("FileName") %>' OnClientClick=<%# "DownloadFile('" + Eval("FileName") + "')" %> >
                                        </asp:LinkButton>
                                        </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                 </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                </div> <br />
            </li> 
            <li></li>
            <li>
                Upload Files<br />
                <asp:FileUpload ID="uploadfile" runat="server"  AllowMultiple="true" />
                <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Please Select Attachment File"
                    ControlToValidate="uploadfile" Display="Dynamic" ForeColor="Red" ValidationGroup="EditNews"></asp:RequiredFieldValidator>
            </li>  
           <li></li>
           <hr runat="server" id="HrgvFiles" />
            <li  id="lblAppDtls" runat="server" visible="false" class ="mobile_inboxEmpCode"> <span><b>Approver Details</b></span><br /><br /></li>
            <li></li>
            <li></li>
            <li id="gvAppFiles" runat="server" visible="false">
                Approver Uploaded Files<br />
                <div>
                    <asp:GridView id="gvAppViewFiles" runat="server" CellPadding="4" BorderColor="black" AutoGenerateColumns="False" Width="100%">
                          <Columns>
                                <asp:BoundField DataField="id" HeaderText="Sr.No" Visible="false">
                                    <ItemStyle HorizontalAlign="Left"  />
                                    <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Files" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                        <asp:LinkButton ID="lnkViewFiles" runat="server" Text='<%# Eval("FileName") %>' OnClientClick=<%# "DownloadFile('" + Eval("FileName") + "')" %> >
                                        </asp:LinkButton>
                                        </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                 </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                </div> 
            </li> 
            <li></li>
            <li id="AppRemark" runat="server" visible ="false"><br />
                Approver Remarks<br />
                <asp:TextBox ID="txtAppRemarks" Enabled="false" runat ="server" CssClass="noresize" Height="50px" MaxLength="300" TextMode="MultiLine"></asp:TextBox>
            </li> 
            <li></li>
         </ul>
                    </div>
                 </div>
            </div>
        </div>
   
    <div class="mobile_Savebtndiv">
        <asp:LinkButton ID="mobile_btnSave" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click" OnClientClick="return SaveMultiClick();">Submit For Approval</asp:LinkButton>
        <%--<asp:LinkButton ID="mobile_cancel" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="Savebtnsve" OnClick="mobile_cancel_Click" OnClientClick="return CancelMultiClick();">Cancel</asp:LinkButton>--%>
        <asp:LinkButton ID="mobile_cancel" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/ITAssetService.aspx">Back</asp:LinkButton>
        </div>
    <br />
    <asp:HiddenField ID="hdnRemid" runat="server" />
    <asp:HiddenField ID="hdnleaveTypeid" runat="server" />
    <asp:HiddenField ID="hdnleaveType" runat="server" />
    <asp:HiddenField ID="hdnEmpCode" runat="server" />                
    <asp:HiddenField ID="hdnApproverType" runat="server" />
    <asp:HiddenField ID="hdnId" runat="server" />
    <asp:HiddenField ID="FilePath" runat="server" />
    <asp:HiddenField ID="hdn_Attchment" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="hdnReqStatus" runat="server" /> 

    <script src="../js/dist/jquery-3.2.1.min.js"></script>       
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" /> 

    <script type="text/javascript">      
        $(document).ready(function () {
            $("#MainContent_ddl_Employee").select2();
        });
    </script>
    <script type="text/javascript">

        function DownloadFile(file) {
            var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;
            window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
        }
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
        //below funcations for calendar
        function onCalendarShown() {

            var cal = $find("calendar1");
            //Setting the default mode to month
            cal._switchMode("months", true);

            //Iterate every month Item and attach click event to it
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }

        function onCalendarHidden() {
            var cal = $find("calendar1");
            //Iterate every month Item and remove click event from it
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }

        }
        function call(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("calendar1");
                    cal._visibleDate = target.date;
                    cal.set_selectedDate(target.date);
                    cal._switchMonth(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged();
                    break;
            }
        }
        function noanyCharecters(e) {
            var keynum;
            var keychar;
            var numcheck = /[]/;

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
            //alert();
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
                        retunboolean = Confirm();

                    if (retunboolean == false)
                        ele.disabled = false;
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
                var ele = document.getElementById('<%=mobile_cancel.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        var retunboolean = Confirm();

                    if (retunboolean == false)
                        ele.disabled = false;
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }

        function Confirm() {
            //Testing();
            var confirmval = false;
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Are you sure you want to submit?")) {
                confirm_value.value = "Yes";
                confirmval = true;
            } else {
                confirm_value.value = "No";
                confirmval = false;
            }
            //alert(confirm_value.value);
            //document.forms[0].appendChild(confirm_value);
            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return confirmval;

        }
        function CheckOtherIsCheckedByGVID(spanChk) {
            var IsChecked = spanChk.checked;
            if (IsChecked) {
                spanChk.parentElement.parentElement.style.backgroundColor = '#3D1956';
                spanChk.parentElement.parentElement.style.color = 'white';
            }
            var CurrentRdbID = spanChk.id;
            var Chk = spanChk;
            Parent = document.getElementById("<%=gvproject.ClientID%>");
            var items = Parent.getElementsByTagName('input');
            for (i = 0; i < items.length; i++) {
                if (items[i].id != CurrentRdbID && items[i].type == "radio") {
                    if (items[i].checked) {
                        items[i].checked = false;
                        items[i].parentElement.parentElement.style.backgroundColor = 'white'
                        items[i].parentElement.parentElement.style.color = 'black';
                    }
                }
            }
        }
    </script>
</asp:Content>


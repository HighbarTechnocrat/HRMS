<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="ExitProcess_ApprovedClearance.aspx.cs" Inherits="procs_ExitProcess_ApprovedClearance" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/manageleave_css.css" type="text/css" media="all" />
    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
            background: #dae1ed;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
	 <script src="../js/dist/jquery-3.2.1.min.js"></script>
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
                        <asp:Label ID="lblheading" runat="server" Text="Approved Clearance"></asp:Label>
                    </span>
                </div>



                <span>
                    <a href="ExitProcess_Index.aspx" class="aaa">Exit Process Menu</a>
                </span>


                <div class="edit-contact">
                    <ul id="Ul1" runat="server">

                        <li style="padding-top: 10px">
                            <span style="font-size:14px">Employee Name</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList runat="server" ID="lstEmployeeName" CssClass="DropdownListSearch">
                            </asp:DropDownList>
                        </li>

                        <li style="padding-top: 10px">
                            <span style="font-size:14px">Project</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList runat="server" CssClass="DropdownListSearch" ID="lstProjets">
                            </asp:DropDownList>
                        </li>
                                                
                    </ul>
                    <%--<div>
                        <asp:LinkButton ID="LinkButton1" runat="server" Text="Submit" ToolTip="Search" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click">Search </asp:LinkButton>
                        <asp:LinkButton ID="LinkButton2" runat="server" Text="Clear Search" OnClick="mobile_btnBack_Click" ToolTip="Clear Search" class="trvl_Savebtndiv">Clear Search</asp:LinkButton>
                    </div>--%>
                </div>


                <div class="mobile_Savebtndiv" style="margin-top: 25px !important">
                    <asp:LinkButton ID="LinkButton1" runat="server" Text="Submit" ToolTip="Search" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click">Search </asp:LinkButton>
                    <asp:LinkButton ID="LinkButton2" runat="server" Text="Clear Search" OnClick="mobile_btnBack_Click" ToolTip="Clear Search" class="trvl_Savebtndiv">Clear</asp:LinkButton>
                    
                </div>


                <div class="manage_grid" style="width: 100%; height: auto;">

                    <asp:Label runat="server" ID="RecordCount" Style="color: red; font-size: 14px;"></asp:Label>

                    <center style="padding-top: 25px">
                        <asp:GridView ID="gvMngLeaveRqstList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="solid" BorderWidth="1px"
                            DataKeyNames="ResignationID" CellPadding="3" AutoGenerateColumns="False" Width="100%" OnPageIndexChanging="gvMngLeaveRqstList_PageIndexChanging"  OnRowDataBound="gvMngTravelRqstList_RowDataBound" EditRowStyle-Wrap="false" PageSize="20" AllowPaging="True">
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
                                 <asp:TemplateField HeaderText="View" HeaderStyle-Width="5%">
                                 <ItemTemplate>
                                     <asp:ImageButton ID="lnkEdit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkEdit_Click" />
                                     <asp:ImageButton id="lnkView" runat="server" Visible="false" Width="15px" ToolTip="Edit" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkView_Click"/>
                                 </ItemTemplate>
                                 <ItemStyle HorizontalAlign="Center" />
                             </asp:TemplateField>
                                <asp:BoundField HeaderText="Employee Code"
                                    DataField="EmployeeCode"
                                    ItemStyle-HorizontalAlign="center"
                                    HeaderStyle-HorizontalAlign="center"
                                    ItemStyle-Width="7%"
                                    ItemStyle-BorderColor="Navy" />

                                <asp:BoundField HeaderText="Employee Name"
                                    DataField="Employee Name"
                                    ItemStyle-HorizontalAlign="center"
                                    ItemStyle-Width="10%"
                                    ItemStyle-BorderColor="Navy" />

                                <asp:BoundField HeaderText="Department"
                                    DataField="Department_Name"
                                    ItemStyle-HorizontalAlign="center"
                                    ItemStyle-Width="10%"
                                    ItemStyle-BorderColor="Navy" />

                                <asp:BoundField HeaderText="Project"
                                    DataField="emp_projectName"
                                    ItemStyle-HorizontalAlign="center"
                                    ItemStyle-Width="8%"
                                    ItemStyle-BorderColor="Navy" />
                                <asp:BoundField HeaderText="Resignation Date"
                                    DataField="Resignation Date"
                                    ItemStyle-HorizontalAlign="center"
                                    ItemStyle-Width="8%"
                                    ItemStyle-BorderColor="Navy" />
                                <asp:BoundField HeaderText="Last Working Day"
                                    DataField="LastWorkingDay"
                                    ItemStyle-HorizontalAlign="center"
                                    ItemStyle-Width="8%"
                                    ItemStyle-BorderColor="Navy" />
                                <asp:BoundField HeaderText="Reason Of Resignation"
                                    DataField="Reason Of Resignation"
                                    ItemStyle-HorizontalAlign="center"
                                    ItemStyle-Width="8%"
                                    ItemStyle-BorderColor="Navy" />
                                <asp:BoundField HeaderText="Resignation submitted on"
                                    DataField="Resignation submitted on"
                                    ItemStyle-HorizontalAlign="center"
                                    ItemStyle-Width="8%"
                                    ItemStyle-BorderColor="Navy" />
                                

                            </Columns>
                        </asp:GridView>
                    </center>
                </div>


                <%--'<%#"~/Details.aspx?ID="+Eval("ID") %>'>--%>



                <div class="edit-contact">
                    <%-- <div class="editprofile" style="text-align: center; border: none;" id="divmsg" runat="server" visible="false">
                        <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: green; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                    </div>--%>
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server"></asp:LinkButton>
                        </div>
                    </div>


                    <ul id="editform" runat="server" visible="false">

                        <li>
                            <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                        </li>

                    </ul>
                </div>
                <asp:HiddenField ID="hdnRetentionID" runat="server" />
                <asp:HiddenField ID="hdnInboxType" runat="server" />
                <asp:HiddenField ID="hdnReqid" runat="server" />
                <asp:HiddenField ID="hdnleaveTypeid" runat="server" />
                <asp:HiddenField ID="hdnleaveType" runat="server" />
                <asp:HiddenField ID="hdnEmpCode" runat="server" />
            </div>
        </div>
    </div>


	<script src="../js/dist/select2.min.js"></script>
	<link href="../js/dist/select2.min.css" rel="stylesheet" /> 
	<script type="text/javascript">
         
		$(document).ready(function () {
			$(".DropdownListSearch").select2(); 
		});

         
	</script>

</asp:Content>


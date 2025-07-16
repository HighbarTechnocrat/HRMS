<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" 
	CodeFile="InboxCustEscalationApp.aspx.cs" Inherits="InboxCustEscalationApp" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
     <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/manageleave_css.css" type="text/css" media="all" />
   
    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
             /*background: #dae1ed;*/
           background:#ebebe4;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
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
                        <asp:Label ID="lblheading" runat="server" Text="Customer Incident"></asp:Label>
                    </span>
                </div>

				<div>			
                    <a href="CustEscalation.aspx" class="aaaa">Cust Incident Home</a>
                </div>
				

                <div class="manage_grid" style="width: 100%; height: auto;padding-top:40px">
                    <center>
                          <asp:GridView ID="gvMngTravelRqstList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" 
                            DataKeyNames="Id" CellPadding="3" AutoGenerateColumns="False" Width="100%"   EditRowStyle-Wrap="false" PageSize="20" AllowPaging="True" OnPageIndexChanging="gvMngTravelRqstList_PageIndexChanging">
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
                            <asp:BoundField HeaderText="Customer Request ID"
                                DataField="ServicesRequestID"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="12%" />

                            <asp:BoundField HeaderText="Creation Date"
                                DataField="ServiceRequestDate"
                                ItemStyle-HorizontalAlign="Center"                                
                                ItemStyle-Width="8%" />

                            <%--<asp:BoundField HeaderText="Employee Name"
                                DataField="EmployeeName"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="15%" />--%>

							 <asp:BoundField HeaderText="Project Name"
                                DataField="Location_name"
                                ItemStyle-HorizontalAlign="Left"                                
                                ItemStyle-Width="20%" />
							 <asp:BoundField HeaderText="Name of Person (Client)"
                                DataField="EscalationRaisedBy"
                                 ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="14%" />
                                                     
                            <%--<asp:BoundField HeaderText="Assigned To"
                                DataField="AssignedTo"
                                 ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="15%" />

                            <asp:BoundField HeaderText="Assignment Date"
                                DataField="AssignmentDate"
                                 ItemStyle-HorizontalAlign="Center"                                
                                ItemStyle-Width="15%" />--%>
							 <asp:BoundField HeaderText="Incident Owner"
                                DataField="IncidentOwner"
                                 ItemStyle-HorizontalAlign="Left"                                
                                ItemStyle-Width="14%" />

                            <asp:BoundField HeaderText="Date of Closure"
                                DataField="DeliveryDate"
                                 ItemStyle-HorizontalAlign="Center"                                
                                ItemStyle-Width="8%" />

                            <asp:BoundField HeaderText="Status"
                                DataField="Status"
                                 ItemStyle-HorizontalAlign="Center"                                
                                ItemStyle-Width="10%" />

                            <asp:TemplateField HeaderText="View" HeaderStyle-Width="5%">
                            <ItemTemplate>
                            <%--<asp:LinkButton ID="lnkEdit" runat="server" Text='View' OnClick="lnkEdit_Click1"></asp:LinkButton>--%>
                            <asp:ImageButton id="lnkEdit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkFuelDetails_Click"/>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                        </center>
					<br />
					<br />
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

                        <li>
                            <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                        </li>

                    </ul>
                </div>
                <asp:HiddenField ID="hdnRemid" runat="server" />
                <asp:HiddenField ID="hdnleaveTypeid" runat="server" />
                <asp:HiddenField ID="hdnleaveType" runat="server" />
                <asp:HiddenField ID="hdnEmpCode" runat="server" />                
                <asp:HiddenField ID="hdnApproverType" runat="server" />
                <asp:HiddenField ID="hdninboxtype" runat="server" />
            </div>
        </div>
    </div>


    <script type="text/javascript">
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

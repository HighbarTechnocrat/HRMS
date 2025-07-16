<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="ViewSurveyReport.aspx.cs" Inherits="ViewSurveyReport" %>


<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/manageleave_css.css" type="text/css" media="all" />
        <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/ClaimMobile_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leaveteamrpt.css" type="text/css" media="all" />

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
    <div class="userposts">
        <span>
            <asp:Label ID="lblheading" runat="server" Text="CustomerFIRST - View Survey"><%=ConfigurationManager.AppSettings["CustomerFeedbackTitle"]%> - View Survey</asp:Label>
        </span>
    </div>
        <div class="leavegrid">
        <a href="customerFirst.aspx" class="aaaa"><%=ConfigurationManager.AppSettings["CustomerFeedbackTitle"]%> Home</a>
    </div>

    <%--'<%#"~/Details.aspx?ID="+Eval("ID") %>'>--%>



    <div class="edit-contact">
        <%-- <div class="editprofile" style="text-align: center; border: none;" id="divmsg" runat="server" visible="false">
            <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: green; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
        </div>--%>
<%--        <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
            <div class="cancelbtndiv">
                <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
            </div>
            <div class="cancelbtndiv">
                <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" OnClick="lnkcont_Click"></asp:LinkButton>
            </div>
        </div>--%>


        <ul id="editform" runat="server" visible="false">

            <li>
                <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
            </li>

        </ul>
        <ul>
            
            <li>
                <span style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">Survey Number</span><br />
                <asp:TextBox ID="txtSurveyNumber" runat="server" ToolTip="Enter survey number " CssClass="txtcls" ></asp:TextBox>
            </li>   
            <li>
                <span style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">Survey Date From</span><br />
                 <asp:TextBox ID="txtDate"  AutoComplete="off" runat="server" AutoPostBack="true" OnTextChanged="txtDate_TextChanged" MaxLength="10" AutoCompleteType="Disabled"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd-MM-yyyy" TargetControlID="txtDate"
                                      runat="server">
                                </ajaxToolkit:CalendarExtender>                
            </li>
            <li>
                <span style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">Survey Date To</span><br />
                 <asp:TextBox ID="txtToDate"  AutoComplete="off" runat="server" AutoPostBack="true" OnTextChanged="txtToDate_TextChanged" MaxLength="10" AutoCompleteType="Disabled"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd-MM-yyyy" TargetControlID="txtToDate"
                                      runat="server">
                                </ajaxToolkit:CalendarExtender>                
            </li>
            <li>
                 <span style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">Business Unit</span><br /><br />
                <asp:DropDownList ID="ddlDepartment" runat="server">
                </asp:DropDownList>
            </li>
             <li>
                 <span style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">Response Status</span><br /><br />
                <asp:DropDownList ID="ddlResponseStatus" runat="server">
                    <asp:ListItem Value="All" Selected="True">All</asp:ListItem>
                    <asp:ListItem Value="Received">Received</asp:ListItem>
                    <asp:ListItem Value="Pending">Pending</asp:ListItem>
                </asp:DropDownList>
            </li>
        </ul>
        <ul>
            <%--<li></li>--%>
            <li>
                <br />
                <asp:LinkButton ID="claimmob_btnSubmit" runat="server" Text="Search" ToolTip="Search" CssClass="Savebtnsve" OnClick="claimmob_btnSubmit_Click">Search</asp:LinkButton>
            </li>
        </ul>
    </div>


    <div class="manage_grid" style="width: 100%; height: auto;">
         <br />
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
                            <asp:BoundField HeaderText="Survey No"
                                DataField="SurveyNo"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="20%" />

                            <asp:BoundField HeaderText="Client"
                                DataField="ClientName"
                                ItemStyle-HorizontalAlign="Center"                                
                                ItemStyle-Width="15%" />

                            <asp:BoundField HeaderText="Contact Name"
                                DataField="ContactName"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="10%" />
                                                        
                            <asp:BoundField HeaderText="Contact Designation"
                                DataField="ContactDesignation"
                                 ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="15%" />

                            <asp:BoundField HeaderText="Question"
                                DataField="Question"
                                 ItemStyle-HorizontalAlign="Left"                                
                                ItemStyle-Width="15%" />

                            <asp:BoundField HeaderText="Sent Date"
                                DataField="CreatedDate"
                                 ItemStyle-HorizontalAlign="Center"                                
                                ItemStyle-Width="15%" />

                            <asp:BoundField HeaderText="Reply Date"
                                DataField="ClientReplyDate"
                                 ItemStyle-HorizontalAlign="Center"                                
                                ItemStyle-Width="15%" />

                            <asp:TemplateField HeaderText="Answer"  HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Image ID="Image1" Height="50" Width="50" runat="server"
                                                ImageUrl='<%# ResolveUrl(Eval("IconFilePath").ToString()) %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                            <asp:TemplateField HeaderText="View" HeaderStyle-Width="5%">
                            <ItemTemplate>

                            <%--<asp:LinkButton ID="lnkEdit" runat="server" Text='View' OnClick="lnkEdit_Click1"></asp:LinkButton>--%>
                            <asp:ImageButton id="lnkEdit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkEdit_Click"/>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
        </asp:GridView>
            </center>
    </div>

    <asp:HiddenField ID="hdnRemid" runat="server" />
    <asp:HiddenField ID="hdnleaveTypeid" runat="server" />
    <asp:HiddenField ID="hdnleaveType" runat="server" />
    <asp:HiddenField ID="hdnEmpCode" runat="server" />                
    <asp:HiddenField ID="hdnApproverType" runat="server" />
    <asp:HiddenField ID="hdninboxtype" runat="server" />

        <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchSurveyId" MinimumPrefixLength="1"
        CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtSurveyNumber"
        ID="AutoCompleteExtender1" runat="server" FirstRowSelected="true">
    </ajaxToolkit:AutoCompleteExtender>

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
    </script>
</asp:Content>


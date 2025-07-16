<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="InboxMobile_Arch.aspx.cs" Inherits="myaccount_InboxMobileReimbursement_ACC" %>


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

    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>

    <script src="../js/dist/jquery-3.2.1.min.js"></script>
     <script src="../js/dist/select2.min.js"></script>
	<link href="../js/dist/select2.min.css" rel="stylesheet" />

     <script type="text/javascript">      
		$(document).ready(function () {			
			$(".DropdownListSearch").select2();		
		});
        </script>

    
    <div class="userposts">
        <span>
            <asp:Label ID="lblheading" runat="server" Text="Approved Mobile Claims"></asp:Label>
        </span>
    </div>

    <div class="leavegrid">
        <a href="Mobile.aspx" class="aaaa">Mobile Claim Home</a>
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
                <span style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">Employee Name:</span><br />
                <%--<asp:TextBox ID="txtApplicantName" runat="server" ToolTip="Enter employee name or code " CssClass="txtcls" ></asp:TextBox>--%>

                 <asp:DropDownList ID="DDL_txtApplicantName"  CssClass="DropdownListSearch" runat="server" TabIndex="0"> 
                 </asp:DropDownList>

            </li>
            <li>
                <span style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">Bill Month:</span><br />
                <asp:TextBox ID="txtDate" runat="server" AutoComplete="off" Format="MMM/yyyy" Font-Bold="true" AutoPostBack="False" Width="200px"></asp:TextBox>                    
                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" OnClientHidden="onCalendarHidden"  OnClientShown="onCalendarShown" BehaviorID="calendar1" Enabled="True"  TargetControlID="txtDate" Format="MMM/yyyy" PopupButtonID="imgcalendarFileDate"  DefaultView="Months"
                    runat="server">
                </ajaxToolkit:CalendarExtender>
            </li>
        </ul>
        <ul>
            <li>
                <asp:LinkButton ID="claimmob_btnSubmit" runat="server" Text="Search" ToolTip="Search" CssClass="Savebtnsve" OnClick="claimmob_btnSubmit_Click">Search</asp:LinkButton>
            </li>
        </ul>
    </div>

    
    <div class="manage_grid" style="width: 100%; height: auto;">
        <center>
                <asp:GridView ID="gvMngTravelRqstList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" 
                DataKeyNames="Rem_id" CellPadding="3" AutoGenerateColumns="False" Width="100%"   EditRowStyle-Wrap="false" PageSize="20" AllowPaging="True" OnPageIndexChanging="gvMngTravelRqstList_PageIndexChanging" >
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
            <asp:BoundField HeaderText="Voucher No."
                DataField="vouno"
                ItemStyle-HorizontalAlign="Center"
                HeaderStyle-HorizontalAlign="Center"
                ItemStyle-Width="20%" />

                <asp:BoundField HeaderText="Claim Date"
                    DataField="Submission On"
                    ItemStyle-HorizontalAlign="Center"                                
                    ItemStyle-Width="15%" />

                <asp:BoundField HeaderText="Bill Type"
                    DataField="BillType"
                    ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="10%" />
                                            
                <asp:BoundField HeaderText="From Date"
                    DataField="From_date"
                        ItemStyle-HorizontalAlign="center"
                    ItemStyle-Width="9%" />

                  <asp:BoundField HeaderText="To Date"
                    DataField="To_date"
                        ItemStyle-HorizontalAlign="center"
                    ItemStyle-Width="9%" />

                <asp:BoundField HeaderText="Bill Amount"
                    DataField="TotalAmount_Claimed"
                        ItemStyle-HorizontalAlign="Center"                                
                    ItemStyle-Width="15%" />

                <asp:BoundField HeaderText="Employee Name"
                    DataField="Applicant"
                        ItemStyle-HorizontalAlign="Center"                                
                    ItemStyle-Width="20%" />
                            
                <asp:TemplateField HeaderText="View" HeaderStyle-Width="5%">
                    <ItemTemplate>
                    <asp:ImageButton id="lnkEdit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkFuelDetails_Click"/>
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

   <%-- <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchEmployees_M" MinimumPrefixLength="1"
        CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtApplicantName"
        ID="AutoCompleteExtender1" runat="server" FirstRowSelected="true">
    </ajaxToolkit:AutoCompleteExtender>--%>


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


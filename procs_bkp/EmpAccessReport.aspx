<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="EmpAccessReport.aspx.cs" Inherits="myaccount_EmpAccessReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leaveteamrpt.css" type="text/css" media="all" />


    <style>
        .btnrpt {
            background-attachment: scroll;
            background-clip: border-box;
            background-color: #febf39;
            background-image: none;
            background-origin: padding-box;
            background-position-x: 0;
            background-position-y: 0;
            background-repeat: repeat;
            background-size: auto auto;
            padding-bottom: 8px;
            padding-left: 23px;
            padding-right: 23px;
            padding-top: 8px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">


    <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Visible="false">

    </asp:Label>
    <div class="leavegrid">
        <a href="Leaveindex.aspx" class="aaa" style="margin-bottom: 50px;">Leave Menu</a>
    </div>

    <table style="width: 100%; margin-bottom: 15px;" class="table table-dark">
        <tr>
            <td>
                <asp:Label ID="lbl" runat="server" Text="Select Month"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtmonth" runat="server" Width="150px" MaxLength="10" autocomplete="off"></asp:TextBox>

                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" OnClientHidden="onCalendarHidden" OnClientShown="onCalendarShown" TargetControlID="txtmonth"
                    Format="MMM/yyyy" runat="server" BehaviorID="calendar1">
                </ajaxToolkit:CalendarExtender>
            </td>


            <td>
                <asp:Label ID="lbllocation" runat="server" Text="Select Company" Style="margin-left: 120px;"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlCompany" runat="server" Width="250px" Style="margin-right: 200px;"></asp:DropDownList>
            </td>
            <td>
                <asp:LinkButton ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" CssClass="btnrpt" Style="margin-right: 50px;" /></td>
        </tr>
    </table>


    <div id="dvdata" runat="server" style="width: 1100px; height: 400px;display:none;">
    </div>

    <div class="manage_grid" style="width: 90%;height:50%; overflow-y: scroll;">
        <center>
        <asp:GridView ID="dgReport" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="false"
             Width="100%" EditRowStyle-Wrap="false" EmptyDataText="No Data Found">
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="false" ForeColor="White" />
            <PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />

            <Columns>
                <asp:TemplateField HeaderText="Location Code" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>

                        <asp:Label ID="locCode" runat="server" Text='<%#Eval("emp_location")%>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Location Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="locname" runat="server" Text='<%#Eval("Location_name")%>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Count" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblCount" runat="server" Text='<%#Eval("Count")%>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>



            </Columns>
        </asp:GridView>


        </center>

    </div>




    <script type="text/javascript">



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

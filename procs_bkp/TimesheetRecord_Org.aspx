<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="TimesheetRecord_Org.aspx.cs" Inherits="TimesheetRecord_Org" EnableEventValidation="false" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>
<%@ Register TagName="calender" TagPrefix="ucical" Src="~/themes/creative1.0/LayoutControls/leaveCalender.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/ModifyLeaveRequest_css.css" type="text/css" media="all" />
    <%-- <link href="<%=ReturnUrl("css") %>calendar/calendar.css" rel="stylesheet" type="text/css" />
    <link href="sampleform_css.css" rel="stylesheet" type="text/css" />--%>
    <%--<ajaxToolkit:ToolkitScriptManager ID="srcp" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    
    <style>
#MainContent_btnIn {
    background-attachment: scroll;
    background-clip: border-box;
    /*by Highbartech on 24-06-2020
        background: #febf39;*/
    background-color: #3D1956;
    color: #febf39 !important;
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
.btnSaveCss {
    background-attachment: scroll;
    background-clip: border-box;
    /*by Highbartech on 24-06-2020
        background: #febf39;*/
    background-color: #3D1956;
    color: #febf39 !important;
    background-image: none;
    background-origin: padding-box;
    background-position-x: 0;
    background-position-y: 0;
    background-repeat: repeat;
    background-size: auto auto;
    padding-bottom: 5px;
    padding-left: 23px;
    padding-right: 23px;
    padding-top: 5px;
}
          #content-container #gvMain {
            width: 231% !important;
        }
        #MainContent_icalender_Calendar1
        {
            color: Black;
    border-color: #2A2A2A;
    border-width: 1px;
    border-style: solid;
    font-size: 14px;
    font-weight: normal;
    text-decoration: none;
    width: 195px !important;
    border-collapse: collapse;
        }
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }


        .Dropdown {
            border-bottom: 2px solid #cccccc;
            /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
            background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;
            cursor: default;
        }

        .grayDropdown {
            border-bottom: 2px solid #cccccc;
            /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
            background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;
            background-color: #ebebe4;
        }

        .grayDropdownTxt {
            background-color: #ebebe4;
        }

        .taskparentclass2 {
            width: 29.5%;
            height: 112px;
        }

        .taskparentclasskkk {
            width: 29.5%;
            height: 72px;
            /*overflow:initial;*/
        }

        #MainContent_ListBox1, #MainContent_ListBox2 {
            padding: 0 0 0% 0 !important;
            /*overflow: unset;*/
        }

        #MainContent_lstApprover, #MainContent_lstIntermediate {
            padding: 0 0 33px 0 !important;
            /*overflow: unset;*/
        }

         .noresize {
            resize: none;
        }     
          /* Style the tab */
        .tab {
            overflow: hidden !important;
            border: 1px solid black !important;
            background-color: #C7D3D4 !important;
            position: relative !important;
            margin-bottom: 1px !important;
            width: 227% !important;
            color: #fff !important;
            overflow: hidden !important;
        }

            /* Style the buttons inside the tab */
            .tab button {
                background-color: inherit;
                float: left;
                border: none;
                outline: none;
                cursor: pointer;
                padding: 14px 16px;
                transition: 0.3s;
                font-size: 17px;
            }

                /* Change background color of buttons on hover */
                .tab button:hover {
                    background-color: #3D1956 !important;
                    color: #febf39 !important;
                }

                /* Create an active/current tablink class */
                .tab button.active {
                    background-color: #3D1956 !important;
                    color: #febf39 !important;
                }

        /* Style the tab content */
        .tabcontent {
            display: none;
            padding: 6px 12px;
            border: 1px solid black;
            border-top: none;
            width: 221% !important;
        }

        .tablinks {
            background-color: #C7D3D4 !important;
        }
        span.select2.select2-container.select2-container--default {
            width: 85% !important;
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
                        <asp:Label ID="lblheading" runat="server" Text="Record Timesheet"></asp:Label>
                    </span>
                  
                    <%--PostBackUrl="~/procs/Index.aspx"--%>
                </div>
                <div>
                      <span>
                     <a href="Timesheet.aspx" class="aaaa" >Timesheet Menu</a>
                </span>
                </div>
                <div class="leavegrid" style="display:none;">                  

                </div>

                <div class="edit-contact">
                    <%-- <div class="editprofile" style="text-align: center; border: none;" id="divmsg" runat="server" visible="false">
                        <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: green; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                    </div>--%>
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
<%--                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" OnClick="lnkcont_Click"></asp:LinkButton>
                        </div>--%>
                    </div>

                    <div class="editprofile" id="editform1" runat="server" visible="true" style="margin: 7px -12% !important;">
                            <div class="editprofileform">
                                <ucical:calender ID="icalender" runat="server"></ucical:calender>
                            </div>
                    </div>

                    <ul id="editform" runat="server" visible="false">

                        <li>
                            <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                            <%--<asp:TextBox runat="server" ID="lblmessage1" Visible="False" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;" OnTextChanged="lblmessage1_TextChanged"></asp:TextBox>--%>
                        </li>


                        <li></li>
                        <li>
                            <span>Timesheet Summary</span>
                            <br />
                            <br />
                               <table style="width:203%;">                                 
                                <tr">
                                 <%--   <td style="color:cornflowerblue">
                                        <table>
                                            <tr>
                                                <th >Shift Full Day Hours</th> <td id="trFullShift" runat="server"></td>
                                            </tr>
                                            <tr>
                                                <th>Shift Half Day Hours</th> <td id="trHalfShift" runat="server"></td>
                                            </tr>
                                        </table>
                                    </td>--%>
                                    <td>Legends</td>
                                    <td class="anlign">
                                        <asp:Image ID="imgLeavePending" runat="server"
                                            ImageUrl="~/images/Calendar/PendLeave.png" Height="13px" Width="25px" Style="margin-bottom: 0px" />
                                        &nbsp;Leave
                                    </td>
                                    <td class="anlign">
                                        <asp:Image ID="HolidayImg" runat="server"
                                            ImageUrl="~/images/Calendar/Holiday_1.png" Height="13px" Width="25px" />
                                        &nbsp;Holiday                   
                           
                                    </td>
                                    <td class="anlign">
                                        <asp:Image ID="imgLeaveApproved" runat="server"
                                            ImageUrl="~/images/Calendar/weekend.png" Height="13px" Width="25px" Style="margin-bottom: 0px" />
                                        &nbsp;Weekend
                                    </td>
                                </tr>
                               
                            </table>
                        </li>
                        <li></li>

                        <li>             
                            
                            <br />
                            <asp:PlaceHolder ID="PlaceHolder1" runat="server" />
                            <%--  <div style="width:100%;overflow:auto">--%>
                            <div id="div_gridholder" style="width: 100%; overflow: auto;">
                                <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333"
                                    GridLines="Both" Style="display: none;" Width="100%"
                                    AutoGenerateColumns="true" OnRowDataBound="GridView1_RowDataBound">
                                </asp:GridView>

                            </div>    
                        </li>
                        <li></li>
                        <li style="visibility:hidden;height: 0px !important;">
                            <span>Date</span> &nbsp;&nbsp;<span style="color: red">*</span><br />
                                            <asp:TextBox ID="txtFromdate" AutoComplete="off" runat="server" AutoPostBack="true" OnTextChanged="txtFromdate_TextChanged" MaxLength="10" AutoCompleteType="Disabled"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtFromdate" runat="server">
                                            </ajaxToolkit:CalendarExtender>
                        </li>
                        <li style="width:78%">
                            <br />
                            <span style="color:red">Note: Please make sure there are no balance Hours for any date. Leave would be deducted against balance hours proportionately.</span>
                        </li>
                        <li>
                            <br />
                            <span>Timesheet Details</span>
                            <br />
                            <div class="tab">
                                <button class="tablinks active" style="border-right: 1px solid !important;" runat="server" id="btn1" onclick="openCity(event, 'tab1');return false;"></button>
                                <button class="tablinks" style="border-right: 1px solid !important" runat="server" id="btn2" onclick="openCity(event, 'tab2');return false;"></button>
                                <button class="tablinks" style="border-right: 1px solid !important" runat="server" id="btn3" onclick="openCity(event, 'tab3');return false;"></button>
                                <button class="tablinks" style="border-right: 1px solid !important" runat="server" id="btn4" onclick="openCity(event, 'tab4');return false;"></button>
                                <button class="tablinks" style="border-right: 1px solid !important" runat="server" id="btn5" onclick="openCity(event, 'tab5');return false;"></button>
                                <button class="tablinks" style="border-right: 1px solid !important" runat="server" id="btn6" onclick="openCity(event, 'tab6');return false;"></button>
                                <button class="tablinks" runat="server" id="btn7" onclick="openCity(event, 'tab7');return false;"></button>
                            </div>
                            <div id="tab1" class="tabcontent active" style="display:block">
                                <asp:Label runat="server" ID="lblmessagetab1" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                                <table style="width:100% !important">
                                    <tr>
                                        <td style="width: 35% !important;">
                                            <span>Select Project</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                            <asp:DropDownList runat="server" ID="ddlProject"></asp:DropDownList>
                                            <asp:TextBox runat="server" Visible="false" ID="txtProject"></asp:TextBox>
                                        </td>
                                        <td style="width: 35% !important;">
                                            <span>Select Task</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                            <asp:DropDownList runat="server" ID="ddlTask"></asp:DropDownList>
                                            <asp:TextBox runat="server" Visible="false" ID="txtTask"></asp:TextBox>
                                        </td>
                                        <td style="width: 30% !important; padding:12px 0px 0px 0px !important;"><span>Hours (12 Hrs - HH:MM)</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                            <asp:TextBox MaxLength="50" ID="txtHours" runat="server" Width="100px"></asp:TextBox>
                                        </td>

                                    </tr>
                                    <tr>
                                         <td colspan="3">
                                            <span>Note</span>&nbsp;&nbsp;<br />
                                            <asp:TextBox MaxLength="500" TextMode="MultiLine" Rows="4" Width="84%" CssClass="noresize" ID="txtDescription" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>                                       
                                        <td></td>
                                        <td></td>
                                        <td style="text-align:center">
                                            <asp:LinkButton ID="lnk_Dt1" runat="server" CssClass="Savebtnsve btnSaveCss" Text="Submit" OnClick="lnk_Dt1_Click" OnClientClick="return validateDT1();">Save</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:GridView ID="gvDT1" runat="server" BackColor="White" DataKeyNames="Id,Timesheet_id" BorderColor="Navy" PageSize="20" AllowPaging="True" OnPageIndexChanging="DgvApprover_PageIndexChanging" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%">
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
                                                    <asp:BoundField HeaderText="Project"
                                                        DataField="ProjectName"
                                                        ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="20%"
                                                        ItemStyle-BorderColor="Navy" />

                                                    <asp:BoundField HeaderText="Task"
                                                        DataField="Activity_Desc"
                                                        ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="30%"
                                                        ItemStyle-BorderColor="Navy" />
                                                    <asp:BoundField HeaderText="Hours"
                                                        DataField="Hours"
                                                        ItemStyle-HorizontalAlign="center"
                                                        HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="2%"
                                                        ItemStyle-BorderColor="Navy" />
                                                    <asp:BoundField HeaderText="Note"
                                                        DataField="Description"
                                                        ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="30%"
                                                        ItemStyle-BorderColor="Navy" />
                                                    <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="lnkEditdt1" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkEditdt1_Click" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>                                
                                <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
                            </div>

                            <div id="tab2" class="tabcontent">
                                <asp:Label runat="server" ID="lblmessagetab2" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                                <table style="width:100% !important">
                                    <tr>                                      
                                        <td style="width: 35% !important;">
                                            <span>Select Project</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                            <asp:DropDownList runat="server" ID="ddlProject1"></asp:DropDownList>
                                            <asp:TextBox runat="server" Visible="false" ID="txtProject1"></asp:TextBox>
                                        </td>
                                        <td style="width: 35% !important;">
                                            <span>Select Task</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                            <asp:DropDownList runat="server" ID="ddlTask1"></asp:DropDownList>
                                            <asp:TextBox runat="server" Visible="false" ID="txtTask1"></asp:TextBox>
                                        </td>
                                          <td style="width:30%;padding: 12px 0px 0px 0px !important;"><span>Hours (12 Hrs - HH:MM)</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                            <asp:TextBox MaxLength="50" ID="txtHours1" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                       
                                    </tr>
                                    <tr>
                                         <td colspan="3">
                                            <span>Note</span>&nbsp;&nbsp;<br />
                                            <asp:TextBox MaxLength="500" TextMode="MultiLine" Rows="4" Width="84%" CssClass="noresize" ID="txtDescription1" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>                                       
                                        <td></td>
                                        <td></td>
                                        <td style="text-align:center">
                                            <asp:LinkButton ID="lnk_dt2" runat="server" CssClass="Savebtnsve btnSaveCss" Text="Submit" OnClick="lnk_Dt1_Click" OnClientClick="return validateDT2();">Save</asp:LinkButton>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td colspan="4">
                                            <asp:GridView ID="gvDT2" runat="server" BackColor="White" DataKeyNames="Id,Timesheet_id" BorderColor="Navy" PageSize="20" AllowPaging="True" OnPageIndexChanging="DgvApprover_PageIndexChanging" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%">
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
                                                    <asp:BoundField HeaderText="Project"
                                                        DataField="ProjectName"
                                                        ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="20%"
                                                        ItemStyle-BorderColor="Navy" />

                                                    <asp:BoundField HeaderText="Task"
                                                        DataField="Activity_Desc"
                                                        ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="30%"
                                                        ItemStyle-BorderColor="Navy" />
                                                    <asp:BoundField HeaderText="Hours"
                                                        DataField="Hours"
                                                        ItemStyle-HorizontalAlign="center"
                                                        HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="2%"
                                                        ItemStyle-BorderColor="Navy" />
                                                    <asp:BoundField HeaderText="Note"
                                                        DataField="Description"
                                                        ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="30%"
                                                        ItemStyle-BorderColor="Navy" />
                                                    <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="lnkEditdt2" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkEditdt2_Click" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                               <asp:PlaceHolder ID="PlaceHolder3" runat="server">
                               </asp:PlaceHolder>
                            </div>
                            <div id="tab3" class="tabcontent">
                                <asp:Label runat="server" ID="lblmessagetab3" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                                <table style="width:100% !important">
                                    <tr>                                       
                                        <td style="width:35% !important">
                                            <span>Select Project</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                            <asp:DropDownList runat="server" ID="ddlProject2"></asp:DropDownList>
                                            <asp:TextBox runat="server" Visible="false" ID="txtProject2"></asp:TextBox>
                                        </td>
                                        <td style="width:35% !important">
                                            <span>Select Task</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                            <asp:DropDownList runat="server" ID="ddlTask2"></asp:DropDownList>
                                            <asp:TextBox runat="server" Visible="false" ID="txtTask2"></asp:TextBox>
                                        </td>
                                        <td style="width:30%;padding: 12px 0px 0px 0px !important;"><span>Hours (12 Hrs - HH:MM)</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                            <asp:TextBox MaxLength="50" ID="txtHours2" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                         <td colspan="3">
                                            <span>Note</span>&nbsp;&nbsp;<br />
                                            <asp:TextBox MaxLength="500" TextMode="MultiLine" Rows="4" Width="84%" CssClass="noresize" ID="txtDescription2" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>                                      
                                        <td></td>
                                        <td></td>
                                        <td style="text-align:center">
                                            <asp:LinkButton ID="lnk_dt3" runat="server" CssClass="Savebtnsve btnSaveCss" Text="Submit" OnClick="lnk_Dt1_Click" OnClientClick="return validateDT3();">Save</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:GridView ID="gvDT3" runat="server" BackColor="White" DataKeyNames="Id,Timesheet_id" BorderColor="Navy" PageSize="20" AllowPaging="True" OnPageIndexChanging="DgvApprover_PageIndexChanging" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%">
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
                                                    <asp:BoundField HeaderText="Project"
                                                        DataField="ProjectName"
                                                        ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="20%"
                                                        ItemStyle-BorderColor="Navy" />

                                                    <asp:BoundField HeaderText="Task"
                                                        DataField="Activity_Desc"
                                                        ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="30%"
                                                        ItemStyle-BorderColor="Navy" />
                                                    <asp:BoundField HeaderText="Hours"
                                                        DataField="Hours"
                                                        ItemStyle-HorizontalAlign="center"
                                                        HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="2%"
                                                        ItemStyle-BorderColor="Navy" />
                                                    <asp:BoundField HeaderText="Note"
                                                        DataField="Description"
                                                        ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="30%"
                                                        ItemStyle-BorderColor="Navy" />
                                                    <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="lnkEditdt3" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkEditdt3_Click" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                               <asp:PlaceHolder ID="PlaceHolder4" runat="server"></asp:PlaceHolder>
                            </div>
                            <div id="tab4" class="tabcontent">
                               <asp:Label runat="server" ID="lblmessagetab4" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                                <table style="width:100% !important">
                                    <tr>                                       
                                        <td style="width:35% !important">
                                            <span>Select Project</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                            <asp:DropDownList runat="server" ID="ddlProject3"></asp:DropDownList>
                                            <asp:TextBox runat="server" Visible="false" ID="txtProject3"></asp:TextBox>
                                        </td>
                                        <td style="width:35% !important">
                                            <span>Select Task</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                            <asp:DropDownList runat="server" ID="ddlTask3"></asp:DropDownList>
                                            <asp:TextBox runat="server" Visible="false" ID="txtTask3"></asp:TextBox>
                                        </td>
                                      
                                         <td style="width:30%;padding: 12px 0px 0px 0px !important;"><span>Hours (12 Hrs - HH:MM)</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                            <asp:TextBox MaxLength="50" ID="txtHours3" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <span>Note</span>&nbsp;&nbsp;<br />
                                            <asp:TextBox MaxLength="500" TextMode="MultiLine" Rows="4" Width="84%" CssClass="noresize" ID="txtDescription3" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>                                       
                                        <td></td>
                                        <td></td>
                                        <td style="text-align:center">
                                            <asp:LinkButton ID="lnk_dt4" runat="server" CssClass="Savebtnsve btnSaveCss" Text="Submit" OnClick="lnk_Dt1_Click" OnClientClick="return validateDT4();" >Save</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:GridView ID="gvDT4" runat="server" BackColor="White" DataKeyNames="Id,Timesheet_id" BorderColor="Navy" PageSize="20" AllowPaging="True" OnPageIndexChanging="DgvApprover_PageIndexChanging" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%">
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
                                                    <asp:BoundField HeaderText="Project"
                                                        DataField="ProjectName"
                                                        ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="20%"
                                                        ItemStyle-BorderColor="Navy" />

                                                    <asp:BoundField HeaderText="Task"
                                                        DataField="Activity_Desc"
                                                        ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="30%"
                                                        ItemStyle-BorderColor="Navy" />
                                                    <asp:BoundField HeaderText="Hours"
                                                        DataField="Hours"
                                                        ItemStyle-HorizontalAlign="center"
                                                        HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="2%"
                                                        ItemStyle-BorderColor="Navy" />
                                                    <asp:BoundField HeaderText="Note"
                                                        DataField="Description"
                                                        ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="30%"
                                                        ItemStyle-BorderColor="Navy" />
                                                    <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="lnkEditdt4" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkEditdt4_Click" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                                <asp:PlaceHolder ID="PlaceHolder5" runat="server"></asp:PlaceHolder>
                            </div>

                            <div id="tab5" class="tabcontent">
                                <asp:Label runat="server" ID="lblmessagetab5" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                                <table style="width:100% !important">
                                    <tr>                                       
                                        <td style="width:35% !important">
                                            <span>Select Project</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                            <asp:DropDownList runat="server" ID="ddlProject4"></asp:DropDownList>
                                            <asp:TextBox runat="server" Visible="false" ID="txtProject4"></asp:TextBox>
                                        </td>
                                        <td style="width:35% !important">
                                            <span>Select Task</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                            <asp:DropDownList runat="server" ID="ddlTask4"></asp:DropDownList>
                                            <asp:TextBox runat="server" Visible="false" ID="txtTask4"></asp:TextBox>
                                        </td>
                                         <td style="width:30%;padding: 12px 0px 0px 0px !important;"><span>Hours (12 Hrs - HH:MM)</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                            <asp:TextBox MaxLength="50" ID="txtHours4" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                      
                                    </tr>
                                    <tr>
                                          <td colspan="3" >
                                            <span>Note</span>&nbsp;&nbsp;<br />
                                            <asp:TextBox MaxLength="500" TextMode="MultiLine" Rows="4" Width="84%" CssClass="noresize" ID="txtDescription4" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>                                       
                                        <td></td>
                                        <td></td>
                                        <td style="text-align:center">
                                            <asp:LinkButton ID="lnk_dt5" runat="server" CssClass="Savebtnsve btnSaveCss" Text="Submit" OnClick="lnk_Dt1_Click" OnClientClick="return validateDT5();">Save</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:GridView ID="gvDT5" runat="server" BackColor="White" DataKeyNames="Id,Timesheet_id" BorderColor="Navy" PageSize="20" AllowPaging="True" OnPageIndexChanging="DgvApprover_PageIndexChanging" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%">
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
                                                    <asp:BoundField HeaderText="Project"
                                                        DataField="ProjectName"
                                                        ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="20%"
                                                        ItemStyle-BorderColor="Navy" />

                                                    <asp:BoundField HeaderText="Task"
                                                        DataField="Activity_Desc"
                                                        ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="30%"
                                                        ItemStyle-BorderColor="Navy" />
                                                    <asp:BoundField HeaderText="Hours"
                                                        DataField="Hours"
                                                        ItemStyle-HorizontalAlign="center"
                                                        HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="2%"
                                                        ItemStyle-BorderColor="Navy" />
                                                    <asp:BoundField HeaderText="Note"
                                                        DataField="Description"
                                                        ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="30%"
                                                        ItemStyle-BorderColor="Navy" />
                                                    <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="lnkEditdt5" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkEditdt5_Click" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                               <asp:PlaceHolder ID="PlaceHolder6" runat="server"></asp:PlaceHolder>
                            </div>

                            <div id="tab6" class="tabcontent">
                                <asp:Label runat="server" ID="lblmessagetab6" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                                <table style="width:100% !important">
                                    <tr>
                                        
                                        <td style="width:35% !important">
                                            <span>Select Project</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                            <asp:DropDownList runat="server" ID="ddlProject5"></asp:DropDownList>
                                            <asp:TextBox runat="server" Visible="false" ID="txtProject5"></asp:TextBox>
                                        </td>
                                        <td style="width:35% !important">
                                            <span>Select Task</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                            <asp:DropDownList runat="server" ID="ddlTask5"></asp:DropDownList>
                                            <asp:TextBox runat="server" Visible="false" ID="txtTask5"></asp:TextBox>
                                        </td>   
                                        <td style="width:30%;padding: 12px 0px 0px 0px !important;"><span>Hours (12 Hrs - HH:MM)</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                            <asp:TextBox MaxLength="50" ID="txtHours5" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                         <td colspan="3" >
                                            <span>Note</span>&nbsp;&nbsp;<br />
                                            <asp:TextBox MaxLength="500" TextMode="MultiLine" Rows="4" Width="84%" CssClass="noresize" ID="txtDescription5" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>                                       
                                        <td></td>
                                        <td></td>
                                        <td style="text-align:center">
                                            <asp:LinkButton ID="lnk_dt6" runat="server" CssClass="Savebtnsve btnSaveCss" Text="Submit" OnClick="lnk_Dt1_Click" OnClientClick="return validateDT6();">Save</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:GridView ID="gvDT6" runat="server" BackColor="White" DataKeyNames="Id,Timesheet_id" BorderColor="Navy" PageSize="20" AllowPaging="True" OnPageIndexChanging="DgvApprover_PageIndexChanging" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%">
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
                                                    <asp:BoundField HeaderText="Project"
                                                        DataField="ProjectName"
                                                        ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="20%"
                                                        ItemStyle-BorderColor="Navy" />

                                                    <asp:BoundField HeaderText="Task"
                                                        DataField="Activity_Desc"
                                                        ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="30%"
                                                        ItemStyle-BorderColor="Navy" />
                                                    <asp:BoundField HeaderText="Hours"
                                                        DataField="Hours"
                                                        ItemStyle-HorizontalAlign="center"
                                                        HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="2%"
                                                        ItemStyle-BorderColor="Navy" />
                                                    <asp:BoundField HeaderText="Note"
                                                        DataField="Description"
                                                        ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="30%"
                                                        ItemStyle-BorderColor="Navy" />
                                                    <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="lnkEditdt6" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkEditdt6_Click" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                               <asp:PlaceHolder ID="PlaceHolder7" runat="server"></asp:PlaceHolder>
                            </div>
                            <div id="tab7" class="tabcontent">
                                <asp:Label runat="server" ID="lblmessagetab7" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                                <table style="width:100% !important">
                                    <tr>                                       
                                        <td style="width:35% !important">
                                            <span>Select Project</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                            <asp:DropDownList runat="server" ID="ddlProject6"></asp:DropDownList>
                                            <asp:TextBox runat="server" Visible="false" ID="txtProject6"></asp:TextBox>
                                        </td>
                                        <td style="width:35% !important">
                                            <span>Select Task</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                            <asp:DropDownList runat="server" ID="ddlTask6"></asp:DropDownList>
                                            <asp:TextBox runat="server" Visible="false" ID="txtTask6"></asp:TextBox>
                                        </td>  
                                         <td style="width:30%;padding: 12px 0px 0px 0px !important;"><span>Hours (12 Hrs - HH:MM)</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                                            <asp:TextBox MaxLength="50" ID="txtHours6" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                         <td colspan="3">
                                            <span>Note</span>&nbsp;&nbsp;<br />
                                            <asp:TextBox MaxLength="500" TextMode="MultiLine" Rows="4" Width="84%" CssClass="noresize" ID="txtDescription6" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>                                       
                                        <td></td>
                                        <td></td>
                                        <td style="text-align:center">
                                            <asp:LinkButton ID="lnk_dt7" runat="server" CssClass="Savebtnsve btnSaveCss" Text="Submit" OnClick="lnk_Dt1_Click" OnClientClick="return validateDT7();">Save</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:GridView ID="gvDT7" runat="server" BackColor="White" DataKeyNames="Id,Timesheet_id" BorderColor="Navy" PageSize="20" AllowPaging="True" OnPageIndexChanging="DgvApprover_PageIndexChanging" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%">
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
                                                    <asp:BoundField HeaderText="Project"
                                                        DataField="ProjectName"
                                                        ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="20%"
                                                        ItemStyle-BorderColor="Navy" />

                                                    <asp:BoundField HeaderText="Task"
                                                        DataField="Activity_Desc"
                                                        ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="30%"
                                                        ItemStyle-BorderColor="Navy" />
                                                    <asp:BoundField HeaderText="Hours"
                                                        DataField="Hours"
                                                        ItemStyle-HorizontalAlign="center"
                                                        HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="2%"
                                                        ItemStyle-BorderColor="Navy" />
                                                    <asp:BoundField HeaderText="Note"
                                                        DataField="Description"
                                                        ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="30%"
                                                        ItemStyle-BorderColor="Navy" />
                                                    <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="lnkEditdt7" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkEditdt7_Click" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                                <asp:PlaceHolder ID="PlaceHolder8" runat="server"></asp:PlaceHolder>
                            </div>
                        </li>                        
                        <li></li>
                        <li></li>
                        <li></li>
                        <li>
                            <asp:LinkButton ID="btnBack" runat="server" CssClass="Savebtnsve" Text="Submit" OnClick="btnBack_Click1" OnClientClick="return SaveOutClick();">Submit</asp:LinkButton>
                            <asp:LinkButton ID="btnIn" runat="server" Visible="false" CssClass="Savebtnsve" Text="Submit" OnClick="btnEdit_Click" OnClientClick="return SaveInClick();">Submit</asp:LinkButton>
                        </li>

                        <li class="Approver">
                            <%--<span>Approver </span>--%>
                            <br />
                            <asp:ListBox Visible="false" ID="lstApprover" runat="server"></asp:ListBox>
                    <asp:GridView ID="DgvApprover" runat="server" BackColor="White" DataKeyNames="Id,Timesheet_id" BorderColor="Navy" PageSize="20" AllowPaging="True" OnPageIndexChanging="DgvApprover_PageIndexChanging" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="141%">
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
                            <asp:BoundField HeaderText="Date"
                                DataField="TimesheetDate"
                                ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="9%" 
                                ItemStyle-BorderColor="Navy"
                                />

                            <asp:BoundField HeaderText="Project"
                                DataField="ProjectName"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="20%" 
                                ItemStyle-BorderColor="Navy"
                                />
                            
                            <asp:BoundField HeaderText="Task"
                                DataField="Activity_Desc"
                                ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="30%" 
                                ItemStyle-BorderColor="Navy"
                                />
                            <asp:BoundField HeaderText="Note"
                                DataField="Description"
                                 ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="30%" 
                                ItemStyle-BorderColor="Navy"
                                
                                />
                            <asp:BoundField HeaderText="Hours"
                                DataField="Hours"
                                ItemStyle-HorizontalAlign="center"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="2%" 
                                ItemStyle-BorderColor="Navy"
                                
                                />
                            
                             <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="1%"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="left">
                                <ItemTemplate>
                                <asp:ImageButton id="lnkEdit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkEdit_Click"/>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                          <%--  <asp:BoundField HeaderText="Emp_Name"
                                DataField="Emp_Name"
                                ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="1%" 
                                ItemStyle-BorderColor="Navy"
                                Visible="false"
                                />

                            <asp:BoundField HeaderText="Emp_Emailaddress"
                                DataField="Emp_Emailaddress"
                                ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="1%" 
                                ItemStyle-BorderColor="Navy"
                                Visible="false"
                                />

                            <asp:BoundField HeaderText="A_EMP_CODE"
                                DataField="A_EMP_CODE"
                                ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="1%" 
                                ItemStyle-BorderColor="Navy"
                                Visible="false"
                                />--%>
                        </Columns>
                    </asp:GridView>
                        </li>
<%--                       <li>

                       </li>
                        <li class="Approver">
                            <span>For Information To </span>
                            <br />
                            <asp:ListBox ID="lstIntermediate" runat="server"></asp:ListBox>
                        </li>--%>

                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="Savebtndiv">
        <asp:LinkButton ID="btnSave1" Visible="false"  runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClick="btnSave_Click1" OnClientClick="return SaveMultiClick();">Submit</asp:LinkButton>
        <asp:LinkButton ID="btnBack1"  Visible="false" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="btnBack_Click">Back</asp:LinkButton>
          
        <%-- Following Popup for Sending Leave Request 
        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" DisplayModalPopupID="mpe" TargetControlID="btnSave">
        </ajaxToolkit:ConfirmButtonExtender>
        <ajaxToolkit:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="btnSave" OkControlID = "btnYes"
            CancelControlID="btnNo" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Confirmation
            </div>
            <div class="body">              
                Do you want to Submit ?
            </div>
            <div class="footer" align="right">
                <asp:Button ID="btnNo" runat="server" Text="No" />
                <asp:Button ID="btnYes" runat="server" Text="Yes" />
            </div>
        </asp:Panel>
        End Here --%>
    </div>
    <div>
        

        <asp:LinkButton ID="btnback_mng" runat="server" Visible="false" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/MyLeave_Req.aspx">Back</asp:LinkButton>
                
        <%-- Following Popup for Modify Leave Request 
        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" DisplayModalPopupID="mpe_Mod" TargetControlID="btnMod">
        </ajaxToolkit:ConfirmButtonExtender>
        <ajaxToolkit:ModalPopupExtender ID="mpe_Mod" runat="server" PopupControlID="pnlPopup_Mod" TargetControlID="btnMod" OkControlID = "btnYes_Mod"
            CancelControlID="btnNo_Mod" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup_Mod" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Confirmation
            </div>
            <div class="body">                                
                Do you want to Update ?
            </div>
            <div class="footer" align="right">
                <asp:Button ID="btnNo_Mod" runat="server" Text="No" />
                <asp:Button ID="btnYes_Mod" runat="server" Text="Yes" />                
            </div>
        </asp:Panel>
        End Here --%>

        <%-- Following Popup for Cancel Leave Request  
        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" DisplayModalPopupID="mpe_Cancel" TargetControlID="btnCancel">
        </ajaxToolkit:ConfirmButtonExtender>
        <ajaxToolkit:ModalPopupExtender ID="mpe_Cancel" runat="server" PopupControlID="pnlPopup_Cancel" TargetControlID="btnCancel" OkControlID = "btnYes_CLR"
            CancelControlID="btnNo_CLR" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup_Cancel" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Confirmation
            </div>
            <div class="body">
               
                Do you want to Cancel ?
            </div>
            <div class="footer" align="right">                                
                <asp:Button ID="btnNo_CLR" runat="server" Text="No" />
                <asp:Button ID="btnYes_CLR" runat="server" Text="Yes" />                
            </div>
        </asp:Panel>
         End Here --%>

    </div>
    <asp:TextBox ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>

    <asp:TextBox ID="TextBox1" runat="server" MaxLength="50" Visible="false" AutoPostBack="true" > </asp:TextBox>

    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />

    <asp:HiddenField ID="hflEmpName" runat="server" />

    <asp:HiddenField ID="hflEmpDesignation" runat="server" />

    <asp:HiddenField ID="hflEmpDepartment" runat="server" />

    <asp:HiddenField ID="hflEmailAddress" runat="server" />

    <asp:HiddenField ID="hflGrade" runat="server" />

    <asp:HiddenField ID="hflapprcode" runat="server" />

    <asp:HiddenField ID="hdnFromFor" runat="server" />


    <asp:HiddenField ID="hdlDate" runat="server" />
    
    <asp:HiddenField ID="hdnleaveconditiontypeid" runat="server" />
    <asp:HiddenField ID="htnleavetypeid" runat="server" />
    <asp:HiddenField ID="hdnleavedays" runat="server" />

    <asp:HiddenField ID="hdnlstfromfor" runat="server" />
    <asp:HiddenField ID="hdnlsttofor" runat="server" />
    <asp:HiddenField ID="hdnToDate" runat="server" />
    <asp:HiddenField ID="hdnReqid" runat="server" />
    <asp:HiddenField ID="hdnOldLeaveCount" runat="server" />
    <asp:HiddenField ID="hdnLeaveStatus" runat="server" />
    <asp:HiddenField ID="hflLeavestatus" runat="server" />
    <asp:HiddenField ID="hflstatusid" runat="server" />
    <asp:HiddenField ID="hdnAppr_status" runat="server" />

    <asp:HiddenField ID="hdnmsg" runat="server" />
    <asp:HiddenField ID="hdnfrmdate_emial" runat="server" />
    <asp:HiddenField ID="hdntodate_emial" runat="server" />
    <asp:HiddenField ID="hdnHRMailId_MLLWP" runat="server" />
    <asp:HiddenField ID="hdnPLwithSL_succession" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="hdnInTime" runat="server" />
    <asp:HiddenField ID="hdnOutTime" runat="server" />
    <asp:HiddenField ID="hdnisTimeInShow" runat="server" />
    <asp:HiddenField ID="hdnisTimeoutShow" runat="server" />
    <asp:HiddenField ID="hdnId" runat="server" />
    <asp:HiddenField ID="hdnStartDate" runat="server" />
    <asp:HiddenField ID="hdnEndDate" runat="server" />
    <asp:HiddenField ID="hdnFullDay" runat="server" />
    <asp:HiddenField ID="hdnHalfDay" runat="server" />
    <asp:HiddenField ID="hdndt1Id" runat="server" />
    <asp:HiddenField ID="hdndt2Id" runat="server" />
    <asp:HiddenField ID="hdndt3Id" runat="server" />
    <asp:HiddenField ID="hdndt4Id" runat="server" />
    <asp:HiddenField ID="hdndt5Id" runat="server" />
    <asp:HiddenField ID="hdndt6Id" runat="server" />
    <asp:HiddenField ID="hdndt7Id" runat="server" />
    <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchProject" MinimumPrefixLength="3"
        CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtProject"
        ID="AutoCompleteExtender1" runat="server" FirstRowSelected="true">
    </ajaxToolkit:AutoCompleteExtender>

    <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchTask" MinimumPrefixLength="3"
        CompletionInterval="1" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtTask"
        ID="AutoCompleteExtender2" runat="server" FirstRowSelected="true">
    </ajaxToolkit:AutoCompleteExtender>
    <script src="../js/dist/jquery-3.2.1.min.js"></script>       
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" /> 
    
    <script type="text/javascript">      
        $(document).ready(function () {
            $("#MainContent_ddlProject").select2();
            $("#MainContent_ddlTask").select2();
            $("#MainContent_ddlProject1").select2();
            $("#MainContent_ddlTask1").select2();
            $("#MainContent_ddlProject2").select2();
            $("#MainContent_ddlTask2").select2();
            $("#MainContent_ddlProject3").select2();
            $("#MainContent_ddlTask3").select2();
            $("#MainContent_ddlProject4").select2();
            $("#MainContent_ddlTask4").select2();
            $("#MainContent_ddlProject5").select2();
            $("#MainContent_ddlTask5").select2();
            $("#MainContent_ddlProject6").select2();
            $("#MainContent_ddlTask6").select2();

        });
    </script>
    
    <script type="text/javascript">
        function StopLoader() {
            alert("hello!")
        }
        function openCity(evt, cityName) {
            //alert(cityName);
            debugger
            var i, tabcontent, tablinks;
            tabcontent = document.getElementsByClassName("tabcontent");
            for (i = 0; i < tabcontent.length; i++) {
                tabcontent[i].style.display = "none";
            }
            tablinks = document.getElementsByClassName("tablinks");
            for (i = 0; i < tablinks.length; i++) {
                tablinks[i].className = tablinks[i].className.replace(" active", "");
            }
            document.getElementById(cityName).style.display = "block";

            if (cityName == 'tab1') {
                document.getElementById("<%= btn1.ClientID %>").className += " active";
            }
            else if (cityName == 'tab2') {
                document.getElementById("<%= btn2.ClientID %>").className += " active";
            }
            else if (cityName == 'tab3') {
                document.getElementById("<%= btn3.ClientID %>").className += " active";
            }
            else if (cityName == 'tab4') {
                document.getElementById("<%= btn4.ClientID %>").className += " active";
            }
            else if (cityName == 'tab5') {
                document.getElementById("<%= btn5.ClientID %>").className += " active";
            }
            else if (cityName == 'tab6') {
                document.getElementById("<%= btn6.ClientID %>").className += " active";
            }
            else if (cityName == 'tab7') {
                document.getElementById("<%= btn7.ClientID %>").className += " active";
            }
            else {
                evt.currentTarget.className += " active";
            }
            return false;
        }
    </script>
    <script type="text/javascript">
        //$(document).ready(function () {
        //    ShowTime();});
        var xmlHttp;
        function srvTime() {
            try {
                //FF, Opera, Safari, Chrome
                xmlHttp = new XMLHttpRequest();
            }
            catch (err1) {
                //IE
                try {
                    xmlHttp = new ActiveXObject('Msxml2.XMLHTTP');
                }
                catch (err2) {
                    try {
                        xmlHttp = new ActiveXObject('Microsoft.XMLHTTP');
                    }
                    catch (eerr3) {
                        //AJAX not supported, use CPU time.
                        alert("AJAX not supported");
                    }
                }
            }
            xmlHttp.open('HEAD', window.location.href.toString(), false);
            xmlHttp.setRequestHeader("Content-Type", "text/html");
            xmlHttp.send('');
            return xmlHttp.getResponseHeader("Date");
        }
        function addZero(i) {
            if (i < 10) {
                i = "0" + i;
            }
            return i;
        }
     <%--   function ShowTime() {
            var st = srvTime();
            var dt = new Date(st);
            var h = addZero(dt.getHours());
            var m = addZero(dt.getMinutes());
            var s = addZero(dt.getSeconds());
            var inval = document.getElementById("<%= hdnisTimeInShow.ClientID %>").value;
            var outval = document.getElementById("<%= hdnisTimeoutShow.ClientID %>").value;
            if (inval == 0) {
                document.getElementById("<%= Txt_InTime.ClientID %>").value = h + ":" + m + ":" + s; 
            }
            if (outval == 0) {
                document.getElementById("<%= Txt_OutTime.ClientID %>").value = h + ":" + m + ":" + s; 
            }
            //document.getElementById("<%= Txt_OutTime.ClientID %>").value = dt.toLocaleTimeString();
            window.setTimeout("ShowTime()", 10);
        }--%>
        //window.setTimeout("ShowTime()", 10);
        function validateFromFor(leavetypeid, leavetypeFSH, tt, todate, fromdate, msg) {

            document.getElementById("<%=lblmessage.ClientID%>").innerHTML = msg;
            return;
        }

        function validateToFor(tt, msg) {

            document.getElementById("<%=lblmessage.ClientID%>").innerHTML = msg;
            return;
        }

        function textboxMultilineMaxNumber(txt, maxLen) {
            try {
                if (txt.value.length > (maxLen - 1)) return false;
            } catch (e) {
            }
        }

        function validateLeaveType(leavetypeid)
        {
            document.getElementById("<%=txtFromdate.ClientID%>").value = "";
            document.getElementById("<%=lblmessage.ClientID%>").innerHTML = "";
            document.getElementById("<%=hdnlstfromfor.ClientID%>").value = "";
            document.getElementById("<%=hdnlsttofor.ClientID%>").value = "";
            document.getElementById("<%=hdnToDate.ClientID%>").value = "";
            document.getElementById("<%=hdnleavedays.ClientID%>").value = "";
            document.getElementById("<%=txtFromdate.ClientID%>").disabled = false;
            return;
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
        function onCharOnlyNumber_Time(e) {
            var keynum;
            var keychar;
            var numcheck = /[0123456789:]/;

            if (window.event) {
                keynum = e.keyCode;
            }
            else if (e.which) {
                keynum = e.which;
            }
            keychar = String.fromCharCode(keynum);
            return numcheck.test(keychar);
        }
        function onCharOnlyNumber(e) {
            var keynum;
            var keychar;
            var numcheck = /[0123456789./]/;

            if (window.event) {
                keynum = e.keyCode;
            }
            else if (e.which) {
                keynum = e.which;
            }
            keychar = String.fromCharCode(keynum);
            return numcheck.test(keychar);
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
            try {
               
                var retunboolean = true;
                var ele = document.getElementById('<%=btnBack.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        ConfirmIn();
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }

        function SaveOutClick() {
            try {

                var retunboolean = true;
                var ele = document.getElementById('<%=btnBack.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        ConfirmIn();
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        function SaveInClick() {
            try {

                var retunboolean = true;
                var ele = document.getElementById('<%=btnIn.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        ConfirmIn();
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        function validateDT1() {
            try {

                var retunboolean = true;
                var ele = document.getElementById('<%=lnk_Dt1.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        ConfirmIn();
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        function validateDT2() {
            try {

                var retunboolean = true;
                var ele = document.getElementById('<%=lnk_dt2.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        ConfirmIn();
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        function validateDT3() {
            try {

                var retunboolean = true;
                var ele = document.getElementById('<%=lnk_dt3.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        ConfirmIn();
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        function validateDT4() {
            try {

                var retunboolean = true;
                var ele = document.getElementById('<%=lnk_dt4.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        ConfirmIn();
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        function validateDT5() {
            try {

                var retunboolean = true;
                var ele = document.getElementById('<%=lnk_dt5.ClientID%>');

                 if (ele != null && !ele.disabled)
                     retunboolean = true;
                 else
                     retunboolean = false;
                 if (ele != null) {
                     ele.disabled = true;
                     if (retunboolean == true)
                         ConfirmIn();
                 }
             }
             catch (err) {
                 alert(err.description);
             }
             return retunboolean;
        }
        function validateDT6() {
            try {

                var retunboolean = true;
                var ele = document.getElementById('<%=lnk_dt6.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        ConfirmIn();
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        function validateDT7() {
            try {

                var retunboolean = true;
                var ele = document.getElementById('<%=lnk_dt7.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        ConfirmIn();
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        function ConfirmIn() {
            //Testing();
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Submit ?")) {
                confirm_value.value = "Yes";               
            } else {
                confirm_value.value = "No";
                document.getElementById("<%= hdnInTime.ClientID %>").value = '';
                document.getElementById("<%= hdnOutTime.ClientID %>").value = '';
            }
            //alert(confirm_value.value);
            //document.forms[0].appendChild(confirm_value);
            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return;

        }

    </script>
    
</asp:Content>

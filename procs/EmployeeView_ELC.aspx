<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="EmployeeView_ELC.aspx.cs"
    Inherits="procs_EmployeeView_ELC" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Assembly="eWorld.UI.Compatibility, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI.Compatibility" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link href="'<%=ConfigurationManager.AppSettings["sitepathadmin"] %>includes/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src='<%=ConfigurationManager.AppSettings["sitepathadmin"] %>js/tinymce/tinymce.min.js'></script>
    <script src='<%=ConfigurationManager.AppSettings["sitepathadmin"] %>js/jquery.min.js' type="text/javascript"></script>
    <script src='<%=ConfigurationManager.AppSettings["sitepathadmin"] %>js/jquery-ui.min.js' type="text/javascript"></script>

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Req_Requisition.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />

    <script type="text/javascript" src="js/dist/jquery-3.2.1.min.js"></script>
    <style>
        #MainContent_lnk_RMC {
            margin: 0% 0% 0% 0% !important;
        }

        #MainContent_lnk_RMC {
            background: #3D1956;
            color: #febf39 !important;
            padding: 6px 16px;
        }

        #MainContent_lnk_Project {
            margin: 0% 0% 0% 0% !important;
        }

        #MainContent_lnk_Project {
            background: #3D1956;
            color: #febf39 !important;
            padding: 6px 16px;
        }

        #MainContent_lnk_Leave {
            margin: 0% 0% 0% 0% !important;
        }

        #MainContent_lnk_Leave {
            background: #3D1956;
            color: #febf39 !important;
            padding: 6px 16px;
        }

        #MainContent_lnk_MissPunch {
            background: #3D1956;
            color: #febf39 !important;
            padding: 6px 16px;
        }

        #MainContent_lnk_Resignation {
            background: #3D1956;
            color: #febf39 !important;
            padding: 6px 16px;
        }

        .ImageImg {
            border: 2px solid black !important;
            vertical-align: middle;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <script type="text/javascript">

		 function DownloadOfferFile(FileName) {
              var OfferPath = document.getElementById("<%=hdnOfferPath.ClientID%>").value;
			//var OffferLetter = document.getElementById("<%=HDNOfferLetter.ClientID%>").value;
        window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + OfferPath + "" + FileName);
           // window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + OfferPath + "" + FileName);
         }
		function DownloadFile(FileName) {
            var localFilePath1 = document.getElementById("<%=FilePathFirstgrid.ClientID%>").value;
            var Txt_Code1 = document.getElementById('MainContent_Txt_EmployeeCode').value;
            var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;
			var AppointPath = document.getElementById("<%=hdnAppointPath.ClientID%>").value;
			const Signed = "ALS_";
            const myArray = FileName.split(".");
            let word1 = myArray[0];
            if (Txt_Code1 == word1) {
                window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath1 + "" + FileName);
				//window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath1 + "" + FileName);

			}
			else if (word1 == Signed.concat(Txt_Code1))
			{
                window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + AppointPath + "" + FileName);
                // window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + AppointPath + "" + FileName);
			}
            else {
                window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
				//window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);

            }
        }
        function DownloadFileFirstGrid(FileName) {
            var localFilePath = document.getElementById("<%=FilePathFirstgrid.ClientID%>").value;
            var localFilePath1 = document.getElementById("<%=FilePathFirstgrid1.ClientID%>").value;
			var Txt_Code1 = document.getElementById('MainContent_Txt_EmployeeCode').value;
			
            const myArray = FileName.split(".");
            let word1 = myArray[0];
			if (Txt_Code1 == word1) {
				window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
				//window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
			}
            else {
                window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
                // window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath1 + "" + FileName);
            }
        }

    </script>

    <table id="TABLE1" border="0" cellpadding="5px" cellspacing="5px" width="90%">
        <div class="userposts">
            <span>
                <asp:Label ID="lblheading" runat="server" Text="Employee Life Cycle Detail"></asp:Label>
            </span>
        </div>
        <div>

            <span style="margin-bottom: 20px">
                <a href="ReportsMenu.aspx" class="aaaa">Report</a>
            </span>
            <span style="margin-bottom: 20px;">
                <a style="margin-right: 10px !important;" href="EmployeeList_ELC.aspx" class="aaaa">Back</a>
            </span>
        </div>

        <tr>
            <td colspan="2" align="left"><b style="font-size: medium; color: mediumblue;" runat="server" id="bStatus">Status:</b></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label Font-Bold="true" ForeColor="Red" ID="msgsave" runat="server" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Employee Type:</td>
            <td>
                <asp:TextBox ID="Txt_EmployeeType" Enabled="false" runat="server" Width="250px"></asp:TextBox><br />
                <asp:HiddenField ID="HDEmpID" runat="server" />
                <asp:HiddenField ID="hdnGender" runat="server" />
                <asp:HiddenField ID="FilePathFirstgrid" runat="server" />
                <asp:HiddenField ID="FilePathFirstgrid1" runat="server" />
				<asp:HiddenField ID="hdnAppointPath" runat="server" />
				<asp:HiddenField ID="hdnOfferPath" runat="server" />
				<asp:HiddenField ID="HDNOfferLetter" runat="server" />
            </td>
            <td rowspan="5" colspan="1">
                <asp:Image CssClass="ImageImg" ID="Profile_Photo" runat="server" Width="200px" Height="250px" ImageUrl="~/themes/creative1.0/images/profile55x55/noimage.jpg" />
                <asp:HiddenField ID="hdnProfilePhoto" runat="server" />
                <asp:HiddenField ID="FilePath" runat="server" />
            </td>
        </tr>

        <tr>
            <td>Employee Code:</td>
            <td>
                <asp:TextBox ID="Txt_EmployeeCode" runat="server" Enabled="false" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Employee Name:</td>
            <td>
                <asp:TextBox ID="Txt_EmployeeName" runat="server" Enabled="false" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Email Address:</td>
            <td>
                <asp:TextBox ID="Txt_EmailAddress" runat="server" Enabled="false" Width="250px"></asp:TextBox>

            </td>
            <td colspan="2">
                <asp:FileUpload ID="uploadfile" runat="server" accept=".jpg" AllowMultiple="false" Visible="false" />
            </td>
        </tr>
        <tr>
            <td>Date Of Joining:</td>
            <td>
                <asp:TextBox ID="Txt_DateOfJoining" runat="server" Enabled="false" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Department:</td>
            <td>
                <asp:TextBox ID="Txt_Department" runat="server" Enabled="false" Width="250px"></asp:TextBox>
            </td>
            <td>Designation: <span style="padding-left: 43px">
                <asp:TextBox ID="Txt_Designation" Enabled="false" runat="server" Width="250px"></asp:TextBox>
            </span></td>
            <td rowspan="1" colspan="1">
                <%--<asp:TextBox ID="Txt_Mobile" runat="server" Width="250px"></asp:TextBox>--%>
            </td>
        </tr>
        <tr>
            <td>Current RM:</td>
            <td>
                <asp:TextBox ID="Txt_CurrentRM" Enabled="false" runat="server" Width="250px"></asp:TextBox>
            </td>
            <td>Location: <span style="padding-left: 60px">
                <asp:TextBox ID="Txt_Location" runat="server" Enabled="false" Width="250px"></asp:TextBox>
            </span></td>
            <td rowspan="1" colspan="1"></td>
        </tr>

        <tr>
            <td>Current Band:</td>
            <td>
                <asp:TextBox ID="Txt_CurrentBand" runat="server" Width="250px" Enabled="false"></asp:TextBox>
            </td>
            <td>Current Shift: <span style="padding-left: 40px">
                <asp:TextBox ID="Txt_CurrentShift" runat="server" Enabled="false" Width="250px"></asp:TextBox>
            </span></td>
            <td rowspan="1" colspan="1"></td>
        </tr>
        <tr>
            <td>Mobile No:</td>
            <td>
                <asp:TextBox ID="Txt_MobileNo" runat="server" Enabled="false" Width="250px"></asp:TextBox>
            </td>
            <td></td>
            <td rowspan="1" colspan="1"></td>
        </tr>
        <tr>
            <td colspan="5">
                <b>Current Work Schedule</b>
            </td>
        </tr>
        <tr>
            <td>Schedule Name: </td>
            <td>
                <asp:TextBox ID="txtScheduleName" runat="server" Enabled="false" Width="250px"></asp:TextBox>
            </td>
            <td>No. Of Working Days:<span style="padding-left: 15px">
                <asp:TextBox ID="txtNoOfWorkingDays" runat="server" Width="250px" Enabled="false"></asp:TextBox>
            </span>
            </td>

            <td rowspan="1" colspan="1"></td>
        </tr>
        <tr>
            <td>Weekends: </td>
            <td>
                <asp:TextBox ID="txtWeekends" runat="server" Enabled="false" Width="250px"></asp:TextBox>
            </td>
            <td>Working Days:<span style="padding-left: 52px">
                <asp:TextBox ID="txtWorkingDays" runat="server" Enabled="false" Width="350px"></asp:TextBox>
            </span>
            </td>
            <%--<td></td>--%>
            <td rowspan="1" colspan="1"></td>
        </tr>
        <tr>
            <td colspan="5">
                <b>Current Shift Details</b>
            </td>
        </tr>

        <tr>
            <td>Shift Name:</td>
            <td>
                <asp:TextBox ID="txtShiftName" runat="server" Width="250px" Enabled="false"></asp:TextBox>
            </td>
            <td></td>
            <td rowspan="1" colspan="1"></td>
        </tr>
        <tr>
            <td>Shift In Start: </td>
            <td>
                <asp:TextBox ID="txtShiftInStart" runat="server" Enabled="false" Width="250px"></asp:TextBox>
            </td>
            <td>Shift In End:<span style="padding-left: 60px">
                <asp:TextBox ID="txtShiftInEnd" runat="server" Width="250px" Enabled="false"></asp:TextBox>
            </span>
            </td>

            <td rowspan="1" colspan="1"></td>
        </tr>
        <tr>
            <td>Shift Out Start:</td>
            <td>
                <asp:TextBox ID="txtShiftOutStart" runat="server" Enabled="false" Width="250px"></asp:TextBox>
            </td>
            <td>Shift Out End:<span style="padding-left: 50px">
                <asp:TextBox ID="txtShiftOutEnd" runat="server" Width="250px" Enabled="false"></asp:TextBox>
            </span>
            </td>

            <td rowspan="1" colspan="1"></td>
        </tr>
        <tr>
            <td>Full Day Working Hrs: </td>
            <td>
                <asp:TextBox ID="txtFullDayWorkingHrs" runat="server" Enabled="false" Width="250px"></asp:TextBox>
            </td>
            <td>Half Day Working Hrs:<span style="padding-left: 5px">
                <asp:TextBox ID="txtHalfDayWorkingHrs" runat="server" Enabled="false" Width="250px"></asp:TextBox>
            </span>
            </td>

            <td rowspan="1" colspan="1"></td>
        </tr>

        <tr>
            <td colspan="5">
                <b>Current Year Count of Leaves due to Incomplete Timesheets & due to Miss Punch </b>
            </td>
        </tr>
        <tr>
            <td>Miss Punch:</td>
            <td>
                <asp:TextBox ID="txtAtte" runat="server" Width="250px" Enabled="false"></asp:TextBox>
            </td>
            <td>Incomplete Timesheets:<span style="padding-left: 40px">
                <asp:TextBox ID="txtTimesheet" runat="server" Enabled="false" Width="250px"></asp:TextBox>
            </span></td>
            <td rowspan="1" colspan="1"></td>
        </tr>
        <tr>
            <td colspan="5">
                <hr />
            </td>
        </tr>
        <tr>
            <td class="trvL_detail">
                <asp:LinkButton ID="btnTra_Details" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" OnClick="btnTra_Details_Click"></asp:LinkButton>
                <span id="spntrvldtls" runat="server">Location Change Details</span>
            </td>
            <td></td>
            <td></td>
            <td rowspan="1" colspan="1"></td>
        </tr>
        <div>
            <table id="TABLE2_Location" runat="server">
                <tr>
                    <td>
                        <div class="manage_grid" style="width: 100%; height: auto;" id="Div_LocationChangeList" runat="server" visible="false">
                            <asp:GridView ID="gvLocationChangeList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="solid" BorderWidth="1px"
                                CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                <RowStyle ForeColor="#000066" />
                                <Columns>
                                    <asp:BoundField HeaderText="Employee Code" DataField="Emp_Code"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="15%" />
                                    <asp:BoundField HeaderText="Old Location" DataField="OldLocation"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="25%" />
                                    <asp:BoundField HeaderText="Current Location" DataField="NewLocation"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="25%" />
                                    <asp:BoundField HeaderText="From Date" DataField="FromDate"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="13%" />
                                    <asp:BoundField HeaderText="To Date" DataField="ToDate"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="13%" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
            </table>
            <br />
        </div>
        <tr style="padding-bottom: 15px">
            <td class="trvL_detail">
                <span style="padding-left: 45px; padding-top: 15px">
                    <asp:LinkButton ID="trvl_localbtn" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" OnClick="trvl_localbtn_Click"></asp:LinkButton>
                    <span id="spnlocalTrvl" runat="server">Designation Change Details</span></span>
            </td>
            <td></td>
            <td></td>
            <td rowspan="1" colspan="1"></td>
        </tr>
        <div style="margin-left: 40px">
            <br />
            <table id="TABLE3_Designation">
                <tr>
                    <td>
                        <div class="manage_grid" style="width: 98%; height: auto;" runat="server" id="Div_DesignationList" visible="false">
                            <asp:GridView ID="GVDesignation" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="solid" BorderWidth="1px"
                                CellPadding="3" AutoGenerateColumns="False" Width="98%" EditRowStyle-Wrap="false">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                <RowStyle ForeColor="#000066" />
                                <Columns>
                                    <asp:BoundField HeaderText="Employee Code" DataField="Emp_Code"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="15%" />
                                    <asp:BoundField HeaderText="Old Designation" DataField="OldDesignation"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="25%" />
                                    <asp:BoundField HeaderText="Current Designation" DataField="NewDesignation"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="25%" />
                                    <asp:BoundField HeaderText="From Date" DataField="FromDate"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="13%" />
                                    <asp:BoundField HeaderText="To Date" DataField="ToDate"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="13%" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
            </table>
            <br />
        </div>

        <tr style="padding-bottom: 15px">
            <td class="trvL_detail">
                <span style="padding-left: 45px">
                    <asp:LinkButton ID="trvl_accmo_btn" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" OnClick="trvl_accmo_btn_Click"></asp:LinkButton>
                    <span id="Span2" runat="server">Band Change Details</span></span>
            </td>
            <td></td>
            <td></td>
            <td rowspan="1" colspan="1"></td>
        </tr>
        <div style="margin-left: 40px; padding-top: 10px">
            <table id="TABLE4_Band" runat="server">
                <tr>
                    <td>
                        <div class="manage_grid" style="width: 98%; height: auto;" runat="server" visible="false" id="Div_BandList">
                            <asp:GridView ID="GVBand" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="solid" BorderWidth="1px"
                                CellPadding="3" AutoGenerateColumns="False" Width="98%" EditRowStyle-Wrap="false">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                <RowStyle ForeColor="#000066" />
                                <Columns>
                                    <asp:BoundField HeaderText="Employee Code" DataField="Emp_Code"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="15%" />
                                    <asp:BoundField HeaderText="Old Band" DataField="OldBand"
                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="25%" />
                                    <asp:BoundField HeaderText="Current Band" DataField="NewBand"
                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="25%" />
                                    <asp:BoundField HeaderText="From Date" DataField="FromDate"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="13%" />
                                    <asp:BoundField HeaderText="To Date" DataField="ToDate"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="13%" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
            </table>
            <br />
        </div>
        <tr style="padding-bottom: 15px">
            <td class="trvL_detail">
                <span style="padding-left: 45px">
                    <asp:LinkButton ID="lnk_RMC" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" OnClick="lnk_RMC_Click"></asp:LinkButton>
                    <span id="Span3" runat="server">Report Manager Change Details</span></span>
            </td>
            <td></td>
            <td></td>
            <td rowspan="1" colspan="1"></td>
        </tr>
        <div style="margin-left: 40px; padding-top: 10px">
            <table id="TABLE2" runat="server">
                <tr>
                    <td>
                        <div class="manage_grid" style="width: 96%; height: auto;" id="Div_RM" runat="server" visible="false">
                            <asp:GridView ID="gv_RMDetails" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="solid" BorderWidth="1px"
                                CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                <RowStyle ForeColor="#000066" />
                                <Columns>
                                    <asp:BoundField HeaderText="Employee Code" DataField="Emp_Code"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="15%" />
                                    <asp:BoundField HeaderText="Old RM" DataField="OldRM"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="25%" />
                                    <asp:BoundField HeaderText="Current RM" DataField="CurrentRM"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="25%" />
                                    <asp:BoundField HeaderText="From Date" DataField="StartDate"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="13%" />
                                    <asp:BoundField HeaderText="To Date" DataField="EndDate"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="13%" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
            </table>
            <br />
        </div>
        <tr style="padding-bottom: 15px">
            <td class="trvL_detail">
                <span style="padding-left: 45px">
                    <asp:LinkButton ID="lnk_Leave" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" OnClick="lnk_Leave_Click"></asp:LinkButton>
                    <span id="Span6" runat="server">Leave Report (Availed Leave)</span></span>
            </td>
            <td></td>
            <td></td>
            <td rowspan="1" colspan="1"></td>
        </tr>
        <div style="margin-left: 40px; padding-top: 10px">
            <table id="TABLE4" runat="server">
                <tr>
                    <td>
                        <div class="manage_grid" style="width: 96%; height: auto;" id="Div_leave" runat="server" visible="false">
                            <asp:GridView ID="gv_Leave" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="solid" BorderWidth="1px"
                                CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false" OnRowDataBound="gv_Leave_RowDataBound">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                <RowStyle ForeColor="#000066" />
                                <Columns>
                                    <asp:BoundField HeaderText="Employee Code" DataField="Emp_Code"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="15%" />
                                    <asp:BoundField HeaderText="Financial Year" DataField="FinancialYear"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="15%" />
                                    <asp:BoundField HeaderText="Privilege Leave" DataField="Privilege Leave"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="15%" />
                                    <asp:BoundField HeaderText="Sick Leave" DataField="Sick Leave"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="15%" />
                                    <asp:BoundField HeaderText="Time Off" DataField="Time Off"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="15%" />
                                    <asp:BoundField HeaderText="Leave Without Pay" DataField="Leave Without Pay"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="15%" />
                                    <asp:BoundField HeaderText="Maternity Leave" DataField="Maternity Leave"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="15%" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
            </table>
            <br />
        </div>
        <tr style="padding-bottom: 15px">
            <td class="trvL_detail">
                <span style="padding-left: 45px">
                    <asp:LinkButton ID="lnk_MissPunch" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" OnClick="lnk_MissPunch_Click"></asp:LinkButton>
                    <span id="Span7" runat="server">Miss punch facility Availed every Month</span></span>
            </td>
            <td></td>
            <td></td>
            <td rowspan="1" colspan="1"></td>
        </tr>
        <div style="margin-left: 40px; padding-top: 10px">
            <table id="TABLE5" runat="server">
                <tr>
                    <td>
                        <div class="manage_grid" style="width: 96%; height: auto;" id="Div_MissPunch" runat="server" visible="false">
                            <asp:GridView ID="gv_MissPunch" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="solid" BorderWidth="1px"
                                CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                <RowStyle ForeColor="#000066" />
                                <Columns>
                                    <asp:BoundField HeaderText="Employee Code" DataField="Emp_Code"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10%" />
                                    <asp:BoundField HeaderText="Year" DataField="CTYear"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="5%" />
                                    <asp:BoundField HeaderText="January" DataField="January"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="6.66%" />
                                    <asp:BoundField HeaderText="February" DataField="February"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="6.66%" />
                                    <asp:BoundField HeaderText="March" DataField="March"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="6.66%" />
                                    <asp:BoundField HeaderText="April" DataField="April"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="6.66%" />
                                    <asp:BoundField HeaderText="May" DataField="May"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="6.66%" />
                                    <asp:BoundField HeaderText="June" DataField="June"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="6.66%" />
                                    <asp:BoundField HeaderText="July" DataField="July"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="6.66%" />
                                    <asp:BoundField HeaderText="August" DataField="August"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="6.66%" />
                                    <asp:BoundField HeaderText="September" DataField="September"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="6.66%" />
                                    <asp:BoundField HeaderText="October" DataField="October"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="6.66%" />
                                    <asp:BoundField HeaderText="November" DataField="November"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="6.66%" />
                                    <asp:BoundField HeaderText="December" DataField="December"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="6.66%" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
            </table>
            <br />
        </div>
        <tr style="padding-bottom: 15px">
            <td class="trvL_detail">
                <span style="padding-left: 45px">
                    <asp:LinkButton ID="lnk_Resignation" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" OnClick="lnk_Resignation_Click"></asp:LinkButton>
                    <span id="Span8" runat="server">Resignation Details</span></span>
            </td>
            <td></td>
            <td></td>
            <td rowspan="1" colspan="1"></td>
        </tr>
        <div style="margin-left: 40px; padding-top: 10px">
            <table id="TABLE6" runat="server">
                <tr>
                    <td>
                        <div class="manage_grid" style="width: 96%; height: auto;" id="Div_Resignation" runat="server" visible="true">
                            <asp:GridView ID="gv_Resignation" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="solid" BorderWidth="1px"
                                CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                <RowStyle ForeColor="#000066" />
                                <Columns>
                                    <asp:BoundField HeaderText="Employee Code" DataField="Emp_Code"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="9%" />
                                    <asp:BoundField HeaderText="Resignation Date" DataField="RESIGN_DATE"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10%" />
                                    <asp:BoundField HeaderText="Reason" DataField="Particulars"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="30%" />
                                    <%--  <asp:BoundField HeaderText="Reason" DataField="Remarks"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="15%" />--%>
                                    <asp:BoundField HeaderText="Release Date" DataField="HrReleaseDate"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="8%" />
                                    <asp:BoundField HeaderText="LWD Date" DataField="LwdAsperpolicy"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="8%" />
                                    <asp:BoundField HeaderText="Resigned on" DataField="ResignationEntryDate"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="8%" />
                                    <asp:BoundField HeaderText="Separation Date" DataField="DATE_OF_SEPARATION"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="9%" />
                                    <asp:BoundField HeaderText="Separated On" DataField="SEPARATION_ENTRYDATE"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="8%" />
                                    <asp:BoundField HeaderText="Status" DataField="StatusName"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10%" />
                                </Columns>
                            </asp:GridView>
                            <span id="Span_Resignation" runat="server" visible="false" style="color: red">Record Not Found </span>
                        </div>
                    </td>
                </tr>
            </table>
            <br />
        </div>
        <tr style="padding-bottom: 15px">
            <td class="trvL_detail">
                <span style="padding-left: 45px">
                    <asp:LinkButton ID="lnk_Project" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" OnClick="lnk_Project_Click"></asp:LinkButton>
                    <span id="Span4" runat="server">Project Responsibilities</span></span>
            </td>
            <td></td>
            <td></td>
            <td rowspan="1" colspan="1"></td>
        </tr>
        <div style="margin-left: 40px; padding-top: 10px">
            <table id="TABLE3" runat="server">
                <tr>
                    <td>
                        <div class="manage_grid" style="width: 96%; height: auto;" id="Div_PR" runat="server" visible="false">
                            <asp:GridView ID="gvPR" runat="server" BackColor="White" AllowPaging="true" OnPageIndexChanging="gvPR_PageIndexChanging" PageSize="10" BorderColor="Navy" BorderStyle="solid" BorderWidth="1px"
                                CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                <RowStyle ForeColor="#000066" />
                                <Columns>
                                    <asp:BoundField HeaderText="Project / Location Code" DataField="comp_code"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="15%" />
                                    <asp:BoundField HeaderText="Project / Location Name" DataField="Location_name"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="25%" />
                                    <asp:BoundField HeaderText="Role" DataField="Role"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="25%" />
                                    <asp:BoundField HeaderText="Status" DataField="Status"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="13%" />
                                </Columns>
                            </asp:GridView>
                            <span id="SpangvPR" runat="server" visible="false" style="color: red">Recored Not Found </span>
                        </div>
                    </td>
                </tr>
            </table>
            <br />
        </div>

        <tr style="padding-bottom: 15px">
            <hr />
            <td class="trvL_detail">
                <span style="">
                    <%--<asp:LinkButton ID="lnkbtn_expdtls" runat="server" Text="+" ToolTip="Browse" CssClass="Savebtnsve" OnClick="lnkbtn_expdtls_Click"></asp:LinkButton>--%>
                    <h1 id="Span1" runat="server">Document Details</h1>
                </span>
            </td>
        </tr>
        <div style="margin-left: 40px; padding-top: 10px">
            <table id="TABLE5_Document" runat="server">

                <tr>
                    <td><b>Basic Uploaded Document</b></td>
                </tr>
                <tr>
                    <td>
                        <div class="manage_grid" style="width: 98%; height: auto;" runat="server" visible="false" id="DIV_Document">
                            <asp:GridView ID="GVDocumentInfo" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="solid" BorderWidth="1px"
                                CellPadding="3" AutoGenerateColumns="False" Width="98%" EditRowStyle-Wrap="false" OnRowDataBound="GVDocumentInfo_RowDataBound">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                <RowStyle ForeColor="#000066" />
                                <Columns>
                                    <asp:BoundField HeaderText="Document Type" DataField="DocumentType"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="25%" />
                                    <asp:BoundField HeaderText="Name" DataField="FileName"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="25%" />
                                    <asp:BoundField HeaderText="Date / Year" DataField="Date/Year"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="13%" />
                                    <asp:TemplateField HeaderText="File View" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkEdit1" runat="server" ToolTip=" File View" Width="15px" Height="15px" ImageUrl="~/Images/Download.png" OnClientClick=<%# "DownloadFile('" + Eval("FilePath") + "')" %> />
                                             <asp:ImageButton ID="lnkEdit2" Visible="false" runat="server" ToolTip=" File View" Width="15px" Height="15px" ImageUrl="~/Images/Download.png" OnClientClick=<%# "DownloadOfferFile('" + Eval("FilePath") + "')" %> />
										</ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <span id="Span_Document" runat="server" visible="false" style="color: red">No Documents Found</span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td><b id="Span5" runat="server">Employment Contract - Documents</b></td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GVFirstGridContractAppletter" runat="server" OnRowDataBound="GVFirstGridContractAppletter_RowDataBound" BackColor="White" BorderColor="Navy" BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="98%"
                            DataKeyNames="Id" AllowPaging="false">
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
                                <asp:BoundField HeaderText="Document Type" DataField="DocumentType" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="13%" />
                                <asp:TemplateField HeaderText="From Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:TextBox CssClass="test12" ID="txtFromDate" Text='<%# Bind("FromDate") %>' runat="server" Width="61%" Enabled="false" Visible="true"></asp:TextBox>
                                        <asp:Label ID="lblid" runat="server" Text='<%# Eval("Id") %>' Visible="false" />
                                        <asp:Label ID="LBLFileName" runat="server" Text='<%# Eval("File") %>' Visible="false" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="To Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:TextBox CssClass="test12" ID="txtToDate" Text='<%# Bind("ToDate") %>' runat="server" Width="61%" Enabled="false" Visible="true"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="lnkEdit1" runat="server" ToolTip="File View" Width="15px" Height="15px" ImageUrl="~/images/Download.png" OnClientClick=<%#"DownloadFileFirstGrid('" + Eval("File") + "')" %> />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>

                    </td>
                </tr>
                <tr>
                    <td><span id="SpanGVFirstGridContractAppletter" runat="server" visible="false" style="color: red">No Documents Found </span></td>
                </tr>
                <tr>
                    <td>
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td><b>Increment Letter/Promotion Letter Year wise</b></td>
                </tr>
                <tr>
                    <td>
                        <div class="manage_grid" style="width: 98%; height: auto;" runat="server" visible="true" id="DIV1">
                            <asp:GridView ID="dvDT7" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="solid" BorderWidth="1px"
                                CellPadding="3" AutoGenerateColumns="False" Width="98%" EditRowStyle-Wrap="false">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                <RowStyle ForeColor="#000066" />
                                <Columns>
                                    <asp:BoundField HeaderText="Document Type" DataField="DocumentType"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="25%" />
                                    <asp:BoundField HeaderText="Name" DataField="FileName"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="25%" />
                                    <asp:BoundField HeaderText="Date / Year" DataField="Date/Year"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="13%" />
                                    <asp:TemplateField HeaderText="File View" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkEdit1" runat="server" ToolTip=" File View" Width="15px" Height="15px" ImageUrl="~/Images/Download.png" OnClientClick=<%# "DownloadFile('" + Eval("FilePath") + "')" %> />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <span id="SpandvDT7" runat="server" visible="false" style="color: red">No Documents Found </span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td><b>Disciplinary Documents</b></td>
                </tr>
                <tr>
                    <td>
                        <div class="manage_grid" style="width: 98%; height: auto;" runat="server" visible="true" id="DIV2">
                            <asp:GridView ID="gvDT8" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="solid" BorderWidth="1px"
                                CellPadding="3" AutoGenerateColumns="False" Width="98%" EditRowStyle-Wrap="false">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                <RowStyle ForeColor="#000066" />
                                <Columns>
                                    <asp:BoundField HeaderText="Document Type" DataField="DocumentType"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="25%" />
                                    <asp:BoundField HeaderText="Name" DataField="FileName"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="25%" />
                                    <asp:BoundField HeaderText="Date / Year" DataField="Date/Year"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="13%" />
                                    <asp:TemplateField HeaderText="File View" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkEdit1" runat="server" ToolTip=" File View" Width="15px" Height="15px" ImageUrl="~/Images/Download.png" OnClientClick=<%# "DownloadFile('" + Eval("FilePath") + "')" %> />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <span id="SpangvDT8" runat="server" visible="false" style="color: red">No Documents Found </span>
                        </div>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td><b>Transfer Letter</b></td>
                </tr>
                <tr>
                    <td>
                        <div class="manage_grid" style="width: 98%; height: auto;" runat="server" visible="true" id="DIV3">
                            <asp:GridView ID="gvDT9" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="solid" BorderWidth="1px"
                                CellPadding="3" AutoGenerateColumns="False" Width="98%" EditRowStyle-Wrap="false">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                <RowStyle ForeColor="#000066" />
                                <Columns>
                                    <asp:BoundField HeaderText="Document Type" DataField="DocumentType"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="25%" />
                                    <asp:BoundField HeaderText="Name" DataField="FileName"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="25%" />
                                    <asp:BoundField HeaderText="Date / Year" DataField="Date/Year"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="13%" />
                                    <asp:TemplateField HeaderText="File View" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkEdit1" runat="server" ToolTip=" File View" Width="15px" Height="15px" ImageUrl="~/Images/Download.png" OnClientClick=<%# "DownloadFile('" + Eval("FilePath") + "')" %> />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <span id="SpangvDT9" runat="server" visible="false" style="color: red">No Documents Found </span>
                        </div>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td><b>Training Certificates</b></td>
                </tr>
                <tr>
                    <td>
                        <div class="manage_grid" style="width: 98%; height: auto;" runat="server" visible="true" id="DIV4">
                            <asp:GridView ID="gvDT10" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="solid" BorderWidth="1px"
                                CellPadding="3" AutoGenerateColumns="False" Width="98%" EditRowStyle-Wrap="false">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                <RowStyle ForeColor="#000066" />
                                <Columns>
                                    <asp:BoundField HeaderText="Document Type" DataField="DocumentType"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="25%" />
                                    <asp:BoundField HeaderText="Name" DataField="FileName"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="25%" />
                                    <asp:BoundField HeaderText="Date / Year" DataField="Date/Year"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="13%" />
                                    <asp:TemplateField HeaderText="File View" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkEdit1" runat="server" ToolTip=" File View" Width="15px" Height="15px" ImageUrl="~/Images/Download.png" OnClientClick=<%# "DownloadFile('" + Eval("FilePath") + "')" %> />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <span id="SpangvDT10" runat="server" visible="false" style="color: red">No Documents Found </span>
                        </div>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td><b>Form16 Year wise</b></td>
                </tr>
                <tr>
                    <td>
                        <div class="manage_grid" style="width: 98%; height: auto;" runat="server" visible="true" id="DIV5">
                            <asp:GridView ID="gvDT11" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="solid" BorderWidth="1px"
                                CellPadding="3" AutoGenerateColumns="False" Width="98%" EditRowStyle-Wrap="false">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                <RowStyle ForeColor="#000066" />
                                <Columns>
                                    <asp:BoundField HeaderText="Document Type" DataField="DocumentType"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="25%" />
                                    <asp:BoundField HeaderText="Name" DataField="FileName"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="25%" />
                                    <asp:BoundField HeaderText="Date / Year" DataField="Date/Year"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="13%" />
                                    <asp:TemplateField HeaderText="File View" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkEdit1" runat="server" ToolTip=" File View" Width="15px" Height="15px" ImageUrl="~/Images/Download.png" OnClientClick=<%# "DownloadFile('" + Eval("FilePath") + "')" %> />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <span id="SpangvDT11" runat="server" visible="false" style="color: red">No Documents Found</span>
                        </div>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td><b>Others</b></td>
                </tr>
                <tr>
                    <td>
                        <div class="manage_grid" style="width: 98%; height: auto;" runat="server" visible="true" id="DIV6">
                            <asp:GridView ID="gvDT12" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="solid" BorderWidth="1px"
                                CellPadding="3" AutoGenerateColumns="False" Width="98%" EditRowStyle-Wrap="false">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                <RowStyle ForeColor="#000066" />
                                <Columns>
                                    <asp:BoundField HeaderText="Document Type" DataField="DocumentType"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="25%" />
                                    <asp:BoundField HeaderText="Name" DataField="FileName"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="25%" />
                                    <asp:BoundField HeaderText="Date / Year" DataField="Date/Year"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="13%" />
                                    <asp:TemplateField HeaderText="File View" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkEdit1" runat="server" ToolTip=" File View" Width="15px" Height="15px" ImageUrl="~/Images/Download.png" OnClientClick=<%# "DownloadFile('" + Eval("FilePath") + "')" %> />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <span id="SpangvDT12" runat="server" visible="false" style="color: red">No Documents Found </span>
                        </div>
                    </td>
                    <td></td>
                </tr>
            </table>
        </div>

        <div class="mobile_Savebtndiv">
            <asp:LinkButton ID="mobile_btnBack" runat="server" Text="Back" ToolTip="Back" OnClick="mobile_btnBack_Click" class="trvl_Savebtndiv">Back</asp:LinkButton>
            <br />
            <br />
        </div>
        <div></div>
    </table>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="Check_In_Out_New.aspx.cs" Inherits="procs_Check_In_Out_New" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/ModifyLeaveRequest_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    
    <style>
#MainContent_btnIn {
    background-attachment: scroll;
    background-clip: border-box;
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
            background-color:#ebebe4;
        }

         .grayDropdownTxt
         {            
            background-color:#ebebe4;
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

	<div id="loader" class="myLoader" style="display:none">
        <div class="loaderContent">
			<span class="DONot">Please  Do Not Refresh  or Close Page</span>
			<img src="../images/loader.gif" ></div>
		
    </div>
    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Record Attendance"></asp:Label>
                    </span>
                  
                </div>
                 <span>
                    <a href="Attendance.aspx" class="aaaa">Attendance Menu</a>
                </span>
     
                <div class="edit-contact">
                   
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                    </div>

                  
                    <ul id="editform" runat="server" visible="false">

                        <li style="width:46%">
                            <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
							<br /><br />
                              <asp:Label runat="server" ID="lbltest" Visible="false" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
						 

                         </li>

 
                          
						
                        <li></li>
                        <li style="width:46%"><asp:Label ID="Label1" runat="server"></asp:Label> </li><li></li>
                        <li class="date">
                            <br />
                            <span>Date</span>                
                                <asp:TextBox ID="txtFromdate" Enabled="false" AutoComplete="off" runat="server" AutoPostBack="true" MaxLength="10" AutoCompleteType="Disabled"></asp:TextBox>
                        </li>
                        <li>

                        </li>
                        <li class="leavedays">
                            <asp:Label ID="In_range" runat="server"></asp:Label><br />
                            <asp:TextBox ID="Txt_InTime" Enabled="false" runat="server"></asp:TextBox>
                            <asp:TextBox ID="Txt_CheckIn" Enabled="false" Visible="false" runat="server"></asp:TextBox>
                        </li>
						<li>

                        </li>
                        <li>
                            <br />
                            <asp:LinkButton ID="btnIn" runat="server" CssClass="Savebtnsve" Text="Check-In" OnClick="btnIn_Click" OnClientClick="return SaveMultiClick();">Check-In</asp:LinkButton>
                        </li>
						<li>

                        </li>
                        <li class="leavedays" runat="server" id="liout">
                         
                            <asp:Label ID="Out_range" runat="server"></asp:Label><br />
                            <asp:TextBox ID="Txt_OutTime" Enabled="false" runat="server"></asp:TextBox>
                            <asp:TextBox ID="Txt_CheckOut" Enabled="false" Visible="false" runat="server"></asp:TextBox>
                        </li>
						<li>

                        </li>
                        <li>
                            <br />
                            <asp:LinkButton ID="btnBack" runat="server" CssClass="Savebtnsve" Text="Check-Out" OnClick="btnBack_Click1" OnClientClick="return SaveOutClick();">Check-Out</asp:LinkButton>
                        </li>

                        <li class="Approver">
                            
                            <br />
                            <asp:ListBox Visible="false" ID="lstApprover" runat="server"></asp:ListBox>
                    <asp:GridView ID="DgvApprover" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="80%">
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
                                DataField="att_date"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="10%" 
                                ItemStyle-BorderColor="Navy"
                                />

                            <asp:BoundField HeaderText="In Time - Out Time"
                                DataField="IN_OUT_TIME"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="15%" 
                                ItemStyle-BorderColor="Navy"
                                />

                          <asp:BoundField HeaderText="Total Hours"
                                DataField="TotalHr"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="10%" 
                                ItemStyle-BorderColor="Navy"
                                />

                            <asp:BoundField HeaderText="Status"
                                DataField="IN_OUT_STATUS"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="15%" 
                                ItemStyle-BorderColor="Navy"
                                />
                           
                        </Columns>
                    </asp:GridView>
                        </li>


                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="Savebtndiv">
        <asp:LinkButton ID="btnSave1" Visible="false"  runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve"  OnClientClick="return SaveMultiClick();">Submit</asp:LinkButton>
        
            </div>
    <div>
        

        <asp:LinkButton ID="btnback_mng" runat="server" Text="Back" ToolTip="Back" Visible="false" CssClass="Savebtnsve" PostBackUrl="~/procs/MyLeave_Req.aspx">Back</asp:LinkButton>
               
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
    <asp:HiddenField ID="hdnTypeId" runat="server" />
    <asp:HiddenField ID="hdnHalfTime" runat="server" />

	<link href="../includes/loader.css" rel="stylesheet" />
    <script type="text/javascript">
   
        var xmlHttp;
        function srvTime() {
            alert("kkk");
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
         function convertTZ(date, tzString) {
            return new Date((typeof date === "string" ? new Date(date) : date).toLocaleString("en-US", { timeZone: tzString }));
        }
        function ShowTime() {
            var st = srvTime();
             var getCo = convertTZ(st, "Asia/Kolkata");
            //console.log(getCo);
            //var dt = new Date(st);
            var dt = getCo;
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
        }
       // window.setTimeout("ShowTime()", 10);
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
        function ConfirmIn() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Submit ?")) {
				confirm_value.value = "Yes";
				StartLoader();
            } else {
                confirm_value.value = "No";
               
            }
            
            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return;

		}

		function StartLoader() {
			
			$('#loader').show();
		}
		function StopLoader() {	
		 $('#loader').hide();
		}

    </script>
    
</asp:Content>


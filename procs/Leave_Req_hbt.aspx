<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="Leave_Req_hbt.aspx.cs" Inherits="Leave_Req_hbt" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link href="<%=ReturnUrl("css") %>calendar/calendar.css" rel="stylesheet" type="text/css" />
    <link href="sampleform_css.css" rel="stylesheet" type="text/css" />
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
                        <asp:Label ID="lblheading" runat="server" Text="Leave Request"></asp:Label>
                    </span>
                </div>

                <div class="containerDiv">
                </div>
                    <h3>Leave Card - 2020</h3>
              
                        <asp:GridView ID="dgLeaveBalance" runat="server"  BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                            <PagerStyle ForeColor="#000066" HorizontalAlign="Left" BackColor="White" />
                            <RowStyle ForeColor="#000066" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                        </asp:GridView>
             

               
                    <%--  <div class="rowDivHeader">
    <div class="cellDivHeader cellwpl">Type</div>
    <div class="cellDivHeader">BAL</div>
    <div class="cellDivHeader">Availed</div>
    <div class="cellDivHeader">BAL</div>
  </div>

  <div class="rowDiv">
    <div class="cellDiv cellwpl">SL</div>
    <div class="cellDiv cellwp2">22</div>
    <div class="cellDiv cellwp3">22</div>
    <div class="cellDiv ">22</div>
    
  </div>
   <div class="rowDiv">
    <div class="cellDiv cellwpl">ML</div>
    <div class="cellDiv cellwp2">10</div>
    <div class="cellDiv cellwp3">10</div>
    <div class="cellDiv">10</div>
    
  </div>
   <div class="rowDiv">
    <div class="cellDiv cellwpl">PL</div>
    <div class="cellDiv cellwp2">15</div>
    <div class="cellDiv cellwp3">15</div>
    <div class="cellDiv">15</div>
    
  </div>
   <div class="rowDiv">
    <div class="cellDiv cellwpl">W/O PAY</div>
    <div class="cellDiv cellwp2"></div>
    <div class="cellDiv cellwp3"></div>
    <div class="cellDiv"></div>
    
  </div>
   <div class="rowDiv">
    <div class="cellDiv cellwpl">Time Off</div>
    <div class="cellDiv cellwp2"></div>
    <div class="cellDiv cellwp3"></div>
    <div class="cellDiv "></div>
    
  </div>--%>


                <a href="#" class="aaa" onclick="">Leave Menu</a>



                <div class="edit-contact">
                    <div class="editprofile" style="text-align: center; border: none;" id="divmsg" runat="server" visible="false">
                        <asp:Label runat="server" ID="lblmessage" Visible="false" Style="color: green; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                    </div>
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" ></asp:LinkButton>
                        </div>
                    </div>


                    <ul id="editform" runat="server" visible="false">
                        <li class="profile-edit">
                            <span>Leave Type</span><br />
                            <asp:UpdatePanel ID="Upl_LeaveType" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlLeaveType" AutoPostBack="true" runat="server">
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlLeaveType" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </li>
                      <li class="profile-edit">

                        </li>

                        <li class="date">
                        <div class="date-picker-start-end-date">
                            <div class="title-all-post">From</div>
                            <span class="startdatepickercontent">
                                <asp:TextBox ID="txtsadate" runat="server" CssClass="txtdatepicker" placeholder="Start Date" OnTextChanged ="txtsadate_TextChanged" AutoPostBack ="true" ></asp:TextBox>
                                <span class="texticon tooltip" title="Select Start Date. Date should be MM/DD/YYYY format."><i class="fa fa-calendar" aria-hidden="true"></i></span>
                                <asp:Label ID="lblsdate" runat="server" Text="" Font-Size="15px"></asp:Label>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="formerror" ControlToValidate="txtsadate" SetFocusOnError="True" ErrorMessage="Please enter start date" ValidationGroup="validate"></asp:RequiredFieldValidator>--%>
                            </span>
                            </div>
                        </li>

                        <li class="profile-edit">
                            <span>For</span><br />
                                <asp:UpdatePanel ID="uplFromfor" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlFromFor" AutoPostBack="true" runat="server" OnSelectedIndexChanged ="ddlFromFor_SelectedIndexChanged">
                                            <asp:ListItem>Full Day</asp:ListItem>
                                            <asp:ListItem>First Half</asp:ListItem>
                                            <asp:ListItem>Second Half</asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlFromFor" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            <span class="texticon"></span>
                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                        </li>

                        
                        <li class="date">
                        <div class="date-picker-start-end-date">
                            <div class="title-all-post">To</div>
                            <span class="enddatepickercontent">
                                <asp:TextBox ID="txtedate" runat="server" CssClass="txtdatepicker" placeholder="End Date" AutoPostBack ="true"  OnTextChanged ="txtedate_TextChanged"></asp:TextBox>
                             <span class="texticon tooltip" title="Select Start Date. Date should be MM/DD/YYYY format."><i class="fa fa-calendar" aria-hidden="true"></i></span>
                                <asp:Label ID="lbledate" runat="server" Text="" Font-Size="15px"></asp:Label>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="formerror" ControlToValidate="txtedate" SetFocusOnError="True" ErrorMessage="Please enter end date" ValidationGroup="validate"></asp:RequiredFieldValidator><br />--%>
                            </span>
                            </div>
                        </li>

                        <li class="profile-edit">
                            <span>For</span><br />
                            <asp:UpdatePanel ID="uplTofor" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlToFor" AutoPostBack="true" runat="server">
                                        <asp:ListItem>Full Day</asp:ListItem>
                                        <asp:ListItem>First Half</asp:ListItem>
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlToFor" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <span class="texticon"></span>
                            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                        </li>

                        <li>
                            <span>Leave Days</span><br />
                            <%-- <asp:TextBox ID="txtEmpCode" runat="server" AutoPostBack="True" OnTextChanged="txtEmpCode_TextChanged"></asp:TextBox>--%>
                            <asp:TextBox ID="txtLeaveDays" runat="server" AutoPostBack="True" OnTextChanged ="txtLeaveDays_TextChanged"> </asp:TextBox>
                           <%-- <span class="texticon tooltip" title="Please enter number of days."><i class="fa fa-user"></i></span>--%>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" CssClass="formerror" ControlToValidate="txtLeaveDays" ValidationGroup="validate" SetFocusOnError="true" />
                        </li>
                        <li class="profile-edit">

                        </li>
<%--                        <li> 
                            <span></span>
                            <br />
                            <br />
                        </li>--%>

                        <li class="profile-edit">
                            <span>Remarks </span>
                            <br />
                            <%-- <asp:TextBox ID="txtEmpCode" runat="server" AutoPostBack="True" OnTextChanged="txtEmpCode_TextChanged"></asp:TextBox>--%>
                            <asp:TextBox ID="txtReason" runat="server" MaxLength="100" AutoPostBack="True"> </asp:TextBox>
                            <span class="texticon tooltip" title="Please enter reason for leave."></span>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter reason for leave" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" CssClass="formerror" ControlToValidate="txtReason" ValidationGroup="validate" SetFocusOnError="true" />
                        </li>
                        
                        <li class="profile-edit">
                            <span>Upload File</span><br />
                            <asp:FileUpload ID="uploadprofile" runat="server" />
                            <asp:TextBox ID="txtprofile" runat="server" CssClass="mobilenotextbox popup" Visible="false"> </asp:TextBox>
                            <span class="texticonselect tooltip" title="Please provide your profile picture."><i class="fa fa-file-image-o"></i></span>

                        </li>

                        <li class="profile-edit">
                            <span>Approver </span>
                            <br />
                            <asp:ListBox Visible="false" ID="lstApprover" runat="server"></asp:ListBox>
                    <asp:GridView ID="DgvApprover" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%">
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
                            <asp:BoundField HeaderText="Approver Name"
                                DataField="tName"
                                ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="left"
                                ItemStyle-Width="25%" 
                                ItemStyle-BorderColor="Navy"
                                />

                            <asp:BoundField HeaderText="Status"
                                DataField="Status"
                                ItemStyle-HorizontalAlign="left"
                                ItemStyle-Width="12%" 
                                ItemStyle-BorderColor="Navy"
                                />

                            <asp:BoundField HeaderText="Approved on"
                                DataField="tdate"
                                ItemStyle-HorizontalAlign="left"
                                ItemStyle-Width="12%" 
                                ItemStyle-BorderColor="Navy"
                                />

                            <asp:BoundField HeaderText="Approver Remarks"
                                DataField="Comment"
                                ItemStyle-HorizontalAlign="left"
                                ItemStyle-Width="46%" 
                                ItemStyle-BorderColor="Navy"
                                />
                            
                            <asp:BoundField HeaderText="APPR_ID"
                                DataField="APPR_ID"
                                ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="2%" 
                                ItemStyle-BorderColor="Navy"
                                Visible="false"
                                />
                            
                            <asp:BoundField HeaderText="Emp_Name"
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
                                />
                        </Columns>
                    </asp:GridView>
                        </li> 

                        <li class="profile-edit" style="display:none;">
                            <span> For Information To </span>
                            <br />
                            <asp:ListBox ID="lstIntermediate" runat="server"></asp:ListBox>
                        </li> 


                        

                         <li>
                           
                            <%-- <asp:TextBox ID="txtEmpCode" runat="server" AutoPostBack="True" OnTextChanged="txtEmpCode_TextChanged"></asp:TextBox>--%>
                            <asp:TextBox ID="txtEmpCode" runat="server" MaxLength="50" AutoPostBack="True" Visible ="false" > </asp:TextBox>
                         
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />
                        </li>

<%--                        <li> 
                            <span></span>
                            <br />
                            <br />
                        </li>--%>

                        <li class="proviewbtn btndiv" style="float: left !important;">

                            <%-- JAYESH COMMNETED BELOW 19sep2017--%>
                            <%--  <div class="cancelbtndiv">
                                <asp:LinkButton ID="btnsubmit" runat="server" Text="Update" ToolTip="Update" ValidationGroup="validate" CssClass="submitbtnupdate" OnClick="btnSaveChanges_Click"><i class="fa fa-pencil" aria-hidden="true"></i>Update</asp:LinkButton>
                            </div>--%>
                            <%--           JAYESH COMMNETED ABOVE to disabled the functionality of save19sep2017 --%>

                            <div class="Savebtndiv">
                                <asp:LinkButton ID="btnSave" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClick="btnSave_Click1">Submit</asp:LinkButton>
                                <asp:LinkButton ID="btnBack" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="btnBack_Click">Back</asp:LinkButton>
                            </div>

                        </li>

                    </ul>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hflEmpName" runat="server" />

    <asp:HiddenField ID="hflEmpDesignation" runat="server" />   

    <asp:HiddenField ID="hflEmpDepartment" runat="server" />

    <asp:HiddenField ID="hflEmailAddress" runat="server" />

    <asp:HiddenField ID="hflGrade" runat="server" />

    <asp:HiddenField ID="hflapprcode" runat="server" />

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

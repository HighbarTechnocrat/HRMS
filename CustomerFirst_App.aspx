    <%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="CustomerFirst_App.aspx.cs" Inherits="CustomerFirst_App" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <%--   <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <%-- <link href="<%=ReturnUrl("css") %>calendar/calendar.css" rel="stylesheet" type="text/css" />
    <link href="sampleform_css.css" rel="stylesheet" type="text/css" />--%>
    <%--<ajaxToolkit:ToolkitScriptManager ID="srcp" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
             /*background: #dae1ed;*/
           background:#ebebe4;
        }

        #MainContent_lstApprover {
            overflow: hidden !important;
        }
        #MainContent_service_btnAssgine, 
        #MainContent_service_btnClose, 
        #MainContent_service_btnEscelateToHOD,
        #MainContent_service_btnEscelateToCEO,
        #MainContent_service_btnClearText,
        #MainContent_service_btnSendSPOC,
        #MainContent_mobile_btnCorrection {
    background: #3D1956;
    color: #febf39 !important;
    padding: 0.5% 1.4%;
    margin: 0% 0% 0 0;
}
           .noresize {
            resize: none;
        }
        #MainContent_service_btnAssgine, #MainContent_service_btnClose, #MainContent_service_btnEscelateToHOD, #MainContent_service_btnEscelateToCEO, #MainContent_service_btnClearText, #MainContent_service_btnSendSPOC, #MainContent_mobile_btnCorrection {
            background: #3D1956;
            color: #febf39 !important;
            padding: 1.5% 1.4%;
            margin: 0% 0% 0 0;
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
                        <asp:Label ID="lblheading" runat="server" Text="CustomerFIRST - Response Negative Feedback"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>
                
                <span runat="server" id="backToArr" visible="false">
                     <a href="ViewSurveyReport.aspx" class="aaaa">Back</a>
                </span>
                 <span runat="server" id="backToApp" visible="false">
                     <a href="InboxCustomerFirst.aspx" class="aaaa">Back</a>
                </span>
                <span runat="server" id="homeBtn">
                    <a href="customerFirst.aspx" style="margin-right: 18px;" class="aaaa">CustomerFIRST Home</a>&nbsp;&nbsp; 
                </span>
                <div class="edit-contact">
                    <div class="editprofile" style="text-align: center; border: none;" id="divmsg" runat="server" visible="false">
                        
                    </div>
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server"></asp:LinkButton>
                        </div>
                    </div>

                    
                    <ul id="editform" runat="server" visible="false">

                                               
                        <li class="mobile_InboxEmpName" >
                           
                            <span>Survey No.</span>
                            <br />
                             <asp:TextBox AutoComplete="off" ID="txtSurveyNo" Enabled="false" runat="server"></asp:TextBox>
                        </li>
                         <li class="mobile_inboxEmpCode">
                        </li>
                         <li class="mobile_InboxEmpName">
                            <span>Client</span><br />

                            <asp:TextBox AutoComplete="off" ID="txtClient" runat="server" Visible="True" Enabled="false"></asp:TextBox>

                        </li>
                          <li class="mobile_inboxEmpCode">
                            <span>Reply Date</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtReplyDate" runat="server" MaxLength="100" Enabled="false"></asp:TextBox>
                        </li>
                         <li class="mobile_InboxEmpName">                            
                            <span >Contact Name </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtContactName" Enabled="false" runat="server"></asp:TextBox>
                        </li>

                        <li class="mobile_inboxEmpCode">                            
                            <span >Contact Designation  </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_Designation" Enabled="false" runat="server" AutoPostBack="true"></asp:TextBox>
                        </li>
                        
                      
                        <li style="visibility:hidden"></li>                       
                       
                        <li class="mobile_Approver" >
                           <span><b>Question and Answer Details</b> </span>
                            <br /><br />
                        </li>


                         <li style="width: 100%;">
                            <span> </span>
                            <asp:GridView ID="gvQuestionDetails" runat="server" BackColor="White" DataKeyNames="id" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" HorizontalAlign="Center" />
                                <PagerStyle ForeColor="#000066" HorizontalAlign="left" BackColor="White" />
                                <RowStyle ForeColor="#000066" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />

                                <Columns>
                                     <asp:BoundField HeaderText="Question"
                                        DataField="Question"
                                        ItemStyle-HorizontalAlign="left"
                                        ItemStyle-Width="20%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Answer"
                                        DataField="Answer"
                                        ItemStyle-HorizontalAlign="left"
                                        ItemStyle-Width="20%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:TemplateField HeaderText="Icon" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Image ID="Image1" Height="50" Width="50" runat="server"
                                                ImageUrl='<%# ResolveUrl(Eval("IconFilePath").ToString()) %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:BoundField HeaderText="Call Back Incident Raised"
                                        DataField="CallBackIncidentRaised"
                                        ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="26%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Call Back Incident Status"
                                        DataField="CallBackIncidentStatus"
                                        ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="26%"
                                        ItemStyle-BorderColor="Navy" />


                                </Columns>
                            </asp:GridView>
                             <br /><br />
                        </li>
                         <hr />
                        
                        <li class="mobile_inboxEmpCode">
                            <br />  
                            <span>Customer Comments</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_Customer_Description" runat="server" TextMode="MultiLine" Rows="6" Width="188%" CssClass="noresize" Enabled="false"></asp:TextBox>
                        </li>
                         <li style="visibility:hidden" class="mobile_inboxEmpCode">

                        </li>
                         <li class="mobile_inboxEmpCode" runat="server" id="litxtComment">
                            <span runat="server" id="lblCommentLble">Actor Comments</span>&nbsp;<span style="color:red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtComment" runat="server" TextMode="MultiLine" Rows="6" Width="188%" CssClass="noresize" Enabled="true"></asp:TextBox>
                        </li>
                         <li style="visibility:hidden" class="mobile_grid">
                           
                        </li>
                        <li class="mobile_inboxEmpCode" runat="server" id="litxtCommentForClient">
                            <span runat="server" id="lbllblcomment">Comments For Client Contact</span>&nbsp;<span style="color:red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtCommentForClient" runat="server" TextMode="MultiLine" Rows="6" Width="188%" CssClass="noresize" Enabled="true"></asp:TextBox>
                        </li>
                        <li style="visibility:hidden" class="mobile_grid">
                           
                        </li>
                        <%--<li style="visibility:hidden" class="mobile_grid">
                           
                        </li>--%>
                        <li runat="server" id="libtn">
                            <asp:LinkButton ID="service_btnEscelateToCEO" Visible="false" runat="server" Text="Close Incident and Resend Survey" ToolTip="Close Incident and Resend Survey For CEO Submit" CssClass="Savebtnsve" OnClientClick="return submitCEO();" OnClick="service_btnEscelateToCEO_Click">Close Incident and Resend Survey</asp:LinkButton>
                            <asp:LinkButton ID="service_btnEscelateToHOD" Visible="false" runat="server" Text="Close Incident and Resend Survey" ToolTip="Close Incident and Resend Survey For HOD Submit" CssClass="Savebtnsve" OnClientClick="return submitHOD();" OnClick="service_btnEscelateToHOD_Click">Close Incident and Resend Survey</asp:LinkButton>
                            <asp:LinkButton ID="service_btnSendSPOC" Visible="false" runat="server" Text="Close Incident and Resend Survey" ToolTip="Close Incident and Resend Survey For HOD Submit" CssClass="Savebtnsve" OnClientClick="return submitHODPM();" OnClick="service_btnSendSPOC_Click">Close Incident and Resend Survey</asp:LinkButton>
                            <asp:LinkButton ID="service_btnAssgine" Visible="false" runat="server" Text="Close Incident and Resend Survey" ToolTip="Close Incident and Resend Survey For PM Submit" CssClass="Savebtnsve" OnClientClick="return submitPM();" OnClick="btnSave_Click">Close Incident and Resend Survey</asp:LinkButton>
                            <asp:LinkButton ID="service_btnClose"  runat="server" Text="Close Service Request" ToolTip="Close Service Request" CssClass="Savebtnsve" OnClick="btnClose_Click" >Cancel</asp:LinkButton>
                       <br /><br />
                            </li>
                         <hr />
                        <li>

                            <span><b>Survey History</b> </span>
                            <br /><br />
                        </li>
                        <li style="visibility:hidden">

                        </li>
                        <li style="width: 100%;">
                            <span> </span>
                            <asp:GridView ID="gvSurveyHistory" runat="server" BackColor="White" DataKeyNames="id" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" HorizontalAlign="Center" />
                                <PagerStyle ForeColor="#000066" HorizontalAlign="left" BackColor="White" />
                                <RowStyle ForeColor="#000066" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />

                                <Columns>
                                     <asp:BoundField HeaderText="Action"
                                        DataField="StatusTitle"
                                        ItemStyle-HorizontalAlign="left"
                                        ItemStyle-Width="20%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Action Date"
                                        DataField="CreatedDate"
                                        ItemStyle-HorizontalAlign="left"
                                        ItemStyle-Width="20%"
                                        ItemStyle-BorderColor="Navy" />

                                      <asp:BoundField HeaderText="Action By"
                                        DataField="UserName"
                                        ItemStyle-HorizontalAlign="left"                                        
                                        ItemStyle-Width="26%"
                                        ItemStyle-BorderColor="Navy" />
                                     <asp:BoundField HeaderText="Client Contact"
                                        DataField="ContactName"
                                        ItemStyle-HorizontalAlign="left"                                        
                                        ItemStyle-Width="26%"
                                        ItemStyle-BorderColor="Navy" />
                                    <asp:TemplateField HeaderText="View" HeaderStyle-Width="5%">
                                         <ItemTemplate>
                                             <asp:ImageButton ID="lnkEdit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnkEdit_Click" />
                                         </ItemTemplate>
                                         <ItemStyle HorizontalAlign="Center" />
                                     </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </li>
                        <li class="mobile_inboxEmpCode" style="visibility: hidden">
                            <asp:TextBox AutoComplete="off" ID="TextBox2" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
                        </li>
                       <hr />
                        <li>

                            <span><b>Survey History Details</b> </span>
                            <br /><br />
                        </li>
                        <li style="visibility:hidden">

                        </li>
                       <li style="width: 100%;">
                            <span> </span>
                            <asp:GridView ID="GridView1" runat="server" BackColor="White" DataKeyNames="id" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" HorizontalAlign="Center" />
                                <PagerStyle ForeColor="#000066" HorizontalAlign="left" BackColor="White" />
                                <RowStyle ForeColor="#000066" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />

                                <Columns>
                                     <asp:BoundField HeaderText="Question"
                                        DataField="Question"
                                        ItemStyle-HorizontalAlign="left"
                                        ItemStyle-Width="20%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Answer"
                                        DataField="Answer"
                                        ItemStyle-HorizontalAlign="left"
                                        ItemStyle-Width="20%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:TemplateField HeaderText="Icon" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Image ID="Image1" Height="50" Width="50" runat="server"
                                                ImageUrl='<%# ResolveUrl(Eval("IconFilePath").ToString()) %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:BoundField HeaderText="Call Back Incident Raised"
                                        DataField="CallBackIncidentRaised"
                                        ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="26%"
                                        ItemStyle-BorderColor="Navy" />

                                    <asp:BoundField HeaderText="Call Back Incident Status"
                                        DataField="CallBackIncidentStatus"
                                        ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="26%"
                                        ItemStyle-BorderColor="Navy" />


                                </Columns>
                            </asp:GridView>
                             <br /><br />
                        </li>
                         <li class="mobile_InboxEmpName">
                            <span>Action By</span><br />

                            <asp:TextBox AutoComplete="off" ID="txtActionBy" runat="server" Visible="True" Enabled="false"></asp:TextBox>

                        </li>
                          <li class="mobile_inboxEmpCode">
                            <span>Action Date</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtActionDate" runat="server" MaxLength="100" Enabled="false"></asp:TextBox>
                        </li>
                                <li class="mobile_inboxEmpCode">
                            <span>Action Status</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtActionStatus" runat="server" MaxLength="100" Enabled="false"></asp:TextBox>
                        </li>
                        <li></li>
                          <li class="mobile_inboxEmpCode">
                            <br />  
                            <span>Customer Comments</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtCustomerCommntesHI" runat="server" TextMode="MultiLine" Rows="6" Width="188%" CssClass="noresize" Enabled="false"></asp:TextBox>
                        </li>
                         <li style="visibility:hidden" class="mobile_inboxEmpCode">

                        </li>
                         
                        <li class="mobile_inboxEmpCode">
                            <span>Comments For Client Contact</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtCommentsForClientHi" runat="server" TextMode="MultiLine" Rows="6" Width="188%" CssClass="noresize" Enabled="false"></asp:TextBox>
                        </li>
                        <li style="visibility:hidden" class="mobile_grid">
                           
                        </li>
                        <li class="mobile_inboxEmpCode" style="visibility:hidden">
                            <span runat="server" id="Span1">Action Comments</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtActionComments" runat="server" TextMode="MultiLine" Rows="6" Width="188%" CssClass="noresize" Enabled="false"></asp:TextBox>
                        </li>
                         <li style="visibility:hidden" class="mobile_grid">
                           
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="mobile_Savebtndiv">

    </div>

    <br />
    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>--%>
    <%--<asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />--%>
    
    <asp:HiddenField ID="hdnvouno" runat="server" />

    <asp:HiddenField ID="hflEmpDesignation" runat="server" />

    <asp:HiddenField ID="hflEmpDepartment" runat="server" />

    <asp:HiddenField ID="hflEmailAddress" runat="server" />
    <asp:HiddenField ID="hflEmp_Name" runat="server" />

    <asp:HiddenField ID="hflGrade" runat="server" />

    <asp:HiddenField ID="hdnContactEmail" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />

    <asp:HiddenField ID="hdnempcode" runat="server" />
    <asp:HiddenField ID="hdnRemid" runat="server" />
    <asp:HiddenField ID="hdnRemid_Type" runat="server" />
    <asp:HiddenField ID="lblQuestion" runat="server" />
    <asp:HiddenField ID="hdnMailSubject" runat="server" />
    <asp:HiddenField ID="hdnMailBody" runat="server" />
    <asp:HiddenField ID="hdnDepartmentId" runat="server" />
    <asp:HiddenField ID="hdnPMComment" runat="server" />
    <asp:HiddenField ID="hdnHODComment" runat="server" />
    <asp:HiddenField ID="hdnStatus" runat="server" />
    
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

        
        function submitHOD() {
            try {
                var msg = "Do you want to send survey to customer";
                var retunboolean = true;
                var ele = document.getElementById('<%=service_btnEscelateToHOD.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        retunboolean = ConfirmToSendSurvey(msg);
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }

        function submitHODPM() {
            try {
                var msg = "Do you want to send survey to customer";
                var retunboolean = true;
                var ele = document.getElementById('<%=service_btnSendSPOC.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        retunboolean = ConfirmToSendSurvey(msg);
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }

        function submitPM() {
            try {
                var msg = "Do you want to send survey to customer";
                var retunboolean = true;
                var ele = document.getElementById('<%=service_btnAssgine.ClientID%>');

                        if (ele != null && !ele.disabled)
                            retunboolean = true;
                        else
                            retunboolean = false;
                        if (ele != null) {
                            ele.disabled = true;
                            if (retunboolean == true)
                                retunboolean = ConfirmToSendSurvey(msg);
                        }
                    }
                    catch (err) {
                        alert(err.description);
                    }
                    return retunboolean;
                }
        function submitCEO() {
            try {
                var msg = "Do you want to send survey to customer";
                var retunboolean = true;
                var ele = document.getElementById('<%=service_btnEscelateToCEO.ClientID%>');

                        if (ele != null && !ele.disabled)
                            retunboolean = true;
                        else
                            retunboolean = false;
                        if (ele != null) {
                            ele.disabled = true;
                            if (retunboolean == true)
                                retunboolean = ConfirmToSendSurvey(msg);
                        }
                    }
                    catch (err) {
                        alert(err.description);
                    }
                    return retunboolean;
                }
        function onCharOnlyNumber(e) {
            var keynum;
            var keychar;
            var numcheck = /[0123456789.]/;

            if (window.event) {
                keynum = e.keyCode;
            }
            else if (e.which) {
                keynum = e.which;
            }
            keychar = String.fromCharCode(keynum);
            return numcheck.test(keychar);
        }
        //
       
        
        // Confirm To All Button
        function ConfirmToSendSurvey(msg) {
            //Testing();
            var isConfirm = false;
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm(msg)) {
                confirm_value.value = "Yes";
                isConfirm = true;
            } else {
                confirm_value.value = "No";
                isConfirm = false;
            }
            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return isConfirm;
        }
       
        
    </script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" 
    CodeFile="InterviewRescheduledList.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="procs_InterviewRescheduledList" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/manageleave_css.css" type="text/css" media="all" />
     <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" /> 
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />


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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

     <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
    <script src="../js/HtmlControl/jquery-1.3.2.js"></script>
    <script src="../js/dist/jquery-3.2.1.min.js"></script>       
    <script src="../js/HtmlControl/jHtmlArea-0.8.min.js"></script>
    <link href="../js/HtmlControl/jHtmlArea.css" rel="stylesheet"/>

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
                        <asp:Label ID="lblheading" runat="server" Text="Scheduled Interviews List"></asp:Label>
                    </span>
                </div>
                  <div>
                        <asp:Label runat="server" ID="lblmessagesearch" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                    </div>
                <div>
                <span>
                    <a href="Requisition_Index.aspx" class="aaaa">Recruitment Home</a>
                </span>
                </div>

                <div class="edit-contact">
                    <ul id="Ul1" runat="server">
                          <li style="padding-top:30px">
                           <span>Skill Set</span>&nbsp;&nbsp;
                             <br />
                           <asp:DropDownList runat="server" ID="lstSkillSet" CssClass="DropdownListSearch">
                            </asp:DropDownList>
                            </li>
                        <li style="padding-top:30px">
                           <span>Location</span>&nbsp;&nbsp;
                             <br />
                           <asp:DropDownList runat="server" ID="LstLocation" CssClass="DropdownListSearch">
                            </asp:DropDownList>
                        </li>
                         <li style="padding-top:30px">
                             <span>Requisition Number</span>&nbsp;&nbsp;
                             <br />
                           <asp:DropDownList runat="server" ID="lstRequisitionNo" CssClass="DropdownListSearch"> 
                            </asp:DropDownList>
                         </li>
                        <li style="padding-top:15px">
                            <span>Band</span>&nbsp;&nbsp;
                             <br />
                           <asp:DropDownList runat="server" ID="Lstband" CssClass="DropdownListSearch">
                            </asp:DropDownList>
                        </li>
                        <li style="padding-top:15px">
                            <span>Department</span>&nbsp;&nbsp;
                             <br />
                           <asp:DropDownList runat="server" ID="LstDepartment" CssClass="DropdownListSearch"> 
                            </asp:DropDownList>
                        </li>
                        <li style="padding-top:15px">
                        </li>
                         </ul>
                     <%--<asp:HiddenField ID="hdCandidate_ID" runat="server" /> --%>
                </div>


                 </div>
        </div>
    </div>

      <div class="mobile_Savebtndiv">
        <asp:LinkButton ID="mobile_btnSave" runat="server" Text="Submit" ToolTip="Search" CssClass="Savebtnsve"  OnClick="mobile_btnSave_Click"> Search</asp:LinkButton>
        <asp:LinkButton ID="mobile_btnBack" runat="server" Text="Clear Search"  OnClick="mobile_btnBack_Click" ToolTip="Clear Search" class="trvl_Savebtndiv">Clear Search</asp:LinkButton>
       </div>
                <div class="manage_grid" style="width: 100%; height: auto; padding-top:10px;">
                    <center>
                          <asp:GridView ID="GVInterviewRescheduledList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="solid" BorderWidth="1px" 
                            DataKeyNames="Recruitment_ReqID,Candidate_ID,CandidateScheduleRound_ID"  OnPageIndexChanging="GVInterviewRescheduledList_PageIndexChanging" CellPadding="3" AutoGenerateColumns="False" Width="100%"   EditRowStyle-Wrap="false" PageSize="20" AllowPaging="True">
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                        <PagerStyle ForeColor="#000066" HorizontalAlign="right" BackColor="White" />
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                        <Columns>
                             <asp:TemplateField HeaderText="View" HeaderStyle-Width="10%">
                             <ItemTemplate>
                             <asp:ImageButton id="lnkEditFeedBack" runat="server" Width="20px" Height="15px"  OnClick="lnkEditFeedBack_Click"  ImageUrl="~/Images/edit.png"/>
                             </ItemTemplate>
                             <ItemStyle HorizontalAlign="Center" />
                         </asp:TemplateField>
                            <asp:BoundField HeaderText="Requisition No"  DataField="RequisitionNumber" ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="15%"  ItemStyle-BorderColor="Navy" />
                            <asp:BoundField HeaderText="Department"  DataField="Department_Name"
                                ItemStyle-Width="15%"  ItemStyle-BorderColor="Navy" />
                            <asp:BoundField HeaderText="Skill Set" DataField="SkillSet" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="8%" />
                            <asp:BoundField HeaderText="Position Title" DataField="PositionTitle"  ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="left" ItemStyle-Width="14%"  ItemStyle-BorderColor="Navy"/>
                            <asp:BoundField HeaderText="Band"  DataField="Band" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="4%" />
                            <asp:BoundField HeaderText="Location" DataField="Location_name" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="13%" />
                          <%--  <asp:BoundField HeaderText="No Of Position" DataField="NoOfPosition"
                                 ItemStyle-HorizontalAlign="center" ItemStyle-Width="5%"  ItemStyle-BorderColor="Navy"/> --%>
                           <%-- <asp:BoundField HeaderText="Requestor Name"  DataField="RequestorName" ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="15%"  ItemStyle-BorderColor="Navy" />--%>
                            <asp:BoundField HeaderText="Requisition Status" DataField="Request_status"
                                 ItemStyle-HorizontalAlign="center"  ItemStyle-Width="7%" ItemStyle-BorderColor="Navy" />
                            <asp:BoundField HeaderText="Candidate Name"  DataField="CandidateName" 
                                ItemStyle-Width="16%"  ItemStyle-BorderColor="Navy" />
                           <%-- <asp:BoundField HeaderText="Interview Status"  DataField="InterviewStatus" ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="11%"  ItemStyle-BorderColor="Navy" />--%>
                            <asp:BoundField HeaderText="Interview Date"  DataField="EnterviewDate"  ItemStyle-HorizontalAlign="center" 
                                ItemStyle-Width="12%"  ItemStyle-BorderColor="Navy"/>
                            <asp:BoundField HeaderText="Interview Time"  DataField="InterviewTime"  ItemStyle-HorizontalAlign="center" 
                                ItemStyle-Width="12%"  ItemStyle-BorderColor="Navy"/>
                            
                        </Columns>
                    </asp:GridView>
                    </center>
                </div>

                <div class="edit-contact">
                  
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" ></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" ></asp:LinkButton>
                        </div>
                    </div>


                    <ul id="editform" runat="server" visible="false">

                        <li>
                            <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                        </li>

                    </ul>
                </div>
                <asp:HiddenField ID="hdnInboxType" runat="server" />
                <asp:HiddenField ID="hdnRecruitment_ReqID" runat="server" />
    <asp:HiddenField ID="hdLinkType" runat="server" />
    <asp:HiddenField ID="HFRecruitment_ReqID" runat="server" />
    <asp:HiddenField ID="HFCandidateID" runat="server" />
    <asp:HiddenField ID="HFISLMID" runat="server" />
    <asp:HiddenField ID="HFCanRID" runat="server" />
               
     <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" /> 

     <script type="text/javascript">      
        $(document).ready(function () {
            $(".DropdownListSearch").select2();
        });
    </script>


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


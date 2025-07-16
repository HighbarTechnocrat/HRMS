<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="SearchCandidate.aspx.cs" 
    Inherits="procs_SearchCandidate" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
     <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" /> 
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
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
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    
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
                        <asp:Label ID="lblheading" runat="server" Text="Candidate Information"></asp:Label>
                    </span>
                </div>
                 <div>
                        <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                    </div>
             <div>
                <span>
                    <a href="Requisition_Index.aspx" class="aaaa">Recruitment  Home</a>
                </span>
                 </div>

                <div class="edit-contact">
                    <ul id="editform" runat="server">
                          <li>
                           <span>Main Skillset</span>&nbsp;&nbsp;<span style="color: red"></span>
                             <br />
                           <asp:DropDownList runat="server" ID="lstMainSkillset">
                            </asp:DropDownList>
                            </li>
                       

                        <li class="mobile_inboxEmpCode">                            
                            <span >Candidate Name </span>&nbsp;&nbsp;
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_CandidateName" runat="server" MaxLength="50"></asp:TextBox>
                        </li>
                        <li class="mobile_InboxEmpName">                            
                            <span >Candidate Email </span>&nbsp;&nbsp;
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_CandidateEmail" runat="server" MaxLength="50"></asp:TextBox>
                        </li>
						<li>
							<span style="padding-top:20px">Candidate Mobile No </span>&nbsp;&nbsp;
                            <br />
                            
                            <asp:TextBox AutoComplete="off" ID="Txt_Candidatemobile" runat="server" Class="number"  MaxLength="15"></asp:TextBox>
                                </li>
						
                        <li>
                            <span style="padding-top:20px">Additional Skillset </span>&nbsp;&nbsp;
                            <br />
                            
                            <asp:TextBox AutoComplete="off" ID="txt_AdditionalSkillset" runat="server"  MaxLength="50"></asp:TextBox>
                                </li>
						 <li></li>
                          <li>
                                  <span>From- Experience(Years)</span>&nbsp;&nbsp;<br />
                                    <asp:TextBox AutoComplete="off" ID="txtExperienceYearSearchFrom" Class="number" runat="server" MaxLength="5"> </asp:TextBox>
                                   </li>
                                <li>
                                    <span>To- Experience(Years)</span>&nbsp;&nbsp;<br />
                                    <asp:TextBox AutoComplete="off" ID="txtExperienceYearSearchTo" Class="number" runat="server" MaxLength="5"> </asp:TextBox>
                                </li>
                                <li></li>
                               
                                <li>
                                     <span>From- Expected CTC(Lakh)</span>&nbsp;&nbsp;<br />
                                    <asp:TextBox AutoComplete="off" ID="TxtExpectedCTCSerchFrom" Class="number" runat="server" MaxLength="5"> </asp:TextBox>
                                </li>
                                 <li>
                                    <span>To- Expected CTC(Lakh)</span>&nbsp;&nbsp;<br />
                                    <asp:TextBox AutoComplete="off" ID="TxtExpectedCTCSerchTo" Class="number" runat="server" MaxLength="5"> </asp:TextBox>

                                   </li>
                                <li></li>

                         </ul>
                   
                     <asp:HiddenField ID="hdCandidate_ID" runat="server" /> 
                </div>
            </div>
        </div>
    </div>
    <div class="mobile_Savebtndiv">
        <asp:LinkButton ID="mobile_btnSave" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click">Search Candidate</asp:LinkButton>
        <asp:LinkButton ID="mobile_btnBack" runat="server" Text="Clear Search" OnClick="linkClearSearch_Click" ToolTip="Clear Search" class="trvl_Savebtndiv">Clear Search</asp:LinkButton>
        <asp:LinkButton ID="mobile_cancel" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="Savebtnsve"  PostBackUrl="~/procs/RecCreateCandidate.aspx">Create Candidate</asp:LinkButton>
    </div>
    <br />
    <div class="manage_grid" style="width: 100%; height: auto;">
                          <asp:GridView ID="gvSearchCandidateList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="solid" BorderWidth="1px" 
                            DataKeyNames="Candidate_ID"   CellPadding="3" AutoGenerateColumns="False" Width="100%" OnPageIndexChanging ="gvSearchCandidateList_PageIndexChanging"  EditRowStyle-Wrap="false" PageSize="20" AllowPaging="True">
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
                            <asp:BoundField HeaderText="Name"
                                DataField="CandidateName"
                                ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="left"
                                ItemStyle-Width="15%" 
                                ItemStyle-BorderColor="Navy"
                                />

                            <asp:BoundField HeaderText="Email"
                                DataField="CandidateEmail"
                                 ItemStyle-HorizontalAlign="center"                                 
                                ItemStyle-Width="20%" 
                                ItemStyle-BorderColor="Navy"
                                />

                            <asp:BoundField HeaderText="Mobile"
                                DataField="CandidateMobile"
                                 ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="8%" 
                                ItemStyle-BorderColor="Navy"
                                />

                            <asp:BoundField HeaderText="Main Skillset"
                                DataField="ModuleDesc"
                                 ItemStyle-HorizontalAlign="center" 
                                ItemStyle-Width="12%"
                                ItemStyle-BorderColor="Navy"
                                />

                             <asp:BoundField HeaderText="Salary(Lakh)"
                                DataField="Salary"
                                 ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="6%" 
                                ItemStyle-BorderColor="Navy"
                                />
                            
                             <asp:BoundField HeaderText="Experience(Years)"
                                DataField="CandidateExperience_Years"
                                 ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="6%" 
                                ItemStyle-BorderColor="Navy"
                                />
                             <asp:BoundField HeaderText="Blocked By"
                                DataField="BlockBY"
                                 ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="15%" 
                                ItemStyle-BorderColor="Navy"
                                />
                            <asp:BoundField HeaderText="Requisition Number"
                                DataField="RequisitionNumber"
                                 ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="25%" 
                                ItemStyle-BorderColor="Navy"
                                />

                            <asp:TemplateField HeaderText="View" HeaderStyle-Width="20%">
                                <ItemTemplate>
                                <asp:ImageButton id="lnkEdit" runat="server" Width="20px" Height="15px" ImageUrl="~/Images/edit.png"  OnClick="lnkEdit_Click"/>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                       
                </div>

    <br />

     
   
   
    <asp:HiddenField ID="hdnYesNo" runat="server" />

    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" /> 

     <script type="text/javascript">      
        $(document).ready(function () {
            $("#MainContent_lstMainSkillset").select2();
        });
    </script>

     <script type="text/javascript">

        $('.number').keypress(function(event) {
      if ((event.which != 46 || $(this).val().indexOf('.') != -1) &&
        ((event.which < 48 || event.which > 57) &&
          (event.which != 0 && event.which != 8))) {
        event.preventDefault();
      }
      var text = $(this).val();
      if ((text.indexOf('.') != -1) &&
        (text.substring(text.indexOf('.')).length > 2) &&
        (event.which != 0 && event.which != 8) &&
        ($(this)[0].selectionStart >= text.length - 2)) {
        event.preventDefault();
      }
         });

         </script>
    
</asp:Content>


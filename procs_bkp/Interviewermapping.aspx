<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="Interviewermapping.aspx.cs" 
    Inherits="procs_Interviewermapping" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
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
            background: #dae1ed;
        }
    	
		.ViewFiles:hover {
            color: red;			
            background-color: transparent;
            text-decoration: none !important;
        }
	    .Paddinggrid {

             padding-left:20px;
	    }
         .btnSearchScreener
        {
            background: #3D1956;
            color: #febf39 !important;
            padding: 0.5% 1.4%;
            /*padding-top:10px;*/
            margin: 0% 0% 0 0;
            height:15px;
	    }

    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/dist/jquery-3.2.1.min.js"></script>
	<script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>

    <%--<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"></script>--%>
   
    <script src="../js/jquery.sumoselect-3.3.29/jquery.sumoselect.min.js"></script>
    <link href="../js/jquery.sumoselect-3.3.29/sumoselect.css" rel="stylesheet" />
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
                        <asp:Label ID="lblheading" runat="server" Text="Interviewer Mapping Info"></asp:Label>
                    </span>
                </div>
				 <div>
                        <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                    </div>
                <span>
                    <a href="Requisition_Index.aspx" class="aaaa">Recruitment  Home</a>
                     
                </span>
				     <div class="edit-contact">
					<ul id="editform" runat="server" >
                        <li>                        
                            <span >Skillset  </span>&nbsp;&nbsp;<span style="color:red">*</span>
                            <br />
                             <asp:DropDownList runat="server" ID="lstSkillset" CssClass="DropdownListSearch"  AutoPostBack="true" OnSelectedIndexChanged="lstSkillset_SelectedIndexChanged">                               
                            </asp:DropDownList> </li>
                        <li>                        
                            <span >Interview Round  </span>&nbsp;&nbsp;<span style="color:red">*</span>
                            <br />
                             <%--<asp:DropDownList runat="server" ID="DDLinterviewRound" CssClass="DropdownListSearch" SelectionMode="multiple">                               
                            </asp:DropDownList> --%>
                           <%-- <asp:ListBox runat="server"  ID="DDLinterviewRound" CssClass="DropdownListSearch" SelectionMode="multiple"></asp:ListBox>--%>
                            <asp:listbox runat="server" id="DDLinterviewRound"  selectionmode="Multiple">
                            </asp:listbox>
                           <%-- AutoPostBack="true" OnSelectedIndexChanged="DDLinterviewRound_SelectedIndexChanged"--%>
                        </li>
                        

                       <li>                        
                            <span >Employee Name </span>&nbsp;&nbsp;<span style="color:red">*</span>
                            <br />
                             <asp:DropDownList runat="server" ID="lstPositionName" Enabled="false" CssClass="DropdownListSearch">                               
                            </asp:DropDownList> </li> 
				
						<div style=" padding-left:300px">							
							<br />							
                            <asp:LinkButton ID="mobile_btnSave" Text="Add Interviewer" ToolTip="Add Interviewer"  runat="server" OnClick="mobile_btnSave_Click" ></asp:LinkButton>
                       
							</div>
					</ul>
					</div>
                <br />
                <div style="padding-left:40px">
                        <asp:Label runat="server" ID="Label1"  Text="Search Parameter :" Style="font-size: 16px; font-weight: 500;"></asp:Label>
                    </div>
                <br />
              
                 <div class="edit-contact">
					<ul id="Ul1" runat="server" >
                        <li class="trvl_date">                        
                           <%-- <span >Skillset  </span>&nbsp;&nbsp;<span style="color:red"></span>
                            <br />
                             <asp:DropDownList runat="server" ID="DDLSkillSetSearch" CssClass="DropdownListSearch" >                               
                            </asp:DropDownList> --%>
                             <span >Search By Employee Name </span>&nbsp;&nbsp;<span style="color:red"></span>
                            <br />
                             <asp:DropDownList runat="server" ID="DDLEmpNameSearch" CssClass="DropdownListSearch" >                               
                            </asp:DropDownList> 

                        </li>

                       <li class="trvl_date">                        
                           

                       </li> 
				
						<div>							
							<br />							
                            <asp:LinkButton ID="Link_BtnSearch" Text="Search" ToolTip="Search"  CssClass="btnSearchScreener" runat="server"  OnClick="Link_BtnSearch_Click" ></asp:LinkButton>
                        <asp:LinkButton ID="Link_BtnSearchClear" runat="server" Text="Clear Search"  ToolTip="Clear Search" CssClass="btnSearchScreener"  OnClick="Link_BtnSearchClear_Click" >Clear Search</asp:LinkButton>      
       
							</div>
					</ul>
					</div>

                <div class="manage_grid" style="width: 100%; height: auto; padding-top:10px;">
                    <div>
          <asp:Label runat="server" ID="lbltotalRecords"  Style="color: red; font-size: 15px; font-weight: 500; text-align: center;"></asp:Label>
                    </div>
                    <center>
                          <asp:GridView ID="gvMngTravelRqstList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                            DataKeyNames="Interviewer_MappingID"   CellPadding="3" AutoGenerateColumns="False" Width="100%"   EditRowStyle-Wrap="false" PageSize="15" AllowPaging="true"  OnPageIndexChanging="gvMngTravelRqstList_PageIndexChanging" >
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
                            <asp:BoundField HeaderText="Skill Set"
                                DataField="ModuleDesc"
                                ItemStyle-HorizontalAlign="left"
                                 HeaderStyle-HorizontalAlign="Center"
                                 ItemStyle-CssClass="Paddinggrid"
                                ItemStyle-Width="15%" />
                            <asp:BoundField HeaderText="Interview Round"
                                DataField="InterviewRound"
                                ItemStyle-HorizontalAlign="left"
                                 HeaderStyle-HorizontalAlign="Center"
                                 ItemStyle-CssClass="Paddinggrid"
                                ItemStyle-Width="15%" />
                            <asp:BoundField HeaderText="Employee Name"
                                DataField="Emp_Name"
                                ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-CssClass="Paddinggrid"
                                ItemStyle-Width="30%" />
                        </Columns>
                    </asp:GridView>
                    </center>

					<br />
					<br /><br />
                </div>

              
                  
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                       
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" ></asp:LinkButton>
                        </div>
                   <%-- </div>--%>


                    
                </div>
                <asp:HiddenField ID="hdnQuest_ID" runat="server" />
                <asp:HiddenField ID="hdnClaimsID" runat="server" />
               <asp:HiddenField ID="FilePath" runat="server" />
            </div>
        </div>
    </div>


    
	<script src="../js/dist/select2.min.js"></script>
	<link href="../js/dist/select2.min.css" rel="stylesheet" />
     
	<script type="text/javascript">  

		$(document).ready(function () {			
			$(".DropdownListSearch").select2();		
		});
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

    <script src="../js/jquery.sumoselect-3.3.29/jquery.sumoselect.min.js"></script>
    <link href="../js/jquery.sumoselect-3.3.29/sumoselect.css" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {
            $(<%=DDLinterviewRound.ClientID%>).SumoSelect({ selectAll: true });
        });
    </script>

</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="BenchListReport.aspx.cs" 
    Inherits="procs_BenchListReport" EnableEventValidation="false" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>
<%@ Register TagName="calender" TagPrefix="ucical" Src="~/themes/creative1.0/LayoutControls/leaveCalender.ascx" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


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
        #content-container #gvMain {
            width: 215% !important;
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

         .noresize {
            resize: none;
        }     

    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("hccurlmain")%>/js/gridviewscroll.js"></script>
    <link rel="stylesheet" href="<%=ReturnUrl("css")%>highbar/web.css" type="text/css" media="all" />
     
    
    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Bench List"></asp:Label>
                    </span>
                  
                    <%--PostBackUrl="~/procs/Index.aspx"--%>
                </div>
                <div>
                      <span>
                     <a href="ReportsMenu.aspx" class="aaaa" >Reports Menu</a>
                </span>
                </div>
             
                <div class="edit-contact">
                   
                    <ul id="editform" runat="server" visible="true">

                        <li>
                            <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                            <%--<asp:TextBox runat="server" ID="lblmessage1" Visible="False" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;" OnTextChanged="lblmessage1_TextChanged"></asp:TextBox>--%>
                        </li>
                        <li></li>
                    
                      
                        <li class="leavedays" runat="server" visible="false" id="li1" >
                          
                        </li>
                        <li>
                            <br /><br />
                           <%--<br /><br />
                            <asp:LinkButton ID="btnIn" runat="server" Visible="true" CssClass="Savebtnsve" Text="Generate Report" 
                                OnClick="btnIn_Click" >Generate Report</asp:LinkButton>--%>
                        </li>
                        <li></li>
                        
                        
                    </ul>
                </div>
                <div style="width: 100%; overflow: auto; padding-bottom:50px">

                    <rsweb:reportviewer id="ReportViewer2" runat="server" height="300px"
                        width="80%" showbackbutton="False" sizetoreportcontent="True"
                        showcredentialprompts="False" showdocumentmapbutton="False"
                        showpagenavigationcontrols="true" showfindcontrols="false" showexportcontrols="true" showrefreshbutton="False" pagecountmode="Actual">

        </rsweb:reportviewer>
                </div>
            </div>
        </div>
    </div>
  
    
    <asp:HiddenField ID="hflEmpName" runat="server" />
    <asp:HiddenField ID="hflEmpDesignation" runat="server" />

    
</asp:Content>


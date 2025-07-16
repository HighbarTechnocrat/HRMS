<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="KRA_DeemedApproval.aspx.cs" Inherits="procs_KRA_DeemedApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" /> 
    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
            /*background: #dae1ed;*/
            background: #ebebe4;
        }

        .taskparentclasskkk {
            width: 29.5%;
            height: 72px;
            /*overflow:initial;*/
        } 

        .Dropdown {
            border-bottom: 2px solid #cccccc;
            /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
            background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;
            cursor: default;
        }

        .textboxBackColor {
            background-color: cadetblue;
            color: aliceblue;
        } 

        .BtnShow {
            color: blue !important;
            background-color: transparent;
            text-decoration: none;
            font-size: 13px !important;
        }

            .BtnShow:visited {
                color: blue !important;
                background-color: transparent;
                text-decoration: none;
            }

            .BtnShow:hover {
                color: red !important;
                background-color: transparent;
                text-decoration: none !important;
            }

        a#MainContent_btnback_mng {
            margin: 25px 0 0 0 !important;
        }

         .LableName {
            color: #F28820;
            font-size: 16px;
            font-weight: normal;
            text-align: left;
        }
         	  

        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
            background: #dae1ed;
        }

        .Dropdown {
            border-bottom: 2px solid #cccccc;
            /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
            background: url('../images/arrowdown.png') no-repeat right center;
            cursor: default;
        }
        input#ctl00_MainContent_ReportViewer1_ctl05_ctl03_ctl00 {
    display: none;
}
        
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">



    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
    <script src="../js/HtmlControl/jHtmlArea-0.8.min.js"></script>
    <link href="../js/HtmlControl/jHtmlArea.css" rel="stylesheet" />

    <div id="loader" class="myLoader" style="display:none">
        <div class="loaderContent">
			<span class="DONot">Please  Do Not Refresh  or Close Page</span>
			<img src="../images/loader.gif" ></div>
		
    </div>

    <div class="commpagesdiv" id="divHolder">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Deemed Approval Automation"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>
                <div>
                    <span>
                        <a href="KRA_index.aspx" class="aaaa">KRA Home</a> 
                    </span>
                </div>
                <div class="edit-contact">

                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" Visible="false"></asp:LinkButton>
                        </div>
                    </div> 
                    <ul id="editform" runat="server" visible="true" style="padding-top:50px">
                        
 
                       <li class="trvl_date">
                                    <span>Select Period &nbsp;<span style="color: red">*</span></span><br />
                                     <asp:DropDownList runat="server" ID="lstPeriod" CssClass="DropdownListSearch" AutoPostBack="true" OnSelectedIndexChanged="lstPeriod_SelectedIndexChanged" ></asp:DropDownList>
                                </li>
                                <li class="trvl_date">
                                    <span>From Date</span><br />
                                    <asp:TextBox AutoComplete="off" ID="Txt_FromDateKRA" Enabled="false" MaxLength="100" runat="server"></asp:TextBox>
                                </li>
                                  <li>
                                    <span>To Date</span><br />
                                      <asp:TextBox AutoComplete="off" ID="Txt_ToDateKRA" Enabled="false" MaxLength="100" runat="server"></asp:TextBox>
                                </li> 
                                <li class="trvl_date"></li> 

                        <li class="trvl_local">
                            <asp:LinkButton ID="btnback_mng" runat="server" Text="Submit" ToolTip="Submit" OnClientClick="StartLoader()" Enabled="false" Visible="false" CssClass="Savebtnsve" OnClick="btnback_mng_Click">Submit</asp:LinkButton>                            
                             </li>
                        <li class="trvl_local">                             
                        </li>
                        <li class="trvl_local"></li>

                    </ul>
                </div>
            </div>
        </div>
    </div>
   <br /><br />
     <div style="width: 100%; overflow: auto">
       
    </div>
    

    <asp:HiddenField ID="hdnloginempcode" runat="server" />

 
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <link href="../includes/loader.css" rel="stylesheet" />

    <script type="text/javascript">
  

        $(document).ready(function () {
            $(".DropdownListSearch").select2(); 
        }); 

        function StartLoader() {
			
			$('#loader').show();
		}
		function StopLoader() {
				$('#loader').hide();
		}

    </script>

   


    
</asp:Content>


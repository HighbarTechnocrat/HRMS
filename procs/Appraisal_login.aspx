<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="Appraisal_login.aspx.cs" 
    Inherits="procs_Appraisal_login" EnableViewState="true" ValidateRequest="false"  MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <%--<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <%-- <link href="<%=ReturnUrl("css") %>calendar/calendar.css" rel="stylesheet" type="text/css" />
    <link href="sampleform_css.css" rel="stylesheet" type="text/css" />--%>
    <%--<ajaxToolkit:ToolkitScriptManager ID="srcp" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
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

        #MainContent_lstApprover, #MainContent_lstIntermediate {
            padding: 0 0 33px 0 !important;
            /*overflow: unset;*/
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
            text-align:right;
        }

        .textboxAlignAmount {
           
            text-align:right;
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

        .POWOContentTextArea {
            width: 783px !important;
            height: 400px !important;
            overflow: auto;
        }

        .LableName {
            color: #F28820;
            font-size: 16px;
            font-weight: normal;
            text-align: left;
        }

        .boxTest {
            BORDER-RIGHT: black 1px solid;
            BORDER-TOP: black 1px solid;
            BORDER-LEFT: black 1px solid;
            BORDER-BOTTOM: black 1px solid;
            BACKGROUND-COLOR: White;
        }

        .GoalDecriptionTextArea {
            width: 722px !important;
            height: 250px !important;
        }
        .txtRemarks {
            width: 623px;
            height: 77px;
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


    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>

    <script src="../js/HtmlControl/jHtmlArea-0.8.min.js"></script>
    <link href="../js/HtmlControl/jHtmlArea.css" rel="stylesheet" />



 <asp:TextBox   ID="TextBox1" runat="server" MaxLength="50" TextMode="Password" Style="width: 1px !important;margin: 0 0 0 -375px !important;"></asp:TextBox>
    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Appraisal Module"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage"  Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>
                <div>
                    <span>
                        <%--  <a href="PersonalDocuments.aspx" class="aaaa">My Corner</a>--%>
                    </span>
                </div>
                <div class="edit-contact"> 
                     <div class="divSessionExpire" id="divClick" runat="server" visible="false">
                                <a href="https://ess.highbartech.com/hrms/procs/Appraisal_login.aspx" class="sessionExpire"> <u>Please click here for PMS Access</u> </a>
                     </div>

                    <ul id="editform" runat="server" visible="true">                          
                        <li class="trvl_date" id="li_PMS_Pass_1" runat="server"></li>
                        <li class="trvl_date" id="li_PMS_Pass_2" runat="server">
                            <span>Enter PMS Access Secret Key</span> &nbsp;&nbsp;
                            <asp:TextBox  ID="txtAccess_Password" runat="server" MaxLength="50" TextMode="Password"></asp:TextBox>
                        </li>
                        <li class="trvl_date" id="li_PMS_Pass_3" runat="server"></li>
                        
                        <li class="trvl_date" id="li_Create_PMS_Pass_1" runat="server"></li>
                        <li class="trvl_date" id="li_Create_PMS_Pass_2" runat="server">
                            <span>Create PMS Access Secret Key</span> &nbsp;&nbsp;
                            <asp:TextBox  ID="txtCreate_Password" runat="server" MaxLength="50" TextMode="Password"></asp:TextBox>
                        </li>
                        <li class="trvl_date" id="li_Create_PMS_Pass_3" runat="server"></li> 

                        <li class="trvl_date" id="li_Change_PMS_Key_old_1" runat="server" visible="false"></li>
                        <li class="trvl_date" id="li_Change_PMS_Key_old_2" runat="server" visible="false">
                            <span>Enter Old PMS Access Secret Key</span> &nbsp;&nbsp;
                            <asp:TextBox   ID="txt_Old_Passowrd" runat="server" MaxLength="50" TextMode="Password"></asp:TextBox>
                        </li>
                        <li class="trvl_date" id="li_Change_PMS_Key_old_3" runat="server" visible="false"></li> 

                        <li class="trvl_date" id="li_Change_PMS_Key_1" runat="server" visible="false"></li>
                        <li class="trvl_date" id="li_Change_PMS_Key_2" runat="server" visible="false">
                            <span>Change PMS Access Secret Key</span> &nbsp;&nbsp;
                            <asp:TextBox   ID="txtChange_Password" runat="server" MaxLength="50" TextMode="Password"></asp:TextBox>
                        </li>
                        <li class="trvl_date" id="li_Change_PMS_Key_3" runat="server" visible="false"></li> 
                     </ul>
                           
                  </div>

                </div>
            </div>
        </div>
   
    <div class="trvl_Savebtndiv" style="margin:0 396px 0 0 !important">
        <asp:LinkButton ID="trvl_btnSave" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();" OnClick="trvl_btnSave_Click">Submit</asp:LinkButton>
       
        <asp:LinkButton ID="btnback_mng" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" Visible="false" >Back</asp:LinkButton>  
        <br /> 
        &nbsp;&nbsp;&nbsp;  &nbsp;&nbsp;&nbsp;  &nbsp;&nbsp;&nbsp;  &nbsp;&nbsp;&nbsp;    &nbsp;&nbsp;&nbsp;
         <a href="Appraisal_login.aspx?p=c" runat="server" class="BtnShow" id="btn_change_PMS_Key" visible="false">Change PMS Acess Key</a> 
         <br /><br />
          &nbsp;&nbsp;&nbsp;  &nbsp;&nbsp;&nbsp;  &nbsp;&nbsp;&nbsp;  &nbsp;&nbsp;&nbsp;    &nbsp;&nbsp;&nbsp;
        <a href="Appraisal_login.aspx?p=f" runat="server" class="BtnShow" id="btn_forgot_PMS_Key">I forgot PMS Acess Key</a> 
         <br /> <br />
   </div>
  

    <asp:HiddenField ID="hdnEmpCode" runat="server" />
    <asp:HiddenField ID="hdnCondintionId" runat="server" />
    <asp:HiddenField ID="hdVendorID" runat="server" />
    <asp:HiddenField ID="HDVendorCode" runat="server" />
    <asp:HiddenField ID="hdnShortClose_Cancelled" runat="server" />
    <asp:HiddenField ID="hdnIsShMilestoneClick" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="FilePath" runat="server" />
    <asp:HiddenField ID="hdnisForgotPMSKey" runat="server" />
     <asp:HiddenField ID="hdnEmpEmail" runat="server" />
    <asp:HiddenField ID="hdnEmpName" runat="server" />
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <script type="text/javascript">

        $(document).ready(function () {
            $(".DropdownListSearch").select2();
            $("#MainContent_txtPOWO_Content").htmlarea();
            $("#MainContent_txt_POWOContent_description").htmlarea();

            $('#MainContent_gvMngTravelRqstList').gridviewScroll({
                width: 1070,
                height: 600,
                freezesize: 4, // Freeze Number of Columns.
                headerrowcount: 1, //Freeze Number of Rows with Header.
			});
        });


        function SaveMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=trvl_btnSave.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        Confirm();
                        
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }

        

         function Confirm() {
          
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Submit ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            //alert(confirm_value.value);
            //document.forms[0].appendChild(confirm_value);
            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return;

        }

        


         function onCharOnlyNumber(e) {
            var keynum;
            var keychar;
            var numcheck = /[0123456789]/;

            if (window.event) {
                keynum = e.keyCode;
            }
            else if (e.which) {
                keynum = e.which;
            }
            keychar = String.fromCharCode(keynum);
            return numcheck.test(keychar);
        }

       function checkDate(sender,args)
         {
           if (sender._selectedDate < new Date())
            {
                alert("You cannot select a day earlier than today!");
                sender._selectedDate = new Date(); 
                // set the date back to the current date
               sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
          }


    </script>
</asp:Content>




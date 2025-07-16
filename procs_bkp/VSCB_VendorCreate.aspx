<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="VSCB_VendorCreate.aspx.cs" 
    Inherits="procs_VSCB_VendorCreate" EnableViewState="true" ValidateRequest="false"  MaintainScrollPositionOnPostback="true" %>

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




    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Vendor Create / Update"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>
                <div>
                    <span>
                          <a href="vscb_index.aspx" class="aaaa"><%=System.Configuration.ConfigurationManager.AppSettings["VSCBPageTitle"]%></a>
                    </span>
                </div>
                <div class="edit-contact">

                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" Visible="false"></asp:LinkButton>
                        </div>
                    </div>

                    <ul id="editform" runat="server" visible="true">
                        <li class="trvl_type">
                            <span>Vendor Name</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_VendorName" runat="server"  MaxLength="150"
                                ></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>Vendor Code</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtVendorCode" runat="server" MaxLength="15"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>GSTIN No. </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtGSTINNo" runat="server" MaxLength="45"></asp:TextBox>
                        </li>


                        <li class="trvl_date">
                               <span>Account No.</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtAccountNo" runat="server" MaxLength="45"></asp:TextBox>
                            </li>
                            <li class="trvl_date">

                            <span>IFSC Code</span> &nbsp;&nbsp;<span style="color: red">*</span>
                            <asp:TextBox AutoComplete="off" ID="txtIFSCCode" runat="server" MaxLength="15"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>Bank Name</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                            <asp:TextBox AutoComplete="off" ID="TxtBankName" runat="server" MaxLength="150"></asp:TextBox>
                        </li>

                         <li class="trvl_date">
                          <span>Vendor Email Address</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtEmailAddress" runat="server"  MaxLength="100"></asp:TextBox>
                                
                         </li>
                         <li class="trvl_date">
                                <span id="uploadfileid" runat="server">Upload File</span>&nbsp;&nbsp;<span style="color:red" id="uploadfileidd" runat="server">*</span>
                            <asp:FileUpload ID="VendorUploadfile" runat="server"  Width="200"  AllowMultiple="false"></asp:FileUpload>
                           
                         </li>
                         <li class="trvl_date">
                          <asp:LinkButton ID="lnkfile_Vendor" runat="server" OnClientClick="DownloadFile_S()" Visible="false" CssClass="BtnShow"></asp:LinkButton>
                      
                         </li>
                        
                        <li class="trvl_date">
                            <span>Vendor Address</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <span style="color: red; font-size: 10px; font-weight: normal; font-style: italic;"> Maximum 300 Characters</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="TxtVendorAddress" runat="server" MaxLength="400" TextMode="MultiLine" Height="80px"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                          
                         </li>
                         <li class="trvl_date">
                         </li>

                        <li class="trvl_date">
                            <asp:CheckBox ID="chkISActive" runat="server"  Text="Vendor Is Active" Checked="true"/>
                        </li>
                        <li class="trvl_date">
                             
                         </li>
                        <li class="trvl_date">
                        
                        </li>

                        <li class="trvl_date">
                       </li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>

                            </ul>
                           
                        </div>

                </div>
            </div>
        </div>
   
    <div class="trvl_Savebtndiv">
        <asp:LinkButton ID="trvl_btnSave" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();" OnClick="trvl_btnSave_Click">Submit</asp:LinkButton>
       
        <asp:LinkButton ID="btnback_mng" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="btnback_mng_Click">Back</asp:LinkButton>
        
        <%--<asp:LinkButton ID="btnCorrection" Visible="false" runat="server" Text="Download PO/ WO" ToolTip="Download PO/ WO" CssClass="Savebtnsve" OnClick="btnCorrection_Click">Download PO/ WO</asp:LinkButton>
         <asp:LinkButton ID="btnApprove"  runat="server" Text="View Draft Copy" ToolTip="View Draft Copy" Visible="false" CssClass="Savebtnsve" OnClick="btnApprove_Click" >View Draft Copy</asp:LinkButton>
        <asp:LinkButton ID="btnReject"  runat="server" Visible="false" Text="View Draft Copy" ToolTip="View Draft Copy"  CssClass="Savebtnsve"  OnClick="btnReject_Click" >View Draft Copy</asp:LinkButton>
   --%>
        </div>

    
    <asp:HiddenField ID="hdnCondintionId" runat="server" />
    <asp:HiddenField ID="hdVendorID" runat="server" />
    <asp:HiddenField ID="HDVendorCode" runat="server" />
    <asp:HiddenField ID="hdnShortClose_Cancelled" runat="server" />
    <asp:HiddenField ID="hdnIsShMilestoneClick" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="FilePath" runat="server" />

    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <script type="text/javascript">

        $(document).ready(function () {
            $(".DropdownListSearch").select2();
            $("#MainContent_txtPOWO_Content").htmlarea();
            $("#MainContent_txt_POWOContent_description").htmlarea();
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

        function checkEmail() {  
            
            var email = document.getElementById('<%=txtEmailAddress.ClientID%>');
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/; 
            if (email.value != '')
            {
             if (!filter.test(email.value))
            {  
            alert('Please provide a valid email address');  
            email.focus;  
            return false;  
           } 
            }
             
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

         function DownloadFile_S() {
            
             var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;
             var localFileName = document.getElementById("<%=lnkfile_Vendor.ClientID%>").innerText;

             //alert(localFilePath);
           //  alert(localFileName);

           // window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + localFileName);
            //window.open("https://192.168.21.193/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
           window.open("http://localhost/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + localFileName);
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


    </script>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" ValidateRequest="false"
    CodeFile="Appreciation_Letter.aspx.cs" Inherits="Appreciation_Letter" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />
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

        #MainContent_lstApprover {
            overflow: hidden !important;
        }

        .Savebtn {
            background: #3D1956;
            color: #febf39 !important;
            padding: 9px 7px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
   <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/HtmlControl/jquery-1.3.2.js"></script>
    <script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="../js/HtmlControl/jHtmlArea-0.8.min.js"></script>
    <link href="../js/HtmlControl/jHtmlArea.css" rel="stylesheet" />
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>


    <div class="userposts">
        <span>
            <br />
            <asp:Label ID="lblheading" runat="server" Text="Send Appreciation Letter"></asp:Label>
        </span>
    </div>
    
    <div class="leavegrid">
         <br />
        <a href="http://localhost//hrms/procs/Appreciation_Letter_index.aspx" class="aaa">Appreciation Index</a>
    </div>

    <div style="width: 100%; overflow: auto; align-content: flex-start">
        <div class="editprofileform">
            <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
        </div>
    </div>

    <div id="divtext" runat="server"></div>
    <div class="edit-contact">
        <ul>
            <li>
                <span>Select Employee Name</span>&nbsp;&nbsp;<span style="color: red">*</span>
                <br />
                <asp:DropDownList ID="ddl_empname" AutoPostBack="false" CssClass="DropdownListSearch" runat="server">
                </asp:DropDownList>
            </li>

            <li>
                <span id="status" runat="server">Select Appreciation Letter</span>&nbsp;&nbsp;<span style="color: red">*</span>
                <br />
                <asp:DropDownList ID="emp_status" CssClass="DropdownListSearch" runat="server" AutoPostBack="true" OnSelectedIndexChanged="emp_status_SelectedIndexChanged">
                </asp:DropDownList>

            </li>
        
            <li id="ddl_letters" runat="server" visible="false" class="leavedays">
                <br />
                <span>Select Letter</span>&nbsp;&nbsp;<span style="color: red">*</span>
                <br />
                <asp:DropDownList ID="ddl_cardlist" AutoPostBack="true" CssClass="DropdownListSearch" runat="server" OnSelectedIndexChanged="ddl_card_image">
                </asp:DropDownList>
                <br />
            </li>

            <li></li> 
            <li class="leavedays" visible="false" id="liSubject" runat="server">
                 <br />
                <span id="txt_lettere_sub" runat="server">Letter Subject</span> &nbsp;&nbsp;<span style="color: red">*</span>
            <span id="txt_char" runat="server" style="color: red; font-size: 10px; font-weight: normal; font-style: italic;">Maximum 150 Characters</span>
            <br />   
                <asp:TextBox ID="txt_sub" AutoComplete="off" runat="server" TextMode="MultiLine" Width="730px" Height="50px" ></asp:TextBox> 
            </li>
        </ul> 
        <ul> 
            <asp:LinkButton ID="mobile_btnSave" Visible="false" runat="server" Text="Send" ToolTip="Send" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click" OnClientClick="return SaveMultiClick();" ValidationGroup="SubmitGroup">Send</asp:LinkButton>
          <li></li><li></li>
           <li> 
               <br />
                <div id="Appreciation_Draft" runat="server" visible="false">
                    <span id="txt_draft" runat="server" class="LableName">Appreciation Draft</span>  &nbsp;&nbsp; 
                    <textarea  id="ddl_letter" runat="server" style="height:500px;width:740px"    ></textarea>

                </div></li>

           </ul>
        

</div>

        <asp:Image ID="imgCard" runat="server" Width="500px" Height="500px" Visible="false" />
      
    
        <asp:Label ID="ddl_emp_names" Style="display: none" runat="server"></asp:Label>

        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:HiddenField ID="hdnEmpCode" runat="server" />

        <asp:HiddenField ID="hflEmpDesignation" runat="server" />
        <asp:HiddenField ID="hflEmpDepartment" runat="server" />
        <asp:HiddenField ID="hflEmailAddress" runat="server" />
        <asp:HiddenField ID="hdnBankDetail_ID" runat="server" />
        <asp:HiddenField ID="hdnGrade" runat="server" />
        <asp:HiddenField ID="hdnYesNo" runat="server" />
        <asp:HiddenField ID="hdnFilter" runat="server" />

        <script src="../js/dist/select2.min.js"></script>
        <link href="../js/dist/select2.min.css" rel="stylesheet" />
        <script type="text/javascript">      
            $(document).ready(function () {

                $("#MainContent_ddl_letter").htmlarea();
                $(".DropdownListSearch").select2();

            });
         
            function updateImage(newSrc) {
                var img = document.getElementById("myImage"); // Find the image by its ID
                if (img) {
                    img.src = newSrc; // Update the src attribute
                    img.style.width = "300px"; // Apply a width
                    img.style.height = "50px"; // Apply a height
                    img.style.border = "2px solid black"; // Add a border
                    img.style.borderRadius = "10px"; // Add rounded corners
                    img.style.boxShadow = "5px 5px 15px rgba(0,0,0,0.3)"; // Add a shadow
                }
            }
             

            function SaveMultiClick() {
                try {
                    var retunboolean = true;
                    var ele = document.getElementById('<%=mobile_btnSave.ClientID%>');

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
                //Testing();
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


        </script>
</asp:Content>


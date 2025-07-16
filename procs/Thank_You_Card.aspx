<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="Thank_You_Card.aspx.cs" Inherits="Thank_You_Card" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leaveteamrpt.css" type="text/css" media="all" />
    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }

        .edit-contact input:focus {
            border-bottom: 2px solid rgb(51, 142, 201) !important;
        }

        .edit-contact input {
            padding-left: 30px !important;
            width: 83%;
        }

        .edit-contact > ul {
            padding: 0;
        }

        .taskparentclasskkk {
            width: 29.5%;
            height: 72px;
            /*overflow:initial;*/
        }
        textarea#MainContent_txt_remakrs {
            width: 597px !important;
            height: 50px !important;
        }

    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="Server">


    <div class="userposts">
        <span>
            <br />
            <asp:Label ID="lblheading" runat="server" Text="Thank You Card"></asp:Label>
        </span>
    </div>

    <div class="leavegrid">
        <a href="https://ess.highbartech.com/hrms/procs/ThankyouCard_Index.aspx" class="aaa">ThankyouCard</a>
    </div>

   

    <div style="width: 100%; overflow: auto; align-content: flex-start">
        <div class="editprofileform">
            <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
        </div>
    </div>

    <div class="edit-contact">
        <ul>
            <li>
                <span>Select Employee Name</span>&nbsp;&nbsp;<span style="color: red">*</span>
                <br />
                <asp:DropDownList ID="ddl_empname" AutoPostBack="false" CssClass="DropdownListSearch" runat="server">
                </asp:DropDownList>
            </li>

            <li>
                <span>Select Thank You Card</span>&nbsp;&nbsp;<span style="color: red">*</span>
                <br />
                <asp:DropDownList ID="ddl_cardlist" AutoPostBack="true" CssClass="DropdownListSearch" runat="server" OnSelectedIndexChanged="ddl_card_image">
                </asp:DropDownList>
            </li>

            <li>
                <div class="mobile_Savebtndiv">
                    <asp:LinkButton ID="mobile_btnSave" runat="server" Text="Send" ToolTip="Send" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click" OnClientClick="return SaveMultiClick();">Send</asp:LinkButton>
                    <br />
                </div>
            </li> 
               <li class="trvl_local">
                    <br />  <br /
                            <span>Thank You Note</span>
                   <asp:TextBox AutoComplete="off" ID="txt_remakrs" runat="server" TextMode="MultiLine" MaxLength="200" ></asp:TextBox>
              </li>

        </ul>
        <div>
            <asp:Image ID="imgCard" runat="server" />
            <%--Width="300px" Height="300px"--%>
        </div>

        <br />
        <br />

    </div>


    <asp:HiddenField ID="hdnYesNo" runat="server" />

    <script src="../js/dist/jquery-3.2.1.min.js"></script>

    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />


    <script type="text/javascript"> 
 

         $(document).ready(function () {
             $(".DropdownListSearch").select2();
         });


         function noanyCharecters(e) {
             var keynum;
             var keychar;
             var numcheck = /[]/;

             if (window.event) {
                 keynum = e.keyCode;
             }
             else if (e.which) {
                 keynum = e.which;
             }
             keychar = String.fromCharCode(keynum);
             return numcheck.test(keychar);
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
            
             document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
             return;

         }

    </script>

</asp:Content>



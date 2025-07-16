<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="KRA_goalView.aspx.cs" Inherits="KRA_goalView" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">


    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
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
        .Measuremntdtls {
            width: 492px !important;
            height: 98px !important;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>

    <script src="../js/freeze/jquery-1.11.0.min.js"></script>
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">

                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Goal and Measurement View"></asp:Label>
                    </span>
                </div>

                <span>
                    <a href="KRA_Index.aspx" class="aaaa">KRA Home</a>
                </span>

                <span>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                </span>
                <div class="edit-contact">

                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" OnClick="lnkcont_Click"></asp:LinkButton>
                        </div>
                    </div>


                    <ul id="editform" runat="server" visible="true">
                        <li class="trvl_type">
                            <span>Goal Title</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_goal_title" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_type">
                            <span>Weightage</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_Weightage" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_local"></li>

                        <li class="trvl_local">
                            <span>Goal Description</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_goal_description" runat="server" TextMode="MultiLine" ReadOnly="true" Enabled="False" CssClass="Measuremntdtls"></asp:TextBox>
                        </li>
                        <li class="trvl_date"></li>
                        <li class="trvl_local"></li>


                        <li class="trvl_local">
                            <span>Measurement Details</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_Measurement_dtls" runat="server" TextMode="MultiLine" ReadOnly="true" Enabled="False" CssClass="Measuremntdtls"></asp:TextBox>
                        </li>
                        <li class="trvl_date"></li>
                        <li class="trvl_local"></li>


                        <li class="trvl_local">
                            <span>Unit</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_unit" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>Quantity</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_Mqty" runat="server" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_local"></li>

                        <li class="trvl_date">
                            <span>Remakrs</span><br />
                            <asp:TextBox AutoComplete="off" ID="txt_remakrs" runat="server" TextMode="MultiLine" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>

                          
                    </ul>

                      <div>
                                 
                          &nbsp;&nbsp;&nbsp;
                          <asp:LinkButton ID="trvldeatils_cancel_btn" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="trvldeatils_cancel_btn_Click">Back</asp:LinkButton>
                     </div>
                    <br /><br />

                </div>
                <asp:HiddenField ID="hdnKRA_ID" runat="server" />
                <asp:HiddenField ID="hdnGoal_ID" runat="server" /> 
                <asp:HiddenField ID="hdnEmpCode" runat="server" />

            </div>
        </div>
    </div>



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


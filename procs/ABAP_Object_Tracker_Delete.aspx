<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="ABAP_Object_Tracker_Delete.aspx.cs" Inherits="ABAP_Object_Tracker_Delete" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <style>
        #MainContent_lnkRemoveABAPObject {
            float: left;
            background: #3D1956;
            color: #febf39 !important;
        }

        .submitbtn {
            border-radius: 0px;
        }

        #content-container .box {
            padding: 50px !important;
        }
    </style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-loader">
        <div class="loader"></div>
    </div>
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
                        <asp:Label ID="lblheading" runat="server" Text="Remove ABAP Object"></asp:Label>
                    </span>
                </div>
                <div runat="server">
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
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

                    <ul id="editform1" runat="server">
                        <li class="trvl_date">

                            <span>Project / Location</span><br />
                            <asp:DropDownList runat="server" ID="DDLProjectLocation" AutoPostBack="True" CssClass="DropdownListSearch" Style="width: 200px;"></asp:DropDownList>



                        </li>
                        <li class="trvl_date">
                            <asp:LinkButton ID="lnkRemoveABAPObject" Text="Remove ABAP Object" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkRemoveABAPObject_Click"></asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </div>
        </div>

        <script src="../js/freeze/jquery-ui.min.js"></script>
        <script src="../js/freeze/gridviewScroll.min.js"></script>

        <script type="text/javascript">

            $(document).ready(function () {
                $(".DropdownListSearch").select2();

                $('#MainContent_gvMngTravelRqstList').gridviewScroll({
                    width: 1070,
                    height: 600,
                    freezesize: 5, // Freeze Number of Columns.
                    headerrowcount: 1, //Freeze Number of Rows with Header.
                });

            });

            document.onreadystatechange = function () {
                if (document.readyState === "complete") {
                    document.getElementById("page-loader").style.display = "none";
                }
            };

            // For async postbacks
            Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(function () {
                document.getElementById("page-loader").style.display = "flex";
            });

            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                document.getElementById("page-loader").style.display = "none";
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
</asp:Content>


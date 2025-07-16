<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="IPAddressCheck.aspx.cs" Inherits="IPAddressCheck" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2" Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />

    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
            background: #ebebe4;
        }

        #MainContent_lstApprover {
            overflow: hidden !important;
        }

        .edit-contact input {
            padding-left: 0px !important;
        }

        #MainContent_View_Reprt {
            background: #3D1956;
            color: #febf39 !important;
            padding: 0.5% 1.4%;
            margin: 0% 0% 0 0;
        }

        .noresize {
            resize: none;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
           // alert("d");
            //function onSuccessCallback(wifi) {
            //    alert("Status: " + wifi.status + "    SSID: " + wifi.ssid
            //        + "\nIP Address: " + wifi.ipAddress + "\nIPV6 Address: " +
            //        wifi.ipv6Address + "    Signal Strength: " + wifi.signalStrength);
            //}

            //function onErrorCallback(error) {
            //    alert("Not supported: " + error.message);
            //}
            //tizen.systeminfo.getPropertyValue("WIFI_NETWORK", onSuccessCallback, onErrorCallback);
        });
    </script>
    <div class="commpagesdiv">
        <div class="commonpages">

            <div class="userposts">
                <span>
                    <asp:Label ID="lblheading" runat="server" Text="IP Address Check"></asp:Label>
                </span>
            </div>
            <div>
                <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
            </div>
            <span runat="server" id="backToSPOC" visible="false">
                <a href="InboxServiceRequest.aspx" class="aaaa">Back</a>
            </span>
            <span runat="server" id="backToEmployee" visible="false">
                <a href="MyService_Req.aspx" class="aaaa">Back</a>
            </span>
            <span runat="server" id="backToArr" visible="false">
                <a href="InboxServiceRequest_Arch.aspx" class="aaaa">Back</a>
            </span>
            <span>
                <a href="TaskMonitoring.aspx" style="margin-right: 18px;" class="aaaa">IP Address Check</a>&nbsp;&nbsp; 
            </span>
            <br />
            <br />
            <br />

            <ul id="editform" runat="server" visible="false">
               
                <li>
                    <span>IP Address Information</span>&nbsp;&nbsp;<br />
                    <br />
                    <asp:Label runat="server" ID="Label2" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                    <asp:TextBox ID="TextBox1" Enabled="false" CssClass="noresize" TextMode="MultiLine" Rows="8" AutoComplete="off" runat="server" MaxLength="100" AutoCompleteType="Disabled"></asp:TextBox>
                </li>
                <li></li>
                <li><br /></li>
                <li><br /></li>
                <li><br /></li>
                <li></li>
                <li></li>
               
            </ul>

        </div>

    </div>
    <div class="mobile_Savebtndiv">
    </div>

    <asp:HiddenField ID="hdnvouno" runat="server" />
    <asp:HiddenField ID="hdnIsMarried" runat="server" />
    <asp:HiddenField ID="hdnTaskRefID" runat="server" />
    <asp:HiddenField ID="hdnAttendeeID" runat="server" />
    <asp:HiddenField ID="hdnTaskID" runat="server" />
    <asp:HiddenField ID="hdnCertificationDetailID" runat="server" />
    <asp:HiddenField ID="hdnProjectDetailID" runat="server" />
    <asp:HiddenField ID="hdnDomainDetailID" runat="server" />
    <asp:HiddenField ID="hdnFileDetailID" runat="server" />
    <asp:HiddenField ID="hdnFileName" runat="server" />
    <asp:HiddenField ID="hdnFilePath" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="hdnempcode" runat="server" />


    <asp:HiddenField ID="FilePath" runat="server" />
</asp:Content>

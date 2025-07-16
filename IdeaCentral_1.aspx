<%@ Page Title="" Language="C#" MasterPageFile="~/cms.master" AutoEventWireup="true" CodeFile="ideacentral.aspx.cs" Inherits="ideacentral" %>

<%@ Register Src="~/control/eventjs.ascx" TagName="js" TagPrefix="ucjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <link href="<%=ReturnUrl("css") %>cupertino/jquery-ui-1.10.3.min.css" rel="stylesheet" type="text/css" />
    <link href="<%=ReturnUrl("css") %>cupertino/fullcalendar.css" rel="stylesheet" type="text/css" />
    <link href="<%=ReturnUrl("css") %>jquery.qtip-2.2.0.css" rel="stylesheet" type="text/css" />
    <style>
    </style>
    <div id="calendar">
    </div>
   
    <div id="addDialog" style="margin: 500px;" title="Add Idea">
        <table cellpadding="0" class="style1 addevent">
            <tr>
                <td class="alignRight">
                    <%--sagar change name to subject in below line 4jan2018--%>
                    <font color="red">*</font>Subject : </td>
                <td class="alignLeft">
                    <input id="addEventName" type="text" /><br />
                </td>
            </tr>
            <tr>
                <td class="alignRight">
                    <font color="red">*</font>Description:</td>
                <td class="alignLeft">
                    <textarea id="addEventDesc" cols="30" rows="3"></textarea></td>
            </tr>
        </table>

    </div>
   
    <input type="hidden" id="hdClient" runat="server" />
    <asp:HiddenField ID="hduser" runat="server" />
    <link rel="stylesheet" type="text/css" href="<%=ReturnUrl("css") %>procs/myaccountpanel.css" />
    <link rel="stylesheet" type="text/css" href="<%=ReturnUrl("css") %>procs/editprofile.css" />
    <ucjs:js ID="cntrljs" runat="server" />
    <script type="text/javascript" defer="defer">              
        $('#calendar').fullCalendar({
            selectable: true,
            select: selectDate,           
            events: "<%=ReturnUrl("sitepathmain")%>MyJsonResponse.ashx?id=" + $("#MainContent_hduser").val(),
            eventRender: function (event, element) {
                //alert(event.id);
                element.qtip({
                    content: {
                        text: qTipText(event.start, event.end, event.description),
                        title: '<strong>' + event.title + '</strong>'
                    },
                    position: {
                        my: 'bottom center',
                        at: 'top center'
                    },
                    style: { classes: 'qtip-shadow qtip-rounded' }
                });
            }
        });
    </script>
  
   
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>autocomplete/app.css" />
    <script src="<%=ReturnUrl("sitepath") %>js/autocomplete/typeahead.bundle.min.js"></script>
    <script src="<%=ReturnUrl("sitepath") %>js/autocomplete/bootstrap-tagsinput.min.js"></script>
    <%--<script src="<%=ReturnUrl("sitepath") %>js/autocomplete/app.js"></script>--%>
    <script src="<%=ReturnUrl("sitepath") %>js/autocomplete/app2.js"></script>
    <asp:Label ID="ltmsg" runat="server" Visible="true"></asp:Label>
</asp:Content>


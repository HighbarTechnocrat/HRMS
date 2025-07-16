<%@ Control Language="C#" AutoEventWireup="true" CodeFile="menu.ascx.cs" Inherits="control_menu" %>
<!--MOVING ICONS-->
<link rel="stylesheet" type="text/css" href="<%=ConfigurationManager.AppSettings["sitepathLnDPortal"]%>includes/screen.css"
    media="all" />

<script type="text/javascript" src="<%=ConfigurationManager.AppSettings["sitepathLnDPortal"]%>scripts/jquery-1.3.2.min.js"></script>

<script type="text/javascript" src="<%=ConfigurationManager.AppSettings["sitepathLnDPortal"]%>scripts/jquery-ui-1.7.1.custom.min.js"></script>

<script type="text/javascript" src="<%=ConfigurationManager.AppSettings["sitepathLnDPortal"]%>scripts/execute.js"></script>

<!--END MOVING ICONS-->

<script language="javascript" type="text/javascript" src="<%=ConfigurationManager.AppSettings["sitepathLnDPortal"]%>/cms/js/NewsTicker.js"> 
</script>

<!--MENUS CSS AND JS -->
<link rel="stylesheet" type="text/css" href="<%=ConfigurationManager.AppSettings["sitepathLnDPortal"]%>includes/ddsmoothmenu1.css" />
<%--
<script type="text/javascript" src="<%=ConfigurationManager.AppSettings["sitepathLnDPortal"]%>/js/jquery.min.js"></script>--%>

<script type="text/javascript" src="<%=ConfigurationManager.AppSettings["sitepathLnDPortal"]%>js/ddsmoothmenu.js"></script>

<script type="text/javascript">
ddsmoothmenu.init({
	mainmenuid: "smoothmenu1", //menu DIV id
	orientation: 'h', //Horizontal or vertical menu: Set to "h" or "v"
	classname: 'ddsmoothmenu', //class added to menu's outer DIV
	//customtheme: ["#1c5a80", "#18374a"],
	contentsource: "markup" //"markup" or ["container_id", "path_to_menu_file"]
})
</script>

<script type="text/javascript">
ddsmoothmenu.init({
	mainmenuid: "smoothmenu2", //menu DIV id
	orientation: 'h', //Horizontal or vertical menu: Set to "h" or "v"
	classname: 'ddsmoothmenu2', //class added to menu's outer DIV
	//customtheme: ["#1c5a80", "#18374a"],
	contentsource: "markup" //"markup" or ["container_id", "path_to_menu_file"]
})
</script>

<table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
    <tr>
        <td height="55" width="15">
            &nbsp;
        </td>
        <td height="45">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td height="35" align="left">
                        <table align="left" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="25" align="left">
                                    <img src="images/contact_icon.gif" width="13" height="9" /></td>
                                <td width="55" align="left">
                                    <a href="<%=MenuLinkReferese%>contactus.aspx" class="top_links_icon">Contact</a></td>
                                <td width="20" align="left">
                                    <img src="images/help_icon.gif" width="13" height="10" /></td>
                                <td width="35" align="left">
                                    <a href="<%=MenuLinkReferese%>help.aspx" class="top_links_icon">Help</a></td>
                                <td width="20" align="left" runat="server" id="td1">
                                    <img src="images/sitemap.gif" width="13" height="9" /></td>
                                
                                <%--<td align="left" width="80">
                                    <a href="<%=MenuLinkReferese%>sitemapadmin.aspx" class="top_links_icon">Admin Sitemap</a></td>
                    --%>
                   
   
    <%--<td width="16" align="left" valign="top" style="background:url(images/white_bg.jpg) repeat-y right top">&nbsp;</td>
    <td valign="top" bgcolor="#FFFFFF" align="left"><a href="<%=ConfigurationManager.AppSettings["sitepathCMS"] %>sitemap.aspx" class="top_links_icon">CMS Sitemap</a><ul id="sitemap-ul"><!--#include file="ConfigurationManager.AppSettings["sitepathCMS"]/menu/sitemap.html" -->
    </ul></td>
    <td width="16" valign="top" style="background:url(images/white_bg.jpg) repeat-y left top">&nbsp;</td>--%>
    
  
                    
                    <td width="50" align="left" id="SitemapEmployee" visible="false" runat="server"><a href="<%=MenuLinkReferese%>sitemapemployee.aspx" class="top_links_icon">Sitemap</a></td>


<td width="50" align="left" id="SitemapSuperVisior" runat="server" visible="false"><a href="<%=MenuLinkReferese%>sitemapsupervisor.aspx" class="top_links_icon">Sitemap </a></td>



<td width="50" align="left" id="SitemapAdmin" visible="false" runat="server"><a href="<%=MenuLinkReferese%>sitemapadmin.aspx" class="top_links_icon">Sitemap </a></td>
                    
                    
                    
                    
                                <td width="20" align="left" runat="server" id="tdAdminIcon" visible="false">
                                    <img src="images/admin_icon.gif" width="13" height="10" /></td>
                                <td align="left" runat="server" id="tdAdminlink" visible="false">
                                    <div id="smoothmenu2" class="ddsmoothmenu2" style="z-index: 2000;">
                                        <ul>
                                            <li><a href="<%=MenuLinkReferese%>help.aspx" class="top_links_icon">Admin</a>
                                                <ul>
                                                    <li><a href="<%=MenuLinkReferese%>tnilist.aspx">TNI(Category) list </a></li>
                                                    <li><a href="<%=MenuLinkReferese%>subtnilist.aspx">SubTNI list </a></li>
                                                    <li><a href="<%=MenuLinkReferese%>trainingprogramlist.aspx">Training Programs </a></li>
                                                    <li><a href="<%=MenuLinkReferese%>forthcoming.aspx">Forthcoming Programs </a></li>
                                                    <li><a href="<%=MenuLinkReferese%>viewchecklistadmin.aspx">Admin Checklist</a></li>
                                                    <li><a href="<%=MenuLinkReferese%>viewchecklisttrainer.aspx">Trainer Checklist</a></li>
                                                    <li><a href="<%=MenuLinkReferese%>viewseatingarrangement.aspx">Seating Arrangement</a></li>
                                                    <li><a href="<%=MenuLinkReferese%>trainerlist.aspx">Trainers</a></li>
                                                    <li><a href="<%=MenuLinkReferese%>divisions.aspx">Divisions</a></li>
                                                    <li><a href="<%=MenuLinkReferese%>location.aspx">Locations</a></li>
                                                    <li><a href="<%=MenuLinkReferese%>attendance.aspx">Attendance</a></li>
                                                    <li><a href="<%=MenuLinkReferese%>holidayslist.aspx">Holidays</a></li>
                                                    <li><a href="<%=MenuLinkReferese%>functionslist.aspx">Functions</a></li>
                                                    <li><a href="#">Videos </a>
                                                        <ul>
                                                            <li><a href="<%=MenuLinkReferese%>videocategorylist.aspx">Video Category</a></li>
                                                            <li><a href="<%=MenuLinkReferese%>video.aspx">Video</a></li>
                                                        </ul>
                                                    </li>
                                                    <li><a href="#">Users </a>
                                                        <ul>
                                                            <li><a href="<%=MenuLinkReferese%>uploadusers.aspx">Upload Users</a></li>
                                                            <li><a href="<%=MenuLinkReferese%>uploadlearningplan.aspx">Upload Learning Plan</a></li>
                                                        </ul>
                                                    </li>
                                                    <li><a href="#">Reports</a>
                                                        <ul>
                                                            <li><a href="<%=MenuLinkReferese%>reportstrainingprogram.aspx">Training Programs </a>
                                                            </li>
                                                            <li><a href="<%=MenuLinkReferese%>users.aspx">Users </a></li>
                                                            <li><a href="<%=MenuLinkReferese%>userslearningplan.aspx">Users Learning Plan </a></li>
                                                            <li><a href="<%=MenuLinkReferese%>userstrainingprograms.aspx">Users Training Program</a></li>
                                                        </ul>
                                                    </li>
                                                </ul>
                                            </li>
                                        </ul>
                                    </div>
                                </td>
                                <td height="55" class="logo" align="right">
                                    <img src="images/logo-white.png" style="padding-right: 0px;" width="175" height="50" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <img src="images/wel_icon.gif" style="padding-right: 10px;" /><font color="#FFFFFF">Welcome,</font>
                        <b>
                            <asp:Label ID="lblFullName" runat="server" CssClass="login_top" Style="color: #FFD869;"></asp:Label></b>
                    </td>
                </tr>
            </table>
        </td>
        <td height="55">
            &nbsp;
        </td>
        <td height="55">
            &nbsp;
        </td>
    </tr>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
    <tr>
        <td width="47" height="57" valign="top"  style="padding-top: 10px">
           </td>
        <td>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
               <%-- <tr>
                    <td align="center" class="top_tab_bg">
                        <table width="868" border="0" cellspacing="0" cellpadding="0" align="center">
                            <tr>
                                <td align="center">
                                    <ul id="nav-shadow">
                                        <li class="button-color-1"><a href="<%=ConfigurationManager.AppSettings["sitepathLnDPortal"]%>Default.aspx"
                                            title="Home">Home</a></li>
                                        <li class="button-color-2"><a href="<%=ConfigurationManager.AppSettings["sitepathCMS"]%>insidepage.aspx?id=53"
                                            title="About L & D">About L & D</a></li>
                                        <li class="button-color-3"><a href="<%=ConfigurationManager.AppSettings["sitepathCMS"]%>insidepage.aspx?id=85"
                                            title="E Learning">E Learning</a></li>
                                        <li class="button-color-4"><a href="<%=ConfigurationManager.AppSettings["sitepathLnDPortal"]%>mylearningplan.aspx"
                                            title="Learning Plan">Learning Plan</a></li>
                                        <li class="button-color-5"><a href="<%=ConfigurationManager.AppSettings["sitepathLnDPortal"]%>quarterlycalendar.aspx"
                                            title="Calendar">Calendar</a></li>
                                        <li class="button-color-6"><a href="<%=ConfigurationManager.AppSettings["sitepathLnDPortal"]%>othertrainingprogram.aspx"
                                            title="External Program">External Programs</a></li>
                                        <li class="button-color-7"><a href="<%=ConfigurationManager.AppSettings["sitepathLnDPortal"]%>requeststatus.aspx"
                                            title="Status & History">Status & History</a></li>
                                    </ul>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>--%>
                <tr>
                    <td height="5" bgcolor="">
                        <img src="images/spacer.gif" width="1" height="1" /></td>
                </tr>
                <tr>
                    <td height="6" bgcolor="">
                        <img src="images/spacer.gif" width="1" height="1" /></td>
                </tr>
                <tr>
                    <td bgcolor="">
                        <table width="880" border="0" cellspacing="0" cellpadding="0" align="center">
                            <tr>
                                
                                <td class="menu_bg1">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
                                        <tr id="tdEmployeeMenu" runat="server">
                                            <td>
                                                <div id="smoothmenu1" class="ddsmoothmenu" style="z-index: 1000; position: relative;
                                                    text-align: center">
                                                    <ul>
                                                        <li style="width: 124px;"><a href="<%=MenuLinkReferese%>Default.aspx">Home</a></li><%=CmsMenuFileContent%><li>
                                                            <a href="#">Learning Plan</a><ul>
                                                                <li><a href="<%=MenuLinkReferese%>mylearningplan.aspx">Self</a></li></ul>
                                                        </li>
                                                        <li style="width: 124px;"><a href="#">Calendar</a><ul>
                                                            <li><a href="<%=MenuLinkReferese%>quarterlycalendar.aspx">Annual calendar </a></li>
                                                            <li><a href="<%=MenuLinkReferese%>mylearningcalendar.aspx">My Learning calendar </a>
                                                            </li>
                                                        </ul>
                                                        </li>
                                                        <li style="width: 126px; padding-left: 5px;"><a href="#">External Program</a><ul>
                                                            <li><a href="<%=MenuLinkReferese%>othertrainingprogram.aspx">Add External Program </a>
                                                            </li>
                                                        </ul>
                                                        </li>
                                                        <li style="width: 122px; border: 0px;"><a href="<%=MenuLinkReferese%>#">Status & History</a><ul>
                                                            <li><a href="#">Status</a>
                                                                <ul>
                                                                    <li><a href="<%=MenuLinkReferese%>requeststatus.aspx">Self </a></li>
                                                                </ul>
                                                            </li>
                                                            <li><a href="#">Training program History</a>
                                                                <ul>
                                                                    <li><a href="<%=MenuLinkReferese%>traininghistory.aspx">Self </a></li>
                                                                </ul>
                                                            </li>
                                                            <li><a href="#">Learning plan History</a>
                                                                <ul>
                                                                    <li><a href="<%=MenuLinkReferese%>selflearningplanhistory.aspx">Self </a></li>
                                                                </ul>
                                                            </li>
                                                        </ul>
                                                        </li>
                                                        <%--   <li><a href="#">Training History</a><ul></ul></li>--%>
                                                    </ul>
                                                </div>
                                            </td>
                                            <%--<td width="20" align="left" runat="server" id="td2">
                                                <img src="images/admin_icon.gif" width="13" height="10" /></td>
                                            <td>
                                                <a href="<%=MenuLinkReferese%>sitemapemployee.aspx" title="Employee Sitemap"></a>
                                            </td>--%>
                                        </tr>
                                        <tr id="tdAdminMenu" runat="server">
                                            <td align="center">
                                                <div id="smoothmenu1" class="ddsmoothmenu" style="z-index: 1000; position: relative;">
                                                    <ul>
                                                        <li style="width: 124px;"><a href="<%=MenuLinkReferese%>Default.aspx">Home</a> </li>
                                                        <%=CmsMenuFileContent%>
                                                        <li style="width: 124px;"><a href="#">Learning Plan</a>
                                                            <ul>
                                                                <li><a href="<%=MenuLinkReferese%>mylearningplan.aspx">Self</a></li>
                                                                <li><a href="<%=MenuLinkReferese%>teamlearningplan.aspx">Team</a></li>
                                                            </ul>
                                                        </li>
                                                        <li style="width: 124px;"><a href="#">Calendar</a>
                                                            <ul>
                                                                <li><a href="<%=MenuLinkReferese%>quarterlycalendar.aspx">Annual calendar </a></li>
                                                                <li><a href="<%=MenuLinkReferese%>mylearningcalendar.aspx">My Learning calendar </a>
                                                                </li>
                                                            </ul>
                                                        </li>
                                                        <li style="width: 126px; padding-left: 5px;"><a href="#">External Program</a>
                                                            <ul>
                                                                <li><a href="<%=MenuLinkReferese%>othertrainingprogram.aspx">Add External Program </a>
                                                                </li>
                                                            </ul>
                                                        </li>
                                                        <li style="width: 122px; border: 0px;"><a href="<%=MenuLinkReferese%>#">Status & History</a><ul>
                                                            <li><a href="#">Status</a>
                                                                <ul>
                                                                    <li><a href="<%=MenuLinkReferese%>requeststatus.aspx">Self </a></li>
                                                                    <li><a href="<%=MenuLinkReferese%>teamrequeststatus.aspx">Team </a></li>
                                                                </ul>
                                                            </li>
                                                            <li><a href="#">Training program History</a>
                                                                <ul>
                                                                    <li><a href="<%=MenuLinkReferese%>traininghistory.aspx">Self </a></li>
                                                                    <li><a href="<%=MenuLinkReferese%>teamtraininghistory.aspx">Team </a></li>
                                                                </ul>
                                                            </li>
                                                            <li><a href="#">Learning plan History</a>
                                                                <ul>
                                                                    <li><a href="<%=MenuLinkReferese%>selflearningplanhistory.aspx">Self </a></li>
                                                                    <li><a href="<%=MenuLinkReferese%>teamlearningplanhistory.aspx">Team </a></li>
                                                                </ul>
                                                            </li>
                                                        </ul>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </td>
                                        </tr>
                                        <%--START SUPER VISIOR MENUS TOP MENUS--%>
                                        <tr id="tsSupervisorMenu" runat="server" visible="false">
                                            <td align="center">
                                                <div id="smoothmenu1" class="ddsmoothmenu" style="z-index: 1000; position: relative;">
                                                    <ul>
                                                        <li style="width: 124px;"><a href="<%=MenuLinkReferese%>Default.aspx">Home</a> </li>
                                                        <%=CmsMenuFileContent%>
                                                        <li style="width: 124px;"><a href="#">Learning Plan</a>
                                                            <ul>
                                                                <li><a href="<%=MenuLinkReferese%>mylearningplan.aspx">Self</a></li>
                                                                <li><a href="<%=MenuLinkReferese%>teamlearningplan.aspx">Team</a></li>
                                                            </ul>
                                                        </li>
                                                        <li style="width: 124px;"><a href="#">Calendar</a>
                                                            <ul>
                                                                <li><a href="<%=MenuLinkReferese%>quarterlycalendar.aspx">Annual calendar </a></li>
                                                                <li><a href="<%=MenuLinkReferese%>mylearningcalendar.aspx">My Learning calendar </a>
                                                                </li>
                                                            </ul>
                                                        </li>
                                                        <li style="width: 126px; padding-left: 5px;"><a href="#">External Program</a>
                                                            <ul>
                                                                <li><a href="<%=MenuLinkReferese%>othertrainingprogram.aspx">Add External Program </a>
                                                                </li>
                                                            </ul>
                                                        </li>
                                                        <li style="width: 122px; border: 0px;"><a href="<%=MenuLinkReferese%>#">Status & History</a><ul>
                                                            <li><a href="#">Status</a>
                                                                <ul>
                                                                    <li><a href="<%=MenuLinkReferese%>requeststatus.aspx">Self </a></li>
                                                                    <li><a href="<%=MenuLinkReferese%>teamrequeststatus.aspx">Team </a></li>
                                                                </ul>
                                                            </li>
                                                            <li><a href="#">Training program History</a>
                                                                <ul>
                                                                    <li><a href="<%=MenuLinkReferese%>traininghistory.aspx">Self </a></li>
                                                                    <li><a href="<%=MenuLinkReferese%>teamtraininghistory.aspx">Team </a></li>
                                                                </ul>
                                                            </li>
                                                            <li><a href="#">Learning plan History</a>
                                                                <ul>
                                                                    <li><a href="<%=MenuLinkReferese%>selflearningplanhistory.aspx">Self </a></li>
                                                                    <li><a href="<%=MenuLinkReferese%>teamlearningplanhistory.aspx">Team </a></li>
                                                                </ul>
                                                            </li>
                                                        </ul>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </td>
                                            <%--<td>
                                                <a href="<%=MenuLinkReferese%>sitemapsupervisor.aspx" class="top_links_icon">Supervisor
                                                    Sitemap</a></td>--%>
                                        </tr>
                                        <%--END SUPER VISIOR MENUS TOP MENUS--%>
                                    </table>
                                </td>
                                
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
        <td width="47" height="47" valign="top" >
           </td>
    </tr>
    <tr>
        <td width="47" height="47">
        </td>
        
        <td width="47" height="47" align="right">
            </td>
    </tr>
</table>
<%--  <%=CmsMenuFileContent%> --%>

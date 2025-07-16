<%@ Page Language="C#" AutoEventWireup="true" CodeFile="notify.aspx.cs" Inherits="notify" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>
    <script type="text/javascript" >
    $(document).ready(function () {
            $("#notificationlink").click(function () {
                $("#notificationcontainer").fadeToggle(300);
                //$("#notification_count").fadeOut("slow");
                return false;
            });

            //Document Click
            $(document).click(function () {
                $("#notificationcontainer").hide();
            });
            //Popup Click
            //$("#notificationcontainer").click(function () {
            //    return false
            //});

        });
    </script>
    <style>
    body {
	font-family: arial
}
    #notificationcontainer {
	background-color: #fff;
	border: 1px solid rgba(100, 100, 100, .4);
	-webkit-box-shadow: 0 3px 8px rgba(0, 0, 0, .25);
	overflow: visible;
	position: absolute;
	top: 60px;
	width: 400px;
	z-index: -1;
	display: none;
}

    #notificationcontainer:before {
	content: '';
	display: block;
	position: absolute;
	width: 0;
	height: 0;
	color: transparent;
	border: 11px solid black;
	border-color: transparent transparent #000;
	margin-top: -20px;
	margin-left: 3px;
}
    #notificationtitle {
	z-index: 1000;
	font-weight: bold;
	padding: 10px 0;
	font-size: 14px;
	background-color: #222;
	width: 100%;
	color:#ffffff;
	text-transform:uppercase;
	text-align:center;
}
    #notificationsbody {
	min-height: 100px;
	height:180px;
	overflow-y:auto;
}
    #notification_count {
	padding: 3px 7px 3px 7px;
	background: #2E85DC;
	color: #ffffff;
	font-weight: bold;
	margin-left: 15px;
	border-radius: 9px;
	position: absolute;
	z-index:999;
	font-size: 11px;
}
    #imgprofile
    {
        width:110;height:110;
    }
    #notificationlink{color:#000;font-size:30px;}
    .notifications-detail{padding:10px;float:left;width:95%;border-bottom:1px solid #bcbfc4;background:#f1f1f1;}
    .notifications-active{background:#ffffff;}
    .notificationsdiv{float:left;width:77%;margin:4px 0 0 15px;}
    .notifications-heading{font-size:15px;font-weight:600;float:left;width:100%;margin:0 0 5px 0}
    .notifications-photo{float:left}
    .notifications-photo img{width:60px;height:60px;border-radius:50%;border:3px solid #222222;}
    .notification-date{float:left;color:#707070;font-size:12px;font-weight:300;width:100%;}
    .no-notifications{font-size:15px;font-style:italic;color:#ccc;text-align:center;padding:70px 0;}
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin:0 auto;width:900px" id="divnotify" runat="server">
    <asp:Label ID="notification_count" runat="server"></asp:Label> 
      <a href="#" id="notificationlink">
          <i class="fa fa-bell"></i>
      </a>
      <div id="notificationcontainer">
        <div id="notificationtitle">Notifications</div>
        <div id="notificationsbody" class="notifications">
            <asp:Repeater ID="rptnotify" runat="server" OnItemCommand="rptnotify_ItemCommand">
                <ItemTemplate>
                 <asp:LinkButton ID="lnkuser" runat="server" CommandName="cmdflag" CommandArgument='<%# Eval("followerid")%>'>
        	<div class="notifications-detail notifications-active">               
            	<div class="notifications-photo">
                    <asp:Image ID="imgprofile" runat="server" />
            	</div>
            	<div class="notificationsdiv">
            		<div class="notifications-heading">
                        <%# Eval("fullname")%> started folllowing you.
            		</div>
                	<div class="notification-date">
                        <asp:Label ID="lblflag" runat="server" Text='<%# Eval("readflag")%>' Visible="false"></asp:Label>
                        <asp:Label ID="lbldate" runat="server"></asp:Label>
                	</div>
                </div>               
            </div> 
               </asp:LinkButton>
                    </ItemTemplate>
                </asp:Repeater>           
            <!--<div class="no-notifications">No notification</div>-->
        </div>
      </div>
</div>
    </form>
</body>
</html>

<%@ Control Language="C#" AutoEventWireup="true" CodeFile="birthday.ascx.cs" Inherits="themes_creative1_LayoutControls_birthday" %>



<style type="text/css">
    .tcls{
        cursor: default;
    }
    
</style>
<asp:Panel ID="bday1" runat="server" class="widget widget_woffice_birthdays widget_woffice_birthdays1">
   <%-- <div id="woffice_birthdays-2" >--%>
        <%--SONY COIMMENTED BELOW CODE AND CREATED NEW CLASS --%>
        <%--<div class="intern-padding">--%>
         <%--    <div class="intern-padding-birthday">--%>

            <%--jayesh Comment below line to hide close sign birthday widget 3oct2017--%>
           <%--  <a class="lnkclose" data="birthday"><i class="fa fa-close"></i></a>--%>
            <%--jayesh Comment Above line to hide close sign birthday widget 3oct2017--%>
               <%-- <div class="intern-box box-title">--%>
           <%-- <h3>Birthdays</h3>--%>
     <%--   </div>--%>
    <%-- <script>
         (function ($) {
             $(window).load(function () {
                 $(".content").mCustomScrollbar();
             });
         })(jQuery);
        </script>--%>

    <div id="new123">
        <div class="text-birth mCustomScrollbar-text">
            <asp:Label ID="lblEmptyRepeater" runat="server" Text="NO BIRTHDAY TODAY" ></asp:Label>
                <%--<ul class="birthdays-list">--%>
                    <asp:Repeater ID="rptbday" runat="server">
                        <ItemTemplate>
                                       <%--<li class="clearfix">--%>
                            <%--SAGAR COMMENTED BELOW LINE FOR REMOVING CLICK EVENT OF BIRTHDAY WIDGET 16OCT2017--%>
                               <%-- <a  href='<%#getuser(Eval("indexid")) %>'>--%>
                             <a class="tcls"  href='#'>
                                         <span class="birthday-item-content">
										 <i class="fa fa-birthday-cake" aria-hidden="true" style="color:grey"></i><span class="birthdaylightfont"><u> <%# Convert.ToDateTime(Eval("DOB")).ToString("M") %></u></span>
										 <br /> 
                                         <%#(DataBinder.Eval(Container, "DataItem.fullname")) %> <%--<i>(28)</i> --%>
                                            
                                          <%-- <span class="birthdaylightfont">   <%# Convert.ToDateTime(Eval("event_date")).ToString("M") %></span>--%>
                                          <%-- <span class="birthdaylightfont"><%#(DataBinder.Eval(Container, "DataItem.DOB")) %> </span>--%>
                                        <%--  <%# (Convert.ToDateTime(Eval("DOB"))).ToString("MMMMMMMMMM dd, yyyy")%>' Font-Bold="false">--%>
                                     <%--  <br /> <span class="birthdaylightfont"><%#(DataBinder.Eval(Container, "DataItem.designation").ToString().Length>30 ? Eval("designation").ToString().Substring(0, 30)+"..." : Eval("designation")) %> </span>
                                             <br />   <span class="birthdaylightfont"><%#(DataBinder.Eval(Container, "DataItem.department").ToString().Length>30 ? Eval("department").ToString().Substring(0, 30)+"..." : Eval("department") ) %> </span>
                                     <br /> <span class="birthdaylightfont"><%#(DataBinder.Eval(Container, "DataItem.location").ToString().Length>30 ? Eval("location").ToString().Substring(0, 30)+"..." : Eval("location") ) %>  </span>
                                             <br />
                                     --%>     
                                           <br /> <span class="birthdaylightfont"><%#(DataBinder.Eval(Container, "DataItem.designation").ToString().Length>40 ? Eval("designation").ToString().Substring(0, 40)+"..." : Eval("designation")) %> </span>
                                             <br />   <span class="birthdaylightfont"><%#(DataBinder.Eval(Container, "DataItem.department").ToString().Length>40 ? Eval("department").ToString().Substring(0, 40)+"..." : Eval("department") ) %> </span>
                                     <%--<br /> <span class="birthdaylightfont"><%#(DataBinder.Eval(Container, "DataItem.location").ToString().Length>40 ? Eval("location").ToString().Substring(0, 40)+"..." : Eval("location") ) %>  </span>--%>
                                             <br />
                                        
                                               
                                      <%--<br />   <span class="birthdaylightfont"><%#(DataBinder.Eval(Container, "DataItem.department")) %> --%>
                                      <%--<span class="label"><i class="fa fa-birthday-cake" aria-hidden="true" style="color:#fff"></i>--%><%-- <%# Convert.ToDateTime(Eval("event_date")).ToString("M") %>--%>
                                    </span>
                                    <img src='<%=ReturnUrl("mediapath")%>profile55x55/<%#(DataBinder.Eval(Container, "DataItem.profilephoto")) %>' class="avatar user-7-avatar avatar-96 photo" width="96" height="96" alt='<%#(DataBinder.Eval(Container, "DataItem.fullname")) %>'align="right" />
                                   </span>
                                </a>
                        
                            <%--</li>--%>
                        </ItemTemplate>
                    </asp:Repeater>
                <%--</ul>--%>
                </div>
        </div>
   <%-- </div>--%>
</asp:Panel>


<%@ Control Language="C#" AutoEventWireup="true" CodeFile="catproduct2.ascx.cs" Inherits="themes_creative_LayoutControls_catproduct2" %>

<%--Sony commented this to stop display of latest news widget on home page--%>
<%--<%@ Register Src="~/themes/creative1.0/LayoutControls/latestnews3.ascx" TagName="uxnews" TagPrefix="ucnews" %>--%>
<%--Sony commented this to stop display of thought of the day widget on home page--%>
<%--<%@ Register Src="~/themes/creative1.0/LayoutControls/thoughts.ascx" TagName="thought" TagPrefix="thgt" %>--%>
<%@ Register Src="~/themes/creative1.0/LayoutControls/birthday.ascx" TagName="birthdays" TagPrefix="birth" %>

<style>
     .lnkview {
  float: right;
  margin-bottom: 10px;
}
 .lnkview
 {
     color:#fff !important;
     border-color:#fff !important;
 }
 .lnkview:hover
 {
     color:#05568b !important;
 }
 
</style>


     <%--     <asp:Label ID="lbladsid" runat="server"></asp:Label>     --%>                 
<%--<asp:Repeater ID="rptcatads" runat="server">
    <ItemTemplate>
        <asp:Panel ID="panads" runat="server" CssClass="widget box WiseChatWidget wtads">
            <div class="intern-padding">
                <asp:Label ID="lbladsid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.categoryid") %>'
                    Visible="false"></asp:Label>
                <div class="intern-box box-title">
                    <h3>--%>
                        <%--Documents--%><%--<%# DataBinder.Eval(Container, "DataItem.categoryname") %></h3>
                   
                </div>
                <asp:Repeater ID="rptads" runat="server">
                    <ItemTemplate>
                        <div class="textwidget">
                            <a href='<%#productUrlrewriting( Eval("productname"), Eval("productid")) %>'>
                                <asp:Image ID="imgads" runat="server" class="fullwidth" hspace="20" align="left" />
                                <span class="adstitles"><%# Eval("productname").ToString().Length > 30 ? Eval("productname").ToString().Substring(0, 30)+"..." : Eval("productname") %></span><br />
                                <%#getsubstr(Eval("shortdescription")) %></a>
                            <asp:Label ID="lbldocuser" runat="server" Text='<%# Eval("createdby")%>' Visible="false"></asp:Label>
                            <asp:Label ID="lbldocuser2" runat="server" Visible='<%#getdocvisible(Eval("categoryname"))%>'>--%><%--Posted By :--%>
                               
                                 <%--<asp:LinkButton ID="lnkuser" CssClass="docuser" runat="server" Text='<% #getfullname(Eval("createdby"))%>'></asp:LinkButton>--%><%--</asp:Label>--%>
                         
         <%--   
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                 <asp:LinkButton ID="lnkview" runat="server" CssClass="lnkview" Visible="false" PostBackUrl='<%#onclick_hlnkcategory(Eval("categoryid"),Eval("categoryname")) %>'></asp:LinkButton>
            </div>
        </asp:Panel>
    </ItemTemplate>
</asp:Repeater>--%>
<%--SONY MOVED this code up here to display birthday control on TOP--%>
<!-- Birth Day -->
<%--<birth:birthdays ID="birthday" runat="server"></birth:birthdays>--%>


<%--SAGAR COMMENTED THIS FOR REMOVING NEWS FROM THE FRONT END 3_OCT2017 STARTS HERE--%>
<!-- Achievements -->
<asp:Repeater ID="rptcatads" runat="server">
    <ItemTemplate>            
        <asp:Panel ID="panads" runat="server" CssClass="widget box WiseChatWidget wtads">
            <div class="rptcatadsHeader">
                <div class="intern-padding">
                <asp:Label ID="lbladsid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.categoryid") %>'
                    Visible="false"></asp:Label>
                <div class="intern-box box-title">
                    <h3 class="HomeNewsTitle">                    
                      <%--  <%--Documents--%>                        
                        <%# DataBinder.Eval(Container, "DataItem.categoryname") %>
                    </h3>
                </div>
                <asp:Repeater ID="rptads" runat="server">
                    <ItemTemplate>
                        <div class="textwidget">
                            <a href='<%#productUrlrewriting( Eval("productname"),Eval("productid")) %>'>
                                <asp:Image ID="imgads" runat="server" class="fullwidth" hspace="20" align="left" />
                                <%--<span class="adstitles"><%# Eval("productname").ToString().Length > 30 ? Eval("productname").ToString().Substring(0, 30)+"..." : Eval("productname") %></span><br />--%>
                                 <p><span class="adstitles"><%# Eval("productname").ToString().Length>65 ? Eval("productname").ToString().Substring(0, 65)+"..." : Eval("productname") %></span><br /></p></a>
                              <%--  <%#getsubstr(Eval("shortdescription")) %>--%>
                            <%--SAGAR ADDED THIS CODE FOR ADDING DATE IN NEW FORMAT 11OCT2017 STARTS HERE--%>
                             <%-- <asp:Label ID="lblicon1" runat="server" Visible="false"></asp:Label>--%>
                            
                             <asp:Label ID="lblneweventdate" runat="server" CssClass="eventdate" Text='<%# (Convert.ToDateTime(Eval("startdate"))).ToString("MMMMMMMMMM dd, yyyy")%>' Font-Bold="false">
                            </asp:Label>
                             <%--SAGAR ADDED THIS CODE FOR ADDING DATE IN NEW FORMAT 11OCT2017 ENDS HERE--%>
                            <asp:Label ID="lbldocuser" Class="new" runat="server" Text='<%# Eval("createdby")%>' Visible="false"></asp:Label>
                            <asp:Label ID="lbldocuser2" Class="new" runat="server" Visible='<%#getdocvisible(Eval("categoryname"))%>'><%--Posted By : <asp:LinkButton ID="lnkuser" CssClass="docuser" runat="server" Text='<%#getfullname(Eval("createdby"))%>'></asp:LinkButton>--%>

                            </asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:Repeater><br/>
                 <asp:LinkButton ID="lnkview" style="background-color:grey" runat="server" CssClass="lnkview" Visible="true" PostBackUrl='<%#onclick_hlnkcategory(Eval("categoryid"),Eval("categoryname")) %>'>View All sanjay</asp:LinkButton>

                <%--  <asp:LinkButton ID="LinkButton2" style="background-color:grey" runat="server" CssClass="lnkview" Visible="true" PostBackUrl='<%#onclick_hlnkcategory1(Eval("categoryid")) %>'>View All</asp:LinkButton>--%>
                </div>
            </div>
        </asp:Panel>
    </ItemTemplate>
</asp:Repeater>
<%--SAGAR COMMENTED THIS FOR REMOVING NEWSS FROM THE FRONT END 3_OCT2017 ENDS HERE--%>

<%--JAYESH ADDED BELOW CODE FOR NEWS WIDGET 9oct2017 with new css class and div class--%>
<%--<asp:Repeater ID="Repeater1" runat="server">
    <ItemTemplate>
        <asp:Panel ID="panads" runat="server" CssClass="widget box WiseChatWidget wtads">
            <div class="intern-padding">
                <asp:Label ID="lbladsid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.categoryid") %>'
                    Visible="false"></asp:Label>
                <div class="intern-box box-title">
                    <h3>                    
                        <%# DataBinder.Eval(Container, "DataItem.categoryname") %></h3>                  
                </div>
                <asp:Repeater ID="rptads" runat="server">
                    <ItemTemplate>
                        <div class="new-textwidget">
                            <a href='<%#productUrlrewriting( Eval("productname"), Eval("productid")) %>'>
                                <asp:Image ID="imgads" runat="server" class="fullwidth" hspace="20" align="left" />
                                <span class="adstitles"><%# Eval("productname").ToString().Length > 30 ? Eval("productname").ToString().Substring(0, 30)+"..." : Eval("productname") %></span><br />
                                <%#getsubstr(Eval("shortdescription")) %></a>
                            <asp:Label ID="lbldocuser" Class="new" runat="server" Text='<%# Eval("createdby")%>' Visible="false"></asp:Label>
                            <asp:Label ID="lbldocuser2" Class="new" runat="server" Visible='<%#getdocvisible(Eval("categoryname"))%>'>--%><%--Posted By : <asp:LinkButton ID="lnkuser" CssClass="docuser" runat="server" Text='<%#getfullname(Eval("createdby"))%>'></asp:LinkButton>--%><%--</asp:Label>--%>
                  <%--      </div>
                    </ItemTemplate>
                </asp:Repeater><br/>
                 <asp:LinkButton ID="lnkview" runat="server" CssClass="lnkview" Visible="false" PostBackUrl='<%#onclick_hlnkcategory(Eval("categoryid"),Eval("categoryname")) %>'>View All</asp:LinkButton>
            </div>
        </asp:Panel>
    </ItemTemplate>
</asp:Repeater>--%>

<%--JAYESH ADDED ABOVE CODE FOR NEWS WIDGET 9oct2017 with new css class and div class--%>

<!-- thought of the days -->
<%--<thgt:thought ID="uxthoughts" runat="server" />--%>

<%--SAGAR COMMENTED THIS FOR REMOVING PHOTO GALLERY FROM THE FRONT END 28SEPT2017 STARTS HERE--%>
<!-- Photo Gallery -->
<%--<asp:Repeater ID="rptcatimg" runat="server">
    <ItemTemplate>
        <asp:Panel ID="pnlphoto" runat="server" CssClass="widget box widget_woffice_tasks_assigned">
            <div class="intern-padding">
                <div class="project-assigned-container">
                    <asp:Label ID="lblimgid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.categoryid") %>'
                        Visible="false"></asp:Label>
                    <asp:Panel ID="panimg" runat="server" CssClass="project-assigned-head">
                        <div class="intern-box box-title">
                            <h3><%# DataBinder.Eval(Container, "DataItem.categoryname") %></h3>
                           
                        </div>
                        <div class="gallary-main">
                            <asp:Repeater ID="rptimg" runat="server">
                                <ItemTemplate>
                                    <div id="divimg" runat="server" class="gallary-image">
                                        <a href='<%#galleryUrlrewriting(Eval("productid")) %>' class="texticon tooltip" title='<%# Eval("productname").ToString().Length > 100 ? Eval("productname").ToString().Substring(0, 100)+"..." : Eval("productname") %>'>
                                            <asp:Image ID="imghome" runat="server" />
                                        </a>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                         <asp:LinkButton ID="lnkview" CssClass="lnkview" runat="server" PostBackUrl='<%#onclick_hlnkcategory(Eval("CategoryId"),Eval("categoryname")) %>'>View All</asp:LinkButton>
                    </asp:Panel>
                </div>
            </div>
        </asp:Panel>
    </ItemTemplate>
</asp:Repeater>--%>



<%--SAGAR ADDED THIS FOR ADDING TIME OFF, ACHIEVMENTS with SINGLE PHOTO FROM THE FRONT END 2OCT2017 STARTS HERE--%>
<!--ACHIEVMENTS , Time out  currently hide -->
<asp:Repeater ID="rptcatimg" runat="server" Visible="false">
    <ItemTemplate>
            <%-- pnlphoto--%>
        <asp:Panel ID="pnlphoto" runat="server" CssClass="widget box widget_woffice_tasks_assigned">
            <div class="intern-padding">
                <div class="project-assigned-container">
                    <asp:Label ID="lblimgid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.categoryid") %>'
                        Visible="false"></asp:Label>
                    <asp:Panel ID="panimg" runat="server" CssClass="project-assigned-head">
                        <div class="intern-box box-title" style="display:none">
                            <h3><%# DataBinder.Eval(Container, "DataItem.categoryname") %></h3>                           
                        </div>

                        <div class="gallary-main">
                            <asp:Repeater ID="rptimg" runat="server">
                                <ItemTemplate>
                                    <div id="divimg" runat="server" class="gallary-image">
                                        <%--Title remove not required title='<%# Eval("productname") %>'--%>
                                        <a href='<%#galleryUrlrewriting(Eval("productid")) %>' class="texticon tooltip">
                                            <asp:Image ID="imghome" runat="server" />
                                              <asp:Label ID="lblpname" runat="server"  Text=' <%# Eval("productname").ToString().Length > 100 ? Eval("productname").ToString().Substring(0, 100)+"..." : Eval("productname") %>' Visible="false"></asp:Label>
                                        </a>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                         <asp:LinkButton ID="lnkview" CssClass="lnkview" runat="server" visible="false" PostBackUrl='<%#onclick_hlnkcategory(Eval("CategoryId"),Eval("categoryname")) %>'>View All</asp:LinkButton>
                    </asp:Panel>
                </div>
            </div>
        </asp:Panel>
    </ItemTemplate>
</asp:Repeater>

<%--SAGAR COMMENTED THIS FOR REMOVING PHOTO GALLERY FROM THE FRONT END 28SEPT2017 ENDS HERE--%>

<!-- Highbar External Links -->
      <%--  <asp:Panel ID="pnlhcclinks" runat="server" CssClass="widget box WiseChatWidget" Visible="false">
<div class="intern-padding">
    <div class="intern-box box-title">
        <h3>Quick  Links</h3>
    </div>
    <div class="textwidget">
        <a id="lnkzp" runat="server" target="_blank">
            <i class="fa fa-square"></i>Highbar Portal
        </a>
    </div>
        <div class="textwidget">
        <a id="lnkzc" runat="server" target="_blank">
            <i class="fa fa-square"></i>Highbar Connect
        </a>
    </div>
        <div class="textwidget">
        <a id="lnktbc" runat="server" target="_blank">
            <i class="fa fa-square"></i>Thanks Buddy Card
        </a>
    </div>
</div>
            </asp:Panel>--%>

<%--SAGAR COMMENTED THIS FOR REMOVING ARTICLES FROM THE FRONT END 28SEPT2017 STARTS HERE--%>
<!-- Market Place -->



<%--SAGAR COMMENTED THIS FOR REMOVING ARTICLES FROM THE FRONT END 28SEPT2017 ENDS HERE--%>
<%--<birth:birthdays ID="birthday" runat="server"></birth:birthdays>--%>

<%-- Text Events , Meetings, Tasks --%>
<%--sagar commented below code working (stable) get data from xml 30nov2017 for meeting and task starts here--%>
<%--<asp:Repeater ID="rptcategname" runat="server">
    <ItemTemplate>
          
        <asp:Panel ID="pnlcat" runat="server" CssClass="widget box WiseChatWidget">--%>
            <%--<div class="intern-padding">--%>
  <%--        
            <div class="intern-padding-events">--%>
             <%--   <asp:Button runat="server" Text="Button"></asp:Button>--%>
               <%-- <asp:Label ID="lbcatlid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.categoryid") %>'
                    Visible="false"></asp:Label>
                <div class="intern-box box-title">
                     <asp:LinkButton ID="Lk1" runat="server" PostBackUrl="http://localhost/hrms/mycalendar"> Add</asp:LinkButton>

                    <h3><%# DataBinder.Eval(Container, "DataItem.categoryname") %></h3>--%>
                       <%--SAGAR ADDED BELOW LINE FOR ADDING ADD FUNCTION FOR MEETING 23OCT2017--%>
                   
                 <%-- <asp:LinkButton ID="Lk1" runat="server" PostBackUrl="http://localhost/hrms/mycalendar"> Add</asp:LinkButton>--%>
               <%-- </div>
                <asp:Repeater ID="rptcatproduct" runat="server" OnItemDataBound="rptcategname_ItemDataBound"
                    OnItemCommand="rptcatproduct_ItemCommand">
                    <ItemTemplate>
                        <div class="textwidget">
                       <asp:Label ID="lblicon" runat="server" Visible="true"></asp:Label>
                            <asp:Label ID="lbleventdate" runat="server" CssClass="eventdate" Text='<%# Eval("startdate")%>' Font-Bold="false" Visible="false">--%>
                                <%--Text='<%# Eval("startdate")%>
                                    asp:Label ID="lblneweventdate" runat="server" CssClass="eventdate" Text='<%# (Convert.ToDateTime(Eval("startdate"))).ToString("MMMMMMMMMM dd, yyyy")%>' Font-Bold="false">--%>
                                <%--<
                            </asp:Label>

                            <%# Eval("productname").ToString().Length > 100 ? Eval("productname").ToString().Substring(0, 100)+"..." : Eval("productname") %>
                                <asp:Label ID="lblpid" runat="server" Text='<%# Eval("productid")%>' Visible="false"></asp:Label>
                                <asp:Label ID="lblpname" runat="server" Text=' <%# Eval("productname") %>' Visible="false"></asp:Label>
                            </a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                 <asp:LinkButton ID="lnkview" CssClass="lnkview" Visible="false" runat="server" PostBackUrl='<%#onclick_hlnkcategory(Eval("CategoryId"),Eval("categoryname")) %>'></asp:LinkButton>
             

            </div>
        </asp:Panel>
            
    </ItemTemplate>
</asp:Repeater>--%>
<%--sagar commented below code working (stable) get data from xml 30nov2017 for meeting and task ends here--%>

<%--SAGAR COMMENTED THIS FOR REMOVING PROJECT FROM THE FRONT END 28SEPT2017 STARTS HERE--%>
<%-- Universal Post --%>
<%--<asp:Repeater ID="rptcatuniversal" runat="server">
    <ItemTemplate>
    <asp:Panel ID="pnluniversal" runat="server" CssClass="widget box WiseChatWidget wtads">
    <div class="intern-padding">
         <asp:Label ID="lblcatid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.categoryid") %>'
                    Visible="false"></asp:Label>
                <div class="intern-box box-title">
                    <h3><%# DataBinder.Eval(Container, "DataItem.categoryname") %></h3>
                </div>
        <asp:Repeater ID="rptuniversal" runat="server">
            <ItemTemplate>
                <div class="textwidget">--%>
                 <%--   <a href='<%#productUrlrewriting( Eval("productname"), Eval("productid")) %>'><i class="fa fa-square"></i><%# Eval("productname").ToString().Length > 100 ? Eval("productname").ToString().Substring(0, 100)+"..." : Eval("productname") %><br />
                        <span>Posted By : <%#getfullname(Eval("createdby"))%></span>
                    </a>--%>
           <%--     </div>
            </ItemTemplate>
        </asp:Repeater>
         <asp:LinkButton ID="lnkview" CssClass="lnkview" Visible="false" runat="server" PostBackUrl='<%#onclick_hlnkcategory(Eval("CategoryId"),Eval("categoryname")) %>'>View All</asp:LinkButton>
    </div>
    </asp:Panel>
    </ItemTemplate>
</asp:Repeater>--%>
<%--SAGAR COMMENTED THIS FOR REMOVING PROJECTS FROM THE FRONT END 28SEPT2017 ENDS HERE--%>

<!-- Birth Day -->
<%--<birth:birthdays ID="birthday" runat="server"></birth:birthdays>--%>


<!-- News-->
<%--SAGAR COMMENTED THIS FOR REMOVING LATEST NEWS WIDGET DISPLAY FROM THE FRONT END 2OCT2017 STARTS HERE--%>
<%--<ucnews:uxnews ID="uxlatestnews" runat="server" />--%>



<%--SAGAR COMMENTED THIS FOR REMOVING NEWSLETTER FROM THE FRONT END 28SEPT2017 STARTS HERE--%>
<!-- Documents / NEWSLETTERS-->

<%--<asp:Repeater ID="rptcatdoc" runat="server">
    <ItemTemplate>
        <asp:Panel ID="pandoc" runat="server" CssClass="widget box WiseChatWidget">
            <div class="intern-padding">
                <asp:Label ID="lbldocid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.categoryid") %>'
                    Visible="false"></asp:Label>
                <div class="intern-box box-title">
                    <h3>
                        <%# DataBinder.Eval(Container, "DataItem.categoryname") %></h3>
                   
                </div>
                <asp:Repeater ID="rptdoc" runat="server">
                    <ItemTemplate>
                        <div class="textwidget">
                            <a href='<%#productUrlrewriting( Eval("productname"), Eval("productid")) %>'>
                                <img src="images/doc.png" style="display: none;" />
                                <%#getdocext(Eval("filename")) %><%#(DataBinder.Eval(Container, "DataItem.productname")) %></a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                 <asp:LinkButton ID="lnkview" runat="server" CssClass="lnkview" Visible="false" PostBackUrl='<%#onclick_hlnkcategory(Eval("categoryid"),Eval("categoryname")) %>'>View All</asp:LinkButton>
            </div>
        </asp:Panel>
    </ItemTemplate>
</asp:Repeater>--%>

<%--SAGAR COMMENTED THIS FOR REMOVING NEWSLETTER FROM THE FRONT END 28SEPT2017 ENDS HERE--%>



<%--SAGAR COMMENTED THIS FOR REMOVING VIDEO FROM THE FRONT END 28SEPT2017 STARTS HERE--%>
<!-- Video -->
<%--<asp:Repeater ID="rptcatvideo" runat="server">
    <ItemTemplate>
        <asp:Panel ID="pnlvideo" runat="server" CssClass="widget box widget_woffice_tasks_assigned">
            <div class="intern-padding">
                <div class="project-assigned-container">
                    <asp:Label ID="lblvideoid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.categoryid") %>'
                        Visible="false"></asp:Label>
                    <asp:Panel ID="panvideo" runat="server" CssClass="project-assigned-head">
                        <div class="intern-box box-title">
                            <h3><%# DataBinder.Eval(Container, "DataItem.categoryname") %></h3>
                            
                        </div>
                        <div class="gallary-main">
                            <asp:Repeater ID="rptvideo" runat="server">
                                <ItemTemplate>
                                    <div class="gallary-image">--%>
                                     <%--   <a href='<%#productUrlrewriting( Eval("productname"), Eval("productid")) %>' class="texticon tooltip" title='<%# Eval("productname").ToString().Length > 100 ? Eval("productname").ToString().Substring(0, 100)+"..." : Eval("productname") %>'>
                                            <asp:Image ID="videoimg" runat="server"/>
                                            <asp:Panel ID="pnlvideogrid" runat="server" Visible="false">
                                                 <span class="gallary-image"><%#getpostvideo(Eval("videoembed"), Eval("movietrailorcode")) %> </span>
                                           </asp:Panel>
                                        </a>--%>
                                  <%--  </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <asp:LinkButton ID="lnkview" runat="server" CssClass="lnkview" Visible="false" PostBackUrl='<%#onclick_hlnkcategory(Eval("CategoryId"),Eval("categoryname")) %>'>View All</asp:LinkButton>
                    </asp:Panel>
                </div>
            </div>
        </asp:Panel>
    </ItemTemplate>
</asp:Repeater>--%>
<%--SAGAR COMMENTED THIS FOR REMOVING VIDEO FROM THE FRONT END 28SEPT2017 ENDS HERE--%>



<!-- Survey -->
<%--<asp:Panel ID="pnlsurvey" runat="server" CssClass="widget box WiseChatWidget">
    <div class="intern-padding">
        <asp:Label ID="lbcatlid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.surveyid") %>'
            Visible="false"></asp:Label>
        <div class="intern-box box-title">
            <h3>Survey</h3>
        </div>

        <asp:Repeater ID="rptsurvey" runat="server" OnItemDataBound="rptsurvey_ItemDataBound"
            OnItemCommand="rptsurvey_ItemCommand">
            <ItemTemplate>
                <div class="textwidget">
                    <a href='surveydetail.aspx?surveyid=<%# Eval("surveyid")%>'>
                        <i class="fa fa-square"></i>&nbsp;<%# Eval("title") %>
                        <asp:Label ID="lblpid" runat="server" Text='<%# Eval("surveyid")%>' Visible="false"></asp:Label>
                        <asp:Label ID="lblpname" runat="server" Text=' <%# Eval("title") %>' Visible="false"></asp:Label>
                        <asp:LinkButton ID="lnksurvey" runat="server" Visible="false" CssClass="lnksurveyclass" Width="20px" CommandName="lnkdownload"
                            CommandArgument='<%# Eval("surveyid") %>' ToolTip="Download File"><i class="fa fa-download"></i></asp:LinkButton>
                        <asp:LinkButton ID="lnksurveyupl" runat="server" Visible="false" Width="20px" CssClass="lnksurveyclass" CommandName="lnkupload"
                            CommandArgument='<%# Eval("surveyid") %>' ToolTip="Upload File"><i class="fa fa-upload"></i></asp:LinkButton>

                    </a>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

</asp:Panel>--%>

<%--SAGAR COMMENTED THIS FOR REMOVING FUNZONE FROM THE FRONT END 28SEPT2017 STARTS HERE--%>
<!-- Fun zone -->
<%--<asp:Panel ID="panfun" runat="server" CssClass="widget box widget_woffice_tasks_assigned">
    <div class="intern-padding">
        <div class="intern-box box-title">
            <h3>Fun zone</h3>
            
        </div>
        <asp:Repeater ID="rptfun" runat="server">
            <ItemTemplate>
                <div class="textwidget">
                    <a href='<%#productUrlrewriting( Eval("productname"), Eval("productid")) %>'><i class="fa fa-square"></i><%#getsubstr(Eval("shortdescription")) %></a>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <asp:LinkButton ID="lnkview" runat="server" CssClass="lnkview" Visible="false" PostBackUrl='<%#onclick_hlnkcategory("27","Fun zone") %>'>View All</asp:LinkButton>
    </div>
</asp:Panel>--%>
<%--SAGAR COMMENTED THIS FOR REMOVING FUNZONE FROM THE FRONT END 28SEPT2017 ENDS HERE--%>

<%--<asp:Panel ID="panfun" runat="server" CssClass="widget box widget_woffice_tasks_assigned">
    <div class="intern-padding">
        <div class="intern-box box-title">
            <h3>TASK</h3>
            
        </div>
        <asp:Repeater ID="rptfun" runat="server">
            <ItemTemplate>
                <div class="textwidget">
                    <a href='<%#productUrlrewriting( Eval("productname"), Eval("productid")) %>'><i class="fa fa-square"></i><%#getsubstr(Eval("shortdescription")) %></a>
                <asp:label ID="lbltask" runat="server" ></asp:label>

                </div>
            </ItemTemplate>
        </asp:Repeater>
        <asp:LinkButton ID="lnkview" runat="server" CssClass="lnkview" Visible="false" PostBackUrl='<%#onclick_hlnkcategory("27","Fun zone") %>'>View All</asp:LinkButton>
    </div>
</asp:Panel>--%>

 <div class="divonBday">
     <div class="divonBdayHeader">
        <birth:birthdays ID="birthday" runat="server"></birth:birthdays>
    </div>
</div>

<!-- MEEEEETINS -->
<%--meeeeeee --%>
<%--<asp:Repeater ID="rpes" runat="server"  >
              
    <ItemTemplate>
        <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.categoryid") %>' ></asp:Label>

    <asp:Repeater ID="rpes1" runat="server">--%>
  <%--  <%#Eval("productname") %>>
        <%#Eval("shortdescription") %>>
          <%#Eval("createdon") %>>
        <%#Eval("createdby") %>>
          <%#Eval("longdescription") %>>
        <%#Eval("smallimage") %>>--%>
        <%--<ItemTemplate>
            <asp:Label ID="lbcats1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.productname") %>' visible="true"></asp:Label>
            <asp:Label ID="lbleventdate1" runat="server" CssClass="eventdate" Text='<%# Eval("startdate")%>' Font-Bold="false" Visible="true"></asp:Label>
            <%#Eval("productname") %>>
            
        </ItemTemplate>
        </asp:Repeater>
   
    </ItemTemplate>
   
</asp:Repeater>--%>

   

<%--   SAGAR ADDED BELOW working code for  NEW TASK AND MEETINGS PANEL ,recored fetch from database 30nov2017--%>
<%-- meeee
meeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee--%>

<%----------------------------TASK---------------------------------%>
<%--<asp:Panel ID="pantask" runat="server" CssClass="widget box WiseChatWidget">--%>

    <div id="taskdb" style="display:none">
         <div class="intern-padding-task">
             <div class="intern-box-task">
                            <h3> Task</h3>
                           
                        </div>
	<asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="http://localhost/hrms/mycalendar1"> Add</asp:LinkButton>
     <%--<asp:Label ID="lbltaskdb" runat="server" Text="Task"></asp:Label>   --%>
	 <asp:Label ID="lblEmptyRepeater" runat="server" Visible="false" Text="NO TASK FOR TODAY" ></asp:Label>
<asp:Repeater ID="taskS" runat="server" Visible="true" OnPreRender="emptyRepeater_PreRender">
    <ItemTemplate>

       <%-- <asp:Label ID="l" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.productname") %>'> </asp:Label>--%>
	   <!-- <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.createdon","{0:d/M/yyyy }") %>'> </asp:Label>-->
	   
		<%--<a href='<%#productUrlrewriting( Eval("productname"), Eval("productid")) %>'>--%>
		<a href='http://localhost/hrms/mycalendar1'>
		 <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.startdate","{0:d/M/yyyy }") %>'> </asp:Label>
        <asp:Label ID="reminder1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.reminder_time") %>'> </asp:Label>
         <asp:Label ID="l" runat="server" Text='<%# Eval("productname").ToString().Length > 20 ? Eval("productname").ToString().Substring(0, 20)+"..." : Eval("productname") %>'> </asp:Label>
            </a>
       <%-- <asp:Label ID="llll" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.categoryid") %>'
                       ></asp:Label>
        <asp:Label ID="Labellll1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.categoryname") %>'
                       ></asp:Label>--%>
        
<%--       <asp:Label ID="l1" runat="server" Text=' <%# DataBinder.Eval(Container, "DataItem.categoryid") %>'></asp:Label>
        <%# DataBinder.Eval(Container, "DataItem.categoryid") %>
        <%# DataBinder.Eval(Container, "DataItem.categoryname") %>--%>
        <%--<%# Eval("productname").ToString().Length > 100 ? Eval("productname").ToString().Substring(0, 100)+"..." : Eval("productname") %>--%>
    </ItemTemplate>
    <%--<FooterTemplate>
     <asp:LinkButton ID="lnktask" runat="server" CssClass="lnkview" PostBackUrl='<%#onclick_hlnkcategory(Eval("CategoryId"),Eval("categoryname")) %>'>View All</asp:LinkButton></FooterTemplate>--%>
   
</asp:Repeater>

<%--             <div id="s" runat="server">

      <asp:LinkButton ID="lnktask" runat="server" PostBackUrl='<%#onclick_hlnkcategory("46","Tasks") %>'>View All</asp:LinkButton>      </div>   --%>         
        </div>
       <asp:LinkButton ID="lnktask" runat="server" PostBackUrl="http://localhost/hrms/mycalendar1">View All</asp:LinkButton> 
        </div>
   
      <%--  </asp:Panel>--%>
<%--meeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee--%>



    <%--<asp:Panel ID="panmeeting" runat="server" CssClass="widget box WiseChatWidget">--%>
        <div id="meetingdb-1" style="display: none">
			<div class="intern-padding-task">
                        <div class="intern-box-task">
							<!--<h3> Meeting</h3>-->
                            <h3> Idea Central</h3>                           
                        </div>
			                <asp:LinkButton ID="Lk1" style="color: #05568b !important;" runat="server" PostBackUrl="http://localhost/hrms/addideacentral"> Add</asp:LinkButton>
				            <asp:Label ID="lblEmptyRepeater1" runat="server" Visible="false" Text="NO IDEA FOR DISPLAY"></asp:Label> 
                    <asp:Repeater ID="meetingS" runat="server" OnPreRender="emptyRepeater_PreRender1">
                        <ItemTemplate>
	                      <a href='http://localhost/hrms/ps/uuU0JEd0-9BdrjPdeKlAmg=='>
								  <asp:Label ID="reminder131" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.uname") %>'> </asp:Label> 
		                         <asp:Label ID="reminder13" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.productname").ToString().Length > 80 ? DataBinder.Eval(Container, "DataItem.productname").ToString().Substring(0, 77)+"..." : DataBinder.Eval(Container, "DataItem.productname") %>'> </asp:Label> 
								<%--<asp:Label ID="reminder132" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.shortdescription").ToString().Length > 60 ? DataBinder.Eval(Container, "DataItem.shortdescription").ToString().Substring(0, 57)+"..." : DataBinder.Eval(Container, "DataItem.shortdescription") %>'> --%>
								  </asp:Label>
                          </a>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
        <asp:LinkButton ID="lnkmeeting" style="color: #05568b !important;" runat="server" PostBackUrl="http://localhost/hrms/ps/uuU0JEd0-9BdrjPdeKlAmg==">View All</asp:LinkButton> 			 
       </div>


    <div id="meetingdb">

        <div id="divkeypersnolN">
            <%--<a href='<%=ReturnUrl("KeyPersonnel") %>'> --%>
            <a href='<%=ReturnUrl("EmpDirectonHome") %>'>
                <%--<img src="http://localhost/hrms/images/homepage_imgs/KeyPersonnel.jpg" class="imgkeyideacntrlapprsalmyfileN" />--%>
                <img src="http://localhost/hrms/images/homepage_imgs/BB1.jpg" class="imgkeyideacntrlapprsalmyfileN" />
            </a>
            <div class="hcenterText1">
                <h1>Contacts</h1>
            </div>
        </div>

		<div id="divmyfilesN">
            <%--<%=ReturnUrl("KeyPersonnel") %>--%>
           <%--<a href='<%=ReturnUrl("myfiles") %>'> --%>  
            <a  href='<%=ReturnUrl("LeaveonHome") %>'> 
                <%--<img src="http://localhost/hrms/images/homepage_imgs/myfiles.jpg" class="imgkeyideacntrlapprsalmyfileN" /> --%>    
               <img src="http://localhost/hrms/images/homepage_imgs/BB2.jpg" class="imgkeyideacntrlapprsalmyfileN" />       
            </a>
            <div class="hcenterText1">
                <h1 runat="server" id="LeavesHead">Leaves</h1>
            </div>
        
        </div>
		
        
        <div id="divAppraisalN">
            <%--'<%=ReturnUrl("Appraisal") %>'--%>
            <%--<a href='<%=ReturnUrl("Appraisal") %>'>  --%>   
            <a href='<%=ReturnUrl("ProceduresonHome") %>'>     
                <%--<img src="http://localhost/hrms/images/homepage_imgs/Appraisal.jpg" class="imgkeyideacntrlapprsalmyfileN" />  --%>
                <img src="http://localhost/hrms/images/homepage_imgs/BL1.jpg" class="imgkeyideacntrlapprsalmyfileN" />            
            </a>
            <div class="hcenterText1">
                <h1>Policy-Procedures</h1>
            </div>

        </div>
		
		 <div id="divideacentralN">
            <%--<a href='<%=ReturnUrl("ideacntralhome") %>'>--%>
             <%--<a  href='<%=ReturnUrl("TravelonHome")%>'>--%>
             <a href="http://localhost/hrms/ps/9nQ-8bwn0dYnjBaQIq6bTg==">
               <%-- <img src="http://localhost/hrms/images/homepage_imgs/Idea_Central.jpg" class="imgkeyideacntrlapprsalmyfileN" /> --%>
				<%--<img src="http://localhost/hrms/images/homepage_imgs/pm_meets.jpg" class="imgkeyideacntrlapprsalmyfileN" />--%>
                <img src="http://localhost/hrms/images/homepage_imgs/BL2.jpg" class="imgkeyideacntrlapprsalmyfileN" />  
            </a>
            <div class="hcenterText1">
                <h1>Meet Highbarians</h1>
            </div>

        </div>
         
    </div>
        
    <%--</asp:Panel>--%>
  <%-- SAGAR ADDED above working code for NEW TASK AND MEETINGS PANEL ,recored fetch from database 30nov2017--%>


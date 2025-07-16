<%@ Page Title="" Language="C#" MasterPageFile="~/cms.master" AutoEventWireup="true"
    CodeFile="footer.aspx.cs" Inherits="footer" %>

<%--<%@ Register Src="~/Themes/creative1.0/LayoutControls/basicbreadcum.ascx" TagName="basicbreadcrumb"
    TagPrefix="ucbasicbreadcrumb" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
<%--    <ucbasicbreadcrumb:basicbreadcrumb ID="basicbreadcrumb" runat="server" />--%>
        <div class="mainpostwallcat">
        <div class="comments-summery1"> 
                 <span>
                    <a id="gobackbtn"  runat="server" class="aaa" ></a>
                </span>  
        </div>
        </div>


    <div id="staticfooter" runat="server">
    </div>
    <asp:Panel ID="pnllinks" runat="server">
    <div class="aboutusdiv1" style="min-height: 300px;">

       <%-- Jayesh Commented below code to hide Related links menu section 3oct2017--%>
	<div class="related-links">Related Links</div>
        <div class="aboutus1">
            <ul>
                <asp:Repeater ID="rptsubcat" runat="server">
                    <ItemTemplate>
                        <li><a href='<%#productUrlrewriting(Eval("short_desc"))%>' title='<%#ProcessUrl(Eval("categoryname").ToString()) %>'><i aria-hidden="true" class="fa fa-angle-double-right"></i><%# Eval("categoryname")%></a></li>
                        
                    </ItemTemplate>
                </asp:Repeater>
          
       <%-- Jayesh Commented above code to hide Related links menu section 3oct2017--%>
          </ul>
        </div>
    </div>
        </asp:Panel>
        <asp:Literal ID="ltcss" runat="server" Visible="false"></asp:Literal>
</asp:Content>

<%@ Control Language="C#" AutoEventWireup="true" CodeFile="testimonial.ascx.cs" Inherits="themes_creative1_LayoutControls_testimonial" %>
<asp:Panel ID="pnltestimonial" runat="server" Visible="false">
<div class="testimonialmaindiv">
<div class="testimonial-main">
<div class="testimonial-heading">Testimonials</div>
<div id="newsticker1">
<ul>
<asp:Repeater ID="rptnews" runat="server">
<ItemTemplate>
<li>
<div class="testimonial">
<div class="testimonialimage">
<img src='<%=ConfigurationManager.AppSettings["adminsitepath"]%>images/testimonial/<%# Eval("imagename") %>' alt="image" title='<%# Eval("name") %>' /></div>
<div class="testimonialtitle">
<%# Eval("name")%>  <%--</a>--%></div>
<div class="testimonialcontent">
  	<font>"</font>
    <asp:Label ID="Label2" runat="server" Text='<%# Eval("comments")%>'></asp:Label>
  	<font>"</font>
</div>
</div>
<div class="testimonialdivider">
    	<span></span>
	</div>
</li>
</ItemTemplate>
</asp:Repeater>
</ul>
</div>
</div>
<div class="completelist">
	<a href="testimonial.aspx" class="completelistbtn" title="Testimonial">Complete list</a>
</div>
</div>
</asp:Panel>

<%@ Control Language="C#" AutoEventWireup="true" CodeFile="interlace.ascx.cs" Inherits="themes_creative1_0_LayoutControls_interlace" %>


<div id="banner">
	<ul id="bannerslides">
		<asp:Repeater ID="rptbanner" runat="server">
			<ItemTemplate>
				<li>
					<a href='<%# DataBinder.Eval(Container.DataItem,"url")%>' target="_blank" title='<%# Eval("altname") %>'>
					<img vspace="0"  alt="side" border="0" src='<%=ConfigurationManager.AppSettings["adminsitepath"]%>images/banner/<%# Eval("imagename") %>' />
					</a>
				</li>
			</ItemTemplate>
		</asp:Repeater>
	</ul>
</div>
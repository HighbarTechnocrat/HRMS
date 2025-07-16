<%@ Page Language="C#" MasterPageFile="~/cms.master" AutoEventWireup="true" CodeFile="newsdetail.aspx.cs" Inherits="newsdetail" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <link href="<%=ReturnUrl("css") %>movie-detail/movie-detail.css" rel="stylesheet" type="text/css"  />
    <style>.sitelogo{left:0;top:0;}.productsummerytitle{border:none;float:none;}.productname{float:none;width:auto;}</style>
    <div id="productsummery" class="productsummery">
        <div class="Default">
        <div class="productsummerytitle">
            <asp:Repeater ID="rptrnewsdetail" runat="server">
                <ItemTemplate>
                     <div class="productname wow fadeInDown animated" style="visibility: visible; animation-name: fadeInDown;">
                    <asp:Label ID="lblnewsttl" runat="server" Text='<%# Bind("newstitle") %>' ></asp:Label> (<asp:Label ID="lbldate" runat="server" Text='<%#((DateTime)DataBinder.Eval(Container, "DataItem.newsdate")).ToString("dd-MMM-yyyy") %>'></asp:Label>)</div>
                                <div class="synopsis wow fadeInDown animated" style="visibility: visible; animation-name: fadeInDown;">
        <article class="readmore" style="max-height: none;"><%# DataBinder.Eval(Container, "DataItem.newsdesclong")%> 
        </article>
           <%-- Jayesh_Sagar commented(commented code of image with bug ) and added above code to display news images on newsdetail.aspx page 21nov2017--%>
        <%--<p> <%# DataBinder.Eval(Container, "DataItem.newsimage")%></p>--%>
                        <%--Image:--%><p><img id="img1" runat="server" visible="false" src='<%#getuserimage(Eval("newsimage")) %>' /></p>
    </div>
           <%--Jayesh_Sagar commented(commented code of image with bug )  added above code to display news images on newsdetail.aspx page 21nov2017--%>
<%--                    
                </ItemTemplate>
                </asp:Repeater>
               

            </div>
            </div>
        </div>
<div  class="content-top-cfc">
      
      <div class="faq-box-container-cfc">
      <div style="text-align:right;display:none;">
       <asp:LinkButton  ID="lbtnback" runat="server" OnClick="lbtnback_Click"  CausesValidation="False" >Back</asp:LinkButton>
       </div>
<fieldset>
              
 <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
          <tr>
            
            <td valign="top">
              <table cellpadding="0" cellspacing="0" width="100%">
              <tr><td class="productbg" valign="top">
               <asp:Repeater ID="rptrnewsdetail2" runat="server">
         <ItemTemplate>
     <table width="98%" cellpadding="0" cellspacing="0" runat="server" id="tbl">
    
        <tr>
        <td width="8"></td>
        <td align="left">
         <font class="ineercontent"><strong>
         </strong> 
          </font>
                                                    
          <%--  <%# DataBinder.Eval(Container, "DataItem.Newstitle")%> --%>
        </td>
         </tr>
        <tr>
        <td width="8"></td>
        <td align="left"><br />
      
                 
        </td>
        
        </tr>
         <tr><td width="8"></td><td height="10px">&nbsp;</td></tr>
        <tr align="right"><td width="8"></td>
        <td align ="right">
       
        </td></tr>
     </table>
     <br />
     </ItemTemplate>
        </asp:Repeater>
    </td></tr>
              </table>
		</td>
          </tr>
        </table>
      </fieldset></div></div>
</asp:Content>


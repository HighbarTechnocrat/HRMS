<%@ Control Language="C#" AutoEventWireup="true" CodeFile="commentlist.ascx.cs" Inherits="themes_creative1_LayoutControls_commentlist" %>
<div  style="width:auto;float:left;padding:10px;min-width:500px;">
     <asp:Repeater ID="rptcomments" runat="server" onitemcommand="rptcomments_ItemCommand">
        <ItemTemplate>
            <div  style="float:left;width:500px;">
            <div style="width:100px;float:left;">
                <div style="width:100px;float:left;">
                <asp:HyperLink ID="lnkusername" runat="server" Text='<%# Eval("fullname")%>' ></asp:HyperLink>
                </div>
                <div style="width:100px;float:left;">
                <asp:HyperLink ID="lnkimage" runat="server"><asp:Image id="imgprofile" runat="server" width="100" height="100" alt=''></asp:Image></asp:HyperLink>
                </div>
            </div>
                <div style="width:380px;float:left;margin-left:20px;margin-top:20px;word-wrap:break-word;">
                <asp:Label ID="lblcomment" runat="server" Text='<%# Eval("commenttext")%>'></asp:Label>
                </div>
            </div>
        </ItemTemplate>
     </asp:Repeater>
     </div>
     <div  style="float:left;width:600px;padding:10px;">
       <div style="width:100px;float:left;">
                <div style="width:100px;float:left;">
                <asp:HyperLink ID="lnkusername2" runat="server" ></asp:HyperLink>
                </div>
                <div style="width:100px;float:left;">
                <asp:HyperLink ID="lnkimage2" runat="server"><img id="imgprofile2" runat="server" width="100" height="100" alt=''/></asp:HyperLink>
                </div>
                
      </div>
      <div style="width:380px;float:left;margin-left:20px;margin-top:20px;">
          <asp:TextBox ID="txtcomment" runat="server" Width="300px" TextMode="MultiLine"></asp:TextBox><br />
<asp:Label ID="lblcount" runat="server" ForeColor="#cccccc"></asp:Label>
<br/>        
 <asp:Label ID="lblerror" runat="server" Visible="false" ForeColor="Red"></asp:Label></div>
      <div style="width:380px;float:left;margin-left:20px;margin-top:20px;">
          <asp:Button ID="btnsubmit" runat="server" Text="Submit" 
              onclick="btnsubmit_Click"  />
      </div>
     </div>

<%@ Control Language="C#" AutoEventWireup="true" CodeFile="pdsumtitle.ascx.cs" Inherits="Themes_SecondTheme_LayoutControls_pdnew_pdsumtitle" %>
<div class="productsummerytitle">
    <%--SAGAR ADDED STYLE IN BELOW LINE FOR TITLE OF ONE POST 120CT2017--%>
    <div class="productname fadeInDown" style="color: #0366ba;font-size: 22px;font-weight: 300;text-align: left;">
        <asp:Label ID="lblproductname" runat="server" Text=""></asp:Label>
    </div>


    <div class="date-director fadeInDown" style="display:none;"> 
        <asp:Label ID="lblyear" runat="server" Text="Label" CssClass="productyear"></asp:Label>
        <span id="director" runat="server">Directed by </span><a id="lnkdirector" runat="server"><asp:Label ID="lbldirectorname" runat="server" Text=""></asp:Label></a>
    </div>

     <div id="multiplepstimg" runat="server" visible="false" >
               <div class="multiimagediv">    
                <asp:Repeater ID="rptmulimage" runat="server">
                <ItemTemplate>
                 <a id="downloadmulimg1" runat="server" target="_blank" href='<%#getImageUrl(Eval("filename")) %>' class="mulimglink" >
                 <asp:Image ID="imgbanner1" runat="server" AlternateText='<%# DataBinder.Eval(Container, "DataItem.filename") %>' CssClass="universalmultiimg" ToolTip="Click to view image in full size." /></a>
                 </ItemTemplate>
                 </asp:Repeater>
              </div> 
         </div>
      
   
    <div id="pstimg" runat="server" visible="false" class="synopsis fadeInDown animated animated animated" style="visibility: visible; animation-name: fadeInDown;">
        <a id="downloadimg" runat="server" target="_blank">
        <asp:Image ID="imgbanner" runat="server" style="max-width: 100%;" Visible="false" />
        </a>
        <asp:Panel ID="pnlimg" runat="server" Visible="false">
         <div class="date-director wow fadeInDown" >
            <asp:LinkButton ID="imgdownload" runat="server" OnClick="imgdownload_Click" CssClass="submitbtndiv">
            <asp:Label ID="lblimgname" runat="server" Text="Download Image" CssClass="producer-name"></asp:Label></asp:LinkButton></div></asp:Panel>
    </div>
 
    
    <div id="pstvideo" runat="server" class="synopsis  fadeInDown animated animated animated" style="visibility: visible; animation-name: fadeInDown;">
        <div id="videodisplay" runat="server"></div>
    </div>
   

    <div class="synopsis fadeInDown">
        <article class="readmore"><asp:Label ID="lbldescription" runat="server" Text=""></asp:Label>
           
            <div class="date-director fadeInDown" id="div3" runat="server" ><span class="producer-heading">
                <asp:Image ID="extimg" runat="server" /></span><a id="download" runat="server"><asp:Label ID="fileload" runat="server" CssClass="producer-name" ToolTip="Click here to download file"></asp:Label></a>
            </div>

             <div id="multipledoc" runat="server" visible="false" >
               <div class="multipledocdiv">    
                <asp:Repeater ID="rptdoc" runat="server">
                <ItemTemplate>
                 <div class="date-director fadeInDown" id="divmuldoc" runat="server" ><span class="producer-heading">
                <asp:Image ID="extimg1" runat="server" AlternateText='<%# DataBinder.Eval(Container, "DataItem.filename") %>'/></span><a id="downloaddoc" runat="server"  href='<%#getFileUrl(Eval("filename")) %>'><asp:Label ID="filename1" runat="server" CssClass="producer-name" ToolTip="Click here to download file" Text='<%# DataBinder.Eval(Container, "DataItem.filename") %>'></asp:Label></a>
            </div>
                 </ItemTemplate>
                 </asp:Repeater>
              </div> 
         </div>

            <div class="date-director fadeInDown" id="div1" runat="server" > <span class="producer-heading"><%--<i class="fa fa-edit"></i>--%>
               <%-- SAGAR COMMENTED BELOW LINE FOR REMOVING POSTED BY AND NAME OF THE USER WHO POSTED THE POST 9OCT2017--%>
                <%-- Posted By </span><a id="lnkuser" runat="server"><asp:Image ID="profileimg" runat="server"  class="postuser" />--%>
                <asp:Label ID="lbluser" runat="server" CssClass="producer-name"></asp:Label> </a></div>
            <div class="date-post-start-end">
             <div class="producerdiv fadeInDown" id="div2" runat="server" ><span class="event-dates">
                <%-- SAGAR COMMENTED THIS FOR REMOVING POSTED DATE ,START DATE AND END DATE OF SINGLE POST 12OCT2017 STARTS HERE--%>
                 <%--<i class="fa fa-calendar"></i> Posted On</span><asp:Label ID="lbldate" runat="server" CssClass="producer-name" Visible="false"></asp:Label><asp:Label ID="lblpdate" runat="server" CssClass="producer-heading1"></asp:Label><asp:Label ID="lblpmonth" runat="server" CssClass="producer6"></asp:Label></div>
             <div class="producerdiv fadeInDown" id="div4" runat="server" > <span class="event-dates"><i class="fa fa-calendar"></i> Start Date</span> <asp:Label ID="lblsdate" runat="server" CssClass="producer-heading1"></asp:Label><asp:Label ID="lblsmonth" runat="server" CssClass="producer6"></asp:Label></div>
             <div class="producerdiv fadeInDown" id="div5" runat="server" > <span class="event-dates"><i class="fa fa-calendar"></i> End Date</span> <asp:Label ID="lbledate" runat="server" CssClass="producer-heading1"></asp:Label><asp:Label ID="lblemonth" runat="server" CssClass="producer6"></asp:Label>--%>
                  <%-- SAGAR COMMENTED THIS FOR REMOVING POSTED DATE ,START DATE AND END DATE OF SINGLE POST 12OCT2017 ENDS HERE--%>
                 </div>
</div>
           

            <div class="producerdiv wow fadeInDown" id="divproducer" runat="server" visible="false"> <span class="producer-heading">Producer: </span><asp:Label ID="lblproducer" runat="server" CssClass="producer-name"></asp:Label> </div>

<div class="actordiv wow fadeInDown" id="divactor" runat="server" style="display:none;"> <span class="actor-heading">Actors: </span><asp:Label ID="lblactors" runat="server" CssClass="actor-name"></asp:Label>
</div>
        </article>
        
    </div>
    <div class="watchbtns" style="display:none;">
      <div class="watchtrailor wow slideInLeft" id="divtrailer" runat="server"> 
          <asp:LinkButton ID="lnktrailor" runat="server" ToolTip="View Trailer" OnClick="lnktrailor_Click">
          <i class="fa fa-film"></i>View Trailer</asp:LinkButton>
      </div>
      <div class="watchadmin wow slideInDown" id="watchadmin" runat="server" visible="false">
          <asp:LinkButton ID="lnkadminwatch" runat="server" ToolTip="Watch Movie" OnClick="lnkadminwatch_Click">
              <i class="fa fa-play"></i>Watch Movie admin</asp:LinkButton>
      </div>
      <div class="watchmovie wow slideInRight" id="divwatch" runat="server">
          <asp:LinkButton ID="lnkprodenquiry" runat="server" ToolTip="Watch Movie" OnClick="lnkprodenquiry_Click">
              <i class="fa fa-play"></i>Watch Movie</asp:LinkButton>
          <span id="spanpkg" runat="server" visible="false">
              <span>
                <div class="pakgsubscript" id="divsub" runat="server">
                    <asp:LinkButton ID="lnksubscribe" runat="server" Visible="true" CssClass="pakgsubscriptbtn" OnClick="lnksubscribe_click"></asp:LinkButton>
                </div>
              </span>
          </span>
      </div>
    </div>
</div>

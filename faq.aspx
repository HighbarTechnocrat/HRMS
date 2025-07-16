<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="faq.aspx.cs" Inherits="newsdetails" %>

<%--<%@ Register Src="~/Themes/creative1.0/LayoutControls/basicbreadcum.ascx" TagName="basicbreadcrumb"
    TagPrefix="ucbasicbreadcrumb" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
         <link href="<%=ReturnUrl("css") %>news/latestnews2.css" rel="stylesheet" type="text/css"  />
     <link href="<%=ReturnUrl("css") %>includes/mywall.css" rel="stylesheet" type="text/css" />
    <link href="<%=ReturnUrl("css") %>includes/accordion.css" rel="stylesheet" type="text/css" />
    <link href="<%=ReturnUrl("css") %>notification/notification.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function SwitchMenu(obj) {
            if (document.getElementById) {
                var el = document.getElementById(obj);
                var ar = document.getElementById("masterdiv").getElementsByTagName("div");
                if (el.style.display != "block") {
                    for (var i = 0; i < ar.length; i++) {
                        if (ar[i].className == "submenu")
                            ar[i].style.display = "none";
                    }
                    el.style.display = "block";
                } else {
                    el.style.display = "none";
                }
            }
        }
    </script>
    <div class="mainpostwallcat1">
    <div class="comments-summery2">
    <div class="userposts"> <span >Faq's</span> </div>
        <asp:Panel ID="pnlfaq" runat="server" Visible="false">
              <div class="breadcrumbdiv">
          <div id="staticfooter1">
             <div class=" commonpagesheading1">
                  <asp:Label runat="server" ID="Label2" Text="Frequently Asked Questions"></asp:Label>
             </div>
          </div>
     </div>
    
    <div style="display:none;" >
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    </div> 

    <div class="faqcms">
       <div class="accordion blue" id="accordion">   
          <asp:Repeater ID="Repeatercat"  runat="server" OnItemDataBound="DateRepeater_ItemDataBound">
          <ItemTemplate>
                                         
                   <div class="accordion-item">

                           <div class="accordion-header"  onclick="SwitchMenu('<%# Eval("faqcatid") %>')" >
                                 <%# Eval("categoryname") %>                                                           
						         <span class="accordion-item-arrow"></span>
                           </div>
                             

                             <asp:Repeater ID="SubRepeater"  runat="server">
                                            <ItemTemplate>
                                 <div class="accordion-content" style="display: none;">              

                                              <!-- <div class="accordion blue" id="accordion">  -->                               
                                               <div class="accordion-item">

                                                <ul >
                                                        <li style="height: 22px;position:left;" onclick="SwitchMenu('<%# Eval("faqid") %>')">
                                                        <img alt="" src="~/themes/creative1.0/images/bullet1.gif" />
                                                           <b>
                                                              <a href="javascript:void(0)" style="text-decoration:none">
                                     
                                                              <asp:Label ID="lblquestion"   runat="server" Text='<%# Eval("question") %>'
                                                               Font-Bold="true" ForeColor=#070000></asp:Label></a>
                                                           </b>
                                                           <br />
                                                        </li>
                                                        <li></li>
                                                        
                                                        <li style="position:left;"  class="masterdiv-li">
                                                      
                                                        <div id='<%# Eval("faqid") %>' style=" font-size: 18px;">
                                                              <b>  Ans :</b>&nbsp;<%# DataBinder.Eval(Container, "DataItem.answer")%></div>
                                                       </li>
                                                 
                                                </ul>

                                                <div style="display: none;">
                                                     <div  class="accordian-header">
                                                         <%# Eval("question") %>
						                                 <span class="accordion-item-arrow"></span>
                                                         </div>

                                                     <div class="accordion-content" Style="display:none;">
                                                         <b>Ans : </b>&nbsp;<%# DataBinder.Eval(Container, "DataItem.answer")%></div>
                                                </div>
                                                
                                                </div>                                        
                                              <!--</div>-->
                                        <br />

                                 </div>
  
                             </ItemTemplate>
                             </asp:Repeater>
                       </div>
                             
                </ItemTemplate>
                </asp:Repeater>
             
            </div>
         </div>
            
             </asp:Panel>

    </div>
    </div>

</asp:Content>


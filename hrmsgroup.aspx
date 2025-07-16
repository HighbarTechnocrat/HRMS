<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hccgroup1.aspx.cs" Inherits="hccgroup1" %>--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/cms.master" AutoEventWireup="true" CodeFile="hrmsgroup.aspx.cs" Inherits="hrmsgroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <link href="<%=ReturnUrl("css") %>profile/mprofile.css" rel="stylesheet" type="text/css" />
   
            <div class="userposts"><span>Highbar Group</span> </div>
                <div class="mainpostwallcat">
                     <div class="comments-summery1"> 
                     <span>
                        <a id="gobackbtn" href='<%=ReturnUrl("KnowledgeCentonHome")%>' class="aaa" > Knowledge Center </a>
                    </span>
                     </div>
                </div>

		<div class="hrmsgroup">
            <ul>
                  <li><a href="files/hrmsgroup/HCC E&C.pdf#toolbar=0" target="_blank">HCC E&C</a><!--<i class="fa fa-plus" id="hccgroupnew" aria-hidden="true"></i>--></li>
				  <li><a href="files/hrmsgroup/HCC Infra.pdf#toolbar=0" target="_blank">HCC Infrastructure</a><!--<i class="fa fa-plus" id="hccgroupnew" aria-hidden="true"></i>--></li>
                <%-- Commneted  <li><a href="files/hrmsgroup/Lavasa Corporation.pdf#toolbar=0" target="_blank">Lavasa</a><i class="fa fa-plus" id="hccgroupnew" aria-hidden="true"></i></li>  --%>				
                <li><a href="files/hrmsgroup/Steiner.pdf#toolbar=0" target="_blank">Steiner</a><!--<i class="fa fa-plus" id="hccgroupnew" aria-hidden="true"></i>--></li>
				 <%-- <li><a href="files/hrmsgroup/Steiner India Limited.pdf#toolbar=0" target="_blank">Steiner India</a><i class="fa fa-plus" id="hccgroupnew" aria-hidden="true"></i></li>
                
                <li><a href="files/hrmsgroup/Charosa Wineries Limited.pdf#toolbar=0" target="_blank">Charosa Wineries</a><i class="fa fa-plus" id="hccgroupnew" aria-hidden="true"></i></li>--%>	
            </ul>
       
        
          
         
        </div>
     
</asp:Content>




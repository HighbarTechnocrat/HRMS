<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hccgroup1.aspx.cs" Inherits="hccgroup1" %>--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/cms.master" AutoEventWireup="true" CodeFile="cc.aspx.cs" Inherits="hrmsgroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <link href="<%=ReturnUrl("css") %>profile/mprofile.css" rel="stylesheet" type="text/css" />   
        <div class="userposts"><span>Policy & Procedure : Corporate Communications</span> </div>      
         <div class="mainpostwallcat">
                    <div class="comments-summery1"> 
                     <span>
                        <a id="gobackbtn" href='<%=ReturnUrl("ProceduresonHome")%>'  class="aaa" > Policy & Procedure </a>
                    </span>
                    </div>
        </div>

		<div class="half">
          
                 <div class="tab blue" > 
             <span style="font-size: 10pt;">
                    <strong><label for="tab-four"><a  href='<%=ReturnUrl("hccurlmain")%>/brandguidelines' style="color: #fff;">Brand Guidelines</a></label></strong></span> </div>
				  <div class="tab blue" > 
             <span style="font-size: 10pt;">
                    <strong><label for="tab-four"><a  href='<%=ReturnUrl("hccurlmain")%>/grouppresentation' style="color: #fff;">Group Presentations</a></label></strong></span> </div>
                  
            
        </div>
</asp:Content>




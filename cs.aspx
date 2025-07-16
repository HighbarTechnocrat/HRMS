<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hccgroup1.aspx.cs" Inherits="hccgroup1" %>--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/cms.master" AutoEventWireup="true" CodeFile="cs.aspx.cs" Inherits="hrmsgroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <link href="<%=ReturnUrl("css") %>profile/mprofile.css" rel="stylesheet" type="text/css" />   
        <div class="userposts"><span>Policy & Procedure : Company Secretarial</span> </div>      
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
                    <strong><label for="tab-four"><a  href='<%=ReturnUrl("hccurlmain")%>/files/CS/Insider Trading-Internal Circular-Signed.pdf' style="color: #fff;">Insider Trading – Internal Circular</a></label></strong></span> </div>
				  <div class="tab blue" > 
             <span style="font-size: 10pt;">
                    <strong><label for="tab-four"><a  href='<%=ReturnUrl("hccurlmain")%>/files/CS/CODE OF CONDUCT FOR PREVENTION OF INSIDER TRADING.pdf' style="color: #fff;">Code of Conduct for Prevention of Insider Trading</a></label></strong></span> </div>
                   <div class="tab blue" > 
                        <span style="font-size: 10pt;">
                             <strong>
                                 <label for="tab-four"><a  href='<%=ReturnUrl("hccurlmain")%>/files/CS/FAIR DISCLOSURE OF UNPUBLISHED PRICE SENSITIVE INFORMATION.pdf' style="color: #fff;">Fair disclosure of Unpublished Price Sensitive Information</a></label>
                             </strong>
                        </span> 


                   </div>

                    <div class="tab blue" > 
                        <span style="font-size: 10pt;">
                             <strong>
                                 <label for="tab-four"><a  href='<%=ReturnUrl("hccurlmain")%>/files/CS/Power of Attorney Policy.PDF' style="color: #fff;">Power of Attorney Policy</a></label>
                             </strong>
                        </span> 


                   </div>
                  
            
        </div>
</asp:Content>




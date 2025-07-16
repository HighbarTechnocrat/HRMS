<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hccgroup1.aspx.cs" Inherits="hccgroup1" %>--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/cms.master" AutoEventWireup="true" CodeFile="IMS.aspx.cs" Inherits="hrmsgroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <link href="<%=ReturnUrl("css") %>profile/mprofile.css" rel="stylesheet" type="text/css" />   
        <div class="userposts"><span>Policy & Procedure : Integrated Management System</span> </div>  
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
                    <strong><label for="tab-four"><a  href='<%=ReturnUrl("hccurlmain")%>/strategicdocuments'  style="color: #fff;">Strategic Documents</a></label></strong></span> </div>
				  <div class="tab blue" > 
             <span style="font-size: 10pt;">
                    <strong><label for="tab-four"><a  href='<%=ReturnUrl("hccurlmain")%>/corporatemanual' style="color: #fff;">Corporate Manual</a></label></strong></span> </div>
                  <div class="tab blue" style="display:none"> 
             <span style="font-size: 10pt;">
                    <strong><label for="tab-four"><a href='<%=ReturnUrl("hccurlmain")%>/Procedures.aspx' style="color: #fff;">Procedures</a></label></strong></span> </div>
                  <div class="tab blue" style="display:none" > 
             <span style="font-size: 10pt;">
                    <strong><label for="tab-four"><a href='<%=ReturnUrl("hccurlmain")%>/contextoforganization' style="color: #fff;">Context of Organization</a></label></strong></span> </div>
                  <div class="tab blue" > 
             <span style="font-size: 10pt;">
                    <strong><label for="tab-four"><a href='<%=ReturnUrl("hccurlmain")%>/OrganizationalKnowledge.aspx' style="color: #fff;">Organizational Knowledge</a></label></strong></span> </div>
                  <div class="tab blue" > 
             <span style="font-size: 10pt;">
                    <strong><label for="tab-four"><a  href='<%=ReturnUrl("hccurlmain")%>/safetyawareness' style="color: #fff;">Safety Awareness</a></label></strong></span> </div>
                  <div class="tab blue" > 
             <span style="font-size: 10pt;">
                    <strong><label for="tab-four"><a href='<%=ReturnUrl("hccurlmain")%>/environmentknowledge' style="color: #fff;">Environment Awareness</a></label></strong></span> </div>
               
            
        </div>
 
</asp:Content>




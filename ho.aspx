<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hccgroup1.aspx.cs" Inherits="hccgroup1" %>--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/cms.master" AutoEventWireup="true" CodeFile="ho.aspx.cs" Inherits="ho" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <link href="<%=ReturnUrl("css") %>profile/mprofile.css" rel="stylesheet" type="text/css" />   
        <div class="userposts"><span>Head Office</span> </div>
            <div class="mainpostwallcat">
                     <div class="comments-summery1"> 
                     <span>
                        <a id="gobackbtn" href='<%=ReturnUrl("KnowledgeCentonHome")%>' class="aaa" > Knowledge Center </a>
                    </span>
                     </div>
              </div>

        <div class="projectmenu" >
            <ul >
                <li><a href="files/HO/Account Finance and Taxation.pdf">Accounts, Finance & Taxation</a><!--<i class="fa fa-plus" id="holist" aria-hidden="true"></i>--></li>
                <li><a href="files/HO/Business Development.pdf">Business Development </a><!--<i class="fa fa-plus" id="holist" aria-hidden="true"></i>--></li>
				 <li><a href="files/HO/Central Projects Planning & Monitoring.pdf">Central Projects Planning & Monitoring</a><!--<i class="fa fa-plus" id="holist" aria-hidden="true"></i>--></li>
                 <li><a href="files/HO/Company Secretarial.pdf">Company Secretarial</a><!--<i class="fa fa-plus" id="holist" aria-hidden="true"></i>--></li>
				<li><a href="files/HO/Contracts & Claims.pdf">Contracts & Claims </a><!--<i class="fa fa-plus" id="holist" aria-hidden="true"></i>--></li>
				<!--<li><a target="_blank"><embed src="files/HO/Corporate Communication.pdf#toolbar=0&navpanes=0&scrollbar=0" width="500" height="500"> Corporate Communication </a><i class="fa fa-plus" id="holist" aria-hidden="true"></i></li>-->
				<!--<li><a href="files/HO/Corporate Communication.pdf">Corporate Communication </a><i class="fa fa-plus" id="holist" aria-hidden="true"></i></li>-->
                <li><a href="files/HO/Corporate Communications.pdf">Corporate Communications </a><!--<i class="fa fa-plus" id="holist" aria-hidden="true"></i>--></li>
                <li><a href="files/HO/Corporate Social Responsibility.pdf">Corporate Social Responsibility </a><!--<i class="fa fa-plus" id="holist" aria-hidden="true"></i>--></li>
                <li><a href="files/HO/Corporate Office Services.pdf">Corporate Office Services</a><!--<i class="fa fa-plus" id="holist" aria-hidden="true"></i>--></li>
                <li><a href="files/HO/Engineering Management.pdf">Engineering Management</a><!--<i class="fa fa-plus" id="holist" aria-hidden="true"></i>--></li>
                <li><a href="files/HO/Equipment.pdf">Equipment</a><!--<i class="fa fa-plus" id="holist" id="holist" aria-hidden="true"></i>--></li>
                 <li><a href="files/HO/Human Resources.pdf">Human Resources</a><!--<i class="fa fa-plus" id="holist" aria-hidden="true"></i>--></li>
                 <li><a href="files/HO/IMS.pdf">Integrated Management System</a><!--<i class="fa fa-plus" id="holist" aria-hidden="true"></i>--></li>
                 <li><a href="files/HO/Information Systems.pdf">Information Systems</a><!--<i class="fa fa-plus" id="holist" aria-hidden="true"></i>--></li>
                 <li><a href="files/HO/Internal Audit.pdf">Internal Audit</a><!--<i class="fa fa-plus" id="holist" aria-hidden="true"></i>--></li>
                 <li><a href="files/HO/Legal.pdf"> Legal</a><!--<i class="fa fa-plus" id="holist" aria-hidden="true"></i>--></li>
                 <li><a href="files/HO/Operations.pdf">Operations</a><!--<i class="fa fa-plus" id="holist" aria-hidden="true"> </i>--></li>
                <!-- <li><a href="#">Project Monitoring & Control</a><!--<i class="fa fa-plus" id="holist" aria-hidden="true"> </i>--></li>
                 <li><a href="#">Project Planning</a><!--<i class="fa fa-plus" id="holist" aria-hidden="true">         </i>--></li>
                 <li><a href="files/HO/Procurement & Subcontracts.pdf">Procurement & Subcontract</a><!--<i class="fa fa-plus" id="holist" aria-hidden="true">
                </i>--></li>                
				 <li><a href="files/HO/Tendering.pdf">Tendering</a><!--<i class="fa fa-plus" id="holist" aria-hidden="true">           </i>--></li>
				<!-- Add New article code above -->
            </ul>
        </div>
        
</asp:Content>

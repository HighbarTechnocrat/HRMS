<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hccgroup1.aspx.cs" Inherits="hccgroup1" %>--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="annualreports.aspx.cs" Inherits="annualreports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <link href="<%=ReturnUrl("css") %>profile/mprofile.css" rel="stylesheet" type="text/css" />
   
            <div class="userposts"><span>Annual Reports</span> </div>
        <div class="mainpostwallcat">
                    <div class="comments-summery1"> 
                    <span>
                    <a id="gobackbtn" href='<%=ReturnUrl("CommunicationonHome")%>' class="aaa" > Communications </a>
                </span></div></div>
			
         <div class="annualreports">
            <ul>
                <!--<li><a href="#" class="financialresultsheadertitle"> Year </a><a href="#" class="financialresultsheaderyear">Title </a><%--<i class="fa fa-plus" aria-hidden="true"> </i></li>--%>      -->
                
               <li><!--<a href="#" class="newfinancialresultstitle">2017-18 </a>--><a href="files\Annual Report\HCC-Annual-Report-FY-2018-19.pdf" class="newfinancialresultsyear">Annual Report FY 2018-19</a><%--<i class="fa fa-plus" aria-hidden="true"></i></li>--%>			  
			   <li><!--<a href="#" class="newfinancialresultstitle">2017-18 </a>--><a href="files\Annual Report\HCC-Annual-Report-FY-2017-18.pdf" class="newfinancialresultsyear">Annual Report FY 2017-18</a><%--<i class="fa fa-plus" aria-hidden="true"></i></li>--%>
			   <li><!--<a href="#" class="newfinancialresultstitle">2016-17 </a>--><a href="files\Annual Report\HCC-Annual-Report-FY-2016-17.pdf" class="newfinancialresultsyear">Annual Report FY 2016-17</a><%--<i class="fa fa-plus" aria-hidden="true"></i></li>--%>
				<li><!--<a href="#" class="newfinancialresultstitle">2015-16 </a>--><a href="files\Annual Report\HCC-Annual-Report-FY-2015-16.pdf" class="newfinancialresultsyear">Annual Report FY 2015-16</a><%--<i class="fa fa-plus" aria-hidden="true"></i></li>--%>
			  
            </ul>
        </div>
       
</asp:Content>


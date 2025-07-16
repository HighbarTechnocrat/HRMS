<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hccgroup1.aspx.cs" Inherits="hccgroup1" %>--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="financialresults.aspx.cs" Inherits="financialresults" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <link href="<%=ReturnUrl("css") %>profile/mprofile.css" rel="stylesheet" type="text/css" />
   
            <div class="userposts"><span>financial Results</span> </div>
                <div class="mainpostwallcat">
                    <div class="comments-summery1"> 
                    <span>
                    <a id="gobackbtn" href='<%=ReturnUrl("CommunicationonHome")%>' class="aaa" > Communications </a>
                </span></div></div>

			
         <div class="financialresults">
            <ul>
                    
                    <li><a href="#" class="financialresultsheadertitle"> Year </a><a href="#" class="financialresultsheaderauthor">Date of Issuance</a><a href="#" class="financialresultsheaderyear">Results</a><%--<i class="fa fa-plus" aria-hidden="true"></i>--%></li>
                <li><a href="#" class="newfinancialresultstitle">2019 </a><a href="#" class="financialresultsauthor">November 14</a><a href="files\Financial Results\Q2 2019-20.pdf" class="newfinancialresultsyear">Q2 2019-20</a><%--<i class="fa fa-plus" aria-hidden="true"></i></li>--%>
				<li><a href="#" class="newfinancialresultstitle">2019 </a><a href="#" class="financialresultsauthor">August 01</a><a href="files\Financial Results\Q1 2019-20.pdf" class="newfinancialresultsyear">Q1 2019-20</a><%--<i class="fa fa-plus" aria-hidden="true"></i></li>--%>
                <li><a href="#" class="newfinancialresultstitle">2019 </a><a href="#" class="financialresultsauthor">May 09</a><a href="files\Financial Results\Q4 2018-19.pdf" class="newfinancialresultsyear">Q4 & Annual Results 2018-19</a><%--<i class="fa fa-plus" aria-hidden="true"></i></li>--%>    
                <li><a href="#" class="newfinancialresultstitle">2019 </a><a href="#" class="financialresultsauthor">February 05</a><a href="files\Financial Results\Q3 2018-19.pdf" class="newfinancialresultsyear">Q3 2018-19</a><%--<i class="fa fa-plus" aria-hidden="true"></i>--%></li>
					<li><a href="#" class="newfinancialresultstitle">2018 </a><a href="#" class="financialresultsauthor">November 01</a><a href="files\Financial Results\Q2 2018-19.pdf" class="newfinancialresultsyear">Q2 2018-19</a><%--<i class="fa fa-plus" aria-hidden="true"></i>--%></li>
					<li><a href="#" class="newfinancialresultstitle">2018 </a><a href="#" class="financialresultsauthor">August 08</a><a href="files\Financial Results\Q1 2018-19.pdf" class="newfinancialresultsyear">Q1 2018-19</a><%--<i class="fa fa-plus" aria-hidden="true"></i>--%></li>
					<li><a href="#" class="newfinancialresultstitle">2018 </a><a href="#" class="financialresultsauthor">May 03</a><a href="files\Financial Results\Q4 & Annual 2017-18.pdf" class="newfinancialresultsyear">Q4 & Annual Results 2017-18</a><%--<i class="fa fa-plus" aria-hidden="true"></i>--%></li>
					<li><a href="#" class="newfinancialresultstitle">2018 </a><a href="#" class="financialresultsauthor">January 31</a><a href="files\Financial Results\Q3 2017-18.pdf" class="newfinancialresultsyear">Q3 2017-18</a><%--<i class="fa fa-plus" aria-hidden="true"></i>--%></li>
					<li><a href="#" class="newfinancialresultstitle">2017 </a><a href="#" class="financialresultsauthor">November 02</a><a href="files\Financial Results\Q2 2017-18.pdf" class="newfinancialresultsyear">Q2 2017-18</a><%--<i class="fa fa-plus" aria-hidden="true"></i>--%></li>
					<li><a href="#" class="newfinancialresultstitle">2017 </a><a href="#" class="financialresultsauthor">August 03</a><a href="files\Financial Results\Q1 2017-18.pdf" class="newfinancialresultsyear">Q1 2017-18</a><%--<i class="fa fa-plus" aria-hidden="true"></i>--%></li>
					<li><a href="#" class="newfinancialresultstitle">2017 </a><a href="#" class="financialresultsauthor">May 04</a><a href="files\Financial Results\Q4 & Annual 2016-17.pdf" class="newfinancialresultsyear">Q4 & Annual Results 2016-17</a><%--<i class="fa fa-plus" aria-hidden="true"></i>--%></li>
					<li><a href="#" class="newfinancialresultstitle">2017 </a><a href="#" class="financialresultsauthor">February 02</a><a href="files\Financial Results\Q3 2016-17.pdf" class="newfinancialresultsyear">Q3 2016-17</a><%--<i class="fa fa-plus" aria-hidden="true"></i>--%></li>
               <!-- Add New article code above -->			   
			   
               <%-- <li><a href="#" class="financialresultstitle">Kolkata Elevated Corridor </a><a href="#" class="financialresultsauthor">Chetan Bhagat </a><a href="#" class="financialresultsyear">2017 </a>--%><%--<i class="fa fa-plus" aria-hidden="true"></i></li>--%>
               <%-- <li><a href="#" class="whitepapertitle">Kolkata Elevated Corridor </a><a href="#" class="whitepaperauthor">Chetan Bhagat </a><a href="#" class="whitepaperyear">2015 </a>--%><%--<i class="fa fa-plus" aria-hidden="true">
                </i></li>--%>
               <%-- <li><a href="#" class="whitepapertitle">Kolkata Elevated Corridor </a><a href="#" class="whitepaperauthor">Chetan Bhagat </a><a href="#" class="whitepaperyear">2014 </a>--%><%--<i class="fa fa-plus" aria-hidden="true">
                </i></li>--%>
               <%-- <li><a href="#" class="whitepapertitle">Kolkata Elevated Corridor </a><a href="#" class="whitepaperauthor">Chetan Bhagat </a><a href="#" class="whitepaperyear">2013 </a><--%>
             <%--   <li><a href="#" class="whitepapertitle">Kolkata Elevated Corridor </a><a href="#" class="whitepaperauthor">Chetan Bhagat </a><a href="#" class="whitepaperyear">2017 </a><i class="fa fa-plus" aria-hidden="true">
                </i></li>--%>
            </ul>
        </div>
       
</asp:Content>


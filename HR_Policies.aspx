<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hccgroup1.aspx.cs" Inherits="hccgroup1" %>--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="humanresource-policies.aspx.cs" Inherits="humanresource_policies" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
<div class='commpagesdiv'><div class='commonpages'>
<div class='commonpagesheading'>Policies</div>
      <div class="mainpostwallcat">
                <div class="comments-summery1"> 
                 <span>
                    <a id="gobackbtn" href='<%=ReturnUrl("ProceduresonHome")%>'  class="aaa" > Policy & Procedure </a>
                </span>
                </div>
    </div>


<div class='aboutusdiv'>
<div class='aboutus'>
<div class="half">
<div class="tab blue">

<div class="tab-content" style="max-height:none;font-size:13px !important;font-weight:bold;">
	<p ><a style="max-height:none;font-size:13px !important;font-family: 'Lato', sans-serif !important;font-weight:bold !important;" href='<%=ReturnUrl("hccurlmain")%>/files/HUMAN RESOURCE/Code of Conduct and Work Ethics.pdf' rel="noopener noreferrer">Code of Conduct and Work Ethics</a></p>
    <P><a  style="max-height:none;font-size:13px !important;font-weight:bold !important;"  href='<%=ReturnUrl("hccurlmain")%>/files/HUMAN RESOURCE/Work Timings, Holidays & Office Guidelines.pdf' rel="noopener noreferrer">Work Timings, Holidays & Office Guidelines</a></p>
    <P><a  style="max-height:none;font-size:13px !important;font-weight:bold !important;"  href='<%=ReturnUrl("hccurlmain")%>/files/HUMAN RESOURCE/Employee Induction & On-Boarding.pdf' rel="noopener noreferrer">Employee Induction & On-Boarding</a></p>
    <P><a style="max-height:none;font-size:13px !important;font-weight:bold !important;"   href='<%=ReturnUrl("hccurlmain")%>/files/HUMAN RESOURCE/Employee Performance Management.pdf' rel="noopener noreferrer">Employee Performance Management</a></p>
    <P><a style="max-height:none;font-size:13px !important;font-weight:bold !important;"   href='<%=ReturnUrl("hccurlmain")%>/files/HUMAN RESOURCE/Employee Development.pdf' rel="noopener noreferrer">Employee Development</a></p>
    <P><a style="max-height:none;font-size:13px !important;font-weight:bold !important;"   href='<%=ReturnUrl("hccurlmain")%>/files/HUMAN RESOURCE/Employee Development – External Training Nomination Format.pdf' rel="noopener noreferrer">Employee Development – External Training Nomination Format</a></p>
    <%--<P><a style="max-height:none;font-size:13px !important;font-weight:bold !important;"   href='<%=ReturnUrl("hccurlmain")%>/files/HUMAN RESOURCE/Approval Format_Nominations to External Training Programs Outside India.pdf' rel="noopener noreferrer">Approval format Nominations to external training programs outside India </a></p>
    <P><a style="max-height:none;font-size:13px !important;font-weight:bold !important;"   href='<%=ReturnUrl("hccurlmain")%>/files/HUMAN RESOURCE/Declaration_External Training Programs Outside India.pdf' rel="noopener noreferrer">Declaration – External Training Programs outside India</a></p>
    <P><a style="max-height:none;font-size:13px !important;font-weight:bold !important;"   href='<%=ReturnUrl("hccurlmain")%>/files/HUMAN RESOURCE/Approval Format_Nominations to External Training Programs Within India.pdf' rel="noopener noreferrer">Approval format Nominations to external Training programs within India</a></p>--%>
    <P><a style="max-height:none;font-size:13px !important;font-weight:bold !important;"   href='<%=ReturnUrl("hccurlmain")%>/files/HUMAN RESOURCE/Sponsoring officers for further educational courses.pdf' rel="noopener noreferrer">Sponsoring officers for further educational courses</a></p>
    <P><a style="max-height:none;font-size:13px !important;font-weight:bold !important;"   href='<%=ReturnUrl("hccurlmain")%>/files/HUMAN RESOURCE/Events & Celebrations.pdf' rel="noopener noreferrer">Events & Celebrations</a></p>
    <P><a style="max-height:none;font-size:13px !important;font-weight:bold !important;"   href='<%=ReturnUrl("hccurlmain")%>/files/HUMAN RESOURCE/Employee Grievance Redress Mechanism.pdf' rel="noopener noreferrer">Employee Grievance Redress Mechanism</a></p>
    <P><a  style="max-height:none;font-size:13px !important;font-weight:bold !important;"  href='<%=ReturnUrl("hccurlmain")%>/files/HUMAN RESOURCE/Prevention and Redress of Sexual Harassment.pdf' rel="noopener noreferrer">Prevention and Redress of Sexual Harassment</a></p>
    <P><a  style="max-height:none;font-size:13px !important;font-weight:bold !important;"  href='<%=ReturnUrl("hccurlmain")%>/files/HUMAN RESOURCE/Vigil Mechanism.pdf' rel="noopener noreferrer">Vigil Mechanism</a></p>
    <P><a  style="max-height:none;font-size:13px !important;font-weight:bold !important;"  href='<%=ReturnUrl("hccurlmain")%>/files/HUMAN RESOURCE/Leave Rules.pdf' rel="noopener noreferrer">Leave Rules</a></p>
    <P><a  style="max-height:none;font-size:13px !important;font-weight:bold !important;"  href='<%=ReturnUrl("hccurlmain")%>/files/HUMAN RESOURCE/LTA Rules.pdf' rel="noopener noreferrer">LTA Rules</a></p>
    <P><a  style="max-height:none;font-size:13px !important;font-weight:bold !important;"  href='<%=ReturnUrl("hccurlmain")%>/files/HUMAN RESOURCE/Car Policy.pdf' rel="noopener noreferrer">Car Policy</a></p>
    <P><a  style="max-height:none;font-size:13px !important;font-weight:bold !important;"  href='<%=ReturnUrl("hccurlmain")%>/files/HUMAN RESOURCE/Medical Pay.pdf' rel="noopener noreferrer">Medical Pay</a></p>
    <P><a  style="max-height:none;font-size:13px !important;font-weight:bold !important;"  href='<%=ReturnUrl("hccurlmain")%>/files/HUMAN RESOURCE/Executive Health Check Up Scheme.pdf' rel="noopener noreferrer">Executive Health Check Up Scheme</a></p>
    <P><a  style="max-height:none;font-size:13px !important;font-weight:bold !important;"  href='<%=ReturnUrl("hccurlmain")%>/files/HUMAN RESOURCE/Group Mediclaim Insurance.pdf' rel="noopener noreferrer">Group Mediclaim Insurance</a></p>
    <P><a  style="max-height:none;font-size:13px !important;font-weight:bold !important;"  href='<%=ReturnUrl("hccurlmain")%>/files/HUMAN RESOURCE/Group Personal Accident Insurance.pdf' rel="noopener noreferrer">Group Personal Accident Insurance</a></p>
    <P><a  style="max-height:none;font-size:13px !important;font-weight:bold !important;"  href='<%=ReturnUrl("hccurlmain")%>/files/HUMAN RESOURCE/Group Life Insurance.pdf' rel="noopener noreferrer">Group Life Insurance</a></p>
    <P><a  style="max-height:none;font-size:13px !important;font-weight:bold !important;"  href='<%=ReturnUrl("hccurlmain")%>/files/HUMAN RESOURCE/Domestic Travel.pdf' rel="noopener noreferrer">Domestic Travel</a></p>
    <P><a  style="max-height:none;font-size:13px !important;font-weight:bold !important;"  href='<%=ReturnUrl("hccurlmain")%>/files/HUMAN RESOURCE/Domestic Travel Rules - Annexures.pdf' rel="noopener noreferrer">Domestic Travel Rules - Annexures</a></p>
    <P><a  style="max-height:none;font-size:13px !important;font-weight:bold !important;"  href='<%=ReturnUrl("hccurlmain")%>/files/HUMAN RESOURCE/Entitlements for New Employees & Transfers.pdf' rel="noopener noreferrer">Entitlements for New Employees & Transfers</a></p>
    <P><a  style="max-height:none;font-size:13px !important;font-weight:bold !important;"  href='<%=ReturnUrl("hccurlmain")%>/files/HUMAN RESOURCE/International Travel.pdf' rel="noopener noreferrer">International Travel</a></p>
    <P><a  style="max-height:none;font-size:13px !important;font-weight:bold !important;"  href='<%=ReturnUrl("hccurlmain")%>/files/HUMAN RESOURCE/Local Official Travel.pdf' rel="noopener noreferrer">Local Official Travel</a></p>
    <P><a  style="max-height:none;font-size:13px !important;font-weight:bold !important;"  href='<%=ReturnUrl("hccurlmain")%>/files/HUMAN RESOURCE/Working on Weekly Off Days - Holidays - Beyond Working Hours.pdf' rel="noopener noreferrer">Working on Weekly Off Days - Holidays - Beyond Working Hours</a></p>
    <P><a  style="max-height:none;font-size:13px !important;font-weight:bold !important;"  href='<%=ReturnUrl("hccurlmain")%>/files/HUMAN RESOURCE/Employee Exit Process.pdf' rel="noopener noreferrer">Employee Exit Process</a></p>
    <P><a  style="max-height:none;font-size:13px !important;font-weight:bold !important;"  href='<%=ReturnUrl("hccurlmain")%>/files/HUMAN RESOURCE/HCC Employee Info Pack - PMS 2019.pdf' rel="noopener noreferrer">HCC Employee PMS Info Pack – 2019</a></p>
    <P><a  style="max-height:none;font-size:13px !important;font-weight:bold !important;"  href='<%=ReturnUrl("hccurlmain")%>/files/HUMAN RESOURCE/HCC Infrastructure Employee Info Pack - PMS 2019.pdf' rel="noopener noreferrer">HCC Infrastructure Employee PMS Info Pack – 2019</a></p>
    <P><a  style="max-height:none;font-size:13px !important;font-weight:bold !important;"  href='<%=ReturnUrl("hccurlmain")%>/files/HUMAN RESOURCE/Steiner India Employee Info Pack - PMS 2019.pdf' rel="noopener noreferrer">Steiner India Employee PMS Info Pack – 2019</a></p>
    <P><a  style="max-height:none;font-size:13px !important;font-weight:bold !important;"  href='<%=ReturnUrl("hccurlmain")%>/files/HUMAN RESOURCE/HBTL Employee Info Pack - PMS 2019.pdf' rel="noopener noreferrer">HBTL Employee PMS Info Pack – 2019</a></p>


</div>
</div>



</div>
</div>
</div>
</div></div>


</asp:Content>




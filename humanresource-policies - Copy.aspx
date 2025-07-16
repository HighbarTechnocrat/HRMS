<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hccgroup1.aspx.cs" Inherits="hccgroup1" %>--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="humanresource-policies.aspx.cs" Inherits="humanresource_policies" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <link href="<%=ReturnUrl("css") %>profile/mprofile.css" rel="stylesheet" type="text/css" />
   
            <div class="userposts"><span>Human Resource Policies</span> </div>

             
			
             
                  
	 <div class="wrapper">

                        <div class="half">
                            <div class="tab blue">
                                <%--JAYESH_PRAJYOT COMMENTED BELOW RADIO BUTTON CODE 12oct2017--%>
                                <input id="tab-four" type="radio" name="tabs2" style="display:none">
                                <%--JAYESH_PRAJYOT COMMENTED ABOVE RADIO BUTTON CODE 12oct2017
                                <label for="tab-four">Forms</label>--%>
								<label for="tab-four">Policies</label>
                                <div class="tab-content">
                                    <%-- <p><a href="#" title="Travel Requisition Form">Travel Requisition Form</a>
<a href="#" title="Requisition Format For Car Hire">Requisition Format For Car Hire</a></p>--%>
                                    <!--<ul>-->
                                        <!--<li class="ullink"><a href="#" title="Travel Requisition Form Requisition Format For Car Hire">Travel Requisition Form Requisition Format For Car Hire</a>-->
										 <p><a  href="http://localhost/hrms/files/HUMAN RESOURCE/Code of Conduct and Work Ethics.pdf#toolbar=0" target="_blank" rel="noopener noreferrer">Code of Conduct and Work Ethics</a></p>
    <P><a  href="http://localhost/hrms/files/HUMAN RESOURCE/Work Timings, Holidays & Office Guidelines.pdf#toolbar=0" target="_blank" rel="noopener noreferrer">Work Timings, Holidays & Office Guidelines</a></p>
    <P><a  href="http://localhost/hrms/files/HUMAN RESOURCE/Employee Induction & On-Boarding.pdf#toolbar=0" target="_blank" rel="noopener noreferrer">Employee Induction & On-Boarding</a></p>
    <P><a  href="http://localhost/hrms/files/HUMAN RESOURCE/Employee Performance Management.pdf#toolbar=0" target="_blank" rel="noopener noreferrer">Employee Performance Management</a></p>
    <P><a  href="http://localhost/hrms/files/HUMAN RESOURCE/Employee Development.pdf#toolbar=0" target="_blank" rel="noopener noreferrer">Employee Development</a></p>
    <P><a  href="http://localhost/hrms/files/HUMAN RESOURCE/Sponsoring officers for further educational courses.pdf#toolbar=0" target="_blank" rel="noopener noreferrer">Sponsoring officers for further educational courses</a></p>
    <P><a  href="http://localhost/hrms/files/HUMAN RESOURCE/Events & Celebrations.pdf#toolbar=0" target="_blank" rel="noopener noreferrer">Events & Celebrations</a></p>
    <P><a  href="http://localhost/hrms/files/HUMAN RESOURCE/Employee Grievance Redress Mechanism.pdf#toolbar=0" target="_blank" rel="noopener noreferrer">Employee Grievance Redress Mechanism</a></p>
    <P><a  href="http://localhost/hrms/files/HUMAN RESOURCE/Prevention and Redress of Sexual Harassment.pdf#toolbar=0" target="_blank" rel="noopener noreferrer">Prevention and Redress of Sexual Harassment</a></p>
    <P><a  href="http://localhost/hrms/files/HUMAN RESOURCE/Vigil Mechanism.pdf#toolbar=0" target="_blank" rel="noopener noreferrer">Vigil Mechanism</a></p>
    <P><a  href="http://localhost/hrms/files/HUMAN RESOURCE/Leave Rules.pdf#toolbar=0" target="_blank" rel="noopener noreferrer">Leave Rules</a></p>
    <P><a  href="http://localhost/hrms/files/HUMAN RESOURCE/LTA Rules.pdf#toolbar=0" target="_blank" rel="noopener noreferrer">LTA Rules</a></p>
    <P><a  href="http://localhost/hrms/files/HUMAN RESOURCE/Car Policy.pdf#toolbar=0" target="_blank" rel="noopener noreferrer">Car Policy</a></p>
    <P><a  href="http://localhost/hrms/files/HUMAN RESOURCE/Medical Pay.pdf#toolbar=0" target="_blank" rel="noopener noreferrer">Medical Pay</a></p>
    <P><a  href="http://localhost/hrms/files/HUMAN RESOURCE/Executive Health Check Up Scheme.pdf#toolbar=0" target="_blank" rel="noopener noreferrer">Executive Health Check Up Scheme</a></p>
    <P><a  href="http://localhost/hrms/files/HUMAN RESOURCE/Group Mediclaim Insurance.pdf#toolbar=0" target="_blank" rel="noopener noreferrer">Group Mediclaim Insurance</a></p>
    <P><a  href="http://localhost/hrms/files/HUMAN RESOURCE/Group Personal Accident Insurance.pdf#toolbar=0" target="_blank" rel="noopener noreferrer">Group Personal Accident Insurance</a></p>
    <P><a  href="http://localhost/hrms/files/HUMAN RESOURCE/Group Life Insurance.pdf#toolbar=0" target="_blank" rel="noopener noreferrer">Group Life Insurance</a></p>
    <P><a  href="http://localhost/hrms/files/HUMAN RESOURCE/Domestic Travel.pdf#toolbar=0" target="_blank" rel="noopener noreferrer">Domestic Travel</a></p>
    <P><a  href="http://localhost/hrms/files/HUMAN RESOURCE/Domestic Travel Rules - Annexures.pdf#toolbar=0" target="_blank" rel="noopener noreferrer">Domestic Travel Rules - Annexures</a></p>
    <P><a  href="http://localhost/hrms/files/HUMAN RESOURCE/Entitlements for New Employees & Transfers.pdf#toolbar=0" target="_blank" rel="noopener noreferrer">Entitlements for New Employees & Transfers</a></p>
    <P><a  href="http://localhost/hrms/files/HUMAN RESOURCE/International Travel.pdf#toolbar=0" target="_blank" rel="noopener noreferrer">International Travel</a></p>
    <P><a  href="http://localhost/hrms/files/HUMAN RESOURCE/Local Official Travel.pdf#toolbar=0" target="_blank" rel="noopener noreferrer">Local Official Travel</a></p>
    <P><a  href="http://localhost/hrms/files/HUMAN RESOURCE/Working on Weekly Off Days - Holidays - Beyond Working Hours.pdf#toolbar=0" target="_blank" rel="noopener noreferrer">Working on Weekly Off Days - Holidays - Beyond Working Hours</a></p>
    <P><a  href="http://localhost/hrms/files/HUMAN RESOURCE/Employee Exit Process.pdf#toolbar=0" target="_blank" rel="noopener noreferrer">Employee Exit Process</a></p>

                                    <!--</ul>-->
                                </div>
                            </div>
                            <div class="tab blue">
                                <%--JAYESH_PRAJYOT COMMENTED BELOW RADIO BUTTON CODE 12oct2017--%>
                                <input id="tab-five" type="radio" name="tabs2" style="display:none">
                                <%--JAYESH_PRAJYOT COMMENTED ABOVE RADIO BUTTON CODE 12oct2017--%>
                                <label for="tab-five">Holiday List</label>
                                <div class="tab-content">
                                    <!--<ul>-->
										<P><a href="http://localhost/hrms/files/Holiday list - 2018.pdf" title="Holiday List for the year 2018">Holiday List for the year 2018</a></p>
                                        <P><a href="http://localhost/hrms/files/Public-Holiday-2017_final.pdf" title="Holiday List for the year 2017">Holiday List for the year 2017</a></p>
                                       <!-- <li class="ullink"><a href="http://localhost/hrms/files/Public-Holiday-2017_final.pdf" title="Holiday List for the year 2016">Holiday List for the year 2016</a></li>
                                        <li class="ullink"><a href="http://localhost/hrms/files/Public-Holiday-2017_final.pdf" title="Holiday List for the year 2015">Holiday List for the year 2015</a></li>-->
                                    <!--</ul>-->
                                </div>
                            </div>
                            <!--<div class="tab blue">
                                <%--JAYESH_PRAJYOT COMMENTED BELOW RADIO BUTTON CODE 12oct2017--%>
                                <input id="tab-six" type="radio" name="tabs2" style="display:none">
                                <%--JAYESH_PRAJYOT COMMENTED ABOVE RADIO BUTTON CODE 12oct2017--%>
                                <label for="tab-six">HR Policy Handbook</label>
                                <div class="tab-content">
                                   
                                    <ul>
                                        <li class="ullink"><a href="#" title="Revised Manpower Requisition Form HCC HIV AIDS policy in regional languages Vigil Mechanism Policy">Revised Manpower Requisition Form HCC HIV AIDS policy in regional languages Vigil Mechanism Policy</a>

                                            <%--NIKITA COMMENTED AND ADDED ABOVE CODE 25oct2017--%>
                                    </ul>
                                </div>
                            </div>-->
							
                           <%-- <div class="tab blue">
                                <input id="tab-seven" type="radio" name="tabs2" style="display:none">
                                <label for="tab-seven">HR Policy Handbook</label>
                                <div class="tab-content">
                                    <ul>
                                        <li class="ullink"><a href="#" title="Revised Manpower Requisition Form HCC HIV AIDS policy in regional languages Vigil Mechanism Policy">Revised Manpower Requisition Form HCC HIV AIDS policy in regional languages Vigil Mechanism Policy</a>
                                    </ul>
                                </div>
                            </div>--%>
                        </div>


                        <%--  <div class="half">
    
                    </div>



                    <%-- JAYESH_PRAJYOT ADDED BELOW CODE FOR REQUEST  FORM 18sep2017--%>
                </div>
    </div>
</asp:Content>




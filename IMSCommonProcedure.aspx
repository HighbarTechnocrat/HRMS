<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hccgroup1.aspx.cs" Inherits="hccgroup1" %>--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/cms.master" AutoEventWireup="true" CodeFile="IMSCommonProcedure.aspx.cs" Inherits="hrmsgroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <link href="<%=ReturnUrl("css") %>profile/mprofile.css" rel="stylesheet" type="text/css" />   
        <div class="userposts"><span>Policy & Procedure : IMS Common Procedures</span> </div>     
         <div class="half">
            <div class="tab blue" >                
                <span style="font-size: 10pt;">
                    <strong><label for="tab-four"><a href="http://localhost/hrms/mr" style="color: #fff;">MR</a></label></strong>
                </span>
             </div>
              <div class="tab blue">                
                <span style="font-size: 10pt;">
                    <strong><label for="tab-four"><a href="http://localhost/hrms/qaqc" style="color: #fff;">QA QC</a></label></strong>
                </span>
             </div>
              <div class="tab blue">                
                <span style="font-size: 10pt;">
                    <strong><label for="tab-four"><a href="http://localhost/hrms/ems" style="color: #fff;">EMS</a></label></strong>
                </span>
             </div>
               <div class="tab blue">                
                <span style="font-size: 10pt;">
                    <strong><label for="tab-four"><a href="http://localhost/hrms/emsperformance" style="color: #fff;">EMS Performance</a></label></strong>
                </span>
             </div>
               <div class="tab blue">                
                <span style="font-size: 10pt;">
                    <strong><label for="tab-four"><a href="http://localhost/hrms/ohsms" style="color: #fff;">OH&SMS</a></label></strong>
                </span>
             </div>
               <div class="tab blue">                
                <span style="font-size: 10pt;">
                    <strong><label for="tab-four"><a href="http://localhost/hrms/ohsmsperformance" style="color: #fff;">Ohsms performance</a></label></strong>
                </span>
             </div>
         </div>

</asp:Content>




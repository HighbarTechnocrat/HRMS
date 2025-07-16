<%@ Control Language="C#" AutoEventWireup="true" CodeFile="homeglobal.ascx.cs" Inherits="Components_Common_homeglobal" %>



<div id="content-container">
    <!-- START CONTENT -->
    <%--ORIGINAL CODE before SONY changed as UNDER--%>
<%--    <div id="content">
        <div id="dashboard">

            <asp:Panel ID="uxcategorypanel" runat="server">
            </asp:Panel>
                                
            <asp:Panel ID="uxbirth" runat="server" CssClass="pnlbirth">
            </asp:Panel>
             <asp:Panel ID="uxwall" runat="server">
            </asp:Panel> 
            <asp:Panel ID="uxgroups" runat="server">
            </asp:Panel>
            <asp:Panel ID="uxwhether" runat="server">
            </asp:Panel>
        </div>
    </div>
</div>--%>

    <%--SONY MODIFIED above code and made it work as below--%>

        <div id="content">
          <%--   <asp:Label ID="lblname"  runat="server" Text="User" Width="200px" ></asp:Label>
           --%>
            <asp:Label ID="lblfname" ForeColor="#3D1956" runat="server" CssClass="woffice-welcome" Text="User"></asp:Label>
            
        <div id="dashboard">

            <div class="bitydays-heading">
            <asp:Label runat="server" Text="Birthdays"></asp:Label></div>

            <asp:Panel ID="uxbirth" runat="server" CssClass="pnlbirth">
            </asp:Panel>

            <asp:Panel ID="uxcategorypanel" runat="server">
            </asp:Panel>    
<%--sony commented these to HIDE display of MYWALL, GROUPS and WEATHER WIDGETS on home page --%>
            <%--                         
             <asp:Panel ID="uxwall" runat="server">
            </asp:Panel> 
            <asp:Panel ID="uxgroups" runat="server">
            </asp:Panel>
            <asp:Panel ID="uxwhether" runat="server">
            </asp:Panel>--%>

                    
    </div>  

         <%--JAYESH ADDED BELOW CODE TO ADD LINKBUTTONS WITH ITS IMAGE ICONS 5oct2017--%>        
        <div class="below-icons">   
            
            <div class="leaveHome">         
              
                <%--<a  href='<%=ReturnUrl("LeaveonHome") %>' class="leaverembursTranning">--%>
                <%--<a href="http://localhost/hrms/ps/9nQ-8bwn0dYnjBaQIq6bTg==" class="leaverembursTranning">--%>
                <a href="http://localhost/hrms/Vision.aspx" class="leaverembursTranning">
                    <img src="images/homepage_imgs/TR1.jpg" class="homepageLeaveimgs">                    
                </a>
               <div class="hcenterText">
                   <%--<h1>Meet Highbarians</h1>--%>
                   <h1>Vision</h1>
               </div>
             
              </div> 
          
              <div class="travelHome">
            <%--<asp:linkbutton runat="server" PostBackUrl="http://localhost/hrms/procs/travelindex.aspx">--%>
                  <%--<a  href='<%=ReturnUrl("TravelonHome")%>' class="leaverembursTranning">--%>
                  <a href='<%=ReturnUrl("Helpdesk") %>' class="leaverembursTranning"> 
                      <img src="images/homepage_imgs/TR2.jpg" class="homepageLeaveimgs">
                </a>
               <div class="hcenterText">
                   <%--<h1>Executive Team</h1>--%>
                   <h1>EmployeeFIRST</h1>
               </div>               
           <%-- </asp:linkbutton>--%>
           </div>
          
              <div class="reimbursementHome">  
                <a href="http://localhost/hrms/GalleryHome.aspx" class="leaverembursTranning">
                    <img src="images/homepage_imgs/TR3.jpg" class="homepageLeaveimgs">
                </a>
                <div class="hcenterText">
                    <h1>Gallery</h1>
                </div> 
  	     </div>

           
            <div class="training1">
                <a href="http://localhost/hrms/procs/TaskMonitoring.aspx" class="leaverembursTranning">
                    <img src="images/homepage_imgs/TR4.jpg" class="homepageLeaveimgs">
                </a>
                <div class="hcenterText">
                    <h1>Task Monitoring</h1>
                </div>
            </div>

           <%-- JAYESH ADDED ABOVE CODE TO ADD LINKBUTTONS WITH ITS IMAGE ICONS  5oct2017--%>
            </div>
  
           
    </div>
</div>

     <%--Key Personnel--%>
<div class="KeyPersonnelHome">
        <div  style="display:none">
        <a href='<%=ReturnUrl("KeyPersonnel") %>'>  
           <%--<img src="images/homepage_imgs/KeyPersonnel.JPG"  class="keypersnolimg" />--%>
            <%--<span class="KeyPersonnel">Key Personnel</span>--%>
            <h3 class="KeyPersonnel">Key Personnel</h3>
        </a>
       </div>
    <div class="procedureHome">         
        <%--<a href='<%=ReturnUrl("ProceduresonHome") %>'>--%>
        <a  href='<%=ReturnUrl("AttendanceonHome") %>'>
            <%--<img src="images/homepage_imgs/Procedures.JPG" class="ssss" />--%>
            <img src="images/homepage_imgs/BB3.JPG" class="ssss" />
        </a>
        <div class="hcenterText2">
            <h1>Attendance</h1>
        </div>
   </div>
    <div class="KnowledgeCentHome">         
       <%-- <a href='<%=ReturnUrl("KnowledgeCentonHome") %>'>--%>
        <a  href='<%=ReturnUrl("reimbursementonHome") %>'>
            <%--<img src="images/homepage_imgs/Knowledge_Center.JPG" class="ssss"/>--%>
            <img src="images/homepage_imgs/BB4.JPG" class="ssss" />
        </a>
        <div class="hcenterText3">
            <h1 runat="server" id="ClaimsHead">Claims</h1>
        </div>
   </div>
</div>
 

<%--NewAchivProcdureKNCenterCommTimeOut--%>
<%--by Highbartech on 28-05-2020--%>
<%--  <div class="EmpDirectPeopleSpkGallery"> 
    <div>         
        <a href='<%=ReturnUrl("EmpDirectonHome") %>'>
            <img src="images/homepage_imgs/employee_directory.JPG"  class="empdirect" />
        </a>
   </div>
     <div class="people_speak">         
        <a href="http://localhost/hrms/ps/9nQ-8bwn0dYnjBaQIq6bTg==">
            <img src="images/homepage_imgs/People_Speak.JPG"   class="empdirect"/>
        </a>   
    </div>
    <div class="homeGallery">         
        <a href='<%=ReturnUrl("homegallery") %>'>
            <img src="images/homepage_imgs/Gallery.JPG"  class="empdirect" />
        </a>
   </div>
</div>--%>

  <%--div for New Letter , Achivement ,Procedures , Knowledge Center , Commnunication, Time out--%>
<div class="NewsAchivProcKnowComTimeout">
     <div class="NewsLetters" >         
        <%--<a href='<%=ReturnUrl("newsletteronHome") %>'>    --%> 
         <a href='<%=ReturnUrl("Appraisal") %>'>   
            <%--<img src="images/homepage_imgs/NewsLetters.JPG"  class="ssss"/>--%>
            <img src="images/homepage_imgs/BL3.JPG" class="ssss" />
        </a>
        <div class="hcenterText1">
            <%--<h1>Appraisal</h1>--%>
            <h1>Recruitment</h1>
        </div>            
   </div>
    <div class="AchivementHome" >         
        <!--<a  href='<%=ReturnUrl("Timesheet")%>'>
            <img src="images/homepage_imgs/BL4.JPG" class="ssss" />
        </a>
        <div class="hcenterText4">
            <h1>Timesheet</h1>
        </div>-->
            <a href="https://highbartechnocrat.sharepoint.com/sites/KMSProd/SiteApplication/Pages/Dashboard.aspx" target="_blank">
                <%--<a href='<%=ReturnUrl("EmployeeRef") %>'>--%>
                <img src="http://localhost/hrms/images/homepage_imgs/BL2.jpg" class="imgkeyideacntrlapprsalmyfileN" />  
            </a>
            <div class="hcenterText4">
                <h1>KMS</h1>
            </div>
   </div>
    
    <div class="CommunicationHome">         
        <%--<a href='<%=ReturnUrl("CommunicationonHome") %>'>--%>
        <a href='<%=ReturnUrl("myfiles") %>'>
            <%--<img src="images/homepage_imgs/Communications.JPG" class="ssss"/>--%>
            <img src="images/homepage_imgs/BR1-2.JPG" class="sssb" />

        </a>
        <div class="hProfileText">
            <h1>My Corner</h1>
        </div>
   </div>
    <%-- by Highbartech on 29-05-2020 --%>
     <div class="TimeOutonHome">         
        <%--<a href='<%=ReturnUrl("TimeOutonHome") %>'>--%>
         <a href='<%=ReturnUrl("myfiles") %>'>
            <%--<img src="images/homepage_imgs/Time_Out.JPG" class="ssss"/>--%>
            <img id="user_profile_image" runat="server" class="profile" />
        </a>
   </div>
        <%-- by Highbartech on 29-05-2020 --%>

</div>


    
 
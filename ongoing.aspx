<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ongoing.aspx.cs" Inherits="ongoing" %>--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/cms.master" AutoEventWireup="true" CodeFile="ongoing.aspx.cs" Inherits="ongoing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <%--<style>	
	img[usemap] {
		border: none;
		height: auto;
		max-width: 900px;
		width: auto;
	}
	</style>--%>
    <link rel="stylesheet" href="MAP_IMG/Includes/bootstrap.css"/>
    <link rel="stylesheet" href="MAP_IMG/Includes/bootstrap-responsive.css"/>
    <link href="MAP_IMG/Includes/prettify.css" rel="stylesheet" />
    <link href="MAP_IMG/Includes/litetooltip.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="MAP_IMG/Includes/styleResponsive.css" />


    <link href="<%=ReturnUrl("css") %>profile/mprofile.css" rel="stylesheet" type="text/css" />
    <div class="mainpostwallcat">
        <div class="comments-summery1">
           <div class="userposts"><span> Projects</span> </div>
             
			<div class="projectlist1">
             
                    <div class="projectlistddl" style="display:none">
                        <asp:DropDownList ID="ddlprojectcat" runat="server" Width="200px" TabIndex="0" Visible="false"> </asp:DropDownList>
                        
                       <%-- <asp:DropDownList ID="projectddl" runat="server" CssClass="msgselect1new" TabIndex="1">
				             <asp:ListItem Text="All"></asp:ListItem>
                             <asp:ListItem Text="Transportation"></asp:ListItem>
                            <asp:ListItem Text="Hydro Power"></asp:ListItem>
                            <asp:ListItem Text="Nuclear & Special Projects"></asp:ListItem>
                            <asp:ListItem Text="Water Soluction"></asp:ListItem>
                        </asp:DropDownList>--%>
                   </div>
		            <div class="dropdown2" style="display:none">
			            <asp:DropDownList ID="ddlprojectstatus" runat="server" Width="200px" TabIndex="0"> </asp:DropDownList>
		            </div>
                  <%--<div class="projectstatusddl">
                         <asp:DropDownList ID="ddlprojectstatus" runat="server" Width="100px" TabIndex="0"> </asp:DropDownList>
                    </div>--%>
                <div class="project-text-box" style="display:none">              
                            <%--<asp:TextBox ID="txtprojectsearch" visible="true"  placeholder="Enter text for search" runat="server"></asp:TextBox>--%>
                            <%--<asp:TextBox ID="txtstarsearch" visible="true"  placeholder="Enter text for search" runat="server" CssClass="txtbox" TabIndex="2"></asp:TextBox>--%>
                    <%--<asp:TextBox ID="txtstarsearch" runat="server" visible="true" placeholder="Enter text for search" CssClass="txtbox" TabIndex="2"></asp:TextBox>--%>
                     <asp:TextBox ID="txttitle" runat="server"  placeholder="Enter text for search" Width="200"></asp:TextBox>
           
                </div>
                  <div class="projectbtn">
                    <span class="projectsearch">
                         <asp:LinkButton ID="projectsearchbtn" runat="server" ToolTip="Search Project" OnClick="lnksearch_Click"
                            TabIndex="3" ValidationGroup="valgrp" OnClientClick="projectsearchbtn_Click"><i class="fa fa-search" ></i></asp:LinkButton>
							<br/>
					</span>
                    <span class="projectsearch" style="display:none">
                        <asp:LinkButton ID="projectresetbtn" OnClick="lnkreset_Click" ToolTip="Reset Search" TabIndex="4" runat="server"><i class="fa fa-undo" aria-hidden="true"></i>
                        </asp:LinkButton></span>
        <%--            <span class="searchalldirectory">
                        <asp:TextBox ID="txtallsearch" placeholder="Search By Name or Email-ID" runat="server" CssClass="txtbox" ToolTip="Type Name or Email-id to search contact." TabIndex="3"></asp:TextBox><asp:LinkButton ID="lnkadllcontact" runat="server" CssClass="searchpostbtn" OnClick="lnkadllcontact_Click" TabIndex="4"><i class="fa fa-search"></i></asp:LinkButton></span>--%>

                </div>
		</div>           
        
            <section id="example2">
                    
                    <div class="imagehotspot-container" style="position: relative; max-width:800px; width: 100%; height: auto;margin: -46px 0 0 0 !important;">
                        <div style="position: relative; height: 0px; padding-bottom: 81.17%;">
                            <img src="images/HBT_Ongoing_Project.jpg" style="position: absolute; top: 0px; left: 0px;  z-index: 102" />
                             <!--North-->
                            <!--Jammu Kashmir http://localhost/hrms/projectdetailc.aspx-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=UkIJIb1ZSK_pcXo-WQDt9A=='><div id="ihotspot1" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 11.9%; left: 28.2%;"> </div></a> <!--Chutak HEP-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=ggs_Kxe7JuBHCUaTvjkEdg=='><div id="ihotspot2" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 13.7%; left: 32.2%;"> </div></a><!--Nimoo-Bazgo HEP-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=6arhDrbzMtSof1Xqq46zVA=='><div id="ihotspot3" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 12.32%; left: 24.23%;" > </div></a><!--Kishanganga HEP-->
                            
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=qnFpidekOSWSLCseeYO-1w=='><div id="ihotspot4" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 14%; left: 22.2%;"> </div></a><!--URI II HEP-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=nQAc-RZ-v3Mh5hwQX5feag=='><div id="ihotspot5" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 15.1%; left: 24%;"> </div></a><!--Mughal Road-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=NJxNuFkNKCLQQWF1mQuN2g=='><div id="ihotspot6" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 16.12%; left: 26.1%;"> </div></a><!--Pir Panjal Rail Tunnel-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=EwxUJ-K79XP8C-SplxUzOQ=='><div id="ihotspot7" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 18.8%; left: 23.7%;"> </div></a><!--Salal HEP-->
                                                                                  
                            <a href="http://localhost/hrms/completedT49.aspx"><div id="ihotspot8" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 17.3%; left: 27.2%;"> </div></a><!--T-49,T13 & Part of T14  2-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=t1wwgUIWObfostNk0wMThg=='><div id="ihotspot9" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 18.9%; left: 26.5%;"> </div></a><!--Ramban Banihal Road-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=Wjp80WZZRMhKqBDq_wCdCA=='><div id="ihotspot10" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 19.16%; left: 25.25%;"> </div></a><!--Sawalkote Access Tunnel Project-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=tQuozoaqsJGw5NA5FqXx2g=='><div id="ihotspot11" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 20.1%; left: 24.4%;"> </div></a><!--Anji Khad Bridge-->
                                                    
                            

                            <!--Himachal Pradesh-->
                            <a href="http://localhost/hrms/completedChamera.aspx"><div id="ihotspot12" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 22.1%; left: 27.8%;"> </div></a><!--Chamera HEP Stage I & III  2-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=ZUi8WiJEh70NlbErWouHWw=='><div id="ihotspot13" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 23.3%; left: 34.1%;"> </div></a><!--Kashang HEP-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=xrG93g22bdv7L4GLcSqL7A=='><div id="ihotspot14" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 25.8%; left: 31.9%;"> </div></a><!--Sainj HEP-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=nbai4O3kd5k6rcuDiIRRlw=='><div id="ihotspot15" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 26.7%; left: 34.1%;"> </div></a><!--Nathpa Jhakri HEP -->                            
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=Uh1hKGYkiJfFRZhU1w6pIQ=='><div id="ihotspot16" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 28.7%; left: 33.3%;"> </div></a><!--Sawara Kuddu HEP-->

                             <!--Uttarakhand-->
                             <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=QpA0ovVBS5DuL7VpejDIMQ=='><div id="ihotspot17" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 32.16%; left: 35.65%;"> </div></a><!--Tehri PSP-->
                             <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=fa7glUlircyvKWLHLafBZg=='><div id="ihotspot18" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 31.9%; left: 38.5%;"> </div></a><!--Vishnugad Pipalkoti HEP-->
                             <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=TFmz--jHzYezhEZevfgY4A=='><div id="ihotspot19" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 30.73%; left: 39.3%;"> </div></a><!--Tapovan Vishnugad-->

                             <!--Panjab-->
                             <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=2lgT2GfYL4MmWVsuEwlntA=='><div id="ihotspot20" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 27.6%; left: 26%;"> </div></a><!--Kapurthala Rail Coach Factory-->

                            
                            <!--Hariyana / Delhi-->
                            <a href="http://localhost/hrms/completedDMRC.aspx"><div id="ihotspot21" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 40.6%; left: 30.5%;"> </div></a><!--Delhi Metro Projects-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=bQhfwrCpbZScxK-4zhG6gQ=='><div id="ihotspot22" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 41.33%; left: 31.65%;"> </div></a><!--Delhi Faridabad Elevated Expressway-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=E8cGsB9CribxCIZaq7Yq4g=='><div id="ihotspot23" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 39.2%; left: 31.17%;"> </div></a><!--Munirka Flyover-->
                            
                            <!--Rajasthan-->
                            <a href="http://localhost/hrms/completedRAPP.aspx"><div id="ihotspot24" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 53.8%; left: 26.4%;"> </div></a><!--Rajasthan Atomic Power   2-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=cMBCL4g7r0CNM6tNfy8jIw=='><div id="ihotspot25" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 51.9%; left: 22.4%;"> </div></a><!--East West Corridor on RJ-7-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=m9vuB-x6yVJkuSPay0gGzQ=='><div id="ihotspot26" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 56.3%; left: 27.9%;"> </div></a><!--Parwan Gravity Dam-->
                           
                           <!--Gujrat-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=lkxgXVpPzsza0OHEN4-URg=='><div id="ihotspot27" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 60.4%; left: 10.9%;"> </div></a><!--Kachch Branch Canal-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=ax_DJQYbuDM81y8kiemSBQ=='><div id="ihotspot28" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 63.35%; left: 14.16%;"> </div></a><!-- Saurashtra Branch Canal-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=wWQmhzwvJq7nTCKewxQCbQ=='><div id="ihotspot29" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 74.4%; left: 9.1%;"> </div></a><!--NC-25-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=X_yl8sfPra4E49IDGLKE0w=='><div id="ihotspot30" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 72.3%; left: 19.4%;"> </div></a><!--Kakrapar Atomic Power Project-->
                            
                            <!--Uttar pradesh-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=e14zTtp06b3kspjcbObPUQ=='><div id="ihotspot93" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 40%; left: 32.8%;"> </div></a><!--Spice Plant-->
                             <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=by7GrjgoOu9zGnWhJg2m6A=='><div id="ihotspot31" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 41.9%; left: 35.4%;"> </div></a><!--Narora Atomic Power-->
                             <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=LSiAFJd2biZDvtIJlhYSAg=='><div id="ihotspot32" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 49.8%; left: 43.5%;"> </div></a><!--Gomti Acqueduct-->
                             <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=poVLeFjP6s1wbkfFm1uCHQ=='><div id="ihotspot33" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 47.9%; left: 51.5%;"> </div></a><!--Lucknow Muzaffarpur Highway on NH28-->
                             <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=PZgCEC2M8xld9jb3p2BSSw=='><div id="ihotspot34" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 54.6%; left: 49.7%;"> </div></a><!--Allahabad Bypass Road-->
                             <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=k60VSFbIacH8squIdxR0SA=='><div id="ihotspot35" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 55.3%; left: 48.2%;"> </div></a><!--Naini Bridge-->
                            
                            <!--West-->

                            <!--Madhya pradesh-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=LJX5Gy5ML_pmxs_HeKD9dw=='><div id="ihotspot36" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 70.4%; left: 26.3%;"></div></a><!--Bistan Lift Irrigation Scheme-->

                            <!--Maharashtra-->
                             <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=fZUkbbUfYwQJJVhmXHcswA=='><div id="ihotspot37" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 73.95%; left: 23.3%;"></div></a><!--Dhule Palesner BOT-->
                             <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=w-6qeE4CXDsmFlM28hWUmg=='><div id="ihotspot38" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 81.4%; left: 19.1%;"></div></a><!--Vaitarna Dam-->
                             <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=PJ9zVNRoe-amp--5UPO7Gw=='><div id="ihotspot39" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 83.9%; left: 16.8%;"></div></a><!--Maroshi Ruparel Water Supply Project-->
                             <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=c9vx8Ft1IMwpVXXrnr1InA=='><div id="ihotspot40" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 84.8%; left: 15.7%;"></div></a><!--Bandra Worli Sea Link-->
                             <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=qODXyd9HlhIL21e37990uA=='><div id="ihotspot41" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 86.5%; left: 16.3%;"></div></a><!-- Mumbai Metro Line 3-->
                             <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=gbygho8IujdSSn-GLSUhNA=='><div id="ihotspot42" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 85.4%; left: 17%;"></div></a><!---Mumbai Coastal Road-->
                             
                             <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=8jzb8_tF9fvXmDYqyz_yKQ=='><div id="ihotspot44" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 86.9%; left: 17.5%;"></div></a><!-- Residential Building at Anushaktinager-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=zlV_pXf5cgszU2N7UY-sCw=='><div id="ihotspot92" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 86.8%; left: 18.8%;"></div></a><!-- Aether, Photogravurs, Ipanema-->
                             <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=v7os6IXAC0MX583SLv_X0w=='><div id="ihotspot45" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 82.6%; left: 15.6%;"></div></a><!--INRP at BARC-->
                             <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=8QPqoi6ufrhCmypTOiWjtw=='><div id="ihotspot46" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 88.1%; left: 15.9%;"></div></a><!--DGNP Dry Dock-->
                             <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=arteWN4x5Rh0z6pEx5Hjhw=='><div id="ihotspot47" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 88.4%; left: 17%;"></div></a><!--Bhandup Water Treatment Plant-->
                             <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=coA1PXE_C5W9wvV7sXgN6g=='><div id="ihotspot48" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 88.4%; left: 18.55%;"></div></a><!--Bhorghat Railway Tunnel-->
						
                             <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=-VJqpBzR9XYFrH1rzq9a5A=='><div id="ihotspot49" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 89.2%; left: 19.8%;"></div></a><!--Mumbai Pune Expressway-->
                             <a href="http://localhost/hrms/completedPune.aspx"><div id="ihotspot50" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 90%; left: 20.9%;"></div></a><!--Pune Metro I & II  2-->
                             <a href="http://localhost/hrms/completedKoyna.aspx"><div id="ihotspot51" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 92.4%; left: 19%;"></div></a><!--Koyna Powerhouse  2-->

                            
                            
                            
                            <!--East-->
                            <!--Odisa-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=8RHeeSJyIna5xBQ84L9cPQ=='><div id="ihotspot52" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 74.1%; left: 53.6%;"> </div></a><!--Hindalco Projects-->
                              
                            <!--Chhattisgarh-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=A6VkxcJkzY6osTQ105mU3Q=='><div id="ihotspot53" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 76.6%; left: 45.9%;"> </div></a><!--Bhilai Steel Plant-->

                            <!--Bhutan-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=Q-8v-zFG2wTEY0_oSj-Kdg=='><div id="ihotspot54" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 43.6%; left: 73.2%;"> </div></a><!--Dagachhu Hydro Power Project-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=IhOCZ3u1g7QwlBP6bQlqqQ=='><div id="ihotspot55" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 46.7%; left: 73%;"> </div></a><!--Tala HEP-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=NOy6oGBPRVNSbcSDx9-UuQ=='><div id="ihotspot56" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 43.55%; left: 75.25%;"> </div></a><!--Punatsangchhu HEP-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=WajzOLsCj5uWtyXcwBxV5g=='><div id="ihotspot57" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 44.6%; left: 71.45%;"> </div></a><!--Nikachhu HEP-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=io0RiQW-0FFVEz8iXRMomw=='><div id="ihotspot58" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 47.5%; left: 68.8%;"> </div></a><!--Teesta Low Dam - Pkg IV-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=qy-ronclb6w6V2J4WBk-2g=='><div id="ihotspot59" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 55%; left: 68.2%;"> </div></a><!--Farakka Barrage-->
                            
                            <!--West Bengal-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=BfXml-reAR4p4GEQxr4YUw=='><div id="ihotspot60" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 64.1%; left: 68.9%;"> </div></a><!--Kolkata Metro-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=qH0eXzeNl7oMLoxWDyBxgg=='><div id="ihotspot61" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 69.3%; left: 68.4%;"> </div></a><!--Haldia Dock-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=2mrujxjB4c5gVMSEUAdbcw=='><div id="ihotspot94" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 65.8%; left: 70.5%;"> </div></a><!--Holiday Inn Express-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=6DtQEjo6m5Pxd6EZYmayrw=='><div id="ihotspot62" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 67.2%; left: 68.9%;"> </div></a><!--Kolkata elevated Road-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=sIT2Wm8dH4vk9TPCl3bLmA=='><div id="ihotspot63" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 59.4%; left: 68%;"> </div></a><!--Bahrampore Farakka Highway-->

                            <!--Bangladesh-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=NrwDeDQg0rgYU3x8ErepUQ=='><div id="ihotspot64" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 58.8%; left: 71.6%;"> </div></a><!--Rooppur Nuclear Power Plant-->
                            
                            <!--Bihar-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=A8Tel0t_eZizc4Dem8RliA=='><div id="ihotspot65" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 56.1%; left: 60.5%;"> </div></a><!--Sone Bridge-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=4jr7xS993z72hleUtaIgTA=='><div id="ihotspot66" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 51.8%; left: 63.77%;"> </div></a><!--Muzaffarpur Thermal Power Plant-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=Jf2HK_dnH9bhlzOEDx2ErQ=='><div id="ihotspot67" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 57.2%; left: 59%;"> </div></a><!--Sone Barrage-->
                            
                            <!--Arunachal-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=B4V25ft_lGoWnzkKTPkFJw=='><div id="ihotspot68" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 44.6%; left: 84.5%;"> </div></a><!--Pare HEP-->
                            <!--Assam-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=M9M884hMOJfD8iDNKmCkjA=='><div id="ihotspot69" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 46.1%; left: 87.6%;"> </div></a><!--Bogibeel Rail-cum-Road Bridge-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=KdXqC2vOMO9egUG0_a0VaA=='><div id="ihotspot70" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 50.1%; left: 85.6%;"> </div></a><!--Numaligarh Jorhat Road Project-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=ws7MU7oshkGKIJ9RR8nuAA=='><div id="ihotspot71" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 50%; left: 78%;"> </div></a><!--Saraighat Bridge-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=5b3bsptAxSxJarZ1beisnw=='><div id="ihotspot72" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 48.2%; left: 81.6%;"> </div></a><!--Kaliabhomora Bridge-->
                            <!--Manipur-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=70WEn-ZgJB2U_9oJ-KKQUw=='><div id="ihotspot73" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 55.4%; left: 87.3%;"> </div></a><!--Manipur Railway Tunnels-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=VNDHOAZ2rzR20beMag5-7Q=='><div id="ihotspot95" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 53.4%; left: 88.2%;"> </div></a><!-- Imphal-Kangchup-Tamenglong Road Project -->
                            <!--South-->

                            <!--Telangana-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=ZczyFKuByuL_ak-oJ9egkg=='><div id="ihotspot74" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 91.3%; left: 39.85%;"> </div></a><!--Godavari Lift Irrigation Scheme-->
                            <a href="#"><div   style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 86.9%; left: 40%;"> </div></a><!--Rajiv Dummugudem Lift Irrigation Scheme-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=z8kkLSjImGkW0NAzO_qneA=='><div id="ihotspot76" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 89.8%; left: 41.5%;"> </div></a><!--Pranhita Chevella Lift Irrigation Scheme-->
                            
                            <!--Andra Pradesh-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=6Wf2agYkIOMHEXiC-eTTrw=='><div id="ihotspot77" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 91.4%; left: 37.75%;"> </div></a><!--North South Corridor NHDP Phase II Package on AP-8-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=GJtP3oSx1QK_llL-fhMEVw=='><div id="ihotspot78" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 105.4%; left: 31.8%;"> </div></a><!--Veligonda Tunnel-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=vpYkRbmMTe64pLbVOVQYWA=='><div id="ihotspot79" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 96.75%; left: 43.9%;"> </div></a><!--Polavaram Canal-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=zye-f87Eeks6JFcdidIVaA=='><div id="ihotspot80" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 94.9%; left: 48.7%;"> </div></a><!--Vizag Oil Cavern-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=VHmfVU7GiJhhQVLMzU2rbA=='><div id="ihotspot81" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 93.3%; left: 47.1%;"> </div></a><!--Tata Memorial Hospital and Research Center-->

                            <!--Karnataka-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=D7ZD0toXWVIReTtKjDeVCQ=='><div id="ihotspot82" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 109.7%; left: 21.77%;"> </div></a><!--Padur Oil Cavern-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=3jC0wJPi8mrzve2uhcVRSg=='><div id="ihotspot83" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 111.4%; left: 31.25%;"> </div></a><!--Bangalore Metro-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=nxWQlbj2ERNC_cadT4sqYA=='><div id="ihotspot84" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 113.9%; left: 26.75%;"> </div></a><!--Yettinahole Project-->

                            <!--Tamilnadu-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=r8jN8UR5RCP3Coaq822shw=='><div id="ihotspot85" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 134.2%; left: 31.5%;"> </div></a><!--Kudankulam Nuclear Power Plant-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=M5-DkdjQpXigg3ci6vJ0bQ=='><div id="ihotspot86" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 121.3%; left: 29.4%;"> </div></a><!--Tirupur Water Supply Project-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=gDY5C_Bs_6CQT0nM7q61jA=='><div id="ihotspot87" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 113.4%; left: 38.85%;"> </div></a><!--Chennai Bypass-->
                            <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=UOFNBx1w7c_Xyy9_--4GZw=='><div id="ihotspot88" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 111.75%; left: 41.46%;"> </div></a><!--Ennore Port Rock Quarry & Breakwaters-->
                            <a href="http://localhost/hrms/completedIGCAR.aspx"><div id="ihotspot89" class="ihotspot_On" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 118.25%; left: 39.39%;"> </div></a><!--IGCAR FRFCF 2-->

                             <!--Kerala-->
                             <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=oUyLuS70h1DSgp5bL1vbdg=='><div id="ihotspot90" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 128%; left: 29.1%;"> </div></a><!--Idukki Dam-->
                             <a href='<%=ReturnUrl("projectOnOngoingpage")%>?projectid=kZhfzguQ06Rjp7exkSBk_A=='><div id="ihotspot91" class="ihotspot" style="position: absolute; z-index: 103; width: 0.7%; height: 0.7%; top: 126%; left: 26.8%;"> </div></a><!--Cochin Oil Tanker Terminal-->
                        </div>
                    </div>
        
            </section>
        </div>  
</div>
    <div style="height:500px"></div>
            
            <asp:Panel ID="pnlproject" runat="server" Visible="false">              
                <asp:Repeater ID="rptrproject" runat="server">
                      <ItemTemplate>
                        <div class="commentsbyuser slideInLeft animated innerbd_wallnew">
                            <div class="userinfo">
                                <div class="user-name">
                                    <a id="lnkusernameew" href='<%#getprojectURL(Eval("projectid")) %>'>
                                        <%# Eval("projecttitle") %>
                                    </a>
                                    <br />
                                 <%--  Project Date :
                                    <asp:Label ID="lbldate" visible="false" runat="server" Text='<%# Eval("projectcompletedate")%>'></asp:Label>--%>
                                </div>
                                <div class="user-rating">
                                </div>

                               <%-- <div class="user-comment">
                                    <%# Eval("projectdescsmall").ToString().Length > 150 ? Eval("projectdescsmall").ToString().Substring(0, 147)+"..." : Eval("projectdescsmall")   %>
                                </div>--%>
                            </div>

                        </div>
                    </ItemTemplate>
                </asp:Repeater>
               <%-- <asp:Repeater ID="project" runat="server">
                    <asp:Label runat="server" Text="<%#Eval('projecttitle')%>"></asp:Label>
                </asp:Repeater>--%>
                <div class="grid-pager">
                    
                    <asp:Repeater ID="rptPager" runat="server">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'
                                OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:Repeater>
                 
                </div>
            </asp:Panel>
            <div class="projectlblmsg">
           <asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>
                        
             </div>
      
 
     

<%--<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>--%>
<%-- <script src="jquery.rwdImageMaps_Min"></script>
<script src="jquery.rwdImageMaps.min.js"></script>--%>
    
     <script src="MAP_IMG/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="MAP_IMG/jquery.app.ui.js" type="text/javascript"></script>    
	<script type="text/javascript" src="MAP_IMG/litetooltip.min.js" ></script>
<script>

    //document.getElementById("MainContent_ImgMapIndia").addEventListener("mouseover", mouseOver);    
    //function mouseOver() {
    //    var x = event.clientX;     // Get the horizontal coordinate
    //    var y = event.clientY;     // Get the vertical coordinate
    //    var coor = "X coords: " + x + ", Y coords: " + y;
    //    //alert(coor);
    //}

    /*$(document).ready(function(e) {
        $('img[usemap]').rwdImageMaps();
	
        $('area').on('click', function() {
            alert($(this).attr('alt') + ' clicked');
        });
    });*/

    $(document).ready(function () {
        // side bar         
        $(".ihotspot").css({
            "border": "solid 1px black",
            "background": "gray",
            "border-radius": "6px"
        });
        $(".ihotspot_On").css({
            "border": "solid 1px blue",
            "background": "rgb(12, 5, 250)",
            "border-radius": "6px"
        });

        function blink_hotspot() {
            //  image
            $('.ihotspot').animate({ "opacity": '0.3' }, 'slow').animate({ 'opacity': '0.8' }, 'slow', function () { blink_hotspot(); });
            $('.ihotspot_On').animate({ "opacity": '0.3' }, 'slow').animate({ 'opacity': '0.8' }, 'slow', function () { blink_hotspot(); });
        }
        blink_hotspot();
    });



    $('#ihotspot1').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Chutak HEP</h4>' + '</div>' });
    $('#ihotspot2').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 3, title: '<div class="template">' + '<h4>Nimoo Bazgo HEP</h4>' + '</div>' });
    $('#ihotspot3').LiteTooltip({ location: 'left', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Kishanganga HEP</h4>' + '</div>' });
    $('#ihotspot4').LiteTooltip({ location: 'left', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>URI II HEP</h4>' + '</div>' });
    $('#ihotspot5').LiteTooltip({ location: 'left', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Mughal Road</h4>' + '</div>' });
    $('#ihotspot6').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Pir Panjal Railway Tunnel</h4>' + '</div>' });
    $('#ihotspot7').LiteTooltip({ location: 'left', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Salal HEP</h4>' + '</div>' });
    $('#ihotspot8').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>T-49,T13 & Part of T14</h4>' + '</div>' });
    $('#ihotspot9').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Ramban Banihal Road</h4>' + '</div>' });
    $('#ihotspot10').LiteTooltip({ location: 'left', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Sawalkote Access Tunnel</h4>' + '</div>' });
    $('#ihotspot11').LiteTooltip({ location: 'left', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Anji Khad Cable Stayed Bridge</h4>' + '</div>' });
    $('#ihotspot12').LiteTooltip({ location: 'left', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Chamera HEP Stage I & III</h4>' + '</div>' });
    $('#ihotspot13').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Kashang HEP</h4>' + '</div>' });
    $('#ihotspot14').LiteTooltip({ location: 'left', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Sainj HEP</h4>' + '</div>' });
    $('#ihotspot15').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Nathpa Jhakri HEP</h4>' + '</div>' });
    $('#ihotspot16').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Sawara Kuddu HEP</h4>' + '</div>' });

    $('#ihotspot17').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Tehri PSP</h4>' + '</div>' });
    $('#ihotspot18').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Vishnugad Pipalkoti HEP</h4>' + '</div>' });
    $('#ihotspot19').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Tapovan Vishnugad HEP</h4>' + '</div>' });
    $('#ihotspot20').LiteTooltip({ location: 'left', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Kapurthala Rail Coach Factory</h4>' + '</div>' });

    $('#ihotspot21').LiteTooltip({ location: 'left', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Delhi Metro Projects</h4>' + '</div>' });
    $('#ihotspot22').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Delhi Faridabad Elevated Expressway</h4>' + '</div>' });
    $('#ihotspot23').LiteTooltip({ location: 'left', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Munirka Flyover</h4>' + '</div>' });

    $('#ihotspot24').LiteTooltip({ location: 'left', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Rajasthan Atomic Power Projects</h4>' + '</div>' });
    $('#ihotspot25').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>RJ7</h4>' + '</div>' });
    $('#ihotspot26').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Parwan Gravity Dam</h4>' + '</div>' });


    $('#ihotspot27').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Kachchh Branch Canal</h4>' + '</div>' });
    $('#ihotspot28').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Saurashtra Branch Canal</h4>' + '</div>' });
    $('#ihotspot29').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>NC25</h4>' + '</div>' });
    $('#ihotspot30').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Kakrapar Atomic Power Project</h4>' + '</div>' });

    $('#ihotspot93').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Spice Plant</h4>' + '</div>' });
    $('#ihotspot31').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Narora Atomic Power Project</h4>' + '</div>' });
    $('#ihotspot32').LiteTooltip({ location: 'left', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Gomti Acqueduct</h4>' + '</div>' });
    $('#ihotspot33').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Lucknow Muzaffarpur National Highway </h4>' + '</div>' });
    $('#ihotspot34').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Allahabad Bypass</h4>' + '</div>' });
    $('#ihotspot35').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Naini Bridge</h4>' + '</div>' });

    $('#ihotspot36').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Bistan Lift Irrigation Scheme</h4>' + '</div>' });

    $('#ihotspot37').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Dhule Palesner Road</h4>' + '</div>' });
    $('#ihotspot38').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Vaitarna Dam</h4>' + '</div>' });
    $('#ihotspot39').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Maroshi Ruparel Water Supply Tunnel</h4>' + '</div>' });
    $('#ihotspot40').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Bandra Worli Sea Link</h4>' + '</div>' });
    $('#ihotspot41').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Mumbai Metro Rail, Line 3</h4>' + '</div>' });
    $('#ihotspot42').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Mumbai Coastal Road</h4>' + '</div>' });
    $('#ihotspot44').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Residential Towers Project at Anushaktinagar(DAE)</h4>' + '</div>' });
    $('#ihotspot92').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Aether, Photogravurs, Ipanema</h4>' + '</div>' });
    $('#ihotspot45').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>BARC Tarapur</h4>' + '</div>' });
    $('#ihotspot46').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>DGNP Dry Dock and Wharves</h4>' + '</div>' });
    $('#ihotspot47').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Bhandup Water Treatment Plant</h4>' + '</div>' });
    $('#ihotspot48').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Bhor Ghat Railway Tunnel</h4>' + '</div>' });
    $('#ihotspot49').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Mumbai Pune Expressway</h4>' + '</div>' });
    $('#ihotspot50').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Pune Metro I & II</h4>' + '</div>' });
    $('#ihotspot51').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Koyna Powerhouse</h4>' + '</div>' });

    $('#ihotspot52').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Aditya Aluminium Projects</h4>' + '</div>' });
    $('#ihotspot53').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Bhilai Steel Plant</h4>' + '</div>' });
    $('#ihotspot54').LiteTooltip({ location: 'left', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Dagachhu HEP</h4>' + '</div>' });
    $('#ihotspot55').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Tala HEP</h4>' + '</div>' });
    $('#ihotspot56').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Punatsangchhu HEP</h4>' + '</div>' });
    $('#ihotspot57').LiteTooltip({ location: 'left', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Nikachhu HEP</h4>' + '</div>' });
    $('#ihotspot58').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Teesta Low Dam HEP Stage IV</h4>' + '</div>' });

    $('#ihotspot59').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Farakka Barrage</h4>' + '</div>' });
    $('#ihotspot60').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Kolkata Metro</h4>' + '</div>' });

    $('#ihotspot61').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Haldia Dock</h4>' + '</div>' });
    $('#ihotspot62').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Kolkata Elevated Road Corridor</h4>' + '</div>' });
    $('#ihotspot94').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Holiday Inn Express</h4>' + '</div>' });
    $('#ihotspot63').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>NH34 Projects</h4>' + '</div>' });
    $('#ihotspot64').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Rooppur Nuclear Power Plant</h4>' + '</div>' });
    $('#ihotspot65').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Sone Bridge</h4>' + '</div>' });
    $('#ihotspot66').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Muzaffarpur Thermal Power Project</h4>' + '</div>' });
    $('#ihotspot67').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Sone Barrage</h4>' + '</div>' });
    $('#ihotspot68').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Pare HEP</h4>' + '</div>' });
    $('#ihotspot69').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Bogibeel Rail-cum-Road Bridge</h4>' + '</div>' });
    $('#ihotspot70').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Numaligarh Jorhat Road</h4>' + '</div>' });
    $('#ihotspot71').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Saraighat Bridge</h4>' + '</div>' });
    $('#ihotspot72').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Kaliabhomora Bridge</h4>' + '</div>' });
    $('#ihotspot73').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Manipur Railway Tunnels</h4>' + '</div>' });
    $('#ihotspot95').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Imphal-Kangchup-Tamenglong Road Project</h4>' + '</div>' });

    $('#ihotspot74').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Godavari Lift Irrigation Scheme</h4>' + '</div>' });
    $('#ihotspot75').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Rajiv Dummugudem Lift Irrigation Scheme</h4>' + '</div>' });
    $('#ihotspot76').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Pranhitha Chevella Lift Irrigation Scheme</h4>' + '</div>' });
    $('#ihotspot77').LiteTooltip({ location: 'left', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>AP8</h4>' + '</div>' });
    $('#ihotspot78').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Veligonda Tunnel</h4>' + '</div>' });
    $('#ihotspot79').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Polavaram Canal</h4>' + '</div>' });
    $('#ihotspot80').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Vizag Oil Cavern</h4>' + '</div>' });

    $('#ihotspot81').LiteTooltip({ location: 'left', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Tata Memorial Hospital</h4>' + '</div>' });
    $('#ihotspot82').LiteTooltip({ location: 'left', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Padur Oil Cavern</h4>' + '</div>' });
    $('#ihotspot83').LiteTooltip({ location: 'left', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Bangalore Metro</h4>' + '</div>' });
    $('#ihotspot84').LiteTooltip({ location: 'left', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Yettinahole Project</h4>' + '</div>' });
    $('#ihotspot85').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Kudankulam Nuclear Power Plant </h4>' + '</div>' });
    $('#ihotspot86').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Tirupur Water Supply</h4>' + '</div>' });
    $('#ihotspot87').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Chennai Bypass</h4>' + '</div>' });
    $('#ihotspot88').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Ennore Port Rock Quarry & Breakwaters</h4>' + '</div>' });
    $('#ihotspot89').LiteTooltip({ location: 'right', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>IGCAR Projects</h4>' + '</div>' });
    $('#ihotspot90').LiteTooltip({ location: 'left', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Idukki Dam </h4>' + '</div>' });
    $('#ihotspot91').LiteTooltip({ location: 'left', textalign: 'left', templatename: 'BostonBlue', padding: 5, title: '<div class="template">' + '<h4>Cochin Oil Tanker Terminal</h4>' + '</div>' });


</script>
      
 
</asp:Content>

 



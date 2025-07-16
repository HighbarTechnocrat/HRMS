<%@ Control Language="C#" AutoEventWireup="true" CodeFile="footer.ascx.cs" Inherits="themes_creative1" %>
<%--Comment by Sanjay Footer control not required <%@ Register Src="~/themes/creative1.0/LayoutControls/footercat.ascx" TagName="catfooter" TagPrefix="uc" %>
<%@ Register Src="~/themes/creative1.0/LayoutControls/footerpost.ascx" TagName="footerpost" TagPrefix="uc" %>--%>
 <footer id="main-footer" class="">
            <!-- START FOOTER WIDGETS -->
            <div id="widgets">
                <div class="container">
					<!-- class="row"-->
                    <div>
						<!--class="widget col-md-4 widget_text animate-me fadeIn" -->
                        <div id="text-3" >
                           <h3>Address</h3>
							<div class="textwidget">
							HIGHBAR TECHNOCRAT LIMITED<br/>
							D-Wing, 14th Floor, Empire Tower, Reliable Cloud City, Off. Thane-Belapur Road, Airoli, Navi Mumbai – 400 708.<br />
                            GST #: 27AABCO4311L1ZI
							</div>
<%--							 <div class="anyqueries1">
                            <span>For any queries, please write to : </span>
                              <span class="anyqueriesEmail"><a href="mailto:hrms@highbartech.com" >hrms@highbartech.com</a></span>
                            </div>--%>
                                  
                        </div>
                        <%--Comment by Sanjay Footer control not required<uc:footerpost ID="uxfooterpost" runat="server" visible="false"/>
                        <uc:catfooter ID="uxcatfooter" runat="server" visible="false"/>--%>
                    </div>
                </div>
            </div>
            <!-- START COPYRIGHT -->
            <div id="copyright">			
                 <p> Copyright © 2020 Highbar. All rights reserved.&nbsp;
				
				</p> 
            </div>
        </footer>
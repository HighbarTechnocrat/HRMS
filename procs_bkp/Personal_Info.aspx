<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" EnableViewState="true"
   MaintainScrollPositionOnPostback="true" CodeFile="Personal_Info.aspx.cs" Inherits="Personal_Info" EnableSessionState="True" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <%--<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <%-- <link href="<%=ReturnUrl("css") %>calendar/calendar.css" rel="stylesheet" type="text/css" />
    <link href="sampleform_css.css" rel="stylesheet" type="text/css" />--%>
    <%--<ajaxToolkit:ToolkitScriptManager ID="srcp" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
             /*background: #dae1ed;*/
           background:#ebebe4;
        }

        .taskparentclasskkk {
            width: 29.5%;
            height: 72px;
            /*overflow:initial;*/
        }

        #MainContent_lstApprover, #MainContent_lstIntermediate {
            padding: 0 0 33px 0 !important;
            /*overflow: unset;*/
        }

        .Dropdown {
            border-bottom: 2px solid #cccccc;
            /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
            background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;
            cursor: default;
        }
        .w3-third {
    /*width: 25% !important;*/
}
        .tbls2 {
    margin: 35% 0 0% -35% !important;
}
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
    <script type="text/javascript">
        var deprt;
        $(document).ready(function () {
            $("#MainContent_txtcity").autocomplete("<%=ReturnUrl("sitepathmain") %>Search_City.ashx", {
                inputClass: "ac_input",
                resultsClass: "ac_results",
                loadingClass: "ac_loading",
                minChars: 1,
                delay: 1,
                matchCase: false,
                matchSubset: false,
                matchContains: false,
                cacheLength: 0,
                max: 12,
                mustMatch: false,
                extraParams: {},
                selectFirst: false,
                formatItem: function (row) { return row[0] },
                formatMatch: null,
                autoFill: false,
                width: 0,
                multiple: false,
                scroll: false,
                scrollHeight: 180,
                success: function (data) {
                    response($.map(data, function (item) {
                        return {

                        }
                    }));
                },
                context: this
            });

            $("#MainContent_txtloc").autocomplete("<%=ReturnUrl("sitepathmain") %>Search_location.ashx", {
                inputClass: "ac_input",
                resultsClass: "ac_results",
                loadingClass: "ac_loading",
                minChars: 1,
                delay: 1,
                matchCase: false,
                matchSubset: false,
                matchContains: false,
                cacheLength: 0,
                max: 12,
                mustMatch: false,
                extraParams: {},
                selectFirst: false,
                formatItem: function (row) { return row[0] },
                formatMatch: null,
                autoFill: false,
                width: 0,
                multiple: false,
                scroll: false,
                scrollHeight: 180,
                success: function (data) {
                    response($.map(data, function (item) {
                        return {

                        }
                    }));
                },
                context: this
            });


            $("#MainContent_txtsubdept").autocomplete("<%=ReturnUrl("sitepathmain") %>Search_subdepartment.ashx?d=" + deprt, {
                inputClass: "ac_input",
                resultsClass: "ac_results",
                loadingClass: "ac_loading",
                minChars: 1,
                delay: 1,
                matchCase: false,
                matchSubset: false,
                matchContains: false,
                cacheLength: 0,
                max: 12,
                mustMatch: false,
                extraParams: { d: deprt },
                selectFirst: false,
                formatItem: function (row) { return row[0] },
                formatMatch: null,
                autoFill: false,
                width: 0,
                multiple: false,
                scroll: false,
                scrollHeight: 180,
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                        }
                    }));
                },

                context: this
            });
        });
    </script>

    <!DOCTYPE html>
<html>
<title>W3.CSS Template</title>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1">
<link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css">
<link rel='stylesheet' href='https://fonts.googleapis.com/css?family=Roboto'>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
<style>
html,body,h1,h2,h3,h4,h5,h6 {font-family: "Roboto", sans-serif}
</style>
<body class="w3-light-grey">

<!-- Page Container -->
<div class="w3-content w3-margin-top" style="max-width:1400px;">

  <!-- The Grid -->
  <div class="w3-row-padding">
  
    <!-- Left Column -->
    <div class="w3-third">
    
      <div class="w3-white w3-text-grey w3-card-4">
        <div class="w3-display-container">
          <img id="emp_img" runat="server" class="img-responsive" style="width:100%" />
          <div class="w3-display-bottomleft w3-container w3-text-White">
            <h4><asp:Label ID="lbl_EmpName" Text="Mr. Ashok Wani" runat="server" ForeColor="White" BackColor="Black"></asp:Label></h4>
          </div>
        </div>
        <div class="w3-container">
          <p><i class="fa fa-info-circle fa-fw w3-margin-right w3-large w3-text-teal"></i><asp:Label ID="lbl_code" Text="Emp Code: 00003851" runat="server"></asp:Label></p>
          <p><i class="fa fa-calendar fa-fw w3-margin-right w3-large w3-text-teal"></i><asp:Label ID="lbl_DOJ" Text="Joining Date" runat="server"></asp:Label> </p>
          <p><i class="fa fa-map fa-fw w3-margin-right w3-large w3-text-teal"></i><asp:Label ID="lbl_Location" Text="Location Name" runat="server"></asp:Label> </p>
          <p><i class="fa fa-suitcase fa-fw w3-margin-right w3-large w3-text-teal"></i><asp:Label ID="lbl_dept" Text="Department Name" runat="server"></asp:Label></p>
          <p><i class="fa fa-briefcase fa-fw w3-margin-right w3-large w3-text-teal"></i><asp:Label ID="lbl_desg" Text="Designation Name" runat="server"></asp:Label></p>
          <%--<p><i class="fa fa-home fa-fw w3-margin-right w3-large w3-text-teal"></i>London, UK</p>--%>
          <p><i class="fa fa-info-circle fa-fw w3-margin-right w3-large w3-text-teal"></i><asp:Label ID="lbl_band" Text="Band - VIII" runat="server"></asp:Label></p>
          <p><i class="fa fa-envelope fa-fw w3-margin-right w3-large w3-text-teal"></i><asp:Label ID="lbl_email" Text="email@comapny.com" runat="server"></asp:Label></p>
          <p><i class="fa fa-mobile fa-fw w3-margin-right w3-large w3-text-teal"></i><asp:Label ID="lbl_mob" Text="mobile no" runat="server"></asp:Label></p>
          <p><i class="fa fa-phone fa-fw w3-margin-right w3-large w3-text-teal"></i><asp:Label ID="lbl_telephone" Text="Telephone no" runat="server"></asp:Label></p>
          <hr />

          <p class="w3-large"><b><i class="fa fa-asterisk fa-fw w3-margin-right w3-text-teal"></i>Personal Information</b></p>
          <p><i class="fa fa-birthday-cake fa-fw w3-margin-right w3-large w3-text-teal"></i><asp:Label ID="lbl_dob" Text="03-01-1983" runat="server"></asp:Label></p>
          <p><i class="fa fa-mars fa-fw w3-margin-right w3-large w3-text-teal"></i><asp:Label ID="lbl_gender" Text="Male" runat="server"></asp:Label></p>
          <p><i class="fa fa-tint fa-fw w3-margin-right w3-large w3-text-red"></i><asp:Label ID="lbl_bloodgrp" Text="O+" runat="server"></asp:Label></p>
          <p><i class="fa fa-group fa-fw w3-margin-right w3-large w3-text-teal"></i><asp:Label ID="lbl_ismarried" Text="Married" runat="server"></asp:Label></p>
          <p><i class="fa fa-envelope-open fa-fw w3-margin-right w3-large w3-text-teal"></i><asp:Label ID="lbl_personalemail" Text="Personal Mail ID" runat="server"></asp:Label></p>
          <hr />

                    
          <p class="w3-large"><b><i class="fa fa-ambulance fa-fw w3-margin-right w3-text-red"></i>Emergency Contact</b></p>
          <p><i class="fa fa-user fa-fw w3-margin-right w3-large w3-text-teal"></i><asp:Label ID="lbl_cont1_name" Text="1. Person" runat="server"></asp:Label></p>
          <p><i class="fa fa-mobile fa-fw w3-margin-right w3-large w3-text-red"></i><asp:Label ID="lbl_cont1_no" Text="8888888888" runat="server"></asp:Label></p>
          <p><i class="fa fa-user fa-fw w3-margin-right w3-large w3-text-teal"></i><asp:Label ID="lbl_cont2_name" Text="2. Person" runat="server"></asp:Label></p>
          <p><i class="fa fa-mobile fa-fw w3-margin-right w3-large w3-text-red"></i><asp:Label ID="lbl_cont2_no" Text="9999999999" runat="server"></asp:Label></p>


          <%--<p class="w3-large"><b><i class="fa fa-asterisk fa-fw w3-margin-right w3-text-teal"></i>Skills</b></p>
          <p>Adobe Photoshop</p>
          <div class="w3-light-grey w3-round-xlarge w3-small">
            <div class="w3-container w3-center w3-round-xlarge w3-teal" style="width:90%">90%</div>
          </div>
          <p>Photography</p>
          <div class="w3-light-grey w3-round-xlarge w3-small">
            <div class="w3-container w3-center w3-round-xlarge w3-teal" style="width:80%">
              <div class="w3-center w3-text-white">80%</div>
            </div>
          </div>
          <p>Illustrator</p>
          <div class="w3-light-grey w3-round-xlarge w3-small">
            <div class="w3-container w3-center w3-round-xlarge w3-teal" style="width:75%">75%</div>
          </div>
          <p>Media</p>
          <div class="w3-light-grey w3-round-xlarge w3-small">
            <div class="w3-container w3-center w3-round-xlarge w3-teal" style="width:50%">50%</div>
          </div>
          <br>

          <p class="w3-large w3-text-theme"><b><i class="fa fa-globe fa-fw w3-margin-right w3-text-teal"></i>Languages</b></p>
          <p>English</p>
          <div class="w3-light-grey w3-round-xlarge">
            <div class="w3-round-xlarge w3-teal" style="height:24px;width:100%"></div>
          </div>
          <p>Spanish</p>
          <div class="w3-light-grey w3-round-xlarge">
            <div class="w3-round-xlarge w3-teal" style="height:24px;width:55%"></div>
          </div>
          <p>German</p>
          <div class="w3-light-grey w3-round-xlarge">
            <div class="w3-round-xlarge w3-teal" style="height:24px;width:25%"></div>
          </div>--%>
          <br>
        </div>
      </div><br>

    <!-- End Left Column -->
    </div>

    <!-- Right Column -->
    <div class="w3-twothird">
    <a href="http://localhost/hrms/PersonalDocuments.aspx" class="aaaa">My Corner</a>
      <div class="w3-container w3-card w3-white w3-margin-bottom">
        <h3 class="w3-text-grey w3-padding-12"><i class="fa fa-home fa-fw w3-margin-right w3-xlarge w3-text-teal"></i>Address</h3>
        <div class="w3-container">
          <h5 class="w3-opacity"><b>Current Address</b></h5>
          <%--<h6 class="w3-text-teal"><i class="fa fa-calendar fa-fw w3-margin-right"></i>Jan 2015 - <span class="w3-tag w3-teal w3-round">Current</span></h6>--%>
          <p><asp:Label runat="server" ID="lbl_curadd" Text="Lorem ipsum dolor sit amet. Praesentium magnam consectetur vel in deserunt aspernatur est reprehenderit sunt hic. Nulla tempora soluta ea et odio, unde doloremque repellendus iure, iste."></asp:Label> </p>
        </div>
        <div class="w3-container">
          <h5 class="w3-opacity"><b>Permanent Address</b></h5>
          <%--<h6 class="w3-text-teal"><i class="fa fa-calendar fa-fw w3-margin-right"></i>Mar 2012 - Dec 2014</h6>--%>
          <p><asp:Label runat="server" ID="lbl_peradd" Text="Consectetur adipisicing elit. Praesentium magnam consectetur vel in deserunt aspernatur est reprehenderit sunt hic. Nulla tempora soluta ea et odio, unde doloremque repellendus iure, iste."></asp:Label></p>
          <hr />
        </div>
<%--        <div class="w3-container">
          <h5 class="w3-opacity"><b>Graphic Designer / designsomething.com</b></h5>
          <h6 class="w3-text-teal"><i class="fa fa-calendar fa-fw w3-margin-right"></i>Jun 2010 - Mar 2012</h6>
          <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. </p><br>
        </div>--%>
      </div>

      <div class="w3-container w3-card w3-white">
        <h3 class="w3-text-grey w3-padding-12"><i class="fa fa-globe fa-fw w3-margin-right w3-xlarge w3-text-teal"></i>Passport Details</h3>
          <div class="w3-container">
          <table>
                <tr>
                    <td>
                        <p><asp:Label ID="lbl_Passport" Text="Passport no:" runat="server"></asp:Label></p>
                    </td>
                </tr>

                <tr>
                    <td>
                        <p><asp:Label ID="lbl_date_Pass_Issue" Text="Date of issue:" runat="server"></asp:Label></p>
                    </td>
                </tr>
                <tr>
                    <td>
                        <p><asp:Label ID="lbl_Pass_Issue" Text="Place of issue:" runat="server"></asp:Label></p>
                    </td>

                </tr>

                <tr>
                    <td>
                        <p><asp:Label ID="lbl_date_Pass_Exp" Text="Expiry Date:" runat="server"></asp:Label></p>
                    </td>
                </tr>
                <tr>
                    <td>
                        <p><asp:Label ID="lbl_ECR" Text="ECR Stamp:" runat="server"></asp:Label></p>
                    </td>
                </tr>
          </table>

        </div>

      </div>

      <div class="w3-container w3-card w3-white">
        <h3 class="w3-text-grey w3-padding-12"><i class="fa fa-bank fa-fw w3-margin-right w3-xlarge w3-text-teal"></i>Banking Details</h3>
          <div class="w3-container">
          <table>
                <tr>
                    <td>
                        <p><asp:Label ID="lbl_bank_ac_name" Text="Bank A/c Name:" runat="server"></asp:Label></p>
                    </td>
                </tr>

                <tr>
                    <td>
                        <p><asp:Label ID="lbl_bank_ac_no" Text="Bank A/c No:" runat="server"></asp:Label></p>
                    </td>
                </tr>
                <tr>
                    <td>
                        <p><asp:Label ID="lbl_bank_name" Text="Bank Name:" runat="server"></asp:Label></p>
                    </td>

                </tr>

                <tr>
                    <td>
                        <p><asp:Label ID="lbl_bank_branch" Text="Bank Branch No:" runat="server"></asp:Label></p>
                    </td>
                </tr>
                <tr>
                    <td>
                        <p><asp:Label ID="lbl_IFSC" Text="IFSC Code:" runat="server"></asp:Label></p>
                    </td>
                </tr>

                <tr>
                    <td>
                        <p><asp:Label ID="lbl_MICR" Text="MICR Code:" runat="server"></asp:Label></p>
                    </td>
                </tr>

                <tr>
                    <td colspan="2">
                        <p><asp:Label ID="lbl_bank_add" Text="Bank Address:" runat="server"></asp:Label></p>
                    </td>
                </tr>
          </table>

        </div>
<%--        <div class="w3-container">
          <h5 class="w3-opacity"><b>W3Schools.com</b></h5>
          <h6 class="w3-text-teal"><i class="fa fa-calendar fa-fw w3-margin-right"></i>Forever</h6>
          <p>Web Development! All I need to know in one place</p>
          <hr>
        </div>
        <div class="w3-container">
          <h5 class="w3-opacity"><b>London Business School</b></h5>
          <h6 class="w3-text-teal"><i class="fa fa-calendar fa-fw w3-margin-right"></i>2013 - 2015</h6>
          <p>Master Degree</p>
          <hr>
        </div>
        <div class="w3-container">
          <h5 class="w3-opacity"><b>School of Coding</b></h5>
          <h6 class="w3-text-teal"><i class="fa fa-calendar fa-fw w3-margin-right"></i>2010 - 2013</h6>
          <p>Bachelor Degree</p><br>
        </div>--%>
      </div>

      <div class="w3-container w3-card w3-white">
        <h3 class="w3-text-grey w3-padding-12"><i class="fa fa-id-card fa-fw w3-margin-right w3-xlarge w3-text-teal"></i>Other Details</h3>
          <div class="w3-container">
          <table>
                <tr>
                    <td>
                        <p><asp:Label ID="lbl_PAN" Text="PAN:" runat="server"></asp:Label></p>
                    </td>
                </tr>

                <tr>
                    <td>
                        <p><asp:Label ID="lbl_AADHAR" Text="AADHAR:" runat="server"></asp:Label></p>
                    </td>
                </tr>
                <tr>
                    <td>
                        <p><asp:Label ID="lbl_PF" Text="PF no:" runat="server"></asp:Label></p>
                    </td>

                </tr>

                <tr>
                    <td>
                        <p><asp:Label ID="lbl_UAN" Text="UAN no:" runat="server"></asp:Label></p>
                    </td>
                </tr>
          </table>

        </div>

      </div>

      <div class="w3-container w3-card w3-white">
        <h3 class="w3-text-grey w3-padding-12"><i class="fa fa-id-card fa-fw w3-margin-right w3-xlarge w3-text-teal"></i>Visa Details</h3>
            <asp:GridView runat="server" ID="dgVisadetails" CssClass="table table-responsive table-striped" AutoGenerateColumns="False" >
                <HeaderStyle BackColor="#00988B" Font-Bold="True" ForeColor="#3D1956" />
                    <Columns>
                        <asp:BoundField HeaderText="Country"
                            DataField="COUNTRY"
                            ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-Width="40%" />

                        <asp:BoundField HeaderText="Visa Type"
                            DataField="TYPE_OF_VISA"
                            ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-Width="20%" />

                        <asp:BoundField HeaderText="Expiry Date"
                            DataField="EXPIRY_DATE"
                            ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-Width="20%" />

                        <asp:BoundField HeaderText="Validity"
                            DataField="VALIDITY"
                            ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-Width="20%" />
                    </Columns>
                </asp:GridView>
            </div>

      <div class="w3-container w3-card w3-white">
        <h3 class="w3-text-grey w3-padding-12"><i class="fa fa-id-card fa-fw w3-margin-right w3-xlarge w3-text-teal"></i>Mediclaim Members</h3>
            <asp:GridView runat="server" ID="dgMediclaim" CssClass="table table-responsive table-striped" AutoGenerateColumns="False" >
                <HeaderStyle BackColor="#00988B" Font-Bold="True" ForeColor="#3D1956" />
                    <Columns>
                        <asp:BoundField HeaderText="Member Name"
                            DataField="MemberName"
                            ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-Width="40%" />

                        <asp:BoundField HeaderText="Member Relation"
                            DataField="Member_Rel"
                            ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-Width="20%" />

                        <asp:BoundField HeaderText="BirthDate"
                            DataField="BirthDate"
                            ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-Width="20%" />

                        <asp:BoundField HeaderText="Age"
                            DataField="Age"
                            ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" DataFormatString="{0:0}"/>

                        <asp:BoundField HeaderText="Sex"
                            DataField="Member_Sex"
                            ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />

                        
                    </Columns>
                </asp:GridView>
            </div>

      <div class="w3-container w3-card w3-white">
        <h3 class="w3-text-grey w3-padding-12"><i class="fa fa-id-card fa-fw w3-margin-right w3-xlarge w3-text-teal"></i>PF Nominatiions</h3>
            <asp:GridView runat="server" ID="dgPF" CssClass="table table-responsive table-striped" AutoGenerateColumns="False" >
                <HeaderStyle BackColor="#00988B" Font-Bold="True" ForeColor="#3D1956" />
                    <Columns>
                        <asp:BoundField HeaderText="Nominee Name"
                            DataField="NomineeName"
                            ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-Width="40%" />

                        <asp:BoundField HeaderText="Nominee Relation"
                            DataField="Nominee_Rel"
                            ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-Width="20%" />

                        <asp:BoundField HeaderText="BirthDate"
                            DataField="BirthDate"
                            ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-Width="20%" />

                        <asp:BoundField HeaderText="Age"
                            DataField="Age"
                            ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />

                        <asp:BoundField HeaderText="Sex"
                            DataField="Nominee_Sex"
                            ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />

                        
                    </Columns>
                </asp:GridView>
            </div>
    <!-- End Right Column -->
    </div>
    
  <!-- End Grid -->
  </div>
  
  <!-- End Page Container -->
</div>

<footer class="w3-container w3-teal w3-center w3-margin-top">
<%--  <p>Find me on social media.</p>
  <i class="fa fa-facebook-official w3-hover-opacity"></i>
  <i class="fa fa-instagram w3-hover-opacity"></i>
  <i class="fa fa-snapchat w3-hover-opacity"></i>
  <i class="fa fa-pinterest-p w3-hover-opacity"></i>
  <i class="fa fa-twitter w3-hover-opacity"></i>
  <i class="fa fa-linkedin w3-hover-opacity"></i>
  <p>Powered by <a href="https://www.w3schools.com/w3css/default.asp" target="_blank">w3.css</a></p>--%>
</footer>

</body>
</html>

<%--    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Personal Information"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>                
                <div>
                    <span>
                    <a href="PersonalDocuments.aspx" class="aaaa" >My Corner</a>
                    </span>
                </div>
                <div class="edit-contact">
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server" Visible="false"></asp:LinkButton>
                        </div>
                    </div>

                    <ul id="editform" runat="server" visible="false">
                        <li class="trvl_date">
                            <span>Employee Code </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_EmpCode" runat="server" AutoPostBack="True"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>Employee Name </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="TextBox1" runat="server" AutoPostBack="True"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>Gender </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="TextBox2" runat="server" AutoPostBack="True"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>Employment </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="TextBox3" runat="server" AutoPostBack="True"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>E-mail address </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="TextBox4" runat="server" AutoPostBack="True"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>Mobile </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="TextBox5" runat="server" AutoPostBack="True"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>Birth Date </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="TextBox6" runat="server" AutoPostBack="True"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>Blood Group </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="TextBox7" runat="server" AutoPostBack="True"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>Marrital Status </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="TextBox8" runat="server" AutoPostBack="True"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>Emergency Contact Person</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="TextBox9" runat="server" AutoPostBack="True"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>Emergency Contact no. 1</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="TextBox10" runat="server" AutoPostBack="True"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>Emergency Contact no. 2</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="TextBox11" runat="server" AutoPostBack="True"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>Current Address</span>
                            <br />
                            <asp:TextBox AutoComplete="off" Width="450px" TextMode="MultiLine" ID="TextBox12" runat="server" AutoPostBack="True"></asp:TextBox>
                        </li>
                    </ul>
                </div> 
            </div>
        </div>
    </div>--%>

    <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
    <asp:TextBox AutoComplete="off" ID="txtEmpName" runat="server" Visible="false"></asp:TextBox>
    <asp:Label ID="testsanjay" runat="server" Visible="false"></asp:Label>
    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />

    <asp:HiddenField ID="hdnvouno" runat="server" />
    <asp:HiddenField ID="hflEmpName" runat="server" />

    <asp:HiddenField ID="hflEmpDesignation" runat="server" />

    <asp:HiddenField ID="hflEmpDepartment" runat="server" />

    <asp:HiddenField ID="hflEmailAddress" runat="server" />

    <asp:HiddenField ID="hflGrade" runat="server" />
    <asp:HiddenField ID="hflEMPAGrade" runat="server" />

    <asp:HiddenField ID="hflapprcode" runat="server" />

    <asp:HiddenField ID="hdnDesk" runat="server" />

    <asp:HiddenField ID="hdnDestnation" runat="server" />

    <asp:HiddenField ID="hdnTripid" runat="server" />

    <asp:HiddenField ID="hdnAcctripid" runat="server" />

    <asp:HiddenField ID="hdnLcalTripid" runat="server" />

    <asp:HiddenField ID="hdnTraveltypeid" runat="server" />

    <asp:HiddenField ID="hdnDeptPlace" runat="server" />

    <asp:HiddenField ID="hdnTravelmode" runat="server" />

    <asp:HiddenField ID="hdnDeviation" runat="server" />

    <asp:HiddenField ID="hdnTrDetRequirements" runat="server" />

    <asp:HiddenField ID="hdnAccReq" runat="server" />

    <asp:HiddenField ID="hdnAccCOS" runat="server" />

    <asp:HiddenField ID="hdnlocaltrReq" runat="server" />

    <asp:HiddenField ID="hdnlocalTrCOS" runat="server" />

    <asp:HiddenField ID="hdnTravelConditionid" runat="server" />

    <asp:HiddenField ID="hdnApprId" runat="server" />

    <asp:HiddenField ID="hdnApprEmailaddress" runat="server" />

    <asp:HiddenField ID="hdnEligible" runat="server" />
    <asp:HiddenField ID="hdnTrdays" runat="server" />
     <asp:HiddenField ID="hdnTravelDtlsId" runat="server" />
     <asp:HiddenField ID="hdnAccId" runat="server" />
     <asp:HiddenField ID="hdnLocalId" runat="server" />
      <asp:HiddenField ID="hdnTravelstatus" runat="server" />
     <asp:HiddenField ID="hdnexp_id" runat="server" />
    <asp:HiddenField ID="hdnexptrvldtls_id" runat="server" />
     <asp:HiddenField ID="hdnfileid" runat="server" />
    <asp:HiddenField ID="hdnfrmdate_emial" runat="server" />
    <asp:HiddenField ID="hdntodate_emial" runat="server" />
    <asp:HiddenField ID="hdnActualTrvlDays" runat="server" />
    <asp:HiddenField ID="hdnmainexpStatus" runat="server" />
    <asp:HiddenField ID="hdnApprovalStatusExp" runat="server" />
    <asp:HiddenField ID="hdn_apprStatus" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />

    <asp:HiddenField ID="hdnfromdate_Trvl" runat="server" />
    <asp:HiddenField ID="hdnTodate_Trvl" runat="server" />
     <asp:HiddenField ID="hdnTryiptypeid" runat="server" />
     <asp:HiddenField ID="hdnCOS" runat="server" />
    <asp:HiddenField ID="hdntrdetailsid" runat="server" />
<asp:HiddenField ID="hdnTrvlBookdStatus" runat="server" />

    <asp:HiddenField ID="hdnAccdtlsid" runat="server" />
    <asp:HiddenField ID="hdnDaysDiff" runat="server" />
    <asp:HiddenField ID="hdnactualEligbility" runat="server" />
    <asp:HiddenField ID="hdnflatEligbility" runat="server" />
    <asp:HiddenField ID="hdnfromdate_Accm" runat="server" />
    <asp:HiddenField ID="hdnTodate_Accm" runat="server" />
    <asp:HiddenField ID="hdnactualdays" runat="server" />
    <asp:HiddenField ID="hdnIsThrughCOS" runat="server" />
        <asp:HiddenField ID="hdnAccomodationStatus" runat="server" />
    <asp:HiddenField ID="hdntripcharges_Accm" runat="server" />

    
     <asp:HiddenField ID="hdnCOS_Locl" runat="server" />

     <asp:HiddenField ID="hflGrade_Locl" runat="server" />

      <asp:HiddenField ID="hdnDeviation_Locl" runat="server" />
     <asp:HiddenField ID="hdnDesk_Locl" runat="server" />
         <asp:HiddenField ID="HiddenField1" runat="server" />

     <asp:HiddenField ID="hdnfromdate_Locl" runat="server" />
      <asp:HiddenField ID="hdnTodate_Locl" runat="server" />
  <asp:HiddenField ID="hdntrdetailsid_Locl" runat="server" />
      <asp:HiddenField ID="hdntrmodeid" runat="server" />

      <asp:HiddenField ID="hdndviation_s" runat="server" />
 
    <asp:HiddenField ID="hdnCOS_Oth" runat="server" />

    <asp:HiddenField ID="hflGrade_Oth" runat="server" />

    <asp:HiddenField ID="hdnDeviation_Oth" runat="server" />
    <asp:HiddenField ID="hdnDesk_Oth" runat="server" />

    <asp:HiddenField ID="hdnfromdate_Oth" runat="server" />
    <asp:HiddenField ID="hdnTodate_Oth" runat="server" />
    <asp:HiddenField ID="hdntrdetailsid_Oth" runat="server" />
    <asp:HiddenField ID="hdntxtAmt_Oth" runat="server" />
    <asp:HiddenField ID="hdnpagereqestfrm_Oth" runat="server" />
    <asp:HiddenField ID="hdnexpSrno_Oth" runat="server" />

    <asp:HiddenField ID="hdnDaysDiff_Oth" runat="server" />
    <asp:HiddenField ID="hdnIncidentalCharges_Oth" runat="server" />
    <asp:HiddenField ID="hdnselectionStatus_Oth" runat="server" />


    <script type="text/javascript">
        
        function textboxMultilineMaxNumber(txt, maxLen) {
            try {
                if (txt.value.length > (maxLen - 1)) return false;
            } catch (e) {
            }
        }


        function maxLengthPaste(field, maxChars) {
            event.returnValue = false;
            if ((field.value.length + window.clipboardData.getData("Text").length) > maxChars) {
                return false;
            }
            event.returnValue = true;
        }

        function Count(text) {
            var maxlength = 250;
            var object = document.getElementById(text.id)
            if (object.value.length > maxlength) {
                object.focus();
                object.value = text.value.substring(0, maxlength);
                object.scrollTop = object.scrollHeight;
                return false;
            }
            return true;
        }

        function noanyCharecters(e) {
            var keynum;
            var keychar;
            var numcheck = /[]/;
           

            if (window.event) {
                keynum = e.keyCode;
            }
            else if (e.which) {
                keynum = e.which;
            }
            var unicode = e.keyCode ? e.keyCode : e.charCode         
            if (unicode == 8 || unicode == 46) {
                keychar = unicode;
            }
            return numcheck.test(keychar);
        }
        function onCharOnlyNumber(e) {
            var keynum;
            var keychar;
            var numcheck = /[0123456789.]/;

            if (window.event) {
                keynum = e.keyCode;
            }
            else if (e.which) {
                keynum = e.which;
            }
            keychar = String.fromCharCode(keynum);
            return numcheck.test(keychar);
        }

        function Confirm() {
            //Testing();
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Submit ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            //alert(confirm_value.value);
            //document.forms[0].appendChild(confirm_value);
            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return;

        }

    </script>
</asp:Content>

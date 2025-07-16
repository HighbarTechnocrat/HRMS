<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="Appreciation_Letter_Details.aspx.cs" Inherits="Appreciation_Letter_Details" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<%@ Register TagName="calender" TagPrefix="ucical" Src="~/themes/creative1.0/LayoutControls/leaveCalender.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/manageleave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/ModifyLeaveRequest_css.css" type="text/css" media="all" />
    <%-- <link href="<%=ReturnUrl("css") %>calendar/calendar.css" rel="stylesheet" type="text/css" />
    <link href="sampleform_css.css" rel="stylesheet" type="text/css" />--%>
    <%--<ajaxToolkit:ToolkitScriptManager ID="srcp" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
            background: #dae1ed;
        }

        #content-container table {
            width: 125% !important;
        }

        /* Style the tab */
        .tab {
            overflow: hidden !important;
            border: 1px solid black !important;
            background-color: #C7D3D4 !important;
            position: relative !important;
            margin-bottom: 1px !important;
            width: 284% !important;
            color: #fff !important;
            overflow: hidden !important;
        }

            /* Style the buttons inside the tab */
            .tab button {
                background-color: inherit;
                float: left;
                border: none;
                outline: none;
                cursor: pointer;
                padding: 14px 16px;
                transition: 0.3s;
                font-size: 17px;
            }

                /* Change background color of buttons on hover */
                .tab button:hover {
                    background-color: #3D1956 !important;
                    color: #febf39 !important;
                }

                /* Create an active/current tablink class */
                .tab button.active {
                   background-color: #3D1956 !important;
                   color: #febf39 !important;
                }

        /* Style the tab content */
        .tabcontent {
            display: none;
            padding: 6px 12px;
            border: 1px solid black;
            border-top: none;
            width:278% !important;
        }
        .tablinks{
          background-color:#C7D3D4 !important;
        }

          .backbtn
          {
            background: #3D1956;
            color: #febf39 !important;
            padding: 9px 7px; 
          }

    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
     <script src="../js/HtmlControl/jHtmlArea-0.8.min.js"></script>
     <link href="../js/HtmlControl/jHtmlArea.css" rel="stylesheet" />
    
    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">

                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="View Send Appreciation Letter"></asp:Label>
                    </span>
                </div>
 
               

                    <span>
                    <a href="Appreciation_Letter_index.aspx" class="aaaa">Appreciation Index</a>
                </span>
                

                
                <div class="edit-contact">
                  
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div> 
                    </div>

                    <div class="editprofile" id="editform1" runat="server" visible="true">
                            <div class="editprofileform"> 
                            </div>
                    </div>

                    <ul id="editform" runat="server" visible="false">

                        <li>
                            <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red;
                                    font-size: 14px;
                                    font-weight: 400;
                                    text-align: center;"></asp:Label> 

                        </li>


                        <li class="date" style="display:none" >
                            <span>srno</span>                
                         <asp:TextBox ID="srno" visible="false" AutoComplete="off" runat="server"></asp:TextBox> 

                        </li>
                        <li></li>

                        <li class="date">
                           
                            <span>Employee Code</span>                
                            <asp:TextBox ID="txt_EmpCode" Enabled="false" AutoComplete="off" runat="server"></asp:TextBox>
                        </li>
                        <li class="leavedays">                            
                            <span>Employee Name</span>                
                            <asp:TextBox ID="txtEmp_Name" Enabled="false" AutoComplete="off" runat="server"></asp:TextBox>
                        </li>
  
                        <li>                             
                            <span>Designation</span>                
                            <asp:TextBox ID="txtEmp_Desigantion" Enabled="false" AutoComplete="off" runat="server"></asp:TextBox>
                        </li>

                        <li class="leavedays">                            
                            <span>Department</span>                
                            <asp:TextBox ID="txtEmp_Department" Enabled="false" AutoComplete="off" runat="server"></asp:TextBox>
                        </li>

                         <li>                             
                         <span>Send Date</span>                
                         <asp:TextBox ID="txtsend_date" Enabled="false" AutoComplete="off" runat="server"></asp:TextBox>
                     </li>

                     <li class="leavedays">                            
                         <span>Appreciation Letter</span>                
                         <asp:TextBox ID="txtletter" Enabled="false" AutoComplete="off" runat="server" ></asp:TextBox>
                     </li>
                        
                           <li  > 
                            <br />
                            <div > 
                            <%--<asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>--%>
                            <span id="txtdraft" runat="server" class="LableName">Letter Description</span>&nbsp;&nbsp;<span style="color:red">*</span><br /> 
                              <div id="HtmlFiter">                        
                            <textarea runat="server" id="ddl_letter"  cols="300" rows="30" ></textarea>
                          
                            </div>        
       </div>
<br />          </li>

                          <li class="leavedays"> 
  
                        <%--  <span>Appreciation Letter</span>  --%> 
                        <asp:Image ID="imgCard" runat="server"  style="width:500px; height:500px;" />
                      </li>  
                             <li> 
                              
                             <span>
                             <a href="View_Appreciation_Letter.aspx?itype=0"  class="backbtn">Back</a>
                            </span>               
                         
                            </li>   
                                                    
                        <asp:TextBox ID="txtEmailAddress" Enabled="false" Visible="false" AutoComplete="off" runat="server"></asp:TextBox>
                                                                   
                        <li></li>

                        <li></li>
                                                                                                  
                        <li>

                            <br />
                            <br />
                            <br />
                        </li>

                    </ul>

                </div>
                <asp:HiddenField ID="hdnReqid" runat="server" /> 
                <asp:HiddenField ID="hdnYesNo" runat="server" /> 
                 <asp:HiddenField ID="hdnEmployeePhoto" runat="server" />

            </div>
        </div>
    </div>

    <script type="text/javascript">

        $(document).ready(function () {

            $("textarea").htmlarea();
            $(".DropdownListSearch").select2();

        });

        function openCity(evt, cityName) {
            
            var i, tabcontent, tablinks;
            tabcontent = document.getElementsByClassName("tabcontent");
            for (i = 0; i < tabcontent.length; i++) {
                tabcontent[i].style.display = "none";
            }
            tablinks = document.getElementsByClassName("tablinks");
            for (i = 0; i < tablinks.length; i++) {
                tablinks[i].className = tablinks[i].className.replace(" active", "");
            }
            document.getElementById(cityName).style.display = "block";
            evt.currentTarget.className += " active";
        }
    </script>

    <script type="text/javascript">
        function textboxMultilineMaxNumber(txt, maxLen) {
            try {
                if (txt.value.length > (maxLen - 1)) return false;
            } catch (e) {
            }
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
       
        function ConfirmIn() {
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

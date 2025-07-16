<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"  ValidateRequest="false"
    CodeFile="Appreciation_toemployee_Details.aspx.cs" Inherits="Appreciation_toemployee_Details" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

 
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
     <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/manageleave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/ModifyLeaveRequest_css.css" type="text/css" media="all" />
    
    <style>

        .BtnShow {
        color: red !important;
        background-color: transparent;
        text-decoration: none;
        font-size: 13px !important;
    }

         

        .aspNetDisabled {
            background: #dae1ed;
        }
          

       .backbtn
{
  background: #3D1956;
  color: #febf39 !important;
  padding: 9px 7px; 
}           

       #MainContent_lstApprover, #MainContent_lstIntermediate {
    padding: 0 0 33px 0 !important;
    
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
                        <asp:Label ID="lblheading" runat="server" Text="Received Appreciation Letter"></asp:Label>
                    </span>
                </div>
 
                
                <span>
                <a href="Appreciation_Letter_index.aspx" class="aaaa">Appreciation Index</a>
            </span>
                

                
                <div class="edit-contact">
                   
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

                        
                        <li class="leavedays">                            
                            <span>Employee Name</span>                
                            <asp:TextBox ID="txtEmp_Name" Enabled="false" AutoComplete="off" runat="server"></asp:TextBox>
                        </li>
  
                        <li>                             
                            <span>Appreciation Letter</span>                
                            <asp:TextBox ID="txt_letter" Enabled="false" AutoComplete="off" runat="server"></asp:TextBox>
                        </li>

                        <li class="trvl_type">
                        <span>Received Date</span>
                        <asp:TextBox AutoComplete="off" ID="txtReceiveddate" runat="server" AutoPostBack="true"  MaxLength="15" Enabled="false" AutoCompleteType="Disabled"></asp:TextBox>
                         </li>
                       
                         <li class="leavedays">                            
                         <span>Received From </span>                
                         <asp:TextBox ID="txtreceviedfrom" Enabled="false" AutoComplete="off" runat="server"></asp:TextBox>
                         </li>

                         <li class="leavedays">                            
                         <span>Appreciation Point</span>                
                         <asp:TextBox ID="txtpoint" Enabled="false" AutoComplete="off" runat="server"></asp:TextBox>
                         </li>
                        
                         <li>
                             <span>Download Appreciation Letter</span> <br />
                              <asp:LinkButton ID="lnkviewletter" runat="server" CssClass="BtnShow" OnClientClick="DownloadFile()"></asp:LinkButton>

                              <span style="display:none">Appreciation Letter</span>   
                             <br />
                           <asp:Image ID="imgCard" runat="server" style="width:500px; display:none; height:500px;" />
                         </li> 

                            <li  > 
                            <br /> 
                            <span id="txt_draft" runat="server" class="LableName">Letter Description</span>&nbsp;&nbsp;<span style="color:red">*</span><br /> 
                              <div id="HtmlFiter">                        
                            <textarea runat="server" id="ddl_letter" cols="300" rows="30" ></textarea> 
                            </div>     
                            <br />   </li>


                        <li> </li>  
                         
                             <li> 
                                
                                  <span>
                                 <a href="Appreciation_to_employee.aspx?itype=0"  class="backbtn">Back</a>
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

            </div>
        </div>
    </div>

      <asp:HiddenField ID="hdnReqid" runat="server" />  
  <asp:HiddenField ID="hdnYesNo" runat="server" />
  <asp:HiddenField ID="hdnEmpEmail" runat="server" /> 
   <asp:HiddenField ID="hdnempid" runat="server" />
   <asp:HiddenField ID="hdnEmployeePhoto" runat="server" />
  <asp:HiddenField ID="hdfilefath" runat="server" />
   <asp:HiddenField ID="hdfilename" runat="server" />

    <script type="text/javascript">

        $(document).ready(function () {

            $("textarea").htmlarea();
            $(".DropdownListSearch").select2();
            $("#MainContent_txtPOWO_Content").htmlarea();
            $("#MainContent_txt_POWOContent_description").htmlarea();

        });

        function DownloadFile() {
            var localFilePath = document.getElementById("<%=hdfilefath.ClientID%>").value;
            var localFileName = document.getElementById("<%=hdfilename.ClientID%>").value;

           //  window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + localFileName); 
            window.open("http://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + localFileName);
       }

       
    </script>
 
</asp:Content>

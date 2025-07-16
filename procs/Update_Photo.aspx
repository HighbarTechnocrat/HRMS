<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="Update_Photo.aspx.cs" Inherits="Update_Photo" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

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
        .select#MainContent_gvMngLeaveRqstList_ddlAproveReject_0 {
    padding-left: 0px;
}
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
    
    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">

                <div class="userposts">
                    <br />
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Upload Passport-Size Photo"></asp:Label>
                    </span>
                </div>

                <span>
                     <a href="Update_Photo_index.aspx" class="aaaa" >Index</a>
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
                            <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
                            <%--<asp:TextBox runat="server" ID="lblmessage1" Visible="False" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;" OnTextChanged="lblmessage1_TextChanged"></asp:TextBox>--%>
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


                        <li class="trvl_type" id="photo" runat="server" visible="false"> 
                            <span>My Photo</span>   <br />      
                          <asp:Image ID="imgCard" runat="server" style="width:270px; height:270px;" />
                        </li> 
                        <li class="trvl_type" id="photo_1" runat="server" visible="false"> 

                         </li>


                        <li class="trvl_type" id="liRemarks" runat="server" visible="false"> 
                            <br />
                        <span>Remarks</span>                      
                        <br />
                        <asp:TextBox AutoComplete="off" ID="txtRemark" runat="server" MaxLength="400" TextMode="MultiLine" Enabled="false" Height="80px" ></asp:TextBox>

                    </li>
                     <li class="trvl_type" id="liRemarks_1" runat="server" visible="false"> </li>


                           <li class="trvl_type" id="file" runat="server" >
                               <br />
                         <span id="spnsportingfiles" runat="server">Upload Photo</span>&nbsp;&nbsp;<span id="spnsportingfiles_1" runat="server" style="color: red">*</span>
                         <asp:FileUpload ID="uplodmultiple" runat="server" accept=".jpg" AllowMultiple="true"  ></asp:FileUpload>
                          
                     </li>                             
                        <li></li>

                        <li>
                             <br /><br />
                            <asp:LinkButton ID="btnIn" runat="server" CssClass="Savebtnsve" Visible="true" Text="Submit" OnClick="btnIn_Click" OnClientClick="return SaveMultiClick();">Submit</asp:LinkButton>
                             <asp:LinkButton ID="btnback_mng" runat="server" Visible="false" CssClass="Savebtnsve" Text="Save" OnClick="btnIn_Click" OnClientClick="return SaveInClick();">Correction</asp:LinkButton>
                          <br /><br /><br /><br />      </li>
                       
                        
                    </ul>
                </div>
                <asp:HiddenField ID="hdnTravel_id" runat="server" /> 
                 <asp:HiddenField ID="hdncurrid" runat="server" />     
                <asp:HiddenField ID="FilePath" runat="server" /> 
                <asp:HiddenField ID="hdnYesNo" runat="server" />
            </div>
        </div>
    </div>


  <script type="text/javascript">

      $(document).ready(function () {
          $(".DropdownListSearch").select2();
          $("#MainContent_txtPOWO_Content").htmlarea();
          $("#MainContent_txt_POWOContent_description").htmlarea();
      });


    
      function Confirm() {

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

      function DownloadFile(file) {
          // alert(file);
          var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;

            //alert(localFilePath);
            // window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);
            window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + file);

        }


      function onCharOnlyNumber(e) {
          var keynum;
          var keychar;
          var numcheck = /[0123456789]/;

          if (window.event) {
              keynum = e.keyCode;
          }
          else if (e.which) {
              keynum = e.which;
          }
          keychar = String.fromCharCode(keynum);
          return numcheck.test(keychar);
      }


  </script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"  validateRequest="false"
    CodeFile="Appreciation_oldletter_details.aspx.cs" Inherits="Appreciation_oldletter_details" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" /> 
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

        #MainContent_lstApprover {
            overflow: hidden !important;
        }
        

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
      <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/HtmlControl/jquery-1.3.2.js"></script>
    <script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="../js/HtmlControl/jHtmlArea-0.8.min.js"></script>
    <link href="../js/HtmlControl/jHtmlArea.css" rel="stylesheet" />
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
    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Appreciation Letter Draft"></asp:Label>
                    </span>
                </div>
                 <div>
                        <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                    </div>
             <div>
                <span>
                      <a href="Appreciation_Letter_index.aspx" class="aaaa">Appreciation Index</a> 
                </span>
                 </div>  
                <div id="divtext" runat="server"></div>
                <div class="edit-contact">
      
                    <ul id="editform" runat="server" >

                          <li class="date" style="display:none" >
                          <span>Appreciation_id</span>                
                          <asp:TextBox ID="Appreciation_id" visible="false" AutoComplete="off" runat="server"></asp:TextBox> 

                         </li>
                         

                        <li class="mobile_inboxEmpCode">                            
                            <span >Appreciation Letter Type</span>&nbsp;&nbsp;<span style="color:red">*</span>
                            <br />
                            <asp:TextBox ID="txt_lettertype" Enabled="false" AutoComplete="off" runat="server"></asp:TextBox> </li>

                        <li class="leavedays">
                            <div id="liSubject" runat="server" > 
                        <span id="txt_lettere_sub" runat="server">Appreciation Letter</span>&nbsp;&nbsp;<span style="color:red">*</span>
                        <asp:TextBox ID="txt_app_letter" Enabled="false" AutoComplete="off" runat="server"></asp:TextBox>
                        <br />
                                </div>
                    </li> 

                        <li class="leavedays">
                                <div id="Point" runat="server" > 
                            <span id="letter_point" runat="server">Appreciation Letter Point</span>&nbsp;&nbsp;<span style="color:red">*</span>
                            <asp:TextBox ID="txt_point" AutoComplete="off" runat="server" onkeypress="return isNumberKey(event);"></asp:TextBox>
                            <br />
                                    </div>
                        </li> 
                        <li>
                                <div id="txt_subject" runat="server" > 
                    <span id="Span1" runat="server">Appreciation Letter Subject</span>&nbsp;&nbsp;<span style="color:red">*</span>
                    <asp:TextBox ID="txt_sub" AutoComplete="off" runat="server"></asp:TextBox>
                    <br />
                            </div>
                        </li>
                         <li class="mobile_InboxEmpName">
                        <asp:RadioButton runat="server" ID="rbtnActive" GroupName="status" Text="Active" Checked="true" />
                        <asp:RadioButton runat="server" ID="rbtnDeactive" GroupName="status" Text="Deactive" />
                    </li>

                       <li></li>

                        <li> 
                    <div >
                        <span>Appreciation Draft</span>            
                        <asp:TextBox AutoComplete="off" ID="txtDescription" runat="server" 
                            Style="height:400px;width: 730px" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </li>

                     
                        <li></li>
                         
                    </ul>
                </div>
            </div>
        </div>
       
    </div>
    <div class="mobile_Savebtndiv">
        <asp:LinkButton ID="mobile_btnSave" runat="server" Text="Save" ToolTip="Save" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click"  OnClientClick="return SaveMultiClick();">Save</asp:LinkButton>    
      <asp:LinkButton ID="mobile_btnBack" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="mobile_btnBack_Click" />


    </div>
    
    <br />
    <br />
         
   
    <br />
    
    <asp:HiddenField ID="hdnReqid" runat="server" />
       
    <asp:HiddenField ID="hflEmpDesignation" runat="server" />
    <asp:HiddenField ID="hflEmpDepartment" runat="server" />
    <asp:HiddenField ID="hflEmailAddress" runat="server" />
    <asp:HiddenField ID="hdnBankDetail_ID" runat="server" />
    <asp:HiddenField ID="hdnGrade" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="hdnFilter" runat="server" />
    
	 <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />
    <script type="text/javascript">      
        $(document).ready(function () {
            			
			$("textarea").htmlarea(); 
            $(".DropdownListSearch").select2();
    
		});

        function isNumberKey(evt) {
            var charCode = evt.which ? evt.which : evt.keyCode;
            // Allow only numbers (0–9)
            return charCode >= 48 && charCode <= 57;
        } 

        function validateDescription() {
            var desc = document.getElementById('<%= txtDescription.ClientID %>').value.trim();
            if (desc === '') {
                alert('Description is required!');
                return false;
            }
            return true;
        }

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
            keychar = String.fromCharCode(keynum);
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

        function SaveMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=mobile_btnSave.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        Confirm();
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
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


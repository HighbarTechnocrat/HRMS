<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddIdeaCentral.aspx.cs" Inherits="IdeaCentral" %>--%>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/cms.master" AutoEventWireup="true" CodeFile="AddIdeaCentral.aspx.cs" Inherits="AddIdeaCentral" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <link href="<%=ReturnUrl("css") %>profile/mprofile.css" rel="stylesheet" type="text/css" />
    <div class="mainpostwallcat">
        <div class="comments-summery1">
            <div class="userposts"><span>Add Idea Central</span> <br>
			</div>     
		</div>
        <asp:Panel ID="pnlIdea" runat="server">
		
            <div class="full-page-description-common">
            <div class="full-page-title-decription-right">
                <ul class="full-page-right-panel-content">
                   
                    <li>
                         <div class="title-all-post"><span class="addpostmadtory">*</span> Enter Title
						 </div>
						 <br>
                        <div id="titlediv">
                            <div id="titlewrap">
                                <asp:TextBox ID="txttitle" runat="server" TextMode="MultiLine" CssClass="posttitle" MaxLength="100"></asp:TextBox>
                                <span class="posttexticon tooltip" style="display:none" title="Enter Title.Max length 100"><i class="fa fa-info-circle"></i></span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="formerror" ControlToValidate="txttitle" ErrorMessage="Please enter title." Display="Dynamic" SetFocusOnError="True" ValidationGroup="validate"></asp:RequiredFieldValidator>
                            </div>
                            <input type="hidden" id="samplepermalinknonce" name="samplepermalinknonce" value="a144318822">
                        </div>
                    </li>
                    <li>
                         <div class="title-all-post">Enter Description.</div>
						 <div class="full-page-container-description">
                            <div class="ckeditor">
                                 <asp:TextBox ID="txtdesc" runat="server" TextMode="MultiLine" CssClass="posttitle"></asp:TextBox>
								 <span class="posttexticon tooltip"  style="display:none" title="Enter Description"><i class="fa fa-info-circle"></i></span>
                            </div>
                        </div>
                    </li>   
              </div>
                <ul>
                    <li>
                          <div class="submitbtndiv">						
						 <asp:LinkButton ID="btnsubmit" runat="server" Text="Save Post" OnClick="btnsubmit_Click"  Enabled="true" Visible="true"></asp:LinkButton>                           
                        </div>
                    </li>
                </ul>
					
            </div>
			</div>
        </asp:Panel>
            <div class="projectlblmsg">
				<asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>               
            </div>
        
    </div>
	
	
	
	
<script lang="javascript" type="text/javascript">
    $(document).ready(function () {
        $(".tabs ul li a").first().attr("href", "product.aspx?cid=" + $("#ContentPlaceHolder5_addpost_hdproductid").val());
    });
</script>



<script type="text/javascript">
    function Check(textBox, maxLength) {
        if (textBox.value.length > maxLength) {
            alert("Max characters allowed are " + maxLength);
            textBox.value = textBox.value.substr(0, maxLength);
        }
    }

</script>

</asp:Content>





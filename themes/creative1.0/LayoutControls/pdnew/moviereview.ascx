<%@ Control Language="C#" AutoEventWireup="true" CodeFile="moviereview.ascx.cs" Inherits="themes_creative1_LayoutControls_pd_moviereview" %>
<link href="<%=ReturnUrl("css") %>review/review.css" rel="stylesheet" type="text/css"/>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>jqwidgets/jqx.base.css" type="text/css" />
    <script type="text/javascript" src="ReturnUrl("sitepath")js/jsframework.js"></script>
    <script type="text/javascript" src="ReturnUrl("sitepath")js/jqwidgets/jqxcore.js"></script>
    <script type="text/javascript" src="ReturnUrl("sitepath")js/jqwidgets/jqxrating.js"></script>
    <script type="text/javascript" src="ReturnUrl("sitepath")js/MaxLength.min.js"></script>
<script type="text/javascript">

    $(document).ready(function () {
        $("#jqxRating").jqxRating({ width: 200, height: 35, value: document.getElementById("MainContent_pdglobal_m_uxPdLayout_ctl04_hdvalue2").value });
        $("#jqxRating").bind('change', function (event) {
            document.getElementById("MainContent_pdglobal_m_uxPdLayout_ctl04_hdvalue2").value = event.value;
        });
    });

    </script>
<script type="text/javascript">

	counter = function() {
    var value = $('#MainContent_pdglobal_m_uxPdLayout_ctl04_txtreview').val();

    if (value.length == 0) {
        $('#MainContent_pdglobal_m_uxPdLayout_ctl04_lblcount').html('400 characters remaining');
        return;
    }
    
    if(value.length>400)
    {
        $('#MainContent_pdglobal_m_uxPdLayout_ctl04_lblcount').html('0 characters remaining');
        return;	
    }	

    var regex = /\s+/gi;
    var wordCount = value.trim().replace(regex, ' ').split(' ').length;
    var totalChars = value.length;
    var charCount = value.trim().length;
    var charCountNoSpace = value.replace(regex, '').length;
    if (totalChars>0) 
    {
	if(totalChars<400)
	{
		$('#MainContent_pdglobal_m_uxPdLayout_ctl04_lblcount').html(400-totalChars+' characters remaining');
        	return;
        }
    }
    if(totalChars<0)
    {
	$('#MainContent_pdglobal_m_uxPdLayout_ctl04_lblcount').html('0 characters remaining');
    }
    else
    {	
    	$('#MainContent_pdglobal_m_uxPdLayout_ctl04_lblcount').html(400-totalChars+' characters remaining');
    }
};

$(document).ready(function() {
    $('#MainContent_pdglobal_m_uxPdLayout_ctl04_txtreview').change(counter);
    $('#MainContent_pdglobal_m_uxPdLayout_ctl04_txtreview').keydown(counter);
    $('#MainContent_pdglobal_m_uxPdLayout_ctl04_txtreview').keypress(counter);
    $('#MainContent_pdglobal_m_uxPdLayout_ctl04_txtreview').keyup(counter);
    $('#MainContent_pdglobal_m_uxPdLayout_ctl04_txtreview').blur(counter);
    $('#MainContent_pdglobal_m_uxPdLayout_ctl04_txtreview').focus(counter);
     $("[id*=MainContent_pdglobal_m_uxPdLayout_ctl04_txtreview]").MaxLength({ MaxLength: 400,DisplayCharacterCount: false });
    var value2 = $('#MainContent_pdglobal_m_uxPdLayout_ctl04_txtreview').val();

    $('#MainContent_pdglobal_m_uxPdLayout_ctl04_lblcount').html((400-value2.length)+' characters remaining');
});
    </script>
<link href="<%=ReturnUrl("css") %>popupnew/popup.css" rel="stylesheet" type="text/css"/>
<asp:HiddenField ID="hdvalue" runat="server" Value="0" ViewStateMode="Inherit" />
<asp:HiddenField ID="hdvalue2" runat="server" Value="0" ViewStateMode="Inherit" />
<asp:LinkButton ID="lnkrev" runat="server" OnClick="lnkrev_onclick" Text="Review" Visible="false"></asp:LinkButton>

<div id="divstar" runat="server" class="star">
    <div class="content">
      <div class="rating-movie-name"><asp:Label ID="lblprdname" runat="server"></asp:Label></div>
      <div>
    Rate this Movie:<br/>
    <div id='jqxRating' Style='margin-left:100px;'>
    </div>
      </div>
        <div>
            Review :
            <asp:TextBox ID="txtreview" runat="server" TextMode="MultiLine" Width="200px"></asp:TextBox>
	<br/>
	<asp:Label ID="lblcount" runat="server" ForeColor="#707070" ></asp:Label>

        </div>
      <br />
      <div>
         
          <asp:Button ID="btnsubmit" runat="server" Text="Submit" ToolTip="Submit"
              onclick="btnsubmit_Click" />
      </div>
    </div>
  </div>


<asp:Repeater ID="rptmoviereview" runat="server" OnItemDataBound="rptmoviereview_ItemDataBound" onitemcommand="rptmoviereview_ItemCommand" >
   
      <ItemTemplate>
           <div id="divstar" runat="server" class="star">
        <div style="border-radius:5px;">
           <a>
               <asp:Image ID="imgprofile" runat="Server" width="30px" height="30px"/>
                </a>&nbsp;&nbsp;<asp:LinkButton ID="lnkusername" runat="server" CommandName="username" ForeColor="Black" OnClientClick="window.document.forms[0].target='_blank';" CommandArgument='<%# Eval("username") %>'><%# Eval("fullname")%></asp:LinkButton> <br />
            <asp:Label ID="lblratval" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ratingvalue") %>' Visible="false"></asp:Label><br /><div style="text-align:center;">
            <asp:Literal ID="ltrjs" runat="server"></asp:Literal></div>
            <asp:Label ID="lbldate" runat="server" Text='<%#Eval("reviewdate","{0:dd-MMM-yyyy}") %>'></asp:Label><br />
            <asp:Label ID="lblrevid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.reviewid") %>' Visible="false">
             </asp:Label>
             <asp:Label ID="lbluseremail" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.username") %>' Visible="false">
             </asp:Label>
          
            <asp:LinkButton ID="lnklike" runat="server" OnClick="lnklike_click" ForeColor="Black"></asp:LinkButton>
            <asp:Panel ID="pnlike" runat="server">
            (<asp:Label ID="lblcnt" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.likecount") %>'></asp:Label>)
             </asp:Panel>

            <asp:LinkButton ID="lnkcomm" runat="server" Text="Comment" ForeColor="Black" OnClick="lnkcomm_onclick"></asp:LinkButton>
            <asp:Panel ID="pncomment" runat="server">
                <asp:Label ID="lblcommentcount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.commentcount") %>'></asp:Label>
            </asp:Panel>                
                <br />
            <div style="word-wrap:break-word;">
            <asp:Label ID="lblreviews" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.reviewtext") %>'></asp:Label>
                </div><br />
<asp:LinkButton ID="lnkfollow" CommandName="follow" ForeColor="#000" runat="server" CommandArgument='<%# Eval("username") %>' Text="Follow"></asp:LinkButton>
        </div>
                   </div>
        </ItemTemplate>
      </asp:Repeater>






<div class="news_update">
  <div class="news">
    <div class="float_left" style="padding:2px 0 0 0;"><img src="<?=$innerpath?>images/news_update.jpg" alt="News Update" title="News Update" /></div>
    <script type="text/javascript">
		new pausescroller(pausecontent2, "pscroller2", "someclass", 2000)
	</script>
  </div>
</div>

<div style="clear:both;"></div>
<div id="footer">
  <div class="footer">
    <ul>
    <span style="display:none">  <li style="padding:0 10px 0 0; background:none;"><a href="http://www.hccindia.com/" target="_blank">HCC Group</a> </li>
 </span>    <li><a href="<?=$innerpath?>careers/jobs.php">Careers</a></li>
      <li><a href="<?=$innerpath?>knowledge_centre/case_studies.php">Knowledge Centre</a></li>
      <li><a href="<?=$innerpath?>customer_lounge/business_challenges.php">Customer Lounge</a></li>
      <li><a href="<?=$innerpath?>contactus/our_office.php">Contact Us</a></li>
    </ul>

    <div style="clear:both;"></div>
    <div style="clear:both;"></div>
    <div style="clear:both;height:16px"></div>
    <div class="copyright">Copyright &copy; <?php echo date('Y')?> Highbar Technocrat Ltd. All Rights Reserved.<br /><br /></div>
  </div>
</div>







<div id="tell_a_friend" style="border:1px solid; display:none; position: fixed; z-index:2000; top:150px; left:400px;  background-image:url(<?=$innerpath?>images/tellafriend_bg.jpg); background-repeat:no-repeat; width:450px; height:333px; ">
    <form name="frm2" id="frm2" action="" method="post" enctype="multipart/form-data">   
	      
		  <?php
			if($_SERVER['QUERY_STRING'])
			    $actionpage='http://'.$_SERVER['HTTP_HOST'].$_SERVER['PHP_SELF']."?".$_SERVER['QUERY_STRING'];
			else
			    $actionpage='http://'.$_SERVER['HTTP_HOST'].$_SERVER['PHP_SELF'];
				
				//echo $actionpage;
			
		    ?>
		  
		 <table width="435" border="0" cellpadding="0" cellspacing="0" class="grey_txt">         
	   	        <tr><td align="right" colspan="4" ><input type="hidden" id="pageurl" name="pageurl" value="<?=$actionpage;?>" /> <img src="<?=$innerpath?>images/tellclose.gif" alt="Close" title="Close" style="cursor:pointer; padding-top:5px; marging-left:-5px;	" onclick="javascriipt:document.getElementById('tell_a_friend').style.display='none'; document.frm2.reset(); reset_tellafriend();"  /></td>
	 	        </tr>
		        <tr><td class="width4">&nbsp;</td>
		            <td colspan="3" class="padd_lft10 height33" valign="top"  style="padding-left:25px; padding-top:5px;"><img src="<?=$innerpath?>images/tellafriend_txt.gif" alt="Tell a Friend" title="Tell a Friend" /></td>
		       </tr>
		       <tr><td colspan="4" class="height12">
               <table width="100%" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td width="35">&nbsp;</td>
    <td align="left" valign="top"><table width="400" border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td align="left" valign="middle" class="tell_text">&nbsp;</td>
        <td align="left" valign="middle">&nbsp;</td>
      </tr>
      <tr>
        <td width="69" align="left" valign="middle" class="tell_text">From * </td>
        <td align="left" valign="middle"><input type="text"  name="your_name" id="your_name" value="Please Fill the Name" onclick="if(this.value=='Please Fill the Name')this.value=''" onfocus="if(this.value=='Please Fill the Name')this.value=''"   class="tell_input" /></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td height="10" colspan="2"></td>
    </tr>
  <tr>
    <td>&nbsp;</td>
    <td align="left" valign="top"><table width="400" border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td width="69" align="left" valign="middle" class="tell_text">E-mail * </td>
        <td align="left" valign="middle"><span class="txtboxcenter">
          <!--<input type="text"  class="tell_input" name="your_email" id="your_email" value="Your E-mail" onclick="if(this.value=='Your E-mail')this.value=''" onfocus="if(this.value=='Your E-mail')this.value=''" />-->
		  
		  <input type="text"  name="your_email" id="your_email" value="Your E-mail" onclick="if(this.value=='Your E-mail')this.value=''" onfocus="if(this.value=='Your E-mail')this.value=''"   class="tell_input" />
		  
        </span></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td height="10" colspan="2"></td>
    </tr>
  <tr>
    <td>&nbsp;</td>
    <td align="left" valign="top"><table width="400" border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td width="69" align="left" valign="middle" class="tell_text">To </td>
        <td align="left" valign="middle"><span class="txtboxcenter">
          <input type="text"  class="tell_input" name="friends_name" id="friends_name" value="Your Friends Name" onclick="if(this.value=='Your Friends Name')this.value=''" onfocus="if(this.value=='Your Friends Name')this.value=''"/>
        </span></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td height="10" colspan="2"></td>
    </tr>
  <tr>
    <td>&nbsp;</td>
    <td align="left" valign="top"><table width="400" border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td width="69" align="left" valign="middle" class="tell_text">E-mail * </td>
        <td align="left" valign="middle"><span class="txtboxcenter">
         <!-- <input type="text"  class="tell_input" name="friends_email" id="friends_email" value="Your Friend's E-mail" onclick="if(this.value=='Please Fill Friend's Email')this.value=''" onfocus="if(this.value=='Please Fill Friend's Email')this.value=''"/>-->
		 
		 <input type="text"  name="friends_email" id="friends_email" value="Your Friends E-mail" onclick="if(this.value=='Your Friends E-mail')this.value=''" onfocus="if(this.value=='Your Friends E-mail')this.value=''"   class="tell_input" />
		 
        </span></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td height="10" colspan="2"></td>
    </tr>
  <tr>
    <td>&nbsp;</td>
    <td align="left" valign="top"><table width="400" border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td width="69" align="left" valign="middle" class="tell_text">Message </td>
        <td align="left" valign="middle"><span class="textarea011">
          <textarea  class="tell_textarea" cols='20' rows='2' name='message' id="message"  onclick="if(this.value=='Enter your message')this.value=''" onfocus="if(this.value=='Enter your message')this.value=''">Enter your message</textarea>
        </span></td>
      </tr>
      <tr>
        <td align="left" valign="middle" class="tell_text">&nbsp;</td>
        <td align="left" valign="middle">&nbsp;</td>
      </tr>
      <tr>
        <td align="left" valign="middle" class="tell_text"><a href="#"></a></td>  
        <td align="left" valign="middle"><a href="javascript:SendTellAfriend()"><img src="<?=$innerpath?>images/send_butt_taf.gif" alt="Send" title="Send"  border="0" /></a></td>
      </tr>
    </table></td>
  </tr>
</table>

               
               
               
               
               
               
               </td></tr>
		   	   
                  <tr><td colspan="4" class="height6"></td></tr>
      </table>    
  </form>				
    </div>
<!--Tell A Friend div end Here-->

<script type="text/javascript">
var prev_tellafriend = document.getElementById("tell_a_friend").innerHTML;
function reset_tellafriend() {
	document.getElementById("tell_a_friend").innerHTML = prev_tellafriend;	
}
</script>



<?
	if($pagename == "index.php" || $pagename == "" || $pagename == "podcasts.php"){}
	else
	{
?>

<script type="text/javascript">
$(document).ready(function() {
   
    $('a[href=#top]').click(function(){
        $('html, body').animate({scrollTop:0}, 'slow');
        return false;
    });

});

</script>
<?}?>
<script type="text/javascript">

function SendTellAfriend(){
	var urlname = document.frm2.pageurl.value;
	var urldata = "";
	
	var objcname = document.getElementById("your_name");
	if(objcname.value=="" || objcname.value=="Please Fill the Name" || isEmpty(objcname)){
		objcname.value="Please Fill the Name";
		return ;
	}
	
	var objemail = document.getElementById("your_email");
	var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
    if(objemail.value=="" || objemail.value=="Your E-mail" || isEmpty(objemail)){
		objemail.value="Your E-mail";
		return ;
	}
	else if(objemail.value!="" && reg.test(objemail.value) == false) {
		objemail.value="Invalid E-mail";
		return;
		
	}
	

	var objfname = document.getElementById("friends_name");
	if(objfname.value=="" || objfname.value=="Your Friends Name" || isEmpty(objfname)){
		objfname.value="Your Friends Name";
		return ;
	}
	
	
	
	var objfriendsemail = document.getElementById("friends_email");
	var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
    if(objfriendsemail.value=="" || objfriendsemail.value=="Your Friends E-mail" || isEmpty(objfriendsemail)){
		objfriendsemail.value="Your Friends E-mail";
		return ;
	}
	else if(objfriendsemail.value!="" && reg.test(objfriendsemail.value) == false) {
	
		objfriendsemail.value="Invalid E-mail";
		return ;
		
	}
	
	
   var urldata = "pageurl="+document.getElementById('pageurl').value+"&name="+objcname.value+"&email="+objemail.value+"&friendsname="+document.getElementById('friends_name').value+"&frindsemail="+objfriendsemail.value+"&message="+document.getElementById('message').value
    
  
	req = createXMLHttpRequest();
	setobj = document.getElementById("tell_a_friend"); 
	sendRequest("<?=$innerpath?>sending.php?"+urldata);
	showdiv(document.getElementById("tell_a_friend"));
}  
function tellAfriend(){
	document.getElementById('tell_a_friend').style.display='block';
	document.frm2.reset();
}

function isEmpty(str){
	strRE = new RegExp( );
	strRE.compile( '^[\s ]*$', 'gi' );
	return strRE.test( str.value );
}
</script>


<!-- Start Google Analytics Code -->
<script type="text/javascript">
var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
</script>
<script type="text/javascript">
try {
var pageTracker = _gat._getTracker("UA-5750714-18");
pageTracker._trackPageview();
} catch(err) {}</script>

<!-- End Google Analytics Code-->

<!-- LeadFormix -->
<a href="http://www.leadformix.com" title="Marketing Automation" onclick="window.open(this.href);return(false);">
<script language="javascript">
var pkBaseURL = (("https:" == document.location.protocol) ? "https://vlog.leadformix.com/" : "http://vlog.leadformix.com/");
document.write(unescape("%3Cscript src='" + pkBaseURL + "bf/bf.js' type='text/javascript'%3E%3C/script%3E"));
</script>
<script type="text/javascript">
var pkBaseURL = (("https:" == document.location.protocol) ? "https://vlog.leadformix.com/" : "http://vlog.leadformix.com/");
<!--
bf_action_name = '';
bf_idsite = 6478;
bf_url = pkBaseURL+'bf/bf.php';
setTimeout('bf_log("' + bf_action_name+'",'+ bf_idsite+',"'+ bf_url +'")',0);
//-->
</script></a>
<!-- LeadFormix -->

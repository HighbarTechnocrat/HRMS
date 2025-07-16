using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Net;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Web.Configuration;
using System.Net.Mail;
using System.Globalization;
using System.Collections.Specialized;
/// <summary>
/// Class contains miscellaneous functionality 
/// </summary>

public static class Utilities
{
    static Utilities()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    // Generic method for sending emails
    public static void SendMail(string from, string to, string subject, string body)
    {
        // Configure mail client (may need additional
        // code for authenticated SMTP servers)
        SmtpClient mailClient = new SmtpClient(creativeconfiguration.MailServer);
        // Create the mail message

        System.Configuration.Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
        System.Net.Configuration.MailSettingsSectionGroup settings = (System.Net.Configuration.MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
        MailAddress SendBCC = new MailAddress(from);
        mailClient.Credentials = new NetworkCredential(settings.Smtp.Network.UserName, "support@123");
        MailMessage mailMessage = new MailMessage(from, to, subject, body);
        mailMessage.From = new MailAddress(from, "Intranet.com");
        mailMessage.Bcc.Add("support@Intranet.com");

        mailMessage.Body = body;
        mailMessage.IsBodyHtml = true;


        /*
           // For SMTP servers that require authentication
           message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", 1);
           message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "SmtpHostUserName");
           message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "SmtpHostPassword");
          */
        // Send mail
        //mailClient.Send(mailMessage);
    }
    public static void SendMailbcc(string from, string to, string bcc, string subject, string body)
    {
        // Configure mail client (may need additional
        // code for authenticated SMTP servers)
        SmtpClient mailClient = new SmtpClient(creativeconfiguration.MailServer);
        // Create the mail message
        System.Configuration.Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
        System.Net.Configuration.MailSettingsSectionGroup settings = (System.Net.Configuration.MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");

        mailClient.Credentials = new NetworkCredential(settings.Smtp.Network.UserName, settings.Smtp.Network.Password);
        MailMessage mailMessage = new MailMessage(from, to, subject, body);
        mailMessage.From = new MailAddress(from, "Intranet.com");

        mailMessage.Body = body;
        mailMessage.IsBodyHtml = true;

       //MailAddress SendBCC = new MailAddress(bcc);
       // mailMessage.Bcc.Add(bcc);
        /*
           // For SMTP servers that require authentication
           message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", 1);
           message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "SmtpHostUserName");
           message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "SmtpHostPassword");
          */
        // Send mail
        //mailClient.Send(mailMessage);
    }
    public static void SendMailCC(string from, string to, string subject, string body)
    {
        // Configure mail client (may need additional
        // code for authenticated SMTP servers)
        SmtpClient mailClient = new SmtpClient(creativeconfiguration.MailServer);
        // Create the mail message
        System.Configuration.Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
        System.Net.Configuration.MailSettingsSectionGroup settings = (System.Net.Configuration.MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");

        mailClient.Credentials = new NetworkCredential(settings.Smtp.Network.UserName, settings.Smtp.Network.Password);
        MailAddress SendCC = new MailAddress(from);
        MailMessage mailMessage = new MailMessage(from, to, subject, body);
        mailMessage.From = new MailAddress(from, "Intranet.com");

        mailMessage.Body = body;
        mailMessage.IsBodyHtml = true;
        mailMessage.CC.Add(SendCC);

        /*
           // For SMTP servers that require authentication
           message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", 1);
           message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "SmtpHostUserName");
           message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "SmtpHostPassword");
          */
        // Send mail
        //mailClient.Send(mailMessage);
    }

    public static void SendMailforInviteUsres(string from, string to, string bcc, string subject, string body)
    {
        // Configure mail client (may need additional
        // code for authenticated SMTP servers)
        SmtpClient mailClient = new SmtpClient(creativeconfiguration.MailServer);
        // Create the mail message
        System.Configuration.Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
        System.Net.Configuration.MailSettingsSectionGroup settings = (System.Net.Configuration.MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");

        mailClient.Credentials = new NetworkCredential(settings.Smtp.Network.UserName, settings.Smtp.Network.Password);

        MailAddress SendCC = new MailAddress(from);
        MailAddress SendBcc = new MailAddress(bcc);

        to = to + ";" + bcc;

        MailMessage mailMessage = new MailMessage(from, to, subject, body);
        mailMessage.From = new MailAddress(from, "Intranet.com");

        mailMessage.Body = body;
        mailMessage.IsBodyHtml = true;
        //mailMessage.CC.Add(SendCC);
        // mailMessage.CC = bcc;
        /*
           // For SMTP servers that require authentication
           message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", 1);
           message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "SmtpHostUserName");
           message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "SmtpHostPassword");
          */
        // Send mail
       // mailClient.Send(mailMessage);
    }
    // Send error log mail
    public static void LogError(Exception ex)
    {
        // get the current date and time
        string dateTime = DateTime.Now.ToLongDateString() + ", at "
                        + DateTime.Now.ToShortTimeString();
        // stores the error message
        string errorMessage = "Exception generated on " + dateTime;
        // obtain the page that generated the error
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        errorMessage += "\n\n Page location: " + context.Request.RawUrl;
        // build the error message
        errorMessage += "\n\n Message: " + ex.Message;
        errorMessage += "\n\n Source: " + ex.Source;
        errorMessage += "\n\n Method: " + ex.TargetSite;
        errorMessage += "\n\n Stack Trace: \n\n" + ex.StackTrace;
        try
        {
            string path = "~/Error/" + DateTime.Today.ToString("dd-MM-yyyy") + ".txt";
            if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
            {
                File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
            }
            using (StreamWriter w = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
            {
                w.WriteLine("\r\nLog Entry : ");
                w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                string err = "Error Page: " + System.Web.HttpContext.Current.Request.Url.ToString();
                w.WriteLine(err);
                err = "Error Message:" + errorMessage;
                w.WriteLine(err);
                w.WriteLine("__________________________");
                w.Flush();
                w.Close();
            }
        }
        catch (Exception e)
        {
        }
    }
    // Configures what button to be clicked when the uses presses Enter in a 
    // textbox. The text box doesn't have to be a TextBox control, but it must 
    // be derived from either HtmlControl or WebControl, and the HTML control it 
    // generates should accept an 'onkeydown' attribute. The HTML generated by 
    // the button must support the 'Click' event
    public static void TieButton(Page page, Control TextBoxToTie, Control ButtonToTie)
    {
        // Init jscript
        string jsString = "";

        // Check button type and get required jscript
        if (ButtonToTie is LinkButton)
        {
            jsString = "if ((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13)) {"
                + page.ClientScript.GetPostBackEventReference(ButtonToTie, "").Replace(":", "$") + ";return false;} else return true;";
        }
        else if (ButtonToTie is ImageButton)
        {
            jsString = "if ((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13)) {"
                + page.ClientScript.GetPostBackEventReference(ButtonToTie, "").Replace(":", "$") + ";return false;} else return true;";
        }
        else
        {
            jsString = "if ((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13)) {document"
                + "forms[0].elements['" + ButtonToTie.UniqueID.Replace(":", "_") + "'].click();return false;} else return true; ";
        }
        // Attach jscript to the onkeydown attribute - we have to cater for HtmlControl or WebControl
        if (TextBoxToTie is HtmlControl)
        {
            ((HtmlControl)TextBoxToTie).Attributes.Add("onkeydown", jsString);
        }
        else if (TextBoxToTie is WebControl)
        {
            ((WebControl)TextBoxToTie).Attributes.Add("onkeydown", jsString);
        }
    }
    public static string sendRegisterstatusEmailformat(string status, string title, string to, string fname, string lname, string password, string username, int userid)
    {
        string body = "";
        string subject = "";
        string mailfrom = creativeconfiguration.adminemail;
        string bcc = "support@Intranet.com";
        if (status == "R")
        {
            subject = fname +" "+ lname + " (Member Id: " + userid + ")" + " Register Successful on Intranet.com";
            body += "<Font face='Arial' size='2' color='#000000'>";
            body += "<b>Dear " + fname + " " + lname + ",</b>";
            body += "<br><br>";
	   body += "<font size='2' face='Arial'>Thanks for joining Intranet.com.</font>";

            body += "<br><br>";
            body += "<font size='2' face='Arial'>Your username is: </font>" +"<b>"+ username+"</b>";
            body += "<br><br>";
            body += "<font size='2' face='Arial'>Your password is: </font>" +"<b>"+ password+"</b>";
            body += "<br><br>";
	   body += "<font size='2' face='Arial'>If you have any questions or need further assistance, please contact us at <a href=''mailto:&#115;&#117;&#112;&#112;&#111;&#114;&#116;&#64;&#105;&#110;&#100;&#105;&#101;&#109;&#117;&#118;&#105;&#122;&#46;&#99;&#111;&#109;'>support@Intranet.com</a>";
            body += "<br><br>";
	   body+="Sincerly,";
	   body += "<br>";
	   body+="Team Intranet.com";
	   body+="<br>";
	   body += "<a href='" + UrlRewritingVM.ChangeURL("sitepathmain") + "' target='_blank'><img src='" + UrlRewritingVM.ChangeURL("sitepath") + "images/logo.png '></a>";
        }
        if (status == "FP")
        {
            subject = fname + " " + lname + " (Member Id: " + userid + ")" + " Password Recovery Request on Intranet.com";
            body += "<Font face='Arial' size='2' color='#000000'>";
            body += "<b>Dear " + fname + " " + lname + ",</b>";
            body += "<br><br>";
            body += "<a href='http://www.Intranet.com' target='_blank' font-size='9'><font>http://www.Intranet.com</font></a> has received a “Forgot Password” mail from you.";
            body += "<br><br>";
            body += "<font size='2'>Please find below your login details:</font>";
            body += "<br><br>";
            body += "<font size='2'>User ID: </font>" +"<b>"+ username+"</b>";
            body += "<br><br>";
	   body += "<font size='2'>Password: </font>" +"<b>"+ password+"</b>";
            body += "<br><br>";
            body += "<font size='2'>For any other help, please feel free to contact us at <a href=''mailto:&#115;&#117;&#112;&#112;&#111;&#114;&#116;&#64;&#105;&#110;&#100;&#105;&#101;&#109;&#117;&#118;&#105;&#122;&#46;&#99;&#111;&#109;'>support@Intranet.com</a>";
            body += "<br><br>";
           
	   body+="Sincerly,";
            body += "<br>";
	   body+="Team Intranet.com";
	   body+="<br>";
	   body += "<a href='" + UrlRewritingVM.ChangeURL("sitepathmain") + "' target='_blank'><img src='" + UrlRewritingVM.ChangeURL("sitepath") + "images/logo.png ' width='150px' height='50px'></a>";
        }
        if (status == "CP")
        {
            subject = fname + " " + lname + " (Member Id: " + userid + ")" + " send an Password Change Request on Intranet.com";
            body += "<Font face='Arial' size='2' color='#000000'>";
            body += "<b>Dear " + fname + " " + lname + ",</b>";
            body += "<br><br>";
	   body += "This email confirms your recent Intranet password change.";
            body += "<br><br>";
            body += "<font size='2'>Your new password is </font>" + "<b>"+password+"</b>";
            body += "<br><br>";
	   body+="Sincerly,";
	   body += "<br>";
	   body+="Team Intranet.com";
	   body+="<br>";
	   body += "<a href='" + UrlRewritingVM.ChangeURL("sitepathmain") + "' target='_blank'><img src='" + UrlRewritingVM.ChangeURL("sitepath") + "images/logo.png ' width='150px' height='50px'></a>";
        }
        if (status == "L")
        {
            subject = fname + " " + lname + " (Member Id: " + userid + ")" + " Account locked on Intranet.com";
            body += "<Font face='Arial' size='2' color='#000000'>";
            body += "<b>Hello " + fname + " " + lname + ",</b>";
            body += "<br><br>";
	   body += "<font size='2'>Your customer account having </font>";
	   body+="<font size='2'> Username: </font>" +"<b>"+ username+"</b>";
	   body+="<br>";
	  body+= " on <a href='http://www.Intranet.com' target='_blank' font-size='9'><font>http://www.Intranet.com</font></a> is Locked.";
            body += "<br><br>";
            body += "<font size='2'>Please contact administrator to unlock your account.</font>";
            body += "<br><br>";
	   body+="Sincerly,";
	   body += "<br>";
	   body+="Team Intranet.com";
	   body+="<br>";
	   body += "<a href='" + UrlRewritingVM.ChangeURL("sitepathmain") + "' target='_blank'><img src='" + UrlRewritingVM.ChangeURL("sitepath") + "images/logo.png ' width='150px' height='50px'></a>";
        }
        if (status == "PE")
        {
            subject = "Enquiry for the Product " + lname;
            body += "<Font face='Arial' size='2' color='#000000'>";
            body += "<b>Dear " + fname + ",</b>";
            body += "<br><br>";
            body += "<font size='2'>Thank you for your Enquiry for the " + lname + " on <a href='http://www.Intranet.com' target='_blank' font-size='9'><font>http://www.Intranet.com</font></a>";
            body += "<br><br>";
            body += "<font size='2'>Our Customer Support Team will get back to you within 24 hours.</font>";
            body += "<br><br>";
            body += "<font size='2'>For any problems or queries you may email to </font><a href=''mailto:&#115;&#117;&#112;&#112;&#111;&#114;&#116;&#64;&#105;&#110;&#100;&#105;&#101;&#109;&#117;&#118;&#105;&#122;&#46;&#99;&#111;&#109;'>support@Intranet.com</a> or call us at 0000000000.";
            body += "<br><br>";
            body += "keep clicking.";
            body += "<br><br>";
	   body += "<a href='" + UrlRewritingVM.ChangeURL("sitepathmain") + "' target='_blank'><img src='" + UrlRewritingVM.ChangeURL("sitepath") + "images/logo.png'></a>";
        }


        Utilities.SendMailbcc(mailfrom, to, bcc, subject, body);
        return body;
    }
    public static string sendcontactus(string status, string title,string name, string from,string location,string msg)
    {
        string body = "";
        string subject = "";

        string to = creativeconfiguration.adminemail;
        if (status == "CU")
        {
            subject = title;
            body += "<!DOCTYPE HTML PUBLIC ' -//W3C//DTD HTML 4.01 Transitional//EN'>";
            body += "<html>";
            body += "<head>";
            body += "</html>";
            body += "<body>";
            body += "<table>";
            body += "<tr><td width='5'></td><td align='left'>";
            body += "<Font face='Arial' size='2' color='#333333'>";
            body += "<b>Dear Administrator,</b>";
            body += "<br><br>";
            body += " <a href='http://www.Intranet.com' target='_blank' font-size='9'><font>http://www.Intranet.com</font></a>";
            body += " has received the following mail from a visitor to Intranet.com:";
            body += "<br><br>";
            body += "<b>Name :</b> " + name;
            body += "<br>";
            if (location != "")
            {
                body += "<b>Location :</b> " + location;
                body += "<br>";
            }
            body += "<b>Subject :</b> " + title;
            body += "<br>";
            body += "<b>Comment :</b> " + msg;
            body += "<br><br>";
            body += " <b>Please respond at :</b> " + from;                      
            body += "</Font><br><br>";
            body += "<b>Best Regards,</b>";
            body += "<br>";
	   body += "<a href='" + UrlRewritingVM.ChangeURL("sitepathmain") + "' target='_blank'><img src='" + UrlRewritingVM.ChangeURL("sitepath") + "images/logo.png'></a>";
            body += "<br>";
            body += "</td></tr></table>";
            body += "</body>";
            body += "</html>";
        }

        Utilities.SendMailbcc(from, to, "", title, body);
        return body;

      
    }

    public static string sendordermail(string Orderstatus, string to, string fname, string lname, string userid, string orderid, string ostatus, string pname, string pnumber, string quantity, int totalamt,string msg,string pkgflag,string giftamt)
    {
        string body = "";
        string subject = "";
        string mailfrom = creativeconfiguration.adminemail;
        string bcc = "support@Intranet.com";
        DateTime dttime = DateTime.Now;
        string time = dttime.ToString("dd-MMM-yyyy");
        int gvid = 0;
        if (Orderstatus == "o")
        {

            DataTable dto = classpkgorder.getgiftamtorderid(Convert.ToDecimal(orderid));
            if (dto.Rows.Count > 0)
            {
                gvid = Convert.ToInt32(dto.Rows[0]["gvid"]);
            }
            if (ostatus.ToString() == "Successful")
            {
                subject = fname + " " + lname + " (Member Id: " + userid + ")" + " Subscription  Confirmed on Intranet.com";
            }

            else
            {
                subject = fname + " " + lname + " (Member Id: " + userid + ")" + " Subscription Failed on Intranet.com";
            }
	      //  subject = fname + " " + lname + " (Member Id: " + userid + ")" + " Subscription Confirmed on Intranet.com";
	        body += "<table width='100%' cellspacing='0' cellpadding='28' border='2'  align='center' style='font-family: arial,sans-serif;'><tbody><tr><td>";
            body += "<Font face='Arial' size='2' color='#000000'>";
            body += "<b>Dear " + fname + " " + lname + ",</b>";
            body += "<br><br>";
            body += " Thank you for your subscription on Intranet.com";
            body += "<br><br>";
            body += " Your Payment is " + ostatus;
            body += "<br><br>";
	   body += "You can reach us at <a href=''mailto:&#115;&#117;&#112;&#112;&#111;&#114;&#116;&#64;&#105;&#110;&#100;&#105;&#101;&#109;&#117;&#118;&#105;&#122;&#46;&#99;&#111;&#109;'>support@Intranet.com</a> or +91 22 60000000.";
            body += "<br><br>";
            body += "Subscription details for your reference as mentioned below :";
            body += "<br><br>";
            body += "Subscription No: " + " " + orderid;
            body += "<br><br>";
            body += "Activation Date:" + " " + time;
          
            if (gvid != 0)
            {
                body += "<br><br>";
                body +="Gift Voucher Amount: INR "+ giftamt;
            }
           
            body += "<br><br>";
            if (pkgflag == "S")
            {
                body += "<table border='1' width='70%' cellpadding='1' cellspacing='1' bordercolor='#ffffff' class='copy'><tr><td align='center' bgcolor='#EBEBEB' class='bcopy' style='font-family:Arial;font-size:smaller'><b>Movie Name</b></td><td align='center' bgcolor='#EBEBEB' class='bcopy' style='font-family:Arial;font-size:smaller'><b>Subtotal (INR)</b></td><td align='center' bgcolor='#EBEBEB' class='bcopy' style='font-family:Arial;font-size:smaller'><b>Activation Date</b></td><td align='center' bgcolor='#EBEBEB' class='bcopy' style='font-family:Arial;font-size:smaller'><b> Expiry Date</b></td></tr><tr>" + msg + " </tr></table>";
            }

            if (pkgflag == "P")
            {
                body += "<table border='1' width='70%' cellpadding='1' cellspacing='1' bordercolor='#ffffff' class='copy'><tr><td align='center' bgcolor='#EBEBEB' class='bcopy' style='font-family:Arial;font-size:smaller'><b>Package Name</b></td><td align='center' bgcolor='#EBEBEB' class='bcopy' style='font-family:Arial;font-size:smaller'><b>Subtotal(INR)</b></td><td align='center' bgcolor='#EBEBEB' class='bcopy' style='font-family:Arial;font-size:smaller'><b>Activation Date</b></td><td align='center' bgcolor='#EBEBEB' class='bcopy' style='font-family:Arial;font-size:smaller'><b> Expiry Date</b></td></tr><tr>" + msg + " </tr></table>";
            }


            body += "<div style='margin-top:10px'>Total Amt: INR " + " " + totalamt + "</div>";
            body += "<br>";
            body += "Sincerly,";
            body += "<br>";
            body += "Team Intranet.com";
            body += "<br>";
            body += "<a href='" + UrlRewritingVM.ChangeURL("sitepathmain") + "' target='_blank'><img src='" + UrlRewritingVM.ChangeURL("sitepath") + "images/logo.png'></a>";

        }
       
		
		// Utilities.SendMailbcc(mailfrom, to, bcc, subject, body);
        return body;
       
    }

    public static string sendonation(string to, string fname, string lname, string directorname, string donationid, string bank, string amt, string date, string strmade, string userid)
    {
        string body = "";
        string subject = "";

        string from = creativeconfiguration.adminemail;

        subject = fname + " " + lname + " (Member Id: " + userid + ")" + " Thank You For Donation.";
        // subject = "Thank You For Donation";
        body += "<table width='601' cellspacing='0' cellpadding='28' border='2' ' align='center' style='font-family: arial,sans-serif;'><tbody><tr><td>";
        body += "<Font face='Arial' size='2' color='#000000'>";
        body += "<b>Dear " + fname + " " + lname + ",</b>";
        body += "<br><br>";

        body += "Thanks for your recent donation to Intranet.";
        body += "<br><br>";
        body += "Generous gifts from donors like you provide the financial and moral support needed to continue our mission.";
        body += "<br><br>";
        body += "With your faithful financial contributions this years, you’ve demonstrated your deep commitment to director(<b> " + directorname + " </b>) works.";

        body += "<br><br>";
        body += "You can reach us at";
        body += " <a href=''mailto:&#115;&#117;&#112;&#112;&#111;&#114;&#116;&#64;&#105;&#110;&#100;&#105;&#101;&#109;&#117;&#118;&#105;&#122;&#46;&#99;&#111;&#109;'>support@Intranet.com</a> or on +91 22 60000000.";

        body += "<br><br>";
        body += "Below are the online transaction details for your reference:";

        body += "<br><br>";
        body += "Donation Id: " + " " + donationid;
        body += "<br><br>";
        body += "<table border='1' width='70%' cellpadding='1' cellspacing='1' bordercolor='#ffffff' class='copy'><tr><td align='center' bgcolor='#EBEBEB' class='bcopy' style='font-family:Arial;font-size:smaller'><b>Sr No</b></td><td align='center' bgcolor='#EBEBEB' class='bcopy' style='font-family:Arial;font-size:smaller'><b>Funds (INR)</b></td><td align='center' bgcolor='#EBEBEB' class='bcopy' style='font-family:Arial;font-size:smaller'><b>Date</b></td></tr><tr>" + strmade + " </tr></table>";
        body += "<br><br>";

        body += "We have received the payment of INR" + " " + amt;
        body += "<br><br>";
        body += "Sincerly,";
        body += "<br>";
        body += "Team Intranet.com";
        body += "<br>";

        body += "<a href='" + UrlRewritingVM.ChangeURL("sitepathmain") + "' target='_blank'><img src='" + UrlRewritingVM.ChangeURL("sitepath") + "images/logo.png'></a>";
        body += "<br><br>";
        body += "</td></tr></tbody></table>";

        body += "</body>";
        body += "</html>";
        Utilities.SendMailbcc(from, to, "", subject, body);

        return body;
    }

    public static string sendgiftvoucher(string to, string fname, string lname, string gvcode, string date, string gvpassword, string gvamt, string userid)
    {
        string bcc = "";
        string subject = fname + " " + lname + " (Member Id: " + userid + ")" + " Gift voucher Offer on Intranet.com";
      
        string from = creativeconfiguration.adminemail;
        bcc = "support@Intranet.com";
        string body = "";
        body += "<!DOCTYPE HTML PUBLIC ' -//W3C//DTD HTML 4.01 Transitional//EN'>";
        body += "<html>";
        body += "<head>";
        body += "<style>a.link{color:#000000}a.link:hover{color:#000099}</style><meta http-equiv='Content-Type' content='text/html'; charset='iso-8859-1'>";
        body += "</html>";
        body += "<body>";
        body += "<table>";
        body += "<tr><td align='left'>";
        body += "<Font face='Arial' size='2' color='#333333'>";
        body += "<b>Dear " + Convert.ToString(fname.Trim()) + ",</b>";

        body += "<br><br>";
        body += "<b>This coupon is valid till " + date + "</b>";
        body += "<br><br>";
        body += " Your Gift voucher Code:" + "" + "<font color='red'> " + gvcode + "</font>";
        body += "<br>";
        body += " Your Password:" + "" + "<font color='red'> " + gvpassword + "</font>";
        body += "<br><br>";
        body += "Gift Voucher amount: INR " + gvamt;
        body += "<br><br>";
        body += "Please enter this coupon code while doing checkout onto the website.";
        body += "<br><br>";

        body += "<b>Each gift voucher can be used only once.</b>";
        body += "<br><br>";
        body += "Sincerly,";
        body += "<br>";
        body += "Team Intranet.com";
        body += "<br>";
        body += "<a href='" + UrlRewritingVM.ChangeURL("sitepathmain") + "' target='_blank'><img src='" + UrlRewritingVM.ChangeURL("sitepath") + "images/logo.png'></a>";

        body += "<br>";

        body += "</td></tr></table>";
        body += "</body>";
        body += "</html>";
        Utilities.SendMailbcc(from, to, "", subject, body);

        return body;
    }


    public static string sendcanceldonation(string to, string fname, string lname, string directorname, string donationid, string bank, decimal amt, string date, string strmade, string userid)
    {
        string body = "";
        string subject = "";
        string bcc = "support@Intranet.com";
        string mailfrom = creativeconfiguration.adminemail;
        subject = fname + " " + lname + " (Member Id: " + userid + ")" + " Transaction Failed on Intranet.com";

        body += "<table width='601' cellspacing='0' cellpadding='28' border='2' align='center' style='font-family: arial,sans-serif;'><tbody><tr><td>";
        body += "<Font face='Arial' size='2' color='#000000'>";
        body += "<b>Dear " + fname + " " + lname + ",</b>";
        body += "<br><br>";
        body += "Your Payment is Failed.";
        body += "<br><br>";
        body += "You can reach us at <a href='mailto:&#115;&#117;&#112;&#112;&#111;&#114;&#116;&#64;&#105;&#110;&#100;&#105;&#101;&#109;&#117;&#118;&#105;&#122;&#46;&#99;&#111;&#109;'>support@Intranet.com</a> or +91 22 60000000.";
        body += "<br><br>";
        body += "Below are the online fund details for your reference:";
        body += "<br><br>";
        body += "<table border='1' width='70%' cellpadding='1' cellspacing='1' bordercolor='#ffffff' class='copy'><tr><td align='center' bgcolor='#EBEBEB' class='bcopy' style='font-family:Arial;font-size:smaller'><b>Sr No</b></td><td align='center' bgcolor='#EBEBEB' class='bcopy' style='font-family:Arial;font-size:smaller'><b>Funds (INR)</b></td><td align='center' bgcolor='#EBEBEB' class='bcopy' style='font-family:Arial;font-size:smaller'><b>Date</b></td></tr><tr>" + strmade + " </tr></table>";
        body += "<br><br>";
        body += "Sincerly,";
        body += "<br>";
        body += "Team Intranet.com";
        body += "<br>";

        body += "<a href='" + UrlRewritingVM.ChangeURL("sitepathmain") + "' target='_blank'><img src='" + UrlRewritingVM.ChangeURL("sitepath") + "images/logo.png'></a>";
        body += "<br><br>";
        body += "</td></tr></tbody></table>";

        body += "</body>";
        body += "</html>";
      
        Utilities.SendMailbcc(mailfrom, to, bcc, subject, body);
        return body;
    }

    public static string sendpackagealertmail(string Orderstatus, string to, string fname, string lname, string userid, string pkgdate)
    {
        string body = "";
        string subject = "";
        string mailfrom = creativeconfiguration.adminemail;
        string bcc = "support@Intranet.com";
        DateTime dttime = DateTime.Now;
        string time = dttime.ToString("dd-MMM-yyyy");

        if (Orderstatus == "PR")
        {
            subject = "Intranet Premium Package expiring on " + pkgdate;
            body += "<table width='601' cellspacing='0' cellpadding='28' border='1'  align='center' style='font-family: arial,sans-serif;'><tbody><tr><td>";
            body += "<Font face='Arial' size='2' color='#000000'>";
            body += "<b>Dear " + fname + " " + lname + ",</b>";
            body += "<br><br>";

            body += "  The validity of your Premium package for watching movies on";
            body += "<br>";
            body += " <a href='http://www.Intranet.com' target='_blank' font-size='9'><font>http://www.Intranet.com</font></a>";

            body += " is expiring on " + "<b>" + pkgdate + "</b>.";
            body += "<br><br>";
            body += "You can also contact our support team for any help on";
            body += "<br>";
            body += " <a href='mailto:&#115;&#117;&#112;&#112;&#111;&#114;&#116;&#64;&#105;&#110;&#100;&#105;&#101;&#109;&#117;&#118;&#105;&#122;&#46;&#99;&#111;&#109;'>support@Intranet.com</a> or on +91  983964444.";
            body += "<br><br>";
            body += "Sincerly,";
            body += "<br>";
            body += "Team Intranet.com";
            body += "<br>";
            body += "<a href='" + UrlRewritingVM.ChangeURL("sitepathmain") + "' target='_blank'><img src='" + UrlRewritingVM.ChangeURL("sitepath") + "images/logo.png'></a>";
            body += "<br><br>";
            body += "</td></tr></tbody></table>";

        }
        Utilities.SendMailbcc(mailfrom, to, bcc, subject, body);

        return body;
    }


}


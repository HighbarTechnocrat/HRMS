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
using System.Collections.Generic;
using System.Web.Profile;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Web.Services;

public partial class sathisendmail : System.Web.UI.Page
{
    public int mailid, mailid1, mailid2;
    public static int indexid=0;
    int number;
     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} 
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Page.User.Identity.IsAuthenticated)
        {
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != "" && Request.QueryString["ID"].Length == 24 && Request.QueryString.Count == 1)
            {
                if (Int32.TryParse(UrlRewritingVM.Decrypt(Request.QueryString["ID"]), out number) && Request.QueryString.Count ==1)
                {
                    mailid = Convert.ToInt32(commonclass.GetSafeIDFromURL(UrlRewritingVM.Decrypt(Request.QueryString["ID"].ToString())));
                }
                else
                {
                    Response.Redirect(ReturnUrl("sitepathmain") + "errorpg.aspx");
                }
            }
            if (Request.QueryString["mid"] != null && Request.QueryString["mid"] != "" && Request.QueryString["mid"].Length == 24 && Request.QueryString.Count == 1)
            {
                if (Int32.TryParse(UrlRewritingVM.Decrypt(Request.QueryString["mid"]), out number) && Request.QueryString.Count == 1)
                {
                    mailid1 = Convert.ToInt32(commonclass.GetSafeIDFromURL(UrlRewritingVM.Decrypt(Request.QueryString["mid"].ToString())));
                }
                else
                {
                    Response.Redirect(ReturnUrl("sitepathmain") + "errorpg.aspx");
                }
            }

            if (Request.QueryString["rid"] != null && Request.QueryString["rid"] != "" && Request.QueryString["rid"].Length == 24 && Request.QueryString.Count == 1)
            {
                if (Int32.TryParse(UrlRewritingVM.Decrypt(Request.QueryString["rid"]), out number) && Request.QueryString.Count == 1)
                {
                    mailid2 = Convert.ToInt32(commonclass.GetSafeIDFromURL(UrlRewritingVM.Decrypt(Request.QueryString["rid"])));
                }
                else
                {
                    Response.Redirect(ReturnUrl("sitepathmain") + "errorpg.aspx");
                }
            }


            if (Request.QueryString["cid"] != null && Request.QueryString["cid"] != "" && Request.QueryString["cid"].Length == 24 && Request.QueryString.Count == 1)
            {
                if (Int32.TryParse(UrlRewritingVM.Decrypt(Request.QueryString["cid"]), out number) && Request.QueryString.Count == 1)
                {
                    indexid = Convert.ToInt32(commonclass.GetSafeIDFromURL(UrlRewritingVM.Decrypt(Request.QueryString["cid"].ToString())));
                    if (indexid > 0)
                    {
                        indexid = Convert.ToInt32(UrlRewritingVM.Decrypt(Request.QueryString["cid"]));
                    }
                }
                else
                {
                    Response.Redirect(ReturnUrl("sitepathmain") + "errorpg.aspx");
                }
            }
            

            if (!Page.IsPostBack)
            {
                txtTo.Text = "";
                txtTo1.Text = "";
                hdvalue.Value = "";
                ltmsg.Text = "";
                ddllabels.SelectedIndex = 0;
                if (mailid != 0)
                {
                    pnltbl1.Visible = true;
                    btnbrowse.Visible = false;
                    txttitle.Focus();
                    loadmessage(mailid);
                    // txtTo.Attributes.Add("readonly", "readonly");
                }
                else if (indexid > 0)
                {
                    pnltbl1.Visible = true;
                    btnbrowse.Visible = false;
                    txttitle.Focus();
                    DataTable dt = classaddress.getuserbyindexid(indexid);
                    if(dt.Rows.Count > 0)
                    {
                        txtTo.Text = dt.Rows[0]["username"].ToString();
                        txtTo1.Text = dt.Rows[0]["fullname"].ToString();
                        hdvalue.Value = dt.Rows[0]["username"].ToString();
                        ltmsg.Visible = true;
                        ltmsg.Text = "<script type='text/javascript' defer='defer'>$(document).ready(function(){$(\"<span class='tag label label-primary'>" + dt.Rows[0]["fullname"].ToString() + "<span data-role='remove'></span></span>\").insertBefore($('.twitter-typeahead'));});</script>";
                    }
                    else
                    {
                        Response.Redirect(ReturnUrl("sitepathmain") + "errorpg.aspx");
                    }
                }
                else if (mailid1 != 0)
                {
                    pnltbl2.Visible = true;
                    txttitle.Focus();
                    loadmessagereply();
                }
                else if (mailid2 != 0)
                {
                    pnltbl2.Visible = true;
                    txttitle.Focus();
                    loadmessagereply();
                }
                else
                {
                    pnltbl1.Visible = true;
                    btnbrowse.Visible = true;
                    txtTo.Focus();
                    txtTo.Attributes.Add("readonly", "readonly");
                }
            }
        }
        else
        {
            if (Request.Url.AbsoluteUri.Contains("intranet.highbartech.com"))
            //if(Request.Url.AbsoluteUri.Contains("192.168.0.172/hrms/"))
            {
                //     Response.Redirect("http://192.168.0.172/hrms/login.aspx");
                Response.Redirect(ConfigurationManager.AppSettings["internetURL"]);
            }
            else if (Request.Url.AbsoluteUri.Contains("localhost"))
            //else if (Request.Url.AbsoluteUri.Contains("HRMS_ADMIN"))
            //else if (Request.Url.AbsoluteUri.Contains("192.168.0.172/HRMS_ADMIN/"))
            {
                //Response.Redirect("http://192.168.0.172/HRMS_ADMIN/login.aspx");
                Response.Redirect(ConfigurationManager.AppSettings["intranetURL"]);
            }
            else
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "errorpg.aspx");
            }
        }
    }
    public void loadmessage(int mailid)
    {
        maildetails maildtls = classmailsend.getmessagelogdetails(mailid);
        DataTable dt1 = classaddress.Getuserbyemail(maildtls.username);
        if(dt1.Rows.Count > 0)
        {
            txtTo1.Text = dt1.Rows[0]["fullname"].ToString();
            txtTo.Text = maildtls.username;
            hdvalue.Value = maildtls.username;
            ltmsg.Visible = true;
            ltmsg.Text = "<script type='text/javascript'>jQuery(document).ready(function(){jQuery('#MainContent_RequiredFieldValidator3').hide();jQuery(\"<span class='tag label label-primary'>" + dt1.Rows[0]["fullname"].ToString() + "<span data-role='remove'></span></span>\").insertBefore(jQuery('.twitter-typeahead'));});</script>";
            txttitle.Text = "RE : " + maildtls.title + "";
        }
    }
    public void loadmessagereply()
    {
        if (mailid1 != 0)
        {
            DataTable dt1 = classmailsend.getmessagelogdetails1(mailid1);
            if (dt1.Rows.Count > 0)
            {
                rptsender.Visible = true;
                rptsender.DataSource = dt1;
                rptsender.DataBind();
            }
        }
        else if (mailid2 != 0)
        {
            DataTable dt1 = classmailsend.getmessagelogdetails1(mailid2);
            if (dt1.Rows.Count > 0)
            {
                rptsender.Visible = true;
                rptsender.DataSource = dt1;
                rptsender.DataBind();
            }
        }
    }
    protected void btnbrowse_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "maillist.aspx");
    }
    protected void btnsendmail_Click(object sender, EventArgs e)
    {
        try
        {
            if(txtlongdesc.Text=="" && txttitle.Text=="" && txtTo.Text=="")
            {
                if(txtTo.Text=="")
                {
                    lblto.ForeColor = System.Drawing.Color.Red;
                    lblto.Text = "Please select Email-id to send a message.";
                }
                if(txttitle.Text=="")
                {
                    lbltitle.ForeColor = System.Drawing.Color.Red;
                    lbltitle.Text = "Please enter title for message.";
                }
                if(txtlongdesc.Text=="")
                {
                    lbldesc.ForeColor = System.Drawing.Color.Red;
                    lbldesc.Text = "Please enter message.";
                }
            }
            else
            {
            DateTime date = DateTime.Now;
            string userName = Page.User.Identity.Name;
            MembershipUser user = Membership.GetUser(userName);
            string frommail = user.UserName;
            //MailAddress SendFrom = new MailAddress(frommail);
            string cfname = "";
            string fname = "";
            string to;
            DataTable usefromrdetails = classaddress.getuserinfodetails(frommail);
            if (usefromrdetails.Rows.Count > 0)
            {
                fname = usefromrdetails.Rows[0]["fullname"].ToString();
            }

            //to = txtTo.Text;
            to = hdvalue.Value;

            string[] tomail = to.Split(',');

            for (int i = 0; i < tomail.Length; i++)
            {
                string tomail1 = "";
                tomail1 = (tomail[i]).ToString();
                // foreach (string tomail in to.Split(','))
                //  {
                if (tomail1 != "")
                {

                    //string Utitle = Uname.title;
                    DataTable usetordetails = classaddress.getuserinfodetails(tomail1);
                    if (usetordetails.Rows.Count > 0)
                    {
                        cfname = usetordetails.Rows[0]["fullname"].ToString();
                    }


                    classmailsend.createmessagelog1(frommail, tomail[i], txttitle.Text, date, txtlongdesc.Text.ToString().Trim(), fname, "Y", "N", cfname);
                    //        string sub = txttitle.Text;
                    //        string body = "";
                    //        body += "<!DOCTYPE HTML PUBLIC ' -//W3C//DTD HTML 4.01 Transitional//EN'>";
                    //        body += "<html>";
                    //        body += "<head>";
                    //        body += "<style>a.link{color:#000000}a.link:hover{color:#000099}</style><meta http-equiv='Content-Type' content='text/html'; charset='iso-8859-1'>";
                    //        body += "</html>";
                    //        body += "<body>";
                    //        body += "<table><tr><td align='left' valign='middle' height='60' colspan='2'>";

                    //        body += "<Font face='Arial' size='2' color='#333333'>";
                    //        body += "<b>Dear " + fname + ",</b>";
                    //        body += "<br><br>";
                    //        body += txtlongdesc.Text;
                    //        body += "<br>";
                    //        body += "Regards,";

                    //        body += "<br>";
                    //        body += cfname;
                    //        body += "</td></tr></table>";
                    //        body += "</body>";
                    //        body += "</html>";

                    //        //Response.Write(body);
                    //        //Response.End();

                    //        Utilities.SendMail(frommail, tomail1, sub, body);
                }
            }
            //lblMessage.Text = "Message Sent Successfully!";
            resetfields();

            if (mailid != 0)
            {
                //string mid = Convert.ToString(mailid);
                //Response.Redirect(string.Format(ReturnUrl("sitepathmain") + "inbox.aspx?id={0}&flag=Y", mid.ToString()));
                lblmessage.Text = "<font style=\"color:Green\">Message has been sent successfully</font>";
                divsearch.Visible = true;
                pnltbl1.Visible = false;
            }
            else
            {
                //Response.Redirect(ReturnUrl("sitepathmain") + "outbox.aspx?flag=Y");
                lblmessage.Text = "<font style=\"color:Green\">Message has been sent successfully</font>";
                divsearch.Visible = true;
                pnltbl1.Visible = false;
            }
            }
            
        }
        catch(Exception ex)
        {

        }
    }
    public void resetfields()
    {
        txtTo.Text = "";
        txttitle.Text = "";
        txtlongdesc.Text = "";
    }
    protected void btnreply_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format(ReturnUrl("sitepathmain") + "composemail.aspx?Id={0}", UrlRewritingVM.Encrypt(mailid1.ToString())));
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format(ReturnUrl("sitepathmain") + "inbox.aspx"));
    }
    protected void ddllabels_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddllabels.SelectedIndex == 0)
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "composemail.aspx");
        }
        else if (ddllabels.SelectedIndex == 1)
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "inbox.aspx");
        }
        else if (ddllabels.SelectedIndex == 2)
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "outbox.aspx");
        }
    }
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }
    protected void lnkcont_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "composemail.aspx");
    }
    [WebMethod]
    public static List<string> GetAutoCompleteData(string username)
    {
        List<string> result = new List<string>();
        using (SqlConnection sqlcon1 = new SqlConnection(ConfigurationManager.ConnectionStrings["GlobalConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("select indexid, firstname+' '+ lastname as fullname from addressbook where firstname LIKE @SearchText+'%'", sqlcon1))
            {
                sqlcon1.Open();
                cmd.Parameters.AddWithValue("@SearchText", username);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    result.Add(dr["fullname"].ToString());
                }
                return result;
            }
        }
    }
}
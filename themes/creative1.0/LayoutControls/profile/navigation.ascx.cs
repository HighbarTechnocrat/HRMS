using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Text;
using System.Web.Security;
using System.Web.UI.HtmlControls;

public partial class themes_creative1 : System.Web.UI.UserControl
{
    public static string siteurl = "",username="";
    public static int userid;

     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {
        loaddata();
    }
    public void loaddata()
    {
        if (Request.QueryString["userid"] != null)
        {
            userid = Convert.ToInt32(UrlRewritingVM.Decrypt(Request.QueryString["userid"].ToString()));
        }
        else
        {
            DataTable dtadd = classaddress.getuserinfodetails(Page.User.Identity.Name);
            if (dtadd.Rows.Count > 0)
            {
                userid = Convert.ToInt32(dtadd.Rows[0]["indexid"].ToString());
            }
        }
        DataTable user = classreviews.getuserbyindexid(userid);
        if (user.Rows.Count > 0)
        {
            DataTable review = classreviews.getuserreviewdetails(userid);
            if (review.Rows.Count > 0)
            {
                lblreview.Text = review.Rows.Count.ToString();
            }
            else
            {
                lblreview.Text = "0";
            }
            username = user.Rows[0]["username"].ToString().Trim();
            DataTable follower = classreviews.getuserfollowers(username);
            if (follower.Rows.Count > 0)
            {
                lblfollowcount.Text = follower.Rows.Count.ToString();
            }
            else
            {
                lblfollowcount.Text = "0";
            }
            DataTable notifiaction = classreviews.getNotificationHeader(username.ToString().Trim());
            if(notifiaction.Rows.Count > 0)
            {
                lblnotifycaount.Text = notifiaction.Rows.Count.ToString().Trim();
            }
            else
            {
                lblnotifycaount.Text = "0";
            }

            DataTable message = classreviews.getnewmessage(username.ToString().Trim());
            if(message.Rows.Count >0)
            {
                lblmsgcount.Text = message.Rows.Count.ToString().Trim();
            }
            else
            {
                lblmsgcount.Text = "0";
            }

            DataTable following = classreviews.getfollowinglist(username);
            if (following.Rows.Count > 0)
            {
                lblfollowingcount.Text = following.Rows.Count.ToString();
            }
            else
            {
                lblfollowingcount.Text = "0";
            }
            DataTable favorite = classreviews.getuserfavorits(username);
            if (favorite.Rows.Count > 0)
            {
                lblfavorites.Text = favorite.Rows.Count.ToString();
            }
            else
            {
                lblfavorites.Text = "0";
            }

            DataTable grp = classreviews.getusergroups(username);
            if (grp.Rows.Count > 0)
            {
                lblgroups.Text = grp.Rows.Count.ToString();
            }
            else
            {
                lblgroups.Text = "0";
            }
            if(username!=Page.User.Identity.Name.ToString().Trim())
            {
                lisettings.Visible = false;
                lienquiry.Visible = false;
                linotification.Visible = false;
                limessgae.Visible = false;
            }
            lnkratings.HRef = ReturnUrl("sitepathmain") + "user/" + UrlRewritingVM.Encrypt(userid.ToString().Trim());
            userprofile.HRef = ReturnUrl("sitepathmain") + "profile/" + UrlRewritingVM.Encrypt(userid.ToString().Trim());
            lnkfollowers.HRef = ReturnUrl("sitepathmain") + "followers/" + UrlRewritingVM.Encrypt(userid.ToString().Trim());
            lnkfollowing.HRef = ReturnUrl("sitepathmain") + "following/" + UrlRewritingVM.Encrypt(userid.ToString().Trim());
            usergroups.HRef = ReturnUrl("sitepathmain") + "groups/" + UrlRewritingVM.Encrypt(userid.ToString().Trim());
            lnkfavorites.HRef = ReturnUrl("sitepathmain") + "favorites/" + UrlRewritingVM.Encrypt(userid.ToString().Trim());
            lnksettings.HRef = ReturnUrl("sitepathmain") + "procs/settings.aspx";
            notifications.HRef = ReturnUrl("sitepathmain") + "notification.aspx";
            messages.HRef = ReturnUrl("sitepathmain") + "inbox.aspx";
            lnkenquiry.HRef = ReturnUrl("sitepathmain") + "enquiry/";
        }

            siteurl = Request.RawUrl.ToString().Trim();
            clear();

            if (siteurl.Contains("/user/"))
            {
                lnkratings.Attributes.Add("class", "selected");
            }
            else if (siteurl.Contains("profile"))
            {
                userprofile.Attributes.Add("class", "selected");
            }
            else if (siteurl.Contains("notification"))
            {
                notifications.Attributes.Add("class", "selected");
            }
            else if (siteurl.Contains("inbox") || siteurl.Contains("outbox") || siteurl.Contains("composemail"))
            {
                messages.Attributes.Add("class", "selected");
            }
            else if (siteurl.Contains("followers"))
            {
                lnkfollowers.Attributes.Add("class", "selected");
            }
            else if (siteurl.Contains("following"))
            {
                lnkfollowing.Attributes.Add("class", "selected");
            }
            else if (siteurl.Contains("groups"))
            {
                usergroups.Attributes.Add("class", "selected");
            }
            else if (siteurl.Contains("settings"))
            {
                lnksettings.Attributes.Add("class", "selected");
            }
            else if (siteurl.Contains("favorites"))
            {
                lnkfavorites.Attributes.Add("class", "selected");
            }
            else if (siteurl.Contains("enquiry"))
            {
                lnkenquiry.Attributes.Add("class", "selected");
            }
    }
    public void clear()
    {
        lnkratings.Attributes.Add("class", "");
        userprofile.Attributes.Add("class", "");
        notifications.Attributes.Add("class", "");
        messages.Attributes.Add("class", "");
        lnkfollowers.Attributes.Add("class", "");
        lnkfollowing.Attributes.Add("class", "");
        usergroups.Attributes.Add("class", "");
        lnksettings.Attributes.Add("class", "");
        lnkfavorites.Attributes.Add("class", "");
    }
}
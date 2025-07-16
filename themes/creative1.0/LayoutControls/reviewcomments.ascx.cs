using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class themes_creative1_LayoutControls_reviewcomments : System.Web.UI.UserControl
{
    public static int reviewid;

     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {
        loadgrid();
    }
    public void loadgrid()
    {
        if (Request.QueryString["reviewid"] != null)
        {
            reviewid = Convert.ToInt32(Request.QueryString["reviewid"].ToString());
        }
        DataTable dt = classreviews.getreviewdetailsbyreviewid(reviewid);
        if (dt.Rows.Count > 0)
        {
            //imgcover.Src = ConfigurationManager.AppSettings["sitepathadmin"]+ "images/bigproduct/" + dt.Rows[0]["bigimage"].ToString();
            //lblproductname.Text = dt.Rows[0]["productname"].ToString();
	    
            lnkusername.Text = dt.Rows[0]["fullname"].ToString();
            string str = dt.Rows[0]["profilephoto"].ToString();
            if (dt.Rows[0]["profilephoto"].ToString()!="")
            {
                imgprofile.Src = "https://graph.facebook.com/" + dt.Rows[0]["profilephoto"].ToString() + "/picture?type=large";
            }
            else
            {
                imgprofile.Src =ReturnUrl("sitepath")+"images/noprofile.jpg";
            }
            lnkusername.NavigateUrl = ReturnUrl("sitepathmain") + "reviewratings.aspx?userid=" + dt.Rows[0]["indexid"].ToString();
            lnkimage.NavigateUrl = ReturnUrl("sitepathmain") + "reviewratings.aspx?userid=" + dt.Rows[0]["indexid"].ToString();
            //rating.Text = dt.Rows[0]["ratingvalue"].ToString();
            hdvalue.Value = dt.Rows[0]["ratingvalue"].ToString();
            review.Text = dt.Rows[0]["reviewtext"].ToString();
            lblreviewid.Text = dt.Rows[0]["reviewid"].ToString();
            lblproductid.Text = dt.Rows[0]["productid"].ToString();
            lnkfollow.CommandArgument = dt.Rows[0]["username"].ToString();
            if (Page.User.Identity.Name == dt.Rows[0]["username"].ToString())
            {
                lnkfollow.Visible = false;
            }
            if (Convert.ToInt32(dt.Rows[0]["commentcount"].ToString()) > 0)
            {
		hdcomment.Value=dt.Rows[0]["commentcount"].ToString();
                //lblcomment.Text = "Comment(" + dt.Rows[0]["commentcount"].ToString() + ")";
            }
            else
            {
                lblcomment.Text = "Comment";
            }
            DataTable dt2 = classreviews.getfollower(Page.User.Identity.Name, dt.Rows[0]["username"].ToString());
            if (dt2.Rows.Count > 0)
            {
                lnkfollow.Text = "Unfollow";
            }
            else
            {
                lnkfollow.Text = "Follow";
            }
            DataTable dt3 = classreviews.getreviewdetails(Convert.ToInt32(lblproductid.Text), Page.User.Identity.Name);
            if (dt3.Rows.Count > 0)
            {
                if (Convert.ToInt32(dt3.Rows[0]["likecount"].ToString()) > 0)
                {
                    lnklike.Text = dt3.Rows[0]["likeflag"].ToString() + "(" + dt3.Rows[0]["likecount"].ToString() + ")";
                }
                else
                {
                    lnklike.Text = dt3.Rows[0]["likeflag"].ToString();
                }
            }
        }
    }
    protected void lnklike_Click(object sender, EventArgs e)
    {
        bool iserror;
        if (Page.User.Identity.IsAuthenticated)
        {
            iserror = classreviews.insertdeletelikes(Page.User.Identity.Name, Convert.ToInt32(lblreviewid.Text));
            if (!iserror)
            {
                loadgrid();
            }
        }
    }
    protected void lnkcomment_Click(object sender, EventArgs e)
    {

    }
    protected void lnkfollow_Click(object sender, EventArgs e)
    {
        bool iserror;
        if (Page.User.Identity.IsAuthenticated)
        {
            iserror = classreviews.insertdeletefollowing(Page.User.Identity.Name, lnkfollow.CommandArgument.ToString());
            if (!iserror)
            {
                loadgrid();
            }
        }
    }
}

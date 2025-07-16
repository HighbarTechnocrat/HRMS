using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class themes_creative1_LayoutControls_profileheader : System.Web.UI.UserControl
{
    public static int userid;

     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["userid"] != null)
        {
            userid = Convert.ToInt32(Request.QueryString["userid"].ToString());
        }
        loadgrid();
    }
    public void loadgrid()
    {
        DataTable user = classreviews.getuserbyindexid(userid);
        if (user.Rows.Count > 0)
        {
            if (user.Rows[0]["coverphoto"].ToString() != "")
            {
                imgcover.Src = ReturnUrl("sitepath")+"images/coverphoto/"+user.Rows[0]["coverphoto"].ToString();
            }
            else
            {
                imgcover.Src = "http://192.168.0.72/imadmin/images/bigproduct/noimage2.gif";
            }
            if (user.Rows[0]["profilephoto"].ToString() != "")
            {
                imgprofile.Src= "https://graph.facebook.com/" + user.Rows[0]["profilephoto"].ToString() + "/picture?type=large";
            }
            else
            {
                imgprofile.Src =ReturnUrl("sitepath")+"images/noprofile.jpg";
            }
            
            lblusername.Text = user.Rows[0]["fullname"].ToString();
            DataTable review = classreviews.getuserreviewdetails(userid);
	    if(review.Rows.Count>0)
	    {
		lnkreview.Text="Reviews & Ratings ("+review.Rows.Count+")";
	    }
	    else
	    {
		lnkreview.Text="Reviews & Ratings (0)";
	    }
	    DataTable follower = classreviews.getuserfollowers(user.Rows[0]["username"].ToString());
	    if(follower.Rows.Count>0)
	    {
		lnkfollowers.Text="Followers ("+follower.Rows.Count+")";
	    }
	    else
	    {
		lnkfollowers.Text="Followers (0)";
	    }
	    DataTable following = classreviews.getfollowinglist(user.Rows[0]["username"].ToString());
	    if(following.Rows.Count>0)
	    {
		lnkfollowing.Text="Following ("+following.Rows.Count+")";
	    }
	    else
	    {
		lnkfollowing.Text="Following (0)";
	    }
            lnkreview.NavigateUrl =ReturnUrl("sitepathmain")+"reviewratings.aspx?userid="+userid;
            lnkfollowers.NavigateUrl = ReturnUrl("sitepathmain") + "followers.aspx?userid=" + userid;
            lnkfollowing.NavigateUrl = ReturnUrl("sitepathmain") + "following.aspx?userid=" + userid;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

public partial class themes_creative1_LayoutControls_profile_zommer: System.Web.UI.UserControl
{
    public static int userid;

    public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;}
    protected void Page_Load(object sender, EventArgs e)
    {
        loadgrid();
    }
    public void loadgrid()
    {
        if (Request.QueryString["userid"] != null)
        {
            userid = Convert.ToInt32(UrlRewritingVM.Decrypt(Request.QueryString["userid"].ToString()));
        }
        string path = "";
        DataTable user = classreviews.getuserbyindexid(userid);
        if (user.Rows.Count > 0)
        {
            if (user.Rows[0]["coverphoto"].ToString() != "")
            {
                if (File.Exists(Server.MapPath("~/themes/creative1.0/images/coverphoto/" + user.Rows[0]["coverphoto"].ToString())))
                {
                    //coverphoto.Attributes.Add("style", "background-image:url('" + ReturnUrl("sitepath") + "images/coverphoto/" + user.Rows[0]["coverphoto"].ToString() + "')");
                }
                
            }
            else
            {
              //  coverphoto.Attributes.Add("style", "background-image:url('" + ConfigurationManager.AppSettings["adminsitepath"]+ "images/coverphoto/cover.jpg')");
			   // coverphoto.Attributes.Add("style", "background-color:#ccccc");
            }
            
            if (user.Rows[0]["profilephoto"].ToString() != "")
            {
                if (File.Exists(Server.MapPath("~/themes/creative1.0/images/profilephoto/" + user.Rows[0]["profilephoto"].ToString())))
                {
                    //imgprofile.Src = ReturnUrl("sitepath") + "images/profilephoto/" + user.Rows[0]["profilephoto"].ToString();
                }
                else
                {
                    //imgprofile.Src = "https://graph.facebook.com/" + user.Rows[0]["profilephoto"].ToString() + "/picture?type=large";
                }
            }
            else
            {
                //imgprofile.Src = ConfigurationManager.AppSettings["adminsitepath"]+ "images/noprofile.jpg";
            }
            //lblusername.Text = user.Rows[0]["fullname"].ToString();
            DataTable review = classreviews.getuserreviewdetails(userid);
            if (review.Rows.Count > 0)
            {
                lnkreview.Text = "Ratings & Reviews (" + review.Rows.Count + ")";
            }
            else
            {
                lnkreview.Text = "Ratings & Reviews (0)";
            }
            DataTable follower = classreviews.getuserfollowers(user.Rows[0]["username"].ToString());
            if (follower.Rows.Count > 0)
            {
                lnkfollowers.Text = "Followers (" + follower.Rows.Count + ")";
            }
            else
            {
                lnkfollowers.Text = "Followers (0)";
            }
            DataTable following = classreviews.getfollowinglist(user.Rows[0]["username"].ToString());
            if (following.Rows.Count > 0)
            {
                lnkfollowing.Text = "Following (" + following.Rows.Count + ")";
            }
            else
            {
                lnkfollowing.Text = "Following (0)";
            }
            DataTable favorite = classreviews.getuserfavorits(user.Rows[0]["username"].ToString());
            if(favorite.Rows.Count>0)
            {
                lnkfavorite.Text = "Favorites (" + favorite.Rows.Count + ")";
            }
            else
            {
                lnkfavorite.Text = "Favorites (0)";
            }

            DataTable grp = classreviews.getusergroups(user.Rows[0]["username"].ToString());
            if (grp.Rows.Count > 0)
            {
                lnkgroup.Text = "Groups (" + grp.Rows.Count + ")";
            }
            else
            {
                lnkgroup.Text = "Groups (0)";
            }


            //lnkuserimage.HRef = ReturnUrl("sitepathmain") + "user/" + UrlRewritingVM.Encrypt(userid.ToString());
            //lnkusername.HRef = ReturnUrl("sitepathmain") + "user/" + UrlRewritingVM.Encrypt(userid.ToString());
            lnkreview.NavigateUrl = ReturnUrl("sitepathmain") + "user/" + UrlRewritingVM.Encrypt(userid.ToString());
            lnkfollowers.NavigateUrl = ReturnUrl("sitepathmain") + "followers/" + UrlRewritingVM.Encrypt(userid.ToString());
            lnkfollowing.NavigateUrl = ReturnUrl("sitepathmain") + "following/" + UrlRewritingVM.Encrypt(userid.ToString());
            lnkfavorite.NavigateUrl = ReturnUrl("sitepathmain") + "favorites/" + UrlRewritingVM.Encrypt(userid.ToString());
            lnkgroup.NavigateUrl = ReturnUrl("sitepathmain") + "groups/" + UrlRewritingVM.Encrypt(userid.ToString());
            lnkprofile.NavigateUrl = ReturnUrl("sitepathmain") + "profile/" + UrlRewritingVM.Encrypt(userid.ToString());
            if (Request.RawUrl.Contains("favorites"))
            {
                lnkfavorite.Attributes.Add("style", "color:#f28820");
                Page.Title = "Favorites";
            }
            else if(Request.RawUrl.Contains("follower"))
            {
                lnkfollowers.Attributes.Add("style", "color:#f28820");
                Page.Title = "Followers";
            }
            else if (Request.RawUrl.Contains("following"))
            {
                lnkfollowing.Attributes.Add("style", "color:#f28820");
                Page.Title = "Following";
            }
            else if (Request.RawUrl.Contains("/user"))
            {
                lnkreview.Attributes.Add("style", "color:#f28820");
                Page.Title = "Ratings & Reviews";
            }    
            else if(Request.RawUrl.Contains("groups"))
            {
                lnkgroup.Attributes.Add("style", "color:#f28820");
                Page.Title = "Groups";
            }
            else if (Request.RawUrl.Contains("profile"))
            {
                lnkprofile.Attributes.Add("style", "color:#f28820");
                Page.Title = "View Profile";
            }
        }
    }
}
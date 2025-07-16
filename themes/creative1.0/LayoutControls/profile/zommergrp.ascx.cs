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
    public static int grpid;
     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {
        loadgrid();
    }
    public void loadgrid()
    {
        if (Request.QueryString["grpid"] != null)
        {
            grpid = Convert.ToInt32(UrlRewritingVM.Decrypt(Request.QueryString["grpid"].ToString()));
        }
        string path = "";
        DataTable user = classreviews.getgrpbygrpid(grpid);
        if (user.Rows.Count > 0)
        {
            if (user.Rows[0]["grpcoverimg"].ToString() != "")
            {
                if (user.Rows[0]["grpcoverimg"].ToString().Length > 0)
                {
                    coverphoto.Attributes.Add("style", "background-image:url('" + ConfigurationManager.AppSettings["sitepathadmin"]+ "images/grpcover/" + user.Rows[0]["grpcoverimg"].ToString() + "')");
                }
            }
            else
            {
                
            }

            if (user.Rows[0]["grpimg"].ToString() != "")
            {
                if ( user.Rows[0]["grpimg"].ToString().Length>0)
                {
                    imgprofile.Src = ConfigurationManager.AppSettings["sitepathadmin"]+ "images/grpimages/thumbnail/" + user.Rows[0]["grpimg"].ToString();
                }
                else
                {
                    imgprofile.Src = "https://graph.facebook.com/" + user.Rows[0]["grpimg"].ToString() + "/picture?type=large";
                }
            }
            else
            {
                imgprofile.Src = ConfigurationManager.AppSettings["adminsitepath"]+ "images/noprofile.jpg";
            }
            lblusername.Text = user.Rows[0]["grpname"].ToString();
            DataTable review = classreviews.getmembercounts(grpid);
            if (review.Rows.Count > 0)
            {
                lnkmem.Text = "Members (" + review.Rows[0]["member"].ToString() + ")";
            }
            else
            {
                lnkmem.Text = "Members (0)";
            }
            DataTable gpst = classreviews.getpostcount(grpid);

            if (gpst.Rows.Count > 0)
            {
                lnkpost.Text = "Post (" + gpst.Rows[0]["grppost"].ToString() +")";
            }
            else
            {
                lnkpost.Text = "Post (0)";
            }

            lnkmem.NavigateUrl = ReturnUrl("sitepathmain") + "groupdetails/" + UrlRewritingVM.Encrypt(grpid.ToString());
            lnkpost.NavigateUrl = ReturnUrl("sitepathmain") + "grouppost/" + UrlRewritingVM.Encrypt(grpid.ToString());
           if (Request.RawUrl.Contains("groupdetails"))
            {
                lnkmem.Attributes.Add("style", "color:#2e85dc");
                Page.Title = "groupdetails";
            }
            else if(Request.RawUrl.Contains("grouppost"))
            {
                lnkpost.Attributes.Add("style", "color:#2e85dc");
                Page.Title = "grouppost";
            }
            
        }
    }
}
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System;
using System.IO;

public partial class notify : System.Web.UI.Page
{
    public static int userid;

     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.User.Identity.IsAuthenticated)
        {
            loadgrid();
        }
        else
        {
            divnotify.Visible = false;
        }
    }
    
    public void loadgrid()
    {
        if (Request.QueryString["userid"] != null)
        {
            userid = Convert.ToInt32(Request.QueryString["userid"].ToString());
        }
        DataTable user = classreviews.getuserbyindexid(userid);
        DataTable dt2 = classreviews.getnotification(user.Rows[0]["username"].ToString());
        rptnotify.DataSource = dt2;
        rptnotify.DataBind();
        if (rptnotify.Items.Count > 0)
        {
            notification_count.Text = rptnotify.Items.Count.ToString();
        }
        else
        {
            notification_count.Visible = false;
        }
        if(rptnotify.Items.Count>0)
        {
            for(int i=0;i<rptnotify.Items.Count;i++)
            {
                LinkButton lnkuser = (LinkButton)rptnotify.Items[i].FindControl("lnkuser");
                Label lbldate = (Label)rptnotify.Items[i].FindControl("lbldate");
                Image imgprofile = (Image)rptnotify.Items[i].FindControl("imgprofile");
                if (dt2.Rows[i]["profilephoto"].ToString() != "")
                {
                    if (File.Exists(Server.MapPath("~/themes/creative1.0/images/profilephoto/" + dt2.Rows[i]["profilephoto"].ToString())))
                    {
                        imgprofile.ImageUrl = ReturnUrl("sitepathmain") + "images/profilephoto/" + dt2.Rows[i]["profilephoto"].ToString();
                    }
                    else
                    {
                        imgprofile.ImageUrl = "https://graph.facebook.com/" + dt2.Rows[i]["profilephoto"].ToString() + "/picture?type=large";
                    }

                }
                else
                {
                    imgprofile.ImageUrl = ConfigurationManager.AppSettings["adminsitepath"]+ "images/noprofile.jpg";
                }
                
                DateTime dat = Convert.ToDateTime(dt2.Rows[i]["followingdate"].ToString());
                lbldate.Text = dat.ToString("dd-MMM-yyyy");
            }
            

        }
    }
    protected void rptnotify_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        bool iserror;

        if(e.CommandName=="cmdflag")
        {
            try
            {
                iserror=classreviews.updatenotification(Page.User.Identity.Name,e.CommandArgument.ToString());
                if(iserror==true)
                {
                    DataTable user=classreviews.getuseridbyemail(e.CommandArgument.ToString());
                    if(user.Rows.Count>0)
                    {
                        Response.Redirect(ReturnUrl("sitepath") + "userprofile.aspx?userid=" + user.Rows[0]["indexid"].ToString());
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
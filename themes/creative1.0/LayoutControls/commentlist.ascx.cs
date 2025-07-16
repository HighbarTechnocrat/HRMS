using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class themes_creative1_LayoutControls_commentlist : System.Web.UI.UserControl
{
    public static int reviewid;

     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {	
	if(!IsPostBack)
	{
        loadgrid();
	txtcomment.Text="";
	}
    }
    public void loadgrid()
    {
        if (Request.QueryString["reviewid"] != null)
        {
            reviewid = Convert.ToInt32(Request.QueryString["reviewid"].ToString());
        }
        //lblcount.Text = (400 - txtcomment.Text.Length) + " characters remaining";
        DataTable dt4 = classreviews.getcommentsbyreviewid(Convert.ToInt32(reviewid));
        rptcomments.DataSource = dt4;
        rptcomments.DataBind();
        if (rptcomments.Items.Count > 0)
        {
            for (int i = 0; i <= rptcomments.Items.Count - 1; i++)
            {
                HyperLink user = (HyperLink)rptcomments.Items[i].FindControl("lnkusername");
                HyperLink image = (HyperLink)rptcomments.Items[i].FindControl("lnkimage");
                Image img = (Image)rptcomments.Items[i].FindControl("imgprofile");
                if (dt4.Rows[i]["profilephoto"].ToString() != "")
                {
                    img.ImageUrl = "https://graph.facebook.com/" + dt4.Rows[0]["profilephoto"].ToString() + "/picture?type=large";
                }
                else
                {
                    img.ImageUrl = ReturnUrl("sitepath")+"images/noprofile.jpg";
                }
                   user.NavigateUrl = ReturnUrl("sitepathmain") + "reviewratings.aspx?userid=" + dt4.Rows[i]["indexid"].ToString();
                   image.NavigateUrl = ReturnUrl("sitepathmain") + "reviewratings.aspx?userid=" + dt4.Rows[i]["indexid"].ToString();
            }
        }

        DataTable dt5 = classreviews.getuseridbyemail(Page.User.Identity.Name);
        if (dt5.Rows.Count > 0)
        {
            lnkusername2.Text = dt5.Rows[0]["fullname"].ToString();
            lnkusername2.NavigateUrl = ReturnUrl("sitepathmain") + "reviewratings.aspx?userid=" + dt5.Rows[0]["indexid"].ToString();
            lnkimage2.NavigateUrl = ReturnUrl("sitepathmain") + "reviewratings.aspx?userid=" + dt5.Rows[0]["indexid"].ToString();
            if(dt5.Rows[0]["profilephoto"].ToString()!="")
            {
                imgprofile2.Src = "https://graph.facebook.com/" + dt5.Rows[0]["profilephoto"].ToString() + "/picture?type=large";
            }
            else
            { 
                imgprofile2.Src = ReturnUrl("sitepath")+"images/noprofile.jpg";
            }
        }
    }

    protected void rptcomments_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        bool iserror;
        if (Request.QueryString["reviewid"] != null)
        {
            reviewid = Convert.ToInt32(Request.QueryString["reviewid"].ToString());
        }
        if (Page.User.Identity.IsAuthenticated)
        {

            if (txtcomment.Text.ToString() == "")
            {
                iserror = true;
                lblerror.Visible = true;
                lblerror.Text = "Please enter comment!";
            }
            else
            {
                lblerror.Visible = false;
                iserror = false;
            }
           
            if (!iserror)
            {
                iserror = classreviews.insercommentbyreviewid(Page.User.Identity.Name, Convert.ToInt32(reviewid), txtcomment.Text);                
                loadgrid();
            }
        }
	txtcomment.Text = "";

    }

}

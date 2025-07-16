using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class themes_creative1_LayoutControls_commentheader : System.Web.UI.UserControl
{
    public static int reviewid;

     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.User.Identity.IsAuthenticated)
        {
            loadgrid(); 
        
        }
        else
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");
        }
       
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
            imgcover.Src = ConfigurationManager.AppSettings["sitepathadmin"]+ "images/bigproduct/" + dt.Rows[0]["bigimage"].ToString();
            lblproductname.Text = dt.Rows[0]["productname"].ToString();
        }
    }
}
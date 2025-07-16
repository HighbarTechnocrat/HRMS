using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System;
using System.IO;

public partial class themes_creative1_LayoutControls_profile_viewprofile : System.Web.UI.UserControl
{
    public static int userid;
    public static string users;

     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["userid"] != null)
        {
            userid = Convert.ToInt32(UrlRewritingVM.Decrypt(Request.QueryString["userid"].ToString()));
        }
        if(Page.User.Identity.IsAuthenticated == false)
        {
            string strur = ReturnUrl("sitepathmain") + "profile/" + UrlRewritingVM.Encrypt(userid.ToString());
            Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + strur);
        }
        if(!IsPostBack)
        {

        }
    }



    protected void btnedit_Click(object sender, EventArgs e)
    {
        
    }
    protected void btnhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pdnew : System.Web.UI.Page
{
     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["c"] != null && Request.QueryString["c"] != "" )
        {
            int catid;
            if (Int32.TryParse(commonclass.GetSafeIDFromURL(UrlRewritingVM.Decrypt(Request.QueryString["c"].ToString().Trim())), out catid))
            {
                Response.Write(catid);
                Response.End();
            }
        }
        if (Request.QueryString["p"] != null && Request.QueryString["p"] != "" && Request.QueryString.Count == 1)
        {
            int proid;
            if (Int32.TryParse(commonclass.GetSafeIDFromURL(UrlRewritingVM.Decrypt(Request.QueryString["p"].ToString().Trim())), out proid))
            {
                //Response.Write(proid);
                //Response.End();
            }
            else
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "errorpg.aspx");
            }
        }
        else
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "errorpg.aspx");
        }

     
    }
}
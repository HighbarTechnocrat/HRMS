using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public partial class themes_creative1_LayoutControls_pdnew_moviebanner : System.Web.UI.UserControl
{
    private static int productId;
     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
            {
                productId = Convert.ToInt32(UrlRewritingVM.Decrypt(Request.QueryString["p"].ToString()));
            }
            loaddata();
        }
    }
    public void loaddata()
    {
        if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
        {
            productId = Convert.ToInt32(UrlRewritingVM.Decrypt(Request.QueryString["p"].ToString()));
        }
        DataTable banner = classreviews.getmoviebanner(productId);
        if(banner.Rows.Count>0)
        {
            if (banner.Rows.Count >1)
            {
                banner1.Src = ConfigurationManager.AppSettings["adminsitepath"]+ "images/moviebanner/" + banner.Rows[0]["imagename"].ToString();
                banner2.Src = ConfigurationManager.AppSettings["adminsitepath"]+ "images/moviebanner/" + banner.Rows[1]["imagename"].ToString();
                lnkbanner1.HRef = banner.Rows[0]["linkname"].ToString();
                lnkbanner2.HRef = banner.Rows[1]["linkname"].ToString();
            }
            else
            {
                banner1.Src = ConfigurationManager.AppSettings["adminsitepath"]+ "images/moviebanner/" + banner.Rows[0]["imagename"].ToString();
                lnkbanner1.HRef = banner.Rows[0]["linkname"].ToString();
            }
        }
        else
        {
            features.Visible = false;
        }
    }
}
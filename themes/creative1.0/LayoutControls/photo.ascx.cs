using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class themes_creative_LayoutControls_photo : System.Web.UI.UserControl
{
     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            popularcontrol();
        }
    }
    protected void popularcontrol()
    {
        creative.Common clsCommon = new creative.Common();
        
    }
    protected void rptcategname_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
    }
    protected void rptcatproduct_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
    }
    public string productUrlrewriting(object productname, object productid)
    {
        string strurl = "";
        try
        {
            strurl = UrlRewritingVM.getUrlRewritingInfo(productname, productid, "PD");
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }
    public string onclick_hlnkcategory(Object catId, Object catname)
    {
        string strurl = "";
        strurl = UrlRewritingVM.getUrlRewritingInfo(catname, catId, "PS");
        return strurl;
    }
}
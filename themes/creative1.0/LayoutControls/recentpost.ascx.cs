using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class themes_creative1 : System.Web.UI.UserControl
{
    public static int userid, PageCount = 5;
    public static string emailid;
    public int pid;
    public string fname = null;

     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            bindwall(PageCount);
        }
    }
    public void bindwall(int pagecount)
    {
        DataSet dswall = new DataSet();
      //  dswall.ReadXml(ConfigurationManager.AppSettings["adminsitepath"]+ "xml/mywall.xml");
        if(dswall.Tables.Count>0)
        {
            rptwall.DataSource = dswall.Tables[0].AsEnumerable().Take(pagecount).CopyToDataTable();
            rptwall.DataBind();
        }
    }
    public string productUrlrewriting2(object cattype, object productname, object productid)
    {
        string strurl = "";
        string cat = "";
        try
        {
            cat = cattype.ToString();
            if (cat == "I")
            {
                strurl = UrlRewritingVM.getUrlRewritingInfo("", UrlRewritingVM.Encrypt(productid.ToString()), "PG");
            }
            else
            {
                strurl = UrlRewritingVM.getUrlRewritingInfo("", UrlRewritingVM.Encrypt(productid.ToString()), "PDID");
            }
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }
}
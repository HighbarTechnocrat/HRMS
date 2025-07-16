using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Xml;
using System.Text;
using System.IO;
using System.Data.Common;
using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;

public partial class newsdetails : System.Web.UI.Page
{
    private int newsid = 0, ID;
    public static int PageSize = 10, RecordCount;
    public static int pgi;
     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Int32.TryParse(Request.QueryString["id"], out ID))
        {
            if (Request.QueryString["id"] != "" && Request.QueryString["id"] != null)
            {
                newsid = Convert.ToInt32(UrlRewritingVM.Decrypt(Request.QueryString["id"].ToString()));
            }
        }
      
        if (!IsPostBack)
        {
                loaddata(1);
        }

    }
    public void loaddata(int PageIndex)
    {
        try
        {

            DataSet dtnews = classnews.newsdetails1(PageIndex, PageSize, out RecordCount);

            if (dtnews.Tables[0].Rows.Count>0)
            {
                pnlnews.Visible = true;
                rptrnews.DataSource = dtnews.Tables[0];
                rptrnews.DataBind();
            }
            this.PopulatePager(RecordCount, PageIndex);

        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }
    private void PopulatePager(int recordCount, int currentPage)
    {
        double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(PageSize));
        int pageCount = (int)Math.Ceiling(dblPageCount);
        List<ListItem> pages = new List<ListItem>();
        if (pageCount > 0)
        {
            for (int i = 1; i <= pageCount; i++)
            {
                pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
            }
            if (pageCount == 1)
            {
                rptPager.Visible = false;
            }
            else
            {
                rptPager.Visible = true;
            }
        }
        else
        {
            rptPager.Visible = true;
        }
        rptPager.DataSource = pages;
        rptPager.DataBind();
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        pgi = pageIndex;
        this.loaddata(pageIndex);

    }
    public string getnewsURL(object newsid)
    {
        string strurl = "";
        try
        {
            strurl = ReturnUrl("sitepathmain") + "newsdetail.aspx?newsid=" + UrlRewritingVM.Encrypt(newsid.ToString());
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class themes_creative_LayoutControls_latestnews3 : System.Web.UI.UserControl
{
     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;}
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getnews();
        }      
    }
    public void getnews()
    {
       // DataTable dtnews = classnews.gettopnewslist();
        DataSet dtnews = new DataSet();
        dtnews.ReadXml(ConfigurationManager.AppSettings["adminsitepath"]+ "xml/news.xml");
        if (dtnews.Tables.Count > 0)
        {
            if(dtnews.Tables[0].Rows.Count>6)
            {
                btnmore.Visible = true;
            }
            else
            {
                btnmore.Visible = false;
            }
            rptrnews.DataSource = dtnews;
            rptrnews.DataBind();
        }
        else
        {
            pnlnews.Visible = false;
        }
    }
    protected void btnmore_onclick(object sender, EventArgs e)
    {
       
    }
    protected void rptrnews_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }
    protected void rptrnews_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }
    protected void btnmore_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "news.aspx");
    }
}
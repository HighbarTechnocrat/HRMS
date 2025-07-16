using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class themes_creative : System.Web.UI.UserControl
{
     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loaddata();
        }
    }
    public void loaddata()
    {
        try
        {
            DataSet dtcat = new DataSet();
            dtcat.ReadXml(ConfigurationManager.AppSettings["adminsitepath"]+ "xml/cats.xml");
            if (dtcat.Tables.Count > 0)
            {
                bday.Visible = true;
                rptcat.DataSource = dtcat;
                rptcat.DataBind();
            }
            else
            {
                bday.Visible = false;
            }
        }
        catch(Exception ex)
        {

        }
    }
    public string getcategoryURL(object catname,object cid)
    {
        string strurl = "";
        try
        {
            strurl = UrlRewritingVM.getUrlRewritingInfo(catname.ToString().Trim(), UrlRewritingVM.Encrypt(cid.ToString().Trim()), "PS");
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

public partial class Themes_SecondTheme_LayoutControls_Promodefault : System.Web.UI.UserControl
{
     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                popularcontrol();
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    protected void popularcontrol()
    {
        try
        {
            creative.Common clsCommon = new creative.Common();
            DataSet ds = clsCommon.Read_XML("HomeBannermiddle.xml", ConfigurationManager.AppSettings["sitepathadmin"]+ "xml/");
            if(ds.Tables[0].Rows.Count>0)
            {
                rptpromo.DataSource = ds.Tables[0];
                rptpromo.DataBind();
            }
            else
            {
                topheadpromo.Visible = false;
            }
            
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

}
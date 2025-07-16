using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

public partial class themes_creative1_0_LayoutControls_interlace : System.Web.UI.UserControl
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
        catch ( Exception ex )
        {
	        ErrorLog.WriteError(ex.ToString());
        }
    }

    protected void popularcontrol()
    {
        try
        {
            creative.Common clsCommon = new creative.Common();
            DataSet ds = clsCommon.Read_XML("interlace.xml", ConfigurationManager.AppSettings["adminsitepath"]+ "Xml/");
            
            rptbanner.DataSource = ds.Tables[0];
            rptbanner.DataBind();
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

}
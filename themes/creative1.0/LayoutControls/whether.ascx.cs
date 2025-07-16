using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class themes_creative : System.Web.UI.UserControl
{
    public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;}
    protected void Page_Load(object sender, EventArgs e)
    {
        weather();
    }
    public void weather()
    {
        DataTable dt = Classuserwidget.getwidgetAdminStatus(2);
        if(dt.Rows.Count > 0)
        {
            pnlweather.Visible = true;
        }      
        else
        {
            pnlweather.Visible = false;
        }
    }
}
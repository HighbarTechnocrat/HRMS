using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

public partial class Components_Common_homebannercontrol : System.Web.UI.UserControl
{
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; } 
    protected void Page_init(object sender, EventArgs e)
    {
        
    }
}
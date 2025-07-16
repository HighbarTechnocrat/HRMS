using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class errorpage : System.Web.UI.Page
{
    public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} 
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
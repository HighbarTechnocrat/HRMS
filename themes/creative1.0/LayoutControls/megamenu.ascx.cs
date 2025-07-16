using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class themes_creative_LayoutControls_megamenu : System.Web.UI.UserControl
{
     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {
        WebClient client = new WebClient();
        /* add by krishna*/
        Stream stream = client.OpenRead(ConfigurationManager.AppSettings["sitepathadmin"]+ "Menu/megamenu.html");

        StreamReader sr = new StreamReader(stream);
        string content = sr.ReadToEnd();
        cmsmenu.Text = content;
    }
}
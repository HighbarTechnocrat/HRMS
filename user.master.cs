using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user : System.Web.UI.MasterPage
{
     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.User.Identity.IsAuthenticated)
        {
            Response.Redirect(ReturnUrl("sitepath") + "login.aspx?ReturnUrl=" + Request.RawUrl);
        }
        WebClient client = new WebClient();
        Stream stream = client.OpenRead(ConfigurationManager.AppSettings["sitepathadmin"]+ "Menu/footer.html");
        StreamReader sr = new StreamReader(stream);
        string content = sr.ReadToEnd();
        cms1.Text = content;
        if (Page.User.Identity.IsAuthenticated)
        {
        }
        else
        {
            Response.Redirect(ReturnUrl("sitepath")+"login");
        }
    }
}

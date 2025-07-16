using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;



public partial class errorpg : System.Web.UI.Page
{
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; } 
    protected void Page_Load(object sender, EventArgs e)
    {
        //string IPAdd = string.Empty;
        //IPAdd = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        //if (string.IsNullOrEmpty(IPAdd))
        //    IPAdd = Request.ServerVariables["REMOTE_ADDR"];
        //IPAdd = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        
        ////IPAdd = new WebClient().DownloadString("http://icanhazip.com").Replace("\n", "");
        //Response.Write(IPAdd);
        //Response.End();

        //WebClient client = new WebClient();
        //string ip = client.DownloadString("http://checkip.dyndns.org");//http://serverIp/PublicIP
        //Response.Write(ip);
        //Response.End();

  

    }
}
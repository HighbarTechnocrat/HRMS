using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Net;
using System.IO;
using System.Text;

public partial class themes_ssologin : System.Web.UI.UserControl
{
    string id = "", ssokey = "", ext_url="";
     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null && Request.QueryString["id"] != "" && Request.QueryString["ssokey"] != null && Request.QueryString["ssokey"] != "")
        {
            id = Request.QueryString["id"].ToString();
            ssokey = Request.QueryString["ssokey"].ToString();
            authenticate(id,ssokey);
        }
    }
    public void authenticate(string param1,string param2)
    {
        ext_url="http://portal.highbartech.com/UserManagement/SSOAuth.asmx";
        Uri myUri = new Uri(ext_url, UriKind.Absolute);
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(myUri);
        request.Method = "POST";
        request.KeepAlive = true;
        request.AllowAutoRedirect = false;
        request.Accept = "*/*";
        request.ContentType = "application/x-www-form-urlencoded";
        request.Headers.Add("User ID", "1000000338");
        request.Headers.Add("SSOKey", "zdVgp9vzoR");
        WebResponse resp = request.GetResponse();
        Stream imageStream = resp.GetResponseStream();
        Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
        StreamReader readStream = new StreamReader(imageStream, encode);
        string responseImage = readStream.ReadToEnd();
        readStream.Close();
        imageStream.Close();
        resp.Close();
    }
}
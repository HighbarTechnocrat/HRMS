using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Security;
using System.Data;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using System.Net.Mail;

public partial class themes_creative1_0_LayoutControls_homepopup : System.Web.UI.UserControl
{
    public static int i = 0;
    OpenIdRelyingParty openid = new OpenIdRelyingParty();
    private Random random = new Random();
     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
           

        }
    }
  
}
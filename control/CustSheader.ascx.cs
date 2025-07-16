using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Text;
using System.Web.Security;

public partial class control_CustSheader : System.Web.UI.UserControl
{
   
    string username = string.Empty;
    string m_flag = string.Empty;
    string online = string.Empty;
    public static string skey = "";
    

    
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (Page.User.Identity.IsAuthenticated)
        {   
            lblname.Visible = false;            
            if (!IsPostBack)
            {
                login();
                 
            }
        }
    }
            

    public void login()
    {
        MembershipUser user = Membership.GetUser(Page.User.Identity.Name.ToString().Trim());
        if (user != null)
        {
            online = user.IsOnline.ToString();
        }
    }
    

     
     

 
     

}
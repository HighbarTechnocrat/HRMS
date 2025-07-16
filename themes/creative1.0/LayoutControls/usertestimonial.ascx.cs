using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.IO;

public partial class themes_creative1_0_LayoutControls_usertestimonial : System.Web.UI.UserControl
{
     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnsend_Click(object sender, EventArgs e)
    {
        DateTime date = DateTime.Now;
       string username = Page.User.Identity.Name;
        classtestimonial.createtestimonial(txtname.Text,"", txtcomment.Text, date, "null", "P", username);
    }


}
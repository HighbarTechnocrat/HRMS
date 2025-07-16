using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class themes_creative : System.Web.UI.UserControl
{
    public static string username = "";
     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {
        if(Page.User.Identity.IsAuthenticated)
        {
            checksettings();
        }
        else
        {
            Response.Redirect(ConfigurationManager.AppSettings["intranetURL"].ToString().Trim());
        }
        //checkalive();
    }
    public void checksettings()
    {
        if (Page.User.Identity.IsAuthenticated)
        {
            username = Page.User.Identity.Name.ToString().Trim();
            DataTable dt = Classuserwidget.getuserwidget(username);
            if (dt.Rows.Count > 0)
            {
                ltjs.Text = ltjs.Text + "<script>jQuery(document).ready(function(){";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["flag"].ToString().Trim()=="F")
                    {
                        ltjs.Text = ltjs.Text + "jQuery('.lnkclose').each(function() {if(jQuery(this).attr('data')=='" + dt.Rows[i]["widget_title"].ToString().Trim() + "'){jQuery(this).parent().parent().css('display', 'none');}});";
                    }                    
                }
                ltjs.Text = ltjs.Text + "});</script>";
            }
        }
    }
    public void checkalive()
    {    
        HttpCookie cookie = HttpContext.Current.Request.Cookies["internet"];
        if(cookie!=null)
        {
            if(cookie.Value.ToString()=="true")
            {
                ltalive.Text = "<script>jQuery(function () {var imgSessionAliveSrc = 'http://portal.highbartech.com/sessionalive.ashx';jQuery('#imgSessionAlive').attr('src', imgSessionAliveSrc + '?' + new Date().getTime()); setInterval(function () { var imgSessionAliveSrc = 'http://portal.highbartech.com/sessionalive.ashx'; jQuery('#imgSessionAlive').attr('src', imgSessionAliveSrc + '?' + new Date().getTime())}, 60000);});</script>";
            }
            else
            {
                ltalive.Text = "<script>jQuery(function () {var imgSessionAliveSrc = 'http://hccportal/sessionalive.ashx';jQuery('#imgSessionAlive').attr('src', imgSessionAliveSrc + '?' + new Date().getTime()); setInterval(function () { var imgSessionAliveSrc = 'http://hccportal/sessionalive.ashx'; jQuery('#imgSessionAlive').attr('src', imgSessionAliveSrc + '?' + new Date().getTime())}, 60000);});</script>";
            }
        }
        else
        {
            ltalive.Text = "<script>jQuery(function () {var imgSessionAliveSrc = 'http://hccportal/sessionalive.ashx';jQuery('#imgSessionAlive').attr('src', imgSessionAliveSrc + '?' + new Date().getTime()); setInterval(function () { var imgSessionAliveSrc = 'http://hccportal/sessionalive.ashx'; jQuery('#imgSessionAlive').attr('src', imgSessionAliveSrc + '?' + new Date().getTime())}, 60000);});</script>";
        }
    }
}
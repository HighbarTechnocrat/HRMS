using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Components_Common_homepopup : System.Web.UI.UserControl
{
    public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {
        Control userpromobanner = LoadControl(String.Format("../../Themes/{0}/LayoutControls/homepopup.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
        userpromobanner.ID = "m_uxpromobanner";
        uxbrandpanel.Controls.Add(userpromobanner);
    }
}
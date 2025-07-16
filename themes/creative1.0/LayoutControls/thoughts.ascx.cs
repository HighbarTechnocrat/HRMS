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
    string user = null;

     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                user = Page.User.Identity.Name.ToString().Trim();
                loadthought();
            }
        }
        else
        {
            thoughts.Visible = false;
        }
    }
    public void loadthought()
    {
        try
        {
            //DataTable thgt = classnews.topthoughtList();
            DataTable dt = Classuserwidget.getwidgetAdminStatus(3);
            if(dt.Rows.Count > 0)
            {
                 DataTable dtuser = Classuserwidget.getwidgetUserStatus(user, 3);
                 if (dtuser.Rows.Count > 0)
                 {
                     DataSet thgt = new DataSet();
                     thgt.ReadXml(ConfigurationManager.AppSettings["adminsitepath"]+ "xml/thoughts.xml");
                     if (thgt.Tables.Count > 0)
                     {
                         thoughts.Visible = true;
                         rptthought.DataSource = thgt;
                         rptthought.DataBind();
                     }
                     else
                     {
                         thoughts.Visible = false;
                     }
                 }
                 else
            {
                thoughts.Visible = false;
            }
            }
            else
            {
                thoughts.Visible = false;
            }
        }
        catch (Exception ex)
        {
            //ErrorLog.WriteError(ex.ToString());
        }
    }

}
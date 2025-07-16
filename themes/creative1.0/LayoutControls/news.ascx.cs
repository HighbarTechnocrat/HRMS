using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

public partial class themes_creative1_LayoutControls_news : System.Web.UI.UserControl
{
  
     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
      

            if (!IsPostBack)
            {                
                    loaddata();               
            }
        }
        catch ( Exception ex )
        {
	        ErrorLog.WriteError(ex.ToString());
        }
    }

    public void loaddata()
    {
        try
        {
           
            DataTable dtnews = classnews.topNewsList();
           
            if (dtnews.Rows.Count > 0)
            {
                pnlnews.Visible = true;
                rptnews.DataSource = dtnews;
                rptnews.DataBind();
            }
            else
            {
                pnlnews.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

   

}
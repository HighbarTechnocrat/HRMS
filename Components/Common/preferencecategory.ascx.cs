using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;


public partial class Components_Common_preferencecategory : System.Web.UI.UserControl
{
    private void PopulateControl()
    {
       

        Control userpanel = LoadControl(String.Format("../../Themes/{0}/LayoutControls/catpreference.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
        userpanel.ID = "uxcategorypanel";
        uxcatpreferencepanel.Controls.Add(userpanel);
    }
    protected void Page_init(object sender, EventArgs e)
    {

        PopulateControl();

    }
}